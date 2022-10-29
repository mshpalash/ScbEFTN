using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class RegenerateOutwardTransactionDB
    {
        public DataTable EFTGetSentEDRByBatchSentIDForRegeneration(Guid TransactionID,
                                                                        string BankCode,
                                                                        int UserID,
                                                                        string EFTConnectionString
                                                                        )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTConnectionString);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_By_BatchSentID_Regenerate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTGetBatchesForTransactionSentRegeneration(string EffectiveEntryDate,
                                                                        string EFTConnectionString

                                                               )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTConnectionString);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_For_TransactionSent_for_Regenerate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable Get_SCBAtlasError_TransactionSentByTraceNumber(string BeginTraceNumber, string EndTraceNumber)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_SCBAtlas_TransactionSentByTraceNumber", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBeginTraceNumber = new SqlParameter("@BeginTraceNumber", SqlDbType.VarChar);
            parameterBeginTraceNumber.Value = BeginTraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginTraceNumber);

            SqlParameter parameterEndTraceNumber = new SqlParameter("@EndTraceNumber", SqlDbType.VarChar);
            parameterEndTraceNumber.Value = EndTraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterEndTraceNumber);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void DeleteSCBAtlas_TransactionSentByTraceNumber(string BeginTraceNumber, string EndTraceNumber, int UserID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DeleteSCBAtlas_TransactionSentByTraceNumber", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBeginTraceNumber = new SqlParameter("@BeginTraceNumber", SqlDbType.VarChar);
            parameterBeginTraceNumber.Value = BeginTraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginTraceNumber);

            SqlParameter parameterEndTraceNumber = new SqlParameter("@EndTraceNumber", SqlDbType.VarChar);
            parameterEndTraceNumber.Value = EndTraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterEndTraceNumber);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }
    }
}
