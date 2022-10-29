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
    public partial class RejectedListOfApprovedReceivedNOC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRejectedListOfApprovedNOC();
                BindRefusedCORCode();
            }

        }

        private void BindRejectedListOfApprovedNOC()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.ApprovedNOCDB approvedNOCDB = new EFTN.component.ApprovedNOCDB();
            dtgEFTRejectedListOfApprovedReceivedNOC.DataSource = approvedNOCDB.GetRejetedListOfApprovedNOC(DepartmentID);
            dtgEFTRejectedListOfApprovedReceivedNOC.DataBind();
        }

        private void BindRefusedCORCode()
        {
            EFTN.component.CodeLookUpDB codeDB = new EFTN.component.CodeLookUpDB();
            ddlRefusedCORCode.DataSource = codeDB.GetCodeLookUp((int)EFTN.Utility.CodeType.DishonourReturn);
            ddlRefusedCORCode.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtgEFTRejectedListOfApprovedReceivedNOC.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTRejectedListOfApprovedReceivedNOC.Items[i].FindControl("chkBoxRNOCofNOC");

                if (cbx.Checked)
                {
                    string nOCID = dtgEFTRejectedListOfApprovedReceivedNOC.DataKeys[i].ToString();
                    Guid NOCID = new Guid(nOCID);
                    EFTN.component.ReceivedNOCDB receivedNOCDB = new EFTN.component.ReceivedNOCDB();
                    receivedNOCDB.UpdateReceivedNOCStatus(EFTN.Utility.TransactionStatus.NOC_Received_Approved, NOCID, ddlRefusedCORCode.SelectedValue, 0, 0, txtCorrectedData.Text.Trim());
                }
            }

            BindRejectedListOfApprovedNOC();
        }
    }
}
