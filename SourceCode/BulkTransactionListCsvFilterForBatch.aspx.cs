using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using EFTN.Utility;
using EFTN.component;
using System.Web.UI;

namespace EFTN
{
    public partial class BulkTransactionListCsvFilterForBatch : System.Web.UI.Page
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
            //int DepartmentID = 0;
            //if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            //{
            //    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            //}
            //Truncate_Table();
            string sqlConnection = ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            EFTN.component.EFTCsvScbBulkDB csvDB = new component.EFTCsvScbBulkDB();
            //csvDB.UpdateEDRSentto1000ForCSVSCB(myConnection);

            dtStsData = csvDB.GetBulkTransactionsUploadedFromSCBTempToFilterPage(sqlConnection);
            dtgFilteredBatchTransactionSent.DataSource = dtStsData;
            dtgFilteredBatchTransactionSent.DataBind();
            BindTransactionTotal(dtStsData);          
        }

        private void BindTransactionTotal(DataTable dataLoadedOnGrid)
        {
            decimal totalAmount = 0;
            if (dataLoadedOnGrid.Rows.Count > 0)
            {                
                lblTotalFilteredTransaction.Text = "Total Transaction : " + dataLoadedOnGrid.Rows.Count.ToString();
                foreach (DataRow item in dataLoadedOnGrid.Rows)
                {
                    totalAmount = totalAmount + Convert.ToDecimal(item["Invoice Amount"]);
                }
                lblFilteredTotalAmount.Text = "Total Amount : " + totalAmount.ToString();
            }
            else
            {             
                lblTotalFilteredTransaction.Text = string.Empty;
                lblFilteredTotalAmount.Text = string.Empty;
            }

        }
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

        protected void dtgFilteredBatchTransactionSent_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgFilteredBatchTransactionSent.CurrentPageIndex = e.NewPageIndex;
            dtgFilteredBatchTransactionSent.DataSource = dtStsData;
            dtgFilteredBatchTransactionSent.DataBind();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string sqlConnection = ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            EFTCsvScbBulkDB csvDB = new EFTCsvScbBulkDB();
            DataTable alreadyExistsMovedTtransactions = new DataTable();
            alreadyExistsMovedTtransactions = csvDB.GetMovedTransactionsToCreateBatch(sqlConnection);
            if (alreadyExistsMovedTtransactions.Rows.Count == 0)
            {
                try
                {
                    for (int i = 0; i < dtgFilteredBatchTransactionSent.Items.Count; i++)
                    {
                        int recordId = 0;
                        CheckBox cbx = (CheckBox)dtgFilteredBatchTransactionSent.Items[i].FindControl("CheckBEFTNList");
                        if (cbx.Checked)
                        {
                            recordId = Convert.ToInt32(dtgFilteredBatchTransactionSent.DataKeys[i].ToString());
                            csvDB.UpdateTransactionAndMoveForBatch(sqlConnection, recordId);
                        }
                    }
                    dtgFilteredBatchTransactionSent.DataSource = null;
                    dtgFilteredBatchTransactionSent.DataBind();
                }
                catch (Exception)
                {
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please create Batch for moved transactions first!')", true);                
            }
            //Response.Redirect("BulkTransactionListCsvMovedForBatch.aspx");
        }       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //add search code here
            string sqlConnection = ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            EFTCsvScbBulkDB csvDB = new EFTCsvScbBulkDB();

            DataTable filteredData = new DataTable();
            filteredData = csvDB.GetFilteredDataFromSCBCsvTemp(sqlConnection,txtAccountNo.Text, txtCustomerName.Text, txtEntryDescription.Text);            
            //dtgFilteredBatchTransactionSent.DataSource = null;
            //dtgFilteredBatchTransactionSent.DataBind();
            dtgFilteredBatchTransactionSent.DataSource = filteredData;
            dtgFilteredBatchTransactionSent.DataBind();
            BindTransactionTotal(filteredData);
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgFilteredBatchTransactionSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgFilteredBatchTransactionSent.Items[i].FindControl("CheckBEFTNList");
                cbx.Checked = checkAllChecked;
            }
        }

       
    }
}
