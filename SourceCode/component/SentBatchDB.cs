using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class SentBatchDB
    {
        public Guid InsertBatchSent
           (
               int envelopID,
               string serviceClassCode,
               string SECC,
               int typeOfPayment,
               DateTime effectiveEntryDate,
               string companyId,
               string companyName,
               string entryDesc,
               int createdBy, int BatchStatus,
               string BatchType,
               int DepartmentID,
               string DataEntryType,
               string currency
           )
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertBatchSent";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramEnvelopID = new SqlParameter("@EnvelopID", SqlDbType.Int, 4);
            paramEnvelopID.Value = envelopID;
            command.Parameters.Add(paramEnvelopID);

            SqlParameter paramServiceClassCode = new SqlParameter("@ServiceClassCode", SqlDbType.NVarChar, 3);
            paramServiceClassCode.Value = serviceClassCode;
            command.Parameters.Add(paramServiceClassCode);

            SqlParameter paramSECC = new SqlParameter("@SECC", SqlDbType.NVarChar, 3);
            paramSECC.Value = SECC;
            command.Parameters.Add(paramSECC);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = typeOfPayment;
            command.Parameters.Add(paramTypeOfPayment); 

            SqlParameter paramEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.DateTime);
            paramEffectiveEntryDate.Value = effectiveEntryDate;
            command.Parameters.Add(paramEffectiveEntryDate);

            SqlParameter paramCompanyId = new SqlParameter("@CompanyId", SqlDbType.NVarChar, 10);
            paramCompanyId.Value = companyId;
            command.Parameters.Add(paramCompanyId);

            SqlParameter paramCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
            paramCompanyName.Value = companyName;
            command.Parameters.Add(paramCompanyName);

            SqlParameter paramEntryDesc = new SqlParameter("@EntryDesc", SqlDbType.NVarChar, 10);
            paramEntryDesc.Value = entryDesc;
            command.Parameters.Add(paramEntryDesc);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
            paramCreatedBy.Value = createdBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter parameterBatchStatus = new SqlParameter("@BatchStatus", SqlDbType.Int);
            parameterBatchStatus.Value = BatchStatus;
            command.Parameters.Add(parameterBatchStatus);

            SqlParameter parameterBatchType = new SqlParameter("@BatchType", SqlDbType.NVarChar, 6);
            parameterBatchType.Value = BatchType;
            command.Parameters.Add(parameterBatchType);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            command.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterDataEntryType = new SqlParameter("@DataEntryType", SqlDbType.NVarChar, 6);
            parameterDataEntryType.Value = DataEntryType;
            command.Parameters.Add(parameterDataEntryType);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            command.Parameters.Add(paramCurrency);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return (Guid)paramTransactionID.Value;
        }


        public Guid InsertBatchSentWithSettlementDay
   (
       int envelopID,
       string serviceClassCode,
       string SECC,
       int typeOfPayment,
       DateTime effectiveEntryDate,
       string companyId,
       string companyName,
       string entryDesc,
       int createdBy, int BatchStatus,
       string BatchType,
       int DepartmentID,
       string DataEntryType,
       int CBSSettlementDay,
       string currency
   )
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertBatchSentWithSettlementDay";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramEnvelopID = new SqlParameter("@EnvelopID", SqlDbType.Int, 4);
            paramEnvelopID.Value = envelopID;
            command.Parameters.Add(paramEnvelopID);

            SqlParameter paramServiceClassCode = new SqlParameter("@ServiceClassCode", SqlDbType.NVarChar, 3);
            paramServiceClassCode.Value = serviceClassCode;
            command.Parameters.Add(paramServiceClassCode);

            SqlParameter paramSECC = new SqlParameter("@SECC", SqlDbType.NVarChar, 3);
            paramSECC.Value = SECC;
            command.Parameters.Add(paramSECC);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = typeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);

            SqlParameter paramEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.DateTime);
            paramEffectiveEntryDate.Value = effectiveEntryDate;
            command.Parameters.Add(paramEffectiveEntryDate);

            SqlParameter paramCompanyId = new SqlParameter("@CompanyId", SqlDbType.NVarChar, 10);
            paramCompanyId.Value = companyId;
            command.Parameters.Add(paramCompanyId);

            SqlParameter paramCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
            paramCompanyName.Value = companyName;
            command.Parameters.Add(paramCompanyName);

            SqlParameter paramEntryDesc = new SqlParameter("@EntryDesc", SqlDbType.NVarChar, 10);
            paramEntryDesc.Value = entryDesc;
            command.Parameters.Add(paramEntryDesc);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
            paramCreatedBy.Value = createdBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter parameterBatchStatus = new SqlParameter("@BatchStatus", SqlDbType.Int);
            parameterBatchStatus.Value = BatchStatus;
            command.Parameters.Add(parameterBatchStatus);

            SqlParameter parameterBatchType = new SqlParameter("@BatchType", SqlDbType.NVarChar, 6);
            parameterBatchType.Value = BatchType;
            command.Parameters.Add(parameterBatchType);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            command.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterDataEntryType = new SqlParameter("@DataEntryType", SqlDbType.NVarChar, 6);
            parameterDataEntryType.Value = DataEntryType;
            command.Parameters.Add(parameterDataEntryType);

            SqlParameter parameterCBSSettlementDay = new SqlParameter("@CBSSettlementDay", SqlDbType.Int);
            parameterCBSSettlementDay.Value = CBSSettlementDay;
            command.Parameters.Add(parameterCBSSettlementDay);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            command.Parameters.Add(paramCurrency);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return (Guid)paramTransactionID.Value;
        }

        public Guid InsertBatchSentWithAccountWise
           (
               int envelopID,
               string serviceClassCode,
               string SECC,
               int typeOfPayment,
               DateTime effectiveEntryDate,
               string companyId,
               string companyName,
               string entryDesc,
               int createdBy, int BatchStatus,
               string BatchType,
               int DepartmentID,
               string DataEntryType,
               int AccountWiseCBSHit
           )
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertBatchSentWithCBSAccountWiseHit";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramEnvelopID = new SqlParameter("@EnvelopID", SqlDbType.Int, 4);
            paramEnvelopID.Value = envelopID;
            command.Parameters.Add(paramEnvelopID);

            SqlParameter paramServiceClassCode = new SqlParameter("@ServiceClassCode", SqlDbType.NVarChar, 3);
            paramServiceClassCode.Value = serviceClassCode;
            command.Parameters.Add(paramServiceClassCode);

            SqlParameter paramSECC = new SqlParameter("@SECC", SqlDbType.NVarChar, 3);
            paramSECC.Value = SECC;
            command.Parameters.Add(paramSECC);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = typeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);

            SqlParameter paramEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.DateTime);
            paramEffectiveEntryDate.Value = effectiveEntryDate;
            command.Parameters.Add(paramEffectiveEntryDate);

            SqlParameter paramCompanyId = new SqlParameter("@CompanyId", SqlDbType.NVarChar, 10);
            paramCompanyId.Value = companyId;
            command.Parameters.Add(paramCompanyId);

            SqlParameter paramCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
            paramCompanyName.Value = companyName;
            command.Parameters.Add(paramCompanyName);

            SqlParameter paramEntryDesc = new SqlParameter("@EntryDesc", SqlDbType.NVarChar, 10);
            paramEntryDesc.Value = entryDesc;
            command.Parameters.Add(paramEntryDesc);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
            paramCreatedBy.Value = createdBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter parameterBatchStatus = new SqlParameter("@BatchStatus", SqlDbType.Int);
            parameterBatchStatus.Value = BatchStatus;
            command.Parameters.Add(parameterBatchStatus);

            SqlParameter parameterBatchType = new SqlParameter("@BatchType", SqlDbType.NVarChar, 6);
            parameterBatchType.Value = BatchType;
            command.Parameters.Add(parameterBatchType);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            command.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterDataEntryType = new SqlParameter("@DataEntryType", SqlDbType.NVarChar, 6);
            parameterDataEntryType.Value = DataEntryType;
            command.Parameters.Add(parameterDataEntryType);

            SqlParameter parameterAccountWiseCBSHit = new SqlParameter("@AccountWiseCBSHit", SqlDbType.Int);
            parameterAccountWiseCBSHit.Value = AccountWiseCBSHit;
            command.Parameters.Add(parameterAccountWiseCBSHit);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return (Guid)paramTransactionID.Value;
        }

        public DataTable GetBatchNumberByTransactionID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatchNumberByTransactionID", myConnection);
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

        public DataTable GetSentBatchByTransactionID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentBatchByTransactionID", myConnection);
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
        //public SqlDataReader GetBatches_For_TransactionSent()
        //{
        //    SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandText = "EFT_GetBatches_For_TransactionSent";
        //    command.CommandType = CommandType.StoredProcedure;

        //    connection.Open();

        //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //    return reader;
        //}

        public DataTable GetBatches_For_TransactionSent(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_For_TransactionSent", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateBatchSent(Guid transactionID, int envelopID, DateTime SettlementJDate, string XMLFileName)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_UpdateBatchSent", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = transactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterEnvelopID = new SqlParameter("@EnvelopID", SqlDbType.Int);
            parameterEnvelopID.Value = envelopID;
            myCommand.Parameters.Add(parameterEnvelopID);

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementJDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterXMLFileName = new SqlParameter("@XMLFileName", SqlDbType.NVarChar, 200);
            parameterXMLFileName.Value = XMLFileName;
            myCommand.Parameters.Add(parameterXMLFileName);

            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }

        public DataTable GetBatchSent(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatchSent_By_BatchSentID", myConnection);
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

        public string GetBatchControl(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_GetBatchControl", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterOrigBank = new SqlParameter("@OrigBank", SqlDbType.VarChar,9);
            parameterOrigBank.Value = ConfigurationManager.AppSettings["OriginBank"];
            myCommand.Parameters.Add(parameterOrigBank);

            SqlParameter parameterBCRXML = new SqlParameter("@BCRXML", SqlDbType.VarChar,8000);
            parameterBCRXML.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBCRXML);


            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();

            return parameterBCRXML.Value.ToString();
            
        }

        public string GetBatchControlForCTX(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_GetBatchControlForCTX", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterOrigBank = new SqlParameter("@OrigBank", SqlDbType.VarChar,9);
            parameterOrigBank.Value = ConfigurationManager.AppSettings["OriginBank"];
            myCommand.Parameters.Add(parameterOrigBank);

            SqlParameter parameterBCRXML = new SqlParameter("@BCRXML", SqlDbType.VarChar,8000);
            parameterBCRXML.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBCRXML);


            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();

            return parameterBCRXML.Value.ToString();
            
        }
        
        //public SqlDataReader GetBatches_For_DishonorSent()
        //{
        //    SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandText = "EFT_GetBatches_For_DishonorSent";
        //    command.CommandType = CommandType.StoredProcedure;

        //    connection.Open();

        //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //    return reader;
        //}

        public DataTable GetBatches_For_DishonorSent()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_For_DishonorSent", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public string GetBatchControlXMLDishonor(Guid TransactionID, DateTime SettlementDate, string xmlFileName)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_BatchControlXMLDishonor", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterOriginBank = new SqlParameter("@OriginBank", SqlDbType.VarChar, 9);
            parameterOriginBank.Value = ConfigurationManager.AppSettings["OriginBank"];
            myCommand.Parameters.Add(parameterOriginBank);

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.DateTime);
            parameterSettlementDate.Value = SettlementDate;
            myCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter paramXmlFile = new SqlParameter("@XmlFileName", SqlDbType.VarChar,200);
            paramXmlFile.Value = xmlFileName;
            myCommand.Parameters.Add(paramXmlFile);

            SqlParameter parameterBCRXML = new SqlParameter("@BCRXML", SqlDbType.VarChar, 8000);
            parameterBCRXML.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBCRXML);          


            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();

            return parameterBCRXML.Value.ToString();

        }
        public string GetBatchControlXMLDishonor_ForDebit(Guid TransactionID, DateTime SettlementDate, string xmlFileName)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_BatchControlXMLDishonor_ForDebit", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterOriginBank = new SqlParameter("@OriginBank", SqlDbType.VarChar, 9);
            parameterOriginBank.Value = ConfigurationManager.AppSettings["OriginBank"];
            myCommand.Parameters.Add(parameterOriginBank);

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.DateTime);
            parameterSettlementDate.Value = SettlementDate;
            myCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter paramXmlFile = new SqlParameter("@XmlFileName", SqlDbType.VarChar, 200);
            paramXmlFile.Value = xmlFileName;
            myCommand.Parameters.Add(paramXmlFile);

            SqlParameter parameterBCRXML = new SqlParameter("@BCRXML", SqlDbType.VarChar, 8000);
            parameterBCRXML.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBCRXML);


            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();

            return parameterBCRXML.Value.ToString();

        }

        public static void DeleteSentBatch(Guid transactionID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_DeleteSentBatch", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = transactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }

        public static void DeleteSentEDR(Guid transactionID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_DeleteSentEDR", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = transactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }

        //public SqlDataReader GetBatchesForTransactionSentForChecker()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBatches_For_TransactionSent_forChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetBatchesForTransactionSentForChecker(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_For_TransactionSent_forChecker", myConnection);
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

        public DataTable GetBatchesForTransactionSentForCheckerSts(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_For_TransactionSent_forCheckerSts", myConnection);
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

        public DataTable GetBatchesForTransactionSentForCheckerStdOrder(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_For_TransactionSent_forCheckerStdOrder", myConnection);
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
        public void UpdateEDRSentStatusForBatchApproval(int StatusID,
                                                        Guid TransactionID,
                                                        int ApprovedBy)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_EDRSent_Status_forBatchApproval", myConnection);
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


            myConnection.Open();
            myCommand.ExecuteReader();
            myConnection.Close();
        }

        public int UpdateEDRSentStatusForBatchApprovalForSCB(int StatusID,
                                                        Guid TransactionID,
                                                        int ApprovedBy)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_EDRSent_Status_forBatchApprovalForSCB", myConnection);
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

            SqlParameter parameterGoodBatch = new SqlParameter("@GoodBatch", SqlDbType.Int);
            parameterGoodBatch.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterGoodBatch);

            myConnection.Open();
            myCommand.ExecuteReader();
            int goodBatch = (int)parameterGoodBatch.Value;
            myConnection.Close();

            return goodBatch;
        }

        //public SqlDataReader GetBatchesRejectedByChecker()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBatches_RejectedByChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetBatchesRejectedByChecker(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_RejectedByChecker", myConnection);
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

        public DataTable GetBatchesRejectedByCheckerForDebit(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_RejectedByChecker_ForDebit", myConnection);
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

        public void DeleteBatchSent(Guid TransactionID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_DeleteBatchSent", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            myConnection.Open();
            myCommand.ExecuteReader();
            myConnection.Close();
        }

        public DataTable GetBulkTransactionUploadedOnly(int DepartmentID, int createdBy)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBulkTransactionSentUploadedOnly", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter paramUser = new SqlParameter("@UserId", SqlDbType.Int);
            paramUser.Value = createdBy;
            myAdapter.SelectCommand.Parameters.Add(paramUser);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        //public SqlDataReader GetBatchesForTransactionSentForEBBSChecker()
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBatches_For_TransactionSent_forEBBSChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetBatchesForTransactionSentForEBBSChecker(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_For_TransactionSent_forEBBSChecker", myConnection);
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

        public DataTable GetBatchesForTransactionSentForEBBSCheckerSTS(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_For_TransactionSent_forEBBSChecker_STS", myConnection);
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

        public DataTable GetBatchesForTransactionSentForEBBSCheckerSTDO(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_For_TransactionSent_forEBBSChecker_STDO", myConnection);
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
        public void UpdateEDRSentStatusForBatchApprovalForEBBSCheckerAcceptance(Guid TransactionID, int ApprovedBy)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_Update_EDRSent_Status_forBatchApproval_ForEBBSCheckerAcceptance", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myCommand.Parameters.Add(parameterApprovedBy);

            myConnection.Open();
            myCommand.ExecuteReader();
            myConnection.Close();
        }

        public void UpdateEDRSentStatusForBatchApprovalForEBBSCheckerRejection(Guid TransactionID, int ApprovedBy)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_Update_EDRSent_Status_forBatchApproval_ForEBBSCheckerRejection", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myCommand.Parameters.Add(parameterApprovedBy);

            myConnection.Open();
            myCommand.ExecuteReader();
            myConnection.Close();
        }

        public DataTable GetCurrencyList(string eftConnectionString)
        {
            SqlConnection eftConnection = new SqlConnection(eftConnectionString);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("EFT_GetCurrencyList", eftConnection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.CommandTimeout = 600;

            DataTable currencyData = new DataTable();
            eftConnection.Open();
            sqlAdapter.Fill(currencyData);
            eftConnection.Close();
            return currencyData;
        }

        public DataTable GetSessions(string eftConnectionString)
        {
            SqlConnection eftConnection = new SqlConnection(eftConnectionString);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("EFT_GetSessions", eftConnection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.CommandTimeout = 600;

            DataTable sessionData = new DataTable();
            eftConnection.Open();
            sqlAdapter.Fill(sessionData);
            eftConnection.Close();
            return sessionData;
        }
    }
}
