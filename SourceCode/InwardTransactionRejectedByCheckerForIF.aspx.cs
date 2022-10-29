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
    public partial class InwardTransactionRejectedByCheckerForIF : System.Web.UI.Page
    {
        private static DataTable myDTTransReceive = new DataTable();
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private static string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        DataView dv;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("InwardTransactions");
                BindCurrencyTypeDropdownlist();
                BindSessionDropdownlist();
                BindData();
                //ddlReturnChangeCode.Enabled = false;
                //txtCorrectedData.Text = "";
                //txtCorrectedData.Visible = false;
                //lblCorrectedDataMsg.Visible = false;
                rblTransactionDecision.SelectedValue = "1";
            }
        }
        protected void BindCurrencyTypeDropdownlist()
        {
            DataTable dropDownData = new DataTable();
            dropDownData = sentBatchDB.GetCurrencyList(eftConString);
            CurrencyDdList.DataSource = dropDownData;
            CurrencyDdList.DataTextField = "Currency";
            CurrencyDdList.DataValueField = "Currency";
            CurrencyDdList.DataBind();
        }
        private void BindSessionDropdownlist()
        {
            DataTable dropDownData = new DataTable();
            dropDownData = sentBatchDB.GetSessions(eftConString);
            SessionDdList.DataSource = dropDownData;
            SessionDdList.DataTextField = "SessionID";
            SessionDdList.DataValueField = "SessionID";
            SessionDdList.DataBind();
        }
        private void BindData()
        {
            int BranchID = 0;

            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            if (DepartmentID == 0)
            {
                BranchID = 0;
            }
            else if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);
            }
            EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();
            //if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            //{
                myDTTransReceive = db.GetInwardTransactionRejectedByCheckerForDebitForIF(BranchID);
            //}
            //else
            //{
            //    myDTTransReceive = db.GetInwardTransactionRejectedByChecker(BranchID);
            //}
            Session.Remove("InwardTransactions");
            Session["InwardTransactions"] = myDTTransReceive;
            if (myDTTransReceive.Rows.Count > 0)
            {
                myDTTransReceive = GetCurrencyWiseData(myDTTransReceive, CurrencyDdList.SelectedValue);
            }
            dtgInwardTransactionMaker.DataSource = myDTTransReceive;
            dtgInwardTransactionMaker.DataBind();
            BindBatchTotal();
        }

        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void rblTransactionDecision_SelectedIndexChanged(object sender, EventArgs e)
        {
            int type = Int32.Parse(rblTransactionDecision.SelectedValue);
            //if (type == 1)
            //{
            //    ddlReturnChangeCode.Enabled = false;
            //    txtCorrectedData.Text = "";
            //    txtCorrectedData.Visible = false;
            //    lblCorrectedDataMsg.Visible = false;

            //}
            //else
            //{
            //    ddlReturnChangeCode.Enabled = true;
            //    EFTN.component.CodeLookUpDB codeDB = new EFTN.component.CodeLookUpDB();
            //    ddlReturnChangeCode.DataSource = codeDB.GetCodeLookUp(type);
            //    ddlReturnChangeCode.DataBind();
            //    if (type == 3)
            //    {
            //        txtCorrectedData.Visible = true;
            //        lblCorrectedDataMsg.Visible = true;
            //    }
            //    else
            //    {
            //        txtCorrectedData.Text = "";
            //        txtCorrectedData.Visible = false;
            //        lblCorrectedDataMsg.Visible = false;
            //    }

            //}
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int type = Int32.Parse(rblTransactionDecision.SelectedValue);
            switch (type)
            {
                case 1:
                    ChangeStatusSelected(EFTN.Utility.TransactionStatus.TransReceived_Approved);
                    break;               
            }
        }

        private void ChangeStatusSelected(int statusID)
        {
            //string returnCode = (ddlReturnChangeCode.SelectedValue != null) ? ddlReturnChangeCode.SelectedValue : "";
            EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();
            int cbxCounter = 0;
            int CreatedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            for (int i = 0; i < dtgInwardTransactionMaker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgInwardTransactionMaker.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    Guid edrId = (Guid)dtgInwardTransactionMaker.DataKeys[i];
                    db.UpdateEDRReceivedStatusForOutwardDebitReturn(edrId, statusID ,CreatedBy);    
                    cbxCounter++;
                }
            }
            if (cbxCounter < 1)
            {
                lblMsg.Text = "Please select an item!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
            }
            else
            {
                lblMsg.Visible = false;
            }
            BindData();
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



        //protected void dtgInwardTransactionMaker_ItemCommand(object source, DataGridCommandEventArgs e)
        //{
        //    if (e.CommandName == "Cancel")
        //    {
        //        dtgInwardTransactionMaker.EditItemIndex = -1;
        //    }
        //    if (e.CommandName == "Edit")
        //    {
        //        dtgInwardTransactionMaker.EditItemIndex = e.Item.ItemIndex;
        //    }
        //    if (e.CommandName == "Update")
        //    {
        //        Guid EDRID = (Guid)dtgInwardTransactionMaker.DataKeys[e.Item.ItemIndex];
        //        TextBox txtDFIAccountNo = (TextBox)e.Item.FindControl("DFIAccountNo");
        //        int CreatedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

        //        EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();

        //        db.UpdateInwardTransactionDFIAccountNumberByEFTUser(EDRID, txtDFIAccountNo.Text.Trim(), CreatedBy);
        //        dtgInwardTransactionMaker.EditItemIndex = -1;
        //    }
        //    BindData();


        //}

        protected void btnGenerateRevarseFlatFile_Click(object sender, EventArgs e)
        {
            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            DataTable dt;

            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            //int DepartmentID = 0;
            //if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            //{
            //    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            //}
            dt = GetTransactionReceivedForDebitReverseForIF();

            EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();

            string flatfileResult = fc.CreatFlatFileForTransactionReceivedDebitReverse(dt);
            string fileName = "CBS" + "-" + "FlatFile-TR" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
            Response.Clear();
            Response.AddHeader("content-disposition",
                     "attachment;filename=" + fileName + ".txt");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.text";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite =
                          new HtmlTextWriter(stringWrite);
            Response.Write(flatfileResult.ToString());
            Response.End();

        }

        private DataTable GetTransactionReceivedForDebitReverse()
        {
            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            return transactionToCBSDB.GetTransactionReceivedFlatFileForDebitReverse(CurrencyDdList.SelectedValue);
        }

        private DataTable GetTransactionReceivedForDebitReverseForIF()
        {
            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            return transactionToCBSDB.GetTransactionReceivedFlatFileForDebitReverseForIF(CurrencyDdList.SelectedValue);
        }
        private void BindBatchTotal()
        {
            if (myDTTransReceive.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + myDTTransReceive.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + myDTTransReceive.Compute("SUM(Amount)", "").ToString();
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
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

        #region Dynamic Functions for filtering at front end
        public DataTable GetCurrencyWiseData(DataTable gridData, string currency)
        {
            DataTable currencyWiseData = new DataTable();
            int currencyCounter = 0;
            int sessionCounter = 0;
            currencyCounter = GetCurrencyCount(gridData, currency);
            sessionCounter = GetSessionCount(gridData, SessionDdList.SelectedValue);
            if (currencyCounter > 0 && sessionCounter > 0)
            {
                var linqResult = gridData.AsEnumerable()
                       .Where(c => c.Field<string>("Currency") == currency && c.Field<Byte>("SessionID") == Byte.Parse(SessionDdList.SelectedValue));
                if (linqResult.AsDataView().ToTable().Rows.Count > 0)
                {
                    currencyWiseData = linqResult.CopyToDataTable();
                }
            }
            return currencyWiseData;
        }

        public DataTable GetSessionWiseData(DataTable gridData, string session)
        {
            DataTable sessionWiseData = new DataTable();
            int currencyCounter = 0;
            int sessionCounter = 0;
            currencyCounter =  GetCurrencyCount(gridData, CurrencyDdList.SelectedValue);
            sessionCounter =  GetSessionCount(gridData, session);
            if (currencyCounter > 0 && sessionCounter > 0)
            {
                var linqResult = gridData.AsEnumerable()
                       .Where(c => c.Field<string>("Currency") == CurrencyDdList.SelectedValue && c.Field<Byte>("SessionID") == Byte.Parse(SessionDdList.SelectedValue));
                if (linqResult.AsDataView().ToTable().Rows.Count > 0)
                {
                    sessionWiseData = linqResult.CopyToDataTable();
                }
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
        #endregion
    }
}
