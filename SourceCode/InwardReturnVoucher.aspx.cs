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
using EFTN.component;

namespace EFTN
{
    public partial class InwardReturnVoucher : System.Web.UI.Page
    {
        private static DataTable DtInwardReturn = new DataTable();
        DataView dv;
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                sortOrder = "asc";
                BindSessionDropdownlist();
                BindCurrencyTypeDropdownlist();
            }
        }
        private void BindSessionDropdownlist()
        {
            SessionDdList.DataSource = sentBatchDB.GetSessions(eftConString);
            SessionDdList.DataTextField = "SessionID";
            SessionDdList.DataValueField = "SessionID";
            SessionDdList.DataBind();
        }
        protected void BindCurrencyTypeDropdownlist()
        {
            DataTable dropDownData = new DataTable();
            dropDownData = sentBatchDB.GetCurrencyList(eftConString);
            CurrencyDdList.DataSource = dropDownData;
            CurrencyDdList.DataTextField = "Currency";
            CurrencyDdList.DataValueField = "Currency";
            CurrencyDdList.DataBind();
            CurrencyDdList.SelectedIndex = 0;
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

        private void BindData()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.ReceivedReturnDB receivedReturnDB = new EFTN.component.ReceivedReturnDB();
            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                DtInwardReturn = receivedReturnDB.GetReceivedReturnApprovedForEBBSChecker_ForDebit(DepartmentID);
            }
            else
            {
                DtInwardReturn = receivedReturnDB.GetReceivedReturnApprovedForEBBSChecker(DepartmentID);
            }
            if (DtInwardReturn.Rows.Count > 0)
            {
                DtInwardReturn = GetFilteredData(DtInwardReturn);
            }
            else
            {
                DtInwardReturn = new DataTable();
            }
            dv = DtInwardReturn.DefaultView;
            dtgApprovedReturnChecker.DataSource = dv;
            dtgApprovedReturnChecker.DataBind();

            txtReceivedReturnApprovedRejectReason.Text = "";

            if (DtInwardReturn.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + DtInwardReturn.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(DtInwardReturn.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
            cbxAll.Checked = false;
        }

        private DataTable GetFilteredData(DataTable MyDt)
        {
            DataTable filteredData = new DataTable();
            int currencyCounter = GetCountForSelectedCurrency(DtInwardReturn, CurrencyDdList.SelectedValue);
            int sessionCounter = GetCountForSelectedSession(DtInwardReturn, SessionDdList.SelectedValue);
            if (currencyCounter > 0 && sessionCounter > 0)
            {
                var linqResult = DtInwardReturn.AsEnumerable()
              .Where(c => c.Field<string>("Currency") == CurrencyDdList.SelectedValue && c.Field<Byte>("SessionID") == Byte.Parse(SessionDdList.SelectedValue));
                if (linqResult.AsDataView().ToTable().Rows.Count > 0)
                {
                    filteredData = linqResult.CopyToDataTable();
                }
            }
            else
            {
                filteredData = new DataTable();
            }
            return filteredData;
        }
        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnInwardReturnApproved_Click(object sender, EventArgs e)
        {
            UpdateReceivedReturn(EFTN.Utility.TransactionStatus.Return_Received_Approval__Approved_by_checker);
            lblMsgApproved.Visible = false;
            txtReceivedReturnApprovedRejectReason.Text = "";
        }

        protected void btnInwardRetrunReject_Click(object sender, EventArgs e)
        {
            if (txtReceivedReturnApprovedRejectReason.Text != "")
            {
                UpdateReceivedReturn(EFTN.Utility.TransactionStatus.Rejected_By_Checker_Return_Received_Approved);
            }
            else
            {
                lblMsgApproved.Visible = true;
            }
        }

        private void UpdateReceivedReturn(int statusID)
        {
            int approvedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            EFTN.component.ReceivedReturnDB db = new EFTN.component.ReceivedReturnDB();
            for (int i = 0; i < dtgApprovedReturnChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgApprovedReturnChecker.Items[i].FindControl("CheckBEFTNList");
                if (cbx.Checked)
                {
                    Guid returnID = (Guid)dtgApprovedReturnChecker.DataKeys[i];
                    db.UpdateReceivedReturnStatusByEBBSChecker(statusID, returnID, approvedBy);
                    if (statusID == EFTN.Utility.TransactionStatus.Rejected_By_Checker_Return_Received_Approved)
                    {
                        string ReturnApproveRejection = txtReceivedReturnApprovedRejectReason.Text;
                        EFTN.component.RejectReasonByCheckerDB rejectReasonByCheckerDB = new EFTN.component.RejectReasonByCheckerDB();
                        rejectReasonByCheckerDB.Insert_RejectReason_ByChecker(returnID, ReturnApproveRejection, (int)EFTN.Utility.ItemType.ReturnReceived);
                    }
                }
            }
            BindData();
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgApprovedReturnChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgApprovedReturnChecker.Items[i].FindControl("CheckBEFTNList");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void PrintVoucherBtn_Click(object sender, EventArgs e)
        {
            string FileName = "InwardReturnVoucher-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".pdf";
            PrintPDF(FileName);
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
            datatable.AddCell(new iTextSharp.text.Phrase("Inward Return Voucher", fntlrg));
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

            decimal totalAmount = 0;

            for (int i = 0; i < dtgApprovedReturnChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgApprovedReturnChecker.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    string returnID = dtgApprovedReturnChecker.DataKeys[i].ToString();
                    Guid ReturnID = new Guid(returnID);

                    //System.Data.SqlClient.SqlDataReader sqlDRRetRecv;
                    EFTN.component.ReceivedReturnDB db = new EFTN.component.ReceivedReturnDB();

                    DataTable dtRR = new DataTable();
                    dtRR = db.GetReceivedReturnbyReturnIDForCBSChecker(ReturnID);

                    //sqlDRRetRecv = db.GetReceivedReturnbyReturnIDForCBSChecker(ReturnID);

                    for (int rRCount = 0; rRCount < dtRR.Rows.Count; rRCount++)
                    {
                        decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(dtRR.Rows[rRCount]["Amount"].ToString());
                        totalAmount += transAmount;

                        string transactionCode = dtRR.Rows[rRCount]["TransactionCode"].ToString();

                        if (transactionCode.Equals("27")
                            || transactionCode.Equals("37")
                            || transactionCode.Equals("55")
                           )
                        {
                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtRR.Rows[rRCount]["AccountNo"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtRR.Rows[rRCount]["SenderName"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtRR.Rows[rRCount]["TraceNumber"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtRR.Rows[rRCount]["EntryDateReturnReceived"].ToString(), fnt));

                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"], fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtRR.Rows[rRCount]["TraceNumber"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtRR.Rows[rRCount]["EntryDateReturnReceived"].ToString(), fnt));
                        }
                        else
                        {
                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"], fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtRR.Rows[rRCount]["TraceNumber"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtRR.Rows[rRCount]["EntryDateReturnReceived"].ToString(), fnt));

                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtRR.Rows[rRCount]["AccountNo"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtRR.Rows[rRCount]["SenderName"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtRR.Rows[rRCount]["TraceNumber"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtRR.Rows[rRCount]["EntryDateReturnReceived"].ToString(), fnt));
                        }
                    }
                    dtRR.Dispose();
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

        protected void dtgApprovedReturnChecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgApprovedReturnChecker.CurrentPageIndex = e.NewPageIndex;
            dtgApprovedReturnChecker.DataSource = DtInwardReturn;
            dtgApprovedReturnChecker.DataBind();
        }

        protected void dtgApprovedReturnChecker_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = DtInwardReturn.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgApprovedReturnChecker.DataSource = dv;
            dtgApprovedReturnChecker.DataBind();
        }
        protected void CurrencyDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        protected void SessionDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        private int GetCountForSelectedSession(DataTable bulkData, string session)
        {
            int txnCounter = 0;
            foreach (DataRow row in bulkData.Rows)
            {
                if (row["SessionID"].ToString().Equals(session))
                {
                    txnCounter++;
                }
            }
            return txnCounter;
        }

        private int GetCountForSelectedCurrency(DataTable bulkData, string currency)
        {
            int txnCounter = 0;
            foreach (DataRow row in bulkData.Rows)
            {
                if (row["Currency"].ToString().Equals(currency))
                {
                    txnCounter++;
                }
            }
            return txnCounter;
        }
    }
}
