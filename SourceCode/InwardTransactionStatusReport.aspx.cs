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
    public partial class InwardTransactionStatusReport : System.Web.UI.Page
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
            DataTable dt;

            if (ddListReportType.SelectedValue.Equals("1"))
            {
                dt = reportDB.GetInwardTransactionStatusSummaryReport(ParseData.StringToInt(ddlistDay.SelectedValue),
                                                   ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                   ParseData.StringToInt(ddlistYear.SelectedValue),
                                                   BankCode);
            }
            else
            {
                dt = reportDB.GetReturnSentStatusSummaryReport(ParseData.StringToInt(ddlistDay.SelectedValue),
                                                   ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                   ParseData.StringToInt(ddlistYear.SelectedValue),
                                                   BankCode);
            }

            if (dt.Rows.Count > 0)
            {
                BindInwardTransactionStatusReport(dt);
            }
        }

        private void BindInwardTransactionStatusReport(DataTable dt)
        {
            dtgInwardTransactionStatusReport.DataSource = dt;
            dtgInwardTransactionStatusReport.DataBind();
        }

        private DataTable GetData()
        {
            string BankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            AdditionalSummaryReportDB reportDB = new AdditionalSummaryReportDB();
            DataTable dt;

            if (ddListReportType.SelectedValue.Equals("1"))
            {
                dt = reportDB.GetInwardTransactionStatusSummaryReport(ParseData.StringToInt(ddlistDay.SelectedValue),
                                                   ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                   ParseData.StringToInt(ddlistYear.SelectedValue),
                                                   BankCode);
            }
            else
            {
                dt = reportDB.GetReturnSentStatusSummaryReport(ParseData.StringToInt(ddlistDay.SelectedValue),
                                                   ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                   ParseData.StringToInt(ddlistYear.SelectedValue),
                                                   BankCode);
            }

            return dt;
        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            string FileName = string.Empty;

            if (ddListReportType.SelectedValue.Equals("1"))
            {
                FileName = "InwardTransactionStatusReport-" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            }
            else
            {
                FileName = "OutwardReturnStatusReport-" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            }

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

            string spacer = "            -              ";

            string str = spacer;
            str = str + ConfigurationManager.AppSettings["OriginBankName"] + " All Rights Reserved" + spacer;
            str = str + "Confidential: internal use only" + spacer;
            str = str + "Powered By Flora Limited" + spacer + "Page No.";

            HeaderFooter footer = new HeaderFooter(new Phrase(str, fnt), true);
            footer.Alignment = Element.ALIGN_CENTER;

            document.Footer = footer;
            document.Open();


            ////////////////////////////////

            string LogoImage = Server.MapPath("images") + "\\SCBLogo.JPG";

            iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
            jpeg.Alignment = Element.ALIGN_RIGHT;


            PdfPCell logo = new PdfPCell();
            logo.BorderWidth = 0;
            logo.Colspan = 2;
            logo.AddElement(jpeg);

            Font headerFont = new Font(Font.HELVETICA, 15);

            ////////////////////////////////

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
            headertable.AddCell(new Phrase(ddListReportType.SelectedItem.Text + " for Settlement Date: " + settlementDate, headerFont));
            headertable.AddCell(logo);

            document.Add(headertable);

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(18);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 6, 13, 4, 7, 3, 7, 3, 7, 3, 7, 3, 7, 3, 7, 3, 7, 3, 7 };
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("RoutingNo", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;
            datatable.AddCell(c0);
            datatable.AddCell(new iTextSharp.text.Phrase("BranchName", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("BranchCode", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ZoneName", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IMItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IMAmt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IMBDAItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IMBDAAmt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ICItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ICAmt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IEBBSItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IEBBSAmt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IEBBSAItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IEBBSAAmt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("CAItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("CAAAmt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("CAPItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("CAPAAmt", fnt));

            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["RoutingNo"], fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 4;
                datatable.AddCell(c1);

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["BranchName"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["BranchCode"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["ZoneName"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["IMItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble((dt.Rows[i]["IMAmt"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["IMBDAItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble((dt.Rows[i]["IMBDAAmt"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["ICItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble((dt.Rows[i]["ICAmt"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["IEBBSItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble((dt.Rows[i]["IEBBSAmt"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["IEBBSAItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble((dt.Rows[i]["IEBBSAAmt"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["CAItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble((dt.Rows[i]["CAAAmt"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["CAPItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble((dt.Rows[i]["CAPAAmt"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            }

            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(IMItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToInt(dt.Compute("SUM(IMItem)", "").ToString()).ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(IMAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(IMBDAItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToInt(dt.Compute("SUM(IMBDAItem)", "").ToString()).ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(IMBDAAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(ICItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToInt(dt.Compute("SUM(ICItem)", "").ToString()).ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(ICAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(IEBBSItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToInt(dt.Compute("SUM(IEBBSItem)", "").ToString()).ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(IEBBSAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(IEBBSAItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToInt(dt.Compute("SUM(IEBBSAItem)", "").ToString()).ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(IEBBSAAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CAItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToInt(dt.Compute("SUM(CAItem)", "").ToString()).ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(CAAAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CAPItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToInt(dt.Compute("SUM(CAPItem)", "").ToString()).ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(CAPAAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));


            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }
    }
}
