using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace FloraSoft
{
    public class SCBDDIDB
    {
        public int InsertSCBDDIBatch(string DebitorBankNumber,
                                        DateTime ProcessingDate,
                                        string DDIFilename,
                                        string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertSCBDDIBatch", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDebitorBankNumber = new SqlParameter("@DebitorBankNumber", SqlDbType.NVarChar, 20);
            parameterDebitorBankNumber.Value = DebitorBankNumber;
            myCommand.Parameters.Add(parameterDebitorBankNumber);

            SqlParameter parameterProcessingDate = new SqlParameter("@ProcessingDate", SqlDbType.DateTime);
            parameterProcessingDate.Value = ProcessingDate;
            myCommand.Parameters.Add(parameterProcessingDate);

            SqlParameter parameterDDIFilename = new SqlParameter("@DDIFilename", SqlDbType.VarChar);
            parameterDDIFilename.Value = DDIFilename;
            myCommand.Parameters.Add(parameterDDIFilename);

            SqlParameter parameterDDIBatchID = new SqlParameter("@DDIBatchID", SqlDbType.Int);
            parameterDDIBatchID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterDDIBatchID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            return (int)parameterDDIBatchID.Value;
        }

        public void InsertSCBDDITransaction(int DDIBatchID,
                                                    string Reason,
                                                    string SenderAccountNo,
                                                    string ReceivingBankRoutingNo,
                                                    string DFIAccountNo,
                                                    string TransactionCode,
                                                    double Amount,
                                                    string IdNumber,
                                                    string ReceiverName,
                                                    string ReferenceNo,
                                                    string DebitorAccountName,
                                                    string ReturnCode,
                                                    string ReturnReason,
                                                    DateTime SettelementDate,
                                                    int StatusID,
                                                    string ConnectionString
                                                 )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertSCBDDITransaction", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDDIBatchID = new SqlParameter("@DDIBatchID", SqlDbType.Int);
            parameterDDIBatchID.Value = DDIBatchID;
            myCommand.Parameters.Add(parameterDDIBatchID);

            SqlParameter parameterReason = new SqlParameter("@Reason", SqlDbType.NVarChar, 200);
            parameterReason.Value = Reason;
            myCommand.Parameters.Add(parameterReason);

            SqlParameter parameterSenderAccountNo = new SqlParameter("@SenderAccountNo", SqlDbType.VarChar);
            parameterSenderAccountNo.Value = SenderAccountNo;
            myCommand.Parameters.Add(parameterSenderAccountNo);

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            myCommand.Parameters.Add(parameterReceivingBankRoutingNo);

            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.VarChar);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterTransactionCode = new SqlParameter("@TransactionCode", SqlDbType.NVarChar);
            parameterTransactionCode.Value = TransactionCode;
            myCommand.Parameters.Add(parameterTransactionCode);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 22);
            parameterIdNumber.Value = IdNumber;
            myCommand.Parameters.Add(parameterIdNumber);

            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            parameterReceiverName.Value = ReceiverName;
            myCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterReferenceNo = new SqlParameter("@ReferenceNo", SqlDbType.VarChar);
            parameterReferenceNo.Value = ReferenceNo;
            myCommand.Parameters.Add(parameterReferenceNo);

            SqlParameter parameterDebitorAccountName = new SqlParameter("@DebitorAccountName", SqlDbType.VarChar);
            parameterDebitorAccountName.Value = DebitorAccountName;
            myCommand.Parameters.Add(parameterDebitorAccountName);

            SqlParameter parameterReturnCode = new SqlParameter("@ReturnCode", SqlDbType.VarChar);
            parameterReturnCode.Value = ReturnCode;
            myCommand.Parameters.Add(parameterReturnCode);

            SqlParameter parameterReturnReason = new SqlParameter("@ReturnReason", SqlDbType.VarChar);
            parameterReturnReason.Value = ReturnReason;
            myCommand.Parameters.Add(parameterReturnReason);

            SqlParameter parameterSettelementDate = new SqlParameter("@SettelementDate", SqlDbType.DateTime);
            parameterSettelementDate.Value = SettelementDate;
            myCommand.Parameters.Add(parameterSettelementDate);

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myCommand.Parameters.Add(parameterStatusID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
        }

        public Guid InsertBatchSentForDDI(
                                            int CreatedBy,
                                            int DepartmentID,
                                            int DDIBatchID,
                                            string ConnectionString
                                         )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertBatchSent_ForDDI", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterDDIBatchID = new SqlParameter("@DDIBatchID", SqlDbType.Int);
            parameterDDIBatchID.Value = DDIBatchID;
            myCommand.Parameters.Add(parameterDDIBatchID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            return (Guid)parameterTransactionID.Value;
        }


        //
        public void InsertTransactionSentforDDI(
                                                            Guid TransactionID,
                                                            int DDIBatchID,
                                                            int CreatedBy,
                                                            int DepartmentID,
                                                            string ConnectionString
                                                        )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertTransactionSent_forDDI", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            //SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            //parameterEDRID.Direction = ParameterDirection.Output;
            //myCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterDDIBatchID = new SqlParameter("@DDIBatchID", SqlDbType.Int);
            parameterDDIBatchID.Value = DDIBatchID;
            myCommand.Parameters.Add(parameterDDIBatchID);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myCommand.Parameters.Add(parameterDepartmentID);


            myConnection.Open();
            myCommand.ExecuteNonQuery();
        }

        public DataTable GetDDITransactionsForReturnByDDIBatchID(string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetDDITransactions_ForReturnByDDIBatchID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetRptDDIDebit_TransactionSentBySettlementDate(string SettlementDate, string currency, int session, string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptDDIDebit_TransactionSentBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.Int);
            paramSession.Value = session;
            myAdapter.SelectCommand.Parameters.Add(paramSession);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetRptDDIDebit_ReturnReceivedBySettlementDate(string SettlementDate, string currency, int session, string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptDDIDebit_ReturnReceivedBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.Int);
            paramSession.Value = session;
            myAdapter.SelectCommand.Parameters.Add(paramSession);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetRptDDIDebit_TransactionFromRCMSBySettlementDate(string SettlementDate, string currency, int session, string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptDDIDebit_TransactionFromRCMSBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.Int);
            paramSession.Value = session;
            myAdapter.SelectCommand.Parameters.Add(paramSession);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetRptDDIDebit_RCMSReturnAfterApprove(string SettlementDate, string currency, int session, string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptDDIDebit_RCMSReturnAfterApprove", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.Int);
            paramSession.Value = session;
            myAdapter.SelectCommand.Parameters.Add(paramSession);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetRptDDIDebit_RCMSInternalReturnBySettlementDate(string SettlementDate, string currency, int session, string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptDDIDebit_RCMSInternalReturnBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.Int);
            paramSession.Value = session;
            myAdapter.SelectCommand.Parameters.Add(paramSession);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetRptDDIBatchesBySettlementDate(string DDIBatchEntryDate, string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetDDIBatches_For_TransactionSent", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterDDIBatchEntryDate = new SqlParameter("@DDIBatchEntryDate", SqlDbType.VarChar);
            parameterDDIBatchEntryDate.Value = DDIBatchEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterDDIBatchEntryDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReceivedReturnForDDI(string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedReturn_ForDDI", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSCBDDIAccountStatus(string ConnectionString, string AccountStatus)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSCBDDIAccountStatus", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterAccountStatus = new SqlParameter("@AccountStatus", SqlDbType.NVarChar, 50);
            parameterAccountStatus.Value = AccountStatus;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountStatus);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public int InsertSCBDDIAccountStatus(string AccountNo,
                                            string OtherBankAcNo,
                                            string RoutingNumber,
                                            DateTime ExpiryDate,
                                            string ConnectionString,
                                            bool AccountException)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertSCBDDIAccountStatus", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
            parameterAccountNo.Value = AccountNo;
            myCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterOtherBankAcNo = new SqlParameter("@OtherBankAcNo", SqlDbType.VarChar);
            parameterOtherBankAcNo.Value = OtherBankAcNo;
            myCommand.Parameters.Add(parameterOtherBankAcNo);

            SqlParameter parameterRoutingNumber = new SqlParameter("@RoutingNumber", SqlDbType.NChar);
            parameterRoutingNumber.Value = RoutingNumber;
            myCommand.Parameters.Add(parameterRoutingNumber);

            SqlParameter parameterExpiryDate = new SqlParameter("@ExpiryDate", SqlDbType.DateTime);
            parameterExpiryDate.Value = ExpiryDate;
            myCommand.Parameters.Add(parameterExpiryDate);

            SqlParameter parameterAccountException = new SqlParameter("@AccountException", SqlDbType.Bit);
            parameterAccountException.Value = AccountException;
            myCommand.Parameters.Add(parameterAccountException);

            SqlParameter parameterInsertResult = new SqlParameter("@InsertResult", SqlDbType.Int);
            parameterInsertResult.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterInsertResult);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            return (int)parameterInsertResult.Value;
        }

        public void UpdateSCBDDIAccountStatus(int AccountID,
                                                        string AccountNo,
                                                        string OtherBankAcNo,
                                                        string RoutingNumber,
                                                        DateTime ExpiryDate,
                                                        string ConnectionString,
                                                        bool AccountException)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("EFT_UpdateSCBDDIAccountStatus", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterAccountID = new SqlParameter("@AccountID", SqlDbType.Int);
            parameterAccountID.Value = AccountID;
            myCommand.Parameters.Add(parameterAccountID);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
            parameterAccountNo.Value = AccountNo;
            myCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterOtherBankAcNo = new SqlParameter("@OtherBankAcNo", SqlDbType.VarChar);
            parameterOtherBankAcNo.Value = OtherBankAcNo;
            myCommand.Parameters.Add(parameterOtherBankAcNo);

            SqlParameter parameterRoutingNumber = new SqlParameter("@RoutingNumber", SqlDbType.NChar);
            parameterRoutingNumber.Value = RoutingNumber;
            myCommand.Parameters.Add(parameterRoutingNumber);

            SqlParameter parameterExpiryDate = new SqlParameter("@ExpiryDate", SqlDbType.DateTime);
            parameterExpiryDate.Value = ExpiryDate;
            myCommand.Parameters.Add(parameterExpiryDate);

            SqlParameter parameterAccountException = new SqlParameter("@AccountException", SqlDbType.Bit);
            parameterAccountException.Value = AccountException;
            myCommand.Parameters.Add(parameterAccountException);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        //EFT_DeleteSCBDDIAccountStatus
        public void DeleteSCBDDIAccountStatus(int AccountID,
                                              string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("EFT_DeleteSCBDDIAccountStatus", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterAccountID = new SqlParameter("@AccountID", SqlDbType.Int);
            parameterAccountID.Value = AccountID;
            myCommand.Parameters.Add(parameterAccountID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }


        public void UpdateSCBDDIAccountStatusByChecker(int AccountID,
                                                        string AccountStatus,
                                                        string ConnectionString
                                                        )
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("EFT_SCBDDIChangeAccountStatusByChecker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterAccountID = new SqlParameter("@AccountID", SqlDbType.Int);
            parameterAccountID.Value = AccountID;
            myCommand.Parameters.Add(parameterAccountID);

            SqlParameter parameterAccountStatus = new SqlParameter("@AccountStatus", SqlDbType.NVarChar, 20);
            parameterAccountStatus.Value = AccountStatus;
            myCommand.Parameters.Add(parameterAccountStatus);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        public DataTable GetCountsDDITransactions(string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetCountsDDITransactions_ForReturnByDDIBatchID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetDDITransactionStatusReportByDateRange(string ConnectionString, string FromEntryDate,
                                                                        string EndEntryDate, int StatusID,
                                                                        string currency, int session)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetDDITransactionStatusReportByDateRange", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterFromEntryDate = new SqlParameter("@FromEntryDate", SqlDbType.VarChar);
            parameterFromEntryDate.Value = FromEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterFromEntryDate);

            SqlParameter parameterEndEntryDate = new SqlParameter("@EndEntryDate", SqlDbType.VarChar);
            parameterEndEntryDate.Value = EndEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndEntryDate);

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myAdapter.SelectCommand.Parameters.Add(parameterStatusID);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.Int);
            paramSession.Value = session;
            myAdapter.SelectCommand.Parameters.Add(paramSession);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}

