using System;
using System.Collections.Generic;
using diag = System.Diagnostics;
using System.Linq;
using System.Text;

namespace EZUtils
{
    public static class Trace
    {
        private static string mTraceCategory = "EZDesk";
        private static string myLog = "EZSoftware";
        public enum TraceLevels { None = 0, Error, Warning, Info, Debug };

        private static TraceLevels mTraceLevel = TraceLevels.Debug;
        ////public TraceLevels MyProperty 
        ////{
        ////    get { return mTraceLevel;  }
        ////    set { mTraceLevel = value; }
        ////}

        #region newrtns
        public static void Enter(string routine)
        {
            Enter(routine, "");
        }

        public static void Enter(string routine, string msg)
        {
            msg = mTraceCategory + ": " + routine + ": Enter " + msg;
            diag.Trace.WriteLine(msg, mTraceCategory);
        }

        public static void Exit(string routine)
        {
            Exit(routine, "");
        }

        public static void Exit(String routine, string msg)
        {
            msg = mTraceCategory + ": " + routine + ": Exit " + msg;
            diag.Trace.WriteLine(msg, mTraceCategory);
        }

        public static void WriteLine(string routine, string msg)
        {
            msg = mTraceCategory + ":" + routine + ": " + msg;
            diag.Trace.WriteLine(msg, mTraceCategory);
        }

        /// <summary>
        /// Write to an event log
        /// </summary>
        /// <param name="application">
        /// i.e. application = "TimeTracker";
        /// </param>
        /// <param name="msg">
        /// i.e. msg = "Sample Event";
        /// </param>
        public static void WriteEventEntry(string application, string msg)
        {
            //Make sure the "Custome EventLog" exists, if not create it.
            if (!diag.EventLog.Exists(myLog))
            {
                Console.WriteLine("Log '" + myLog + "' does not exist.");
                diag.EventLog.CreateEventSource("ApplicationName", myLog);
            }

            //Make sure the "EventSource" exists, if not create it.
            if (!diag.EventLog.SourceExists(application))
                diag.EventLog.CreateEventSource(application, myLog);

            diag.EventLog.WriteEntry(application, msg);
        }

        /// <summary>
        /// Write to an event log with event type and eventid
        /// </summary>
        /// <param name="application">
        /// i.e. application = "TimeTracker";
        /// </param>
        /// <param name="msg">
        /// i.e. msg = "Sample Event";
        /// </param>
        /// <param name="typ"></param>
        /// <param name="eventID"></param>
        public static void WriteEventEntry(string application, string msg,
                                diag.EventLogEntryType typ, int eventID)
        {
            //Make sure the eventlog details have been defined.
            // ("Custom EventLog" and "EventSource". 
            zEventLogCheck(application);

            diag.EventLog.WriteEntry(application, msg, typ, eventID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        /// <param name="exception"></param>
        public static void WriteEventEntry(string application, Exception exception)
        {
            //Make sure the eventlog details have been defined.
            // ("Custom EventLog" and "EventSource". 
            zEventLogCheck(application);

            diag.EventLog.WriteEntry(application, exception.StackTrace,
                                  diag.EventLogEntryType.Error);
        }

        /// <summary>
        /// Make sure the eventlog details have been defined.
        ///   ("Custom EventLog" and "EventSource".
        /// </summary>
        /// <param name="application"></param>
        private static void zEventLogCheck(string application)
        {
            //Make sure the "Custome EventLog" exists, if not create it.
            if (!diag.EventLog.Exists(myLog))
            {
                diag.EventLog.CreateEventSource("ApplicationName", myLog);
            }

            //Make sure the "EventSource" exists, if not create it.
            if (!diag.EventLog.SourceExists(application))
            {
                diag.EventLog.CreateEventSource(application, myLog);
            }
        }

        public static string RtnName(string ModName, string meathodName)
        {
            string rtn = "";

            rtn = ModName;
            if (!rtn.EndsWith("."))
            {
                rtn += ".";
            }
            rtn += meathodName;

            return rtn;
        }
        #endregion

        #region oldrtns
        public static void WriteLine(String s, String MethodName, bool forceTrace)
        {
            WriteLine(TraceLevels.Debug, s + ": " + MethodName);
        }

        /// <summary>
        /// Write a line to the Diagnostic Trace Log.
        /// </summary>
        /// <param name="severity">Severity Level of this message</param>
        /// <param name="message">Message to write</param>
        public static void WriteLine(TraceLevels severity, string message)
        {
            if (severity <= mTraceLevel)
            {
                System.Diagnostics.Trace.WriteLine(MethodName(2) + " " + 
                                severity.ToString() + ":" + message, 
                        severity.ToString());
            }
        }

        /// <summary>
        /// Log a method has been entered with a message.
        /// </summary>
        /// <param name="message"></param>
        public static void EnterOld(string message)
        {
            if (mTraceLevel != TraceLevels.None)
            {
                System.Diagnostics.Trace.WriteLine(MethodName(2) + " Enter: " + message);
            }
        }

        /// <summary>
        /// Log a method has been entered.
        /// </summary>
        public static void EnterOld()
        {
            if (mTraceLevel != TraceLevels.None)
            {
                System.Diagnostics.Trace.WriteLine(MethodName(2) + " Enter");
            }
        }

        /// <summary>
        /// Log a method is exiting with a message.
        /// </summary>
        /// <param name="message"></param>
        public static void ExitOld(string message)
        {
            if (mTraceLevel != TraceLevels.None)
            {
                System.Diagnostics.Trace.WriteLine(MethodName(2) + " Exit: " + message);
            }
        }

        /// <summary>
        /// Log a method is exiting with NO message.
        /// </summary>
        public static void ExitOld()
        {
            if (mTraceLevel != TraceLevels.None)
            {
                System.Diagnostics.Trace.WriteLine(MethodName(2) + " Exit");
            }
        }

        public static string MethodName(int NestingLevel)
        {
            string s = "unknown";
            try
            {
                System.Reflection.MethodBase b = new System.Diagnostics.StackTrace().GetFrame(NestingLevel).GetMethod();
                s = b.DeclaringType.FullName + "." + b.Name;
            }
            catch 
            {
            }
            return s;
        }

        #endregion

    }
}
