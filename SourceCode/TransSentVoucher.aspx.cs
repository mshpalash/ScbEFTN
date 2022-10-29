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
    public partial class TransSentVoucher : System.Web.UI.Page
    {
        private static DataTable myDataTable = new DataTable();

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

                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("225"))
                {
                    finacleDiv.Visible = true;
                }
                else
                {
                    finacleDiv.Visible = false;
                }
            }
        }

        private void BindData()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }
            
            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();
            myDataTable = edrDB.GetSentEDR_ApprovedByChecker(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), 
                                                                  ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDayEnd.SelectedValue.PadLeft(2, '0'),                                                                
                                                                DepartmentID);
            dtgEFTChecker.DataSource = myDataTable;
            try
            {
                dtgEFTChecker.DataBind();

            }
            catch
            {
                dtgEFTChecker.CurrentPageIndex = 0;
                dtgEFTChecker.DataBind();
            }
            //.DataBind();
        }

        protected void dtgEFTChecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTChecker.CurrentPageIndex = e.NewPageIndex;
            dtgEFTChecker.DataSource = myDataTable;
            dtgEFTChecker.DataBind();
        }

        protected void PrintVoucherBtn_Click(object sender, EventArgs e)
        {
            string FileName = "Outward Transaction Voucher-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".pdf";
            PrintPDF(FileName);

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

            document.Open();

            iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
            jpeg.Alignment = Element.ALIGN_RIGHT;

            //headertable.AddCell(logocell);

            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(10);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 10, 10, 15, 4, 19, 15, 5, 7, 7, 8 };
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
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatable.AddCell(new iTextSharp.text.Phrase("System Name\n\nUser Id\n\nUser Name\n\nCountry", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("CBS\n\n\n\n\n\nBangladesh", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("Transaction Sent Voucher", fntlrg));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Daily Voucher\n\n" + System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
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
            datatable.AddCell(new iTextSharp.text.Phrase("Payment Info", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Seq. No", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Debit", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Credit", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Value Date", fnt));

            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 1;

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{

            decimal totalAmount = 0;
            //System.Data.SqlClient.SqlDataReader sqlDRTS;

            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxTransSent");

                if (cbx.Checked)
                {
                    string edrId = dtgEFTChecker.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    EFTN.component.SentEDRDB db = new EFTN.component.SentEDRDB();
                    DataTable dtTS = new DataTable();
                    dtTS = db.GetSentEDRApprovedByCheckerByEDRID(EDRID);


                    for (int trCount = 0; trCount < dtTS.Rows.Count; trCount++)
                    {
                        decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(dtTS.Rows[trCount]["Amount"].ToString());
                        totalAmount += transAmount;

                        string transactionCode = dtTS.Rows[trCount]["TransactionCode"].ToString();
                        if (transactionCode.Equals("27")
                            || transactionCode.Equals("37")
                            || transactionCode.Equals("55")
                           )
                        {

                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"]), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[trCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[trCount]["PaymentInfo"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[trCount]["EntryDate"], fnt));

                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(dtTS.Rows[trCount]["IdNumber"].ToString()), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[trCount]["AccountName"], fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[trCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[trCount]["PaymentInfo"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[trCount]["EntryDate"], fnt));
                        }
                        else
                        {
                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(dtTS.Rows[trCount]["IdNumber"].ToString()), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[trCount]["AccountName"], fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[trCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[trCount]["PaymentInfo"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[trCount]["EntryDate"], fnt));

                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"]), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[trCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[trCount]["PaymentInfo"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[trCount]["EntryDate"], fnt));
                        }
                    }
                    dtTS.Dispose();
                }
            }

            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Total (BDT)", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
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
        }

        //protected void cbxSelectAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool checkedCbx = cbxSelectAll.Checked;
        //    for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
        //    {
        //        CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxTransSent");
        //        cbx.Checked = checkedCbx;
        //    }
        //}

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            //string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxTransSent");

                if (cbx.Checked)
                {
                    string edrId = dtgEFTChecker.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    EFTN.component.EDRSentEBBSCheckerDB db = new EFTN.component.EDRSentEBBSCheckerDB();
                    db.GetEDRSentApprovedByEBBSChecker(EDRID, ApprovedBy);
                }
            }
            BindData();
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (txtRejectedReason.Text != "")
            {
                EnterRejectReason();
                ChangeStatusOfCheckedEDR(EFTN.Utility.TransactionStatus.TransSent_Rejected_By_Checker);

                BindData();
            }
            else
            {
                lblNoReturnReason.Visible = true;
            }
        }

        protected void btnAcceptAll_Click(object sender, EventArgs e)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                string edrId = dtgEFTChecker.DataKeys[i].ToString();
                Guid EDRID = new Guid(edrId);

                EFTN.component.EDRSentEBBSCheckerDB db = new EFTN.component.EDRSentEBBSCheckerDB();
                db.GetEDRSentApprovedByEBBSChecker(EDRID, ApprovedBy);
            }
            BindData();
        }

        protected void btnRejectAll_Click(object sender, EventArgs e)
        {
            if (txtRejectedReason.Text != "")
            {
                EnterRejectReasonForAll();
                ChangeStatusOfCheckedEDRForAll(EFTN.Utility.TransactionStatus.TransSent_Rejected_By_Checker);

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
            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxTransSent");

                if (cbx.Checked)
                {
                    string edrId = dtgEFTChecker.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(EDRID, rejectReason,
                            (int)EFTN.Utility.ItemType.TransactionSent);
                }
            }

        }

        private void EnterRejectReasonForAll()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                string edrId = dtgEFTChecker.DataKeys[i].ToString();
                Guid EDRID = new Guid(edrId);

                EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                db.Insert_RejectReason_ByChecker(EDRID, rejectReason,
                        (int)EFTN.Utility.ItemType.TransactionSent);
            }
        }
        
        private void ChangeStatusOfCheckedEDR(int statusID)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxTransSent");

                if (cbx.Checked)
                {
                    string edrId = dtgEFTChecker.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    EFTN.component.SentEDRDB db = new EFTN.component.SentEDRDB();
                    db.Update_EDRSent_Status(statusID, EDRID, ApprovedBy);
                }
            }
        }


        private void ChangeStatusOfCheckedEDRForAll(int statusID)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                string edrId = dtgEFTChecker.DataKeys[i].ToString();
                Guid EDRID = new Guid(edrId);

                EFTN.component.SentEDRDB db = new EFTN.component.SentEDRDB();
                db.Update_EDRSent_Status(statusID, EDRID, ApprovedBy);
            }
        }
    }
}
