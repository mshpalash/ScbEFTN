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

namespace EFTN
{
    public partial class InwardTransactionNOCChecker : System.Web.UI.Page
    {
        private static DataTable dtSentNOC = new DataTable();
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                lblNoReturnReason.Visible = false;
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

        private void BindData()
        {
            int BranchID = 0;
            EFTN.component.SentNOCDB db = new EFTN.component.SentNOCDB();

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);
            }

            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                dtSentNOC = db.GetSentNOCForCheckerForDebit(BranchID).Copy();
            }
            else
            {
                //In UCBL for credit transaction when DepartmentID is only 0 then able to see the transaction.
                if (ConfigurationManager.AppSettings["CentralInward"].Equals("1"))
                {
                    if (ParseData.StringToInt(Request.Cookies["DepartmentID"].Value) == 0)
                    {
                        BranchID = 0;
                        dtSentNOC = db.GetSentNOCForChecker(BranchID).Copy();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    dtSentNOC = db.GetSentNOCForChecker(BranchID).Copy();
                }
                //dtSentNOC = db.GetSentNOCForChecker(BranchID).Copy();
            }

            dv = dtSentNOC.DefaultView;

            dtgEFTCheckerNOC.CurrentPageIndex = 0;
            dtgEFTCheckerNOC.DataSource = dv;
            dtgEFTCheckerNOC.DataBind();

            if (dtSentNOC.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + dtSentNOC.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(dtSentNOC.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
        }

        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void dtgEFTCheckerNOC_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTCheckerNOC.CurrentPageIndex = e.NewPageIndex;
            dtgEFTCheckerNOC.DataSource = dtSentNOC;
            dtgEFTCheckerNOC.DataBind();
        }

        protected void dtgEFTCheckerNOC_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = dtSentNOC.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgEFTCheckerNOC.DataSource = dv;
            dtgEFTCheckerNOC.DataBind();
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            ChangeStatusOfCheckedItems(EFTN.Utility.TransactionStatus.NOCSent_Approved_By_Checker);
            txtRejectedReason.Text = "";
            lblNoReturnReason.Visible = false;
            BindData();
            cbxAll.Checked = false;
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (txtRejectedReason.Text != "")
            {
                lblNoReturnReason.Visible = false;
                EnterRejectReason();
                ChangeStatusOfCheckedItems(EFTN.Utility.TransactionStatus.NOCSent_Rejected_By_Checker);
                txtRejectedReason.Text = "";
                BindData();
                cbxAll.Checked = false;
            }
            else
            {
                lblNoReturnReason.Visible = true;
            }
        }
        private void EnterRejectReason()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgEFTCheckerNOC.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerNOC.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    string edrId = dtgEFTCheckerNOC.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(EDRID, rejectReason,
                            (int)EFTN.Utility.ItemType.NOCSent);
                }
            }

        }

        private void ChangeStatusOfCheckedItems(int statusID)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            for (int i = 0; i < dtgEFTCheckerNOC.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerNOC.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {

                    Guid nocID = (Guid)dtgEFTCheckerNOC.DataKeys[i];

                    EFTN.component.SentNOCDB db = new EFTN.component.SentNOCDB();
                    db.UpdateNOCSentStatus(statusID, nocID, ApprovedBy);
                }
            }
        }
        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgEFTCheckerNOC.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerNOC.Items[i].FindControl("CheckBEFTNList");
                cbx.Checked = checkAllChecked;
            }

        }
    }
}
