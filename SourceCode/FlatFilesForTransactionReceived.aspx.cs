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
    public partial class FlatFilesForTransactionReceived : System.Web.UI.Page
    {
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        private static DataTable inwardDataTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSessionDropdownlist();
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');
                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("115"))
                {
                    Response.Redirect("~/FlatFilesForTransactionReceivedToHUB.aspx");
                }
                BindCurrencyTypeDropdownlist();
                BindData();
            }

        }
        private void BindSessionDropdownlist()
        {
            SessionDdList.DataSource = sentBatchDB.GetSessions(eftConString);
            SessionDdList.DataTextField = "SessionID";
            SessionDdList.DataValueField = "SessionID";
            SessionDdList.DataBind();
        }
        protected void BindCurrencyTypeDropdownlist()
        {
            DataTable dropDownData = new DataTable();
            dropDownData = sentBatchDB.GetCurrencyList(eftConString);
            CurrencyDdList.DataSource = dropDownData;
            CurrencyDdList.DataTextField = "Currency";
            CurrencyDdList.DataValueField = "Currency";
            CurrencyDdList.DataBind();
            CurrencyDdList.SelectedIndex = 0;
        }
        private void BindData()
        {
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            int txnCounter = 0;
            if (SelectedBank.Equals("215"))
            {
                if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    //dtgTransactionSentToOwnBank.DataSource = GetTransactionReceivedForDebit(ddListHold.SelectedValue, CurrencyDdList.SelectedValue);
                    //dtgTransactionSentToOwnBank.DataBind();
                    inwardDataTable = GetTransactionReceivedForDebit(ddListHold.SelectedValue, CurrencyDdList.SelectedValue);
                    txnCounter = GetCountForSelectedSession(inwardDataTable, SessionDdList.SelectedValue);
                    if (inwardDataTable.Rows.Count > 0 && txnCounter > 0)
                    {
                        inwardDataTable = GetFilteredDataFromBulkDataByfilterExpression(inwardDataTable, "SessionID", SessionDdList.SelectedValue);
                        dtgTransactionSentToOwnBank.DataSource = inwardDataTable;
                    }
                    else
                    {
                        dtgTransactionSentToOwnBank.DataSource = new DataTable();
                    }
                }
                else
                {
                    //dtgTransactionSentToOwnBank.DataSource = GetTransactionReceivedForCredit(ddListHold.SelectedValue,CurrencyDdList.SelectedValue);
                    //dtgTransactionSentToOwnBank.DataBind();
                    inwardDataTable = GetTransactionReceivedForCredit(ddListHold.SelectedValue, CurrencyDdList.SelectedValue);
                    txnCounter = GetCountForSelectedSession(inwardDataTable, SessionDdList.SelectedValue);
                    if (inwardDataTable.Rows.Count > 0 && txnCounter > 0)
                    {
                        inwardDataTable = GetFilteredDataFromBulkDataByfilterExpression(inwardDataTable, "SessionID", SessionDdList.SelectedValue);
                        dtgTransactionSentToOwnBank.DataSource = inwardDataTable;
                    }
                    else
                    {
                        dtgTransactionSentToOwnBank.DataSource = new DataTable();
                    }
                }
                dtgTransactionSentToOwnBank.DataBind();
            }
        }

        private DataTable GetTransactionReceivedForDebit(string Hold, string currency)
        {
            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            int hold = ParseData.StringToInt(Hold);
            return transactionToCBSDB.GetTransactionReceivedFlatFileForDebit(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                         + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                         + ddlistDay.SelectedValue.PadLeft(2, '0'), hold
                                                         , currency);
        }

        private DataTable GetTransactionReceivedForCredit(string Hold, string currency)
        {
            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            int hold = ParseData.StringToInt(Hold);
            return transactionToCBSDB.GetTransactionReceivedFlatFile(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                         + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                         + ddlistDay.SelectedValue.PadLeft(2, '0'), hold
                                                         , currency);

        }

        protected void btnGenerateFlatFileTransactionReceived_Click(object sender, EventArgs e)
        {
            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            DataTable dt;

            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }
            if (SelectedBank.Equals("215"))
            {
                if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    dt = GetTransactionReceivedForDebit(ddListHold.SelectedValue,CurrencyDdList.SelectedValue);
                    dt = GetFilteredDataFromBulkDataByfilterExpression(dt, "SessionID", SessionDdList.SelectedValue);
                }
                else
                {
                    dt = GetTransactionReceivedForCredit(ddListHold.SelectedValue, CurrencyDdList.SelectedValue);
                    dt = GetFilteredDataFromBulkDataByfilterExpression(dt, "SessionID", SessionDdList.SelectedValue);
                }
                EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();

                string flatfileResult = fc.CreatFlatFileForTransactionReceived(dt, ddListTransactionType.SelectedValue, DepartmentID);
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

            //Response.Redirect("~/FileWatcherChecker.aspx?PathKey=EFTCBSExport");
        }


        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void ddListHold_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void CurrencyDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        #region Dynamic Filtered Function

        private int GetCountForSelectedSession(DataTable bulkData, string session)
        {
            int txnCounter = 0;
            foreach (DataRow row in bulkData.Rows)
            {
                if (row["SessionID"].ToString().Equals(session))
                {
                    txnCounter++;
                }
            }
            return txnCounter;
        }

        public DataTable GetFilteredDataFromBulkDataByfilterExpression(DataTable bulkData, string columnName, string filterExpression)
        {
            DataTable filteredData = new DataTable();
            return filteredData = bulkData.Select(columnName + "='" + filterExpression + "'").CopyToDataTable() ?? bulkData;
        }
        #endregion

        protected void SessionDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
