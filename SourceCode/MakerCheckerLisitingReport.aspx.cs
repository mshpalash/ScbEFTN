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
    public partial class MakerCheckerLisitingReport : System.Web.UI.Page
    {
        DataView dv;
        private static DataTable dt = new DataTable();
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            EFTN.component.ReportDB MakerCheckerReportDb = new ReportDB();
            if (ddListReportType.SelectedValue.Equals("1"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dt = MakerCheckerReportDb.GetOutwardWithMakerChecker(
                                                                ddlistYear.SelectedValue.PadLeft(4, '0')
                                                              + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                              + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("2"))
            {
                dt = MakerCheckerReportDb.GetOutwardReturnWithMakerChecker(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("3"))
            {
                dt = MakerCheckerReportDb.GetOutwardNOCWithMakerChecker(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("4"))
            {
                dt = MakerCheckerReportDb.GetOutwardRNOCWithMakerChecker(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("5"))
            {
                dt = MakerCheckerReportDb.GetOutwardDishonorWithMakerChecker(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("6"))
            {
                dt = MakerCheckerReportDb.GetOutwardContestedWithMakerChecker(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("7"))
            {
                dt = MakerCheckerReportDb.GetInwardTransactionApproveWithMakerChecker(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("8"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dt = MakerCheckerReportDb.GetInwardReturnWithMakerChecker(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("9"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                dt = MakerCheckerReportDb.GetInwardNOCWithMakerChecker(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("10"))
            {
                dt = MakerCheckerReportDb.GetInwardRNOCWithMakerChecker(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("11"))
            {
                dt = MakerCheckerReportDb.GetInwardDishonorWithMakerChecker(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("12"))
            {
                dt = MakerCheckerReportDb.GetInwardContestedWithMakerChecker(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            dtgReportMakerCheckerListing.DataSource = dt;
            dtgReportMakerCheckerListing.DataBind();

        }

        private DataTable GetData()
        {

            EFTN.component.ReportDB MakerCheckerReportDb = new ReportDB();
            if (ddListReportType.SelectedValue.Equals("1"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return MakerCheckerReportDb.GetOutwardWithMakerChecker(
                                                                ddlistYear.SelectedValue.PadLeft(4, '0')
                                                              + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                              + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("2"))
            {
                return MakerCheckerReportDb.GetOutwardReturnWithMakerChecker(
                                                                     ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                   + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                  + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("3"))
            {
                return MakerCheckerReportDb.GetOutwardNOCWithMakerChecker(
                                                                     ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                   + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                  + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("4"))
            {
                return MakerCheckerReportDb.GetOutwardRNOCWithMakerChecker(
                                                                     ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                   + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                  + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("5"))
            {
                return MakerCheckerReportDb.GetOutwardDishonorWithMakerChecker(
                                                                     ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                   + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                  + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }


            else if (ddListReportType.SelectedValue.Equals("6"))
            {
                return MakerCheckerReportDb.GetOutwardContestedWithMakerChecker(
                                                                     ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                   + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                  + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("7"))
            {
                return MakerCheckerReportDb.GetInwardTransactionApproveWithMakerChecker(
                                                                     ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                   + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                  + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("8"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return MakerCheckerReportDb.GetInwardReturnWithMakerChecker(
                                                                     ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                   + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                  + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("9"))
            {
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                return MakerCheckerReportDb.GetInwardNOCWithMakerChecker(
                                                                     ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                   + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                  + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            }
            else if (ddListReportType.SelectedValue.Equals("10"))
            {
                return MakerCheckerReportDb.GetInwardRNOCWithMakerChecker(
                                                                     ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                   + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                  + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if (ddListReportType.SelectedValue.Equals("11"))
            {
                return MakerCheckerReportDb.GetInwardDishonorWithMakerChecker(
                                                                     ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                   + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                  + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else
            {
                return MakerCheckerReportDb.GetInwardContestedWithMakerChecker(
                                                                     ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                   + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                  + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }


        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            if (ddListReportType.SelectedValue.Equals("1"))
            {
                string FileName = "Report-TransactionSentMakerCheckerListing" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            if (ddListReportType.SelectedValue.Equals("2"))
            {
                string FileName = "Report-OutwardReturnMakerCheckerListing" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            if (ddListReportType.SelectedValue.Equals("3"))
            {
                string FileName = "Report-OutwardNOCMakerCheckerListing" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            if (ddListReportType.SelectedValue.Equals("4"))
            {
                string FileName = "Report-OutwardRNOCMakerCheckerListing" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            if (ddListReportType.SelectedValue.Equals("5"))
            {
                string FileName = "Report-OutwardDishonorMakerCheckerListing" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            if (ddListReportType.SelectedValue.Equals("6"))
            {
                string FileName = "Report-OutwardContestedMakerCheckerListing" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            if (ddListReportType.SelectedValue.Equals("7"))
            {
                string FileName = "Report-InwardTransactionMakerCheckerListing" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            if (ddListReportType.SelectedValue.Equals("8"))
            {
                string FileName = "Report-InwardReturnMakerCheckerListing" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            if (ddListReportType.SelectedValue.Equals("9"))
            {
                string FileName = "Report-InwardNOCMakerCheckerListing" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            if (ddListReportType.SelectedValue.Equals("10"))
            {
                string FileName = "Report-InwardRNOCMakerCheckerListing" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            if (ddListReportType.SelectedValue.Equals("11"))
            {
                string FileName = "Report-InwardDishonorMakerCheckerListing" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            if (ddListReportType.SelectedValue.Equals("12"))
            {
                string FileName = "Report-InwardContestedMakerCheckerListing" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
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
            float[] widthsAtHeader = { 50, 30, 20 };
            headertable.SetWidths(widthsAtHeader);

            string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
            if (ddListReportType.SelectedValue.Equals("1"))
            {
                headertable.AddCell(new Phrase("Maker Checker Listing(Transaction Sent)Report: " + settlementDate, headerFont));

            }
            if (ddListReportType.SelectedValue.Equals("2"))
            {
                headertable.AddCell(new Phrase("Maker Checker Listing(Outward Return)Report: " + settlementDate, headerFont));

            }
            if (ddListReportType.SelectedValue.Equals("3"))
            {
                headertable.AddCell(new Phrase("Maker Checker Listing(Outward NOC)Report: " + settlementDate, headerFont));

            }
            if (ddListReportType.SelectedValue.Equals("4"))
            {
                headertable.AddCell(new Phrase("Maker Checker Listing(Outward RNOC)Report: " + settlementDate, headerFont));

            }
            if (ddListReportType.SelectedValue.Equals("5"))
            {
                headertable.AddCell(new Phrase("Maker Checker Listing(Outward Dishonor)Report: " + settlementDate, headerFont));

            }
            if (ddListReportType.SelectedValue.Equals("6"))
            {
                headertable.AddCell(new Phrase("Maker Checker Listing(Outward Contested)Report: " + settlementDate, headerFont));

            }
            if (ddListReportType.SelectedValue.Equals("7"))
            {
                headertable.AddCell(new Phrase("Maker Checker Listing(Inward Transaction Approve)Report: " + settlementDate, headerFont));

            }
            if (ddListReportType.SelectedValue.Equals("8"))
            {
                headertable.AddCell(new Phrase("Maker Checker Listing(Inward Return)Report: " + settlementDate, headerFont));

            }
            if (ddListReportType.SelectedValue.Equals("9"))
            {
                headertable.AddCell(new Phrase("Maker Checker Listing(Inward NOC)Report: " + settlementDate, headerFont));

            }
            if (ddListReportType.SelectedValue.Equals("10"))
            {
                headertable.AddCell(new Phrase("Maker Checker Listing(Inward RNOC)Report: " + settlementDate, headerFont));

            }
            if (ddListReportType.SelectedValue.Equals("11"))
            {
                headertable.AddCell(new Phrase("Maker Checker Listing(Inward Dishonor)Report: " + settlementDate, headerFont));

            }
            if (ddListReportType.SelectedValue.Equals("12"))
            {
                headertable.AddCell(new Phrase("Maker Checker Listing(Inward Contested)Report: " + settlementDate, headerFont));

            }

            headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(10);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 17, 15, 15, 10, 7, 10, 7, 10, 7, 10 };
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Register Date", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;

            datatable.AddCell(c0);


            datatable.AddCell(new iTextSharp.text.Phrase("Maker", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Checker", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("SenderAccountNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccountNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ReceiverBankName", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("SECC", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("TraceNumber", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("TransactionCode", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["EntryDate"].ToString(), fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["UserName"], fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 4;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["UserNameChecker"], fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["AccountNo"], fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);

                PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["DFIAccountNo"], fnt));
                c5.BorderWidth = 0.5f;
                c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c5.Padding = 4;
                datatable.AddCell(c5);

                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankName"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 4;
                datatable.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["SECC"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);


                PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TraceNumber"], fnt));
                c8.BorderWidth = 0.5f;
                c8.HorizontalAlignment = Cell.ALIGN_LEFT;
                c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c8.Padding = 4;
                datatable.AddCell(c8);

                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_LEFT;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 4;
                datatable.AddCell(c9);

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["Amount"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));


            }

            //-------------TOTAL IN FOOTER --------------------
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(TraceNumber)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            //-------------END TOTAL -------------------------



            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }

        protected void dtgReportMakerCheckerListing_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgReportMakerCheckerListing.CurrentPageIndex = e.NewPageIndex;
            dtgReportMakerCheckerListing.DataSource = dt;
            dtgReportMakerCheckerListing.DataBind();
        }

        protected void dtgReportMakerCheckerListing_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = dt.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgReportMakerCheckerListing.DataSource = dv;
            dtgReportMakerCheckerListing.DataBind();
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

        protected void btnExpotToCSV_Click(object sender, EventArgs e)
        {
            DataTable myDt = GetData();

            if (myDt.Rows.Count > 0)
            {
                string xlsFileName = "MakerCheckerListingReport" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                string attachment = "attachment; filename=" + xlsFileName + ".csv";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.csv";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                // Create the CSV file to which grid data will be exported. 
                //StreamWriter sw = new StreamWriter();
                int iColCount = myDt.Columns.Count;

                // First we will write the headers. 

                for (int i = 0; i < iColCount; i++)
                {
                    sw.Write("\"");
                    sw.Write(myDt.Columns[i]);
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
                foreach (DataRow dr in myDt.Rows)
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
