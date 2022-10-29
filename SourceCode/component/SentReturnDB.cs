using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class SentReturnDB
    {
        //public SqlDataReader GetSentRRForChecker(int BranchID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetSentRR_ForChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
        //    parameterBranchID.Value = BranchID;
        //    myCommand.Parameters.Add(parameterBranchID);

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetSentRRForChecker(int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_ForChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentRRForChecker_ForDebit(int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_ForChecker_ForDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentRRForChecker_ForSCB(int BranchID, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_ForChecker_forSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentRRForChecker_ForDebit_ForSCB(int BranchID, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_ForChecker_ForDebit_forSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateReturnSentStatus(int statusId, Guid ReturnID, int ApprovedBy)
        {
            using (SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "EFT_Update_RRSent_Status";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
                    paramStatusID.Value = statusId;
                    command.Parameters.Add(paramStatusID);

                    SqlParameter paramReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
                    paramReturnID.Value = ReturnID;
                    command.Parameters.Add(paramReturnID);

                    SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
                    parameterApprovedBy.Value = ApprovedBy;
                    command.Parameters.Add(parameterApprovedBy);

                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }

        }

        public void UpdateReturnSentByRRSent(Guid ReturnID, int statusId, string returnCode,
                                                string CorrectedData, int CreatedBy)
        {
            using (SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "EFT_Update_RRSent_By_RRSentID";
                    command.CommandType = CommandType.StoredProcedure;


                    SqlParameter paramReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
                    paramReturnID.Value = ReturnID;
                    command.Parameters.Add(paramReturnID);


                    SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
                    paramStatusID.Value = statusId;
                    command.Parameters.Add(paramStatusID);


                    SqlParameter paramReturnCode = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 3);
                    paramReturnCode.Value = returnCode;
                    command.Parameters.Add(paramReturnCode);

                    SqlParameter parameterCorrectedData = new SqlParameter("@CorrectedData", SqlDbType.NVarChar, 30);
                    parameterCorrectedData.Value = CorrectedData;
                    command.Parameters.Add(parameterCorrectedData);

                    SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
                    parameterCreatedBy.Value = CreatedBy;
                    command.Parameters.Add(parameterCreatedBy);



                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }

        }

        //public SqlDataReader GetSentRRByCheckerRejection()
        //{
        //    SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandText = "EFT_GetSentRR_ByCheckerRejection";
        //    command.CommandType = CommandType.StoredProcedure;



        //    connection.Open();

        //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //    return reader;
        //}

        public DataTable GetSentRRByCheckerRejection(int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_ByCheckerRejection", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentRRByCheckerRejectionForDebit(int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_ByCheckerRejection_ForDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        //public SqlDataReader GetSentRR_By_RRSentID(Guid ReturnID)
        //{
        //    SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandText = "EFT_GetSentRR_By_RRSentID";
        //    command.CommandType = CommandType.StoredProcedure;


        //    SqlParameter paramReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
        //    paramReturnID.Value = ReturnID;
        //    command.Parameters.Add(paramReturnID);
        //    connection.Open();

        //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //    return reader;

        //}

        public DataTable GetSentRR_By_RRSentID(Guid ReturnID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_By_RRSentID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            parameterReturnID.Value = ReturnID;
            myAdapter.SelectCommand.Parameters.Add(parameterReturnID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetSentRR_By_ReceivedBatchID(Guid transactionId)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand();
        //    myCommand.Connection = myConnection;
        //    myCommand.CommandText = "EFT_GetSentRR_By_ReceivedBatchID";
        //    myCommand.CommandType = CommandType.StoredProcedure;
        //    myCommand.CommandTimeout = 600;


        //    SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
        //    parameterTransactionID.Value = transactionId;
        //    myCommand.Parameters.Add(parameterTransactionID);


        //    myConnection.Open();

        //    SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

        //    return reader;
        //}

        public DataTable GetSentRR_By_ReceivedBatchID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_By_ReceivedBatchID", myConnection);
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

        public DataTable GetReturnSentFlatFileData()
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_FlatFile_GetReturnSent", myConnection))
                {
                    myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdpater.SelectCommand.CommandTimeout = 3600;

                    SqlParameter paramOriginBank = new SqlParameter("@OriginBank", SqlDbType.NVarChar, 9);
                    paramOriginBank.Value = ConfigurationManager.AppSettings["OriginBank"];
                    myAdpater.SelectCommand.Parameters.Add(paramOriginBank);

                    DataTable dt = new DataTable();

                    myConnection.Open();

                    myAdpater.Fill(dt);

                    myConnection.Close();

                    return dt;
                }
            }
        }

        //public SqlDataReader GetSentRRForEBBSChecker(string EffectiveEntryDate, int BranchID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetSentRR_ForEBBSChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar, 8);
        //    parameterEffectiveEntryDate.Value = EffectiveEntryDate;
        //    myCommand.Parameters.Add(parameterEffectiveEntryDate);

        //    SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
        //    parameterBranchID.Value = BranchID;
        //    myCommand.Parameters.Add(parameterBranchID);

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetSentRRForEBBSChecker(string EffectiveEntryDate, string EndingEffectiveEntryDate, int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_ForEBBSChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterEndingEffectiveEntryDate = new SqlParameter("@EndingEffectiveEntryDate", SqlDbType.VarChar);
            parameterEndingEffectiveEntryDate.Value = EndingEffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndingEffectiveEntryDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentRRForEBBSChecker_ForDebit(string EffectiveEntryDate, string EndingEffectiveEntryDate, int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_ForEBBSChecker_ForDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterEndingEffectiveEntryDate = new SqlParameter("@EndingEffectiveEntryDate", SqlDbType.VarChar);
            parameterEndingEffectiveEntryDate.Value = EndingEffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndingEffectiveEntryDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentRRForEBBSChecker_ForSCB(string EffectiveEntryDate, string EndingEffectiveEntryDate, int BranchID, int DepartmentID, string currency)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_ForEBBSChecker_forSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterEndingEffectiveEntryDate = new SqlParameter("@EndingEffectiveEntryDate", SqlDbType.VarChar);
            parameterEndingEffectiveEntryDate.Value = EndingEffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndingEffectiveEntryDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentRRForEBBSChecker_ForDebit_ForSCB(string EffectiveEntryDate, string EndingEffectiveEntryDate, int BranchID, int DepartmentID, string currency)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_ForEBBSChecker_ForDebit_forSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterEndingEffectiveEntryDate = new SqlParameter("@EndingEffectiveEntryDate", SqlDbType.VarChar);
            parameterEndingEffectiveEntryDate.Value = EndingEffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndingEffectiveEntryDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar,3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            //SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.Int);
            //paramSession.Value = session;
            //myAdapter.SelectCommand.Parameters.Add(paramSession);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentRRForEBBSCheckerBySettelementJDate(string EffectiveEntryDate, int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_ForEBBSCheckerBySettelementJDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetSentRRForEBBSCheckerByReturnID(Guid ReturnID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetSentRR_ForEBBSChecker_byReturnID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
        //    parameterReturnID.Value = ReturnID;
        //    myCommand.Parameters.Add(parameterReturnID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetSentRRForEBBSCheckerByReturnID(Guid ReturnID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_ForEBBSChecker_byReturnID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            parameterReturnID.Value = ReturnID;
            myAdapter.SelectCommand.Parameters.Add(parameterReturnID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateRRSentStatusApprovedByEBBSChecker(Guid ReturnID, int approvedBy)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_RRSent_Status_ApprovedByEBBSChecker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            parameterReturnID.Value = ReturnID;
            myCommand.Parameters.Add(parameterReturnID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = approvedBy;
            myCommand.Parameters.Add(parameterApprovedBy);

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public void UpdateRRSentStatusRejectedByEBBSChecker(Guid ReturnID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_RRSent_Status_RejectedByEBBSChecker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            parameterReturnID.Value = ReturnID;
            myCommand.Parameters.Add(parameterReturnID);


            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public DataTable EFTGetSentRRForCAbyBatchID(Guid TransactionID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentRR_ForCAbyBatchID", myConnection);
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
    }
}
