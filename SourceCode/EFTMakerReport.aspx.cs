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
    public partial class EFTMakerReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                if (originBank.Equals("185"))
                {
                    linkBtnEFTAdvice.Visible = true;
                }
                else
                {
                    linkBtnEFTAdvice.Visible = false;
                }

                if (originBank.Equals(OriginalBankCode.SCB))
                {
                    linkBtnHoldStatusReport.Visible = true;
                }
                else
                {
                    linkBtnHoldStatusReport.Visible = false;
                }

            }
        }

        protected void linkBtnEFTAdvice_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrintCustomerAdviceMaker.aspx");
        }

        protected void linkBtnHoldStatusReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("HoldStatusReport.aspx");
        }

        protected void linkBtnCustomerWiseReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerWiseReportMaker.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerWiseReportMaker_arc.aspx");
        }
    }
}
