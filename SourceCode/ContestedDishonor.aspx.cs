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
    public partial class ContestedDishonor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindContestedDishonor();
            }
        }

        private void BindContestedDishonor()
        {
            EFTN.component.ContestedDishonorDB contestedDishonorDB = new EFTN.component.ContestedDishonorDB();
            dtgContestedDishonorList.DataSource = contestedDishonorDB.GetContestedDishonor();
            dtgContestedDishonorList.DataBind();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            UpdateContestedDishonorStatus("Approve");

        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            UpdateContestedDishonorStatus("Reject");
        }

        private void UpdateContestedDishonorStatus(string TypeOfSave)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            for (int i = 0; i < dtgContestedDishonorList.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgContestedDishonorList.Items[i].FindControl("chkBoxContestedDishonor");

                if (cbx.Checked)
                {
                    string eDRID = dtgContestedDishonorList.DataKeys[i].ToString();
                    Guid dishonoredID = new Guid(dtgContestedDishonorList.Items[i].Cells[1].Text);
                    Guid contestedID = new Guid(dtgContestedDishonorList.Items[i].Cells[2].Text);

                    EFTN.component.ContestedDishonorDB contestedDishonorDB = new EFTN.component.ContestedDishonorDB();

                    if (TypeOfSave.Equals("Approve"))
                    {
                        contestedDishonorDB.UpdateSentContestedStatus(EFTN.Utility.TransactionStatus.DishonorReceived_Contested_Approved_by_checker, contestedID, dishonoredID, ApprovedBy);
                    }
                    else
                    {
                        contestedDishonorDB.UpdateSentContestedStatus(EFTN.Utility.TransactionStatus.Dishonor_Received_Rejected_By_Checker, contestedID, dishonoredID, ApprovedBy);
                    }
                }
            }
            BindContestedDishonor();
        }
    }
}
