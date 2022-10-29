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
    public partial class RejectedReceivedReturnDishonorSent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
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
            dtgDishonorSentChecker.DataSource = sentDishonorDB.ReceivedReturnDishonorRejectedByChecker(DepartmentID);
            dtgDishonorSentChecker.DataBind();

            EFTN.component.CodeLookUpDB codeLookUpDb = new EFTN.component.CodeLookUpDB();
            ddlDishonour.DataSource = codeLookUpDb.GetCodeLookUp((int)EFTN.Utility.CodeType.DishonourReturn);
            ddlDishonour.DataBind(); 
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EFTMaker.aspx");
        }

        protected void rblDishonorDecision_SelectedIndexChanged(object sender, EventArgs e)
        {
            int type = Int32.Parse(rblDishonorDecision.SelectedValue);
            switch (type)
            {
                case 1:
                    ddlDishonour.Enabled = false;
                    txtAddendaInfo.Visible = false;
                    break;
                case 5:
                    ddlDishonour.Enabled = true;
                    txtAddendaInfo.Visible = true;
                    break;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int type = Int32.Parse(rblDishonorDecision.SelectedValue);
            for (int i = 0; i < dtgDishonorSentChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgDishonorSentChecker.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    string returnID = dtgDishonorSentChecker.DataKeys[i].ToString();
                    Guid ReturnID = new Guid(returnID);
                    switch (type)
                    {
                        case 1:
                            ChangeStatus(EFTN.Utility.TransactionStatus.Return_Received_Approved, ReturnID, "", "");
                            break;
                        case (int)EFTN.Utility.CodeType.DishonourReturn:
                            ChangeStatus(EFTN.Utility.TransactionStatus.Return_Received_Dishonor, ReturnID, ddlDishonour.SelectedValue, txtAddendaInfo.Text);

                            break;
                    }
                }
            }
            BindData();
        }

        private void ChangeStatus(int statusID, Guid returnID, string dishonorCode, string addendaInfo)
        {
            EFTN.component.ReceivedReturnDB db = new EFTN.component.ReceivedReturnDB();
            int CreatedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            db.UpdateDishonorSentRejectedByCheckerStatusByMaker(statusID, returnID, dishonorCode, addendaInfo);

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
