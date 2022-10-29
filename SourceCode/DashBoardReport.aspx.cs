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
    public partial class DashBoardReport : System.Web.UI.Page
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
                    dtDashBoardReport = reportDB.GetBranchWiseInwardTransactionMonitorReport(
                                                       ParseData.StringToInt(ddlistDay.SelectedValue),
                                                       ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                       ParseData.StringToInt(ddlistYear.SelectedValue),
                                                        ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));


                }
                else if (ddlDashBoardReport.SelectedValue.Equals("2"))
                {
                    dtDashBoardReport = reportDB.GetBranchWiseReturnSentMonitorReport(
                                                    ParseData.StringToInt(ddlistDay.SelectedValue),
                                                    ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                    ParseData.StringToInt(ddlistYear.SelectedValue),
                                                    ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));


                }
                else if (ddlDashBoardReport.SelectedValue.Equals("3"))
                {
                    dtDashBoardReport = reportDB.GetBranchWiseNOCSentMonitorReport(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue),
                                                 ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));

                }
                else if (ddlDashBoardReport.SelectedValue.Equals("4"))
                {
                    dtDashBoardReport = reportDB.GetBranchWiseInwardDishonorMonitorReport(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue),
                                                 ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));

                }
                else if (ddlDashBoardReport.SelectedValue.Equals("5"))
                {
                    dtDashBoardReport = reportDB.GetBranchWiseContestedSentMonitorReport(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue),
                                                 ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));

                }
                else
                //if (ddlDashBoardReport.SelectedValue.Equals("6"))
                {
                    dtDashBoardReport = reportDB.GetBranchWiseInwardRNOCMonitorReport(
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
                    dtDashBoardReport = reportDB.GetMonitorReportDepartmentwiseForOutwardReport(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue));

                }
                else if (ddldashBoardOutward.SelectedValue.Equals("8"))
                {
                    dtDashBoardReport = reportDB.GetMonitorReportDepartmentwiseForInwardReturn(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue));

                }
                else if (ddldashBoardOutward.SelectedValue.Equals("9"))
                {
                    dtDashBoardReport = reportDB.GetMonitorReportDepartmentwiseForInwardNOC(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue));

                }
                else if (ddldashBoardOutward.SelectedValue.Equals("10"))
                {
                    dtDashBoardReport = reportDB.GetMonitorReportDepartmentwiseForOutwardDishonor(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue));

                }
                else if (ddldashBoardOutward.SelectedValue.Equals("11"))
                {
                    dtDashBoardReport = reportDB.GetMonitorReportDepartmentwiseForInwardContested(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue));

                }
                else
                //if (ddldashBoardOutward.SelectedValue.Equals("12"))
                {
                    dtDashBoardReport = reportDB.GetMonitorReportDepartmentwiseForOutwardRNOC(
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
            dtgDashBoardReport.Columns[5].FooterText = dtDashBoardReport.Compute("SUM(IMBDAItem)", "").ToString();
            dtgDashBoardReport.Columns[6].FooterText = dtDashBoardReport.Compute("SUM(IMBDAAmt)", "").ToString();
            dtgDashBoardReport.Columns[7].FooterText = dtDashBoardReport.Compute("SUM(ICItem)", "").ToString();
            dtgDashBoardReport.Columns[8].FooterText = dtDashBoardReport.Compute("SUM(ICAmt)", "").ToString();
            dtgDashBoardReport.Columns[9].FooterText = dtDashBoardReport.Compute("SUM(IEBBSItem)", "").ToString();
            dtgDashBoardReport.Columns[10].FooterText = dtDashBoardReport.Compute("SUM(IEBBSAmt)", "").ToString();
            dtgDashBoardReport.Columns[11].FooterText = dtDashBoardReport.Compute("SUM(IEBBSAItem)", "").ToString();
            dtgDashBoardReport.Columns[12].FooterText = dtDashBoardReport.Compute("SUM(IEBBSAAmt)", "").ToString();
            dtgDashBoardReport.Columns[13].FooterText = dtDashBoardReport.Compute("SUM(CAItem)", "").ToString();
            dtgDashBoardReport.Columns[14].FooterText = dtDashBoardReport.Compute("SUM(CAAAmt)", "").ToString();
            dtgDashBoardReport.Columns[15].FooterText = dtDashBoardReport.Compute("SUM(CAPItem)", "").ToString();
            dtgDashBoardReport.Columns[16].FooterText = dtDashBoardReport.Compute("SUM(CAPAAmt)", "").ToString();
            dtgDashBoardReport.DataBind();
        }

        private DataTable GetData()
        {
            ReportDB reportDB = new ReportDB();
            if (ddlDashBoard.SelectedValue.Equals("1"))
            {
                if (ddlDashBoardReport.SelectedValue.Equals("1"))
                {
                    return reportDB.GetBranchWiseInwardTransactionMonitorReport(
                                                   ParseData.StringToInt(ddlistDay.SelectedValue),
                                                   ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                   ParseData.StringToInt(ddlistYear.SelectedValue),
                                                   ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));

                }
                else if (ddlDashBoardReport.SelectedValue.Equals("2"))
                {
                    return reportDB.GetBranchWiseReturnSentMonitorReport(
                                                ParseData.StringToInt(ddlistDay.SelectedValue),
                                                ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                ParseData.StringToInt(ddlistYear.SelectedValue),
                                                ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));
                }
                else if (ddlDashBoardReport.SelectedValue.Equals("3"))
                {
                    return reportDB.GetBranchWiseNOCSentMonitorReport(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue),
                                                 ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));
                }
                else if (ddlDashBoardReport.SelectedValue.Equals("4"))
                {
                    return reportDB.GetBranchWiseInwardDishonorMonitorReport(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue),
                                                 ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));

                }
                else if (ddlDashBoardReport.SelectedValue.Equals("5"))
                {
                    return reportDB.GetBranchWiseContestedSentMonitorReport(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue),
                                                 ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));

                }
                else
                //if (ddlDashBoardReport.SelectedValue.Equals("6"))
                {
                    return reportDB.GetBranchWiseInwardRNOCMonitorReport(
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
                    return reportDB.GetMonitorReportDepartmentwiseForOutwardReport(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue));

                }
                else if (ddldashBoardOutward.SelectedValue.Equals("8"))
                {
                    return reportDB.GetMonitorReportDepartmentwiseForInwardReturn(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue));

                }
                else if (ddldashBoardOutward.SelectedValue.Equals("9"))
                {
                    return reportDB.GetMonitorReportDepartmentwiseForInwardNOC(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue));

                }
                else if (ddldashBoardOutward.SelectedValue.Equals("10"))
                {
                    return reportDB.GetMonitorReportDepartmentwiseForOutwardDishonor(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue));

                }
                else if (ddldashBoardOutward.SelectedValue.Equals("11"))
                {
                    return reportDB.GetMonitorReportDepartmentwiseForInwardContested(
                                                 ParseData.StringToInt(ddlistDay.SelectedValue),
                                                 ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                 ParseData.StringToInt(ddlistYear.SelectedValue));
                                              

                }
                //else if (ddldashBoardOutward.SelectedValue.Equals("12"))

                else
                {
                    return reportDB.GetMonitorReportDepartmentwiseForOutwardRNOC(
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
                string FileName = "DashBoardReport-InwardTransaction" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            else if (ddlDashBoardReport.SelectedValue.Equals("2"))
            {
                string FileName = "DashBoardReport-ReturnSent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            else if (ddlDashBoardReport.SelectedValue.Equals("3"))
            {
                string FileName = "DashBoardReport-NOCSent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            else if (ddlDashBoardReport.SelectedValue.Equals("4"))
            {
                string FileName = "DashBoardReport-InwardDishonor" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            else if (ddlDashBoardReport.SelectedValue.Equals("5"))
            {
                string FileName = "DashBoardReport-ContestedSent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
            else 
            {
                string FileName = "DashBoardReport-InwardRNOC" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

             }
            }
            else
            {
                if (ddlDashBoardReport.SelectedValue.Equals("7"))
                {
                    string FileName = "DashBoardReport-OutwardTransaction" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);

                }
                else if (ddlDashBoardReport.SelectedValue.Equals("8"))
                {
                    string FileName = "DashBoardReport-InwardReturn" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);

                }
                else if (ddlDashBoardReport.SelectedValue.Equals("9"))
                {
                    string FileName = "DashBoardReport-InwardNOC" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);

                }
                else if (ddlDashBoardReport.SelectedValue.Equals("10"))
                {
                    string FileName = "DashBoardReport-OutwardDishonor" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);

                }
                else if (ddlDashBoardReport.SelectedValue.Equals("11"))
                {
                    string FileName = "DashBoardReport-InwardContestedt" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    PrintPDF(FileName);

                }
                else
                {
                    string FileName = "DashBoardReport-OutwardRNOC" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
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
            datatable.AddCell(new iTextSharp.text.Phrase("IMBDAItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IMBDAAmt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ICItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("ICAmt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IEBBSItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IEBBSAmt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IEBBSAItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IEBBSAAmt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("CAItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("CAAAmt", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("CAPItem", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("CAPAAmt", fnt));


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

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["IMBDAItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["IMBDAAmt"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["ICItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["ICAmt"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["IEBBSItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["IEBBSAmt"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["IEBBSAItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["IEBBSAAmt"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["CAItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["CAAAmt"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                datatable.AddCell(new iTextSharp.text.Phrase((dt.Rows[i]["CAPItem"]).ToString(), fnt));
                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Rows[i]["CAPAAmt"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

            }
            //-------------TOTAL IN FOOTER --------------------
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(IMItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(IMAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(IMBDAItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(IMBDAAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(ICItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(ICAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(IEBBSItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(IEBBSAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(IEBBSAItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(IEBBSAAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(CAItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(CAAAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dt.Compute("SUM(CAPItem)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dt.Compute("SUM(CAPAAmt)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
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
