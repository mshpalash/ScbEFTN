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
    public partial class EFTTransactionRejectedForMaker : System.Web.UI.Page
    {
        DataView dv;
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("RejectedTransactions");
                BindCurrencyTypeDropdownlist();
                BindData();
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
        private void BindData()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();
            DataTable myDt = new DataTable();
            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                myDt = edrDB.GetSentEDR_ByCheckerRejectionDebit(DepartmentID);
            }
            else
            {
                myDt = edrDB.GetSentEDR_ByCheckerRejectionCredit(DepartmentID);

            }
            Session.Remove("RejectedTransactions");
            Session["RejectedTransactions"] = myDt;
            if (myDt.Rows.Count > 0)
            {
                myDt = GetFilteredDataFromBulkDataByfilterExpression(myDt, "Currency", CurrencyDdList.SelectedValue);
            }
            dtgRejectedEDR.DataSource = myDt;
            dtgRejectedEDR.DataBind();
            BindBatchTotal(myDt);
        }

        private void BindBatchTotal(DataTable myDTTransReceive)
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();
            int cbxCounter = 0;
            for (int i = 0; i < dtgRejectedEDR.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgRejectedEDR.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    Guid edrId = (Guid)dtgRejectedEDR.DataKeys[i];
                    //db.UpdateEDRReceivedStatus(edrId, statusID, returnCode, 0, 0, txtCorrectedData.Text);
                    edrDB.DeleteTransactionSent(edrId);
                    cbxCounter++;
                }
            }
            if (cbxCounter < 1)
            {
                lblMsg.Text = "*Please select item";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
            }
            else
            {
                lblMsg.Visible = false;
            }
            BindData();
        }

        private void GenerateFlatFile(DataTable dt)
        {
            EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            string flatfileResult = string.Empty;

            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                flatfileResult = fc.CreatFlatFileForTransactionSentReverseForRejectedDebit(dt, DepartmentID);
            }
            else
            {
                flatfileResult = fc.CreatFlatFileForTransactionSentReverseForRejectedCredit(dt, DepartmentID);
            }

            string fileName = "CBS" + "-" + "FlatFile-TS-RevRej" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
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

            //Response.Redirect("~/FileWatcherChecker.aspx?PathKey=EFTCBSExport");
        }

        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void linkBtnFlatFile_Click(object sender, EventArgs e)
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();
            DataTable dtReveseFlatFileData = new DataTable();//edrDB.GetSentEDR_ByCheckerRejectionForReverseFlatFile(DepartmentID);//Query Will be Changed
            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                dtReveseFlatFileData = edrDB.GetSentEDR_ByCheckerRejectionForReverseFlatFileDebit(DepartmentID, CurrencyDdList.SelectedValue);
            }
            else
            {
                dtReveseFlatFileData = edrDB.GetSentEDR_ByCheckerRejectionForReverseFlatFileCredit(DepartmentID, CurrencyDdList.SelectedValue);
            }
            GenerateFlatFile(dtReveseFlatFileData);
        }


        protected void CurrencyDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int txnCounter = 0;
            DataTable gridData = new DataTable();
            DataTable filteredData = new DataTable();
            gridData = (DataTable)Session["RejectedTransactions"];
            txnCounter = GetCurrencyCount(gridData, CurrencyDdList.SelectedValue);
            if (txnCounter > 0)
            {
                gridData = GetFilteredDataFromBulkDataByfilterExpression(gridData, "Currency", CurrencyDdList.SelectedValue);
                BindGridWithFilteredData(gridData);
            }
            else
            {
                gridData = new DataTable();
                BindGridWithFilteredData(gridData);
            }
        }

        #region Dynamic Functions For Dynamic Filtering on Front End

        private void BindGridWithFilteredData(DataTable filteredData)
        {
            dv = filteredData.DefaultView;
            dtgRejectedEDR.DataSource = dv;
            dtgRejectedEDR.DataBind();
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
        public DataTable GetFilteredDataFromBulkDataByfilterExpression(DataTable bulkData, string columnName, string filterExpression)
        {
            DataTable filteredData = new DataTable();
            int currencyCounter = 0;
            currencyCounter = GetCurrencyCount(bulkData, filterExpression.Trim());
            if (currencyCounter > 0)
            {
                filteredData = bulkData.Select(columnName + "='" + filterExpression + "'").CopyToDataTable() ?? bulkData;
            }
            return filteredData;
        }
        #endregion
    }
}
