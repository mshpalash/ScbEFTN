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
using EFTN.Utility;
using EFTN.BLL;

namespace EFTN
{
    public partial class EFTEditTransactionSent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                if (bankCode.Equals(OriginalBankCode.NRB) || bankCode.Equals(OriginalBankCode.UCBL))
                {
                    btnCheckCbsAccount.Visible = true;
                }
                else
                {
                    btnCheckCbsAccount.Visible = false;
                }
            }
        }

        private void BindData()
        {
            string edrId = Request.Params["EDRID"];
            EFTN.component.SentEDRDB edrDb = new EFTN.component.SentEDRDB();
            rptEditTransaction.DataSource = edrDb.GetSentEDR_By_EDRSentID(new Guid(edrId));
            rptEditTransaction.DataBind();
        }
        
        public string GetFieldName(string fieldColumn)
        {
            int typeOfPayment = 0;
            if (Request.Params["TypeOfPayment"] != null)
            {
                typeOfPayment = Int32.Parse(Request.Params["TypeOfPayment"]);
            }
            return EFTN.BLL.FieldNameConverter.GetFieldName(fieldColumn,typeOfPayment);
        }

        public bool IsDecimal(string theValue)
        {
            try
            {
                Convert.ToDouble(theValue);
                return true;
            }
            catch
            {
                return false;
            }
        } //IsDecimal
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!IsDecimal(((TextBox)rptEditTransaction.Items[0].FindControl("txtAmount")).Text))
            {
                lblMsg.Text = "Failed to save. Please enter valid amount";
                return;
            }
            Guid EDRID = new Guid(Request.Params["EDRID"]);
            string DFIAccountNo = ((TextBox)rptEditTransaction.Items[0].FindControl("txtDFIAccountNo")).Text;
            string AccountNo = ((TextBox)rptEditTransaction.Items[0].FindControl("txtAccountNo")).Text;
            decimal amount = Decimal.Parse(((TextBox)rptEditTransaction.Items[0].FindControl("txtAmount")).Text);
            string IdNumber = ((TextBox)rptEditTransaction.Items[0].FindControl("txtIdNumber")).Text;
            string receiverName = ((TextBox)rptEditTransaction.Items[0].FindControl("txtReceiverName")).Text;
            string paymentInfo = ((TextBox)rptEditTransaction.Items[0].FindControl("txtPaymentInfo")).Text;
            string receivingBankRoutingNo = ((TextBox)rptEditTransaction.Items[0].FindControl("txtReceivingBankRoutingNo")).Text;
            
            string secc = ((TextBox)rptEditTransaction.Items[0].FindControl("txtSECC")).Text;

            if (!secc.Equals("CIE"))
            {
                if (IdNumber.Length > 15)
                {
                    lblMsg.Text = "Receiver ID maximum 15 character allowed for this type of transaction";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
            else
            {
                if (receiverName.Length > 15)
                {
                    lblMsg.Text = "Receiver Name must be less than 16 character";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
            EFTN.component.SentEDRDB db = new EFTN.component.SentEDRDB();
            db.UpdateSentEDR_By_EDRSentID
                (
                    EDRID,
                    DFIAccountNo,
                    AccountNo,
                    amount,
                    IdNumber,
                    receiverName,
                    paymentInfo,
                    receivingBankRoutingNo);
            EFTN.component.RejectReasonByCheckerDB reasonDb = new EFTN.component.RejectReasonByCheckerDB();
            //reasonDb.ClearRejectReeason(EDRID);
            Response.Redirect("~/EFTTransactionRejectedForMaker.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EFTTransactionRejectedForMaker.aspx");
        }

        protected void btnCheckCbsAccount_Click(object sender, EventArgs e)
        {
            string AccountNo = ((TextBox)rptEditTransaction.Items[0].FindControl("txtAccountNo")).Text;
            FCUBSAccountStatusManager accStatusManager = new FCUBSAccountStatusManager();
            string AccountStatus = accStatusManager.GetFCUBSAccountStatus(AccountNo);

            if (accStatusManager.IsValidAcc)
            {
                lblMsg.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            lblMsg.Text = AccountStatus;

        }

        
    }
}
