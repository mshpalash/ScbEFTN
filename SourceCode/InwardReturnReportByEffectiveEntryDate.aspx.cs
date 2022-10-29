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
using iTextSharp.text;
using iTextSharp.text.pdf;
using EFTN.Utility;
using System.IO;
using EFTN.component;

namespace EFTN
{
    public partial class InwardReturnReportByEffectiveEntryDate : System.Web.UI.Page
    {
        private static DataTable dt = new DataTable();
        string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        SentBatchDB sentBatchDB = new SentBatchDB();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();
                BindCurrencyTypeDropdownlist();
                BindSessionDropdownlist();
            }
        }
        protected void BindCurrencyTypeDropdownlist()
        {
            CurrencyDdList.DataSource = sentBatchDB.GetCurrencyList(eftConString);
            CurrencyDdList.DataTextField = "Currency";
            CurrencyDdList.DataValueField = "Currency";
            CurrencyDdList.DataBind();
            CurrencyDdList.SelectedIndex = 0;
        }

        protected void BindSessionDropdownlist()
        {
            SessionDdList.DataSource = sentBatchDB.GetSessions(eftConString);
            SessionDdList.DataTextField = "SessionID";
            SessionDdList.DataValueField = "SessionID";
            SessionDdList.DataBind();
            SessionDdList.SelectedIndex = 0;
        }
        private DataTable BindData()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.ReportDB edrDB = new EFTN.component.ReportDB();

            if (ddListReportType.SelectedValue.Equals("1"))
            {

                dt = edrDB.GetReturnReceivedByTransactionSentEntryDate(
                                                                      ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                    + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue, SessionDdList.SelectedValue);
            }
            else
            {
                dt = edrDB.GetReturnReceivedBySettlementJDate(
                                                                      ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                    + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, CurrencyDdList.SelectedValue, SessionDdList.SelectedValue);
            }

            dtgInwardtReturnRptByEffectiveEntryDate.DataSource = dt;
            try
            {
                dtgInwardtReturnRptByEffectiveEntryDate.DataBind();

            }
            catch
            {
                dtgInwardtReturnRptByEffectiveEntryDate.CurrentPageIndex = 0;
                dtgInwardtReturnRptByEffectiveEntryDate.DataBind();
            }
            //.DataBind();
            return dt;
        }

        protected void dtgInwardtReturnRptByEffectiveEntryDate_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgInwardtReturnRptByEffectiveEntryDate.CurrentPageIndex = e.NewPageIndex;
            dtgInwardtReturnRptByEffectiveEntryDate.DataSource = dt;
            dtgInwardtReturnRptByEffectiveEntryDate.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            string FileName = "RetrunReceived-ByEffectiveEntryDate" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".pdf";
            if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("215"))
            {
                PrintPDFForSCB(FileName);
            }
            else
            {
                PrintPDF(FileName);
            }
        }

        private void PrintPDF(string FileName)
        {
            DataTable dt = BindData();

            if (dt.Rows.Count == 0)
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


            string EntryDateTransactionSent = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
            headertable.AddCell(new Phrase("Retrun Received By Effective Entry Date: " + EntryDateTransactionSent, headerFont));
            headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(12);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 8, 8, 8, 12, 12, 8, 7, 8, 7, 10, 8, 3 };
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("EntryDateTransactionSent", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;

            datatable.AddCell(c0);
            datatable.AddCell(new iTextSharp.text.Phrase("Settlement Date", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Date Of Return Received", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Sender A/C", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Receiver Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccountNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Company Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Reject Reason", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Tr. Code", fnt));

            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["EntryDateTransactionSent"].ToString(), fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["SettlementJDate"].ToString(), fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 4;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["EntryDateReturnReceived"].ToString(), fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankName"], fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);

                PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BranchName"], fnt));
                c5.BorderWidth = 0.5f;
                c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c5.Padding = 4;
                datatable.AddCell(c5);

                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["AccountNo"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 4;
                datatable.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["ReceiverName"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["DFIAccountNo"], fnt));
                c8.BorderWidth = 0.5f;
                c8.HorizontalAlignment = Cell.ALIGN_LEFT;
                c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c8.Padding = 4;
                datatable.AddCell(c8);

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dt.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["CompanyName"], fnt));
                c10.BorderWidth = 0.5f;
                c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c10.Padding = 4;
                datatable.AddCell(c10);

                PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["RejectReason"], fnt));
                c11.BorderWidth = 0.5f;
                c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c11.Padding = 4;
                datatable.AddCell(c11);

                PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                c12.BorderWidth = 0.5f;
                c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c12.Padding = 4;
                datatable.AddCell(c12);
            }
            //-------------TOTAL IN FOOTER --------------------
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));

            //-------------END TOTAL -------------------------



            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }

        private void PrintPDFForSCB(string FileName)
        {
            DataTable dt = BindData();

            if (dt.Rows.Count == 0)
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


            string EntryDateTransactionSent = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
            headertable.AddCell(new Phrase("Retrun Received By Effective Entry Date: " + EntryDateTransactionSent, headerFont));
            headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(12);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 8, 8, 8, 12, 12, 8, 7, 8, 7, 10, 8, 3 };
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Sender A/C", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;

            datatable.AddCell(c0);
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Reject Reason", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IDNumber", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Settlement Date", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Date Of Return Received", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Receiver Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccountNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Company Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Tr. Code", fnt));

            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["AccountNo"].ToString(), fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dt.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["RejectReason"], fnt));
                c11.BorderWidth = 0.5f;
                c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c11.Padding = 4;
                datatable.AddCell(c11);

                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["IDNumber"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 4;
                datatable.AddCell(c6);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["SettlementJDate"].ToString(), fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 4;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["EntryDateReturnReceived"].ToString(), fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankName"], fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);

                PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BranchName"], fnt));
                c5.BorderWidth = 0.5f;
                c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c5.Padding = 4;
                datatable.AddCell(c5);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["ReceiverName"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["DFIAccountNo"], fnt));
                c8.BorderWidth = 0.5f;
                c8.HorizontalAlignment = Cell.ALIGN_LEFT;
                c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c8.Padding = 4;
                datatable.AddCell(c8);


                PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["CompanyName"], fnt));
                c10.BorderWidth = 0.5f;
                c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c10.Padding = 4;
                datatable.AddCell(c10);

                PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                c12.BorderWidth = 0.5f;
                c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c12.Padding = 4;
                datatable.AddCell(c12);
            }
            //-------------TOTAL IN FOOTER --------------------
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));

            //-------------END TOTAL -------------------------



            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }

        protected void btnExpotToCSV_Click(object sender, EventArgs e)
        {
            DataTable myDt = BindData();

            if (myDt.Rows.Count > 0)
            {
                string xlsFileName = "InwardReturnReport" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
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

