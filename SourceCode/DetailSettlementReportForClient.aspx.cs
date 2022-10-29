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
    public partial class DetailSettlementReportForClient : System.Web.UI.Page
    {
        private static DataTable dtSettlementReport = new DataTable();
        DataView dv;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2,'0');
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
            ClientDB clientDB = new ClientDB();

            if (ddListTransactionType.SelectedValue.Equals("Credit"))
            {
                string SenderAccountNumber = Request.Cookies["LoginID"].Value;

                if (ddListReportType.SelectedValue.Equals("1"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForTransactionSentCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);
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
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForReturnReceivedCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);
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
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForNOCReceivedCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);
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
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForTransactionReceivedCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);//this is receiver account number
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
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForNOCSentCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);//this is receiver account number
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
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForReturnSentCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);//this is receiver account number
                    if (dtSettlementReport.Rows.Count > 0)
                    {
                        dtgSettlementReport.CurrentPageIndex = 0;
                        dtgSettlementReport.DataSource = dtSettlementReport;
                        dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                        dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                        dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
            }
            else
            {
                string SenderAccountNumber = Request.Cookies["LoginID"].Value;

                if (ddListReportType.SelectedValue.Equals("1"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForTransactionReceivedDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);
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
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForReturnReceivedDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);
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
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForNOCReceivedDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);
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
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForTransactionReceivedDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);//this is receiver account number
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
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForNOCSentDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);//this is receiver account number
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
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForReturnSentDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);//this is receiver account number
                    if (dtSettlementReport.Rows.Count > 0)
                    {
                        dtgSettlementReport.CurrentPageIndex = 0;
                        dtgSettlementReport.DataSource = dtSettlementReport;
                        dtgSettlementReport.Columns[7].FooterText = "Total Amount";
                        dtgSettlementReport.Columns[1].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                        dtgSettlementReport.Columns[8].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                    }
                }

            }
            dtgSettlementReport.DataBind();
        }

        private DataTable GetData()
        {
            ClientDB clientDB = new ClientDB();

            string SenderAccountNumber = Request.Cookies["LoginID"].Value;

            if (ddListTransactionType.SelectedValue.Equals("Credit"))
            {
                if (ddListReportType.SelectedValue.Equals("1"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForTransactionSentCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);

                }
                else if (ddListReportType.SelectedValue.Equals("2"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForReturnReceivedCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);

                }
                else if (ddListReportType.SelectedValue.Equals("3"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForNOCReceivedCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);

                }
                else if (ddListReportType.SelectedValue.Equals("4"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForTransactionReceivedCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);

                }
                else if (ddListReportType.SelectedValue.Equals("5"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForNOCSentCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);

                }
                else if (ddListReportType.SelectedValue.Equals("6"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForReturnSentCredit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);

                }
            }
            else
            {
                if (ddListReportType.SelectedValue.Equals("1"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForTransactionSentDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);


                }
                else if (ddListReportType.SelectedValue.Equals("2"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForReturnReceivedDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);

                }
                else if (ddListReportType.SelectedValue.Equals("3"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForNOCReceivedDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);

                }
                else if (ddListReportType.SelectedValue.Equals("4"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForTransactionReceivedDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);

                }
                else if (ddListReportType.SelectedValue.Equals("5"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForNOCSentDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);

                }
                else if (ddListReportType.SelectedValue.Equals("6"))
                {
                    dtSettlementReport = clientDB.EFTRptSenderAccountNumberWiseForReturnSentDebit(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), SenderAccountNumber);

                }
            }

            return dtSettlementReport;
        }
        
        protected void dtgSettlementReport_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgSettlementReport.CurrentPageIndex = e.NewPageIndex;
            dtgSettlementReport.DataSource = dtSettlementReport;
            dtgSettlementReport.DataBind();
        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {

            if (ddListTransactionType.SelectedValue.Equals("Credit"))
            {
                if (ddListReportType.SelectedValue.Equals("1"))
                {
                    string FileName = "SettlementReport-TransactionSentCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);
                }
                else if (ddListReportType.SelectedValue.Equals("2"))
                {
                    string FileName = "SettlementReport-ReturnReceivedCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);
                }
                else if (ddListReportType.SelectedValue.Equals("3"))
                {
                    string FileName = "SettlementReport-NOCReceivedCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);
                }
                else if (ddListReportType.SelectedValue.Equals("4"))
                {
                    string FileName = "SettlementReport-TransactionReceivedCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);
                }
                else if (ddListReportType.SelectedValue.Equals("5"))
                {
                    string FileName = "SettlementReport-NOCSentCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);
                }
                else if (ddListReportType.SelectedValue.Equals("6"))
                {
                    string FileName = "SettlementReport-ReturnSentCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);
                }
            }
            else
            {
                if (ddListReportType.SelectedValue.Equals("1"))
                {
                    string FileName = "SettlementReport-TransactionSentDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);
                }
                else if (ddListReportType.SelectedValue.Equals("2"))
                {
                    string FileName = "SettlementReport-ReturnReceivedDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);
                }
                else if (ddListReportType.SelectedValue.Equals("3"))
                {
                    string FileName = "SettlementReport-NOCReceivedDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);
                }
                else if (ddListReportType.SelectedValue.Equals("4"))
                {
                    string FileName = "SettlementReport-TransactionReceivedDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);
                }
                else if (ddListReportType.SelectedValue.Equals("5"))
                {
                    string FileName = "SettlementReport-NOCSentDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);
                }
                else if (ddListReportType.SelectedValue.Equals("6"))
                {
                    string FileName = "SettlementReport-ReturnSentDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);
                }
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
            Font headerFont = new Font(Font.HELVETICA, 12);

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

            if (ddListTransactionType.SelectedValue.Equals("Credit"))
            {
                if (ddListReportType.SelectedValue.Equals("1"))
                {
                    string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                    headertable.AddCell(new Phrase("Transaction Sent Credit For Settlement Date: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("2"))
                {
                    string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                    headertable.AddCell(new Phrase("Return Received Credit For Settlement Date: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("3"))
                {
                    string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                    headertable.AddCell(new Phrase("NOC Received Credit For Settlement Date: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("4"))
                {
                    string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                    headertable.AddCell(new Phrase("Transaction Received Credit For Settlement Date: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("5"))
                {
                    string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                    headertable.AddCell(new Phrase("NOC Sent Credit For Settlement Date: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("6"))
                {
                    string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                    headertable.AddCell(new Phrase("Return Sent Credit For Settlement Date: " + settlementDate, headerFont));
                }
            }
            else
            {
                if (ddListReportType.SelectedValue.Equals("1"))
                {
                    string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                    headertable.AddCell(new Phrase("Transaction Sent Debit For Settlement Date: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("2"))
                {
                    string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                    headertable.AddCell(new Phrase("Return Received Debit For Settlement Date: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("3"))
                {
                    string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                    headertable.AddCell(new Phrase("NOC Received Debit For Settlement Date: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("4"))
                {
                    string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                    headertable.AddCell(new Phrase("Transaction Received Debit For Settlement Date: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("5"))
                {
                    string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                    headertable.AddCell(new Phrase("NOC Sent Debit For Settlement Date: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("6"))
                {
                    string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                    headertable.AddCell(new Phrase("Return Sent Debit For Settlement Date: " + settlementDate, headerFont));
                }
            }

            headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);
            
            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(12);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 10, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;
  
            datatable.AddCell(c0);


            datatable.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("SECC", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("TraceNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("TranCode", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("BankRoutNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Receiver /Payer Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("C.Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Ent.Desc", fnt));


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
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

            }

            //-------------TOTAL IN FOOTER --------------------
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
            //-------------END TOTAL -------------------------



            /////////////////////////////////
            document.Add(datatable);

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
    }
}
