using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EFTN.component
{
    public class FCUBSRejectedTxnDB
    {
        public DataTable GetReceivedEDR_RejectedByFCUBSChecker(int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_RejectedByFCUBSChecker", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEffectiveBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterEffectiveBranchID.Value = BranchID;
            myCommand.SelectCommand.Parameters.Add(parameterEffectiveBranchID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDR_RejectedByFCUBSChecker_ForDebit(int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_RejectedByFCUBSChecker_ForDebit", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEffectiveBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterEffectiveBranchID.Value = BranchID;
            myCommand.SelectCommand.Parameters.Add(parameterEffectiveBranchID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDR_RejectedByFCUBSChecker_BankWise(int BranchID, string BankCode)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_RejectedByFCUBSChecker_Bankwise", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEffectiveBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterEffectiveBranchID.Value = BranchID;
            myCommand.SelectCommand.Parameters.Add(parameterEffectiveBranchID);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myCommand.SelectCommand.Parameters.Add(parameterBankCode);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDR_RejectedByFCUBSChecker_ForDebit_BankWise(int BranchID, string BankCode)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_RejectedByFCUBSChecker_ForDebit_Bankwise", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEffectiveBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterEffectiveBranchID.Value = BranchID;
            myCommand.SelectCommand.Parameters.Add(parameterEffectiveBranchID);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myCommand.SelectCommand.Parameters.Add(parameterBankCode);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public void TransferFCUBS_Error_To_ReceivedEDR(Guid EDRID, string TXNRemarks)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_TransferFCUBS_Err_To_ReceivedEDR", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterTXNRemarks = new SqlParameter("@TXNRemarks", SqlDbType.VarChar);
            parameterTXNRemarks.Value = TXNRemarks;
            myAdapter.SelectCommand.Parameters.Add(parameterTXNRemarks);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable GetReceivedReturn_RejectedBy_FCUBS(int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedReturn_RejectedBy_FCUBS", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myCommand.SelectCommand.Parameters.Add(parameterDepartmentID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }


        public DataTable GetReceivedReturn_Debit_RejectedBy_FCUBS(int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedReturn_Debit_RejectedBy_FCUBS", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myCommand.SelectCommand.Parameters.Add(parameterDepartmentID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public void TransferFCUBS_Err_To_ReceivedReturn(Guid ReturnID, string TXNRemarks)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_TransferFCUBS_Err_To_ReceivedReturn", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            parameterReturnID.Value = ReturnID;
            myAdapter.SelectCommand.Parameters.Add(parameterReturnID);

            SqlParameter parameterTXNRemarks = new SqlParameter("@TXNRemarks", SqlDbType.VarChar);
            parameterTXNRemarks.Value = TXNRemarks;
            myAdapter.SelectCommand.Parameters.Add(parameterTXNRemarks);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void Update_FCUBS_UnsuccessfulDebitBy_EDRID(Guid EDRID,
                                                            string CBSErrorMsg)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = connection;
            myCommand.CommandText = "EFT_Update_FCUBS_UnsuccessfulDebitBy_EDRID";
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterCBSErrorMsg = new SqlParameter("@CBSErrorMsg", SqlDbType.VarChar);
            parameterCBSErrorMsg.Value = CBSErrorMsg;
            myCommand.Parameters.Add(parameterCBSErrorMsg);

            connection.Open();
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public SqlDataReader GetCounts_RejectedByFCUBSChecker()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_GetCounts_RejectedByFCUBSChecker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;

            //SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            //SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetCounts_RejectedByFCUBSChecker", myConnection);
            //myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            //myConnection.Open();
            //DataTable dt = new DataTable();
            //myCommand.Fill(dt);

            //myConnection.Close();
            //myCommand.Dispose();
            //myConnection.Dispose();

            //return dt;
        }

    }
}