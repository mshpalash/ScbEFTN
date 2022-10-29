using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace EFTN.component
{
    public class FCUBSTransactionsentBatchDB
    {
        //EFT_Update_EDRSentStatus_forBatchApprovalByAccountNo
        public void UpdateEDRSentStatusForBatchApprovalByAccountNo(int StatusID,
                                                Guid TransactionID,
                                                int ApprovedBy,
                                                string AccountNo,
                                                string CBSReference,
                                                string FloraReference)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_EDRSentStatus_forBatchApprovalByAccountNo", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myCommand.Parameters.Add(parameterApprovedBy);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
            parameterAccountNo.Value = AccountNo;
            myCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterCBSReference = new SqlParameter("@CBSReference", SqlDbType.VarChar);
            parameterCBSReference.Value = CBSReference;
            myCommand.Parameters.Add(parameterCBSReference);

            SqlParameter parameterFloraReference = new SqlParameter("@FloraReference", SqlDbType.VarChar);
            parameterFloraReference.Value = FloraReference;
            myCommand.Parameters.Add(parameterFloraReference);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public void UpdateEDRSentSuccessfulStatusForBatchApprovalByAccountNoForDebit(
                                                Guid TransactionID,
                                                int ApprovedBy,
                                                string AccountNo,
                                                string CBSReference,
                                                string FloraReference,
                                                int CBSStatus)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_EDRSentStatus_SuccessfulForBatchApprovalByAccountNoForDebit", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myCommand.Parameters.Add(parameterApprovedBy);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
            parameterAccountNo.Value = AccountNo;
            myCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterCBSReference = new SqlParameter("@CBSReference", SqlDbType.VarChar);
            parameterCBSReference.Value = CBSReference;
            myCommand.Parameters.Add(parameterCBSReference);

            SqlParameter parameterFloraReference = new SqlParameter("@FloraReference", SqlDbType.VarChar);
            parameterFloraReference.Value = FloraReference;
            myCommand.Parameters.Add(parameterFloraReference);

            SqlParameter parameterCBSStatus = new SqlParameter("@CBSStatus", SqlDbType.Int);
            parameterCBSStatus.Value = CBSStatus;
            myCommand.Parameters.Add(parameterCBSStatus);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public DataTable GetSentEDRByTransactionIDForBatchDebit(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDRByTransactionID_forBatchDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public void UpdateEDRSentSuccessfulStatusForOutwardDebitByEDRID(
                                        Guid EDRID,
                                        int ApprovedBy,
                                        string CBSReference,
                                        string FloraReference,
                                        int CBSStatus)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_EDRSentStatus_SuccessfulForOutwardDebitByEDRID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myCommand.Parameters.Add(parameterApprovedBy);

            SqlParameter parameterCBSReference = new SqlParameter("@CBSReference", SqlDbType.VarChar);
            parameterCBSReference.Value = CBSReference;
            myCommand.Parameters.Add(parameterCBSReference);

            SqlParameter parameterFloraReference = new SqlParameter("@FloraReference", SqlDbType.VarChar);
            parameterFloraReference.Value = FloraReference;
            myCommand.Parameters.Add(parameterFloraReference);

            SqlParameter parameterCBSStatus = new SqlParameter("@CBSStatus", SqlDbType.Int);
            parameterCBSStatus.Value = CBSStatus;
            myCommand.Parameters.Add(parameterCBSStatus);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();
        }

    }
}