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
using EFTN.Utility;
using System.Data.SqlClient;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using EFTN.component;

namespace EFTN
{
    public partial class OutwardSearchForChecker : System.Web.UI.Page
    {
        private static DataTable myDTSearch = new DataTable();
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private static string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCurrencyTypeDropdownlist();
                BindSessionDropdownlist();
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0'); ;
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();

                ddlistDayEnd.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0'); ;
                ddlistMonthEnd.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYearEnd.SelectedValue = System.DateTime.Now.Year.ToString();

                BindData();
                BindBank();
                BindBranch();
                BindDepartments();

                //if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("225"))
                //{
                //    lblBeginDate.Text = "Entry Date Begin";
                //    lblEndDate.Text = "Entry Date End";
                //}
                //else
                //{
                //    lblBeginDate.Text = "Settlement Date Begin Date";
                //    lblEndDate.Text = "Settlement Date End Date";
                //}
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
            if (Request.Params["outwardSearchTraceNumber"] != null)
            {
                BindReturnData(Request.Params["outwardSearchTraceNumber"]);

                if (Session["paramRetTraceNumber"] != null)
                {
                    Session.Remove("paramRetTraceNumber");
                }
                Session.Add("paramRetTraceNumber", Request.Params["outwardSearchTraceNumber"]);

                SetSearchParameterFromSession();
            }

            if (Request.Params["outwardSearchReturnID"] != null)
            {
                SetSearchParameterFromSession();
                BindReturnData(Session["paramRetTraceNumber"].ToString());
                BindDishonorRecordByReturnID(Request.Params["outwardSearchReturnID"].ToString());
            }

            if (Request.Params["outwardSearchNOCID"] != null)
            {
                BindNOCData(Session["paramRetTraceNumber"].ToString());
                BindRNOCRecordByNOCID(Request.Params["outwardSearchNOCID"].ToString());
                SetSearchParameterFromSession();
                if (chkBoxArchive.Checked)
                {
                    isFromArchive = true;
                }
                BindSearchResult(isFromArchive);
            }
        }

        private void BindRNOCRecordByNOCID(string NOCID)
        {
            EFTN.component.OutwardSearchDB outwardSearchDB = new EFTN.component.OutwardSearchDB();
            Guid gNOCID = new Guid(NOCID);
            DataTable dtRNOCData = outwardSearchDB.GetOutwardSearchRNOCByNOCID(gNOCID);
            //SqlDataReader sqlDRRNOCData = outwardSearchDB.GetOutwardSearchRNOCByNOCID(gNOCID);

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
            dtRNOCData.Dispose();
        }

        private void BindDishonorRecordByReturnID(string ReturnID)
        {
            EFTN.component.OutwardSearchDB outwardSearchDB = new EFTN.component.OutwardSearchDB();
            Guid gReturnID = new Guid(ReturnID);
            //SqlDataReader sqlDRDishonorData = outwardSearchDB.GetOutwardSearchDishonorRecordByReturnID(gReturnID);
            DataTable dtDishonorData = outwardSearchDB.GetOutwardSearchDishonorRecordByReturnID(gReturnID);

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
            dtDishonorData.Dispose();
        }

        private void BindReturnData(string retTraceNumber)
        {
            EFTN.component.OutwardSearchDB outwardSearchDB = new EFTN.component.OutwardSearchDB();
            DataTable dtReturnData = outwardSearchDB.GetOutwardSearchReturnRecordByTraceNumber(retTraceNumber);
            //SqlDataReader sqlDRReturnData = outwardSearchDB.GetOutwardSearchReturnRecordByTraceNumber(retTraceNumber);
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
                BindNOCData(retTraceNumber);
                lblReturnMsg.Visible = true;
                dtgReturnRecord.Visible = false;
            }

            dtReturnData.Dispose();
            SetSearchParameterFromSession();
            BindSearchResult(isFromArchive);
        }

        private void BindNOCData(string nOCTraceNumber)
        {
            EFTN.component.OutwardSearchDB outwardSearchDB = new EFTN.component.OutwardSearchDB();
            DataTable dtRNOCData = outwardSearchDB.GetOutwardSearchNOCByTraceNumber(nOCTraceNumber);
            //SqlDataReader sqlDRNOCData = outwardSearchDB.GetOutwardSearchNOCByTraceNumber(nOCTraceNumber);

            if (dtRNOCData.Rows.Count > 0)
            {
                lblNOCMsg.Visible = false;
                dtgNOCRecord.Visible = true;
                dtgNOCRecord.DataSource = dtRNOCData;
                dtgNOCRecord.DataBind();
            }
            else
            {
                lblNOCMsg.Visible = true;
                dtgNOCRecord.Visible = false;
            }
            dtRNOCData.Dispose();
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
            try
            {
                DateTime dateTimeStart = new DateTime(ParseData.StringToInt(ddlistYear.SelectedValue), ParseData.StringToInt(ddlistMonth.SelectedValue), ParseData.StringToInt(ddlistDay.SelectedValue));
                DateTime dateTimeEnd = new DateTime(ParseData.StringToInt(ddlistYearEnd.SelectedValue), ParseData.StringToInt(ddlistMonthEnd.SelectedValue), ParseData.StringToInt(ddlistDayEnd.SelectedValue));
                double dateDiff = (dateTimeEnd - dateTimeStart).TotalDays;

                string BranchWise = ConfigurationManager.AppSettings["BranchWise"];

                if (BranchWise.Equals("0"))
                {
                    if (dateDiff > 30)
                    {
                        lblSearchMsg.Text = "You are allowed to take maximum 1 month data at a time";
                        return;
                    }
                }

                if (chkBoxArchive.Checked)
                {
                    isFromArchive = true;
                }

            }
            catch
            {
                lblSearchMsg.Text = "Insert Valid Date";
                return;
            }

            BindSearchResult(isFromArchive);
            dtgReturnRecord.Visible = false;
            dtgNOCRecord.Visible = false;
            lblNOCMsg.Visible = false;
            lblReturnMsg.Visible = false;
            AddSearchParameterInSession();
        }

        private void AddSearchParameterInSession()
        {
            RemoveSearchParameterFromeSession();
            Session.Add("outwardSearchParamDay", ddlistDay.SelectedValue);
            Session.Add("outwardSearchParamMonth", ddlistMonth.SelectedValue);
            Session.Add("outwardSearchParamYear", ddlistYear.SelectedValue);
            Session.Add("outwardSearchParamEndDay", ddlistDayEnd.SelectedValue);
            Session.Add("outwardSearchParamEndMonth", ddlistMonthEnd.SelectedValue);
            Session.Add("outwardSearchParamEndYear", ddlistYearEnd.SelectedValue);
            Session.Add("outwardSearchParamReceivingBankRoutingNo", txtReceivingBankRoutingNo.Text.Trim());
            Session.Add("outwardSearchParamDFIAccountNo", txtDFIAccountNo.Text.Trim());
            Session.Add("outwardSearchParamReceiverName", txtReceiverName.Text.Trim());
            Session.Add("outwardSearchParamAmount", txtAmount.Text.Trim());
            Session.Add("outwardSearchParamMaxAmount", txtMaxAmount.Text.Trim());
            Session.Add("outwardSearchParamAccountNo", txtAccountNo.Text.Trim());
            Session.Add("outwardSearchParamIDNumber", txtIdNumber.Text.Trim());
            Session.Add("outwardSearchParamCompanyName", txtCompanyName.Text.Trim());
            Session.Add("outwardSearchParamBankName", txtBankName.Text.Trim());
            Session.Add("outwardSearchParamTraceNumber", txtTraceNumber.Text.Trim());
            Session.Add("outwardSearchParamBatchNumber", txtTraceNumber.Text.Trim());
            Session.Add("outwardSearchParamRdoBtnSearchType", rdoBtnSearchType.SelectedValue.Trim());
            Session.Add("outwardSearchParamDDListTransactionType", ddListTransactionType.SelectedValue.Trim());
            Session.Add("outwardSearchParamDDListDeptID", ddListDeptID.SelectedValue.Trim());
            Session.Add("outwardSearchParamPaymentInfo", txtPaymentInfo.Text.Trim());
            Session.Add("outwardSearchParamFromArchive", chkBoxArchive.Checked.ToString());
        }

        private void RemoveSearchParameterFromeSession()
        {
            if (Session["outwardSearchParamDay"] != null)
            {
                Session.Remove("outwardSearchParamDay");
            }

            if (Session["outwardSearchParamMonth"] != null)
            {
                Session.Remove("outwardSearchParamMonth");
            }

            if (Session["outwardSearchParamYear"] != null)
            {
                Session.Remove("outwardSearchParamYear");
            }

            if (Session["outwardSearchParamEndDay"] != null)
            {
                Session.Remove("outwardSearchParamEndDay");
            }

            if (Session["outwardSearchParamEndMonth"] != null)
            {
                Session.Remove("outwardSearchParamEndMonth");
            }

            if (Session["outwardSearchParamEndYear"] != null)
            {
                Session.Remove("outwardSearchParamEndYear");
            }

            if (Session["outwardSearchParamReceivingBankRoutingNo"] != null)
            {
                Session.Remove("outwardSearchParamReceivingBankRoutingNo");
            }

            if (Session["outwardSearchParamDFIAccountNo"] != null)
            {
                Session.Remove("outwardSearchParamDFIAccountNo");
            }

            if (Session["outwardSearchParamReceiverName"] != null)
            {
                Session.Remove("outwardSearchParamReceiverName");
            }

            if (Session["outwardSearchParamAmount"] != null)
            {
                Session.Remove("outwardSearchParamAmount");
            }

            if (Session["outwardSearchParamMaxAmount"] != null)
            {
                Session.Remove("outwardSearchParamMaxAmount");
            }

            if (Session["outwardSearchParamAccountNo"] != null)
            {
                Session.Remove("outwardSearchParamAccountNo");
            }

            if (Session["outwardSearchParamIDNumber"] != null)
            {
                Session.Remove("outwardSearchParamIDNumber");
            }

            if (Session["outwardSearchParamCompanyName"] != null)
            {
                Session.Remove("outwardSearchParamCompanyName");
            }

            if (Session["outwardSearchParamBankName"] != null)
            {
                Session.Remove("outwardSearchParamBankName");
            }

            if (Session["outwardSearchParamTraceNumber"] != null)
            {
                Session.Remove("outwardSearchParamTraceNumber");
            }
            if (Session["outwardSearchParamBatchNumber"] != null)
            {
                Session.Remove("outwardSearchParamBatchNumber");
            }
            if (Session["outwardSearchParamRdoBtnSearchType"] != null)
            {
                Session.Remove("outwardSearchParamRdoBtnSearchType");
            }
            if (Session["outwardSearchParamDDListTransactionType"] != null)
            {
                Session.Remove("outwardSearchParamDDListTransactionType");
            }
            if (Session["outwardSearchParamDDListDeptID"] != null)
            {
                Session.Remove("outwardSearchParamDDListDeptID");
            }
            if (Session["outwardSearchParamPaymentInfo"] != null)
            {
                Session.Remove("outwardSearchParamPaymentInfo");
            }
            if (Session["outwardSearchParamFromArchive"] != null)
            {
                Session.Remove("outwardSearchParamFromArchive");
            }
        }

        private void SetSearchParameterFromSession()
        {
            if (Session["outwardSearchParamDay"] != null)
            {
                ddlistDay.SelectedValue = Session["outwardSearchParamDay"].ToString();
            }

            if (Session["outwardSearchParamMonth"] != null)
            {
                ddlistMonth.SelectedValue = Session["outwardSearchParamMonth"].ToString();
            }

            if (Session["outwardSearchParamYear"] != null)
            {
                ddlistYear.SelectedValue = Session["outwardSearchParamYear"].ToString();
            }

            if (Session["outwardSearchParamEndDay"] != null)
            {
                ddlistDayEnd.SelectedValue = Session["outwardSearchParamEndDay"].ToString();
            }

            if (Session["outwardSearchParamEndMonth"] != null)
            {
                ddlistMonthEnd.SelectedValue = Session["outwardSearchParamEndMonth"].ToString();
            }

            if (Session["outwardSearchParamEndYear"] != null)
            {
                ddlistYearEnd.SelectedValue = Session["outwardSearchParamEndYear"].ToString();
            }

            if (Session["outwardSearchParamReceivingBankRoutingNo"] != null)
            {
                txtReceivingBankRoutingNo.Text = Session["outwardSearchParamReceivingBankRoutingNo"].ToString();
            }

            if (Session["outwardSearchParamDFIAccountNo"] != null)
            {
                txtDFIAccountNo.Text = Session["outwardSearchParamDFIAccountNo"].ToString();
            }

            if (Session["outwardSearchParamReceiverName"] != null)
            {
                txtReceiverName.Text = Session["outwardSearchParamReceiverName"].ToString();
            }

            if (Session["outwardSearchParamAmount"] != null)
            {
                txtAmount.Text = Session["outwardSearchParamAmount"].ToString();
            }

            if (Session["outwardSearchParamMaxAmount"] != null)
            {
                txtAmount.Text = Session["outwardSearchParamMaxAmount"].ToString();
            }

            if (Session["outwardSearchParamAccountNo"] != null)
            {
                txtAccountNo.Text = Session["outwardSearchParamAccountNo"].ToString();
            }

            if (Session["outwardSearchParamIDNumber"] != null)
            {
                txtIdNumber.Text = Session["outwardSearchParamIDNumber"].ToString();
            }
            if (Session["outwardSearchParamCompanyName"] != null)
            {
                txtCompanyName.Text = Session["outwardSearchParamCompanyName"].ToString();
            }
            if (Session["outwardSearchParamBankName"] != null)
            {
                txtBankName.Text = Session["outwardSearchParamBankName"].ToString();
            }

            if (Session["outwardSearchParamTraceNumber"] != null)
            {
                txtTraceNumber.Text = Session["outwardSearchParamTraceNumber"].ToString();
            }

            if (Session["outwardSearchParamBatchNumber"] != null)
            {
                txtBatchNumber.Text = Session["outwardSearchParamBatchNumber"].ToString();
            }

            if (Session["outwardSearchParamRdoBtnSearchType"] != null)
            {
                rdoBtnSearchType.SelectedValue = Session["outwardSearchParamRdoBtnSearchType"].ToString();
            }

            if (Session["outwardSearchParamDDListTransactionType"] != null)
            {
                ddListTransactionType.SelectedValue = Session["outwardSearchParamDDListTransactionType"].ToString();
            }

            if (Session["outwardSearchParamDDListDeptID"] != null)
            {
                ddListDeptID.SelectedValue = Session["outwardSearchParamDDListDeptID"].ToString();
            }

            if (Session["outwardSearchParamPaymentInfo"] != null)
            {
                txtPaymentInfo.Text = Session["outwardSearchParamPaymentInfo"].ToString();
            }

            if (Session["outwardSearchParamFromArchive"] != null)
            {
                chkBoxArchive.Checked = bool.Parse(Session["outwardSearchParamFromArchive"].ToString());
            }
        }

        public void BindSearchResult(bool isFromArchive)
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

            EFTN.component.OutwardSearchDB outwardSearchDB = new EFTN.component.OutwardSearchDB();

            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            myDTSearch.Clear();//did it for test
            myDTSearch.Dispose();

            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            string BranchWise = ConfigurationManager.AppSettings["BranchWise"];

            if (BranchWise.Equals("1"))
            {

                myDTSearch = outwardSearchDB.GetOutwardSearchResultForBranchWise(beginDate,
                                                       endDate,
                                                       txtReceivingBankRoutingNo.Text.Trim(),
                                                       txtDFIAccountNo.Text.Trim(),
                                                       txtReceiverName.Text.Trim(),
                                                       ParseData.StringToDouble(txtAmount.Text.Trim()),
                                                       ParseData.StringToDouble(txtMaxAmount.Text.Trim()),
                                                       txtAccountNo.Text.Trim(),
                                                       txtIdNumber.Text.Trim(),
                                                       txtCompanyName.Text.Trim(),
                                                       txtBankName.Text.Trim(),
                                                       txtTraceNumber.Text.Trim(),
                                                       txtBatchNumber.Text.Trim(),
                                                       rdoBtnSearchType.SelectedValue.Trim(),
                                                       ParseData.StringToInt(ddListTransactionType.SelectedValue),
                                                       txtPaymentInfo.Text.Trim(),
                                                       ParseData.StringToInt(ddListDeptID.SelectedValue),
                                                       bankCode
                                                       , UserID
                                                       , CurrencyDdList.SelectedValue
                                                       , SessionDdList.SelectedValue);
            }
            else
            {
                myDTSearch = outwardSearchDB.GetOutwardSearchResult(beginDate,
                                                       endDate,
                                                       txtReceivingBankRoutingNo.Text.Trim(),
                                                       txtDFIAccountNo.Text.Trim(),
                                                       txtReceiverName.Text.Trim(),
                                                       ParseData.StringToDouble(txtAmount.Text.Trim()),
                                                       ParseData.StringToDouble(txtMaxAmount.Text.Trim()),
                                                       txtAccountNo.Text.Trim(),
                                                       txtIdNumber.Text.Trim(),
                                                       txtCompanyName.Text.Trim(),
                                                       txtBankName.Text.Trim(),
                                                       txtTraceNumber.Text.Trim(),
                                                       txtBatchNumber.Text.Trim(),
                                                       rdoBtnSearchType.SelectedValue.Trim(),
                                                       ParseData.StringToInt(ddListTransactionType.SelectedValue),
                                                       txtPaymentInfo.Text.Trim(),
                                                       ParseData.StringToInt(ddListDeptID.SelectedValue),
                                                       bankCode
                                                       , UserID
                                                       , isFromArchive
                                                       , CurrencyDdList.SelectedValue
                                                       , SessionDdList.SelectedValue);
            }
            if (myDTSearch.Rows.Count > 0)
            {
                lblMsg.Visible = false;
                dtgOutwardSearch.Visible = true;
                dtgOutwardSearch.CurrentPageIndex = 0;
                dtgOutwardSearch.DataSource = myDTSearch;
                dtgOutwardSearch.DataBind();
            }
            else
            {
                lblMsg.Visible = true;
                lblNOCMsg.Visible = false;
                lblReturnMsg.Visible = false;
                dtgOutwardSearch.Visible = false;
                dtgReturnRecord.Visible = false;
                dtgNOCRecord.Visible = false;
            }
        }

        private void BindDepartments()
        {
            FloraSoft.DepartmentsDB dbDept = new FloraSoft.DepartmentsDB();
            ddListDeptID.DataSource = dbDept.GetDepartments();
            ddListDeptID.DataTextField = "DepartmentName";
            ddListDeptID.DataValueField = "DepartmentID";
            ddListDeptID.SelectedValue = "0";
            ddListDeptID.DataBind();
            ddListDeptID.Items.Add(new System.Web.UI.WebControls.ListItem("All", "-1"));

            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            string DepartmentID = Request.Cookies["DepartmentID"].Value;

            if (bankCode.Equals(OriginalBankCode.CBL) && !DepartmentID.Equals("0"))
            {
                ddListDeptID.SelectedValue = DepartmentID;
                ddListDeptID.Enabled = false;
            }
            else
            {
                if (Session["outwardSearchParamDDListDeptID"] != null)
                {
                    ddListDeptID.SelectedValue = Session["outwardSearchParamDDListDeptID"].ToString();
                }
                else
                {
                    ddListDeptID.SelectedIndex = ddListDeptID.Items.Count - 1;
                }
            }
        }

        protected void dtgOutwardSearch_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgOutwardSearch.CurrentPageIndex = e.NewPageIndex;
            dtgOutwardSearch.DataSource = myDTSearch;
            dtgOutwardSearch.DataBind();
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
            for (int i = 0; i < dtgOutwardSearch.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgOutwardSearch.Items[i].FindControl("chkBoxTransSent");
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
            str = str + "It is understood that this remittance is to be affected at the sole risk of the customer and that the bank shall not be held reponsible for any loss";
            str = str + "\nconsequent upon circumstances arising over which it has no control for the solvency of agents employed.";
            str = str + "\nThis is a computer generated advice.  It requires no signature";
            //str = str + ConfigurationManager.AppSettings["OriginBankName"] + " All Rights Reserved";

            string strAdviceBody = "Dear Customer, \n\nWe advise having made a payment from your account, details of which are as follows:\n\n";
            string strAdviceEnd = "We thank you for banking with " + ConfigurationManager.AppSettings["OriginBankName"] + " and are pleased to be of service to you.";

            HeaderFooter footer = new HeaderFooter(new Phrase(str, fnt), false);
            footer.Alignment = Element.ALIGN_CENTER;

            document.Footer = footer;

            document.Open();

            string prevAccountNumber = string.Empty;
            string currnectAccountNumber = string.Empty;
            //datatable 5
            iTextSharp.text.pdf.PdfPTable datatable5 = new iTextSharp.text.pdf.PdfPTable(1);
            datatable5.DefaultCell.Padding = 3;
            datatable5.DefaultCell.Border = 2;
            datatable5.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            //float[] headerwidths2 = { 15,35,20,15,15};
            datatable5.DefaultCell.BorderWidth = 0;
            datatable5.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            datatable5.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

            datatable5.AddCell(new iTextSharp.text.Phrase(strAdviceEnd, fntlrg));
            int adviceCounter = 0;

            for (int i = 0; i < dtgOutwardSearch.Items.Count; i++)
            {

                CheckBox cbx = (CheckBox)dtgOutwardSearch.Items[i].FindControl("chkBoxTransSent");

                if (cbx.Checked)
                {
                    iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
                    jpeg.Alignment = Element.ALIGN_RIGHT;


                    /////////////////////////////////////////////////////////
                    iTextSharp.text.pdf.PdfPTable datatableADDRESS = new iTextSharp.text.pdf.PdfPTable(4);
                    datatableADDRESS.DefaultCell.Padding = 4;
                    datatableADDRESS.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    float[] headerwidthsADDRESS = { 25, 7, 53, 15 };
                    datatableADDRESS.SetWidths(headerwidthsADDRESS);
                    datatableADDRESS.WidthPercentage = 99;

                    // iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                    datatableADDRESS.DefaultCell.BorderWidth = 0;
                    datatableADDRESS.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    datatableADDRESS.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);


                    /////////////////////////////////////////////////

                    iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(16);
                    datatable.DefaultCell.Padding = 3;
                    datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    float[] headerwidths = { 4, 5, 5, 8, 10, 10, 6, 8, 8, 6, 8, 2, 4, 8, 4, 4 };
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
                    iTextSharp.text.pdf.PdfPTable datatable2 = new iTextSharp.text.pdf.PdfPTable(16);
                    datatable2.DefaultCell.Padding = 3;
                    datatable2.DefaultCell.Border = 2;
                    datatable2.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    //float[] headerwidths2 = { 15,35,20,15,15};
                    float[] headerwidths2 = { 4, 5, 5, 8, 10, 10, 6, 8, 8, 6, 8, 2, 4, 8, 4, 4 };
                    datatable2.DefaultCell.BorderWidth = 2;
                    datatable2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    datatable2.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                    //datatable 3
                    iTextSharp.text.pdf.PdfPTable datatable3 = new iTextSharp.text.pdf.PdfPTable(16);
                    datatable3.DefaultCell.Padding = 3;
                    datatable3.DefaultCell.Border = 2;
                    datatable3.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    //float[] headerwidths2 = { 15,35,20,15,15};
                    datatable3.DefaultCell.BorderWidth = 1;
                    datatable3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    datatable3.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                    //datatable 4
                    iTextSharp.text.pdf.PdfPTable datatable4 = new iTextSharp.text.pdf.PdfPTable(1);
                    datatable4.DefaultCell.Padding = 3;
                    datatable4.DefaultCell.Border = 2;
                    datatable4.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    //float[] headerwidths2 = { 15,35,20,15,15};
                    datatable4.DefaultCell.BorderWidth = 1;
                    datatable4.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    datatable4.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                    datatable4.AddCell(new iTextSharp.text.Phrase(strAdviceBody, fntlrg));



                    string edrId = dtgOutwardSearch.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    //System.Data.SqlClient.SqlDataReader sqlDRTS;
                    EFTN.component.OutwardSearchDB db = new EFTN.component.OutwardSearchDB();

                    DataTable dtTS = new DataTable();
                    dtTS = db.GetTransactionSentAdviceReport(EDRID);
                    for (int rowNum = 0; rowNum < dtTS.Rows.Count; rowNum++)
                    {
                        currnectAccountNumber = dtTS.Rows[rowNum]["AccountNo"].ToString();

                        if (currnectAccountNumber.Equals(prevAccountNumber))
                        {
                            adviceCounter++;
                        }
                        else
                        {
                            adviceCounter = 1;
                        }

                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fntlrg));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["TITLE"].ToString(), fntlrg));
                        string batchType = "EFT Outward " + dtTS.Rows[rowNum]["BatchType"].ToString() + " Advice";
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase(batchType, fntlrg));


                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fntlrg));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["ADDRESS"].ToString(), fntlrg));
                        datatableADDRESS.AddCell(logo);

                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                        datatable.AddCell(new iTextSharp.text.Phrase("Date :", fntbld));
                        datatable.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["EffectiveEntryDate"].ToString(), fntbld));
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

                        datatable.AddCell(new iTextSharp.text.Phrase("Ref. No.: ", fntbld));
                        datatable.AddCell(new iTextSharp.text.Phrase("EFTEX" + dtTS.Rows[rowNum]["TraceNumber"].ToString(), fntbld));
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
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));
                        datatable.AddCell(new iTextSharp.text.Phrase("", fnt));

                        datatable2.AddCell(new iTextSharp.text.Phrase("Sl.", fnt));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Beneficiary Bank", fnt));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Beneficiary Branch", fnt));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Receiving Bank Routing No.", fnt));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Beneficiary A/C No.", fnt));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Beneficiary Name", fnt));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Receiver ID", fnt));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Debit A/C", fnt)); //sender
                        datatable2.AddCell(new iTextSharp.text.Phrase("Name Of Originator", fnt));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Reason", fnt)); //sender
                        datatable2.AddCell(new iTextSharp.text.Phrase("Value Date", fnt));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Payment Info", fnt));

                        datatable2.AddCell(new iTextSharp.text.Phrase("RejectReason", fnt));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Charges", fnt));
                        datatable2.AddCell(new iTextSharp.text.Phrase("Charge VAT", fnt));

                        double TotalChargeAmount = ParseData.StringToDouble(dtTS.Rows[rowNum]["BBCharge"].ToString())
                                                    + ParseData.StringToDouble(dtTS.Rows[rowNum]["BankCharge"].ToString());

                        double ChargeVATAmount = ParseData.StringToDouble(dtTS.Rows[rowNum]["BBChargeVAT"].ToString())
                                                    + ParseData.StringToDouble(dtTS.Rows[rowNum]["BankChargeVAT"].ToString());


                        //.ToString("N", System.Globalization.CultureInfo.InvariantCulture)

                        datatable3.AddCell(new iTextSharp.text.Phrase(adviceCounter.ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["BankName"].ToString(), fnt));
                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["BranchName"].ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["ReceivingBankRoutingNo"].ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["DFIAccountNo"].ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["ReceiverName"].ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["IdNumber"].ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["AccountNo"].ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["CompanyName"].ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["EntryDesc"].ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["EffectiveEntryDate"].ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["PaymentInfo"].ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["RejectReason"].ToString(), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtTS.Rows[rowNum]["Amount"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));

                        datatable3.AddCell(new iTextSharp.text.Phrase(TotalChargeAmount.ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                        datatable3.AddCell(new iTextSharp.text.Phrase(ChargeVATAmount.ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                    }
                    //sqlDRTS.Close();
                    //sqlDRTS.Dispose();

                    //document.Add(datatable);
                    //document.Add(datatable2);
                    //document.NewPage();
                    dtTS.Dispose();

                    if (!currnectAccountNumber.Equals(prevAccountNumber) && !prevAccountNumber.Equals(string.Empty))
                    {
                        document.Add(datatable5);
                        document.NewPage();
                    }
                    if (!currnectAccountNumber.Equals(prevAccountNumber))
                    {
                        document.Add(datatableADDRESS);
                        document.Add(datatable);
                        document.Add(datatable4);
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
            document.Add(datatable5);
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
            str = str + "It is understood that this remittance is to be affected at the sole risk of the customer and that the bank shall not be held reponsible for any loss";
            str = str + "\nconsequent upon circumstances arising over which it has no control for the solvency of agents employed.";
            str = str + "\nThis is a computer generated advice.  It requires no signature";
            //str = str + ConfigurationManager.AppSettings["OriginBankName"] + " All Rights Reserved";

            string strAdviceBody = "Dear Customer, \n\nWe advise having made a payment from your account, details of which are as follows:\n\n";
            string strAdviceEnd = "We thank you for banking with " + ConfigurationManager.AppSettings["OriginBankName"] + " and are pleased to be of service to you.";

            HeaderFooter footer = new HeaderFooter(new Phrase(str, fnt), false);
            footer.Alignment = Element.ALIGN_CENTER;

            document.Footer = footer;

            document.Open();

            string prevAccountNumber = string.Empty;
            string currnectAccountNumber = string.Empty;
            //datatable 5
            iTextSharp.text.pdf.PdfPTable datatable5 = new iTextSharp.text.pdf.PdfPTable(1);
            datatable5.DefaultCell.Padding = 3;
            datatable5.DefaultCell.Border = 2;
            datatable5.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            //float[] headerwidths2 = { 15,35,20,15,15};
            datatable5.DefaultCell.BorderWidth = 0;
            datatable5.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            datatable5.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

            datatable5.AddCell(new iTextSharp.text.Phrase(strAdviceEnd, fntlrg));
            int adviceCounter = 0;

            for (int i = 0; i < myDTSearch.Rows.Count; i++)
            {
                iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
                jpeg.Alignment = Element.ALIGN_RIGHT;


                /////////////////////////////////////////////////////////
                iTextSharp.text.pdf.PdfPTable datatableADDRESS = new iTextSharp.text.pdf.PdfPTable(4);
                datatableADDRESS.DefaultCell.Padding = 4;
                datatableADDRESS.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                float[] headerwidthsADDRESS = { 25, 7, 53, 15 };
                datatableADDRESS.SetWidths(headerwidthsADDRESS);
                datatableADDRESS.WidthPercentage = 99;

                // iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                datatableADDRESS.DefaultCell.BorderWidth = 0;
                datatableADDRESS.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                datatableADDRESS.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);


                /////////////////////////////////////////////////

                iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(15);
                datatable.DefaultCell.Padding = 3;
                datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                float[] headerwidths = { 4, 5, 5, 8, 10, 10, 6, 8, 8, 6, 8, 2, 8, 8, 4 };
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
                iTextSharp.text.pdf.PdfPTable datatable2 = new iTextSharp.text.pdf.PdfPTable(15);
                datatable2.DefaultCell.Padding = 3;
                datatable2.DefaultCell.Border = 2;
                datatable2.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //float[] headerwidths2 = { 15,35,20,15,15};
                float[] headerwidths2 = { 4, 5, 5, 8, 10, 10, 6, 8, 8, 6, 8, 2, 8, 8, 4 };
                datatable2.DefaultCell.BorderWidth = 2;
                datatable2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                datatable2.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                //datatable 3
                iTextSharp.text.pdf.PdfPTable datatable3 = new iTextSharp.text.pdf.PdfPTable(15);
                datatable3.DefaultCell.Padding = 3;
                datatable3.DefaultCell.Border = 2;
                datatable3.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //float[] headerwidths2 = { 15,35,20,15,15};
                datatable3.DefaultCell.BorderWidth = 1;
                datatable3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                datatable3.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                //datatable 4
                iTextSharp.text.pdf.PdfPTable datatable4 = new iTextSharp.text.pdf.PdfPTable(1);
                datatable4.DefaultCell.Padding = 3;
                datatable4.DefaultCell.Border = 2;
                datatable4.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                //float[] headerwidths2 = { 15,35,20,15,15};
                datatable4.DefaultCell.BorderWidth = 1;
                datatable4.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                datatable4.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);

                datatable4.AddCell(new iTextSharp.text.Phrase(strAdviceBody, fntlrg));



                string edrId = myDTSearch.Rows[i]["EDRID"].ToString();
                Guid EDRID = new Guid(edrId);

                //System.Data.SqlClient.SqlDataReader sqlDRTS;
                EFTN.component.OutwardSearchDB db = new EFTN.component.OutwardSearchDB();

                DataTable dtTS = new DataTable();
                dtTS = db.GetTransactionSentAdviceReport(EDRID);
                for (int rowNum = 0; rowNum < dtTS.Rows.Count; rowNum++)
                {
                    currnectAccountNumber = dtTS.Rows[rowNum]["AccountNo"].ToString();

                    if (currnectAccountNumber.Equals(prevAccountNumber))
                    {
                        adviceCounter++;
                    }
                    else
                    {
                        adviceCounter = 1;
                    }

                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fntlrg));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["TITLE"].ToString(), fntlrg));
                    string batchType = "EFT Outward " + dtTS.Rows[rowNum]["BatchType"].ToString() + " Advice";
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase(batchType, fntlrg));

                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fntlrg));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["ADDRESS"].ToString(), fntlrg));
                    datatableADDRESS.AddCell(logo);

                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));
                    datatableADDRESS.AddCell(new iTextSharp.text.Phrase("", fnt));

                    datatable.AddCell(new iTextSharp.text.Phrase("Date :", fntbld));
                    datatable.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["EffectiveEntryDate"].ToString(), fntbld));
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

                    datatable.AddCell(new iTextSharp.text.Phrase("Ref. No.: ", fntbld));
                    datatable.AddCell(new iTextSharp.text.Phrase("EFTEX" + dtTS.Rows[rowNum]["TraceNumber"].ToString(), fntbld));
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

                    datatable2.AddCell(new iTextSharp.text.Phrase("Sl.", fnt));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Beneficiary Bank", fnt));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Beneficiary Branch", fnt));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Receiving Bank Routing No.", fnt));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Beneficiary A/C No.", fnt));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Beneficiary Name", fnt));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Receiver ID", fnt));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Debit A/C", fnt)); //sender
                    datatable2.AddCell(new iTextSharp.text.Phrase("Name Of Originator", fnt));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Reason", fnt)); //sender
                    datatable2.AddCell(new iTextSharp.text.Phrase("Value Date", fnt));
                    datatable2.AddCell(new iTextSharp.text.Phrase("RejectReason", fnt));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Charges", fnt));
                    datatable2.AddCell(new iTextSharp.text.Phrase("Charge VAT", fnt));

                    double TotalChargeAmount = ParseData.StringToDouble(dtTS.Rows[rowNum]["BBCharge"].ToString())
                                                + ParseData.StringToDouble(dtTS.Rows[rowNum]["BankCharge"].ToString());

                    double ChargeVATAmount = ParseData.StringToDouble(dtTS.Rows[rowNum]["BBChargeVAT"].ToString())
                                                + ParseData.StringToDouble(dtTS.Rows[rowNum]["BankChargeVAT"].ToString());




                    datatable3.AddCell(new iTextSharp.text.Phrase(adviceCounter.ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["BankName"].ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["BranchName"].ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["ReceivingBankRoutingNo"].ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["DFIAccountNo"].ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["ReceiverName"].ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["IdNumber"].ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["AccountNo"].ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["CompanyName"].ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["EntryDesc"].ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["EffectiveEntryDate"].ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(dtTS.Rows[rowNum]["RejectReason"].ToString(), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(dtTS.Rows[rowNum]["Amount"].ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));

                    datatable3.AddCell(new iTextSharp.text.Phrase(TotalChargeAmount.ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                    datatable3.AddCell(new iTextSharp.text.Phrase(ChargeVATAmount.ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));
                }
                //sqlDRTS.Close();
                //sqlDRTS.Dispose();

                //document.Add(datatable);
                //document.Add(datatable2);
                //document.NewPage();
                dtTS.Dispose();

                if (!currnectAccountNumber.Equals(prevAccountNumber) && !prevAccountNumber.Equals(string.Empty))
                {
                    document.Add(datatable5);
                    document.NewPage();
                }
                if (!currnectAccountNumber.Equals(prevAccountNumber))
                {
                    document.Add(datatableADDRESS);
                    document.Add(datatable);
                    document.Add(datatable4);
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
            document.Add(datatable5);
            //////////////////////////////////////////////
            document.Close();
            Response.End();
        }

        protected void cbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedCbx = cbxSelectAll.Checked;
            for (int i = 0; i < dtgOutwardSearch.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgOutwardSearch.Items[i].FindControl("chkBoxTransSent");
                cbx.Checked = checkedCbx;
            }
        }

        protected void btnExpotToCSV_Click(object sender, EventArgs e)
        {
            //DataTable dt = GetData();    

            /***    Commented out for exporting CSV directly from datagrid _ 07-01-2018     ***/
            //GenerateCSVFromDataReader();

            /***    Added new function to export CSV directly from datagrid _ 07-01-2018    ***/
            ExportCSVFromGrid();

            /***    Previousely commented out    ***/
            //if (myDTSearch.Rows.Count > 0)
            //{
            //    GenerateCSVFromDataTable();
            //}
        }

        protected void ExportCSVFromGrid()
        {
            DataTable searchResultBox = new DataTable();
            DataTable refreshedSearch = new DataTable();
            if (myDTSearch.Rows.Count > 0)
            {
                searchResultBox = myDTSearch.Copy();
            }
            if (searchResultBox.Rows.Count > 0)
            {
                refreshedSearch = searchResultBox.Copy();
                if (cbxSelectAll.Checked)
                {
                    ExportCSVFromDataTable(refreshedSearch);
                }
                else
                {
                    try
                    {
                        foreach (DataGridItem item in dtgOutwardSearch.Items)
                        {
                            Guid edrId = (Guid)dtgOutwardSearch.DataKeys[item.ItemIndex];
                            CheckBox cbx = (CheckBox)item.FindControl("chkBoxTransSent");
                            int gridRowIndex = item.ItemIndex;
                            if (!cbx.Checked)
                            {
                                DataRow[] rows = refreshedSearch.Select("EDRID ='" + edrId + "'");
                                refreshedSearch.Rows.Remove(rows[0]);
                            }
                        }
                    }
                    catch
                    {
                    }
                    if (refreshedSearch.Rows.Count > 0)
                    {
                        ExportCSVFromDataTable(refreshedSearch);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select transactions to export CSV!')", true);
                        return;
                    }
                }
            }
            dtgOutwardSearch.DataSource = null;
            dtgOutwardSearch.DataBind();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Transactions not found to export CSV! Please check search result.')", true);
            return;
        }

        protected void ExportCSVFromDataTable(DataTable dataToExport)
        {
            if (dataToExport.Rows.Count > 0)
            {
                string xlsFileName = "Outward" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                string attachment = "attachment; filename=" + xlsFileName + ".csv";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.csv";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                // Create the CSV file to which grid data will be exported.                
                int iColCount = dataToExport.Columns.Count;

                // First we will write the headers.
                for (int i = 1; i < iColCount; i++)
                {
                    sw.Write("\"");
                    sw.Write(dataToExport.Columns[i]);
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
                foreach (DataRow dr in dataToExport.Rows)
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
                Response.Flush();
            }
        }


        private void GenerateCSVFromDataReader()
        {
            bool isFromArchive = false;
            if (chkBoxArchive.Checked)
            {
                isFromArchive = true;
            }
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
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            EFTN.component.OutwardSearchDB outwardSearchDB = new EFTN.component.OutwardSearchDB();

            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            SqlDataReader sqlDRSearch = outwardSearchDB.GetOutwardSearchResultForCSV(beginDate,
                                                   endDate,
                                                   txtReceivingBankRoutingNo.Text.Trim(),
                                                   txtDFIAccountNo.Text.Trim(),
                                                   txtReceiverName.Text.Trim(),
                                                   ParseData.StringToDouble(txtAmount.Text.Trim()),
                                                   ParseData.StringToDouble(txtMaxAmount.Text.Trim()),
                                                   txtAccountNo.Text.Trim(),
                                                   txtIdNumber.Text.Trim(),
                                                   txtCompanyName.Text.Trim(),
                                                   txtBankName.Text.Trim(),
                                                   txtTraceNumber.Text.Trim(),
                                                   txtBatchNumber.Text.Trim(),
                                                   rdoBtnSearchType.SelectedValue.Trim(),
                                                   ParseData.StringToInt(ddListTransactionType.SelectedValue),
                                                   txtPaymentInfo.Text.Trim(),
                                                   ParseData.StringToInt(ddListDeptID.SelectedValue),
                                                   bankCode, UserID, isFromArchive
                                                   ,CurrencyDdList.SelectedValue
                                                   ,SessionDdList.SelectedValue);

            //int columnNo = 
            string xlsFileName = "Outward" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
            string attachment = "attachment; filename=" + xlsFileName + ".csv";

            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.csv";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create the CSV file to which grid data will be exported. 
            //StreamWriter sw = new StreamWriter();
            int iColCount = sqlDRSearch.FieldCount;

            // First we will write the headers. 

            //if (iColCount > 0)
            //{
            //    sw.Write("\"");
            //}
            //sw.Write(sw.NewLine);

            //while (sqlDRSearch.Read())
            //{
            //    //sqlItemCount["InwardTransaction"].ToString()
            //    for (int i = 1; i < iColCount; i++)
            //    {
            //        sw.Write("\"");
            //        sw.Write(sqlItemCount[i].ToString());
            //        if (i < iColCount - 1)
            //        {
            //            sw.Write("\",");
            //            //sw.Write(";");
            //        }
            //    }
            //}

            //for (int i = 0; i < iColCount; i++)
            //{
            //sw.Write("\"");
            //sw.Write(sqlDRSearch.GetName(i));
            for (int i = 1; i < iColCount; i++)
            {
                if (!Convert.IsDBNull(sqlDRSearch.GetName(i)))
                {
                    sw.Write("\"");
                    sw.Write(sqlDRSearch.GetName(i).ToString());
                }
                if (i < iColCount - 1)
                {
                    sw.Write("\",");
                }
            }
            //}
            if (iColCount > 0)
            {
                sw.Write("\"");
            }
            sw.Write(sw.NewLine);

            // Now write all the rows.
            while (sqlDRSearch.Read())

            //foreach (DataRow dr in myDTSearch.Rows)
            {
                for (int i = 1; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(sqlDRSearch[i]))
                    {
                        sw.Write("\"");
                        sw.Write(sqlDRSearch[i].ToString());
                    }
                    else
                    {
                        sw.Write("\"");
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
            sqlDRSearch.Dispose();
            sqlDRSearch = null;
            Response.Write(sw.ToString());
            Response.End();
            Response.Flush();
        }

        private void GenerateCSVFromDataTable()
        {
            string xlsFileName = "Outward" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
            string attachment = "attachment; filename=" + xlsFileName + ".csv";

            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.csv";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create the CSV file to which grid data will be exported. 
            //StreamWriter sw = new StreamWriter();
            int iColCount = myDTSearch.Columns.Count;

            // First we will write the headers. 

            for (int i = 1; i < iColCount; i++)
            {
                sw.Write("\"");
                sw.Write(myDTSearch.Columns[i]);
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
            foreach (DataRow dr in myDTSearch.Rows)
            {
                for (int i = 1; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write("\"");
                        sw.Write(dr[i].ToString());
                    }
                    else
                    {
                        sw.Write("\"");
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
            Response.Flush();
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

        private void PrintPDDetailReport(string FileName)
        {
            if (myDTSearch.Rows.Count == 0)
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

            string DeptAndBranch = "\nFor " + ddListTransactionType.SelectedItem.Text + " Transaction From " + ddListDeptID.SelectedItem.Text;

            if (rdoBtnSearchType.SelectedValue.Equals("1"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                string settlementDateEnd = ddlistDayEnd.SelectedValue + "/" + ddlistMonthEnd.SelectedValue.PadLeft(2, '0') + "/" + ddlistYearEnd.SelectedValue;
                headertable.AddCell(new Phrase("Transaction Sent Report From: " + settlementDate + " To " + settlementDateEnd + DeptAndBranch, headerFont));
            }
            else if (rdoBtnSearchType.SelectedValue.Equals("2"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                string settlementDateEnd = ddlistDayEnd.SelectedValue + "/" + ddlistMonthEnd.SelectedValue.PadLeft(2, '0') + "/" + ddlistYearEnd.SelectedValue;
                headertable.AddCell(new Phrase("Received Return Report From: " + settlementDate + " To " + settlementDateEnd + DeptAndBranch, headerFont));
            }
            else if (rdoBtnSearchType.SelectedValue.Equals("3"))
            {
                string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                string settlementDateEnd = ddlistDayEnd.SelectedValue + "/" + ddlistMonthEnd.SelectedValue.PadLeft(2, '0') + "/" + ddlistYearEnd.SelectedValue;
                headertable.AddCell(new Phrase("Received NOC Report From: " + settlementDate + " To " + settlementDateEnd + DeptAndBranch, headerFont));
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
                headerwidths = new float[] { 11, 11, 8, 10, 4, 8, 6, 10, 8, 8, 8, 12, 8 };
                NumberOfPdfColumn = 13;
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
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Rec.Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("C.Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Ent.Desc", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("RejectReason", fnt));


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < myDTSearch.Rows.Count; i++)
            {
                if (SelectedBank.Equals("135"))
                {
                    PdfPCell cZone = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["ZoneName"], fnt));
                    cZone.BorderWidth = 0.5f;
                    cZone.HorizontalAlignment = Cell.ALIGN_LEFT;
                    cZone.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    cZone.Padding = 4;
                    datatable.AddCell(cZone);
                }

                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["BankName"], fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["BranchName"], fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 4;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["SECC"], fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["TraceNumber"], fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);

                PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["TransactionCode"], fnt));
                c5.BorderWidth = 0.5f;
                c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c5.Padding = 4;
                datatable.AddCell(c5);

                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["DFIAccountNo"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 4;
                datatable.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["ReceivingBankRoutingNo"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((myDTSearch.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["IdNumber"], fnt));
                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_LEFT;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 4;
                datatable.AddCell(c9);

                PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["ReceiverName"], fnt));
                c10.BorderWidth = 0.5f;
                c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c10.Padding = 4;
                datatable.AddCell(c10);

                PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["CompanyName"], fnt));
                c11.BorderWidth = 0.5f;
                c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c11.Padding = 4;
                datatable.AddCell(c11);

                PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["EntryDesc"], fnt));
                c12.BorderWidth = 0.5f;
                c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c12.Padding = 4;
                datatable.AddCell(c12);

                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["RejectReason"], fnt));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_LEFT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable.AddCell(c13);

            }

            //-------------TOTAL IN FOOTER --------------------
            if (SelectedBank.Equals("135"))
            {
                datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            }
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(myDTSearch.Compute("COUNT(TraceNumber)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(myDTSearch.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(myDTSearch.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
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
                string FileName = "DetailsReport-TransactionSent" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDDetailReport(FileName);
            }
            else if (rdoBtnSearchType.SelectedValue.Equals("2"))
            {
                string FileName = "DetailsReport-ReceivedReturn" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PrintPDDetailReport(FileName);
            }
            else if (rdoBtnSearchType.SelectedValue.Equals("3"))
            {
                string FileName = "DetailsReport-ReceivedNOC" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
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
            if (myDTSearch.Rows.Count == 0)
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
                headerwidths = new float[] { 11, 11, 8, 10, 4, 8, 6, 10, 8, 8, 8, 12, 8 };
                NumberOfPdfColumn = 13;
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
            datatable.AddCell(new iTextSharp.text.Phrase("Amount", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("IdNumber", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Receiver /Payer Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("C.Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Ent.Desc", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("RejectReason", fnt));


            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < myDTSearch.Rows.Count; i++)
            {
                if (SelectedBank.Equals("135"))
                {
                    PdfPCell cZone = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["ZoneName"], fnt));
                    cZone.BorderWidth = 0.5f;
                    cZone.HorizontalAlignment = Cell.ALIGN_LEFT;
                    cZone.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                    cZone.Padding = 4;
                    datatable.AddCell(cZone);
                }

                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["BankName"], fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["BranchName"], fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 4;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["SECC"], fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["TraceNumber"], fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);

                PdfPCell c5 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["TransactionCode"], fnt));
                c5.BorderWidth = 0.5f;
                c5.HorizontalAlignment = Cell.ALIGN_LEFT;
                c5.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c5.Padding = 4;
                datatable.AddCell(c5);

                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["DFIAccountNo"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 4;
                datatable.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["ReceivingBankRoutingNo"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble((myDTSearch.Rows[i]["Amount"]).ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fnt));

                PdfPCell c9 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["IdNumber"], fnt));
                c9.BorderWidth = 0.5f;
                c9.HorizontalAlignment = Cell.ALIGN_LEFT;
                c9.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c9.Padding = 4;
                datatable.AddCell(c9);

                PdfPCell c10 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["ReceiverName"], fnt));
                c10.BorderWidth = 0.5f;
                c10.HorizontalAlignment = Cell.ALIGN_LEFT;
                c10.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c10.Padding = 4;
                datatable.AddCell(c10);

                PdfPCell c11 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["CompanyName"], fnt));
                c11.BorderWidth = 0.5f;
                c11.HorizontalAlignment = Cell.ALIGN_LEFT;
                c11.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c11.Padding = 4;
                datatable.AddCell(c11);

                PdfPCell c12 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["EntryDesc"], fnt));
                c12.BorderWidth = 0.5f;
                c12.HorizontalAlignment = Cell.ALIGN_LEFT;
                c12.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c12.Padding = 4;
                datatable.AddCell(c12);

                PdfPCell c13 = new PdfPCell(new iTextSharp.text.Phrase((string)myDTSearch.Rows[i]["RejectReason"], fnt));
                c13.BorderWidth = 0.5f;
                c13.HorizontalAlignment = Cell.ALIGN_LEFT;
                c13.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c13.Padding = 4;
                datatable.AddCell(c13);

            }

            //-------------TOTAL IN FOOTER --------------------
            if (SelectedBank.Equals("135"))
            {
                datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            }
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(myDTSearch.Compute("COUNT(TraceNumber)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("", fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase("TOTAL", fntbld));
            //datatable.AddCell(new iTextSharp.text.Phrase(myDTSearch.Compute("COUNT(CheckSLNo)", "").ToString(), fntbld));
            datatable.AddCell(new iTextSharp.text.Phrase(ParseData.StringToDouble(myDTSearch.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture), fntbld));
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