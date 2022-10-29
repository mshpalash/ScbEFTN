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
    public class BranchWiseTransactionStatusDB
    {
        public DataTable GetBranchwiseTransactionStatusForCredit(int Day, int Month, int Year, string BankCode)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSettlementReportBranchwise_ForCredit", myConnection);
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

        public DataTable GetBranchwiseTransactionStatusForDebit(int Day, int Month, int Year, string BankCode)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSettlementReportBranchwise_ForDebit", myConnection);
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
