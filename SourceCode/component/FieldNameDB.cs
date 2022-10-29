using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class FieldNameDB
    {
        public string GetFieldName(int paymentTypeID, string fieldColumn)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_GetFieldName";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramPaymentTypeID = new SqlParameter("@PaymentTypeID", SqlDbType.Int, 4);
            paramPaymentTypeID.Value = paymentTypeID;
            command.Parameters.Add(paramPaymentTypeID);

            SqlParameter paramFieldColumn = new SqlParameter("@FieldColumn", SqlDbType.NVarChar, 50);
            paramFieldColumn.Value = fieldColumn;
            command.Parameters.Add(paramFieldColumn);

            SqlParameter paramFieldName = new SqlParameter("@FieldName", SqlDbType.NVarChar, 100);
            paramFieldName.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramFieldName);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();

            return paramFieldName.Value.ToString();
        }
    }
}
