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
using System.IO;
using EFTN.Utility;
using Ionic.Zip;
using EFTN.component;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EFTN
{
    public partial class SchedulerReportMakerExecutionDay : System.Web.UI.Page
    {
        private static DataTable dtStandingOrderBatchList = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();

                ddlistDayEnd.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonthEnd.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYearEnd.SelectedValue = System.DateTime.Now.Year.ToString();
            }
        }

        //private void BindDataForTransactionSent()
        //{
        //    EFTN.component.StandingOrderDB STODB = new EFTN.component.StandingOrderDB();

        //    dtgStandingOrderBatch.DataSource = STODB.GetAllStandingOrderList();
        //    dtgStandingOrderBatch.DataBind();
        //}

        //protected void linkBtnActive_Click(object sender, EventArgs e)
        //{
        //    UpdateStandingOrderBatchStatus("ACTIVE");
        //}

        //protected void linkBtnInactive_Click(object sender, EventArgs e)
        //{

        //    UpdateStandingOrderBatchStatus("INACTIVE");
        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            dtStandingOrderBatchList = LoadStandingOrderBatch();
            dtgStandingOrderBatch.DataSource = dtStandingOrderBatchList;
            dtgStandingOrderBatch.DataBind();
            
        }

        private DataTable LoadStandingOrderBatch()
        {
            string BeginDate = ddlistYear.SelectedValue.PadLeft(4, '0')
                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                + ddlistDay.SelectedValue.PadLeft(2, '0');

            string EndDate = ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                + ddlistDayEnd.SelectedValue.PadLeft(2, '0');

            StandingOrderDB stdOrderdb = new StandingOrderDB();
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            return stdOrderdb.GetAllStandingOrderListbyExecutionDateRange(BeginDate, EndDate, UserID, ddListActiveStatus.SelectedValue);
        }

        protected void dtgStandingOrderBatch_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgStandingOrderBatch.CurrentPageIndex = e.NewPageIndex;
            dtgStandingOrderBatch.DataSource = dtStandingOrderBatchList;
            dtgStandingOrderBatch.DataBind();
        }

        protected void btnCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = LoadStandingOrderBatch();

            if (dt.Rows.Count > 0)
            {
                string xlsFileName = "SchedulerReportExecutionDay" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
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

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            string fileName = "SchedulerReportExecutionDay";
            printPDF(fileName);
        }

        public void printPDF(string fileName)
        {

            DataTable myDt = LoadStandingOrderBatch();

            if (myDt.Rows.Count == 0)
            {
                return;
            }

            string LogoImage = Server.MapPath("images") + "\\SCBLogo.JPG";
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);


            Document pdfdoc = new Document(PageSize.A4.Rotate(), 1, 1, 12, 12);
            //PdfWriter writer = PdfWriter.GetInstance(pdfdoc, new FileStream(fileName, FileMode.Create));
            PdfWriter.GetInstance(pdfdoc, Response.OutputStream);
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

            pdfdoc.Footer = footer;
            pdfdoc.Open();


            iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
            jpeg.Alignment = Element.ALIGN_RIGHT;


            PdfPCell logo = new PdfPCell();
            logo.BorderWidth = 0;
            // logo.Colspan = 2;
            logo.AddElement(jpeg);

            PdfPTable headertable = new PdfPTable(2);
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            headertable.DefaultCell.Padding = 0;
            headertable.WidthPercentage = 99;
            headertable.DefaultCell.Border = 0;
            float[] widthsAtHeader = { 80, 19 };
            headertable.SetWidths(widthsAtHeader);
            headertable.AddCell(new Phrase("Scheduler Report on Execution Day", headerFont));

            headertable.AddCell(logo);

            pdfdoc.Add(headertable);

            pdfdoc.Add(new iTextSharp.text.Paragraph(" "));

            PdfPTable headertable2 = new PdfPTable(2);
            headertable2.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable2.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            headertable2.DefaultCell.Padding = 0;
            headertable2.WidthPercentage = 99;
            headertable2.DefaultCell.Border = 0;
            float[] widthsAtHeader2 = { 50, 50 };
            headertable2.SetWidths(widthsAtHeader2);


            headertable2.AddCell(new Phrase("Date Range : " + ddlistYear.SelectedValue.PadLeft(4, '0') + "-"
                                + ddlistMonth.SelectedValue.PadLeft(2, '0') + "-"
                                + ddlistDay.SelectedValue.PadLeft(2, '0') + " - " + ddlistYearEnd.SelectedValue.PadLeft(4, '0') + "-"
                                + ddlistMonthEnd.SelectedValue.PadLeft(2, '0') + "-"
                                + ddlistDayEnd.SelectedValue.PadLeft(2, '0')));

            headertable2.AddCell(new Phrase("PDF Generation Time : " + System.DateTime.Now.ToString()));


            pdfdoc.Add(headertable2);

            pdfdoc.Add(new Paragraph(" "));
            pdfdoc.Add(new Paragraph(" "));
            pdfdoc.Add(new Paragraph(" "));

            PdfPTable table = new PdfPTable(16);
            table.TotalWidth = 1540f;
            float[] widths = new float[] { 40f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f };
            table.SetWidths(widths);

            table.AddCell(new iTextSharp.text.Phrase("SL.", fnt));
            table.AddCell(new iTextSharp.text.Phrase("Title", fnt));
            table.AddCell(new iTextSharp.text.Phrase("Account No", fnt));
            table.AddCell(new iTextSharp.text.Phrase("Transaction Frequency", fnt));
            table.AddCell(new iTextSharp.text.Phrase("Begin Date", fnt));
            table.AddCell(new iTextSharp.text.Phrase("End Date", fnt));
            table.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            table.AddCell(new iTextSharp.text.Phrase("Batch Type", fnt));
            table.AddCell(new iTextSharp.text.Phrase("DFI Account No", fnt));
            table.AddCell(new iTextSharp.text.Phrase("Receiver Account Type", fnt));
            table.AddCell(new iTextSharp.text.Phrase("Receiver Name", fnt));
            table.AddCell(new iTextSharp.text.Phrase("Receiving Bank Name", fnt));
            table.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
            table.AddCell(new iTextSharp.text.Phrase("Routing No", fnt));
            table.AddCell(new iTextSharp.text.Phrase("Active Status", fnt));
            table.AddCell(new iTextSharp.text.Phrase("Batch Entry Date", fnt));

            for (int rowNum = 0; rowNum < myDt.Rows.Count; rowNum++)
            {
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((rowNum + 1).ToString(), fnt));

                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                table.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)myDt.Rows[rowNum]["Title"], fnt));

                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 1;
                table.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)myDt.Rows[rowNum]["AccountNo"], fnt));

                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 1;
                table.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)myDt.Rows[rowNum]["TransactionFrequency"], fnt));

                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 1;
                table.AddCell(c4);


                PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((myDt.Rows[rowNum]["BeginDate"].ToString()).Substring(0, 9), fnt));

                c5.BorderWidth = 0.5f;
                c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c5.Padding = 1;
                table.AddCell(c5);

                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((myDt.Rows[rowNum]["EndDate"].ToString()).Substring(0, 9), fnt));

                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 1;
                table.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((myDt.Rows[rowNum]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 1;
                table.AddCell(c7);

                PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase((string)myDt.Rows[rowNum]["BatchType"], fnt));

                c8.BorderWidth = 0.5f;
                c8.HorizontalAlignment = Cell.ALIGN_LEFT;
                c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c8.Padding = 1;
                table.AddCell(c8);

                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)myDt.Rows[rowNum]["DFIAccountNo"], fnt));

                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_LEFT;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 1;
                table.AddCell(c9);

                PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase(myDt.Rows[rowNum]["ReceiverAccountType"].ToString(), fnt));

                c10.BorderWidth = 0.5f;
                c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c10.Padding = 1;
                table.AddCell(c10);

                PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)myDt.Rows[rowNum]["ReceiverName"], fnt));

                c11.BorderWidth = 0.5f;
                c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c11.Padding = 1;
                table.AddCell(c11);

                PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)myDt.Rows[rowNum]["BankName"], fnt));

                c12.BorderWidth = 0.5f;
                c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c12.Padding = 1;
                table.AddCell(c12);

                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase((string)myDt.Rows[rowNum]["BranchName"], fnt));

                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_LEFT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 1;
                table.AddCell(c13);

                PdfPCell c14 = new PdfPCell(new iTextSharp.text.Phrase((string)myDt.Rows[rowNum]["ReceivingBankRoutingNo"], fnt));

                c14.BorderWidth = 0.5f;
                c14.HorizontalAlignment = Cell.ALIGN_LEFT;
                c14.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c14.Padding = 1;
                table.AddCell(c14);

                PdfPCell c15 = new PdfPCell(new iTextSharp.text.Phrase((string)myDt.Rows[rowNum]["ActiveStatus"], fnt));

                c15.BorderWidth = 0.5f;
                c15.HorizontalAlignment = Cell.ALIGN_LEFT;
                c15.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c15.Padding = 1;
                table.AddCell(c15);

                PdfPCell c16 = new PdfPCell(new iTextSharp.text.Phrase(myDt.Rows[rowNum]["BatchEntryDate"].ToString(), fnt));

                c16.BorderWidth = 0.5f;
                c16.HorizontalAlignment = Cell.ALIGN_LEFT;
                c16.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c16.Padding = 1;
                table.AddCell(c16);

            }

            pdfdoc.Add(table);
            pdfdoc.Close();


        }
    }
}
