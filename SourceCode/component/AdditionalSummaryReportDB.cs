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
    public class AdditionalSummaryReportDB
    {
        public DataTable GetInwardCreditSummary(int Day,
                                                int Month,
                                                int Year,
                                                string BankCode)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetJanataInwardCredit", myConnection);
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

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetJanataInwardCreditExcel(int Day,
                                        int Month,
                                        int Year,
                                        string BankCode)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetJanataInwardCreditExcel", myConnection);
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

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetInwardDebitSummary(int Day,
                                                int Month,
                                                int Year,
                                                string BankCode)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetJanataInwardDebit", myConnection);
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

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetOutwardCreditSummary(int Day,
                                                int Month,
                                                int Year,
                                                string BankCode)
        {
            // Must enter your connection string
                        SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetJanataOutwardCredit", myConnection);
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

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetOutwardDebitSummary(int Day,
                                                int Month,
                                                int Year,
                                                string BankCode)
        {
            // Must enter your connection string
                        SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetJanataOutwardDebit", myConnection);
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

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetInwardTransactionStatusSummaryReport(int Day,
                                                int Month,
                                                int Year,
                                                string BankCode)
        {
            // Must enter your connection string
                        SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetJanMonitorReportBranchwise", myConnection);
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

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReturnSentStatusSummaryReport(int Day,
                                                int Month,
                                                int Year,
                                                string BankCode)
        {
            // Must enter your connection string
                        SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetJanMonitorReportBranchwiseForReturnSent", myConnection);
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

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetFlatFileInwardCreditJanata(int Day,
                                int Month,
                                int Year,
                                string BankCode)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFileInwardCreditJanata", myConnection);
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

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetFlatFileInwardDebitJanata(int Day,
                        int Month,
                        int Year,
                        string BankCode)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFileInwardDebitJanata", myConnection);
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

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}
