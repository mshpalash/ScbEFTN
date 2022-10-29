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
    public partial class UpdateWorkingDayOnFridaySaturday : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calendarHoliday.SelectedDate = System.DateTime.Now;
                BindWorkingdayForFridaySaturdayMonthWise(System.DateTime.Now.ToString("yyyyMM"));
                lblMsg.Visible = false;
            }
        }

        public void BindWorkingdayForFridaySaturdayMonthWise(string yearMonth)
        {
            HolidayDB holidayDB = new HolidayDB();
            DataTable dtSWorkingDay = holidayDB.GetWorkingdayByMonthForFridaySaturday(yearMonth);

            //SelectedDatesCollection theDates = calendarHoliday.SelectedDates;
            //theDates.Clear();
            //for (int i = 0; i < dtSWorkingDay.Rows.Count; i++)
            //{
            //    theDates.Add(DateTime.Parse(dtSWorkingDay.Rows[i]["CalDate"].ToString()));
            //}
            dtgHolidays.DataSource = dtSWorkingDay;
            dtgHolidays.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string dayOfWeek = calendarHoliday.SelectedDate.DayOfWeek.ToString();
            if (dayOfWeek.ToUpper().Equals("FRIDAY")
                || dayOfWeek.ToUpper().Equals("SATURDAY"))
            {
                HolidayDB holidayDB = new HolidayDB();
                holidayDB.InsertWorkingDayForFridaySaturday(calendarHoliday.SelectedDate, txtHolidayDesc.Text.Trim());
                BindWorkingdayForFridaySaturdayMonthWise(calendarHoliday.SelectedDate.ToString("yyyyMM"));
                lblMsg.Visible = false;
            }
            else
            {
                lblMsg.Visible = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void calendarHoliday_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            BindWorkingdayForFridaySaturdayMonthWise(calendarHoliday.VisibleDate.ToString("yyyyMM"));
        }
    }
}
