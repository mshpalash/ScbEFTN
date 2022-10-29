using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using EFTN.Utility;

namespace EFTN.component
{
    public class ReceivedBatchDB
    {
        public Guid InsertBatchReceived
         (            
            int EnvelopID,
            string ServiceClassCode,
            string SECC,
            string batchNumber,
            int settlementJDate,            
            DateTime EffectiveEntryDate,
            string CompanyId,
            string CompanyName,
            string EntryDesc
            ,string currency
            ,int session
         )
        {

            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertBatchReceived",myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
                    parameterTransactionID.Direction = ParameterDirection.Output;
                    myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

                    SqlParameter parameterEnvelopID = new SqlParameter("@EnvelopID", SqlDbType.Int);
                    parameterEnvelopID.Value = EnvelopID;
                    myAdapter.SelectCommand.Parameters.Add(parameterEnvelopID);

                    SqlParameter parameterServiceClassCode = new SqlParameter("@ServiceClassCode", SqlDbType.NVarChar, 3);
                    parameterServiceClassCode.Value = ServiceClassCode;
                    myAdapter.SelectCommand.Parameters.Add(parameterServiceClassCode);

                    SqlParameter parameterSECC = new SqlParameter("@SECC", SqlDbType.NVarChar, 3);
                    parameterSECC.Value = SECC;
                    myAdapter.SelectCommand.Parameters.Add(parameterSECC);

                    SqlParameter parameterBatchNumber = new SqlParameter("@BatchNumber", SqlDbType.NVarChar, EFTLength.BatchNumber);
                    parameterBatchNumber.Value = batchNumber;
                    myAdapter.SelectCommand.Parameters.Add(parameterBatchNumber);

                    SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.SmallInt);
                    parameterSettlementJDate.Value = settlementJDate;
                    myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);                   

                    SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.DateTime);
                    parameterEffectiveEntryDate.Value = EffectiveEntryDate;
                    myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

                    SqlParameter parameterCompanyId = new SqlParameter("@CompanyId", SqlDbType.NVarChar, 10);
                    parameterCompanyId.Value = CompanyId;
                    myAdapter.SelectCommand.Parameters.Add(parameterCompanyId);

                    SqlParameter parameterCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
                    parameterCompanyName.Value = CompanyName;
                    myAdapter.SelectCommand.Parameters.Add(parameterCompanyName);

                    SqlParameter parameterEntryDesc = new SqlParameter("@EntryDesc", SqlDbType.NVarChar, 10);
                    parameterEntryDesc.Value = EntryDesc;
                    myAdapter.SelectCommand.Parameters.Add(parameterEntryDesc);

                    SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.NVarChar, 10);
                    paramCurrency.Value = currency;
                    myAdapter.SelectCommand.Parameters.Add(paramCurrency);

                    SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.Int, 2);
                    paramSession.Value = session;
                    myAdapter.SelectCommand.Parameters.Add(paramSession);

                    myConnection.Open();

                    myAdapter.SelectCommand.ExecuteNonQuery();

                    myConnection.Close();
                    myConnection.Dispose();

                    return (Guid)parameterTransactionID.Value;


                }
            }
        }

        public string GetBatchControlXMLReturn(Guid TransactionID, DateTime SettlementDate, string XMLFileName)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_BatchControlXMLReturn", myConnection);
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

            SqlParameter parameterXMLFileName = new SqlParameter("@XMLFileName", SqlDbType.NVarChar, 200);
            parameterXMLFileName.Value = XMLFileName;
            myCommand.Parameters.Add(parameterXMLFileName);


            SqlParameter parameterBCRXML = new SqlParameter("@BCRXML", SqlDbType.VarChar, 8000);
            parameterBCRXML.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBCRXML);


            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();

            return parameterBCRXML.Value.ToString();

        }

        public string GetBatchControlXMLReturn_ForDebit(Guid TransactionID, DateTime SettlementDate, string XMLFileName)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_BatchControlXMLReturn_ForDebit", myConnection);
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

            SqlParameter parameterXMLFileName = new SqlParameter("@XMLFileName", SqlDbType.NVarChar, 200);
            parameterXMLFileName.Value = XMLFileName;
            myCommand.Parameters.Add(parameterXMLFileName);

            SqlParameter parameterBCRXML = new SqlParameter("@BCRXML", SqlDbType.VarChar, 8000);
            parameterBCRXML.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBCRXML);


            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();

            return parameterBCRXML.Value.ToString();

        }

        public string GetBatchControlXMLNOC(Guid TransactionID, DateTime SettlementDate, string XMLFileName)
        {
            
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            
            SqlCommand myCommand = new SqlCommand("EFT_BatchControlXMLNOC", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterOriginBank = new SqlParameter("@OriginBank", SqlDbType.VarChar,9);
            parameterOriginBank.Value = ConfigurationManager.AppSettings["OriginBank"];
            myCommand.Parameters.Add(parameterOriginBank);

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.DateTime);
            parameterSettlementDate.Value = SettlementDate;
            myCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterXMLFileName = new SqlParameter("@XMLFileName", SqlDbType.NVarChar, 200);
            parameterXMLFileName.Value = XMLFileName;
            myCommand.Parameters.Add(parameterXMLFileName);

            SqlParameter parameterBCRXML = new SqlParameter("@BCRXML", SqlDbType.VarChar, 8000);
            parameterBCRXML.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBCRXML);


            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();

            return parameterBCRXML.Value.ToString();

        }

        //public SqlDataReader GetBatches_For_RRSent()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand();
        //    myCommand.Connection = myConnection;
        //    myCommand.CommandText = "[EFT_GetBatches_For_RRSent]";
        //    myCommand.CommandType = CommandType.StoredProcedure;
        //    myCommand.CommandTimeout = 600;
        //    myConnection.Open();
        //    SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return reader;
        //}

        public DataTable GetBatches_For_RRSent()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_For_RRSent", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetBatchReceived_By_BatchID(Guid transactionId)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand();
        //    myCommand.Connection = myConnection;
        //    myCommand.CommandText = "[EFT_GetBatchReceived_By_BatchID]";
        //    myCommand.CommandType = CommandType.StoredProcedure;
        //    myCommand.CommandTimeout = 600;
        //    SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
        //    parameterTransactionID.Value = transactionId;
        //    myCommand.Parameters.Add(parameterTransactionID);
        //    myConnection.Open();
        //    SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return reader;
        //}

        public DataTable GetBatchReceived_By_BatchID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatchReceived_By_BatchID", myConnection);
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

        //public SqlDataReader GetBatches_For_NOCSent()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand();
        //    myCommand.Connection = myConnection;
        //    myCommand.CommandText = "EFT_GetBatches_For_NOCSent";
        //    myCommand.CommandType = CommandType.StoredProcedure;
        //    myCommand.CommandTimeout = 600;


        //    myConnection.Open();

        //    SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

        //    return reader;


        //}

        public DataTable GetBatches_For_NOCSent()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_For_NOCSent", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetBatchesForContestedSent()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBatches_For_ContestedSent", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader();
        //    return dr;
        //}

        public DataTable GetBatchesForContestedSent()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_For_ContestedSent", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}
