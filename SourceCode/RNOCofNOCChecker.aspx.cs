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
    public partial class RNOCofNOCChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRNOCofNOCData();
            }
        }

        private void BindRNOCofNOCData()
        {
            EFTN.component.RNOCofNOCDB rNOCofNOCDB = new EFTN.component.RNOCofNOCDB();
            dtgEFTRNOCOfReceivedNOC.DataSource = rNOCofNOCDB.GetRNOCofReceivedNOC();
            dtgEFTRNOCOfReceivedNOC.DataBind();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            BindRNOCData("Approve");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            BindRNOCData("Reject");
        }

        public void BindRNOCData(string TypeOfSave)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            for (int i = 0; i < dtgEFTRNOCOfReceivedNOC.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTRNOCOfReceivedNOC.Items[i].FindControl("chkBoxRNOCofNOC");

                if (cbx.Checked)
                {
                    string rnocID = dtgEFTRNOCOfReceivedNOC.DataKeys[i].ToString();
                    Guid RNOCID = new Guid(rnocID);
                    EFTN.component.RNOCofNOCDB rNOCofNOCDB = new EFTN.component.RNOCofNOCDB();
                    string rejectReason = txtRejectReason.Text.Trim();
                    if (TypeOfSave.Equals("Approve"))
                    {
                        rNOCofNOCDB.UpdateSentRNOCStatus(EFTN.Utility.TransactionStatus.NOC_Received_RNOC_Approved_by_checker, RNOCID, ApprovedBy);
                    }
                    else if (TypeOfSave.Equals("Reject"))
                    {
                        rNOCofNOCDB.UpdateSentRNOCStatus(EFTN.Utility.TransactionStatus.NOC_Received_Rejected_By_Checker, RNOCID, ApprovedBy);

                        EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                        db.Insert_RejectReason_ByChecker(RNOCID, rejectReason,
                                (int)EFTN.Utility.ItemType.NOCReceived);
                    }
                }
            }

            BindRNOCofNOCData();
        }
    }
}
