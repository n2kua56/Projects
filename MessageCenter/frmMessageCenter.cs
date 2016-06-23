using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using EAGetMail;
using EZUtils;
using EZDeskDataLayer;

namespace MessageCenter
{
    public partial class frmMessageCenter : Form
    {
        private bool m_bcancel = false;
        private string m_uidlfile = "uidl.txt";
        private string m_curpath = "";
        private ArrayList m_arUidl = new ArrayList();
        private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        private EZDeskCommon mCommon;
        private MailServer oServer;
        private MailClient oClient;

        private string mServer = "";
        private string mAuthType = "";
        private string mProtocol = "";
        private bool mChkSSL = false;
        private bool mLeaveCopy = true;
        private string mUser = "";
        private string mPassword = "";
        private string mSubject = "";
        private string mFrom = "";
        private string mMessageText = "";

        private string mModName = "MessageCenter.Form1.";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="common"></param>
        public frmMessageCenter(EZDeskCommon common)
        {
            Trace.Enter("MessageCenter.Form1");
            InitializeComponent();
            mCommon = common;
            eCtrl = new EZDeskDataLayer.ehr.ehrCtrl(mCommon);
            Trace.Exit("MessageCenter.Form1");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            string temp = "";
            
            Trace.Enter(mModName+"Form1_Load");

            try
            {
                mServer = eCtrl.GetProperty("MailServer");
                mAuthType = eCtrl.GetProperty("MailAuthType");
                mProtocol = eCtrl.GetProperty("MailProtocol");

                temp = eCtrl.GetProperty("MailSSL").ToUpper();
                mChkSSL = (temp == "YES".Substring(0, temp.Length));

                temp = eCtrl.GetProperty("MailLeaveCopy");
                mLeaveCopy = (temp == "YES".Substring(0, temp.Length));

                mUser = mCommon.User.MailUserName;
                mPassword = mCommon.User.MailPassword;

                timer1.Interval = Convert.ToInt32(mCommon.eCtrl.GetProperty("MailRefresh")) * 1000;
            }

            catch (Exception ex)
            {
                EZException ezex = new EZException("Form load failed", ex);
                throw ezex;
            }

            finally
            {
                Trace.Exit(mModName + "Form1_Load");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstMail_Click(object sender, EventArgs e)
        {
            Trace.Enter(mModName + "lstMail");

            try
            {
                ListView.SelectedListViewItemCollection items = lstMail.SelectedItems;
                if (items.Count == 0)
                    return;

                ListViewItem item = items[0] as ListViewItem;
                string name = item.SubItems[1].Name;
                mSubject = item.SubItems[1].Text;
                mFrom = item.SubItems[0].Text;
                ShowMail(item.Tag as string);
                item.Font = new System.Drawing.Font(item.Font, FontStyle.Regular);
                btnForward.Enabled = true;
                btnReply.Enabled = true;
            }

            catch (Exception ex)
            {
                EZException ezex = new EZException(mModName + "lstMail_Click failed", ex);
                EZUtils.ExceptionDialog dlg = new ExceptionDialog(ex, "Click failed");
                dlg.ShowDialog();
                btnForward.Enabled = false;
                btnReply.Enabled = false;
            }

            finally
            {
                Trace.Exit(mModName + "lstMail");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            Trace.Enter(mModName + "btnDel_Click");

            try
            {
                ListView.SelectedListViewItemCollection items = lstMail.SelectedItems;
                if (items.Count == 0)
                    return;

                if (MessageBox.Show("Do you want to delete all selected emails",
                                "",
                                MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                while (items.Count > 0)
                {
                    try
                    {
                        string fileName = items[0].Tag as string;
                        File.Delete(fileName);
                        int pos = fileName.LastIndexOf(".");
                        string tempFolder = fileName.Substring(0, pos);
                        string htmlName = tempFolder + ".htm";
                        if (File.Exists(htmlName))
                            File.Delete(htmlName);

                        if (Directory.Exists(tempFolder))
                        {
                            Directory.Delete(tempFolder, true);
                        }

                        lstMail.Items.Remove(items[0]);
                    }
                    catch (Exception ep)
                    {
                        MessageBox.Show(ep.Message);
                        break;
                    }
                }

                lblTotal.Text = String.Format("Total {0} email(s)", lstMail.Items.Count);

                object empty = System.Reflection.Missing.Value;
                webMail.Navigate("about:blank");
            }

            catch (Exception ex)
            {
                EZException ezex = new EZException(mModName + "btnDel failed", ex);
                throw ezex;
            }

            finally
            {
                Trace.Exit(mModName + "btnDel_Click");
            }
        }

        #region EAGetMail Event Handler

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cancel"></param>
        public void OnConnected(object sender, ref bool cancel)
        {
            Trace.Enter(mModName + "OnConnected");

            try
            {
                lblTotal.Text = "Connected ...";
                cancel = m_bcancel;
                Application.DoEvents();
            }

            catch (Exception ex)
            {
                EZException ezex = new EZException(mModName + "OnConnected failed", ex);
                throw ezex;
            }

            finally
            {
                Trace.Exit(mModName + "OnConnected");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cancel"></param>
        public void OnQuit(object sender, ref bool cancel)
        {
            Trace.Enter(mModName + "OnQuit");

            try
            {
                lblTotal.Text = "Quit ...";
                cancel = m_bcancel;
                Application.DoEvents();
            }

            catch (Exception ex)
            {
                EZException ezex = new EZException(mModName + "OnQuit failed", ex);
                throw ezex;
            }

            finally
            {
                Trace.Exit(mModName + "OnQuit");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="info"></param>
        /// <param name="received"></param>
        /// <param name="total"></param>
        /// <param name="cancel"></param>
        public void OnReceivingDataStream(object sender, MailInfo info, int received, int total, ref bool cancel)
        {
            Trace.Enter(mModName + "OnReceivingDataStream");

            try
            {
                pgBar.Visible = true;
                pgBar.Minimum = 0;
                pgBar.Maximum = total;
                pgBar.Value = received;
                cancel = m_bcancel;
                Application.DoEvents();
            }

            catch (Exception ex)
            {
                EZException ezex = new EZException(mModName + "OnReceivingDataStream failed", ex);
                throw ezex;
            }

            finally
            {
                Trace.Exit(mModName + "OnReceivingDataStream");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cancel"></param>
        public void OnIdle(object sender, ref bool cancel)
        {
            cancel = m_bcancel;
            Application.DoEvents();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cancel"></param>
        public void OnAuthorized(object sender, ref bool cancel)
        {
            Trace.Enter(mModName + "OnAuthorized");

            try
            {
                lblTotal.Text = "Authorized ...";
                cancel = m_bcancel;
                Application.DoEvents();
            }

            catch (Exception ex)
            {
                EZException ezex = new EZException(mModName + "OnAuthorized failed", ex);
                throw ezex;
            }

            finally
            {
                Trace.Exit(mModName + "OnAuthorized");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cancel"></param>
        public void OnSecuring(object sender, ref bool cancel)
        {
            Trace.Enter(mModName + "OnSecuring");

            try
            {
                lblTotal.Text = "Securing ...";
                cancel = m_bcancel;
                Application.DoEvents();
            }

            catch (Exception ex)
            {
                EZException ezex = new EZException(mModName + "OnSecuring failed", ex);
                throw ezex;
            }

            finally
            {
                Trace.Exit(mModName + "OnSecuring");
            }
        }

        #endregion

        #region Parse and Display Mails

        /// <summary>
        /// 
        /// </summary>
        private void LoadMails()
        {
            Trace.Enter(mModName + "LoadMails");

            try
            {
                lstMail.Items.Clear();
                string mailFolder = String.Format("{0}\\inbox", m_curpath);
                if (!Directory.Exists(mailFolder))
                    Directory.CreateDirectory(mailFolder);

                string[] files = Directory.GetFiles(mailFolder, "*.eml");
                int count = files.Length;
                for (int i = 0; i < count; i++)
                {
                    string fullname = files[i];
                    //For evaluation usage, please use "TryIt" as the license code, otherwise the 
                    //"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
                    //"trial version expired" exception will be thrown.
                    Mail oMail = new Mail("TryIt");

                    // Load( file, true ) only load the email header to Mail object to save the CPU and memory
                    // the Mail object will load the whole email file later automatically if bodytext or attachment is required..
                    oMail.Load(fullname, true);

                    ListViewItem item = new ListViewItem(oMail.From.ToString());
                    item.SubItems.Add(oMail.Subject);
                    item.SubItems.Add(oMail.ReceivedDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    item.Tag = fullname;
                    lstMail.Items.Add(item);

                    int pos = fullname.LastIndexOf(".");
                    string mainName = fullname.Substring(0, pos);
                    string htmlName = mainName + ".htm";
                    if (!File.Exists(htmlName))
                    {
                        // this email is unread, we set the font style to bold.
                        item.Font = new System.Drawing.Font(item.Font, FontStyle.Bold);
                    }

                    oMail.Clear();
                }
            }

            catch (Exception ex)
            {
                EZException ezex = new EZException(mModName + "LoadMails failed", ex);
                throw ezex;
            }

            finally
            {
                Trace.Exit(mModName + "LoadMails");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private string _FormatHtmlTag(string src)
        {
            Trace.Enter(mModName + "_FormatHtmlTag");
            src = src.Replace(">", "&gt;");
            src = src.Replace("<", "&lt;");
            Trace.Exit(mModName + "_FormatHtmlTag");
            return src;
        }

        //we generate a html + attachment folder for every email, once the html is create,
        // next time we don't need to parse the email again.
        private void _GenerateHtmlForEmail(string htmlName, string emlFile, string tempFolder)
        {
            Trace.Enter(mModName + "_GenerateHtmlForEmail");

            try
            {
                //For evaluation usage, please use "TryIt" as the license code, otherwise the 
                //"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
                //"trial version expired" exception will be thrown.
                Mail oMail = new Mail("TryIt");
                oMail.Load(emlFile, false);

                if (oMail.IsEncrypted)
                {
                    try
                    {
                        //this email is encrypted, we decrypt it by user default certificate.
                        // you can also use specified certificate like this
                        // oCert = new Certificate();
                        // oCert.Load("c:\\test.pfx", "pfxpassword", Certificate.CertificateKeyLocation.CRYPT_USER_KEYSET)
                        // oMail = oMail.Decrypt( oCert );
                        oMail = oMail.Decrypt(null);
                    }
                    catch (Exception ep)
                    {
                        MessageBox.Show(ep.Message);
                        oMail.Load(emlFile, false);
                    }
                }

                if (oMail.IsSigned)
                {
                    try
                    {
                        //this email is digital signed.
                        EAGetMail.Certificate cert = oMail.VerifySignature();
                        MessageBox.Show("This email contains a valid digital signature.");
                        //you can add the certificate to your certificate storage like this
                        //cert.AddToStore( Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
                        //	"addressbook" );
                        // then you can use send the encrypted email back to this sender.
                    }
                    catch (Exception ep)
                    {
                        MessageBox.Show(ep.Message);
                    }
                }

                mMessageText = oMail.TextBody;
                // decode winmail.dat (Outlook TNEF stream) automatically.
                // also convert RTF body to HTML body automatically.
                oMail.DecodeTNEF();

                string html = oMail.HtmlBody;
                StringBuilder hdr = new StringBuilder();

                hdr.Append("<font face=\"Courier New,Arial\" size=2>");
                hdr.Append("<b>From:</b> " + _FormatHtmlTag(oMail.From.ToString()) + "<br>");
                MailAddress[] addrs = oMail.To;
                int count = addrs.Length;
                if (count > 0)
                {
                    hdr.Append("<b>To:</b> ");
                    for (int i = 0; i < count; i++)
                    {
                        hdr.Append(_FormatHtmlTag(addrs[i].ToString()));
                        if (i < count - 1)
                        {
                            hdr.Append(";");
                        }
                    }
                    hdr.Append("<br>");
                }

                addrs = oMail.Cc;

                count = addrs.Length;
                if (count > 0)
                {
                    hdr.Append("<b>Cc:</b> ");
                    for (int i = 0; i < count; i++)
                    {
                        hdr.Append(_FormatHtmlTag(addrs[i].ToString()));
                        if (i < count - 1)
                        {
                            hdr.Append(";");
                        }
                    }
                    hdr.Append("<br>");
                }

                hdr.Append(String.Format("<b>Subject:</b>{0}<br>\r\n", _FormatHtmlTag(oMail.Subject)));

                Attachment[] atts = oMail.Attachments;
                count = atts.Length;
                if (count > 0)
                {
                    if (!Directory.Exists(tempFolder))
                        Directory.CreateDirectory(tempFolder);

                    hdr.Append("<b>Attachments:</b>");
                    for (int i = 0; i < count; i++)
                    {
                        Attachment att = atts[i];

                        string attname = String.Format("{0}\\{1}", tempFolder, att.Name);
                        att.SaveAs(attname, true);
                        hdr.Append(String.Format("<a href=\"{0}\" target=\"_blank\">{1}</a> ", attname, att.Name));
                        if (att.ContentID.Length > 0)
                        {	//show embedded image.
                            html = html.Replace("cid:" + att.ContentID, attname);
                        }
                        else if (String.Compare(att.ContentType, 0, "image/", 0, "image/".Length, true) == 0)
                        {
                            //show attached image.
                            html = html + String.Format("<hr><img src=\"{0}\">", attname);
                        }
                    }
                }

                Regex reg = new Regex("(<meta[^>]*charset[ \t]*=[ \t\"]*)([^<> \r\n\"]*)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                html = reg.Replace(html, "$1utf-8");
                if (!reg.IsMatch(html))
                {
                    hdr.Insert(0, "<meta HTTP-EQUIV=\"Content-Type\" Content=\"text/html; charset=utf-8\">");
                }

                html = hdr.ToString() + "<hr>" + html;
                FileStream fs = new FileStream(htmlName, FileMode.Create, FileAccess.Write, FileShare.None);
                byte[] data = System.Text.UTF8Encoding.UTF8.GetBytes(html);
                fs.Write(data, 0, data.Length);
                fs.Close();
                oMail.Clear();
            }

            catch (Exception ex)
            {
                EZException ezex = new EZException(mModName + "_GenerateHtmlForEmail failed", ex);
                throw ezex;
            }

            finally
            {
                Trace.Exit(mModName + "_GenerateHtmlForEmail");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        private void ShowMail(string fileName)
        {
            Trace.Enter(mModName + "ShowMail");

            try
            {
                int pos = fileName.LastIndexOf(".");
                string mainName = fileName.Substring(0, pos);
                string htmlName = mainName + ".htm";

                string tempFolder = mainName;
                if (!File.Exists(htmlName))
                {	//we haven't generate the html for this email, generate it now.
                    _GenerateHtmlForEmail(htmlName, fileName, tempFolder);
                }

                object empty = System.Reflection.Missing.Value;
                webMail.Navigate(htmlName);
            }
            catch (Exception ep)
            {
                MessageBox.Show(ep.Message);
            }
            finally
            {
                Trace.Exit(mModName + "ShowMail");
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void zStartGetMail() // btnStart_Click(object sender, System.EventArgs e)
        {
            Trace.Enter(mModName + "zStartGetMail");

            try
            {
                if (mServer.Length == 0 || mUser == null || mUser.Length == 0 || mPassword == null || mPassword.Length == 0)
                {
                    MessageBox.Show("Please input server, user and password.");
                    return;
                }

                ServerAuthType authType = ServerAuthType.AuthLogin;
                switch (mAuthType.ToUpper())
                {
                    case "USER/LOGIN":
                        authType = ServerAuthType.AuthLogin;
                        break;
                    case "APOP":
                        authType = ServerAuthType.AuthCRAM5;
                        break;
                    case "NTLM":
                        authType = ServerAuthType.AuthNTLM;
                        break;
                    default:
                        break;
                }

                ServerProtocol protocol = ServerProtocol.Pop3;
                switch (mProtocol.ToUpper())
                {
                    case "POP3":
                        protocol = ServerProtocol.Pop3;
                        break;
                    case "IMAP4":
                        protocol = ServerProtocol.Imap4;
                        break;
                    case "EXCHANGEEW":
                        protocol = ServerProtocol.ExchangeEWS;
                        break;
                    case "EXCHANGEWEBDAV":
                        protocol = ServerProtocol.ExchangeWebDAV;
                        break;
                    default:
                        break;
                }

                oServer = new MailServer(mServer, mUser, mPassword,
                    mChkSSL, authType, protocol);

                //For evaluation usage, please use "TryIt" as the license code, otherwise the 
                //"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
                //"trial version expired" exception will be thrown.
                oClient = new MailClient("TryIt");

                //Catching the following events is not necessary, 
                //just make the application more user friendly.
                //If you use the object in asp.net/windows service or non-gui application, 
                //You need not to catch the following events.
                //To learn more detail, please refer to the code in EAGetMail EventHandler region
                oClient.OnAuthorized += new MailClient.OnAuthorizedEventHandler(OnAuthorized);
                oClient.OnConnected += new MailClient.OnConnectedEventHandler(OnConnected);
                oClient.OnIdle += new MailClient.OnIdleEventHandler(OnIdle);
                oClient.OnSecuring += new MailClient.OnSecuringEventHandler(OnSecuring);
                oClient.OnReceivingDataStream += new MailClient.OnReceivingDataStreamEventHandler(OnReceivingDataStream);

                //# bool bLeaveCopy = chkLeaveCopy.Checked;

                zGetMail(1);
                timer1.Enabled = true;
            }

            catch (Exception ex)
            {
                EZException ezex = new EZException(mModName + "zStartGetMail failed", ex);
                throw ezex;
            }

            finally
            {
                Trace.Exit(mModName + "zStartGetMail");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oClient"></param>
        private void zGetMail(int firstTime)
        {
            // UIDL is the identifier of every email on POP3/IMAP4/Exchange server, to avoid retrieve
            // the same email from server more than once, we record the email UIDL retrieved every time
            // if you delete the email from server every time and not to leave a copy of email on
            // the server, then please remove all the function about uidl.
            // UIDLManager wraps the function to write/read uidl record from a text file.
            UIDLManager oUIDLManager = new UIDLManager();

            try
            {
                // load existed uidl records to UIDLManager
                string uidlfile = String.Format("{0}\\{1}", m_curpath, m_uidlfile);
                oUIDLManager.Load(uidlfile);

                string mailFolder = String.Format("{0}\\inbox", m_curpath);
                if (!Directory.Exists(mailFolder))
                    Directory.CreateDirectory(mailFolder);

                m_bcancel = false;
                lblTotal.Text = "Connecting ...";
                oClient.Connect(oServer);
                MailInfo[] infos = oClient.GetMailInfos();
                lblTotal.Text = String.Format("Total {0} email(s)", infos.Length);
                Application.DoEvents();

                // remove the local uidl which is not existed on the server.
                oUIDLManager.SyncUIDL(oServer, infos);
                oUIDLManager.Update();

                int count = infos.Length;

                //lstMail.Items.Clear();
                for (int i = 0; i < count; i++)
                {
                    MailInfo info = infos[i];
                    if (oUIDLManager.FindUIDL(oServer, info.UIDL) != null)
                    {
                        //this email has been downloaded before.
                        if (firstTime == 0) { continue; }
                    }

                    lblTotal.Text = String.Format("Retrieving {0}/{1}...", info.Index, count);
                    Application.DoEvents();

                    Mail oMail = oClient.GetMail(info);
                    System.DateTime d = System.DateTime.Now;
                    System.Globalization.CultureInfo cur = new System.Globalization.CultureInfo("en-US");
                    string sdate = d.ToString("yyyyMMddHHmmss", cur);
                    string fileName = String.Format("{0}\\{1}{2}{3}.eml", mailFolder, sdate, d.Millisecond.ToString("d3"), i);
                    oMail.SaveAs(fileName, true);

                    ListViewItem item = new ListViewItem(oMail.From.ToString());
                    string sub = oMail.Subject;
                    int idx = sub.IndexOf("(Trial Version)");
                    if (idx > -1)
                    {
                        sub = sub.Remove(idx, 15);
                    }
                    item.SubItems.Add(sub);
                    item.SubItems.Add(oMail.ReceivedDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    item.Font = new System.Drawing.Font(item.Font, FontStyle.Bold);
                    item.Tag = fileName;
                    lstMail.Items.Insert(0, item);
                    oMail.Clear();

                    lblTotal.Text = String.Format("Total {0} email(s)", lstMail.Items.Count);

                    if (mLeaveCopy)
                    {
                        //add the email uidl to uidl file to avoid we retrieve it next time. 
                        oUIDLManager.AddUIDL(oServer, info.UIDL, fileName);
                    }
                }

                if (!mLeaveCopy)
                {
                    lblTotal.Text = "Deleting ...";
                    Application.DoEvents();
                    for (int i = 0; i < count; i++)
                    {
                        oClient.Delete(infos[i]);
                        // Remove UIDL from local uidl file.
                        oUIDLManager.RemoveUIDL(oServer, infos[i].UIDL);
                    }
                }
                // Delete method just mark the email as deleted, 
                // Quit method pure the emails from server exactly.
                oClient.Quit();

            }
            catch (Exception ep)
            {
                MessageBox.Show(ep.Message);
            }

            // Update the uidl list to local uidl file and then we can load it next time.
            oUIDLManager.Update();

            lblTotal.Text = "Completed";
            pgBar.Maximum = 100;
            pgBar.Minimum = 0;
            pgBar.Value = 0;
            pgBar.Visible = false;
        }

        private void frmMessageCenter_Shown(object sender, EventArgs e)
        {
            zStartGetMail();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTotal.Text = "Getting mail";
            Application.DoEvents();
            zGetMail(0);
            lblTotal.Text = lstMail.Items.Count.ToString() + " items.";
            Application.DoEvents();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            SendMailNew frm = new SendMailNew(mCommon);
            frm.ShowDialog();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            SendMailNew frm = new SendMailNew(mCommon);
            frm.MailBody = "\r\n\r\n==========\r\n" + mCommon.User.UserName + "\r\r" + mCommon.User.MailUserName + "\r\n\r\n==========\r\n" + mMessageText;
            frm.MailSubject = "Fwd: " + mSubject;
            frm.ShowDialog();
        }

        private void btnReply_Click(object sender, EventArgs e)
        {
            SendMailNew frm = new SendMailNew(mCommon);
            frm.MailBody = "\r\n\r\n==========\r\n" + mCommon.User.UserName + "\r\r" + mCommon.User.MailUserName + "\r\n\r\n==========\r\n" + mMessageText;
            frm.MailSubject = "Re: " + mSubject;
            frm.MailTo = mFrom;
            frm.ShowDialog();
        }

    }
}
