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
using EFTN.BLL;
using EFTN.Utility;

namespace FloraSoft
{
    public partial class CityChargeAccountsManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindChargeCodeInfo();
            }

            MyDataGrid.Focus();
        }

        private void BindChargeCodeInfo()
        {
            dataGridChargeCodeInfo.DataSource = GetChargeCodeInfo();
            dataGridChargeCodeInfo.DataBind();
        }

        public DataTable GetChargeCodeInfo()
        {
            EFTChargeManager eftChargeManager = new EFTChargeManager();
            return eftChargeManager.GetCityChargeCodeInfo();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            EFTChargeManager eftChargeManager = new EFTChargeManager();
            MyDataGrid.DataSource = eftChargeManager.GetCityChargeAccount();
            MyDataGrid.DataBind();
        }

        protected void MyDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            BranchesDB db = new BranchesDB();
            if (e.CommandName == "Cancel")
            {
                MyDataGrid.EditItemIndex = -1;
            }
            if (e.CommandName == "Edit")
            {
                MyDataGrid.EditItemIndex = e.Item.ItemIndex;
            }
            if (e.CommandName == "Insert")
            {
                TextBox txtAccountNo = (TextBox)e.Item.FindControl("addAccountNo");
                DropDownList ddListCityChargeCode = (DropDownList)e.Item.FindControl("addCityChargeCode");
                int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
                
                EFTChargeManager eftChargeManager = new EFTChargeManager();
                eftChargeManager.InsertCityChargeAccount(txtAccountNo.Text, ParseData.StringToInt(ddListCityChargeCode.SelectedValue), UserID);
                lblErrorMsg.Text = "Inserted Successfully.";
                lblErrorMsg.ForeColor = System.Drawing.Color.Blue;
            }
            if (e.CommandName == "Update")
            {
                TextBox txtAccountNo = (TextBox)e.Item.FindControl("AccountNo");
                if (txtAccountNo.Text.Trim().Equals(string.Empty))
                {
                    lblErrorMsg.Text = "Insert Account Number";
                    lblErrorMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                DropDownList ddListCityChargeCode = (DropDownList)e.Item.FindControl("ddListChargeCode");
                int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

                EFTChargeManager eftChargeManager = new EFTChargeManager();
                int CityChargeAccID = (int)MyDataGrid.DataKeys[e.Item.ItemIndex];
                string AccountNo = txtAccountNo.Text;
                eftChargeManager.UpdateCityChargeAccount(CityChargeAccID, ParseData.StringToInt(ddListCityChargeCode.SelectedValue), AccountNo);
                MyDataGrid.EditItemIndex = -1;
                lblErrorMsg.Text = "Updated Successfully.";
                lblErrorMsg.ForeColor = System.Drawing.Color.Blue;
            }
            if (e.CommandName == "Delete")
            {
                EFTChargeManager eftChargeManager = new EFTChargeManager();
                int CityChargeAccID = (int)MyDataGrid.DataKeys[e.Item.ItemIndex];

                eftChargeManager.DeleteEFTCityChargeAccount(CityChargeAccID);
                MyDataGrid.EditItemIndex = -1;
                lblErrorMsg.Text = "Deleted Successfully.";
                lblErrorMsg.ForeColor = System.Drawing.Color.Blue;
            }
        }
    }
}