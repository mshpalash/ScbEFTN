using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class HolidayDB
    {
        public DataTable GetHolidayByMonth(string HoliDayMonth)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetHolidayByMonth", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterHoliDayMonth = new SqlParameter("@HoliDayMonth", SqlDbType.NVarChar, 6);
            parameterHoliDayMonth.Value = HoliDayMonth;
            myAdapter.SelectCommand.Parameters.Add(parameterHoliDayMonth);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void InsertHoliday(DateTime CalDate, string Description)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertDate", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterCalDate = new SqlParameter("@CalDate", SqlDbType.DateTime);
            parameterCalDate.Value = CalDate;
            myCommand.Parameters.Add(parameterCalDate);

            SqlParameter parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 100);
            parameterDescription.Value = Description;
            myCommand.Parameters.Add(parameterDescription);

            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myConnection.Dispose();
        }


        public DataTable GetWorkingdayByMonthForFridaySaturday(string WorkingDayMonth)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetWorkingDayByMonth", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterWorkingDayMonth = new SqlParameter("@WorkingDayMonth", SqlDbType.NVarChar, 6);
            parameterWorkingDayMonth.Value = WorkingDayMonth;
            myAdapter.SelectCommand.Parameters.Add(parameterWorkingDayMonth);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void InsertWorkingDayForFridaySaturday(DateTime CalDate, string Description)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertWorkingDayForFridaySaturday", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterCalDate = new SqlParameter("@CalDate", SqlDbType.DateTime);
            parameterCalDate.Value = CalDate;
            myCommand.Parameters.Add(parameterCalDate);

            SqlParameter parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 100);
            parameterDescription.Value = Description;
            myCommand.Parameters.Add(parameterDescription);

            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myConnection.Dispose();
        }

    }
}
