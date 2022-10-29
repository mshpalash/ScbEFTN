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
using System.IO;

namespace EFTN
{
    public partial class DishonorSentVoucher : System.Web.UI.Page
    {
        private static DataTable myDTDishonorSent = new DataTable();

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

        private void BindData()
        {
            EFTN.component.EBBSCheckerDishonorSentDB eBBSCheckerDishonorSentDB = new EFTN.component.EBBSCheckerDishonorSentDB();

            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                myDTDishonorSent = eBBSCheckerDishonorSentDB.GetSentDishonorForEBBSCheckerForDebit(
                                                                      ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                    + ddlistDay.SelectedValue.PadLeft(2, '0'),
                                                                      ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                                                    + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                                                    + ddlistDayEnd.SelectedValue.PadLeft(2, '0'),
                                                                    DepartmentID);
            }
            else
            {
                myDTDishonorSent = eBBSCheckerDishonorSentDB.GetSentDishonorForEBBSChecker(
                                                                      ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                    + ddlistDay.SelectedValue.PadLeft(2, '0'),
                                                                      ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                                                    + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                                                    + ddlistDayEnd.SelectedValue.PadLeft(2, '0'),
                                                                    DepartmentID);
            }
            dtgDishonorSentChecker.CurrentPageIndex = 0;
            dtgDishonorSentChecker.DataSource = myDTDishonorSent;

            dtgDishonorSentChecker.DataBind();
        }

        protected void PrintVoucherBtn_Click(object sender, EventArgs e)
        {
            string FileName = "Outward Dishonor Voucher-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".pdf";
            PrintPDF(FileName);

        }


        protected void dtgDishonorSentChecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgDishonorSentChecker.CurrentPageIndex = e.NewPageIndex;
            dtgDishonorSentChecker.DataSource = myDTDishonorSent;
            dtgDishonorSentChecker.DataBind();
        }

        protected void PrintPDF(string FileName)
        {

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

            document.Open();

            iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
            jpeg.Alignment = Element.ALIGN_RIGHT;

            //headertable.AddCell(logocell);

            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(9);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 10, 10, 20, 6, 19, 5, 10, 10, 10 };
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

            PdfPCell logo = new PdfPCell();
            logo.BorderWidth = 0;
            logo.Colspan = 2;
            logo.AddElement(jpeg);
            //------------------------------------------

            datatable.AddCell(new iTextSharp.text.Phrase("Branch Code", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("060", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase(ConfigurationManager.AppSettings["OriginBankName"], fntlrg));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatable.AddCell(new iTextSharp.text.Phrase("System Name\n\nUser Id\n\nUser Name\n\nCountry", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("CBS\n\n\n\n\n\nBangladesh", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("Dishonor Sent Voucher", fntlrg));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Daily Voucher\n\n" + System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(logo);
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.DefaultCell.BorderWidth = 1;

            datatable.AddCell(new iTextSharp.text.Phrase("C.cy", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Account No.", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("A/C Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Trn Code", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Narration", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Seq. No", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Debit", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Credit", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Value Date", fnt));

            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 1;

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{

            EFTN.component.EBBSCheckerDishonorSentDB eBBSCheckerDishonorSentDB = new EFTN.component.EBBSCheckerDishonorSentDB();
            decimal totalAmount = 0;
            //System.Data.SqlClient.SqlDataReader sqlDRTS;


            for (int i = 0; i < dtgDishonorSentChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgDishonorSentChecker.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    string dishonoredID = dtgDishonorSentChecker.DataKeys[i].ToString();
                    Guid DishonoredID = new Guid(dishonoredID);

                    DataTable dtTS = new DataTable();
                    dtTS = eBBSCheckerDishonorSentDB.GetSentDishonorSentForEBBSCheckerByDishonorSentID(DishonoredID).Copy();

                    for (int tsCount = 0; tsCount < dtTS.Rows.Count; tsCount++)
                    {
                        decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(dtTS.Rows[tsCount]["Amount"].ToString());
                        totalAmount += transAmount;
                        datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["IdNumber"]).ToString(), fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["AccountName"], fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["TraceNumber"]).ToString(), fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["EntryDate"], fnt));

                        datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"], fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["TraceNumber"]).ToString(), fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["EntryDate"], fnt));
                    }
                }
            }

            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total (BDT)", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Maker", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            PdfPCell ca1 = new PdfPCell();
            ca1.BorderWidth = 0;
            ca1.BorderWidthRight = 1;
            ca1.BorderColorRight = new iTextSharp.text.Color(200, 200, 200);
            ca1.Colspan = 3;
            ca1.AddElement(new iTextSharp.text.Phrase("Checked by / Authorised By", fnt));
            datatable.AddCell(ca1);

            datatable.AddCell(new iTextSharp.text.Phrase("Batch No", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Signature", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            PdfPCell ca2 = new PdfPCell();
            ca2.BorderWidth = 0;
            ca2.BorderWidthTop = 1;
            ca2.BorderColorTop = new iTextSharp.text.Color(200, 200, 200);
            ca2.BorderWidthRight = 1;
            ca2.BorderColorRight = new iTextSharp.text.Color(200, 200, 200);
            ca2.Colspan = 3;
            ca2.AddElement(new iTextSharp.text.Phrase("Signature", fnt));
            datatable.AddCell(ca2);

            datatable.AddCell(new iTextSharp.text.Phrase("Input Date", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Date", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            document.Add(datatable);

            //////////////////////////////////////////////

            document.Close();
            Response.End();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData();
            cbxSelectAll.Checked = false;
        }

        protected void cbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedCbx = cbxSelectAll.Checked;
            for (int i = 0; i < dtgDishonorSentChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgDishonorSentChecker.Items[i].FindControl("CheckBEFTNList");
                cbx.Checked = checkedCbx;
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgDishonorSentChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgDishonorSentChecker.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    string dishonoredID = dtgDishonorSentChecker.DataKeys[i].ToString();
                    Guid DishonoredID = new Guid(dishonoredID);

                    EFTN.component.EBBSCheckerDishonorSentDB db = new EFTN.component.EBBSCheckerDishonorSentDB();
                    db.UpdateDishonorSentStatusApprovedByEBBSChecker(DishonoredID);
                }
            }
            BindData();
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (txtRejectedReason.Text != "")
            {
                EnterRejectReason();
                UpdateDishonorSentStatusRejectedByEBBSChecker();

                BindData();
            }
            else
            {
                lblNoReturnReason.Visible = true;
            }
        }

        private void EnterRejectReason()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgDishonorSentChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgDishonorSentChecker.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    string dishonoredID = dtgDishonorSentChecker.DataKeys[i].ToString();
                    Guid DishonoredID = new Guid(dishonoredID);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(DishonoredID, rejectReason,
                            (int)EFTN.Utility.ItemType.DishonouredSent);
                }
            }
        }

        private void UpdateDishonorSentStatusRejectedByEBBSChecker()
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            for (int i = 0; i < dtgDishonorSentChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgDishonorSentChecker.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    string dishonoredID = dtgDishonorSentChecker.DataKeys[i].ToString();
                    Guid DishonoredID = new Guid(dishonoredID);

                    EFTN.component.EBBSCheckerDishonorSentDB db = new EFTN.component.EBBSCheckerDishonorSentDB();
                    db.UpdateDishonorSentStatusRejectedByEBBSChecker(DishonoredID);
                }
            }
        }

        protected void btnExpotToCSV_Click(object sender, EventArgs e)
        {
            //DataTable dt = GetData();

            if (myDTDishonorSent.Rows.Count > 0)
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
                int iColCount = myDTDishonorSent.Columns.Count;

                // First we will write the headers. 

                for (int i = 1; i < iColCount; i++)
                {
                    sw.Write("\"");
                    sw.Write(myDTDishonorSent.Columns[i]);
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
                foreach (DataRow dr in myDTDishonorSent.Rows)
                {
                    for (int i = 1; i < iColCount; i++)
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
