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
using EFTNAccelerator;

namespace FloraSoft
{
    public partial class SuperAdminCheckerUserManagement : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Response.Redirect("AccessDenied.aspx");
            BindData();
        }
        private void BindData()
        {
            UserDB db = new UserDB();
            //int isPending = 0;

            //if (ckbIsPending.Checked == true)
            //    isPending = 1;

            //if (Request.Cookies["RoleID"].Value != "10")//Checker Authorizer
            //{
            //    ckbIsPending.Visible = false;
            //    isPending = 0;
            //}

            MyDataGrid.DataSource = db.GetAllUsers("ALL", 1);
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
                #region  User Licensing Block
                int totalNoOfUser = db.GetTotalNoOfUser();
                if (UserLicense.IsOverToAdd(totalNoOfUser))
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "User activation failed: Please renew user license agreement";
                    return;
                }
                #endregion
                int UserID = (int)MyDataGrid.DataKeys[e.Item.ItemIndex];
                TextBox txtUsername = (TextBox)e.Item.FindControl("Username");
                TextBox txtRoleName = (TextBox)e.Item.FindControl("RoleName");
                //TextBox txtContactNo = (TextBox)e.Item.FindControl("ContactNo");
                //TextBox txtLoginID = (TextBox)e.Item.FindControl("LoginID");
                DropDownList txtUserRole = (DropDownList)e.Item.FindControl("Role");
                DropDownList ddlUserStatus = (DropDownList)e.Item.FindControl("ddlUserStatus");
                db.UpdateUserbyAdminChecker(UserID, ddlUserStatus.Text);

                HtmlInputHidden hdnID = e.Item.FindControl("hdnRoleId") as HtmlInputHidden;
                int roleId = Convert.ToInt32(hdnID.Value);

                UserHistoryDB userHistoryDB = new UserHistoryDB();
                userHistoryDB.InsertUserHistory(ParseData.StringToInt(Request.Cookies["UserID"].Value),
                                                  GetIPAddress()
                                                , UserHistoryVar.STATUS_CHANGED
                                                , UserID
                                                , roleId
                                                , 0
                                                , Request.Cookies["LoginID"].Value
                                                , 0
                                                , ""
                                                , ""
                                                ,""
                                                , ddlUserStatus.Text);

                MyDataGrid.EditItemIndex = -1;
            }
        }

        protected void ddListUserStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        protected void ckbIsPending_CheckedChanged(object sender, EventArgs e)
        {
            BindData();
        }
        public DataTable ACH_GetRoles()
        {
            RoleDB oDB = new RoleDB();
            return oDB.GetAllRoles();
        }

        private string GetIPAddress()
        {
            string ipaddress;
            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];

            return ipaddress;
        }
    }
}