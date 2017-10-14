using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EZUtils
{
    public partial class ExceptionDialog : Form
    {
        private EZException m_e;


        public ExceptionDialog(Exception e, String message)
        {
            InitializeComponent();
            try
            {
                if (e == null)
                {
                    m_e = new EZException("Error information is missing");
                }
                else if (EZException.IsEZException(e))
                {
                    m_e = (e as EZException);
                }
                else
                {
                    m_e = new EZException(e.Message, e);
                }

                this.Text = Application.ProductName;
                this.textBoxErrorMessage.Text = message + e.Message;

                zHideDetails();
                zInitDetails(m_e.TopNonEZExceptionSummary, m_e.BottomEZExceptionSummary);
                //buttonSend.Enabled = Mail.ConfiguredToSendToSRSSupport();
            }
            catch
            {
            }
        }


        public ExceptionDialog(String EZExceptioXML, String message)
        {
            ExceptionSummary EZExceptionSummary;
            ExceptionSummary BottomEZExceptionSummary;
            ExceptionSummary TopNonEZExceptionSummary;

            InitializeComponent();
            try
            {
                EZException.ExtractSummaryFromExceptionXML(EZExceptioXML, out EZExceptionSummary,
                    out BottomEZExceptionSummary, out TopNonEZExceptionSummary);

                this.Text = Application.ProductName;
                this.textBoxErrorMessage.Text = message + EZExceptionSummary.Message;

                zHideDetails();
                zInitDetails(TopNonEZExceptionSummary, BottomEZExceptionSummary);
                //buttonSend.Enabled = Mail.ConfiguredToSendToSRSSupport();
            }
            catch
            {
            }
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            if (panelDetails.Visible)
            {
                zHideDetails();
            }
            else
            {
                zShowDetails();
            }
        }

        private void zShowDetails()
        {
            this.Height += panelDetails.Height + 15;
            panelDetails.Visible = true;
        }

        private void zHideDetails()
        {
            panelDetails.Visible = false;
            this.Height -= panelDetails.Height + 15;
       }

        private void zInitDetails(ExceptionSummary TopNonEZExceptionSummary, 
            ExceptionSummary BottomEZExceptionSummary)
        {
            this.textBoxSysErrorType.Text = TopNonEZExceptionSummary.ExceptionType;
            this.textBoxSysErrorMsg.Text = TopNonEZExceptionSummary.Message;
            this.textBoxEZMessage.Text = BottomEZExceptionSummary.Message;
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText((m_e as EZException).OuterXml);
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
           //Mail.SendToSRSSupport((m_e as SRSException).OuterXml);
        }
    }
}
