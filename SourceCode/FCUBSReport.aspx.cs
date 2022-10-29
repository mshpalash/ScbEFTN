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
    public partial class FCUBSReport : System.Web.UI.Page
    {
        private static DataTable dtSettlementReport = new DataTable();
        DataView dv;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2,'0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');
                sortOrder = "asc";
            }
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

        protected void dtgSettlementReport_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = dtSettlementReport.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgSettlementReport.DataSource = dv;
            dtgSettlementReport.DataBind();
        }

        protected void dtgSettlementReport_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgSettlementReport.CurrentPageIndex = e.NewPageIndex;
            dtgSettlementReport.DataSource = dtSettlementReport;
            dtgSettlementReport.DataBind();
        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            if (ddListReportType.SelectedValue.Equals("9"))
            {
                string FileName = "SettlementReport-Branchwise-TransactionReceivedAll" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("109"))
            {
                string FileName = "SettlementReport-Branchwise-TransactionReceivedAllCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("209"))
            {
                string FileName = "SettlementReport-Branchwise-TransactionReceivedAllDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFBranchWise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("25"))
            {
                string FileName = "Department Wise Transaction Sent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("125"))
            {
                string FileName = "Department Wise Transaction Sent Credit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("225"))
            {
                string FileName = "Department Wise Transaction Sent Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("26"))
            {
                string FileName = "Department Wise Inward Return" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("126"))
            {
                string FileName = "Department Wise Inward Return Credit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
            else if (ddListReportType.SelectedValue.Equals("226"))
            {
                string FileName = "Department Wise Inward Return Debit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDepartmentwise(FileName);
            }
        }

        private DataTable GetDataBranchWise(int branchID)
        {
            FCUBSReportDB detailSettlementReportDB = new FCUBSReportDB();

            if (ddListReportType.SelectedValue.Equals("9"))
            {
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDate(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);
            }
            if (ddListReportType.SelectedValue.Equals("109"))
            {
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateForCredit(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);
            }
            if (ddListReportType.SelectedValue.Equals("209"))
            {
                return detailSettlementReportDB.GetReportForTransactionReceivedBySettlementDateForDebit(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'), branchID);
            }

            return new DataTable();
        }

        //public DataTable GetBranches()
        //{
        //    DetailSettlementReportDB detailSettlementReportDB = new DetailSettlementReportDB();
        //    if (ddListReportType.SelectedValue.Equals("9"))
        //    {
        //        return detailSettlementReportDB.GetBranchForTransactionReceivedBySettlementDate(
        //                                                           ddlistYear.SelectedValue.PadLeft(4, '0')
        //                                                         + ddlistMonth.SelectedValue.PadLeft(2, '0')
        //                                                         + ddlistDay.SelectedValue.PadLeft(2, '0'));

        //    }
        //    if (ddListReportType.SelectedValue.Equals("109"))
        //    {
        //        return detailSettlementReportDB.GetBranchForTransactionReceivedBySettlementDateForCredit(
        //                                                           ddlistYear.SelectedValue.PadLeft(4, '0')
        //                                                         + ddlistMonth.SelectedValue.PadLeft(2, '0')
        //                                                         + ddlistDay.SelectedValue.PadLeft(2, '0'));

        //    }
        //    if (ddListReportType.SelectedValue.Equals("209"))
        //    {
        //        return detailSettlementReportDB.GetBranchForTransactionReceivedBySettlementDateForDebit(
        //                                                           ddlistYear.SelectedValue.PadLeft(4, '0')
        //                                                         + ddlistMonth.SelectedValue.PadLeft(2, '0')
        //                                                         + ddlistDay.SelectedValue.PadLeft(2, '0'));

        //    }  
        //    return new DataTable();
        //}

        public DataTable GetActiveBranches()
        {
            FCUBSReportDB branchDB = new FCUBSReportDB();
            return branchDB.GetCBSActiveBranches();
        }

        public DataTable GetActiveDepartment()
        {
            FCUBSReportDB departmentDB = new FCUBSReportDB();
            return departmentDB.GetCBSActiveDepartments();
        }

        private void PrintPDFBranchWise(string FileName)
        {
            DataTable dtBranch = GetActiveBranches();

            if (dtBranch.Rows.Count == 0)
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

            double allBranchesTotalAmount = 0;
            int allBranchesTotalItem = 0;
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            
            for (int brRow = 0; brRow < dtBranch.Rows.Count; brRow++)
            {
                string LogoImage = Server.MapPath("images") + "\\SCBLogo.JPG";
                DataTable dt = GetDataBranchWise(ParseData.StringToInt(dtBranch.Rows[brRow]["BranchID"].ToString()));

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


                document.Add(new iTextSharp.text.Paragraph(" "));
                iTextSharp.text.pdf.PdfPTable datatable2 = new iTextSharp.text.pdf.PdfPTable(13);
                datatable2.DefaultCell.Padding = 3;
                datatable2.DefaultCell.Border = 2;
                datatable2.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //float[] headerwidths2 = { 15,35,20,15,15};
                float[] headerwidths2 = { 10, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,10 };
                datatable2.DefaultCell.BorderWidth = 2;
                datatable2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                datatable2.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;

                if (ddListReportType.SelectedValue.Equals("9"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Transaction Received All Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("109"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Transaction Received All Credit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("209"))
                {
                    headertable.AddCell(new Phrase("Branchwise-Transaction Received All Debit Report: " + settlementDate, headerFont));
                }
                headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
                headertable.AddCell(logo); ;
                document.Add(headertable);

                document.Add(new iTextSharp.text.Paragraph(" "));
                iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(13);
                datatable.DefaultCell.Padding = 4;
                datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                float[] headerwidths = { 10, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,10 };
                datatable.SetWidths(headerwidths);
                datatable.WidthPercentage = 99;

                iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                datatable.DefaultCell.BorderWidth = 0.5f;
                datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);

                //datatable2.HeaderRows = 1;
                //datatable2.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
                //datatable2.SetWidths(headerwidths);
                ////datatable2.DefaultCell.BorderWidth = 0.25f;
                //datatable.WidthPercentage = 99;
                //------------------------------------------

                //PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
                //c0.BorderWidth = 0.5f;
                //c0.HorizontalAlignment = Cell.ALIGN_LEFT;
                //c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
                //c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //c0.Padding = 4;

                //datatable.AddCell(c0);


                datatable2.AddCell(new iTextSharp.text.Phrase("Bank Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("CBSReference", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("FloraReference", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("TraceNo", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("DFIAccNo", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("BankRoutNo", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Receiver /Payer Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Company Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Ent.Desc", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("RejectReason", fnt));


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankName"], fnt));
                    c1.BorderWidth = 0.5f;
                    c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c1.Padding = 1;
                    datatable2.AddCell(c1);

                    PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BranchName"], fnt));
                    c2.BorderWidth = 0.5f;
                    c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c2.Padding = 4;
                    datatable2.AddCell(c2);

                    PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["CBSReference"], fnt));
                    c3.BorderWidth = 0.5f;
                    c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c3.Padding = 4;
                    datatable2.AddCell(c3);

                    datatable2.AddCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["FloraReference"], fnt));


                    PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TraceNumber"], fnt));
                    c4.BorderWidth = 0.5f;
                    c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c4.Padding = 4;
                    datatable2.AddCell(c4);

                    //PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                    //c5.BorderWidth = 0.5f;
                    //c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    //c5.Padding = 4;
                    //datatable2.AddCell(c5);

                    PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["DFIAccountNo"], fnt));
                    c6.BorderWidth = 0.5f;
                    c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c6.Padding = 4;
                    datatable2.AddCell(c6);

                    PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankRoutingNo"], fnt));
                    c7.BorderWidth = 0.5f;
                    c7.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c7.Padding = 4;
                    datatable2.AddCell(c7);


                    PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dt.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                    c8.BorderWidth = 0.5f;
                    c8.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c8.Padding = 4;
                    datatable2.AddCell(c8);

                    PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["IdNumber"], fnt));
                    c9.BorderWidth = 0.5f;
                    c9.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c9.Padding = 4;
                    datatable2.AddCell(c9);

                    PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["ReceiverName"], fnt));
                    c10.BorderWidth = 0.5f;
                    c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c10.Padding = 4;
                    datatable2.AddCell(c10);

                    PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["CompanyName"], fnt));
                    c11.BorderWidth = 0.5f;
                    c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c11.Padding = 4;
                    datatable2.AddCell(c11);

                    PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["EntryDesc"], fnt));
                    c12.BorderWidth = 0.5f;
                    c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c12.Padding = 4;
                    datatable2.AddCell(c12);

                    PdfPCell c14 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["RejectReason"], fnt));
                    c14.BorderWidth = 0.5f;
                    c14.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c14.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c14.Padding = 4;
                    datatable2.AddCell(c14);
                }

                //-------------TOTAL IN FOOTER --------------------
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(TraceNumber)", "").ToString(), fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));

                allBranchesTotalItem += dt.Rows.Count;
                allBranchesTotalAmount = allBranchesTotalAmount + ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString());

                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_RIGHT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable2.AddCell(c13);
                //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));
                //datatable2.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                //-------------END TOTAL -------------------------
                /////////////////////////////////
                document.Add(datatable);
                document.Add(datatable2);
                document.NewPage();
            }


            string strTotalTransactionInfo = "Total Item : " + allBranchesTotalItem.ToString() + "  -- Total Amount : " + allBranchesTotalAmount.ToString();
            iTextSharp.text.pdf.PdfPTable datatable5 = new iTextSharp.text.pdf.PdfPTable(1);
            datatable5.DefaultCell.Padding = 3;
            datatable5.DefaultCell.Border = 2;
            datatable5.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            //float[] headerwidths2 = { 15,35,20,15,15};
            datatable5.DefaultCell.BorderWidth = 0;
            datatable5.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            datatable5.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

            Font fntTotalTran = new Font(Font.HELVETICA, 16);
            fntTotalTran.SetStyle(Font.BOLD);

            datatable5.AddCell(new iTextSharp.text.Phrase(strTotalTransactionInfo, fntTotalTran));

            document.Add(datatable5);

            document.Close();
            Response.End();
        }

        private DataTable GetDataDepartmentWise(int DepartmentID)
        {
            FCUBSReportDB detailSettlementReportDB = new FCUBSReportDB();
            //DepartmentID = 0;
            //if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            //{
            //    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            //}
            if (ddListReportType.SelectedValue.Equals("25"))
            {

                return detailSettlementReportDB.GetReportDepartmentwiseTransactionSent(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("125"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseTransactionSentCredit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("225"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseTransactionSentDebit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("26"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseInwardReturn(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("126"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseInwardReturnCredit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            if (ddListReportType.SelectedValue.Equals("226"))
            {
                return detailSettlementReportDB.GetReportDepartmentwiseInwardReturnDebit(
                                                                        ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                      + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                      + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);

            }
            return new DataTable();
        }

        //public DataTable GetDepartment()
        //{
        //    DetailSettlementReportDB detailSettlementReportDB = new DetailSettlementReportDB();
        //    if (ddListReportType.SelectedValue.Equals("25"))
        //    {
        //        return detailSettlementReportDB.GetDepartmentForTransactionSentBySettlementDate(
        //                                                               ddlistYear.SelectedValue.PadLeft(4, '0')
        //                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
        //                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'));


        //    }
        //    else if (ddListReportType.SelectedValue.Equals("125"))
        //    {

        //        return detailSettlementReportDB.GetDepartmentForTransactionSentBySettlementDateForCredit(
        //                                                               ddlistYear.SelectedValue.PadLeft(4, '0')
        //                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
        //                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'));


        //    }
        //    else if (ddListReportType.SelectedValue.Equals("225"))
        //    {

        //        return detailSettlementReportDB.GetDepartmentForTransactionSentBySettlementDateForDebit(
        //                                                               ddlistYear.SelectedValue.PadLeft(4, '0')
        //                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
        //                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'));


        //    }
        //    else if (ddListReportType.SelectedValue.Equals("26"))
        //    {
        //        return detailSettlementReportDB.GetDepartmentForInwardReturnBySettlementDate(
        //                                                               ddlistYear.SelectedValue.PadLeft(4, '0')
        //                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
        //                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'));


        //    }
        //    else if (ddListReportType.SelectedValue.Equals("126"))
        //    {
        //        return detailSettlementReportDB.GetDepartmentForInwardReturnBySettlementDateForCredit(
        //                                                               ddlistYear.SelectedValue.PadLeft(4, '0')
        //                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
        //                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'));


        //    }
        //    else if (ddListReportType.SelectedValue.Equals("226"))
        //    {
        //        return detailSettlementReportDB.GetDepartmentForInwardReturnBySettlementDateForDebit(
        //                                                               ddlistYear.SelectedValue.PadLeft(4, '0')
        //                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
        //                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'));


        //    }
        //    return new DataTable();
        //}

        private void PrintPDFDepartmentwise(string FileName)
        {
            DataTable dtDepartment = GetActiveDepartment();

            if (dtDepartment.Rows.Count == 0)
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
            double allDepartmentsTotalAmount = 0;
            int allDepartmentsTotalItem = 0;

            for (int brRow = 0; brRow < dtDepartment.Rows.Count; brRow++)
            {
                string LogoImage = Server.MapPath("images") + "\\SCBLogo.JPG";
                DataTable dt = GetDataDepartmentWise(ParseData.StringToInt(dtDepartment.Rows[brRow]["DepartmentID"].ToString()));

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


                document.Add(new iTextSharp.text.Paragraph(" "));
                iTextSharp.text.pdf.PdfPTable datatable2 = new iTextSharp.text.pdf.PdfPTable(13);
                datatable2.DefaultCell.Padding = 3;
                datatable2.DefaultCell.Border = 2;
                datatable2.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //float[] headerwidths2 = { 15,35,20,15,15};
                float[] headerwidths2 = { 10, 10, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,10 };
                datatable2.DefaultCell.BorderWidth = 2;
                datatable2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                datatable2.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

                string nameOfDept = "Department-"+dtDepartment.Rows[brRow]["DepartmentName"].ToString();

                if (ddListReportType.SelectedValue.Equals("25"))
                {
                    headertable.AddCell(new Phrase(nameOfDept + "-Transaction Sent Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("125"))
                {
                    headertable.AddCell(new Phrase(nameOfDept + "-Transaction Sent Credit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("225"))
                {
                    headertable.AddCell(new Phrase(nameOfDept + "-Transaction Sent Debit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("26"))
                {
                    headertable.AddCell(new Phrase(nameOfDept + "-Inward Return Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("126"))
                {
                    headertable.AddCell(new Phrase(nameOfDept + "-Inward Return  Credit Report: " + settlementDate, headerFont));
                }
                else if (ddListReportType.SelectedValue.Equals("226"))
                {
                    headertable.AddCell(new Phrase(nameOfDept + "-Inward Return Debit Report: " + settlementDate, headerFont));
                }

                headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
                headertable.AddCell(logo); ;
                document.Add(headertable);

                document.Add(new iTextSharp.text.Paragraph(" "));
                iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(13);
                datatable.DefaultCell.Padding = 4;
                datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                float[] headerwidths = { 10, 10, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 10 };
                datatable.SetWidths(headerwidths);
                datatable.WidthPercentage = 99;

                iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                datatable.DefaultCell.BorderWidth = 0.5f;
                datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);



                datatable2.AddCell(new iTextSharp.text.Phrase("Bank Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Department Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("CBSReference", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("FloraReference", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("TraceNo", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("DFIAccNo", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("BankRoutNo", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Receiver /Payer Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Company Name", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("Ent.Desc", fnt));
                datatable2.AddCell(new iTextSharp.text.Phrase("RejectReason", fnt));



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankName"], fnt));
                    c1.BorderWidth = 0.5f;
                    c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c1.Padding = 1;
                    datatable2.AddCell(c1);

                    PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["DepartmentName"], fnt));
                    c2.BorderWidth = 0.5f;
                    c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c2.Padding = 4;
                    datatable2.AddCell(c2);

                    PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["CBSReference"], fnt));
                    c3.BorderWidth = 0.5f;
                    c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c3.Padding = 4;
                    datatable2.AddCell(c3);

                    PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["FloraReference"], fnt));
                    c5.BorderWidth = 0.5f;
                    c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c5.Padding = 4;
                    datatable2.AddCell(c5);

                    PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TraceNumber"], fnt));
                    c4.BorderWidth = 0.5f;
                    c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c4.Padding = 4;
                    datatable2.AddCell(c4);

                    //datatable2.AddCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));

                    PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["DFIAccountNo"], fnt));
                    c6.BorderWidth = 0.5f;
                    c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c6.Padding = 4;
                    datatable2.AddCell(c6);

                    PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["BankRoutingNo"], fnt));
                    c7.BorderWidth = 0.5f;
                    c7.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c7.Padding = 4;
                    datatable2.AddCell(c7);


                    PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dt.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                    c8.BorderWidth = 0.5f;
                    c8.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c8.Padding = 4;
                    datatable2.AddCell(c8);

                    PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["IdNumber"], fnt));
                    c9.BorderWidth = 0.5f;
                    c9.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c9.Padding = 4;
                    datatable2.AddCell(c9);

                    PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["ReceiverName"], fnt));
                    c10.BorderWidth = 0.5f;
                    c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c10.Padding = 4;
                    datatable2.AddCell(c10);

                    PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["CompanyName"], fnt));
                    c11.BorderWidth = 0.5f;
                    c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c11.Padding = 4;
                    datatable2.AddCell(c11);

                    PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["EntryDesc"], fnt));
                    c12.BorderWidth = 0.5f;
                    c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c12.Padding = 4;
                    datatable2.AddCell(c12);

                    PdfPCell c14 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["RejectReason"], fnt));
                    c14.BorderWidth = 0.5f;
                    c14.HorizontalAlignment = Cell.ALIGN_LEFT;
                    c14.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    c14.Padding = 4;
                    datatable2.AddCell(c14);
                }

                //-------------TOTAL IN FOOTER --------------------
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(TraceNumber)", "").ToString(), fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));

                allDepartmentsTotalItem += dt.Rows.Count;
                allDepartmentsTotalAmount = allDepartmentsTotalAmount + ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString());

                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_RIGHT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable2.AddCell(c13);
                //datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));
                //datatable2.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                datatable2.AddCell(new iTextSharp.text.Phrase("", fntbld));
                //-------------END TOTAL -------------------------
                /////////////////////////////////
                document.Add(datatable);
                document.Add(datatable2);
                if (brRow < (dtDepartment.Rows.Count - 1))
                {
                    document.NewPage();
                }
            }

            string strTotalTransactionInfo = "Total Item : " + allDepartmentsTotalItem.ToString() + "  -- Total Amount : " + allDepartmentsTotalAmount.ToString();
            iTextSharp.text.pdf.PdfPTable datatable5 = new iTextSharp.text.pdf.PdfPTable(1);
            datatable5.DefaultCell.Padding = 3;
            datatable5.DefaultCell.Border = 2;
            datatable5.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            //float[] headerwidths2 = { 15,35,20,15,15};
            datatable5.DefaultCell.BorderWidth = 0;
            datatable5.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            datatable5.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

            Font fntTotalTran = new Font(Font.HELVETICA, 16);
            fntTotalTran.SetStyle(Font.BOLD);

            datatable5.AddCell(new iTextSharp.text.Phrase(strTotalTransactionInfo, fntTotalTran));

            document.Add(datatable5);

            document.Close();
            Response.End();
        }
    }
}
