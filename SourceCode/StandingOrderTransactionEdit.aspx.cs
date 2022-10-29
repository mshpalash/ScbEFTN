using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using EFTN.component;
using EFTN.Utility;
using FloraSoft;
using EFTN.BLL;
using System.Globalization;

namespace EFTN
{
    public partial class StandingOrderTransactionEdit : System.Web.UI.Page
    {
        //private static Guid TransactionID;
        //private static string BatchNumber = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string trID = Request.Params["StandingOrderBatchID"].ToString();
                Guid standingOrderBatchID = new Guid(trID);

                //TransactionID = Guid.Empty;
                BindBank();
                BindBranch();
                sortOrder = "asc";
                BindChargeCategory();

                /////////////////////////
                BindDataForSTDOTransactionDetailsByStandingOrderBatchID(standingOrderBatchID);
            }
        }

        private void BindDataForSTDOTransactionDetailsByStandingOrderBatchID(Guid standingOrderBatchID)
        {
            //throw new NotImplementedException();
            StandingOrderDB sTDODB = new StandingOrderDB();

            DataTable dtSTDOTransactionDetails = sTDODB.GetStandingOrderByStandingOrderBatchID(standingOrderBatchID);
            if(dtSTDOTransactionDetails.Rows.Count>0)
            {

                txtCompanyID.Text = dtSTDOTransactionDetails.Rows[0]["CompanyId"].ToString();
                txtCompanyName.Text= dtSTDOTransactionDetails.Rows[0]["CompanyName"].ToString();
                txtCompanyEntryDescription.Text= dtSTDOTransactionDetails.Rows[0]["EntryDesc"].ToString();
                txtReceiverName.Text= dtSTDOTransactionDetails.Rows[0]["ReceiverName"].ToString();
                txtReceiverID.Text= dtSTDOTransactionDetails.Rows[0]["IdNumber"].ToString();
                ddListReceivingBank.SelectedValue= dtSTDOTransactionDetails.Rows[0]["BankID"].ToString();
                ddListBranch.SelectedValue= dtSTDOTransactionDetails.Rows[0]["ReceivingBankRoutingNo"].ToString();
                txtAccountNumber.Text= dtSTDOTransactionDetails.Rows[0]["AccountNo"].ToString();
                txtAmount.Text= dtSTDOTransactionDetails.Rows[0]["Amount"].ToString();
                txtReasonForPayment.Text= dtSTDOTransactionDetails.Rows[0]["PaymentInfo"].ToString();
                txtDFIAccountNumber.Text= dtSTDOTransactionDetails.Rows[0]["DFIAccountNo"].ToString();
                rdoBtnReceiverAccountType.SelectedValue= dtSTDOTransactionDetails.Rows[0]["ReceiverAccountType"].ToString();
                txtOrderingCustomer.Text= dtSTDOTransactionDetails.Rows[0]["OrderingCustomer"].ToString();
                txtCustomerLetterReferenceNo.Text= dtSTDOTransactionDetails.Rows[0]["CustomerLetterReference"].ToString();

                ddListTransactionFrequency.SelectedValue= dtSTDOTransactionDetails.Rows[0]["TransactionFrequency"].ToString();

                DateTime dtStdOrderStartDate = DateTime.Parse(dtSTDOTransactionDetails.Rows[0]["BeginDate"].ToString());
                ddlistDay.SelectedValue = dtStdOrderStartDate.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = dtStdOrderStartDate.Month.ToString();
                ddlistYear.SelectedValue = dtStdOrderStartDate.Year.ToString().PadLeft(4, '0');

                DateTime dtStdOrderEndDate = DateTime.Parse(dtSTDOTransactionDetails.Rows[0]["EndDate"].ToString());
                ddlistDayEnd.SelectedValue = dtStdOrderEndDate.Day.ToString().PadLeft(2, '0');
                ddlistMonthEnd.SelectedValue = dtStdOrderEndDate.Month.ToString();
                ddlistYearEnd.SelectedValue = dtStdOrderEndDate.Year.ToString().PadLeft(4, '0');
            }

            DataTable dtSTDOExecutedTransactions = sTDODB.GetStandingOrderBatchDateAlreadyExecuted(standingOrderBatchID);
            if (dtSTDOExecutedTransactions.Rows.Count > 0)
            {
                btnSave.Enabled = false;
                lblMsg.Text = "This Batch is not Editable because the batch is already executd transactions. Please Inactive this batch and upload new batch";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                EdittTransaction();
                //Response.Redirect("StandingOrderTransactionList.aspx?StandingOrderBatchID=" + Request.Params["StandingOrderBatchID"].ToString());
                Response.Redirect("StandingOrderManagement.aspx" );
            }
        }

        //protected void btnSaveAndNewBatch_Click(object sender, EventArgs e)
        //{
        //    if (Page.IsValid)
        //    {
        //        InsertTransaction("SaveAndNew");
        //    }
        //}

        //protected void btnSaveAndExit_Click(object sender, EventArgs e)
        //{
        //    if (Page.IsValid)
        //    {
        //        InsertTransaction("SaveAndExit");
        //    } 
        //}

        private void EdittTransaction()
        {
            string trID = Request.Params["StandingOrderBatchID"].ToString();
            Guid standingOrderBatchID = new Guid(trID);

            int receiverAccountType = ParseData.StringToInt(rdoBtnReceiverAccountType.SelectedValue);
            string dfiAccNo = txtDFIAccountNumber.Text.Trim();
            string accNo = txtAccountNumber.Text.Trim();

            string receivingBankRoutingNo = (ddListBranch.SelectedValue);
            double amount = ParseData.StringToDouble(txtAmount.Text.Trim());
            string idNumber = txtReceiverID.Text.Trim();
            string receiverName = txtReceiverName.Text.Trim();
            int transactionFrequency = ParseData.StringToInt(ddListTransactionFrequency.SelectedValue);
            string stringBegDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
            string stringEndDate = ddlistDayEnd.SelectedValue + "/" + ddlistMonthEnd.SelectedValue.PadLeft(2, '0') + "/" + ddlistYearEnd.SelectedValue;
            DateTime stdBeginDate = new DateTime();
            DateTime stdEndDate = new DateTime();
            EFTN.component.StandingOrderDB edrDB = new EFTN.component.StandingOrderDB();
            int CreatedBy = ParseData.StringToInt(Request.Cookies["UserID"].Value);
            string OrderingCustomer = txtOrderingCustomer.Text.Trim();
            //string StandingOrderReference = txtStandingOrderReference.Text.Trim();
            string CustomerLetterReference = txtCustomerLetterReferenceNo.Text.Trim();
            string strCharge = rdoBtnCharge.SelectedValue;

            bool Charge = true;

            string companyID = txtCompanyID.Text.Trim();
            string companyName = txtCompanyName.Text.Trim();
            string companyEntryDescription = txtCompanyEntryDescription.Text.Trim();
            string reasonForPayment = txtReasonForPayment.Text.Trim();

            if (strCharge.Equals("0"))
            {
                Charge = false;
            }
            Guid BundleID = Guid.NewGuid();

            if (!stringBegDate.Equals(string.Empty))
            {
                try
                {
                    stdBeginDate = DateTime.ParseExact(stringBegDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //stdBeginDate = DateTime.Parse(stringBegDate);
                }
                catch
                {
                    lblMsg.Text = "Invalid Begin Date";
                    return;
                }
            }
            else
            {
                lblMsg.Text = "Invalid Begin Date";
                return;
            }
            if (!stringEndDate.Equals(string.Empty))
            {
                try
                {
                    stdEndDate = DateTime.ParseExact(stringEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //stdEndDate = DateTime.Parse(stringEndDate);
                }
                catch
                {
                    lblMsg.Text = "Invalid End Date";
                    return;
                }
            }
            else
            {
                lblMsg.Text = "Invalid End Date";
                return;
            }

            if (stdBeginDate < System.DateTime.Today)
            {
                lblMsg.Text = "Standing order start date (" + stdBeginDate.ToString("dd-MM-yyyy") + ") can not be earlier than today ";
                return;
            }
            if (stdEndDate <= stdBeginDate)
            {
                lblMsg.Text = "End Date must be at a later date than Start Date";
                return;
            }
            string SECC = "CIE";

            edrDB.UpdateStandingOrderBatchAndEDREntryDateForSCB(dfiAccNo,
                    accNo,
                    receivingBankRoutingNo,
                    amount,
                    receiverName,
                    idNumber,
                    reasonForPayment,
                    stdBeginDate,
                    stdEndDate,
                    standingOrderBatchID
                    ,OrderingCustomer
                    //StandingOrderReference, 
                    ,CustomerLetterReference
                    ,companyID
                    ,companyName
                    ,companyEntryDescription
                    ,CreatedBy
                    );
            lblMsg.Text = "Updated Successfully";
            BindImportedExcel(standingOrderBatchID);
        }

        private void BindBank()
        {
            FloraSoft.BanksDB db = new FloraSoft.BanksDB();

            ddListReceivingBank.DataSource = db.GetAllBanks();
            ddListReceivingBank.DataTextField = "BankName";
            ddListReceivingBank.DataValueField = "BankID";
            ddListReceivingBank.DataBind();
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        private void BindBranch()
        {
            FloraSoft.BranchesDB branchesDB = new FloraSoft.BranchesDB();

            ddListBranch.DataSource = branchesDB.GetBranchesByBankID(ParseData.StringToInt(ddListReceivingBank.SelectedValue));
            ddListBranch.DataBind();
            txtRoutingNo.Text = ddListBranch.SelectedValue;
        }

        protected void ddListBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoutingNo.Text = ddListBranch.SelectedValue;
        }

        protected void ddListReceivingBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBranch();
        }

        protected void CheckeBBS_Click(object sender, EventArgs e)
        {
            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            AccountEnquiryDB accEnqDB = new AccountEnquiryDB();
            string CCY = accEnqDB.GetCCY_Code("BDT");

            if (bankCode.Equals(OriginalBankCode.NRB) || bankCode.Equals(OriginalBankCode.UCBL))
            {
                string AccountNo = txtAccountNumber.Text.Trim();
                FCUBSAccountStatusManager accStatusManager = new FCUBSAccountStatusManager();
                string AccountStatus = accStatusManager.GetFCUBSAccountStatus(AccountNo);

                if (accStatusManager.IsValidAcc)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                lblMsg.Text = AccountStatus;

            }
            else
            {
                string accNumber = txtAccountNumber.Text.Replace("-", "");
                MICRDB db = new MICRDB();

                DataTable dt = db.GetMICRAccountInfo(accNumber, CCY);
                dtgMicrInfo.DataSource = dt;
                dtgMicrInfo.DataBind();
            }
        }

        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";

                }
                else
                {
                    ViewState["sortOrder"] = "desc";
                }

                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }

        private void BindImportedExcel(Guid StandingOrderBatchID)
        {
            //SuccessMessage(myDTCorUpload.Rows.Count);
            lblMsg.Text = "Standing Order Uploaded Successfully. Please scroll down to see details";
            lblMsg.ForeColor = System.Drawing.Color.Blue;
            StandingOrderDB stdOrderDB = new StandingOrderDB();
            DataTable myDTCorUpload = new DataTable();
            myDTCorUpload = stdOrderDB.GetStandingOrderBatchByStandingOrderBatchID(StandingOrderBatchID);
            dtgXcelUpload.DataSource = myDTCorUpload;
            dtgXcelUpload.DataBind();
            if (myDTCorUpload.Rows.Count > 0)
            {
                BindStandingOrderBatchDate(StandingOrderBatchID);
            }
            ClearControlValue();
        }

        private void BindStandingOrderBatchDate(Guid StandingOrderBatchID)
        {
            StandingOrderDB stdOrderDB = new StandingOrderDB();
            DataTable stdDate = stdOrderDB.GetStandingOrderDateByStandingOrderBatchID(StandingOrderBatchID);
            dtgStandingOrderDate.DataSource = stdDate;
            dtgStandingOrderDate.DataBind();
        }

        private void ClearControlValue()
        {
            txtAccountNumber.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtDFIAccountNumber.Text = string.Empty;
            txtReasonForPayment.Text = string.Empty;
            txtReceiverName.Text = string.Empty;
            txtReceiverID.Text = string.Empty;
            txtOrderingCustomer.Text = string.Empty;
            txtCustomerLetterReferenceNo.Text = string.Empty;
            //lblMsg.Text = string.Empty;
        }

        private void BindBatchNumberByTransactionID(Guid TransactionID)
        {
            EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
            DataTable dtBatchNumber = sentBatchDB.GetBatchNumberByTransactionID(TransactionID);
            string batchnumber = string.Empty;
            if (dtBatchNumber.Rows.Count > 0)
            {
                batchnumber = dtBatchNumber.Rows[0]["BatchNumber"].ToString();
            }
            //return batchnumber;
            lblMsgBatchNumber.Text = "Batch Number: " + batchnumber;
        }

        protected void BtnRoutChk_Click(object sender, EventArgs e)
        {
            BranchesDB branchesDB = new BranchesDB();
            DataTable dtBranches = branchesDB.GetBankBranchByRoutingNumber(txtRoutnoChk.Text.Trim());
            if (dtBranches.Rows.Count > 0)
            {
                lblBank.Text = "Bank Name : " + dtBranches.Rows[0]["BankName"].ToString();
                lblBranch.Text = "Branch Name : " + dtBranches.Rows[0]["BranchName"].ToString();
            }
        }

        private void BindChargeCategory()
        {
            //string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            //if (SelectedBank.Equals("225"))
            //{
            //    EFTChargeManager eftChargeManager = new EFTChargeManager();

            //    ddListChargeCategoryList.DataSource = eftChargeManager.GetCityChargeDefineList();
            //    ddListChargeCategoryList.DataTextField = "CityChargeDefineDes";
            //    ddListChargeCategoryList.DataValueField = "CityChargeDefineID";
            //    ddListChargeCategoryList.DataBind();
            //    pnlChargeCategory.Visible = true;
            //    pnlChargeCode.Visible = true;
            //}
            //else
            //{
                pnlChargeCategory.Visible = false;
                pnlChargeCode.Visible = false;
            //}
        }

        protected void ddListChargeCategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddListChargeCategoryList.SelectedValue.Equals("2"))
            {
                pnlChargeCode.Visible = true;
            }
            else
            {
                pnlChargeCode.Visible = false;
            }
        }
    }
}
