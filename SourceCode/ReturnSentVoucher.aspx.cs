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
using System.IO;
using EFTN.component;

namespace EFTN
{
    public partial class ReturnSentVoucher : System.Web.UI.Page
    {
        private static DataTable myDTReturnSent = new DataTable();
        DataView dv;
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private static string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCurrencyTypeDropdownlist();
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();

                ddlistDayEnd.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonthEnd.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYearEnd.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');

                sortOrder = "asc";
            }
        }
        protected void BindCurrencyTypeDropdownlist()
        {
            DataTable dropDownData = new DataTable();
            dropDownData.Columns.Add("Currency");
            DataRow row = dropDownData.NewRow();
            row["Currency"] = "ALL";
            dropDownData.Rows.Add(row);
            dropDownData.Merge(sentBatchDB.GetCurrencyList(eftConString));
            CurrencyDdList.DataSource = dropDownData;
            CurrencyDdList.DataTextField = "Currency";
            CurrencyDdList.DataValueField = "Currency";
            CurrencyDdList.DataBind();
        }
        //private void BindSessionDropdownlist()
        //{
        //    DataTable dropDownData = new DataTable();
        //    dropDownData.Columns.Add("SessionID");
        //    DataRow row = dropDownData.NewRow();
        //    row["SessionID"] = "ALL";
        //    dropDownData.Rows.Add(row);
        //    dropDownData.Merge(sentBatchDB.GetSessions(eftConString));
        //    SessionDdList.DataSource = dropDownData;
        //    SessionDdList.DataTextField = "SessionID";
        //    SessionDdList.DataValueField = "SessionID";
        //    SessionDdList.DataBind();
        //}
        private void BindData()
        {
            EFTN.component.SentReturnDB sentReturnDB = new EFTN.component.SentReturnDB();

            int BranchID = 0;
            string originBankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);
            }

            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                if (originBankCode.Equals(OriginalBankCode.SCB))
                {
                    int DepartmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

                    myDTReturnSent = sentReturnDB.GetSentRRForEBBSChecker_ForDebit_ForSCB(
                                                                          ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                        + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                        + ddlistDay.SelectedValue.PadLeft(2, '0'),
                                                                          ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                                                        + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                                                        + ddlistDayEnd.SelectedValue.PadLeft(2, '0'),

                                                                        BranchID, DepartmentID
                                                                        ,CurrencyDdList.SelectedValue);
                }
                else
                {
                    myDTReturnSent = sentReturnDB.GetSentRRForEBBSChecker_ForDebit(
                                                                          ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                        + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                        + ddlistDay.SelectedValue.PadLeft(2, '0'),
                                                                          ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                                                        + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                                                        + ddlistDayEnd.SelectedValue.PadLeft(2, '0'),

                                                                        BranchID);
                }
            }
            else
            {
                if (originBankCode.Equals(OriginalBankCode.SCB))
                {
                    int DepartmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                    myDTReturnSent = sentReturnDB.GetSentRRForEBBSChecker_ForSCB(
                                                                          ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                        + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                        + ddlistDay.SelectedValue.PadLeft(2, '0'),
                                                                          ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                                                        + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                                                        + ddlistDayEnd.SelectedValue.PadLeft(2, '0'),

                                                                        BranchID, DepartmentID
                                                                        ,CurrencyDdList.SelectedValue);
                }
                else
                {
                    myDTReturnSent = sentReturnDB.GetSentRRForEBBSChecker(
                                                                          ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                        + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                        + ddlistDay.SelectedValue.PadLeft(2, '0'),
                                                                          ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                                                        + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                                                        + ddlistDayEnd.SelectedValue.PadLeft(2, '0'),

                                                                        BranchID);
                }
            }

            if (myDTReturnSent.Rows.Count > 0)
            {
                if (!CurrencyDdList.SelectedValue.Equals("ALL"))
                {
                    int currencyCount = GetCountForSelectedCurrency(myDTReturnSent, CurrencyDdList.SelectedValue);
                    if (currencyCount > 0)
                    {
                        myDTReturnSent = GetFilteredDataFromBulkDataByfilterExpression(myDTReturnSent, "Currency", CurrencyDdList.SelectedValue);
                    }
                    else
                    {
                        myDTReturnSent = new DataTable();
                    }
                }
            }            
            dv = myDTReturnSent.DefaultView;
            dtgEFTChecker.CurrentPageIndex = 0;
            dtgEFTChecker.DataSource = dv;
            dtgEFTChecker.DataBind();

            if (myDTReturnSent.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + myDTReturnSent.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(myDTReturnSent.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
            cbxSelectAll.Checked = false;
        }

        

        protected void dtgEFTChecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTChecker.CurrentPageIndex = e.NewPageIndex;
            dtgEFTChecker.DataSource = myDTReturnSent;
            dtgEFTChecker.DataBind();
        }

        protected void dtgEFTChecker_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = myDTReturnSent.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgEFTChecker.DataSource = dv;
            dtgEFTChecker.DataBind();
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

        protected void PrintVoucherBtn_Click(object sender, EventArgs e)
        {
            string FileName = "Outward Return Voucher-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".pdf";
            PrintPDF(FileName);

        }

        protected void PrintPDF(string FileName)
        {
            EFTN.component.SentReturnDB sentReturnDB = new EFTN.component.SentReturnDB();

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
            float[] headerwidths = { 10, 10, 20, 6,19, 5, 10, 10, 10 };
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
            datatable.AddCell(new iTextSharp.text.Phrase("Return Sent Voucher", fntlrg));
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
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Value Date", fnt));

            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 1;

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{

            decimal totalAmount = 0;
            //System.Data.SqlClient.SqlDataReader sqlDRTS;

            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxRetSent");

                if (cbx.Checked)
                {
                    string returnId = dtgEFTChecker.DataKeys[i].ToString();
                    Guid ReturnID = new Guid(returnId);

                    EFTN.component.SentReturnDB db = new EFTN.component.SentReturnDB();
                    DataTable dtRS = db.GetSentRRForEBBSCheckerByReturnID(ReturnID);

                    //while (sqlDRTS.Read())
                    for (int rsCount = 0; rsCount < dtRS.Rows.Count; rsCount++)
                    {
                        decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(dtRS.Rows[rsCount]["Amount"].ToString());
                        totalAmount += transAmount;

                        string transactionCode = dtRS.Rows[rsCount]["TransactionCode"].ToString();

                        if (transactionCode.Equals("27")
                            || transactionCode.Equals("37")
                            || transactionCode.Equals("55")
                           )
                        {
                            //datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            //datatable.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"]), fnt));
                            //datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                            //datatable.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                            //datatable.AddCell(new iTextSharp.text.Phrase(((string)dtRS.Rows[rsCount]["TraceNumber"]).ToString(), fnt));
                            //datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                            //datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            //datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            //datatable.AddCell(new iTextSharp.text.Phrase((string)dtRS.Rows[rsCount]["EntryDate"], fnt));

                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(dtRS.Rows[rsCount]["IdNumber"].ToString()), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtRS.Rows[rsCount]["AccountName"], fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("N/A", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtRS.Rows[rsCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtRS.Rows[rsCount]["EntryDate"], fnt));
                        }
                        else
                        {
                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(dtRS.Rows[rsCount]["IdNumber"].ToString()), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtRS.Rows[rsCount]["AccountName"], fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("N/A", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(((string)dtRS.Rows[rsCount]["TraceNumber"]).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((string)dtRS.Rows[rsCount]["EntryDate"], fnt));

                        //    datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                        //    datatable.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"]), fnt));
                        //    datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                        //    datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                        //    datatable.AddCell(new iTextSharp.text.Phrase(((string)dtRS.Rows[rsCount]["TraceNumber"]).ToString(), fnt));
                        //    datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                        //    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        //    datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                        //    datatable.AddCell(new iTextSharp.text.Phrase((string)dtRS.Rows[rsCount]["EntryDate"], fnt));
                        //
                        }
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
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            //datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(totalAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

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
        }

        protected void cbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedCbx = cbxSelectAll.Checked;
            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxRetSent");
                cbx.Checked = checkedCbx;
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            //string rejectReason = txtRejectedReason.Text;
            int approvedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxRetSent");

                if (cbx.Checked)
                {
                    string edrId = dtgEFTChecker.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    EFTN.component.SentReturnDB db = new EFTN.component.SentReturnDB();
                    db.UpdateRRSentStatusApprovedByEBBSChecker(EDRID, approvedBy);
                }
            }
            BindData();
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (txtRejectedReason.Text != "")
            {
                EnterRejectReason();
                ChangeStatusOfReturnSent();

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
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxRetSent");

                if (cbx.Checked)
                {
                    string returnId = dtgEFTChecker.DataKeys[i].ToString();
                    Guid ReturnID = new Guid(returnId);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(ReturnID, rejectReason,
                            (int)EFTN.Utility.ItemType.ReturnSent);
                }
            }
        }

        private void ChangeStatusOfReturnSent()
        {
            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxRetSent");

                if (cbx.Checked)
                {
                    string returnId = dtgEFTChecker.DataKeys[i].ToString();
                    Guid ReturnID = new Guid(returnId);

                    EFTN.component.SentReturnDB db = new EFTN.component.SentReturnDB();
                    db.UpdateRRSentStatusRejectedByEBBSChecker(ReturnID);
                }
            }
        }

        protected void btnExpotToCSV_Click(object sender, EventArgs e)
        {
            //DataTable dt = GetData();

            if (myDTReturnSent.Rows.Count > 0)
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
                int iColCount = myDTReturnSent.Columns.Count;

                // First we will write the headers. 

                for (int i = 1; i < iColCount; i++)
                {
                    sw.Write("\"");
                    sw.Write(myDTReturnSent.Columns[i]);
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
                foreach (DataRow dr in myDTReturnSent.Rows)
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


        public DataTable GetFilteredDataFromBulkDataByfilterExpression(DataTable bulkData, string columnName, string filterExpression)
        {
            DataTable filteredData = new DataTable();
            return filteredData = bulkData.Select(columnName + "='" + filterExpression + "'").CopyToDataTable() ?? bulkData;
        }

        protected void CurrencyDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
