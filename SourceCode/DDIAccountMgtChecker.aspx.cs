using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using EFTN.component;
using EFTN.Utility;
using FloraSoft;

namespace EFTN
{
    public partial class DDIAccountMgtChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // Response.Redirect("AccessDenied.aspx");
            if (!IsPostBack)
            {
                BindData();
            }
        }
        private void BindData()
        {
            DDIManager ddiManager = new DDIManager();
            MyDataGrid2.DataSource = ddiManager.GetDDIAccountStatus("ALL");
            MyDataGrid2.DataBind();
            cbxAll.Checked = false;
        }

        protected void MyDataGrid2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DDIManager ddiManager = new DDIManager();

            if (e.CommandName == "Cancel")
            {
                MyDataGrid2.EditItemIndex = -1;
            }
            if (e.CommandName == "Edit")
            {
                MyDataGrid2.EditItemIndex = e.Item.ItemIndex;

            }
            if (e.CommandName == "Insert")
            {
             
                string AccountNo = string.Empty;
                string OtherBankAcNo = string.Empty;
                string RoutingNumber = string.Empty;
                string ExpiryDate = string.Empty;
                bool AccountException = false;
                DateTime dtExpiry;

                TextBox txtAccountNo = (TextBox)e.Item.FindControl("addAccountNo");
                TextBox txtOtherBankAcNo = (TextBox)e.Item.FindControl("addOtherBankAcNo");
                TextBox txtRoutingNumber = (TextBox)e.Item.FindControl("addRoutingNumber");
                TextBox txtExpiryDate = (TextBox)e.Item.FindControl("addExpiryDate");
                CheckBox chkBoxException = (CheckBox)e.Item.FindControl("chkBoxADDException");

                AccountNo = txtAccountNo.Text.Trim();
                OtherBankAcNo = txtOtherBankAcNo.Text.Trim();
                RoutingNumber = txtRoutingNumber.Text.Trim();
                ExpiryDate = txtExpiryDate.Text.Trim();
                AccountException = chkBoxException.Checked;

                try
                {
                    dtExpiry = DateTime.Parse(ExpiryDate);
                }
                catch
                {
                    txtMsg.Text = "Invalid Date Format.";
                    txtMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                ddiManager.InsertDDIAccountStatus(AccountNo, OtherBankAcNo, RoutingNumber, dtExpiry, AccountException);
                txtMsg.Text = "Inserted Successfully";
                txtMsg.ForeColor = System.Drawing.Color.Blue;
            }
            if (e.CommandName == "Update")
            {
                int AccountID = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];

                string AccountNo = string.Empty;
                string OtherBankAcNo = string.Empty;
                string RoutingNumber = string.Empty;
                string ExpiryDate = string.Empty;
                DateTime dtExpiry;
                bool AccountException;

                TextBox txtAccountNo = (TextBox)e.Item.FindControl("txtAccountNo");
                TextBox txtOtherBankAcNo = (TextBox)e.Item.FindControl("txtOtherBankAcNo");
                TextBox txtRoutingNumber = (TextBox)e.Item.FindControl("txtRoutingNumber");
                TextBox txtExpiryDate = (TextBox)e.Item.FindControl("txtExpiryDate");
                CheckBox chkBoxException = (CheckBox)e.Item.FindControl("chkBoxException");
                
                AccountNo = txtAccountNo.Text.Trim();
                OtherBankAcNo = txtOtherBankAcNo.Text.Trim();
                RoutingNumber = txtRoutingNumber.Text.Trim();
                ExpiryDate = txtExpiryDate.Text.Trim();
                AccountException = chkBoxException.Checked;

                try
                {
                    dtExpiry = DateTime.Parse(ExpiryDate);
                }
                catch
                {
                    txtMsg.Text = "Invalid Date Format.";
                    txtMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                ddiManager.UpdateSCBDDIAccountStatus(AccountID, AccountNo, OtherBankAcNo, RoutingNumber, dtExpiry, AccountException);
                MyDataGrid2.EditItemIndex = -1;
                txtMsg.Text = "Updated Successfully";
                txtMsg.ForeColor = System.Drawing.Color.Blue;
            }
        }

        protected void btnActive_Click(object sender, EventArgs e)
        {
            DDIManager ddiManager = new DDIManager();
            int cbxCounter = 0;

            for (int i = 0; i < MyDataGrid2.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)MyDataGrid2.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    int AccountID = (int)MyDataGrid2.DataKeys[i];// (Guid)dtgInwardTransactionMaker.DataKeys[i];
                    ddiManager.UpdateSCBDDIAccountStatusByChecker(AccountID, "ACTIVE");
                    cbxCounter++;
                }
            }
            if (cbxCounter < 1)
            {
                txtMsg.Text = "Please select item";
                txtMsg.ForeColor = System.Drawing.Color.Red;
                txtMsg.Visible = true;
            }
            else
            {
                txtMsg.Text = "Account Status Changed";
                txtMsg.ForeColor = System.Drawing.Color.Red;
                txtMsg.Visible = true;
            }
            BindData();

        }

        protected void btnInactive_Click(object sender, EventArgs e)
        {
            DDIManager ddiManager = new DDIManager();
            int cbxCounter = 0;

            for (int i = 0; i < MyDataGrid2.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)MyDataGrid2.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    int AccountID = (int)MyDataGrid2.DataKeys[i];// (Guid)dtgInwardTransactionMaker.DataKeys[i];
                    ddiManager.UpdateSCBDDIAccountStatusByChecker(AccountID, "INACTIVE");
                    cbxCounter++;
                }
            }
            if (cbxCounter < 1)
            {
                txtMsg.Text = "Please select item";
                txtMsg.ForeColor = System.Drawing.Color.Red;
                txtMsg.Visible = true;
            }
            else
            {
                txtMsg.Text = "Account Status Changed";
                txtMsg.ForeColor = System.Drawing.Color.Red;
                txtMsg.Visible = true;
            }
            BindData();
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < MyDataGrid2.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)MyDataGrid2.Items[i].FindControl("cbxCheck");
                cbx.Checked = checkAllChecked;
            }

        }
    }
}

