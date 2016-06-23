using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EZDesk
{
    /// <summary>
    /// Edits the editable fields of a document properties record.
    /// After the caller creates an instance of this class the
    /// caller must specify the Doc Id to be edited. On exiting
    /// the form DialogResult is OK if the user saved the updates
    /// and Cancel if they cancelled out of the form.
    /// </summary>
    public partial class frmDocumentEdit : Form
    {
        //DONE: 20141028 Filled in the fields from the document record
        //DONE: 20141028 Save the updates and return OK
        //DONE: 20141028 Return Cancel if cancel button pressed.

        private EZDeskDataLayer.EZDeskCommon mCommon = null;
        private EZDeskDataLayer.Documents.DocumentsController dCtrl = null;
        private EZDeskDataLayer.Documents.Models.documentDetail mDoc = null;
        private int mDocId = -1;

        /// <summary>
        /// The caller has set the docId that needs to be edited.
        /// </summary>
        public int docId 
        {
            get { return mDocId; }
            set
            {
                mDocId = value;
                mDoc = dCtrl.GetDocumentById(mDocId);
                zFillForm();
            }
        }

        /// <summary>
        /// Save common in case we need it. Initialize a document
        /// controller to read the record and possibly save the record.
        /// </summary>
        /// <param name="common"></param>
        public frmDocumentEdit(EZDeskDataLayer.EZDeskCommon common)
        {
            InitializeComponent();
            mCommon = common;
            dCtrl = new EZDeskDataLayer.Documents.DocumentsController(mCommon);
        }

        /// <summary>
        /// The user is saving the updates made to the name and to IsActive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool valid = true;
            
            if (tbDocName.Text.Length > 0) { mDoc.Name = tbDocName.Text.Trim(); }
            else { valid = false; }
            mDoc.IsActive = cbIsActive.Checked;
            if (valid)
            {
                dCtrl.WriteDocumnet(mDoc, null);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("You must have some name", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbDocName.Focus();
            }
        }

        /// <summary>
        /// Fill the fields with the data for this document id.
        /// </summary>
        private void zFillForm()
        {
            tbDocId.Text = mDoc.Id.ToString();
            tbPersonId.Text = mDoc.PersonId.ToString();
            tbTabId.Text = mDoc.TabId.ToString();
            dtpCreated.Value = mDoc.Created;
            cbIsActive.Checked = mDoc.IsActive;
            tbDocName.Text = mDoc.Name;
            tbDocFullPathName.Text = mDoc.PathName;
            tbGroupRestriction.Text = mDoc.GroupRestriction.ToString();
        }

    }
}
