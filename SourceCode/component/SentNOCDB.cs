using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class SentNOCDB
    {
        public DataTable GetSentNOCForChecker(int BranchID)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentNOC_ForChecker", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;


                    SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                    parameterBranchID.Value = BranchID;
                    myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

                    //SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
                    //parameterEDRID.Value = EDRID;
                    //myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

                    DataTable dt = new DataTable();

                    myConnection.Open();
                    myAdapter.Fill(dt);
                    myConnection.Close();
                    myAdapter.Dispose();
                    myConnection.Dispose();

                    return dt;
                }
            }
        }

        public DataTable GetSentNOCForCheckerForDebit(int BranchID)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentNOC_ForCheckerForDebit", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                    parameterBranchID.Value = BranchID;
                    myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

                    DataTable dt = new DataTable();

                    myConnection.Open();
                    myAdapter.Fill(dt);
                    myConnection.Close();
                    myAdapter.Dispose();
                    myConnection.Dispose();

                    return dt;
                }
            }
        }

        public void UpdateNOCSentStatus(int statusId, Guid NOCID, int ApprovedBy)
        {
            using (SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "EFT_Update_NOCSent_Status";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
                    paramStatusID.Value = statusId;
                    command.Parameters.Add(paramStatusID);

                    SqlParameter paramNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
                    paramNOCID.Value = NOCID;
                    command.Parameters.Add(paramNOCID);

                    SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
                    parameterApprovedBy.Value = ApprovedBy;
                    command.Parameters.Add(parameterApprovedBy); 
                    
                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }

        }

        //public SqlDataReader GetSentNOCByCheckerRejection()
        //{
        //    SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandText = "EFT_GetSentNOC_ByCheckerRejection";
        //    command.CommandType = CommandType.StoredProcedure;



        //    connection.Open();

        //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //    return reader;
        //}

        public DataTable GetSentNOCByCheckerRejection(int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentNOC_ByCheckerRejection", myConnection);
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

        public DataTable GetSentNOCByCheckerRejectionForDebit(int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentNOC_ByCheckerRejection_ForDebit", myConnection);
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

        //public SqlDataReader GetSentNOC_By_NOCID(Guid NOCID)
        //{
        //    SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandText = "EFT_GetSentNOC_By_NOCSentID";
        //    command.CommandType = CommandType.StoredProcedure;


        //    SqlParameter paramNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
        //    paramNOCID.Value = NOCID;
        //    command.Parameters.Add(paramNOCID);
        //    connection.Open();

        //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //    return reader;

        //}

        public DataTable GetSentNOC_By_NOCID(Guid NOCID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentNOC_By_NOCSentID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
            parameterNOCID.Value = NOCID;
            myAdapter.SelectCommand.Parameters.Add(parameterNOCID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateNOCByNOCID(Guid nocID, int statusId, string changeCode, string correctedData, int CreatedBy)
        {
            using (SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "EFT_Update_NOCSent_By_NOCSentID";
                    command.CommandType = CommandType.StoredProcedure;


                    SqlParameter paramNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
                    paramNOCID.Value = nocID;
                    command.Parameters.Add(paramNOCID);


                    SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
                    paramStatusID.Value = statusId;
                    command.Parameters.Add(paramStatusID);


                    SqlParameter paramChangeCode = new SqlParameter("@ChangeCode", SqlDbType.NVarChar, 3);
                    paramChangeCode.Value = changeCode;
                    command.Parameters.Add(paramChangeCode);

                    SqlParameter paramCorrectedData = new SqlParameter("@CorrectedData", SqlDbType.NVarChar, 30);
                    paramCorrectedData.Value = correctedData;
                    command.Parameters.Add(paramCorrectedData);

                    SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
                    parameterCreatedBy.Value = CreatedBy;
                    command.Parameters.Add(parameterCreatedBy);

                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }

        //public SqlDataReader GetSentNOCByReceivedBatchID(Guid TransactionID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetSentNOC_By_ReceivedBatchID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
        //    parameterTransactionID.Value = TransactionID;
        //    myCommand.Parameters.Add(parameterTransactionID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetSentNOCByReceivedBatchID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentNOC_By_ReceivedBatchID", myConnection);
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

        public DataTable GetSentNOCForEBBSChecker(string EffectiveEntryDate, string EndingEffectiveEntryDate, int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentNOC_ForEBBSChecker", myConnection);
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

        public DataTable GetSentNOCForEBBSCheckerBySettlementJDate(string EffectiveEntryDate, int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentNOC_ForEBBSCheckerBySettlementJDate", myConnection);
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

        //public SqlDataReader GetSentNOCForEBBSChecker(string EffectiveEntryDate, int BranchID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetSentNOC_ForEBBSChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
        //    parameterEffectiveEntryDate.Value = EffectiveEntryDate;
        //    myCommand.Parameters.Add(parameterEffectiveEntryDate);

        //    SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
        //    parameterBranchID.Value = BranchID;
        //    myCommand.Parameters.Add(parameterBranchID);

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public void UpdateNOCSentStatusApprovedByEBBSChecker(Guid NOCID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_NOCSent_Status_ApprovedByEBBSChecker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
            parameterNOCID.Value = NOCID;
            myCommand.Parameters.Add(parameterNOCID);


            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public void UpdateNOCSentStatusRejectedByEBBSChecker(Guid NOCID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_NOCSent_Status_RejectedByEBBSChecker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
            parameterNOCID.Value = NOCID;
            myCommand.Parameters.Add(parameterNOCID);

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        //public SqlDataReader GetSentNOCForEBBSCheckerByNOCID(Guid NOCID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetSentNOC_ForEBBSChecker_byNOCID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
        //    parameterNOCID.Value = NOCID;
        //    myCommand.Parameters.Add(parameterNOCID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetSentNOCForEBBSCheckerByNOCID(Guid NOCID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentNOC_ForEBBSChecker_byNOCID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
            parameterNOCID.Value = NOCID;
            myAdapter.SelectCommand.Parameters.Add(parameterNOCID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}
