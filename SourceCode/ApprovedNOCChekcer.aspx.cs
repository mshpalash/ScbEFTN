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

namespace EFTN
{
    public partial class ApprovedNOCChekcer : System.Web.UI.Page
    {
        private static DataTable dtApproveNOC = new DataTable();
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindApprovedNOCData();
                sortOrder = "asc";
            }
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

        private void BindApprovedNOCData()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }


            EFTN.component.ApprovedNOCDB approvedNOCDB = new EFTN.component.ApprovedNOCDB();

            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                dtApproveNOC = approvedNOCDB.GetApprovedReceivedNOCForDebit(DepartmentID);
            }
            else
            {
                dtApproveNOC = approvedNOCDB.GetApprovedReceivedNOC(DepartmentID);
            }

            dv = dtApproveNOC.DefaultView;
            dtgEFTApprovedOfReceivedNOC.DataSource = dv;
            dtgEFTApprovedOfReceivedNOC.DataBind();

            if (dtApproveNOC.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + dtApproveNOC.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(dtApproveNOC.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }

            //dtApproveNOC = approvedNOCDB.GetApprovedReceivedNOC();
            //dtgEFTApprovedOfReceivedNOC.DataSource = dtApproveNOC;
            //dtgEFTApprovedOfReceivedNOC.DataBind();

            cbxAll.Checked = false;
        }

        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindApprovedNOCData();
        }


        protected void dtgEFTApprovedOfReceivedNOC_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTApprovedOfReceivedNOC.CurrentPageIndex = e.NewPageIndex;
            dtgEFTApprovedOfReceivedNOC.DataSource = dtApproveNOC;
            dtgEFTApprovedOfReceivedNOC.DataBind();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            BindApprovedNOCData("Approve");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            BindApprovedNOCData("Reject");
        }

        public void BindApprovedNOCData(string TypeOfSave)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            for (int i = 0; i < dtgEFTApprovedOfReceivedNOC.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTApprovedOfReceivedNOC.Items[i].FindControl("chkBoxApproveOfNOC");

                if (cbx.Checked)
                {
                    string rnocID = dtgEFTApprovedOfReceivedNOC.DataKeys[i].ToString();
                    Guid RNOCID = new Guid(rnocID);
                    EFTN.component.ApprovedNOCDB approvedNOCDB = new EFTN.component.ApprovedNOCDB();
                    string rejectReason = txtRejectReason.Text.Trim();
                    if (TypeOfSave.Equals("Approve"))
                    {
                        approvedNOCDB.UpdateReceivedNOCStatus(EFTN.Utility.TransactionStatus.NOC__Received_Approval__Approved_by_checker, RNOCID, ApprovedBy);
                    }
                    else if (TypeOfSave.Equals("Reject"))
                    {
                        approvedNOCDB.UpdateReceivedNOCStatus(EFTN.Utility.TransactionStatus.NOC_Received_Rejected_By_Checker, RNOCID, ApprovedBy);

                        EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                        db.Insert_RejectReason_ByChecker(RNOCID, rejectReason,
                                (int)EFTN.Utility.ItemType.NOCReceived);
                    }
                }
            }

            BindApprovedNOCData();
        }
        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgEFTApprovedOfReceivedNOC.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTApprovedOfReceivedNOC.Items[i].FindControl("chkBoxApproveOfNOC");
                cbx.Checked = checkAllChecked;
            }

        }
    }
}
