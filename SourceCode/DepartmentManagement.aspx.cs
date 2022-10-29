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
    public partial class DepartmentManagement : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
           // Response.Redirect("AccessDenied.aspx");
            BindData();
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
            }
            if (e.CommandName == "Edit")
            {
                MyDataGrid2.EditItemIndex = e.Item.ItemIndex;

            }
            if (e.CommandName == "Insert")
            {
                TextBox txtDepartmentName = (TextBox)e.Item.FindControl("addDepartmentName");
                string parkingAccountIn = string.Empty;
                string parkingAccountOut = string.Empty;

                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("215"))
                {
                    TextBox txtParkingAccountIn = (TextBox)e.Item.FindControl("txtParkingAccountIn");
                    TextBox txtParkingAccountOut = (TextBox)e.Item.FindControl("txtParkingAccountOut");

                    parkingAccountIn = txtParkingAccountIn.Text.Trim();
                    parkingAccountOut = txtParkingAccountOut.Text.Trim();
                }
                //TextBox txtParkingAccountIn = (TextBox)e.Item.FindControl("addParkingAccountIn");
                //TextBox txtParkingAccountOut = (TextBox)e.Item.FindControl("addParkingAccountOut");
                db.InsertDepartment(txtDepartmentName.Text.Trim(), parkingAccountIn, parkingAccountOut, ParseData.StringToInt(Request.Cookies["UserID"].Value));
            }
            if (e.CommandName == "Update")
            {
                int departmentID                  = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];
                TextBox txtDepartmentName = (TextBox)e.Item.FindControl("txtDepartmentName");
                string parkingAccountIn = string.Empty;
                string parkingAccountOut = string.Empty;
                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("215"))
                {
                    TextBox txtParkingAccountIn = (TextBox)e.Item.FindControl("txtParkingAccountIn");
                    TextBox txtParkingAccountOut = (TextBox)e.Item.FindControl("txtParkingAccountOut");

                    parkingAccountIn = txtParkingAccountIn.Text.Trim();
                    parkingAccountOut = txtParkingAccountOut.Text.Trim();
                }
                db.UpdateDepartment(txtDepartmentName.Text.Trim(), parkingAccountIn, parkingAccountOut, departmentID);
                MyDataGrid2.EditItemIndex   = -1;
            }
        }
    }
}

