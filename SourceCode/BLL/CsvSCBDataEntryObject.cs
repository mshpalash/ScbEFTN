using System;
using System.Data;
using System.Configuration;
using EFTN.Utility;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace EFTN.BLL
{
    public class CsvSCBDataEntryObject
    {
        #region properties
        private string companyId;

        public string CompanyId
        {
            get { return companyId; }
        }

        private int typeOfPayment;

        public int TypeOfPayment
        {
            get { return typeOfPayment; }
        }

        private string companyName;

        public string CompanyName
        {
            get { return companyName; }

        }

        private string reasonForPayment;

        public string ReasonForPayment
        {
            get { return reasonForPayment; }
        }

        private string textFilePath;

        public string TextFilePath
        {
            get { return textFilePath; }
        }

        private int createdBy;

        public int CreatedBy
        {
            get { return createdBy; }
        }

        private int batchStatus;

        public int BatchStatus
        {
            get { return batchStatus; }
        }

        private string eFTTransactionType;

        public string EFTTransactionType
        {
            get { return eFTTransactionType; }
        }

        private Guid transactionID;

        public Guid TransactionID
        {
            get { return transactionID; }
        }

        private string batchNumber;

        public string BatchNumber
        {
            get { return batchNumber; }
        }

        private int departmentID;

        public int DepartmentID
        {
            get { return departmentID; }
        }

        private string dataEntryType;

        public string DataEntryType
        {
            get { return dataEntryType; }
        }

        public int cBSSettlementDay;

        public int CBSSettlementDay
        {
            get { return cBSSettlementDay; }
        }

        private string connectionString;

        public string ConnectionString
        {
            get { return connectionString; }
        }


        private int sentEDRCount = 0;
        #endregion

        #region Constructor
        public CsvSCBDataEntryObject(
            string companyId,
            int typeOfPayment,
            string companyName,
            string reasonForPayment,
            string textFilePath,
            int createdBy,
            int batchStatus,
            string eFTTransactionType,
            int departmentID,
            string dataEntryType,
            int cbsSettlementDay,
            string connectionString)
        {
            this.companyId = companyId;
            this.typeOfPayment = typeOfPayment;
            this.companyName = companyName;
            this.reasonForPayment = reasonForPayment;
            this.textFilePath = textFilePath;
            this.createdBy = createdBy;
            this.batchStatus = batchStatus;
            this.eFTTransactionType = eFTTransactionType;
            this.departmentID = departmentID;
            this.dataEntryType = dataEntryType;
            this.cBSSettlementDay = cbsSettlementDay;
            this.connectionString = connectionString;
        }

        #endregion

        #region Commented out for merging STS updates between live system and discutributed batch
        //public void EntryData()
        //{
        //    this.transactionID = this.InsertIntoBatch();        // Unblocked to retrieve the live operation at SCB

        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        //dt = InsertEDR(transactionID);                  // Blocked to retrieve the live operation at SCB
        //        //ValidateCSVEFTForSCB(this.textFilePath, typeOfPayment);   // Blocked to retrieve the live operation at SCB
        //        ValidateCSVEFTForSCB(this.textFilePath, typeOfPayment, transactionID);
        //        this.batchNumber = GetBatchNumberByTransactionID();     // Unblocked to retrieve the live operation at SCB
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sentEDRCount > 0)
        //        {
        //            EFTN.component.SentBatchDB.DeleteSentBatch(transactionID);
        //            EFTN.component.SentBatchDB.DeleteSentEDR(transactionID);
        //        }
        //    }
        //    finally
        //    {
        //        System.IO.File.Delete(this.textFilePath);
        //    }
        //    //return dt;
        //}
        #endregion

        #region New Merged Scope for STS Changed on_03-01-2018  
        //Currency removed for uploading mupltiple currencies transactions dynamically
        //public DataTable EntryData(string currency)


        public DataTable EntryData(int createBy)
        {
            #region Commented out for multiple currencies source upload 01-March-2018
            //this.transactionID = this.InsertIntoBatch(currency);
            #endregion

            DataTable dt = new DataTable();
            try
            {
                #region Commented out the below line due transaction Id removed from the function   
                //dt = ValidateCSVEFTForSCB(this.textFilePath, typeOfPayment, transactionID);
                #endregion

                dt = ValidateCSVEFTForSCB(this.textFilePath, typeOfPayment, createdBy);

                #region Commented out below codes due to upload multiple currencies BACH II _ 05-03-2018
                //if (dt.Rows.Count > 0)
                //{
                //    this.batchNumber = GetBatchNumberByTransactionID();
                //}
                #endregion
            }
            catch (Exception ex)
            {
                if (sentEDRCount > 0)
                {
                    EFTN.component.SentBatchDB.DeleteSentBatch(transactionID);
                    EFTN.component.SentBatchDB.DeleteSentEDR(transactionID);
                }
            }
            finally
            {
                System.IO.File.Delete(this.textFilePath);
            }
            return dt;
        }
        #endregion


        private string GetBatchNumberByTransactionID()
        {
            EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
            DataTable dtBatchNumber = sentBatchDB.GetBatchNumberByTransactionID(this.transactionID);
            string batchnumber = string.Empty;
            if (dtBatchNumber.Rows.Count > 0)
            {
                batchnumber = dtBatchNumber.Rows[0]["BatchNumber"].ToString();
            }
            return batchnumber;
        }

        private DataTable InsertEDR(Guid transactionID)
        {
            //EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();
            //string delim = ConfigurationManager.AppSettings["CSVDelim"];
            //DataTable data = DelimitedTextReader.ReadFile(this.textFilePath, delim);
            EFTCSVReader eftCSVReader = new EFTCSVReader();
            DataTable data = eftCSVReader.ReadCSV(this.textFilePath);
            //DataTable data = ImportDelimitedFile.ReadDelimitedFile(this.textFilePath, delim).Copy();

            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();
            foreach (DataRow row in data.Rows)
            {
                string accountType = EFTVariableParser.ParseEFTName(row["Payee Address 1 BO"].ToString().Trim());

                string transactionCode = this.GetTransactionCode(accountType);
                int receiverAccountType = (int)(accountType.ToUpper().StartsWith("C") ? EFTN.Utility.AccountType.Current : EFTN.Utility.AccountType.Savings);
                string dfiAccNo = EFTVariableParser.ParseEFTAccountNumber(row["Beneficiary Account"].ToString().Trim());
                string accNo = EFTVariableParser.ParseEFTAccountNumber(row["Debit Account No."].ToString().Trim());

                string receivingBankRoutingNo = EFTVariableParser.ParseEFTRoutingNumber(row["Payee Address 2 BO"].ToString().Trim());
                decimal amount = Math.Truncate(Decimal.Parse(EFTVariableParser.ParseEFTAmount(row["Invoice Amount"].ToString().Trim())) * 100) / 100;
                string idNumber = EFTVariableParser.ParseEFTReceiverID(row["Customer Ref."].ToString().Trim());

                if (typeOfPayment == 2)
                {
                    if (idNumber.Length > EFTLength.ReceiverIDLengthForCIE)
                    {
                        idNumber = idNumber.Substring(0, EFTLength.ReceiverIDLength);
                    }
                }
                else
                {
                    if (idNumber.Length > EFTLength.ReceiverIDLength)
                    {
                        idNumber = idNumber.Substring(0, EFTLength.ReceiverIDLength);
                    }
                }

                if (idNumber.Equals(string.Empty))
                {
                    idNumber = "BEFTN";
                }
                string receiverName = EFTVariableParser.ParseEFTName(row["Payee Name"].ToString().Trim());

                if (typeOfPayment == 2)
                {
                    if (receiverName.Length > EFTLength.ReceiverNameLengthForCIE)
                    {
                        receiverName = receiverName.Substring(0, EFTLength.ReceiverNameLengthForCIE);
                    }
                }
                else
                {
                    if (receiverName.Length > EFTLength.ReceiverNameLength)
                    {
                        receiverName = receiverName.Substring(0, EFTLength.ReceiverNameLength);
                    }
                }

                int statusId = 1;
                string paymentInfo = EFTVariableParser.ParseEFTPaymentInfo(row["Payee Details 1 BO"].ToString().Trim());

                if (paymentInfo.Length > 80)
                {
                    paymentInfo = paymentInfo.Substring(0, 80);
                }

                if (paymentInfo.Equals(string.Empty))
                {
                    paymentInfo = "BEFTN TRANSACTION";
                }
                /*************************************************************/
                string customerID = Regex.Replace(row["Customer ID"].ToString().Trim().Replace(",", "").Replace("%", "").Replace("'", ""), @"\s{2,}", " ");
                string batchRef = Regex.Replace(row["Batch Ref."].ToString().Trim().Replace(",", "").Replace("%", "").Replace("'", ""), @"\s{2,}", " ");
                string paymentRef = Regex.Replace(row["Payment Ref."].ToString().Trim().Replace(",", "").Replace("%", "").Replace("'", ""), @"\s{2,}", " ");
                string currnecyCode = accNo.Substring(0, 2).ToString();


                if (currnecyCode.Equals("00"))
                {
                    DataTable dtDuplicateData = edrDB.GetSentEDRForSCB_CSV(customerID
                                                        , batchRef, paymentRef);
                    /*************************************************************/
                    if (dtDuplicateData.Rows.Count == 0)
                    {
                        edrDB.InsertTransactionSentForCSVSCB(
                            transactionID,
                            transactionCode,
                            receiverAccountType,
                            this.typeOfPayment,
                            dfiAccNo,
                            accNo,
                            receivingBankRoutingNo,
                            amount,
                            idNumber, receiverName,
                            statusId,
                            createdBy,
                            paymentInfo,
                            this.departmentID,
                            customerID,
                            batchRef,
                            paymentRef,
                            this.connectionString
                            );

                        sentEDRCount++;
                    }
                }
                else
                {
                    /*************************************************************/

                    edrDB.InsertTransactionSentForCSVSCBNonBDT(
                        transactionID,
                        transactionCode,
                        receiverAccountType,
                        this.typeOfPayment,
                        dfiAccNo,
                        accNo,
                        receivingBankRoutingNo,
                        amount,
                        idNumber, receiverName,
                        statusId,
                        createdBy,
                        paymentInfo,
                        this.departmentID,
                        customerID,
                        batchRef,
                        paymentRef,
                        currnecyCode,
                        this.connectionString
                        );

                    //sentEDRCount++;
                }
            }
            EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
            sentEDRDB.UpdateEDRSentto1000(transactionID);
            //data.Clear();
            //data = edrDB.GetSentEDRByTransactionID(transactionID);
            return edrDB.GetSentEDRByTransactionIDForBulk(transactionID);
        }

        #region BlockedToReverseBackTo_Old_STS_System

        //private Guid InsertIntoBatch(string strCompanyID, string strCompanyName, string strReasonForPayment)
        //{
        //    int envelopeID = -1;
        //    string serviceClassCode = this.GetServiceClassCode();
        //    string secc = this.GetStandardEntryClassCode();
        //    DateTime effectiveEntryDate = DateTime.Now;

        //    //new add for distributed batch///////////////////
        //    //////////////////////////////////////////////////

        //    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
        //    Guid transactionID = db.InsertBatchSentWithSettlementDay(
        //            envelopeID,
        //            serviceClassCode,
        //            secc,
        //            this.typeOfPayment,
        //            effectiveEntryDate,
        //            //this.companyId,
        //            //this.companyName,
        //            //this.reasonForPayment,
        //            strCompanyID,
        //            strCompanyName,
        //            strReasonForPayment,
        //            createdBy, this.batchStatus, this.eFTTransactionType
        //            , this.departmentID, this.dataEntryType, this.cBSSettlementDay);
        //    return transactionID;

        //}
        #endregion

        private Guid InsertIntoBatch(string currency)
        {
            int envelopeID = -1;
            string serviceClassCode = this.GetServiceClassCode();
            string secc = this.GetStandardEntryClassCode();
            DateTime effectiveEntryDate = DateTime.Now;

            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            Guid transactionID = db.InsertBatchSentWithSettlementDay(
                    envelopeID,
                    serviceClassCode,
                    secc,
                    this.typeOfPayment,
                    effectiveEntryDate,
                    this.companyId,
                    this.companyName,
                    this.reasonForPayment,
                    createdBy, this.batchStatus, this.eFTTransactionType
                    , this.departmentID, this.dataEntryType, this.cBSSettlementDay
                    , currency);
            return transactionID;

        }

        private string GetTransactionCode(string accountType)
        {
            if (this.eFTTransactionType.Equals(TransactionCodes.EFTTransactionTypeCredit))
            {
                return accountType.ToUpper().StartsWith("C") ? TransactionCodes.CreditCurrentAcc.ToString() : TransactionCodes.CreditSavingsAcc.ToString();

            }
            else
            {
                return accountType.ToUpper().StartsWith("C") ? TransactionCodes.DebitCurrentAcc.ToString() : TransactionCodes.DebitSavingsAcc.ToString();
            }

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


        public DataTable ValidateCSVEFTForSCB(string CSVFilePath, int paymentType, int createBy)
        {
            string extension = System.IO.Path.GetExtension(CSVFilePath);
            DataTable validatedData = new DataTable();
            DataTable dtCSVforSCB = new DataTable();
            DataTable errorData = new DataTable();
            DataTable remainingData = new DataTable();
            EFTCSVReader eftCSVReader = new EFTCSVReader();
            string sqlConnection = ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
            EFTN.component.EFTCsvScbBulkDB csvDB = new component.EFTCsvScbBulkDB();
            int transactionsMovedToMainTable = 0;
            /***    Commented out for implementing the new csv file format    ***/
            //DataTable dtCSVforSCB = eftCSVReader.ReadCSV(CSVFilePath);

            /** New cheking added for remaining transactions as we got agreed with business team this way **/
            remainingData = csvDB.CheckIfRemainingTransactionsAvailableForThisUser(sqlConnection, createdBy);
            if (remainingData.Rows.Count.Equals(0))
            {
                try
                {
                    /***    Implementing the new csv file format dated 04-01-2018    ***/
                    dtCSVforSCB = eftCSVReader.ReadCSVNewFormat(CSVFilePath);
                }
                catch
                {
                    dtCSVforSCB.Columns.Add("999");
                    return dtCSVforSCB;
                }
            }
            else if (remainingData.Rows.Count > 0)
            {
                dtCSVforSCB.Columns.Add("121");
                return dtCSVforSCB;
            }



            if (dtCSVforSCB.Rows.Count > 0 && dtCSVforSCB.Rows.Count > transactionsMovedToMainTable)
            {
                // Commented out due to Flora and SCB Business team meeting on 05-03-2018
                csvDB.DeleteCsvTempData(sqlConnection, createdBy, false);

                List<string> currencies = csvDB.GetCurrenciesDynamicallyFromUploadedFile(dtCSVforSCB, "Payment Currency");
                foreach (var currency in currencies)
                {
                    DataTable filetedData = csvDB.GetFilteredDataFromBulkDataByCurrency(dtCSVforSCB, "[Payment Currency]", currency);
                    try
                    {
                        //Write to CSV Temp Table
                        csvDB.UploadBulkCsvScbDataToTempTable(sqlConnection, filetedData);  //Changed dtCSVforSCB with filetedData 
                    }
                    catch
                    {
                        errorData.Columns.Add("111");
                        validatedData = errorData;
                        return validatedData;
                    }

                    try
                    {

                        /** Added InsertBatch function to add currency wise **/
                        this.transactionID = InsertIntoBatch(currency);
                        //Update other column value 
                        /** Commented out and added below line foor update transactions with transaction id dynamically currency wise**/
                        //csvDB.UpdateTempSentEDRColumnForScbCSV(this.transactionID, this.createdBy, this.eFTTransactionType, this.typeOfPayment, this.departmentID, sqlConnection);
                        csvDB.UpdateTempSentEDRColumnForScbCSV(this.transactionID, this.createdBy, this.eFTTransactionType, this.typeOfPayment, this.departmentID, sqlConnection, currency);

                    }
                    catch
                    {
                        errorData.Columns.Add("222");
                        validatedData = errorData;
                        return validatedData;
                    }

                    //Validate data and set the error reason
                    csvDB.ValidateTempSentEDRColumnForScbCsv(sqlConnection, OriginalBankCode.SCB);

                    //Transfer to Main Table
                    csvDB.TransferTransactionSent_FromBulkEntryCsvSCB(sqlConnection);
                    sentEDRDB.UpdateEDRSentto1000(transactionID);
                    transactionsMovedToMainTable = transactionsMovedToMainTable + filetedData.Rows.Count;
                }
                if (dtCSVforSCB.Rows.Count.Equals(transactionsMovedToMainTable))
                {
                    //Transfer Invalid data to CSV Rejected Table
                    csvDB.TransferTransactionSent_FromBulkEntryCsvScbRejectedData(sqlConnection);

                    /***    Commented out for retriving data to the screen new way  ***/
                    //validatedData = sentEDRDB.GetSentEDRByTransactionIDForBulk(transactionID);
                    /*** Added new to show all the transactions after successful upload ***/
                    validatedData = sentEDRDB.GetTransactionsBasedOnCsvUpload(sqlConnection, createdBy, dataEntryType);

                    //Truncate_Table();     
                    /*** SCB HQ requested to change truncate operation into delete action, So the below function is commented out   ***/
                    //csvDB.TruncateTempSentEDRCsvTable(sqlConnection);
                    /*** Delete action should be executed while user click on save button ***/
                    //csvDB.DeleteCsvTempData(sqlConnection); 
                    if (validatedData.Rows.Count.Equals(0))
                    {
                        errorData.Columns.Add("400");
                        validatedData = errorData;
                        return validatedData;
                    }
                }                
            }
            else
            {
                validatedData = dtCSVforSCB;
            }
            return validatedData;
        }

       

        #region LatestMethod_ValidateCSVEFTForSCB
        //     public void ValidateCSVEFTForSCB(string CSVFilePath, int paymentType)
        //     {
        //         string extension = System.IO.Path.GetExtension(CSVFilePath);

        //         EFTCSVReader eftCSVReader = new EFTCSVReader();
        //         DataTable dtCSVforSCB = eftCSVReader.ReadCSV(CSVFilePath);

        //         //create new component class copy ExcelEFTBulkDB and Paste then Change
        //         EFTN.component.EFTCsvScbBulkDB csvDB = new component.EFTCsvScbBulkDB();
        //string myConnection = ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        //         //Read Exel Data and Write to Temp Table
        //         csvDB.UploadBulkCsvScbDataToTempTable(myConnection, dtCSVforSCB);


        //         #region STS Update by Junayed 
        //         /******* Kept now According to the Previous Requirement, but 
        //         if they want to enable the STS enhancement again then following four methods should be commented out ********/

        //         /********** Update other column value***********/
        //         csvDB.UpdateTempSentEDRColumnForScbCSV(this.transactionID, this.createdBy, this.eFTTransactionType, this.typeOfPayment, this.departmentID, myConnection);

        //         /****   Validate data and set the error reason  ****/
        //         csvDB.ValidateTempSentEDRColumnForScbCsv(myConnection, OriginalBankCode.SCB);
        //         /******** Batch Creation for STS update **********/
        //         DistributeBatch(myConnection); 

        //         /******** Transfer to Main Table _ SentEDR ********/
        //         csvDB.TransferTransactionSent_FromBulkEntryCsvSCB(myConnection);
        //         #endregion


        //         /***************************************************************************/
        //         //Transfer Invalid data to CSV Rejected Table      
        //         csvDB.TransferTransactionSent_FromBulkEntryCsvScbRejectedData(myConnection);
        //         /***************************************************************************/
        //         ////Transfer NON BDT to NON BDT Table
        //         //csvDB.TransferTransactionSent_FromBulkEntryNonBDT(myConnection);
        //         /***************************************************************************/
        //         //Truncate_Table();
        //         //csvDB.TruncateTempSentEDRCsvTable(myConnection);
        //         /***************************************************************************/

        //         //EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
        //         //sentEDRDB.UpdateEDRSentto1000ForCSVSCB();
        //         //data.Clear();
        //         //data = edrDB.GetSentEDRByTransactionID(transactionID);
        //         //return sentEDRDB.GetSentEDRByTransactionIDForBulk(transactionID);

        //         //return errorString.ToString();
        //     }
        #endregion


        #region Blocked_DistributeBatchFunction_ToReverseBackToTheOldChanges
        //public void DistributeBatch(string conStr)
        //{
        //    EFTN.component.EFTCsvScbBulkDB csvDB = new EFTN.component.EFTCsvScbBulkDB();
        //    /***************************************************************************/
        //    //Get data by distinct accountno and reason
        //    DataTable dtDistinctData = csvDB.GetDistinctScbCsvBatchInfo(conStr);
        //    /***************************************************************************/
        //    //Create Batch and assign TransactionID in the sentEDR Table
        //    string strCompanyID = string.Empty;

        //    for (int i = 0; i < dtDistinctData.Rows.Count; i++)
        //    {
        //        string AccountNo = dtDistinctData.Rows[i]["AccountNo"].ToString();
        //        string CompanyEntryDesc = dtDistinctData.Rows[i]["CompanyEntryDesc"].ToString();
        //        string CustomerName = dtDistinctData.Rows[i]["CustomerName"].ToString().Trim();
        //        strCompanyID = AccountNo;

        //        /***************************************************************************/
        //        if (CustomerName.Length > EFTLength.CompanyNameLength)
        //        {
        //            CustomerName = CustomerName.Substring(0, EFTLength.CompanyNameLength);
        //        }
        //        //Create Batch and Get TransactionID for CSV
        //        Guid TransactionID = InsertIntoBatch(strCompanyID, CustomerName, CompanyEntryDesc);

        //        //Assign TransactionID in csv SentEDR table
        //        csvDB.UpdateTransactionIDForScbCsvTemp(conStr, TransactionID, AccountNo, CompanyEntryDesc);
        //    }

        //    //Get TransactionID List
        //    //DataTable dtTransactionIDList = csvDB.GetCSVTransactionIDList(conStr);
        //}
        #endregion

        public void InsertCSVDataForSCB(
                                            Guid TransactionID,
                                            string DFIAccountNo,
                                            string AccountNo,
                                            string ReceivingBankRoutingNo,
                                            string Amount,
                                            string IdNumber,
                                            string ReceiverName,
                                            string PaymentInfo,
                                            int CreatedBy,
                                            string CustomerID,
                                            string BatchReference,
                                            string PaymentReference,
                                            string transactionCode,
                                            int receiverAccountType,
                                            int statusId
                                        )
        {

            SCBCSVDataObject scbCSVDataObj = new SCBCSVDataObject();
            scbCSVDataObj.InitializeObject();

            int totalError = 0;

            if (typeOfPayment == 2)
            {
                if (IdNumber.Length > 22)
                {
                    IdNumber = IdNumber.Substring(0, 22);
                }
            }
            else
            {
                if (IdNumber.Length > 15)
                {
                    IdNumber = IdNumber.Substring(0, 15);
                }
            }


            if (typeOfPayment == 2)
            {
                if (ReceiverName.Length > EFTLength.ReceiverNameLengthForCIE)
                {
                    ReceiverName = ReceiverName.Substring(0, EFTLength.ReceiverNameLengthForCIE);
                }
            }
            else
            {
                if (ReceiverName.Length > EFTLength.ReceiverNameLength)
                {
                    ReceiverName = ReceiverName.Substring(0, EFTLength.ReceiverNameLength);
                }
            }

            //int approvedBy = 1;
            if (PaymentInfo.Length > 80)
            {
                PaymentInfo = PaymentInfo.Substring(0, 80);
            }

            PaymentInfo = EFTVariableParser.ParseEFTPaymentInfo(PaymentInfo);
            AccountNo = EFTVariableParser.ParseEFTAccountNumber(AccountNo);
            ReceivingBankRoutingNo = EFTVariableParser.ParseEFTRoutingNumber(ReceivingBankRoutingNo);
            DFIAccountNo = EFTVariableParser.ParseEFTAccountNumber(DFIAccountNo);
            //accountType = EFTVariableParser.ParseEFTAccountNumber(accountType);
            Amount = EFTVariableParser.ParseEFTAmount(Amount);
            IdNumber = EFTVariableParser.ParseEFTReceiverID(IdNumber);
            ReceiverName = EFTVariableParser.ParseEFTName(ReceiverName);

            //string currnecyCode = AccountNo.Substring(0, 2).ToString();

            if (DFIAccountNo.Length > EFTLength.BankAccNoLength)
            {
                scbCSVDataObj.FlagDFIAccountNoLength = 2;
                totalError++;
            }

            if (DFIAccountNo.Equals(string.Empty))
            {
                scbCSVDataObj.FlagDFIAccountNoLength = 2;
                totalError++;
            }

            if (!scbCSVDataObj.IsEFTAccountNumber(DFIAccountNo.Replace(" ", "")))
            {
                scbCSVDataObj.FlagDFIAccountNoCharacter = 1;
                totalError++;
            }

            if (AccountNo.Length > EFTLength.SenderAccNumberLength)
            {
                scbCSVDataObj.FlagAccountNoLength = 2;
                totalError++;
            }

            if (AccountNo.Equals(string.Empty))
            {
                scbCSVDataObj.FlagAccountNoLength = 2;
                totalError++;
            }

            if (!scbCSVDataObj.IsEFTAccountNumber(AccountNo.Replace(" ", "")))
            {
                scbCSVDataObj.FlagAccountNoCharacter = 1;
                totalError++;
            }

            if (ReceivingBankRoutingNo.Length > EFTLength.ReceivingBankRoutingLength)
            {
                scbCSVDataObj.FlagReceivingBankRTLength = 2;
                totalError++;
            }

            if (!scbCSVDataObj.IsWholeNumber(ReceivingBankRoutingNo) || ReceivingBankRoutingNo.Equals(string.Empty)
                || ReceivingBankRoutingNo.Length != 9 || ReceivingBankRoutingNo.Equals("000000000"))
            {
                scbCSVDataObj.FlagReceivingBankRTCharacter = 1;
                totalError++;
            }

            if (!scbCSVDataObj.IsPositiveNumber(Amount) || Amount.Equals(string.Empty))
            {
                scbCSVDataObj.FlagAmountCharacter = 1;
                totalError++;
            }

            if (!scbCSVDataObj.IsAlphaNumericForEFT(IdNumber))
            {
                scbCSVDataObj.FlagIdNumberCharacter = 1;
                totalError++;
            }

            if (ReceiverName.Equals(string.Empty))
            {
                scbCSVDataObj.FlagReceiverNameLength = 2;
                totalError++;
            }

            if (!scbCSVDataObj.IsAlphaNumericForEFT(ReceiverName))
            {
                scbCSVDataObj.FlagReceiverNameCharacter = 1;
                totalError++;
            }

            if (ReceivingBankRoutingNo.Substring(0, 3) == "215")
            {
                scbCSVDataObj.FlagReceivingBankRTOnUs = 4;
                totalError++;
            }


            decimal eftAmount = ParseData.StringToDecimal(Amount);
            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();
            int NoOfError = scbCSVDataObj.FlagAccountNoCharacter
                            + scbCSVDataObj.FlagAccountNoLength
                            + scbCSVDataObj.FlagAmountCharacter
                            + scbCSVDataObj.FlagAmountLength
                            + scbCSVDataObj.FlagDFIAccountNoCharacter
                            + scbCSVDataObj.FlagDFIAccountNoLength
                            + scbCSVDataObj.FlagIdNumberCharacter
                            + scbCSVDataObj.FlagIdNumberLength
                            + scbCSVDataObj.FlagReceiverNameCharacter
                            + scbCSVDataObj.FlagReceiverNameLength
                            + scbCSVDataObj.FlagReceivingBankRTCharacter
                            + scbCSVDataObj.FlagReceivingBankRTLength
                            + scbCSVDataObj.FlagReceivingBankRTOnUs;

            int ErrorRecievingBankRouting = (scbCSVDataObj.FlagReceivingBankRTLength + scbCSVDataObj.FlagReceivingBankRTCharacter + scbCSVDataObj.FlagReceivingBankRTOnUs);
            int ErrorAmount = (scbCSVDataObj.FlagAmountLength + scbCSVDataObj.FlagAmountCharacter);
            int ErrorIdNumber = (scbCSVDataObj.FlagIdNumberLength + scbCSVDataObj.FlagIdNumberCharacter);
            int ErrorReceiverName = (scbCSVDataObj.FlagReceiverNameLength + scbCSVDataObj.FlagReceiverNameCharacter);
            int ErrorDFIAccountNo = (scbCSVDataObj.FlagDFIAccountNoLength + scbCSVDataObj.FlagDFIAccountNoCharacter);
            int ErrorAccountNo = (scbCSVDataObj.FlagAccountNoLength + scbCSVDataObj.FlagAccountNoCharacter);


            if (NoOfError == 0)
            {
                //if (currnecyCode.Equals("00"))
                //{
                DataTable dtDuplicateData = edrDB.GetSentEDRForSCB_CSV(CustomerID
                                                    , BatchReference, PaymentReference);
                /*************************************************************/
                if (dtDuplicateData.Rows.Count == 0)
                {
                    edrDB.InsertTransactionSentForCSVSCB(
                        transactionID,
                        transactionCode,
                        receiverAccountType,
                        this.typeOfPayment,
                        DFIAccountNo,
                        AccountNo,
                        ReceivingBankRoutingNo,
                        eftAmount,
                        IdNumber,
                        ReceiverName,
                        statusId,
                        createdBy,
                        PaymentInfo,
                        this.departmentID,
                        CustomerID,
                        BatchReference,
                        PaymentReference,
                        this.connectionString
                        );

                    sentEDRCount++;
                }
                //}
                //else
                //{
                //    /*************************************************************/

                //    edrDB.InsertTransactionSentForCSVSCBNonBDT(
                //        transactionID,
                //        transactionCode,
                //        receiverAccountType,
                //        this.typeOfPayment,
                //        DFIAccountNo,
                //        AccountNo,
                //        ReceivingBankRoutingNo,
                //        eftAmount,
                //        IdNumber,
                //        ReceiverName,
                //        statusId,
                //        createdBy,
                //        PaymentInfo,
                //        this.departmentID,
                //        CustomerID,
                //        BatchReference,
                //        PaymentReference,
                //        currnecyCode,
                //        this.connectionString
                //        );

                //    //sentEDRCount++;
                //}
            }
            else
            {
                edrDB.InsertTransactionSentforRejectedCSVTransactionsOfSCB(transactionID,
                                                                            transactionCode,
                                                                            DFIAccountNo,
                                                                            AccountNo,
                                                                            ReceivingBankRoutingNo,
                                                                            ParseData.StringToDouble(Amount),
                                                                            IdNumber,
                                                                            ReceiverName,
                                                                            PaymentInfo,
                                                                            createdBy,
                                                                            CustomerID,
                                                                            BatchReference,
                                                                            PaymentReference,
                                                                            ErrorRecievingBankRouting,
                                                                            ErrorAmount,
                                                                            ErrorIdNumber,
                                                                            ErrorReceiverName,
                                                                            ErrorDFIAccountNo,
                                                                            ErrorAccountNo
                                                                        );



                //INSERT REJECTED DATA
            }

            //return errorString.ToString();
        }
    }
}