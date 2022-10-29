using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace FloraSoft
{
    public class MessageDB
    {
        public void InsertMessage(string BankCode, string MessageFrom, string RoutingNo, string ExpiryDate, string MessageText)
        {
            string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

            SqlConnection myConnection = new SqlConnection(EFTConnectionString);
            SqlCommand myCommand = new SqlCommand("EFT_InsertMessage", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.SmallInt, 2);
            parameterBankCode.Value = BankCode;
            myCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterMessageFrom = new SqlParameter("@MessageFrom", SqlDbType.VarChar, 50);
            parameterMessageFrom.Value = MessageFrom;
            myCommand.Parameters.Add(parameterMessageFrom);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.Int, 4);
            parameterRoutingNo.Value = RoutingNo;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterExpiryDate = new SqlParameter("@ExpiryDate", SqlDbType.Int, 4);
            parameterExpiryDate.Value = ExpiryDate;
            myCommand.Parameters.Add(parameterExpiryDate);

            SqlParameter parameterMessageText = new SqlParameter("@MessageText", SqlDbType.NVarChar, 1000);
            parameterMessageText.Value = MessageText;
            myCommand.Parameters.Add(parameterMessageText);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
        public SqlDataReader GetBranchMessages(int BranchID)
        {
            string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlConnection myConnection = new SqlConnection(EFTConnectionString);
            SqlCommand myCommand = new SqlCommand("EFT_GetBranchMessages", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            parameterBranchID.Value = BranchID;
            myCommand.Parameters.Add(parameterBranchID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public SqlDataReader GetAllMessages()
        {
            string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlConnection myConnection = new SqlConnection(EFTConnectionString);
            SqlCommand myCommand = new SqlCommand("EFT_GetAllMessages", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public void ExpireMessage(int MessageID)
        {
            string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlConnection myConnection = new SqlConnection(EFTConnectionString);
            SqlCommand myCommand = new SqlCommand("EFT_ExpireMessage", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterMessageID = new SqlParameter("@MessageID", SqlDbType.Int,4);
            parameterMessageID.Value = MessageID;
            myCommand.Parameters.Add(parameterMessageID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}

