using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Web.Services.Protocols;

namespace EZUtils
{

    public struct ExceptionSummary
    {
        public string Message;
        public string ExceptionType;
        public string Source;
    }

    public class EZException : ApplicationException
    {

        #region Constructors

        public EZException()
            : base()
        {
            zOnConstruction();
        }

        public EZException(string message)
            : base(message)
        {
            zOnConstruction();
        }

        public EZException(string message, Exception inner)
            : base(message, inner)
        {
            zOnConstruction();
        }


        #endregion Constructors

        #region Private Fields

        private Serialize mDetail;
        private Exception mTopNonEZException;
        private Exception mBottomEZException;
        private const string CRYPT_KEY = "EZ-SOAP@KEY!";
        private const string EZEXCEPTION_XMLNAMESPACE = ""; //"http://www.ezsoftware.com/EZException/";
        private bool mEncryptXML = false;

        #endregion

        #region Private

        private void zInitDetail()
        {
            try
            {
                if (mDetail == null)
                {
                    mDetail = new Serialize("Detail", "http://www.ezsoftware.com/ezException/");
                }
            }
            catch (Exception e)
            {
                e.Source = "EZException.zInitDetail - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }
        }

        private void zOnConstruction()
        {
            try
            {
                // Get the name of the method that threw the exception
                // ASSUMPTION: this method will only give the correct name if called from the expected next level.
                // It is intended to be called from the constructors.
                MethodBase b = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod();
                this.Source = b.DeclaringType.FullName + '.' + b.Name;
                EZUtils.Trace.WriteLine("EZException: " + this.Message, this.Source, true);
            }
            catch (Exception e)
            {
                e.Source = "EZException.zOnCOnstruction - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
                try
                {
                    EZUtils.Trace.WriteLine("EZException: " + this.Message, "?", true);
                }
                catch { }
            }

            // Keep track of the bottom EZException and top non-EZException
            try
            {
                if (InnerException != null)
                {
                    if (IsEZException(InnerException))
                    {
                        mTopNonEZException = (InnerException as EZException).mTopNonEZException;
                        mBottomEZException = (InnerException as EZException).mBottomEZException;
                        if (mBottomEZException == null)
                        {
                            mBottomEZException = (InnerException as EZException);
                        }
                    }
                    else if (IsSoapException(InnerException))
                    {
                        XmlNode SoapDetail = (InnerException as SoapException).Detail;
                        if (SoapDetail != null)
                        {
                            XmlNode tmp;
                            tmp = SoapDetail.SelectSingleNode("BottomEZException");
                            if (tmp != null)
                            {
                                mBottomEZException = zNodeToEZException(tmp);
                            }
                            tmp = SoapDetail.SelectSingleNode("TopNonEZException");
                            if (tmp != null)
                            {
                                mTopNonEZException = zNodeToEZException(tmp);
                            }
                        }
                    }
                    else
                    {
                        mTopNonEZException = this.InnerException;
                        try
                        {
                            EZUtils.Trace.WriteLine("Top Non-EZException: " + this.InnerException.Message, this.InnerException.Source, true);
                        }
                        catch { }
                    }
                }
            }
            catch (Exception e)
            {
                e.Source = "EZException.zOnCOnstruction - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
                // This should 'never' fail (but if it does we won't crash)
            }
        }

        private Exception zNodeToEZException(XmlNode Node)
        {
            try
            {
                string message = "Unknown";
                XmlNode messageNode = Node.SelectSingleNode("Message");
                if (messageNode != null)
                {
                    message = messageNode.InnerText;
                }
                Exception newException = new Exception(message);

                XmlNode sourceNode = Node.SelectSingleNode("Source");
                if (sourceNode != null)
                {
                    newException.Source = sourceNode.InnerText;
                }

                // We cannot set the exception type. It may not even exist in the current context
                // store the type in the data section
                XmlNode exceptionTypeNode = Node.SelectSingleNode("ExceptionType");
                if (exceptionTypeNode != null)
                {
                    newException.Data.Add("ExceptionType", exceptionTypeNode.InnerText);
                }
                return newException;
            }
            catch (Exception e)
            {
                e.Source = "EZException.zNodeToEZException - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }
            return null;
        }

        private XmlNode zAsXmlNode(String Name, String Namespace)
        {
            try
            {
                Serialize s = new Serialize(Name, Namespace);

                ExceptionSummary ThisException = new ExceptionSummary();
                ThisException.Message = this.Message;
                ThisException.ExceptionType = this.GetType().Name;
                ThisException.Source = this.Source;
                s.Add("TopException", ThisException);

                if (mBottomEZException != null)
                {
                    s.Add("BottomEZException", BottomEZExceptionSummary);
                }

                if (mTopNonEZException != null)
                {
                    s.Add("TopNonEZException", TopNonEZExceptionSummary);
                }

                if (mEncryptXML)
                {
                    s.Add(this, CRYPT_KEY);
                    s.AddEnvironment(CRYPT_KEY);
                }
                else
                {
                    s.Add(this);
                    s.AddEnvironment();
                }


                return (s.Root as XmlNode);
            }
            catch (Exception e)
            {
                e.Source = "EZException.zAsXmlNode - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
                return null;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Boolean IsEZException(Exception e)
        {
            return (typeof(EZException).IsAssignableFrom(e.GetType()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Boolean IsSoapException(Exception e)
        {
            return (typeof(SoapException).IsAssignableFrom(e.GetType()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EZExceptionOuterXML"></param>
        /// <param name="EZExceptionSummary"></param>
        /// <param name="BottomEZExceptionSummary"></param>
        /// <param name="TopNonEZExceptionSummary"></param>
        public static void ExtractSummaryFromExceptionXML(
              string EZExceptionOuterXML,
              out ExceptionSummary EZExceptionSummary,
              out ExceptionSummary BottomEZExceptionSummary,
              out ExceptionSummary TopNonEZExceptionSummary)
        {
            XmlDocument xdoc = null;
            XmlNode root = null;
            String s = string.Empty;

            EZExceptionSummary = new ExceptionSummary();
            BottomEZExceptionSummary = new ExceptionSummary();
            TopNonEZExceptionSummary = new ExceptionSummary();

            try
            {
                xdoc = new XmlDocument();
                s = EZUtils.Serialize.Decrypt(EZExceptionOuterXML, "EZ-SOAP@KEY!");
                xdoc.LoadXml(s);
                root = xdoc.DocumentElement;

                EZExceptionSummary.Message = root.SelectSingleNode("TopException/Message").InnerText;
                EZExceptionSummary.ExceptionType = root.SelectSingleNode("TopException/ExceptionType").InnerText;
                EZExceptionSummary.Source = root.SelectSingleNode("TopException/Source").InnerText;

                BottomEZExceptionSummary.Message = root.SelectSingleNode("BottomEZException/Message").InnerText;
                BottomEZExceptionSummary.ExceptionType = root.SelectSingleNode("BottomEZException/ExceptionType").InnerText;
                BottomEZExceptionSummary.Source = root.SelectSingleNode("BottomEZException/Source").InnerText;

                TopNonEZExceptionSummary.Message = root.SelectSingleNode("TopNonEZException/Message").Value;
                TopNonEZExceptionSummary.ExceptionType = root.SelectSingleNode("TopNonEZException/ExceptionType").InnerText;
                TopNonEZExceptionSummary.Source = root.SelectSingleNode("TopNonEZException/Source").InnerText;
            }
            catch
            {
                // Take as much info as you can get...
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ExceptionSummary BottomEZExceptionSummary
        {
            get
            {
                ExceptionSummary BottomEZException = new ExceptionSummary();
                try
                {
                    if (mBottomEZException != null)
                    {
                        BottomEZException.Message = mBottomEZException.Message;
                        BottomEZException.ExceptionType = "EZException";
                        BottomEZException.Source = mBottomEZException.Source;
                    }
                    else
                    {
                        BottomEZException.Message = "NA";
                        BottomEZException.ExceptionType = "NA";
                        BottomEZException.Source = "NA";
                    }
                }
                catch (Exception e)
                {
                    e.Source = "EZException.BottomEZExceptionSummary - " + e.Source;
                    EZUtils.EventLog.WriteErrorEntry(e);
                }
                return BottomEZException;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ExceptionSummary TopNonEZExceptionSummary
        {
            get
            {
                ExceptionSummary TopNonEZException = new ExceptionSummary();
                try
                {
                    if (mTopNonEZException != null)
                    {
                        TopNonEZException.Message = mTopNonEZException.Message;
                        TopNonEZException.ExceptionType = mTopNonEZException.GetType().Name;
                        if (TopNonEZException.ExceptionType == "Exception" && mTopNonEZException.Data.Contains("ExceptionType"))
                        {
                            TopNonEZException.ExceptionType = mTopNonEZException.Data["ExceptionType"].ToString();
                        }
                        TopNonEZException.Source = mTopNonEZException.Source;
                    }
                    else
                    {
                        TopNonEZException.Message = "NA";
                        TopNonEZException.ExceptionType = "NA";
                        TopNonEZException.Source = "NA";
                    }
                }
                catch (Exception e)
                {
                    e.Source = "EZException.TopNonEZExceptionSummary - " + e.Source;
                    EZUtils.EventLog.WriteErrorEntry(e);
                }
                return TopNonEZException;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public XmlNode Detail
        {
            get
            {
                try
                {
                    zInitDetail();
                    return mDetail.Root;
                }
                catch (Exception e)
                {
                    e.Source = "EZException.Detail - " + e.Source;
                    EZUtils.EventLog.WriteErrorEntry(e);
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EncryptXML
        {
            get { return mEncryptXML; }
            set { mEncryptXML = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public XmlNode AsSoapDetail
        {
            get
            {
                try
                {
                    return zAsXmlNode(SoapException.DetailElementName.Name,
                        SoapException.DetailElementName.Namespace);
                }
                catch (Exception e)
                {
                    e.Source = "EZException.AsSoapDetail - " + e.Source;
                    EZUtils.EventLog.WriteErrorEntry(e);
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string InnerXml
        {
            get
            {
                try
                {
                    return zAsXmlNode("EZException", EZEXCEPTION_XMLNAMESPACE).InnerXml;
                }
                catch (Exception e)
                {
                    e.Source = "EZException.InnerXml - " + e.Source;
                    EZUtils.EventLog.WriteErrorEntry(e);
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OuterXml
        {
            get
            {
                try
                {
                    return zAsXmlNode("EZException", EZEXCEPTION_XMLNAMESPACE).OuterXml;
                }
                catch (Exception e)
                {
                    e.Source = "EZException.OuterXml - " + e.Source;
                    EZUtils.EventLog.WriteErrorEntry(e);
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        public void Add(object o)
        {
            try
            {
                zInitDetail();
                mDetail.Add(o);
            }
            catch (Exception e)
            {
                e.Source = "EZException.Add - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="o"></param>
        public void Add(string label, object o)
        {
            try
            {
                zInitDetail();
                mDetail.Add(label, o);
            }
            catch (Exception e)
            {
                e.Source = "EZException.Add - " + e.Source;
                EZUtils.EventLog.WriteErrorEntry(e);
            }
        }

        #endregion
    }
}