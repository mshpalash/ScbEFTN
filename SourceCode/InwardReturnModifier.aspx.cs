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
    public partial class InwardReturnModifier : System.Web.UI.Page
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
                txtBatchType.Text = dtRR.Rows[rRCount]["BatchType"].ToString();
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
            string BatchType = txtBatchType.Text.Trim();

            EntryDesc = txtEntryDesc.Text.Trim();

            int CreatedBy = ParseData.StringToInt(Request.Cookies["UserID"].Value);
            string TransactionCode = string.Empty;

            if (rdoBtnReceiverAccountType.SelectedValue.Equals("1"))
            {
                if (BatchType.Equals(TransactionCodes.EFTTransactionTypeCredit))
                {
                    TransactionCode = TransactionCodes.CreditCurrentAcc.ToString();
                }
                else
                {
                    TransactionCode = TransactionCodes.DebitCurrentAcc.ToString();
                }
            }
            else if (rdoBtnReceiverAccountType.SelectedValue.Equals("2"))
            {
                if (BatchType.Equals(TransactionCodes.EFTTransactionTypeCredit))
                {
                    TransactionCode = TransactionCodes.CreditSavingsAcc.ToString();
                }
                else
                {
                    TransactionCode = TransactionCodes.DebitSavingsAcc.ToString();
                }
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

            SentBatchDB sentBatchDB = new SentBatchDB();
            Guid TransactionID = new Guid();

            string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

            try
            {
                if (TransactionID.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    TransactionID = sentBatchDB.InsertBatchSent(EnvelopID, ServiceClassCode
                                                            , SECC, TypeOfPayment, EffectiveEntryDate
                                                            , CompanyId, CompanyName, EntryDesc
                                                            , CreatedBy, 0, BatchType
                                                            , ParseData.StringToInt(Request.Cookies["DepartmentID"].Value)
                                                            , DataEntryType.Manual
                                                            ,"");
                }

                SentEDRDB sentEDRDB = new SentEDRDB();
                sentEDRDB.InsertTransactionSent(TransactionID, TransactionCode
                                        , ReceiverAccountType, TypeOfPayment, DFIAccountNo
                                        , AccountNo, ReceivingBankRoutingNo, Amount, IdNumber
                                        , ReceiverName, StatusID, CreatedBy
                                        , PaymentInfo, ParseData.StringToInt(Request.Cookies["DepartmentID"].Value)
                                        , EFTConnectionString, 0); // 0 for Remittance as this function is not useful
                //sentEDRDB.InsertTransactionSent(transa
                lblMsg.Text = "Saved successfully";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                if (transactionType.Equals("SaveAndExit"))
                {
                    TransactionID = Guid.Empty;
                    Response.Redirect("InwardReturnMaker.aspx", false);
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
            Response.Redirect("InwardReturnMaker.aspx");
        }


        protected void CheckeBBS_Click(object sender, EventArgs e)
        {
            string accNumber = txtAccountNumber.Text.Replace("-", "");
            AccountEnquiryDB accEnqDB = new AccountEnquiryDB();
            string CCY = accEnqDB.GetCCY_Code("BDT");

            MICRDB db = new MICRDB();

            DataTable dt = db.GetMICRAccountInfo(accNumber, CCY);
            dtgMicrInfo.DataSource = dt;
            dtgMicrInfo.DataBind();
        }
    }
}
