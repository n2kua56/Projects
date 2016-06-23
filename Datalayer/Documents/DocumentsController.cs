using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EZDeskDataLayer.User.Models;
using EZUtils;

namespace EZDeskDataLayer.Documents
{
    public class DocumentsController : Controller
    {
        private string mModName = "DocumentsController";
        private EZDeskCommon mCommon;
        private MySqlConnection mConn;

        public DocumentsController(EZDeskCommon common /*MySqlConnection conn*/)
        {
            Trace.Enter(Trace.RtnName(mModName, "DocumentsController-Constructor"));
            mCommon = common;
            mConn = mCommon.Connection;
            Init(mCommon.Connection);
            Trace.Exit(Trace.RtnName(mModName, "DocumentsController-Constructor"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabId"></param>
        /// <returns></returns>
        public DataTable GetDocumentsForTab(int personId, int tabId)
        {
            DataTable dt = null;
            string step = "";
            string sql = "";

            Trace.Enter(Trace.RtnName(mModName, "GetDocumentsForTab"));

            try
            {
                step = "Build querry";
                sql = "SELECT `docId`, `PersonId`, `TabId`, `Created`, `IsActive`, " +
                            "CONCAT(`docName`, ' ', `Created`) AS `docName`, `docFullPathName`, `GroupRestriction` " +
                        "FROM `doc_documents` " +
                        "WHERE `PersonId`=@pid " +
                            "AND `TabId`=@tabid ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@pid", personId));
                cmd.Parameters.Add(new MySqlParameter("@tabid", tabId));

                //Get the requested data into a databable.
                step = "Get data";
                dt = GetDataTable(cmd);

                return dt;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetDocumentsForTab failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("personId", personId);
                eze.Add("tabId", tabId);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetDocumentsForTab"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="transaction"></param>
        public void WriteDocumnet(EZDeskDataLayer.Documents.Models.documentDetail doc, MySqlTransaction transaction)
        {
            string sql = "";
            MySqlCommand cmd = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "WriteDocumnet"));
            try
            {
                step = "Build querry";
                if (doc.Id > 0)
                {
                    sql = "UPDATE `doc_documents` SET " +
                                "`PersonId`=@personid, " +
                                "`TabId`=@tabid, " +
                                "`Created`=@created, " +
                                "`IsActive`=@active, " +
                                "`docName`=@name, " +
                                "`docFullPathName`=@path, " +
                                "`GroupRestriction`=@group " +
                            "WHERE Id=@id ";
                    cmd = new MySqlCommand(sql, mConn, transaction);
                    cmd.Parameters.Add(new MySqlParameter("@id", doc.Id));
                }

                else
                {
                    sql = "INSERT INTO `doc_documents` (`PersonId`, " +
                                "`TabId`, `Created`, `IsActive`, `docName`, " +
                                "`docFullPathName`, `GroupRestriction`) " +
                            "VALUES(@personid, @tabid, @created, @active, @name, " +
                                   "@path, @group)";
                    cmd = new MySqlCommand(sql, mConn, transaction);
                }

                cmd.Parameters.Add(new MySqlParameter("@personid", doc.PersonId));
                cmd.Parameters.Add(new MySqlParameter("@tabid", doc.TabId));
                cmd.Parameters.Add(new MySqlParameter("@created", doc.Created));
                cmd.Parameters.Add(new MySqlParameter("@active", doc.IsActive));
                cmd.Parameters.Add(new MySqlParameter("@name", doc.Name));
                cmd.Parameters.Add(new MySqlParameter("@path", doc.PathName));
                cmd.Parameters.Add(new MySqlParameter("@group", doc.GroupRestriction));

                step = "Write data";
                int rc = ExecuteNonQueryCmd(cmd);
                if (doc.Id < 1)
                {
                    step = "Get LastInsertedId";
                    doc.Id = (int)cmd.LastInsertedId;
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("WriteDocument failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("doc", doc);
                throw eze;
            }

            finally
            {
                Trace.Enter(Trace.RtnName(mModName, "WriteDocument"));
            }
        }

        /// <summary>
        /// The caller has a file (fullpathname), a name for the file in the document record,
        /// the type of drag-drop operation (Move/Copy), the id of the tab we are adding the
        /// the document too, and common has the selected person and the user performing the
        /// operation.
        /// We need to:
        ///  1) Make sure the file exists
        ///  2) Make sure it is a valid extention
        ///  3) Build the destination fullpathFileName from the source fullpathFileName.
        ///  4) Move or Copy the source file to the destination
        ///  5) Create the document record.
        /// </summary>
        /// <param name="fullpathfilename"></param>
        /// <param name="Name"></param>
        /// <param name="dragType"></param>
        /// <param name="tabId"></param>
        /// <param name="common"></param>
        /// <returns></returns>
        public Models.documentDetail AddFile(string fullpathfilename, string Name, string dragType, int tabId, EZDeskCommon common)
        {
            Models.documentDetail rtn = new Models.documentDetail();
            string ext = "";
            FileInfo fi = null;
            string destFileName = "";
            bool cont = true;
            EZDeskDataLayer.ehr.ehrCtrl eCtrl = new ehr.ehrCtrl(mCommon);

            Trace.Enter(Trace.RtnName(mModName, "AddFile"));

            try
            {
                //The file MUST exists -- This should ALWAYS be true since the ListView was built frm 
                // the directory OR we are dragging files directly from a System Directory, BUT someone 
                // or some other process may be deleting files... so the check is necessary!
                fi = new FileInfo(fullpathfilename);
                if (!fi.Exists)
                {
                    rtn.DocumentError = "File: " + fullpathfilename + " doesn't exists.";
                }

                else
                {
                    //The file extention must be one of the extentions that we recognize! Since we 
                    // control this in the ListView it should already be true, BUT when the user is
                    // dragging from a System Directory... could be anything
                    ext = fi.Extension.ToLower();
                    if ((ext != ".jpg") && (ext != ".pdf") &&
                        (ext != ".txt") && (ext != ".doc") &&
                        (ext != ".xsl") && (ext != ".odt") &&
                        (ext != ".ods") && (ext != ".docx"))
                    {
                        rtn.DocumentError = "File: " + fullpathfilename + " invalid extention.";
                    }

                    //This file passes all the tests, Do the MOVE or COPY.
                    else
                    {
                        //Build the destination fullpathFileName.
                        destFileName = zBuildDocumentFileName(common.Person.LastName, 
                                common.Person.FirstName, 
                                fullpathfilename);

                        //Now either Move or Copy the source file to the destination file. If
                        // the dragType is invalid we don't do anything (shouldn't happen).
                        try
                        {
                            switch (dragType.ToLower())
                            {
                                case "copy":
                                    fi.CopyTo(destFileName);
                                    break;
                                case "move":
                                    fi.MoveTo(destFileName);
                                    break;
                                default:
                                    cont = false;
                                    break;
                            }
                            if (cont)
                            {
                                fi = new FileInfo(destFileName);
                                if (!fi.Exists)
                                {
                                    rtn.DocumentError = "File: " + destFileName + " didn't copy.";
                                    cont = false;
                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                            cont = false;
                        }

                        if (cont)
                        {
                            try
                            {
                                //Finally, if all went well above, we need to write the Document
                                //record so that we can find this document later.
                                EZDeskDataLayer.Documents.Models.documentDetail doc =
                                    new EZDeskDataLayer.Documents.Models.documentDetail();
                                doc.Created = DateTime.Now;
                                doc.Description = "";
                                doc.GroupRestriction = 0;
                                doc.Id = -1;
                                doc.IsActive = true;
                                doc.PathName = destFileName;
                                doc.PersonId = common.Person.PersonID;
                                doc.TabId = tabId;
                                doc.UserId = common.User.UserSecurityID;
                                doc.Name = Name;
                                WriteDocumnet(doc, null);
                            }

                            catch (Exception ex)
                            {
                                File.Delete(destFileName);
                                throw ex;
                            }
                        }
                    }
                }

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("AddFile failed", ex);
                eze.Add("fullpathfilename", fullpathfilename);
                eze.Add("Name", Name);
                eze.Add("dragType", dragType);
                eze.Add("tabId", tabId);
                eze.Add("destFileName", destFileName);
                if (common != null)
                {
                    if (common.Person != null) { eze.Add("PersonId", common.Person.PersonID); }
                    if (common.User != null) { eze.Add("UserSecurityId", common.User.UserSecurityID); }
                }
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "AddFile"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastName">Persons last name - used to build the directory where the document goes</param>
        /// <param name="firstname">Persons first name - used to build the directory where the document goes</param>
        /// <param name="fullpathFileName">Source Full Path File Name</param>
        /// <returns>The fullpath name for the document to be saved under</returns>
        private string zBuildDocumentFileName(string lastName, string firstname, string sourceFullPathFileName)
        {
            EZDeskDataLayer.ehr.ehrCtrl eCtrl = new ehr.ehrCtrl(mCommon);
            DirectoryInfo di = null;
            FileInfo fi = null;

            //Following used to generate destination path
            string destFileName = "";
            string path = "";
            string dir = "";

            //Extracts the name and extention of the source file.
            string sourceFileName = "";
            string sourceExtention = "";

            //Used in generating a unique name (nnn appended if necessary).
            int idx = -1;
            bool cont = true;
            string testname = "";

            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "zBuildDocumentFileName"));

            try
            {
                //Get the SourceFile filename (no extention) and the extention.
                step = "Extract file name and extention from source file";
                fi = new FileInfo(sourceFullPathFileName);
                sourceFileName = fi.Name;
                sourceExtention = fi.Extension;
                sourceFileName = sourceFileName.Substring(0, (sourceFileName.Length - sourceExtention.Length));

                //Get the path (directory) where this document file will exist! This will be the combination
                // of the ServerPath and DocumentBase in the available properties. To that we add the first
                // leter (if any) of the Persons last name and Persons first name.  This is done to try and
                // spread the files out over some number of directories instead of all in one.
                step = "Build path name";
                path = Path.Combine(eCtrl.GetProperty("ServerPath"), eCtrl.GetProperty("DocumentBase"));
                dir = "";
                if (lastName.Length > 0) { dir += lastName.Substring(0, 1); }
                if (firstname.Length > 0) { dir += firstname.Substring(0, 1); }
                path = Path.Combine(path, dir);
                di = new DirectoryInfo(path);
                if (!di.Exists) { di.Create(); }

                //Now we need to work on the file name it's self. This is the Proposed file name (either the
                // pc filename portion of the original file, OR the tab name. To this will will add a number
                // if adding with this name would become a duplicate.
                step = "Build file name";
                cont = true;
                destFileName = "";
                //Find the next filename that doesn't exist!
                for (idx = -1; cont; idx++)
                {
                    testname = sourceFileName;
                    if (idx > -1) { testname += "." + idx.ToString("D4"); }   //Don't add the 00 until there is already one
                    testname += sourceExtention;
                    destFileName = Path.Combine(path, testname);
                    fi = new FileInfo(destFileName);
                    if (!fi.Exists) { cont = false; }
                }

                return destFileName;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zBuildDocumentFileName failed", ex);
                eze.Add("step", step);
                eze.Add("lastName", lastName);
                eze.Add("firstname", firstname);
                eze.Add("destFileName", destFileName);
                eze.Add("sourceFileName", sourceFileName);
                eze.Add("sourceExtention", sourceExtention);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zBUildDocumentFileName"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EZDeskDataLayer.Documents.Models.documentDetail GetDocumentById(int id)
        {
            DataTable dt = null;
            string step = "";
            string sql = "";

            Trace.Enter(Trace.RtnName(mModName, "GetDocumentsForTab"));

            try
            {
                step = "Build querry";
                sql = "SELECT `docId`, `PersonId`, `TabId`, `Created`, `IsActive`, " +
                            "`docName`, `docFullPathName`, `GroupRestriction` " +
                        "FROM `doc_documents` " +
                        "WHERE `docId`=@id ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@id", id));

                //Get the requested data into a databable.
                step = "Get data";
                dt = GetDataTable(cmd);

                step = "Pull data";
                EZDeskDataLayer.Documents.Models.documentDetail doc = null;
                if (dt.Rows.Count == 1)
                {
                    doc = new Models.documentDetail();
                    DataRow dr = dt.Rows[0];
                    doc.Id = Convert.ToInt32(dr["docId"].ToString());
                    doc.PersonId = Convert.ToInt32(dr["PersonId"].ToString());
                    doc.TabId = Convert.ToInt32(dr["TabId"].ToString());
                    doc.Created = Convert.ToDateTime(dr["Created"].ToString());
                    doc.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                    doc.Name = dr["docName"].ToString();
                    doc.PathName = dr["docFullPathName"].ToString();
                    doc.GroupRestriction = Convert.ToInt32(dr["GroupRestriction"].ToString());
                }

                return doc;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetDocumentById failed", ex);
                eze.Add("Step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetDocumentById"));
            }
        }

    }
}
