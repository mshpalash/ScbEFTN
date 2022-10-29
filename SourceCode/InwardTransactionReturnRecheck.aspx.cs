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
    public partial class InwardTransactionReturnRecheck : System.Web.UI.Page
    {
        private static DataTable dtInwardReturn = new DataTable();
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
            string trID = Request.Params["TransactionID"].ToString();
            Guid TransactionID = new Guid(trID);

            EFTN.component.SentReturnDB db = new EFTN.component.SentReturnDB();
            dtInwardReturn = db.EFTGetSentRRForCAbyBatchID(TransactionID);

            dv = dtInwardReturn.DefaultView;

            dtgEFTCheckerReturns.CurrentPageIndex = 0;
            dtgEFTCheckerReturns.DataSource = dv;
            dtgEFTCheckerReturns.DataBind();


            if (dtInwardReturn.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + dtInwardReturn.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(dtInwardReturn.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
            cbxAll.Checked = false;
        }

        protected void dtgEFTCheckerReturns_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTCheckerReturns.CurrentPageIndex = e.NewPageIndex;
            dtgEFTCheckerReturns.DataSource = dtInwardReturn;
            dtgEFTCheckerReturns.DataBind();
        }

        protected void dtgEFTCheckerReturns_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = dtInwardReturn.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgEFTCheckerReturns.DataSource = dv;
            dtgEFTCheckerReturns.DataBind();
        }
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            ChangeStatusOfCheckedItems(EFTN.Utility.TransactionStatus.RRSentApprovedByChecker);
            txtRejectedReason.Text = "";
            lblNoReturnReason.Visible = false;
            BindData();
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (txtRejectedReason.Text != "")
            {
                lblNoReturnReason.Visible = false;
                EnterRejectReason();
                ChangeStatusOfCheckedItems(EFTN.Utility.TransactionStatus.RRSent_Rejected_By_Checker);
                txtRejectedReason.Text = "";
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
            for (int i = 0; i < dtgEFTCheckerReturns.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerReturns.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    string edrId = dtgEFTCheckerReturns.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(EDRID, rejectReason,
                            (int)EFTN.Utility.ItemType.ReturnSent);
                }
            }

        }
        private void ChangeStatusOfCheckedItems(int statusID)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            for (int i = 0; i < dtgEFTCheckerReturns.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerReturns.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    
                    Guid returnID = (Guid)dtgEFTCheckerReturns.DataKeys[i];

                    EFTN.component.SentReturnDB db = new EFTN.component.SentReturnDB();
                    db.UpdateReturnSentStatus(statusID, returnID, ApprovedBy);
                }
            }
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgEFTCheckerReturns.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerReturns.Items[i].FindControl("CheckBEFTNList");
                cbx.Checked = checkAllChecked;
            }
        }
    }
}
