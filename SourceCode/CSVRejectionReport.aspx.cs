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
    public partial class CSVRejectionReport : System.Web.UI.Page
    {
        private static DataTable dtCSVRejectionReport = new DataTable();
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
            CSVReportDB cSVReportDB = new CSVReportDB();
            //dtCSVRejectionReport = cSVReportDB.GetEFTTransactionSentFORCSVRejectionByEntryDate(
            //                                                  ddlistYear.SelectedValue.PadLeft(4, '0') 
            //                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
            //                                                + ddlistDay.SelectedValue.PadLeft(2, '0'));
            //if (dtMisMatchReport.Rows.Count > 0)
            //   {
            //       dtgCSVRejectionReport.CurrentPageIndex = 0;
            //       dtgCSVRejectionReport.DataSource = dtMisMatchReport;
            //       dtgCSVRejectionReport.Columns[3].FooterText = "Total Amount";
            //       dtgCSVRejectionReport.Columns[1].FooterText = "Total Item :" + dtMisMatchReport.Compute("COUNT(TraceNumber)", "").ToString();
            //       dtgCSVRejectionReport.Columns[4].FooterText = ParseData.StringToDouble(dtMisMatchReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            //   }
            dtgCSVRejectionReport.DataSource = GetData();
            dtgCSVRejectionReport.DataBind();
        }

        private DataTable GetData()
        {
            CSVReportDB cSVReportDB = new CSVReportDB();
            return dtCSVRejectionReport = cSVReportDB.GetEFTTransactionSentFORCSVRejectionByEntryDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0') 
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'));

        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
                string FileName = "CSVRejectionReport-TransactionSent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);
        }
        
        private void PrintPDF(string FileName)
        {
            dtCSVRejectionReport = GetData();

            if (dtCSVRejectionReport.Rows.Count == 0)
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


            string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
            headertable.AddCell(new Phrase("Rejected CSV Transaction", headerFont));
            headertable.AddCell(new Phrase("Transaction Entry Date : " + settlementDate, headerFont));
            //headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
            headertable.AddCell(logo);
            document.Add(headertable);

            ////////////////////////////////

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(15);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = {10, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 8, 6 ,8, 6};
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("BankName.", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;

            datatable.AddCell(c0);
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccount No.", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccount No. Status", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Account No.", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Account No. Status", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Receiving Bank RoutingNo.", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Receiving Bank RoutingNo. Status", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Amount Status", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IdNumber Status", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Receiver Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Receiver Name Status", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("PaymentInfo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("PaymentInfo Status", fnt));

  
            
            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;

            for (int i = 0; i < dtCSVRejectionReport.Rows.Count; i++)
            {
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase(dtCSVRejectionReport.Rows[i]["BankName"].ToString(), fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase(dtCSVRejectionReport.Rows[i]["DFIAccountNo"].ToString(), fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 1;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase(dtCSVRejectionReport.Rows[i]["FlagDFIAccountNoLength"].ToString(), fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                datatable.AddCell(new iTextSharp.text.Phrase((string)dtCSVRejectionReport.Rows[i]["AccountNo"], fnt));

                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCSVRejectionReport.Rows[i]["FlagAccountNoLength"], fnt));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_LEFT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable.AddCell(c13);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase(dtCSVRejectionReport.Rows[i]["BankRoutingNo"].ToString(), fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);

                PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCSVRejectionReport.Rows[i]["FlagReceivingBankRT"], fnt));
                c8.BorderWidth = 0.5f;
                c8.HorizontalAlignment = Cell.ALIGN_LEFT;
                c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c8.Padding = 4;
                datatable.AddCell(c8);
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dtCSVRejectionReport.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCSVRejectionReport.Rows[i]["FlagAmount"], fnt));
                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_LEFT;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 4;
                datatable.AddCell(c9);

                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCSVRejectionReport.Rows[i]["IdNumber"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 4;
                datatable.AddCell(c6);

                PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCSVRejectionReport.Rows[i]["FlagIdNumber"], fnt));
                c10.BorderWidth = 0.5f;
                c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c10.Padding = 4;
                datatable.AddCell(c10);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCSVRejectionReport.Rows[i]["ReceiverName"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                datatable.AddCell(new iTextSharp.text.Phrase((string)(dtCSVRejectionReport.Rows[i]["FlagReceiverName"]), fnt));

                PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCSVRejectionReport.Rows[i]["PaymentInfo"], fnt));
                c12.BorderWidth = 0.5f;
                c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c12.Padding = 4;
                datatable.AddCell(c12);

                datatable.AddCell(new iTextSharp.text.Phrase((string)(dtCSVRejectionReport.Rows[i]["FlagPaymentInfo"]), fnt));


            }
            //-------------TOTAL IN FOOTER --------------------
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtCSVRejectionReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
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

        protected void dtgCSVRejectionReport_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgCSVRejectionReport.CurrentPageIndex = e.NewPageIndex;
            dtgCSVRejectionReport.DataSource = dtCSVRejectionReport;
            dtgCSVRejectionReport.DataBind();
        }

        protected void btnExpotToCSV_Click(object sender, EventArgs e)
        {
            DataTable myDt = GetData();

            if (myDt.Rows.Count > 0)
            {
                string xlsFileName = "STSTransactionRejectionReport" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
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
