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
    public partial class InwardTransactionWithMICR : System.Web.UI.Page
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

            DataTable dt = db.GetReceivedEDRByEDRIDForMICR(EDRID);
            if (dt.Rows.Count > 0)
            {
                dtgMicrInfo.DataSource = dt;
                dtgMicrInfo.DataBind();
            }
            else
            {
                lblMsg.Text = "MICR Data Not Found";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void EnableFor_CorrectedData()
        {
            lblCorrectedDataMsg.Text = "Corrected Data";
            lblCorrectedDataMsg.Visible = true;
            txtCorrectedData.Visible = true;
            txtCorrectedData.MaxLength = 30;

            DisableRoutingNumber();
            DisableTransactionCode();
        }

        private void EnableFor_BAcc_RtNo()
        {
            EnableBankAccount();
            EnableRoutingNumber();
            DisableTransactionCode();
        }

        private void EnableFor_BAcc_TrCode()
        {
            EnableBankAccount();
            DisableRoutingNumber();
            EnableTransactionCode();
        }

        private void EnableFor_RtNo_BAcc_TrCode()
        {
            EnableBankAccount();
            EnableTransactionCode();
            EnableRoutingNumber();
        }

        private void EnableBankAccount()
        {
            lblCorrectedDataMsg.Text = "Bank Account";
            lblCorrectedDataMsg.Visible = true;
            txtCorrectedData.Visible = true;
            txtCorrectedData.MaxLength = 13;
        }

        private void EnableRoutingNumber()
        {
            txtRoutingNumber.Visible = true;
            lblRoutingNumber.Visible = true;
        }

        private void EnableTransactionCode()
        {
            txtTransactionCode.Visible = true;
            lblTransactionCode.Visible = true;
        }

        private void DisableBankAccount()
        {
            lblCorrectedDataMsg.Visible = false;
            txtCorrectedData.Visible = false;
        }

        private void DisableRoutingNumber()
        {
            txtRoutingNumber.Visible = false;
            lblRoutingNumber.Visible = false;
        }

        private void DisableTransactionCode()
        {
            txtTransactionCode.Visible = false;
            lblTransactionCode.Visible = false;
        }

        protected void ddlReturnChangeCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReturnChangeCode.SelectedValue.Equals("C03"))
            {
                EnableFor_BAcc_RtNo();
            }
            else if (ddlReturnChangeCode.SelectedValue.Equals("C06"))
            {
                EnableFor_BAcc_TrCode();
            }
            else if (ddlReturnChangeCode.SelectedValue.Equals("C07"))
            {
                EnableFor_RtNo_BAcc_TrCode();
            }
            else
            {
                EnableFor_CorrectedData();
            }
        }

        protected void rblTransactionDecision_SelectedIndexChanged(object sender, EventArgs e)
        {
            int type = Int32.Parse(rblTransactionDecision.SelectedValue);
            if (type == 1)
            {
                ddlReturnChangeCode.Enabled = false;
                DisableBankAccount();
                DisableRoutingNumber();
                DisableTransactionCode();

            }
            else
            {
                ddlReturnChangeCode.Enabled = true;
                EFTN.component.CodeLookUpDB codeDB = new EFTN.component.CodeLookUpDB();
                ddlReturnChangeCode.DataSource = codeDB.GetCodeLookUp(type);
                ddlReturnChangeCode.DataBind();
                if (type == 3)
                {
                    EnableFor_CorrectedData();
                    ddlReturnChangeCode.AutoPostBack = true;
                }
                else
                {
                    DisableBankAccount();
                    ddlReturnChangeCode.AutoPostBack = false;
                    DisableRoutingNumber();
                    DisableTransactionCode();
                }

            }
        }

        //protected void rblTransactionDecision_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int type = Int32.Parse(rblTransactionDecision.SelectedValue);
        //    if (type == 1)
        //    {
        //        ddlReturnChangeCode.Enabled = false;
        //        txtCorrectedData.Text = "";
        //        txtCorrectedData.Visible = false;
        //        lblCorrectedDataMsg.Visible = false;

        //    }
        //    else
        //    {
        //        ddlReturnChangeCode.Enabled = true;
        //        EFTN.component.CodeLookUpDB codeDB = new EFTN.component.CodeLookUpDB();
        //        ddlReturnChangeCode.DataSource = codeDB.GetCodeLookUp(type);
        //        ddlReturnChangeCode.DataBind();
        //        if (type == 3)
        //        {
        //            txtCorrectedData.Visible = true;
        //            lblCorrectedDataMsg.Visible = true;
        //        }
        //        else
        //        {
        //            txtCorrectedData.Text = "";
        //            txtCorrectedData.Visible = false;
        //            lblCorrectedDataMsg.Visible = false;
        //        }

        //    }
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int type = Int32.Parse(rblTransactionDecision.SelectedValue);
            switch (type)
            {
                case 1:
                    CleanAllTextBox();
                    ChangeStatusSelected(EFTN.Utility.TransactionStatus.TransReceived_Approved);
                    break;
                case 2:
                    CleanAllTextBox();
                    ChangeStatusSelected(EFTN.Utility.TransactionStatus.TransReceived_Reject_RR);
                    break;
                case 3:
                    if (ddlReturnChangeCode.SelectedValue.Equals("C03"))
                    {
                        if (txtCorrectedData.Text.Trim().Equals(string.Empty)
                            || txtRoutingNumber.Text.Trim().Equals(string.Empty))
                        {
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Text = "Please Enter Required Data";
                            return;
                        }
                    }
                    else if (ddlReturnChangeCode.SelectedValue.Equals("C06"))
                    {
                        if (txtCorrectedData.Text.Trim().Equals(string.Empty)
                            || txtTransactionCode.Text.Trim().Equals(string.Empty))
                        {
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Text = "Please Enter Required Data";
                            return;
                        }
                    }
                    else if (ddlReturnChangeCode.SelectedValue.Equals("C07"))
                    {
                        if (txtCorrectedData.Text.Trim().Equals(string.Empty)
                            || txtTransactionCode.Text.Trim().Equals(string.Empty)
                            || txtRoutingNumber.Text.Trim().Equals(string.Empty))
                        {
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Text = "Please Enter Required Data";
                            return;
                        }
                    }
                    else
                    {
                        if (txtCorrectedData.Text.Trim().Equals(string.Empty))
                        {
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Text = "Please Enter Required Data";
                            return;
                        }
                    }
                    ChangeStatusSelected(EFTN.Utility.TransactionStatus.TransReceived_Reject_NOC);
                    break;
            }

            if (rblTransactionDecision.SelectedValue.Equals(string.Empty))
            {
                lblMsg.Text = "Select Transaction Type";
            }
            else
            {
                Response.Redirect("InwardTransactionMaker.aspx");
            }
        }

        private void CleanAllTextBox()
        {
            txtCorrectedData.Text = string.Empty;
            txtRoutingNumber.Text = string.Empty;
            txtTransactionCode.Text = string.Empty;
        }

        private void ChangeStatusSelected(int statusID)
        {
            string returnCode = (ddlReturnChangeCode.SelectedValue != null) ? ddlReturnChangeCode.SelectedValue : "";
            EFTN.component.ReceivedEDRDB db = new EFTN.component.ReceivedEDRDB();
            int CreatedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            string correctedData = txtCorrectedData.Text.Trim() + " " + txtRoutingNumber.Text.Trim() + " " + txtTransactionCode.Text.Trim();
            correctedData = correctedData.Trim();

            string strEDRID = Request.Params["inwardTransactionEDRID"].ToString();
            Guid EDRID = new Guid(strEDRID);
             
            db.UpdateEDRReceivedStatus(EDRID, statusID, returnCode, CreatedBy, correctedData);
        }
    }
}
