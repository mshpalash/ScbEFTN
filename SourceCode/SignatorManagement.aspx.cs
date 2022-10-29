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
using EFTN.Utility;

namespace EFTN
{
    public partial class SignatorManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string signatorName = ddListSignator.SelectedValue;
            string counterSignatorName = ddListSignatorCounter.SelectedValue;
            DateTime signatureDate = calendarSignature.SelectedDate;

            SignatureDB signatureDB = new SignatureDB();

            int signatureStatus = signatureDB.InsertStatus(signatureDate, counterSignatorName, signatorName);

            if (signatureStatus == 1)
            {
                lblMsg.Text = "Inserted successfully.";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                lblMsg.Text = "Signature already exists in the system for this date.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
