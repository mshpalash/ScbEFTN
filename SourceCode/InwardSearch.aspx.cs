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
using EFTN.component;
using EFTN.Utility;
using System.Data.SqlClient;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace EFTN
{
    public partial class InwardSearch : System.Web.UI.Page
    {
        private static DataTable dtSearch = new DataTable();
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private static string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCurrencyTypeDropdownlist();
                BindSessionDropdownlist();
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();

                ddlistDayEnd.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0'); ;
                ddlistMonthEnd.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYearEnd.SelectedValue = System.DateTime.Now.Year.ToString();
                BindData();
                BindBank();
                BindBranch();
            }
        }
        private void BindCurrencyTypeDropdownlist()
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

        private void BindSessionDropdownlist()
        {
            DataTable dropDownData = new DataTable();
            dropDownData.Columns.Add("SessionID");
            DataRow row = dropDownData.NewRow();
            row["SessionID"] = "ALL";
            dropDownData.Rows.Add(row);
            dropDownData.Merge(sentBatchDB.GetSessions(eftConString));
            SessionDdList.DataSource = dropDownData;
            SessionDdList.DataTextField = "SessionID";
            SessionDdList.DataValueField = "SessionID";
            SessionDdList.DataBind();          
        }
        private void BindData()
        {
            bool isFromArchive = true;
            if (Request.Params["inwardSearchEDRID"] != null)
            {
                SetSearchParameterFromSession();
                BindReturnData(Request.Params["inwardSearchEDRID"]);

                if (Session["paramRetTraceNumber"] != null)
                {
                    Session.Remove("paramRetTraceNumber");
                }
                Session.Add("paramRetTraceNumber", Request.Params["inwardSearchEDRID"]);

            }

            if (Request.Params["inwardSearchReturnID"] != null)
            {
                SetSearchParameterFromSession();
                BindReturnData(Session["paramRetTraceNumber"].ToString());
                BindDishonorRecordByReturnTraceNumber(Request.Params["inwardSearchReturnID"].ToString());
            }

            if (Request.Params["inwardSearchNOCID"] != null)
            {
                BindNOCData(Session["paramRetTraceNumber"].ToString());
                BindRNOCRecordByNOCTraceNumber(Request.Params["inwardSearchNOCID"].ToString());
                SetSearchParameterFromSession();
                if (chkBoxArchive.Checked)
                {
                    isFromArchive = true;
                }
                BindInwardSearchResult(isFromArchive);
            }

        }

        private void BindRNOCRecordByNOCTraceNumber(string NOCTraceNumber)
        {
            EFTN.component.InwardSearchDB inwardSearchDB = new EFTN.component.InwardSearchDB();
            DataTable dtRNOCData = inwardSearchDB.GetInwardSearchRNOCByTraceNumber(NOCTraceNumber);
            //SqlDataReader sqlDRRNOCData = inwardSearchDB.GetInwardSearchRNOCByTraceNumber(NOCTraceNumber);

            if (dtRNOCData.Rows.Count > 0)
            {
                lblRNOC.Visible = false;
                dtgRNOC.DataSource = dtRNOCData;
                dtgRNOC.DataBind();
            }
            else
            {
                lblRNOC.Visible = true;
                dtgRNOC.Visible = false;
            }
        }

        private void BindDishonorRecordByReturnTraceNumber(string ReturnTraceNumber)
        {
            EFTN.component.InwardSearchDB inwardSearchDB = new EFTN.component.InwardSearchDB();
            DataTable dtDishonorData = inwardSearchDB.GetInwardSearchDishonorRecordByTraceNumber(ReturnTraceNumber);
            //SqlDataReader sqlDRDishonorData = inwardSearchDB.GetInwardSearchDishonorRecordByTraceNumber(ReturnTraceNumber);

            if (dtDishonorData.Rows.Count > 0)
            {
                lblDishonorMsg.Visible = false;
                dtgDishonorRecord.DataSource = dtDishonorData;
                dtgDishonorRecord.DataBind();
            }
            else
            {
                lblDishonorMsg.Visible = true;
                dtgDishonorRecord.Visible = false;
            }
        }

        private void BindReturnData(string EDRID)
        {
            EFTN.component.InwardSearchDB inwardSearchDB = new EFTN.component.InwardSearchDB();
            Guid gEDRID = new Guid(EDRID);
            DataTable dtReturnData = inwardSearchDB.GetInwardSearchReturnRecordByEDRID(gEDRID);
            //SqlDataReader sqlDRReturnData = inwardSearchDB.GetInwardSearchReturnRecordByEDRID(gEDRID);
            bool isFromArchive = false;
            if (chkBoxArchive.Checked)
            {
                isFromArchive = true;
            }
            if (dtReturnData.Rows.Count > 0)
            {
                lblReturnMsg.Visible = false;
                dtgReturnRecord.Visible = true;
                dtgReturnRecord.DataSource = dtReturnData;
                dtgReturnRecord.DataBind();
            }
            else
            {
                BindNOCData(EDRID);
                lblReturnMsg.Visible = true;
                dtgReturnRecord.Visible = false;
            }

            BindInwardSearchResult(isFromArchive);
        }

        private void BindNOCData(string EDRID)
        {
            EFTN.component.InwardSearchDB inwardSearchDB = new EFTN.component.InwardSearchDB();
            Guid gEDRID = new Guid(EDRID);
            DataTable dtNOCData = inwardSearchDB.GetInwardSearchNOCByEDRID(gEDRID);
            //SqlDataReader sqlDRNOCData = inwardSearchDB.GetInwardSearchNOCByEDRID(gEDRID);

            if (dtNOCData.Rows.Count > 0)
            {
                lblNOCMsg.Visible = false;
                dtgNOCRecord.Visible = true;
                dtgNOCRecord.DataSource = dtNOCData;
                dtgNOCRecord.DataBind();
            }
            else
            {
                lblNOCMsg.Visible = true;
                dtgNOCRecord.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchEndDateParam = ddlistYearEnd.SelectedValue.PadLeft(4, '0') + ddlistMonthEnd.SelectedValue.PadLeft(2, '0') + ddlistDayEnd.SelectedValue.PadLeft(2, '0');
            string searchStartDateParam = ddlistYear.SelectedValue.PadLeft(4, '0') + ddlistMonth.SelectedValue.PadLeft(2, '0') + ddlistDay.SelectedValue.PadLeft(2, '0');
            bool isFromArchive = false;
            if (ParseData.StringToInt(searchEndDateParam) < ParseData.StringToInt(searchStartDateParam))
            {
                lblSearchMsg.Text = "End date must be greater than Start date";
                return;
            }
            else
            {
                lblSearchMsg.Text = string.Empty;
            }
            RemoveReturnData();
            RemoveDishonorData();
            RemoveContestedData();
            RemoveNOCData();
            RemoveRNOCData();
            if (chkBoxArchive.Checked)
            {
                isFromArchive = true;
            }
            BindInwardSearchResult(isFromArchive);
            AddSearchParameterInSession();
        }

        private void RemoveReturnData()
        {
            lblReturnMsg.Text = string.Empty;
            dtgReturnRecord.DataSource = null;
            dtgReturnRecord.DataBind();
        }

        private void RemoveDishonorData()
        {
            lblDishonorMsg.Text = string.Empty;
            dtgDishonorRecord.DataSource = null;
            dtgDishonorRecord.DataBind();
        }

        private void RemoveContestedData()
        {

        }

        private void RemoveNOCData()
        {
            lblNOCMsg.Text = string.Empty;
            dtgNOCRecord.DataSource = null;
            dtgNOCRecord.DataBind();
        }

        private void RemoveRNOCData()
        {
            lblRNOC.Text = string.Empty;
            dtgRNOC.DataSource = null;
            dtgRNOC.DataBind();
        }

        private void AddSearchParameterInSession()
        {
            RemoveSearchParameterFromeSession();
            Session.Add("inwardSearchParamDay", ddlistDay.SelectedValue);
            Session.Add("inwardSearchParamMonth", ddlistMonth.SelectedValue);
            Session.Add("inwardSearchParamYear", ddlistYear.SelectedValue);
            Session.Add("inwardSearchParamEndDay", ddlistDayEnd.SelectedValue);
            Session.Add("inwardSearchParamEndMonth", ddlistMonthEnd.SelectedValue);
            Session.Add("inwardSearchParamEndYear", ddlistYearEnd.SelectedValue);

            Session.Add("inwardSearchParamReceivingBankRoutingNo", txtReceivingBankRoutingNo.Text.Trim());
            Session.Add("inwardSearchParamSendingBankRoutNo", txtSendingBankRoutNo.Text.Trim());
            Session.Add("inwardSearchParamDFIAccountNo", txtDFIAccountNo.Text.Trim());
            Session.Add("inwardSearchParamReceiverName", txtReceiverName.Text.Trim());
            Session.Add("inwardSearchParamAmount", txtAmount.Text.Trim());
            Session.Add("inwardSearchParamMaxAmount", txtMaxAmount.Text.Trim());

            Session.Add("inwardSearchParamIDNumber", txtIdNumber.Text.Trim());
            Session.Add("inwardSearchParamCompanyName", txtCompanyName.Text.Trim());
            Session.Add("inwardSearchParamBankName", txtBankName.Text.Trim());
            Session.Add("inwardSearchParamBranchName", txtBranchName.Text.Trim());
            Session.Add("inwardSearchParamTraceNumber", txtTraceNumber.Text.Trim());
            Session.Add("inwardSearchParamBatchNumber", txtTraceNumber.Text.Trim());
            Session.Add("inwardSearchParamRdoBtnSearchType", rdoBtnSearchType.SelectedValue.Trim());
            Session.Add("inwardSearchParamPaymentInfo", txtPaymentInfo.Text.Trim());
            Session.Add("inwardSearchParamFromArchive", chkBoxArchive.Checked.ToString());



            //Session.Add("inwardSearchParamAccountNo", txtAccountNo.Text.Trim());
        }

        private void RemoveSearchParameterFromeSession()
        {
            if (Session["inwardSearchParamDay"] != null)
            {
                Session.Remove("inwardSearchParamDay");
            }

            if (Session["inwardSearchParamMonth"] != null)
            {
                Session.Remove("inwardSearchParamMonth");
            }

            if (Session["inwardSearchParamYear"] != null)
            {
                Session.Remove("inwardSearchParamYear");
            }

            if (Session["inwardSearchParamEndDay"] != null)
            {
                Session.Remove("inwardSearchParamEndDay");
            }

            if (Session["inwardSearchParamEndMonth"] != null)
            {
                Session.Remove("inwardSearchParamEndMonth");
            }

            if (Session["inwardSearchParamEndYear"] != null)
            {
                Session.Remove("inwardSearchParamEndYear");
            }

            if (Session["inwardSearchParamReceivingBankRoutingNo"] != null)
            {
                Session.Remove("inwardSearchParamReceivingBankRoutingNo");
            }

            if (Session["inwardSearchParamDFIAccountNo"] != null)
            {
                Session.Remove("inwardSearchParamDFIAccountNo");
            }

            if (Session["inwardSearchParamReceiverName"] != null)
            {
                Session.Remove("inwardSearchParamReceiverName");
            }

            if (Session["inwardSearchParamAmount"] != null)
            {
                Session.Remove("inwardSearchParamAmount");
            }

            if (Session["inwardSearchParamMaxAmount"] != null)
            {
                Session.Remove("inwardSearchParamMaxAmount");
            }

            if (Session["inwardSearchParamIDNumber"] != null)
            {
                Session.Remove("inwardSearchParamIDNumber");
            }
            if (Session["inwardSearchParamCompanyName"] != null)
            {
                Session.Remove("inwardSearchParamCompanyName");
            }
            if (Session["inwardSearchParamBankName"] != null)
            {
                Session.Remove("inwardSearchParamBankName");
            }
            if (Session["inwardSearchParamBranchName"] != null)
            {
                Session.Remove("inwardSearchParamBranchName");
            }
            if (Session["inwardSearchParamTraceNumber"] != null)
            {
                Session.Remove("inwardSearchParamTraceNumber");
            }
            if (Session["inwardSearchParamBatchNumber"] != null)
            {
                Session.Remove("inwardSearchParamBatchNumber");
            }
            if (Session["paramRetTraceNumber"] != null)
            {
                Session.Remove("paramRetTraceNumber");
            }
            if (Session["inwardSearchParamRdoBtnSearchType"] != null)
            {
                Session.Remove("inwardSearchParamRdoBtnSearchType");
            }
            if (Session["inwardSearchParamPaymentInfo"] != null)
            {
                Session.Remove("inwardSearchParamPaymentInfo");
            }
            if (Session["inwardSearchParamFromArchive"] != null)
            {
                Session.Remove("inwardSearchParamFromArchive");
            }
        }

        private void SetSearchParameterFromSession()
        {
            if (Session["inwardSearchParamDay"] != null)
            {
                ddlistDay.SelectedValue = Session["inwardSearchParamDay"].ToString();
            }

            if (Session["inwardSearchParamMonth"] != null)
            {
                ddlistMonth.SelectedValue = Session["inwardSearchParamMonth"].ToString();
            }

            if (Session["inwardSearchParamYear"] != null)
            {
                ddlistYear.SelectedValue = Session["inwardSearchParamYear"].ToString();
            }

            if (Session["inwardSearchParamEndDay"] != null)
            {
                ddlistDayEnd.SelectedValue = Session["inwardSearchParamEndDay"].ToString();
            }

            if (Session["inwardSearchParamEndMonth"] != null)
            {
                ddlistMonthEnd.SelectedValue = Session["inwardSearchParamEndMonth"].ToString();
            }

            if (Session["inwardSearchParamEndYear"] != null)
            {
                ddlistYearEnd.SelectedValue = Session["inwardSearchParamEndYear"].ToString();
            }

            if (Session["inwardSearchParamReceivingBankRoutingNo"] != null)
            {
                txtReceivingBankRoutingNo.Text = Session["inwardSearchParamReceivingBankRoutingNo"].ToString();
            }

            if (Session["inwardSearchParamSendingBankRoutNo"] != null)
            {
                txtSendingBankRoutNo.Text = Session["inwardSearchParamSendingBankRoutNo"].ToString();
            }

            if (Session["inwardSearchParamDFIAccountNo"] != null)
            {
                txtDFIAccountNo.Text = Session["inwardSearchParamDFIAccountNo"].ToString();
            }

            if (Session["inwardSearchParamReceiverName"] != null)
            {
                txtReceiverName.Text = Session["inwardSearchParamReceiverName"].ToString();
            }

            if (Session["inwardSearchParamAmount"] != null)
            {
                txtAmount.Text = Session["inwardSearchParamAmount"].ToString();
            }

            if (Session["inwardSearchParamMaxAmount"] != null)
            {
                txtAmount.Text = Session["inwardSearchParamMaxAmount"].ToString();
            }

            if (Session["inwardSearchParamIDNumber"] != null)
            {
                txtIdNumber.Text = Session["inwardSearchParamIDNumber"].ToString();
            }

            if (Session["inwardSearchParamCompanyName"] != null)
            {
                txtCompanyName.Text = Session["inwardSearchParamCompanyName"].ToString();
            }

            if (Session["inwardSearchParamBankName"] != null)
            {
                txtBankName.Text = Session["inwardSearchParamBankName"].ToString();
            }

            if (Session["inwardSearchParamBranchName"] != null)
            {
                txtBranchName.Text = Session["inwardSearchParamBranchName"].ToString();
            }

            if (Session["inwardSearchParamTraceNumber"] != null)
            {
                txtTraceNumber.Text = Session["inwardSearchParamTraceNumber"].ToString();
            }

            if (Session["inwardSearchParamBatchNumber"] != null)
            {
                txtBatchNumber.Text = Session["inwardSearchParamBatchNumber"].ToString();
            }

            if (Session["inwardSearchParamRdoBtnSearchType"] != null)
            {
                rdoBtnSearchType.SelectedValue = Session["inwardSearchParamRdoBtnSearchType"].ToString();
            }

            if (Session["inwardSearchParamPaymentInfo"] != null)
            {
                txtPaymentInfo.Text = Session["inwardSearchParamPaymentInfo"].ToString();
            }
            if (Session["inwardSearchParamFromArchive"] != null)
            {
                chkBoxArchive.Checked = bool.Parse(Session["inwardSearchParamFromArchive"].ToString());
            }
        }

        private void BindInwardSearchResult(bool isFromArchive)
        {
            string beginDate = ddlistYear.SelectedValue.PadLeft(4, '0')
                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                 + ddlistDay.SelectedValue.PadLeft(2, '0');

            string endDate = ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                 + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                 + ddlistDayEnd.SelectedValue.PadLeft(2, '0');

            if (txtAmount.Text.Trim().Equals(string.Empty))
            {
                txtAmount.Text = txtMaxAmount.Text.Trim();
            }
            if (txtMaxAmount.Text.Trim().Equals(string.Empty))
            {
                txtMaxAmount.Text = txtAmount.Text.Trim();
            }

            dtSearch.Clear();
            dtSearch.Dispose();

            InwardSearchDB inwardSearchDB = new InwardSearchDB();
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            string BranchWise = ConfigurationManager.AppSettings["BranchWise"];

            if (BranchWise.Equals("1"))
            {
                dtSearch = inwardSearchDB.GetInwardSearchResultForBranchWise(beginDate, endDate,
                                                                    txtSendingBankRoutNo.Text.Trim(),
                                                                    txtDFIAccountNo.Text.Trim(),
                                                                    txtReceiverName.Text.Trim(),
                                                                    ParseData.StringToDouble(txtAmount.Text.Trim()),
                                                                    ParseData.StringToDouble(txtMaxAmount.Text.Trim()),
                                                                    txtIdNumber.Text.Trim(),
                                                                    txtCompanyName.Text.Trim(),
                                                                    txtBankName.Text.Trim(),
                                                                    txtReceivingBankRoutingNo.Text.Trim(),
                                                                    txtBranchName.Text.Trim(),
                                                                    txtTraceNumber.Text.Trim(),
                                                                    txtBatchNumber.Text.Trim(),
                                                                    rdoBtnSearchType.SelectedValue.Trim(),
                                                                    txtPaymentInfo.Text.Trim(),
                                                                    ParseData.StringToInt(ddListTransactionType.SelectedValue)
                                                                    , UserID
                                                                    ,CurrencyDdList.SelectedValue
                                                                    ,SessionDdList.SelectedValue);
            }
            else
            {
                dtSearch = inwardSearchDB.GetInwardSearchResult(beginDate, endDate,
                                                                    txtSendingBankRoutNo.Text.Trim(),
                                                                    txtDFIAccountNo.Text.Trim(),
                                                                    txtReceiverName.Text.Trim(),
                                                                    ParseData.StringToDouble(txtAmount.Text.Trim()),
                                                                    ParseData.StringToDouble(txtMaxAmount.Text.Trim()),
                                                                    txtIdNumber.Text.Trim(),
                                                                    txtCompanyName.Text.Trim(),
                                                                    txtBankName.Text.Trim(),
                                                                    txtReceivingBankRoutingNo.Text.Trim(),
                                                                    txtBranchName.Text.Trim(),
                                                                    txtTraceNumber.Text.Trim(),
                                                                    txtBatchNumber.Text.Trim(),
                                                                    rdoBtnSearchType.SelectedValue.Trim(),
                                                                    txtPaymentInfo.Text.Trim(),
                                                                    ParseData.StringToInt(ddListTransactionType.SelectedValue),
                                                                    UserID,
                                                                    isFromArchive
                                                                    , CurrencyDdList.SelectedValue
                                                                    , SessionDdList.SelectedValue);
            }

            if (dtSearch.Rows.Count > 0)
            {
                lblMsg.Visible = false;
                dtgInwardSearch.Visible = true;
                dtgInwardSearch.CurrentPageIndex = 0;
                dtgInwardSearch.DataSource = dtSearch;
                dtgInwardSearch.DataBind();
            }
            else
            {
                lblMsg.Visible = true;
                dtgInwardSearch.Visible = false;
            }
        }

        protected void cbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedCbx = cbxSelectAll.Checked;
            for (int i = 0; i < dtgInwardSearch.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgInwardSearch.Items[i].FindControl("chkBoxTransReceived");
                cbx.Checked = checkedCbx;
            }
        }

        protected void PrintVoucherBtn_Click(object sender, EventArgs e)
        {
            string FileName = "CustomerAdvice-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".pdf";
            PrintPDF(FileName);
        }

        protected void lnkBtnPrintAllSearchResult_Click(object sender, EventArgs e)
        {
            string FileName = "CustomerAdvice-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".pdf";
            PrintPDFForAllSearchData(FileName);
        }

        protected void PrintPDF(string FileName)
        {
            int printItemCount = 0;

            for (int i = 0; i < dtgInwardSearch.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgInwardSearch.Items[i].FindControl("chkBoxTransReceived");
                if (cbx.Checked)
                {
                    printItemCount++;
                }
                else if (printItemCount > 0)
                {
                    break;
                }
            }
            if (printItemCount == 0)
            {
                return;
            }

            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();

            string UserName = Request.Cookies["UserName"].Value;
            string LogoImage = Server.MapPath("images") + "\\SCBLogo.JPG";

            Response.ClearContent();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

            Document document = new Document(PageSize.A4.Rotate(), 1, 1, 12, 12);
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

            Font fnt = new Font(Font.HELVETICA, 8);
            Font fntlrg = new Font(Font.HELVETICA, 12);
            Font fntblue = new Font(Font.HELVETICA, 8);
            fntblue.Color = new Color(0, 0, 255);
            Font fntbld = new Font(Font.HELVETICA, 8);
            fntbld.SetStyle(Font.BOLD);


            string str = string.Empty;
            str = str + "PLEASE NOTE THAT WE RESERVE THE RIGHT TO REVERSE THE CREDIT ENTRY FROM YOUR ACCOUNT IN THE EVENT THAT STANDARD CHARTERED BANK DOES NOT RECEIVE THE ACTUAL FUNDS IN FULL";
            str = str + "\nThis is a computer generated advice and requires no signature.                                 .";
            str = str + ConfigurationManager.AppSettings["OriginBankName"] + " All Rights Reserved";


            HeaderFooter footer = new HeaderFooter(new Phrase(str, fnt), false);
            footer.Alignment = Element.ALIGN_CENTER;

            document.Footer = footer;

            document.Open();

            string prevAccountNumber = string.Empty;
            string currnectAccountNumber = string.Empty;

            for (int i = 0; i < dtgInwardSearch.Items.Count; i++)
            {

                CheckBox cbx = (CheckBox)dtgInwardSearch.Items[i].FindControl("chkBoxTransReceived");

                if (cbx.Checked)
                {
                    iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
                    jpeg.Alignment = Element.ALIGN_RIGHT;


                    /////////////////////////////////////////////////////////
                    iTextSharp.text.pdf.PdfPTable datatableADDRESS = new iTextSharp.text.pdf.PdfPTable(4);
                    datatableADDRESS.DefaultCell.Padding = 4;
                    datatableADDRESS.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    float[] headerwidthsADDRESS = { 25, 2, 48, 25 };
                    datatableADDRESS.SetWidths(headerwidthsADDRESS);
                    datatableADDRESS.WidthPercentage = 99;

                    // iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                    datatableADDRESS.DefaultCell.BorderWidth = 0;
                    datatableADDRESS.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    datatableADDRESS.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);


                    /////////////////////////////////////////////////


                    iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(11);
                    datatable.DefaultCell.Padding = 4;
                    datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    float[] headerwidths = { 5, 5, 10, 10, 10, 6, 19, 5, 10, 10, 10 };
                    datatable.SetWidths(headerwidths);
                    datatable.WidthPercentage = 99;

                    iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                    datatable.DefaultCell.BorderWidth = 0;
                    datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                    PdfPCell logo = new PdfPCell();
                    logo.BorderWidth = 0;
                    logo.Colspan = 2;
                    logo.AddElement(jpeg);
                    //------------------------------------------

                    //datatable 2
                    document.Add(new iTextSharp.text.Paragraph(" "));
                    iTextSharp.text.pdf.PdfPTable datatable2 = new iTextSharp.text.pdf.PdfPTable(11);
                    datatable2.DefaultCell.Padding = 3;
                    datatable2.DefaultCell.Border = 2;
                    datatable2.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    //float[] headerwidths2 = { 15,35,20,15,15};
                    float[] headerwidths2 = { 8, 8, 5, 5, 10, 12, 12, 10, 10, 10, 10 };
                    datatable2.DefaultCell.BorderWidth = 2;
                    datatable2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    datatable2.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                    //datatable 3
                    iTextSharp.text.pdf.PdfPTable datatable3 = new iTextSharp.text.pdf.PdfPTable(11);
                    datatable3.DefaultCell.Padding = 3;
                    datatable3.DefaultCell.Border = 2;
                    datatable3.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    //float[] headerwidths2 = { 15,35,20,15,15};
                    datatable3.DefaultCell.BorderWidth = 1;
                    datatable3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    datatable3.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);



                    string edrId = dtgInwardSearch.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    //System.Data.SqlClient.SqlDataReader sqlDRTS;
                    DataTable dtTS = new DataTable();
                    EFTN.component.InwardSearchDB db = new EFTN.component.InwardSearchDB();
                    dtTS = db.RptAdv_forTransactionReceived(EDRID);

                    for (int rowNum = 0; rowNum < dtTS.Rows.Count; rowNum++)
                    {

                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase(" ", fntlrg));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["TITLE"].ToString(), fntlrg));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fntlrg));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["ADDRESS"].ToString(), fntlrg));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                        datatable.AddCell(new iTextSharp.text.Phrase(" ", fntbld));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("EFT Inward Advice", fntlrg));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

                        datatable.AddCell(new iTextSharp.text.Phrase("Date :\n\nReference No. :", fntbld));
                        datatable.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy") + "\n\n" + dtTS.Rows[rowNum]["TraceNumber"].ToString(), fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fntlrg));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(logo);


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
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

                        datatable2.AddCell(new iTextSharp.text.Phrase("Beneficiary Bank :", fntbld));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Beneficiary Branch :", fntbld));
                        datatable2.AddCell(new iTextSharp.text.Phrase("A/C No. :", fntbld));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Payment Info :", fntbld));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Receiver ID :", fntbld));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Chg. Dr. A/C :", fntbld));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Reason :", fntbld));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Value Date :", fntbld));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Payment Info :", fntbld));

                        datatable2.AddCell(new iTextSharp.text.Phrase("RejectReason :", fntbld));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Amount", fntbld));

                        string trCode = dtTS.Rows[rowNum]["TransactionCode"].ToString();
                        //22,24,32,42,52
                        string trType = string.Empty;
                        if (trCode.Equals("22")
                            || trCode.Equals("24")
                            || trCode.Equals("32")
                            || trCode.Equals("42")
                            || trCode.Equals("52")
                            )
                        {
                            trType = "(Cr.)";
                        }
                        else
                        {
                            trType = "(Dr.)";
                        }
                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["BankName"].ToString(), fnt));
                        //datatable3.AddCell(new iTextSharp.text.Phrase("                ", fnt));
                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["BranchName"].ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["DFIAccountNo"].ToString() + trType, fnt));
                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["PaymentInfo"].ToString(), fnt));


                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["IdNumber"].ToString(), fnt));
                        //datatable3.AddCell(new iTextSharp.text.Phrase("                ", fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["DFIAccountNo"].ToString(), fnt));


                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["EntryDesc"].ToString(), fnt));
                        // datatable3.AddCell(new iTextSharp.text.Phrase("                ", fnt));
                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["EffectiveEntryDate"].ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["PaymentInfo"].ToString(), fnt));



                        // datatable3.AddCell(new iTextSharp.text.Phrase("                ", fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["RejectReason"].ToString(), fnt));

                        //datatable3.AddCell(new iTextSharp.text.Phrase("CO TO BE POSTED", fnt));
                        //datatable3.AddCell(new iTextSharp.text.Phrase("", fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtTS.Rows[rowNum]["Amount"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
                        currnectAccountNumber = dtTS.Rows[rowNum]["DFIAccountNo"].ToString();
                    }
                    dtTS.Dispose();

                    if (!currnectAccountNumber.Equals(prevAccountNumber) && !prevAccountNumber.Equals(string.Empty))
                    {
                        document.NewPage();
                    }
                    if (!currnectAccountNumber.Equals(prevAccountNumber))
                    {
                        document.Add(datatableADDRESS);

                        document.Add(datatable);
                        document.Add(datatable2);
                        document.Add(datatable3);
                        prevAccountNumber = currnectAccountNumber;
                    }
                    else
                    {
                        document.Add(datatable3);
                        prevAccountNumber = currnectAccountNumber;
                    }

                }
            }
            //////////////////////////////////////////////
            document.Close();
            Response.End();
        }

        protected void PrintPDFForAllSearchData(string FileName)
        {
            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();

            string UserName = Request.Cookies["UserName"].Value;
            string LogoImage = Server.MapPath("images") + "\\SCBLogo.JPG";

            Response.ClearContent();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

            Document document = new Document(PageSize.A4.Rotate(), 1, 1, 12, 12);
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

            Font fnt = new Font(Font.HELVETICA, 8);
            Font fntlrg = new Font(Font.HELVETICA, 12);
            Font fntblue = new Font(Font.HELVETICA, 8);
            fntblue.Color = new Color(0, 0, 255);
            Font fntbld = new Font(Font.HELVETICA, 8);
            fntbld.SetStyle(Font.BOLD);


            string str = string.Empty;
            str = str + "PLEASE NOTE THAT WE RESERVE THE RIGHT TO REVERSE THE CREDIT ENTRY FROM YOUR ACCOUNT IN THE EVENT THAT STANDARD CHARTERED BANK DOES NOT RECEIVE THE ACTUAL FUNDS IN FULL";
            str = str + "\nThis is a computer generated advice and requires no signature.                                 .";
            str = str + ConfigurationManager.AppSettings["OriginBankName"] + " All Rights Reserved";


            HeaderFooter footer = new HeaderFooter(new Phrase(str, fnt), false);
            footer.Alignment = Element.ALIGN_CENTER;

            document.Footer = footer;

            document.Open();

            string prevAccountNumber = string.Empty;
            string currnectAccountNumber = string.Empty;

            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
                jpeg.Alignment = Element.ALIGN_RIGHT;


                /////////////////////////////////////////////////////////
                iTextSharp.text.pdf.PdfPTable datatableADDRESS = new iTextSharp.text.pdf.PdfPTable(4);
                datatableADDRESS.DefaultCell.Padding = 4;
                datatableADDRESS.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                float[] headerwidthsADDRESS = { 25, 2, 48, 25 };
                datatableADDRESS.SetWidths(headerwidthsADDRESS);
                datatableADDRESS.WidthPercentage = 99;

                // iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                datatableADDRESS.DefaultCell.BorderWidth = 0;
                datatableADDRESS.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                datatableADDRESS.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);


                /////////////////////////////////////////////////


                iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(11);
                datatable.DefaultCell.Padding = 4;
                datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                float[] headerwidths = { 5, 5, 10, 10, 10, 6, 19, 5, 10, 10, 10 };
                datatable.SetWidths(headerwidths);
                datatable.WidthPercentage = 99;

                iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                datatable.DefaultCell.BorderWidth = 0;
                datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                PdfPCell logo = new PdfPCell();
                logo.BorderWidth = 0;
                logo.Colspan = 2;
                logo.AddElement(jpeg);
                //------------------------------------------

                //datatable 2
                document.Add(new iTextSharp.text.Paragraph(" "));
                iTextSharp.text.pdf.PdfPTable datatable2 = new iTextSharp.text.pdf.PdfPTable(11);
                datatable2.DefaultCell.Padding = 3;
                datatable2.DefaultCell.Border = 2;
                datatable2.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //float[] headerwidths2 = { 15,35,20,15,15};
                float[] headerwidths2 = { 8, 8, 5, 5, 10, 12, 12, 10, 10, 10, 10 };
                datatable2.DefaultCell.BorderWidth = 2;
                datatable2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                datatable2.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                //datatable 3
                iTextSharp.text.pdf.PdfPTable datatable3 = new iTextSharp.text.pdf.PdfPTable(11);
                datatable3.DefaultCell.Padding = 3;
                datatable3.DefaultCell.Border = 2;
                datatable3.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //float[] headerwidths2 = { 15,35,20,15,15};
                datatable3.DefaultCell.BorderWidth = 1;
                datatable3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                datatable3.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);



                string edrId = dtSearch.Rows[i]["EDRID"].ToString();
                Guid EDRID = new Guid(edrId);

                //System.Data.SqlClient.SqlDataReader sqlDRTS;
                DataTable dtTS = new DataTable();
                EFTN.component.InwardSearchDB db = new EFTN.component.InwardSearchDB();
                dtTS = db.RptAdv_forTransactionReceived(EDRID);

                for (int rowNum = 0; rowNum < dtTS.Rows.Count; rowNum++)
                {

                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase(" ", fntlrg));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["TITLE"].ToString(), fntlrg));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fntlrg));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["ADDRESS"].ToString(), fntlrg));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                    datatable.AddCell(new iTextSharp.text.Phrase(" ", fntbld));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("EFT Inward Advice", fntlrg));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

                    datatable.AddCell(new iTextSharp.text.Phrase("Date :\n\nReference No. :", fntbld));
                    datatable.AddCell(new iTextSharp.text.Phrase(System.DateTime.Today.ToString("dd/MM/yyyy") + "\n\n" + dtTS.Rows[rowNum]["TraceNumber"].ToString(), fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fntlrg));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(logo);


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
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

                    datatable2.AddCell(new iTextSharp.text.Phrase("Beneficiary Bank :", fntbld));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Beneficiary Branch :", fntbld));
                    datatable2.AddCell(new iTextSharp.text.Phrase("A/C No. :", fntbld));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Payment Info :", fntbld));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Receiver ID :", fntbld));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Chg. Dr. A/C :", fntbld));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Reason :", fntbld));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Value Date :", fntbld));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Amount :", fntbld));

                    datatable2.AddCell(new iTextSharp.text.Phrase("RejectReason :", fntbld));
                    datatable2.AddCell(new iTextSharp.text.Phrase("AMOUNT CHARGE TOTAL", fntbld));

                    string trCode = dtTS.Rows[rowNum]["TransactionCode"].ToString();
                    //22,24,32,42,52
                    string trType = string.Empty;
                    if (trCode.Equals("22")
                        || trCode.Equals("24")
                        || trCode.Equals("32")
                        || trCode.Equals("42")
                        || trCode.Equals("52")
                        )
                    {
                        trType = "(Cr.)";
                    }
                    else
                    {
                        trType = "(Dr.)";
                    }


                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["BankName"].ToString(), fnt));
                    //datatable3.AddCell(new iTextSharp.text.Phrase("                ", fnt));
                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["BranchName"].ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["DFIAccountNo"].ToString() + trType, fnt));
                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["PaymentInfo"].ToString(), fnt));


                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["IdNumber"].ToString(), fnt));
                    //datatable3.AddCell(new iTextSharp.text.Phrase("                ", fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["DFIAccountNo"].ToString(), fnt));


                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["EntryDesc"].ToString(), fnt));
                    // datatable3.AddCell(new iTextSharp.text.Phrase("                ", fnt));
                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["EffectiveEntryDate"].ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtTS.Rows[rowNum]["Amount"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));



                    // datatable3.AddCell(new iTextSharp.text.Phrase("                ", fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["RejectReason"].ToString(), fnt));
                   
                    //datatable3.AddCell(new iTextSharp.text.Phrase("CO TO BE POSTED", fnt));
                    //datatable3.AddCell(new iTextSharp.text.Phrase("", fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtTS.Rows[rowNum]["Amount"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
                    currnectAccountNumber = dtTS.Rows[rowNum]["DFIAccountNo"].ToString();
                }
                dtTS.Dispose();

                if (!currnectAccountNumber.Equals(prevAccountNumber) && !prevAccountNumber.Equals(string.Empty))
                {
                    document.NewPage();
                }
                if (!currnectAccountNumber.Equals(prevAccountNumber))
                {
                    document.Add(datatableADDRESS);

                    document.Add(datatable);
                    document.Add(datatable2);
                    document.Add(datatable3);
                    prevAccountNumber = currnectAccountNumber;
                }
                else
                {
                    document.Add(datatable3);
                    prevAccountNumber = currnectAccountNumber;
                }


            }
            //////////////////////////////////////////////
            document.Close();
            Response.End();
        }

        protected void dtgInwardSearch_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgInwardSearch.CurrentPageIndex = e.NewPageIndex;
            dtgInwardSearch.DataSource = dtSearch;
            dtgInwardSearch.DataBind();
        }

        protected void btnExpotToCSV_Click(object sender, EventArgs e)
        {
            //DataTable dt = GetData();

            if (dtSearch.Rows.Count > 0)
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
                int iColCount = dtSearch.Columns.Count;

                // First we will write the headers. 

                for (int i = 1; i < iColCount; i++)
                {
                    sw.Write("\"");
                    sw.Write(dtSearch.Columns[i]);
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
                foreach (DataRow dr in dtSearch.Rows)
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

        private void BindBank()
        {
            FloraSoft.BanksDB db = new FloraSoft.BanksDB();

            ddListReceivingBank.DataSource = db.GetAllBanks();
            ddListReceivingBank.DataTextField = "BankName";
            ddListReceivingBank.DataValueField = "BankID";
            ddListReceivingBank.DataBind();
        }

        private void BindBranch()
        {
            FloraSoft.BranchesDB branchesDB = new FloraSoft.BranchesDB();

            ddListBranch.DataSource = branchesDB.GetBranchesByBankID(ParseData.StringToInt(ddListReceivingBank.SelectedValue));
            ddListBranch.DataBind();
            txtRoutingNo.Text = ddListBranch.SelectedValue;
        }

        protected void ddListBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoutingNo.Text = ddListBranch.SelectedValue;
        }

        protected void ddListReceivingBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBranch();
        }

        protected void btnRecBank_Click(object sender, EventArgs e)
        {
            txtBankName.Text = ddListReceivingBank.SelectedItem.Text;
        }

        protected void btnRoutNo_Click(object sender, EventArgs e)
        {
            txtReceivingBankRoutingNo.Text = txtRoutingNo.Text;
        }

        protected void btnSendBankRoutNo_Click(object sender, EventArgs e)
        {
            txtSendingBankRoutNo.Text = txtRoutingNo.Text;
        }

        protected void btnBranch_Click(object sender, EventArgs e)
        {
            txtBranchName.Text = ddListBranch.SelectedItem.Text;
        }

        private void PrintPDDetailReport(string FileName)
        {
            if (dtSearch.Rows.Count == 0)
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
            iTextSharp.text.pdf.PdfPTable headertable = new iTextSharp.text.pdf.PdfPTable(2);
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            headertable.DefaultCell.Padding = 0;
            headertable.WidthPercentage = 99;
            headertable.DefaultCell.Border = 0;
            float[] widthsAtHeader = { 80, 20 };
            headertable.SetWidths(widthsAtHeader);

            string DeptAndBranch = "\nFor " + ddListTransactionType.SelectedItem.Text + " Transaction";

            if (rdoBtnSearchType.SelectedValue.Equals("1"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                string settlementDateEnd = ddlistDayEnd.SelectedValue + "/" + ddlistMonthEnd.SelectedValue.PadLeft(2, '0') + "/" + ddlistYearEnd.SelectedValue;
                headertable.AddCell(new Phrase("Transaction Receive Report From: " + settlementDate + " To " + settlementDateEnd + DeptAndBranch, headerFont));
            }
            else if (rdoBtnSearchType.SelectedValue.Equals("2"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                string settlementDateEnd = ddlistDayEnd.SelectedValue + "/" + ddlistMonthEnd.SelectedValue.PadLeft(2, '0') + "/" + ddlistYearEnd.SelectedValue;
                headertable.AddCell(new Phrase("Return Sent Report From: " + settlementDate + " To " + settlementDateEnd + DeptAndBranch, headerFont));
            }
            else if (rdoBtnSearchType.SelectedValue.Equals("3"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                string settlementDateEnd = ddlistDayEnd.SelectedValue + "/" + ddlistMonthEnd.SelectedValue.PadLeft(2, '0') + "/" + ddlistYearEnd.SelectedValue;
                headertable.AddCell(new Phrase("NOC Sent Report From: " + settlementDate + " To " + settlementDateEnd + DeptAndBranch, headerFont));
            }


            //headertable.AddCell(new Phrase(" To " + settlementDateEnd, headerFont));
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            //string SelectedBank = string.Empty;

            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            float[] headerwidths;
            int NumberOfPdfColumn = 0;
            if (SelectedBank.Equals("135"))
            {
                headerwidths = new float[] { 6, 10, 10, 4, 8, 4, 8, 6, 8, 8, 8, 8, 12, 8 };
                NumberOfPdfColumn = 14;
            }
            else
            {
                //headerwidths = new float[] { 11, 11, 8, 10, 4, 8, 4, 10, 8, 8, 8, 12, 8 };
                //headerwidths = new float[] { 11, 11, 8, 10, 4, 8, 6, 6, 6, 10, 8, 8, 8, 12, 8 };4+4+2+2+2
                headerwidths = new float[] { 9, 9, 5, 10, 4, 8, 6, 6, 6, 10, 8, 8, 8, 8, 5 };
                NumberOfPdfColumn = 15;
            }

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
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0;
            if (SelectedBank.Equals("135"))
            {
                c0 = new PdfPCell(new iTextSharp.text.Phrase("Zone Name", fnt));
            }
            else
            {
                c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            }

            //PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;

            datatable.AddCell(c0);

            if (SelectedBank.Equals("135"))
            {
                datatable.AddCell(new iTextSharp.text.Phrase("Bank Name", fnt)); //only for JANATA BANK
            }
            datatable.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("SECC", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Trace No.", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Tran. Code", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Bank RoutNo", fnt));


            datatable.AddCell(new iTextSharp.text.Phrase("CCY", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Session", fnt));


            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Rec.Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("C.Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Ent.Desc", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Reject Reason", fnt));


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                if (SelectedBank.Equals("135"))
                {
                    PdfPCell cZone = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["ZoneName"], fnt));
                    cZone.BorderWidth = 0.5f;
                    cZone.HorizontalAlignment = Cell.ALIGN_LEFT;
                    cZone.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    cZone.Padding = 4;
                    datatable.AddCell(cZone);
                }

                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["BankName"], fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["BranchName"], fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 4;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["SECC"], fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["TraceNumber"], fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);

                PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["TransactionCode"], fnt));
                c5.BorderWidth = 0.5f;
                c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c5.Padding = 4;
                datatable.AddCell(c5);

                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["DFIAccountNo"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 4;
                datatable.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["ReceivingBankRoutNo"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["Currency"], fnt));
                c8.BorderWidth = 0.5f;
                c8.HorizontalAlignment = Cell.ALIGN_LEFT;
                c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c8.Padding = 4;
                datatable.AddCell(c8);

                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["SessionID"], fnt));
                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_LEFT;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 4;
                datatable.AddCell(c9);


                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dtSearch.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["IdNumber"], fnt));
                c11.BorderWidth = 0.5f;
                c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c11.Padding = 4;
                datatable.AddCell(c11);

                PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["ReceiverName"], fnt));
                c12.BorderWidth = 0.5f;
                c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c12.Padding = 4;
                datatable.AddCell(c12);

                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["CompanyName"], fnt));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_LEFT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable.AddCell(c13);

                PdfPCell c14 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["EntryDesc"], fnt));
                c14.BorderWidth = 0.5f;
                c14.HorizontalAlignment = Cell.ALIGN_LEFT;
                c14.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c14.Padding = 4;
                datatable.AddCell(c14);

                PdfPCell c15 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["RejectReason"], fnt));
                c15.BorderWidth = 0.5f;
                c15.HorizontalAlignment = Cell.ALIGN_LEFT;
                c15.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c15.Padding = 4;
                datatable.AddCell(c15);

            }

            //-------------TOTAL IN FOOTER --------------------
            if (SelectedBank.Equals("135"))
            {
                datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            }
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dtSearch.Compute("COUNT(TraceNumber)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));

            //datatable.AddCell(new iTextSharp.text.Phrase(dtSearch.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtSearch.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //-------------END TOTAL -------------------------



            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();
        }

        protected void lnkBtnPrintDetailReport_Click(object sender, EventArgs e)
        {
            if (rdoBtnSearchType.SelectedValue.Equals("1"))
            {
                string FileName = "DetailsReport-TransactionReceive" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDDetailReport(FileName);
            }
            else if (rdoBtnSearchType.SelectedValue.Equals("2"))
            {
                string FileName = "DetailsReport-ReturnSent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDDetailReport(FileName);
            }
            else if (rdoBtnSearchType.SelectedValue.Equals("3"))
            {
                string FileName = "DetailsReport-NOCSent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDDetailReport(FileName);
            }
        }

        protected void lnkBtnPrintDetailSettlementReportFormat_Click(object sender, EventArgs e)
        {
            if (rdoBtnSearchType.SelectedValue.Equals("1"))
            {
                string FileName = "DetailsReport-TransactionReceive" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDetailSettlementReportFormat(FileName);
            }
            else if (rdoBtnSearchType.SelectedValue.Equals("2"))
            {
                string FileName = "DetailsReport-ReturnSent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDetailSettlementReportFormat(FileName);
            }
            else if (rdoBtnSearchType.SelectedValue.Equals("3"))
            {
                string FileName = "DetailsReport-NOCSent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDFDetailSettlementReportFormat(FileName);
            }
        }

        private void PrintPDFDetailSettlementReportFormat(string FileName)
        {
            if (dtSearch.Rows.Count == 0)
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

            if (rdoBtnSearchType.SelectedValue.Equals("1"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                string settlementDateEnd = ddlistDayEnd.SelectedValue + "/" + ddlistMonthEnd.SelectedValue.PadLeft(2, '0') + "/" + ddlistYearEnd.SelectedValue;
                headertable.AddCell(new Phrase("Transaction Receive Report From: " + settlementDate + " To " + settlementDateEnd, headerFont));
            }
            else if (rdoBtnSearchType.SelectedValue.Equals("2"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                string settlementDateEnd = ddlistDayEnd.SelectedValue + "/" + ddlistMonthEnd.SelectedValue.PadLeft(2, '0') + "/" + ddlistYearEnd.SelectedValue;
                headertable.AddCell(new Phrase("Return Sent Report From: " + settlementDate + " To " + settlementDateEnd, headerFont));
            }
            else if (rdoBtnSearchType.SelectedValue.Equals("3"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                string settlementDateEnd = ddlistDayEnd.SelectedValue + "/" + ddlistMonthEnd.SelectedValue.PadLeft(2, '0') + "/" + ddlistYearEnd.SelectedValue;
                headertable.AddCell(new Phrase("NOC Sent Report From: " + settlementDate + " To " + settlementDateEnd, headerFont));
            }

            headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString(), headerFont));
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            //string SelectedBank = string.Empty;

            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            float[] headerwidths;
            int NumberOfPdfColumn = 0;
            if (SelectedBank.Equals("135"))
            {
                headerwidths = new float[] { 6, 10, 10, 4, 8, 4, 8, 6, 8, 8, 8, 8, 12, 8 };
                NumberOfPdfColumn = 14;
            }
            else
            {
                //headerwidths = new float[] { 11, 11, 8, 10, 4, 8, 6, 6, 6, 10, 8, 8, 8, 12, 8 };
                headerwidths = new float[] { 9, 9, 5, 10, 4, 8, 6, 6, 6, 10, 8, 8, 8, 8, 5 };
                NumberOfPdfColumn = 15;
            }

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
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            PdfPCell c0;
            if (SelectedBank.Equals("135"))
            {
                c0 = new PdfPCell(new iTextSharp.text.Phrase("Zone Name", fnt));
            }
            else
            {
                c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            }

            //PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            c0.BorderWidth = 0.5f;
            c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            c0.Padding = 4;

            datatable.AddCell(c0);

            if (SelectedBank.Equals("135"))
            {
                datatable.AddCell(new iTextSharp.text.Phrase("Bank Name", fnt)); //only for JANATA BANK
            }
            datatable.AddCell(new iTextSharp.text.Phrase("Branch Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("SECC", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Trace No.", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Tran. Code", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("DFIAccNo", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Bank RoutNo", fnt));

            datatable.AddCell(new iTextSharp.text.Phrase("CCY", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Session", fnt));

            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Receiver /Payer Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("C.Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Ent.Desc", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Reject Reason", fnt));


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                if (SelectedBank.Equals("135"))
                {
                    PdfPCell cZone = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["ZoneName"], fnt));
                    cZone.BorderWidth = 0.5f;
                    cZone.HorizontalAlignment = Cell.ALIGN_LEFT;
                    cZone.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    cZone.Padding = 4;
                    datatable.AddCell(cZone);
                }

                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["BankName"], fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["BranchName"], fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 4;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["SECC"], fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["TraceNumber"], fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);

                PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["TransactionCode"], fnt));
                c5.BorderWidth = 0.5f;
                c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c5.Padding = 4;
                datatable.AddCell(c5);

                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["DFIAccountNo"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 4;
                datatable.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["SendingBankRoutNo"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["Currency"], fnt));
                c8.BorderWidth = 0.5f;
                c8.HorizontalAlignment = Cell.ALIGN_LEFT;
                c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c8.Padding = 4;
                datatable.AddCell(c8);

                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["SessionID"], fnt));
                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_LEFT;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 4;
                datatable.AddCell(c9);

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((dtSearch.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["IdNumber"], fnt));
                c11.BorderWidth = 0.5f;
                c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c11.Padding = 4;
                datatable.AddCell(c11);

                PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["ReceiverName"], fnt));
                c12.BorderWidth = 0.5f;
                c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c12.Padding = 4;
                datatable.AddCell(c12);

                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["CompanyName"], fnt));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_LEFT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable.AddCell(c13);

                PdfPCell c14 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["EntryDesc"], fnt));
                c14.BorderWidth = 0.5f;
                c14.HorizontalAlignment = Cell.ALIGN_LEFT;
                c14.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c14.Padding = 4;
                datatable.AddCell(c14);

                PdfPCell c15 = new PdfPCell(new iTextSharp.text.Phrase((string)dtSearch.Rows[i]["RejectReason"], fnt));
                c15.BorderWidth = 0.5f;
                c15.HorizontalAlignment = Cell.ALIGN_LEFT;
                c15.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c15.Padding = 4;
                datatable.AddCell(c15);

            }

            //-------------TOTAL IN FOOTER --------------------
            if (SelectedBank.Equals("135"))
            {
                datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            }
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(dtSearch.Compute("COUNT(TraceNumber)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));

            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));


            //datatable.AddCell(new iTextSharp.text.Phrase(dtSearch.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtSearch.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            //-------------END TOTAL -------------------------



            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }
    }
}
