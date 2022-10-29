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

namespace EFTN
{
    public partial class TotalTransaction : System.Web.UI.Page
    {
        private static DataTable myDTTransactionSent = new DataTable();
        private static DataTable myDTTransactionReceived = new DataTable();
        private static DataTable myDTReturnSent = new DataTable();
        private static DataTable myDTReturnReceived = new DataTable();
        private static DataTable myDTNOCSent = new DataTable();
        private static DataTable myDTNOCReceived = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();
            }
        }

        private void BindTransactionSent()
        {
            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();
            myDTTransactionSent = edrDB.GetSentEDR_ArrovedByCheckerBySettlementDate(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'));
        }

        private void BindTransactionReceived()
        {
            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();

            int BranchID = 0;

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);
            } 
            
            myDTTransactionReceived = edrDB.GetReceivedEDR_ApprovedByMakerBySettlementJDate(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);
        }

        private void BindReturnSent()
        {
            EFTN.component.SentReturnDB sentReturnDB = new EFTN.component.SentReturnDB();

            int BranchID = 0;

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);
            }

            myDTReturnSent = sentReturnDB.GetSentRRForEBBSCheckerBySettelementJDate(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

        }

        private void BindReturnReceived()
        {
            EFTN.component.ReceivedReturnDB receivedReturnDB = new EFTN.component.ReceivedReturnDB();
            myDTReturnReceived = receivedReturnDB.ReceivedReturnApprovedForEBBSCheckerBySettlementDate(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'));
        }

        private void BindNOCSent()
        {
            int BranchID = 0;

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);
            }

            EFTN.component.SentNOCDB sentNOCDB = new EFTN.component.SentNOCDB();
            myDTNOCSent = sentNOCDB.GetSentNOCForEBBSCheckerBySettlementJDate(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);
        }

        private void BindNOCReceived()
        {
            EFTN.component.ReceivedNOCDB receivedNOCDB = new EFTN.component.ReceivedNOCDB();
            myDTNOCReceived = receivedNOCDB.ReceivedNOCApprovedForEBBSCheckerBySettlementDate(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'));
        }

        protected void PrintVoucherBtn_Click(object sender, EventArgs e)
        {
            string FileName = "CBSVoucher-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".pdf";
            BindTransactionSent();
            BindTransactionReceived();
            BindReturnSent();
            BindReturnReceived();
            BindNOCSent();
            BindNOCReceived();
            if (myDTTransactionSent.Rows.Count > 0
                || myDTTransactionReceived.Rows.Count > 0
                || myDTReturnSent.Rows.Count > 0
                || myDTReturnReceived.Rows.Count > 0
                || myDTNOCSent.Rows.Count > 0
                || myDTNOCReceived.Rows.Count > 0)
            {

                PrintPDF(FileName);
            }

        }

        protected void PrintPDF(string FileName)
        {
            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();

            string UserName = Request.Cookies["UserName"].Value;
            string LogoImage = Server.MapPath("images") + "\\SCBLogo.JPG";


            Response.ClearContent();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

            Document document = new Document(PageSize.A4.Rotate(), 10, 10, 8, 8);
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);


            Font fnt = new Font(Font.HELVETICA, 8);
            Font fntlrg = new Font(Font.HELVETICA, 12);
            Font fntblue = new Font(Font.HELVETICA, 8);
            fntblue.Color = new Color(0, 0, 255);
            Font fntbld = new Font(Font.HELVETICA, 8);
            fntbld.SetStyle(Font.BOLD);

            Font fntTableHeader = new Font(Font.HELVETICA, 14);
            fntTableHeader.SetStyle(Font.BOLD);

            document.Open();

            iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
            jpeg.Alignment = Element.ALIGN_RIGHT;

            //headertable.AddCell(logocell);
                        ////////////////////////////////
            iTextSharp.text.pdf.PdfPTable headertable = new iTextSharp.text.pdf.PdfPTable(4);
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            headertable.DefaultCell.Padding = 4;
            headertable.WidthPercentage = 99;
            headertable.DefaultCell.Border = 0;
            float[] widthsAtHeader = { 25, 25, 25, 25 };
            headertable.SetWidths(widthsAtHeader);

            PdfPCell logo = new PdfPCell();
            logo.BorderWidth = 0;
            logo.AddElement(jpeg);
            //------------------------------------------

            headertable.AddCell(new iTextSharp.text.Phrase("", fnt));
            headertable.AddCell(new iTextSharp.text.Phrase("", fnt));
            headertable.AddCell(new iTextSharp.text.Phrase("", fnt));
            headertable.AddCell(logo);


            headertable.AddCell(new iTextSharp.text.Phrase("Branch Code\n\nSystem Name\n\nUser Id\n\nUser Name\n\nCountry\n\nDaily Voucher", fnt));
            headertable.AddCell(new iTextSharp.text.Phrase("060\n\nCBS\n\n\n\n\n\nBangladesh\n\n" + System.DateTime.Today.ToString("dd/MM/yyyy"), fntbld));
            headertable.AddCell(new iTextSharp.text.Phrase("", fnt));
            headertable.AddCell(new iTextSharp.text.Phrase("", fnt));


            if (myDTTransactionSent.Rows.Count > 0)
            {
                PrintTransactionSent(document, fnt, fntbld, fntTableHeader, headertable);
            }

            //////////////////////////////////////////////

            if (myDTTransactionReceived.Rows.Count > 0)
            {
                PrintTransactionReceived(document, fnt, fntbld, fntTableHeader, headertable);
            }

            if (myDTReturnSent.Rows.Count > 0)
            {
                PrintReturnSent(document, fnt, fntbld, fntTableHeader, headertable);
            }

            if (myDTReturnReceived.Rows.Count > 0)
            {
                PrintReturnReceived(document, fnt, fntbld, fntTableHeader, headertable);
            }

            if (myDTNOCSent.Rows.Count > 0)
            {
                PrintNOCSent(document, fnt, fntbld, fntTableHeader, headertable);
            }

            if (myDTNOCReceived.Rows.Count > 0)
            {
                PrintNOCReceived(document, fnt, fntbld, fntTableHeader, headertable);
            }

            document.Close();
            Response.End();
        }

        private static void PrintNOCReceived(Document document, Font fnt, Font fntbld, Font fntTableHeader, iTextSharp.text.pdf.PdfPTable headertable)
        {
            document.Add(headertable);

            iTextSharp.text.pdf.PdfPTable datatableNR = new iTextSharp.text.pdf.PdfPTable(9);
            datatableNR.DefaultCell.Padding = 4;
            datatableNR.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 10, 10, 20, 6, 19, 5, 10, 10, 10 };
            datatableNR.SetWidths(headerwidths);
            datatableNR.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatableNR.DefaultCell.BorderWidth = 0;
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("NOC Received", fntTableHeader));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));



            datatableNR.DefaultCell.BorderWidth = 0.5f;
            datatableNR.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            datatableNR.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);


            datatableNR.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            datatableNR.DefaultCell.BorderWidth = 1;

            datatableNR.AddCell(new iTextSharp.text.Phrase("C.cy", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("Account No.", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("A/C Name", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("Trn Code", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("Narration", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("Seq. No", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("Debit", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("Credit", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("Value Date", fnt));

            datatableNR.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatableNR.DefaultCell.BorderWidth = 1;


            decimal totalAmount = 0;

            for (int i = 0; i < myDTNOCReceived.Rows.Count; i++)
            {
                decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(myDTNOCReceived.Rows[i]["Amount"].ToString());
                totalAmount += transAmount;

                datatableNR.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"]), fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase(myDTNOCReceived.Rows[i]["TraceNumber"].ToString(), fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase(myDTNOCReceived.Rows[i]["EntryDate"].ToString(), fnt));

                datatableNR.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(myDTNOCReceived.Rows[i]["DFIAccountNo"].ToString()), fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase(myDTNOCReceived.Rows[i]["AccountName"].ToString(), fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase(myDTNOCReceived.Rows[i]["TraceNumber"].ToString(), fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatableNR.AddCell(new iTextSharp.text.Phrase(myDTNOCReceived.Rows[i]["EntryDate"].ToString(), fnt));
            }

            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("Total (BDT)", fntbld));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));


            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("Maker", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableNR.AddCell(new iTextSharp.text.Phrase("Input Date", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableNR.AddCell(new iTextSharp.text.Phrase("", fnt));
            document.Add(datatableNR);
        }

        private static void PrintNOCSent(Document document, Font fnt, Font fntbld, Font fntTableHeader, iTextSharp.text.pdf.PdfPTable headertable)
        {
            document.Add(headertable);

            iTextSharp.text.pdf.PdfPTable datatableNS = new iTextSharp.text.pdf.PdfPTable(9);
            datatableNS.DefaultCell.Padding = 4;
            datatableNS.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 10, 10, 20, 6, 19, 5, 10, 10, 10 };
            datatableNS.SetWidths(headerwidths);
            datatableNS.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatableNS.DefaultCell.BorderWidth = 0;
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("NOC Sent", fntTableHeader));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));


            datatableNS.DefaultCell.BorderWidth = 0.5f;
            datatableNS.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            datatableNS.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);


            datatableNS.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            datatableNS.DefaultCell.BorderWidth = 1;

            datatableNS.AddCell(new iTextSharp.text.Phrase("C.cy", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("Account No.", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("A/C Name", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("Trn Code", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("Narration", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("Seq. No", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("Debit", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("Credit", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("Value Date", fnt));

            datatableNS.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatableNS.DefaultCell.BorderWidth = 1;


            decimal totalAmount = 0;

            for (int i = 0; i < myDTNOCSent.Rows.Count; i++)
            {
                decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(myDTNOCSent.Rows[i]["Amount"].ToString());
                totalAmount += transAmount;

                datatableNS.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"]), fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase(myDTNOCSent.Rows[i]["TraceNumber"].ToString(), fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase(myDTNOCSent.Rows[i]["EntryDate"].ToString(), fnt));

                datatableNS.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(myDTNOCSent.Rows[i]["DFIAccountNo"].ToString()), fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase(myDTNOCSent.Rows[i]["AccountName"].ToString(), fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase(myDTNOCSent.Rows[i]["TraceNumber"].ToString(), fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatableNS.AddCell(new iTextSharp.text.Phrase(myDTNOCSent.Rows[i]["EntryDate"].ToString(), fnt));
            }

            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("Total (BDT)", fntbld));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));


            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("Maker", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableNS.AddCell(new iTextSharp.text.Phrase("Input Date", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableNS.AddCell(new iTextSharp.text.Phrase("", fnt));
            document.Add(datatableNS);
            document.NewPage();
        }

        private static void PrintReturnReceived(Document document, Font fnt, Font fntbld, Font fntTableHeader, iTextSharp.text.pdf.PdfPTable headertable)
        {
            document.Add(headertable);

            iTextSharp.text.pdf.PdfPTable datatableRR = new iTextSharp.text.pdf.PdfPTable(9);
            datatableRR.DefaultCell.Padding = 4;
            datatableRR.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 10, 10, 20, 6, 19, 5, 10, 10, 10 };
            datatableRR.SetWidths(headerwidths);
            datatableRR.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatableRR.DefaultCell.BorderWidth = 0;
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("Return Received", fntTableHeader));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));


            datatableRR.DefaultCell.BorderWidth = 0.5f;
            datatableRR.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            datatableRR.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);


            datatableRR.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            datatableRR.DefaultCell.BorderWidth = 1;

            datatableRR.AddCell(new iTextSharp.text.Phrase("C.cy", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("Account No.", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("A/C Name", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("Trn Code", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("Narration", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("Seq. No", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("Debit", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("Credit", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("Value Date", fnt));

            datatableRR.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatableRR.DefaultCell.BorderWidth = 1;


            decimal totalAmount = 0;

            for (int i = 0; i < myDTReturnReceived.Rows.Count; i++)
            {
                decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(myDTReturnReceived.Rows[i]["Amount"].ToString());
                totalAmount += transAmount;

                datatableRR.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"]), fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase(myDTReturnReceived.Rows[i]["TraceNumber"].ToString(), fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase(myDTReturnReceived.Rows[i]["EntryDate"].ToString(), fnt));

                datatableRR.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(myDTReturnReceived.Rows[i]["DFIAccountNo"].ToString()), fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase(myDTReturnReceived.Rows[i]["AccountName"].ToString(), fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase(myDTReturnReceived.Rows[i]["TraceNumber"].ToString(), fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatableRR.AddCell(new iTextSharp.text.Phrase(myDTReturnReceived.Rows[i]["EntryDate"].ToString(), fnt));
            }

            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("Total (BDT)", fntbld));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));


            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("Maker", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableRR.AddCell(new iTextSharp.text.Phrase("Input Date", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableRR.AddCell(new iTextSharp.text.Phrase("", fnt));
            document.Add(datatableRR);
            document.NewPage();
        }

        private static void PrintReturnSent(Document document, Font fnt, Font fntbld, Font fntTableHeader, iTextSharp.text.pdf.PdfPTable headertable)
        {
            document.Add(headertable);

            iTextSharp.text.pdf.PdfPTable datatableRS = new iTextSharp.text.pdf.PdfPTable(9);
            datatableRS.DefaultCell.Padding = 4;
            datatableRS.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 10, 10, 20, 6, 19, 5, 10, 10, 10 };
            datatableRS.SetWidths(headerwidths);
            datatableRS.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatableRS.DefaultCell.BorderWidth = 0;
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("Return Sent", fntTableHeader));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableRS.DefaultCell.BorderWidth = 0.5f;
            datatableRS.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            datatableRS.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);


            datatableRS.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            datatableRS.DefaultCell.BorderWidth = 1;

            datatableRS.AddCell(new iTextSharp.text.Phrase("C.cy", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("Account No.", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("A/C Name", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("Trn Code", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("Narration", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("Seq. No", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("Debit", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("Credit", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("Value Date", fnt));

            datatableRS.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatableRS.DefaultCell.BorderWidth = 1;


            decimal totalAmount = 0;

            for (int i = 0; i < myDTReturnSent.Rows.Count; i++)
            {
                decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(myDTReturnSent.Rows[i]["Amount"].ToString());
                totalAmount += transAmount;

                datatableRS.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"]), fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase(myDTReturnSent.Rows[i]["TraceNumber"].ToString(), fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase(myDTReturnSent.Rows[i]["EntryDate"].ToString(), fnt));

                datatableRS.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(myDTReturnSent.Rows[i]["DFIAccountNo"].ToString()), fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase(myDTReturnSent.Rows[i]["AccountName"].ToString(), fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase(myDTReturnSent.Rows[i]["TraceNumber"].ToString(), fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatableRS.AddCell(new iTextSharp.text.Phrase(myDTReturnSent.Rows[i]["EntryDate"].ToString(), fnt));
            }

            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("Total (BDT)", fntbld));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));


            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("Maker", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableRS.AddCell(new iTextSharp.text.Phrase("Input Date", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableRS.AddCell(new iTextSharp.text.Phrase("", fnt));
            document.Add(datatableRS);
            document.NewPage();
        }

        private static void PrintTransactionReceived(Document document, Font fnt, Font fntbld, Font fntTableHeader, iTextSharp.text.pdf.PdfPTable headertable)
        {
            document.Add(headertable);

            iTextSharp.text.pdf.PdfPTable datatableTR = new iTextSharp.text.pdf.PdfPTable(9);
            datatableTR.DefaultCell.Padding = 4;
            datatableTR.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 10, 10, 20, 6, 19, 5, 10, 10, 10 };
            datatableTR.SetWidths(headerwidths);
            datatableTR.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatableTR.DefaultCell.BorderWidth = 0;
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("Transaction Received", fntTableHeader));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableTR.DefaultCell.BorderWidth = 0.5f;
            datatableTR.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            datatableTR.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);


            datatableTR.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            datatableTR.DefaultCell.BorderWidth = 1;

            datatableTR.AddCell(new iTextSharp.text.Phrase("C.cy", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("Account No.", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("A/C Name", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("Trn Code", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("Narration", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("Seq. No", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("Debit", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("Credit", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("Value Date", fnt));

            datatableTR.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatableTR.DefaultCell.BorderWidth = 1;

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{

            decimal totalAmount = 0;

            for (int i = 0; i < myDTTransactionReceived.Rows.Count; i++)
            {
                decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(myDTTransactionReceived.Rows[i]["Amount"].ToString());
                totalAmount += transAmount;

                datatableTR.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"]), fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase(myDTTransactionReceived.Rows[i]["TraceNumber"].ToString(), fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase(myDTTransactionReceived.Rows[i]["EntryDate"].ToString(), fnt));

                datatableTR.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(myDTTransactionReceived.Rows[i]["DFIAccountNo"].ToString()), fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase(myDTTransactionReceived.Rows[i]["AccountName"].ToString(), fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase(myDTTransactionReceived.Rows[i]["TraceNumber"].ToString(), fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatableTR.AddCell(new iTextSharp.text.Phrase(myDTTransactionReceived.Rows[i]["EntryDate"].ToString(), fnt));
            }

            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("Total (BDT)", fntbld));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));


            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("Maker", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableTR.AddCell(new iTextSharp.text.Phrase("Input Date", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableTR.AddCell(new iTextSharp.text.Phrase("", fnt));
            document.Add(datatableTR);
            document.NewPage();
        }

        private static void PrintTransactionSent(Document document, Font fnt, Font fntbld, Font fntTableHeader, iTextSharp.text.pdf.PdfPTable headertable)
        {
            document.Add(headertable);

            iTextSharp.text.pdf.PdfPTable datatableTS = new iTextSharp.text.pdf.PdfPTable(9);
            datatableTS.DefaultCell.Padding = 4;
            datatableTS.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 10, 10, 20, 6, 19, 5, 10, 10, 10 };
            datatableTS.SetWidths(headerwidths);
            datatableTS.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);


            datatableTS.DefaultCell.BorderWidth = 0;
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("Transaction Sent", fntTableHeader));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableTS.DefaultCell.BorderWidth = 0.5f;
            datatableTS.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            datatableTS.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

            datatableTS.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            datatableTS.DefaultCell.BorderWidth = 1;

            datatableTS.AddCell(new iTextSharp.text.Phrase("C.cy", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("Account No.", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("A/C Name", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("Trn Code", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("Narration", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("Seq. No", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("Debit", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("Credit", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("Value Date", fnt));

            datatableTS.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatableTS.DefaultCell.BorderWidth = 1;

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{

            decimal totalAmount = 0;

            for (int i = 0; i < myDTTransactionSent.Rows.Count; i++)
            {
                decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(myDTTransactionSent.Rows[i]["Amount"].ToString());
                totalAmount += transAmount;
                datatableTS.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(myDTTransactionSent.Rows[i]["IdNumber"].ToString()), fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase((string)myDTTransactionSent.Rows[i]["AccountName"], fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase(((string)myDTTransactionSent.Rows[i]["TraceNumber"]).ToString(), fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase((string)myDTTransactionSent.Rows[i]["EntryDate"], fnt));

                datatableTS.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"]), fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase(((string)myDTTransactionSent.Rows[i]["TraceNumber"]).ToString(), fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatableTS.AddCell(new iTextSharp.text.Phrase((string)myDTTransactionSent.Rows[i]["EntryDate"], fnt));
            }

            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("Total (BDT)", fntbld));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("Maker", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatableTS.AddCell(new iTextSharp.text.Phrase("Input Date", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatableTS.AddCell(new iTextSharp.text.Phrase("", fnt));
            
            document.Add(datatableTS);
            document.NewPage();
        }

    }
}
