using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class ReceivedReturnDB
    {
        public Guid InsertReturnReceived
            (                
        	string orgTraceNumber
			,string traceNumber 
            ,string returnCode 
			,string addendaInfo 
            ,string dateOfDeath
            ,DateTime SettlementJDate
            ,decimal XMLAmount
            , string XMLFileName
            , string currency
            ,int session)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertReturnReceived", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter paramReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
                    paramReturnID.Direction = ParameterDirection.Output;
                    myAdapter.SelectCommand.Parameters.Add(paramReturnID);

                    SqlParameter paramOrgTraceNumber = new SqlParameter("@OrgTraceNumber", SqlDbType.NVarChar, 15);
                    paramOrgTraceNumber.Value = orgTraceNumber;
                    myAdapter.SelectCommand.Parameters.Add(paramOrgTraceNumber);

                    SqlParameter paramTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
                    paramTraceNumber.Value = traceNumber;
                    myAdapter.SelectCommand.Parameters.Add(paramTraceNumber);

                    SqlParameter paramReturnCode = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 3);
                    paramReturnCode.Value = returnCode;
                    myAdapter.SelectCommand.Parameters.Add(paramReturnCode);

                    SqlParameter paramAddendaInfo = new SqlParameter("@AddendaInfo", SqlDbType.NVarChar, 15);
                    paramAddendaInfo.Value = addendaInfo;
                    myAdapter.SelectCommand.Parameters.Add(paramAddendaInfo);

                    SqlParameter paramDateOfDeath = new SqlParameter("@DateOfDeath", SqlDbType.NVarChar, 15);
                    paramDateOfDeath.Value = dateOfDeath;
                    myAdapter.SelectCommand.Parameters.Add(paramDateOfDeath);

                    SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.DateTime);
                    parameterSettlementJDate.Value = SettlementJDate;
                    myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);

                    SqlParameter parameterXMLAmount = new SqlParameter("@XMLAmount", SqlDbType.Money);
                    parameterXMLAmount.Value = XMLAmount;
                    myAdapter.SelectCommand.Parameters.Add(parameterXMLAmount);

                    SqlParameter parameterXMLFileName = new SqlParameter("@XMLFileName", SqlDbType.NVarChar, 200);
                    parameterXMLFileName.Value = XMLFileName;
                    myAdapter.SelectCommand.Parameters.Add(parameterXMLFileName);

                    SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
                    paramCurrency.Value = currency;
                    myAdapter.SelectCommand.Parameters.Add(paramCurrency);

                    SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.Int, 2);
                    paramSession.Value = session;
                    myAdapter.SelectCommand.Parameters.Add(paramSession);

                    myConnection.Open();
                    myAdapter.SelectCommand.ExecuteNonQuery();

                    myConnection.Close();

                    return (Guid)paramReturnID.Value;
                }
            }

        }

        public DataTable GetReceivedReturn(int DepartmentID)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedReturn", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;


                    SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    parameterDepartmentID.Value = DepartmentID;
                    myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

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

        public DataTable GetReceivedReturn_ForDebit(int DepartmentID)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedReturn_ForDebit", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;


                    SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    parameterDepartmentID.Value = DepartmentID;
                    myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

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

        public DataTable GetReceivedReturnForTraceNumberMismatch()
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedReturnForTraceNumberMismatch", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

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

        public void Update_ReceivedReturn_Status
            (
                int statusID,
	            Guid returnID,   
	            string dishonorReason, 
	            int createdBy,
                string addendaInfo 
            )
            
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("[EFT_Update_ReceivedReturn_Status]", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
                    paramStatusID.Value = statusID;
                    myAdapter.SelectCommand.Parameters.Add(paramStatusID);

                    SqlParameter paramReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
                    paramReturnID.Value = returnID;
                    myAdapter.SelectCommand.Parameters.Add(paramReturnID);

                    SqlParameter paramDishonoredID = new SqlParameter("@DishonoredID", SqlDbType.UniqueIdentifier);
                    paramDishonoredID.Direction = ParameterDirection.Output;
                    myAdapter.SelectCommand.Parameters.Add(paramDishonoredID);

                    SqlParameter paramDishonorReason = new SqlParameter("@DishonorReason", SqlDbType.NVarChar, 3);
                    paramDishonorReason.Value = dishonorReason;
                    myAdapter.SelectCommand.Parameters.Add(paramDishonorReason);

                    SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
                    paramCreatedBy.Value = createdBy;
                    myAdapter.SelectCommand.Parameters.Add(paramCreatedBy);

                    SqlParameter paramAddendaInfo = new SqlParameter("@AddendaInfo", SqlDbType.NVarChar, 30);
                    paramAddendaInfo.Value = addendaInfo;
                    myAdapter.SelectCommand.Parameters.Add(paramAddendaInfo);


                    myConnection.Open();

                    myAdapter.SelectCommand.ExecuteNonQuery();
                    myAdapter.Dispose();
                    myConnection.Close();
                    myConnection.Dispose();
                }
            }
        }

        //public SqlDataReader ReceivedReturn_Approved_ForChecker()
        //{
        //    SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandText = "EFT_ReceivedReturn_Approved_ForChecker";
        //    command.CommandType = CommandType.StoredProcedure;

        //    connection.Open();

        //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //    return reader;
        //}

        public DataTable ReceivedReturn_Approved_ForChecker(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ReceivedReturn_Approved_ForChecker", myConnection);
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

        public DataTable ReceivedReturn_Approved_ForChecker_ForDebit(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ReceivedReturn_Approved_ForChecker_ForDebit", myConnection);
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

        public void Update_ReceivedReturn_Status_ByChecker(int statusID, Guid ReturnID)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Update_ReceivedReturn_Status_ByChecker", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
                    paramStatusID.Value = statusID;
                    myAdapter.SelectCommand.Parameters.Add(paramStatusID);

                    SqlParameter paramReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
                    paramReturnID.Value = ReturnID;
                    myAdapter.SelectCommand.Parameters.Add(paramReturnID);

                    myConnection.Open();
                    myAdapter.SelectCommand.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
        }

        public void UpdateReceivedReturnStatusByEBBSChecker(int statusID, Guid ReturnID,int approvedBy)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Update_ReceivedReturn_Status_ByEBBSChecker", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
                    paramStatusID.Value = statusID;
                    myAdapter.SelectCommand.Parameters.Add(paramStatusID);

                    SqlParameter paramReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
                    paramReturnID.Value = ReturnID;
                    myAdapter.SelectCommand.Parameters.Add(paramReturnID);

                    SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
                    parameterApprovedBy.Value = approvedBy;
                    myAdapter.SelectCommand.Parameters.Add(parameterApprovedBy);

                    myConnection.Open();
                    myAdapter.SelectCommand.ExecuteNonQuery();
                    myConnection.Close();
                    myAdapter.Dispose();
                    myConnection.Dispose();
                }
            }
        }

        public void UpdateDishonorSentRejectedByCheckerStatusByMaker(int StatusID,
                                                                        Guid ReturnID,
                                                                        string DishonorReason,
                                                                        string AddendaInfo)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_DishonorSentRejectedByChecker_Status_ByMaker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            parameterReturnID.Value = ReturnID;
            myCommand.Parameters.Add(parameterReturnID);

            SqlParameter parameterDishonorReason = new SqlParameter("@DishonorReason", SqlDbType.NVarChar, 3);
            parameterDishonorReason.Value = DishonorReason;
            myCommand.Parameters.Add(parameterDishonorReason);

            SqlParameter parameterAddendaInfo = new SqlParameter("@AddendaInfo", SqlDbType.NVarChar, 30);
            parameterAddendaInfo.Value = AddendaInfo;
            myCommand.Parameters.Add(parameterAddendaInfo);

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable GetReceivedReturnApprovedForEBBSChecker(int DepartmentID)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ReceivedReturn_Approved_ForEBBSChecker", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    parameterDepartmentID.Value = DepartmentID;
                    myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

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

        public DataTable GetReceivedReturnApprovedForEBBSChecker_ForDebit(int DepartmentID)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ReceivedReturn_Approved_ForEBBSChecker_ForDebit", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    parameterDepartmentID.Value = DepartmentID;
                    myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

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
        public DataTable GetReceivedReturnbyReturnID(Guid ReturnID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedReturnbyReturnID", myConnection);
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

        //public SqlDataReader GetReceivedReturnbyReturnIDForCBSChecker(Guid ReturnID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetReceivedReturnbyReturnID_forEBBSChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
        //    parameterReturnID.Value = ReturnID;
        //    myCommand.Parameters.Add(parameterReturnID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetReceivedReturnbyReturnIDForCBSChecker(Guid ReturnID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedReturnbyReturnID_forEBBSChecker", myConnection);
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

        public DataTable ReceivedReturnApprovedForEBBSCheckerBySettlementDate(string ReturnReceivedSettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ReceivedReturn_Approved_ForEBBSCheckerBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterReturnReceivedSettlementDate = new SqlParameter("@ReturnReceivedSettlementDate", SqlDbType.VarChar);
            parameterReturnReceivedSettlementDate.Value = ReturnReceivedSettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterReturnReceivedSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetReceivedReturn_AccInfo_ForFCUBS(Guid ReturnID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedReturn_AccInfo_ForFCUBS", myConnection);
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

        public DataTable GetReceivedReturn_Missing()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("Eft_GetMissingReceivedReturn", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            //SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            //parameterReturnID.Value = ReturnID;
            //myAdapter.SelectCommand.Parameters.Add(parameterReturnID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void InsertReceivedReturn_FromArchive(string OrgTraceNumber)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ReInsertMismatchReturn", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterOrgTraceNumber = new SqlParameter("@OrgTracenumber", SqlDbType.VarChar, 15);
            parameterOrgTraceNumber.Value = OrgTraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterOrgTraceNumber);


            //DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myConnection.Close();
            myAdapter.Dispose();
            myConnection.Close();
            myConnection.Dispose();


            //return myDT;
        }

        public void InsertReceivedReturn_Batch_FromArchive(string OrgTraceNumber)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ReInsertMismatchReturn_Batch", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterOrgTraceNumber = new SqlParameter("@OrgTracenumber", SqlDbType.VarChar, 15);
            parameterOrgTraceNumber.Value = OrgTraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterOrgTraceNumber);


            //DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myConnection.Close();
            myAdapter.Dispose();
            myConnection.Close();
            myConnection.Dispose();


            //return myDT;
        }

    }
}
