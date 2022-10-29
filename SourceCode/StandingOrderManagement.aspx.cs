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
using EFTN.Utility;

namespace EFTN
{
    public partial class StandingOrderManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                if (originBank.Equals("225"))
                {
                    linkBtnCityDebitAccountMgt.Visible = true;
                }
                else
                {
                    linkBtnCityDebitAccountMgt.Visible = false;
                }
            }
        }

        protected void linkBtnStandingOrderUpload_Click(object sender, EventArgs e)
        {
            string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            if (originBank.Equals("225") || originBank.Equals("290"))
            {
                Response.Redirect("StandingOrderUploadCity.aspx");
            }
            else if (originBank.Equals(OriginalBankCode.SCB))
            {
                //Response.Redirect("StandingOrderUploadSCB.aspx");
                Response.Redirect("StandingOrderEntryCredit.aspx");
            }
            else
            {
                Response.Redirect("StandingOrderUpload.aspx");
            }
        }

        protected void linkBtnCityDebitAccountMgt_Click(object sender, EventArgs e)
        {
            Response.Redirect("CityDebitAccountManagement.aspx");
        }

        protected void linkBtnStandingOrderUploadDebit_Click(object sender, EventArgs e)
        {
            Response.Redirect("StandingOrderEntryDebit.aspx");
        }
    }
}
