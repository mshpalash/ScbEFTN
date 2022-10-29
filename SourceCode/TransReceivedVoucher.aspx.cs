using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EFTN.Utility;
using System.IO;
using EFTN.component;

namespace EFTN
{
    public partial class TransReceivedVoucher : System.Web.UI.Page
    {
        private static DataTable MyDt = new DataTable();
        DataView dv;
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        private static DataTable inwardReturnDataTable = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');

                ddlistDayEnd.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonthEnd.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYearEnd.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');

                sortOrder = "asc";
                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("225"))
                {
                    finacleDiv.Visible = true;
                }
                else
                {
                    finacleDiv.Visible = false;
                }
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
        private void BindData()
        {
            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();
            EFTN.component.ReceivedEDRDB receivedEDR = new component.ReceivedEDRDB();

            int BranchID = 0;

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);
            }

            if (ddListReportType.SelectedValue.Equals("1"))
            {
                if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    MyDt = edrDB.GetReceivedEDR_ApprovedByMaker_ForDebit(
                                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'),
                                                                              ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                                                            + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                                                            + ddlistDayEnd.SelectedValue.PadLeft(2, '0'),
                                                                            BranchID);
                }
                else
                {
                    //In CentralInward=1 and DepartmentID is only 0 then able to see the transaction.
                    if (ConfigurationManager.AppSettings["CentralInward"].Equals("1"))
                    {
                        if (ParseData.StringToInt(Request.Cookies["DepartmentID"].Value) == 0)
                        {
                            BranchID = 0;

                            MyDt = edrDB.GetReceivedEDR_ApprovedByMaker(
                                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'),
                                                                              ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                                                            + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                                                            + ddlistDayEnd.SelectedValue.PadLeft(2, '0'),
                                                                            BranchID);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals(OriginalBankCode.SCB))
                    {
                        int DepartmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                        MyDt = receivedEDR.GetReceivedEDR_ApprovedByMaker_forSCB(
                                                                             ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'),
                                                                              ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                                                            + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                                                            + ddlistDayEnd.SelectedValue.PadLeft(2, '0'),
                                                                            BranchID, DepartmentID);
                    }
                    //GetReceivedEDR_ApprovedByMaker_forSCB(
                    else
                    {

                        MyDt = edrDB.GetReceivedEDR_ApprovedByMaker(
                                                                              ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                            + ddlistDay.SelectedValue.PadLeft(2, '0'),
                                                                              ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                                                            + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                                                            + ddlistDayEnd.SelectedValue.PadLeft(2, '0'),
                                                                            BranchID);
                    }
                }
                if (MyDt.Rows.Count > 0)
                {
                    MyDt = GetFilteredData(MyDt);
                }
                else
                {
                    MyDt = new DataTable();
                }
                dv = MyDt.DefaultView;
                dtgEFTChecker.CurrentPageIndex = 0;
                dtgEFTChecker.DataSource = dv;
                dtgEFTChecker.DataBind();

                EnableButton();
            }
            else
            {

                if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    MyDt = edrDB.GetReceivedEDR_ApprovedByMakerBySettlementJDate_ForDebit(
                                                                      ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                    + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);
                }
                else
                {
                    if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals(OriginalBankCode.SCB))
                    {
                        int DepartmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                        MyDt = receivedEDR.GetReceivedEDR_ApprovedByMakerBySettlementJDate_forSCB(
                                                                          ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                        + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                        + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID, DepartmentID);
                    }
                    else
                    {
                        MyDt = edrDB.GetReceivedEDR_ApprovedByMakerBySettlementJDate(
                                                                          ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                        + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                        + ddlistDay.SelectedValue.PadLeft(2, '0'), BranchID);
                    }
                }
                if (MyDt.Rows.Count > 0)
                {
                    MyDt = GetFilteredData(MyDt);
                }
                else
                {
                    MyDt = new DataTable();
                }
                dv = MyDt.DefaultView;
                dtgEFTChecker.CurrentPageIndex = 0;
                dtgEFTChecker.DataSource = dv;
                dtgEFTChecker.DataBind();
                DisableButton();
            }

            if (MyDt.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + MyDt.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(MyDt.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
        }

        private DataTable GetFilteredData(DataTable MyDt)
        {
            DataTable filteredData = new DataTable();
            int currencyCounter = GetCountForSelectedCurrency(MyDt, CurrencyDdList.SelectedValue);
            int sessionCounter = GetCountForSelectedSession(MyDt, SessionDdList.SelectedValue);
            if (currencyCounter > 0 && sessionCounter > 0)
            {
                var linqResult = MyDt.AsEnumerable()
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
        private void DisableButton()
        {
            btnAccept.Visible = false;
            btnReject.Visible = false;
        }


        private void EnableButton()
        {
            btnAccept.Visible = true;
            btnReject.Visible = true;
        }

        protected void dtgEFTChecker_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = MyDt.DefaultView;
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

        protected void dtgEFTChecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTChecker.CurrentPageIndex = e.NewPageIndex;
            dtgEFTChecker.DataSource = MyDt;
            dtgEFTChecker.DataBind();
        }

        protected void PrintVoucherBtn_Click(object sender, EventArgs e)
        {
            string FileName = "Inward Transaction Voucher-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".pdf";
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

            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(11);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            float[] headerwidths = { 8, 8, 10, 10, 6, 17, 16, 4, 7, 7, 7 };
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
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase(ConfigurationManager.AppSettings["OriginBankName"], fntlrg));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatable.AddCell(new iTextSharp.text.Phrase("System Name\n\nUser Id\n\nUser Name\n\nCountry", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("CBS\n\n\n\n\n\nBangladesh", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Daily Voucher\n\n" + System.DateTime.Today.ToString("dd/MM/yyyy"), fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Transaction Received Voucher", fntlrg));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(logo);
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            datatable.DefaultCell.BorderWidth = 1;

            datatable.AddCell(new iTextSharp.text.Phrase("C.cy", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Account No.", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("A/C Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("TraceNo.", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Trn Code", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Narration", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Payment Info", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Seq. No", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Debit", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Credit", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Value Date", fnt));

            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 1;

            decimal totalAmount = 0;

            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxTransReceived");

                if (cbx.Checked)
                {
                    string edrId = dtgEFTChecker.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();
                    //sqlDRTR = db.GetReceivedEDRApprovedByMakerByEDRID(EDRID);
                    DataTable dtTR = new DataTable();
                    dtTR = db.GetReceivedEDRApprovedByMakerByEDRID(EDRID);

                    for (int trCount = 0; trCount < dtTR.Rows.Count; trCount++)
                    {
                        decimal transAmount = EFTN.Utility.ParseData.StringToDecimal(dtTR.Rows[trCount]["Amount"].ToString());
                        totalAmount += transAmount;

                        string transactionCode = dtTR.Rows[trCount]["TransactionCode"].ToString();

                        if (transactionCode.Equals("27")
                            || transactionCode.Equals("37")
                            || transactionCode.Equals("55")
                           )
                        {
                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(dtTR.Rows[trCount]["DFIAccountNo"].ToString()), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["AccountName"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["TraceNumber"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["Narration"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["PaymentInfo"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(System.DateTime.Now.ToString("dd/MM/yyyy"), fnt));

                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"]), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["TraceNumber"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["Narration"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["PaymentInfo"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(System.DateTime.Now.ToString("dd/MM/yyyy"), fnt));

                        }
                        else
                        {
                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"]), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("BANGLADESH BANK", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["TraceNumber"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Dr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["Narration"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["PaymentInfo"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 1).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(System.DateTime.Now.ToString("dd/MM/yyyy"), fnt));

                            datatable.AddCell(new iTextSharp.text.Phrase("BDT", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(AccountMasking.MaskAccount(dtTR.Rows[trCount]["DFIAccountNo"].ToString()), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["AccountName"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["TraceNumber"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("Cr.", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["Narration"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(dtTR.Rows[trCount]["PaymentInfo"].ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase((i * 2 + 2).ToString(), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(EFTN.Utility.ParseData.StringToDouble(transAmount.ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                            datatable.AddCell(new iTextSharp.text.Phrase(System.DateTime.Now.ToString("dd/MM/yyyy"), fnt));
                        }
                    }

                    dtTR.Dispose();
                }
            }

            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
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
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Maker", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            PdfPCell ca1 = new PdfPCell();
            ca1.BorderWidth = 0;
            ca1.BorderWidthRight = 1;
            ca1.BorderColorRight = new iTextSharp.text.Color(200, 200, 200);
            ca1.Colspan = 3;
            ca1.AddElement(new iTextSharp.text.Phrase("Checked by / Authorised By", fnt));
            datatable.AddCell(ca1);
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

            datatable.AddCell(new iTextSharp.text.Phrase("Batch No", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
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
            datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
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
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxTransReceived");
                cbx.Checked = checkedCbx;
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            ChangeStatusSelected("Approve");
            BindData();
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (!txtRejectedReason.Text.Equals(string.Empty))
            {
                ChangeStatusSelected("Reject");
                EnterRejectReason();
                BindData();
            }
            else
            {
                lblNoReturnReason.Text = "Please enter reason";
                lblNoReturnReason.ForeColor = System.Drawing.Color.Red;
                lblNoReturnReason.Visible = true;
            }
        }

        private void ChangeStatusSelected(string statuschangesfor)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();
            int cbxCounter = 0;
            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxTransReceived");
                if (cbx.Checked)
                {
                    Guid edrId = (Guid)dtgEFTChecker.DataKeys[i];
                    if (statuschangesfor.Equals("Approve"))
                    {
                        db.UpdateReceivedEDRStatusApprovedByEBBSChecker(edrId, ApprovedBy);
                    }
                    else
                    {
                        db.UpdateReceivedEDRStatusRejectedByEBBSChecker(edrId, ApprovedBy);
                    }
                    cbxCounter++;
                }
            }
            if (cbxCounter < 1)
            {
                lblNoReturnReason.Text = "*Please select item";
                lblNoReturnReason.ForeColor = System.Drawing.Color.Red;
                lblNoReturnReason.Visible = true;
            }
            else
            {
                lblNoReturnReason.Visible = false;
            }
        }

        private void EnterRejectReason()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("chkBoxTransReceived");

                if (cbx.Checked)
                {
                    string edrId = dtgEFTChecker.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(EDRID, rejectReason,
                            (int)EFTN.Utility.ItemType.TransactionReceived);
                }
            }
        }

        protected void btnExpotToCSV_Click(object sender, EventArgs e)
        {
            //DataTable dt = GetData();

            if (MyDt.Rows.Count > 0)
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
                int iColCount = MyDt.Columns.Count;

                // First we will write the headers. 

                for (int i = 1; i < iColCount; i++)
                {
                    sw.Write("\"");
                    sw.Write(MyDt.Columns[i]);
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
                foreach (DataRow dr in MyDt.Rows)
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
