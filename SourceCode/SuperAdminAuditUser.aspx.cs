using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using EFTN.component;
using EFTN.Utility;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FloraSoft
{
    public partial class SuperAdminAuditUser : System.Web.UI.Page
    {
        private static DataTable dtUserHistory = new DataTable();
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Response.Redirect("AccessDenied.aspx");
            //BindData();
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();

                ddlistDayEnd.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonthEnd.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYearEnd.SelectedValue = System.DateTime.Now.Year.ToString();

                sortOrder = "asc";

            }
        }
        private void BindData()
        {
            string beginDate = ddlistYear.SelectedValue.PadLeft(4, '0')
                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                + ddlistDay.SelectedValue.PadLeft(2, '0');
            string endDate = ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                + ddlistDayEnd.SelectedValue.PadLeft(2, '0');

            UserHistoryDB db = new UserHistoryDB();
            dtUserHistory = db.GetUserHistoryByDate(beginDate, endDate);
            dv = dtUserHistory.DefaultView;

            MyDataGrid.DataSource = dv;
            MyDataGrid.DataBind();
        }

        //private void SortData(string sortType, string sortDirection)
        //{
        //    if (dtUserHistory != null)
        //    {
        //        dtUserHistory.DefaultView.Sort = sortType + " " + sortDirection;
        //        dtUserHistory = dtUserHistory.DefaultView.ToTable();
        //    }
        //}
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void MyDataGrid_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (dtUserHistory != null)
            {
                dv = dtUserHistory.DefaultView;
                dv.Sort = e.SortExpression + " " + sortOrder;
                dtUserHistory = dv.ToTable();
                MyDataGrid.DataSource = dv;
                MyDataGrid.DataBind();
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

        protected void MyDataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            MyDataGrid.CurrentPageIndex = e.NewPageIndex;
            MyDataGrid.DataSource = dtUserHistory;
            MyDataGrid.DataBind();

        }

        protected void btnExpotToExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = dtUserHistory;

            if (dt.Rows.Count > 0)
            {
                string xlsFileName = "UserAuditTrailReport" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
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

     

        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {
            if (dtUserHistory.Rows.Count > 0)
            {
                string FileName = "AuditTrail-Report" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDF(FileName);

            }
        }

        private void PrintPDF(string FileName)
        {
            DataTable dt = dtUserHistory;

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
            float[] widthsAtHeader = { 40, 40, 20 };
            headertable.SetWidths(widthsAtHeader);


            //   string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
            headertable.AddCell(new Phrase("Audit Trail Report: ", headerFont));



            headertable.AddCell(new Phrase("Generated Time: " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), headerFont));
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            //string SelectedBank = string.Empty;

            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            float[] headerwidths;
            int NumberOfPdfColumn = 0;


            headerwidths = new float[] { 15, 15, 15, 15, 15, 17, 8 };
            NumberOfPdfColumn = 7;


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
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            //PdfPCell c0;

            //    c0 = new PdfPCell(new iTextSharp.text.Phrase("User Name", fnt));

            ////PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            //c0.BorderWidth = 0.5f;
            //c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            //c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            //c0.Padding = 4;

            //datatable.AddCell(c0);

            datatable.AddCell(new iTextSharp.text.Phrase("User Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("LoginID", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IPAddress", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Action Time", fnt));
            //datatable.AddCell(new iTextSharp.text.Phrase("PasswordDate", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Action Name ", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("UserStatus", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("User Role", fnt));




            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dt.Rows.Count; i++)
            {


                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["UserName"].ToString(), fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["LoginID"].ToString(), fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 4;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["IPAddress"].ToString(), fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["HistoryTime"].ToString(), fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);


                //PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["PasswordDate"].ToString(), fnt));
                //c6.BorderWidth = 0.5f;
                //c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                //c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //c6.Padding = 4;
                //datatable.AddCell(c6);



                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["ActionName"].ToString(), fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);


                PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["UserStatus"].ToString(), fnt));
                c8.BorderWidth = 0.5f;
                c8.HorizontalAlignment = Cell.ALIGN_LEFT;
                c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c8.Padding = 4;
                datatable.AddCell(c8);


                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["RoleName"].ToString(), fnt));
                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_LEFT;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 4;
                datatable.AddCell(c9);
            }




            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }

    }
}