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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EFTN.BLL;
using EFTN.component;

using System.Data.SqlClient;
using System.Collections.Generic;

namespace EFTN
{
    public partial class InwardTransactionMaker : System.Web.UI.Page
    {
        private static DataTable dtInwardTransaction = new DataTable();
        DataView dv;
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private static string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("InwardTransactions");
                BindBankList();
                BindCurrencyTypeDropdownlist();
                BindSessionDropdownlist();
                BindData();
                ddlReturnChangeCode.Enabled = false;
                txtCorrectedData.Text = "";
                txtCorrectedData.Visible = false;
                lblCorrectedDataMsg.Visible = false;
                rblTransactionDecision.SelectedValue = "1";
                sortOrder = "asc";
                BindAccountNumberValidationButton();
                BindTransactionDecision();
                AddCheckBoxes();
            }
        }
        protected void BindCurrencyTypeDropdownlist()
        {
            DataTable dropDownData = new DataTable();
            dropDownData.Columns.Add("Currency");
            DataRow row = dropDownData.NewRow();
            row["Currency"] = "ALL";
            dropDownData.Rows.Add(row);
            dropDownData.Merge(sentBatchDB.GetCurrencyList(eftConString));
            CurrencyDdList.DataSource = dropDownData;
            CurrencyDdList.DataTextField = "Currency";
            CurrencyDdList.DataValueField = "Currency";
            CurrencyDdList.DataBind();
        }
        private void BindSessionDropdownlist()
        {
            DataTable dropDownData = new DataTable();
            dropDownData.Columns.Add("SessionID");
            DataRow row = dropDownData.NewRow();
            row["SessionID"] = "ALL";
            dropDownData.Rows.Add(row);
            dropDownData.Merge(sentBatchDB.GetSessions(eftConString));
            SessionDdList.DataSource = dropDownData;
            SessionDdList.DataTextField = "SessionID";
            SessionDdList.DataValueField = "SessionID";
            SessionDdList.DataBind();
        }
        private void BindTransactionDecision()
        {
            System.Web.UI.WebControls.ListItem item;
            item = new System.Web.UI.WebControls.ListItem("Accept", "1");
            rblTransactionDecision.Items.Add(item);
            item = new System.Web.UI.WebControls.ListItem("Return", "2");
            rblTransactionDecision.Items.Add(item);
            item = new System.Web.UI.WebControls.ListItem("Notify Change", "3");
            rblTransactionDecision.Items.Add(item);
            string selectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            if (selectedBank.Equals("215"))
            {
                item = new System.Web.UI.WebControls.ListItem("Hold", "4");
                rblTransactionDecision.Items.Add(item);
            }
        }

        private void BindAccountNumberValidationButton()
        {
            string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            if (originBank.Equals(OriginalBankCode.SCB))
            {
                RemoveInvalidCharacterFromAccNo.Visible = true;
            }
            else
            {
                RemoveInvalidCharacterFromAccNo.Visible = false;
            }

            if (originBank.Equals(OriginalBankCode.UCBL) || originBank.Equals(OriginalBankCode.NRB))
            {
                btnSynchronizeCBSAccountInfo.Visible = true;
            }
            else
            {
                btnSynchronizeCBSAccountInfo.Visible = false;
            }
        }
        private void AddCheckBoxes()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(eftConString))
                {
                    // ArrayList SelectedItems;                   
                    SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT ISNULL(ID,'')ID, ISNULL(RISK,'')RISK FROM EFT_Risk ORDER BY RISK", con);
                    con.Open();
                    da.SelectCommand.ExecuteReader();
                    con.Close();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    cbxList.DataSource = dt;
                    cbxList.DataTextField = "RISK";
                    cbxList.DataValueField = "ID";
                    cbxList.DataBind();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        private void BindBankList()
        {
            FloraSoft.BanksDB dbBank = new FloraSoft.BanksDB();
            BankList.DataSource = dbBank.GetAllBanks();
            BankList.DataBind();
            BankList.Items.Add(new System.Web.UI.WebControls.ListItem("All", "0"));
            BankList.SelectedIndex = BankList.Items.Count - 1;
        }
        //private void BindDateList()
        //{
        //    string CurDate = System.DateTime.Today.ToShortDateString();
        //    DateList.Items.Add(new ListItem(CurDate));
        //}

        private void BindData()
        {

            EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();
            string originBankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            dtInwardTransaction = new DataTable();
            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                if (originBankCode.Equals(OriginalBankCode.SCB))
                {
                    int DepartmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                    string strRisk = "ALL";
                    string strStatus = "ALL";

                    if (!txtSearchRiskParam.Text.Trim().Equals(string.Empty))
                    {
                        strRisk = txtSearchRiskParam.Text.Trim();
                    }

                    if (!txtSearchStatusParam.Text.Trim().Equals(string.Empty))
                    {
                        strStatus = txtSearchStatusParam.Text.Trim();
                    }

                    dtInwardTransaction = db.GetReceivedEDRForAdminForDebitForSCB(BankList.SelectedValue, DepartmentID, strRisk, strStatus);
                }
                else
                {
                    if (ConfigurationManager.AppSettings["BranchWise"].Equals("0"))
                    {
                        dtInwardTransaction = db.GetReceivedEDRForAdmin_ForDebit(BankList.SelectedValue).Copy();
                    }
                    else
                    {
                        dtInwardTransaction = db.GetReceivedEDRForAdmin_ForDebit(
                            EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value)
                            , BankList.SelectedValue
                            ).Copy();
                    }
                }
            }
            else if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeCredit))
            {
                //In UCBL for credit transaction when DepartmentID is only 0 then able to see the transaction.
                //or If CentralInward = 1 and DepartmentID = 0 then able to see the transaction.

                if (originBankCode.Equals(OriginalBankCode.SCB))
                {
                    int DepartmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                    string strRisk = "ALL";
                    string strStatus = "ALL";

                    if (!txtSearchRiskParam.Text.Trim().Equals(string.Empty))
                    {
                        strRisk = txtSearchRiskParam.Text.Trim();
                    }

                    if (!txtSearchStatusParam.Text.Trim().Equals(string.Empty))
                    {
                        strStatus = txtSearchStatusParam.Text.Trim();
                    }
                    dtInwardTransaction = db.GetReceivedEDRForAdminForSCB(BankList.SelectedValue, DepartmentID, strRisk, strStatus);
                }
                else if (ConfigurationManager.AppSettings["CentralInward"].Equals("1"))
                {
                    if (ParseData.StringToInt(Request.Cookies["DepartmentID"].Value) == 0)
                    {
                        dtInwardTransaction = db.GetReceivedEDRForAdmin(BankList.SelectedValue).Copy();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (ConfigurationManager.AppSettings["BranchWise"].Equals("0"))
                    {
                        dtInwardTransaction = db.GetReceivedEDRForAdmin(BankList.SelectedValue).Copy();
                    }
                    else
                    {
                        dtInwardTransaction = db.GetReceivedEDRForAdmin(
                            EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value)
                            , BankList.SelectedValue
                            ).Copy();
                    }
                }
            }
            else
            {
                //In UCBL for credit transaction when DepartmentID is only 0 then able to see the transaction.
                //or If CentralInward = 1 and DepartmentID = 0 then able to see the transaction.
                if (ConfigurationManager.AppSettings["CentralInward"].Equals("1"))
                {
                    if (ParseData.StringToInt(Request.Cookies["DepartmentID"].Value) == 0)
                    {
                        dtInwardTransaction = db.GetReceivedEDRForAdmin(BankList.SelectedValue).Copy();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (ConfigurationManager.AppSettings["BranchWise"].Equals("0"))
                    {
                        dtInwardTransaction = db.GetReceivedEDRForAdmin_ForALL(BankList.SelectedValue).Copy();
                    }
                    else
                    {
                        dtInwardTransaction = db.EFT_GetReceivedEDR_ForAdmin_byBranches_ForAll(
                            EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value)
                            , BankList.SelectedValue
                            ).Copy();
                    }
                }
            }
            //dtInwardTransaction = db.GetReceivedEDRForAdmin().Copy();          
            Session.Remove("InwardTransactions");
            Session["InwardTransactions"] = dtInwardTransaction;
            if (dtInwardTransaction.Rows.Count > 0)
            {
                dtInwardTransaction = GetCurrencyWiseData(dtInwardTransaction, CurrencyDdList.SelectedValue);
            }
            //CurrentPage = 0; // or u can make this.ViewState["CurrentPage"] = null;
            dv = dtInwardTransaction.DefaultView;
            dtgInwardTransactionMaker.DataSource = dv;
            dtgInwardTransactionMaker.DataBind();
           // dtgInwardTransactionMaker.CurrentPageIndex = 1;

            if (dtInwardTransaction.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + dtInwardTransaction.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(dtInwardTransaction.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }

            cbxAll.Checked = false;
            //EnableFor_CorrectedData();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchParam = txtSearchParam.Text.Trim();
            if (searchParam.Equals(string.Empty))
            {
                BindData();
            }
            else
            {
                BindSearchData(searchParam);
            }
        }

        private void BindSearchData(string searchParam)
        {

            EFTN.component.SearchReceivedEDRDB db = new EFTN.component.SearchReceivedEDRDB();
            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                if (ConfigurationManager.AppSettings["BranchWise"].Equals("0"))
                {
                    dtInwardTransaction = db.GetReceivedEDRForAdmin_ForDebit(BankList.SelectedValue, searchParam).Copy();
                }
                else
                {
                    if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("185"))//In Rupali if BranchID = 0 then able to see all the transaction
                    {
                        dtInwardTransaction = db.GetReceivedEDRForAdmin_ForDebit(BankList.SelectedValue, searchParam).Copy();
                    }
                    else
                    {
                        dtInwardTransaction = db.GetReceivedEDRForAdmin_ForDebit(
                            EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value)
                            , BankList.SelectedValue, searchParam
                            ).Copy();
                    }
                }
            }
            else if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeCredit))
            {
                //In UCBL for credit transaction when DepartmentID is only 0 then able to see the transaction.
                //or If CentralInward = 1 and DepartmentID = 0 then able to see the transaction.
                if (ConfigurationManager.AppSettings["CentralInward"].Equals("1"))
                {
                    if (ParseData.StringToInt(Request.Cookies["DepartmentID"].Value) == 0)
                    {
                        dtInwardTransaction = db.GetReceivedEDRForAdmin(BankList.SelectedValue, searchParam).Copy();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (ConfigurationManager.AppSettings["BranchWise"].Equals("0"))
                    {
                        dtInwardTransaction = db.GetReceivedEDRForAdmin(BankList.SelectedValue, searchParam).Copy();
                    }
                    else
                    {
                        if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("185"))//In Rupali if BranchID = 0 then able to see all the transaction
                        {
                            dtInwardTransaction = db.GetReceivedEDRForAdmin(BankList.SelectedValue, searchParam).Copy();
                        }
                        else
                        {
                            dtInwardTransaction = db.GetReceivedEDRForAdmin(
                                EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value)
                                , BankList.SelectedValue, searchParam
                                ).Copy();
                        }
                    }
                }
            }
            else
            {
                //In UCBL for credit transaction when DepartmentID is only 0 then able to see the transaction.
                //or If CentralInward = 1 and DepartmentID = 0 then able to see the transaction.
                if (ConfigurationManager.AppSettings["CentralInward"].Equals("1"))
                {
                    if (ParseData.StringToInt(Request.Cookies["DepartmentID"].Value) == 0)
                    {
                        dtInwardTransaction = db.GetReceivedEDRForAdmin(BankList.SelectedValue, searchParam).Copy();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (ConfigurationManager.AppSettings["BranchWise"].Equals("0"))
                    {
                        dtInwardTransaction = db.GetReceivedEDRForAdmin_ForALL(BankList.SelectedValue, searchParam).Copy();
                    }
                    else
                    {
                        if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("185"))//In Rupali if BranchID = 0 then able to see all the transaction
                        {
                            dtInwardTransaction = db.GetReceivedEDRForAdmin_ForALL(BankList.SelectedValue, searchParam).Copy();
                        }
                        else
                        {
                            dtInwardTransaction = db.EFT_GetReceivedEDR_ForAdmin_byBranches_ForAll(
                                EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value)
                                , BankList.SelectedValue, searchParam
                                ).Copy();
                        }
                    }
                }
            }

            //dtInwardTransaction = db.GetReceivedEDRForAdmin().Copy();          
            Session.Remove("InwardTransactions");
            Session["InwardTransactions"] = dtInwardTransaction;
            if (dtInwardTransaction.Rows.Count > 0)
            {
                dtInwardTransaction = GetCurrencyWiseData(dtInwardTransaction, CurrencyDdList.SelectedValue);
            }
            dv = dtInwardTransaction.DefaultView;
            dtgInwardTransactionMaker.DataSource = dv;
            dtgInwardTransactionMaker.DataBind();
           // dtgInwardTransactionMaker.CurrentPageIndex = 1;

            if (dtInwardTransaction.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + dtInwardTransaction.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(dtInwardTransaction.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }

            cbxAll.Checked = false;
            //EnableFor_CorrectedData();
        }

        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void BankList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void dtgInwardTransactionMaker_SortCommand(object source, DataGridSortCommandEventArgs e)
        {



            //EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();

            //if (ConfigurationManager.AppSettings["BranchWise"].Equals("0"))
            //{
            //    dtInwardTransaction = db.GetReceivedEDRForAdmin().Copy();
            //}
            //else
            //{
            //    dtInwardTransaction = db.GetReceivedEDRForAdmin(EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value)).Copy();
            //}

            //dtInwardTransaction = db.GetReceivedEDRForAdmin().Copy();
            dv = dtInwardTransaction.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgInwardTransactionMaker.DataSource = dv;
            dtgInwardTransactionMaker.DataBind();


            EnableFor_CorrectedData();
        }

        public string sortOrder
        {
            get
            {
                dtgInwardTransactionMaker.DataSource = dtInwardTransaction;
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
                dtgInwardTransactionMaker.DataBind();
            }
        }

        private void EnableFor_CorrectedData()
        {
            lblCorrectedDataMsg.Text = "Corrected Data";
            lblCorrectedDataMsg.Visible = true;
            txtCorrectedData.Visible = true;
            txtCorrectedData.MaxLength = 30;

            DisableRoutingNumber();
            DisableTransactionCode();
        }

        private void EnableFor_BAcc_RtNo()
        {
            EnableBankAccount();
            EnableRoutingNumber();
            DisableTransactionCode();
        }

        private void EnableFor_BAcc_TrCode()
        {
            EnableBankAccount();
            DisableRoutingNumber();
            EnableTransactionCode();
        }

        private void EnableFor_RtNo_BAcc_TrCode()
        {
            EnableBankAccount();
            EnableTransactionCode();
            EnableRoutingNumber();
        }

        private void EnableBankAccount()
        {
            lblCorrectedDataMsg.Text = "Bank Account";
            lblCorrectedDataMsg.Visible = true;
            txtCorrectedData.Visible = true;
            txtCorrectedData.MaxLength = 13;
        }

        private void EnableRoutingNumber()
        {
            txtRoutingNumber.Visible = true;
            lblRoutingNumber.Visible = true;
        }

        private void EnableTransactionCode()
        {
            txtTransactionCode.Visible = true;
            lblTransactionCode.Visible = true;
        }

        private void DisableBankAccount()
        {
            lblCorrectedDataMsg.Visible = false;
            txtCorrectedData.Visible = false;
        }

        private void DisableRoutingNumber()
        {
            txtRoutingNumber.Visible = false;
            lblRoutingNumber.Visible = false;
        }

        private void DisableTransactionCode()
        {
            txtTransactionCode.Visible = false;
            lblTransactionCode.Visible = false;
        }

        protected void ddlReturnChangeCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReturnChangeCode.SelectedValue.Equals("C03"))
            {
                EnableFor_BAcc_RtNo();
            }
            else if (ddlReturnChangeCode.SelectedValue.Equals("C06"))
            {
                EnableFor_BAcc_TrCode();
            }
            else if (ddlReturnChangeCode.SelectedValue.Equals("C07"))
            {
                EnableFor_RtNo_BAcc_TrCode();
            }
            else
            {
                EnableFor_CorrectedData();
            }
        }

        protected void rblTransactionDecision_SelectedIndexChanged(object sender, EventArgs e)
        {
            int type = Int32.Parse(rblTransactionDecision.SelectedValue);
            if (type == 1 || type == 4)
            {
                ddlReturnChangeCode.Enabled = false;
                DisableBankAccount();
                DisableRoutingNumber();
                DisableTransactionCode();

            }
            else
            {
                ddlReturnChangeCode.Enabled = true;
                EFTN.component.CodeLookUpDB codeDB = new EFTN.component.CodeLookUpDB();
                ddlReturnChangeCode.DataSource = codeDB.GetCodeLookUp(type);
                ddlReturnChangeCode.DataBind();
                if (type == 3)
                {
                    EnableFor_CorrectedData();
                    ddlReturnChangeCode.AutoPostBack = true;
                }
                else
                {
                    ddlReturnChangeCode.Items.Add(new System.Web.UI.WebControls.ListItem("Select", "0"));
                    ddlReturnChangeCode.SelectedIndex = ddlReturnChangeCode.Items.Count - 1;

                    DisableBankAccount();
                    ddlReturnChangeCode.AutoPostBack = false;
                    DisableRoutingNumber();
                    DisableTransactionCode();
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int type = ParseData.StringToInt(rblTransactionDecision.SelectedValue);
            if (type == 0)
            {
                lblMsg.Text = "** Please select the transaction decision";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            switch (type)
            {
                case 1:
                    CleanAllTextBox();
                    ChangeStatusSelected(EFTN.Utility.TransactionStatus.TransReceived_Approved);
                    break;
                case 2:
                    CleanAllTextBox();
                    if (ddlReturnChangeCode.SelectedValue.Equals("0"))
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = "Please Select Return Code";
                        return;
                    }
                    ChangeStatusSelected(EFTN.Utility.TransactionStatus.TransReceived_Reject_RR);
                    break;
                case 3:
                    if (ddlReturnChangeCode.SelectedValue.Equals("C03"))
                    {
                        if (txtCorrectedData.Text.Trim().Equals(string.Empty)
                            || txtRoutingNumber.Text.Trim().Equals(string.Empty))
                        {
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Text = "Please Enter Required Data";
                            return;
                        }
                    }
                    else if (ddlReturnChangeCode.SelectedValue.Equals("C06"))
                    {
                        if (txtCorrectedData.Text.Trim().Equals(string.Empty)
                            || txtTransactionCode.Text.Trim().Equals(string.Empty))
                        {
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Text = "Please Enter Required Data";
                            return;
                        }
                    }
                    else if (ddlReturnChangeCode.SelectedValue.Equals("C07"))
                    {
                        if (txtCorrectedData.Text.Trim().Equals(string.Empty)
                            || txtTransactionCode.Text.Trim().Equals(string.Empty)
                            || txtRoutingNumber.Text.Trim().Equals(string.Empty))
                        {
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Text = "Please Enter Required Data";
                            return;
                        }
                    }
                    else
                    {
                        if (txtCorrectedData.Text.Trim().Equals(string.Empty))
                        {
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Text = "Please Enter Required Data";
                            return;
                        }
                    }
                    ChangeStatusSelected(EFTN.Utility.TransactionStatus.TransReceived_Reject_NOC);
                    CleanAllTextBox();
                    break;
                case 4:
                    UpdateHoldStatus();
                    break;
            }
        }

        private void UpdateHoldStatus()
        {
            EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();
            int cbxCounter = 0;

            for (int i = 0; i < dtgInwardTransactionMaker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgInwardTransactionMaker.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    Guid edrId = (Guid)dtgInwardTransactionMaker.DataKeys[i];
                    int holdStatus = 1;

                    db.UpdateHoldStatus(edrId, holdStatus);
                    cbxCounter++;
                }
            }
            if (cbxCounter < 1)
            {
                lblMsg.Text = "Please select item";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
            }
            else
            {
                lblMsg.Visible = false;
            }
            BindData();
        }

        private void CleanAllTextBox()
        {
            txtCorrectedData.Text = string.Empty;
            txtRoutingNumber.Text = string.Empty;
            txtTransactionCode.Text = string.Empty;
        }

        private void ChangeStatusSelected(int statusID)
        {
            string connectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            string returnCode = (ddlReturnChangeCode.SelectedValue != null) ? ddlReturnChangeCode.SelectedValue : "";
            EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();
            EFTN.component.SentBatchDB sentBatchDB = new component.SentBatchDB();
            int cbxCounter = 0;
            int CreatedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            string correctedData = txtCorrectedData.Text.Trim()
                                    + " " + txtRoutingNumber.Text.Trim()
                                    + " " + txtTransactionCode.Text.Trim();

            correctedData = correctedData.Trim();
            int type = Int32.Parse(rblTransactionDecision.SelectedValue);

            if (type == 2)
            {
                if (returnCode.Equals("R15") || returnCode.Equals("R14"))
                {
                    correctedData = txtDateOfDeath.Text.Trim();
                }
                else
                {
                    correctedData = string.Empty;
                }
            }

            int successfullyChanged = 0;
            for (int i = 0; i < dtgInwardTransactionMaker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgInwardTransactionMaker.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    Guid edrId = (Guid)dtgInwardTransactionMaker.DataKeys[i];
                    int changedStatus = 0;
                    dtInwardTransaction = (DataTable)Session["InwardTransactions"];
                    DataRow[] row = dtInwardTransaction.Select("EDRID ='" + edrId + "'");
                    string currency = row[0].ItemArray[dtInwardTransaction.Columns.IndexOf("Currency")].ToString().Trim();

                    DataTable currencyList = sentBatchDB.GetCurrencyList(connectionString);
                    if (CheckCurrencyValidity(currencyList, currency))
                    {
                        changedStatus = db.UpdateEDRReceivedStatus(edrId, statusID, returnCode, CreatedBy, correctedData);
                        successfullyChanged += changedStatus;
                        cbxCounter++;
                    }
                    else
                    {
                        cbxCounter = 979797;
                    }
                }
            }
            if (cbxCounter < 1)
            {
                lblMsg.Text = "Please select item";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
            }
            else if (cbxCounter.Equals(979797))
            {
                lblMsg.Text = "Transaction/s can not approved! Invalid currency found!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
            }
            else
            {
                //if (cbxCounter > successfullyChanged)
                //{
                //    lblMsg.Text = successfullyChanged.ToString() + " item(s) changed successfully out of " + cbxCounter + " items(s)";
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //    lblMsg.Visible = true;
                //}
                //else
                //{
                lblMsg.Visible = false;
                //}
            }
            BindData();
        }
        private bool CheckCurrencyValidity(DataTable currencyList, string currency)
        {
            bool valid = false;
            foreach (DataRow item in currencyList.Rows)
            {
                if (!valid)
                {
                    if (item.ItemArray[0].ToString().Equals(currency))
                    {
                        valid = true;
                    }
                }
            }
            return valid;
        }
        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgInwardTransactionMaker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgInwardTransactionMaker.Items[i].FindControl("cbxCheck");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void dtgInwardTransactionMaker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgInwardTransactionMaker.CurrentPageIndex = e.NewPageIndex;
            dtgInwardTransactionMaker.DataSource = dtInwardTransaction;
            dtgInwardTransactionMaker.DataBind();
        }

        protected void btnExpotToCSV_Click(object sender, EventArgs e)
        {
            //DataTable dt = GetData();

            if (dtInwardTransaction.Rows.Count > 0)
            {
                string xlsFileName = "Inward" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                string attachment = "attachment; filename=" + xlsFileName + ".csv";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.csv";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                // Create the CSV file to which grid data will be exported. 
                //StreamWriter sw = new StreamWriter();
                int iColCount = dtInwardTransaction.Columns.Count;

                // First we will write the headers. 

                for (int i = 1; i < iColCount; i++)
                {
                    sw.Write("\"");
                    sw.Write(dtInwardTransaction.Columns[i]);
                    if (i < iColCount - 1)
                    {
                        sw.Write("\",");
                        //sw.Write(";");
                    }
                }

                if (iColCount > 0)
                {
                    sw.Write("\"");
                }
                sw.Write(sw.NewLine);

                // Now write all the rows. 
                foreach (DataRow dr in dtInwardTransaction.Rows)
                {
                    for (int i = 1; i < iColCount; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            sw.Write("\"");
                            sw.Write(dr[i].ToString());
                        }
                        if (i < iColCount - 1)
                        {
                            sw.Write("\",");
                        }
                    }
                    if (iColCount > 0)
                    {
                        sw.Write("\"");
                    }
                    sw.Write(sw.NewLine);
                }

                Response.Write(sw.ToString());
                Response.End();
            }
        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            string FileName = "Inward Transaction" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            PrintPDF(FileName);
        }

        private void PrintPDF(string FileName)
        {
            //DataTable dt = GetData();
            //dtInwardTransaction;
            if (dtInwardTransaction.Rows.Count == 0)
            {
                return;
            }

            Response.ClearContent();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

            Document document = new Document(PageSize.A4.Rotate(), 10, 10, 8, 8);
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

            Font fnt = new Font(Font.HELVETICA, 8);
            Font fntblue = new Font(Font.HELVETICA, 8);
            fntblue.Color = new Color(0, 0, 255);
            Font fntbld = new Font(Font.HELVETICA, 8);
            fntbld.SetStyle(Font.BOLD);
            Font headerFont = new Font(Font.HELVETICA, 15);

            string spacer = "            -              ";

            string str = spacer;
            str = str + ConfigurationManager.AppSettings["OriginBankName"] + " All Rights Reserved" + spacer;
            str = str + "Confidential: internal use only" + spacer;
            str = str + "Powered By Flora Limited";

            HeaderFooter footer = new HeaderFooter(new Phrase(str, fnt), false);
            footer.Alignment = Element.ALIGN_CENTER;

            document.Footer = footer;
            document.Open();

            string LogoImage = Server.MapPath("images") + "\\SCBLogo.JPG";

            iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
            jpeg.Alignment = Element.ALIGN_RIGHT;


            PdfPCell logo = new PdfPCell();
            logo.BorderWidth = 0;
            logo.Colspan = 2;
            logo.AddElement(jpeg);

            ////////////////////////////////
            iTextSharp.text.pdf.PdfPTable headertable = new iTextSharp.text.pdf.PdfPTable(3);
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            headertable.DefaultCell.Padding = 0;
            headertable.WidthPercentage = 99;
            headertable.DefaultCell.Border = 0;
            float[] widthsAtHeader = { 40, 40, 20 };
            headertable.SetWidths(widthsAtHeader);
            headertable.AddCell(new Phrase("Inward Transaction Report: "));

            headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            //string SelectedBank = string.Empty;

            float[] headerwidths;
            int NumberOfPdfColumn = 0;

            headerwidths = new float[] { 13, 13, 4, 10, 4, 8, 6, 10, 8, 8, 8, 12, 8 };
            NumberOfPdfColumn = 13;


            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(NumberOfPdfColumn);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            //float[] headerwidths = { 10, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };
            //float[] headerwidths = { 10, 10, 4, 8, 4, 8, 8, 8, 8, 8, 8, 8 };//Only For JANATA BANK
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0;

            c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));

            //PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;

            datatable.AddCell(c0);
            datatable.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("SECC", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Trace No.", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Tran. Code", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ReceivingBank RoutNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Receiver /Payer Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("C.Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Ent.Desc", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Transaction Type", fnt));


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dtInwardTransaction.Rows.Count; i++)
            {
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dtInwardTransaction.Rows[i]["BankName"], fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)dtInwardTransaction.Rows[i]["BranchName"], fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 4;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)dtInwardTransaction.Rows[i]["SECC"], fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)dtInwardTransaction.Rows[i]["TraceNumber"], fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);

                PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dtInwardTransaction.Rows[i]["TransactionCode"], fnt));
                c5.BorderWidth = 0.5f;
                c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c5.Padding = 4;
                datatable.AddCell(c5);

                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dtInwardTransaction.Rows[i]["DFIAccountNo"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 4;
                datatable.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dtInwardTransaction.Rows[i]["ReceivingBankRoutNo"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dtInwardTransaction.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dtInwardTransaction.Rows[i]["IdNumber"], fnt));
                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_LEFT;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 4;
                datatable.AddCell(c9);

                PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)dtInwardTransaction.Rows[i]["ReceiverName"], fnt));
                c10.BorderWidth = 0.5f;
                c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c10.Padding = 4;
                datatable.AddCell(c10);

                PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dtInwardTransaction.Rows[i]["CompanyName"], fnt));
                c11.BorderWidth = 0.5f;
                c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c11.Padding = 4;
                datatable.AddCell(c11);

                PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)dtInwardTransaction.Rows[i]["EntryDesc"], fnt));
                c12.BorderWidth = 0.5f;
                c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c12.Padding = 4;
                datatable.AddCell(c12);

                string trType = dtInwardTransaction.Rows[i]["TransactionCode"].ToString();
                if (trType.Equals("22")
                    || trType.Equals("24")
                    || trType.Equals("32")
                    || trType.Equals("42")
                    || trType.Equals("52")
                    )
                {
                    trType = "Credit";
                }
                else
                {
                    trType = "Debit";
                }
                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase(trType, fnt));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_LEFT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable.AddCell(c13);

            }

            //-------------TOTAL IN FOOTER --------------------
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dtInwardTransaction.Compute("COUNT(TraceNumber)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dtInwardTransaction.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtInwardTransaction.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //-------------END TOTAL -------------------------



            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }

        protected void dtgInwardTransactionMaker_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                dtgInwardTransactionMaker.EditItemIndex = -1;
            }
            if (e.CommandName == "Edit")
            {
                dtgInwardTransactionMaker.EditItemIndex = e.Item.ItemIndex;
            }
            if (e.CommandName == "Update")
            {
                Guid EDRID = (Guid)dtgInwardTransactionMaker.DataKeys[e.Item.ItemIndex];
                TextBox txtDFIAccountNo = (TextBox)e.Item.FindControl("DFIAccountNo");
                int CreatedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

                string selectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                if (selectedBank.Equals(OriginalBankCode.SCB))
                {
                    if (!txtDFIAccountNo.Text.Trim().Substring(0, 2).Equals("09"))
                    {
                        return;
                    }
                }

                EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();

                db.UpdateInwardTransactionDFIAccountNumberByEFTUser(EDRID, txtDFIAccountNo.Text.Trim(), CreatedBy);
                dtgInwardTransactionMaker.EditItemIndex = -1;
            }
            BindData();

        }

        protected void RemoveInvalidCharacterFromAccNo_Click(object sender, EventArgs e)
        {
            EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();
            db.AutoRemoveNonNumericNumberForSCB();
            BindData();
        }

        protected void btnSynchronizeCBSAccountInfo_Click(object sender, EventArgs e)
        {
            int cbxCounter = 0;
            ReceivedEDRDB receivedEDRDB = new ReceivedEDRDB();
            FCUBSRTManager fcubsManager = new FCUBSRTManager();
            string connectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            string amountFieldName = ConfigurationManager.AppSettings["ACC_SERVICE_FIELDNAME_BALANCE"].ToString();

            for (int i = 0; i < dtgInwardTransactionMaker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgInwardTransactionMaker.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    Guid EDRID = (Guid)dtgInwardTransactionMaker.DataKeys[i];
                    string TranType = string.Empty;
                    double Amount = 0.0;
                    string AccountNo = receivedEDRDB.GetReceivedEDRAccountNo_By_EDRID(EDRID, ref TranType, connectionString, ref Amount);
                    fcubsManager.SynchronizeAccountStatusWithFCUBS(EDRID, AccountNo, TranType, amountFieldName, connectionString, Amount);
                    //changedStatus = db.UpdateEDRReceivedStatus(edrId, statusID, returnCode, CreatedBy, correctedData);
                    cbxCounter++;
                }
            }
            if (cbxCounter < 1)
            {
                lblMsg.Text = "Please select item";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
            }
            BindData();
        }

        protected void btnSearchStatusAndRisk_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void CurrencyDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable gridData = new DataTable();
            DataTable filteredData = new DataTable();
            gridData = (DataTable)Session["InwardTransactions"];
            if (gridData != null && gridData.Rows.Count > 0)
            {
                filteredData = GetCurrencyWiseData(gridData, CurrencyDdList.SelectedValue);
                BindGridWithFilteredData(filteredData);
            }
        }

        protected void SessionDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable gridData = new DataTable();
            DataTable filteredData = new DataTable();
            gridData = (DataTable)Session["InwardTransactions"];
            if (gridData != null && gridData.Rows.Count > 0)
            {              
                filteredData = GetSessionWiseData(gridData, SessionDdList.SelectedValue);
                BindGridWithFilteredData(filteredData);
            }
        }

        public DataTable GetCurrencyWiseData(DataTable gridData, string currency)
        {
            DataTable currencyWiseData = new DataTable();
            int currencyCounter = 0;
            int sessionCounter = 0;
            currencyCounter = currency.Equals("ALL") ? gridData.Rows.Count : GetCurrencyCount(gridData, currency);
            sessionCounter = SessionDdList.SelectedValue.Equals("ALL") ? gridData.Rows.Count : GetSessionCount(gridData, SessionDdList.SelectedValue);
            if (currency.Equals("ALL") && SessionDdList.SelectedValue.Equals("ALL"))
            {
                currencyWiseData = gridData;
            }
            else if (currency.Equals("ALL") && !SessionDdList.SelectedValue.Equals("ALL") && sessionCounter > 0)
            {
                currencyWiseData = gridData.AsEnumerable()
                       .Where(c => c.Field<Byte>("SessionID") == Byte.Parse(SessionDdList.SelectedValue)).CopyToDataTable();
            }                               
            else if (!currency.Equals("ALL") && currencyCounter > 0 && !SessionDdList.SelectedValue.Equals("ALL") && sessionCounter > 0)
            {
                var linqResult = gridData.AsEnumerable()
                       .Where(c => c.Field<string>("Currency") == currency && c.Field<Byte>("SessionID") == Byte.Parse(SessionDdList.SelectedValue));
                if (linqResult.AsDataView().ToTable().Rows.Count > 0)
                {
                    currencyWiseData = linqResult.CopyToDataTable();
                }
            }            
            else if (!currency.Equals("ALL") && currencyCounter > 0 && SessionDdList.SelectedValue.Equals("ALL"))
            {
                currencyWiseData = gridData.AsEnumerable()
                       .Where(c => c.Field<string>("Currency") == currency).CopyToDataTable();
            }
            return currencyWiseData;
        }

        public DataTable GetSessionWiseData(DataTable gridData, string session)
        {
            DataTable sessionWiseData = new DataTable();
            int currencyCounter = 0;
            int sessionCounter = 0;
            currencyCounter = CurrencyDdList.SelectedValue.Equals("ALL") ? gridData.Rows.Count : GetCurrencyCount(gridData, CurrencyDdList.SelectedValue);
            sessionCounter = session.Equals("ALL") ? gridData.Rows.Count : GetSessionCount(gridData, session);
            if (session.Equals("ALL") && CurrencyDdList.SelectedValue.Equals("ALL"))
            {
                sessionWiseData = gridData;
            }
            else if (session.Equals("ALL") && !CurrencyDdList.SelectedValue.Equals("ALL") && currencyCounter > 0)
            {
                sessionWiseData = gridData.AsEnumerable()
                       .Where(c => c.Field<string>("Currency") == CurrencyDdList.SelectedValue).CopyToDataTable();
            }          
            else if (!session.Equals("ALL") && sessionCounter > 0 && CurrencyDdList.SelectedValue.Equals("ALL"))
            {
                sessionWiseData = gridData.AsEnumerable()
                       .Where(c => c.Field<Byte>("SessionID") == Byte.Parse(session)).CopyToDataTable();
            }
            
            else if (!session.Equals("ALL") && sessionCounter > 0 && !CurrencyDdList.SelectedValue.Equals("ALL") && currencyCounter > 0 )
            {
                var linqResult = gridData.AsEnumerable()
                       .Where(c => c.Field<Byte>("SessionID") == Byte.Parse(session) && c.Field<string>("Currency") == CurrencyDdList.SelectedValue);
                if (linqResult.AsDataView().ToTable().Rows.Count > 0)
                {
                    sessionWiseData = linqResult.CopyToDataTable();
                }
            }             
            return sessionWiseData;
        }

        private void BindGridWithRiskData(DataTable riskData)
        {
            dv = riskData.DefaultView;
            dtgInwardTransactionMaker.DataSource = dv;
            dtgInwardTransactionMaker.DataBind();
            if (dtInwardTransaction.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + dtInwardTransaction.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(dtInwardTransaction.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
        }

        public DataTable GetRiskData(DataTable grvData, List<string> risks)
        {
            DataTable riskData = new DataTable();

            foreach (string item in risks)
            {
                var linqResult = grvData.AsEnumerable()
                      .Where(c => c.Field<string>("RISKS").Contains(item));
                if (linqResult.AsDataView().ToTable().Rows.Count > 0)
                {
                    riskData = linqResult.CopyToDataTable();
                }
            }
            return riskData;
        }

        private void BindGridWithFilteredData(DataTable filteredData)
        {
            dv = filteredData.DefaultView;
            dtgInwardTransactionMaker.DataSource = dv;
            dtgInwardTransactionMaker.DataBind();
           

            if (filteredData.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + filteredData.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(filteredData.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
            cbxAll.Checked = false;
        }

        protected void cbxList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable grvData = new DataTable();
            DataTable filteredRiskData = new DataTable();
            grvData = (DataTable)Session["InwardTransactions"];
            List<string> chkList = new List<string>();


            for (int i = 0; i < cbxList.Items.Count; i++)
            {
                if (cbxList.Items[i].Selected)
                {
                    string str = cbxList.Items[i].Text;


                    var itemValue = cbxList.Items[i].Value;
                    chkList.Add(str);
                }
            }

            if (grvData != null && grvData.Rows.Count > 0 && chkList.Count > 0)
            {
                filteredRiskData = GetRiskData(grvData, chkList);                
            }
            else
            {
                filteredRiskData = grvData;
            }
            BindGridWithRiskData(filteredRiskData);
        }

        private int GetCurrencyCount(DataTable gridData, string currency)
        {
            int curCount = 0;
            foreach (DataRow row in gridData.Rows)
            {
                if (row["Currency"].ToString().Equals(currency))
                {
                    curCount++;
                }
            }
            return curCount;
        }

        private int GetSessionCount(DataTable gridData, string session)
        {
            int sessionCount = 0;
            foreach (DataRow row in gridData.Rows)
            {
                if (row["SessionID"].ToString().Equals(session))
                {
                    sessionCount++;
                }
            }
            return sessionCount;
        }          
    }
}
