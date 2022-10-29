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
using EFTN.component;
using EFTN.Utility;
using FloraSoft;
using System.Data.SqlClient;


namespace EFTN
{
    public partial class InwardReturnModifierRFC : System.Web.UI.Page
    {
        //private static Guid TransactionID;
        private static string ServiceClassCode = string.Empty;
        private static string SECC = string.Empty;
        private static int TypeOfPayment = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindBank();
                //BindBranch();
                BindDataForReturn();
            }
        }

        public void BindDataForReturn()
        {
            Guid ReturnID =new Guid(Request.Params["inwardReturnID"].ToString());
            ReceivedReturnDB receivedReturnDB = new ReceivedReturnDB();
            //SqlDataReader sqlDR = receivedReturnDB.GetReceivedReturnbyReturnID(ReturnID);
            DataTable dtRR = new DataTable();
            dtRR = receivedReturnDB.GetReceivedReturnbyReturnID(ReturnID);


            for (int rRCount = 0; rRCount < dtRR.Rows.Count; rRCount++)
            {
                txtAccountNumber.Text = dtRR.Rows[rRCount]["AccountNo"].ToString();
                txtRoutingNo.Text = dtRR.Rows[rRCount]["ReceivingBankRoutingNo"].ToString();
                txtReasonForPayment.Text = dtRR.Rows[rRCount]["PaymentInfo"].ToString();
                txtDFIAccountNumber.Text = dtRR.Rows[rRCount]["DFIAccountNo"].ToString();
                txtAmount.Text = dtRR.Rows[rRCount]["Amount"].ToString();
                txtReceiverName.Text = dtRR.Rows[rRCount]["ReceiverName"].ToString();
                rdoBtnReceiverAccountType.SelectedValue = dtRR.Rows[rRCount]["ReceiverAccountType"].ToString();
                ServiceClassCode = dtRR.Rows[rRCount]["ServiceClassCode"].ToString();
                SECC = dtRR.Rows[rRCount]["SECC"].ToString();
                TypeOfPayment = ParseData.StringToInt(dtRR.Rows[rRCount]["TypeOfPayment"].ToString());
                txtEntryDesc.Text = dtRR.Rows[rRCount]["EntryDesc"].ToString();
            }
        }

        protected void btnSaveAndExit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertTransaction("SaveAndExit");
            } 
        }

        private void InsertTransaction(string transactionType)
        {

            int EnvelopID = -1;
            DateTime EffectiveEntryDate = System.DateTime.Now;
            //DateTime EffectiveEntryDate = System.DateTime.Today.AddDays(1);
            //int SettlementJDate = 0;
            string CompanyId = ConfigurationManager.AppSettings["CompanyId"];
            string CompanyName = ConfigurationManager.AppSettings["CompanyName"];
            string EntryDesc = string.Empty;


            EntryDesc = txtEntryDesc.Text.Trim();

            //int CreatedBy = ParseData.StringToInt(Request.Cookies["UserID"].Value);
            string TransactionCode = string.Empty;

            if (rdoBtnReceiverAccountType.SelectedValue.Equals("1"))
            {
                TransactionCode = TransactionCodes.CreditCurrentAcc.ToString();
            }
            else if (rdoBtnReceiverAccountType.SelectedValue.Equals("2"))
            {
                TransactionCode = TransactionCodes.CreditSavingsAcc.ToString();
            }
            else
            {
                lblMsg.Text = "Select Account Type  " + rdoBtnReceiverAccountType.SelectedValue;
                return;
            }

            int ReceiverAccountType = ParseData.StringToInt(rdoBtnReceiverAccountType.SelectedValue);
            string DFIAccountNo = txtDFIAccountNumber.Text.Trim();
            string AccountNo = txtAccountNumber.Text.Trim();

            //FloraSoft.BanksDB banksDB = new FloraSoft.BanksDB();

            string ReceivingBankRoutingNo = txtRoutingNo.Text;

            int TypeOfAccount = ParseData.StringToInt(rdoBtnReceiverAccountType.SelectedValue);
            decimal Amount = ParseData.StringToDecimal(txtAmount.Text.Trim());
            string IdNumber = txtAccountNumber.Text.Trim(); //// change///////////////////////
            string ReceiverName = txtReceiverName.Text.Trim();
            int StatusID = TransactionStatus.TransSent_Data_Entered;
            string PaymentInfo = txtReasonForPayment.Text.Trim();
            Guid ReturnID = new Guid(Request.Params["inwardReturnID"].ToString());
            //string ReturnID = Request.Params["inwardReturnID"].ToString();
            SentBatchDB sentBatchDB = new SentBatchDB();
            Guid TransactionID = new Guid();

            string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

            try
            {
                int UserID =  ParseData.StringToInt(Request.Cookies["UserID"].Value);
                StandingOrderDB sentEDRDB = new StandingOrderDB();
                sentEDRDB.InsertTransactionSentForRFCReturn(ReturnID, UserID, Amount);
                //sentEDRDB.InsertTransactionSent(transa
                lblMsg.Text = "Saved successfully";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                if (transactionType.Equals("SaveAndExit"))
                {
                    TransactionID = Guid.Empty;
                    Response.Redirect("InwardReturnMakerRFC.aspx", false);
                }
            }
            catch
            {
                lblMsg.Text = "Failed to Save";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("InwardReturnMakerRFC.aspx");
        }
    }
}
