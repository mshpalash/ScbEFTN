using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using EFTN.component;
using EFTN.Utility;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EFTNAccelerator;
using System.Web;
using EFTN;

namespace FloraSoft
{
    public partial class MasterUserKillSession : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
        }

        private string GetIPAddress()
        {
            string ipaddress;
            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];

            return ipaddress;
        }

        protected void btnKillSession_Click(object sender, EventArgs e)
        {
            string key = txtLoginID.Text;
            HttpContext.Current.Cache.Remove(key);
            lblMsg.Text = "Session Killed";
        }

        protected void btn_unlocked_global_veriable_Click(object sender, EventArgs e)
        {
            Global.fileGenerateStatus = "OPEN";
            lblMsg.Text = "Reset Global varibale for File genearting";
        }

        protected void btnUnlocked_Click(object sender, EventArgs e)
        {
            string key = txtLoginID.Text;
            UserDB.Logout(key);
            lblMsg.Text = "Login ID unlocked";
        }
    }
}

