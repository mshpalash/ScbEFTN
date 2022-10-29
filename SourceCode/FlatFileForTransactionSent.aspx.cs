using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EFTN.Utility;
using EFTN.component;

namespace EFTN
{
    public partial class FlatFileForTransactionSent : System.Web.UI.Page
    {
        private static DataTable myDataTable = new DataTable();
        private static DataTable myDataTableSTS = new DataTable();
        private static DataTable myDataTableSTDO = new DataTable();
        //private EFTN.BLL.FinacleManager fm;
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private static string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        private bool firstTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSessionDropdownlist();
                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("115"))
                {
                    Response.Redirect("~/FlatFileForTransactionSentToHUB.aspx");
                }
                else if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("225"))
                {
                    Response.Redirect("~/FlatFileForTransactionSentCity.aspx");
                }

                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();

                //if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("225"))
                //{
                //    if (fm == null)
                //    {
                //        fm = new EFTN.BLL.FinacleManager();
                //    }
                //    if (!fm.IsConnected)
                //    {
                //        fm.Connect();
                //    }

                //    firstTime = true;
                //}
            }
        }

        void Page_Unload(Object sender, EventArgs e)
        {
            //if (!IsPostBack && fm != null && fm.IsConnected)
            //{
            //    if (firstTime)
            //    {
            //        firstTime = false;
            //    }
            //    else
            //    {
            //        fm.Disconnect();
            //    }
            //}
        }

        private void BindSessionDropdownlist()
        {
            DataTable dropDownData = new DataTable();
            dropDownData.Columns.Add("SessionID");
            DataRow row = dropDownData.NewRow();
            row["SessionID"] = "0";
            dropDownData.Rows.Add(row);
            dropDownData.Merge(sentBatchDB.GetSessions(eftConString));
            SessionDdList.DataSource = dropDownData;
            SessionDdList.DataTextField = "SessionID";
            SessionDdList.DataValueField = "SessionID";
            SessionDdList.DataBind();
        }
        private void BindData()
        {
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            int txnCounter = 0;
            if (SelectedBank.Equals("215"))
            {
                EFTN.component.CSVReportDB cSVReportDB = new EFTN.component.CSVReportDB();
                myDataTable = cSVReportDB.EFTGetBatchesForTransactionSentForSCBCSV(
                                                                      ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                    + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                txnCounter = GetCountForSelectedSession(myDataTable, SessionDdList.SelectedValue);
                if (myDataTable.Rows.Count > 0 && txnCounter > 0)
                {
                    myDataTable = GetFilteredDataFromBulkDataByfilterExpression(myDataTable, "SessionID", SessionDdList.SelectedValue);
                    dtgEFTChecker.DataSource = myDataTable;
                }
                else
                {
                    dtgEFTChecker.DataSource = new DataTable();
                }
                try
                {
                    dtgEFTChecker.DataBind();
                }
                catch
                {
                    dtgEFTChecker.CurrentPageIndex = 0;
                    dtgEFTChecker.DataBind();
                }


                myDataTableSTS = cSVReportDB.EFTGetBatchesForTransactionSentForSCBCSVSTS(
                                                      ddlistYear.SelectedValue.PadLeft(4, '0')
                                                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                    + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                txnCounter = GetCountForSelectedSession(myDataTableSTS, SessionDdList.SelectedValue);
                if (myDataTableSTS.Rows.Count > 0 && txnCounter > 0)
                {
                    myDataTableSTS = GetFilteredDataFromBulkDataByfilterExpression(myDataTableSTS, "SessionID", SessionDdList.SelectedValue);
                    dtgTransactionListOfSTS.DataSource = myDataTableSTS;
                }
                else
                {
                    dtgTransactionListOfSTS.DataSource = new DataTable();
                }
                try
                {
                    dtgTransactionListOfSTS.DataBind();
                }
                catch
                {
                    dtgTransactionListOfSTS.CurrentPageIndex = 0;
                    dtgTransactionListOfSTS.DataBind();
                }

                myDataTableSTDO = cSVReportDB.EFTGetBatchesForTransactionSentForSCBCSVSTDO(
                                      ddlistYear.SelectedValue.PadLeft(4, '0')
                                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                    + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                txnCounter = GetCountForSelectedSession(myDataTableSTDO, SessionDdList.SelectedValue);
                if (myDataTableSTDO.Rows.Count > 0 && txnCounter > 0)
                {
                    myDataTableSTDO = GetFilteredDataFromBulkDataByfilterExpression(myDataTableSTDO, "SessionID", SessionDdList.SelectedValue);
                    dtgTransactionListOfSTDOrder.DataSource = myDataTableSTDO;
                }
                else
                {
                    dtgTransactionListOfSTDOrder.DataSource = new DataTable(); 
                }
                try
                {
                    dtgTransactionListOfSTDOrder.DataBind();
                }
                catch
                {
                    dtgTransactionListOfSTDOrder.CurrentPageIndex = 0;
                    dtgTransactionListOfSTDOrder.DataBind();
                }
            }
        }

        protected void dtgEFTChecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTChecker.CurrentPageIndex = e.NewPageIndex;
            dtgEFTChecker.DataSource = myDataTable;
            dtgEFTChecker.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void dtgEFTChecker_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            DataTable dtFlatFile;
            Guid TransactionID = (Guid)dtgEFTChecker.DataKeys[e.Item.ItemIndex];

            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);


            if (e.CommandName == "GenerateFlatFile")
            {
                dtFlatFile = transactionToCBSDB.GetTransactionSentFlatFile(TransactionID, DepartmentID);
                GenerateFlatFile(dtFlatFile);
            }

            if (e.CommandName == "GenerateReverseFlatFile")
            {
                dtFlatFile = transactionToCBSDB.GetTransactionSentFlatFileReverse(TransactionID, DepartmentID);
                GenerateFlatFile(dtFlatFile);
            }

            if (e.CommandName == "GenerateAccumulatedFlatFile")
            {
                dtFlatFile = transactionToCBSDB.GetFlatFileDataForTransactionSentByDistinctAccountNum(TransactionID);
                GenerateFlatFile(dtFlatFile);
            }

            if (e.CommandName == "GenerateReverseAccumulatedFlatFile")
            {
                dtFlatFile = transactionToCBSDB.GetFlatFileDataForTransactionSentReverseByDistinctAccountNum(TransactionID);
                GenerateFlatFile(dtFlatFile);
            }

        }

        private void GetFinacleInsertMessage(Guid TransactionID)
        {
            //ISOMessageDB isoMessageDB = new ISOMessageDB();
            ////DataTable dtFinacleMessage = isoMessageDB.GetISOMessageByTransactionID(TransactionID, ISOMessageStatus.ReverseMessageSent);
            //dtgISOMessageStatus.DataSource = isoMessageDB.GetISOMessageByTransactionID(TransactionID);
            //dtgISOMessageStatus.DataBind();
        }

        private void GenerateFlatFile(DataTable dt)
        {
            EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            string flatfileResult = fc.CreatFlatFileForTransactionSent(dt, DepartmentID);
            string fileName = "CBS" + "-" + "FlatFile-TS" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
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

        protected void linkBtnTransactionSentCharge_Click(object sender, EventArgs e)
        {
            Response.Redirect("FlatFileForTransactionSentCharge.aspx");
        }

        protected void btnExportCSV_Click(object sender, EventArgs e)
        {
            if (myDataTable.Rows.Count > 0)
            {
                string xlsFileName = "DetailedSettlementReport" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
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

        protected void dtgTransactionListOfSTS_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            DataTable dtFlatFile;
            Guid transactionID = new Guid();
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            transactionID = new Guid(dtgTransactionListOfSTS.DataKeys[e.Item.ItemIndex].ToString());
            //int PrintFlag = ParseData.StringToInt(dtgTransactionListOfSTS.DataKeys[e.Item.ItemIndex].ToString());
            DataRow[] row = myDataTableSTS.Select("TransactionID = '" + transactionID.ToString() + "'");
            int PrintFlag = ParseData.StringToInt(row[0].ItemArray[myDataTableSTS.Columns.IndexOf("PrintFlag")].ToString());

            string EffectiveEntryDate = ddlistYear.SelectedValue.PadLeft(4, '0')
                                        + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                        + ddlistDay.SelectedValue.PadLeft(2, '0');

            if (e.CommandName == "GenerateFlatFileSTS")
            {
                dtFlatFile = transactionToCBSDB.GetTransactionSentFlatFileSTS(EffectiveEntryDate, DepartmentID, PrintFlag, transactionID);
                GenerateFlatFile(dtFlatFile);
            }

            if (e.CommandName == "GenerateReverseFlatFileSTS")
            {
                dtFlatFile = transactionToCBSDB.GetTransactionSentFlatFileReverseSTS(EffectiveEntryDate, DepartmentID, PrintFlag, transactionID);
                GenerateFlatFile(dtFlatFile);
            }

            if (e.CommandName == "GenerateFlatFileSTSAccumulated")
            {
                if (transactionID != null)
                {
                    dtFlatFile = transactionToCBSDB.GetTransactionSentFlatFileSTSAccumulated(EffectiveEntryDate, DepartmentID, PrintFlag, transactionID);
                    GenerateFlatFile(dtFlatFile);
                }
            }
            //if (e.CommandName == "GenerateAccumulatedFlatFileSTS")
            //{
            //    dtFlatFile = transactionToCBSDB.GetFlatFileDataForTransactionSentByDistinctAccountNum(TransactionID);
            //    GenerateFlatFile(dtFlatFile);
            //}

            //if (e.CommandName == "GenerateReverseAccumulatedFlatFileSTS")
            //{
            //    dtFlatFile = transactionToCBSDB.GetFlatFileDataForTransactionSentReverseByDistinctAccountNum(TransactionID);
            //    GenerateFlatFile(dtFlatFile);
            //}
        }

        protected void dtgTransactionListOfSTDOrder_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            DataTable dtFlatFile;
            Guid TransactionID = (Guid)dtgTransactionListOfSTDOrder.DataKeys[e.Item.ItemIndex];

            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);


            if (e.CommandName == "GenerateFlatFileSTDOrder")
            {
                dtFlatFile = transactionToCBSDB.GetTransactionSentFlatFile(TransactionID, DepartmentID);
                GenerateFlatFile(dtFlatFile);
            }

            if (e.CommandName == "GenerateReverseFlatFileSTOrder")
            {
                dtFlatFile = transactionToCBSDB.GetTransactionSentFlatFileReverse(TransactionID, DepartmentID);
                GenerateFlatFile(dtFlatFile);
            }

            if (e.CommandName == "GenerateAccumulatedFlatFileSTDOrder")
            {
                dtFlatFile = transactionToCBSDB.GetFlatFileDataForTransactionSentByDistinctAccountNum(TransactionID);
                GenerateFlatFile(dtFlatFile);
            }

            if (e.CommandName == "GenerateReverseAccumulatedFlatFileSTDOrder")
            {
                dtFlatFile = transactionToCBSDB.GetFlatFileDataForTransactionSentReverseByDistinctAccountNum(TransactionID);
                GenerateFlatFile(dtFlatFile);
            }
        }

        protected void linkBtnAccumulateSTDOrderFlatFile_Click(object sender, EventArgs e)
        {
            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            DataTable dtFlatFile;
            //Guid TransactionID = (Guid)dtgTransactionListOfSTDOrder.DataKeys[e.Item.ItemIndex];

            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            dtFlatFile = transactionToCBSDB.GetTransactionSentFlatFileSTDOrderByEffectiveEntryDate(
                                                                    ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                    + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            GenerateFlatFile(dtFlatFile);

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

    }
}
