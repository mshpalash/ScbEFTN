using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class TransactionToCBSDB
    {
        public DataTable GetTransactionToSendCBS()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            using (SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_GetOnUsTransaction", myConnection))
            {
                myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
                myAdpater.SelectCommand.CommandTimeout = 3600;

                DataTable dt = new DataTable();

                myConnection.Open();

                myAdpater.Fill(dt);

                myConnection.Close();

                return dt;
            }
        }

        public void UpdateTransactionToSendCBS()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_SendOnUsTransactionToCBS", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myConnection.Dispose();
            myCommand.Dispose();
        }

        //public DataTable GetTransactionReceivedFlatFile()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    using (SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_FlatFile_GetTransactionReceived", myConnection))
        //    {
        //        myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        myAdpater.SelectCommand.CommandTimeout = 3600;

        //        DataTable dt = new DataTable();

        //        myConnection.Open();
        //        myAdpater.Fill(dt);
        //        myConnection.Close();

        //        return dt;
        //    }
        //}
        public DataTable GetTransactionReceivedFlatFile(string SettlementJDate, int Hold, string currency)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionReceived", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.VarChar);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterHold = new SqlParameter("@Hold", SqlDbType.Int);
            parameterHold.Value = Hold;
            myAdapter.SelectCommand.Parameters.Add(parameterHold);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar,3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetCSVRejectionFlatFile(string SettlementJDate, string currency)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_CSVRejection", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@EntryDateTransactionSent", SqlDbType.VarChar);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);



            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        //public DataTable GetTransactionReceivedFlatFileForDebit()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    using (SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_FlatFile_GetTransactionReceived_Debit", myConnection))
        //    {
        //        myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        myAdpater.SelectCommand.CommandTimeout = 3600;

        //        DataTable dt = new DataTable();

        //        myConnection.Open();
        //        myAdpater.Fill(dt);
        //        myConnection.Close();

        //        return dt;
        //    }
        //}
        public DataTable GetTransactionReceivedFlatFileForDebit(string SettlementJDate, int Hold, string currency)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionReceived_Debit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.VarChar);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterHold = new SqlParameter("@Hold", SqlDbType.Int);
            parameterHold.Value = Hold;
            myAdapter.SelectCommand.Parameters.Add(parameterHold);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetTransactionReceivedFlatFileForDebitReverse(string currency)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionReceived_Debit_Reverse", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetTransactionReceivedFlatFileForDebitReverseForIF(string currency)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionReceived_Debit_ReverseForIF", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetCSVRejectionFlatFileForDebit(string SettlementJDate, string currency)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_CSVRejection_Debit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@EntryDateTransactionSent", SqlDbType.VarChar);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);


            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetTransactionSentFlatFile(Guid TransactionID, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionSent", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            //SqlParameter parameterBatchType = new SqlParameter("@BatchType", SqlDbType.NVarChar, 6);
            //parameterBatchType.Value = BatchType;
            //myAdapter.SelectCommand.Parameters.Add(parameterBatchType);
            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetTransactionSentFlatFileSTS(string EffectiveEntryDate, int DepartmentID, int PrintFlag, Guid TransactionId)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionSentSTS", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterPrintFlag = new SqlParameter("@PrintFlag", SqlDbType.Int);
            parameterPrintFlag.Value = PrintFlag;
            myAdapter.SelectCommand.Parameters.Add(parameterPrintFlag);

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionId;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetTransactionSentFlatFileSTDOrderByEffectiveEntryDate(string EffectiveEntryDate, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionSent_STDorderByDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar, 8);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            //SqlParameter parameterBatchType = new SqlParameter("@BatchType", SqlDbType.NVarChar, 6);
            //parameterBatchType.Value = BatchType;
            //myAdapter.SelectCommand.Parameters.Add(parameterBatchType);
            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetTransactionSentFlatFileSTSAccumulated(string EffectiveEntryDate, int DepartmentID, int PrintFlag, Guid transactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionSentSTSAccumulated", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterPrintFlag = new SqlParameter("@PrintFlag", SqlDbType.Int);
            parameterPrintFlag.Value = PrintFlag;
            myAdapter.SelectCommand.Parameters.Add(parameterPrintFlag);

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = transactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public DataTable GetTransactionSentforSCBCharge(Guid TransactionID, int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            string spName = string.Empty;
            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            if (bankCode.Equals("215")) //SCB
            {
                spName = "EFT_FlatFile_GetTransactionSentforSCBCharge";
            }
            else if(bankCode.Equals("225")) //CITY bank
            {
                spName = "EFT_FlatFile_GetTransactionSentforCityCharge";
            }
            SqlDataAdapter myAdapter = new SqlDataAdapter(spName, myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }



        public DataTable GetTransactionSentByDistinctAccountNumForCharge(Guid TransactionID, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionSent_ByDistinctAccountNumForCharge", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetFlatFileDataForTransactionSentByDistinctAccountNum(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionSent_ByDistinctAccountNum", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            //SqlParameter parameterBatchType = new SqlParameter("@BatchType", SqlDbType.NVarChar, 6);
            //parameterBatchType.Value = BatchType;
            //myAdapter.SelectCommand.Parameters.Add(parameterBatchType);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetTransactionSentFlatFileReverse(Guid TransactionID, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionSent_Reverse", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            //SqlParameter parameterBatchType = new SqlParameter("@BatchType", SqlDbType.NVarChar, 6);
            //parameterBatchType.Value = BatchType;
            //myAdapter.SelectCommand.Parameters.Add(parameterBatchType);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetTransactionSentFlatFileReverseSTS(string EffectiveEntryDate, int DepartmentID, int PrintFlag, Guid TransactionId)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionSent_ReverseSTS", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterPrintFlag = new SqlParameter("@PrintFlag", SqlDbType.Int);
            parameterPrintFlag.Value = PrintFlag;
            myAdapter.SelectCommand.Parameters.Add(parameterPrintFlag);

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionId;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetFlatFileDataForTransactionSentReverseByDistinctAccountNum(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetTransactionSent_Reverse_ByDistinctAccountNum", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            //SqlParameter parameterBatchType = new SqlParameter("@BatchType", SqlDbType.NVarChar, 6);
            //parameterBatchType.Value = BatchType;
            //myAdapter.SelectCommand.Parameters.Add(parameterBatchType);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetTransactionSentForISO(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ISO_GetTransactionSent", myConnection);
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

        public DataTable GetTransactionReceivedCreditForISO()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ISO_GetTransactionReceived", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetTransactionReceivedDebitForISO()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ISO_GetTransactionReceivedDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }



        public DataTable GetFlatFileForReturnReceivedForSCB(string SettlementJDate, int DepartmentID, string Currency)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetReturnReceivedForSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.VarChar);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterCurrency = new SqlParameter("@Currency ", SqlDbType.VarChar);
            parameterCurrency.Value = Currency;
            myAdapter.SelectCommand.Parameters.Add(parameterCurrency);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetFlatFileForReturnReceivedDebitForSCB(string SettlementJDate, int DepartmentID , string Currency)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFile_GetReturnReceived_DebitForSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.VarChar);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterCurrency = new SqlParameter("@Currency ", SqlDbType.VarChar);
            parameterCurrency.Value = Currency;
            myAdapter.SelectCommand.Parameters.Add(parameterCurrency);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetHSBCTransactionSentForChargeforBulkOnly(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FlatFileHSBC_GetTransactionSentForChargeforBulkOnly", myConnection);
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

        public DataTable GetHSBCTransactionSentForChargeNonBulk(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            string spName = "EFT_FlatFileHSBC_GetTransactionSentForChargeNonBulk";
            SqlDataAdapter myAdapter = new SqlDataAdapter(spName , myConnection);
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

        public DataTable GetPubaliFlatFileForReturnReceived(string SettlementJDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            string spName = "EFT_FlatFilePubali_GetReceivedReturn";
            SqlDataAdapter myAdapter = new SqlDataAdapter(spName, myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.VarChar);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}
