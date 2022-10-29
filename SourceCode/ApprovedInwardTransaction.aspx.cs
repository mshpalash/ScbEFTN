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
    public partial class ApprovedInwardTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                txtRejectReason.Text = "";
                txtRejectReason.Visible = false;
                lblRejectReasonMsg.Visible = false;
                rblTransactionDecision.SelectedValue = "1";
            }
        }
        private void BindData()
        {
            EFTN.component.ApprovedInwardTransactionDB db = new EFTN.component.ApprovedInwardTransactionDB();

            //EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value)
            int BranchID = 0;

            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            if (DepartmentID == 0)
            {
                BranchID = 0;
            }
            else if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);
            }
            dtgInwardApprovedTransaction.DataSource = db.GetReceivedEDRApprovedByMakerForChecker(BranchID);
            dtgInwardApprovedTransaction.DataBind();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {

            int type = Int32.Parse(rblTransactionDecision.SelectedValue);
            switch (type)
            {
                case 1:
                    ChangeStatusSelected(EFTN.Utility.TransactionStatus.Transaction_Received_Approved_By_Checker);
                    break;
                case 2:
                    ChangeStatusSelected(EFTN.Utility.TransactionStatus.Transaction_Received_Rejected_By_Checker);
                    break;
            }
        }

        private void ChangeStatusSelected(int statusID)
        {
            EFTN.component.ApprovedInwardTransactionDB db = new EFTN.component.ApprovedInwardTransactionDB();
            int cbxCounter = 0;
            for (int i = 0; i < dtgInwardApprovedTransaction.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgInwardApprovedTransaction.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    Guid edrId = (Guid)dtgInwardApprovedTransaction.DataKeys[i];
                    //db.UpdateEDRReceivedStatus(edrId, statusID, returnCode, 0, 0, txtCorrectedData.Text);
                    db.UpdateReceivedEDRStatus(statusID, edrId);
                    cbxCounter++;
                }
            }
            if (cbxCounter < 1)
            {
                lblMsg.Text = "Please select item";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
            }
            else
            {
                lblMsg.Visible = false;
            }
            BindData();
        }


        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgInwardApprovedTransaction.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgInwardApprovedTransaction.Items[i].FindControl("cbxCheck");
                cbx.Checked = checkAllChecked;
            }
        }

       
        

     
    }
}
