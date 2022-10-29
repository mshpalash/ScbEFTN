using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class NonAckFileListDB
    {
        public DataTable GetNonAckTransactionSentXMLListByEntryDate(string FileEntryDate)
        {
            string spName = "EFT_NonAck_TransactionSentXMLListByEntryDate";

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterFileEntryDate = new SqlParameter("@FileEntryDate", SqlDbType.VarChar, 8);
            parameterFileEntryDate.Value = FileEntryDate;
            myAdpater.SelectCommand.Parameters.Add(parameterFileEntryDate);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }


    }
}