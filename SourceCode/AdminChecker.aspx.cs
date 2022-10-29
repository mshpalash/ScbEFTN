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

namespace EFTN
{
    public partial class AdminChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string originBankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

                if (originBankCode.Equals("115"))
                {
                    linkBtnTransactionManager.Visible = true;
                }
                else
                {
                    linkBtnTransactionManager.Visible = false;
                }
            }
        }

        protected void linkBtnTransactionManager_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransactionManager.aspx");
        }
    }
}
