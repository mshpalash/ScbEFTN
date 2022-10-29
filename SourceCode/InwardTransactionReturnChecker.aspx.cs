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

namespace EFTN
{
    public partial class InwardTransactionReturnChecker : System.Web.UI.Page
    {
        private static DataTable dtInwardReturn = new DataTable();
        DataView dv;
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("InwardReturns");
                BindCurrencyTypeDropdownlist();
                BindSessionDropdownlist();
                BindData();
                lblNoReturnReason.Visible = false;
                sortOrder = "asc";
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

        private void BindData()
        {
            EFTN.component.SentReturnDB db = new EFTN.component.SentReturnDB();

            int BranchID = 0;
            string originBankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);
            }

            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                if (originBankCode.Equals(OriginalBankCode.SCB))
                {
                    int DepartmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                    dtInwardReturn = db.GetSentRRForChecker_ForDebit_ForSCB(BranchID, DepartmentID);
                    Session.Remove("InwardReturns");
                    Session["InwardReturns"] = dtInwardReturn;
                }
                else
                {
                    dtInwardReturn = db.GetSentRRForChecker_ForDebit(BranchID);
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
                        dtInwardReturn = db.GetSentRRForChecker(BranchID);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (originBankCode.Equals(OriginalBankCode.SCB))
                    {
                        int DepartmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                        dtInwardReturn = db.GetSentRRForChecker_ForSCB(BranchID, DepartmentID);
                        Session.Remove("InwardReturns");
                        Session["InwardReturns"] = dtInwardReturn;
                    }
                    else
                    {
                        dtInwardReturn = db.GetSentRRForChecker(BranchID);
                    }
                }
            }

            dv = dtInwardReturn.DefaultView;

            dtgEFTCheckerReturns.CurrentPageIndex = 0;
            dtgEFTCheckerReturns.DataSource = dv;
            dtgEFTCheckerReturns.DataBind();


            if (dtInwardReturn.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + dtInwardReturn.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(dtInwardReturn.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
            cbxAll.Checked = false;
        }

        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void dtgEFTCheckerReturns_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTCheckerReturns.CurrentPageIndex = e.NewPageIndex;
            dtgEFTCheckerReturns.DataSource = dtInwardReturn;
            dtgEFTCheckerReturns.DataBind();
        }

        protected void dtgEFTCheckerReturns_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = dtInwardReturn.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgEFTCheckerReturns.DataSource = dv;
            dtgEFTCheckerReturns.DataBind();
        }
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            ChangeStatusOfCheckedItems(EFTN.Utility.TransactionStatus.RRSentApprovedByChecker);
            txtRejectedReason.Text = "";
            lblNoReturnReason.Visible = false;
            BindData();
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (txtRejectedReason.Text != "")
            {
                lblNoReturnReason.Visible = false;
                EnterRejectReason();
                ChangeStatusOfCheckedItems(EFTN.Utility.TransactionStatus.RRSent_Rejected_By_Checker);
                txtRejectedReason.Text = "";
                BindData();
            }
            else
            {
                lblNoReturnReason.Visible = true;
            }
        }
        private void EnterRejectReason()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgEFTCheckerReturns.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerReturns.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    string edrId = dtgEFTCheckerReturns.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(EDRID, rejectReason,
                            (int)EFTN.Utility.ItemType.ReturnSent);
                }
            }

        }
        private void ChangeStatusOfCheckedItems(int statusID)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            for (int i = 0; i < dtgEFTCheckerReturns.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerReturns.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    
                    Guid returnID = (Guid)dtgEFTCheckerReturns.DataKeys[i];

                    EFTN.component.SentReturnDB db = new EFTN.component.SentReturnDB();
                    db.UpdateReturnSentStatus(statusID, returnID, ApprovedBy);
                }
            }
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgEFTCheckerReturns.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerReturns.Items[i].FindControl("CheckBEFTNList");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void CurrencyDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable gridData = new DataTable();
            DataTable filteredData = new DataTable();
            gridData = (DataTable)Session["InwardReturns"];
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
            gridData = (DataTable)Session["InwardReturns"];
            if (gridData != null && gridData.Rows.Count > 0)
            {
                filteredData = GetSessionWiseData(gridData, SessionDdList.SelectedValue);
                BindGridWithFilteredData(filteredData);
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
            if (!currency.Equals("ALL") && currencyCounter.Equals(0) && SessionDdList.SelectedValue.Equals("ALL"))
            {
                currencyWiseData = new DataTable();
            }
            else if (!currency.Equals("ALL") && SessionDdList.SelectedValue.Equals("ALL"))
            {
                currencyWiseData = gridData.AsEnumerable()
                       .Where(c => c.Field<string>("Currency") == currency).CopyToDataTable();
            }
            else if (currency.Equals("ALL") && !SessionDdList.SelectedValue.Equals("ALL"))
            {
                currencyWiseData = gridData.AsEnumerable()
                       .Where(c => c.Field<Byte>("SessionID") == Byte.Parse(SessionDdList.SelectedValue)).CopyToDataTable();
            }
            else if (!currency.Equals("ALL") && !SessionDdList.SelectedValue.Equals("ALL") && currencyCounter > 0 && sessionCounter > 0)
            {
                var linqResult = gridData.AsEnumerable()
                       .Where(c => c.Field<string>("Currency") == currency && c.Field<Byte>("SessionID") == Byte.Parse(SessionDdList.SelectedValue));
                if (linqResult.AsDataView().ToTable().Rows.Count > 0)
                {
                    currencyWiseData = linqResult.CopyToDataTable();
                }
            }
            else if (currency.Equals("ALL") && SessionDdList.SelectedValue.Equals("ALL"))
            {
                currencyWiseData = gridData;
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
            if (!session.Equals("ALL") && sessionCounter.Equals(0) && CurrencyDdList.SelectedValue.Equals("ALL"))
            {
                sessionWiseData = new DataTable();
            }
            else if (!session.Equals("ALL") && sessionCounter > 0 && CurrencyDdList.SelectedValue.Equals("ALL"))
            {
                sessionWiseData = gridData.AsEnumerable()
                       .Where(c => c.Field<Byte>("SessionID") == Byte.Parse(session)).CopyToDataTable();
            }
            else if (session.Equals("ALL") && !CurrencyDdList.SelectedValue.Equals("ALL"))
            {
                sessionWiseData = gridData.AsEnumerable()
                       .Where(c => c.Field<string>("Currency") == CurrencyDdList.SelectedValue).CopyToDataTable();
            }
            else if (!session.Equals("ALL") && !CurrencyDdList.SelectedValue.Equals("ALL") && currencyCounter > 0 && sessionCounter > 0)
            {
                var linqResult = gridData.AsEnumerable()
                       .Where(c => c.Field<Byte>("SessionID") == Byte.Parse(session) && c.Field<string>("Currency") == CurrencyDdList.SelectedValue);
                if (linqResult.AsDataView().ToTable().Rows.Count > 0)
                {
                    sessionWiseData = linqResult.CopyToDataTable();
                }
            }
            else if (session.Equals("ALL") && CurrencyDdList.SelectedValue.Equals("ALL"))
            {
                sessionWiseData = gridData;
            }
            return sessionWiseData;
        }


        private void BindGridWithFilteredData(DataTable filteredData)
        {
            dv = filteredData.DefaultView;
            dtgEFTCheckerReturns.DataSource = dv;
            dtgEFTCheckerReturns.DataBind();
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
        #endregion               
    }
}
