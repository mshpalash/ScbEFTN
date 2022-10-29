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
    public partial class CBSMisMatchReport : System.Web.UI.Page
    {
        private static DataTable dtMisMatchReport = new DataTable();
        private static DataTable dtMisMatchReportBatchWise = new DataTable();
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');
               
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CBSMissMatchXLDB CbsMismatchReport = new CBSMissMatchXLDB();
            if (ddListMismatchReportType.SelectedValue.Equals("1"))
            {
             dtMisMatchReport=CbsMismatchReport.GetMismatchReportTransactionSentByEntryDateTransactionSent(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0') 
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));
             if (dtMisMatchReport.Rows.Count > 0)
                {
                    divItemWiseMismatch.Visible = true;
                    divBatchWiseMismatch.Visible = false;

                    dtgMismatchReport.CurrentPageIndex = 0;
                    dtgMismatchReport.DataSource = dtMisMatchReport;
                    dtgMismatchReport.Columns[3].FooterText = "Total Amount";
                    dtgMismatchReport.Columns[1].FooterText = "Total Item :" + dtMisMatchReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgMismatchReport.Columns[4].FooterText = ParseData.StringToDouble(dtMisMatchReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
                dtgMismatchReport.DataBind();
            }
            else if(ddListMismatchReportType.SelectedValue.Equals("2"))
            {
             dtMisMatchReport=CbsMismatchReport.GetMismatchReportTransactionReceivedBySettlementJDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0') 
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));
             if (dtMisMatchReport.Rows.Count > 0)
                {
                    divItemWiseMismatch.Visible = true;
                    divBatchWiseMismatch.Visible = false;

                    dtgMismatchReport.CurrentPageIndex = 0;
                    dtgMismatchReport.DataSource = dtMisMatchReport;
                    dtgMismatchReport.Columns[3].FooterText = "Total Amount";
                    dtgMismatchReport.Columns[1].FooterText = "Total Item :" + dtMisMatchReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgMismatchReport.Columns[4].FooterText = ParseData.StringToDouble(dtMisMatchReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
                dtgMismatchReport.DataBind();
            }
            else if (ddListMismatchReportType.SelectedValue.Equals("3"))
            {
                dtMisMatchReport = CbsMismatchReport.GetMismatchReportInwardReturnBySettlementJDate(
                                                                 ddlistYear.SelectedValue.PadLeft(4, '0')
                                                               + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'));
                if (dtMisMatchReport.Rows.Count > 0)
                {
                    divItemWiseMismatch.Visible = true;
                    divBatchWiseMismatch.Visible = false;

                    dtgMismatchReport.CurrentPageIndex = 0;
                    dtgMismatchReport.DataSource = dtMisMatchReport;
                    dtgMismatchReport.Columns[3].FooterText = "Total Amount";
                    dtgMismatchReport.Columns[1].FooterText = "Total Item :" + dtMisMatchReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgMismatchReport.Columns[4].FooterText = ParseData.StringToDouble(dtMisMatchReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
                dtgMismatchReport.DataBind();
            }
            else if (ddListMismatchReportType.SelectedValue.Equals("4"))
            {
                dtMisMatchReportBatchWise = CbsMismatchReport.GetBatches_ForExcelMismatch(
                                                                 ddlistYear.SelectedValue.PadLeft(4, '0')
                                                               + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'));
                if (dtMisMatchReportBatchWise.Rows.Count > 0)
                {
                    divItemWiseMismatch.Visible = false;
                    divBatchWiseMismatch.Visible = true;

                    dtgMisMatchBatchWise.CurrentPageIndex = 0;
                    dtgMisMatchBatchWise.DataSource = dtMisMatchReportBatchWise;                }
                dtgMisMatchBatchWise.DataBind();
            }
            //dtgMisMatchBatchWise
        }

        private DataTable GetData()
        {
            CBSMissMatchXLDB CbsMismatchReport = new CBSMissMatchXLDB();
            if (ddListMismatchReportType.SelectedValue.Equals("1"))
            {
             return dtMisMatchReport=CbsMismatchReport.GetMismatchReportTransactionSentByEntryDateTransactionSent(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0') 
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if(ddListMismatchReportType.SelectedValue.Equals("2"))
            {
            return  dtMisMatchReport=CbsMismatchReport.GetMismatchReportTransactionReceivedBySettlementJDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0') 
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else if(ddListMismatchReportType.SelectedValue.Equals("3"))
            {
             return dtMisMatchReport =CbsMismatchReport.GetMismatchReportInwardReturnBySettlementJDate(
                                                                 ddlistYear.SelectedValue.PadLeft(4, '0')
                                                               + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }
            else
            {
                return dtMisMatchReport = CbsMismatchReport.GetBatches_ForExcelMismatch(
                                                                    ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                  + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                  + ddlistDay.SelectedValue.PadLeft(2, '0'));
            }


        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            if (ddListMismatchReportType.SelectedValue.Equals("1"))
            {
                string FileName = "MisMatchReport-TransactionSent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            else if (ddListMismatchReportType.SelectedValue.Equals("2"))
            {
                string FileName = "MisMatchReport-TransactionSentReceived" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListMismatchReportType.SelectedValue.Equals("3"))
            {
                string FileName = "MisMatchReport-TransactionSentReceived" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
            }
            else if (ddListMismatchReportType.SelectedValue.Equals("4"))
            {
                string FileName = "MisMatchReport-BatchWiseS" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBatchWise(FileName);
            }

        }

        private void PrintPDFBatchWise(string FileName)
        {
            dtMisMatchReportBatchWise = GetData();

            if (dtMisMatchReportBatchWise.Rows.Count == 0)
            {
                return;
            }
            EFTN.component.ReportDB edrDB = new EFTN.component.ReportDB();

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


            string Date = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
            if (ddListMismatchReportType.SelectedValue.Equals("4"))
            {
                headertable.AddCell(new Phrase("MisMatch for Batch Wise Transaction: " + Date, headerFont));
            }
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(14);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = {4, 4, 10, 4, 6, 15, 4, 9, 4, 4, 8, 8, 10,10};
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("BatchNumber", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;

            datatable.AddCell(c0);
            datatable.AddCell(new iTextSharp.text.Phrase("Batch Type", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Company Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("SECC", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Effective EntryDate", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Entry Description", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total Transactions", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total Item in Excel", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Mismatch Item", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total Amount in Excel", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Mismatch Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Created By", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Approved By", fnt));
            
            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;

            for (int i = 0; i < dtMisMatchReportBatchWise.Rows.Count; i++)
            {
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase(dtMisMatchReportBatchWise.Rows[i]["BatchNumber"].ToString(), fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase(dtMisMatchReportBatchWise.Rows[i]["BatchType"].ToString(), fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 1;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase(dtMisMatchReportBatchWise.Rows[i]["CompanyName"].ToString(), fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase(dtMisMatchReportBatchWise.Rows[i]["SECC"].ToString(), fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);

                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dtMisMatchReportBatchWise.Rows[i]["EffectiveEntryDate"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 4;
                datatable.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dtMisMatchReportBatchWise.Rows[i]["EntryDesc"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase(dtMisMatchReportBatchWise.Rows[i]["TotalTransactions"].ToString(), fnt));
                c8.BorderWidth = 0.5f;
                c8.HorizontalAlignment = Cell.ALIGN_LEFT;
                c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c8.Padding = 4;
                datatable.AddCell(c8);


                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dtMisMatchReportBatchWise.Rows[i]["TotalAmount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));


                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase(dtMisMatchReportBatchWise.Rows[i]["ExcelTotalItem"].ToString(), fnt));
                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_LEFT;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 4;
                datatable.AddCell(c9);

                PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase(dtMisMatchReportBatchWise.Rows[i]["MismatchItem"].ToString(), fnt));
                c10.BorderWidth = 0.5f;
                c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c10.Padding = 4;
                datatable.AddCell(c10);

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dtMisMatchReportBatchWise.Rows[i]["ExcelTotalAmount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dtMisMatchReportBatchWise.Rows[i]["MismatchAmount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase(dtMisMatchReportBatchWise.Rows[i]["CreatedBy"].ToString(), fnt));
                c12.BorderWidth = 0.5f;
                c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c12.Padding = 4;
                datatable.AddCell(c12);


                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase(dtMisMatchReportBatchWise.Rows[i]["ApprovedBy"].ToString(), fnt));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_LEFT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable.AddCell(c13);

            }
            //-------------TOTAL IN FOOTER --------------------
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtMisMatchReportBatchWise.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            ////datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));

            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));


            //-------------END TOTAL -------------------------



            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }

        private void PrintPDF(string FileName)
        {
            dtMisMatchReport = GetData();

            if (dtMisMatchReport.Rows.Count == 0)
            {
                return;
            }
            EFTN.component.ReportDB edrDB = new EFTN.component.ReportDB();

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


            string Date = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
            if (ddListMismatchReportType.SelectedValue.Equals("1"))
            {
                headertable.AddCell(new Phrase("MisMatch Transaction Sent: " + Date, headerFont));
            }
            else if(ddListMismatchReportType.SelectedValue.Equals("2"))
            {
                headertable.AddCell(new Phrase("MisMatch Transaction Received: " + Date, headerFont));

            }
            else if (ddListMismatchReportType.SelectedValue.Equals("3"))
            {
                headertable.AddCell(new Phrase("MisMatch Transaction Received: " + Date, headerFont));

            }
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(14);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = {15, 17, 15, 7, 10, 7, 10, 7, 10, 7, 10, 7, 10 ,7};
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("TraceNumber", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;

            datatable.AddCell(c0);
            datatable.AddCell(new iTextSharp.text.Phrase("TransactionCode", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ReceiverName", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccountNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("SendingBankRoutNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ReceivingBankRoutNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("SettlementDate", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("EntryDate", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("AccountMismatch", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("AmountMismatch", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("XLAccountNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("XLAmount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("RejectReason", fnt));
            
            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;

            for (int i = 0; i < dtMisMatchReport.Rows.Count; i++)
            {
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase(dtMisMatchReport.Rows[i]["TraceNumber"].ToString(), fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase(dtMisMatchReport.Rows[i]["TransactionCode"].ToString(), fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 1;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase(dtMisMatchReport.Rows[i]["ReceiverName"].ToString(), fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase(dtMisMatchReport.Rows[i]["DFIAccountNo"].ToString(), fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dtMisMatchReport.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));


                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dtMisMatchReport.Rows[i]["SendingBankRoutNo"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 4;
                datatable.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dtMisMatchReport.Rows[i]["ReceivingBankRoutNo"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase((string)dtMisMatchReport.Rows[i]["SettlementJDate"], fnt));
                c8.BorderWidth = 0.5f;
                c8.HorizontalAlignment = Cell.ALIGN_LEFT;
                c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c8.Padding = 4;
                datatable.AddCell(c8);

                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dtMisMatchReport.Rows[i]["EntryDate"], fnt));
                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_LEFT;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 4;
                datatable.AddCell(c9);

                PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)dtMisMatchReport.Rows[i]["AccountMismatch"], fnt));
                c10.BorderWidth = 0.5f;
                c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c10.Padding = 4;
                datatable.AddCell(c10);

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dtMisMatchReport.Rows[i]["AmountMismatch"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)dtMisMatchReport.Rows[i]["XLAccountNo"], fnt));
                c12.BorderWidth = 0.5f;
                c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c12.Padding = 4;
                datatable.AddCell(c12);

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dtMisMatchReport.Rows[i]["XLAmount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase((string)dtMisMatchReport.Rows[i]["RejectReason"], fnt));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_LEFT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable.AddCell(c13);

            }
            //-------------TOTAL IN FOOTER --------------------
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtMisMatchReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));

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

        protected void dtgMismatchReport_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgMismatchReport.CurrentPageIndex = e.NewPageIndex;
            dtgMismatchReport.DataSource = dtMisMatchReport;
            dtgMismatchReport.DataBind();
      
        }
    }
}
