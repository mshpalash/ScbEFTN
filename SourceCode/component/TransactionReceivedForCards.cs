using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EFTN.component
{
    public class TransactionReceivedForCards
    {
        public DataTable GetFlatFileForTransactionReceived_ForCards(string SettlementJDate, string currency, int session)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionReceived_ForCards", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.VarChar);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar,3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.Int);
            paramSession.Value = session;
            myAdapter.SelectCommand.Parameters.Add(paramSession);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
            return myDT;
        }

        public DataTable GetFlatFileForReturnSent_ForCardsForSCB(string SettlementJDate)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetReturnSent_ForCards", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.VarChar);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
            return myDT;
        }

    }
}