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
    public partial class EFTDishonorSentChecker : System.Web.UI.Page
    {
        private static DataTable dtSentDishonor = new DataTable();
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
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
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.SentDishonorDB sentDishonorDB = new EFTN.component.SentDishonorDB();
            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                dtSentDishonor = sentDishonorDB.ReceivedReturn_Dishonor_ForCheckerForDebit(DepartmentID).Copy();
            }
            else
            {
                dtSentDishonor = sentDishonorDB.ReceivedReturn_Dishonor_ForChecker(DepartmentID).Copy();
            }

            //dtgDishonorSentChecker.DataSource = dtSentDishonor;
            //dtgDishonorSentChecker.DataBind();

            dv = dtSentDishonor.DefaultView;

            dtgDishonorSentChecker.CurrentPageIndex = 0;
            dtgDishonorSentChecker.DataSource = dv;
            dtgDishonorSentChecker.DataBind();

            if (dtSentDishonor.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + dtSentDishonor.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(dtSentDishonor.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }

            lblDishonorSentMsg.Visible = false;

            txtDishonorSentRejectReason.Text = "";
        }

        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        
        protected void dtgDishonorSentChecker_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = dtSentDishonor.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgDishonorSentChecker.DataSource = dv;
            dtgDishonorSentChecker.DataBind();
        }

        protected void dtgDishonorSentChecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgDishonorSentChecker.CurrentPageIndex = e.NewPageIndex;
            dtgDishonorSentChecker.DataSource = dtSentDishonor;
            dtgDishonorSentChecker.DataBind();
        }

        protected void btnDishonorApprove_Click(object sender, EventArgs e)
        {
            UpdateDishonorStatus(EFTN.Utility.TransactionStatus.Return_Received_Dishonor_Approved_by_checker);
            lblDishonorSentMsg.Visible = false;
            txtDishonorSentRejectReason.Text = "";
        }

        protected void btnDishonorReject_Click(object sender, EventArgs e)
        {
            if (txtDishonorSentRejectReason.Text != "")
            {
                
                UpdateDishonorStatus(EFTN.Utility.TransactionStatus.Return_Received_Rejected_By_Checker);
            }
            else
            {
                lblDishonorSentMsg.Visible = true;
            }
        }

        private void UpdateDishonorStatus(int statusID)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            EFTN.component.SentDishonorDB db = new EFTN.component.SentDishonorDB();
            for (int i = 0; i < dtgDishonorSentChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgDishonorSentChecker.Items[i].FindControl("CheckBEFTNList");
                if (cbx.Checked)
                {
                    Guid dishonorID = (Guid)dtgDishonorSentChecker.DataKeys[i];
                    db.Update_SentDishonor_Status_ByChecker(statusID, dishonorID, ApprovedBy);

                    if (statusID == EFTN.Utility.TransactionStatus.Return_Received_Rejected_By_Checker)
                    {
                        string rejectReason = txtDishonorSentRejectReason.Text;
                        EFTN.component.RejectReasonByCheckerDB rejectReasonByCheckerDB = new EFTN.component.RejectReasonByCheckerDB();
                        rejectReasonByCheckerDB.Insert_RejectReason_ByChecker(dishonorID, rejectReason, (int)EFTN.Utility.ItemType.DishonouredSent);
                    }
                }
            }
            BindData();
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgDishonorSentChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgDishonorSentChecker.Items[i].FindControl("CheckBEFTNList");
                cbx.Checked = checkAllChecked;
            }

        }
    }
}
