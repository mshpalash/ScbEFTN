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
using FloraSoft;


namespace EFTN
{
    public partial class DDIReportByEntryDateForCSGViewer : System.Web.UI.Page
    {
        private static DataTable dtSettlementReport = new DataTable();
        DataView dv;
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private static string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCurrencyTypeDropdownlist();
                BindSessionDropdownlist();
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');

                ddlistDayEnd.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonthEnd.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYearEnd.SelectedValue = System.DateTime.Now.Year.ToString();

                sortOrder = "asc";
            }
        }
        protected void BindCurrencyTypeDropdownlist()
        {
            DataTable dropDownData = new DataTable();
            //dropDownData.Columns.Add("Currency");
            //DataRow row = dropDownData.NewRow();
            //row["Currency"] = "ALL";
            //dropDownData.Rows.Add(row);
            //dropDownData.Merge(sentBatchDB.GetCurrencyList(eftConString));
            CurrencyDdList.DataSource = sentBatchDB.GetCurrencyList(eftConString);
            CurrencyDdList.DataTextField = "Currency";
            CurrencyDdList.DataValueField = "Currency";
            CurrencyDdList.DataBind();
        }
        private void BindSessionDropdownlist()
        {
            DataTable dropDownData = new DataTable();
            //dropDownData.Columns.Add("SessionID");
            //DataRow row = dropDownData.NewRow();
            //row["SessionID"] = "ALL";
            //dropDownData.Rows.Add(row);
            //dropDownData.Merge(sentBatchDB.GetSessions(eftConString));
            SessionDdList.DataSource = sentBatchDB.GetSessions(eftConString);
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

        protected void dtgSettlementReport_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = dtSettlementReport.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgSettlementReport.DataSource = dv;
            dtgSettlementReport.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            dtSettlementReport = GetData();
            //if (dtSettlementReport.Rows.Count > 0)
            //{
            //    dtgSettlementReport.CurrentPageIndex = 0;
            //    dtgSettlementReport.DataSource = dtSettlementReport;
            //    dtgSettlementReport.Columns[6].FooterText = "Total Amount";
            //    dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(DDIEDRID)", "").ToString();
            //    dtgSettlementReport.Columns[7].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            //}
            dtgSettlementReport.CurrentPageIndex = 0;
            dtgSettlementReport.DataSource = dtSettlementReport;
            dtgSettlementReport.DataBind();
        }

        private DataTable GetData()
        {
            string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

            SCBDDIDB scbDDIDB = new SCBDDIDB();

            string BeginDate = ddlistYear.SelectedValue.PadLeft(4, '0')
                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                    + ddlistDay.SelectedValue.PadLeft(2, '0');

            string EndDate = ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                + ddlistDayEnd.SelectedValue.PadLeft(2, '0');

            return scbDDIDB.GetDDITransactionStatusReportByDateRange(EFTConnectionString, BeginDate, EndDate, ParseData.StringToInt(ddListReportType.SelectedValue), CurrencyDdList.SelectedValue, int.Parse(SessionDdList.SelectedValue));
        }



        protected void dtgSettlementReport_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgSettlementReport.CurrentPageIndex = e.NewPageIndex;
            dtgSettlementReport.DataSource = dtSettlementReport;
            dtgSettlementReport.DataBind();
        }



        protected void BtnExpotToCSV_Click(object sender, EventArgs e)
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
    }
}
