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
using System.IO;
using EFTN.Utility;
using Ionic.Zip;
using EFTN.component;

namespace EFTN
{
    public partial class StandingOrderLogChecker : System.Web.UI.Page
    {
        private static DataTable dtStandingOrderLog = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();

                ddlistDayEnd.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonthEnd.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYearEnd.SelectedValue = System.DateTime.Now.Year.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoadStandingOrderLog();
        }


        private void LoadStandingOrderLog()
        {
            string BeginDate = ddlistYear.SelectedValue.PadLeft(4, '0')
                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                + ddlistDay.SelectedValue.PadLeft(2, '0');

            string EndDate = ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                + ddlistDayEnd.SelectedValue.PadLeft(2, '0');

            StandingOrderDB stdOrderdb = new StandingOrderDB();
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            dtStandingOrderLog = stdOrderdb.GetStandingOrderLogByDateRange(BeginDate, EndDate);
            dtgStandingOrderLog.DataSource = dtStandingOrderLog;
            dtgStandingOrderLog.DataBind();
        }
        protected void dtgStandingOrderLog_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgStandingOrderLog.CurrentPageIndex = e.NewPageIndex;
            dtgStandingOrderLog.DataSource = dtStandingOrderLog;
            dtgStandingOrderLog.DataBind();
        }

    }
}
