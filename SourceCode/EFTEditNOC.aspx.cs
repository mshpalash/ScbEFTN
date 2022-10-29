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
    public partial class EFTEditNOC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                ddlReturnChangeCode.Enabled = false;
                txtCorrectedData.Text = "";
                txtCorrectedData.Visible = false;
                lblCorrectedDataMsg.Visible = false;

                rblTransactionDecision.SelectedValue = "1";
            }
        }
        private void BindData()
        {
            string nocID = Request.Params["NOCID"];
            EFTN.component.SentNOCDB sentNOCDB = new EFTN.component.SentNOCDB();
            rptEditNOC.DataSource = sentNOCDB.GetSentNOC_By_NOCID(new Guid(nocID));
            rptEditNOC.DataBind();
            
        }

        protected void rblTransactionDecision_SelectedIndexChanged(object sender, EventArgs e)
        {
            int type = Int32.Parse(rblTransactionDecision.SelectedValue);
            if (type == 1)
            {
                ddlReturnChangeCode.Enabled = false;
                txtCorrectedData.Text = "";
                txtCorrectedData.Visible = false;
                lblCorrectedDataMsg.Visible = false;

            }
            else
            {
                ddlReturnChangeCode.Enabled = true;
                EFTN.component.CodeLookUpDB codeDB = new EFTN.component.CodeLookUpDB();
                ddlReturnChangeCode.DataSource = codeDB.GetCodeLookUp(type);
                ddlReturnChangeCode.DataBind();
                if (type == 3)
                {
                    txtCorrectedData.Visible = true;
                    lblCorrectedDataMsg.Visible = true;
                }
                else
                {
                    txtCorrectedData.Text = "";
                    txtCorrectedData.Visible = false;
                    lblCorrectedDataMsg.Visible = false;
                }

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
                    ChangeStatusSelected(EFTN.Utility.TransactionStatus.TransReceived_Reject_NOC);
                    break;
            }

        }
        private void ChangeStatusSelected(int statusID)
        {
            string nocID = Request.Params["NOCID"];
            string changeCode = (ddlReturnChangeCode.SelectedValue != null) ? ddlReturnChangeCode.SelectedValue : "";
            string correctedData = txtCorrectedData.Text;
            int createdBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            EFTN.component.SentNOCDB db = new EFTN.component.SentNOCDB();
            db.UpdateNOCByNOCID(new Guid(nocID), statusID, changeCode, correctedData, createdBy);
            BindData();
            Response.Redirect("~/EFTNOCRejectedForMaker.aspx");
        }
    }
}
