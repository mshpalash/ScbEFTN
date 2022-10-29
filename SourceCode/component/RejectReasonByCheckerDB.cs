using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class RejectReasonByCheckerDB
    {
        public void Insert_RejectReason_ByChecker(Guid EDRSentID, string rejectReason,int itemType)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "[EFT_Insert_RejectReason_ByChecker]";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEDRSentID = new SqlParameter("@EDRSentID", SqlDbType.UniqueIdentifier);
            paramEDRSentID.Value = EDRSentID;
            command.Parameters.Add(paramEDRSentID);

            SqlParameter paramRejectReason = new SqlParameter("@RejectReason", SqlDbType.NVarChar,50);
            paramRejectReason.Value = rejectReason;
            command.Parameters.Add(paramRejectReason);

            SqlParameter paramItemType = new SqlParameter("@ItemType", SqlDbType.TinyInt);
            paramItemType.Value = itemType;
            command.Parameters.Add(paramItemType);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
            command.Dispose();
            connection.Dispose();



        }

        public void ClearRejectReeason(Guid EDRSentID)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_ClearRejectReeason";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEDRSentID = new SqlParameter("@EDRSentID", SqlDbType.UniqueIdentifier);
            paramEDRSentID.Value = EDRSentID;
            command.Parameters.Add(paramEDRSentID);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
            command.Dispose();
            connection.Dispose();

        }
    }
}
