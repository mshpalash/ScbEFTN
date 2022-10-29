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
using EFTN.Utility;

namespace EFTN
{
    public partial class CorToInd : System.Web.UI.Page
    {
        protected void btnCorToIndBatchInfo_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Session["CompanyId"] = txtCompanyTIN.Text.Trim();
                Session["CompanyName"] = txtCompanyName.Text.Trim();
                Session["PaymentTypeID"] = 6;

                Response.Redirect("ConsumerCommonInfo.aspx");
            }
        }
    }
}
