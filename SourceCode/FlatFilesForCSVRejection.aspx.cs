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
using System.IO;
using EFTN.Utility;
using EFTN.component;

namespace EFTN
{
    public partial class FlatFilesForCSVRejection : System.Web.UI.Page
    {
        //DataView dv;
        public static DataTable myDt = new DataTable();
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');
                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("115"))
                {
                    Response.Redirect("~/FlatFilesForTransactionReceivedToHUB.aspx");
                }

                BindCurrencyTypeDropdownlist();

                BindData();

               

                //Session.Remove("RejectedTransactions");
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
            CurrencyDdList.SelectedIndex = 0;
        }


        private void BindData()
        {
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
           
            if (SelectedBank.Equals("215"))
            {
                if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
                    //dtgTransactionSentToOwnBank.DataSource = transactionToCBSDB.GetCSVRejectionFlatFileForDebit(ddlistYear.SelectedValue.PadLeft(4, '0')
                    //                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
                    //                                             + ddlistDay.SelectedValue.PadLeft(2, '0'), CurrencyDdList.SelectedValue);
                    //dtgTransactionSentToOwnBank.DataBind();

                    myDt = transactionToCBSDB.GetCSVRejectionFlatFileForDebit(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), CurrencyDdList.SelectedValue);
                }
                else
                {
                    EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
                    //dtgTransactionSentToOwnBank.DataSource = transactionToCBSDB.GetCSVRejectionFlatFile(ddlistYear.SelectedValue.PadLeft(4, '0')
                    //                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
                    //                                             + ddlistDay.SelectedValue.PadLeft(2, '0'), CurrencyDdList.SelectedValue);
                    //dtgTransactionSentToOwnBank.DataBind();

                    myDt = transactionToCBSDB.GetCSVRejectionFlatFile(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), CurrencyDdList.SelectedValue);
                }

               
                //if (myDt.Rows.Count > 0)
                //{
                //    Session.Remove("RejectedTransactions");
                //    Session["RejectedTransactions"] = myDt;
                //    myDt = GetFilteredDataFromBulkDataByfilterExpression(myDt, "Currency", CurrencyDdList.SelectedValue);
                //}
                dtgTransactionSentToOwnBank.DataSource = myDt;
                dtgTransactionSentToOwnBank.DataBind();
                //BindBatchTotal(myDt);
            }
        }

        protected void btnGenerateFlatFileTransactionReceived_Click(object sender, EventArgs e)
        {
            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            DataTable dt;

            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            if (SelectedBank.Equals("215"))
            {
                if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    dt = transactionToCBSDB.GetCSVRejectionFlatFileForDebit(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), CurrencyDdList.SelectedValue);
                }
                else
                {
                    dt = transactionToCBSDB.GetCSVRejectionFlatFile(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), CurrencyDdList.SelectedValue);
                }
                EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();

                string flatfileResult = fc.CreatFlatFileForCSVRejection(dt, ddListTransactionType.SelectedValue);
                string fileName = "CBS" + "-" + "FlatFile-Rejected CSV" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
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

        protected void btnExportCSV_Click(object sender, EventArgs e)
        {
            DataTable myDataTable = new DataTable();

            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
                myDataTable = transactionToCBSDB.GetCSVRejectionFlatFileForDebit(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'), CurrencyDdList.SelectedValue);
            }
            else
            {
                EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
                myDataTable = transactionToCBSDB.GetCSVRejectionFlatFile(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'), CurrencyDdList.SelectedValue);
            }

            if (myDataTable.Rows.Count > 0)
            {
                string xlsFileName = "CSVRejectionReport" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                string attachment = "attachment; filename=" + xlsFileName + ".csv";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.csv";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                // Create the CSV file to which grid data will be exported. 
                //StreamWriter sw = new StreamWriter();
                int iColCount = myDataTable.Columns.Count;

                // First we will write the headers. 

                for (int i = 0; i < iColCount; i++)
                {
                    sw.Write("\"");
                    sw.Write(myDataTable.Columns[i]);
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
                foreach (DataRow dr in myDataTable.Rows)
                {
                    for (int i = 0; i < iColCount; i++)
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

        protected void CurrencyDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
            //int txnCounter = 0;
            //DataTable gridData = new DataTable();
            //DataTable filteredData = new DataTable();
            //gridData = (DataTable)Session["RejectedTransactions"];
            ////if (CurrencyDdList.SelectedValue.Equals("ALL"))
            ////{
            ////    BindGridWithFilteredData(gridData);
            ////}
            ////if (!CurrencyDdList.SelectedValue.Equals("ALL") && gridData.Rows.Count > 0)
            //if ( myDt.Rows.Count > 0)
            //{
            //    txnCounter = GetCurrencyCount(gridData, CurrencyDdList.SelectedValue);
            //    if (txnCounter > 0)
            //    {
            //        gridData = GetFilteredDataFromBulkDataByfilterExpression(gridData, "Currency", CurrencyDdList.SelectedValue);
            //        BindGridWithFilteredData(gridData);
            //    }
            //    else
            //    {
            //        gridData = new DataTable();
            //        BindGridWithFilteredData(gridData);
            //    }
            //}
            //else
            //{
            //    gridData = new DataTable();
            //    BindGridWithFilteredData(gridData);
            //}
        }

        //#region Dynamic Functions For Dynamic Filtering on Front End

        //private void BindGridWithFilteredData(DataTable filteredData)
        //{
        //    dv = filteredData.DefaultView;
        //    dtgTransactionSentToOwnBank.DataSource = dv;
        //    dtgTransactionSentToOwnBank.DataBind();
        //    //if (filteredData.Rows.Count > 0)
        //    //{
        //    //    lblTotalItem.Text = "Total Item : " + filteredData.Rows.Count.ToString();
        //    //    lblTotalAmount.Text = "Total Amount : " + decimal.Parse(filteredData.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
        //    //}
        //    //else
        //    //{
        //    //    lblTotalItem.Text = string.Empty;
        //    //    lblTotalAmount.Text = string.Empty;
        //    //}
        //}

        //private int GetCurrencyCount(DataTable gridData, string currency)
        //{
        //    int curCount = 0;
        //    foreach (DataRow row in gridData.Rows)
        //    {
        //        if (row["Currency"].ToString().Equals(currency))
        //        {
        //            curCount++;
        //        }
        //    }
        //    return curCount;
        //}
        //public DataTable GetFilteredDataFromBulkDataByfilterExpression(DataTable bulkData, string columnName, string filterExpression)
        //{
        //    DataTable filteredData = new DataTable();
        //    return filteredData = bulkData.Select(columnName + "='" + filterExpression + "'").CopyToDataTable() ?? bulkData;
        //}

        //#endregion
    }
}
