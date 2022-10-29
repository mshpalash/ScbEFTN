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
    public partial class CityCardsCustomerManagement : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            BindData();
        }
        private void BindData()
        {
            CityCardManager cityCardManager = new CityCardManager();
            MyDataGrid2.DataSource = cityCardManager.GetCityCardsCustomer();
            MyDataGrid2.DataBind();
        }

        protected void MyDataGrid2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            CityCardManager cityCardManager = new CityCardManager();

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
                string AccountName = string.Empty;
                string CustomerCardNo = string.Empty;

                TextBox txtAccountNo = (TextBox)e.Item.FindControl("addAccountNo");
                TextBox txtOtherBankAcNo = (TextBox)e.Item.FindControl("addOtherBankAcNo");
                TextBox txtRoutingNumber = (TextBox)e.Item.FindControl("addRoutingNumber");
                TextBox txtAccountName = (TextBox)e.Item.FindControl("addAccountName");
                TextBox txtCustomerCardNo = (TextBox)e.Item.FindControl("addCustomerCardNo");

                AccountNo = txtAccountNo.Text.Trim();
                OtherBankAcNo = txtOtherBankAcNo.Text.Trim();
                RoutingNumber = txtRoutingNumber.Text.Trim();
                AccountName = txtAccountName.Text.Trim();
                CustomerCardNo = txtCustomerCardNo.Text.Trim();

                if (!EFTN.BLL.RoutingNumberValidator.CheckDigitOk(RoutingNumber))
                {
                    txtMsg.Text = "Invalid Routing Number.";
                    txtMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                cityCardManager.InsertCityCardsCustomer(CustomerCardNo, AccountNo, OtherBankAcNo, AccountName, RoutingNumber);
                txtMsg.Text = "Inserted Successfully";
                txtMsg.ForeColor = System.Drawing.Color.Blue;
            }
            if (e.CommandName == "Update")
            {
                int CustomerID = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];

                string AccountNo = string.Empty;
                string OtherBankAcNo = string.Empty;
                string RoutingNumber = string.Empty;
                string AccountName = string.Empty;
                string CustomerCardNo = string.Empty;

                TextBox txtCustomerCardNo = (TextBox)e.Item.FindControl("txtCustomerCardNo");
                TextBox txtAccountNo = (TextBox)e.Item.FindControl("txtAccountNo");
                TextBox txtOtherBankAcNo = (TextBox)e.Item.FindControl("txtOtherBankAcNo");
                TextBox txtRoutingNumber = (TextBox)e.Item.FindControl("txtRoutingNumber");
                TextBox txtAccountName = (TextBox)e.Item.FindControl("txtAccountName");

                CustomerCardNo = txtCustomerCardNo.Text.Trim();
                AccountNo = txtAccountNo.Text.Trim();
                OtherBankAcNo = txtOtherBankAcNo.Text.Trim();
                RoutingNumber = txtRoutingNumber.Text.Trim();
                AccountName = txtAccountName.Text.Trim();

                if (!EFTN.BLL.RoutingNumberValidator.CheckDigitOk(RoutingNumber))
                {
                    txtMsg.Text = "Invalid Routing Number.";
                    txtMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                cityCardManager.UpdateCityCardsCustomer(CustomerID, CustomerCardNo, AccountNo, OtherBankAcNo, AccountName, RoutingNumber);
                MyDataGrid2.EditItemIndex = -1;
                txtMsg.Text = "Updated Successfully";
                txtMsg.ForeColor = System.Drawing.Color.Blue;
            }
            if (e.CommandName == "Delete")
            {
                int CustomerID = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];

                cityCardManager.DeleteCityCardsCustomer(CustomerID);

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

                    EFTN.BLL.ExcelDataCityDebitAccStatus excelObj = new EFTN.BLL.ExcelDataCityDebitAccStatus(savePath);
                    DataTable errTable = excelObj.EntryData();
                    if (errTable.Rows.Count > 0)
                    {
                        txtMsg.Text = "Following data are failed to Upload";
                        txtMsg.ForeColor = System.Drawing.Color.Red;
                        txtMsg.Visible = true;

                        //dtgErrGrid.DataSource = errTable;
                        //dtgErrGrid.DataBind();
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

