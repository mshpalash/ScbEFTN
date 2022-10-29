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

namespace EFTN.modules
{
    public partial class MakerHeader : System.Web.UI.UserControl
    {
        protected System.Web.UI.WebControls.Label WelcomeMsg;
        protected void Page_Load(object sender, EventArgs e)
        {
            WelcomeMsg.Text = "Welcome " + Request.Cookies["UserName"].Value + " (" + Request.Cookies["RoleName"].Value + ") of " + Request.Cookies["BranchName"].Value + " branch.";
            WelcomeMsg.ForeColor = System.Drawing.Color.Blue;
            WelcomeMsg.Visible = true;
            //int RemainingDayToChangePassword = EFTN.Utility.ParseData.StringToInt(Request.Cookies["RemainingDayToChangePassword"].Value);
            //if (RemainingDayToChangePassword < 15 && RemainingDayToChangePassword > 0)
            //{
            //    lblWarningMsg.Text = "Please change your password. Your password will expire after " + RemainingDayToChangePassword + " days";
            //    lblWarningMsg.ForeColor = System.Drawing.Color.Red;
            //    lblWarningMsg.Visible = true;
            //}
            //else
            //{
                lblWarningMsg.Text = string.Empty;
            //}

            if (Request.Cookies["RoleID"].Value != "2")
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }
    }
}