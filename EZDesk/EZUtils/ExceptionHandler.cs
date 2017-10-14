using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Web.Services.Protocols;

namespace EZUtils
{
    public static class ExceptionHandler
    {
        #region Public

        /// <summary>
        /// 
        /// </summary>
        public static void UnhandledExceptionEventHandlerSetup()
        {
            UnhandledExceptionEventHandlerSetup(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShowDialog"></param>
        public static void UnhandledExceptionEventHandlerSetup(Boolean ShowDialog)
        {
            mShowDialog = ShowDialog;

            // Add the event handler for handling UI thread exceptions to the event:
            Application.ThreadException += 
                new ThreadExceptionEventHandler(ThreadExceptionFunction);

            // Set the unhandled exception mode to force all Windows Forms 
            // errors to go through our handler:
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event:
            AppDomain.CurrentDomain.UnhandledException += 
                new UnhandledExceptionEventHandler(UnhandledExceptionFunction);
        }

        public static void ShowErrorInMessageBox(Exception e)
        {
            try
            {
                // Some errors are generated in a location/low level where the last resort is a mesagebox
                System.Windows.Forms.MessageBox.Show(
                    e.Source + ": " + e.Message,
                    System.Windows.Forms.Application.ProductName,
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch 
            {
            } // Give up
        }

        public static SoapException WrapInSoapException(Exception e)
        {
            EZUtils.EZException ezE;

            if (EZUtils.EZException.IsEZException(e))
            {
                ezE = (EZException)e;
            }
            else
            {
                ezE = new EZUtils.EZException("EZException wrapper", e);
            }

            EZUtils.EventLog.WriteErrorEntry(ezE);

            if (ezE.AsSoapDetail.OuterXml.Length > 65000)
            {
                return new System.Web.Services.Protocols.SoapException(
                  "Error propagated to web service client", SoapException.ClientFaultCode,
                  "", e);
            }
            else
            {
                return new System.Web.Services.Protocols.SoapException(
                  "Error propagated to web service client", SoapException.ClientFaultCode,
                  "", ezE.AsSoapDetail);
            }
        }

        /// <summary>
        /// Raise Exception Dialog box for both UI and non-UI Unhandled Exceptions
        /// </summary>
        /// <param name="e">Caught exception</param>
        public static void ShowUnhandledExceptionDlg(Exception e)
        {
            ExceptionDialog exDlgForm = new ExceptionDialog(e, cMsgText);
            try
            {
                exDlgForm.ShowDialog();
            }
            finally
            {
                exDlgForm.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EZExceptionOuterXML"></param>
        public static void ShowAndLogExceptionDlg(string EZExceptionOuterXML)
        {
            EventLog.WriteErrorEntry(EZExceptionOuterXML);
            ShowExceptionDlg(EZExceptionOuterXML);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public static void ShowAndLogUnhandledExceptionDlg(Exception e)
        {
            EventLog.WriteErrorEntry(e);
            ShowUnhandledExceptionDlg(e);
        }

        /// <summary>
        /// Raise Exception Dialog box for both UI and non-UI  Exceptions
        /// </summary>
        /// <param name="e">Caught exception</param>
        public static void ShowExceptionDlg(string EZExceptionOuterXML)
        {
            ExceptionDialog exDlgForm = new ExceptionDialog(EZExceptionOuterXML, "");
            try
            {
                exDlgForm.ShowDialog();
            }
            finally
            {
                exDlgForm.Dispose();
            }
        }
        
        /// <summary>
        /// Raise Exception Dialog box for both UI and non-UI  Exceptions
        /// </summary>
        /// <param name="e">Caught exception</param>
        public static void ShowExceptionDlg(Exception e)
        {
            ExceptionDialog exDlgForm = new ExceptionDialog(e, "");
            try
            {
                exDlgForm.ShowDialog();
            }
            finally
            {
                exDlgForm.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public static void ShowAndLogExceptionDlg(Exception e)
        {
            EventLog.WriteErrorEntry(e);
            ShowExceptionDlg(e);
        }

        
#endregion

        #region Private

        private static Boolean mShowDialog;

        private const string cMsgText = "An unhandled exception occurred." +
            "If you click Quit the application will close immediately. Click " +
            "Continue to continue.\r\n\r\n";

        /// <summary>
        /// Handle the UI exceptions by showing a dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ThreadExceptionFunction(Object sender, ThreadExceptionEventArgs e)
        {
            EventLog.WriteErrorEntry(e.Exception);
            if (mShowDialog)
            {
                ShowUnhandledExceptionDlg(e.Exception);
            }
            else
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Handle the UI exceptions by showing a dialog box
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="args">Passing arguments: original exception etc.</param>
        private static void UnhandledExceptionFunction(Object sender, UnhandledExceptionEventArgs args)
        {
            EventLog.WriteErrorEntry((Exception)args.ExceptionObject);
            if (mShowDialog)
            {
                ShowUnhandledExceptionDlg((Exception)args.ExceptionObject);
            }
            else
            {
                Application.Exit();
            }
        }

        #endregion
    }
}
