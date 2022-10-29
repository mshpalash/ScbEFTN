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
    public class EFTChargeReportDB
    {
        public DataTable GetEFTChargeReportDayWise(string ConnectionString, int Day, int Month, int Year)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptChargeDaywise", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);

            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);

            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetEFTChargeReportMonthWise(string ConnectionString, int Month, int Year)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptChargeMonthwise", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);

            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetEFTChargeReportAccountWise(string ConnectionString, int Day, int Month, int Year, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptChargeAccountwise", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);

            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);

            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}
