using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EASendMail;
using EZDeskDataLayer;

namespace MessageCenter
{
    public partial class SendMailNew : Form
    {
        private string[,] m_arCharset = new string[28, 2];
        private ArrayList m_arAttachment = new ArrayList();
        private bool m_bcancel = false;
        private long m_eventtick = 0;
        private int minWidth = 0;
        private int minHeight = 0;
        private string mServer = "";
        private string mUser = "";
        private string mPassword = "";
        private int mProtocol = 0;
        private string mCharSet = "";
        private bool mChkAuth = false;
        private EZDeskCommon mCommon;

        /// <summary>
        /// gets/sets the body of the message. Usually for Forward or Reply.
        /// </summary>
        public string MailBody 
        {
            get { return textBody.Text; }
            set { textBody.Text = value; } 
        }

        /// <summary>
        /// gets/sets the subject of the message. Usually used for Forward or Reply.
        /// </summary>
        public string MailSubject 
        {
            get { return textSubject.Text; }
            set { textSubject.Text = value; } 
        }

        /// <summary>
        /// gets/sets the mail recipient. Usually for the forward or Reply.
        /// </summary>
        public string MailTo
        {
            get { return textTo.Text; }
            set { textTo.Text = value; }
        }

        private double mRelatesTo = -1;
        /// <summary>
        /// gets/sets the personId that this relates to.
        /// </summary>
        public double RelatesTo 
        {
            get { return mRelatesTo; }
            set { mRelatesTo = value; } 
        }

        private int mTabID = -1;
        public int TabID
        {
            get { return mTabID; }
            set { mTabID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="common"></param>
        public SendMailNew(EZDeskCommon common)
        {
            InitializeComponent();
            mCommon = common;
            _Init();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMailNew_Load(object sender, EventArgs e)
        {
            minWidth = this.Width;
            minHeight = this.Height;
            mServer = mCommon.eCtrl.GetProperty("SendMailServer");
            mUser = mCommon.User.MailUserName;
            mPassword = mCommon.User.MailPassword;
            mChkAuth = true;

            mCharSet = "utf-8";
            ////m_arCharset[nIndex, 0] = "Unicode(UTF-8)";
            ////m_arCharset[nIndex, 1] = "utf-8";

            mProtocol = 0;
            ////lstProtocol.Items.Add("SMTP Protocol - Recommended");
            ////lstProtocol.Items.Add("Exchange Web Service - 2007/2010");
            ////lstProtocol.Items.Add("Exchange WebDav - 2000/2003");
            ////lstProtocol.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMailNew_Resize(object sender, EventArgs e)
        {
            if (minWidth > this.Width) { this.Width = minWidth; }
            if (minHeight > this.Height) { this.Height = minHeight; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            attachmentDlg.Reset();
            attachmentDlg.Multiselect = true;
            attachmentDlg.CheckFileExists = true;
            attachmentDlg.CheckPathExists = true;
            if (attachmentDlg.ShowDialog() != DialogResult.OK)
                return;

            string[] attachments = attachmentDlg.FileNames;
            int nLen = attachments.Length;
            for (int i = 0; i < nLen; i++)
            {
                m_arAttachment.Add(attachments[i]);
                string fileName = attachments[i];
                int pos = fileName.LastIndexOf("\\");
                if (pos != -1)
                    fileName = fileName.Substring(pos + 1);

                textAttachments.Text += fileName;
                textAttachments.Text += ";";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            m_arAttachment.Clear();
            textAttachments.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (textTo.Text.Length == 0 &&
                textCc.Text.Length == 0)
            {
                MessageBox.Show("Please input To or Cc!, the format can be test@adminsystem.com or Tester<test@adminsystem.com>, please use , or ; to separate multiple recipients");
                return;
            }

            btnSend.Enabled = false;
            btnCancel.Enabled = true;
            m_bcancel = false;

            //For evaluation usage, please use "TryIt" as the license code, otherwise the 
            //"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
            //"trial version expired" exception will be thrown.

            //For licensed uasage, please use your license code instead of "TryIt", then the object
            //will never expire
            SmtpMail oMail = new SmtpMail("TryIt");
            SmtpClient oSmtp = new SmtpClient();
            //To generate a log file for SMTP transaction, please use
            //oSmtp.LogFileName = "c:\\smtp.log";
            string err = "";

            try
            {
                oMail.Reset();
                //If you want to specify a reply address
                //oMail.Headers.ReplaceHeader( "Reply-To: <reply@mydomain>" );

                //From is a MailAddress object, in c#, it supports implicit converting from string.
                //The syntax is like this: "test@adminsystem.com" or "Tester<test@adminsystem.com>"

                //The example code without implicit converting
                // oMail.From = new MailAddress( "Tester", "test@adminsystem.com" )
                // oMail.From = new MailAddress( "Tester<test@adminsystem.com>" )
                // oMail.From = new MailAddress( "test@adminsystem.com" )
                MailAddress ma;
                string mailFrom = mCommon.User.MailUserName.Trim();
                if (!mailFrom.Contains("@"))
                {
                    ma = new MailAddress(mailFrom, mailFrom + "@" + mCommon.eCtrl.GetProperty("MailDomain"));
                }
                else
                {
                    ma = new MailAddress(mailFrom);
                }
                oMail.From = ma;

                //To, Cc and Bcc is a AddressCollection object, in C#, it supports implicit converting from string.
                // multiple address are separated with (,;)
                //The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"

                //The example code without implicit converting
                // oMail.To = new AddressCollection( "test1@adminsystem.com, test2@adminsystem.com" );
                // oMail.To = new AddressCollection( "Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>");
                string mailTo = textTo.Text.Trim();
                if (!mailTo.Contains("@"))
                {
                    ma = new MailAddress(mailTo, mailTo + "@" + mCommon.eCtrl.GetProperty("MailDomain"));
                }
                else
                {
                    ma = new MailAddress(mailTo);
                }
                AddressCollection ac = new AddressCollection();
                ac.Add(ma);
                oMail.To = ac;
                
                //You can add more recipient by Add method
                // oMail.To.Add( new MailAddress( "tester", "test@adminsystem.com"));

                oMail.Cc = textCc.Text;
                oMail.Subject = textSubject.Text;
                oMail.Charset = mCharSet;

                //////Digital signature and encryption
                ////if (!_SignEncrypt(ref oMail))
                ////{
                ////    btnSend.Enabled = true;
                ////    btnCancel.Enabled = false;
                ////    return;
                ////}

                string body = textBody.Text;
                body = body.Replace("[$from]", oMail.From.ToString());
                body = body.Replace("[$to]", oMail.To.ToString());
                body = body.Replace("[$subject]", oMail.Subject);

                if (chkHtml.Checked)
                    oMail.HtmlBody = body;
                else
                    oMail.TextBody = body;

                int count = m_arAttachment.Count;
                for (int i = 0; i < count; i++)
                {
                    //Add attachment
                    oMail.AddAttachment(m_arAttachment[i] as string);
                }

                SmtpServer oServer = new SmtpServer(mServer);
                oServer.Protocol = (ServerProtocol)mProtocol;

                if (oServer.Server.Length != 0)
                {
                    if (mChkAuth)
                    {
                        oServer.User = mUser;
                        oServer.Password = mPassword;
                    }

                    //// NOTE: No chkSSL
                    ////if (chkSSL.Checked)
                    ////    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                }
                else
                {
                    //To send email to the recipient directly(simulating the smtp server), 
                    //please add a Received header, 
                    //otherwise, many anti-spam filter will make it as junk email.
                    System.Globalization.CultureInfo cur = new System.Globalization.CultureInfo("en-US");
                    string gmtdate = System.DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss zzz", cur);
                    gmtdate.Remove(gmtdate.Length - 3, 1);
                    string recvheader = String.Format("from {0} ([127.0.0.1]) by {0} ([127.0.0.1]) with SMTPSVC;\r\n\t {1}",
                        oServer.HeloDomain,
                        gmtdate);

                    oMail.Headers.Insert(0, new HeaderItem("Received", recvheader));
                }

                //Catching the following events is not necessary, 
                //just make the application more user friendly.
                //If you use the object in asp.net/windows service or non-gui application, 
                //You need not to catch the following events.
                //To learn more detail, please refer to the code in EASendMail EventHandler region
                oSmtp.OnIdle += new SmtpClient.OnIdleEventHandler(OnIdle);
                oSmtp.OnAuthorized += new SmtpClient.OnAuthorizedEventHandler(OnAuthorized);
                oSmtp.OnConnected += new SmtpClient.OnConnectedEventHandler(OnConnected);
                oSmtp.OnSecuring += new SmtpClient.OnSecuringEventHandler(OnSecuring);
                oSmtp.OnSendingDataStream += new SmtpClient.OnSendingDataStreamEventHandler(OnSendingDataStream);

                //// NOTE: No Direct send
                ////if (oServer.Server.Length == 0 && oMail.Recipients.Count > 1)
                ////{
                ////    //To send email without specified smtp server, we have to send the emails one by one 
                ////    // to multiple recipients. That is because every recipient has different smtp server.
                ////    _DirectSend(ref oMail, ref oSmtp);
                ////}
                ////else
                ////{
                    sbStatus.Text = "Connecting ... ";
                    pgSending.Value = 0;

                    oSmtp.SendMail(oServer, oMail);

                    sbStatus.Text = "Completed";

                    MessageBox.Show(String.Format("The message was sent to {0} successfully!",
                        oSmtp.CurrentSmtpServer.Server));

                    this.DialogResult = DialogResult.OK;
                ////}

                //If you want to reuse the mail object, please reset the Date and Message-ID, otherwise
                //the Date and Message-ID will not change.
                //oMail.Date = System.DateTime.Now;
                //oMail.ResetMessageID();
                //oMail.To = "another@example.com";
                //oSmtp.SendMail( oServer, oMail );
            }
            catch (SmtpTerminatedException exp)
            {
                err = exp.Message;
            }
            catch (SmtpServerException exp)
            {
                err = String.Format("Exception: Server Respond: {0}", exp.ErrorMessage);
            }
            catch (System.Net.Sockets.SocketException exp)
            {
                err = String.Format("Exception: Networking Error: {0} {1}", exp.ErrorCode, exp.Message);
            }
            catch (System.ComponentModel.Win32Exception exp)
            {
                err = String.Format("Exception: System Error: {0} {1}", exp.ErrorCode, exp.Message);
            }
            catch (System.Exception exp)
            {
                err = String.Format("Exception: Common: {0}", exp.Message);
            }

            if (err.Length > 0)
            {
                MessageBox.Show(err);
                sbStatus.Text = err;
            }
            //to get more debug information, please use
            //MessageBox.Show( oSmtp.SmtpConversation );

            btnSend.Enabled = true;
            btnCancel.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            m_bcancel = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void _Init()
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append("This sample demonstrates how to send simple email.\r\n\r\n");
            s.Append("From: [$from]\r\n");
            s.Append("To: [$to]\r\n");
            s.Append("Subject: [$subject]\r\n\r\n");
            s.Append("If no sever address was specified, the email will be delivered to the recipient's server directly,");
            s.Append("However, if you don't have a static IP address, ");
            s.Append("many anti-spam filters will mark it as a junk-email.\r\n\r\n");
            s.Append("If \"Digitial Signature\" was checked, please make sure you have the certificate for the sender address installed on ");
            s.Append("Local User Certificate Store.\r\n\r\n");
            s.Append("If \"Encrypt\" was checked, please make sure you have the certificate for recipient address installed on the Local User Certificate Store.\r\n");

            textBody.Text = s.ToString();

            //// NOTE: These are not needed as they will come from the database.
            ////_InitCharset();
            ////_InitProtocols();

            //// NOTE: There is no chkAuth checkbox
            ////_ChangeAuthStatus();    
        }

        /// <summary>
        /// NOTE: There was no combobox lstProtocol control. added one visible=false and enabled=false
        /// </summary>
        private void _InitProtocols()
        {
        }

        /////// <summary>
        /////// NOTE: There is no chkAuth checkbox
        /////// </summary>
        ////private void _ChangeAuthStatus()
        ////{
        ////    textUser.Enabled = chkAuth.Checked;
        ////    textPassword.Enabled = chkAuth.Checked;
        ////}

        /////// <summary>
        /////// NOTE: There is no chkAuth checkbox 
        /////// </summary>
        /////// <param name="sender"></param>
        /////// <param name="e"></param>
        ////private void chkAuth_CheckedChanged(object sender, System.EventArgs e)
        ////{
        ////    _ChangeAuthStatus();
        ////}

        #region Cross Thread Access Control
        protected delegate void SetStatusDelegate(string v);
        protected delegate void SetProgressDelegate(int sent, int total);

        protected void _SetProgressCallBack(int sent, int total)
        {
            long t = sent;
            t = t * 100;
            t = t / total;
            int x = (int)t;
            pgSending.Value = x;

            long tick = System.DateTime.Now.Ticks;
            // call DoEvents every 0.2 second 
            if (tick - m_eventtick > 2000000)
            {
                // Do not call DoEvents too frequently in a very fast lan + larg email.
                m_eventtick = tick;
                Application.DoEvents();
            }
        }

        protected void _SetStatusCallBack(string v)
        {
            sbStatus.Text = v;
        }

        //Why we need to change the status text by this function.
        //Because some the events are fired on another
        //thread, to change the control value safety, we used this function to 
        //update control value. more detail, please refer to Control.BeginInvoke method
        // in MSDN
        protected void _SetStatus(string v)
        {
            if (InvokeRequired)
            {
                object[] args = new object[1];
                args[0] = v;

                SetStatusDelegate d = new SetStatusDelegate(_SetStatusCallBack);
                BeginInvoke(d, args);
            }
            else
            {
                _SetStatusCallBack(v);
            }
        }
        protected void _SetProgress(int sent, int total)
        {
            if (InvokeRequired)
            {
                object[] args = new object[2];
                args[0] = sent;
                args[1] = total;

                SetProgressDelegate d = new SetProgressDelegate(_SetProgressCallBack);
                BeginInvoke(d, args);
            }
            else
            {
                _SetProgressCallBack(sent, total);
            }
        }
        #endregion

        #region	EASendMail EventHandler
        void OnIdle(object sender, ref bool cancel)
        {
            cancel = m_bcancel;
            if (!cancel)
            {
                //Current object is waiting server reponse or connecting server, 
                //that means current object is idle. Application.DoEvents
                //can processes all Windows(form) messages(events) currently in the message queue. 
                //If you don't invoke this method, the application will not respond the Cancel and other
                //events.
                Application.DoEvents();
            }
        }

        void OnConnected(object sender, ref bool cancel)
        {
            _SetStatus("Connected");
            cancel = m_bcancel;
        }

        void OnSendingDataStream(object sender, int sent, int total, ref bool cancel)
        {
            if (pgSending.Value == 0)
            {
                _SetStatus("Sending ...");
            }
            _SetProgress(sent, total);
            cancel = m_bcancel;
            if (sent == total)
                _SetStatus("Disconnecting ...");
        }

        void OnAuthorized(object sender, ref bool cancel)
        {
            _SetStatus("Authorized");
            cancel = m_bcancel;
        }

        void OnSecuring(object sender, ref bool cancel)
        {
            _SetStatus("Securing ...");
            cancel = m_bcancel;
        }

        #endregion

        #region Initialize the Encoding List
        ////private void _InitCharset()
        ////{
        ////    int nIndex = 0;
        ////    string defaultEncoding = "utf-8";//System.Text.Encoding.Default.HeaderName;

        ////    m_arCharset[nIndex, 0] = "Arabic(Windows)";
        ////    m_arCharset[nIndex, 1] = "windows-1256";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Baltic(ISO)";
        ////    m_arCharset[nIndex, 1] = "iso-8859-4";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Baltic(Windows)";
        ////    m_arCharset[nIndex, 1] = "windows-1257";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Central Euporean(ISO)";
        ////    m_arCharset[nIndex, 1] = "iso-8859-2";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Central Euporean(Windows)";
        ////    m_arCharset[nIndex, 1] = "windows-1250";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Chinese Simplified(GB18030)";
        ////    m_arCharset[nIndex, 1] = "GB18030";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Chinese Simplified(GB2312)";
        ////    m_arCharset[nIndex, 1] = "gb2312";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Chinese Simplified(HZ)";
        ////    m_arCharset[nIndex, 1] = "hz-gb-2312";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Chinese Traditional(Big5)";
        ////    m_arCharset[nIndex, 1] = "big5";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Cyrillic(ISO)";
        ////    m_arCharset[nIndex, 1] = "iso-8859-5";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Cyrillic(KOI8-R)";
        ////    m_arCharset[nIndex, 1] = "koi8-r";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Cyrillic(KOI8-U)";
        ////    m_arCharset[nIndex, 1] = "koi8-u";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Cyrillic(Windows)";
        ////    m_arCharset[nIndex, 1] = "windows-1251";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Greek(ISO)";
        ////    m_arCharset[nIndex, 1] = "iso-8859-7";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Greek(Windows)";
        ////    m_arCharset[nIndex, 1] = "windows-1253";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Hebrew(Windows)";
        ////    m_arCharset[nIndex, 1] = "windows-1255";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Japanese(JIS)";
        ////    m_arCharset[nIndex, 1] = "iso-2022-jp";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Korean";
        ////    m_arCharset[nIndex, 1] = "ks_c_5601-1987";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Korean(EUC)";
        ////    m_arCharset[nIndex, 1] = "euc-kr";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Latin 9(ISO)";
        ////    m_arCharset[nIndex, 1] = "iso-8859-15";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Thai(Windows)";
        ////    m_arCharset[nIndex, 1] = "windows-874";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Turkish(ISO)";
        ////    m_arCharset[nIndex, 1] = "iso-8859-9";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Turkish(Windows)";
        ////    m_arCharset[nIndex, 1] = "windows-1254";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Unicode(UTF-7)";
        ////    m_arCharset[nIndex, 1] = "utf-7";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Unicode(UTF-8)";
        ////    m_arCharset[nIndex, 1] = "utf-8";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Vietnames(Windows)";
        ////    m_arCharset[nIndex, 1] = "windows-1258";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Western European(ISO)";
        ////    m_arCharset[nIndex, 1] = "iso-8859-1";
        ////    nIndex++;

        ////    m_arCharset[nIndex, 0] = "Western European(Windows)";
        ////    m_arCharset[nIndex, 1] = "Windows-1252";
        ////    nIndex++;

        ////    int selectIndex = 25; //utf-8
        ////    for (int i = 0; i < nIndex; i++)
        ////    {
        ////        lstCharset.Items.Add(m_arCharset[i, 0]);
        ////        if (String.Compare(
        ////            m_arCharset[i, 1], defaultEncoding, true) == 0)
        ////        {
        ////            selectIndex = i;
        ////        }
        ////    }

        ////    lstCharset.SelectedIndex = selectIndex;
        ////}
        #endregion

    }
}
