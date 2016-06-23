using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using EZDeskDataLayer;
using EZUtils;

namespace EZDesk
{
    /// <summary>
    /// This form displays and allows edits of the selected system available 
    /// properties. The available properties specify values that are used 
    /// system wide by all users. For example, the path to the document 
    /// repository.  Not all properties are visible! Only the properties 
    /// meant to be modified by the client. For values that would be specific 
    /// to a user use the Profile settings.
    /// </summary>
    public partial class frmAvailableProperties : Form
    {
        //DONE: 20141028 Display of the selected available property.
        //DONE: 20141028 Save of any changes.
        //DONE: 20141028 Audit of changes.

        private EZDeskDataLayer.ehr.Models.AvailablePropertyItem mItem = null;
        private EZDeskDataLayer.EZDeskCommon mCommon = null;
        private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Conn"></param>
        /// <param name="item"></param>
        public frmAvailableProperties(EZDeskDataLayer.EZDeskCommon common,
                    EZDeskDataLayer.ehr.Models.AvailablePropertyItem item)
        {
            InitializeComponent();
            mItem = item;
            mCommon = common;
            eCtrl = new EZDeskDataLayer.ehr.ehrCtrl(mCommon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAvailableProperties_Load(object sender, EventArgs e)
        {
            tbPropertyName.Text = mItem.PropertyName;
            tbPropertyDescription.Text = mItem.Description;
            tbPropertyValue.Text = mItem.PropertyValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            mItem.Description = tbPropertyDescription.Text.Trim();
            mItem.PropertyValue = tbPropertyValue.Text.Trim();
            eCtrl.WriteAvailablePropertyItem(mItem);
            EZDeskDataLayer.ehr.Models.AuditItem aItem =
                new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System, 
                        EZDeskDataLayer.ehr.Models.AuditActivities.Edit, 
                        mItem.PropertyName + " Changed");
            eCtrl.WriteAuditRecord(aItem);
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
