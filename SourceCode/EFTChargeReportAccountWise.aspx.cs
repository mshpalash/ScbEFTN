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
using FloraSoft;
using System.IO;
using EFTN.component;
using EFTN.Utility;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EFTN
{
    public partial class EFTChargeReportAccountWise : System.Web.UI.Page
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
            BindData();
        }

        private void BindData()
        {
            DataTable dtChargeReport = GetData();
            dtgChargeReport.DataSource = dtChargeReport;
            dtgChargeReport.Columns[0].FooterText = "Total Item";
            dtgChargeReport.Columns[1].FooterText = dtChargeReport.Compute("SUM(SentItems)", "").ToString();
            dtgChargeReport.Columns[3].FooterText = "Total";
            dtgChargeReport.Columns[4].FooterText = ParseData.StringToDouble(dtChargeReport.Compute("SUM(DeductCharge)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            dtgChargeReport.Columns[5].FooterText = ParseData.StringToDouble(dtChargeReport.Compute("SUM(BankDeductCharge)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            dtgChargeReport.Columns[6].FooterText = ParseData.StringToDouble(dtChargeReport.Compute("SUM(TotalDeductCharge)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            dtgChargeReport.Columns[7].FooterText = ParseData.StringToDouble(dtChargeReport.Compute("SUM(VatonTotalDeductCharge)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            dtgChargeReport.Columns[8].FooterText = ParseData.StringToDouble(dtChargeReport.Compute("SUM(TotalDeductChargewithVat)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            dtgChargeReport.DataBind();
        }

        private DataTable GetData()
        {
            EFTChargeReportDB db = new EFTChargeReportDB();
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            int day = ParseData.StringToInt(ddlistDay.SelectedValue);
            int month = ParseData.StringToInt(ddlistMonth.SelectedValue);
            int year = ParseData.StringToInt(ddlistYear.SelectedValue);
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            return db.GetEFTChargeReportAccountWise(connectionString, day, month, year, DepartmentID);
        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            string FileName = "Accountwise Charge Report" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
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
            headertable.AddCell(new Phrase("Account wise Charge Sent Report: " + settlementDate, headerFont));


            headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), headerFont));
            //headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            float[] headerwidths;
            int NumberOfPdfColumn = 0;

            headerwidths = new float[] { 20, 10,10,10,10,10,10,10,10 };
            NumberOfPdfColumn = 9;



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

            c0 = new PdfPCell(new iTextSharp.text.Phrase("Account Number", fnt));



            //PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;

            datatable.AddCell(c0);
            datatable.AddCell(new iTextSharp.text.Phrase("SentItems", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Charge", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("BankCharge", fnt));


            datatable.AddCell(new iTextSharp.text.Phrase("DeductCharge", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("BankDeductCharge", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("TotalDeductCharge", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("VatonTotalDeductCharge", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("TotalDeductChargewithVat", fnt));







            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["AccountNumber"], fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["SentItems"].ToString(), fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 4;
                datatable.AddCell(c2);


                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["Charge"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["BankCharge"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["DeductCharge"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["BankDeductCharge"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["TotalDeductCharge"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["VatonTotalDeductCharge"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["TotalDeductChargewithVat"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));


            }

            //-------------TOTAL IN FOOTER --------------------

            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(SentItems)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));

            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(DeductCharge)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(BankDeductCharge)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(TotalDeductCharge)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(VatonTotalDeductCharge)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(TotalDeductChargewithVat)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            
            
            
            
            //-------------END TOTAL -------------------------



            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }

    }
}
