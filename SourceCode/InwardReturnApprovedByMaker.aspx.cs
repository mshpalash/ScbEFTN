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
using EFTN.BLL;
using EFTN.Utility;
using FloraSoft;
using EFTN.component;

namespace EFTN
{
    public partial class InwardReturnApprovedByMaker : System.Web.UI.Page
    {
        private static DataTable myDTReturnReceived = new DataTable();
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
            EFTN.component.ReceivedReturnDB receivedReturnDB = new EFTN.component.ReceivedReturnDB();
            int DepartmentID = 0;
            //int txnCounter = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }
            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                myDTReturnReceived = receivedReturnDB.ReceivedReturn_Approved_ForChecker_ForDebit(DepartmentID);
                Session.Remove("InwardReturns");
                Session["InwardReturns"] = myDTReturnReceived;
                //txnCounter = GetCountForSelectedSession(myDTReturnReceived, SessionDdList.SelectedValue);
                //if (myDTReturnReceived.Rows.Count > 0 && txnCounter > 0)
                //{
                //    myDTReturnReceived = GetFilteredDataFromBulkDataByfilterExpression(myDTReturnReceived, "SessionID", SessionDdList.SelectedValue);
                //}
                //else
                //{
                //    myDTReturnReceived = new DataTable();
                //}
            }
            else
            {
                myDTReturnReceived = receivedReturnDB.ReceivedReturn_Approved_ForChecker(DepartmentID);
                Session.Remove("InwardReturns");
                Session["InwardReturns"] = myDTReturnReceived;
                //txnCounter = GetCountForSelectedSession(myDTReturnReceived, SessionDdList.SelectedValue);
                //if (myDTReturnReceived.Rows.Count > 0 && txnCounter > 0)
                //{
                //    myDTReturnReceived = GetFilteredDataFromBulkDataByfilterExpression(myDTReturnReceived, "SessionID", SessionDdList.SelectedValue);
                //}
                //else
                //{
                //    myDTReturnReceived = new DataTable();
                //}
            }

            dv = myDTReturnReceived.DefaultView;
            dtgApprovedReturnChecker.DataSource = dv;
            dtgApprovedReturnChecker.DataBind();

            txtReceivedReturnApprovedRejectReason.Text = "";

            if (myDTReturnReceived.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + myDTReturnReceived.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(myDTReturnReceived.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
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

        protected void btnInwardReturnApproved_Click(object sender, EventArgs e)
        {
            UpdateReceivedReturn(EFTN.Utility.TransactionStatus.Return_Received_Approval__Approved_by_checker);
        }

        protected void btnInwardRetrunReject_Click(object sender, EventArgs e)
        {
            if (txtReceivedReturnApprovedRejectReason.Text != "")
            {
                UpdateReceivedReturn(EFTN.Utility.TransactionStatus.Rejected_By_Checker_Return_Received_Approved);
            }
            else
            {
                lblMsgApproved.Text = "Enter reject reason";
                lblMsgApproved.Visible = true;
            }
        }
        
        private void UpdateReceivedReturn(int statusID)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            bool CBSActive = true;

            if (bankCode.Equals(OriginalBankCode.UCBL))
            {
                int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                DepartmentsDB departmentDB = new DepartmentsDB();
                DataTable dtDept = departmentDB.EFT_GetDepartmentDetailsByDepartmentID(DepartmentID);

                if (bool.Parse(dtDept.Rows[0]["CBSActive"].ToString()))
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

            EFTN.component.ReceivedReturnDB db = new EFTN.component.ReceivedReturnDB();
            int failedCounter = 0;
            int cbxCounter = 0;

            for (int i = 0; i < dtgApprovedReturnChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgApprovedReturnChecker.Items[i].FindControl("CheckBEFTNList");
                if (cbx.Checked)
                {
                    Guid returnID = (Guid)dtgApprovedReturnChecker.DataKeys[i];
                    //string CBSReference = string.Empty;
                    cbxCounter++;

                    if ((bankCode.Equals(OriginalBankCode.NRB) || bankCode.Equals(OriginalBankCode.UCBL)) && CBSActive)
                    {
                        if (statusID == EFTN.Utility.TransactionStatus.Return_Received_Approval__Approved_by_checker)
                        {
                            FCUBSRTManager nrbManager = new FCUBSRTManager();
                            failedCounter += nrbManager.SendNRBRTReceivedReturnXML(returnID, bankCode, ApprovedBy);
                        }
                        else
                        {
                            db.Update_ReceivedReturn_Status_ByChecker(statusID, returnID);
                        }
                    }
                    else
                    {
                        db.Update_ReceivedReturn_Status_ByChecker(statusID, returnID);
                    }
                    if (statusID == EFTN.Utility.TransactionStatus.Rejected_By_Checker_Return_Received_Approved)
                    {
                        string ReturnApproveRejection = txtReceivedReturnApprovedRejectReason.Text;
                        EFTN.component.RejectReasonByCheckerDB rejectReasonByCheckerDB = new EFTN.component.RejectReasonByCheckerDB();
                        rejectReasonByCheckerDB.Insert_RejectReason_ByChecker(returnID, ReturnApproveRejection, (int)EFTN.Utility.ItemType.ReturnReceived);
                    }
                }
            }
            BindData();
            if ((bankCode.Equals(OriginalBankCode.NRB) || bankCode.Equals(OriginalBankCode.UCBL)) && CBSActive)
            {
                int successfullCounter = cbxCounter - failedCounter;
                lblMsgApproved.Text = successfullCounter + " transaction successfully processed and " + failedCounter + " transaction failed to process";
                lblMsgApproved.Visible = true;
            }
            else
            {
                lblMsgApproved.Visible = false;
                txtReceivedReturnApprovedRejectReason.Text = "";
            }
        }


        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgApprovedReturnChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgApprovedReturnChecker.Items[i].FindControl("CheckBEFTNList");
                cbx.Checked = checkAllChecked;
            }
        }

            
        protected void dtgApprovedReturnChecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgApprovedReturnChecker.CurrentPageIndex = e.NewPageIndex;
            dtgApprovedReturnChecker.DataSource = myDTReturnReceived;
            dtgApprovedReturnChecker.DataBind();
        }

        protected void dtgApprovedReturnChecker_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = myDTReturnReceived.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgApprovedReturnChecker.DataSource = dv;
            dtgApprovedReturnChecker.DataBind();
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
            dtgApprovedReturnChecker.DataSource = dv;
            dtgApprovedReturnChecker.DataBind();
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
