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
    public partial class EFTCheckerAuthorizerReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void linkBtnEFTAdvice_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrintCustomerAdviceCA.aspx");
        }

        protected void linkBtnEFTAdicePrintStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdvicePrintStatusCA.aspx");
        }

        protected void linkBtnBACHBranch_Click(object sender, EventArgs e)
        {
            Response.Redirect("BACHBranchReportCA.aspx");
        }

        protected void linkBtnZoneWiseReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("ZoneWiseReportCA.aspx");
        }

    }
}
