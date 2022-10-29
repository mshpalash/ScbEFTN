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
    public partial class DetailSettlementReportViewer : System.Web.UI.Page
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
            }
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
            string currency =string.Empty;
            string session = string.Empty;

            if (ddListReportType.SelectedValue.Equals("1"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID,"","");
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }

            }
            if (ddListReportType.SelectedValue.Equals("101"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, currency,session);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }

            }
            if (ddListReportType.SelectedValue.Equals("201"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }

            }
            else if (ddListReportType.SelectedValue.Equals("2"))
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

                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDate(
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
            else if (ddListReportType.SelectedValue.Equals("102"))
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

                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateForCredit(
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

            else if (ddListReportType.SelectedValue.Equals("202"))
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

                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateForDebit(
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
            else if (ddListReportType.SelectedValue.Equals("3"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateApprovedOnly(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, "ALL"
                                                            , "ALL");
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            else if (ddListReportType.SelectedValue.Equals("103"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionReceviedBySettlementDateApprovedOnlyForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, "ALL", "ALL");
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            else if (ddListReportType.SelectedValue.Equals("203"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateApprovedOnlyForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, "ALL", "ALL");
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
                dtSettlementReport = detailSettlementReportDB.GetReportForReturnSentBySettlementDate(
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
                dtSettlementReport = detailSettlementReportDB.GetReportForReturnSentBySettlementDateForCredit(
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
                dtSettlementReport = detailSettlementReportDB.GetReportForReturnSentBySettlementDateForDebit(
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

            else if (ddListReportType.SelectedValue.Equals("5"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForReturnReceivedBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            else if (ddListReportType.SelectedValue.Equals("105"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForReturnReceivedBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            else if (ddListReportType.SelectedValue.Equals("205"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForReturnReceivedBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
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
                dtSettlementReport = detailSettlementReportDB.GetReportForNOCSentBySettlementDate(
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
                dtSettlementReport = detailSettlementReportDB.GetReportForNOCSentBySettlementDateForCredit(
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
                dtSettlementReport = detailSettlementReportDB.GetReportForNOCSentBySettlementDateForDebit(
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

            else if (ddListReportType.SelectedValue.Equals("7"))
            {
                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateApprovedByDefault(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), "ALL", "ALL");
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("107"))
            {
                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateApprovedByDefaultForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), "ALL", "ALL");
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("207"))
            {
                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateApprovedByDefaultForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), "ALL", "ALL");
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("8"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForNOCReceivedBySettlementDate(
                                                            ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            else if (ddListReportType.SelectedValue.Equals("108"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForNOCReceivedBySettlementDateForCredit(
                                                            ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("208"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForNOCReceivedBySettlementDateForDebit(
                                                            ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("11"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForDishonorSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }

            }
            else if (ddListReportType.SelectedValue.Equals("113"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForDishonorSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("213"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForDishonorSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("12"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }

                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionSentBySettlementDate(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID,"","");

                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("13"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForReturnReceivedBySettlementDate(
                                                               ddlistYear.SelectedValue.PadLeft(4, '0')
                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("111"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForReturnReceivedBySettlementDateForCredit(
                                                                ddlistYear.SelectedValue.PadLeft(4, '0')
                                                              + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                              + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("211"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReportForReturnReceivedBySettlementDateForDebit(
                                                               ddlistYear.SelectedValue.PadLeft(4, '0')
                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
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
                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, currency,session);
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
                dtSettlementReport = detailSettlementReportDB.GetReportForTransactionSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }

            }
            else if (ddListReportType.SelectedValue.Equals("20"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetRNOCSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            else if (ddListReportType.SelectedValue.Equals("120"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetDepartmentwiseRNOCSentCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("220"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetDepartmentwiseRNOCSentDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("21"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReceivedContestedBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("121"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReceivedCreditContestedBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("221"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dtSettlementReport = detailSettlementReportDB.GetReceivedDebitContestedBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                if (dtSettlementReport.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtSettlementReport;
                    dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (ddListReportType.SelectedValue.Equals("22"))
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

                dtSettlementReport = detailSettlementReportDB.GetReportInwardDishonorBySettlementDate(
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
            else if (ddListReportType.SelectedValue.Equals("122"))
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

                dtSettlementReport = detailSettlementReportDB.GetReportCreditInwardDishonorBySettlementDate(
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
            else if (ddListReportType.SelectedValue.Equals("222"))
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

                dtSettlementReport = detailSettlementReportDB.GetReportDebitInwardDishonorBySettlementDate(
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
            else if (ddListReportType.SelectedValue.Equals("23"))
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

                dtSettlementReport = detailSettlementReportDB.GetReportInwardRNOCBySettlementDate(
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
            else if (ddListReportType.SelectedValue.Equals("123"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportCreditInwardRNOCBySettlementDate(
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
            else if (ddListReportType.SelectedValue.Equals("223"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportDebitInwardRNOCBySettlementDate(
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
            else if (ddListReportType.SelectedValue.Equals("24"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportSentContestedBySettlementDate(
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
            else if (ddListReportType.SelectedValue.Equals("124"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportCreditSentContestedBySettlementDate(
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
            else if (ddListReportType.SelectedValue.Equals("224"))
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
                dtSettlementReport = detailSettlementReportDB.GetReportDebitSentContestedBySettlementDate(
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
            string currency =string.Empty;
            string session = string.Empty;
            if (ddListReportType.SelectedValue.Equals("1"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return detailSettlementReportDB.GetReportForTransactionSentBySettlementDate(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID,"","");

            }
            if (ddListReportType.SelectedValue.Equals("101"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return detailSettlementReportDB.GetReportForTransactionSentBySettlementDateForCredit(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID,currency,session);

            }
            if (ddListReportType.SelectedValue.Equals("201"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return detailSettlementReportDB.GetReportForTransactionSentBySettlementDateForDebit(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            else if (ddListReportType.SelectedValue.Equals("2"))
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

                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("102"))
            {
                int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                int BranchID = 0;

                if (DepartmentID == 0)
                {
                    BranchID = 0;
                }
                else if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
                {
                    BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);
                }

                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("202"))
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

                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("3"))
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
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateApprovedOnly(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID,"ALL","ALL");

            }
            else if (ddListReportType.SelectedValue.Equals("103"))
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
                return detailSettlementReportDB.GetReportForTransactionReceviedBySettlementDateApprovedOnlyForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, "ALL", "ALL");

            }
            else if (ddListReportType.SelectedValue.Equals("203"))
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
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateApprovedOnlyForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, "ALL", "ALL");

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
                return detailSettlementReportDB.GetReportForReturnSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

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
                return detailSettlementReportDB.GetReportForReturnSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

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
                return detailSettlementReportDB.GetReportForReturnSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("5"))
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
                return detailSettlementReportDB.GetReportForReturnReceivedBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }

            else if (ddListReportType.SelectedValue.Equals("105"))
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
                return detailSettlementReportDB.GetReportForReturnReceivedBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("205"))
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
                return detailSettlementReportDB.GetReportForReturnReceivedBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

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
                return detailSettlementReportDB.GetReportForNOCSentBySettlementDate(
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
                return detailSettlementReportDB.GetReportForNOCSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

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
                return detailSettlementReportDB.GetReportForNOCSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }

            else if (ddListReportType.SelectedValue.Equals("8"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return detailSettlementReportDB.GetReportForNOCReceivedBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            else if (ddListReportType.SelectedValue.Equals("108"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return detailSettlementReportDB.GetReportForNOCReceivedBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            else if (ddListReportType.SelectedValue.Equals("208"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return detailSettlementReportDB.GetReportForNOCReceivedBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }

            else if (ddListReportType.SelectedValue.Equals("7"))
            {
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateApprovedByDefault(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), "ALL", "ALL");

            }
            else if (ddListReportType.SelectedValue.Equals("107"))
            {
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateApprovedByDefaultForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), "ALL", "ALL");

            }
            else if (ddListReportType.SelectedValue.Equals("11"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return detailSettlementReportDB.GetReportForDishonorSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            else if (ddListReportType.SelectedValue.Equals("113"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return detailSettlementReportDB.GetReportForDishonorSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            else if (ddListReportType.SelectedValue.Equals("213"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return detailSettlementReportDB.GetReportForDishonorSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            else if (ddListReportType.SelectedValue.Equals("12"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return detailSettlementReportDB.GetReportForTransactionSentBySettlementDate(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID,"","");

            }
            else if (ddListReportType.SelectedValue.Equals("13"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return dtSettlementReport = detailSettlementReportDB.GetReportForReturnReceivedBySettlementDate(
                                                               ddlistYear.SelectedValue.PadLeft(4, '0')
                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            else if (ddListReportType.SelectedValue.Equals("111"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return dtSettlementReport = detailSettlementReportDB.GetReportForReturnReceivedBySettlementDateForCredit(
                                                               ddlistYear.SelectedValue.PadLeft(4, '0')
                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("211"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return dtSettlementReport = detailSettlementReportDB.GetReportForReturnReceivedBySettlementDateForDebit(
                                                               ddlistYear.SelectedValue.PadLeft(4, '0')
                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("112"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return dtSettlementReport = detailSettlementReportDB.GetReportForTransactionSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID,currency,session);

            }
            else if (ddListReportType.SelectedValue.Equals("212"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return dtSettlementReport = detailSettlementReportDB.GetReportForTransactionSentBySettlementDateForDebit(
                                                                 ddlistYear.SelectedValue.PadLeft(4, '0')
                                                               + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            else if (ddListReportType.SelectedValue.Equals("20"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return dtSettlementReport = detailSettlementReportDB.GetRNOCSentBySettlementDate(
                                                                 ddlistYear.SelectedValue.PadLeft(4, '0')
                                                               + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("120"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return dtSettlementReport = detailSettlementReportDB.GetDepartmentwiseRNOCSentCredit(
                                                                 ddlistYear.SelectedValue.PadLeft(4, '0')
                                                               + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("220"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return dtSettlementReport = detailSettlementReportDB.GetDepartmentwiseRNOCSentDebit(
                                                                 ddlistYear.SelectedValue.PadLeft(4, '0')
                                                               + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("21"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return dtSettlementReport = detailSettlementReportDB.GetReceivedContestedBySettlementDate(
                                                                 ddlistYear.SelectedValue.PadLeft(4, '0')
                                                               + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("121"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return dtSettlementReport = detailSettlementReportDB.GetReceivedCreditContestedBySettlementDate(
                                                                 ddlistYear.SelectedValue.PadLeft(4, '0')
                                                               + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("221"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return dtSettlementReport = detailSettlementReportDB.GetReceivedDebitContestedBySettlementDate(
                                                                 ddlistYear.SelectedValue.PadLeft(4, '0')
                                                               + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("22"))
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
                return detailSettlementReportDB.GetReportInwardDishonorBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("122"))
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
                return detailSettlementReportDB.GetReportCreditInwardDishonorBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("222"))
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
                return detailSettlementReportDB.GetReportDebitInwardDishonorBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("23"))
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
                return detailSettlementReportDB.GetReportInwardRNOCBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("123"))
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
                return detailSettlementReportDB.GetReportCreditInwardRNOCBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("223"))
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
                return detailSettlementReportDB.GetReportDebitInwardRNOCBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("24"))
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
                return detailSettlementReportDB.GetReportDebitInwardRNOCBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("124"))
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
                return detailSettlementReportDB.GetReportCreditSentContestedBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ddListReportType.SelectedValue.Equals("224"))
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
                return detailSettlementReportDB.GetReportDebitSentContestedBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else //if (ddListReportType.SelectedValue.Equals("7"))
            {
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateApprovedByDefaultForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), "ALL", "ALL");

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
            if (ddListReportType.SelectedValue.Equals("1"))
            {
                string FileName = "SettlementReport-TransactionSent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            if (ddListReportType.SelectedValue.Equals("101"))
            {
                string FileName = "SettlementReport-TransactionSentCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            if (ddListReportType.SelectedValue.Equals("201"))
            {
                string FileName = "SettlementReport-TransactionSentDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            else if (ddListReportType.SelectedValue.Equals("2"))
            {
                string FileName = "SettlementReport-TransactionReceivedAll" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("102"))
            {
                string FileName = "SettlementReport-TransactionReceivedAllCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("202"))
            {
                string FileName = "SettlementReport-TransactionReceivedAllDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("103"))
            {
                string FileName = "SettlementReport-TransactionReceivedApprovedCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("203"))
            {
                string FileName = "SettlementReport-TransactionReceivedApprovedDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("3"))
            {
                string FileName = "SettlementReport-TransactionReceivedApproved" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("4"))
            {
                string FileName = "SettlementReport-ReturnSent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("104"))
            {
                string FileName = "SettlementReport-ReturnSentCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("204"))
            {
                string FileName = "SettlementReport-ReturnSentDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("5"))
            {
                string FileName = "SettlementReport-RetrunReceived" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("105"))
            {
                string FileName = "SettlementReport-RetrunReceivedCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("205"))
            {
                string FileName = "SettlementReport-RetrunReceivedDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("6"))
            {
                string FileName = "SettlementReport-NOCSent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("106"))
            {
                string FileName = "SettlementReport-NOCSentCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("206"))
            {
                string FileName = "SettlementReport-NOCSentDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("8"))
            {
                string FileName = "SettlementReport-NOCReceived" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("108"))
            {
                string FileName = "SettlementReport-NOCReceivedCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("208"))
            {
                string FileName = "SettlementReport-NOCReceivedDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("7"))
            {
                string FileName = "SettlementReport-DefaultApproved" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("107"))
            {
                string FileName = "SettlementReport-DefaultApprovedCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("207"))
            {
                string FileName = "SettlementReport-DefaultApprovedDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("9"))
            {
                string FileName = "SettlementReport-Branchwise-TransactionReceivedAll" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("109"))
            {
                string FileName = "SettlementReport-Branchwise-TransactionReceivedAllCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("209"))
            {
                string FileName = "SettlementReport-Branchwise-TransactionReceivedAllDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("10"))
            {
                string FileName = "SettlementReport-Branchwise-TransactionReceivedApproved" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("110"))
            {
                string FileName = "SettlementReport-Branchwise-TransactionReceivedApprovedCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("210"))
            {
                string FileName = "SettlementReport-Branchwise-TransactionReceivedApprovedDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("11"))
            {
                string FileName = "SettlementReport-OutwardDishonor" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("113"))
            {
                string FileName = "SettlementReport-OutwardDishonorCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("213"))
            {
                string FileName = "SettlementReport-OutwardDishonorDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("12"))
            {
                string FileName = "SettlementReport-DepartmentWiseOutwardTransaction" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }

            else if (ddListReportType.SelectedValue.Equals("112"))
            {
                string FileName = "SettlementReport-DepartmentWiseOutwardTransactionCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("212"))
            {
                string FileName = "SettlementReport-DepartmentWiseOutwardTransactionDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("13"))
            {
                string FileName = "SettlementReport-DepartmentWiseInwardReturn" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("111"))
            {
                string FileName = "SettlementReport-DepartmentWiseInwardReturnCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("211"))
            {
                string FileName = "SettlementReport-DepartmentWiseInwardReturnDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("20"))
            {
                string FileName = "SettlementReport-OutwardRNOC" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("120"))
            {
                string FileName = "SettlementReport-OutwardRNOC-Credit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("220"))
            {
                string FileName = "SettlementReport-OutwardRNOC-Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("21"))
            {
                string FileName = "SettlementReport-OutwardRNOC-Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("121"))
            {
                string FileName = "SettlementReport-OutwardRNOC-Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("221"))
            {
                string FileName = "SettlementReport-OutwardRNOC-Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("22"))
            {
                string FileName = "SettlementReport-InwardContested-Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("122"))
            {
                string FileName = "SettlementReport-InwardContested-Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("222"))
            {
                string FileName = "SettlementReport-InwardContested-Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("23"))
            {
                string FileName = "SettlementReport-InwardRNOC-Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("123"))
            {
                string FileName = "SettlementReport-InwardRNOC-Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("223"))
            {
                string FileName = "SettlementReport-InwardRNOC-Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("24"))
            {
                string FileName = "SettlementReport-SentContested" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("124"))
            {
                string FileName = "SettlementReport-SentContested-Credit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("224"))
            {
                string FileName = "SettlementReport-SentContested-Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }

            else if (ddListReportType.SelectedValue.Equals("25"))
            {
                string FileName = "Department Wise Transaction Sent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("125"))
            {
                string FileName = "Department Wise Transaction Sent Credit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("225"))
            {
                string FileName = "Department Wise Transaction Sent Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("26"))
            {
                string FileName = "Department Wise Inward Return" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("126"))
            {
                string FileName = "Department Wise Inward Return Credit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("226"))
            {
                string FileName = "Department Wise Inward Return Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("27"))
            {
                string FileName = "Department Wise Inward NOC" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("127"))
            {
                string FileName = "Department Wise Inward NOC Credit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("227"))
            {
                string FileName = "Department Wise Inward NOC Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("28"))
            {
                string FileName = "Department Wise Outward RNOC" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("128"))
            {
                string FileName = "Department Wise Outward RNOC Credit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("228"))
            {
                string FileName = "Department Wise Outward RNOC Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("29"))
            {
                string FileName = "Department Wise Outward Dishonor" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("129"))
            {
                string FileName = "Department Wise Outward Dishonor Credit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("229"))
            {
                string FileName = "Department Wise Outward Dishonor Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }

            else if (ddListReportType.SelectedValue.Equals("30"))
            {
                string FileName = "Department Wise Inward Contested" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("130"))
            {
                string FileName = "Department Wise Inward Contested Credit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("230"))
            {
                string FileName = "Department Wise Inward Contested Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("31"))
            {
                string FileName = "Branch Wise Return Sent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("131"))
            {
                string FileName = "Branch Wise Credit Return Sent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("231"))
            {
                string FileName = "Branch Wise Debit Return Sent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("32"))
            {
                string FileName = "Branch Wise NOC Sent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("132"))
            {
                string FileName = "Branch Wise Credit NOC Sent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("232"))
            {
                string FileName = "Branch Wise Debit NOC Sent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("33"))
            {
                string FileName = "Branch Wise Inward RNOC" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("133"))
            {
                string FileName = "Branch Wise Credit Inward RNOC" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("233"))
            {
                string FileName = "Branch Wise Debit Inward RNOC" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("34"))
            {
                string FileName = "Branch Wise Inward Dishonor" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("134"))
            {
                string FileName = "Branch Wise Credit Inward Dishonor" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("234"))
            {
                string FileName = "Branch Wise Debit Inward Dishonor" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("35"))
            {
                string FileName = "Branch Wise Outward Contested" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("135"))
            {
                string FileName = "Branch Wise Credit Outward Contested" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("235"))
            {
                string FileName = "Branch Wise Debit Outward Contested" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
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

            if (ddListReportType.SelectedValue.Equals("1"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Transaction Sent Report: " + settlementDate, headerFont));

            }
            if (ddListReportType.SelectedValue.Equals("101"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Transaction Sent Credit Report: " + settlementDate, headerFont));

            }
            if (ddListReportType.SelectedValue.Equals("201"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Transaction Sent Debit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("2"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Transaction Received All Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("102"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Transaction Received All Credit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("202"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Transaction Received All Debit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("103"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Transaction Received Approved Credit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("203"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Transaction Received Approved Debit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("3"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Transaction Received Approved Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("4"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Return Sent Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("104"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Return Sent Credit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("204"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Return Sent Debit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("5"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Retrun Received Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("105"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Retrun Received Credit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("205"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Retrun eceived Debit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("6"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("NOC Sent Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("106"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("NOC Sent Credit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("206"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("NOC Sent Debit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("8"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("NOC Received Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("108"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("NOC Received Credit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("208"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("NOC Received Debit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("7"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Default Approved Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("107"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Default Approved Credit Report: " + settlementDate, headerFont));


            }
            else if (ddListReportType.SelectedValue.Equals("207"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Default Approved Debit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("9"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Branchwise-Transaction Received All Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("109"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Branchwise-Transaction Received All Credit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("209"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Branchwise-Transaction Received All Debit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("10"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Branchwise-Transaction Received Approved Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("110"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Branchwise-Transaction Received Approved Credit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("210"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Branchwise-Transaction Received Approved Debit Report: " + settlementDate, headerFont));


            }
            else if (ddListReportType.SelectedValue.Equals("11"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Outward Dishonor Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("113"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Outward Dishonor Credit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("213"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Outward Dishonor Debit Report: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("12"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Departmentwise Outward Transaction Report: " + settlementDate, headerFont));


            }
            else if (ddListReportType.SelectedValue.Equals("13"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Departmentwise Inward Return: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("111"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Departmentwise Inward Return Credit: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("211"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Departmentwise Inward Return Debit: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("112"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Departmentwise Outward Transaction Credit: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("212"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Departmentwise Outward Transaction Debit: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("113"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Departmentwise Outward Dishonor Credit: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("213"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Departmentwise Outward Dishonor Debit: " + settlementDate, headerFont));


            }
            else if (ddListReportType.SelectedValue.Equals("20"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Outward RNOC: " + settlementDate, headerFont));


            }
            else if (ddListReportType.SelectedValue.Equals("120"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Outward RNOC Credit: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("220"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Outward RNOC Debit: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("21"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Inward Contested: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("121"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Inward Contested Credit: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("221"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Inward Contested Debit: " + settlementDate, headerFont));

            }
            else if (ddListReportType.SelectedValue.Equals("22"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Inward Dishonor Report: " + settlementDate, headerFont));
            }
            else if (ddListReportType.SelectedValue.Equals("122"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Inward Dishonor Credit Report: " + settlementDate, headerFont));
            }
            else if (ddListReportType.SelectedValue.Equals("222"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Inward Dishonor Debit Report: " + settlementDate, headerFont));
            }
            else if (ddListReportType.SelectedValue.Equals("23"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Inward RNOC Debit Report: " + settlementDate, headerFont));
            }
            else if (ddListReportType.SelectedValue.Equals("123"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Inward RNOC Debit Report: " + settlementDate, headerFont));
            }
            else if (ddListReportType.SelectedValue.Equals("223"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Inward RNOC Debit Report: " + settlementDate, headerFont));
            }
            else if (ddListReportType.SelectedValue.Equals("24"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Outward Contested Report: " + settlementDate, headerFont));
            }
            else if (ddListReportType.SelectedValue.Equals("124"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Outward Contested Credit Report: " + settlementDate, headerFont));
            }
            else if (ddListReportType.SelectedValue.Equals("224"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("Outward Contested Debit Report: " + settlementDate, headerFont));
            }

            headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
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
                headerwidths = new float[] { 11, 11, 8, 10, 4, 8, 6, 10, 8, 8, 8, 12, 8 };
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
            datatable.AddCell(new iTextSharp.text.Phrase("Tran. Code", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Bank RoutNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
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

                PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                c5.BorderWidth = 0.5f;
                c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c5.Padding = 4;
                datatable.AddCell(c5);

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

                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["IdNumber"], fnt));
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

            if (ddListReportType.SelectedValue.Equals("9"))
            {
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDate(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);
            }
            if (ddListReportType.SelectedValue.Equals("109"))
            {
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateForCredit(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);
            }
            if (ddListReportType.SelectedValue.Equals("209"))
            {
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateForDebit(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);
            }

            else if (ddListReportType.SelectedValue.Equals("10"))
            {
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateApprovedOnly(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID, "ALL", "ALL");


            }
            else if (ddListReportType.SelectedValue.Equals("110"))
            {
                return detailSettlementReportDB.GetReportForTransactionReceviedBySettlementDateApprovedOnlyForCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID, "ALL", "ALL");


            }
            else if (ddListReportType.SelectedValue.Equals("210"))
            {
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateApprovedOnlyForDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID, "ALL", "ALL");


            }
            else if (ddListReportType.SelectedValue.Equals("31"))
            {
                return detailSettlementReportDB.GetReportBranchwiseReturnSent(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("131"))
            {
                return detailSettlementReportDB.GetReportBranchwiseReturnSentCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("231"))
            {
                return detailSettlementReportDB.GetReportBranchwiseReturnSentDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("32"))
            {
                return detailSettlementReportDB.GetReportBranchwiseNOCSent(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("132"))
            {
                return detailSettlementReportDB.GetReportBranchwiseNOCSentCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("232"))
            {
                return detailSettlementReportDB.GetReportBranchwiseNOCSentDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("33"))
            {
                return detailSettlementReportDB.GetReportBranchwiseInwardRNOC(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("133"))
            {
                return detailSettlementReportDB.GetReportBranchwiseInwardRNOCCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("233"))
            {
                return detailSettlementReportDB.GetReportBranchwiseInwardRNOCDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("34"))
            {
                return detailSettlementReportDB.GetReportBranchwiseInwardDishonor(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("134"))
            {
                return detailSettlementReportDB.GetReportBranchwiseInwardDishonorCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("234"))
            {
                return detailSettlementReportDB.GetReportBranchwiseInwardDishonorDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("35"))
            {
                return detailSettlementReportDB.GetReportBranchwiseContestedSent(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("135"))
            {
                return detailSettlementReportDB.GetReportBranchwiseContestedSentCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            else if (ddListReportType.SelectedValue.Equals("235"))
            {
                return detailSettlementReportDB.GetReportBranchwiseeContestedSentDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);


            }
            return new DataTable();
        }

        public DataTable GetBranches()
        {
            DetailSettlementReportDB detailSettlementReportDB = new DetailSettlementReportDB();
            if (ddListReportType.SelectedValue.Equals("9"))
            {
                return detailSettlementReportDB.GetBranchForTransactionReceivedBySettlementDate(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            if (ddListReportType.SelectedValue.Equals("109"))
            {
                return detailSettlementReportDB.GetBranchForTransactionReceivedBySettlementDateForCredit(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            if (ddListReportType.SelectedValue.Equals("209"))
            {
                return detailSettlementReportDB.GetBranchForTransactionReceivedBySettlementDateForDebit(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("10"))
            {
                return detailSettlementReportDB.GetBranchForTransactionReceivedBySettlementDate_ApprovedOnly(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("110"))
            {
                return detailSettlementReportDB.GetBranchForTransactionReceivedBySettlementDate_ApprovedOnlyForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("210"))
            {
                return detailSettlementReportDB.GetBranchForTransactionReceivedBySettlementDate_ApprovedOnlyForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("31"))
            {
                return detailSettlementReportDB.GetBranchForReturnSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("131"))
            {
                return detailSettlementReportDB.GetBranchForReturnSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("231"))
            {
                return detailSettlementReportDB.GetBranchForReturnSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("32"))
            {
                return detailSettlementReportDB.GetBranchForNOCSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("132"))
            {
                return detailSettlementReportDB.GetBranchForNOCSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("232"))
            {
                return detailSettlementReportDB.GetBranchForNOCSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("33"))
            {
                return detailSettlementReportDB.GetBranchInwardRNOCBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("133"))
            {
                return detailSettlementReportDB.GetBranchForInwardRNOCBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("233"))
            {
                return detailSettlementReportDB.GetBranchForInwardRNOCBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("34"))
            {
                return detailSettlementReportDB.GetBranchForInwardDishonortBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("134"))
            {
                return detailSettlementReportDB.GetBranchForInwardDishonorBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("234"))
            {
                return detailSettlementReportDB.GetBranchForInwardDishonorBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("35"))
            {
                return detailSettlementReportDB.GetBranchForContestedSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("135"))
            {
                return detailSettlementReportDB.GetBranchForContestedSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (ddListReportType.SelectedValue.Equals("235"))
            {
                return detailSettlementReportDB.GetBranchForContestedSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
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

                if (ddListReportType.SelectedValue.Equals("9"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Transaction Received All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("109"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Transaction Received All Credit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("209"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Transaction Received All Debit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("10"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Transaction Received Approved Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("110"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Transaction Received Approved Credit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("210"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Transaction Received Approved Debit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("31"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Return Sent Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("131"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Return Sent Credit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("231"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Return Sent Debit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("32"))
                {
                    headertable.AddCell(new Phrase("Branchwise-NOC Sent Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("132"))
                {
                    headertable.AddCell(new Phrase("Branchwise-NOC Sent Credit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("232"))
                {
                    headertable.AddCell(new Phrase("Branchwise-NOC Sent Debit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("33"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Inward RNOC Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("133"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Inward RNOC Credit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("233"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Inward RNOC Debit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("34"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Inward Dishonor Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("134"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Inward Dishonor Credit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("234"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Inward Dishonor Debit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("35"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Outward Contested Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("135"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Outward Contested Credit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("235"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Outward Contested Debit Report: " + settlementDate, headerFont));
                }
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

                    PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                    c5.BorderWidth = 0.5f;
                    c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c5.Padding = 4;
                    datatable2.AddCell(c5);

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
            document.Close();
            Response.End();
        }

        protected void ExpotToExcelbtn_Click(object sender, EventArgs e)
        {
            DataTable dt = GetData();

            if (dt.Rows.Count > 0)
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

            //if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            //{
            //    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            //}
            if (ddListReportType.SelectedValue.Equals("25"))
            {

                return detailSettlementReportDB.GetReportDepartmentwiseTransactionSent(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("125"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseTransactionSentCredit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("225"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseTransactionSentDebit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("26"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseInwardReturn(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("126"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseInwardReturnCredit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("226"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseInwardReturnDebit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("27"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseInwardNOC(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("127"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseInwardNOCCredit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("227"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseInwardNOCDebit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("28"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseOutwardRNOC(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("128"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseRNOCSentCredit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("228"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseRNOCSentDebit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("29"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseOutwardDishonor(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("129"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseDishonorSentCredit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("229"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseDishonorSentDebit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("30"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseInwradContested(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            else if (ddListReportType.SelectedValue.Equals("130"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseInwardContestedCredit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            else if (ddListReportType.SelectedValue.Equals("230"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseInwardContestedDebit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            return new DataTable();
        }

        public DataTable GetDepartment()
        {
            DetailSettlementReportDB detailSettlementReportDB = new DetailSettlementReportDB();
            if (ddListReportType.SelectedValue.Equals("25"))
            {
                return detailSettlementReportDB.GetDepartmentForTransactionSentBySettlementDate(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("125"))
            {

                return detailSettlementReportDB.GetDepartmentForTransactionSentBySettlementDateForCredit(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("225"))
            {

                return detailSettlementReportDB.GetDepartmentForTransactionSentBySettlementDateForDebit(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("26"))
            {
                return detailSettlementReportDB.GetDepartmentForInwardReturnBySettlementDate(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("126"))
            {
                return detailSettlementReportDB.GetDepartmentForInwardReturnBySettlementDateForCredit(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("226"))
            {
                return detailSettlementReportDB.GetDepartmentForInwardReturnBySettlementDateForDebit(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("27"))
            {
                return detailSettlementReportDB.GetDepartmentForInwardNOCBySettlementDate(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("127"))
            {
                return detailSettlementReportDB.GetDepartmentForInwardNOCBySettlementDateForCredit(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("227"))
            {
                return detailSettlementReportDB.GetDepartmentForInwardNOCBySettlementDateForDebit(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("28"))
            {
                return detailSettlementReportDB.GetDepartmentForOutwardRNOCBySettlementDate(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("128"))
            {
                return detailSettlementReportDB.GetDepartmentForOutwardRNOCBySettlementDateForCredit(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("228"))
            {
                return detailSettlementReportDB.GetDepartmentForOutwardRNOCBySettlementDateForDebit(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("29"))
            {
                return detailSettlementReportDB.GetDepartmentForOutwardDishonorBySettlementDate(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("129"))
            {
                return detailSettlementReportDB.GetDepartmentForOutwardDishonorBySettlementDateForCredit(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("229"))
            {
                return detailSettlementReportDB.GetDepartmentForOutwardDishonorBySettlementDateForDebit(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("30"))
            {
                return detailSettlementReportDB.GetDepartmentForInwradContestedBySettlementDate(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("130"))
            {
                return detailSettlementReportDB.GetDepartmentForInwradContestedBySettlementDateForCredit(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
            else if (ddListReportType.SelectedValue.Equals("230"))
            {
                return detailSettlementReportDB.GetDepartmentForInwradContestedBySettlementDateForDebit(
                                                                       ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'));


            }
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
                if (ddListReportType.SelectedValue.Equals("25"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Transaction Sent All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("125"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Transaction Sent Credit All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("225"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Transaction Sent Debit All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("26"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Inward Return All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("126"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Inward Return  Credit All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("226"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Inward Return Debit All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("27"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Inward NOC All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("127"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Inward NOC Credit All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("227"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Inward NOC Debit All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("28"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Outward RNOC All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("128"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Outward RNOC Credit All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("228"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Outward RNOC Debit All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("29"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Outward Dishonor All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("129"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Outward Dishonor Credit All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("229"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Outward Dishonor Debit All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("30"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Inward Contested All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("130"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Inward Contested Credit All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("230"))
                {
                    headertable.AddCell(new Phrase("Departmentwise-Inward Contested Credit All Report: " + settlementDate, headerFont));
                }

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

                    PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                    c5.BorderWidth = 0.5f;
                    c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c5.Padding = 4;
                    datatable2.AddCell(c5);

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

                    PdfPCell c14 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["EntryDesc"], fnt));
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
            document.Close();
            Response.End();
        }
    }
}

