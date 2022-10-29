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
    public partial class ApprovedDishonor : System.Web.UI.Page
    {
        private static DataTable myDTApprovedDishonor = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindApprovedDishonor();
            }
        }

        private void BindApprovedDishonor()
        {
            EFTN.component.ApprovedDishonorDB approvedDishonorDB = new EFTN.component.ApprovedDishonorDB();
            myDTApprovedDishonor = approvedDishonorDB.GetApprovedDishonor();
            dtgApprovedDishonorList.DataSource = myDTApprovedDishonor;
            dtgApprovedDishonorList.DataBind();
        }

        
        protected void dtgApprovedDishonorList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgApprovedDishonorList.CurrentPageIndex = e.NewPageIndex;
            dtgApprovedDishonorList.DataSource = myDTApprovedDishonor;
            dtgApprovedDishonorList.DataBind();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            UpdateApprovedDishonorStatus("Approve");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            UpdateApprovedDishonorStatus("Reject");
        }

        private void UpdateApprovedDishonorStatus(string TypeOfSave)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            for (int i = 0; i < dtgApprovedDishonorList.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgApprovedDishonorList.Items[i].FindControl("chkBoxApprovedDishonor");

                if (cbx.Checked)
                {
                    string strDishonorID = dtgApprovedDishonorList.DataKeys[i].ToString();
                    Guid dishonoredID = new Guid(strDishonorID);
                    string returnTraceNumber = dtgApprovedDishonorList.Items[i].Cells[11].Text;

                    EFTN.component.ApprovedDishonorDB approvedDishonorDB = new EFTN.component.ApprovedDishonorDB();

                    if (TypeOfSave.Equals("Approve"))
                    {
                        approvedDishonorDB.UpdatedSentApprovedDishonorStatus(EFTN.Utility.TransactionStatus.Dishonor_Received_Approval_Approved_by_checker, dishonoredID, returnTraceNumber, ApprovedBy);
                    }
                    else
                    {
                        approvedDishonorDB.UpdatedSentApprovedDishonorStatus(EFTN.Utility.TransactionStatus.Dishonor_Received_Rejected_By_Checker, dishonoredID, returnTraceNumber, ApprovedBy);
                    }
                }
            }
            BindApprovedDishonor();
        }
    }
}
