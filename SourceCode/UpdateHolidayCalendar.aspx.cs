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
    public partial class UpdateHolidayCalendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calendarHoliday.SelectedDate = System.DateTime.Now;
                BindHolidayCalendarMonthWise(System.DateTime.Now.ToString("yyyyMM"));
            }
        }

        public void BindHolidayCalendarMonthWise(string yearMonth)
        {
            HolidayDB holidayDB = new HolidayDB();
            dtgHolidays.DataSource = holidayDB.GetHolidayByMonth(yearMonth);
            dtgHolidays.DataBind();
            //DataTable dtHoliday = holidayDB.GetHolidayByMonth(yearMonth);
            //DateTime[] holidays = new DateTime[dtHoliday.Rows.Count];
            //int columnNo = 0;
            //foreach (DataRow dr in dtHoliday.Rows)
            //{
            //    holidays[columnNo] = DateTime.Parse(dr["CalDate"].ToString());
            //    columnNo++;
            //    calendarHoliday.SelectedDate = DateTime.Parse(dr["CalDate"].ToString());
            //}

            ////calendarHoliday.SelectedDates = holidays[1];

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            HolidayDB holidayDB = new HolidayDB();
            holidayDB.InsertHoliday(calendarHoliday.SelectedDate, txtHolidayDesc.Text.Trim());
            BindHolidayCalendarMonthWise(calendarHoliday.SelectedDate.ToString("yyyyMM"));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}
