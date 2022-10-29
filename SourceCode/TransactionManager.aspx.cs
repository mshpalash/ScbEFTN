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
    public partial class TransactionManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistYear.SelectedValue = System.DateTime.Today.Year.ToString();
                ddlistMonth.SelectedValue = System.DateTime.Today.Month.ToString();
                ddlistDay.SelectedValue = System.DateTime.Today.Day.ToString().PadLeft(2, '0');
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int DeletedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            int transactionType = ParseData.StringToInt(ddListTransactionType.SelectedValue);
            TransactionManagerDB trMgrDB = new TransactionManagerDB();
            trMgrDB.DeleteInwardBySettlementDate(transactionType,
                                                      ddlistYear.SelectedValue.PadLeft(4, '0')
                                                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                    + ddlistDay.SelectedValue.PadLeft(2, '0'), DeletedBy);
            lblMsg.Text = "Deleted Successfully";
        }

    }
}
