using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class RejectedDebitTransactionDB
    {
        public DataTable GetTransactionSentByEntryDate_RejectedDebit(string EntryDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_TransactionSentByEntryDate_RejectedDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEntryDate = new SqlParameter("@EntryDate", SqlDbType.VarChar);
            parameterEntryDate.Value = EntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}
