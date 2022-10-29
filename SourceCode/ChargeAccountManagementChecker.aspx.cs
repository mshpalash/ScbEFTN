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
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FloraSoft
{
    public partial class ChargeAccountManagementChecker : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
           // Response.Redirect("AccessDenied.aspx");
            BindData();
        }
        private void BindData()
        {
            EFTChargeDB db = new EFTChargeDB();
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            MyDataGrid2.DataSource = db.GetChargeACCOUNT(connectionString);
            MyDataGrid2.DataBind();
        }

        //protected void ddListUserStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindData();
        //}

        //public DataTable ACH_GetRoles()
        //{
        //    RoleDB oDB = new RoleDB();
        //    return oDB.GetAllRoles();
        //}

        //public DataTable GetDepartments()
        //{
        //    DepartmentsDB deptDB = new DepartmentsDB();
        //    return deptDB.GetDepartments();
        //}

        //public DataTable GetBranchListByBankID()
        //{
        //    BranchesDB branchDB = new BranchesDB();
        //    return branchDB.GetBranchByBankCode(ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));
        //    //return branchDB.GetBranchesByBankID(1);
        //}
        protected void MyDataGrid2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            //UserDB db = new UserDB();
            //if (Request.Cookies["RoleID"].Value == "9")
            //{
            //    Response.Redirect("AccessDenied.aspx");
            //}
            //if (e.CommandName == "Cancel")
            //{
            //    MyDataGrid2.EditItemIndex = -1;
            //}
            //if (e.CommandName == "Edit")
            //{
            //    MyDataGrid2.EditItemIndex = e.Item.ItemIndex;

            //}
            if (e.CommandName == "Insert")
            {
                string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

                TextBox txtACCOUNT = (TextBox)e.Item.FindControl("addACCOUNT");
                DropDownList ddListBBCharge = (DropDownList)e.Item.FindControl("addBBCharge");
                DropDownList ddListBankCharge = (DropDownList)e.Item.FindControl("addBankCharge");
                DropDownList ddListBBChargeVAT = (DropDownList)e.Item.FindControl("addBBChargeVAT");
                DropDownList ddListBankChargeVAT = (DropDownList)e.Item.FindControl("addBankChargeVAT");

                EFTChargeDB eftChargeDB = new EFTChargeDB();

                eftChargeDB.InsertChargeAccount(txtACCOUNT.Text
                                                , ParseData.StringToInt(ddListBBCharge.SelectedValue)
                                                , ParseData.StringToInt(ddListBankCharge.SelectedValue)
                                                , ParseData.StringToInt(ddListBBChargeVAT.SelectedValue)
                                                , ParseData.StringToInt(ddListBankChargeVAT.SelectedValue)
                                                , UserID
                                                , connectionString);

            }
            //if (e.CommandName == "Update")
            //{
            //    int UserID = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];
            //    TextBox txtUsername = (TextBox)e.Item.FindControl("Username");
            //    DropDownList ddlDepartment = (DropDownList)e.Item.FindControl("Department");
            //    TextBox txtContactNo = (TextBox)e.Item.FindControl("ContactNo");
            //    TextBox txtLoginID = (TextBox)e.Item.FindControl("LoginID");
            //    DropDownList txtUserRole = (DropDownList)e.Item.FindControl("Role");
            //    DropDownList ddlBranchID = (DropDownList)e.Item.FindControl("updateBranch");
            //    db.UpdateUser(UserID, txtUsername.Text, ddlDepartment.SelectedItem.Text, txtContactNo.Text, txtLoginID.Text, Int32.Parse(txtUserRole.SelectedValue), Int32.Parse(ddlBranchID.SelectedValue), Int32.Parse(Context.User.Identity.Name), GetIPAddress(), EFTN.Utility.ParseData.StringToInt(ddlDepartment.SelectedValue));
            //    MyDataGrid2.EditItemIndex = -1;


            //    UserHistoryDB userHistoryDB = new UserHistoryDB();
            //    userHistoryDB.InsertUserHistory(ParseData.StringToInt(Request.Cookies["UserID"].Value),
            //                GetIPAddress()
            //                                    , UserHistoryVar.USER_INFORMATION_UPDATED
            //                                    , UserID
            //                                    , ParseData.StringToInt(txtUserRole.SelectedValue)
            //                                    , ParseData.StringToInt(ddlBranchID.SelectedValue)
            //                                    , txtUsername.Text
            //                                    , ParseData.StringToInt(ddlDepartment.SelectedValue)
            //                                    , txtContactNo.Text
            //                                    , txtLoginID.Text
            //                                    , ""
            //                                    , UserHistoryVar.USER_INACTIVE);
            //}
        }
    }
}

