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
    public partial class FlatFilesForReturnReceived : System.Web.UI.Page
    {
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        private static DataTable inwardReturnDataTable = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');

                string originBankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                if (originBankCode.Equals(OriginalBankCode.HSBC))
                {
                    Response.Redirect("~/FlatFilesForReturnReceivedToHUB.aspx");
                }
                else if (originBankCode.Equals(OriginalBankCode.CBL))
                {
                    Response.Redirect("~/FlatFilesForReturnReceivedCity.aspx");
                }

                BindSessionDropdownlist();
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
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            int txnCounter = 0;

            if (SelectedBank.Equals("215"))
            {
                if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
                    inwardReturnDataTable = transactionToCBSDB.GetFlatFileForReturnReceivedDebitForSCB(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue);
                    txnCounter = GetCountForSelectedSession(inwardReturnDataTable, SessionDdList.SelectedValue);


                    if (inwardReturnDataTable.Rows.Count > 0 && txnCounter > 0)
                    {
                        inwardReturnDataTable = GetFilteredDataFromBulkDataByfilterExpression(inwardReturnDataTable, "SessionID", SessionDdList.SelectedValue);
                        dtgTransactionSentToOwnBank.DataSource = inwardReturnDataTable;
                    }
                    else
                    {
                        dtgTransactionSentToOwnBank.DataSource = new DataTable();
                    }

                    //dtgTransactionSentToOwnBank.DataSource = transactionToCBSDB.GetFlatFileForReturnReceivedDebitForSCB(ddlistYear.SelectedValue.PadLeft(4, '0')
                    //                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
                    //                                             + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                    //dtgTransactionSentToOwnBank.DataBind();
                }
                else
                {
                    EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
                    inwardReturnDataTable = transactionToCBSDB.GetFlatFileForReturnReceivedForSCB(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue);
                    txnCounter = GetCountForSelectedSession(inwardReturnDataTable, SessionDdList.SelectedValue);


                    if (inwardReturnDataTable.Rows.Count > 0 && txnCounter > 0)
                    {
                        inwardReturnDataTable = GetFilteredDataFromBulkDataByfilterExpression(inwardReturnDataTable, "SessionID", SessionDdList.SelectedValue);
                        dtgTransactionSentToOwnBank.DataSource = inwardReturnDataTable;
                    }
                    else
                    {
                        dtgTransactionSentToOwnBank.DataSource = new DataTable();
                    }
                    //dtgTransactionSentToOwnBank.DataSource = transactionToCBSDB.GetFlatFileForReturnReceivedForSCB(ddlistYear.SelectedValue.PadLeft(4, '0')
                    //                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
                    //                                             + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                    //dtgTransactionSentToOwnBank.DataBind();
                }
                dtgTransactionSentToOwnBank.DataBind();
            }
        }

        
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

        protected void linkBtnFlatFileReturnReceived_Click(object sender, EventArgs e)
        {

            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            DataTable dt;
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            if (SelectedBank.Equals("215"))
            {
                if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    dt = transactionToCBSDB.GetFlatFileForReturnReceivedDebitForSCB(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue);
                    // dt = GetFilteredDataFromBulkDataByfilterExpression(dt, "SessionID", SessionDdList.SelectedValue);
                }
                else
                {
                    dt = transactionToCBSDB.GetFlatFileForReturnReceivedForSCB(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue);
                    // dt = GetFilteredDataFromBulkDataByfilterExpression(dt, "SessionID", SessionDdList.SelectedValue);
                }
                if (dt.Rows.Count > 0)
                {
                    dt = GetFilteredDataFromBulkDataByfilterExpression(dt, "SessionID", SessionDdList.SelectedValue);
                    EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();

                    string flatfileResult = fc.CreatFlatFileForReturnReceivedForSCB(dt, ddListTransactionType.SelectedValue, DepartmentID);
                    string fileName = "CBS" + "-" + "FlatFile-RR" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
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
            }
            else if (SelectedBank.Equals("175"))
            {
                dt = transactionToCBSDB.GetPubaliFlatFileForReturnReceived(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'));

                EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();

                string flatfileResult = fc.CreatFlatFileForReturnReceivedForPubali(dt);
                string fileName = "CBS" + "-" + "FlatFile-RR" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
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
        protected void CurrencyDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        protected void SessionDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
