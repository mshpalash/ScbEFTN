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

namespace EFTN.BLL
{
    public class ExcelDataEntryObject
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

        private string excelFilePath;

        public string ExcelFilePath
        {
            get { return excelFilePath; }
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

        private string ctx_InvoiceNumber;

        public string CTX_InvoiceNumber
        {
            get { return ctx_InvoiceNumber; }
        }

        private string ctx_InvoiceDate;

        public string CTX_InvoiceDate
        {
            get { return ctx_InvoiceDate; }
        }

        private double ctx_InvoiceGrossAmount;

        public double CTX_InvoiceGrossAmount
        {
            get { return ctx_InvoiceGrossAmount; }
        }

        private double ctx_InvoiceAmountPaid;

        public double CTX_InvoiceAmountPaid
        {
            get { return ctx_InvoiceAmountPaid; }
        }

        private string ctx_PurchaseOrder;

        public string CTX_PurchaseOrder
        {
            get { return ctx_PurchaseOrder; }
        }

        private double ctx_AdjustmentAmount;

        public double CTX_AdjustmentAmount
        {
            get { return ctx_AdjustmentAmount; }
        }

        private string ctx_AdjustmentCode;

        public string CTX_AdjustmentCode
        {
            get { return ctx_AdjustmentCode; }
        }

        private string ctx_AdjustmentDescription;

        public string CTX_AdjustmentDescription
        {
            get { return ctx_AdjustmentDescription; }
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
        public ExcelDataEntryObject(
            string companyId,
            int typeOfPayment,
            string companyName,
            string reasonForPayment,
            string excelFilePath,
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
            this.excelFilePath = excelFilePath;
            this.createdBy = createdBy;
            this.batchStatus = batchStatus;
            this.eFTTransactionType = eFTTransactionType;
            this.departmentID = departmentID;
            this.dataEntryType = dataEntryType;
            this.cBSSettlementDay = cbsSettlementDay;
            this.connectionString = connectionString;
        }

        #endregion

        #region ConstructorCTX
        public ExcelDataEntryObject(
            string companyId,
            int typeOfPayment,
            string companyName,
            string reasonForPayment,
            string excelFilePath,
            int createdBy,
            int batchStatus,
            string eFTTransactionType,
            int departmentID,
            string dataEntryType,
            string ctx_InvoiceNumber,
            string ctx_InvoiceDate,
            double ctx_InvoiceGrossAmount,
            double ctx_InvoiceAmountPaid,
            string ctx_PurchaseOrder,
            double ctx_AdjustmentAmount,
            string ctx_AdjustmentCode,
            string ctx_AdjustmentDescription)
        {
            this.companyId = companyId;
            this.typeOfPayment = typeOfPayment;
            this.companyName = companyName;
            this.reasonForPayment = reasonForPayment;
            this.excelFilePath = excelFilePath;
            this.createdBy = createdBy;
            this.batchStatus = batchStatus;
            this.eFTTransactionType = eFTTransactionType;
            this.departmentID = departmentID;
            this.dataEntryType = dataEntryType;
            this.ctx_InvoiceNumber = ctx_InvoiceNumber;
            this.ctx_InvoiceDate = ctx_InvoiceDate;
            this.ctx_InvoiceGrossAmount = ctx_InvoiceGrossAmount;
            this.ctx_InvoiceAmountPaid = ctx_InvoiceAmountPaid;
            this.ctx_PurchaseOrder = ctx_PurchaseOrder;
            this.ctx_AdjustmentAmount = ctx_AdjustmentAmount;
            this.ctx_AdjustmentCode = ctx_AdjustmentCode;
            this.ctx_AdjustmentDescription = ctx_AdjustmentDescription;
        }

        #endregion

        public DataTable EntryData(string originBank, int CBSAccountWiseHit)
        {
            this.transactionID = this.InsertIntoBatch(originBank, CBSAccountWiseHit);

            DataTable dt = new DataTable();
            try
            {
                dt = InsertEDR(transactionID);

                this.batchNumber = GetBatchNumberByTransactionID();
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
                System.IO.File.Delete(this.excelFilePath);
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

        private DataTable InsertEDR(Guid transactionID)
        {
            //Create Dynamically Temp Table with login ID

            //Read Exel Data

            //Write to Temp Table

            //Transfer to Main Table

            //Update other column value

            //Drop Temp Table



            EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();
            DataTable data = excelDB.GetData(this.excelFilePath);

            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();
            foreach (DataRow row in data.Rows)
            {
                string accountType = row["AccType"].ToString().Trim();

                string transactionCode = this.GetTransactionCode(accountType);
                int receiverAccountType = (int)(accountType.ToUpper().StartsWith("C") ? EFTN.Utility.AccountType.Current : EFTN.Utility.AccountType.Savings);
                string dfiAccNo = EFTVariableParser.ParseEFTAccountNumber(row["BankAccNo"].ToString());
                string accNo = EFTVariableParser.ParseEFTAccountNumber(row["SenderAccNumber"].ToString());               
                
                string receivingBankRoutingNo = EFTVariableParser.ParseEFTRoutingNumber(row["ReceivingBankRouting"].ToString());
                decimal amount = Math.Truncate(ParseData.StringToDecimal(EFTVariableParser.ParseEFTAmount(row["Amount"].ToString())) * 100) / 100;
                string idNumber = EFTVariableParser.ParseEFTReceiverID(row["ReceiverID"].ToString());
                string receiverName = EFTVariableParser.ParseEFTName(row["ReceiverName"].ToString().Trim());
                int statusId = 1;
                //int approvedBy = 1;
                string paymentInfo = EFTVariableParser.ParseEFTPaymentInfo(row["Reason"].ToString().Trim());
                if (this.typeOfPayment == 8)
                {
                    string invoiceNumber = EFTVariableParser.ParseEFTPaymentInfo(row["InvoiceNumber"].ToString().Trim());
                    string invoiceDate = string.Empty;
                    try
                    {
                        DateTime dtInvDate = DateTime.Parse(row["InvoiceDate"].ToString().Trim());
                        invoiceDate = DateTime.Parse(row["InvoiceDate"].ToString().Trim()).ToString("yyyyMMdd");

                    }
                    catch
                    {
                        invoiceDate = string.Empty;
                    }

                    decimal invoiceGrossAmount = Math.Truncate(ParseData.StringToDecimal(EFTVariableParser.ParseEFTAmount(row["InvoiceGrossAmount"].ToString().Trim())) * 100) / 100;
                    decimal invoiceAmountPaid = Math.Truncate(ParseData.StringToDecimal(EFTVariableParser.ParseEFTAmount(row["InvoiceAmountPaid"].ToString().Trim())) * 100) / 100;
                    string purchaseOrder = EFTVariableParser.ParseEFTName(row["PurchaseOrder"].ToString().Trim());
                    decimal adjustmentAmount = Math.Truncate(ParseData.StringToDecimal(EFTVariableParser.ParseEFTAmount(row["AdjustmentAmount"].ToString().Trim())) * 100) / 100;
                    string adjustmentCode = EFTVariableParser.ParseEFTName(row["AdjustmentCode"].ToString().Trim());
                    string adjustmentDescription = EFTVariableParser.ParseEFTName(row["AdjustmentDescription"].ToString().Trim());
                    
                    if (
                           paymentInfo.Equals(string.Empty) &&
                           accNo.Equals(string.Empty) &&
                           receivingBankRoutingNo.Equals("000000000") &&
                           dfiAccNo.Equals(string.Empty) &&
                           accountType.Equals(string.Empty) &&
                           amount.ToString().Equals("0") &&
                           idNumber.Equals(string.Empty) &&
                           receiverName.Equals(string.Empty) 
                           //&& invoiceNumber.Equals(string.Empty) &&
                           //invoiceDate.Equals(string.Empty) &&
                           //invoiceGrossAmount.ToString().Equals("0") &&
                           //invoiceAmountPaid.ToString().Equals("0") &&
                           //purchaseOrder.Equals(string.Empty) &&
                           //adjustmentAmount.ToString().Equals("0") &&
                           //adjustmentCode.Equals(string.Empty) &&
                           //adjustmentDescription.Equals(string.Empty)

                      )
                    {
                    }
                    else
                    {


                        edrDB.InsertTransactionSentforCTX(
                            transactionID, transactionCode, receiverAccountType, this.typeOfPayment,
                            dfiAccNo, accNo, receivingBankRoutingNo, amount, idNumber, receiverName,
                            statusId, createdBy, paymentInfo, invoiceNumber, invoiceDate,
                            invoiceGrossAmount, invoiceAmountPaid, purchaseOrder, adjustmentAmount,
                            adjustmentCode, adjustmentDescription, this.departmentID, this.connectionString,0);// 0 for Remittance as this function is not useful

                        sentEDRCount++;
                    }
                }
                else
                {
                    if (
                           paymentInfo.Equals(string.Empty) &&
                           accNo.Equals(string.Empty) &&
                           receivingBankRoutingNo.Equals("000000000") &&
                           dfiAccNo.Equals(string.Empty) &&
                           accountType.Equals(string.Empty) &&
                           amount.ToString().Equals("0") &&
                           idNumber.Equals(string.Empty) &&
                           receiverName.Equals(string.Empty)
                      )
                    {
                    }
                    else
                    {
                        edrDB.InsertTransactionSent(
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
                            paymentInfo, this.departmentID, this.connectionString,0); // 0 for Remittance as this function is not useful

                        sentEDRCount++;
                    }
                }
            }
            EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
            sentEDRDB.UpdateEDRSentto1000(transactionID);
            //data.Clear();
            //data = edrDB.GetSentEDRByTransactionID(transactionID);
            return edrDB.GetSentEDRByTransactionIDForBulk(transactionID); ;
        }

        private Guid InsertIntoBatch(string originBank, int AccountWiseCBSHit)
        {
            int envelopeID = -1;
            string serviceClassCode = this.GetServiceClassCode();
            string secc = this.GetStandardEntryClassCode();
            DateTime effectiveEntryDate = DateTime.Now;

            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            if (originBank.Equals(OriginalBankCode.SCB))
            {
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
                        , this.departmentID
                        , this.dataEntryType, this.cBSSettlementDay,
                        "");
                return transactionID;
            }
            else if (originBank.Equals(OriginalBankCode.NRB) 
                    || originBank.Equals(OriginalBankCode.UCBL))
            {
                Guid transactionID = db.InsertBatchSentWithAccountWise(
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
                        , this.dataEntryType, AccountWiseCBSHit);
                return transactionID;
            }
            else
            {
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
                        ,"");
                return transactionID;
            }

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
                    secc= "CCD";
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
