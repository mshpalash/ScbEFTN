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
    public partial class BranchWiseTransactionStatus : System.Web.UI.Page
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
            DataTable dt = new DataTable();
            dt = GetData();
            if (dt.Rows.Count > 0)
            {
                BindBranchWiseSettlementReport(dt);
            }
        }

        private DataTable GetData()
        {
            BranchWiseTransactionStatusDB reportDB = new BranchWiseTransactionStatusDB();
            DataTable dt = new DataTable();
            if (ddListTransactionType.SelectedValue.Equals("1"))
            {
                dt = reportDB.GetBranchwiseTransactionStatusForCredit(ParseData.StringToInt(ddlistDay.SelectedValue),
                                                       ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                       ParseData.StringToInt(ddlistYear.SelectedValue),
                                                        ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));
            }
            else if (ddListTransactionType.SelectedValue.Equals("2"))
            {
                dt = reportDB.GetBranchwiseTransactionStatusForDebit(ParseData.StringToInt(ddlistDay.SelectedValue),
                                                       ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                       ParseData.StringToInt(ddlistYear.SelectedValue),
                                                        ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));
            }
            return dt;
        }

        private void BindBranchWiseSettlementReport(DataTable dt)
        {
            dtgBranchWiseSettlementReport.DataSource = dt;
            dtgBranchWiseSettlementReport.Columns[1].FooterText = "Total:";
            dtgBranchWiseSettlementReport.Columns[2].FooterText = dt.Compute("SUM(TotalItem)", "").ToString();
            dtgBranchWiseSettlementReport.Columns[3].FooterText = dt.Compute("SUM(TotalAmount)", "").ToString();
            dtgBranchWiseSettlementReport.Columns[4].FooterText = dt.Compute("SUM(ReturnItem)", "").ToString();
            dtgBranchWiseSettlementReport.Columns[5].FooterText = dt.Compute("SUM(ReturnAmount)", "").ToString();
            dtgBranchWiseSettlementReport.Columns[6].FooterText = dt.Compute("SUM(ApprovedItem)", "").ToString();
            dtgBranchWiseSettlementReport.Columns[7].FooterText = dt.Compute("SUM(ApprovedAmont)", "").ToString();
            dtgBranchWiseSettlementReport.DataBind();
        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            string FileName = "BranchWiseTransactionStatus-" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

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
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(8);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 15, 16, 8, 15, 8, 15, 8, 15 };
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Routing No", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;
            datatable.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
            datatable.AddCell(c0);


            datatable.AddCell(new iTextSharp.text.Phrase("TotalItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("TotalAmount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ReturnItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ReturnAmount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ApprovedItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ApprovedAmont", fnt));


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["RoutingNo"], fnt));
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

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["TotalItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["TotalAmount"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["ReturnItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["ReturnAmount"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["ApprovedItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["ApprovedAmont"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            }

            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total", fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(TotalItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(TotalAmount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(ReturnItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(ReturnAmount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(ApprovedItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(ApprovedAmont)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));

            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();
        }
    }
}
