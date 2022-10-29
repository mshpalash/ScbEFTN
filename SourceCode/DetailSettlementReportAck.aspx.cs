using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI.WebControls;
using EFTN.component;
using EFTN.Utility;


namespace EFTN
{
    public partial class DetailSettlementReportAck : System.Web.UI.Page
    {
        private static DataTable dtSettlementReport = new DataTable();
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');
                sortOrder = "asc";
                BindCurrencyTypeDropdownlist();
                BindSessionDropdownlist();
                SessionDdList.Enabled = true;
            }
        }

        protected void BindCurrencyTypeDropdownlist()
        {
            string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            SentBatchDB sentBatchDB = new SentBatchDB();
            DataTable dropDownData = new DataTable();

            //dropDownData.Columns.Add("Currency");
            //DataRow row = dropDownData.NewRow();
            //row["Currency"] = "ALL";
            //dropDownData.Rows.Add(row);

            //dropDownData = sentBatchDB.GetCurrencyList(eftConString);
            dropDownData.Merge(sentBatchDB.GetCurrencyList(eftConString));
            CurrencyDdList.DataSource = dropDownData;
            CurrencyDdList.DataTextField = "Currency";
            CurrencyDdList.DataValueField = "Currency";
            CurrencyDdList.DataBind();
            CurrencyDdList.SelectedIndex = 0;
        }

        protected void BindSessionDropdownlist()
        {
            string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            SentBatchDB sentBatchDB = new SentBatchDB();
            DataTable dropDownData = new DataTable();

            //dropDownData.Columns.Add("SessionID");
            //DataRow row = dropDownData.NewRow();
            //row["SessionID"] = "ALL";
            //dropDownData.Rows.Add(row);

            //dropDownData = sentBatchDB.GetSessions(eftConString);
            dropDownData.Merge(sentBatchDB.GetSessions(eftConString));
            SessionDdList.DataSource = dropDownData;
            SessionDdList.DataTextField = "SessionID";
            SessionDdList.DataValueField = "SessionID";
            SessionDdList.DataBind();
            SessionDdList.SelectedIndex = 0;
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

        protected void dtgSettlementReport_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = dtSettlementReport.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgSettlementReport.DataSource = dv;
            dtgSettlementReport.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DetailSettlementReportDB detailSettlementReportDB = new DetailSettlementReportDB();



            if (ddListReportType.SelectedValue.Equals("12"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }

                dtSettlementReport = detailSettlementReportDB.GetReportAckForTransactionSentBySettlementDate(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);

                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }



            else if (ddListReportType.SelectedValue.Equals("8412"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }

                dtSettlementReport = detailSettlementReportDB.GetReportNonAckForTransactionSentBySettlementDate(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);

                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }


            else if (ddListReportType.SelectedValue.Equals("112"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportAckForTransactionSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }

            }
            if (ddListReportType.SelectedValue.Equals("212"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportAckForTransactionSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }

            }

            else if (ddListReportType.SelectedValue.Equals("4"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportAckForReturnSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            else if (ddListReportType.SelectedValue.Equals("844"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportNonAckForReturnSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("104"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportAckForReturnSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("204"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportAckForReturnSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            else if (ddListReportType.SelectedValue.Equals("6"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportAckForNOCSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            else if (ddListReportType.SelectedValue.Equals("846"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportNonAckForNOCSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("106"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportAckForNOCSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            else if (ddListReportType.SelectedValue.Equals("206"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportAckForNOCSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }




            else if (ddListReportType.SelectedValue.Equals("2000"))
            {
                Response.Redirect("DetailSettlementReport.aspx");


            }
            dtgSettlementReport.DataBind();
        }

        private DataTable GetData()
        {
            DetailSettlementReportDB detailSettlementReportDB = new DetailSettlementReportDB();


            if (ddListReportType.SelectedValue.Equals("12"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return detailSettlementReportDB.GetReportAckForTransactionSentBySettlementDate(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);

            }

            else if (ddListReportType.SelectedValue.Equals("8412"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return detailSettlementReportDB.GetReportNonAckForTransactionSentBySettlementDate(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);

            }

            else if (ddListReportType.SelectedValue.Equals("112"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return dtSettlementReport = detailSettlementReportDB.GetReportAckForTransactionSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);

            }
            else if (ddListReportType.SelectedValue.Equals("212"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return dtSettlementReport = detailSettlementReportDB.GetReportAckForTransactionSentBySettlementDateForDebit(
                                                                 ddlistYear.SelectedValue.PadLeft(4, '0')
                                                               + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue
                                                              , SessionDdList.SelectedValue);

            }
            else if (ddListReportType.SelectedValue.Equals("4"))
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
                return detailSettlementReportDB.GetReportAckForReturnSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);

            }

            else if (ddListReportType.SelectedValue.Equals("844"))
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
                return detailSettlementReportDB.GetReportNonAckForReturnSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);

            }
            else if (ddListReportType.SelectedValue.Equals("104"))
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
                return detailSettlementReportDB.GetReportAckForReturnSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);

            }
            else if (ddListReportType.SelectedValue.Equals("204"))
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
                return detailSettlementReportDB.GetReportAckForReturnSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, CurrencyDdList.SelectedValue
                                                           , SessionDdList.SelectedValue);

            }

            else if (ddListReportType.SelectedValue.Equals("6"))
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
                return detailSettlementReportDB.GetReportAckForNOCSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }

            else if (ddListReportType.SelectedValue.Equals("846"))
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
                return detailSettlementReportDB.GetReportNonAckForNOCSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("106"))
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
                return detailSettlementReportDB.GetReportAckForNOCSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else
            //if (ddListReportType.SelectedValue.Equals("206"))
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
                return detailSettlementReportDB.GetReportAckForNOCSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }




        }

        protected void dtgSettlementReport_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgSettlementReport.CurrentPageIndex = e.NewPageIndex;
            dtgSettlementReport.DataSource = dtSettlementReport;
            dtgSettlementReport.DataBind();
        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            if (ddListReportType.SelectedValue.Equals("12"))
            {
                string FileName = "SettlementReport-DepartmentWiseOutwardTransactionAck" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }

            else if (ddListReportType.SelectedValue.Equals("8412"))
            {
                string FileName = "SettlementReport-DepartmentWiseOutwardTransactionNonAck" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }


            else if (ddListReportType.SelectedValue.Equals("112"))
            {
                string FileName = "SettlementReport-DepartmentWiseOutwardTransactionCreditAck" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("212"))
            {
                string FileName = "SettlementReport-DepartmentWiseOutwardTransactionDebitAck" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }

            else if (ddListReportType.SelectedValue.Equals("4"))
            {
                string FileName = "SettlementReport-ReturnSentAck" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }

            else if (ddListReportType.SelectedValue.Equals("844"))
            {
                string FileName = "SettlementReport-ReturnSentNonAck" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("104"))
            {
                string FileName = "SettlementReport-ReturnSentCreditAck" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("204"))
            {
                string FileName = "SettlementReport-ReturnSentDebitAck" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }

            else if (ddListReportType.SelectedValue.Equals("6"))
            {
                string FileName = "SettlementReport-NOCSentAck" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("846"))
            {
                string FileName = "SettlementReport-NOCSentNonAck" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("106"))
            {
                string FileName = "SettlementReport-NOCSentCreditAck" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("206"))
            {
                string FileName = "SettlementReport-NOCSentDebitAck" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }


        }

        private void PrintPDF(string FileName)
        {
            DataTable dt = GetData();

            if (dt.Rows.Count == 0)
            {
                return;
            }

            Response.ClearContent();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

            Document document = new Document(PageSize.A4.Rotate(), 10, 10, 8, 8);
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

            Font fnt = new Font(Font.HELVETICA, 8);
            Font fntblue = new Font(Font.HELVETICA, 8);
            fntblue.Color = new Color(0, 0, 255);
            Font fntbld = new Font(Font.HELVETICA, 8);
            fntbld.SetStyle(Font.BOLD);
            Font headerFont = new Font(Font.HELVETICA, 15);

            string spacer = "            -              ";

            string str = spacer;
            str = str + ConfigurationManager.AppSettings["OriginBankName"] + " All Rights Reserved" + spacer;
            str = str + "Confidential: internal use only" + spacer;
            str = str + "Powered By Flora Limited";

            HeaderFooter footer = new HeaderFooter(new Phrase(str, fnt), false);
            footer.Alignment = Element.ALIGN_CENTER;

            document.Footer = footer;
            document.Open();

            string LogoImage = Server.MapPath("images") + "\\SCBLogo.JPG";

            iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
            jpeg.Alignment = Element.ALIGN_RIGHT;


            PdfPCell logo = new PdfPCell();
            logo.BorderWidth = 0;
            logo.Colspan = 2;
            logo.AddElement(jpeg);

            ////////////////////////////////
            iTextSharp.text.pdf.PdfPTable headertable = new iTextSharp.text.pdf.PdfPTable(3);
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            headertable.DefaultCell.Padding = 0;
            headertable.WidthPercentage = 99;
            headertable.DefaultCell.Border = 0;
            float[] widthsAtHeader = { 40, 40, 20 };
            headertable.SetWidths(widthsAtHeader);


            if (ddListReportType.SelectedValue.Equals("12"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Departmentwise Outward Transaction Report Ack: " + settlementDate, headerFont));


            }


            else if (ddListReportType.SelectedValue.Equals("8412"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Departmentwise Outward Transaction Report Non Ack: " + settlementDate, headerFont));


            }



            else if (ddListReportType.SelectedValue.Equals("112"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Departmentwise Outward Transaction Credit Ack: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("212"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Departmentwise Outward Transaction Debit Ack: " + settlementDate, headerFont));

            }

            else if (ddListReportType.SelectedValue.Equals("4"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Return Sent Report Ack: " + settlementDate, headerFont));

            }

            else if (ddListReportType.SelectedValue.Equals("844"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Return Sent Report Non Ack: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("104"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Return Sent Credit Report Ack: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("204"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Return Sent Debit Report Ack: " + settlementDate, headerFont));

            }

            else if (ddListReportType.SelectedValue.Equals("6"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("NOC Sent Report Ack: " + settlementDate, headerFont));

            }

            else if (ddListReportType.SelectedValue.Equals("846"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("NOC Sent Report Non Ack: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("106"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("NOC Sent Credit Report Ack: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("206"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("NOC Sent Debit Report Ack: " + settlementDate, headerFont));

            }







            headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), headerFont));
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            //string SelectedBank = string.Empty;

            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            float[] headerwidths;
            int NumberOfPdfColumn = 0;
            if (SelectedBank.Equals("135"))
            {
                headerwidths = new float[] { 6, 10, 10, 4, 8, 4, 8, 6, 8, 8, 8, 8, 12, 8 };
                NumberOfPdfColumn = 14;
            }
            else
            {
                headerwidths = new float[] { 11, 11, 4, 10, 8, 8, 6, 10, 8, 8, 8, 12, 8 };
                NumberOfPdfColumn = 13;
            }

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(NumberOfPdfColumn);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            //float[] headerwidths = { 10, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };
            //float[] headerwidths = { 10, 10, 4, 8, 4, 8, 8, 8, 8, 8, 8, 8 };//Only For JANATA BANK
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0;
            if (SelectedBank.Equals("135"))
            {
                c0 = new PdfPCell(new iTextSharp.text.Phrase("Zone Name", fnt));
            }
            else
            {
                c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            }

            //PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;

            datatable.AddCell(c0);

            if (SelectedBank.Equals("135"))
            {
                datatable.AddCell(new iTextSharp.text.Phrase("Bank Name", fnt)); //only for JANATA BANK
            }
            datatable.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("SECC", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Trace No.", fnt));
            if (SelectedBank.Equals("185"))
            {
                datatable.AddCell(new iTextSharp.text.Phrase("Sending Br.", fnt));
            }
            else
            {
                datatable.AddCell(new iTextSharp.text.Phrase("Tran. Code", fnt));
            }
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Bank RoutNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Sender A/C", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Receiver /Payer Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("C.Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Ent.Desc", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("RejectReason", fnt));


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (SelectedBank.Equals("135"))
                {
                    PdfPCell cZone = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["ZoneName"], fnt));
                    cZone.BorderWidth = 0.5f;
                    cZone.HorizontalAlignment = Cell.ALIGN_LEFT;
                    cZone.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    cZone.Padding = 4;
                    datatable.AddCell(cZone);
                }

                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankName"], fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BranchName"], fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 4;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["SECC"], fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TraceNumber"], fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);

                if (SelectedBank.Equals("185"))
                {
                    datatable.AddCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                }
                else
                {
                    datatable.AddCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                }

                //PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                //c5.BorderWidth = 0.5f;
                //c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                //c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //c5.Padding = 4;
                //datatable.AddCell(c5);

                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["DFIAccountNo"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 4;
                datatable.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankRoutingNo"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dt.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["AccountNo"], fnt));
                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_LEFT;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 4;
                datatable.AddCell(c9);

                PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["ReceiverName"], fnt));
                c10.BorderWidth = 0.5f;
                c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c10.Padding = 4;
                datatable.AddCell(c10);

                PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["CompanyName"], fnt));
                c11.BorderWidth = 0.5f;
                c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c11.Padding = 4;
                datatable.AddCell(c11);

                PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["EntryDesc"], fnt));
                c12.BorderWidth = 0.5f;
                c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c12.Padding = 4;
                datatable.AddCell(c12);

                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["RejectReason"], fnt));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_LEFT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable.AddCell(c13);

            }

            //-------------TOTAL IN FOOTER --------------------
            if (SelectedBank.Equals("135"))
            {
                datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            }
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(TraceNumber)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //-------------END TOTAL -------------------------



            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }

        private DataTable GetDataBranchWise(int branchID)
        {
            DetailSettlementReportDB detailSettlementReportDB = new DetailSettlementReportDB();


            return new DataTable();


        }

        public DataTable GetBranches()
        {
            DetailSettlementReportDB detailSettlementReportDB = new DetailSettlementReportDB();
            return new DataTable();
        }

        private void PrintPDFBranchWise(string FileName)
        {
            DataTable dtBranch = GetBranches();

            if (dtBranch.Rows.Count == 0)
            {
                return;
            }

            Response.ClearContent();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

            Document document = new Document(PageSize.A4.Rotate(), 10, 10, 8, 8);
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

            Font fnt = new Font(Font.HELVETICA, 8);
            Font fntblue = new Font(Font.HELVETICA, 8);
            fntblue.Color = new Color(0, 0, 255);
            Font fntbld = new Font(Font.HELVETICA, 8);
            fntbld.SetStyle(Font.BOLD);
            Font headerFont = new Font(Font.HELVETICA, 15);

            string spacer = "            -              ";

            string str = spacer;
            str = str + ConfigurationManager.AppSettings["OriginBankName"] + " All Rights Reserved" + spacer;
            str = str + "Confidential: internal use only" + spacer;
            str = str + "Powered By Flora Limited";

            HeaderFooter footer = new HeaderFooter(new Phrase(str, fnt), false);
            footer.Alignment = Element.ALIGN_CENTER;

            document.Footer = footer;
            document.Open();

            double allBranchesTotalAmount = 0;
            int allBranchesTotalItem = 0;
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            for (int brRow = 0; brRow < dtBranch.Rows.Count; brRow++)
            {
                string LogoImage = Server.MapPath("images") + "\\SCBLogo.JPG";
                DataTable dt = GetDataBranchWise(ParseData.StringToInt(dtBranch.Rows[brRow]["BranchID"].ToString()));

                iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
                jpeg.Alignment = Element.ALIGN_RIGHT;


                PdfPCell logo = new PdfPCell();
                logo.BorderWidth = 0;
                logo.Colspan = 2;
                logo.AddElement(jpeg);

                ////////////////////////////////
                iTextSharp.text.pdf.PdfPTable headertable = new iTextSharp.text.pdf.PdfPTable(3);
                headertable.DefaultCell.Border = Rectangle.NO_BORDER;
                headertable.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
                headertable.DefaultCell.Padding = 0;
                headertable.WidthPercentage = 99;
                headertable.DefaultCell.Border = 0;
                float[] widthsAtHeader = { 50, 30, 20 };
                headertable.SetWidths(widthsAtHeader);


                document.Add(new iTextSharp.text.Paragraph(" "));
                iTextSharp.text.pdf.PdfPTable datatable2 = new iTextSharp.text.pdf.PdfPTable(13);
                datatable2.DefaultCell.Padding = 3;
                datatable2.DefaultCell.Border = 2;
                datatable2.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //float[] headerwidths2 = { 15,35,20,15,15};
                float[] headerwidths2 = { 10, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 10 };
                datatable2.DefaultCell.BorderWidth = 2;
                datatable2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                datatable2.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;


                headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
                headertable.AddCell(logo); ;
                document.Add(headertable);

                document.Add(new iTextSharp.text.Paragraph(" "));
                iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(13);
                datatable.DefaultCell.Padding = 4;
                datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                float[] headerwidths = { 10, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 10 };
                datatable.SetWidths(headerwidths);
                datatable.WidthPercentage = 99;

                iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                datatable.DefaultCell.BorderWidth = 0.5f;
                datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);

                //datatable2.HeaderRows = 1;
                //datatable2.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
                //datatable2.SetWidths(headerwidths);
                ////datatable2.DefaultCell.BorderWidth = 0.25f;
                //datatable.WidthPercentage = 99;
                //------------------------------------------

                //PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
                //c0.BorderWidth = 0.5f;
                //c0.HorizontalAlignment = Cell.ALIGN_LEFT;
                //c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
                //c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //c0.Padding = 4;

                //datatable.AddCell(c0);


                datatable2.AddCell(new iTextSharp.text.Phrase("Bank Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("SECC", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("TraceNo", fnt));
                if (SelectedBank.Equals("185"))
                {
                    datatable2.AddCell(new iTextSharp.text.Phrase("Sending Br.", fnt));
                }
                else
                {
                    datatable2.AddCell(new iTextSharp.text.Phrase("Tran. Code", fnt));
                }
                //datatable2.AddCell(new iTextSharp.text.Phrase("TranCode", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("DFIAccNo", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("BankRoutNo", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Receiver /Payer Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Company Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Ent.Desc", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("RejectReason", fnt));


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankName"], fnt));
                    c1.BorderWidth = 0.5f;
                    c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c1.Padding = 1;
                    datatable2.AddCell(c1);

                    PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BranchName"], fnt));
                    c2.BorderWidth = 0.5f;
                    c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c2.Padding = 4;
                    datatable2.AddCell(c2);

                    PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["SECC"], fnt));
                    c3.BorderWidth = 0.5f;
                    c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c3.Padding = 4;
                    datatable2.AddCell(c3);

                    PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TraceNumber"], fnt));
                    c4.BorderWidth = 0.5f;
                    c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c4.Padding = 4;
                    datatable2.AddCell(c4);

                    if (SelectedBank.Equals("185"))
                    {
                        datatable2.AddCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["SendingBranchName"], fnt));
                    }
                    else
                    {
                        datatable2.AddCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                    }


                    //PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                    //c5.BorderWidth = 0.5f;
                    //c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    //c5.Padding = 4;
                    //datatable2.AddCell(c5);

                    PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["DFIAccountNo"], fnt));
                    c6.BorderWidth = 0.5f;
                    c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c6.Padding = 4;
                    datatable2.AddCell(c6);

                    PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankRoutingNo"], fnt));
                    c7.BorderWidth = 0.5f;
                    c7.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c7.Padding = 4;
                    datatable2.AddCell(c7);


                    PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dt.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                    c8.BorderWidth = 0.5f;
                    c8.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c8.Padding = 4;
                    datatable2.AddCell(c8);

                    PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["IdNumber"], fnt));
                    c9.BorderWidth = 0.5f;
                    c9.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c9.Padding = 4;
                    datatable2.AddCell(c9);

                    PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["ReceiverName"], fnt));
                    c10.BorderWidth = 0.5f;
                    c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c10.Padding = 4;
                    datatable2.AddCell(c10);

                    PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["CompanyName"], fnt));
                    c11.BorderWidth = 0.5f;
                    c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c11.Padding = 4;
                    datatable2.AddCell(c11);

                    PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["EntryDesc"], fnt));
                    c12.BorderWidth = 0.5f;
                    c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c12.Padding = 4;
                    datatable2.AddCell(c12);

                    PdfPCell c14 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["RejectReason"], fnt));
                    c14.BorderWidth = 0.5f;
                    c14.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c14.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c14.Padding = 4;
                    datatable2.AddCell(c14);
                }

                //-------------TOTAL IN FOOTER --------------------
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(TraceNumber)", "").ToString(), fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));

                allBranchesTotalItem += dt.Rows.Count;
                allBranchesTotalAmount = allBranchesTotalAmount + ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString());

                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_RIGHT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable2.AddCell(c13);
                //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));
                //datatable2.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                //-------------END TOTAL -------------------------
                /////////////////////////////////
                document.Add(datatable);
                document.Add(datatable2);
                document.NewPage();
            }


            string strTotalTransactionInfo = "Total Item : " + allBranchesTotalItem.ToString() + "  -- Total Amount : " + allBranchesTotalAmount.ToString();
            iTextSharp.text.pdf.PdfPTable datatable5 = new iTextSharp.text.pdf.PdfPTable(1);
            datatable5.DefaultCell.Padding = 3;
            datatable5.DefaultCell.Border = 2;
            datatable5.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            //float[] headerwidths2 = { 15,35,20,15,15};
            datatable5.DefaultCell.BorderWidth = 0;
            datatable5.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            datatable5.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

            Font fntTotalTran = new Font(Font.HELVETICA, 16);
            fntTotalTran.SetStyle(Font.BOLD);

            datatable5.AddCell(new iTextSharp.text.Phrase(strTotalTransactionInfo, fntTotalTran));

            document.Add(datatable5);

            document.Close();
            Response.End();
        }

        protected void ExpotToExcelbtn_Click(object sender, EventArgs e)
        {
            DataTable dt = GetData();

            if (dt.Rows.Count > 0)
            {
                string xlsFileName = "DetailedSettlementReportAck" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                string attachment = "attachment; filename=" + xlsFileName + ".csv";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.csv";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                // Create the CSV file to which grid data will be exported. 
                //StreamWriter sw = new StreamWriter();
                int iColCount = dt.Columns.Count;

                // First we will write the headers. 

                for (int i = 0; i < iColCount; i++)
                {
                    sw.Write("\"");
                    sw.Write(dt.Columns[i]);
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
                foreach (DataRow dr in dt.Rows)
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

        private DataTable GetDataDepartmentWise(int DepartmentID)
        {
            DetailSettlementReportDB detailSettlementReportDB = new DetailSettlementReportDB();
            //DepartmentID = 0;
            //if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            //{
            //    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            //}
            return new DataTable();
        }

        public DataTable GetDepartment()
        {
            DetailSettlementReportDB detailSettlementReportDB = new DetailSettlementReportDB();

            return new DataTable();
        }

        private void PrintPDFDepartmentwise(string FileName)
        {
            DataTable dtDepartment = GetDepartment();

            if (dtDepartment.Rows.Count == 0)
            {
                return;
            }

            Response.ClearContent();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

            Document document = new Document(PageSize.A4.Rotate(), 10, 10, 8, 8);
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

            Font fnt = new Font(Font.HELVETICA, 8);
            Font fntblue = new Font(Font.HELVETICA, 8);
            fntblue.Color = new Color(0, 0, 255);
            Font fntbld = new Font(Font.HELVETICA, 8);
            fntbld.SetStyle(Font.BOLD);
            Font headerFont = new Font(Font.HELVETICA, 15);

            string spacer = "            -              ";

            string str = spacer;
            str = str + ConfigurationManager.AppSettings["OriginBankName"] + " All Rights Reserved" + spacer;
            str = str + "Confidential: internal use only" + spacer;
            str = str + "Powered By Flora Limited";

            HeaderFooter footer = new HeaderFooter(new Phrase(str, fnt), false);
            footer.Alignment = Element.ALIGN_CENTER;

            document.Footer = footer;
            document.Open();
            double allDepartmentsTotalAmount = 0;
            int allDepartmentsTotalItem = 0;

            for (int brRow = 0; brRow < dtDepartment.Rows.Count; brRow++)
            {
                string LogoImage = Server.MapPath("images") + "\\SCBLogo.JPG";
                DataTable dt = GetDataDepartmentWise(ParseData.StringToInt(dtDepartment.Rows[brRow]["DepartmentID"].ToString()));

                iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
                jpeg.Alignment = Element.ALIGN_RIGHT;


                PdfPCell logo = new PdfPCell();
                logo.BorderWidth = 0;
                logo.Colspan = 2;
                logo.AddElement(jpeg);

                ////////////////////////////////
                iTextSharp.text.pdf.PdfPTable headertable = new iTextSharp.text.pdf.PdfPTable(3);
                headertable.DefaultCell.Border = Rectangle.NO_BORDER;
                headertable.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
                headertable.DefaultCell.Padding = 0;
                headertable.WidthPercentage = 99;
                headertable.DefaultCell.Border = 0;
                float[] widthsAtHeader = { 50, 30, 20 };
                headertable.SetWidths(widthsAtHeader);


                document.Add(new iTextSharp.text.Paragraph(" "));
                iTextSharp.text.pdf.PdfPTable datatable2 = new iTextSharp.text.pdf.PdfPTable(13);
                datatable2.DefaultCell.Padding = 3;
                datatable2.DefaultCell.Border = 2;
                datatable2.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //float[] headerwidths2 = { 15,35,20,15,15};
                float[] headerwidths2 = { 10, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 10 };
                datatable2.DefaultCell.BorderWidth = 2;
                datatable2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                datatable2.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

                string nameOfDept = "Department-" + dtDepartment.Rows[brRow]["DepartmentName"].ToString();


                headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
                headertable.AddCell(logo); ;
                document.Add(headertable);

                document.Add(new iTextSharp.text.Paragraph(" "));
                iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(13);
                datatable.DefaultCell.Padding = 4;
                datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                float[] headerwidths = { 10, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 10 };
                datatable.SetWidths(headerwidths);
                datatable.WidthPercentage = 99;

                iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                datatable.DefaultCell.BorderWidth = 0.5f;
                datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);



                datatable2.AddCell(new iTextSharp.text.Phrase("Bank Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Department Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("SECC", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("TraceNo", fnt));
                if (SelectedBank.Equals("185"))
                {
                    datatable2.AddCell(new iTextSharp.text.Phrase("Sending Br.", fnt));
                }
                else
                {
                    datatable2.AddCell(new iTextSharp.text.Phrase("Tran. Code", fnt));
                }
                datatable2.AddCell(new iTextSharp.text.Phrase("TranCode", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("DFIAccNo", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("BankRoutNo", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Receiver /Payer Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Company Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Ent.Desc", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("RejectReason", fnt));



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankName"], fnt));
                    c1.BorderWidth = 0.5f;
                    c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c1.Padding = 1;
                    datatable2.AddCell(c1);

                    PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["DepartmentName"], fnt));
                    c2.BorderWidth = 0.5f;
                    c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c2.Padding = 4;
                    datatable2.AddCell(c2);

                    PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["SECC"], fnt));
                    c3.BorderWidth = 0.5f;
                    c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c3.Padding = 4;
                    datatable2.AddCell(c3);

                    PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TraceNumber"], fnt));
                    c4.BorderWidth = 0.5f;
                    c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c4.Padding = 4;
                    datatable2.AddCell(c4);

                    if (SelectedBank.Equals("185"))
                    {
                        datatable2.AddCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["SendingBranchName"], fnt));
                    }
                    else
                    {
                        datatable2.AddCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                    }

                    //PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                    //c5.BorderWidth = 0.5f;
                    //c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    //c5.Padding = 4;
                    //datatable2.AddCell(c5);

                    PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["DFIAccountNo"], fnt));
                    c6.BorderWidth = 0.5f;
                    c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c6.Padding = 4;
                    datatable2.AddCell(c6);

                    PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankRoutingNo"], fnt));
                    c7.BorderWidth = 0.5f;
                    c7.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c7.Padding = 4;
                    datatable2.AddCell(c7);


                    PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dt.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                    c8.BorderWidth = 0.5f;
                    c8.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c8.Padding = 4;
                    datatable2.AddCell(c8);

                    PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["IdNumber"], fnt));
                    c9.BorderWidth = 0.5f;
                    c9.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c9.Padding = 4;
                    datatable2.AddCell(c9);

                    PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["ReceiverName"], fnt));
                    c10.BorderWidth = 0.5f;
                    c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c10.Padding = 4;
                    datatable2.AddCell(c10);

                    PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["CompanyName"], fnt));
                    c11.BorderWidth = 0.5f;
                    c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c11.Padding = 4;
                    datatable2.AddCell(c11);

                    PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["EntryDesc"], fnt));
                    c12.BorderWidth = 0.5f;
                    c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c12.Padding = 4;
                    datatable2.AddCell(c12);

                    PdfPCell c14 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["RejectReason"], fnt));
                    c14.BorderWidth = 0.5f;
                    c14.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c14.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c14.Padding = 4;
                    datatable2.AddCell(c14);
                }

                //-------------TOTAL IN FOOTER --------------------
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(TraceNumber)", "").ToString(), fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));

                allDepartmentsTotalItem += dt.Rows.Count;
                allDepartmentsTotalAmount = allDepartmentsTotalAmount + ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString());

                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_RIGHT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable2.AddCell(c13);
                //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));
                //datatable2.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                //-------------END TOTAL -------------------------
                /////////////////////////////////
                document.Add(datatable);
                document.Add(datatable2);
                if (brRow < (dtDepartment.Rows.Count - 1))
                {
                    document.NewPage();
                }
            }

            string strTotalTransactionInfo = "Total Item : " + allDepartmentsTotalItem.ToString() + "  -- Total Amount : " + allDepartmentsTotalAmount.ToString();
            iTextSharp.text.pdf.PdfPTable datatable5 = new iTextSharp.text.pdf.PdfPTable(1);
            datatable5.DefaultCell.Padding = 3;
            datatable5.DefaultCell.Border = 2;
            datatable5.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            //float[] headerwidths2 = { 15,35,20,15,15};
            datatable5.DefaultCell.BorderWidth = 0;
            datatable5.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            datatable5.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

            Font fntTotalTran = new Font(Font.HELVETICA, 16);
            fntTotalTran.SetStyle(Font.BOLD);

            datatable5.AddCell(new iTextSharp.text.Phrase(strTotalTransactionInfo, fntTotalTran));

            document.Add(datatable5);

            document.Close();
            Response.End();
        }

        protected void ddListReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ddListReportType.SelectedValue.Equals("8412")) ||  (ddListReportType.SelectedValue.Equals("844")))
            {
                SessionDdList.Enabled = false;
                SessionDdList.BackColor = System.Drawing.Color.Gray;
            }
            else
            {
                SessionDdList.Enabled = true;
                SessionDdList.BackColor = System.Drawing.Color.White ;
            }
        }
    }
}
