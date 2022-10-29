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
    public partial class SchedulerReportMakerOld : System.Web.UI.Page
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
            DataTable dt = GetData();
            if (dt.Rows.Count > 0)
            {
                BindStatusReport(dt);
            }
        }

        private DataTable GetData()
        {
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            CityRFCDB reportDB = new CityRFCDB();
            DataTable dt = reportDB.GetScheduleTransactionByEffectiveEntryDate(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            return dt;
        }

        private void BindStatusReport(DataTable dt)
        {
            dtgStatusReport.DataSource = dt;
            dtgStatusReport.DataBind();
        }

        //private DataTable GetData()
        //{
        //    ReportDB reportDB = new ReportDB();
        //    return reportDB.GetStatusReport(ParseData.StringToInt(ddlistDay.SelectedValue),
        //                                           ParseData.StringToInt(ddlistMonth.SelectedValue),
        //                                           ParseData.StringToInt(ddlistYear.SelectedValue));
        //}

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            string FileName = "SchedulerReport-" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
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

            string spacer = "            -              ";

            string str =spacer;
            str = str + ConfigurationManager.AppSettings["OriginBankName"] + " All Rights Reserved" + spacer;
            str = str + "Confidential: internal use only" + spacer;
            str = str + "Powered By Flora Limited";

            HeaderFooter footer = new HeaderFooter(new Phrase(str, fnt), false);
            footer.Alignment = Element.ALIGN_CENTER;

            document.Footer = footer;
            document.Open();


            ////////////////////////////////
            iTextSharp.text.Table headertable = new iTextSharp.text.Table(3);
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            headertable.Cellpadding = 0;
            headertable.Cellspacing = 0;
            headertable.Border = 0;
            headertable.Width = 100;
            document.Add(headertable);

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(7);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 5, 20, 15, 15, 15, 15, 15 };
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("TransactionCode", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;
            datatable.AddCell(c0);
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccountNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("BankRoutingNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ReceiverName", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("CompanyName", fnt));


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["TransactionCode"], fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 4;
                datatable.AddCell(c1);

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["DFIAccountNo"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["BankRoutingNo"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["Amount"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["IdNumber"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["ReceiverName"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["CompanyName"]).ToString(), fnt));
            }

            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }
    }
}