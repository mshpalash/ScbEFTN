using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class ReceivedReturnRFCDB
    {
        public DataTable GetReceivedReturnForRFC(string SettlementDate)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedReturn_forRFC", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter parameterReturnReceivedSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
                    parameterReturnReceivedSettlementDate.Value = SettlementDate;
                    myAdapter.SelectCommand.Parameters.Add(parameterReturnReceivedSettlementDate);

                    DataTable dt = new DataTable();
                    myConnection.Open();
                    myAdapter.Fill(dt);
                    myConnection.Close();
                    myAdapter.Dispose();
                    myConnection.Dispose();
                    return dt;
                }
            }
        }

    }
}
