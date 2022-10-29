using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using EFTN.component;
using EFTN.Utility;
using FloraSoft;
using EFTN.BLL;


namespace EFTN
{
    public partial class IndToInd : System.Web.UI.Page
    {
        DataView dv;
        private static DataTable myDTCorUpload = new DataTable();
        //private static Guid TransactionID;
        //private static string BatchNumber = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //TransactionID = Guid.Empty;
                SetCurrencyAndTransactionUtilities();
                BindBank();
                BindBranch();
                sortOrder = "asc";
                BindChargeCategory();
                Session.Remove("TransactionID");
            }
        }
        protected void SetCurrencyAndTransactionUtilities()
        {
            txtCurrency.Text = string.Empty;

            if (Session["Currency"] != null)
            {
                txtCurrency.Text = Session["Currency"].ToString();
            }
            if (Session["EFTTransactionType"] != null)
            {
                txtTransactionType.Text = Session["EFTTransactionType"].ToString();
            }
            if (Session["EntryDesc"] != null)
            {
                txtPaymentReason.Text = Session["EntryDesc"].ToString();
            }
        }
        protected void btnSaveAndSameBatch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertTransaction("SaveInSame");
            }
        }

        protected void btnSaveAndNewBatch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertTransaction("SaveAndNew");
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
            string ServiceClassCode = "220";
            string SECC = "CIE";
            int EnvelopID = -1;
            int TypeOfPayment = PaymentType.IndividualToIndividual;
            DateTime EffectiveEntryDate = System.DateTime.Now;
            //DateTime EffectiveEntryDate = System.DateTime.Today.AddDays(1);
            //int SettlementJDate = 0;
            string CompanyId = ConfigurationManager.AppSettings["CompanyId"];
            string CompanyName = ConfigurationManager.AppSettings["CompanyName"];
            string EntryDesc = string.Empty;
            string currency = string.Empty;
            currency = txtCurrency.Text;

            if (Session["EntryDesc"] != null)
            {
                EntryDesc = Session["EntryDesc"].ToString();
            }

            int CreatedBy = ParseData.StringToInt(Request.Cookies["UserID"].Value);
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

            if (!EFTN.BLL.RoutingNumberValidator.CheckDigitOk(txtRoutingNo.Text))
            {
                lblMsg.Text = "Invalid Routing Number. Please insert valid routing number.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            /*  REMITTANCE PART */
            int isRemittance = 0;
            if (cbxRemittance.Checked)
            {
                isRemittance = 1;
            }           


            int ReceiverAccountType = ParseData.StringToInt(rdoBtnReceiverAccountType.SelectedValue);
            string DFIAccountNo = txtDFIAccountNumber.Text.Trim();
            string AccountNo = txtAccountNumber.Text.Trim();

            FloraSoft.BanksDB banksDB = new FloraSoft.BanksDB();

            string ReceivingBankRoutingNo = txtRoutingNo.Text;

            int TypeOfAccount = ParseData.StringToInt(rdoBtnReceiverAccountType.SelectedValue);
            decimal Amount = ParseData.StringToDecimal(txtAmount.Text.Trim());
            string IdNumber = txtReceiverID.Text.Trim(); //// change///////////////////////
            string ReceiverName = txtReceiverName.Text.Trim();
            int StatusID = TransactionStatus.TransSent_Data_Entered;
            string PaymentInfo = txtReasonForPayment.Text.Trim();

            string strTransactionID;
            Guid TransactionID;

            if (Session["TransactionID"] != null)
            {
                strTransactionID = Session["TransactionID"].ToString();
                TransactionID = new Guid(strTransactionID);
            }
            else
            {
                TransactionID = new Guid();
            }

            SentBatchDB sentBatchDB = new SentBatchDB();

            string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

            try
            {
                if (TransactionID.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    TransactionID = sentBatchDB.InsertBatchSent(EnvelopID, ServiceClassCode
                                                            , SECC, TypeOfPayment, EffectiveEntryDate
                                                            , CompanyId, CompanyName, EntryDesc
                                                            , CreatedBy, 0,
                                                            TransactionCodes.EFTTransactionTypeCredit
                                                            ,ParseData.StringToInt(Request.Cookies["DepartmentID"].Value)
                                                            , DataEntryType.Manual, currency);
                    Session["TransactionID"] = TransactionID;
                }

                SentEDRDB sentEDRDB = new SentEDRDB();
                Guid EDRID = sentEDRDB.InsertTransactionSent(TransactionID, TransactionCode
                                        , ReceiverAccountType, TypeOfPayment, DFIAccountNo
                                        , AccountNo, ReceivingBankRoutingNo, Amount, IdNumber
                                        , ReceiverName, StatusID, CreatedBy
                                        , PaymentInfo
                                        , ParseData.StringToInt(Request.Cookies["DepartmentID"].Value)
                                        , EFTConnectionString
                                        ,isRemittance);         // Added For Remittance

                //Insert Charge Definition for City Bank -- START
                //string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                //if (SelectedBank.Equals("225"))
                //{
                //    EFTChargeManager eftChargeManager = new EFTChargeManager();
                //    if (ddListChargeCategoryList.SelectedValue.Equals("2"))
                //    {
                //        eftChargeManager.InsertCityChargeCodeByEDRID(EDRID, ParseData.StringToInt(ddListChargeCode.SelectedValue));
                //    }
                //    eftChargeManager.InsertChargeDefinitionForCityBankByEDRID(EDRID, ParseData.StringToInt(ddListChargeCategoryList.SelectedValue));
                //}
                //Insert Charge Definition for City Bank -- END

                lblMsg.Text = "Saved successfully";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                if (transactionType.Equals("SaveAndExit"))
                {
                    TransactionID = Guid.Empty;
                    Response.Redirect("Default.aspx");
                }
                else if (transactionType.Equals("SaveAndNew"))
                {
                    TransactionID = Guid.Empty;
                    Response.Redirect("ConsumerType.aspx");
                }
            }
            catch
            {
                lblMsg.Text = "Failed to Save";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            BindData();
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
            string CCY = accEnqDB.GetCCY_Code (txtCurrency.Text);


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

        //private string CCYCode( string CCY)
        //{
        //    string CCYCode = "00";
        //    switch (CCY)
        //    {
        //        case "BDT":
        //            CCYCode = "00";
        //            break;
        //        case "USD":
        //            CCYCode = "01";
        //            break;
        //        case "CAD":
        //            CCYCode = "02";
        //            break;
        //        case "JPY":
        //            CCYCode = "15";
        //            break;
        //        case "EUR":
        //            CCYCode = "16";
        //            break;
        //        case "GBP":
        //            CCYCode = "80";
        //            break;
        //    }
        //    return CCYCode;
        //}

        private void BindBatchTotal()
        {
            //lblMsgBatchNumber.Text = "Batch Number : " + BatchNumber;
            if (myDTCorUpload.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + myDTCorUpload.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + myDTCorUpload.Compute("SUM(Amount)", "").ToString();
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
        }

        protected void dtgXcelUpload_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = myDTCorUpload.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgXcelUpload.DataSource = dv;
            dtgXcelUpload.DataBind();
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

        protected void dtgXcelUpload_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgXcelUpload.CurrentPageIndex = e.NewPageIndex;
            dtgXcelUpload.DataSource = myDTCorUpload;
            dtgXcelUpload.DataBind();
        }

        protected void dtgXcelUpload_ItemCommand(object source, DataGridCommandEventArgs e)
        {

            EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();

            if (e.CommandName == "Cancel")
            {
                dtgXcelUpload.EditItemIndex = -1;
                BindData();
            }
            if (e.CommandName == "Edit")
            {
                dtgXcelUpload.EditItemIndex = e.Item.ItemIndex;
                BindData();
            }

            if (e.CommandName == "Update")
            {
                Guid EDRID = (Guid)dtgXcelUpload.DataKeys[e.Item.ItemIndex];

                TextBox txtDFIAccountNo = (TextBox)e.Item.FindControl("txtDFIAccountNo");
                TextBox txtAccountNo = (TextBox)e.Item.FindControl("txtAccountNo");
                TextBox txtReceivingBankRoutingNo = (TextBox)e.Item.FindControl("txtReceivingBankRoutingNo");
                TextBox txtPaymentInfo = (TextBox)e.Item.FindControl("txtPaymentInfo");
                TextBox txtIdNumber = (TextBox)e.Item.FindControl("txtIdNumber");
                TextBox txtReceiverName = (TextBox)e.Item.FindControl("txtReceiverName");
                TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");

                string txtInvoiceNumber = string.Empty;
                string txtInvoiceDate = string.Empty;
                string txtInvoiceGrossAmount = string.Empty;
                string txtInvoiceAmountPaid = string.Empty;
                string txtPurchaseOrder = string.Empty;
                string txtAdjustmentAmount = string.Empty;
                string txtAdjustmentCode = string.Empty;
                string txtAdjustmentDescription = string.Empty;

                sentEDRDB.UpdateSentEDRByEDRSentIDForBulkUpload(EDRID, txtDFIAccountNo.Text.Trim(), txtAccountNo.Text.Trim()
                                                        , ParseData.StringToDecimal(txtAmount.Text.Trim())
                                                        , txtIdNumber.Text.Trim(), txtReceiverName.Text.Trim()
                                                        , txtPaymentInfo.Text.Trim()
                                                        , txtReceivingBankRoutingNo.Text.Trim()
                                                        , txtInvoiceNumber, txtInvoiceDate
                                                        , ParseData.StringToDecimal(txtInvoiceGrossAmount)
                                                        , ParseData.StringToDecimal(txtInvoiceAmountPaid)
                                                        , txtPurchaseOrder
                                                        , ParseData.StringToDecimal(txtAdjustmentAmount)
                                                        , txtAdjustmentCode
                                                        , txtAdjustmentDescription);

                dtgXcelUpload.EditItemIndex = -1;

                BindData();
            }
            if (e.CommandName == "Delete")
            {
                Guid EDRID = (Guid)dtgXcelUpload.DataKeys[e.Item.ItemIndex];
                sentEDRDB.DeleteTransactionSent(EDRID);
                BindData();
            }
        }

        private void BindData()
        {
            string strTransactionID;
            Guid TransactionID;

            if (Session["TransactionID"] != null)
            {
                strTransactionID = Session["TransactionID"].ToString();
                TransactionID = new Guid(strTransactionID);
            }
            else
            {
                TransactionID = new Guid();
            }

            EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
            myDTCorUpload = sentEDRDB.GetSentEDRByTransactionIDForManualEntry(TransactionID);
            dtgXcelUpload.DataSource = myDTCorUpload;
            dtgXcelUpload.DataBind();
            BindBatchTotal();
            BindBatchNumberByTransactionID(TransactionID);
            ClearControlValue();
        }

        private void ClearControlValue()
        {
            txtAccountNumber.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtDFIAccountNumber.Text = string.Empty;
            txtReasonForPayment.Text = string.Empty;
            txtReceiverName.Text = string.Empty;
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
