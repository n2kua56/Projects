using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Web.Services.Protocols;

namespace EZUtils
{

    public class Serialize
    {
        #region Private Fields

        private XmlDocument mDocument;
        private System.Xml.XmlNode mNode;

        #endregion

        #region Private

        private void zAdd(XmlNode Parent, string Name, object Value, Boolean IncludeObjects)
        {
            zAdd(Parent,Name,Value,IncludeObjects,"");
        }

        private void zAdd(XmlNode Parent, string Name, object Value, Boolean IncludeObjects, string EncryptKey)
        {
            // Remove illegal ' ', [ and ]
            Name = Name.Replace("[", "");
            Name = Name.Replace("]", "");
            Name = Name.Replace(" ", "");

            XmlNode Child = mDocument.CreateNode(XmlNodeType.Element, Name, mNode.NamespaceURI);

            try
            {
                if (Value == null)
                {
                    AddScalar(Child, "value is null");
                }
                else
                {
                    Type t = Value.GetType();

                    if (t.IsEnum || t.IsPrimitive || t.ToString() == "System.String" || t.ToString() == "System.DateTime")
                    {
                        AddScalar(Child, Value.ToString());
                    }
                    else if (t.IsArray)
                    {
                        AddArray(Child, Value as Array);
                    }
                    else if (typeof(ArrayList).IsAssignableFrom(t))
                    {
                        AddArrayList(Child, Value as ArrayList);
                    }
                    else if (typeof(CollectionBase).IsAssignableFrom(t))
                    {
                        AddCollectionBase(Child, Value as CollectionBase);
                    }
                    else if (typeof(Exception).IsAssignableFrom(t))
                    {
                        AddException(Child, Value as Exception);
                    }
                    else if (typeof(IDictionary).IsAssignableFrom(t))
                    {
                        AddIDictionary(Child, Value as IDictionary);
                    }
                    else if (!IncludeObjects)
                    {
                        return;
                    }
                    else
                    {
                        //Struct, Object or something else we did not catch.
                        AddObject(Child, Value);
                    }
                }

                Boolean Encrypt = (EncryptKey != "");
                if (Encrypt)
                {
                    Child = EncryptNode(Child, EncryptKey);
                }

                Parent.AppendChild(Child);
            }
            catch
            {
                try
                {
                    AddScalar(Parent, "Failed to add " + Name);
                }
                catch { }
            }
        }

        private XmlNode EncryptNode(XmlNode Node, string Key)
        {
            try
            {
                XmlNode CryptChild = mDocument.CreateNode(XmlNodeType.Element, Node.Name, Node.NamespaceURI);
                Cryptography crypt = new Cryptography(Key);
                CryptChild.InnerText = crypt.Encrypt(Node.InnerXml);
                XmlAttribute attribute = mDocument.CreateAttribute(ENCRYPT_ATTRIBUTE);
                //attribute.InnerText = true.ToString();
                CryptChild.Attributes.Append(attribute);
                return CryptChild;
            }
            catch (Exception e)
            {
                e.Source = "Serialize.EncryptNode - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
                return Node; //Nasty not encrypted!
            }
        }

        private void AddIDictionary(XmlNode Parent, IDictionary Value)
        {
            try
            {
                foreach (string key in Value.Keys)
                {
                    zAdd(Parent, key, Value[key], true);
                }
            }
            catch
            {
                try
                {
                    AddScalar(Parent, "Failed to add dictionary");
                }
                catch { }
            }
        }

        private void AddException(XmlNode Parent,  Exception Value)
        {
            try
            {
                if (Value == null)
                {
                    return;
                }

                zAdd(Parent, "Message", Value.Message, false);
                zAdd(Parent, "Source", Value.Source, false);
                zAdd(Parent, "Data", Value.Data, true);
                zAdd(Parent, "StackTrace", Value.StackTrace, true); //will be null (and ignored) if not yet thrown
                
                if (EZException.IsEZException(Value))
                {
                    XmlNode Child = mDocument.ImportNode((Value as EZException).Detail, true);
                    Parent.AppendChild(Child);
                }
                else if (EZException.IsSoapException(Value))
                {
                    if ((Value as SoapException).Detail != null)
                    {
                        XmlNode Child = mDocument.ImportNode((Value as SoapException).Detail, true);
                        Parent.AppendChild(Child);
                    }
                }

             
                if (Value.InnerException != null)
                {
                    XmlNode NextChild = mDocument.CreateNode(XmlNodeType.Element, Value.InnerException.GetType().Name, Parent.NamespaceURI);
                    AddException(NextChild,  Value.InnerException);
                    Parent.AppendChild(NextChild);
                }
            }
            catch
            {
                try
                {
                    AddScalar(Parent, "Failed to add exception");
                }
                catch { }
            }
        }

        private void AddScalar(XmlNode Parent,  object Value)
        {
            try
            {
                Parent.InnerText = Value.ToString();
            }
            catch (Exception e)
            {
                e.Source = "Serialize.AddScalar - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }

        }

        private void AddArray(XmlNode Parent,  Array Value)
        {
            try
            {
                switch (Value.Rank)
                {
                    case 1:
                        for (int i = 0; i < Value.Length; i++)
                        {
                            zAdd(Parent,  "_" + i.ToString(), Value.GetValue(i), false);
                        }
                        break;

                    case 2:
                        for (int i = 0; i < Value.Length; i++)
                        {
                            int length = Value.GetLength(i);
                            for (int j = 0; i < length; j++)
                            {
                                zAdd(Parent,  "_" + i.ToString() + "_" + j.ToString(), Value.GetValue(i,j), false);
                            }
                        }
                        break;

                   default:
                        // to many dimensions. Give up
                        AddScalar(Parent,Value);
                        break;
                }
            }
            catch
            {
                try
                {
                    AddScalar(Parent, "Failed to add array");
                }
                catch { }
            }

        }

        private void AddCollectionBase(XmlNode Parent,  CollectionBase Value)
        {
            try
            {
                foreach (object member in Value)
                {
                    zAdd(Parent, member.GetType().Name, member, false);
                }
            }
            catch
            {
                try
                {
                    AddScalar(Parent, "Failed to add collectionbase");
                }
                catch { }
            }

        }

        private void AddArrayList(XmlNode Parent,  ArrayList Value)
        {
            try
            {
                foreach (object member in Value)
                {
                    zAdd(Parent, member.GetType().Name, member, false);
                }
            }
            catch
            {
                try
                {
                    AddScalar(Parent, "Failed to add array");
                }
                catch { }
            }

        }
        private void AddObject(XmlNode Parent,  object Value)
        {
            try
            {
                MemberInfo[] members = Value.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.GetProperty);
                foreach (MemberInfo member in members)
                {
                    if (member is FieldInfo)
                    {
                        FieldInfo info = member as FieldInfo;
                        zAdd(Parent, member.Name, info.GetValue(Value), false);
                    }

                    if (member is PropertyInfo)
                    {
                        PropertyInfo info = member as PropertyInfo;
                        zAdd(Parent, member.Name, info.GetValue(Value, null), false);
                    }
                }
            }
            catch
            {
                try
                {
                    AddScalar(Parent, "Failed to add object");
                }
                catch { }
            }
        }
        #endregion

        #region Public

        public const string ENCRYPT_ATTRIBUTE = "encrypted";

        public Serialize(String Name, String Namespace)
        {
            mDocument = new System.Xml.XmlDocument();
            mNode = mDocument.CreateNode(XmlNodeType.Element, Name, Namespace);
        }

        public static string Decrypt(String EncryptedEZExceptionAsXML, string key)
        {
            if (EncryptedEZExceptionAsXML == "")
            {
                return "";
            }

            try
            {
                XmlDocument document = new XmlDocument();

                document.LoadXml(EncryptedEZExceptionAsXML);

                if (document.DocumentElement.Name == "MsgPack")
                {
                    zDecryptVB6MsgPack(document, key);
                }
                else
                {
                    Decrypt(document.DocumentElement, key);
                }
                return document.DocumentElement.OuterXml;
            }
            catch (Exception e)
            {
                e.Source = "Serialize.Decrypt - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }
            return EncryptedEZExceptionAsXML;
       }

        private static void zDecryptVB6MsgPack(XmlDocument document, string key)
        {
            // vb6 msgpack
            XmlNodeList list = null;

            
            EZUtils.Trace.Enter("Serialize.zDecryptVB6MsgPack");

            try
            {
                list = document.SelectNodes("//Detail[starts-with(@ItemVal, '<EZException')]");
                for (int i = 0; i < list.Count; i++)
                {
                    String Decrypted = Decrypt(list[i].Attributes["ItemVal"].Value, key);
                    list[i].InnerXml = Decrypted;
                    list[i].Attributes["ItemVal"].Value = "Decrypted. See below";
                }
            }
            catch (Exception ex)
            {
                EZUtils.EZException EZEx = new EZUtils.EZException("Failed.", ex);
                throw EZEx;
            }
            finally
            {
                EZUtils.Trace.Exit("Serialize.zDecryptVB6MsgPack");
            }
        }

        public static void Decrypt(XmlNode Node, string Key)
        {
            if (Node == null)
            {
                return;
            }
            try
            {
                if (Node.NodeType == XmlNodeType.Element)
                {
                    Cryptography crypt = new Cryptography(Key);
                    Decrypt((Node as XmlElement), crypt);
                }
            }
            catch (Exception e)
            {
                e.Source = "Serialize.Decrypt - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }
       }

        private static void Decrypt(XmlElement Element, Cryptography crypt)
        {
            try
            {
                XmlNode Encrypted = Element.Attributes.GetNamedItem(ENCRYPT_ATTRIBUTE);

                if (Encrypted != null)
                {
                    Element.InnerXml = crypt.Decrypt(Element.InnerXml);
                    Element.Attributes.Remove((Encrypted as XmlAttribute));
                }

                foreach (XmlNode child in Element.ChildNodes)
                {
                    if (child.NodeType == XmlNodeType.Element)
                    {
                        Decrypt((child as XmlElement), crypt);
                    }
                }
            }
            catch(Exception e)
            {
                e.Source = "Serialize.Decrypt - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }
        }

        public XmlNode Root
        {
            get { return mNode; }
        }

        public void Add(string Name, object Value)
        {
            try
            {
                if (Value == null)
                {
                    zAdd(mNode, Name, Value, true);
                }
                else
                {
                    zAdd(mNode, Name, Value, true);
                }
            }
            catch(Exception e)
            {
                e.Source = "Serialize.Add - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }
        }

        public void Add(object Value)
        {
            try
            {
                if (Value == null)
                {
                    zAdd(mNode, "null", Value, true);
                }
                else
                {
                    zAdd(mNode, Value.GetType().Name, Value, true);
                }
            }
            catch (Exception e)
            {
                e.Source = "Serialize.Add - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }
        }

        public void Add(string Name, object Value, string EncryptKey)
        {
            try
            {
                zAdd(mNode, Name, Value, true, EncryptKey);
            }
            catch (Exception e)
            {
                e.Source = "Serialize.Add - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }

        }

        public void Add(object Value, string EncryptKey)
        {
            try
            {
                zAdd(mNode, Value.GetType().Name, Value, true, EncryptKey);
            }
            catch (Exception e)
            {
                e.Source = "Serialize.Add - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }
        }

        public void AddEnvironment()
        {
            AddEnvironment("");
        }

        public void AddEnvironment(string EncryptKey)
        {
            try
            {
                XmlNode Child = mDocument.CreateNode(XmlNodeType.Element, "Enviroment", mNode.NamespaceURI);

                zAdd(Child, "Date", System.DateTime.Now.ToLongDateString(), false);
                zAdd(Child, "Time", System.DateTime.Now.ToLongTimeString(), false);
                zAdd(Child, "MachineName", Environment.MachineName, false);
                zAdd(Child, "OSVersion", Environment.OSVersion, false);
                zAdd(Child, "UserDomainName", Environment.UserDomainName, false);
                zAdd(Child, "UserName", Environment.UserName, false);
                zAdd(Child, "StackTrace", Environment.StackTrace, false);
                zAdd(Child, "CLRVersion", Environment.Version, false);
                zAdd(Child, "EnvironmentVariables", Environment.GetEnvironmentVariables(), true);

                Boolean Encrypt = (EncryptKey != "");
                if (Encrypt)
                {
                    Child = EncryptNode(Child, EncryptKey);
                }
                mNode.AppendChild(Child);
            }
            catch (Exception e)
            {
                e.Source = "Serialize.AddEnvironment - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }
        }

        public void AddNode(XmlNode Node)
        {
            try
            {
                XmlNode Child = mDocument.ImportNode(Node, true);
                mNode.AppendChild(Child);
            }
            catch (Exception e)
            {
                e.Source = "Serialize.AddNode - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }
        }

        #endregion

    }
}
