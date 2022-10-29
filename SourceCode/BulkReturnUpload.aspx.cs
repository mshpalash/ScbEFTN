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
using FloraSoft;

namespace EFTN
{
    public partial class BulkReturnUpload : System.Web.UI.Page
    {
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private static string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        private static DataTable dtInwardTransaction = new DataTable();
        DataView dv;
        private BulkReturnDB db = new BulkReturnDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //BindCurrencyTypeDropdownlist();
                lblErrMsg.Text = string.Empty;
                BindData();
            }
        }

        protected void BindData()
        {
            int DepartmentID = int.Parse(Request.Cookies["DepartmentID"].Value);
            dtInwardTransaction = db.GetReceivedEDRForAdminForDebitForSCBForIFReturn(DepartmentID);
            dv = dtInwardTransaction.DefaultView;
            dtgInwardTransactionMaker.DataSource = dv;
            dtgInwardTransactionMaker.DataBind();
        }


        //protected void BindCurrencyTypeDropdownlist()
        //{
        //    DataTable dropDownData = new DataTable();
        //    dropDownData = sentBatchDB.GetCurrencyList(eftConString);
        //    CurrencyDdList.DataSource = dropDownData;
        //    CurrencyDdList.DataTextField = "Currency";
        //    CurrencyDdList.DataValueField = "Currency";
        //    CurrencyDdList.DataBind();
        //}
        protected void Page_PreRender(object sender, EventArgs e)
        {
        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            lblErrMsg.Text = string.Empty;
            if (fulExcelFile.HasFile)
            {
                string fileName = fulExcelFile.FileName;
                try
                {
                    string savePath = ConfigurationManager.AppSettings["EFTExcelFiles"] + Guid.NewGuid().ToString() + fileName;
                    fulExcelFile.SaveAs(savePath);
                    EFTN.BLL.ExcelDataBulkReturn excelObj = new EFTN.BLL.ExcelDataBulkReturn(savePath);
                    int CreatedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
                    string returnMsg = excelObj.EntryData(CreatedBy);
                    lblErrMsg.Text = returnMsg;
                    lblErrMsg.ForeColor = System.Drawing.Color.Blue;
                    lblErrMsg.Font.Bold = true;
                    lblErrMsg.Visible = true;
                    BindData();
                }
                catch
                {
                    FailedMessage();
                }
            }
        }

        private void FailedMessage()
        {
            lblErrMsg.Text = "Invalid excel file! Please upload valid excel file.";
            lblErrMsg.ForeColor = System.Drawing.Color.Crimson;
            lblErrMsg.Font.Bold = true;
            lblErrMsg.Visible = true;
        }
        public string sortOrder
        {
            get
            {
                dtgInwardTransactionMaker.DataSource = dtInwardTransaction;
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
                dtgInwardTransactionMaker.DataBind();
            }
        }
        protected void dtgInwardTransactionMaker_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = dtInwardTransaction.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgInwardTransactionMaker.DataSource = dv;
            dtgInwardTransactionMaker.DataBind();            
        }
        protected void dtgInwardTransactionMaker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgInwardTransactionMaker.CurrentPageIndex = e.NewPageIndex;
            dtgInwardTransactionMaker.DataSource = dtInwardTransaction;
            dtgInwardTransactionMaker.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblErrMsg.Text = string.Empty;
            for (int i = 0; i < dtgInwardTransactionMaker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgInwardTransactionMaker.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    Guid edrId = (Guid)dtgInwardTransactionMaker.DataKeys[i];
                    int createdBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
                    try
                    {
                        db.InsertReturnForIF(edrId, createdBy);
                        lblErrMsg.Text = "Selected transaction has been returned successfully.";
                        lblErrMsg.ForeColor = System.Drawing.Color.ForestGreen;
                        lblErrMsg.Font.Bold = true;
                        lblErrMsg.Visible = true;
                    }
                    catch
                    {
                    }                   
                }
            }
            BindData();
        }

        protected void bdnAccept_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtgInwardTransactionMaker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgInwardTransactionMaker.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();
                    Guid edrId = (Guid)dtgInwardTransactionMaker.DataKeys[i];
                    int createdBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
                    try
                    {                       
                        db.UpdateEDRReceivedStatusForOutwardDebitReturn(edrId, EFTN.Utility.TransactionStatus.TransReceived_Approved, createdBy);
                        lblErrMsg.Text = "Selected transaction has been accepted.";
                        lblErrMsg.ForeColor = System.Drawing.Color.ForestGreen;
                        lblErrMsg.Font.Bold = true;
                        lblErrMsg.Visible = true;
                    }
                    catch
                    {
                        lblErrMsg.Text = "Transaction could not be accepted! Please contact system admin.";
                        lblErrMsg.ForeColor = System.Drawing.Color.Crimson;
                        lblErrMsg.Font.Bold = true;
                        lblErrMsg.Visible = true;
                    }
                }
            }
            BindData();
        }
    }
}