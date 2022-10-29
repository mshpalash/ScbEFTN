using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using EFTN.Utility;
namespace EFTN.component
{
    public class ReceivedEDRDB
    {
        /*
        public Guid InsertReceivedEDR
            (
                Guid TransactionID,
                string traceNumber,
                string TransactionCode,
                string DFIAccountNo,
                string SendingBankRoutNo,
                decimal Amount,
                string IdNumber,
                string ReceiverName,
                int StatusID,
                string PaymentInfo,
                DateTime SettlementJDate)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("[EFT_InsertReceivedEDR]", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
                    parameterEDRID.Direction = ParameterDirection.Output;
                    myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

                    SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
                    parameterTransactionID.Value = TransactionID;
                    myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

                    SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
                    parameterTraceNumber.Value = traceNumber;
                    myAdapter.SelectCommand.Parameters.Add(parameterTraceNumber);

                    SqlParameter parameterTransactionCode = new SqlParameter("@TransactionCode", SqlDbType.NVarChar, 3);
                    parameterTransactionCode.Value = TransactionCode;
                    myAdapter.SelectCommand.Parameters.Add(parameterTransactionCode);

                    SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 15);
                    parameterDFIAccountNo.Value = DFIAccountNo;
                    myAdapter.SelectCommand.Parameters.Add(parameterDFIAccountNo);

                    SqlParameter parameterSendingBankRoutNo = new SqlParameter("@SendingBankRoutNo", SqlDbType.NVarChar, 8);
                    parameterSendingBankRoutNo.Value = SendingBankRoutNo;
                    myAdapter.SelectCommand.Parameters.Add(parameterSendingBankRoutNo);

                    SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
                    parameterAmount.Value = Amount;
                    myAdapter.SelectCommand.Parameters.Add(parameterAmount);

                    SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 15);
                    parameterIdNumber.Value = IdNumber;
                    myAdapter.SelectCommand.Parameters.Add(parameterIdNumber);

                    SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
                    parameterReceiverName.Value = ReceiverName;
                    myAdapter.SelectCommand.Parameters.Add(parameterReceiverName);

                    SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
                    parameterStatusID.Value = StatusID;
                    myAdapter.SelectCommand.Parameters.Add(parameterStatusID);

                    SqlParameter parameterPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
                    parameterPaymentInfo.Value = PaymentInfo;
                    myAdapter.SelectCommand.Parameters.Add(parameterPaymentInfo);

                    SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.DateTime);
                    parameterSettlementJDate.Value = SettlementJDate;
                    myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);
                    
                    myConnection.Open();

                    myAdapter.SelectCommand.ExecuteNonQuery();

                    myConnection.Close();
                    myConnection.Dispose();

                    return (Guid)parameterEDRID.Value;

                }
            }
        }
        */
        public Guid InsertReceivedEDR
            (
                Guid TransactionID,
                string traceNumber,
                string TransactionCode,
                string DFIAccountNo,
                string SendingBankRoutNo,
                decimal Amount,
                string IdNumber,
                string ReceiverName,
                int StatusID,
                string PaymentInfo,
                DateTime SettlementJDate,
                string invoiceNumber,
                string invoiceDate,
                decimal invoiceGrossAmount,
                decimal invoiceAmountPaid,
                string purchaseOrder,
                decimal adjustmentAmount,
                string adjustmentCode,
                string adjustmentDescription,
                string ReceivingBankRoutNo,
                string XMLFileName
            )
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("[EFT_InsertReceivedEDR]", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
                    parameterEDRID.Direction = ParameterDirection.Output;
                    myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

                    SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
                    parameterTransactionID.Value = TransactionID;
                    myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

                    SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
                    parameterTraceNumber.Value = traceNumber;
                    myAdapter.SelectCommand.Parameters.Add(parameterTraceNumber);

                    SqlParameter parameterTransactionCode = new SqlParameter("@TransactionCode", SqlDbType.NVarChar, 3);
                    parameterTransactionCode.Value = TransactionCode;
                    myAdapter.SelectCommand.Parameters.Add(parameterTransactionCode);

                    SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
                    parameterDFIAccountNo.Value = DFIAccountNo;
                    myAdapter.SelectCommand.Parameters.Add(parameterDFIAccountNo);

                    SqlParameter parameterSendingBankRoutNo = new SqlParameter("@SendingBankRoutNo", SqlDbType.NVarChar, 8);
                    parameterSendingBankRoutNo.Value = SendingBankRoutNo;
                    myAdapter.SelectCommand.Parameters.Add(parameterSendingBankRoutNo);

                    SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
                    parameterAmount.Value = Amount;
                    myAdapter.SelectCommand.Parameters.Add(parameterAmount);

                    SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, EFTLength.ReceiverIDLengthForCIE);
                    parameterIdNumber.Value = IdNumber;
                    myAdapter.SelectCommand.Parameters.Add(parameterIdNumber);

                    SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
                    parameterReceiverName.Value = ReceiverName;
                    myAdapter.SelectCommand.Parameters.Add(parameterReceiverName);

                    SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
                    parameterStatusID.Value = StatusID;
                    myAdapter.SelectCommand.Parameters.Add(parameterStatusID);

                    SqlParameter parameterPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
                    parameterPaymentInfo.Value = PaymentInfo;
                    myAdapter.SelectCommand.Parameters.Add(parameterPaymentInfo);

                    SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.DateTime);
                    parameterSettlementJDate.Value = SettlementJDate;
                    myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);

                    SqlParameter paraminvoiceNumber = new SqlParameter("@CTX_InvoiceNumber", SqlDbType.NVarChar, 10);
                    paraminvoiceNumber.Value = invoiceNumber;
                    myAdapter.SelectCommand.Parameters.Add(paraminvoiceNumber);

                    SqlParameter paraminvoiceDate = new SqlParameter("@CTX_InvoiceDate", SqlDbType.NVarChar, 8);
                    paraminvoiceDate.Value = invoiceDate;
                    myAdapter.SelectCommand.Parameters.Add(paraminvoiceDate);

                    SqlParameter paraminvoiceGrossAmount = new SqlParameter("@CTX_InvoiceGrossAmount", SqlDbType.Money);
                    paraminvoiceGrossAmount.Value = invoiceGrossAmount;
                    myAdapter.SelectCommand.Parameters.Add(paraminvoiceGrossAmount);

                    SqlParameter paraminvoiceAmountPaid = new SqlParameter("@CTX_InvoiceAmountPaid", SqlDbType.Money);
                    paraminvoiceAmountPaid.Value = invoiceAmountPaid;
                    myAdapter.SelectCommand.Parameters.Add(paraminvoiceAmountPaid);

                    SqlParameter parampurchaseOrder = new SqlParameter("@CTX_PurchaseOrder", SqlDbType.NVarChar, 10);
                    parampurchaseOrder.Value = purchaseOrder;
                    myAdapter.SelectCommand.Parameters.Add(parampurchaseOrder);

                    SqlParameter paramadjustmentAmount = new SqlParameter("@CTX_AdjustmentAmount", SqlDbType.Money);
                    paramadjustmentAmount.Value = adjustmentAmount;
                    myAdapter.SelectCommand.Parameters.Add(paramadjustmentAmount);

                    SqlParameter paramadjustmentCode = new SqlParameter("@CTX_AdjustmentCode", SqlDbType.NVarChar, 2);
                    paramadjustmentCode.Value = adjustmentCode;
                    myAdapter.SelectCommand.Parameters.Add(paramadjustmentCode);

                    SqlParameter paramadjustmentDescription = new SqlParameter("@CTX_AdjustmentDescription", SqlDbType.NVarChar, 40);
                    paramadjustmentDescription.Value = adjustmentDescription;
                    myAdapter.SelectCommand.Parameters.Add(paramadjustmentDescription);

                    SqlParameter parameterReceivingBankRoutNo = new SqlParameter("@ReceivingBankRoutNo", SqlDbType.NVarChar, 8);
                    parameterReceivingBankRoutNo.Value = ReceivingBankRoutNo;
                    myAdapter.SelectCommand.Parameters.Add(parameterReceivingBankRoutNo);

                    SqlParameter parameterXMLFileName = new SqlParameter("@XMLFileName", SqlDbType.NVarChar, 200);
                    parameterXMLFileName.Value = XMLFileName;
                    myAdapter.SelectCommand.Parameters.Add(parameterXMLFileName);

                    myConnection.Open();

                    myAdapter.SelectCommand.ExecuteNonQuery();

                    myConnection.Close();
                    myConnection.Dispose();

                    return (Guid)parameterEDRID.Value;

                }
            }
        }


        public DataTable GetReceivedEDRForAdmin(string BankCode)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetReceivedEDR_ForAdmin", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            command.Parameters.Add(parameterBankCode);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            adapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;            
        }

        public DataTable GetReceivedEDRForAdmin(int BranchID, string BankCode)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_ForAdmin_byBranches", myConnection);
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

        public DataTable GetReceivedEDRForAdmin_ForDebit(string BankCode)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetReceivedEDR_ForAdmin_ForDebit", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            command.Parameters.Add(parameterBankCode);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            adapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDRForAdmin_ForDebit(int BranchID, string BankCode)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_ForAdmin_byBranches_ForDebit", myConnection);
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

        public DataTable GetReceivedEDRForAdmin_ForALL(string BankCode)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ForAdmin_ForAll", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFT_GetReceivedEDR_ForAdmin_byBranches_ForAll(int BranchID, string BankCode)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ForAdmin_byBranches_ForAll", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable FlatFileGetTransactionReceived()
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_FlatFile_GetTransactionReceived", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            adapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public void UpdateEDRReceivedStatusForOutwardDebitReturn(Guid EDRID, int statusID, int createdBy)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Update_EDR_Status_For_OutwardDebitReturn_IF", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
                    paramStatusID.Value = statusID;
                    myAdapter.SelectCommand.Parameters.Add(paramStatusID);

                    SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
                    paramEDRID.Value = EDRID;
                    myAdapter.SelectCommand.Parameters.Add(paramEDRID);

                    SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
                    paramCreatedBy.Value = createdBy;
                    myAdapter.SelectCommand.Parameters.Add(paramCreatedBy);     
                      
                    myConnection.Open();
                    myAdapter.SelectCommand.ExecuteNonQuery();
                    myConnection.Close();
                    myConnection.Dispose();   
                }
            }
        }
        public int UpdateEDRReceivedStatus(Guid EDRID,int statusID,string returnCode,int createdBy, string correctedData)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("[EFT_Update_EDRReceived_Status]", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
                    paramStatusID.Value = statusID;
                    myAdapter.SelectCommand.Parameters.Add(paramStatusID);

                    SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
                    paramEDRID.Value = EDRID;
                    myAdapter.SelectCommand.Parameters.Add(paramEDRID);

                    SqlParameter paramReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
                    paramReturnID.Direction = ParameterDirection.Output;
                    myAdapter.SelectCommand.Parameters.Add(paramReturnID);


                    SqlParameter paramNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
                    paramNOCID.Direction = ParameterDirection.Output;
                    myAdapter.SelectCommand.Parameters.Add(paramNOCID);

                    SqlParameter paramReturnCode = new SqlParameter("@ReturnCode", SqlDbType.NVarChar,3);
                    paramReturnCode.Value = returnCode;
                    myAdapter.SelectCommand.Parameters.Add(paramReturnCode);

                    SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
                    paramCreatedBy.Value = createdBy;
                    myAdapter.SelectCommand.Parameters.Add(paramCreatedBy);

                    SqlParameter paramCorrectedData = new SqlParameter("@CorrectedData", SqlDbType.NVarChar, 30);
                    paramCorrectedData.Value = correctedData;
                    myAdapter.SelectCommand.Parameters.Add(paramCorrectedData);

                    SqlParameter parameterChangedStatus = new SqlParameter("@ChangedStatus", SqlDbType.Int);
                    parameterChangedStatus.Direction = ParameterDirection.Output;
                    myAdapter.SelectCommand.Parameters.Add(parameterChangedStatus);

 

                    myConnection.Open();

                    myAdapter.SelectCommand.ExecuteNonQuery();

                    myConnection.Close();
                    myConnection.Dispose();

                    int changeStatus = 0;
                    try
                    {
                        changeStatus = (int)paramReturnID.Value;
                    }
                    catch
                    {

                    }

                    return changeStatus;
                    

                }
            }
        }

        //public SqlDataReader GetReceivedEDR_By_BatchSentID(Guid transactionId)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand();
        //    myCommand.Connection = myConnection;
        //    myCommand.CommandText = "[EFT_GetReceivedEDR_By_BatchSentID]";
        //    myCommand.CommandType = CommandType.StoredProcedure;
        //    myCommand.CommandTimeout = 600;


        //    SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
        //    parameterTransactionID.Value = transactionId;
        //    myCommand.Parameters.Add(parameterTransactionID);


        //    myConnection.Open();

        //    SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

        //    return reader;
        //}

        public DataTable GetReceivedEDR_By_BatchSentID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_By_BatchSentID", myConnection);
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

        //public SqlDataReader GetReceivedEDRApprovedByMakerByEDRID(Guid EDRID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetReceivedEDR_ApprovedByMakerByEDRID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
        //    parameterEDRID.Value = EDRID;
        //    myCommand.Parameters.Add(parameterEDRID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetReceivedEDRApprovedByMakerByEDRID(Guid EDRID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMakerByEDRID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
            return myDT;
        }

        public DataTable GetReceivedEDRApprovedByMaker(int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMaker_ForChecker", myConnection);
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

        public DataTable GetReceivedEDRApprovedByMaker_ForDebit(int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMaker_ForChecker_ForDebit", myConnection);
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

        public DataTable GetReceivedEDRApprovedByMaker(int BranchID, string BankCode)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMaker_ForChecker_Bankwise", myConnection);
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

        public DataTable GetReceivedEDRApprovedByMaker_ForDebit(int BranchID, string BankCode)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMaker_ForChecker_ForDebit_Bankwise", myConnection);
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
        //public SqlDataReader GetInwardTransactionRejectedByChecker()
        //{

        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlCommand myCommand = new SqlCommand("EFT_GetInwardTransaction_RejectedByChecker", myConnection);

        //    myCommand.CommandType = CommandType.StoredProcedure;
        //    myCommand.CommandTimeout = 600;


        //    myConnection.Open();
        //    SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);


        //    return reader;

        //}

        public DataTable GetInwardTransactionRejectedByChecker(int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetInwardTransaction_RejectedByChecker", myConnection);
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
        
        public DataTable GetInwardTransactionRejectedByCheckerForDebit(int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetInwardTransaction_RejectedByChecker_ForDebit", myConnection);
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

        public DataTable GetInwardTransactionRejectedByCheckerForDebitForIF(int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetInwardTransaction_RejectedByChecker_ForDebit_ForIF", myConnection);
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

        //public SqlDataReader UpdateReceivedEDRStatusApprovedByEBBSChecker(Guid EDRID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_Update_ReceivedEDR_Status_ApprovedByEBBSChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
        //    parameterEDRID.Value = EDRID;
        //    myCommand.Parameters.Add(parameterEDRID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public void UpdateReceivedEDRStatusApprovedByEBBSChecker(Guid EDRID, int approvedBy)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Update_ReceivedEDR_Status_ApprovedByEBBSChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = approvedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterApprovedBy);

            //DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
            //return myDT;
        }

        //public SqlDataReader UpdateReceivedEDRStatusRejectedByEBBSChecker(Guid EDRID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_Update_ReceivedEDR_Status_RejectedByEBBSChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
        //    parameterEDRID.Value = EDRID;
        //    myCommand.Parameters.Add(parameterEDRID);

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public void UpdateReceivedEDRStatusRejectedByEBBSChecker(Guid EDRID, int ApprovedBy)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Update_ReceivedEDR_Status_RejectedByEBBSChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterApprovedBy);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
            //return myDT;
        }

        public void UpdateHoldStatus(Guid EDRID, int Hold)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateHoldStatus", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterHold = new SqlParameter("@Hold", SqlDbType.Int);
            parameterHold.Value = Hold;
            myAdapter.SelectCommand.Parameters.Add(parameterHold);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
            //return myDT;
        }

        public void UpdateInwardTransactionDFIAccountNumberByEFTUser(
                                            Guid EDRID,
                                            string DFIAccountNo,
                                            int CreatedBy
                                         )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateInwardTransactionDFIAccountNumberByEFTUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myCommand.Parameters.Add(parameterCreatedBy);


            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void AutoRemoveNonNumericNumberForSCB()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Update_ReceivedEDR_DFIAccountNumberForSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();

        }


        public string GetReceivedEDRAccountNo_By_EDRID(Guid EDRID, ref string TranType, string connectionString, ref double Amount)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            SqlCommand myCommand = new SqlCommand("EFT_GetReceivedEDRAccountNo_By_EDRID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandTimeout = 600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.VarChar, 17);
            parameterDFIAccountNo.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterTranType = new SqlParameter("@TranType", SqlDbType.VarChar, 2);
            parameterTranType.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterTranType);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterAmount);

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            string DFIAccountNo = parameterDFIAccountNo.Value.ToString();
            TranType = parameterTranType.Value.ToString();
            Amount = ParseData.StringToDouble(parameterAmount.Value.ToString());

            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

            return DFIAccountNo;
        }

        public void UpdateReceivedEDRAmountAndAccountStatusFromCBS(Guid EDRID, double Amount, string CBSAccountStatus, string ReceiverNameFromCBS, string connectionString)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateReceivedEDRAmountAndAccountStatusFromCBS", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterAmount = new SqlParameter("@CBSAmount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterCBSAccountStatus = new SqlParameter("@CBSAccountStatus", SqlDbType.VarChar);
            parameterCBSAccountStatus.Value = CBSAccountStatus;
            myCommand.Parameters.Add(parameterCBSAccountStatus);

            SqlParameter parameterReceiverNameFromCBS = new SqlParameter("@ReceiverNameFromCBS", SqlDbType.VarChar);
            parameterReceiverNameFromCBS.Value = ReceiverNameFromCBS;
            myCommand.Parameters.Add(parameterReceiverNameFromCBS);

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable GetReceivedEDRForAdminForSCB(string BankCode, int DepartmentID, string RISKS, string STATUS)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ForAdmin_forSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterRISKS = new SqlParameter("@RISKS", SqlDbType.VarChar);
            parameterRISKS.Value = RISKS;
            myAdapter.SelectCommand.Parameters.Add(parameterRISKS);

            SqlParameter parameterSTATUS = new SqlParameter("@STATUS", SqlDbType.VarChar);
            parameterSTATUS.Value = STATUS;
            myAdapter.SelectCommand.Parameters.Add(parameterSTATUS);

            myConnection.Open();
            DataTable dt = new DataTable();
            myAdapter.Fill(dt);

            myConnection.Close();
            myAdapter.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDRForAdminForDebitForSCB(string BankCode, int DepartmentID, string RISKS, string STATUS)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ForAdmin_ForDebit_ForSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterRISKS = new SqlParameter("@RISKS", SqlDbType.VarChar);
            parameterRISKS.Value = RISKS;
            myAdapter.SelectCommand.Parameters.Add(parameterRISKS);

            SqlParameter parameterSTATUS = new SqlParameter("@STATUS", SqlDbType.VarChar);
            parameterSTATUS.Value = STATUS;
            myAdapter.SelectCommand.Parameters.Add(parameterSTATUS);

            myConnection.Open();
            DataTable dt = new DataTable();
            myAdapter.Fill(dt);

            myConnection.Close();
            myAdapter.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDR_ApprovedByMaker_ForChecker_forSCB(int BranchID, int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMaker_ForChecker_forSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myAdapter.Fill(dt);

            myConnection.Close();
            myAdapter.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDR_ApprovedByMaker_ForChecker_ForDebit_forSCB(int BranchID, int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMaker_ForChecker_ForDebit_forSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myAdapter.Fill(dt);

            myConnection.Close();
            myAdapter.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDR_ApprovedByMaker_ForChecker_Bankwise_forSCB(int BranchID,
                                    string BankCode,
                                    int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMaker_ForChecker_Bankwise_forSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myAdapter.Fill(dt);

            myConnection.Close();
            myAdapter.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDR_ApprovedByMaker_ForChecker_ForDebit_Bankwise_forSCB(int BranchID,
                                    string BankCode,
                                    int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMaker_ForChecker_ForDebit_Bankwise_forSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myAdapter.Fill(dt);

            myConnection.Close();
            myAdapter.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDR_ApprovedByMaker_forSCB(string TransactionReceivedDate,
                                    string EndingTransactionReceivedDate,
                                    int BranchID,
                                    int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMaker_forSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionReceivedDate = new SqlParameter("@TransactionReceivedDate", SqlDbType.VarChar);
            parameterTransactionReceivedDate.Value = TransactionReceivedDate;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionReceivedDate);

            SqlParameter parameterEndingTransactionReceivedDate = new SqlParameter("@EndingTransactionReceivedDate", SqlDbType.VarChar);
            parameterEndingTransactionReceivedDate.Value = EndingTransactionReceivedDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndingTransactionReceivedDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myAdapter.Fill(dt);

            myConnection.Close();
            myAdapter.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDR_ApprovedByMakerBySettlementJDate_forSCB(string TransReceivedSettlementDate,
                                                                                int BranchID,
                                                                                int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMakerBySettlementJDate_forSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransReceivedSettlementDate = new SqlParameter("@TransReceivedSettlementDate", SqlDbType.VarChar);
            parameterTransReceivedSettlementDate.Value = TransReceivedSettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterTransReceivedSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myAdapter.Fill(dt);

            myConnection.Close();
            myAdapter.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDR_ForAdmin_forSCB(string BankCode,
                                                        int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ForAdmin_forSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myAdapter.Fill(dt);

            myConnection.Close();
            myAdapter.Dispose();
            myConnection.Dispose();

            return dt;
        }
    }
}
