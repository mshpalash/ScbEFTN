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
using System.IO;

namespace EFTN
{
    public partial class EFTChargeManagementCity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("225"))
                {
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
}
