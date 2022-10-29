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
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Msg.Text = "";
            int ResetUserID = 0;
            if ((Request.Params["ResetUserID"] != null) && (Request.Params["ResetUserID"] != ""))
            {
                ResetUserID = Int32.Parse(Request.Params["ResetUserID"]);
            }

            if (Page.IsValid)
            {
                UserDB db = new UserDB();
                try
                {
                    int updateStatus = db.ResetPassword(ResetUserID, txtNewPassword.Text);
                    if (updateStatus == 0)
                    {
                        UserHistoryDB userHistoryDB = new UserHistoryDB();
                        userHistoryDB.InsertUserHistory(ParseData.StringToInt(Request.Cookies["UserID"].Value),
                                                          GetIPAddress()
                                                        , UserHistoryVar.PASSWORD_RESET
                                                        , ResetUserID
                                                        , 0
                                                        , 0
                                                        , ""
                                                        , 0
                                                        , ""
                                                        , ""
                                                        , db.Encrypt(txtNewPassword.Text)
                                                        , UserHistoryVar.USER_INACTIVE);
                        Msg.Text = "Password Successfully Changed.";
                        Msg.ForeColor = System.Drawing.Color.Blue;
                    }
                    else if (updateStatus == 1)
                    {
                        Msg.Text = "You are not authorized to Reset this User Password";
                        Msg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch
                {
                    Msg.Text = "Failed to save";
                    Msg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManagement.aspx");
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

