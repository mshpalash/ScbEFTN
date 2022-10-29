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
    public partial class EFTEditReturnSent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                ddlReturnChangeCode.Enabled = false;
                rblTransactionDecision.SelectedValue = "1";
                DisableFields();
            }
        }
        private void BindData()
        {
            string returnId = Request.Params["ReturnID"];
            EFTN.component.SentReturnDB sentReturnDB = new EFTN.component.SentReturnDB();
            rptEditReturn.DataSource = sentReturnDB.GetSentRR_By_RRSentID(new Guid(returnId));
            rptEditReturn.DataBind();
        }

        protected void rblTransactionDecision_SelectedIndexChanged(object sender, EventArgs e)
        {
            int type = Int32.Parse(rblTransactionDecision.SelectedValue);
            if (type == 1)
            {
                ddlReturnChangeCode.Enabled = false;
            }
            else
            {
                ddlReturnChangeCode.Enabled = true;
                EFTN.component.CodeLookUpDB codeDB = new EFTN.component.CodeLookUpDB();
                ddlReturnChangeCode.DataSource = codeDB.GetCodeLookUp(type);
                ddlReturnChangeCode.DataBind();
            }

            switch (type)
            {
                case 1: DisableFields();
                    break;
                case 2: DisableFields();
                    break;
                case 3: EnableFields();
                    break;
            }
        }

        public string GetFieldName(string fieldColumn)
        {
            int typeOfPayment = 1;
            if (Request.Params["TypeOfPayment"] != null)
            {
                typeOfPayment = Int32.Parse(Request.Params["TypeOfPayment"]);
            }
            return EFTN.BLL.FieldNameConverter.GetFieldName(fieldColumn, typeOfPayment);
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            int type = Int32.Parse(rblTransactionDecision.SelectedValue);
            switch (type)
            {
                case 1:
                    ChangeStatusSelected(EFTN.Utility.TransactionStatus.TransReceived_Approved);
                    break;
                case 2:
                    ChangeStatusSelected(EFTN.Utility.TransactionStatus.TransReceived_Reject_RR);
                    break;
                case 3:
                    if (txtCorrectedData.Text.Trim().Equals(string.Empty))
                    {
                        return;
                    }
                    else
                    {
                        ChangeStatusSelected(EFTN.Utility.TransactionStatus.TransReceived_Reject_NOC);
                    }
                    break;
            }

        }

        private void EnableFields()
        {
            lblCorrectedData.Visible = true;
            txtCorrectedData.Visible = true;
            lblMsg.Visible = true;
        }

        private void DisableFields()
        {
            lblCorrectedData.Visible = false;
            txtCorrectedData.Visible = false;
            lblMsg.Visible = false;
        }

        private void ChangeStatusSelected(int statusID)
        {
            string returnId = Request.Params["ReturnID"];
            string returnCode = (ddlReturnChangeCode.SelectedValue != null) ? ddlReturnChangeCode.SelectedValue : "";
            string correctedData = txtCorrectedData.Text.Trim();
            int createdBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            EFTN.component.SentReturnDB sentReturnDB = new EFTN.component.SentReturnDB();
            sentReturnDB.UpdateReturnSentByRRSent(new Guid(returnId), statusID, returnCode, correctedData, createdBy);
            BindData();
            Response.Redirect("~/EFTReturnRejectedForMaker.aspx");
        }
    }
}
