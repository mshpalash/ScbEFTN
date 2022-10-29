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
    public partial class DetailSettlementReportReconciliator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2,'0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ReconcilationDB detailSettlementReportDB = new ReconcilationDB();

            DataTable dtSettlementReport = detailSettlementReportDB.GetReconcilReportForTransactionSentBySettlementDate(
                                                          ddlistYear.SelectedValue.PadLeft(4, '0')
                                                        + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                        + ddlistDay.SelectedValue.PadLeft(2, '0'));
            decimal TrSentAmount = 0;
            if (dtSettlementReport.Rows.Count > 0)
            {
                dtgSettlementReport.CurrentPageIndex = 0;
                dtgSettlementReport.DataSource = dtSettlementReport;
                dtgSettlementReport.Columns[6].FooterText = "Total";
                dtgSettlementReport.Columns[0].FooterText = "Total Item :" + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
                dtgSettlementReport.Columns[7].FooterText = ParseData.StringToDouble(dtSettlementReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                TrSentAmount = ParseData.StringToDecimal(dtSettlementReport.Compute("SUM(Amount)", "").ToString());
            }
            dtgSettlementReport.DataBind();

            decimal TrRecAmount = 0;
            DataTable dtSettlementReportRecAppr = detailSettlementReportDB.GetReconcilRptForTransactionRecBySettlementDateApprovedOnly(
                                                          ddlistYear.SelectedValue.PadLeft(4, '0')
                                                        + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                        + ddlistDay.SelectedValue.PadLeft(2, '0'));
            if (dtSettlementReportRecAppr.Rows.Count > 0)
            {
                dtgSettlementReport.CurrentPageIndex = 0;
                dtgSettlementReportReceivedApproved.DataSource = dtSettlementReportRecAppr;
                dtgSettlementReportReceivedApproved.Columns[6].FooterText = "Total";
                dtgSettlementReportReceivedApproved.Columns[0].FooterText = "Total Item :" + dtSettlementReportRecAppr.Compute("COUNT(TraceNumber)", "").ToString();
                dtgSettlementReportReceivedApproved.Columns[7].FooterText = ParseData.StringToDouble(dtSettlementReportRecAppr.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                TrRecAmount = ParseData.StringToDecimal(dtSettlementReportRecAppr.Compute("SUM(Amount)", "").ToString());
            }

            decimal RetSentAmount = 0;
            DataTable dtSettlementReportRetSent = detailSettlementReportDB.GetReconcilRptForReturnSentBySettlementDate(
                                                          ddlistYear.SelectedValue.PadLeft(4, '0')
                                                        + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                        + ddlistDay.SelectedValue.PadLeft(2, '0'));
            if (dtSettlementReportRetSent.Rows.Count > 0)
            {
                dtgSettlementReport.CurrentPageIndex = 0;
                dtgSettlementReportRS.DataSource = dtSettlementReportRetSent;
                dtgSettlementReportRS.Columns[6].FooterText = "Total";
                dtgSettlementReportRS.Columns[0].FooterText = "Total Item :" + dtSettlementReportRetSent.Compute("COUNT(TraceNumber)", "").ToString();
                dtgSettlementReportRS.Columns[7].FooterText = ParseData.StringToDouble(dtSettlementReportRetSent.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                RetSentAmount = ParseData.StringToDecimal(dtSettlementReportRetSent.Compute("SUM(Amount)", "").ToString());
            }

            decimal NOCSentAmount = 0;
            DataTable dtSettlementReportNOCSent = detailSettlementReportDB.GetReconcilRptForNOCSentBySettlementDate(
                                                          ddlistYear.SelectedValue.PadLeft(4, '0')
                                                        + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                        + ddlistDay.SelectedValue.PadLeft(2, '0'));
            if (dtSettlementReportNOCSent.Rows.Count > 0)
            {
                dtgSettlementReport.CurrentPageIndex = 0;
                dtgSettlementReportNS.DataSource = dtSettlementReportNOCSent;
                dtgSettlementReportNS.Columns[6].FooterText = "Total";
                dtgSettlementReportNS.Columns[0].FooterText = "Total Item :" + dtSettlementReportNOCSent.Compute("COUNT(TraceNumber)", "").ToString();
                dtgSettlementReportNS.Columns[7].FooterText = ParseData.StringToDouble(dtSettlementReportNOCSent.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                NOCSentAmount = ParseData.StringToDecimal(dtSettlementReportNOCSent.Compute("SUM(Amount)", "").ToString());
            }

            decimal RetReceivedAmount = 0;
            DataTable dtSettlementReportRetReceived = detailSettlementReportDB.GetReconcilRptForReturnReceivedBySettlementDate(
                                                          ddlistYear.SelectedValue.PadLeft(4, '0')
                                                        + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                        + ddlistDay.SelectedValue.PadLeft(2, '0'));
            if (dtSettlementReportRetReceived.Rows.Count > 0)
            {
                dtgSettlementReport.CurrentPageIndex = 0;
                dtgSettlementReportRR.DataSource = dtSettlementReportRetReceived;
                dtgSettlementReportRR.Columns[6].FooterText = "Total";
                dtgSettlementReportRR.Columns[0].FooterText = "Total Item :" + dtSettlementReportRetReceived.Compute("COUNT(TraceNumber)", "").ToString();
                dtgSettlementReportRR.Columns[7].FooterText = ParseData.StringToDouble(dtSettlementReportRetReceived.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                RetReceivedAmount = ParseData.StringToDecimal(dtSettlementReportRetReceived.Compute("SUM(Amount)", "").ToString());
            }

            decimal NOCReceivedAmount = 0;
            DataTable dtSettlementReportNOCReceived = detailSettlementReportDB.GetReconcilRptForNOCReceivedBySettlementDate(
                                                          ddlistYear.SelectedValue.PadLeft(4, '0')
                                                        + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                        + ddlistDay.SelectedValue.PadLeft(2, '0'));
            if (dtSettlementReportNOCReceived.Rows.Count > 0)
            {
                dtgSettlementReport.CurrentPageIndex = 0;
                dtgSettlementReportNR.DataSource = dtSettlementReportNOCReceived;
                dtgSettlementReportNR.Columns[6].FooterText = "Total";
                dtgSettlementReportNR.Columns[0].FooterText = "Total Item :" + dtSettlementReportNOCReceived.Compute("COUNT(TraceNumber)", "").ToString();
                dtgSettlementReportNR.Columns[7].FooterText = ParseData.StringToDouble(dtSettlementReportNOCReceived.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                NOCReceivedAmount = ParseData.StringToDecimal(dtSettlementReportNOCReceived.Compute("SUM(Amount)", "").ToString());
            }

            decimal NetAmount = (TrRecAmount + RetReceivedAmount) - (TrSentAmount + RetSentAmount);

            lblNetResult.Text = "[(TR)Dr. " + EFTN.Utility.ParseData.StringToDouble(TrRecAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture)
                                    + " + " +
                                "(RR)Dr. " + EFTN.Utility.ParseData.StringToDouble(RetReceivedAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture)
                                    + "] - [" +
                                "(TS)Cr. " + EFTN.Utility.ParseData.StringToDouble(TrSentAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture)
                                    + " + " +
                                "(RS)Cr. " + EFTN.Utility.ParseData.StringToDouble(RetSentAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture)
                                + "] = " + EFTN.Utility.ParseData.StringToDouble(NetAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture)
                                + "(Net)";

            lblItemTrSent.Text = "Total Transaction Sent Item : " + dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString();
            lblItemRetSent.Text = "Total Return Sent Item : " + dtSettlementReportRetSent.Compute("COUNT(TraceNumber)", "").ToString();
            lblItemNOCSent.Text = "Total NOC Sent Item : " + dtSettlementReportNOCSent.Compute("COUNT(TraceNumber)", "").ToString();
            lblItemTotalSent.Text = "Total Sent Item : " + (ParseData.StringToInt(dtSettlementReport.Compute("COUNT(TraceNumber)", "").ToString())
                                                                + ParseData.StringToInt(dtSettlementReportRetSent.Compute("COUNT(TraceNumber)", "").ToString())
                                                                + ParseData.StringToInt(dtSettlementReportNOCSent.Compute("COUNT(TraceNumber)", "").ToString()));

            lblItemTrRec.Text = "Total Transaction Received Item : " + dtSettlementReportRecAppr.Compute("COUNT(TraceNumber)", "").ToString();
            lblItemRetRec.Text = "Total Return Received Item : " + dtSettlementReportRetReceived.Compute("COUNT(TraceNumber)", "").ToString();
            lblItemNOCRec.Text = "Total NOC Received Item : " + dtSettlementReportNOCReceived.Compute("COUNT(TraceNumber)", "").ToString();
            lblItemTotalReceived.Text = "Total Received Item : " + (ParseData.StringToInt(dtSettlementReportRecAppr.Compute("COUNT(TraceNumber)", "").ToString())
                                                                + ParseData.StringToInt(dtSettlementReportRetReceived.Compute("COUNT(TraceNumber)", "").ToString())
                                                                + ParseData.StringToInt(dtSettlementReportNOCReceived.Compute("COUNT(TraceNumber)", "").ToString()));

            dtgSettlementReportReceivedApproved.DataBind();
            dtgSettlementReport.DataBind();
            dtgSettlementReportNR.DataBind();
            dtgSettlementReportNS.DataBind();
            dtgSettlementReportRR.DataBind();
            dtgSettlementReportRS.DataBind();
            dtgSettlementReport.DataBind();            
        }

        private DataTable GetData(string transactionTypeForData)
        {
            ReconcilationDB detailSettlementReportDB = new ReconcilationDB();
            if (transactionTypeForData.Equals("TrSent"))
            {
                return detailSettlementReportDB.GetReconcilReportForTransactionSentBySettlementDate(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
            else if (transactionTypeForData.Equals("TrRecieved"))
            {
                return detailSettlementReportDB.GetReconcilRptForTransactionRecBySettlementDateApprovedOnly(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }

            else if (transactionTypeForData.Equals("RS"))
            {
                return detailSettlementReportDB.GetReconcilRptForReturnSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }

            else if (transactionTypeForData.Equals("RR"))
            {
                return detailSettlementReportDB.GetReconcilRptForReturnReceivedBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }

            else if (transactionTypeForData.Equals("NS"))
            {
                return detailSettlementReportDB.GetReconcilRptForNOCSentBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }

            else //if (transactionTypeForData.Equals("NR"))
            {
                return detailSettlementReportDB.GetReconcilRptForNOCReceivedBySettlementDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

            }
        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            string FileName = "ReconciliatorReport-" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            PrintPDF(FileName);
        }

        private void PrintPDF(string FileName)
        {
            DataTable dtTrSent = GetData("TrSent");
            DataTable dtTrRec = GetData("TrRecieved");
            DataTable NS = GetData("NS");
            DataTable NR = GetData("NR");

            DataTable RS = GetData("RS");
            DataTable RR = GetData("RR");



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
            headertable.AddCell(new Phrase("Reconciliation Report: " + settlementDate, headerFont));
            headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(11);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 17, 10, 3, 10, 3, 10, 7, 10, 7, 10, 13 };
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------
            iTextSharp.text.pdf.PdfPTable datatable2 = new iTextSharp.text.pdf.PdfPTable(11);
            datatable2.DefaultCell.Padding = 4;
            datatable2.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths2 = { 17, 10, 3, 10, 3, 10, 7, 10, 7, 10, 13 };
            datatable2.SetWidths(headerwidths2);
            datatable2.WidthPercentage = 99;

            //iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable2.DefaultCell.BorderWidth = 0.5f;
            datatable2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable2.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);

          

            //iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable2.DefaultCell.BorderWidth = 0.5f;
            datatable2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable2.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);


            ///////////////////////////////////
            iTextSharp.text.pdf.PdfPTable datatable3 = new iTextSharp.text.pdf.PdfPTable(11);
            datatable3.DefaultCell.Padding = 4;
            datatable3.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths3 = { 17, 10, 3, 10, 3, 10, 7, 10, 7, 10, 13 };
            datatable3.SetWidths(headerwidths3);
            datatable3.WidthPercentage = 99;

            //iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable3.DefaultCell.BorderWidth = 0.5f;
            datatable3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable3.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            ////////////////////////////////////


            ///////////////////////////////////
            iTextSharp.text.pdf.PdfPTable datatable4 = new iTextSharp.text.pdf.PdfPTable(11);
            datatable4.DefaultCell.Padding = 4;
            datatable4.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths4 = { 17, 10, 3, 10, 3, 10, 7, 10, 7, 10, 13 };
            datatable4.SetWidths(headerwidths4);
            datatable4.WidthPercentage = 99;

            //iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable4.DefaultCell.BorderWidth = 0.5f;
            datatable4.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable4.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            ////////////////////////////////////


            ///////////////////////////////////
            iTextSharp.text.pdf.PdfPTable datatable5 = new iTextSharp.text.pdf.PdfPTable(11);
            datatable5.DefaultCell.Padding = 4;
            datatable5.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths5 = { 17, 10, 3, 10, 3, 10, 7, 10, 7, 10, 13 };
            datatable5.SetWidths(headerwidths5);
            datatable5.WidthPercentage = 99;

            //iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable5.DefaultCell.BorderWidth = 0.5f;
            datatable5.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable5.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            ////////////////////////////////////

            ///////////////////////////////////
            iTextSharp.text.pdf.PdfPTable datatable6 = new iTextSharp.text.pdf.PdfPTable(11);
            datatable6.DefaultCell.Padding = 4;
            datatable6.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths6 = { 17, 10, 3, 10, 3, 10, 7, 10, 7, 10, 13 };
            datatable6.SetWidths(headerwidths6);
            datatable6.WidthPercentage = 99;

            //iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable6.DefaultCell.BorderWidth = 0.5f;
            datatable6.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable6.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            ////////////////////////////////////

            decimal BBankCrTs = 0;
            decimal BBankDrTr = 0;
            decimal BBankCrRs = 0;
            decimal BBankDrRr = 0;
            decimal BBankCrNs = 0;
            decimal BBankDrNr = 0;

            BBankCrTs = WriteDataToPDF(dtTrSent, fnt, fntbld, datatable);
            BBankDrTr = WriteDataToPDF(dtTrRec, fnt, fntbld, datatable2);

            BBankCrRs = WriteDataToPDF(RS, fnt, fntbld, datatable3);
            BBankDrRr = WriteDataToPDF(RR, fnt, fntbld, datatable4);


            BBankCrNs = WriteDataToPDF(NS, fnt, fntbld, datatable5);
            BBankDrNr = WriteDataToPDF(NR, fnt, fntbld, datatable6);

            decimal BBankNet = (BBankDrTr + BBankDrRr) - (BBankCrTs + BBankCrRs);

            string pdfNetResult1 = "Bangladesh Bank(TR.) Dr. Amount : " + EFTN.Utility.ParseData.StringToDouble(BBankDrTr.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            string pdfNetResult2 = "Bangladesh Bank Cr(TS). Amount  : (-)" + EFTN.Utility.ParseData.StringToDouble(BBankCrTs.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            string pdfNetResult3 = "Bangladesh Bank Dr(RR). Amount  : " + EFTN.Utility.ParseData.StringToDouble(BBankDrRr.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            string pdfNetResult4 = "Bangladesh Bank Cr(RS). Amount  : (-)" + EFTN.Utility.ParseData.StringToDouble(BBankCrRs.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            string pdfNetResult5 = ".                                   Net. Amount : " + EFTN.Utility.ParseData.StringToDouble(BBankNet.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);


            /////////////////////////////////
            document.Add(new iTextSharp.text.Paragraph("\n"));
            document.Add(new iTextSharp.text.Paragraph(pdfNetResult1));
            document.Add(new iTextSharp.text.Paragraph(pdfNetResult2));
            document.Add(new iTextSharp.text.Paragraph(pdfNetResult3));
            document.Add(new iTextSharp.text.Paragraph(pdfNetResult4));
            document.Add(new iTextSharp.text.Paragraph(pdfNetResult5)); 
            
            document.Add(new iTextSharp.text.Paragraph("\n"));
            document.Add(new iTextSharp.text.Paragraph("Bangladesh Bank Credit Transactions (TS)"));
            document.Add(datatable);
            document.Add(new iTextSharp.text.Paragraph("\n"));
            document.Add(new iTextSharp.text.Paragraph("\n"));
            document.Add(new iTextSharp.text.Paragraph("Bangladesh Bank Credit Transactions (RS)"));
            document.Add(datatable3);
            document.Add(new iTextSharp.text.Paragraph("\n"));
            document.Add(new iTextSharp.text.Paragraph("\n"));
            document.Add(new iTextSharp.text.Paragraph("Bangladesh Bank Credit Transactions (NS)"));
            document.Add(datatable5);
            document.Add(new iTextSharp.text.Paragraph("\n"));
            document.Add(new iTextSharp.text.Paragraph("\n"));
            
            document.Add(new iTextSharp.text.Paragraph("\n"));

            document.Add(new iTextSharp.text.Paragraph("Bangladesh Bank Debit Transactions (TR)"));
            document.Add(datatable2);
            document.Add(new iTextSharp.text.Paragraph("\n"));
            document.Add(new iTextSharp.text.Paragraph("\n"));
            document.Add(new iTextSharp.text.Paragraph("Bangladesh Bank Debit Transactions (RR)"));
            document.Add(datatable4);
            document.Add(new iTextSharp.text.Paragraph("\n"));
            document.Add(new iTextSharp.text.Paragraph("\n"));
            document.Add(new iTextSharp.text.Paragraph("Bangladesh Bank Debit Transactions (NR)"));
            document.Add(datatable6);
            document.Add(new iTextSharp.text.Paragraph("\n"));
            

            document.Close();
            Response.End();

        }

        private static decimal WriteDataToPDF(DataTable dt, Font fnt, Font fntbld, iTextSharp.text.pdf.PdfPTable datatable)
        {
            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;

            datatable.AddCell(c0);


            datatable.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("SECC", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("TraceNumber", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Tr. Code", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("DFI Account No", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Bank Routing No", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Id Number", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Receiver Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Narration", fnt));


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

                PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["Narration"], fnt));
                c11.BorderWidth = 0.5f;
                c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c11.Padding = 4;
                datatable.AddCell(c11);
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
            //-------------END TOTAL -------------------------

            return ParseData.StringToDecimal(dt.Compute("SUM(Amount)", "").ToString());
        }

        protected void ExpotToExcelbtn_Click(object sender, EventArgs e)
        {
            DataTable dtTrSent = GetData("TrSent");
            DataTable dtTrReceived = GetData("TrRecieved");

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
            System.Web.UI.WebControls.DataGrid dg2 = new System.Web.UI.WebControls.DataGrid();

            if (dtTrSent.Rows.Count > 0 || dtTrReceived.Rows.Count > 0)
            {
                //string SettlementDate = ddllistYear.SelectedValue + ddllistMonth.SelectedValue + ddllistDay.SelectedValue;
                //  string xlsFileName = "DetailedSettlementReport(" + SettlementDate + ")-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".xls";

                string xlsFileName = "ReconciliatorReport" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                string attachment = "attachment; filename=" + xlsFileName + ".xls";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";

                dg.DataSource = dtTrSent;
                dg.DataBind();
                dg2.DataSource = dtTrReceived;
                dg2.DataBind();
                dg.RenderControl(htw);
                dg2.RenderControl(htw);

                Response.Write(sw.ToString());
                Response.End();
            }

        }
    }
}
