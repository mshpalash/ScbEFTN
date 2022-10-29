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
using EFTN.component;

namespace EFTN
{
    public partial class CorToCor : System.Web.UI.Page
    {
        protected void btnCorToCorNext_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Session["PaymentTypeID"] = ddListTypeOfPayment.SelectedValue;
                Session["CompanyId"] = txtCompanyTIN.Text.Trim();
                Session["CompanyName"] = txtCompanyName.Text.Trim();

                Response.Redirect("ConsumerCommonInfo.aspx");
            }
        }
    }
}
