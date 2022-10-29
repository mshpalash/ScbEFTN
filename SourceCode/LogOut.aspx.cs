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
using FloraSoft;
using EFTN;

namespace Check
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.fileGenerateStatus = "OPEN";


            string key = Request.Cookies["LoginID"].Value;
            UserDB.Logout(key);


            if (Request.Cookies["LoginID"] != null)
                Response.Cookies["UserIDT" + Request.Cookies["LoginID"].Value].Expires = DateTime.Now.AddDays(-1);

            //HttpContext.Current.Cache.Remove(key);
            Session.Remove(Session.SessionID);
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }
    }
}
