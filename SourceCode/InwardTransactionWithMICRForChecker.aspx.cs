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
using FloraSoft;

namespace EFTN
{
    public partial class InwardTransactionWithMICRForChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataGrid();
            }
        }

        private void BindDataGrid()
        {
            string strEDRID = Request.Params["inwardTransactionEDRID"].ToString();
            Guid EDRID = new Guid(strEDRID);
            MICRDB db = new MICRDB();

            DataTable dt = db.GetReceivedEDRByEDRIDForMICRForChecker(EDRID);
            if (dt.Rows.Count > 0)
            {
                dtgMicrInfo.DataSource = dt;
                dtgMicrInfo.DataBind();
            }
            else
            {
                lblMsg.Text = "CBS Data Not Found";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            ChangeStatusSelected("Approve");
            Response.Redirect("~/InwardTransactionApprovedChecker.aspx");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (!txtRejectedReason.Text.Equals(string.Empty))
            {
                ChangeStatusSelected("Reject");
                EnterRejectReason();
                Response.Redirect("~/InwardTransactionApprovedChecker.aspx");
            }
            else
            {
                lblNoReturnReason.Text = "Please enter reason";
                lblNoReturnReason.ForeColor = System.Drawing.Color.Red;
                lblNoReturnReason.Visible = true;
            }
        }

        private void ChangeStatusSelected(string statuschangesfor)
        {
            EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();
            EFTN.component.ApprovedInwardTransactionDB dbAIT = new EFTN.component.ApprovedInwardTransactionDB();
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);


            string strEDRID = Request.Params["inwardTransactionEDRID"].ToString();
            Guid EDRID = new Guid(strEDRID);
            if (statuschangesfor.Equals("Approve"))
            {
                dbAIT.UpdateReceivedEDRStatusApprovedByChecker(EDRID, ApprovedBy);
            }
            else
            {
                db.UpdateReceivedEDRStatusRejectedByEBBSChecker(EDRID, ApprovedBy);
            }
        }

        private void EnterRejectReason()
        {
            string rejectReason = txtRejectedReason.Text;
            string strEDRID = Request.Params["inwardTransactionEDRID"].ToString();
            Guid EDRID = new Guid(strEDRID);

            EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
            db.Insert_RejectReason_ByChecker(EDRID, rejectReason,
                    (int)EFTN.Utility.ItemType.TransactionReceived);
        }

    }
}
