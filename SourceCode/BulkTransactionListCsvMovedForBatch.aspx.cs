using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using EFTN.Utility;
using EFTN.component;
using EFTN.BLL;

namespace EFTN
{
    public partial class BulkTransactionListCsvMovedForBatch : System.Web.UI.Page
    {
        private static DataTable dtStsData = new DataTable();

        #region properties      

       // private string companyId = "";       
        private int typeOfPayment = 0;       
        private string eFTTransactionType = "";      
        private Guid transactionID = new Guid();       
        private int createdBy = 0;
        private int departmentID=0;
              
        #endregion      


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDatagridOfMovedTransactions();
            }
        }

        private void BindDatagridOfMovedTransactions()
        {
            string myConnection = ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            EFTCsvScbBulkDB csvDB = new EFTCsvScbBulkDB();
            //csvDB.UpdateEDRSentto1000ForCSVSCB(myConnection);

            dtStsData = csvDB.GetMovedTransactionsToCreateBatch(myConnection);
            dtgMovedBatchTransactionSent.DataSource = dtStsData;
            dtgMovedBatchTransactionSent.DataBind();
            BindTransactionTotal(dtStsData);

            // csvDB.TruncateTempSentEDRCsvTable(myConnection);
        }

        private void BindTransactionTotal(DataTable dataLoadedOnGrid)
        {
            decimal totalAmount = 0;
            if (dataLoadedOnGrid.Rows.Count > 0)
            {
                lblTotalMovedTransaction.Text = "Total Transaction : " + dataLoadedOnGrid.Rows.Count.ToString();
                foreach (DataRow item in dataLoadedOnGrid.Rows)
                {
                    totalAmount = totalAmount + Convert.ToDecimal(item["Invoice Amount"]);
                }
                lblMovedTotalAmount.Text = "Total Amount : " + totalAmount.ToString();
            }
            else
            {
                lblTotalMovedTransaction.Text = string.Empty;
                lblMovedTotalAmount.Text = string.Empty;
            }
        }

        #region Old Button Codes
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
        //    for (int i = 0; i < dtgFilteredBatchTransactionSent.Items.Count; i++)
        //    {
        //        string strTransactionId = dtgFilteredBatchTransactionSent.DataKeys[i].ToString();
        //        Guid TransactionID = new Guid(strTransactionId);
        //        sentEDRDB.UpdateEDRSentto1(TransactionID);
        //    }
        //    Response.Redirect("EFTMaker.aspx");
        //}

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
        //    for (int i = 0; i < dtgFilteredBatchTransactionSent.Items.Count; i++)
        //    {
        //        string strTransactionId = dtgFilteredBatchTransactionSent.DataKeys[i].ToString();
        //        Guid TransactionID = new Guid(strTransactionId);
        //        sentBatchDB.DeleteBatchSent(TransactionID);
        //    }
        //    Response.Redirect("EFTMaker.aspx");
        //}
        #endregion

        protected void dtgMovedBatchTransactionSent_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgMovedBatchTransactionSent.CurrentPageIndex = e.NewPageIndex;
            dtgMovedBatchTransactionSent.DataSource = dtStsData;
            dtgMovedBatchTransactionSent.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //add Create Batch Code
            string entryDetail = txtEntryDetail.Text;
            string sqlConnection = ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            EFTCsvScbBulkDB csvDB = new EFTCsvScbBulkDB();
            if (entryDetail == null || entryDetail == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please provide entry description first!')", true);
                return;                
            }
            else
            {
                createdBy = ParseData.StringToInt(Request.Cookies["UserID"].Value);
                departmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                csvDB.UpdateTempSentEDRColumnForScbCSV_ToCreateBatch(this.transactionID,createdBy, this.eFTTransactionType, this.typeOfPayment, departmentID, sqlConnection);
                csvDB.DistributeBatchWithEntryDescription(createdBy, departmentID, sqlConnection, entryDetail);
                DataTable dataToBeFiltered = csvDB.GetBulkTransactionsUploadedFromSCBTempToFilterPage(sqlConnection);
                if (dataToBeFiltered.Rows.Count > 0)
                {
                    Response.Redirect("BulkTransactionListCsvFilterForBatch.aspx");
                }
                else
                {
                    Response.Redirect("BulkTransactionListCsvSCB.aspx");
                }                
            }
        }

        //protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool checkAllChecked = cbxAll.Checked;
        //    for (int i = 0; i < dtgMovedBatchTransactionSent.Items.Count; i++)
        //    {
        //        CheckBox cbx = (CheckBox)dtgMovedBatchTransactionSent.Items[i].FindControl("CheckMovedList");
        //        cbx.Checked = checkAllChecked;
        //    }
        //}

    }
}
