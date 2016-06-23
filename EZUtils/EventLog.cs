using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Security;
using System.Xml;

namespace EZUtils
{
    /// <summary>
    /// Stateless loggin to EZ eventlog on current machine
    /// </summary>
    public static class EventLog
    {
        static private string mLogName = "EZSoftware";
        static private string mSourceName = "EZSoftware";
        static private int mMaximumKilobytes= 64000;

        #region Public

        // TODO: Create a resource file with texts for each category to show in the eventviewer
        public enum  Category
        {
            General = 0,
            Utilities = 1
        }

        public static string LogName
        {
            get { return mLogName; }
            set { mLogName = value; }
        }

        public static string SourceName
        {
            get { return mSourceName; }
            set { mSourceName = value; }
        }

        public static int MaximumKilobytes
        {
            get { return mMaximumKilobytes; }
            set 
            { 
                if (value < 64 || value > 4194240 || value % 64 != 0)
                {
                    throw new EZException("The specified value is less than 64, or greater than 4194240, or not an even multiple of 64");
                }
                mMaximumKilobytes = value; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Clear()
        {
            System.Diagnostics.EventLog tmpLog = null;
            try
            {
                tmpLog = zGetEZEventLog();
                tmpLog.Clear();
                tmpLog.Close();
            }
            catch
            {
                // Sorry
            }
            finally
            {
                if (tmpLog != null)
                {
                    tmpLog.Dispose();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Boolean WriteErrorEntry(Exception e)
        {
            string message = null;
            byte[] data = null;

            try
            {
                if (EZException.IsEZException(e))
                {
                    EZException EZEx = (e as EZException);

                    string str = EZEx.AsSoapDetail.OuterXml;
                    data = ASCIIEncoding.ASCII.GetBytes(str);
                    message = "Message: " + e.Message + "\r\nSource: " + e.Source
                        + "\r\nBottom EZException: " + EZEx.BottomEZExceptionSummary.Message
                        + "\r\nTop non EZException: " + EZEx.TopNonEZExceptionSummary.Message;
                }
                else
                {
                    message = "Message: " + e.Message + "\r\nSource: " + e.Source;
                }
            }
            catch
            {
                return false;
            }

            zWriteEntry(message, EventLogEntryType.Error, 0, Category.General, data);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExceptionXML"></param>
        /// <returns></returns>
        public static Boolean WriteErrorEntry(string ExceptionXML)
        {
            string message = null;
            byte[] data = null;

            ExceptionSummary EZExceptionSummary;
            ExceptionSummary BottomEZExceptionSummary;
            ExceptionSummary TopNonEZExceptionSummary;

            try
            {
                data = ASCIIEncoding.ASCII.GetBytes(ExceptionXML);

                EZException.ExtractSummaryFromExceptionXML(ExceptionXML, out EZExceptionSummary,
                    out TopNonEZExceptionSummary, out BottomEZExceptionSummary);

                message = "Message: " + EZExceptionSummary.Message
                    + "\r\nSource: " + EZExceptionSummary.Source
                    + "\r\nBottom EZ Exception: " + BottomEZExceptionSummary.Message
                    + "\r\nTop non EZ Exception: " + TopNonEZExceptionSummary.Message;
            }
            catch
            {
                return false;
            }

            zWriteEntry(message, EventLogEntryType.Error, 0, Category.General, data);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static Boolean WriteErrorEntry(Exception e, int eventID)
        {
            string message = null;
            byte[] data = null;

            try
            {
                message = "Message: " + e.Message + "\r\nSource: " + e.Source;

                if (EZException.IsEZException(e))
                {
                    string str = (e as EZException).AsSoapDetail.OuterXml;
                    data = ASCIIEncoding.ASCII.GetBytes(str);

                }
            }
            catch
            {
                return false;
            }

            zWriteEntry(message, EventLogEntryType.Error, eventID, Category.General, data);
            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Boolean WriteWarningEntry(String Message, Exception e)
        {
           byte[] data = null;

           try
           {
               if (EZException.IsEZException(e))
               {
                   EZException EZEx = (e as EZException);

                   string str = EZEx.AsSoapDetail.OuterXml;
                   data = ASCIIEncoding.ASCII.GetBytes(str);
                   Message += "\r\n\r\nMessage: " + e.Message + "\r\nSource: " + e.Source
                       + "\r\nBottom EZ Exception: " + EZEx.BottomEZExceptionSummary.Message
                       + "\r\nTop non EZ Exception: " + EZEx.TopNonEZExceptionSummary.Message;
               }

           }
           catch
           {
               return false;
           }

           zWriteEntry(Message, EventLogEntryType.Error, 0, Category.General, data);
           return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="entryType"></param>
        /// <param name="eventID"></param>
        /// <param name="EZArea"></param>
        /// <param name="rawData"></param>
        public static void WriteEntry(
            string message, 
            EventLogEntryType entryType,
            int eventID,
            Category EZArea,
            byte[] rawData)
        {
            zWriteEntry(message, entryType, eventID, EZArea, rawData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="entryType"></param>
        /// <param name="eventID"></param>
        /// <param name="EZArea"></param>
        public static void WriteEntry(string message, EventLogEntryType entryType, int eventID, Category EZArea)
        {
            zWriteEntry(message, entryType, eventID, EZArea, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="entryType"></param>
        public static void WriteEntry(string message, EventLogEntryType entryType)
        {
            zWriteEntry(message, entryType,  0, Category.General, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void WriteEntry(string message)
        {
            zWriteEntry(message, EventLogEntryType.Information, 0, Category.General, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="EZArea"></param>
        public static void WriteErrorEntry(string message, int eventID, Category EZArea)
        {
            zWriteEntry(message, EventLogEntryType.Error, eventID, EZArea, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="EZArea"></param>
        /// <param name="data"></param>
        public static void WriteErrorEntry(string message, int eventID, Category EZArea, byte[] data)
        {
            zWriteEntry(message, EventLogEntryType.Error, eventID, EZArea, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="EZArea"></param>
        public static void WriteWarningEntry(string message, int eventID, Category EZArea)
        {
            zWriteEntry(message, EventLogEntryType.Warning, eventID, EZArea, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="EZArea"></param>
        public static void WriteInformationEntry(string message, int eventID, Category EZArea)
        {
            zWriteEntry(message, EventLogEntryType.Information, eventID, EZArea, null);
        }

        #endregion

        #region Private

        private static void zWriteEntry(
            string message,
            EventLogEntryType entryType,
            int eventID,
            Category EZArea,
            byte[] rawData)
        {
            System.Diagnostics.EventLog tmpLog = null;
            try
            {
                Trace.WriteLine(message, MiscUtils.MethodName(3), true);

                tmpLog = zGetEZEventLog();
                if (rawData == null)
                {
                    tmpLog.WriteEntry(message, entryType, eventID, (short)EZArea);
                }
                else
                {
                    tmpLog.WriteEntry(message, entryType, eventID, (short)EZArea, rawData);
                }
                tmpLog.Close();
            }
            catch (Exception Ex)
            {
                Trace.WriteLine(Trace.TraceLevels.Error, 
                        "EZwriteEntry failed: " + Ex.Message);
                // Sorry
            }
            finally
            {
                if (tmpLog != null)
                {
                    tmpLog.Dispose();
                }
            }
        }

        private static System.Diagnostics.EventLog zGetEZEventLog()
        {
            System.Diagnostics.EventLog tmpLog = null;
            bool NewLog;

            try
            {
                NewLog = !System.Diagnostics.EventLog.Exists(mLogName);

                try
                {
                    if (System.Diagnostics.EventLog.SourceExists(mSourceName))
                    {
                        if (mLogName != System.Diagnostics.EventLog.LogNameFromSourceName(mSourceName, System.Environment.MachineName))
                        {
                            // The source is associated with a different log. remove it and connect it to the wanted source
                            System.Diagnostics.EventLog.DeleteEventSource(mSourceName);
                        }
                    }

                    if (!System.Diagnostics.EventLog.SourceExists(mSourceName))
                    {
                        System.Diagnostics.EventSourceCreationData creationData = new
                            System.Diagnostics.EventSourceCreationData(mSourceName, mLogName);
                        System.Diagnostics.EventLog.CreateEventSource(creationData);
                    }
                }
                catch (SecurityException)
                {
                    // Most likely System.Diagnostics.EventLog.SourceExists(mSourceName) failed because we do
                    // not have access to the security log. Just hope we end up in the right place.
                }

                tmpLog = new System.Diagnostics.EventLog();
                tmpLog.Source = mSourceName;

                if (NewLog)
                {
                    tmpLog.MaximumKilobytes = mMaximumKilobytes; // must be multiple off 64 KB
                    tmpLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 0);
                }
            }
            catch (Exception Ex)
            {
                Trace.WriteLine(Trace.TraceLevels.Error, 
                        "zGetEZEventLog failed: " + Ex.Message);
                //Sorry
            }
            return tmpLog;
        }

        #endregion
    }
}
