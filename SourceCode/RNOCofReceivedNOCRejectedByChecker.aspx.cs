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
    public partial class RNOCofReceivedNOCRejectedByChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRNOCofNOCRejectedList();
                BindRefusedCORCode();
            }
        }

        private void BindRNOCofNOCRejectedList()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.RNOCofNOCDB rNOCofNOCDB = new EFTN.component.RNOCofNOCDB();
            dtgEFTRNOCOfReceivedNOCRejectedList.DataSource = rNOCofNOCDB.GetRNOCofNOCRejetedByChecker(DepartmentID);
            dtgEFTRNOCOfReceivedNOCRejectedList.DataBind();
        }

        private void BindRefusedCORCode()
        {
            EFTN.component.CodeLookUpDB codeDB = new EFTN.component.CodeLookUpDB();
            ddlRefusedCORCode.DataSource = codeDB.GetCodeLookUp((int)EFTN.Utility.CodeType.DishonourReturn);
            ddlRefusedCORCode.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtgEFTRNOCOfReceivedNOCRejectedList.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTRNOCOfReceivedNOCRejectedList.Items[i].FindControl("chkBoxRNOCofNOC");

                if (cbx.Checked)
                {
                    string nOCID = dtgEFTRNOCOfReceivedNOCRejectedList.DataKeys[i].ToString();
                    Guid NOCID = new Guid(nOCID);
                    EFTN.component.RNOCofNOCDB rNOCofNOCDB = new EFTN.component.RNOCofNOCDB();
                    rNOCofNOCDB.UpdateRNOCSentStatusByMaker(EFTN.Utility.TransactionStatus.NOC_Received_RNOC, NOCID, ddlRefusedCORCode.SelectedValue, txtCorrectedData.Text.Trim());
                    //rNOCofNOCDB.UpdateReceivedNOCStatus(EFTN.Utility.TransactionStatus.NOC_Received_RNOC, NOCID, ddlRefusedCORCode.SelectedValue, 0, 0, txtCorrectedData.Text.Trim());
                }
            }

            BindRNOCofNOCRejectedList();

        }

        protected void lnkBtnNOC_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtgEFTRNOCOfReceivedNOCRejectedList.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTRNOCOfReceivedNOCRejectedList.Items[i].FindControl("chkBoxRNOCofNOC");

                if (cbx.Checked)
                {
                    string nOCID = dtgEFTRNOCOfReceivedNOCRejectedList.DataKeys[i].ToString();
                    Guid NOCID = new Guid(nOCID);
                    EFTN.component.RNOCofNOCDB rNOCofNOCDB = new EFTN.component.RNOCofNOCDB();
                    rNOCofNOCDB.CancelRNOCAndApproveAsNOC(EFTN.Utility.TransactionStatus.NOC_Received_Approved, NOCID);
                    //rNOCofNOCDB.UpdateReceivedNOCStatus(EFTN.Utility.TransactionStatus.NOC_Received_RNOC, NOCID, ddlRefusedCORCode.SelectedValue, 0, 0, txtCorrectedData.Text.Trim());
                }
            }

            BindRNOCofNOCRejectedList();

        }
    }
}
