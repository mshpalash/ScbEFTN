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

namespace EFTN
{
    public partial class BatchSentVoucher : System.Web.UI.Page
    {
        private static DataTable myDTBatchSent = new DataTable();
        private static DataTable myDTBatchSentSTS = new DataTable();
        private static DataTable myDTBatchSentSTDO = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataForBatchSent();
                BindDataForBatchSentSTS();
                BindDataForBatchSentSTDO();
            }
        }

        private void BindDataForBatchSent()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.SentBatchDB batchDB = new EFTN.component.SentBatchDB();
            myDTBatchSent = batchDB.GetBatchesForTransactionSentForEBBSChecker(DepartmentID);
            dtgBatchTransactionSent.DataSource = myDTBatchSent;
            dtgBatchTransactionSent.DataBind();
        }

        private void BindDataForBatchSentSTS()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.SentBatchDB batchDB = new EFTN.component.SentBatchDB();
            myDTBatchSentSTS = batchDB.GetBatchesForTransactionSentForEBBSCheckerSTS(DepartmentID);
            dtgBatchTransactionSentSTS.DataSource = myDTBatchSentSTS;
            dtgBatchTransactionSentSTS.DataBind();
        }

        private void BindDataForBatchSentSTDO()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.SentBatchDB batchDB = new EFTN.component.SentBatchDB();
            myDTBatchSentSTDO = batchDB.GetBatchesForTransactionSentForEBBSCheckerSTDO(DepartmentID);
            dtgBatchTransactionSentSTDO.DataSource = myDTBatchSentSTDO;
            dtgBatchTransactionSentSTDO.DataBind();
        }
        protected void dtgBatchTransactionSent_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgBatchTransactionSent.CurrentPageIndex = e.NewPageIndex;
            dtgBatchTransactionSent.DataSource = myDTBatchSent;
            dtgBatchTransactionSent.DataBind();
        }

        protected void cbxAllTransactionSent_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllTransactionSent.Checked;
            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            UpdateEDRSentStatusForEBBSChecker(ApprovedBy);
            UpdateEDRSentStatusForEBBSCheckerSTS(ApprovedBy);
            UpdateEDRSentStatusForEBBSCheckerSTDO(ApprovedBy);
        }

        private void UpdateEDRSentStatusForEBBSChecker(int ApprovedBy)
        {
            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSent.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                    db.UpdateEDRSentStatusForBatchApprovalForEBBSCheckerAcceptance(TransactionID, ApprovedBy);
                }
            }
            BindDataForBatchSent();
            txtRejectedReason.Text = "";
            lblNoReturnReason.Visible = false;
        }

        private void UpdateEDRSentStatusForEBBSCheckerSTS(int ApprovedBy)
        {
            for (int i = 0; i < dtgBatchTransactionSentSTS.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentSTS.Items[i].FindControl("cbxSentBatchTSSTS");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSentSTS.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                    db.UpdateEDRSentStatusForBatchApprovalForEBBSCheckerAcceptance(TransactionID, ApprovedBy);
                }
            }
            BindDataForBatchSentSTS();
            txtRejectedReason.Text = "";
            lblNoReturnReason.Visible = false;
        }

        private void UpdateEDRSentStatusForEBBSCheckerSTDO(int ApprovedBy)
        {
            for (int i = 0; i < dtgBatchTransactionSentSTDO.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentSTDO.Items[i].FindControl("cbxSentBatchTSSTDO");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSentSTDO.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                    db.UpdateEDRSentStatusForBatchApprovalForEBBSCheckerAcceptance(TransactionID, ApprovedBy);
                }
            }
            BindDataForBatchSentSTDO();
            txtRejectedReason.Text = "";
            lblNoReturnReason.Visible = false;
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (txtRejectedReason.Text != "")
            {
                int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
                RejectBatches(ApprovedBy);

            }
            else
            {
                lblNoReturnReason.Visible = true;
            }
        }

        private void RejectBatches(int ApprovedBy)
        {
            #region TS   
            CheckBox cbxTS = new CheckBox();
            foreach (DataGridItem item in dtgBatchTransactionSent.Items)
            {                
                cbxTS = (CheckBox)item.FindControl("cbxSentBatchTS");
                if (cbxTS.Checked)
                {
                    EnterRejectReason();
                    //for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
                    //{
                    // string transactionID = dtgBatchTransactionSent.DataKeys[i].ToString();
                    string transactionID = dtgBatchTransactionSent.DataKeys[item.ItemIndex].ToString();
                    Guid TransactionID = new Guid(transactionID);
                    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                    db.UpdateEDRSentStatusForBatchApprovalForEBBSCheckerRejection(TransactionID, ApprovedBy);
                    //}                   
                }
            }
            //txtRejectedReason.Text = "";
            //lblNoReturnReason.Visible = false;
            BindDataForBatchSent();
            #endregion

            #region STS 
            CheckBox cbxSTS = new CheckBox();
            foreach (DataGridItem item in dtgBatchTransactionSentSTS.Items)
            {                
                cbxSTS = (CheckBox)item.FindControl("cbxSentBatchTSSTS");
                if (cbxSTS.Checked)
                {
                    EnterRejectReasonSTS();
                    //for (int i = 0; i < dtgBatchTransactionSentSTS.Items.Count; i++)
                    //{
                    // string transactionID = dtgBatchTransactionSentSTS.DataKeys[i].ToString();
                    string transactionID = dtgBatchTransactionSentSTS.DataKeys[item.ItemIndex].ToString();
                    Guid TransactionID = new Guid(transactionID);
                    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                    db.UpdateEDRSentStatusForBatchApprovalForEBBSCheckerRejection(TransactionID, ApprovedBy);
                    //}                   
                }
            }
            txtRejectedReason.Text = "";
            lblNoReturnReason.Visible = false;
            BindDataForBatchSentSTS();
            #endregion

            #region STDO
            #endregion
        }

        private void RejectBatchesSTS(int ApprovedBy)
        {
            EnterRejectReasonSTS();
            for (int i = 0; i < dtgBatchTransactionSentSTS.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentSTS.Items[i].FindControl("cbxSentBatchTSSTS");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSentSTS.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                    db.UpdateEDRSentStatusForBatchApprovalForEBBSCheckerRejection(TransactionID, ApprovedBy);
                }
            }
            txtRejectedReason.Text = "";
            lblNoReturnReason.Visible = false;
            BindDataForBatchSentSTS();
        }

        private void RejectBatchesSTDO(int ApprovedBy)
        {
            EnterRejectReasonSTDO();
            for (int i = 0; i < dtgBatchTransactionSentSTDO.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentSTDO.Items[i].FindControl("cbxSentBatchTSSTDO");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSentSTDO.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                    db.UpdateEDRSentStatusForBatchApprovalForEBBSCheckerRejection(TransactionID, ApprovedBy);
                }
            }
            txtRejectedReason.Text = "";
            lblNoReturnReason.Visible = false;
            BindDataForBatchSentSTDO();
        }
        private void EnterRejectReason()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSent.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(TransactionID, rejectReason,
                            (int)EFTN.Utility.ItemType.TransactionSent);
                }
            }
        }

        private void EnterRejectReasonSTS()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgBatchTransactionSentSTS.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentSTS.Items[i].FindControl("cbxSentBatchTSSTS");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSentSTS.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(TransactionID, rejectReason,
                            (int)EFTN.Utility.ItemType.TransactionSent);
                }
            }
        }

        private void EnterRejectReasonSTDO()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgBatchTransactionSentSTDO.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentSTDO.Items[i].FindControl("cbxSentBatchTSSTDO");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSentSTDO.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(TransactionID, rejectReason,
                            (int)EFTN.Utility.ItemType.TransactionSent);
                }
            }
        }
        private void ChangeStatusOfCheckedEDR1234(int statusID)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSent.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                    db.UpdateEDRSentStatusForBatchApproval(statusID, TransactionID, ApprovedBy);
                }
            }
            BindDataForBatchSent();
        }

        protected void PrintVoucherBtn_Click(object sender, EventArgs e)
        {
            string FileName = "Outward Batch Voucher-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".pdf";
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
            datatable.AddCell(new iTextSharp.text.Phrase("Batch Sent Voucher", fntlrg));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Daily Voucher\n\n" + System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(logo);
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.DefaultCell.BorderWidth = 1;

            datatable.AddCell(new iTextSharp.text.Phrase("C.cy", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Company Tin.", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("A/C Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Trn Code", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Batch Number", fnt));
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

            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSent.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.SentEDRDB db = new EFTN.component.SentEDRDB();
                    DataTable dtTS = new DataTable();
                    dtTS = db.GetBatchesPrintVoucher_For_TransactionSent_forEBBSCheckerbyTransactionID(TransactionID);
                    //sqlDRTS = db.GetBatchesPrintVoucher_For_TransactionSent_forEBBSCheckerbyTransactionID(TransactionID);


                    for (int tsCount = 0; tsCount < dtTS.Rows.Count; tsCount++)
                    {
                        decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(dtTS.Rows[tsCount]["Amount"].ToString());
                        totalAmount += transAmount;

                        string batchType = (string)dtTS.Rows[tsCount]["BatchType"];
                        if (batchType.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeCredit))
                        {
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
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["IdNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["EntryDate"], fnt));
                        }
                        else
                        {
                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["IdNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["EntryDate"], fnt));

                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["IdNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["AccountName"], fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["EntryDate"], fnt));

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

        protected void cbxAllTransactionSentSTS_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllTransactionSentSTS.Checked;
            for (int i = 0; i < dtgBatchTransactionSentSTS.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentSTS.Items[i].FindControl("cbxSentBatchTSSTS");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void cbxAllTransactionSentSTDO_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllTransactionSentSTDO.Checked;
            for (int i = 0; i < dtgBatchTransactionSentSTDO.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentSTDO.Items[i].FindControl("cbxSentBatchTSSTDO");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void dtgBatchTransactionSentSTS_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgBatchTransactionSentSTS.CurrentPageIndex = e.NewPageIndex;
            dtgBatchTransactionSentSTS.DataSource = myDTBatchSentSTS;
            dtgBatchTransactionSentSTS.DataBind();
        }

        protected void dtgBatchTransactionSentSTDO_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgBatchTransactionSentSTDO.CurrentPageIndex = e.NewPageIndex;
            dtgBatchTransactionSentSTDO.DataSource = myDTBatchSentSTDO;
            dtgBatchTransactionSentSTDO.DataBind();
        }

        protected void PrintVoucherBtnSTS_Click(object sender, EventArgs e)
        {
            string FileName = "Outward Batch Voucher STS-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".pdf";
            PrintPDFSTS(FileName);
        }

        protected void PrintPDFSTS(string FileName)
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
            datatable.AddCell(new iTextSharp.text.Phrase("Batch Sent Voucher", fntlrg));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Daily Voucher\n\n" + System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(logo);
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.DefaultCell.BorderWidth = 1;

            datatable.AddCell(new iTextSharp.text.Phrase("C.cy", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Company Tin.", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("A/C Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Trn Code", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Batch Number", fnt));
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

            for (int i = 0; i < dtgBatchTransactionSentSTS.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentSTS.Items[i].FindControl("cbxSentBatchTSSTS");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSentSTS.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.SentEDRDB db = new EFTN.component.SentEDRDB();
                    DataTable dtTS = new DataTable();
                    dtTS = db.GetBatchesPrintVoucher_For_TransactionSent_forEBBSCheckerbyTransactionID(TransactionID);
                    //sqlDRTS = db.GetBatchesPrintVoucher_For_TransactionSent_forEBBSCheckerbyTransactionID(TransactionID);


                    for (int tsCount = 0; tsCount < dtTS.Rows.Count; tsCount++)
                    {
                        decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(dtTS.Rows[tsCount]["Amount"].ToString());
                        totalAmount += transAmount;

                        string batchType = (string)dtTS.Rows[tsCount]["BatchType"];
                        if (batchType.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeCredit))
                        {
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
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["IdNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["EntryDate"], fnt));
                        }
                        else
                        {
                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["IdNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["EntryDate"], fnt));

                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["IdNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["AccountName"], fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["EntryDate"], fnt));

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

        protected void PrintVoucherBtnSTDO_Click(object sender, EventArgs e)
        {
            string FileName = "Outward Batch Voucher STDO-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".pdf";
            PrintPDFSTDO(FileName);
        }

        protected void PrintPDFSTDO(string FileName)
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
            datatable.AddCell(new iTextSharp.text.Phrase("Batch Sent Voucher", fntlrg));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Daily Voucher\n\n" + System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(logo);
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.DefaultCell.BorderWidth = 1;

            datatable.AddCell(new iTextSharp.text.Phrase("C.cy", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Company Tin.", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("A/C Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Trn Code", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Batch Number", fnt));
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

            for (int i = 0; i < dtgBatchTransactionSentSTDO.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentSTDO.Items[i].FindControl("cbxSentBatchTSSTDO");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSentSTDO.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.SentEDRDB db = new EFTN.component.SentEDRDB();
                    DataTable dtTS = new DataTable();
                    dtTS = db.GetBatchesPrintVoucher_For_TransactionSent_forEBBSCheckerbyTransactionID(TransactionID);
                    //sqlDRTS = db.GetBatchesPrintVoucher_For_TransactionSent_forEBBSCheckerbyTransactionID(TransactionID);


                    for (int tsCount = 0; tsCount < dtTS.Rows.Count; tsCount++)
                    {
                        decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(dtTS.Rows[tsCount]["Amount"].ToString());
                        totalAmount += transAmount;

                        string batchType = (string)dtTS.Rows[tsCount]["BatchType"];
                        if (batchType.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeCredit))
                        {
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
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["IdNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["EntryDate"], fnt));
                        }
                        else
                        {
                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["IdNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["EntryDate"], fnt));

                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["IdNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["AccountName"], fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtTS.Rows[tsCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtTS.Rows[tsCount]["EntryDate"], fnt));

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
    }
}
