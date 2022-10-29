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
using EFTN.Utility;
using EFTN.component;
using FloraSoft;
using EFTN.BLL;

namespace EFTN
{
    public partial class ConsumerScreen3 : System.Web.UI.Page
    {
        DataView dv;
        private static DataTable myDTCorUpload = new DataTable();
        //private static Guid TransactionID;
        //private static string BatchNumber = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetCurrencyAndTransactionUtilities();
                //TransactionID = Guid.Empty;
                int paymentTypeID = ParseData.StringToInt(Session["PaymentTypeID"].ToString());
                //lblMsg.Text = paymentTypeID.ToString();
                if (paymentTypeID == PaymentType.CorporateToCorporateTradePayment)
                {
                    pnlCTX.Visible = true;
                }
                else
                {
                    pnlCTX.Visible = false;
                }
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
        private void BindBank()
        {
            FloraSoft.BanksDB db = new FloraSoft.BanksDB();

            ddListReceivingBank.DataSource = db.GetAllBanks();
            ddListReceivingBank.DataTextField = "BankName";
            ddListReceivingBank.DataValueField = "BankID";
            ddListReceivingBank.DataBind();
        }

        protected void btnSaveAndSameBatch_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
                InsertTransaction("SaveInSame");
        }

        //protected void btnSaveAndNew_Click(object sender, EventArgs e)
        //{
        //    if (Page.IsValid)
        //        InsertTransaction("SaveAndNew");
        //}

        //protected void btnSaveAndExit_Click(object sender, EventArgs e)
        //{
        //    if (Page.IsValid)
        //        InsertTransaction("SaveAndExit");
        //}

        private void InsertTransaction(string transactionType)
        {
            string ServiceClassCode = string.Empty;
            string SECC = string.Empty;
            int EnvelopID = -1;
            int TypeOfPayment = PaymentType.CorporateToIndividual;
            //DateTime EffectiveEntryDate = System.DateTime.Today.AddDays(1);
            DateTime EffectiveEntryDate = System.DateTime.Now;
            //int SettlementJDate = 0;

            string CompanyId = string.Empty;
            string CompanyName = string.Empty;
            int CreatedBy = ParseData.StringToInt(Request.Cookies["UserID"].Value);
            //int ApprovedBy = 0;
            string TransactionCode = string.Empty;
            string EntryDesc = string.Empty;
            string IdNumber = string.Empty; //// change///////////////////////
            string ReceiverName = string.Empty;

            //////Corporate To Corporate Trade Payment Info///////
            string invoiceNumber = string.Empty;
            string invoiceDate = string.Empty;
            decimal invoiceGrossAmount = 0;
            decimal invoiceAmountPaid = 0;
            string purchaseOrder = string.Empty;
            decimal adjustmentAmount = 0;
            string adjustmentCode = string.Empty;
            string adjustmentDescription = string.Empty;
            string EFTTransactionType = string.Empty;
            string currency = string.Empty;
            currency = txtCurrency.Text;
            //////Corporate To Corporate Trade Payment Info///////
            if (Session["EFTTransactionType"] != null)
            {
                EFTTransactionType = Session["EFTTransactionType"].ToString();
                if (EFTTransactionType.Equals(TransactionCodes.EFTTransactionTypeCredit))
                {
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
                        lblMsg.Text = "Select Account Type";
                        return;
                    }
                }
                else if (EFTTransactionType.Equals(TransactionCodes.EFTTransactionTypeDebit))
                {
                    if (rdoBtnReceiverAccountType.SelectedValue.Equals("1"))
                    {
                        TransactionCode = TransactionCodes.DebitCurrentAcc.ToString();
                    }
                    else if (rdoBtnReceiverAccountType.SelectedValue.Equals("2"))
                    {
                        TransactionCode = TransactionCodes.DebitSavingsAcc.ToString();
                    }
                    else
                    {
                        lblMsg.Text = "Select Account Type";
                        return;
                    }
                }
            }
            else
            {
                lblMsg.Text = "Transaction Type is not selected";
                return;
            }

            int ReceiverAccountType = ParseData.StringToInt(rdoBtnReceiverAccountType.SelectedValue);

            if (!EFTN.BLL.RoutingNumberValidator.CheckDigitOk(txtRoutingNo.Text))
            {
                lblMsg.Text = "Invalid Routing Number. Please insert valid routing number.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (Session["PaymentTypeID"] != null)
            {
                string PaymentTypeID = Session["PaymentTypeID"].ToString();
                if (PaymentTypeID.Equals(PaymentType.CorporateToCorporateTradePayment.ToString()))
                {
                    ServiceClassCode = "200";
                    SECC = "CTX";
                    invoiceNumber = txtInvoiceNumber.Text;
                    invoiceDate = txtInvoiceDateYear.Text.PadLeft(4, '0')
                            + txtInvoiceDateMonth.Text.PadLeft(2, '0')
                            + txtInvoiceDateDay.Text.PadLeft(2, '0');
                    invoiceGrossAmount = ParseData.StringToDecimal(txtInvoiceGrossAmount.Text);
                    invoiceAmountPaid = ParseData.StringToDecimal(txtInvoiceAmountPaid.Text);
                    purchaseOrder = txtPurchaseOrder.Text;
                    adjustmentAmount = ParseData.StringToDecimal(txtAdjustmentAmount.Text);
                    adjustmentCode = txtAdjustmentCode.Text;
                    adjustmentDescription = txtAdjustmentDescription.Text;
                }
                else if (PaymentTypeID.Equals(PaymentType.BillToCorporate.ToString())
                             || PaymentTypeID.Equals(PaymentType.CashConcentration.ToString()))
                {
                    ServiceClassCode = "200";
                    SECC = "CCD";
                }
            }
            if (ServiceClassCode.Equals(string.Empty) || SECC.Equals(string.Empty))
            {
                ServiceClassCode = "200";
                SECC = "PPD";
            }

            if (Session["EntryDesc"] != null)
            {
                EntryDesc = Session["EntryDesc"].ToString();
            }
            if (Session["CompanyId"] != null)
            {
                CompanyId = Session["CompanyId"].ToString();
            }
            if (Session["CompanyName"] != null)
            {
                CompanyName = Session["CompanyName"].ToString();
            }

            IdNumber = txtReceiverID.Text.Trim();
            ReceiverName = txtReceiverName.Text.Trim();
            string DFIAccountNo = txtDFIAccountNumber.Text.Trim();
            string AccountNo = txtAccountNumber.Text.Trim();
            string ReceivingBankRoutingNo = txtRoutingNo.Text;
            int TypeOfAccount = ReceiverAccountType;
            decimal Amount = ParseData.StringToDecimal(txtAmount.Text.Trim());
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

            /*  REMITTANCE PART */
            int isRemittance = 0;
            if (cbxRemittance.Checked)
            {
                isRemittance = 1;
            }
           
            string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

            SentBatchDB sentBatchDB = new SentBatchDB();
            try
            {
                if (TransactionID.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    TransactionID = sentBatchDB.InsertBatchSent(EnvelopID, ServiceClassCode
                                                            , SECC, TypeOfPayment, EffectiveEntryDate
                                                            , CompanyId, CompanyName, EntryDesc
                                                            , CreatedBy, 0, EFTTransactionType
                                                            , ParseData.StringToInt(Request.Cookies["DepartmentID"].Value)
                                                            , DataEntryType.Manual
                                                            , currency);
                                                           
                    Session["TransactionID"] = TransactionID;
                }
                string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

                SentEDRDB sentEDRDB = new SentEDRDB();
                if (SelectedBank.Equals("215") && EFTTransactionType.Equals(TransactionCodes.EFTTransactionTypeDebit))
                {
                    int successfullTR = sentEDRDB.InsertTransactionSentforCTXForSCBDebit(TransactionID, TransactionCode
                                            , ReceiverAccountType, TypeOfPayment, DFIAccountNo
                                            , AccountNo, ReceivingBankRoutingNo, Amount, IdNumber
                                            , ReceiverName, StatusID, CreatedBy
                                            , PaymentInfo, invoiceNumber, invoiceDate, invoiceGrossAmount
                                            , invoiceAmountPaid, purchaseOrder, adjustmentAmount
                                            , adjustmentCode, adjustmentDescription
                                            , ParseData.StringToInt(Request.Cookies["DepartmentID"].Value)
                                            , EFTConnectionString
                                             , isRemittance);     // Added For Remittance Upgrade

                    if (successfullTR == 0)
                    {
                        lblMsg.Text = "Account Info not found in database";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
                else
                {
                    Guid EDRID = sentEDRDB.InsertTransactionSentforCTX(TransactionID, TransactionCode
                                            , ReceiverAccountType, TypeOfPayment, DFIAccountNo
                                            , AccountNo, ReceivingBankRoutingNo, Amount, IdNumber
                                            , ReceiverName, StatusID, CreatedBy
                                            , PaymentInfo, invoiceNumber, invoiceDate, invoiceGrossAmount
                                            , invoiceAmountPaid, purchaseOrder, adjustmentAmount
                                            , adjustmentCode, adjustmentDescription
                                            , ParseData.StringToInt(Request.Cookies["DepartmentID"].Value), EFTConnectionString,isRemittance);
                }
                //sentEDRDB.InsertTransactionSent(transa
                //Insert Charge Definition for City Bank -- START
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

                if (transactionType.Equals("SaveAndExit"))
                {
                    TransactionID = Guid.Empty;
                    Response.Redirect("Default.aspx", false);
                    Session.Remove("PaymentTypeID");
                }
                else if (transactionType.Equals("SaveAndNew"))
                {
                    TransactionID = Guid.Empty;
                    Response.Redirect("ConsumerType.aspx", false);
                }
            }
            catch
            {
                lblMsg.Text = "Failed to Save";
            }

            BindData();
        }

        protected void CheckeBBS_Click(object sender, EventArgs e)
        {
            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            //string CCY = CCYCode(txtCurrency.Text);
            AccountEnquiryDB accEnqDB = new AccountEnquiryDB();
            string CCY = accEnqDB.GetCCY_Code(txtCurrency.Text);

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

        //private string CCYCode(string CCY)
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

                    TextBox txtInvoiceNumber = (TextBox)e.Item.FindControl("txtInvoiceNumber");
                    TextBox txtInvoiceDate = (TextBox)e.Item.FindControl("txtInvoiceDate");
                    TextBox txtInvoiceGrossAmount = (TextBox)e.Item.FindControl("txtInvoiceGrossAmount");
                    TextBox txtInvoiceAmountPaid = (TextBox)e.Item.FindControl("txtInvoiceAmountPaid");
                    TextBox txtPurchaseOrder = (TextBox)e.Item.FindControl("txtPurchaseOrder");
                    TextBox txtAdjustmentAmount = (TextBox)e.Item.FindControl("txtAdjustmentAmount");
                    TextBox txtAdjustmentCode = (TextBox)e.Item.FindControl("txtAdjustmentCode");
                    TextBox txtAdjustmentDescription = (TextBox)e.Item.FindControl("txtAdjustmentDescription");

                    sentEDRDB.UpdateSentEDRByEDRSentIDForBulkUpload(EDRID, txtDFIAccountNo.Text.Trim(), txtAccountNo.Text.Trim()
                                                            , ParseData.StringToDecimal(txtAmount.Text.Trim())
                                                            , txtIdNumber.Text.Trim(), txtReceiverName.Text.Trim()
                                                            , txtPaymentInfo.Text.Trim()
                                                            , txtReceivingBankRoutingNo.Text.Trim()
                                                            , txtInvoiceNumber.Text.Trim(), txtInvoiceDate.Text.Trim()
                                                            , ParseData.StringToDecimal(txtInvoiceGrossAmount.Text.Trim())
                                                            , ParseData.StringToDecimal(txtInvoiceAmountPaid.Text.Trim())
                                                            , txtPurchaseOrder.Text.Trim()
                                                            , ParseData.StringToDecimal(txtAdjustmentAmount.Text.Trim())
                                                            , txtAdjustmentCode.Text.Trim()
                                                            , txtAdjustmentDescription.Text.Trim());

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
