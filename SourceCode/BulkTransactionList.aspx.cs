using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using EFTN.Utility;
using Ionic.Zip;
using EFTN.component;

namespace EFTN
{
    public partial class BulkTransactionList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataForTransactionSent();
            }
        }

        private void BindDataForTransactionSent()
        {
            int DepartmentID = 0;
            int createdBy = 0;
            createdBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);                
            }
            EFTN.component.SentBatchDB batchDB = new EFTN.component.SentBatchDB();

            dtgBatchTransactionSent.DataSource = batchDB.GetBulkTransactionUploadedOnly(DepartmentID, createdBy); // user added on  06-03-2018
            dtgBatchTransactionSent.DataBind();
        }

        /** This checkbox has been disabled according to the Flora and SCB business department _ 05 - 03 - 2018 **/
        //protected void cbxAllTransactionSent_CheckedChanged(object sender, EventArgs e)
        //{

        //    bool checkAllChecked = cbxAllTransactionSent.Checked;
        //    for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
        //    {
        //        CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");
        //        cbx.Checked = checkAllChecked;
        //    }
        //}

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
            EFTCsvScbBulkDB csvDb = new EFTCsvScbBulkDB();

            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                /** This checkbox has been disabled according to the Flora and SCB business department _ 05 - 03 - 2018 **/
                //CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");
                //if (cbx.Checked)
                //{
                string transactionID = dtgBatchTransactionSent.DataKeys[i].ToString().ToUpper();
                Guid TransactionID = new Guid(transactionID);
                /**   Commented out as they need accept multiple currencies batch _ 04-03-2018  **/
                // sentEDRDB.UpdateEDRSentto1(TransactionID);
                //Added new _ 04-03-2018
                sentEDRDB.UpdateEDRSentFromCSVAndDeleteTempData(TransactionID);
                //}
            }
            BindDataForTransactionSent();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                /** This checkbox has been disabled according to the Flora and SCB business department _ 05 - 03 - 2018 **/
                //CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");
                //if (cbx.Checked)
                //{
                string transactionID = dtgBatchTransactionSent.DataKeys[i].ToString();
                Guid TransactionID = new Guid(transactionID);
                sentBatchDB.DeleteBatchSent(TransactionID);
                //}
            }
            BindDataForTransactionSent();
        }
    }
}
