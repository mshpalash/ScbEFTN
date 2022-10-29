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
    public partial class SettlementReportViewer : System.Web.UI.Page
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
            ReportDB reportDB = new ReportDB();
            DataTable dt = reportDB.GetSettlementReport(ParseData.StringToInt(ddlistDay.SelectedValue),
                                                   ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                   ParseData.StringToInt(ddlistYear.SelectedValue)
                                                   ,""
                                                   ,"");
            if (dt.Rows.Count > 0)
            {
                BindSettlementReport(dt);
            }
        }

        private void BindSettlementReport(DataTable dt)
        {
            dtgSettlementReport.DataSource = dt;
            dtgSettlementReport.Columns[1].FooterText = "Total:";
            dtgSettlementReport.Columns[2].FooterText = dt.Compute("SUM(OCI)", "").ToString();
            dtgSettlementReport.Columns[3].FooterText = dt.Compute("SUM(OCA)", "").ToString();
            dtgSettlementReport.Columns[4].FooterText = dt.Compute("SUM(ODI)", "").ToString();
            dtgSettlementReport.Columns[5].FooterText = dt.Compute("SUM(ODA)", "").ToString();
            dtgSettlementReport.Columns[6].FooterText = dt.Compute("SUM(RCI)", "").ToString();
            dtgSettlementReport.Columns[7].FooterText = dt.Compute("SUM(RCA)", "").ToString();
            dtgSettlementReport.Columns[8].FooterText = dt.Compute("SUM(RDI)", "").ToString();
            dtgSettlementReport.Columns[9].FooterText = dt.Compute("SUM(RDA)", "").ToString();
            dtgSettlementReport.Columns[10].FooterText = dt.Compute("SUM(Net)", "").ToString();
            dtgSettlementReport.DataBind();
        }

        private DataTable GetData()
        {
            ReportDB reportDB = new ReportDB();
            return reportDB.GetSettlementReport(ParseData.StringToInt(ddlistDay.SelectedValue),
                                                   ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                   ParseData.StringToInt(ddlistYear.SelectedValue)
                                                   ,""
                                                   ,"");
        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            string FileName = "SettlementReport-" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
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
            headertable.DefaultCell.Border =0;
            float[] widthsAtHeader = { 50, 30, 20 };
            headertable.SetWidths(widthsAtHeader);
            

            //string settlementDate =  ddlistDay.SelectedValue+"/"+  ddlistMonth.SelectedValue.PadLeft(2,'0') +"/"+ddlistYear.SelectedValue;
            headertable.AddCell(new Phrase("Source : Flora BEFTN System", headerFont));
            headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo);
            document.Add(headertable);

            ////////////////////////////////
            iTextSharp.text.pdf.PdfPTable headertable2 = new iTextSharp.text.pdf.PdfPTable(3);
            headertable2.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable2.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            headertable2.DefaultCell.Padding = 0;
            headertable2.WidthPercentage = 99;
            headertable2.DefaultCell.Border = 0;
            float[] widthsAtHeader2 = { 40, 40, 20 };
            headertable2.SetWidths(widthsAtHeader2);


            string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
            headertable2.AddCell(new Phrase("Settlement Date: " + settlementDate, headerFont));
            headertable2.AddCell(new Phrase("Created : " + System.DateTime.Now.ToString("F"), headerFont));
            headertable2.AddCell(new Phrase(""));
            document.Add(headertable2);

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(11);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = {7,15,7,10,7,10,7,10,7,10,10}; 
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
            datatable.AddCell(new iTextSharp.text.Phrase("Bank Code", fnt));
            datatable.AddCell(c0);

       
            datatable.AddCell(new iTextSharp.text.Phrase("OCI", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("OCA", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ODI", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ODA", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("RCI", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("RCA", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("RDI", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("RDA", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Net", fnt));


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankCode"], fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["Bank"], fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 4;
                datatable.AddCell(c2);

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["OCI"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["OCA"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["ODI"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["ODA"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["RCI"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["RCA"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["RDI"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["RDA"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["Net"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            }

            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total", fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(OCI)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(OCA)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(ODI)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(ODA)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(RCI)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(RCA)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(RDI)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(RDA)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));

            decimal settlementNet =   (decimal)dt.Compute("SUM(OCA)", "")
                                    + (decimal)dt.Compute("SUM(ODA)", "")
                                    + (decimal)dt.Compute("SUM(RCA)", "")
                                    + (decimal)dt.Compute("SUM(RDA)", "");
            //datatable.AddCell(new iTextSharp.text.Phrase(((decimal)dt.Compute("SUM(Net)", "")).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(settlementNet.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));


            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }
    }
}
