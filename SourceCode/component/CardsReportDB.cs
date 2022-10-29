using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EFTN.component
{
    public class CardsReportDB
    {
        public DataTable GetReportForTransactionReceivedBySettlementDateForCards(string SettlementDate,string Currency,string Session)
        {
            string spName = string.Empty;

            spName = "EFT_Rpt_TransactionReceivedBySettlementDate_ForCards";

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }
    }
}