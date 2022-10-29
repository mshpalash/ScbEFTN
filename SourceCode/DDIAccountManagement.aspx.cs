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
    public partial class DDIAccountManagement : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            BindData();
        }
        private void BindData()
        {
            DDIManager ddiManager = new DDIManager();
            MyDataGrid2.DataSource = ddiManager.GetDDIAccountStatus("ALL");
            MyDataGrid2.DataBind();
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
                if (!EFTN.BLL.RoutingNumberValidator.CheckDigitOk(RoutingNumber))
                {
                    txtMsg.Text = "Invalid Routing Number.";
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
                bool AccountException = false;
                DateTime dtExpiry;

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
                if (!EFTN.BLL.RoutingNumberValidator.CheckDigitOk(RoutingNumber))
                {
                    txtMsg.Text = "Invalid Routing Number.";
                    txtMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                ddiManager.UpdateSCBDDIAccountStatus(AccountID, AccountNo, OtherBankAcNo, RoutingNumber, dtExpiry, AccountException);
                MyDataGrid2.EditItemIndex = -1;
                txtMsg.Text = "Updated Successfully";
                txtMsg.ForeColor = System.Drawing.Color.Blue;
            }
            if (e.CommandName == "Delete")
            {
                int AccountID = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];

                ddiManager.DeleteSCBDDIAccountStatus(AccountID);

                txtMsg.Text = "Deleted Successfully";
                txtMsg.ForeColor = System.Drawing.Color.Blue;
            }

        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            if (fulExcelFile.HasFile)
            {
                string fileName = fulExcelFile.FileName;
                try
                {
                    //string savePath = Server.MapPath("~/ExcelFiles") + "\\" + Guid.NewGuid().ToString() + fileName;
                    string savePath = ConfigurationManager.AppSettings["EFTExcelFiles"] + Guid.NewGuid().ToString() + fileName;

                    fulExcelFile.SaveAs(savePath);

                    EFTN.BLL.ExcelDataDDIAccStatus excelObj = new EFTN.BLL.ExcelDataDDIAccStatus(savePath);
                    DataTable errTable = excelObj.EntryData();
                    if (errTable.Rows.Count > 0)
                    {
                        txtMsg.Text = "Following data are failed to Upload";
                        txtMsg.ForeColor = System.Drawing.Color.Red;
                        txtMsg.Visible = true;

                        dtgErrGrid.DataSource = errTable;
                        dtgErrGrid.DataBind();
                    }
                    else
                    {
                        SuccessMessage();
                    }
                }
                catch
                {
                    FailedMessage();
                }
            }
        }

        private void FailedMessage()
        {
            txtMsg.Text = "Failed to upload invalid file. Please upload valid excel file";
            txtMsg.ForeColor = System.Drawing.Color.Red;
            txtMsg.Visible = true;
        }

        private void SuccessMessage()
        {
            txtMsg.Text = "Data successfully uploaded";
            txtMsg.ForeColor = System.Drawing.Color.Blue;
            txtMsg.Visible = true;
        }

    }
}

