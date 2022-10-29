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
using FloraSoft;


namespace EFTN
{
    public partial class PrintCustomerAdvice : System.Web.UI.Page
    {
        private static DataTable dtSettlementReport = new DataTable();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2,'0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');

                string OriginBankCode = ConfigurationManager.AppSettings["OriginBank"].ToString().Substring(0, 3);

                if (OriginBankCode.Equals("185"))
                {
                    btnPrintVoucher.Visible = false;
                    BindBranchesForRupali();

                    if (ddListBranch.Items.Count == 1)
                    {
                        ddListReportType.Visible = false;
                        ddListTransactionType.Visible = false;
                        ddListDhakaNonDhaka.Visible = false;
                    }
                    else
                    {
                        ddListDhakaNonDhaka.Visible = true;
                    }
                }
                else
                {
                    BindBranches();

                    ddListReportType.Visible = false;
                    ddListDhakaNonDhaka.Visible = false;
                    //ddListTransactionType.Visible = false;
                }

                //if (OriginBankCode.Equals("135"))
                //{
                //    btnExportToText.Visible = true;
                //    btnExpotToCSV.Visible = true;
                //}
                //else
                //{
                    btnExportToText.Visible = false;
                    btnExpotToCSV.Visible = false;
                //}
            }
        }

        private void BindBranches()
        {
            string RoutingNoWebConfig = ConfigurationManager.AppSettings["OriginBank"].ToString();
            int BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);

            PrintCustomerAdviceDB printCustomerAdviceDB = new PrintCustomerAdviceDB();
            DataTable dtBranch = printCustomerAdviceDB.GetBranchesByRoutingNo(RoutingNoWebConfig, BranchID);
            ddListBranch.DataSource = dtBranch;
            ddListBranch.DataBind();
            if (dtBranch.Rows.Count > 1)
            {
                ddListBranch.Items.Add(new System.Web.UI.WebControls.ListItem("All", "0"));
                ddListBranch.SelectedIndex = ddListBranch.Items.Count - 1;
            }
        }

        private void BindBranchesForRupali()
        {
            string RoutingNoWebConfig = ConfigurationManager.AppSettings["OriginBank"].ToString();
            int BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);

            int DhakaBranch = EFTN.Utility.ParseData.StringToInt(ddListDhakaNonDhaka.SelectedValue);

            PrintCustomerAdviceDB printCustomerAdviceDB = new PrintCustomerAdviceDB();
            DataTable dtBranch = printCustomerAdviceDB.GetBranchesByRoutingNoForRupali(RoutingNoWebConfig, BranchID, DhakaBranch);

            ddListBranch.DataSource = dtBranch;
            ddListBranch.DataBind();
            if (dtBranch.Rows.Count > 1)
            {
                ddListBranch.Items.Add(new System.Web.UI.WebControls.ListItem("All", "0"));
                ddListBranch.SelectedIndex = ddListBranch.Items.Count - 1;
            }
        }

        protected void ddListDhakaNonDhaka_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBranchesForRupali();
        }

        protected void btnPrintAdvice_Click(object sender, EventArgs e)
        {
            string OriginBankCode = ConfigurationManager.AppSettings["OriginBank"].ToString().Substring(0, 3);
            string BankName = ConfigurationManager.AppSettings["CompanyName"].ToString();

            string fileName = "Advice Report - " + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            if (OriginBankCode.Equals("135"))
            {
                printPDFAviceForJanata(fileName, BankName, OriginBankCode);
            }
            else if (OriginBankCode.Equals("185"))
            {
                printPDFAviceForRupali(fileName, BankName, OriginBankCode);
            }
            //PrintDetailSettlementReport(fileName);
        }

        private DataTable GetDataForBranchAdvice()
        {
            PrintCustomerAdviceDB printCustomerAdviceDB = new PrintCustomerAdviceDB();
            return printCustomerAdviceDB.GetReceivedEDRForBranchAdvice(ParseData.StringToInt(ddListBranch.SelectedValue)
                                                                , ParseData.StringToInt(ddListTransactionType.SelectedValue)
                                                                , ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'));

        }

        private DataTable GetDataForBranchAdviceForRupali(int TransactionType, int BranchID, int userID)
        {
            PrintCustomerAdviceDB printCustomerAdviceDB = new PrintCustomerAdviceDB();
            return printCustomerAdviceDB.GetReceivedEDRForBranchAdviceForRupali(BranchID
                                                                , TransactionType
                                                                , ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), userID);

        }

        //private DataTable GetDataForBranchAdviceForRupaliForByBranchID(int BranchID, int userID)
        //{
        //    PrintCustomerAdviceDB printCustomerAdviceDB = new PrintCustomerAdviceDB();
        //    return printCustomerAdviceDB.GetReceivedEDRForBranchAdviceForRupali(BranchID
        //                                                            , ParseData.StringToInt(ddListTransactionType.SelectedValue)
        //                                                            , ddlistYear.SelectedValue.PadLeft(4, '0')
        //                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
        //                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), userID);

        //}

        private void printPDFAviceForJanata(string FileName, string BankName, string OriginBankCode)
        {
            DataTable dt = GetDataForBranchAdvice();

            if (dt.Rows.Count == 0)
            {
                return;
            }

            Response.ClearContent();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

            Document document = new Document(PageSize.A4, 10, 10, 8, 8);
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

            Font fnt = new Font(Font.HELVETICA, 8);
            Font fntblue = new Font(Font.HELVETICA, 8);
            fntblue.Color = new Color(0, 0, 255);
            Font fntbld = new Font(Font.HELVETICA, 7);
            fntbld.SetStyle(Font.BOLD);

            Font headerFontBankName = new Font(Font.HELVETICA, 18);
            headerFontBankName.SetStyle(Font.BOLD);

            Font headerFontLarge = new Font(Font.HELVETICA, 13);
            headerFontLarge.SetStyle(Font.BOLD);

            Font headerFontMedium = new Font(Font.HELVETICA, 9);

            string spacer = "            -              ";

            string str = spacer;
            str = str + ConfigurationManager.AppSettings["OriginBankName"] + " All Rights Reserved" + spacer;
            str = str + "Confidential: internal use only" + spacer;
            str = str + "Powered By Flora Limited";

            HeaderFooter footer = new HeaderFooter(new Phrase(str, fnt), false);
            footer.Alignment = Element.ALIGN_CENTER;

            document.Footer = footer;
            document.Open();
            PdfContentByte pdfCB = writer.DirectContent;


            ///////////////////////////////////////
            //string AdviceCode = dt.Rows[0]["AdviceCode"].ToString();
            string ZoneName = dt.Rows[0]["ZoneName"].ToString();
            string BranchName = dt.Rows[0]["BranchName"].ToString();
            //string CurDate = DateTime.Parse(dt.Rows[0]["CurDate"].ToString()).ToString("dd/MM/yyyy");
            string Cnt = dt.Rows[0]["Cnt"].ToString();
            string Amount = dt.Rows[0]["Amount"].ToString();
            string SettlementJDate = dt.Rows[0]["SettlementJDate"].ToString();
            string CreditDebit = dt.Rows[0]["CreditDebit"].ToString();
            string OrgBranch = dt.Rows[0]["OrgBranch"].ToString();
            string OrgBranchCode = dt.Rows[0]["OrgBranchCode"].ToString();
            string RoutingNo = dt.Rows[0]["RoutingNo"].ToString();
            int branchIDFromDB = ParseData.StringToInt(dt.Rows[0]["BranchID"].ToString());
            string SolID = dt.Rows[0]["SolID"].ToString();

            //AdviceCode = AdviceCode + currentTime;

            string adviceFor = string.Empty;

            //string AdviceCode = SolID + DateTime.Parse(SettlementJDate).ToString("ddMMyy") + Cnt.PadLeft(5, '0');

            string AdviceCode = string.Empty;
            AdviceCode = dt.Rows[0]["AdviceCode"].ToString(); 
            //AdviceCode = ddListTransactionType.SelectedValue            //1 digit Credit = 1 and Debit = 2
            //          + "2"                                             //1 digit Outward = 1 and Inward = 2
            //          + SolID                                           //3 digit SolID = BranchCode
            //          + (ddlistYear.SelectedValue.Substring(2, 2))      //2 digit From Selected Year
            //          + (ddlistMonth.SelectedValue.PadLeft(2, '0'))     //2 digit From Selected Month
            //          + (ddlistDay.SelectedValue.PadLeft(2, '0'))       //2 digit From Selected Day
            //          + Cnt.PadLeft(4, '0');                            //4 digit From No Of Item
            
            int BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);

            string LogoImage = Server.MapPath("images") + "\\SCBLogo.JPG";
            PdfPCell logo = new PdfPCell();

            iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
            jpeg.Alignment = Element.ALIGN_RIGHT;

            logo.BorderWidth = 0;
            logo.AddElement(jpeg);

            /////////////////////////RespondingBrSeal/////////////
            //string LogoImageRespondingBrSeal = Server.MapPath("images") + "\\RespondingBrSeal.JPG";
            //PdfPCell logoRespondingBrSeal = new PdfPCell();

            //iTextSharp.text.Image jpegRespondingBrSeal = iTextSharp.text.Image.GetInstance(LogoImageRespondingBrSeal);
            //jpegRespondingBrSeal.Alignment = Element.ALIGN_RIGHT;

            //logoRespondingBrSeal.BorderWidth = 0;
            //logoRespondingBrSeal.AddElement(jpegRespondingBrSeal);


            /////////////////////////RespondingBrCode/////////////
            //string LogoImageRespondingBrCode = Server.MapPath("images") + "\\RespondingBrCode.JPG";
            //PdfPCell logoRespondingBrCode = new PdfPCell();

            //iTextSharp.text.Image jpegRespondingBrCode = iTextSharp.text.Image.GetInstance(LogoImageRespondingBrCode);
            //jpegRespondingBrCode.Alignment = Element.ALIGN_RIGHT;

            //logoRespondingBrCode.BorderWidth = 0;
            //logoRespondingBrCode.AddElement(jpegRespondingBrCode);


            /////////////////////////RspondingDate/////////////
            //string LogoImageRspondingDate = Server.MapPath("images") + "\\RspondingDate.JPG";
            //PdfPCell logoRspondingDate = new PdfPCell();

            //iTextSharp.text.Image jpegRspondingDate = iTextSharp.text.Image.GetInstance(LogoImageRspondingDate);
            //jpegRspondingDate.Alignment = Element.ALIGN_RIGHT;

            //logoRspondingDate.BorderWidth = 0;
            //logoRspondingDate.AddElement(jpegRspondingDate);

            //string loginID = Request.Cookies["LoginID"].Value;
            //string currentTime = System.DateTime.Now.ToString("hhmmss");

            //if (loginID.Length > 3)
            //{
            //    loginID = loginID.Substring(0, loginID.Length - 1);
            //}

            //string AdviceCode = loginID + DateTime.Parse(SettlementJDate).ToString("ddMMyy") + currentTime;
            string strAdviceHeader1 = "Electronic Fund Transfer Network";
            string strAdviceHeader2 = string.Empty;
            
            string strLocalOffice1 = string.Empty;
            string strLocalOffice2 = string.Empty;
            string strLocalOffice3 = string.Empty;
            string strLocalOffice4 = string.Empty;
            string strFrom1 = string.Empty;
            string strTo1 = string.Empty;
            string strTo2 = string.Empty;
            string strTransactionParticular1 = string.Empty;
            string strTransactionParticular2 = string.Empty;
            string strPA = string.Empty;
            if (OriginBankCode.Equals("135"))//Janata Bank
            {
                strAdviceHeader1 = BankName;
                strAdviceHeader2 = "COMPUTERIZED INTER BRANCH TRANSACTION";
                strLocalOffice1 = "NAME OF COMPUTERIZED IBT A/C. SUB-HEAD";
                strLocalOffice2 = "5023";

                strLocalOffice3 = "CODE NO. OF THE SUB-HEAD OF\nCOMPUTERIZED IBT A/C.";
                strFrom1 = "0102";
                //strTo1 = SolID;
                strTo2 = "05";
                strTransactionParticular1 = "Being the a/o BEFTN transaction received from several exchange companies/Banks to our";
                strTransactionParticular2="branch a/c holders as per auto statement stated in website IP Adds: 202.51.179.178/login.aspx";
                strLocalOffice4 = "NAME OF COMPUTERIZED IBT A/C. SUB-HEAD";
                strPA = "Officer (PA No.)";
            }
            else if (OriginBankCode.Equals("185"))//Rupali Bank
            {
                strAdviceHeader1 = "Electronic Fund Transfer Network";
                strAdviceHeader2 = "INTER BRANCH TRANSACTION";
                strLocalOffice1 = "NAME OF BEFTN A/C";
                if (RoutingNo.Substring(3, 2).Equals("26")
                    || RoutingNo.Substring(3, 2).Equals("27"))
                {
                    strLocalOffice2 = "101003";
                }
                else
                {
                    strLocalOffice2 = "101002";
                }
                strLocalOffice3 = "CODE NO. OF THE SUB-HEAD OF\nBEFTN A/C";
                strFrom1 = "0018";
                SolID = "0067";
                strTo2 = "12";
                strTransactionParticular1 = "Being the a/o BEFTN transaction received from Banks to our branch a/c holders";
                strTransactionParticular2 = "as per auto statement stated in Rupali Bank EFT System";
                strLocalOffice4 = "NAME OF EFTN A/C.";
                strPA = "Officer (IBS No.)";
            }


            if (OriginBankCode.Equals("135"))//Janata Bank
            {
                PrintCustomerAdvicePage(strAdviceHeader1, document, fnt, headerFontLarge, headerFontMedium, headerFontBankName,
                                        AdviceCode, ZoneName, BranchName, Cnt, Amount,
                                        OrgBranch, adviceFor, RoutingNo, logo, pdfCB, branchIDFromDB, SolID
                                        , strLocalOffice1, strLocalOffice2, strLocalOffice3, strLocalOffice4
                                        , strFrom1, strTo2, strTransactionParticular1
                                        , strTransactionParticular2, strPA, strAdviceHeader1
                                        , strAdviceHeader2);
            }
            else if (OriginBankCode.Equals("185"))//Rupali Bank
            {
                int length = 3;
                for (int i = 0; i < length; i++)
                {
                    if (i == 1)
                    {
                        document.NewPage();
                        adviceFor = "Head Office Copy";
                    }
                    else if (i == 2)
                    {
                        document.NewPage();
                        adviceFor = "Office Copy";
                    }
                    PrintCustomerAdvicePage(strAdviceHeader1, document, fnt, headerFontLarge, headerFontMedium, headerFontBankName,
                                            AdviceCode, ZoneName, BranchName, Cnt, Amount,
                                            OrgBranch, adviceFor, RoutingNo, logo, pdfCB, branchIDFromDB, SolID
                                            , strLocalOffice1, strLocalOffice2, strLocalOffice3, strLocalOffice4
                                            , strFrom1, strTo2, strTransactionParticular1
                                            , strTransactionParticular2, strPA, strAdviceHeader1
                                            , strAdviceHeader2);
                }

            }
            InsertAdvicePrintToDB(BranchID);

            document.Close();
            Response.End();
        }

        private void InsertAdvicePrintToDB(int BranchID)
        {
            PrintCustomerAdviceDB printCustAdvDB = new PrintCustomerAdviceDB();
            printCustAdvDB.InsertAdvicePrintTrack(  BranchID,
                                                    System.DateTime.Now,
                                                    DateTime.Parse(ddlistYear.SelectedValue
                                                                   + "-" + ddlistMonth.SelectedValue
                                                                   + "-" + ddlistDay.SelectedValue));
        }

        private void PrintCustomerAdvicePage(string BankName, Document document, Font fnt,
                                            Font headerFontLarge, Font headerFontMedium, Font headerFontBankName, string AdviceCode, 
                                            string ZoneName, string BranchName, string Cnt,
                                            string Amount, string OrgBranch, string adviceFor,
                                            string RoutingNo, PdfPCell logo, PdfContentByte pdfCB,
                                            int branchIDFromDB, string SolID,
                                            string strLocalOffice1, string strLocalOffice2,
                                            string strLocalOffice3, string strLocalOffice4,
                                            string strFrom1, string strTo2,
                                            string strTransactionParticular1,
                                            string strTransactionParticular2, string strPA, 
                                            string strAdviceHeader1, string strAdviceHeader2)
        {
            ///////////////////////////////////////
            iTextSharp.text.pdf.PdfPTable headertable = new iTextSharp.text.pdf.PdfPTable(3);
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            headertable.DefaultCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            headertable.DefaultCell.Padding = 0;
            headertable.WidthPercentage = 80;
            headertable.DefaultCell.Border = 0;
            float[] widthsAtHeader = { 28, 44, 28 };
            headertable.SetWidths(widthsAtHeader);

            string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;

            headertable.AddCell(logo);
            headertable.AddCell(new Phrase(strAdviceHeader1, headerFontBankName));
            headertable.AddCell(new Phrase("Advice No:\n" + AdviceCode + "\n\n" + adviceFor, headerFontMedium));

            headertable.AddCell(new Phrase("\n", fnt));
            headertable.AddCell(new Phrase("\n" + strAdviceHeader2 + "\n\n" + ddListTransactionType.SelectedItem.Text.ToUpper() + " ADVICE ", fnt));
            headertable.AddCell(new Phrase("\n", fnt));
            //headertable.AddCell(new Phrase("Advice No:\n" + AdviceCode + "\n\n" + adviceFor, headerFontMedium));

            document.Add(headertable);


            ////////////////////////////////
            iTextSharp.text.pdf.PdfPTable infoTable = new iTextSharp.text.pdf.PdfPTable(3);
            infoTable.DefaultCell.Border = Rectangle.NO_BORDER;
            infoTable.DefaultCell.VerticalAlignment = Cell.ALIGN_TOP;
            infoTable.DefaultCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            infoTable.DefaultCell.Padding = 0;
            infoTable.WidthPercentage = 99;
            infoTable.DefaultCell.Border = 0;
            float[] widthsInfoHeader = { 35, 30, 35 };
            infoTable.SetWidths(widthsInfoHeader);

            infoTable.AddCell(new Phrase("Local Office", fnt));
            infoTable.AddCell(new Phrase("", fnt));
            infoTable.AddCell(new Phrase(strLocalOffice2, fnt));

            infoTable.AddCell(new Phrase("_______________________________________", fnt));
            infoTable.AddCell(new Phrase("", fnt));
            infoTable.AddCell(new Phrase("__________________________________", fnt));

            infoTable.AddCell(new Phrase(strLocalOffice1, fnt));
            infoTable.AddCell(new Phrase("", fnt));
            infoTable.AddCell(new Phrase(strLocalOffice3, fnt));

            document.Add(infoTable);


            ////////////////////////////////
            iTextSharp.text.pdf.PdfPTable infoTable2 = new iTextSharp.text.pdf.PdfPTable(5);
            infoTable2.DefaultCell.Border = Rectangle.NO_BORDER;
            infoTable2.DefaultCell.VerticalAlignment = Cell.ALIGN_TOP;
            infoTable2.DefaultCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            infoTable2.DefaultCell.Padding = 0;
            infoTable2.WidthPercentage = 99;
            infoTable2.DefaultCell.Border = 0;
            float[] widthsInfo2Header = { 12, 22, 22, 22, 22 };
            infoTable2.SetWidths(widthsInfo2Header);

            infoTable2.AddCell(new Phrase("\n\n", fnt));
            infoTable2.AddCell(new Phrase("\n\n", fnt));
            infoTable2.AddCell(new Phrase("\n\n", fnt));
            infoTable2.AddCell(new Phrase("\n\n", fnt));
            infoTable2.AddCell(new Phrase("\n\n", fnt));

            infoTable2.AddCell(new Phrase("\n", fnt));
            infoTable2.AddCell(new Phrase("Local Office\n", fnt));
            infoTable2.AddCell(new Phrase("Local Office\n", fnt));
            infoTable2.AddCell(new Phrase(strFrom1, fnt));
            infoTable2.AddCell(new Phrase(settlementDate, fnt));

            infoTable2.AddCell(new Phrase("", fnt));
            infoTable2.AddCell(new Phrase("___________________________\n", fnt));
            infoTable2.AddCell(new Phrase("___________________________\n", fnt));
            infoTable2.AddCell(new Phrase("___________________________\n", fnt));
            infoTable2.AddCell(new Phrase("___________________________\n", fnt));

            infoTable2.AddCell(new Phrase("FROM\n", fnt));
            infoTable2.AddCell(new Phrase("ORIGINATING BRANCH NAME\n", fnt));
            infoTable2.AddCell(new Phrase("NAME OF THE REGION OF\nORIGINATING BRANCH\n", fnt));
            infoTable2.AddCell(new Phrase("ORIGINATING BRANCH CODE\n", fnt));
            infoTable2.AddCell(new Phrase("ORIGINATING DATE\n", fnt));

            infoTable2.AddCell(new Phrase("\n\n", fnt));
            infoTable2.AddCell(new Phrase("\n\n", fnt));
            infoTable2.AddCell(new Phrase("\n\n", fnt));
            infoTable2.AddCell(new Phrase("\n\n", fnt));
            infoTable2.AddCell(new Phrase("\n\n", fnt));

            infoTable2.AddCell(new Phrase("\n", fnt));
            infoTable2.AddCell(new Phrase(BranchName, fnt));
            infoTable2.AddCell(new Phrase(ZoneName, fnt));
            infoTable2.AddCell(new Phrase(SolID, fnt));
            infoTable2.AddCell(new Phrase(strTo2, fnt));

            infoTable2.AddCell(new Phrase("", fnt));
            infoTable2.AddCell(new Phrase("___________________________\n", fnt));
            infoTable2.AddCell(new Phrase("___________________________\n", fnt));
            infoTable2.AddCell(new Phrase("___________________________\n", fnt));
            infoTable2.AddCell(new Phrase("___________________________\n", fnt));

            infoTable2.AddCell(new Phrase("TO\n", fnt));
            infoTable2.AddCell(new Phrase("NAME OF THE RESPONDING\n BRANCH \n", fnt));
            infoTable2.AddCell(new Phrase("NAME OF THE REGION OF\nRESPONDING BRANCH\n", fnt));
            infoTable2.AddCell(new Phrase("RESPONDING BRANCH CODE\n\n", fnt));
            infoTable2.AddCell(new Phrase("TRANSACTION CODE\n\n", fnt));

            infoTable2.AddCell(new Phrase("\n\n", fnt));
            infoTable2.AddCell(new Phrase("\n\n", fnt));
            infoTable2.AddCell(new Phrase("\n\n", fnt));
            infoTable2.AddCell(new Phrase("\n\n", fnt));
            infoTable2.AddCell(new Phrase("\n\n", fnt));

            document.Add(infoTable2);

            ////////////////////////////////
            iTextSharp.text.pdf.PdfPTable infoTable3 = new iTextSharp.text.pdf.PdfPTable(2);
            infoTable3.DefaultCell.Border = 5;
            infoTable3.DefaultCell.VerticalAlignment = Cell.ALIGN_TOP;
            infoTable3.DefaultCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            infoTable3.DefaultCell.Padding = 4;
            infoTable3.WidthPercentage = 90;
            float[] widthsInfoHeader3 = { 80, 20 };
            infoTable3.SetWidths(widthsInfoHeader3);
            infoTable3.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

            infoTable3.AddCell(new Phrase("                     TRANSACTION PARTICULARS", headerFontLarge));
            infoTable3.AddCell(new Phrase("     AMOUNT", headerFontLarge));

            infoTable3.AddCell(new Phrase(strTransactionParticular1, headerFontMedium));
            infoTable3.AddCell(new Phrase("", headerFontMedium));

            infoTable3.AddCell(new Phrase(strTransactionParticular2, headerFontMedium));
            infoTable3.AddCell(new Phrase("", headerFontMedium));

            infoTable3.AddCell(new Phrase("on date:" + settlementDate, headerFontMedium));
            infoTable3.AddCell(new Phrase("", headerFontMedium));

            infoTable3.AddCell(new Phrase("Total Taka (in word): " + EFTN.Utility.NumberToWordConverter.GetAmountInWords(Amount.ToString()), headerFontMedium));
            infoTable3.AddCell(new Phrase(EFTN.Utility.ParseData.StringToDouble(Amount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture).PadLeft(30, ' '), headerFontMedium));

            infoTable3.AddCell(new Phrase("\nTotal Item : " + Cnt, headerFontMedium));
            infoTable3.AddCell(new Phrase("", headerFontMedium));

            document.Add(infoTable3);

            ////////////////////////////////
            iTextSharp.text.pdf.PdfPTable infoTable4 = new iTextSharp.text.pdf.PdfPTable(2);
            infoTable4.DefaultCell.Border = Rectangle.NO_BORDER;
            infoTable4.DefaultCell.VerticalAlignment = Cell.ALIGN_TOP;
            infoTable4.DefaultCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            infoTable4.DefaultCell.Padding = 0;
            infoTable4.WidthPercentage = 90;
            infoTable4.DefaultCell.Border = 0;
            float[] widthsInfoHeader4 = { 80, 20 };
            infoTable4.SetWidths(widthsInfoHeader4);

            infoTable4.AddCell(new Phrase("Computer Generated advice, originating branch signature does not require\n\n", headerFontMedium));
            infoTable4.AddCell(new Phrase("", fnt));
            document.Add(infoTable4);

            ////////////////////////////////
            iTextSharp.text.pdf.PdfPTable infoTable5 = new iTextSharp.text.pdf.PdfPTable(4);
            infoTable5.DefaultCell.Border = Rectangle.NO_BORDER;
            infoTable5.DefaultCell.VerticalAlignment = Cell.ALIGN_TOP;
            infoTable5.DefaultCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            infoTable5.DefaultCell.Padding = 0;
            infoTable5.WidthPercentage = 99;
            infoTable5.DefaultCell.Border = 0;
            float[] widthsInfoHeader5 = { 20, 40, 20, 20 };
            infoTable5.SetWidths(widthsInfoHeader5);

            infoTable5.AddCell(new Phrase("RESPONDING BR. SEAL", headerFontMedium));
            if (ddListTransactionType.SelectedValue.Equals("1"))
            {
                infoTable5.AddCell(new Phrase("DEBIT          Local Office", headerFontMedium));
            }
            else
            {
                infoTable5.AddCell(new Phrase("CREDIT         Local Office", headerFontMedium));
            }
            infoTable5.AddCell(new Phrase("RESPONDING BR. CODE", headerFontMedium));
            infoTable5.AddCell(new Phrase("RESPONDING DATE", headerFontMedium));

            //infoTable5.AddCell(logoRespondingBrSeal);
            //infoTable5.AddCell(new Phrase("________________________________________________________\n", fnt));
            //infoTable5.AddCell(logoRespondingBrCode);
            //infoTable5.AddCell(logoRespondingDate);
            pdfCB.Rectangle(15f, 420f, 100f, 25f);
            

            pdfCB.Rectangle(355f, 420f, 100f, 25f);

            pdfCB.Rectangle(470f, 420f, 30f, 25f);
            pdfCB.Rectangle(500f, 420f, 30f, 25f);
            pdfCB.Rectangle(530f, 420f, 30f, 25f);
            pdfCB.Stroke();

            infoTable5.AddCell(new Phrase("\n", fnt));
            infoTable5.AddCell(new Phrase("______________________________________________\n", fnt));
            infoTable5.AddCell(new Phrase("\n", fnt));
            infoTable5.AddCell(new Phrase("\n", fnt));


            infoTable5.AddCell(new Phrase("\n", fnt));
            infoTable5.AddCell(new Phrase(strLocalOffice4, headerFontMedium));
            infoTable5.AddCell(new Phrase("\n", fnt));
            infoTable5.AddCell(new Phrase("\n", fnt));

            infoTable5.AddCell(new Phrase("\n", fnt));
            infoTable5.AddCell(new Phrase("Amount and Particulars as above\n", headerFontMedium));
            infoTable5.AddCell(new Phrase("\n", fnt));
            infoTable5.AddCell(new Phrase("\n", fnt));
            document.Add(infoTable5);

            ////////////////////////////////
            iTextSharp.text.pdf.PdfPTable infoTable6 = new iTextSharp.text.pdf.PdfPTable(2);
            infoTable6.DefaultCell.Border = Rectangle.NO_BORDER;
            infoTable6.DefaultCell.VerticalAlignment = Cell.ALIGN_TOP;
            infoTable6.DefaultCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            infoTable6.DefaultCell.Padding = 0;
            infoTable6.WidthPercentage = 99;
            infoTable6.DefaultCell.Border = 0;
            float[] widthsInfoHeader6 = { 50, 50 };
            infoTable6.SetWidths(widthsInfoHeader6);

            infoTable6.AddCell(new Phrase("\n\n\n\n\n\n", headerFontMedium));
            infoTable6.AddCell(new Phrase("\n\n\n\n\n\n", headerFontMedium));

            infoTable6.AddCell(new Phrase("________________________", headerFontMedium));
            infoTable6.AddCell(new Phrase("________________________", headerFontMedium));

            infoTable6.AddCell(new Phrase(strPA, headerFontMedium));
            infoTable6.AddCell(new Phrase(strPA, headerFontMedium));
            document.Add(infoTable6);
        }

        protected void btnPrintVoucher_Click(object sender, EventArgs e)
        {
            string originBankRoutingNo = ConfigurationManager.AppSettings["OriginBank"].ToString();

            //string loginID = Request.Cookies["LoginID"].Value;

            //if (loginID.Length > 3)
            //{
            //    loginID = loginID.Substring(0, loginID.Length - 1);
            //}


            if (originBankRoutingNo.Substring(0, 3).Equals("135"))
            {
                string BankName = "Janata Bank Limited";
                string fileName = "Voucher Report - " + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                printPDFVoucherForJanata(fileName, BankName);
            }
            //else if (originBankRoutingNo.Substring(0, 3).Equals("185"))
            //{
            //    string BankName = "Rupali Bank Limited";
            //    string fileName = "Voucher Report - " + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            //    printPDFVoucherForJanata(fileName, BankName);
            //}
        }

        private DataTable GetDataForVoucher()
        {
            PrintCustomerAdviceDB printCustomerAdviceDB = new PrintCustomerAdviceDB();
            return printCustomerAdviceDB.GetReceivedEDRForVoucher(ParseData.StringToInt(ddListBranch.SelectedValue)
                                                                , ParseData.StringToInt(ddListTransactionType.SelectedValue)
                                                                , ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'));
        }

        private void printPDFVoucherForJanata(string FileName, string BankName)
        {
            DataTable dt = GetDataForVoucher();

            if (dt.Rows.Count == 0)
            {
                return;
            }

            Response.ClearContent();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

            Document document = new Document(PageSize.A4, 10, 10, 8, 8);
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

            Font fnt = new Font(Font.HELVETICA, 8);
            Font fntblue = new Font(Font.HELVETICA, 8);
            fntblue.Color = new Color(0, 0, 255);
            Font fntbld = new Font(Font.HELVETICA, 8);
            fntbld.SetStyle(Font.BOLD);

            Font headerFontBankName = new Font(Font.HELVETICA, 18);
            headerFontBankName.SetStyle(Font.BOLD);

            Font headerFontLarge = new Font(Font.HELVETICA, 13);
            headerFontLarge.SetStyle(Font.BOLD);

            Font headerFontMedium = new Font(Font.HELVETICA, 9);
            //Font headerFontLarge = new Font(Font.HELVETICA, 18);
            headerFontLarge.SetStyle(Font.BOLD);
            //Font headerFontMedium = new Font(Font.HELVETICA, 11);

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
            PdfPCell logo = new PdfPCell();

            iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
            jpeg.Alignment = Element.ALIGN_RIGHT;

            logo.BorderWidth = 0;
            logo.AddElement(jpeg);

            ///////////////////////////////////////
            for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
            {
                string ReceiverName = dt.Rows[rowNum]["ReceiverName"].ToString();
                string TraceNumber = dt.Rows[rowNum]["TraceNumber"].ToString();
                string SendingBankName = dt.Rows[rowNum]["BankName"].ToString();
                string CurDate = System.DateTime.Now.ToString();
                string Amount = dt.Rows[rowNum]["Amount"].ToString();
                string CreditDebit = dt.Rows[rowNum]["CreditDebit"].ToString();
                string RoutingNo = dt.Rows[rowNum]["ReceivingBankRoutNo"].ToString();
                string DFIAccountNo = dt.Rows[rowNum]["DFIAccountNo"].ToString();
                string BranchName = dt.Rows[rowNum]["BranchName"].ToString();
                string PaymentInfo = dt.Rows[rowNum]["PaymentInfo"].ToString();
                string SolID = dt.Rows[rowNum]["SolID"].ToString();
                string AdviceCode = dt.Rows[rowNum]["AdviceCode"].ToString();

                ///////////////////////////////////////
                iTextSharp.text.pdf.PdfPTable headertable = new iTextSharp.text.pdf.PdfPTable(3);
                headertable.DefaultCell.Border = Rectangle.NO_BORDER;
                headertable.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
                headertable.DefaultCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                headertable.DefaultCell.Padding = 0;
                headertable.WidthPercentage = 99;
                headertable.DefaultCell.Border = 0;
                float[] widthsAtHeader = { 28, 44, 28 };
                headertable.SetWidths(widthsAtHeader);

                iTextSharp.text.pdf.PdfPTable headerAccountNo = new iTextSharp.text.pdf.PdfPTable(3);
                headerAccountNo.DefaultCell.Border = Rectangle.NO_BORDER;
                headerAccountNo.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
                headerAccountNo.DefaultCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                headerAccountNo.DefaultCell.Padding = 0;
                headerAccountNo.WidthPercentage = 99;
                headerAccountNo.DefaultCell.Border = 0;
                //float[] widthsAtHeader = { 28, 44, 28 };
                headerAccountNo.SetWidths(widthsAtHeader);

                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;

                headertable.AddCell(logo);
                headertable.AddCell(new Phrase(BankName + "\n\n", headerFontBankName));
                headertable.AddCell(new Phrase("", fnt));

                headertable.AddCell(new Phrase("", fnt));
                headertable.AddCell(new Phrase(BranchName, headerFontMedium));
                headertable.AddCell(new Phrase("", fnt));

                headertable.AddCell(new Phrase("", fnt));
                headertable.AddCell(new Phrase("(Code: " + AdviceCode + ")\n\n", headerFontMedium));
                headertable.AddCell(new Phrase("", fnt));



                headerAccountNo.AddCell(new Phrase(ddListTransactionType.SelectedItem.Text + " A/C No. " + DFIAccountNo + "\n\n", headerFontMedium));
                headerAccountNo.AddCell(new Phrase("\n\n", fnt));
                headerAccountNo.AddCell(new Phrase("\n\n", fnt));


                if (rowNum < dt.Rows.Count)
                {
                    if (rowNum % 3 == 0)
                    {
                        document.NewPage();
                        document.Add(headertable);
                    }
                }



                document.Add(headerAccountNo);

                ////////////////////////////////
                iTextSharp.text.pdf.PdfPTable infoTable3 = new iTextSharp.text.pdf.PdfPTable(2);
                infoTable3.DefaultCell.Border = 5;
                infoTable3.DefaultCell.VerticalAlignment = Cell.ALIGN_TOP;
                infoTable3.DefaultCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                infoTable3.DefaultCell.Padding = 4;
                infoTable3.WidthPercentage = 99;
                float[] widthsInfoHeader3 = { 70, 30 };
                infoTable3.SetWidths(widthsInfoHeader3);
                infoTable3.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                infoTable3.AddCell(new Phrase("                            Description", headerFontLarge));
                infoTable3.AddCell(new Phrase("            Amount", headerFontLarge));

                infoTable3.AddCell(new Phrase("Benificiary Name : " + ReceiverName, headerFontMedium));
                infoTable3.AddCell(new Phrase("", headerFontMedium));

                infoTable3.AddCell(new Phrase("TraceNumber : " + TraceNumber + ", " + SendingBankName + ",", headerFontMedium));
                infoTable3.AddCell(new Phrase(EFTN.Utility.ParseData.StringToDouble(Amount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture).PadLeft(30, ' '), headerFontMedium));

                infoTable3.AddCell(new Phrase("Entry Desc: " + PaymentInfo, headerFontMedium));
                infoTable3.AddCell(new Phrase("", headerFontMedium));

                infoTable3.AddCell(new Phrase("Total: ", headerFontMedium));
                infoTable3.AddCell(new Phrase(EFTN.Utility.ParseData.StringToDouble(Amount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture).PadLeft(30, ' '), headerFontMedium));

                infoTable3.AddCell(new Phrase("Total Taka (in word): " + EFTN.Utility.NumberToWordConverter.GetAmountInWords(Amount.ToString()), headerFontMedium));
                infoTable3.AddCell(new Phrase("", headerFontMedium));

                document.Add(infoTable3);

                ////////////////////////////////
                float[] widthsInfoHeader = { 50, 50};

                iTextSharp.text.pdf.PdfPTable infoTable4 = new iTextSharp.text.pdf.PdfPTable(2);
                infoTable4.DefaultCell.Border = Rectangle.NO_BORDER;
                infoTable4.DefaultCell.VerticalAlignment = Cell.ALIGN_TOP;
                infoTable4.DefaultCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                infoTable4.DefaultCell.Padding = 0;
                infoTable4.WidthPercentage = 99;
                infoTable4.DefaultCell.Border = 0;
                infoTable4.SetWidths(widthsInfoHeader);

                infoTable4.AddCell(new Phrase("\n\n\n\n", fnt));
                infoTable4.AddCell(new Phrase("\n\n\n\n", fnt));

                infoTable4.AddCell(new Phrase("________________________________\n\n", fnt));
                infoTable4.AddCell(new Phrase("________________________________\n\n", fnt));

                infoTable4.AddCell(new Phrase("Officer (PA No.)", headerFontMedium));
                infoTable4.AddCell(new Phrase("Officer (PA No.)", headerFontMedium));

                infoTable4.AddCell(new Phrase("\n\n\n\n\n\n", fnt));
                infoTable4.AddCell(new Phrase("\n\n\n\n\n\n", fnt));

                document.Add(infoTable4);
            }
            document.Close();
            Response.End();
        }

        public DataTable GetBranches()
        {
            BranchesDB branchDB = new BranchesDB();

            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            return branchDB.GetBranchesByBankCode(bankCode);
        }

        public class AdviceData
        {
            public string ZoneName;
            public string BranchName;
            public string Cnt;
           // public string Amount;
            public double Amount;

            
            public string SettlementJDate;
            public string CreditDebit ;
            public string OrgBranch ;
            public string OrgBranchCode ;
            public string RoutingNo ;
            public int branchIDFromDB ;
            public string SolID ;
            public string adviceFor;
            public string AdviceCode;
            public string CounterSignator;
            public string Signator;
        }

        private void printPDFAviceForRupali(string FileName, string BankName, string OriginBankCode)
        {
            int BranchID = ParseData.StringToInt(ddListBranch.SelectedValue);
            int userID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            int TransactionType = ParseData.StringToInt(ddListTransactionType.SelectedValue);

            SignatureDB signatureDB = new SignatureDB();
            SqlDataReader sqlDRSign = signatureDB.GetSignator(ddlistYear.SelectedValue.PadLeft(4, '0')
                                           + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                           + ddlistDay.SelectedValue.PadLeft(2, '0'));

            string signator = string.Empty;
            string counterSignator = string.Empty;
            while(sqlDRSign.Read())
            {
                signator = sqlDRSign["Signator"].ToString();
                counterSignator = sqlDRSign["CounterSignator"].ToString();
            }

            if (sqlDRSign.HasRows)
            {
                
                if (ddListBranch.Items.Count == 1)
                {
                    GenerateBranchVoucher(BranchID, userID, signator, counterSignator);
                }
                else if (ddListBranch.Items.Count > 1)
                {
                    if (ddListBranch.SelectedValue.Equals("0"))
                    {
                        GenerateAllBranchVoucherForTruncation(userID, signator, counterSignator);
                    }
                    else
                    {
                        DataTable dt = GetDataForBranchAdviceForRupali(TransactionType, BranchID, userID);

                        if (dt.Rows.Count > 0)
                        {
                            AdviceData adv = new AdviceData();
                            ///////////////////////////////////////
                            //string AdviceCode = dt.Rows[0]["AdviceCode"].ToString();
                            //if(sqlDRSign.Read()){
                            //    adv.Signator = sqlDRSign[
                            //}
                            adv.ZoneName = dt.Rows[0]["ZoneName"].ToString();
                            adv.BranchName = dt.Rows[0]["BranchName"].ToString();
                            //string CurDate = DateTime.Parse(dt.Rows[0]["CurDate"].ToString()).ToString("dd/MM/yyyy");
                            adv.Cnt = dt.Rows[0]["Cnt"].ToString();
                            // adv.Amount =  5000;

                            adv.Amount = ParseData.StringToDouble(dt.Rows[0]["Amount"].ToString());
                            adv.SettlementJDate = dt.Rows[0]["SettlementJDate"].ToString();
                            adv.CreditDebit = dt.Rows[0]["CreditDebit"].ToString();
                            adv.OrgBranch = dt.Rows[0]["OrgBranch"].ToString();
                            adv.OrgBranchCode = dt.Rows[0]["OrgBranchCode"].ToString();
                            adv.RoutingNo = dt.Rows[0]["RoutingNo"].ToString();
                            adv.branchIDFromDB = ParseData.StringToInt(dt.Rows[0]["BranchID"].ToString());
                            adv.SolID = dt.Rows[0]["SolID"].ToString();

                            //AdviceCode = AdviceCode + currentTime;

                            adv.adviceFor = string.Empty;

                            //string AdviceCode = SolID + DateTime.Parse(SettlementJDate).ToString("ddMMyy") + Cnt.PadLeft(5, '0');

                            // adv.AdviceCode = string.Empty;
                            adv.AdviceCode = dt.Rows[0]["AdviceCode"].ToString();

                            adv.CounterSignator = counterSignator;
                            adv.Signator = signator;
                            GenerateBranchVoucherForTruncation(adv);
                        }
                    }
                }
            }
            InsertAdvicePrintToDB(BranchID);
        }


        private void GenerateAllBranchVoucherForTruncation(int userID, string signator, string counterSignator)
        {
            int TransactionType = ParseData.StringToInt(ddListTransactionType.SelectedValue);
            int ReportType = ParseData.StringToInt(ddListReportType.SelectedValue);

            PrintCustomerAdviceDB printDB = new PrintCustomerAdviceDB();

            DataTable dtBranchID = new DataTable();

            if (ReportType == 1)
            {
                dtBranchID = printDB.GetBranchIDForTxnReceivedBySettlementDateForAdvice(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), ParseData.StringToInt(ddListDhakaNonDhaka.SelectedValue));
            }
            else if (ReportType == 2)
            {
                dtBranchID = printDB.GetBranchIDForReturnSentBySettlementDateForAdvice(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), ParseData.StringToInt(ddListDhakaNonDhaka.SelectedValue));
            }
            else if (ReportType == 3)
            {
                dtBranchID = printDB.GetBranchIDForTXNSentBySettlementDateForAdvice(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), ParseData.StringToInt(ddListDhakaNonDhaka.SelectedValue));
            }
            else if (ReportType == 4)
            {
                dtBranchID = printDB.GetBranchIDForReturnReceivedBySettlementDateForAdvice(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), ParseData.StringToInt(ddListDhakaNonDhaka.SelectedValue));
            }
            AdviceData[] advArr = new AdviceData[dtBranchID.Rows.Count];

            int AdviceCounter = -1;
            DataTable dt;

            string strRptType = string.Empty;
            string strRptCode = string.Empty;

            for (int i = 0; i < dtBranchID.Rows.Count; i++)
            {
                int BranchID = ParseData.StringToInt(dtBranchID.Rows[i]["BranchID"].ToString());

                if (ReportType == 1)
                {
                    if (TransactionType == 1)
                    {
                        dt = GetDataForBranchAdviceForRupali(1, BranchID, userID);
                        strRptType = "Inward Credit Transaction";
                        strRptCode= "179";
                    }
                    else
                    {
                        dt = GetDataForBranchAdviceForRupali(2, BranchID, userID);
                        strRptType = "Inward Debit Transaction";
                        strRptCode= "180";
                    }
                }
                else if (ReportType == 2)
                {
                    if (TransactionType == 1)
                    {
                        dt = GetReturnSentCreditForBranchAdviceForRupali(BranchID, userID);
                        strRptType = "Outward Credit Return";
                        strRptCode = "180";
                    }
                    else
                    {
                        dt = GetReturnSentDebitForBranchAdviceForRupali(BranchID, userID);
                        strRptType = "Outward Debit Return";
                        strRptCode = "179";
                    }
                }
                else if (ReportType == 3)
                {
                    if (TransactionType == 1)
                    {
                        dt = GetTransactionSentCreditForBranchAdviceForRupali(BranchID, userID);
                        strRptType = "Outward Credit Transaction";
                        strRptCode = "180";
                    }
                    else
                    {
                        dt = GetTransactionSentDebitForBranchAdviceForRupali(BranchID, userID);
                        strRptType = "Outward Debit Transaction";
                        strRptCode = "179";
                    }
                }
                else //if (ReportType == 4)
                {
                    if (TransactionType == 1)
                    {
                        dt = GetReturnReceivedCreditForBranchAdviceForRupali(BranchID, userID);
                        strRptType = "Inward Credit Return";
                        strRptCode = "179";
                    }
                    else
                    {
                        dt = GetReturnReceivedDebitForBranchAdviceForRupali(BranchID, userID);
                        strRptType = "Inward Debit Return";
                        strRptCode = "180";
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    AdviceData adv = new AdviceData();
                    ///////////////////////////////////////
                    //string AdviceCode = dt.Rows[0]["AdviceCode"].ToString();
                    adv.ZoneName = dt.Rows[0]["ZoneName"].ToString();
                    adv.BranchName = dt.Rows[0]["BranchName"].ToString();
                    //string CurDate = DateTime.Parse(dt.Rows[0]["CurDate"].ToString()).ToString("dd/MM/yyyy");
                    adv.Cnt = dt.Rows[0]["Cnt"].ToString();
                    // adv.Amount =  5000;

                    adv.Amount = ParseData.StringToDouble(dt.Rows[0]["Amount"].ToString());
                    adv.SettlementJDate = dt.Rows[0]["SettlementJDate"].ToString();
                    adv.CreditDebit = dt.Rows[0]["CreditDebit"].ToString();
                    adv.OrgBranch = dt.Rows[0]["OrgBranch"].ToString();
                    adv.OrgBranchCode = dt.Rows[0]["OrgBranchCode"].ToString();
                    adv.RoutingNo = dt.Rows[0]["RoutingNo"].ToString();
                    adv.branchIDFromDB = ParseData.StringToInt(dt.Rows[0]["BranchID"].ToString());
                    adv.SolID = dt.Rows[0]["SolID"].ToString();

                    adv.adviceFor = string.Empty;

                    adv.AdviceCode = dt.Rows[0]["AdviceCode"].ToString();
                    AdviceCounter++;
                    advArr[AdviceCounter] = adv;
                    //Generate Advice
                }
            }

            if (AdviceCounter >= 0)
            {
                GenerateBranchVoucherForTruncationForAllBranch(advArr, AdviceCounter, strRptType, strRptCode, signator, counterSignator);
            }
        }

        private string boxString(string s)
        {
            string sss = "";
            for (int i = 0; i < s.Length; i++)
            {
                sss = sss + s.Substring(i, 1) + "  ";
            }
            return sss;
        }

        private string boxDate(string s)
        {

            string sss = "";
            for (int i = 0; i < s.Length; i++)
            {
                sss = sss + s.Substring(i, 1) + " ";
            }
            return sss;
        }
        
        public void GenerateBranchVoucher(int BranchID, int userID, string signator, string counterSignator)
        {
            string adviceFileName = "Advice_" + ddlistYear.SelectedValue + ddlistMonth.SelectedValue + ddlistDay.SelectedValue + ".pdf";

            DataTable dtAdviceInwardTXNCredit = GetDataForBranchAdviceForRupali(1, BranchID, userID);
            AdviceData adviceTXNReceivedCredit = InitializeAdvice(dtAdviceInwardTXNCredit, signator, counterSignator);

            DataTable dtAdviceInwardTXNDebit = GetDataForBranchAdviceForRupali(2, BranchID, userID);
            AdviceData adviceTXNReceivedDebit = InitializeAdvice(dtAdviceInwardTXNDebit, signator, counterSignator);

            DataTable dtAdviceOutwardTXNCredit = GetTransactionSentCreditForBranchAdviceForRupali(BranchID, userID);
            AdviceData adviceTXNSentCredit = InitializeAdvice(dtAdviceOutwardTXNCredit, signator, counterSignator);

            DataTable dtAdviceOutwardTXNDebit = GetTransactionSentDebitForBranchAdviceForRupali(BranchID, userID);
            AdviceData adviceTXNSentDebit = InitializeAdvice(dtAdviceOutwardTXNDebit, signator, counterSignator);

            DataTable dtAdviceOutwardReturnCredit = GetReturnSentCreditForBranchAdviceForRupali(BranchID, userID);
            AdviceData adviceReturnSentCredit = InitializeAdvice(dtAdviceOutwardReturnCredit, signator, counterSignator);

            DataTable dtAdviceOutwardReturnDebit = GetReturnSentDebitForBranchAdviceForRupali(BranchID, userID);
            AdviceData adviceReturnSentDebit = InitializeAdvice(dtAdviceOutwardReturnDebit, signator, counterSignator);

            DataTable dtAdviceInwardReturnCredit = GetReturnReceivedCreditForBranchAdviceForRupali(BranchID, userID);
            AdviceData adviceReturnReceivedCredit = InitializeAdvice(dtAdviceInwardReturnCredit, signator, counterSignator);

            DataTable dtAdviceInwardReturnDebit = GetReturnReceivedDebitForBranchAdviceForRupali(BranchID, userID);
            AdviceData adviceReturnReceivedDebit = InitializeAdvice(dtAdviceInwardReturnDebit, signator, counterSignator);
            
            Document document = new Document(PageSize.A4, 10, 10, 8, 8);
            try
            {
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(getRupaliLogo());
                jpg.SetAbsolutePosition(165, 745);

                Response.ClearContent();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + adviceFileName);

                PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

                document.Open();

                if (dtAdviceInwardTXNCredit.Rows.Count>0)
                {
                    document.NewPage();
                    document.Add(jpg);

                    PrintCreditAdvice(adviceTXNReceivedCredit, writer, "Inward Credit Transaction", "179");
                    DataTable dt = GetDetailSettlementReportData("102");

                    if (dt.Rows.Count > 0)
                    {
                        GetDetailSettlementReportPDF(document, dt, "Inward Credit Transaction", adviceTXNReceivedCredit.AdviceCode);
                    }

                }
                if (dtAdviceInwardTXNDebit.Rows.Count > 0)
                {
                    document.NewPage();
                    document.Add(jpg);
                    
                    PrintDebitAdvice(adviceTXNReceivedDebit, writer, "Inward Debit Transaction", "180");
                    DataTable dt = GetDetailSettlementReportData("202");

                    if (dt.Rows.Count > 0)
                    {
                        GetDetailSettlementReportPDF(document, dt, "Inward Debit Transaction", adviceTXNReceivedDebit.AdviceCode);
                    }
                }

                if (dtAdviceOutwardTXNCredit.Rows.Count > 0)
                {
                    document.NewPage();
                    document.Add(jpg);
                    
                    PrintCreditAdvice(adviceTXNSentCredit, writer, "Outward Credit Transaction", "180");
                    DataTable dt = GetDetailSettlementReportData("112");

                    if (dt.Rows.Count > 0)
                    {
                        GetDetailSettlementReportPDF(document, dt, "Outward Credit Transaction", adviceTXNSentCredit.AdviceCode);
                    }
                }
                if (dtAdviceOutwardTXNDebit.Rows.Count > 0)
                {
                    document.NewPage();
                    document.Add(jpg);
                    
                    PrintDebitAdvice(adviceTXNSentDebit, writer, "Outward Debit Transaction", "179");
                    DataTable dt = GetDetailSettlementReportData("212");

                    if (dt.Rows.Count > 0)
                    {
                        GetDetailSettlementReportPDF(document, dt, "Outward Debit Transaction", adviceTXNSentDebit.AdviceCode);
                    }
                }

                if (dtAdviceOutwardReturnCredit.Rows.Count > 0)
                {
                    document.NewPage();
                    document.Add(jpg);
                    
                    PrintCreditAdvice(adviceReturnSentCredit, writer, "Outward Credit Return", "180");
                    DataTable dt = GetDetailSettlementReportData("104");

                    if (dt.Rows.Count > 0)
                    {
                        GetDetailSettlementReportPDF(document, dt, "Outward Credit Return", adviceReturnSentCredit.AdviceCode);
                    }

                }
                if (dtAdviceOutwardReturnDebit.Rows.Count > 0)
                {
                    document.NewPage();
                    document.Add(jpg);
                    
                    PrintDebitAdvice(adviceReturnSentDebit, writer, "Outward Debit Return", "179");
                    DataTable dt = GetDetailSettlementReportData("204");

                    if (dt.Rows.Count > 0)
                    {
                        GetDetailSettlementReportPDF(document, dt, "Outward Debit Return", adviceReturnSentDebit.AdviceCode);
                    }
                }

                /////////////////////////////////
                if (dtAdviceInwardReturnCredit.Rows.Count > 0)
                {
                    document.NewPage();
                    document.Add(jpg);
                    
                    PrintCreditAdvice(adviceReturnReceivedCredit, writer, "Inward Credit Return", "179");
                    DataTable dt = GetDetailSettlementReportData("105");

                    if (dt.Rows.Count > 0)
                    {
                        GetDetailSettlementReportPDF(document, dt, "Inward Credit Return", adviceReturnReceivedCredit.AdviceCode);
                    }

                }
                if (dtAdviceInwardReturnDebit.Rows.Count > 0)
                {
                    document.NewPage();
                    document.Add(jpg);
                    
                    PrintDebitAdvice(adviceReturnReceivedDebit, writer, "Inward Debit Return", "180");
                    DataTable dt = GetDetailSettlementReportData("205");

                    if (dt.Rows.Count > 0)
                    {
                        GetDetailSettlementReportPDF(document, dt, "Inward Debit Return", adviceReturnReceivedDebit.AdviceCode);
                    }
                }

            }
            catch (DocumentException de)
            {

                System.Console.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {

                System.Console.WriteLine(ioe.Message);
            }
            document.Close();
        }

        private static AdviceData InitializeAdvice(DataTable dtAdvice, string signator, string counterSignator)
        {
            AdviceData advice = new AdviceData();
            if (dtAdvice.Rows.Count > 0)
            {
                ///////////////////////////////////////
                //string AdviceCode = dt.Rows[0]["AdviceCode"].ToString();
                advice.ZoneName = dtAdvice.Rows[0]["ZoneName"].ToString();
                advice.BranchName = dtAdvice.Rows[0]["BranchName"].ToString();
                //string CurDate = DateTime.Parse(dt.Rows[0]["CurDate"].ToString()).ToString("dd/MM/yyyy");
                advice.Cnt = dtAdvice.Rows[0]["Cnt"].ToString();
                // adv.Amount =  5000;

                advice.Amount = ParseData.StringToDouble(dtAdvice.Rows[0]["Amount"].ToString());
                advice.SettlementJDate = dtAdvice.Rows[0]["SettlementJDate"].ToString();
                advice.CreditDebit = dtAdvice.Rows[0]["CreditDebit"].ToString();
                advice.OrgBranch = dtAdvice.Rows[0]["OrgBranch"].ToString();
                advice.OrgBranchCode = dtAdvice.Rows[0]["OrgBranchCode"].ToString();
                advice.RoutingNo = dtAdvice.Rows[0]["RoutingNo"].ToString();
                advice.branchIDFromDB = ParseData.StringToInt(dtAdvice.Rows[0]["BranchID"].ToString());
                advice.SolID = dtAdvice.Rows[0]["SolID"].ToString();

                //AdviceCode = AdviceCode + currentTime;

                advice.adviceFor = string.Empty;

                //string AdviceCode = SolID + DateTime.Parse(SettlementJDate).ToString("ddMMyy") + Cnt.PadLeft(5, '0');

                // adv.AdviceCode = string.Empty;
                advice.AdviceCode = dtAdvice.Rows[0]["AdviceCode"].ToString();
                advice.Signator = signator;
                advice.CounterSignator = counterSignator;
                //Generate Advice
            }
            return advice;
        }

        private void PrintCreditAdvice(AdviceData advice, PdfWriter writer, string RptType, string strRptTypeCode)
        {
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.TIMES_BOLD).BaseFont, 20);
            cb.SetTextMatrix(210, 755);
            cb.ShowText("Rupali Bank Ltd.");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12); // set text Sf           
            cb.SetTextMatrix(18, 780);
            cb.ShowText("SF- " + strRptTypeCode);

            cb.SetTextMatrix(219, 778); // set text TransCode
            cb.ShowText("Trans: Code 1  2 ");
            cb.MoveTo(285, 774);
            cb.LineTo(312, 774);
            cb.Stroke();
            cb.MoveTo(285, 774);
            cb.LineTo(285, 790);
            cb.Stroke();
            cb.MoveTo(285, 790);
            cb.LineTo(312, 790);
            cb.Stroke();
            cb.MoveTo(312, 790);
            cb.LineTo(312, 774);
            cb.Stroke();
            cb.MoveTo(299, 774);
            cb.LineTo(299, 790);
            cb.Stroke();

            cb.SetTextMatrix(470, 770); // set text AdviceNo
            cb.ShowText("Advice No :" + advice.AdviceCode);

            cb.MoveTo(182, 718);  // set text Sender
            cb.LineTo(182, 730);
            cb.Stroke();
            cb.MoveTo(182, 730);
            cb.LineTo(244, 730);
            cb.Stroke();
            cb.MoveTo(244, 730);
            cb.LineTo(244, 718);
            cb.Stroke();
            cb.MoveTo(244, 718);
            cb.LineTo(182, 718);
            cb.Stroke();

            cb.MoveTo(196, 718);
            cb.LineTo(196, 730);
            cb.Stroke();
            cb.MoveTo(210, 718);
            cb.LineTo(210, 730);
            cb.Stroke();
            cb.MoveTo(224, 718);
            cb.LineTo(224, 730);
            cb.Stroke();
            cb.SetTextMatrix(18, 720);
            cb.ShowText("Sender : Local Office  Dhaka");

            cb.SetTextMatrix(185, 720);
            cb.ShowText(boxString("0018"));

            cb.MoveTo(182, 702);// set text Reciever
            cb.LineTo(182, 714);
            cb.Stroke();
            cb.MoveTo(182, 714);
            cb.LineTo(244, 714);
            cb.Stroke();
            cb.MoveTo(244, 714);
            cb.LineTo(244, 702);
            cb.Stroke();
            cb.MoveTo(244, 702);
            cb.LineTo(182, 702);
            cb.Stroke();

            cb.MoveTo(196, 702);
            cb.LineTo(196, 714);
            cb.Stroke();
            cb.MoveTo(210, 702);
            cb.LineTo(210, 714);
            cb.Stroke();
            cb.MoveTo(224, 702);
            cb.LineTo(224, 714);
            cb.Stroke();

            // -----------------------------
            cb.SetTextMatrix(18, 692); // set Reciver Name and Code
            cb.ShowText("Reciever Branch Name : " + advice.BranchName);

            cb.SetTextMatrix(18, 704);
            cb.ShowText("Receiver  Branch Code :");

            cb.SetTextMatrix(185, 704);
            cb.ShowText(boxString(advice.SolID));
            //---------------------------------

            cb.SetTextMatrix(470, 718); // set text Ref
            cb.ShowText("Ref : BEFTN");

            cb.MoveTo(502, 702); // set text Date
            cb.LineTo(502, 714);
            cb.Stroke();
            cb.MoveTo(502, 714);
            cb.LineTo(582, 714);
            cb.Stroke();
            cb.MoveTo(582, 714);
            cb.LineTo(582, 702);
            cb.Stroke();
            cb.MoveTo(582, 702);
            cb.LineTo(502, 702);
            cb.Stroke();

            cb.MoveTo(512, 702);
            cb.LineTo(512, 714);
            cb.Stroke();
            cb.MoveTo(522, 702);
            cb.LineTo(522, 714);
            cb.Stroke();
            cb.MoveTo(532, 702);
            cb.LineTo(532, 714);
            cb.Stroke();
            cb.MoveTo(542, 702);
            cb.LineTo(542, 714);
            cb.Stroke();
            cb.MoveTo(552, 702);
            cb.LineTo(552, 714);
            cb.Stroke();
            cb.MoveTo(562, 702);
            cb.LineTo(562, 714);
            cb.Stroke();
            cb.MoveTo(572, 702);
            cb.LineTo(572, 714);
            cb.Stroke();
            cb.SetTextMatrix(468, 704);
            cb.ShowText("Date:  " + boxDate(advice.SettlementJDate.ToString()));

            cb.SetTextMatrix(18, 685);
            cb.ShowText("================================================================================");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 9);
            cb.SetTextMatrix(18, 673);
            if (strRptTypeCode.Equals("180"))
            {
                cb.ShowText("We Have Debited Head Office Account with the Sum of TK: " + advice.Amount.ToString("N2"));
            }
            else
            {
                cb.ShowText("We Have Credited Head Office Account with the Sum of TK: " + advice.Amount.ToString("N2"));
            }
            cb.SetTextMatrix(18, 661);
            cb.ShowText("TAKA(In word) " + EFTN.Utility.NumberToWordConverter.GetAmountInWords(advice.Amount.ToString()));
            cb.SetTextMatrix(18, 649);
            cb.ShowText("As per today's EFTN Total Number Of " + RptType + " " + advice.Cnt + " and amount TK " + advice.Amount.ToString("N2") + " as per Details Report enclosed.");
            cb.SetTextMatrix(18, 637);
            cb.ShowText("Please respond the entry.");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 11);

            //cb.SetTextMatrix(150, 614);
            //cb.ShowText(advice.AdviceCode);
            cb.SetTextMatrix(150, 615);
            cb.ShowText(advice.CounterSignator);
            cb.SetTextMatrix(150, 612);
            cb.ShowText("________________");
            cb.SetTextMatrix(150, 600);
            cb.ShowText("Counter Signature");

            cb.SetTextMatrix(430, 615);
            cb.ShowText(advice.Signator);
            cb.SetTextMatrix(430, 612);
            cb.ShowText("__________");
            cb.SetTextMatrix(430, 600);
            cb.ShowText("Signature");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
            cb.SetTextMatrix(18, 590);
            cb.ShowText("================================================================================");

            cb.SetTextMatrix(18, 580);
            cb.ShowText("V. No. .....   .........  ......");
            cb.SetTextMatrix(18, 563);
            cb.ShowText("L. F.  .....   .........  .......                                                                                        Date:");

            cb.SetTextMatrix(18, 546);
            if (strRptTypeCode.Equals("180"))
            {
                cb.ShowText("Credit Head Office Account");
            }
            else
            {
                cb.ShowText("Debit Head Office Account");
            }
            cb.SetTextMatrix(18, 529);
            cb.ShowText("By Amount TK. " + advice.Amount.ToString("N2") + "                                           as per above noted details");
            cb.SetTextMatrix(18, 500);
            cb.ShowText("Contra ..............");

            //cb.SetTextMatrix(150, 510);
            //cb.ShowText(advice.CounterSignator);

            cb.SetTextMatrix(150, 482);
            cb.ShowText("________________");
            cb.SetTextMatrix(150, 470);
            cb.ShowText("Counter Signature");

            //cb.SetTextMatrix(425, 510);
            //cb.ShowText(advice.Signator);

            cb.SetTextMatrix(425, 482);
            cb.ShowText("__________");
            cb.SetTextMatrix(425, 470);
            cb.ShowText("Signature");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 7);
            cb.SetTextMatrix(15, 448);
            cb.ShowText("NB: It is a computer generated advice, Originator's signature is not required.");
            cb.SetTextMatrix(15, 440);
            cb.ShowText("Please check Settlement Report before responding. Please Contact with Local Office for any discrepancy.");
            cb.SetTextMatrix(15, 435);
            cb.ShowText("- - - -  - - - - -   - - - - - - - - -  -  - -  -  - - - - - - - - - - - -  - - -  - - -  - -  - -  -  -  - -  - - - - -  -  - - - - - - - - - - - -  - -  - - - - -  - - - - -  - - - - - - -  - - - - -  - - -  - - - - - - - Powered by Flora Limited");
            cb.EndText();
        }

        private void PrintDebitAdvice(AdviceData advice, PdfWriter writer, string RptType, string strRptTypeCode)
        {
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.TIMES_BOLD).BaseFont, 20);
            cb.SetTextMatrix(210, 755);
            cb.ShowText("Rupali Bank Ltd.");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
            cb.SetTextMatrix(18, 780);    // set text Sf
            cb.ShowText("SF- " + strRptTypeCode);

            cb.SetTextMatrix(218, 778);  // set text TransCode
            cb.ShowText("Trans: Code 1  2 ");
            cb.MoveTo(285, 774);
            cb.LineTo(312, 774);
            cb.Stroke();
            cb.MoveTo(285, 774);
            cb.LineTo(285, 790);
            cb.Stroke();
            cb.MoveTo(285, 790);
            cb.LineTo(312, 790);
            cb.Stroke();
            cb.MoveTo(312, 790);
            cb.LineTo(312, 774);
            cb.Stroke();
            cb.MoveTo(299, 774);
            cb.LineTo(299, 790);
            cb.Stroke();

            cb.SetTextMatrix(470, 770);// set text AdviceNo
            cb.ShowText("Advice No :" + advice.AdviceCode);

            cb.MoveTo(182, 718);  // set text Sender
            cb.LineTo(182, 730);
            cb.Stroke();
            cb.MoveTo(182, 730);
            cb.LineTo(244, 730);
            cb.Stroke();
            cb.MoveTo(244, 730);
            cb.LineTo(244, 718);
            cb.Stroke();
            cb.MoveTo(244, 718);
            cb.LineTo(182, 718);
            cb.Stroke();

            cb.MoveTo(196, 718);
            cb.LineTo(196, 730);
            cb.Stroke();
            cb.MoveTo(210, 718);
            cb.LineTo(210, 730);
            cb.Stroke();
            cb.MoveTo(224, 718);
            cb.LineTo(224, 730);
            cb.Stroke();
            cb.SetTextMatrix(18, 720);
            cb.ShowText("Sender :  Local Office  Dhaka");

            cb.SetTextMatrix(185, 720);
            cb.ShowText(boxString("0018"));

            cb.MoveTo(182, 702); // set text Reciever
            cb.LineTo(182, 714);
            cb.Stroke();
            cb.MoveTo(182, 714);
            cb.LineTo(244, 714);
            cb.Stroke();
            cb.MoveTo(244, 714);
            cb.LineTo(244, 702);
            cb.Stroke();
            cb.MoveTo(244, 702);
            cb.LineTo(182, 702);
            cb.Stroke();


            cb.MoveTo(196, 702);
            cb.LineTo(196, 714);
            cb.Stroke();
            cb.MoveTo(210, 702);
            cb.LineTo(210, 714);
            cb.Stroke();
            cb.MoveTo(224, 702);
            cb.LineTo(224, 714);
            cb.Stroke();


            //  -----------------------------
            cb.SetTextMatrix(18, 692); // set Reciver Name and Code
            cb.ShowText("Reciever Branch Name:" + advice.BranchName);

            cb.SetTextMatrix(18, 704);
            cb.ShowText("Receiver  Branch Code: ");

            cb.SetTextMatrix(185, 704);
            cb.ShowText(boxString(advice.SolID));
            //   ---------------------------------


            cb.SetTextMatrix(470, 718);// set text Ref
            cb.ShowText("Ref : BEFTN");

            cb.MoveTo(502, 702);  // set text Date
            cb.LineTo(502, 714);
            cb.Stroke();
            cb.MoveTo(502, 714);
            cb.LineTo(582, 714);
            cb.Stroke();
            cb.MoveTo(582, 714);
            cb.LineTo(582, 702);
            cb.Stroke();
            cb.MoveTo(582, 702);
            cb.LineTo(502, 702);
            cb.Stroke();
            cb.MoveTo(512, 702);
            cb.LineTo(512, 714);
            cb.Stroke();
            cb.MoveTo(522, 702);
            cb.LineTo(522, 714);
            cb.Stroke();
            cb.MoveTo(532, 702);
            cb.LineTo(532, 714);
            cb.Stroke();
            cb.MoveTo(542, 702);
            cb.LineTo(542, 714);
            cb.Stroke();
            cb.MoveTo(552, 702);
            cb.LineTo(552, 714);
            cb.Stroke();
            cb.MoveTo(562, 702);
            cb.LineTo(562, 714);
            cb.Stroke();
            cb.MoveTo(572, 702);
            cb.LineTo(572, 714);
            cb.Stroke();
            cb.SetTextMatrix(468, 704);
            cb.ShowText("Date:  " + boxDate(advice.SettlementJDate.ToString()));

            cb.SetTextMatrix(18, 685);
            cb.ShowText("================================================================================");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 9);
            cb.SetTextMatrix(18, 673);
            if (strRptTypeCode.Equals("180"))
            {
                cb.ShowText("We Have Debited Head Office Account with the Sum of TK:" + advice.Amount.ToString("N2"));
            }
            else
            {
                cb.ShowText("We Have Credited Head Office Account with the Sum of TK:" + advice.Amount.ToString("N2"));
            }
            cb.SetTextMatrix(18, 661);
            cb.ShowText("TAKA(In word) " + EFTN.Utility.NumberToWordConverter.GetAmountInWords(advice.Amount.ToString()));
            //cb.ShowText("We have debited Head office Account with the sum of TK:" + advice.Amount.ToString());

            cb.SetTextMatrix(18, 649);
            //cb.ShowText("As per today's  " + "EFTN" + " Net Settlement. Please respond the entry.");
            cb.ShowText("As per today's EFTN Total Number of " + RptType + " " + advice.Cnt + " and amount TK " + advice.Amount.ToString("N2") + " as per Details Report enclosed.");
            cb.SetTextMatrix(18, 637);
            cb.ShowText("Please respond the entry.");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 11);

            cb.SetTextMatrix(150, 615);
            cb.ShowText(advice.CounterSignator);

            cb.SetTextMatrix(150, 612);
            cb.ShowText("________________");
            cb.SetTextMatrix(150, 600);
            cb.ShowText("Counter Signature");

            cb.SetTextMatrix(430, 615);
            cb.ShowText(advice.Signator);
            cb.SetTextMatrix(430, 612);
            cb.ShowText("__________");
            cb.SetTextMatrix(430, 600);
            cb.ShowText("Signature");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
            cb.SetTextMatrix(18, 590);
            cb.ShowText("================================================================================");

            cb.SetTextMatrix(18, 580);
            cb.ShowText("V. No. .....   .........  ......");
            cb.SetTextMatrix(18, 563);
            cb.ShowText("L. F.  .....   .........  .......                                                                                        Date:");

            cb.SetTextMatrix(18, 546);
            if (strRptTypeCode.Equals("180"))
            {
                cb.ShowText("Credit Head Office Account");
            }
            else
            {
                cb.ShowText("Debit Head Office Account");
            }
            cb.SetTextMatrix(18, 529);
            cb.ShowText("By Amount TK. " + advice.Amount.ToString("N2") + "                                           as per above noted details");

            //cb.ShowText("To Amount TK. " + intToMoney(Math.Abs(advice.NetAmount)) + "                                            as per above noted details");
            cb.SetTextMatrix(18, 500);
            cb.ShowText("Contra ..............");

            //cb.SetTextMatrix(150, 492);
            //cb.ShowText(advice.AdviceCode);
            //cb.SetTextMatrix(150, 510);
            //cb.ShowText(advice.CounterSignator);

            cb.SetTextMatrix(150, 482);
            cb.ShowText("________________");
            cb.SetTextMatrix(150, 470);
            cb.ShowText("Counter Signature");

            //cb.SetTextMatrix(425, 510);
            //cb.ShowText(advice.Signator);
            cb.SetTextMatrix(425, 482);
            cb.ShowText("__________");
            cb.SetTextMatrix(425, 470);
            cb.ShowText("Signature");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 7);
            cb.SetTextMatrix(15, 448);
            cb.ShowText("NB: It is a computer generated advice, Originator's signature is not required.");
            cb.SetTextMatrix(15, 440);
            cb.ShowText("Please check Settlement Report before responding. Please Contact with Local Office for any discrepancy.");
            cb.SetTextMatrix(15, 435);
            cb.ShowText("- - - -  - - - - -   - - - - - - - - -  -  - -  -  - - - - - - - - - - - -  - - -  - - -  - -  - -  -  -  - -  - - - - -  -  - - - - - - - - - - - -  - -  - - - - -  - - - - -  - - - - - - -  - - - - -  - - -  - - - - - - - Powered by Flora Limited");
            cb.EndText();
        }

        private void GetDetailSettlementReportPDF(Document document, DataTable dt, string ReportType, string adviceCode)
        {
            document.NewPage();

            Font fnt = new Font(Font.HELVETICA, 7);
            Font fntblue = new Font(Font.HELVETICA, 7);
            fntblue.Color = new Color(0, 0, 255);
            Font fntbld = new Font(Font.HELVETICA, 7);
            fntbld.SetStyle(Font.BOLD);
            Font headerFont = new Font(Font.HELVETICA, 10);

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
            float[] widthsAtHeader = { 40, 40, 20 };
            headertable.SetWidths(widthsAtHeader);

            string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
            headertable.AddCell(new Phrase(ReportType + " Details, Dt: " + settlementDate + ", Adv: "+ adviceCode+" Tr. Code: 12", headerFont));


            headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), fnt));
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            //string SelectedBank = string.Empty;

            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            float[] headerwidths;
            int NumberOfPdfColumn = 0;
            if (SelectedBank.Equals("135"))
            {
                headerwidths = new float[] { 6, 10, 10, 4, 8, 4, 8, 6, 8, 8, 8, 8, 12, 8 };
                NumberOfPdfColumn = 14;
            }
            else
            {
                headerwidths = new float[] { 11, 11, 4, 10, 8, 8, 6, 10, 8, 8, 8, 12, 8 };
                NumberOfPdfColumn = 13;
            }

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
            if (SelectedBank.Equals("135"))
            {
                c0 = new PdfPCell(new iTextSharp.text.Phrase("Zone Name", fnt));
            }
            else
            {
                c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            }

            //PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;

            datatable.AddCell(c0);

            if (SelectedBank.Equals("135"))
            {
                datatable.AddCell(new iTextSharp.text.Phrase("Bank Name", fnt)); //only for JANATA BANK
            }
            datatable.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("SECC", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Trace No.", fnt));
            if (SelectedBank.Equals("185"))
            {
                datatable.AddCell(new iTextSharp.text.Phrase("Sending Br.", fnt));
            }
            else
            {
                datatable.AddCell(new iTextSharp.text.Phrase("Tran. Code", fnt));
            }
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Bank RoutNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Receiver /Payer Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("C.Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Ent.Desc", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("RejectReason", fnt));


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (SelectedBank.Equals("135"))
                {
                    PdfPCell cZone = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["ZoneName"], fnt));
                    cZone.BorderWidth = 0.5f;
                    cZone.HorizontalAlignment = Cell.ALIGN_LEFT;
                    cZone.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    cZone.Padding = 4;
                    datatable.AddCell(cZone);
                }

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

                if (SelectedBank.Equals("185"))
                {
                    datatable.AddCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["SendingBranchName"], fnt));
                }
                else
                {
                    datatable.AddCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                }

                //PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                //c5.BorderWidth = 0.5f;
                //c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                //c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //c5.Padding = 4;
                //datatable.AddCell(c5);

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

                PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["CompanyName"], fnt));
                c11.BorderWidth = 0.5f;
                c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c11.Padding = 4;
                datatable.AddCell(c11);

                PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["EntryDesc"], fnt));
                c12.BorderWidth = 0.5f;
                c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c12.Padding = 4;
                datatable.AddCell(c12);

                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["RejectReason"], fnt));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_LEFT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable.AddCell(c13);

            }

            //-------------TOTAL IN FOOTER --------------------
            if (SelectedBank.Equals("135"))
            {
                datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            }
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(TraceNumber)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //-------------END TOTAL -------------------------



            /////////////////////////////////
            document.Add(datatable);

        }

        public void GenerateBranchVoucherForTruncation(AdviceData advice)
        {
            Document document = new Document(PageSize.A4, 10, 10, 8, 8);
            try
            {

                int yCor = 0;

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(getRupaliLogo());
                jpg.SetAbsolutePosition(165, 745 - yCor);

                iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(getRupaliLogo());
                jpg2.SetAbsolutePosition(165, 745 - (yCor + 395));

                if (advice.CreditDebit == "Debit")
                {

                    Response.ClearContent();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + "Advice_" + advice.SettlementJDate + ".pdf");

                    PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

                    document.Open();
                    document.NewPage();
                    document.Add(jpg);
                    document.Add(jpg2);
                    PdfContentByte cb = writer.DirectContent;
                    cb.BeginText();
                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.TIMES_BOLD).BaseFont, 20);
                    cb.SetTextMatrix(210, 755 - yCor);
                    cb.ShowText("Rupali Bank Ltd.");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
                    cb.SetTextMatrix(18, 780 - yCor);    // set text Sf
                    cb.ShowText("SF- 180");

                    cb.SetTextMatrix(218, 778 - yCor);  // set text TransCode
                    cb.ShowText("Trans: Code 1  2 ");
                    cb.MoveTo(285, 774 - yCor);
                    cb.LineTo(312, 774 - yCor);
                    cb.Stroke();
                    cb.MoveTo(285, 774 - yCor);
                    cb.LineTo(285, 790 - yCor);
                    cb.Stroke();
                    cb.MoveTo(285, 790 - yCor);
                    cb.LineTo(312, 790 - yCor);
                    cb.Stroke();
                    cb.MoveTo(312, 790 - yCor);
                    cb.LineTo(312, 774 - yCor);
                    cb.Stroke();
                    cb.MoveTo(299, 774 - yCor);
                    cb.LineTo(299, 790 - yCor);
                    cb.Stroke();

                    cb.SetTextMatrix(470, 770 - yCor);// set text AdviceNo
                    cb.ShowText("Advice No :" + advice.AdviceCode);

                    cb.MoveTo(182, 718 - yCor);  // set text Sender
                    cb.LineTo(182, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(182, 730 - yCor);
                    cb.LineTo(244, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 730 - yCor);
                    cb.LineTo(244, 718 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 718 - yCor);
                    cb.LineTo(182, 718 - yCor);
                    cb.Stroke();

                    cb.MoveTo(196, 718 - yCor);
                    cb.LineTo(196, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(210, 718 - yCor);
                    cb.LineTo(210, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(224, 718 - yCor);
                    cb.LineTo(224, 730 - yCor);
                    cb.Stroke();
                    cb.SetTextMatrix(18, 720 - yCor);
                    cb.ShowText("Sender :  Local Office  Dhaka");

                    cb.SetTextMatrix(185, 720 - yCor);
                    cb.ShowText(boxString("0018"));

                    cb.MoveTo(182, 702 - yCor); // set text Reciever
                    cb.LineTo(182, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(182, 714 - yCor);
                    cb.LineTo(244, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 714 - yCor);
                    cb.LineTo(244, 702 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 702 - yCor);
                    cb.LineTo(182, 702 - yCor);
                    cb.Stroke();


                    cb.MoveTo(196, 702 - yCor);
                    cb.LineTo(196, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(210, 702 - yCor);
                    cb.LineTo(210, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(224, 702 - yCor);
                    cb.LineTo(224, 714 - yCor);
                    cb.Stroke();


                    //  -----------------------------
                    cb.SetTextMatrix(18, 692 - yCor); // set Reciver Name and Code
                    cb.ShowText("Reciever Branch Name:" + advice.BranchName);

                    cb.SetTextMatrix(18, 704 - yCor);
                    cb.ShowText("Receiver  Branch Code: ");

                    cb.SetTextMatrix(185, 704 - yCor);
                    cb.ShowText(boxString(advice.SolID));
                    //   ---------------------------------


                    cb.SetTextMatrix(470, 718 - yCor);// set text Ref
                    cb.ShowText("Ref : BEFTN");

                    cb.SetTextMatrix(470, 732 - yCor); // set text Ref
                    cb.ShowText("Head Office Copy");

                    cb.MoveTo(502, 702 - yCor);  // set text Date
                    cb.LineTo(502, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(502, 714 - yCor);
                    cb.LineTo(582, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(582, 714 - yCor);
                    cb.LineTo(582, 702 - yCor);
                    cb.Stroke();
                    cb.MoveTo(582, 702 - yCor);
                    cb.LineTo(502, 702 - yCor);
                    cb.Stroke();
                    cb.MoveTo(512, 702 - yCor);
                    cb.LineTo(512, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(522, 702 - yCor);
                    cb.LineTo(522, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(532, 702 - yCor);
                    cb.LineTo(532, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(542, 702 - yCor);
                    cb.LineTo(542, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(552, 702 - yCor);
                    cb.LineTo(552, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(562, 702 - yCor);
                    cb.LineTo(562, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(572, 702 - yCor);
                    cb.LineTo(572, 714 - yCor);
                    cb.Stroke();
                    cb.SetTextMatrix(468, 704 - yCor);
                    cb.ShowText("Date:  " + boxDate(advice.SettlementJDate.ToString()));

                    cb.SetTextMatrix(18, 682 - yCor);
                    cb.ShowText("================================================================================");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 9);
                    cb.SetTextMatrix(18, 673 - yCor);
                    cb.ShowText("We have debited Head office Account with the sum of TK:" + advice.Amount.ToString("N2"));
                    cb.SetTextMatrix(18, 656 - yCor);
                    cb.ShowText("TAKA(In word) " + EFTN.Utility.NumberToWordConverter.GetAmountInWords(advice.Amount.ToString()));
                    //cb.ShowText("We have debited Head office Account with the sum of TK:" + advice.Amount.ToString());

                    cb.SetTextMatrix(18, 639 - yCor);
                    //cb.ShowText("As per today's  " + "EFTN" + " Net Settlement. Please respond the entry.");
                    cb.ShowText("As per today's  EFTN Total Inward Tr. " + advice.Cnt + " and TK " + advice.Amount.ToString("N2") + " as per Details Report enclosed. Please respond the entry.");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 11);

                    cb.SetTextMatrix(150, 615 - yCor);
                    cb.ShowText(advice.CounterSignator);
                    cb.SetTextMatrix(150, 612 - yCor);
                    cb.ShowText("________________");
                    cb.SetTextMatrix(150, 600 - yCor);
                    cb.ShowText("Counter Signature");

                    cb.SetTextMatrix(430, 610 - yCor);
                    cb.ShowText(advice.Signator);
                    cb.SetTextMatrix(430, 607 - yCor);
                    cb.ShowText("__________");
                    cb.SetTextMatrix(430, 600 - yCor);
                    cb.ShowText("Signature");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
                    cb.SetTextMatrix(18, 590 - yCor);
                    cb.ShowText("================================================================================");

                    cb.SetTextMatrix(18, 580 - yCor);
                    cb.ShowText("V. No. .....   .........  ......");
                    cb.SetTextMatrix(18, 563 - yCor);
                    cb.ShowText("L. F.  .....   .........  .......                                                                                        Date:");

                    cb.SetTextMatrix(18, 546 - yCor);
                    cb.ShowText("Credit Head Office Account");
                    cb.SetTextMatrix(18, 529 - yCor);
                    cb.ShowText("Credit Head Office Account");

                    //cb.ShowText("To Amount TK. " + intToMoney(Math.Abs(advice.NetAmount)) + "                                            as per above noted details");
                    cb.SetTextMatrix(18, 500 - yCor);
                    cb.ShowText("Contra ..............");

                    //cb.SetTextMatrix(150, 502 - yCor);
                    //cb.ShowText(advice.CounterSignator);
                    cb.SetTextMatrix(150, 482 - yCor);
                    cb.ShowText("________________");
                    cb.SetTextMatrix(150, 470 - yCor);
                    cb.ShowText("Counter Signature");

                    //cb.SetTextMatrix(425, 502 - yCor);
                    //cb.ShowText(advice.Signator);
                    cb.SetTextMatrix(425, 482 - yCor);
                    cb.ShowText("__________");
                    cb.SetTextMatrix(425, 470 - yCor);
                    cb.ShowText("Signature");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 7);
                    cb.SetTextMatrix(15, 448 - yCor);
                    cb.ShowText("NB: It is a computer generated advice, Originator's signature is not required.");
                    cb.SetTextMatrix(15, 440 - yCor);
                    cb.ShowText("Please check Settlement Report before responding. Please Contact with Local Office for any discrepancy.");
                    cb.SetTextMatrix(15, 435 - yCor);
                    cb.ShowText("- - - -  - - - - -   - - - - - - - - -  -  - -  -  - - - - - - - - - - - -  - - -  - - -  - -  - -  -  -  - -  - - - - -  -  - - - - - - - - - - - -  - -  - - - - -  - - - - -  - - - - - - -  - - - - -  - - -  - - - - - - - Powered by Flora Limited");


                    ///////////////////////FOR THE SECOND PART///////

                    yCor = 395;
                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.TIMES_BOLD).BaseFont, 20);
                    cb.SetTextMatrix(210, 755 - yCor);
                    cb.ShowText("Rupali Bank Ltd.");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
                    cb.SetTextMatrix(18, 780 - yCor);    // set text Sf
                    cb.ShowText("SF- 180");

                    cb.SetTextMatrix(218, 778 - yCor);  // set text TransCode
                    cb.ShowText("Trans: Code 1  2 ");
                    cb.MoveTo(285, 774 - yCor);
                    cb.LineTo(312, 774 - yCor);
                    cb.Stroke();
                    cb.MoveTo(285, 774 - yCor);
                    cb.LineTo(285, 790 - yCor);
                    cb.Stroke();
                    cb.MoveTo(285, 790 - yCor);
                    cb.LineTo(312, 790 - yCor);
                    cb.Stroke();
                    cb.MoveTo(312, 790 - yCor);
                    cb.LineTo(312, 774 - yCor);
                    cb.Stroke();
                    cb.MoveTo(299, 774 - yCor);
                    cb.LineTo(299, 790 - yCor);
                    cb.Stroke();

                    cb.SetTextMatrix(470, 770 - yCor);// set text AdviceNo
                    cb.ShowText("Advice No :" + advice.AdviceCode);

                    cb.MoveTo(182, 718 - yCor);  // set text Sender
                    cb.LineTo(182, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(182, 730 - yCor);
                    cb.LineTo(244, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 730 - yCor);
                    cb.LineTo(244, 718 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 718 - yCor);
                    cb.LineTo(182, 718 - yCor);
                    cb.Stroke();

                    cb.MoveTo(196, 718 - yCor);
                    cb.LineTo(196, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(210, 718 - yCor);
                    cb.LineTo(210, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(224, 718 - yCor);
                    cb.LineTo(224, 730 - yCor);
                    cb.Stroke();
                    cb.SetTextMatrix(18, 720 - yCor);
                    cb.ShowText("Sender :  Local Office  Dhaka");

                    cb.SetTextMatrix(185, 720 - yCor);
                    cb.ShowText(boxString("0018"));

                    cb.MoveTo(182, 702 - yCor); // set text Reciever
                    cb.LineTo(182, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(182, 714 - yCor);
                    cb.LineTo(244, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 714 - yCor);
                    cb.LineTo(244, 702 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 702 - yCor);
                    cb.LineTo(182, 702 - yCor);
                    cb.Stroke();


                    cb.MoveTo(196, 702 - yCor);
                    cb.LineTo(196, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(210, 702 - yCor);
                    cb.LineTo(210, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(224, 702 - yCor);
                    cb.LineTo(224, 714 - yCor);
                    cb.Stroke();


                    //  -----------------------------
                    cb.SetTextMatrix(18, 692 - yCor); // set Reciver Name and Code
                    cb.ShowText("Reciever Branch Name:" + advice.BranchName);

                    cb.SetTextMatrix(18, 704 - yCor);
                    cb.ShowText("Receiver  Branch Code: ");

                    cb.SetTextMatrix(185, 704 - yCor);
                    cb.ShowText(boxString(advice.SolID));
                    //   ---------------------------------


                    cb.SetTextMatrix(470, 718 - yCor);// set text Ref
                    cb.ShowText("Ref : BEFTN");

                    cb.MoveTo(502, 702 - yCor);  // set text Date
                    cb.LineTo(502, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(502, 714 - yCor);
                    cb.LineTo(582, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(582, 714 - yCor);
                    cb.LineTo(582, 702 - yCor);
                    cb.Stroke();
                    cb.MoveTo(582, 702 - yCor);
                    cb.LineTo(502, 702 - yCor);
                    cb.Stroke();
                    cb.MoveTo(512, 702 - yCor);
                    cb.LineTo(512, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(522, 702 - yCor);
                    cb.LineTo(522, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(532, 702 - yCor);
                    cb.LineTo(532, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(542, 702 - yCor);
                    cb.LineTo(542, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(552, 702 - yCor);
                    cb.LineTo(552, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(562, 702 - yCor);
                    cb.LineTo(562, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(572, 702 - yCor);
                    cb.LineTo(572, 714 - yCor);
                    cb.Stroke();
                    cb.SetTextMatrix(468, 704 - yCor);
                    cb.ShowText("Date:  " + boxDate(advice.SettlementJDate.ToString()));

                    cb.SetTextMatrix(18, 682 - yCor);
                    cb.ShowText("================================================================================");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 9);
                    cb.SetTextMatrix(18, 673 - yCor);
                    cb.ShowText("We have debited Head office Account with the sum of TK:" + advice.Amount.ToString("N2"));
                    cb.SetTextMatrix(18, 656 - yCor);
                    cb.ShowText("TAKA(In word) " + EFTN.Utility.NumberToWordConverter.GetAmountInWords(advice.Amount.ToString()));
                    //cb.ShowText("We have debited Head office Account with the sum of TK:" + advice.Amount.ToString());

                    cb.SetTextMatrix(18, 639 - yCor);
                    //cb.ShowText("As per today's  " + "EFTN" + " Net Settlement. Please respond the entry.");
                    cb.ShowText("As per today's  EFTN Total Inward Tr. " + advice.Cnt + " and TK " + advice.Amount.ToString("N2") + " as per Details Report enclosed. Please respond the entry.");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 11);

                    cb.SetTextMatrix(150, 615 - yCor);
                    cb.ShowText(advice.CounterSignator);
                    cb.SetTextMatrix(150, 612 - yCor);
                    cb.ShowText("________________");
                    cb.SetTextMatrix(150, 600 - yCor);
                    cb.ShowText("Counter Signature");

                    cb.SetTextMatrix(430, 615 - yCor);
                    cb.ShowText(advice.Signator);
                    cb.SetTextMatrix(430, 612 - yCor);
                    cb.ShowText("__________");
                    cb.SetTextMatrix(430, 600 - yCor);
                    cb.ShowText("Signature");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
                    cb.SetTextMatrix(18, 590 - yCor);
                    cb.ShowText("================================================================================");

                    cb.SetTextMatrix(18, 580 - yCor);
                    cb.ShowText("V. No. .....   .........  ......");
                    cb.SetTextMatrix(18, 563 - yCor);
                    cb.ShowText("L. F.  .....   .........  .......                                                                                        Date:");

                    cb.SetTextMatrix(18, 546 - yCor);
                    cb.ShowText("Credit Head Office Account");
                    cb.SetTextMatrix(18, 529 - yCor);
                    cb.ShowText("Credit Head Office Account");

                    //cb.ShowText("To Amount TK. " + intToMoney(Math.Abs(advice.NetAmount)) + "                                            as per above noted details");
                    cb.SetTextMatrix(18, 500 - yCor);
                    cb.ShowText("Contra ..............");

                    //cb.SetTextMatrix(150, 502 - yCor);
                    //cb.ShowText(advice.CounterSignator);
                    cb.SetTextMatrix(150, 482 - yCor);
                    cb.ShowText("________________");
                    cb.SetTextMatrix(150, 470 - yCor);
                    cb.ShowText("Counter Signature");

                    //cb.SetTextMatrix(425, 502 - yCor);
                    //cb.ShowText(advice.Signator);
                    cb.SetTextMatrix(425, 482 - yCor);
                    cb.ShowText("__________");
                    cb.SetTextMatrix(425, 470 - yCor);
                    cb.ShowText("Signature");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 7);
                    cb.SetTextMatrix(15, 448 - yCor);
                    cb.ShowText("NB: It is a computer generated advice, Originator's signature is not required.");
                    cb.SetTextMatrix(15, 440 - yCor);
                    cb.ShowText("Please check Settlement Report before responding. Please Contact with Local Office for any discrepancy.");
                    cb.SetTextMatrix(15, 435 - yCor);
                    cb.ShowText("- - - -  - - - - -   - - - - - - - - -  -  - -  -  - - - - - - - - - - - -  - - -  - - -  - -  - -  -  -  - -  - - - - -  -  - - - - - - - - - - - -  - -  - - - - -  - - - - -  - - - - - - -  - - - - -  - - -  - - - - - - - Powered by Flora Limited");

                    cb.EndText();
                }
                else if (advice.CreditDebit == "Credit")//>0
                {
                    yCor = 0;
                    Response.ClearContent();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + "Advice_" + advice.SettlementJDate + ".pdf");

                    PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

                    document.Open();
                    document.NewPage();
                    document.Add(jpg);
                    document.Add(jpg2);
                    PdfContentByte cb = writer.DirectContent;
                    cb.BeginText();
                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.TIMES_BOLD).BaseFont, 20);
                    cb.SetTextMatrix(210, 755 - yCor);
                    cb.ShowText("Rupali Bank Ltd.");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12); // set text Sf           
                    cb.SetTextMatrix(18, 780 - yCor);
                    cb.ShowText("SF- 179");

                    cb.SetTextMatrix(219, 778); // set text TransCode
                    cb.ShowText("Trans: Code 1  2 ");
                    cb.MoveTo(285, 774 - yCor);
                    cb.LineTo(312, 774 - yCor);
                    cb.Stroke();
                    cb.MoveTo(285, 774 - yCor);
                    cb.LineTo(285, 790 - yCor);
                    cb.Stroke();
                    cb.MoveTo(285, 790 - yCor);
                    cb.LineTo(312, 790 - yCor);
                    cb.Stroke();
                    cb.MoveTo(312, 790 - yCor);
                    cb.LineTo(312, 774 - yCor);
                    cb.Stroke();
                    cb.MoveTo(299, 774 - yCor);
                    cb.LineTo(299, 790 - yCor);
                    cb.Stroke();

                    cb.SetTextMatrix(470, 770 - yCor); // set text AdviceNo
                    cb.ShowText("Advice No :" + advice.AdviceCode);

                    cb.MoveTo(182, 718 - yCor);  // set text Sender
                    cb.LineTo(182, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(182, 730 - yCor);
                    cb.LineTo(244, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 730 - yCor);
                    cb.LineTo(244, 718 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 718 - yCor);
                    cb.LineTo(182, 718 - yCor);
                    cb.Stroke();

                    cb.MoveTo(196, 718 - yCor);
                    cb.LineTo(196, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(210, 718 - yCor);
                    cb.LineTo(210, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(224, 718 - yCor);
                    cb.LineTo(224, 730 - yCor);
                    cb.Stroke();
                    cb.SetTextMatrix(18, 720 - yCor);
                    cb.ShowText("Sender : Local Office  Dhaka");

                    cb.SetTextMatrix(185, 720 - yCor);
                    cb.ShowText(boxString("0018"));

                    cb.MoveTo(182, 702 - yCor);// set text Reciever
                    cb.LineTo(182, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(182, 714 - yCor);
                    cb.LineTo(244, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 714 - yCor);
                    cb.LineTo(244, 702 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 702 - yCor);
                    cb.LineTo(182, 702 - yCor);
                    cb.Stroke();

                    cb.MoveTo(196, 702 - yCor);
                    cb.LineTo(196, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(210, 702 - yCor);
                    cb.LineTo(210, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(224, 702 - yCor);
                    cb.LineTo(224, 714 - yCor);
                    cb.Stroke();

                    // -----------------------------
                    cb.SetTextMatrix(18, 692 - yCor); // set Reciver Name and Code
                    cb.ShowText("Reciever Branch Name : " + advice.BranchName);

                    cb.SetTextMatrix(18, 704 - yCor);
                    cb.ShowText("Receiver  Branch Code :");

                    cb.SetTextMatrix(185, 704 - yCor);
                    cb.ShowText(boxString(advice.SolID));
                    //---------------------------------

                    cb.SetTextMatrix(470, 718 - yCor); // set text Ref
                    cb.ShowText("Ref : BEFTN");

                    cb.SetTextMatrix(470, 732 - yCor); // set text Ref
                    cb.ShowText("Head Office Copy");

                    cb.MoveTo(502, 702 - yCor); // set text Date
                    cb.LineTo(502, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(502, 714 - yCor);
                    cb.LineTo(582, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(582, 714 - yCor);
                    cb.LineTo(582, 702 - yCor);
                    cb.Stroke();
                    cb.MoveTo(582, 702 - yCor);
                    cb.LineTo(502, 702 - yCor);
                    cb.Stroke();

                    cb.MoveTo(512, 702 - yCor);
                    cb.LineTo(512, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(522, 702 - yCor);
                    cb.LineTo(522, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(532, 702 - yCor);
                    cb.LineTo(532, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(542, 702 - yCor);
                    cb.LineTo(542, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(552, 702 - yCor);
                    cb.LineTo(552, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(562, 702 - yCor);
                    cb.LineTo(562, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(572, 702 - yCor);
                    cb.LineTo(572, 714 - yCor);
                    cb.Stroke();
                    cb.SetTextMatrix(468, 704 - yCor);
                    cb.ShowText("Date:  " + boxDate(advice.SettlementJDate.ToString()));

                    cb.SetTextMatrix(18, 682 - yCor);
                    cb.ShowText("================================================================================");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 9);
                    cb.SetTextMatrix(18, 673 - yCor);
                    cb.ShowText("We have credited Head office Account with the sum of TK: " + advice.Amount.ToString("N2"));
                    cb.SetTextMatrix(18, 656 - yCor);
                    cb.ShowText("TAKA(In word) " + EFTN.Utility.NumberToWordConverter.GetAmountInWords(advice.Amount.ToString()));

                    //cb.ShowText("TAKA(In word) " + numtoCon.GetAmountInWords(advice.NetAmount.ToString()));
                    cb.SetTextMatrix(18, 639 - yCor);
                    //cb.ShowText("As per today's BEFTN " + " Net Settlement. Please respond the entry.");
                    cb.ShowText("As per today's  EFTN Total Inward Tr. " + advice.Cnt + " and TK " + advice.Amount.ToString("N2") + " as per Details Report enclosed. Please respond the entry.");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 11);

                    cb.SetTextMatrix(150, 615 - yCor);
                    cb.ShowText(advice.CounterSignator);
                    cb.SetTextMatrix(150, 612 - yCor);
                    cb.ShowText("________________");
                    cb.SetTextMatrix(150, 600 - yCor);
                    cb.ShowText("Counter Signature");

                    cb.SetTextMatrix(430, 615 - yCor);
                    cb.ShowText(advice.Signator);
                    cb.SetTextMatrix(430, 612 - yCor);
                    cb.ShowText("__________");
                    cb.SetTextMatrix(430, 600 - yCor);
                    cb.ShowText("Signature");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
                    cb.SetTextMatrix(18, 590 - yCor);
                    cb.ShowText("================================================================================");

                    cb.SetTextMatrix(18, 580 - yCor);
                    cb.ShowText("V. No. .....   .........  ......");
                    cb.SetTextMatrix(18, 563 - yCor);
                    cb.ShowText("L. F.  .....   .........  .......                                                                                        Date:");

                    cb.SetTextMatrix(18, 546 - yCor);
                    cb.ShowText("Debit Head Office Account");
                    cb.SetTextMatrix(18, 529 - yCor);
                    cb.ShowText("By Amount TK. " + advice.Amount.ToString("N2") + "                                           as per above noted details");
                    cb.SetTextMatrix(18, 500 - yCor);
                    cb.ShowText("Contra ..............");

                    //cb.SetTextMatrix(150, 512 - yCor);
                    //cb.ShowText(advice.CounterSignator);
                    cb.SetTextMatrix(150, 482 - yCor);
                    cb.ShowText("________________");
                    cb.SetTextMatrix(150, 470 - yCor);
                    cb.ShowText("Counter Signature");

                    //cb.SetTextMatrix(425, 512 - yCor);
                    //cb.ShowText(advice.Signator);
                    cb.SetTextMatrix(425, 482 - yCor);
                    cb.ShowText("__________");
                    cb.SetTextMatrix(425, 470 - yCor);
                    cb.ShowText("Signature");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 7);
                    cb.SetTextMatrix(15, 448 - yCor);
                    cb.ShowText("NB: It is a computer generated advice, Originator's signature is not required.");
                    cb.SetTextMatrix(15, 440 - yCor);
                    cb.ShowText("Please check Settlement Report before responding. Please Contact with Local Office for any discrepancy.");
                    cb.SetTextMatrix(15, 435 - yCor);
                    cb.ShowText("- - - -  - - - - -   - - - - - - - - -  -  - -  -  - - - - - - - - - - - -  - - -  - - -  - -  - -  -  -  - -  - - - - -  -  - - - - - - - - - - - -  - -  - - - - -  - - - - -  - - - - - - -  - - - - -  - - -  - - - - - - - Powered by Flora Limited");

                    /////////////////////SECOND PART///////////
                    yCor = 395;
                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.TIMES_BOLD).BaseFont, 20);
                    cb.SetTextMatrix(210, 755 - yCor);
                    cb.ShowText("Rupali Bank Ltd.");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12); // set text Sf           
                    cb.SetTextMatrix(18, 780 - yCor);
                    cb.ShowText("SF- 179");

                    cb.SetTextMatrix(219, 778 - yCor); // set text TransCode
                    cb.ShowText("Trans: Code 1  2 ");
                    cb.MoveTo(285, 774 - yCor);
                    cb.LineTo(312, 774 - yCor);
                    cb.Stroke();
                    cb.MoveTo(285, 774 - yCor);
                    cb.LineTo(285, 790 - yCor);
                    cb.Stroke();
                    cb.MoveTo(285, 790 - yCor);
                    cb.LineTo(312, 790 - yCor);
                    cb.Stroke();
                    cb.MoveTo(312, 790 - yCor);
                    cb.LineTo(312, 774 - yCor);
                    cb.Stroke();
                    cb.MoveTo(299, 774 - yCor);
                    cb.LineTo(299, 790 - yCor);
                    cb.Stroke();

                    cb.SetTextMatrix(470, 770 - yCor); // set text AdviceNo
                    cb.ShowText("Advice No :" + advice.AdviceCode);

                    cb.MoveTo(182, 718 - yCor);  // set text Sender
                    cb.LineTo(182, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(182, 730 - yCor);
                    cb.LineTo(244, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 730 - yCor);
                    cb.LineTo(244, 718 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 718 - yCor);
                    cb.LineTo(182, 718 - yCor);
                    cb.Stroke();

                    cb.MoveTo(196, 718 - yCor);
                    cb.LineTo(196, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(210, 718 - yCor);
                    cb.LineTo(210, 730 - yCor);
                    cb.Stroke();
                    cb.MoveTo(224, 718 - yCor);
                    cb.LineTo(224, 730 - yCor);
                    cb.Stroke();
                    cb.SetTextMatrix(18, 720 - yCor);
                    cb.ShowText("Sender : Local Office  Dhaka");

                    cb.SetTextMatrix(185, 720 - yCor);
                    cb.ShowText(boxString("0018"));

                    cb.MoveTo(182, 702 - yCor);// set text Reciever
                    cb.LineTo(182, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(182, 714 - yCor);
                    cb.LineTo(244, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 714 - yCor);
                    cb.LineTo(244, 702 - yCor);
                    cb.Stroke();
                    cb.MoveTo(244, 702 - yCor);
                    cb.LineTo(182, 702 - yCor);
                    cb.Stroke();

                    cb.MoveTo(196, 702 - yCor);
                    cb.LineTo(196, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(210, 702 - yCor);
                    cb.LineTo(210, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(224, 702 - yCor);
                    cb.LineTo(224, 714 - yCor);
                    cb.Stroke();

                    // -----------------------------
                    cb.SetTextMatrix(18, 692 - yCor); // set Reciver Name and Code
                    cb.ShowText("Reciever Branch Name : " + advice.BranchName);

                    cb.SetTextMatrix(18, 704 - yCor);
                    cb.ShowText("Receiver  Branch Code :");

                    cb.SetTextMatrix(185, 704 - yCor);
                    cb.ShowText(boxString(advice.SolID));
                    //---------------------------------

                    cb.SetTextMatrix(470, 718 - yCor); // set text Ref
                    cb.ShowText("Ref : BEFTN");

                    cb.MoveTo(502, 702 - yCor); // set text Date
                    cb.LineTo(502, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(502, 714 - yCor);
                    cb.LineTo(582, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(582, 714 - yCor);
                    cb.LineTo(582, 702 - yCor);
                    cb.Stroke();
                    cb.MoveTo(582, 702 - yCor);
                    cb.LineTo(502, 702 - yCor);
                    cb.Stroke();

                    cb.MoveTo(512, 702 - yCor);
                    cb.LineTo(512, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(522, 702 - yCor);
                    cb.LineTo(522, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(532, 702 - yCor);
                    cb.LineTo(532, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(542, 702 - yCor);
                    cb.LineTo(542, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(552, 702 - yCor);
                    cb.LineTo(552, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(562, 702 - yCor);
                    cb.LineTo(562, 714 - yCor);
                    cb.Stroke();
                    cb.MoveTo(572, 702 - yCor);
                    cb.LineTo(572, 714 - yCor);
                    cb.Stroke();
                    cb.SetTextMatrix(468, 704 - yCor);
                    cb.ShowText("Date:  " + boxDate(advice.SettlementJDate.ToString()));

                    cb.SetTextMatrix(18, 682 - yCor);
                    cb.ShowText("================================================================================");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 9);
                    cb.SetTextMatrix(18, 673 - yCor);
                    cb.ShowText("We have credited Head office Account with the sum of TK: " + advice.Amount.ToString("N2"));
                    cb.SetTextMatrix(18, 656 - yCor);
                    cb.ShowText("TAKA(In word) " + EFTN.Utility.NumberToWordConverter.GetAmountInWords(advice.Amount.ToString()));

                    //cb.ShowText("TAKA(In word) " + numtoCon.GetAmountInWords(advice.NetAmount.ToString()));
                    cb.SetTextMatrix(18, 639 - yCor);
                    //cb.ShowText("As per today's BEFTN " + " Net Settlement. Please respond the entry.");
                    cb.ShowText("As per today's  EFTN Total Inward Tr. " + advice.Cnt + " and TK " + advice.Amount.ToString("N2") + " as per Details Report enclosed. Please respond the entry.");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 11);

                    cb.SetTextMatrix(150, 615 - yCor);
                    cb.ShowText(advice.CounterSignator);
                    cb.SetTextMatrix(150, 612 - yCor);
                    cb.ShowText("________________");
                    cb.SetTextMatrix(150, 600 - yCor);
                    cb.ShowText("Counter Signature");

                    cb.SetTextMatrix(430, 615 - yCor);
                    cb.ShowText(advice.Signator);
                    cb.SetTextMatrix(430, 612 - yCor);
                    cb.ShowText("__________");
                    cb.SetTextMatrix(430, 600 - yCor);
                    cb.ShowText("Signature");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
                    cb.SetTextMatrix(18, 590 - yCor);
                    cb.ShowText("================================================================================");

                    cb.SetTextMatrix(18, 580 - yCor);
                    cb.ShowText("V. No. .....   .........  ......");
                    cb.SetTextMatrix(18, 563 - yCor);
                    cb.ShowText("L. F.  .....   .........  .......                                                                                        Date:");

                    cb.SetTextMatrix(18, 546 - yCor);
                    cb.ShowText("Debit Head Office Account");
                    cb.SetTextMatrix(18, 529 - yCor);
                    cb.ShowText("By Amount TK. " + advice.Amount.ToString("N2") + "                                           as per above noted details");
                    cb.SetTextMatrix(18, 500 - yCor);
                    cb.ShowText("Contra ..............");

                    //cb.SetTextMatrix(150, 512 - yCor);
                    //cb.ShowText(advice.CounterSignator);
                    cb.SetTextMatrix(150, 482 - yCor);
                    cb.ShowText("________________");
                    cb.SetTextMatrix(150, 470 - yCor);
                    cb.ShowText("Counter Signature");

                    //cb.SetTextMatrix(425, 512 - yCor);
                    //cb.ShowText(advice.Signator);
                    cb.SetTextMatrix(425, 482 - yCor);
                    cb.ShowText("__________");
                    cb.SetTextMatrix(425, 470 - yCor);
                    cb.ShowText("Signature");

                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 7);
                    cb.SetTextMatrix(15, 448 - yCor);
                    cb.ShowText("NB: It is a computer generated advice, Originator's signature is not required.");
                    cb.SetTextMatrix(15, 440 - yCor);
                    cb.ShowText("Please check Settlement Report before responding. Please Contact with Local Office for any discrepancy.");
                    cb.SetTextMatrix(15, 435 - yCor);
                    cb.ShowText("- - - -  - - - - -   - - - - - - - - -  -  - -  -  - - - - - - - - - - - -  - - -  - - -  - -  - -  -  -  - -  - - - - -  -  - - - - - - - - - - - -  - -  - - - - -  - - - - -  - - - - - - -  - - - - -  - - -  - - - - - - - Powered by Flora Limited");

                    cb.EndText();
                }
            }
            catch (DocumentException de)
            {

                System.Console.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {

                System.Console.WriteLine(ioe.Message);
            }

            document.Close();

        }

        public void GenerateBranchVoucherForTruncationForAllBranch(AdviceData[] adviceArr, int AdviceCounter,string strRptType, string strRptCode, string signator, string counterSignator)
        {
            Document document = new Document(PageSize.A4, 10, 10, 8, 8);
            try
            {

                int yCor = 0;

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(getRupaliLogo());
                jpg.SetAbsolutePosition(165, 745 - yCor);

                iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(getRupaliLogo());
                jpg2.SetAbsolutePosition(165, 745 - (yCor + 395));

                Response.ClearContent();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "Advice_" + adviceArr[0].SettlementJDate + ".pdf");

                PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
                document.Open();

                for (int i = 0; i <= AdviceCounter; i++)
                {
                    if (adviceArr[i].CreditDebit == "Debit")
                    {
                        yCor = 0;
                        yCor = PrintAdviceForRupaliTruncationForDebit(adviceArr, document, yCor, jpg, jpg2, writer, i, strRptType, strRptCode, signator, counterSignator);
                    }
                    else if (adviceArr[i].CreditDebit == "Credit")//>0
                    {
                        yCor = 0;
                        yCor = PrintAdviceForTruncationRupaliForCredit(adviceArr, document, yCor, jpg, jpg2, writer, i, strRptType, strRptCode, signator, counterSignator);
                    }
                }
            }
            catch (DocumentException de)
            {
                System.Console.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                System.Console.WriteLine(ioe.Message);
            }

            document.Close();

        }

        private int PrintAdviceForTruncationRupaliForCredit(AdviceData[] adviceArr, Document document, int yCor, iTextSharp.text.Image jpg, iTextSharp.text.Image jpg2, PdfWriter writer, int i, string strRptType, string strRptCode, string signator, string counterSignator)
        {
            document.NewPage();
            document.Add(jpg);
            document.Add(jpg2);
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.TIMES_BOLD).BaseFont, 20);
            cb.SetTextMatrix(210, 755 - yCor);
            cb.ShowText("Rupali Bank Ltd.");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12); // set text Sf           
            cb.SetTextMatrix(18, 780 - yCor);
            cb.ShowText("SF- " + strRptCode);

            cb.SetTextMatrix(219, 778); // set text TransCode
            cb.ShowText("Trans: Code 1  2 ");
            cb.MoveTo(285, 774 - yCor);
            cb.LineTo(312, 774 - yCor);
            cb.Stroke();
            cb.MoveTo(285, 774 - yCor);
            cb.LineTo(285, 790 - yCor);
            cb.Stroke();
            cb.MoveTo(285, 790 - yCor);
            cb.LineTo(312, 790 - yCor);
            cb.Stroke();
            cb.MoveTo(312, 790 - yCor);
            cb.LineTo(312, 774 - yCor);
            cb.Stroke();
            cb.MoveTo(299, 774 - yCor);
            cb.LineTo(299, 790 - yCor);
            cb.Stroke();

            cb.SetTextMatrix(470, 770 - yCor); // set text AdviceNo
            cb.ShowText("Advice No :" + adviceArr[i].AdviceCode);

            cb.MoveTo(182, 718 - yCor);  // set text Sender
            cb.LineTo(182, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(182, 730 - yCor);
            cb.LineTo(244, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 730 - yCor);
            cb.LineTo(244, 718 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 718 - yCor);
            cb.LineTo(182, 718 - yCor);
            cb.Stroke();

            cb.MoveTo(196, 718 - yCor);
            cb.LineTo(196, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(210, 718 - yCor);
            cb.LineTo(210, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(224, 718 - yCor);
            cb.LineTo(224, 730 - yCor);
            cb.Stroke();
            cb.SetTextMatrix(18, 720 - yCor);
            cb.ShowText("Sender : Local Office  Dhaka");

            cb.SetTextMatrix(185, 720 - yCor);
            cb.ShowText(boxString("0018"));

            cb.MoveTo(182, 702 - yCor);// set text Reciever
            cb.LineTo(182, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(182, 714 - yCor);
            cb.LineTo(244, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 714 - yCor);
            cb.LineTo(244, 702 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 702 - yCor);
            cb.LineTo(182, 702 - yCor);
            cb.Stroke();

            cb.MoveTo(196, 702 - yCor);
            cb.LineTo(196, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(210, 702 - yCor);
            cb.LineTo(210, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(224, 702 - yCor);
            cb.LineTo(224, 714 - yCor);
            cb.Stroke();

            // -----------------------------
            cb.SetTextMatrix(18, 692 - yCor); // set Reciver Name and Code
            cb.ShowText("Reciever Branch Name : " + adviceArr[i].BranchName);

            cb.SetTextMatrix(18, 704 - yCor);
            cb.ShowText("Receiver  Branch Code :");

            cb.SetTextMatrix(185, 704 - yCor);
            cb.ShowText(boxString(adviceArr[i].SolID));
            //---------------------------------

            cb.SetTextMatrix(470, 718 - yCor); // set text Ref
            cb.ShowText("Ref : BEFTN");

            cb.SetTextMatrix(470, 732 - yCor); // set text Ref
            cb.ShowText("Head Office Copy");

            cb.MoveTo(502, 702 - yCor); // set text Date
            cb.LineTo(502, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(502, 714 - yCor);
            cb.LineTo(582, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(582, 714 - yCor);
            cb.LineTo(582, 702 - yCor);
            cb.Stroke();
            cb.MoveTo(582, 702 - yCor);
            cb.LineTo(502, 702 - yCor);
            cb.Stroke();

            cb.MoveTo(512, 702 - yCor);
            cb.LineTo(512, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(522, 702 - yCor);
            cb.LineTo(522, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(532, 702 - yCor);
            cb.LineTo(532, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(542, 702 - yCor);
            cb.LineTo(542, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(552, 702 - yCor);
            cb.LineTo(552, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(562, 702 - yCor);
            cb.LineTo(562, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(572, 702 - yCor);
            cb.LineTo(572, 714 - yCor);
            cb.Stroke();
            cb.SetTextMatrix(468, 704 - yCor);
            cb.ShowText("Date:  " + boxDate(adviceArr[i].SettlementJDate.ToString()));

            cb.SetTextMatrix(18, 682 - yCor);
            cb.ShowText("================================================================================");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 9);
            cb.SetTextMatrix(18, 673 - yCor);
            if (strRptCode.Equals("180"))
            {
                cb.ShowText("We Have Debited Head office Account with the Sum of TK: " + adviceArr[i].Amount.ToString("N2"));
            }
            else
            {
                cb.ShowText("We Have Credited Head office Account with the Sum of TK: " + adviceArr[i].Amount.ToString("N2"));
            }
            cb.SetTextMatrix(18, 656 - yCor);
            cb.ShowText("TAKA(In word) " + EFTN.Utility.NumberToWordConverter.GetAmountInWords(adviceArr[i].Amount.ToString()));

            //cb.ShowText("TAKA(In word) " + numtoCon.GetAmountInWords(advice.NetAmount.ToString()));
            cb.SetTextMatrix(18, 639 - yCor);
            //cb.ShowText("As per today's BEFTN " + " Net Settlement. Please respond the entry.");
            cb.ShowText("As per today's EFTN Total " + strRptType + " No. " + adviceArr[i].Cnt + " and TK " + adviceArr[i].Amount.ToString("N2") + " as per Details Report enclosed. Please respond the entry.");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 11);

            cb.SetTextMatrix(150, 615 - yCor);
            cb.ShowText(counterSignator);
            cb.SetTextMatrix(150, 612 - yCor);
            cb.ShowText("________________");
            cb.SetTextMatrix(150, 600 - yCor);
            cb.ShowText("Counter Signature");

            cb.SetTextMatrix(430, 615 - yCor);
            cb.ShowText(signator);
            cb.SetTextMatrix(430, 612 - yCor);
            cb.ShowText("__________");
            cb.SetTextMatrix(430, 600 - yCor);
            cb.ShowText("Signature");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
            cb.SetTextMatrix(18, 590 - yCor);
            cb.ShowText("================================================================================");

            cb.SetTextMatrix(18, 580 - yCor);
            cb.ShowText("V. No. .....   .........  ......");
            cb.SetTextMatrix(18, 563 - yCor);
            cb.ShowText("L. F.  .....   .........  .......                                                                                        Date:");

            cb.SetTextMatrix(18, 546 - yCor);
            if (strRptCode.Equals("180"))
            {
                cb.ShowText("Debit Head Office Account");
            }
            else
            {
                cb.ShowText("Credit Head Office Account");
            }
            cb.SetTextMatrix(18, 529 - yCor);
            cb.ShowText("By Amount TK. " + adviceArr[i].Amount.ToString("N2") + "                                           as per above noted details");
            cb.SetTextMatrix(18, 500 - yCor);
            cb.ShowText("Contra ..............");

            //cb.SetTextMatrix(150, 510 - yCor);
            //cb.ShowText(adviceArr[i].CounterSignator);
            cb.SetTextMatrix(150, 482 - yCor);
            cb.ShowText("________________");
            cb.SetTextMatrix(150, 470 - yCor);
            cb.ShowText("Counter Signature");

            //cb.SetTextMatrix(425, 510 - yCor);
            //cb.ShowText(adviceArr[i].Signator);
            cb.SetTextMatrix(425, 482 - yCor);
            cb.ShowText("__________");
            cb.SetTextMatrix(425, 470 - yCor);
            cb.ShowText("Signature");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 7);
            cb.SetTextMatrix(15, 448 - yCor);
            cb.ShowText("NB: It is a computer generated advice, Originator's signature is not required.");
            cb.SetTextMatrix(15, 440 - yCor);
            cb.ShowText("Please check Settlement Report before responding. Please Contact with Local Office for any discrepancy.");
            cb.SetTextMatrix(15, 435 - yCor);
            cb.ShowText("- - - -  - - - - -   - - - - - - - - -  -  - -  -  - - - - - - - - - - - -  - - -  - - -  - -  - -  -  -  - -  - - - - -  -  - - - - - - - - - - - -  - -  - - - - -  - - - - -  - - - - - - -  - - - - -  - - -  - - - - - - - Powered by Flora Limited");

            /////////////////////SECOND PART///////////
            yCor = 395;
            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.TIMES_BOLD).BaseFont, 20);
            cb.SetTextMatrix(210, 755 - yCor);
            cb.ShowText("Rupali Bank Ltd.");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12); // set text Sf           
            cb.SetTextMatrix(18, 780 - yCor);
            cb.ShowText("SF- " + strRptCode);

            cb.SetTextMatrix(219, 778 - yCor); // set text TransCode
            cb.ShowText("Trans: Code 1  2 ");
            cb.MoveTo(285, 774 - yCor);
            cb.LineTo(312, 774 - yCor);
            cb.Stroke();
            cb.MoveTo(285, 774 - yCor);
            cb.LineTo(285, 790 - yCor);
            cb.Stroke();
            cb.MoveTo(285, 790 - yCor);
            cb.LineTo(312, 790 - yCor);
            cb.Stroke();
            cb.MoveTo(312, 790 - yCor);
            cb.LineTo(312, 774 - yCor);
            cb.Stroke();
            cb.MoveTo(299, 774 - yCor);
            cb.LineTo(299, 790 - yCor);
            cb.Stroke();

            cb.SetTextMatrix(470, 770 - yCor); // set text AdviceNo
            cb.ShowText("Advice No :" + adviceArr[i].AdviceCode);

            cb.MoveTo(182, 718 - yCor);  // set text Sender
            cb.LineTo(182, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(182, 730 - yCor);
            cb.LineTo(244, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 730 - yCor);
            cb.LineTo(244, 718 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 718 - yCor);
            cb.LineTo(182, 718 - yCor);
            cb.Stroke();

            cb.MoveTo(196, 718 - yCor);
            cb.LineTo(196, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(210, 718 - yCor);
            cb.LineTo(210, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(224, 718 - yCor);
            cb.LineTo(224, 730 - yCor);
            cb.Stroke();
            cb.SetTextMatrix(18, 720 - yCor);
            cb.ShowText("Sender : Local Office  Dhaka");

            cb.SetTextMatrix(185, 720 - yCor);
            cb.ShowText(boxString("0018"));

            cb.MoveTo(182, 702 - yCor);// set text Reciever
            cb.LineTo(182, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(182, 714 - yCor);
            cb.LineTo(244, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 714 - yCor);
            cb.LineTo(244, 702 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 702 - yCor);
            cb.LineTo(182, 702 - yCor);
            cb.Stroke();

            cb.MoveTo(196, 702 - yCor);
            cb.LineTo(196, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(210, 702 - yCor);
            cb.LineTo(210, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(224, 702 - yCor);
            cb.LineTo(224, 714 - yCor);
            cb.Stroke();

            // -----------------------------
            cb.SetTextMatrix(18, 692 - yCor); // set Reciver Name and Code
            cb.ShowText("Reciever Branch Name : " + adviceArr[i].BranchName);

            cb.SetTextMatrix(18, 704 - yCor);
            cb.ShowText("Receiver  Branch Code :");

            cb.SetTextMatrix(185, 704 - yCor);
            cb.ShowText(boxString(adviceArr[i].SolID));
            //---------------------------------

            cb.SetTextMatrix(470, 718 - yCor); // set text Ref
            cb.ShowText("Ref : BEFTN");

            cb.SetTextMatrix(470, 732 - yCor); // set text Ref
            cb.ShowText("Office Copy");

            cb.MoveTo(502, 702 - yCor); // set text Date
            cb.LineTo(502, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(502, 714 - yCor);
            cb.LineTo(582, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(582, 714 - yCor);
            cb.LineTo(582, 702 - yCor);
            cb.Stroke();
            cb.MoveTo(582, 702 - yCor);
            cb.LineTo(502, 702 - yCor);
            cb.Stroke();

            cb.MoveTo(512, 702 - yCor);
            cb.LineTo(512, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(522, 702 - yCor);
            cb.LineTo(522, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(532, 702 - yCor);
            cb.LineTo(532, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(542, 702 - yCor);
            cb.LineTo(542, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(552, 702 - yCor);
            cb.LineTo(552, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(562, 702 - yCor);
            cb.LineTo(562, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(572, 702 - yCor);
            cb.LineTo(572, 714 - yCor);
            cb.Stroke();
            cb.SetTextMatrix(468, 704 - yCor);
            cb.ShowText("Date:  " + boxDate(adviceArr[i].SettlementJDate.ToString()));

            cb.SetTextMatrix(18, 682 - yCor);
            cb.ShowText("================================================================================");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 9);
            cb.SetTextMatrix(18, 673 - yCor);
            if (strRptCode.Equals("180"))
            {
                cb.ShowText("We Have Debited Head office Account with the Sum of TK: " + adviceArr[i].Amount.ToString("N2"));
            }
            else
            {
                cb.ShowText("We Have Credited Head office Account with the Sum of TK: " + adviceArr[i].Amount.ToString("N2"));
            }
            cb.SetTextMatrix(18, 656 - yCor);
            cb.ShowText("TAKA(In word) " + EFTN.Utility.NumberToWordConverter.GetAmountInWords(adviceArr[i].Amount.ToString()));

            //cb.ShowText("TAKA(In word) " + numtoCon.GetAmountInWords(advice.NetAmount.ToString()));
            cb.SetTextMatrix(18, 639 - yCor);
            //cb.ShowText("As per today's BEFTN " + " Net Settlement. Please respond the entry.");
            cb.ShowText("As per today's EFTN Total " + strRptType + " No. " + adviceArr[i].Cnt + " and TK " + adviceArr[i].Amount.ToString("N2") + " as per Details Report enclosed. Please respond the entry.");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 11);

            cb.SetTextMatrix(150, 615 - yCor);
            cb.ShowText(counterSignator);
            cb.SetTextMatrix(150, 612 - yCor);
            cb.ShowText("________________");
            cb.SetTextMatrix(150, 600 - yCor);
            cb.ShowText("Counter Signature");

            cb.SetTextMatrix(430, 615 - yCor);
            cb.ShowText(signator);
            cb.SetTextMatrix(430, 612 - yCor);
            cb.ShowText("__________");
            cb.SetTextMatrix(430, 600 - yCor);
            cb.ShowText("Signature");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
            cb.SetTextMatrix(18, 590 - yCor);
            cb.ShowText("================================================================================");

            cb.SetTextMatrix(18, 580 - yCor);
            cb.ShowText("V. No. .....   .........  ......");
            cb.SetTextMatrix(18, 563 - yCor);
            cb.ShowText("L. F.  .....   .........  .......                                                                                        Date:");

            cb.SetTextMatrix(18, 546 - yCor);
            if (strRptCode.Equals("180"))
            {
                cb.ShowText("Debit Head Office Account");
            }
            else
            {
                cb.ShowText("Credit Head Office Account");
            }
            cb.SetTextMatrix(18, 529 - yCor);
            cb.ShowText("By Amount TK. " + adviceArr[i].Amount.ToString("N2") + "                                           as per above noted details");
            cb.SetTextMatrix(18, 500 - yCor);
            cb.ShowText("Contra ..............");

            cb.SetTextMatrix(150, 485 - yCor);
            cb.ShowText(counterSignator);
            cb.SetTextMatrix(150, 482 - yCor);
            cb.ShowText("________________");
            cb.SetTextMatrix(150, 470 - yCor);
            cb.ShowText("Counter Signature");

            cb.SetTextMatrix(425, 485 - yCor);
            cb.ShowText(signator);
            cb.SetTextMatrix(425, 482 - yCor);
            cb.ShowText("__________");
            cb.SetTextMatrix(425, 470 - yCor);
            cb.ShowText("Signature");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 7);
            cb.SetTextMatrix(15, 448 - yCor);
            cb.ShowText("NB: It is a computer generated advice, Originator's signature is not required.");
            cb.SetTextMatrix(15, 440 - yCor);
            cb.ShowText("Please check Settlement Report before responding. Please Contact with Local Office for any discrepancy.");
            cb.SetTextMatrix(15, 435 - yCor);
            cb.ShowText("- - - -  - - - - -   - - - - - - - - -  -  - -  -  - - - - - - - - - - - -  - - -  - - -  - -  - -  -  -  - -  - - - - -  -  - - - - - - - - - - - -  - -  - - - - -  - - - - -  - - - - - - -  - - - - -  - - -  - - - - - - - Powered by Flora Limited");

            cb.EndText();
            return yCor;
        }

        private int PrintAdviceForRupaliTruncationForDebit(AdviceData[] adviceArr, Document document, int yCor, iTextSharp.text.Image jpg, iTextSharp.text.Image jpg2, PdfWriter writer, int i, string strRptType, string strRptCode, string signator, string counterSignator)
        {
            document.NewPage();
            document.Add(jpg);
            document.Add(jpg2);
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.TIMES_BOLD).BaseFont, 20);
            cb.SetTextMatrix(210, 755 - yCor);
            cb.ShowText("Rupali Bank Ltd.");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
            cb.SetTextMatrix(18, 780 - yCor);    // set text Sf
            cb.ShowText("SF- " + strRptCode);

            cb.SetTextMatrix(218, 778 - yCor);  // set text TransCode
            cb.ShowText("Trans: Code 1  2 ");
            cb.MoveTo(285, 774 - yCor);
            cb.LineTo(312, 774 - yCor);
            cb.Stroke();
            cb.MoveTo(285, 774 - yCor);
            cb.LineTo(285, 790 - yCor);
            cb.Stroke();
            cb.MoveTo(285, 790 - yCor);
            cb.LineTo(312, 790 - yCor);
            cb.Stroke();
            cb.MoveTo(312, 790 - yCor);
            cb.LineTo(312, 774 - yCor);
            cb.Stroke();
            cb.MoveTo(299, 774 - yCor);
            cb.LineTo(299, 790 - yCor);
            cb.Stroke();

            cb.SetTextMatrix(470, 770 - yCor);// set text AdviceNo
            cb.ShowText("Advice No :" + adviceArr[i].AdviceCode);

            cb.MoveTo(182, 718 - yCor);  // set text Sender
            cb.LineTo(182, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(182, 730 - yCor);
            cb.LineTo(244, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 730 - yCor);
            cb.LineTo(244, 718 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 718 - yCor);
            cb.LineTo(182, 718 - yCor);
            cb.Stroke();

            cb.MoveTo(196, 718 - yCor);
            cb.LineTo(196, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(210, 718 - yCor);
            cb.LineTo(210, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(224, 718 - yCor);
            cb.LineTo(224, 730 - yCor);
            cb.Stroke();
            cb.SetTextMatrix(18, 720 - yCor);
            cb.ShowText("Sender :  Local Office  Dhaka");

            cb.SetTextMatrix(185, 720 - yCor);
            cb.ShowText(boxString("0018"));

            cb.MoveTo(182, 702 - yCor); // set text Reciever
            cb.LineTo(182, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(182, 714 - yCor);
            cb.LineTo(244, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 714 - yCor);
            cb.LineTo(244, 702 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 702 - yCor);
            cb.LineTo(182, 702 - yCor);
            cb.Stroke();


            cb.MoveTo(196, 702 - yCor);
            cb.LineTo(196, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(210, 702 - yCor);
            cb.LineTo(210, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(224, 702 - yCor);
            cb.LineTo(224, 714 - yCor);
            cb.Stroke();


            //  -----------------------------
            cb.SetTextMatrix(18, 692 - yCor); // set Reciver Name and Code
            cb.ShowText("Reciever Branch Name:" + adviceArr[i].BranchName);

            cb.SetTextMatrix(18, 704 - yCor);
            cb.ShowText("Receiver  Branch Code: ");

            cb.SetTextMatrix(185, 704 - yCor);
            cb.ShowText(boxString(adviceArr[i].SolID));
            //   ---------------------------------


            cb.SetTextMatrix(470, 718 - yCor);// set text Ref
            cb.ShowText("Ref : BEFTN");

            cb.SetTextMatrix(470, 732 - yCor); // set text Ref
            cb.ShowText("Head Office Copy");

            cb.MoveTo(502, 702 - yCor);  // set text Date
            cb.LineTo(502, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(502, 714 - yCor);
            cb.LineTo(582, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(582, 714 - yCor);
            cb.LineTo(582, 702 - yCor);
            cb.Stroke();
            cb.MoveTo(582, 702 - yCor);
            cb.LineTo(502, 702 - yCor);
            cb.Stroke();
            cb.MoveTo(512, 702 - yCor);
            cb.LineTo(512, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(522, 702 - yCor);
            cb.LineTo(522, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(532, 702 - yCor);
            cb.LineTo(532, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(542, 702 - yCor);
            cb.LineTo(542, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(552, 702 - yCor);
            cb.LineTo(552, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(562, 702 - yCor);
            cb.LineTo(562, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(572, 702 - yCor);
            cb.LineTo(572, 714 - yCor);
            cb.Stroke();
            cb.SetTextMatrix(468, 704 - yCor);
            cb.ShowText("Date:  " + boxDate(adviceArr[i].SettlementJDate.ToString()));

            cb.SetTextMatrix(18, 682 - yCor);
            cb.ShowText("================================================================================");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 9);
            cb.SetTextMatrix(18, 673 - yCor);
            if (strRptCode.Equals("180"))
            {
                cb.ShowText("We Have Debited Head Office Account with the Sum of TK:" + adviceArr[i].Amount.ToString("N2"));
            }
            else
            {
                cb.ShowText("We Have Credited Head Office Account with the Sum of TK:" + adviceArr[i].Amount.ToString("N2"));
            }
            cb.SetTextMatrix(18, 656 - yCor);
            cb.ShowText("TAKA(In word) " + EFTN.Utility.NumberToWordConverter.GetAmountInWords(adviceArr[i].Amount.ToString()));
            //cb.ShowText("We have debited Head office Account with the sum of TK:" + advice.Amount.ToString());

            cb.SetTextMatrix(18, 639 - yCor);
            //cb.ShowText("As per today's  " + "EFTN" + " Net Settlement. Please respond the entry.");
            cb.ShowText("As per today's EFTN Total " + strRptType + " No. " + adviceArr[i].Cnt + " and TK " + adviceArr[i].Amount.ToString("N2") + " as per Details Report enclosed. Please respond the entry.");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 11);

            cb.SetTextMatrix(150, 615 - yCor);
            cb.ShowText(counterSignator);

            cb.SetTextMatrix(150, 612 - yCor);
            cb.ShowText("________________");
            cb.SetTextMatrix(150, 600 - yCor);
            cb.ShowText("Counter Signature");

            cb.SetTextMatrix(430, 615 - yCor);
            cb.ShowText(signator);
            cb.SetTextMatrix(430, 612 - yCor);
            cb.ShowText("__________");
            cb.SetTextMatrix(430, 600 - yCor);
            cb.ShowText("Signature");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
            cb.SetTextMatrix(18, 590 - yCor);
            cb.ShowText("================================================================================");

            cb.SetTextMatrix(18, 580 - yCor);
            cb.ShowText("V. No. .....   .........  ......");
            cb.SetTextMatrix(18, 563 - yCor);
            cb.ShowText("L. F.  .....   .........  .......                                                                                        Date:");

            cb.SetTextMatrix(18, 546 - yCor);
            if (strRptCode.Equals("180"))
            {
                cb.ShowText("Debit Head Office Account");
            }
            else
            {
                cb.ShowText("Credit Head Office Account");
            }
            //cb.SetTextMatrix(18, 529 - yCor);
            //cb.ShowText("Credit Head Office Account");

            //cb.ShowText("To Amount TK. " + intToMoney(Math.Abs(advice.NetAmount)) + "                                            as per above noted details");
            cb.SetTextMatrix(18, 500 - yCor);
            cb.ShowText("Contra ..............");

            //cb.SetTextMatrix(150, 510 - yCor);
            //cb.ShowText(adviceArr[i].CounterSignator);
            cb.SetTextMatrix(150, 482 - yCor);
            cb.ShowText("________________");
            cb.SetTextMatrix(150, 470 - yCor);
            cb.ShowText("Counter Signature");

            //cb.SetTextMatrix(425, 510 - yCor);
            //cb.ShowText(adviceArr[i].Signator);
            cb.SetTextMatrix(425, 482 - yCor);
            cb.ShowText("__________");
            cb.SetTextMatrix(425, 470 - yCor);
            cb.ShowText("Signature");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 7);
            cb.SetTextMatrix(15, 448 - yCor);
            cb.ShowText("NB: It is a computer generated advice, Originator's signature is not required.");
            cb.SetTextMatrix(15, 440 - yCor);
            cb.ShowText("Please check Settlement Report before responding. Please Contact with Local Office for any discrepancy.");
            cb.SetTextMatrix(15, 435 - yCor);
            cb.ShowText("- - - -  - - - - -   - - - - - - - - -  -  - -  -  - - - - - - - - - - - -  - - -  - - -  - -  - -  -  -  - -  - - - - -  -  - - - - - - - - - - - -  - -  - - - - -  - - - - -  - - - - - - -  - - - - -  - - -  - - - - - - - Powered by Flora Limited");


            ///////////////////////FOR THE SECOND PART///////

            yCor = 395;
            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.TIMES_BOLD).BaseFont, 20);
            cb.SetTextMatrix(210, 755 - yCor);
            cb.ShowText("Rupali Bank Ltd.");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
            cb.SetTextMatrix(18, 780 - yCor);    // set text Sf
            cb.ShowText("SF- " + strRptCode);

            cb.SetTextMatrix(218, 778 - yCor);  // set text TransCode
            cb.ShowText("Trans: Code 1  2 ");
            cb.MoveTo(285, 774 - yCor);
            cb.LineTo(312, 774 - yCor);
            cb.Stroke();
            cb.MoveTo(285, 774 - yCor);
            cb.LineTo(285, 790 - yCor);
            cb.Stroke();
            cb.MoveTo(285, 790 - yCor);
            cb.LineTo(312, 790 - yCor);
            cb.Stroke();
            cb.MoveTo(312, 790 - yCor);
            cb.LineTo(312, 774 - yCor);
            cb.Stroke();
            cb.MoveTo(299, 774 - yCor);
            cb.LineTo(299, 790 - yCor);
            cb.Stroke();

            cb.SetTextMatrix(470, 770 - yCor);// set text AdviceNo
            cb.ShowText("Advice No :" + adviceArr[i].AdviceCode);

            cb.MoveTo(182, 718 - yCor);  // set text Sender
            cb.LineTo(182, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(182, 730 - yCor);
            cb.LineTo(244, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 730 - yCor);
            cb.LineTo(244, 718 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 718 - yCor);
            cb.LineTo(182, 718 - yCor);
            cb.Stroke();

            cb.MoveTo(196, 718 - yCor);
            cb.LineTo(196, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(210, 718 - yCor);
            cb.LineTo(210, 730 - yCor);
            cb.Stroke();
            cb.MoveTo(224, 718 - yCor);
            cb.LineTo(224, 730 - yCor);
            cb.Stroke();
            cb.SetTextMatrix(18, 720 - yCor);
            cb.ShowText("Sender :  Local Office  Dhaka");

            cb.SetTextMatrix(185, 720 - yCor);
            cb.ShowText(boxString("0018"));

            cb.MoveTo(182, 702 - yCor); // set text Reciever
            cb.LineTo(182, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(182, 714 - yCor);
            cb.LineTo(244, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 714 - yCor);
            cb.LineTo(244, 702 - yCor);
            cb.Stroke();
            cb.MoveTo(244, 702 - yCor);
            cb.LineTo(182, 702 - yCor);
            cb.Stroke();


            cb.MoveTo(196, 702 - yCor);
            cb.LineTo(196, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(210, 702 - yCor);
            cb.LineTo(210, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(224, 702 - yCor);
            cb.LineTo(224, 714 - yCor);
            cb.Stroke();


            //  -----------------------------
            cb.SetTextMatrix(18, 692 - yCor); // set Reciver Name and Code
            cb.ShowText("Reciever Branch Name:" + adviceArr[i].BranchName);

            cb.SetTextMatrix(18, 704 - yCor);
            cb.ShowText("Receiver  Branch Code: ");

            cb.SetTextMatrix(185, 704 - yCor);
            cb.ShowText(boxString(adviceArr[i].SolID));
            //   ---------------------------------


            cb.SetTextMatrix(470, 718 - yCor);// set text Ref
            cb.ShowText("Ref : BEFTN");

            cb.SetTextMatrix(470, 732 - yCor); // set text Ref
            cb.ShowText("Office Copy");

            cb.MoveTo(502, 702 - yCor);  // set text Date
            cb.LineTo(502, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(502, 714 - yCor);
            cb.LineTo(582, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(582, 714 - yCor);
            cb.LineTo(582, 702 - yCor);
            cb.Stroke();
            cb.MoveTo(582, 702 - yCor);
            cb.LineTo(502, 702 - yCor);
            cb.Stroke();
            cb.MoveTo(512, 702 - yCor);
            cb.LineTo(512, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(522, 702 - yCor);
            cb.LineTo(522, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(532, 702 - yCor);
            cb.LineTo(532, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(542, 702 - yCor);
            cb.LineTo(542, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(552, 702 - yCor);
            cb.LineTo(552, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(562, 702 - yCor);
            cb.LineTo(562, 714 - yCor);
            cb.Stroke();
            cb.MoveTo(572, 702 - yCor);
            cb.LineTo(572, 714 - yCor);
            cb.Stroke();
            cb.SetTextMatrix(468, 704 - yCor);
            cb.ShowText("Date:  " + boxDate(adviceArr[i].SettlementJDate.ToString()));

            cb.SetTextMatrix(18, 682 - yCor);
            cb.ShowText("================================================================================");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 9);
            cb.SetTextMatrix(18, 673 - yCor);
            if (strRptCode.Equals("180"))
            {
                cb.ShowText("We Have Debited Head office Account with the Sum of TK:" + adviceArr[i].Amount.ToString("N2"));
            }
            else
            {
                cb.ShowText("We Have Credited Head office Account with the Sum of TK:" + adviceArr[i].Amount.ToString("N2"));
            }
            cb.SetTextMatrix(18, 656 - yCor);
            cb.ShowText("TAKA(In word) " + EFTN.Utility.NumberToWordConverter.GetAmountInWords(adviceArr[i].Amount.ToString()));
            //cb.ShowText("We have debited Head office Account with the sum of TK:" + advice.Amount.ToString());

            cb.SetTextMatrix(18, 639 - yCor);
            //cb.ShowText("As per today's  " + "EFTN" + " Net Settlement. Please respond the entry.");
            cb.ShowText("As per today's EFTN Total " + strRptType + " No. " + adviceArr[i].Cnt + " and TK " + adviceArr[i].Amount.ToString("N2") + " as per Details Report enclosed. Please respond the entry.");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 11);

            cb.SetTextMatrix(150, 615 - yCor);
            cb.ShowText(counterSignator);
            cb.SetTextMatrix(150, 612 - yCor);
            cb.ShowText("________________");
            cb.SetTextMatrix(150, 600 - yCor);
            cb.ShowText("Counter Signature");

            cb.SetTextMatrix(430, 615 - yCor);
            cb.ShowText(signator);
            cb.SetTextMatrix(430, 612 - yCor);
            cb.ShowText("__________");
            cb.SetTextMatrix(430, 600 - yCor);
            cb.ShowText("Signature");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
            cb.SetTextMatrix(18, 590 - yCor);
            cb.ShowText("================================================================================");

            cb.SetTextMatrix(18, 580 - yCor);
            cb.ShowText("V. No. .....   .........  ......");
            cb.SetTextMatrix(18, 563 - yCor);
            cb.ShowText("L. F.  .....   .........  .......                                                                                        Date:");

            cb.SetTextMatrix(18, 546 - yCor);
            if (strRptCode.Equals("180"))
            {
                cb.ShowText("Debit Head Office Account");
            }
            else
            {
                cb.ShowText("Credit Head Office Account");
            }
            cb.SetTextMatrix(18, 529 - yCor);
            cb.ShowText("By Amount TK. " + adviceArr[i].Amount.ToString("N2") + "                                           as per above noted details");

            //cb.ShowText("To Amount TK. " + intToMoney(Math.Abs(advice.NetAmount)) + "                                            as per above noted details");
            cb.SetTextMatrix(18, 500 - yCor);
            cb.ShowText("Contra ..............");

            cb.SetTextMatrix(150, 485 - yCor);
            cb.ShowText(counterSignator);
            cb.SetTextMatrix(150, 482 - yCor);
            cb.ShowText("________________");
            cb.SetTextMatrix(150, 470 - yCor);
            cb.ShowText("Counter Signature");

            cb.SetTextMatrix(425, 485 - yCor);
            cb.ShowText(signator);
            cb.SetTextMatrix(425, 482 - yCor);
            cb.ShowText("__________");
            cb.SetTextMatrix(425, 470 - yCor);
            cb.ShowText("Signature");

            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 7);
            cb.SetTextMatrix(15, 448 - yCor);
            cb.ShowText("NB: It is a computer generated advice, Originator's signature is not required.");
            cb.SetTextMatrix(15, 440 - yCor);
            cb.ShowText("Please check Settlement Report before responding. Please Contact with Local Office for any discrepancy.");
            cb.SetTextMatrix(15, 435 - yCor);
            cb.ShowText("- - - -  - - - - -   - - - - - - - - -  -  - -  -  - - - - - - - - - - - -  - - -  - - -  - -  - -  -  -  - -  - - - - -  -  - - - - - - - - - - - -  - -  - - - - -  - - - - -  - - - - - - -  - - - - -  - - -  - - - - - - - Powered by Flora Limited");

            cb.EndText();
            return yCor;
        }

        public string intToMoney(Int64 amount)
        {
            string stramount = amount.ToString();
            int l = stramount.Length;
            if (l > 2)
                stramount = stramount.Substring(0, l - 2) + "." + stramount.Substring(l - 2);

            if (l > 5)
                stramount = stramount.Substring(0, l - 5) + "," + stramount.Substring(l - 5);

            if (l > 7)
                stramount = stramount.Substring(0, l - 7) + "," + stramount.Substring(l - 7);

            if (l > 9)
                stramount = stramount.Substring(0, l - 9) + "," + stramount.Substring(l - 9);

            if (l > 11)
                stramount = stramount.Substring(0, l - 11) + "," + stramount.Substring(l - 11);

            if (l > 13)
                stramount = stramount.Substring(0, l - 13) + "," + stramount.Substring(l - 13);

            return stramount;

        }

        private byte[] getRupaliLogo()
        {
            string rupalilogo = "/9j/4AAQSkZJRgABAQEAYABgAAD/7AARRHVja3kAAQAEAAAAPAAA/+IMWElDQ19QUk9GSUxFAAEBAAAMSExpbm8CEAAAbW50clJHQiBYWVogB84AAgAJAAYAMQAAYWNzcE1TRlQAAAAASUVDIHNSR0IAAAAAAAAAAAAAAAAAAPbWAAEAAAAA0y1IUCAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAARY3BydAAAAVAAAAAzZGVzYwAAAYQAAABsd3RwdAAAAfAAAAAUYmtwdAAAAgQAAAAUclhZWgAAAhgAAAAUZ1hZWgAAAiwAAAAUYlhZWgAAAkAAAAAUZG1uZAAAAlQAAABwZG1kZAAAAsQAAACIdnVlZAAAA0wAAACGdmlldwAAA9QAAAAkbHVtaQAAA/gAAAAUbWVhcwAABAwAAAAkdGVjaAAABDAAAAAMclRSQwAABDwAAAgMZ1RSQwAABDwAAAgMYlRSQwAABDwAAAgMdGV4dAAAAABDb3B5cmlnaHQgKGMpIDE5OTggSGV3bGV0dC1QYWNrYXJkIENvbXBhbnkAAGRlc2MAAAAAAAAAEnNSR0IgSUVDNjE5NjYtMi4xAAAAAAAAAAAAAAASc1JHQiBJRUM2MTk2Ni0yLjEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFhZWiAAAAAAAADzUQABAAAAARbMWFlaIAAAAAAAAAAAAAAAAAAAAABYWVogAAAAAAAAb6IAADj1AAADkFhZWiAAAAAAAABimQAAt4UAABjaWFlaIAAAAAAAACSgAAAPhAAAts9kZXNjAAAAAAAAABZJRUMgaHR0cDovL3d3dy5pZWMuY2gAAAAAAAAAAAAAABZJRUMgaHR0cDovL3d3dy5pZWMuY2gAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAZGVzYwAAAAAAAAAuSUVDIDYxOTY2LTIuMSBEZWZhdWx0IFJHQiBjb2xvdXIgc3BhY2UgLSBzUkdCAAAAAAAAAAAAAAAuSUVDIDYxOTY2LTIuMSBEZWZhdWx0IFJHQiBjb2xvdXIgc3BhY2UgLSBzUkdCAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGRlc2MAAAAAAAAALFJlZmVyZW5jZSBWaWV3aW5nIENvbmRpdGlvbiBpbiBJRUM2MTk2Ni0yLjEAAAAAAAAAAAAAACxSZWZlcmVuY2UgVmlld2luZyBDb25kaXRpb24gaW4gSUVDNjE5NjYtMi4xAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB2aWV3AAAAAAATpP4AFF8uABDPFAAD7cwABBMLAANcngAAAAFYWVogAAAAAABMCVYAUAAAAFcf521lYXMAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAKPAAAAAnNpZyAAAAAAQ1JUIGN1cnYAAAAAAAAEAAAAAAUACgAPABQAGQAeACMAKAAtADIANwA7AEAARQBKAE8AVABZAF4AYwBoAG0AcgB3AHwAgQCGAIsAkACVAJoAnwCkAKkArgCyALcAvADBAMYAywDQANUA2wDgAOUA6wDwAPYA+wEBAQcBDQETARkBHwElASsBMgE4AT4BRQFMAVIBWQFgAWcBbgF1AXwBgwGLAZIBmgGhAakBsQG5AcEByQHRAdkB4QHpAfIB+gIDAgwCFAIdAiYCLwI4AkECSwJUAl0CZwJxAnoChAKOApgCogKsArYCwQLLAtUC4ALrAvUDAAMLAxYDIQMtAzgDQwNPA1oDZgNyA34DigOWA6IDrgO6A8cD0wPgA+wD+QQGBBMEIAQtBDsESARVBGMEcQR+BIwEmgSoBLYExATTBOEE8AT+BQ0FHAUrBToFSQVYBWcFdwWGBZYFpgW1BcUF1QXlBfYGBgYWBicGNwZIBlkGagZ7BowGnQavBsAG0QbjBvUHBwcZBysHPQdPB2EHdAeGB5kHrAe/B9IH5Qf4CAsIHwgyCEYIWghuCIIIlgiqCL4I0gjnCPsJEAklCToJTwlkCXkJjwmkCboJzwnlCfsKEQonCj0KVApqCoEKmAquCsUK3ArzCwsLIgs5C1ELaQuAC5gLsAvIC+EL+QwSDCoMQwxcDHUMjgynDMAM2QzzDQ0NJg1ADVoNdA2ODakNww3eDfgOEw4uDkkOZA5/DpsOtg7SDu4PCQ8lD0EPXg96D5YPsw/PD+wQCRAmEEMQYRB+EJsQuRDXEPURExExEU8RbRGMEaoRyRHoEgcSJhJFEmQShBKjEsMS4xMDEyMTQxNjE4MTpBPFE+UUBhQnFEkUahSLFK0UzhTwFRIVNBVWFXgVmxW9FeAWAxYmFkkWbBaPFrIW1hb6Fx0XQRdlF4kXrhfSF/cYGxhAGGUYihivGNUY+hkgGUUZaxmRGbcZ3RoEGioaURp3Gp4axRrsGxQbOxtjG4obshvaHAIcKhxSHHscoxzMHPUdHh1HHXAdmR3DHeweFh5AHmoelB6+HukfEx8+H2kflB+/H+ogFSBBIGwgmCDEIPAhHCFIIXUhoSHOIfsiJyJVIoIiryLdIwojOCNmI5QjwiPwJB8kTSR8JKsk2iUJJTglaCWXJccl9yYnJlcmhya3JugnGCdJJ3onqyfcKA0oPyhxKKIo1CkGKTgpaymdKdAqAio1KmgqmyrPKwIrNitpK50r0SwFLDksbiyiLNctDC1BLXYtqy3hLhYuTC6CLrcu7i8kL1ovkS/HL/4wNTBsMKQw2zESMUoxgjG6MfIyKjJjMpsy1DMNM0YzfzO4M/E0KzRlNJ402DUTNU01hzXCNf02NzZyNq426TckN2A3nDfXOBQ4UDiMOMg5BTlCOX85vDn5OjY6dDqyOu87LTtrO6o76DwnPGU8pDzjPSI9YT2hPeA+ID5gPqA+4D8hP2E/oj/iQCNAZECmQOdBKUFqQaxB7kIwQnJCtUL3QzpDfUPARANER0SKRM5FEkVVRZpF3kYiRmdGq0bwRzVHe0fASAVIS0iRSNdJHUljSalJ8Eo3Sn1KxEsMS1NLmkviTCpMcky6TQJNSk2TTdxOJU5uTrdPAE9JT5NP3VAnUHFQu1EGUVBRm1HmUjFSfFLHUxNTX1OqU/ZUQlSPVNtVKFV1VcJWD1ZcVqlW91dEV5JX4FgvWH1Yy1kaWWlZuFoHWlZaplr1W0VblVvlXDVchlzWXSddeF3JXhpebF69Xw9fYV+zYAVgV2CqYPxhT2GiYfViSWKcYvBjQ2OXY+tkQGSUZOllPWWSZedmPWaSZuhnPWeTZ+loP2iWaOxpQ2maafFqSGqfavdrT2una/9sV2yvbQhtYG25bhJua27Ebx5veG/RcCtwhnDgcTpxlXHwcktypnMBc11zuHQUdHB0zHUodYV14XY+dpt2+HdWd7N4EXhueMx5KnmJeed6RnqlewR7Y3vCfCF8gXzhfUF9oX4BfmJ+wn8jf4R/5YBHgKiBCoFrgc2CMIKSgvSDV4O6hB2EgITjhUeFq4YOhnKG14c7h5+IBIhpiM6JM4mZif6KZIrKizCLlov8jGOMyo0xjZiN/45mjs6PNo+ekAaQbpDWkT+RqJIRknqS45NNk7aUIJSKlPSVX5XJljSWn5cKl3WX4JhMmLiZJJmQmfyaaJrVm0Kbr5wcnImc951kndKeQJ6unx2fi5/6oGmg2KFHobaiJqKWowajdqPmpFakx6U4pammGqaLpv2nbqfgqFKoxKk3qamqHKqPqwKrdavprFys0K1ErbiuLa6hrxavi7AAsHWw6rFgsdayS7LCszizrrQltJy1E7WKtgG2ebbwt2i34LhZuNG5SrnCuju6tbsuu6e8IbybvRW9j74KvoS+/796v/XAcMDswWfB48JfwtvDWMPUxFHEzsVLxcjGRsbDx0HHv8g9yLzJOsm5yjjKt8s2y7bMNcy1zTXNtc42zrbPN8+40DnQutE80b7SP9LB00TTxtRJ1MvVTtXR1lXW2Ndc1+DYZNjo2WzZ8dp22vvbgNwF3IrdEN2W3hzeot8p36/gNuC94UThzOJT4tvjY+Pr5HPk/OWE5g3mlucf56noMui86Ubp0Opb6uXrcOv77IbtEe2c7ijutO9A78zwWPDl8XLx//KM8xnzp/Q09ML1UPXe9m32+/eK+Bn4qPk4+cf6V/rn+3f8B/yY/Sn9uv5L/tz/bf///9sAQwACAQECAQECAgICAgICAgMFAwMDAwMGBAQDBQcGBwcHBgcHCAkLCQgICggHBwoNCgoLDAwMDAcJDg8NDA4LDAwM/9sAQwECAgIDAwMGAwMGDAgHCAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwM/8AAEQgAKQAqAwEiAAIRAQMRAf/EAB8AAAEFAQEBAQEBAAAAAAAAAAABAgMEBQYHCAkKC//EALUQAAIBAwMCBAMFBQQEAAABfQECAwAEEQUSITFBBhNRYQcicRQygZGhCCNCscEVUtHwJDNicoIJChYXGBkaJSYnKCkqNDU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6g4SFhoeIiYqSk5SVlpeYmZqio6Slpqeoqaqys7S1tre4ubrCw8TFxsfIycrS09TV1tfY2drh4uPk5ebn6Onq8fLz9PX29/j5+v/EAB8BAAMBAQEBAQEBAQEAAAAAAAABAgMEBQYHCAkKC//EALURAAIBAgQEAwQHBQQEAAECdwABAgMRBAUhMQYSQVEHYXETIjKBCBRCkaGxwQkjM1LwFWJy0QoWJDThJfEXGBkaJicoKSo1Njc4OTpDREVGR0hJSlNUVVZXWFlaY2RlZmdoaWpzdHV2d3h5eoKDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uLj5OXm5+jp6vLz9PX29/j5+v/aAAwDAQACEQMRAD8A/fyvnz/gob/wUM8F/wDBPj4JXPibxNKLvVrotDomixShbrV5x2UfwxrkFnxgD3IFP/4KEf8ABQjwR/wT6+C8ninxTcPd6ndB49G0OCUJdazOBwinB2IMjdIQQo7E4B/n/wDiv8V/FH7bHxO1L46fHbU7y40Wa5Nppum2Y2LKoJaLTbCM/chQN8znLElmZmdmY+hgsH7T95U+H8/JH6HwXwZ/aFswzBNYaLskviqy6QgurfV7JX1Vm4/Snjz/AIKI/tWftO/Cy4+N3w88TeK/Dlr4OZD4h8NadCkmm28SjAvYFKFpbZtoEiSbijbiCVPy/pH/AMEmv+CtHhn/AIKIfDkWV4LXQ/iZo0CnVtHEg23agYNzbZ5aM9SvJQnByMGviX9nn/goH+0X8GPhNat8Nf2YvByfDOODdLHM91cahcwqmS004kVFO0HjydozgACvmL4meEbXxPqt1+0f+y/aa/8ADnxH4KuF1Txf4LV9954Onc7/ALZbBhifTpMnchTagJUoEO0djoRrRcGkrbNa28n/AJn2tTI8Jm+Gng69KlQcZWpVKc4TUG3ZUqzg3u9FJ6X0Tbtzf0gA5APrRXxr/wAEmv8Agrb4a/4KJfDeK01J7PQvido8AGsaOjFYrvHBurXJyY26lclkJwScBj9kLIHUMASCM149WlKnJwkrNH4rmeW4nL8TPB4yDjOLs0/zXdPo1oz8Fv8Agsb8K08Q/wDBWW4m+MHi2Xw14A1N7C30GadJLmNLUwoJGEakGOBJd/mOO578ke0ftc/sbeDNJ/bS+C3hOx8RaD4Z+HHhbwBJ4is9Wv1jl0q9m+1Ms80pOFYGIwAbWGAVOQOv2z/wVD/4Je+Ev+CjPwkWzv5INF8b6PG50HXfKDG3cjPkzY5eBj1HVc5HI5/FSDVtQ8FW8n7Kf7TZ1LwhZeGtQc+G/EdwrTS+DppPl3DH/HxpVwACdp2qMOpGCB7FGSrwTUrOKtby7rv5n7LkFWGeYGi6NeUamGpunKmop2hJcrq0o296VtJ3u3d2+zGX7TfF/wAX+BvB37Jtr4i1TxRoC+EvD0i6bc6jolpNqHhq2YSBUmvLS2nVmhjk2K37zgnkgHI+QvAOqfC5f+Cl3wi8SfCTxv4Z8beJvGNvqNt8QBpDE21/A/lCOadBmKJTkqIuSoVcs2Cx8o8Hfsr/ALcvwB+EVx8K/B8PhPX/AIaa1bNbw3un2sF9YahaTIQJVbYcB1bduPc5yetfFesy3X7OGtal8LvhjqY1/wAZavu0zXtb0iQOHblZLK1kTI8sfMJJQxU4bDYGamhhYPmjGd99tku77W/MxyLhfDVadfD4XGKpJqS91rkjTkmnKt7seTl+JQUleSVkuXmX0l8Uvif8Jf2fP+CuOmSfs0pfNCPEES6xdwujaXC+7FxaaftG42zE5diSu4FU+QCv360dhfaTazuoDzQo7YyBkqDX5lf8EYv+CNVj8DtA074gePbOO41+7jWe0tZo8eWp5Vyp5Ve4zy3Xpiv09jhCRquSMDFcGMrQnJKnqkrX7/10PhON82wWNxdOngXKcaUVD2k23Ko19rXW3RLstkPIB6gHFfKX/BUb/gl34V/4KM/CrybpbXRvHmiow0DXhHl4cnP2ebHLwMc5X+EncMHOfq2oNR/49j9awpVJQkpQdmj5nL8xxOBxEMZhJuFSDumv61T2aejWjPwl/Z/tv2n/AIU/B/xl+yj4107x14f8O6ujaboniOxie4XQZN+fJ81eX06cZVthDIrfLwWWvq3/AIJH/wDBD+D9mZz4p+JNjYX3iWJgsNspEsKFTkEeqZAIHfHPpX6VQf8AIPH4Utt/rh/u10V8ZOacNEnvbS59NnPGuNx1Orh4RjRhValNU1ZTltd3betldJ2b1te95Uj3feGAO3rUlFFcZ8akf//Z";
            byte[] bytes = Convert.FromBase64String(rupalilogo);
            return bytes;
        }

        protected void btnExpotToCSV_Click(object sender, EventArgs e)
        {
            PrintCustomerAdviceDB printDB = new PrintCustomerAdviceDB();
            
            DataTable dtCSVData = printDB.GetReceivedEDRForBranchAdviceExcel(ParseData.StringToInt(ddListBranch.SelectedValue)
                                                                , ParseData.StringToInt(ddListTransactionType.SelectedValue)
                                                                , ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'));

            if (dtCSVData.Rows.Count > 0)
            {
                ExportToCSV(dtCSVData);
            }
        }

        protected void btnExportToText_Click(object sender, EventArgs e)
        {
            PrintCustomerAdviceDB printDB = new PrintCustomerAdviceDB();

            DataTable dtCSVData = printDB.GetReceivedEDRForBranchAdviceExcel(ParseData.StringToInt(ddListBranch.SelectedValue)
                                                                , ParseData.StringToInt(ddListTransactionType.SelectedValue)
                                                                , ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'));

            if (dtCSVData.Rows.Count > 0)
            {
                GenerateTextFile(dtCSVData);
            }

        }

        private void ExportToCSV(DataTable dtCSVData)
        {
            string xlsFileName = "Inward" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
            string attachment = "attachment; filename=" + xlsFileName + ".csv";

            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.csv";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create the CSV file to which grid data will be exported. 
            //StreamWriter sw = new StreamWriter();
            int iColCount = dtCSVData.Columns.Count;

            // First we will write the headers. 

            for (int i = 0; i < iColCount; i++)
            {
                sw.Write("\"");
                sw.Write(dtCSVData.Columns[i]);
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
            foreach (DataRow dr in dtCSVData.Rows)
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

        private void GenerateTextFile(DataTable dt)
        {
            string flatfileResult = CreateFlatFile(dt);
            string fileName = "CustomerAdvice-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
            Response.Clear();
            Response.AddHeader("content-disposition",
                     "attachment;filename=" + fileName + ".txt");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.text";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite =
                          new HtmlTextWriter(stringWrite);
            Response.Write(flatfileResult.ToString());
            Response.End();

            //Response.Redirect("~/FileWatcherChecker.aspx?PathKey=EFTCBSExport");
        }

        private string CreateFlatFile(DataTable dt)
        {
            string delim = "|";

            string result = string.Empty;

            //double totalDebitAmount = 0;

            foreach (DataRow row in dt.Rows)
            {
                string line = row["TZN_CD"].ToString() + delim;
                line += row["TBR_CD"].ToString() + delim;
                line += row["TR_DATE"].ToString() + delim;
                line += row["TR_TYPE"].ToString() + delim;
                line += row["SL_NO"].ToString() + delim;
                line += row["RO_ZNCD"].ToString() + delim;
                line += row["ROBR_CD"].ToString() + delim;
                line += row["ORG_DATE"].ToString() + delim;
                line += row["ADV_NO"].ToString() + delim;
                line += row["TR_CD"].ToString() + delim;
                line += ParseData.StringToDouble(row["DR_AMT"].ToString()).ToString() + delim;
                line += ParseData.StringToDouble(row["CR_AMT"].ToString()).ToString() + delim;
                line += row["Particulars"].ToString();


                //"TZN_CD","TBR_CD","TR_DATE","TR_TYPE","SL_NO","RO_ZNCD","ROBR_CD","ORG_DATE","ADV_NO","TR_CD","Cnt","CR_AMT","DR_AMT","Particulars"
                //totalDebitAmount += EFTN.Utility.ParseData.StringToDouble(Amount.Trim());

                //if (AccountNo.Trim().Length == 13)
                //{

                if (result.Equals(string.Empty))
                {
                    result += line;
                }

                else
                {
                    result += "\n" + line;
                }
                //}
            }
            return result;
        }

        private DataTable GetDetailSettlementReportData(string ReportType)
        {
            string currency = string.Empty;
            string session = string.Empty;
            DetailSettlementReportDB detailSettlementReportDB = new DetailSettlementReportDB();

            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            int BranchID = EFTN.Utility.ParseData.StringToInt(ddListBranch.SelectedValue);

            if (ReportType.Equals("102"))
            {

                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ReportType.Equals("202"))
            {
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ReportType.Equals("104"))
            {
                return detailSettlementReportDB.GetReportForReturnSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ReportType.Equals("204"))
            {
                return detailSettlementReportDB.GetReportForReturnSentBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);

            }
            else if (ReportType.Equals("112"))
            {
                return dtSettlementReport = detailSettlementReportDB.GetReportForTransactionSentBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID, currency,session);

            }
            else if (ReportType.Equals("212"))
            {
                return dtSettlementReport = detailSettlementReportDB.GetReportForTransactionSentBySettlementDateForDebit(
                                                                 ddlistYear.SelectedValue.PadLeft(4, '0')
                                                               + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            else if (ReportType.Equals("105"))
            {
                return detailSettlementReportDB.GetReportForReturnReceivedBySettlementDateForCredit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            else //if (ReportType.Equals("205"))
            {
                return detailSettlementReportDB.GetReportForReturnReceivedBySettlementDateForDebit(
                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }

        }

        private DataTable GetTransactionSentCreditForBranchAdviceForRupali(int BranchID, int userID)
        {
            int Credit = 1;//Credit

            PrintCustomerAdviceDB printCustomerAdviceDB = new PrintCustomerAdviceDB();
            return printCustomerAdviceDB.GetTransactionSentForBranchAdviceForRupali(BranchID, Credit
                                                                , ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), userID);
        }


        private DataTable GetTransactionSentDebitForBranchAdviceForRupali(int BranchID, int userID)
        {
            int Credit = 0;//Debit

            PrintCustomerAdviceDB printCustomerAdviceDB = new PrintCustomerAdviceDB();
            return printCustomerAdviceDB.GetTransactionSentForBranchAdviceForRupali(BranchID, Credit
                                                                , ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), userID);
        }

        private DataTable GetReturnSentCreditForBranchAdviceForRupali(int BranchID, int userID)
        {
            int Credit = 1;//Credit

            PrintCustomerAdviceDB printCustomerAdviceDB = new PrintCustomerAdviceDB();
            return printCustomerAdviceDB.GetReturnSentForBranchAdviceForRupali(BranchID, Credit
                                                                , ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), userID);
        }

        private DataTable GetReturnSentDebitForBranchAdviceForRupali(int BranchID, int userID)
        {
            int Credit = 0;//Debit

            PrintCustomerAdviceDB printCustomerAdviceDB = new PrintCustomerAdviceDB();
            return printCustomerAdviceDB.GetReturnSentForBranchAdviceForRupali(BranchID, Credit
                                                                , ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), userID);
        }

        private DataTable GetReturnReceivedCreditForBranchAdviceForRupali(int BranchID, int userID)
        {
            int Credit = 1;//Credit

            PrintCustomerAdviceDB printCustomerAdviceDB = new PrintCustomerAdviceDB();
            return printCustomerAdviceDB.GetReturnReceivedForBranchAdviceForRupali(BranchID, Credit
                                                                , ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), userID);
        }

        private DataTable GetReturnReceivedDebitForBranchAdviceForRupali(int BranchID, int userID)
        {
            int Credit = 0;//Debit

            PrintCustomerAdviceDB printCustomerAdviceDB = new PrintCustomerAdviceDB();
            return printCustomerAdviceDB.GetReturnReceivedForBranchAdviceForRupali(BranchID, Credit
                                                                , ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), userID);
        }

        protected void btnExportCSV_Click(object sender, EventArgs e)
        {

            string OriginBankCode = ConfigurationManager.AppSettings["OriginBank"].ToString().Substring(0, 3);
            string BankName = ConfigurationManager.AppSettings["CompanyName"].ToString();

            string fileName = "Advice Report - " + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            if (OriginBankCode.Equals("185"))
            {
                ExportToCSVForRupali(fileName, BankName, OriginBankCode);
            }
        }

        private void ExportToCSVForRupali(string FileName, string BankName, string OriginBankCode)
        {
            int BranchID = ParseData.StringToInt(ddListBranch.SelectedValue);
            int userID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            int TransactionType = ParseData.StringToInt(ddListTransactionType.SelectedValue);

            SignatureDB signatureDB = new SignatureDB();
            SqlDataReader sqlDRSign = signatureDB.GetSignator(ddlistYear.SelectedValue.PadLeft(4, '0')
                                           + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                           + ddlistDay.SelectedValue.PadLeft(2, '0'));

            string signator = string.Empty;
            string counterSignator = string.Empty;
            while (sqlDRSign.Read())
            {
                signator = sqlDRSign["Signator"].ToString();
                counterSignator = sqlDRSign["CounterSignator"].ToString();
            }

            if (sqlDRSign.HasRows)
            {
                if (ddListBranch.Items.Count > 1)
                {
                    if (ddListBranch.SelectedValue.Equals("0"))
                    {
                        GenerateAllBranchCSV(userID, signator, counterSignator);
                    }
                }
            }
        }

        private void GenerateAllBranchCSV(int userID, string signator, string counterSignator)
        {
            int TransactionType = ParseData.StringToInt(ddListTransactionType.SelectedValue);
            int ReportType = ParseData.StringToInt(ddListReportType.SelectedValue);

            PrintCustomerAdviceDB printDB = new PrintCustomerAdviceDB();

            DataTable dtBranchID = new DataTable();

            if (ReportType == 1)
            {
                dtBranchID = printDB.GetBranchIDForTxnReceivedBySettlementDateForAdvice(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), ParseData.StringToInt(ddListDhakaNonDhaka.SelectedValue));
            }
            else if (ReportType == 2)
            {
                dtBranchID = printDB.GetBranchIDForReturnSentBySettlementDateForAdvice(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), ParseData.StringToInt(ddListDhakaNonDhaka.SelectedValue));
            }
            else if (ReportType == 3)
            {
                dtBranchID = printDB.GetBranchIDForTXNSentBySettlementDateForAdvice(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), ParseData.StringToInt(ddListDhakaNonDhaka.SelectedValue));
            }
            else if (ReportType == 4)
            {
                dtBranchID = printDB.GetBranchIDForReturnReceivedBySettlementDateForAdvice(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), ParseData.StringToInt(ddListDhakaNonDhaka.SelectedValue));
            }
            AdviceData[] advArr = new AdviceData[dtBranchID.Rows.Count];

            int AdviceCounter = -1;
            DataTable dt;

            DataTable dtExcelAllBranchData = new DataTable();
            string strRptType = string.Empty;
            string strRptCode = string.Empty;

            for (int i = 0; i < dtBranchID.Rows.Count; i++)
            {
                int BranchID = ParseData.StringToInt(dtBranchID.Rows[i]["BranchID"].ToString());

                if (ReportType == 1)
                {
                    if (TransactionType == 1)
                    {
                        dt = GetDataForBranchAdviceForRupali(1, BranchID, userID);
                        strRptType = "Inward Credit Transaction";
                        strRptCode = "179";
                    }
                    else
                    {
                        dt = GetDataForBranchAdviceForRupali(2, BranchID, userID);
                        strRptType = "Inward Debit Transaction";
                        strRptCode = "180";
                    }
                }
                else if (ReportType == 2)
                {
                    if (TransactionType == 1)
                    {
                        dt = GetReturnSentCreditForBranchAdviceForRupali(BranchID, userID);
                        strRptType = "Outward Credit Return";
                        strRptCode = "180";
                    }
                    else
                    {
                        dt = GetReturnSentDebitForBranchAdviceForRupali(BranchID, userID);
                        strRptType = "Outward Debit Return";
                        strRptCode = "179";
                    }
                }
                else if (ReportType == 3)
                {
                    if (TransactionType == 1)
                    {
                        dt = GetTransactionSentCreditForBranchAdviceForRupali(BranchID, userID);
                        strRptType = "Outward Credit Transaction";
                        strRptCode = "180";
                    }
                    else
                    {
                        dt = GetTransactionSentDebitForBranchAdviceForRupali(BranchID, userID);
                        strRptType = "Outward Debit Transaction";
                        strRptCode = "179";
                    }
                }
                else //if (ReportType == 4)
                {
                    if (TransactionType == 1)
                    {
                        dt = GetReturnReceivedCreditForBranchAdviceForRupali(BranchID, userID);
                        strRptType = "Inward Credit Return";
                        strRptCode = "179";
                    }
                    else
                    {
                        dt = GetReturnReceivedDebitForBranchAdviceForRupali(BranchID, userID);
                        strRptType = "Inward Debit Return";
                        strRptCode = "180";
                    }
                }

                if (dtExcelAllBranchData.Rows.Count < 1)
                {
                    dtExcelAllBranchData = dt.Clone();
                }
                dtExcelAllBranchData.Merge(dt);

            }
            if (dtExcelAllBranchData.Rows.Count > 0)
            {
                ExportToCSV(dtExcelAllBranchData);
            }
        }
    }
}
