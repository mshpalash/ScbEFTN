using System;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using EFTN.Utility;
using EFTN.BLL;
using System.IO;
using System.Collections.Generic;

namespace EFTN.component
{
    public class EFTCsvScbBulkDB
    {
        private int typeOfPayment;
        public int TypeOfPayment
        {
            get { return typeOfPayment; }
        }

        private int createdBy;
        public int CreatedBy
        {
            get { return createdBy; }
        }

        private int batchStatus = 0;
        public int BatchStatus
        {
            get { return batchStatus; }
        }

        private string eFTTransactionType = string.Empty;

        //private int departmentID;
        //public int DepartmentID
        //{
        //    get { return departmentID; }
        //}

        private string dataEntryType = "Csv";
        public int cBSSettlementDay = 1;
        public void UploadBulkCsvScbDataToTempTableOld(string sqlServerConnectionString
                                                    , DataTable dtTransactions)
        {
            SqlBulkCopy bulkinsert = new SqlBulkCopy(sqlServerConnectionString);
            bulkinsert.DestinationTableName = "dbo.EFT_SCBCsvTemp";

            bulkinsert.ColumnMappings.Add("[Customer ID]", "[Customer ID]");
            bulkinsert.ColumnMappings.Add("[Batch Ref#]", "[Batch Ref.]");
            bulkinsert.ColumnMappings.Add("[Sub-Batch No#]", "[Sub-Batch No.]");
            bulkinsert.ColumnMappings.Add("[Payment Ref#]", "[Payment Ref.]");
            bulkinsert.ColumnMappings.Add("[Status]", "[Status]");
            bulkinsert.ColumnMappings.Add("[Debit Account No#]", "[Debit Account No.]");
            bulkinsert.ColumnMappings.Add("[Debit Amount]", "[Debit Amount]");
            bulkinsert.ColumnMappings.Add("[Debit Date]", "[Debit Date]");
            bulkinsert.ColumnMappings.Add("[Debit Status]", "[Debit Status]");
            bulkinsert.ColumnMappings.Add("[Payment Currency]", "[Payment Currency]");
            bulkinsert.ColumnMappings.Add("[Credit Amount]", "[Credit Amount]");
            bulkinsert.ColumnMappings.Add("[Invoice Amount]", "[Invoice Amount]");
            bulkinsert.ColumnMappings.Add("[Payee Amount]", "[Payee Amount]");
            bulkinsert.ColumnMappings.Add("[Payee Charge]", "[Payee Charge]");
            bulkinsert.ColumnMappings.Add("[Payment Date]", "[Payment Date]");
            bulkinsert.ColumnMappings.Add("[Processing Date]", "[Processing Date]");
            bulkinsert.ColumnMappings.Add("[Payee Name]", "[Payee Name]");
            bulkinsert.ColumnMappings.Add("[Beneficiary Account]", "[Beneficiary Account]");
            bulkinsert.ColumnMappings.Add("[Beneficiary Bank code]", "[Beneficiary Bank code]");
            bulkinsert.ColumnMappings.Add("[Customer Ref#]", "[Customer Ref.]");
            bulkinsert.ColumnMappings.Add("[Payee Name 1]", "[Payee Name 1]");
            bulkinsert.ColumnMappings.Add("[Local Bank Clearing Code]", "[Local Bank Clearing Code]");
            bulkinsert.ColumnMappings.Add("[Payee Address 1 BO]", "[Payee Address 1 BO]");
            bulkinsert.ColumnMappings.Add("[Payee Address 2 BO]", "[Payee Address 2 BO]");
            bulkinsert.ColumnMappings.Add("[Payee Details 1 BO]", "[Payee Details 1 BO]");

            bulkinsert.WriteToServer(dtTransactions);

            //bulkinsert.WriteToServer(dtTransactions);
        }

        public void UploadBulkCsvScbDataToTempTable(string sqlServerConnectionString
                                            , DataTable dtTransactions)
        {
            SqlBulkCopy bulkinsert = new SqlBulkCopy(sqlServerConnectionString);
            bulkinsert.DestinationTableName = "dbo.EFT_SCBCsvTemp";

            bulkinsert.ColumnMappings.Add("[Customer ID]", "[Customer ID]");
            bulkinsert.ColumnMappings.Add("[Batch Ref.]", "[Batch Ref.]");
            bulkinsert.ColumnMappings.Add("[Sub-Batch No.]", "[Sub-Batch No.]");
            bulkinsert.ColumnMappings.Add("[Payment Ref.]", "[Payment Ref.]");
            bulkinsert.ColumnMappings.Add("[Status]", "[Status]");
            bulkinsert.ColumnMappings.Add("[Debit Account No.]", "[Debit Account No.]");
            bulkinsert.ColumnMappings.Add("[Debit Amount]", "[Debit Amount]");
            bulkinsert.ColumnMappings.Add("[Debit Date]", "[Debit Date]");
            bulkinsert.ColumnMappings.Add("[Debit Status]", "[Debit Status]");
            bulkinsert.ColumnMappings.Add("[Payment Currency]", "[Payment Currency]");
            bulkinsert.ColumnMappings.Add("[Credit Amount]", "[Credit Amount]");
            bulkinsert.ColumnMappings.Add("[Invoice Amount]", "[Invoice Amount]");
            bulkinsert.ColumnMappings.Add("[Payee Amount]", "[Payee Amount]");
            bulkinsert.ColumnMappings.Add("[Payee Charge]", "[Payee Charge]");
            bulkinsert.ColumnMappings.Add("[Payment Date]", "[Payment Date]");
            bulkinsert.ColumnMappings.Add("[Processing Date]", "[Processing Date]");
            bulkinsert.ColumnMappings.Add("[Payee Name]", "[Payee Name]");
            bulkinsert.ColumnMappings.Add("[Beneficiary Account]", "[Beneficiary Account]");
            bulkinsert.ColumnMappings.Add("[Beneficiary Bank code]", "[Beneficiary Bank code]");
            bulkinsert.ColumnMappings.Add("[Customer Ref.]", "[Customer Ref.]");
            //bulkinsert.ColumnMappings.Add("[Payee Name 1]", "[Payee Name 1]");
            bulkinsert.ColumnMappings.Add("[Local Bank Clearing Code]", "[Local Bank Clearing Code]");
            bulkinsert.ColumnMappings.Add("[Payee Address 1 BO]", "[Payee Address 1 BO]");
            bulkinsert.ColumnMappings.Add("[Payee Address 2 BO]", "[Payee Address 2 BO]");
            bulkinsert.ColumnMappings.Add("[Payee Details 1 BO]", "[Payee Details 1 BO]");
            //bulkinsert.ColumnMappings.Add("[Customer Name]", "[Customer Name]");

            bulkinsert.WriteToServer(dtTransactions);
        }

        public void DistributeBatchWithEntryDescription(int createBy, int departmentID, string conStr, string entryDescription)
        {
            /**** Get data by distinct accountno and customer name ****/
            DataTable dtDistinctData = EFT_GetDistinctScbCsvBatchInfo_ToCreateBatch(conStr);

            /**** Create Batch, assign TransactionID and transafer transactions into the sentEDR Table ****/
            string strCompanyID = string.Empty;
            string bulkTransactionType = string.Empty;
            bulkTransactionType = dtDistinctData.Rows[0]["BulkTransactionType"].ToString();

            /**** Defining transaction type here based on BulkTransactionType  ****/
            if (bulkTransactionType == "C")
            {
                this.eFTTransactionType = "CREDIT";
            }
            else
            {
                this.eFTTransactionType = "DEBIT";
            }
            string AccountNo = dtDistinctData.Rows[0]["AccountNo"].ToString();
            string CustomerName = dtDistinctData.Rows[0]["CustomerName"].ToString().Trim();
            strCompanyID = AccountNo;

            /**** Insert Batch ****/
            Guid TransactionID = InsertIntoBatchWithEntryDescription(createBy, departmentID, strCompanyID, CustomerName, entryDescription);
            /**** Update TransactionID from Batch into Temp table  ****/
            UpdateTransactionIDForScbCsvTemp_ToCreateBatch(conStr, TransactionID, AccountNo, entryDescription);
            /**** Transfer Transactions from temp table to SentEDR table & truncate temp table ****/
            Transfer_ToSentEDR_FromCsvTransactionsTemp(conStr);
            #region Old_ForLoopUsed
            //for (int i = 0; i < dtDistinctData.Rows.Count; i++)
            //{
            //    string AccountNo = dtDistinctData.Rows[i]["AccountNo"].ToString();
            //    string CompanyEntryDesc = dtDistinctData.Rows[i]["CompanyEntryDesc"].ToString();
            //    string CustomerName = dtDistinctData.Rows[i]["CustomerName"].ToString().Trim();
            //    strCompanyID = AccountNo;

            //    /***************************************************************************/
            //    if (CustomerName.Length > EFTLength.CompanyNameLength)
            //    {
            //        CustomerName = CustomerName.Substring(0, EFTLength.CompanyNameLength);
            //    }
            //    //Create Batch and Get TransactionID for CSV
            //    Guid TransactionID = InsertIntoBatchWithEntryDescription(createBy, departmentID, strCompanyID, CustomerName, entryDescription);

            //    #region Commented out for STS Update
            //    //Assign TransactionID in csv SentEDR table
            //    //csvDB.UpdateTransactionIDForScbCsvTemp(conStr, TransactionID, AccountNo, entryDescription);
            //    #endregion

            //    //Assign TransactionID in csv SentEDR table
            //    UpdateTransactionIDForScbCsvTemp_ToCreateBatch(conStr, TransactionID, AccountNo, entryDescription);
            //}
            #endregion
        }
        private string GetServiceClassCode()
        {
            string serviceClassCode = "200";
            switch (this.typeOfPayment)
            {
                case 0:
                    serviceClassCode = "200";
                    break;
                case 1:
                    serviceClassCode = "200";
                    break;
                case 2:
                    serviceClassCode = "200";
                    break;
                case 8:
                    serviceClassCode = "200";
                    break;
                case 6:
                    serviceClassCode = "200";
                    break;
            }
            return serviceClassCode;
        }

        private string GetStandardEntryClassCode()
        {
            string secc = "CCD";
            switch (this.typeOfPayment)
            {
                case 0:
                    secc = "CCD";
                    break;
                case 1:
                    secc = "CCD";
                    break;
                case 2:
                    secc = "CIE";
                    break;
                case 8:
                    secc = "CTX";
                    break;
                case 6:
                    secc = "PPD";
                    break;
            }
            return secc;
        }

        private Guid InsertIntoBatchWithEntryDescription(int createBy, int departmentID, string strCompanyID, string strCompanyName, string strReasonForPayment)
        {
            int envelopeID = -1;
            string serviceClassCode = this.GetServiceClassCode();
            string secc = this.GetStandardEntryClassCode();
            DateTime effectiveEntryDate = DateTime.Now;
            //new add for distributed batch///////////////////
            //////////////////////////////////////////////////

            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            Guid transactionID = db.InsertBatchSentWithSettlementDay(
                    envelopeID,
                    serviceClassCode,
                    secc,
                    this.typeOfPayment,
                    effectiveEntryDate,
                    //this.companyId,
                    //this.companyName,
                    //this.reasonForPayment,
                    strCompanyID,
                    strCompanyName,
                    strReasonForPayment,
                    createBy, this.batchStatus, this.eFTTransactionType
                    , departmentID, this.dataEntryType, this.cBSSettlementDay
                    , "");
            return transactionID;

        }

        public void UpdateTempSentEDRColumnForScbCSV_ToCreateBatch
        (
           Guid TransactionID,
           int CreatedBy,
           string TransactionType,
           int TypeOfPayment,
           int DepartmentID,
           string myConnection
        )
        {
            SqlConnection connection = new SqlConnection(myConnection);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "STS_UpdateTempSentEDRColumnScbCsv_ToCreateBatch";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Value = TransactionID;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            paramCreatedBy.Value = CreatedBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter paramTransactionType = new SqlParameter("@TransactionType", SqlDbType.VarChar, 10);
            paramTransactionType.Value = TransactionType;
            command.Parameters.Add(paramTransactionType);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = TypeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);


            SqlParameter paramDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            paramDepartmentID.Value = DepartmentID;
            command.Parameters.Add(paramDepartmentID);

            connection.Open();

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }
        public void UpdateTempSentEDRColumnForScbCSV
         (
            Guid TransactionID,
            int CreatedBy,
            string TransactionType,
            int TypeOfPayment,
            int DepartmentID,
            string myConnection,
            string currency
         )
        {
            SqlConnection connection = new SqlConnection(myConnection);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_UpdateTempSentEDRColumnScbCsv";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Value = TransactionID;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            paramCreatedBy.Value = CreatedBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter paramTransactionType = new SqlParameter("@TransactionType", SqlDbType.VarChar, 10);
            paramTransactionType.Value = TransactionType;
            command.Parameters.Add(paramTransactionType);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = TypeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);


            SqlParameter paramDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            paramDepartmentID.Value = DepartmentID;
            command.Parameters.Add(paramDepartmentID);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            command.Parameters.Add(paramCurrency);

            connection.Open();

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public void TransferTransactionSent_FromBulkEntryCsvSCB(string constr)
        {
            SqlConnection connection = new SqlConnection(constr);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertTransactionSent_FromBulkEntryCsvScb";
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public void Transfer_ToSentEDR_FromCsvTransactionsTemp(string constr)
        {
            SqlConnection connection = new SqlConnection(constr);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "STS_InsertTransactionSent_FromBulkEntryCsvScb";
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public void TransferTransactionSent_FromBulkEntryCsvScbRejectedData(string constr)
        {
            SqlConnection connection = new SqlConnection(constr);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertTransactionSent_FromBulkEntryCsvScbRejectedData";
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }
        public void TransferTransactionSent_FromBulkEntryNonBDT(string constr)
        {
            SqlConnection connection = new SqlConnection(constr);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertTransactionSent_FromBulkEntryCsvScbNonBDT";
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }
        public void TruncateTempSentEDRCsvTable(string constr)
        {
            SqlConnection connection = new SqlConnection(constr);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_TruncateTempSentEDRCsvTable";
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public void ValidateTempSentEDRColumnForScbCsv(string constr, string OriginBankCode)
        {
            SqlConnection connection = new SqlConnection(constr);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_ValidateTempSentEDRColumnScbCsv";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterOriginBankCode = new SqlParameter("@OriginBankCode", SqlDbType.VarChar);
            parameterOriginBankCode.Value = OriginBankCode;
            command.Parameters.Add(parameterOriginBankCode);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public DataTable EFT_GetDistinctScbCsvBatchInfo_ToCreateBatch(string sqlCon)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlCon);
            SqlDataAdapter sdaAdapter = new SqlDataAdapter("STS_GetDistinctScbCsvBatchInfo_ToCreateBatch", sqlConnection);
            sdaAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sdaAdapter.SelectCommand.CommandTimeout = 600;

            DataTable myDT = new DataTable();
            sqlConnection.Open();
            sdaAdapter.Fill(myDT);
            sqlConnection.Close();
            return myDT;
        }

        public DataTable GetDistinctScbCsvBatchInfo(string conStr)
        {
            SqlConnection myConnection = new SqlConnection(conStr);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetDistinctScbCsvBatchInfo", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public void UpdateTransactionIDForScbCsvTemp
         (
            string myConnection,
            Guid TransactionID,
            string AccountNo,
            string CompanyEntryDesc
         )
        {
            SqlConnection connection = new SqlConnection(myConnection);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = connection;
            myCommand.CommandText = "EFT_UpdateTransactionIDForScbCsvTemp";
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
            parameterAccountNo.Value = AccountNo;
            myCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterCompanyEntryDesc = new SqlParameter("@CompanyEntryDesc", SqlDbType.VarChar);
            parameterCompanyEntryDesc.Value = CompanyEntryDesc;
            myCommand.Parameters.Add(parameterCompanyEntryDesc);

            connection.Open();

            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public void UpdateTransactionIDForScbCsvTemp_ToCreateBatch
         (
            string myConnection,
            Guid TransactionID,
            string AccountNo,
            string CompanyEntryDesc
         )
        {
            SqlConnection connection = new SqlConnection(myConnection);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = connection;
            myCommand.CommandText = "STS_UpdateTransactionIDForScbCsvTemp_ToCreateBatch";
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
            parameterAccountNo.Value = AccountNo;
            myCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterCompanyEntryDesc = new SqlParameter("@CompanyEntryDesc", SqlDbType.VarChar);
            parameterCompanyEntryDesc.Value = CompanyEntryDesc;
            myCommand.Parameters.Add(parameterCompanyEntryDesc);

            connection.Open();

            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public DataTable GetDistinctTransactionIDFromScbCsvTemp(string strConn)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(strConn);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetDistinctTransactionIDFromScbCsvTemp", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public void UpdateEDRSentto1000ForCSVSCB(string strConn)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(strConn);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateEDRSentto1000ForCSVSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
        }

        public DataTable GetBulkTransactionSentUploadedOnlyCsvSCB(int DepartmentID, string strConn)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(strConn);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBulkTransactionSentUploadedOnlyCsvSCB", myConnection);
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

        public DataTable GetBulkTransactionsUploadedFromSCBTempToFilterPage(string strConn)
        {

            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("STS_GetBulkTransactionsUploadedForFilterList", sqlConnection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.CommandTimeout = 600;

            DataTable data = new DataTable();
            sqlConnection.Open();
            sqlAdapter.Fill(data);
            sqlConnection.Close();
            return data;
        }

        public DataTable GetFilteredDataFromSCBCsvTemp(string strConn, string accountNo, string customerName, string paymentInfo)
        {
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("STS_GetFilteredTransactionsFromSCBCsvTemp", sqlConnection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
            paramAccountNo.Value = accountNo;
            sqlAdapter.SelectCommand.Parameters.Add(paramAccountNo);

            SqlParameter paramCustomerName = new SqlParameter("@CustomerName", SqlDbType.VarChar);
            paramCustomerName.Value = customerName;
            sqlAdapter.SelectCommand.Parameters.Add(paramCustomerName);

            SqlParameter paramPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.VarChar);
            paramPaymentInfo.Value = paymentInfo;
            sqlAdapter.SelectCommand.Parameters.Add(paramPaymentInfo);

            DataTable data = new DataTable();
            sqlConnection.Open();
            sqlAdapter.Fill(data);
            sqlConnection.Close();
            return data;
        }


        public void UpdateTransactionAndMoveForBatch(string strConn, int recordId)
        {
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "[STS_UpdateTransactionAndMoveForBatch]";
            command.CommandType = CommandType.StoredProcedure;


            SqlParameter paramRecordId = new SqlParameter("@RecordId", SqlDbType.Int);
            paramRecordId.Value = recordId;
            command.Parameters.Add(paramRecordId);

            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();

            command.Dispose();
            sqlConnection.Dispose();
        }


        public DataTable GetMovedTransactionsToCreateBatch(string strConn)
        {
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("STS_GetMovedBulkTransactionsToCreateBatch", sqlConnection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.CommandTimeout = 600;

            DataTable data = new DataTable();
            sqlConnection.Open();
            sqlAdapter.Fill(data);
            sqlConnection.Close();
            return data;
        }

        public void DeleteCsvTempData(string sqlConnection, int createdBy, bool isFromCancel)
        {
            string spName = string.Empty;
            int spFlag = 0;
            if (isFromCancel)
            {
                spName = "EFT_DeleteBatch_EDR_AND_SCBCsvTempTableData";
                spFlag = 1;
            }
            else
            {
                spName = "EFT_DeleteSCBCsvTempTable";
            }
            SqlConnection connection = new SqlConnection(sqlConnection);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = spName;
            command.CommandType = CommandType.StoredProcedure;
            if (spFlag.Equals(1))
            {
                SqlParameter paramCreatedBy = new SqlParameter("@UserId", SqlDbType.Int);
                paramCreatedBy.Value = createdBy;
                command.Parameters.Add(paramCreatedBy);
            }
            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public DataTable GetFilteredDataFromBulkDataByCurrency(DataTable bulkData, string columnName, string currency)
        {
            DataTable filteredData = new DataTable();
            return filteredData = bulkData.Select(columnName + "='" + currency + "'").CopyToDataTable();
        }
        public List<string> GetCurrenciesDynamicallyFromUploadedFile(DataTable bulkData, string columnName)
        {
            List<string> currencies = new List<string>(bulkData.Rows.Count);
            string currencyToBeAdded = string.Empty;
            foreach (DataRow item in bulkData.Rows)
            {
                currencyToBeAdded = (string)item[columnName];
                if (!currencies.Contains(currencyToBeAdded))
                {
                    currencies.Add(currencyToBeAdded);
                }
            }
            return currencies;
        }
        public DataTable CheckIfRemainingTransactionsAvailableForThisUser(string dbConnection, int createdBy)
        {
            DataTable remainingData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(dbConnection);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("CheckIfRemainingTransactionsAvailableForThisUser", sqlConnection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter paramUser = new SqlParameter("@UserId", SqlDbType.Int);
            paramUser.Value = createdBy;
            sqlAdapter.SelectCommand.Parameters.Add(paramUser);



            sqlConnection.Open();
            sqlAdapter.Fill(remainingData);
            sqlConnection.Close();
            return remainingData;
        }
    }
}