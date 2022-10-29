using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using EFTN.Utility;

namespace EFTN
{
    public partial class BulkTransactionListCsvSCB : System.Web.UI.Page
    {
        private static DataTable dtStsData = new DataTable();
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
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }
            //Truncate_Table();
            string myConnection = ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            EFTN.component.EFTCsvScbBulkDB csvDB = new component.EFTCsvScbBulkDB();
            csvDB.UpdateEDRSentto1000ForCSVSCB(myConnection);

            dtStsData = csvDB.GetBulkTransactionSentUploadedOnlyCsvSCB(DepartmentID, myConnection);
            dtgBatchTransactionSent.DataSource = dtStsData;
            dtgBatchTransactionSent.DataBind();
            BindBatchTotal(dtStsData);

           // csvDB.TruncateTempSentEDRCsvTable(myConnection);          // commented out by junayed
        }

        private void BindBatchTotal(DataTable myDTCorUpload)
        {
            if (myDTCorUpload.Rows.Count > 0)
            {
                lblTotalBatch.Text = "Total Batch : " + myDTCorUpload.Rows.Count.ToString();
                lblTotalTransaction.Text = "Total Transaction : " + myDTCorUpload.Compute("SUM(TotalTransactions)", "").ToString();
                lblTotalAmount.Text = "Total Amount : " + myDTCorUpload.Compute("SUM(TotalAmount)", "").ToString();
            }
            else
            {
                lblTotalBatch.Text = string.Empty;
                lblTotalTransaction.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                string strTransactionId = dtgBatchTransactionSent.DataKeys[i].ToString();
                Guid TransactionID = new Guid(strTransactionId);
                sentEDRDB.UpdateEDRSentto1(TransactionID);
            }
            Response.Redirect("EFTMaker.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                string strTransactionId = dtgBatchTransactionSent.DataKeys[i].ToString();
                Guid TransactionID = new Guid(strTransactionId);
                sentBatchDB.DeleteBatchSent(TransactionID);
            }
            Response.Redirect("EFTMaker.aspx");
        }

        protected void dtgBatchTransactionSent_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgBatchTransactionSent.CurrentPageIndex = e.NewPageIndex;
            dtgBatchTransactionSent.DataSource = dtStsData;
            dtgBatchTransactionSent.DataBind();
        }
    }
}
