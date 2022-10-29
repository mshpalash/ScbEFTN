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
using EFTN.BLL;
using FloraSoft;
using EFTN.component;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace EFTN
{
    public partial class InwardTransactionApprovedChecker : System.Web.UI.Page
    {
        private static DataTable dtInwardTransaction = new DataTable();
        DataView dv;
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("InwardTransactions");
                BindCurrencyTypeDropdownlist();
                BindSessionDropdownlist();
                BindBankList();
                BindData();
                lblNoReturnReason.Visible = false;
                sortOrder = "asc";
                AddCheckBoxes();
            }
        }
        private void BindCurrencyTypeDropdownlist()
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
        private void BindBankList()
        {
            FloraSoft.BanksDB dbBank = new FloraSoft.BanksDB();
            BankList.DataSource = dbBank.GetAllBanks();
            BankList.DataBind();
            BankList.Items.Add(new System.Web.UI.WebControls.ListItem("All", "0"));
            BankList.SelectedIndex = BankList.Items.Count - 1;
        }

        private void BindData()
        {

            EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();

            int BranchID = 0;
            int txnCounter = 0;
            if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);
            }

            if (BankList.SelectedValue.Equals("0"))
            {
                if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals(OriginalBankCode.SCB))
                    {
                        int DepartmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                        dtInwardTransaction = db.GetReceivedEDR_ApprovedByMaker_ForChecker_ForDebit_forSCB(BranchID, DepartmentID);
                    }
                    else
                    {
                        dtInwardTransaction = db.GetReceivedEDRApprovedByMaker_ForDebit(BranchID).Copy();                                                
                    }
                }
                else
                {
                    //In CentralInward=1 and DepartmentID is only 0 then able to see the transaction.
                    if (ConfigurationManager.AppSettings["CentralInward"].Equals("1"))
                    {
                        if (ParseData.StringToInt(Request.Cookies["DepartmentID"].Value) == 0)
                        {
                            BranchID = 0;
                            dtInwardTransaction = db.GetReceivedEDRApprovedByMaker(BranchID).Copy();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals(OriginalBankCode.SCB))
                    {
                        int DepartmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                        dtInwardTransaction = db.GetReceivedEDR_ApprovedByMaker_ForChecker_forSCB(BranchID, DepartmentID);
                    }
                    else
                    {
                        dtInwardTransaction = db.GetReceivedEDRApprovedByMaker(BranchID).Copy();
                    }
                }
            }
            else
            {
                if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals(OriginalBankCode.SCB))
                    {
                        int DepartmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                        dtInwardTransaction = db.GetReceivedEDR_ApprovedByMaker_ForChecker_ForDebit_Bankwise_forSCB(BranchID, BankList.SelectedValue, DepartmentID).Copy();
                    }
                    else
                    {
                        dtInwardTransaction = db.GetReceivedEDRApprovedByMaker_ForDebit(BranchID, BankList.SelectedValue).Copy();
                    }
                }
                else
                {
                    //In CentralInward=1 and DepartmentID is only 0 then able to see the transaction.
                    if (ConfigurationManager.AppSettings["CentralInward"].Equals("1"))
                    {
                        if (ParseData.StringToInt(Request.Cookies["DepartmentID"].Value) == 0)
                        {
                            BranchID = 0;
                            dtInwardTransaction = db.GetReceivedEDRApprovedByMaker(BranchID, BankList.SelectedValue).Copy();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals(OriginalBankCode.SCB))
                    {
                        int DepartmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                        dtInwardTransaction = db.GetReceivedEDR_ApprovedByMaker_ForChecker_Bankwise_forSCB(BranchID, BankList.SelectedValue, DepartmentID).Copy();
                    }
                    else
                    {
                        dtInwardTransaction = db.GetReceivedEDRApprovedByMaker(BranchID, BankList.SelectedValue).Copy();
                    }
                }
            }
            Session.Remove("InwardTransactions");
            Session["InwardTransactions"] = dtInwardTransaction;
            if (dtInwardTransaction.Rows.Count > 0)
            {
                dtInwardTransaction = GetCurrencyWiseData(dtInwardTransaction, CurrencyDdList.SelectedValue);
            }
            dv = dtInwardTransaction.DefaultView;
            dtgEFTCheckerApproved.CurrentPageIndex = 0;

            dtgEFTCheckerApproved.DataSource = dv;
            dtgEFTCheckerApproved.DataBind();

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

        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void BankList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void dtgEFTCheckerApproved_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = dtInwardTransaction.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgEFTCheckerApproved.DataSource = dv;
            dtgEFTCheckerApproved.DataBind();
        }

        public string sortOrder
        {
            get
            {
                dtgEFTCheckerApproved.DataSource = dtInwardTransaction;
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
                dtgEFTCheckerApproved.DataBind();
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            //ChangeStatusSelected();
            ChangeStatusSelected("Approve");
            BindData();
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (!txtRejectedReason.Text.Equals(string.Empty))
            {
                ChangeStatusSelected("Reject");
                //ChangeStatusSelected();
                EnterRejectReason();
                BindData();
            }
            else
            {
                lblNoReturnReason.Text = "Please enter reason";
                lblNoReturnReason.ForeColor = System.Drawing.Color.Red;
                lblNoReturnReason.Visible = true;
            }
        }

        private void ChangeStatusSelected(string statuschangesfor)
        {
            //THIS BLOCK IS DONE ONLY FOR NRB TESTING PURPOSE//
            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            int BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);

            //END//

            EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();
            EFTN.component.ApprovedInwardTransactionDB dbAIT = new EFTN.component.ApprovedInwardTransactionDB();
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            int cbxCounter = 0;
            bool CBSActive = true;

            if (bankCode.Equals(OriginalBankCode.UCBL))
            {
                BranchesDB branchDB = new BranchesDB();
                DataTable dtBranch = branchDB.GetBranchDetailsByBranchID(BranchID);

                if (bool.Parse(dtBranch.Rows[0]["CBSActive"].ToString()))
                {
                    CBSActive = true;
                }
                else
                {
                    CBSActive = false;
                }
            }
            else if (bankCode.Equals(OriginalBankCode.NRB))
            {
                string CBSIntegrationActive = ConfigurationManager.AppSettings["CBSIntegrationActive"];

                if (CBSIntegrationActive.Equals("1"))
                {
                    CBSActive = true;
                }
                else
                {
                    CBSActive = false;
                }
            }

            int failedCounter = 0;
            for (int i = 0; i < dtgEFTCheckerApproved.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerApproved.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    Guid edrId = (Guid)dtgEFTCheckerApproved.DataKeys[i];
                    if (statuschangesfor.Equals("Approve"))
                    {
                        if (bankCode.Equals(OriginalBankCode.NRB) && CBSActive)
                        {
                            FCUBSRTManager nrbManager = new FCUBSRTManager();
                            nrbManager.SendNRBRTReceivedTransactionXML(edrId, bankCode, ApprovedBy, ref failedCounter);
                        }
                        else if (bankCode.Equals(OriginalBankCode.UCBL) && CBSActive)
                        {
                            FCUBSRTManager nrbManager = new FCUBSRTManager();
                            nrbManager.SendNRBRTReceivedTransactionXML(edrId, bankCode, ApprovedBy, ref failedCounter);
                        }
                        else
                        {
                            dbAIT.UpdateReceivedEDRStatusApprovedByChecker(edrId, ApprovedBy);
                        }
                    }
                    else
                    {
                        db.UpdateReceivedEDRStatusRejectedByEBBSChecker(edrId, ApprovedBy);
                    }
                    cbxCounter++;
                }
            }
            if (cbxCounter < 1)
            {
                lblNoReturnReason.Text = "*Please select item";
                lblNoReturnReason.ForeColor = System.Drawing.Color.Red;
                lblNoReturnReason.Visible = true;
            }
            else
            {
                if ((bankCode.Equals(OriginalBankCode.UCBL) || bankCode.Equals(OriginalBankCode.NRB)) && CBSActive)
                {
                    int successfullCounter = cbxCounter - failedCounter;
                    lblNoReturnReason.ForeColor = System.Drawing.Color.Red;
                    lblNoReturnReason.Visible = true;
                    lblNoReturnReason.Text = successfullCounter + " Transactions Successful and " + failedCounter + " Transaction Failed";
                }
                else
                {
                    lblNoReturnReason.Visible = false;
                }
            }
        }

        private void EnterRejectReason()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgEFTCheckerApproved.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerApproved.Items[i].FindControl("cbxCheck");

                if (cbx.Checked)
                {
                    string edrId = dtgEFTCheckerApproved.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(EDRID, rejectReason,
                            (int)EFTN.Utility.ItemType.TransactionReceived);
                }
            }
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgEFTCheckerApproved.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerApproved.Items[i].FindControl("cbxCheck");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void dtgEFTCheckerApproved_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTCheckerApproved.CurrentPageIndex = e.NewPageIndex;
            dtgEFTCheckerApproved.DataSource = dtInwardTransaction;
            dtgEFTCheckerApproved.DataBind();
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
        private void BindGridWithRiskData(DataTable riskData)
        {
            dv = riskData.DefaultView;
            dtgEFTCheckerApproved.DataSource = dv;
            dtgEFTCheckerApproved.DataBind();
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
        #region Dynamic Functions For Dynamic Filtering on Front End

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

            else if (!session.Equals("ALL") && sessionCounter > 0 && !CurrencyDdList.SelectedValue.Equals("ALL") && currencyCounter > 0)
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


        private void BindGridWithFilteredData(DataTable filteredData)
        {
            dv = filteredData.DefaultView;
            dtgEFTCheckerApproved.DataSource = dv;
            dtgEFTCheckerApproved.DataBind();
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

        //private int GetCountForSelectedSession(DataTable bulkData, string session)
        //{
        //    int txnCounter = 0;
        //    foreach (DataRow row in bulkData.Rows)
        //    {
        //        if (row["SessionID"].ToString().Equals(session))
        //        {
        //            txnCounter++;
        //        }
        //    }
        //    return txnCounter;
        //}

        //public DataTable GetFilteredDataFromBulkDataByfilterExpression(DataTable bulkData, string columnName, string filterExpression)
        //{
        //    DataTable filteredData = new DataTable();
        //    return filteredData = bulkData.Select(columnName + "='" + filterExpression + "'").CopyToDataTable() ?? bulkData;
        //}



        #endregion        
    }
}
