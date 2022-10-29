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

namespace EFTN
{
    public partial class InwardReturnMaker : System.Web.UI.Page
    {
        private static DataTable myDTReturnReceived = new DataTable();
        DataView dv;
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private static string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("InwardReturns");
                BindCurrencyTypeDropdownlist();
                BindSessionDropdownlist();
                BindData();
                rblDishonorDecision.SelectedValue = "1";
                ddlDishonour.Enabled = false;
                txtAddendaInfo.Visible = false;
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
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.ReceivedReturnDB receivedReturnDB = new EFTN.component.ReceivedReturnDB();
            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                myDTReturnReceived = receivedReturnDB.GetReceivedReturn_ForDebit(DepartmentID);
                Session.Remove("InwardReturns");
                Session["InwardReturns"] = myDTReturnReceived;
            }
            else
            {
                myDTReturnReceived = receivedReturnDB.GetReceivedReturn(DepartmentID);
                Session.Remove("InwardReturns");
                Session["InwardReturns"] = myDTReturnReceived;
            }

            dv = myDTReturnReceived.DefaultView;
            dtgInwardReturnMaker.DataSource = dv;
            dtgInwardReturnMaker.DataBind();

            EFTN.component.CodeLookUpDB codeLookUpDb = new EFTN.component.CodeLookUpDB();
            ddlDishonour.DataSource = codeLookUpDb.GetCodeLookUp((int)EFTN.Utility.CodeType.DishonourReturn);
            ddlDishonour.DataBind();

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

        protected void dtgInwardReturnMaker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgInwardReturnMaker.CurrentPageIndex = e.NewPageIndex;
            dtgInwardReturnMaker.DataSource = myDTReturnReceived;
            dtgInwardReturnMaker.DataBind();
        }

        protected void dtgInwardReturnMaker_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = myDTReturnReceived.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgInwardReturnMaker.DataSource = dv;
            dtgInwardReturnMaker.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EFTMaker.aspx");
        }

        protected void rblDishonorDecision_SelectedIndexChanged(object sender, EventArgs e)
        {
            int type = Int32.Parse(rblDishonorDecision.SelectedValue);
            switch (type)
            {
                case 1:
                    ddlDishonour.Enabled = false;
                    txtAddendaInfo.Visible = false;
                    break;
                case 5:
                    ddlDishonour.Enabled = true;
                    txtAddendaInfo.Visible = true;
                    break;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int type = Int32.Parse(rblDishonorDecision.SelectedValue);
            for (int i = 0; i < dtgInwardReturnMaker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgInwardReturnMaker.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    string returnID = dtgInwardReturnMaker.DataKeys[i].ToString();
                    Guid ReturnID = new Guid(returnID);
                    switch (type)
                    {
                        case 1:
                            ChangeStatus(EFTN.Utility.TransactionStatus.Return_Received_Approved, ReturnID, "", "");
                            break;
                        case (int)EFTN.Utility.CodeType.DishonourReturn:
                            ChangeStatus(EFTN.Utility.TransactionStatus.Return_Received_Dishonor, ReturnID, ddlDishonour.SelectedValue, txtAddendaInfo.Text);
                            break;
                    }
                }
            }
            BindData();
        }

        private void ChangeStatus(int statusID, Guid returnID, string dishonorCode, string addendaInfo)
        {
            EFTN.component.ReceivedReturnDB db = new EFTN.component.ReceivedReturnDB();
            int CreatedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            db.Update_ReceivedReturn_Status(statusID, returnID, dishonorCode, CreatedBy, addendaInfo);
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgInwardReturnMaker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgInwardReturnMaker.Items[i].FindControl("CheckBEFTNList");
                cbx.Checked = checkAllChecked;
            }
        }



        protected void dtgInwardReturnMaker_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            DataRowView test;
            if (e.Item != null && e.Item.DataItem is DataRowView)
            {
                test = (DataRowView)e.Item.DataItem;
                if (test.Row["MismatchAmount"].ToString() == "yes")
                {
                    e.Item.BackColor = System.Drawing.Color.Red;
                }
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

        #region Dynamic Functions for filtering at front end
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

        private void BindGridWithFilteredData(DataTable filteredData)
        {
            dv = filteredData.DefaultView;
            dtgInwardReturnMaker.DataSource = dv;
            dtgInwardReturnMaker.DataBind();
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
        #endregion
    }

}
