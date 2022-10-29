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
    public partial class OutwardCreditSummaryReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string BankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            AdditionalSummaryReportDB reportDB = new AdditionalSummaryReportDB();
            DataTable dt = reportDB.GetOutwardCreditSummary(ParseData.StringToInt(ddlistDay.SelectedValue),
                                                   ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                   ParseData.StringToInt(ddlistYear.SelectedValue),
                                                   BankCode);
            if (dt.Rows.Count > 0)
            {
                BindOutwardCreditSummaryReport(dt);
            }
        }

        private void BindOutwardCreditSummaryReport(DataTable dt)
        {
            dtgOutwardCreditSummaryReport.DataSource = dt;
            dtgOutwardCreditSummaryReport.DataBind();
        }

        private DataTable GetData()
        {
            string BankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            AdditionalSummaryReportDB reportDB = new AdditionalSummaryReportDB();
            return reportDB.GetOutwardCreditSummary(ParseData.StringToInt(ddlistDay.SelectedValue),
                                                   ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                   ParseData.StringToInt(ddlistYear.SelectedValue),
                                                   BankCode);
        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            string FileName = "OutwardCreditSummaryReport-" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            PrintPDF(FileName);
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
            str = str + "Powered By Flora Limited" + spacer + "Page No.";


            HeaderFooter footer = new HeaderFooter(new Phrase(str, fnt), true);
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
            //iTextSharp.text.Table headertable = new iTextSharp.text.Table(3);
            //headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            //headertable.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            //headertable.Cellpadding = 0;
            //headertable.Cellspacing = 0;
            //headertable.Border = 0;
            //headertable.Width = 100;
            iTextSharp.text.pdf.PdfPTable headertable = new iTextSharp.text.pdf.PdfPTable(3);
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            headertable.DefaultCell.Padding = 0;
            headertable.WidthPercentage = 99;
            headertable.DefaultCell.Border = 0;
            float[] widthsAtHeader = { 15, 65, 20 };
            headertable.SetWidths(widthsAtHeader);

            string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;

            headertable.AddCell(new Phrase("", headerFont));
            headertable.AddCell(new Phrase("Outward Credit Summary Report for Settlement Date: " + settlementDate, headerFont));
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo);

            document.Add(headertable);

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(11);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 4, 6, 14, 5, 11, 6, 11, 6, 11, 12, 14 };
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("SL No.", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;
            datatable.AddCell(c0);
            datatable.AddCell(new iTextSharp.text.Phrase("Routing No", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Branch Code", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Zone Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Cr. Item", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Cr. Amt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Ret Cr. Item", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Ret Cr. Amt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Net", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("AdviceNo.", fnt));


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string adviceNo = string.Empty;
                adviceNo = "1"                                                 //1 digit Credit = 1 and Debit = 2
                          + "1"                                                 //1 digit Outward = 1 and Inward = 2
                          + (dt.Rows[i]["BranchCode"]).ToString().Trim()               //3 digit SolID = BranchCode
                          + (ddlistYear.SelectedValue.Substring(2, 2))            //2 digit From Selected Year
                          + (ddlistMonth.SelectedValue.PadLeft(2, '0'))           //2 digit From Selected Month
                          + (ddlistDay.SelectedValue.PadLeft(2, '0'))             //2 digit From Selected Day
                          + (dt.Rows[i]["CrItem"]).ToString().PadLeft(4, '0');  //4 digit From No Of Item
                int SLNo = i + 1;
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase(SLNo.ToString(), fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 4;
                datatable.AddCell(c1);

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["RoutingNo"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["BranchName"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["BranchCode"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["ZoneName"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["CrItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble((dt.Rows[i]["CrAmt"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["RetCrItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble((dt.Rows[i]["RetCrAmt"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble((dt.Rows[i]["Net"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(adviceNo.ToString(), fnt));
            }


            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToInt(dt.Compute("SUM(CrItem)", "").ToString()).ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(CrAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToInt(dt.Compute("SUM(RetCrItem)", "").ToString()).ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(RetCrAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Net)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }
    }
}
