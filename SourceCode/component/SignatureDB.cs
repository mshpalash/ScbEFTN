using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System;

namespace EFTN.component
{
    public class SignatureDB
    {
        public int InsertStatus(DateTime SignDate,
                                    string CounterSignator,
                                    string Signator)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_InsertSignator", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterSignDate = new SqlParameter("@SignDate", SqlDbType.DateTime);
            parameterSignDate.Value = SignDate;
            myCommand.Parameters.Add(parameterSignDate);

            SqlParameter parameterCounterSignator = new SqlParameter("@CounterSignator", SqlDbType.NVarChar, 50);
            parameterCounterSignator.Value = CounterSignator;
            myCommand.Parameters.Add(parameterCounterSignator);

            SqlParameter parameterSignator = new SqlParameter("@Signator", SqlDbType.NVarChar, 50);
            parameterSignator.Value = Signator;
            myCommand.Parameters.Add(parameterSignator);

            SqlParameter parameterInsertStatus = new SqlParameter("@InsertStatus", SqlDbType.Int);
            parameterInsertStatus.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterInsertStatus);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            return (int)parameterInsertStatus.Value;
        }

        public SqlDataReader GetSignator(string SignDate)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_GetSignator", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterSignDate = new SqlParameter("@SignDate", SqlDbType.VarChar);
            parameterSignDate.Value = SignDate;
            myCommand.Parameters.Add(parameterSignDate);


            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
    }
}
