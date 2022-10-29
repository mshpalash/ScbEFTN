using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class CodeLookUpDB
    {
        //public SqlDataReader GetCodeLookUp(int type)
        //{
        //    SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandText = "[EFT_GetCodeLookUp_ByType]";
        //    command.CommandType = CommandType.StoredProcedure;


        //    SqlParameter paramRejectReasonType = new SqlParameter("@RejectReasonType", SqlDbType.Int);
        //    paramRejectReasonType.Value = type;
        //    command.Parameters.Add(paramRejectReasonType);

        //    connection.Open();

        //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //    return reader;
        //}

        public DataTable GetCodeLookUp(int RejectReasonType)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetCodeLookUp_ByType", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterRejectReasonType = new SqlParameter("@RejectReasonType", SqlDbType.Int);
            parameterRejectReasonType.Value = RejectReasonType;
            myAdapter.SelectCommand.Parameters.Add(parameterRejectReasonType);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

    }
}
