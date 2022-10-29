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
using EFTN.component;
using EFTN.BLL;

namespace EFTN
{
    public partial class CreditFCUBSForEftDebitTXN : System.Web.UI.Page
    {
        private static DataTable myDataTable = new DataTable();
        //private EFTN.BLL.FinacleManager fm;

        private bool firstTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();
            }
        }


        private void BindData()
        {
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            RTServiceDB rtService = new RTServiceDB();
            string currentSystemDate = System.DateTime.Now.ToString("yyyyMMdd");

            string selectedDate = ddlistYear.SelectedValue.PadLeft(4, '0')
                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                + ddlistDay.SelectedValue.PadLeft(2, '0');

            if (ParseData.StringToInt(currentSystemDate) < ParseData.StringToInt(selectedDate))
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "You can select maximum current date";
                lblMsg.Visible = true;
                return;
            }
            else
            {
                lblMsg.Visible = false;
            }

            myDataTable = rtService.GetBatchesToCreditFCUBSForOutwardDebitBySettlementDate(DepartmentID,
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'));

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
        }

        protected void dtgEFTChecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTChecker.CurrentPageIndex = e.NewPageIndex;
            dtgEFTChecker.DataSource = myDataTable;
            dtgEFTChecker.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void dtgEFTChecker_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            Guid TransactionID = (Guid)dtgEFTChecker.DataKeys[e.Item.ItemIndex];

            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            string displayMsg = string.Empty;
            if (e.CommandName == "GenerateFlatFile")
            {
                string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                FCUBSRTManager fcubsManager = new FCUBSRTManager();
                displayMsg = fcubsManager.SendRTServiceEDRXMLForOutwardByBatchWiseForDebit(TransactionID, bankCode, ApprovedBy);
                BindData();
                txtMsg.Text = displayMsg;
                txtMsg.ForeColor = System.Drawing.Color.Blue;
                txtMsg.Visible = true;
            }
        }
    }
}
