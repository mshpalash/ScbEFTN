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

namespace FloraSoft
{
    public partial class CheckerUserManagement : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Response.Redirect("AccessDenied.aspx");
            BindData();
        }
        private void BindData()
        {
            string OwnBankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            if (OwnBankCode.Equals("215"))
            {
                Response.Redirect("AdminChecker.aspx");
            }
            int isPending = 0;

            if (ckbIsPending.Checked == true)
                isPending = 1;

            if (Request.Cookies["RoleID"].Value != "10")//Checker Authorizer
            {
                ckbIsPending.Visible = false;
                isPending = 0;
            }
            UserDB db = new UserDB();

            MyDataGrid.DataSource = db.GetAllUsers(ddListUserStatus.SelectedValue, isPending);
            MyDataGrid.DataBind();
        }
        //public SqlDataReader ACH_GetRoles()
        //{
        //    RoleDB oDB = new RoleDB();
        //    return oDB.GetAllRoles();
        //}
        public DataTable GetBranchListByBankID()
        {
            BranchesDB branchDB = new BranchesDB();
            return branchDB.GetBranchByBankCode(ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));
            //return branchDB.GetBranchesByBankID(1);
        }
        protected void MyDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            UserDB db = new UserDB();
            //if (Request.Cookies["RoleID"].Value == "9")
            //{
            //    Response.Redirect("AccessDenied.aspx");
            //}
            if (e.CommandName == "Cancel")
            {
                MyDataGrid.EditItemIndex = -1;
            }
            if (e.CommandName == "Edit")
            {
                MyDataGrid.EditItemIndex = e.Item.ItemIndex;

            }
            //if (e.CommandName == "Insert")
            //{
            //    TextBox txtUsername = (TextBox)e.Item.FindControl("addUsername");
            //    TextBox txtDepartment = (TextBox)e.Item.FindControl("addDepartment");
            //    TextBox txtContactNo = (TextBox)e.Item.FindControl("addContactNo");
            //    TextBox txtLoginID = (TextBox)e.Item.FindControl("addLoginID");
            //    TextBox txtPassword = (TextBox)e.Item.FindControl("addPassword");
            //    DropDownList txtUserRole = (DropDownList)e.Item.FindControl("addRoleName");
            //    DropDownList ddlBranchID = (DropDownList)e.Item.FindControl("addBranchName");
            //    db.InsertUser(txtUsername.Text, txtDepartment.Text, txtContactNo.Text, txtLoginID.Text, txtPassword.Text, Int32.Parse(txtUserRole.SelectedValue), Int32.Parse(ddlBranchID.SelectedValue), Int32.Parse(Context.User.Identity.Name), Request.UserHostAddress);
            //}
            if (e.CommandName == "Update")
            {
                int UserID = (int)MyDataGrid.DataKeys[e.Item.ItemIndex];
                //TextBox txtUsername = (TextBox)e.Item.FindControl("Username");
                //TextBox txtDepartment = (TextBox)e.Item.FindControl("Department");
                //TextBox txtContactNo = (TextBox)e.Item.FindControl("ContactNo");
                //TextBox txtLoginID = (TextBox)e.Item.FindControl("LoginID");
                //DropDownList txtUserRole = (DropDownList)e.Item.FindControl("Role");
                DropDownList ddlUserStatus = (DropDownList)e.Item.FindControl("ddlUserStatus");
                db.UpdateUserbyAdminChecker(UserID, ddlUserStatus.Text);


                UserHistoryDB userHistoryDB = new UserHistoryDB();
                userHistoryDB.InsertUserHistory(ParseData.StringToInt(Request.Cookies["UserID"].Value),
                                                  GetIPAddress()
                                                , ddlUserStatus.Text
                                                , UserID
                                                , 0
                                                , 0
                                                , ""
                                                , 0
                                                , ""
                                                , ""
                                                , ""
                                                , ddlUserStatus.Text);

                MyDataGrid.EditItemIndex = -1;
            }
        }

        protected void ddListUserStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private string GetIPAddress()
        {
            string ipaddress;
            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];

            return ipaddress;
        }
        protected void ckbIsPending_CheckedChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}