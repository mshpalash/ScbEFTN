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
using EFTN.BLL;

namespace EFTN
{
    public partial class CityChargeDepartmentManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCityChargeCodeInfo();
                BindData();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindCityChargeCodeInfo()
        {
            dataGridChargeCodeInfo.DataSource = GetChargeCodeInfo();
            dataGridChargeCodeInfo.DataBind();
        }

        public DataTable GetChargeCodeInfo()
        {
            EFTChargeManager eftChargeManager = new EFTChargeManager();
            return eftChargeManager.GetCityChargeCodeInfo();
        }

        private void BindData()
        {
            DepartmentsDB db = new DepartmentsDB();

            MyDataGrid2.DataSource = db.GetDepartments();
            MyDataGrid2.DataBind();
        }

        protected void MyDataGrid2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DepartmentsDB db = new DepartmentsDB();

            if (e.CommandName == "Cancel")
            {
                MyDataGrid2.EditItemIndex = -1;
                BindData();
            }
            if (e.CommandName == "Edit")
            {
                MyDataGrid2.EditItemIndex = e.Item.ItemIndex;
                BindData();
            }
            if (e.CommandName == "Update")
            {
                int departmentID                  = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];
                DropDownList ddListCityChargeCode = (DropDownList)e.Item.FindControl("ddListChargeCode");
                int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

                TextBox CityBBChargeAcc = (TextBox)e.Item.FindControl("addCityBBChargeAcc");
                TextBox CityBankChargeAcc = (TextBox)e.Item.FindControl("addCityBankChargeAcc");
                TextBox CityBBChargeVATAcc = (TextBox)e.Item.FindControl("addCityBBChargeVATAcc");
                TextBox CityBankChargeVATAcc = (TextBox)e.Item.FindControl("addCityBankChargeVATAcc");
                TextBox CityChargeWaveAcc = (TextBox)e.Item.FindControl("addCityChargeWaveAcc");
                TextBox CityVATWaveAcc = (TextBox)e.Item.FindControl("addCityVATWaveAcc");

                EFTChargeManager eftChargeManager = new EFTChargeManager();

                eftChargeManager.UpdateDepartmentChargeForCity(departmentID, CityBBChargeAcc.Text.Trim()
                                                                , CityBankChargeAcc.Text.Trim()
                                                                , CityBBChargeVATAcc.Text.Trim()
                                                                , CityBankChargeVATAcc.Text.Trim()
                                                                , CityChargeWaveAcc.Text.Trim()
                                                                , CityVATWaveAcc.Text.Trim()
                                                                , ParseData.StringToInt(ddListCityChargeCode.SelectedValue)
                                                                , UserID);
                MyDataGrid2.EditItemIndex = -1;

                BindData();
            }
        }
    }
}

