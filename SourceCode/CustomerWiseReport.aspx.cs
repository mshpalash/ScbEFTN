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
    public partial class CustomerWiseReport : System.Web.UI.Page
    {
        private static DataTable dtCustomerWiseReport = new DataTable();
        //int ReportOption;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistFromDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistFromMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistFromYear.SelectedValue = System.DateTime.Now.Year.ToString();

                ddlistToDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistToMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistToYear.SelectedValue = System.DateTime.Now.Year.ToString();

                //ddListBank.Items.Add("");
                BindBank();
                ddListBank.Items.Insert(0, "");

                BindCurrencyTypeDropdownlist();


                rdoReportOption.SelectedValue = "1";
                //ddListBank.Enabled = false;
                lblbank.Visible = false;
                ddListBank.Visible = false;
                ddlistFromDay.Visible = true;
                ddlistToDay.Visible = true;
                lblFromDate.Visible = true;
                lblToDate.Visible = true;
            }
        }

        protected void BindCurrencyTypeDropdownlist()
        {
            string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            SentBatchDB sentBatchDB = new SentBatchDB();
            DataTable dropDownData = new DataTable();

            //dropDownData.Columns.Add("Currency");
            //DataRow row = dropDownData.NewRow();
            //row["Currency"] = "ALL";
            //dropDownData.Rows.Add(row);

            //dropDownData = sentBatchDB.GetCurrencyList(eftConString);
            dropDownData.Merge(sentBatchDB.GetCurrencyList(eftConString));
            CurrencyDdList.DataSource = dropDownData;
            CurrencyDdList.DataTextField = "Currency";
            CurrencyDdList.DataValueField = "Currency";
            CurrencyDdList.DataBind();
            CurrencyDdList.SelectedIndex = 0;
        }

        private void BindBank()
        {
            FloraSoft.BanksDB db = new FloraSoft.BanksDB();

            ddListBank.DataSource = db.GetAllBanks();
            ddListBank.DataTextField = "BankName";
            ddListBank.DataValueField = "BankCode";
            ddListBank.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ReportDB reportDB = new ReportDB();
            //GetCustomerWiseReportForBranches
            //if(ConfigurationManager.AppSettings["BranchWise"])
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            //string BranchWise = ConfigurationManager.AppSettings["BranchWise"];

            //DataTable dtCustomerWiseReport;
            string FromDate = ddlistFromYear.SelectedValue + ddlistFromMonth.SelectedValue.PadLeft(2, '0') + ddlistFromDay.SelectedValue.PadLeft(2, '0');
            string ToDate = ddlistToYear.SelectedValue + ddlistToMonth.SelectedValue.PadLeft(2, '0') + ddlistToDay.SelectedValue.PadLeft(2, '0');

            string AccountNo = txtAccountNo.Text;
            int ReportType = ParseData.StringToInt(ddlistReportOption.SelectedValue);
            

            

            //if (rdoReportOption.SelectedValue.Equals("1"))
            //{
            // ReportOption = 2;
            //}
            //else if (rdoReportOption.SelectedValue.Equals("2"))
            //{
            //    ReportOption = 2;
            //}
            //else if (rdoReportOption.SelectedValue.Equals("3"))
            //{
            //    ReportOption = 3;
            //}

  

            if (rdoReportOption.SelectedValue.Equals("1"))
            {
               
            dtCustomerWiseReport = reportDB.GetCustomerWiseReport(FromDate, ToDate, AccountNo, ReportType,UserID, CurrencyDdList.SelectedValue);
            dtgCustomerWiseReport.Columns[4].Visible = false;
            dtgCustomerWiseReport.Columns[5].Visible = false;
            }
            else if (rdoReportOption.SelectedValue.Equals("2"))
            {
                
              string BankCode = ddListBank.SelectedValue;
             dtCustomerWiseReport = reportDB.GetCustomerWiseReport_BankWise (FromDate, ToDate, AccountNo,BankCode, ReportType,UserID, CurrencyDdList.SelectedValue);
             dtgCustomerWiseReport.Columns[4].Visible = true;
             dtgCustomerWiseReport.Columns[5].Visible = true;
            }
            //if (BranchWise.Equals("1"))
            //{
            //    dtCustomerWiseReport = reportDB.GetSettlementReportForBranches(ParseData.StringToInt(ddlistDay.SelectedValue),
            //                                           ParseData.StringToInt(ddlistMonth.SelectedValue),
            //                                           ParseData.StringToInt(ddlistYear.SelectedValue), UserID);
            //}
            //else
            //{
            //    dtCustomerWiseReport = reportDB.GetSettlementReport(ParseData.StringToInt(ddlistDay.SelectedValue),
            //                                           ParseData.StringToInt(ddlistMonth.SelectedValue),
            //                                           ParseData.StringToInt(ddlistYear.SelectedValue));
            //}

           

            BindCustomerWiseReport(dtCustomerWiseReport);
            //if (dtCustomerWiseReport.Rows.Count > 0)
            //{
            //    BindCustomerWiseReport(dtCustomerWiseReport);
            //}
        }

        private void BindCustomerWiseReport(DataTable dtCustomerWiseReport)
        {
            if (dtCustomerWiseReport.Rows.Count > 0)
            {
                dtgCustomerWiseReport.CurrentPageIndex = 0;
            }
            dtgCustomerWiseReport.DataSource = dtCustomerWiseReport;
            dtgCustomerWiseReport.Columns[8].FooterText = "Total:";
            dtgCustomerWiseReport.Columns[9].FooterText = dtCustomerWiseReport.Compute("SUM(NOTranPayment)", "").ToString();
            dtgCustomerWiseReport.Columns[10].FooterText = dtCustomerWiseReport.Compute("SUM(TotalPayment)", "").ToString();
            dtgCustomerWiseReport.Columns[11].FooterText = dtCustomerWiseReport.Compute("SUM(NOTranCollection)", "").ToString();
            dtgCustomerWiseReport.Columns[12].FooterText = dtCustomerWiseReport.Compute("SUM(TotalCollection)", "").ToString();
            //dtgCustomerWiseReport.Columns[6].FooterText = dtCustomerWiseReport.Compute("SUM(RCI)", "").ToString();
            //dtgCustomerWiseReport.Columns[7].FooterText = dtCustomerWiseReport.Compute("SUM(RCA)", "").ToString();
            //dtgCustomerWiseReport.Columns[8].FooterText = dtCustomerWiseReport.Compute("SUM(RDI)", "").ToString();
            //dtgCustomerWiseReport.Columns[9].FooterText = dtCustomerWiseReport.Compute("SUM(RDA)", "").ToString();
            //dtgCustomerWiseReport.Columns[10].FooterText = dtCustomerWiseReport.Compute("SUM(Net)", "").ToString();
            dtgCustomerWiseReport.DataBind();
        }

        protected void ExpotToCSVBtn_Click(object sender, EventArgs e)
        {
            ReportDB reportDB = new ReportDB();
            //GetCustomerWiseReportForBranches
            //if(ConfigurationManager.AppSettings["BranchWise"])
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            string BranchWise = ConfigurationManager.AppSettings["BranchWise"];

            //DataTable dtCustomerWiseReport;
            string FromDate = ddlistFromYear.SelectedValue + ddlistFromMonth.SelectedValue.PadLeft(2, '0') + ddlistFromDay.SelectedValue.PadLeft(2, '0');
            string ToDate = ddlistToYear.SelectedValue + ddlistToMonth.SelectedValue.PadLeft(2, '0') + ddlistToDay.SelectedValue.PadLeft(2, '0');

            string FromMonth=ddlistFromMonth.SelectedValue.PadLeft(2, '0');
            string ToMonth=ddlistToMonth.SelectedValue.PadLeft(2, '0');
            string FromYear = ddlistFromYear.SelectedValue;
            string ToYear = ddlistToYear.SelectedValue;

            string AccountNo = txtAccountNo.Text;
            int ReportType = ParseData.StringToInt(ddlistReportOption.SelectedValue);
            string BankCode = "";

           //int ReportOption = 2;

           //DataTable dt;

           if (rdoReportOption.SelectedValue.Equals("1"))
           {
               DataTable dt = reportDB.GetCustomerWiseReport(FromDate, ToDate, AccountNo, ReportType, UserID, CurrencyDdList.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    string xlsFileName = "CustomerWiseReport" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    string attachment = "attachment; filename=" + xlsFileName + ".csv";

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.csv";

                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);

                    // Create the CSV file to which grid data will be exported. 
                    //StreamWriter sw = new StreamWriter();
                    int iColCount = dt.Columns.Count;

                    // First we will write the headers. 

                    for (int i = 0; i < iColCount; i++)
                    {
                        sw.Write("\"");
                        sw.Write(dt.Columns[i]);
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
                    foreach (DataRow dr in dt.Rows)
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
           else if (rdoReportOption.SelectedValue.Equals("2"))
           {
               BankCode = ddListBank.SelectedValue;
               DataTable dt_bankwise = reportDB.GetCustomerWiseReport_BankWise(FromDate, ToDate, AccountNo,BankCode, ReportType, UserID, CurrencyDdList.SelectedValue);
               if (dt_bankwise.Rows.Count > 0)
               {
                   string xlsFileName = "CustomerWiseReportBankWise" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                   string attachment = "attachment; filename=" + xlsFileName + ".csv";

                   Response.ClearContent();
                   Response.AddHeader("content-disposition", attachment);
                   Response.ContentType = "application/vnd.csv";

                   StringWriter sw = new StringWriter();
                   HtmlTextWriter htw = new HtmlTextWriter(sw);

                   // Create the CSV file to which grid data will be exported. 
                   //StreamWriter sw = new StreamWriter();
                   int iColCount = dt_bankwise.Columns.Count;

                   // First we will write the headers. 

                   for (int i = 0; i < iColCount; i++)
                   {
                       sw.Write("\"");
                       sw.Write(dt_bankwise.Columns[i]);
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
                   foreach (DataRow dr in dt_bankwise.Rows)
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

           else if (rdoReportOption.SelectedValue.Equals("3"))
           {
               DataTable dt_MonthWise = reportDB.GetCustomerWiseReport_MonthWise(FromMonth, ToMonth, FromYear, ToYear, AccountNo, BankCode, ReportType, UserID, CurrencyDdList.SelectedValue);
               if (dt_MonthWise.Rows.Count > 0)
               {
                   string xlsFileName = "CustomerWiseReportMonthWise" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                   string attachment = "attachment; filename=" + xlsFileName + ".csv";

                   Response.ClearContent();
                   Response.AddHeader("content-disposition", attachment);
                   Response.ContentType = "application/vnd.csv";

                   StringWriter sw = new StringWriter();
                   HtmlTextWriter htw = new HtmlTextWriter(sw);

                   // Create the CSV file to which grid data will be exported. 
                   //StreamWriter sw = new StreamWriter();
                   int iColCount = dt_MonthWise.Columns.Count;

                   //if (dt_MonthWise.Rows.Count == 31151)
                   //{
                   //    string stop;
                   //}

                   // First we will write the headers. 

                   for (int i = 0; i < iColCount; i++)
                   {
                       sw.Write("\"");
                       sw.Write(dt_MonthWise.Columns[i]);
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
                   foreach (DataRow dr in dt_MonthWise.Rows)
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

        //private DataTable GetData()
        //{
        //    ReportDB reportDB = new ReportDB();
        //    return reportDB.GetSettlementReport(ParseData.StringToInt(ddlistDay.SelectedValue),
        //                                           ParseData.StringToInt(ddlistMonth.SelectedValue),
        //                                           ParseData.StringToInt(ddlistYear.SelectedValue));
        //}

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            if (rdoReportOption.SelectedValue.Equals("1"))
            {
                //string FileName = "CustomerWiseReport-" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                if (rdoReportOption.SelectedValue.Equals("1"))
                {
                    string FileName = "CustomerWiseReport-" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF_ClientWise(FileName);
                }
            }
            else if (rdoReportOption.SelectedValue.Equals("2"))
            {
                string FileName = "CustomerWiseReportBankwise-" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF_ClienBanktWise(FileName);
                
            }

            
        }

        protected void dtgCustomerWiseReport_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgCustomerWiseReport.CurrentPageIndex = e.NewPageIndex;
            dtgCustomerWiseReport.DataSource = dtCustomerWiseReport;
            dtgCustomerWiseReport.DataBind();
        }

        //protected void rdoReportOption_CheckedChanged(object sender, EventArgs e)  
        //{
        //    if (rdoReportOption.SelectedValue.Equals("1"))
        //    {
        //        ddListBank.Enabled = false;
        //    }
        //    if (rdoReportOption.SelectedValue.Equals("2"))
        //    {
        //        ddListBank.Enabled = true;
        //    }

        //}

        protected void rdoReportOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoReportOption.SelectedValue.Equals("1"))
            {
                lblbank.Visible = false;
                ddListBank.Visible = false;
                ddlistFromDay.Visible = true;
                ddlistToDay.Visible = true;
                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                btnSubmit.Enabled  = true;
                ExpotToPDFBtn.Enabled = true;

                dtgCustomerWiseReport.DataSource = null;
                dtgCustomerWiseReport.DataBind();
            }
            if (rdoReportOption.SelectedValue.Equals("2"))
            {
                lblbank.Visible = true;
                ddListBank.Visible = true;
                ddlistFromDay.Visible = true;
                ddlistToDay.Visible = true;
                lblFromDate.Visible = true;
                lblToDate.Visible = true;

                BindBank();
                ddListBank.Items.Insert(0, "");
                btnSubmit.Enabled = true;
                ExpotToPDFBtn.Enabled = true;

                dtgCustomerWiseReport.DataSource = null;
                dtgCustomerWiseReport.DataBind();
            }
            if (rdoReportOption.SelectedValue.Equals("3"))
            {
                lblbank.Visible = true;
                ddListBank.Visible = true;
                ddlistFromDay.Visible  = false;
                ddlistToDay.Visible = false;
                lblFromDate.Visible = false;
                lblToDate.Visible = false;
                btnSubmit.Enabled = false;
                ExpotToPDFBtn.Enabled = false;

                dtgCustomerWiseReport.DataSource = null;
                dtgCustomerWiseReport.DataBind();
            }

        }

        private void PrintPDF_ClientWise(string FileName)
        {
            //DataTable dtCustomerWiseReport = GetData();

            ReportDB reportDB = new ReportDB();
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
          
            //DataTable dtCustomerWiseReport;
            string FromDate = ddlistFromYear.SelectedValue + ddlistFromMonth.SelectedValue.PadLeft(2, '0') + ddlistFromDay.SelectedValue.PadLeft(2, '0');
            string ToDate = ddlistToYear.SelectedValue + ddlistToMonth.SelectedValue.PadLeft(2, '0') + ddlistToDay.SelectedValue.PadLeft(2, '0');

            string AccountNo = txtAccountNo.Text;
            int ReportType = ParseData.StringToInt(ddlistReportOption.SelectedValue);

            dtCustomerWiseReport = reportDB.GetCustomerWiseReport(FromDate, ToDate, AccountNo, ReportType, UserID, CurrencyDdList.SelectedValue);
            //if (rdoReportOption.SelectedValue.Equals("1"))
            //{

            //    dtCustomerWiseReport = reportDB.GetCustomerWiseReport(FromDate, ToDate, AccountNo, ReportType, UserID);
                
            //}
            //else if (rdoReportOption.SelectedValue.Equals("2"))
            //{

            //    string BankCode = ddListBank.SelectedValue;
            //    dtCustomerWiseReport = reportDB.GetCustomerWiseReport_BankWise(FromDate, ToDate, AccountNo, BankCode, ReportType, UserID);
             
            //}

            if (dtCustomerWiseReport.Rows.Count == 0)
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


            string rptFromDate = ddlistFromDay.SelectedValue + "/" + ddlistFromMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistFromYear.SelectedValue;
            headertable2.AddCell(new Phrase("From Date: " + rptFromDate, headerFont));

            headertable2.AddCell(new Phrase("Created : " + System.DateTime.Now.ToString("F"), headerFont));
            headertable2.AddCell(new Phrase(""));
            document.Add(headertable2);


            iTextSharp.text.pdf.PdfPTable headertable3 = new iTextSharp.text.pdf.PdfPTable(3);
            headertable3.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable3.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            headertable3.DefaultCell.Padding = 0;
            headertable3.WidthPercentage = 99;
            headertable3.DefaultCell.Border = 0;
            float[] widthsAtHeader3 = { 40, 40, 20 };
            headertable3.SetWidths(widthsAtHeader2);


            string rptToDate = ddlistToDay.SelectedValue + "/" + ddlistToMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistToYear.SelectedValue;
            headertable3.AddCell(new Phrase("TO Date: " + rptToDate, headerFont));

            headertable3.AddCell(new Phrase(""));
            headertable3.AddCell(new Phrase(""));
            document.Add(headertable3);



            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(10);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 10, 20, 10, 7, 7, 10, 8, 10, 8, 10 };
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Account No", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c0);
            //c0.Padding = 4;

            PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase("Client Name", fnt));
            c1.BorderWidth = 0.5f;
            c1.HorizontalAlignment = Cell.ALIGN_LEFT;
            c1.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c1);

            PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase("Segment", fnt));
            c2.BorderWidth = 0.5f;
            c2.HorizontalAlignment = Cell.ALIGN_LEFT ;
            c2.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c2);

            PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase("Segment Code", fnt));
            c3.BorderWidth = 0.5f;
            c3.HorizontalAlignment = Cell.ALIGN_CENTER  ;
            c3.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c3);

            PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase("Master", fnt));
            c4.BorderWidth = 0.5f;
            c4.HorizontalAlignment = Cell.ALIGN_CENTER;
            c4.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c4);  

            PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase("BDM Name", fnt));
            c5.BorderWidth = 0.5f;
            c5.HorizontalAlignment = Cell.ALIGN_LEFT ;
            c5.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c5);


            //datatable.AddCell(new iTextSharp.text.Phrase("Client Name", fnt));
            //datatable.AddCell(c0);

            //datatable.AddCell(new iTextSharp.text.Phrase("Account No", fnt));
            //datatable.AddCell(new iTextSharp.text.Phrase("Client Name", fnt));
            //datatable.AddCell(new iTextSharp.text.Phrase("Segment", fnt));
            //datatable.AddCell(new iTextSharp.text.Phrase("Segment Code", fnt));
            //datatable.AddCell(new iTextSharp.text.Phrase("Master", fnt));
            //datatable.AddCell(new iTextSharp.text.Phrase("BDM Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("No Of Txn Pay", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total Pay", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("No Of Txn Colc", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total Colc", fnt));
   


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dtCustomerWiseReport.Rows.Count; i++)
            {
                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["Accountno"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 1;
                datatable.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["ClientName"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["CustomerSegment"], fnt));
                c8.BorderWidth = 0.5f;
                c8.HorizontalAlignment = Cell.ALIGN_LEFT;
                c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c8.Padding = 4;
                datatable.AddCell(c8);

                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["SEGMENT"], fnt));
                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_CENTER;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 4;
                datatable.AddCell(c9);

                PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["Master"], fnt));
                c10.BorderWidth = 0.5f;
                c10.HorizontalAlignment = Cell.ALIGN_CENTER ;
                c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c10.Padding = 4;
                datatable.AddCell(c10);

                PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["BDMName"], fnt));
                c11.BorderWidth = 0.5f;
                c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c11.Padding = 4;
                datatable.AddCell(c11);

                //datatable.AddCell(new iTextSharp.text.Phrase((dtCustomerWiseReport.Rows[i]["SEGMENT"]).ToString(), fnt));
                //datatable.AddCell(new iTextSharp.text.Phrase((dtCustomerWiseReport.Rows[i]["Master"]).ToString(), fnt));
                //datatable.AddCell(new iTextSharp.text.Phrase((dtCustomerWiseReport.Rows[i]["BDMName"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dtCustomerWiseReport.Rows[i]["NOTranPayment"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtCustomerWiseReport.Rows[i]["TotalPayment"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dtCustomerWiseReport.Rows[i]["NOTranCollection"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtCustomerWiseReport.Rows[i]["TotalCollection"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
             }

            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total", fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase(dtCustomerWiseReport.Compute("SUM(NOTranPayment)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtCustomerWiseReport.Compute("SUM(TotalPayment)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase(dtCustomerWiseReport.Compute("SUM(NOTranCollection)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtCustomerWiseReport.Compute("SUM(TotalCollection)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            
           
            
            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }

        private void PrintPDF_ClienBanktWise(string FileName)
        {
            //DataTable dtCustomerWiseReport = GetData();

            ReportDB reportDB = new ReportDB();
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            //DataTable dtCustomerWiseReport;
            string FromDate = ddlistFromYear.SelectedValue + ddlistFromMonth.SelectedValue.PadLeft(2, '0') + ddlistFromDay.SelectedValue.PadLeft(2, '0');
            string ToDate = ddlistToYear.SelectedValue + ddlistToMonth.SelectedValue.PadLeft(2, '0') + ddlistToDay.SelectedValue.PadLeft(2, '0');

            string AccountNo = txtAccountNo.Text;
             int ReportType = ParseData.StringToInt(ddlistReportOption.SelectedValue);

            //if (rdoReportOption.SelectedValue.Equals("1"))
            //{

            //    dtCustomerWiseReport = reportDB.GetCustomerWiseReport_BankWise(FromDate, ToDate, AccountNo,BankCode, ReportType, UserID);

            //}
            //else if (rdoReportOption.SelectedValue.Equals("2"))
            //{

            //    string BankCode = ddListBank.SelectedValue;
            //    dtCustomerWiseReport = reportDB.GetCustomerWiseReport_BankWise(FromDate, ToDate, AccountNo, BankCode, ReportType, UserID);

            //}

            string BankCode = ddListBank.SelectedValue;
            dtCustomerWiseReport = reportDB.GetCustomerWiseReport_BankWise(FromDate, ToDate, AccountNo, BankCode, ReportType, UserID, CurrencyDdList.SelectedValue);

            if (dtCustomerWiseReport.Rows.Count == 0)
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


            string rptFromDate = ddlistFromDay.SelectedValue + "/" + ddlistFromMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistFromYear.SelectedValue;
            headertable2.AddCell(new Phrase("From Date: " + rptFromDate, headerFont));

            headertable2.AddCell(new Phrase("Created : " + System.DateTime.Now.ToString("F"), headerFont));
            headertable2.AddCell(new Phrase(""));
            document.Add(headertable2);


            iTextSharp.text.pdf.PdfPTable headertable3 = new iTextSharp.text.pdf.PdfPTable(3);
            headertable3.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable3.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            headertable3.DefaultCell.Padding = 0;
            headertable3.WidthPercentage = 99;
            headertable3.DefaultCell.Border = 0;
            float[] widthsAtHeader3 = { 40, 40, 20 };
            headertable3.SetWidths(widthsAtHeader2);


            string rptToDate = ddlistToDay.SelectedValue + "/" + ddlistToMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistToYear.SelectedValue;
            headertable3.AddCell(new Phrase("To Date: " + rptToDate, headerFont));

            headertable3.AddCell(new Phrase(""));
            headertable3.AddCell(new Phrase(""));
            document.Add(headertable3);



            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(12);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 7, 15,5,12,7, 5, 6, 7, 8, 10, 8, 10 };
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Account No", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c0);
            //c0.Padding = 4;

            PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase("Client Name", fnt));
            c1.BorderWidth = 0.5f;
            c1.HorizontalAlignment = Cell.ALIGN_LEFT;
            c1.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c1);

            PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase("Bank Code", fnt));
            c12.BorderWidth = 0.5f;
            c12.HorizontalAlignment = Cell.ALIGN_CENTER;
            c12.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c12);
            //c0.Padding = 4;

            PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            c13.BorderWidth = 0.5f;
            c13.HorizontalAlignment = Cell.ALIGN_LEFT;
            c13.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c13);

            PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase("Segment", fnt));
            c2.BorderWidth = 0.5f;
            c2.HorizontalAlignment = Cell.ALIGN_LEFT;
            c2.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c2);

            PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase("Segment Code", fnt));
            c3.BorderWidth = 0.5f;
            c3.HorizontalAlignment = Cell.ALIGN_CENTER;
            c3.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c3);

            PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase("Master", fnt));
            c4.BorderWidth = 0.5f;
            c4.HorizontalAlignment = Cell.ALIGN_CENTER;
            c4.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c4);

            PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase("BDM Name", fnt));
            c5.BorderWidth = 0.5f;
            c5.HorizontalAlignment = Cell.ALIGN_LEFT;
            c5.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.AddCell(c5);


            //datatable.AddCell(new iTextSharp.text.Phrase("Client Name", fnt));
            //datatable.AddCell(c0);

            //datatable.AddCell(new iTextSharp.text.Phrase("Account No", fnt));
            //datatable.AddCell(new iTextSharp.text.Phrase("Client Name", fnt));
            //datatable.AddCell(new iTextSharp.text.Phrase("Segment", fnt));
            //datatable.AddCell(new iTextSharp.text.Phrase("Segment Code", fnt));
            //datatable.AddCell(new iTextSharp.text.Phrase("Master", fnt));
            //datatable.AddCell(new iTextSharp.text.Phrase("BDM Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("No Of Txn Pay", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total Pay", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("No Of Txn Colc", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total Colc", fnt));



            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dtCustomerWiseReport.Rows.Count; i++)
            {
                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["Accountno"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 1;
                datatable.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["ClientName"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                PdfPCell c14 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["BankCode"], fnt));
                c14.BorderWidth = 0.5f;
                c14.HorizontalAlignment = Cell.ALIGN_CENTER;
                c14.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c14.Padding = 1;
                datatable.AddCell(c14);

                PdfPCell c15 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["BankName"], fnt));
                c15.BorderWidth = 0.5f;
                c15.HorizontalAlignment = Cell.ALIGN_LEFT;
                c15.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c15.Padding = 4;
                datatable.AddCell(c15);

                PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["CustomerSegment"], fnt));
                c8.BorderWidth = 0.5f;
                c8.HorizontalAlignment = Cell.ALIGN_LEFT;
                c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c8.Padding = 4;
                datatable.AddCell(c8);

                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["SEGMENT"], fnt));
                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_CENTER;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 4;
                datatable.AddCell(c9);

                PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["Master"], fnt));
                c10.BorderWidth = 0.5f;
                c10.HorizontalAlignment = Cell.ALIGN_CENTER;
                c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c10.Padding = 4;
                datatable.AddCell(c10);

                PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dtCustomerWiseReport.Rows[i]["BDMName"], fnt));
                c11.BorderWidth = 0.5f;
                c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c11.Padding = 4;
                datatable.AddCell(c11);

                //datatable.AddCell(new iTextSharp.text.Phrase((dtCustomerWiseReport.Rows[i]["SEGMENT"]).ToString(), fnt));
                //datatable.AddCell(new iTextSharp.text.Phrase((dtCustomerWiseReport.Rows[i]["Master"]).ToString(), fnt));
                //datatable.AddCell(new iTextSharp.text.Phrase((dtCustomerWiseReport.Rows[i]["BDMName"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dtCustomerWiseReport.Rows[i]["NOTranPayment"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtCustomerWiseReport.Rows[i]["TotalPayment"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dtCustomerWiseReport.Rows[i]["NOTranCollection"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtCustomerWiseReport.Rows[i]["TotalCollection"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            }

            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total", fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase(dtCustomerWiseReport.Compute("SUM(NOTranPayment)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtCustomerWiseReport.Compute("SUM(TotalPayment)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase(dtCustomerWiseReport.Compute("SUM(NOTranCollection)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtCustomerWiseReport.Compute("SUM(TotalCollection)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));



            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }
    }
}
