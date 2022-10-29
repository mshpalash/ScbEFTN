using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EFTN.Utility;
using System.Collections.Generic;
using EFTN.component;

namespace EFTN.BLL
{
    public class TextDataEntryObject
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

        private string connectionString;

        public string ConnectionString
        {
            get { return connectionString; }
        }

        private int sentEDRCount = 0;
        #endregion

        #region Constructor
        public TextDataEntryObject(
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
            string connectionString
            )
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
            this.connectionString = connectionString;
        }

        #endregion

        public DataTable EntryData()
        {
            /** Commented out below function for uploading multiple currencies excel data   **/
            //this.transactionID = this.InsertIntoBatch(currency);

            DataTable dt = new DataTable();
            try
            {
                //dt = InsertEDR(transactionID);
                dt = InsertEDR();
                /** Commented out for uploading multiple currencies **/
                // this.batchNumber = GetBatchNumberByTransactionID();
            }
            catch
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

        private DataTable InsertEDR()
        {
            //EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();
            string delim = ConfigurationManager.AppSettings["TextDelim"];
            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();
            EFTCsvScbBulkDB bulkDB = new EFTCsvScbBulkDB();
            DataTable data = DelimitedTextReader.ReadFile(this.textFilePath, delim);
            List<string> currencies = bulkDB.GetCurrenciesDynamicallyFromUploadedFile(data, "Currency");

            foreach (var currency in currencies)
            {
                DataTable filteredData = new DataTable();
                filteredData = bulkDB.GetFilteredDataFromBulkDataByCurrency(data, "Currency", currency);
                if (filteredData.Rows.Count > 0)
                {
                    this.transactionID = this.InsertIntoBatch(currency);
                    foreach (DataRow row in filteredData.Rows)
                    {
                        string accountType = row["AccType"].ToString();
                        string transactionCode = this.GetTransactionCode(accountType);
                        int receiverAccountType = (int)(accountType.ToUpper().StartsWith("C") ? EFTN.Utility.AccountType.Current : EFTN.Utility.AccountType.Savings);
                        string dfiAccNo = EFTVariableParser.ParseEFTAccountNumber(row["BankAccNo"].ToString().Trim());
                        string accNo = EFTVariableParser.ParseEFTAccountNumber(row["SenderAccNumber"].ToString().Trim().Replace(" ", ""));

                        string receivingBankRoutingNo = EFTVariableParser.ParseEFTRoutingNumber(row["ReceivingBankRouting"].ToString().Trim());
                        decimal amount = Math.Truncate(Decimal.Parse(EFTVariableParser.ParseEFTAmount(row["Amount"].ToString().Trim())) * 100) / 100;
                        string idNumber = EFTVariableParser.ParseEFTReceiverID(row["ReceiverID"].ToString().Trim());
                        string receiverName = EFTVariableParser.ParseEFTName(row["ReceiverName"].ToString().Trim());
                        int statusId = 1;
                        //int approvedBy = 1;
                        string paymentInfo = EFTVariableParser.ParseEFTPaymentInfo(row["Reason"].ToString());

                        edrDB.InsertTransactionSent(
                            this.transactionID,
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
                            paymentInfo, this.departmentID, this.connectionString, 0 ); // 0 for Remittance as this function is not useful

                        sentEDRCount++;
                    }
                    EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
                    sentEDRDB.UpdateEDRSentto1000(this.transactionID);
                }
            }
            /** Commented out for getting the list based on user and data entry type **/
            //return edrDB.GetSentEDRByTransactionIDForBulk(transactionID); 
            return edrDB.GetTransactionsBasedOnCsvUpload(this.connectionString, createdBy, this.dataEntryType);
        }

        private Guid InsertIntoBatch(string currency)
        {
            int envelopeID = -1;
            string serviceClassCode = this.GetServiceClassCode();
            string secc = this.GetStandardEntryClassCode();
            DateTime effectiveEntryDate = DateTime.Now;

            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            Guid transactionID = db.InsertBatchSent(
                    envelopeID,
                    serviceClassCode,
                    secc,
                    this.typeOfPayment,
                    effectiveEntryDate,
                    this.companyId,
                    this.companyName,
                    this.reasonForPayment,
                    createdBy, this.batchStatus, this.eFTTransactionType
                    , this.departmentID
                    , this.dataEntryType
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
    }
}
