using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System;
using EFTN.Utility;

namespace EFTN.component
{
    public class RTServiceDB
    {
        public DataTable GetSentEDRByBatchSentID_forNrbRTServ(Guid EDRID,
                                string BankCode)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_By_BatchSentID_forNrbRTServ", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetReceivedReturn_Approved_ForNrbRTService(Guid ReturnID)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ReceivedReturn_Approved_ForNrbRTService", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            parameterReturnID.Value = ReturnID;
            myAdapter.SelectCommand.Parameters.Add(parameterReturnID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReceivedTransactionApprovedForNrbRTService(Guid EDRID)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ForCheckerForRtServiceByEDRID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentEDR_ForCheckerForAll(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ForChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentEDR_ForCheckerForCredit(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ForCheckerForCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentEDR_ForCheckerForDebit(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ForCheckerForDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetFCUBS_Acc_Service_Conf(string AccountTranType)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Get_FCUBS_Acc_Service_Conf", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            SqlParameter parameterAccountTranType = new SqlParameter("@AccountTranType", SqlDbType.VarChar);
            parameterAccountTranType.Value = AccountTranType;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountTranType);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void InsertFCUBS_Err_For_ReceivedEDR(Guid EDRID, string ErrorMsg)
        {
            SqlConnection connection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertFCUBS_Err_For_ReceivedEDR";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Value = EDRID;
            command.Parameters.Add(paramEDRID);

            SqlParameter paramErrorMsg = new SqlParameter("@ErrorMsg", SqlDbType.NVarChar, 50);
            paramErrorMsg.Value = ErrorMsg;
            command.Parameters.Add(paramErrorMsg);
            connection.Open();

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();

        }

        public DataTable GetTransactionSentApprovedForCheckerFCUBSRTService(Guid EDRID)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ForCheckerForRtServiceByEDRID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetDistinctAccount_For_TransactionSent_forFCUBS(Guid TransactionID)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetDistinctAccount_For_TransactionSent_forFCUBS", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBatchesToCreditFCUBSForOutwardDebitBySettlementDate(int DepartmentID, string SettlementDate)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_To_CreditFCUBS_ForOutwardDebitBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetReceivedEDR_ApprovedByMaker_ForNrbRTServ(Guid EDRID)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMaker_ForNrbRTServ", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void InsertFCUBS_Err_For_ReceivedReturn(Guid ReturnID, string ErrorMsg)
        {
            SqlConnection connection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertFCUBS_Err_For_ReceivedReturn";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            paramReturnID.Value = ReturnID;
            command.Parameters.Add(paramReturnID);

            SqlParameter paramErrorMsg = new SqlParameter("@ErrorMsg", SqlDbType.NVarChar, 200);
            paramErrorMsg.Value = ErrorMsg;
            command.Parameters.Add(paramErrorMsg);
            connection.Open();

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();

        }


        public void UpdateReceivedReturnReferenceForFCUBS(Guid ReturnID, string FloraReference, string CBSReference)
        {
            SqlConnection connection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_UpdateReceivedReturnReferenceForFCUBS";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            paramReturnID.Value = ReturnID;
            command.Parameters.Add(paramReturnID);

            SqlParameter paramFloraReference = new SqlParameter("@FloraReference", SqlDbType.NVarChar);
            paramFloraReference.Value = FloraReference;
            command.Parameters.Add(paramFloraReference);

            SqlParameter paramCBSReference = new SqlParameter("@CBSRTReference", SqlDbType.NVarChar);
            paramCBSReference.Value = CBSReference;
            command.Parameters.Add(paramCBSReference);
            
            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public void UpdateTransactionSentReferenceForFCUBS(Guid EDRID, string FloraReference)
        {
            SqlConnection connection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_UpdateTransactionSentReferenceForFCUBS";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Value = EDRID;
            command.Parameters.Add(paramEDRID);

            SqlParameter paramFloraReference = new SqlParameter("@FloraReference", SqlDbType.NVarChar);
            paramFloraReference.Value = FloraReference;
            command.Parameters.Add(paramFloraReference);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public DataTable GetUnsuccessfulTransactionSentDebit_forFCUBS(Guid TransactionID)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetUnsuccessful_TransactionSent_Debit_forFCUBS", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateIBankingReturnACKStatus(Guid ReturnID, bool IBankingAck)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_Update_IBankingReturnACKStatus", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            parameterReturnID.Value = ReturnID;
            myCommand.Parameters.Add(parameterReturnID);

            SqlParameter parameterIBankingAck = new SqlParameter("@IBankingAck", SqlDbType.Bit);
            parameterIBankingAck.Value = IBankingAck;
            myCommand.Parameters.Add(parameterIBankingAck);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
            myConnection.Close();
            myConnection.Dispose();
        }
    }
}