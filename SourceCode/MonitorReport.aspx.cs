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
    public partial class MonitorReport : System.Web.UI.Page
    {
        private static DataTable dtDashBoardReport = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("225"))
                {
                    Response.Redirect("EFTChecker.aspx");
                }
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');
                ddldashBoardOutward.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ReportDB reportDB = new ReportDB();
            if (ddlDashBoard.SelectedValue.Equals("1"))
            {
                if (ddlDashBoardReport.SelectedValue.Equals("1"))
                {
                    dtDashBoardReport = reportDB.GetMonitorReportBranchwiseCreditForUCB(
                                                       ParseData.StringToInt(ddlistDay.SelectedValue),
                                                       ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                       ParseData.StringToInt(ddlistYear.SelectedValue),
                                                        ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));


                }
               
               
                else
                //if (ddlDashBoardReport.SelectedValue.Equals("6"))
                {
                    dtDashBoardReport = reportDB.GetMonitorReportBranchwiseDebitForUCB(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue),
                                                 ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));

                }
            }
            else
            {
                if (ddldashBoardOutward.SelectedValue.Equals("7"))
                {
                    dtDashBoardReport = reportDB.GetMonitorReportDepartmentwiseCreditForUCB(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue));

                }
               
                else
                //if (ddldashBoardOutward.SelectedValue.Equals("12"))
                {
                    dtDashBoardReport = reportDB.GetMonitorReportDepartmentwiseDebitForUCB(
                                                ParseData.StringToInt(ddlistDay.SelectedValue),
                                                ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                ParseData.StringToInt(ddlistYear.SelectedValue));
                }
            }
            BindBranchWiseDashBoardReport();

        }
        private void BindBranchWiseDashBoardReport()
        {
            dtgDashBoardReport.DataSource = dtDashBoardReport;
            dtgDashBoardReport.Columns[2].FooterText = "Total:";
            dtgDashBoardReport.Columns[3].FooterText = dtDashBoardReport.Compute("SUM(IMItem)", "").ToString();
            dtgDashBoardReport.Columns[4].FooterText = dtDashBoardReport.Compute("SUM(IMAmt)", "").ToString();
            dtgDashBoardReport.Columns[5].FooterText = dtDashBoardReport.Compute("SUM(IMAItem)", "").ToString();
            dtgDashBoardReport.Columns[6].FooterText = dtDashBoardReport.Compute("SUM(IMAAmt)", "").ToString();
            dtgDashBoardReport.Columns[7].FooterText = dtDashBoardReport.Compute("SUM(IRetItem)", "").ToString();
            dtgDashBoardReport.Columns[8].FooterText = dtDashBoardReport.Compute("SUM(IRetAmt)", "").ToString();

            dtgDashBoardReport.DataBind();
        }

        private DataTable GetData()
        {
            ReportDB reportDB = new ReportDB();
            if (ddlDashBoard.SelectedValue.Equals("1"))
            {
                if (ddlDashBoardReport.SelectedValue.Equals("1"))
                {
                    return reportDB.GetMonitorReportBranchwiseCreditForUCB(
                                                   ParseData.StringToInt(ddlistDay.SelectedValue),
                                                   ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                   ParseData.StringToInt(ddlistYear.SelectedValue),
                                                   ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));

                }
               
                else
                //if (ddlDashBoardReport.SelectedValue.Equals("6"))
                {
                    return reportDB.GetMonitorReportBranchwiseDebitForUCB(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue),
                                                 ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));

                }
            }
            else
            {
                if (ddldashBoardOutward.SelectedValue.Equals("7"))
                {
                    return reportDB.GetMonitorReportDepartmentwiseCreditForUCB(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue));

                }
               

                else
                {
                    return reportDB.GetMonitorReportDepartmentwiseDebitForUCB(
                                                ParseData.StringToInt(ddlistDay.SelectedValue),
                                                ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                ParseData.StringToInt(ddlistYear.SelectedValue));
                }

            }

        }

        protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        {
            if (ddlDashBoard.SelectedValue.Equals("1"))
            {
            if (ddlDashBoardReport.SelectedValue.Equals("1"))
            {
                string FileName = "MonitorReport-InwardCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
           
            else 
            {
                string FileName = "MonitorReport-InwardDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

             }
            }
            else
            {
                if (ddlDashBoardReport.SelectedValue.Equals("7"))
                {
                    string FileName = "MonitorReport-OutwardCredit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);

                }
               
                else
                {
                    string FileName = "MonitorReport-OutwardDebit" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);

                }
            }
           
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
            headertable.AddCell(new Phrase("Dash Board Report", headerFont));
            headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
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

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(17);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 7, 15,10, 7, 10, 7, 10, 7, 10, 7, 10, 10, 10, 8, 8, 8, 8 };
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Branch Name", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;
            datatable.AddCell(new iTextSharp.text.Phrase("Routing No", fnt));
            datatable.AddCell(c0);

            datatable.AddCell(new iTextSharp.text.Phrase("DepartmentName", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IMItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IMAmt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IMAItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IMAAmt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IRetItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IRetAmt", fnt));
     

            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["RoutingNo"], fnt));
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

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["DepartmentName"], fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);


                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["IMItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["IMAmt"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["IMAItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["IMAAmt"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["IRetAmt"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["IRetAmt"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

              
            }
            //-------------TOTAL IN FOOTER --------------------
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(IMItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(IMAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(IMAItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(IMAAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(IRetItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(IRetItem)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
                 //-------------END TOTAL -------------------------
            /////////////////////////////////
            document.Add(datatable);
            document.Close();
            Response.End();

        }

        protected void ddlDashBoard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDashBoard.SelectedValue.Equals("1"))
            {
                ddldashBoardOutward.Visible = false;
                ddlDashBoardReport.Visible = true;
            }
            else if (ddlDashBoard.SelectedValue.Equals("2"))
            {
                ddldashBoardOutward.Visible = true;
                ddlDashBoardReport.Visible = false;
            }
        }

      
    }
}
