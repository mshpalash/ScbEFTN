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
    public class ExcelForStandingOrder
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

        private Guid standingOrderBatchID;

        public Guid StandingOrderBatchID
        {
            get { return standingOrderBatchID; }
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

        private DateTime beginDate;

        public DateTime BeginDate
        {
            get { return beginDate; }
        }

        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
        }

        private int startDayOfMonth;

        public int StartDayOfMonth
        {
            get { return startDayOfMonth; }
        }

        private int endDayOfMonth;

        public int EndDayOfMonth
        {
            get { return endDayOfMonth; }
        }

        private int transactoinFrequency;

        public int TransactoinFrequency
        {
            get { return transactoinFrequency; }
        }

        private int sentEDRCount = 0;
        #endregion

        #region Constructor
        public ExcelForStandingOrder(
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
            DateTime beginDate,
            DateTime endDate,
            int startDayOfMonth,
            int endDayOfMonth,
            int transactoinFrequency
            )
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
            this.beginDate = beginDate;
            this.endDate = endDate;
            this.startDayOfMonth = startDayOfMonth;
            this.endDayOfMonth = endDayOfMonth;
            this.transactoinFrequency = transactoinFrequency;
        }

        #endregion

        public DataTable EntryData(string originBank)
        {
            this.standingOrderBatchID = this.InsertIntoBatch(originBank);

            DataTable dt = new DataTable();
            try
            {
                dt = InsertEDR(standingOrderBatchID);

                if (dt.Rows.Count > 0)
                {
                    InsertStandingOrderDate(this.standingOrderBatchID);
                }
                //this.batchNumber = GetBatchNumberByTransactionID();
            }
            catch
            {
                if (sentEDRCount > 0)
                {
                    //EFTN.component.SentBatchDB.DeleteSentBatch(transactionID);
                    //EFTN.component.SentBatchDB.DeleteSentEDR(transactionID);
                }
            }
            finally
            {
                System.IO.File.Delete(this.excelFilePath);
            }
            return dt;
        }

        private void InsertStandingOrderDate(Guid StandingOrderBatchID)
        {
            EFTN.component.StandingOrderDB standingOrderDB = new EFTN.component.StandingOrderDB();
            standingOrderDB.UpdateStandingOrderDate(StandingOrderBatchID);
        }

        //private string GetBatchNumberByTransactionID()
        //{
        //    EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
        //    DataTable dtBatchNumber = sentBatchDB.GetBatchNumberByTransactionID(this.standingOrderBatchID);
        //    string batchnumber = string.Empty;
        //    if (dtBatchNumber.Rows.Count > 0)
        //    {
        //        batchnumber = dtBatchNumber.Rows[0]["BatchNumber"].ToString();
        //    }
        //    return batchnumber;
        //}

        private DataTable InsertEDR(Guid standingOrderBatchID)
        {
            EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();
            DataTable data = excelDB.GetData(this.excelFilePath);

            EFTN.component.StandingOrderDB edrDB = new EFTN.component.StandingOrderDB();
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
                        edrDB.InsertStandingOrderEDR(standingOrderBatchID,
                            transactionCode,
                            receiverAccountType,
                            this.typeOfPayment,
                            dfiAccNo,
                            accNo,
                            receivingBankRoutingNo,
                            amount,
                            idNumber, 
                            receiverName,
                            statusId,
                            createdBy,
                            paymentInfo, 
                            this.departmentID);

                        sentEDRCount++;
                    }
            }

            return edrDB.GetStandingOrderByStandingOrderBatchID(standingOrderBatchID); ;
        }

        private Guid InsertIntoBatch(string originBank)
        {
            string serviceClassCode = this.GetServiceClassCode();
            string secc = this.GetStandardEntryClassCode();
            DateTime effectiveEntryDate = DateTime.Now;

            EFTN.component.StandingOrderDB db = new EFTN.component.StandingOrderDB();
            Guid standingOrderBatchID = db.InsertStandingOrderBATCH(
                          serviceClassCode
                        , secc
                        , this.typeOfPayment
                        , effectiveEntryDate
                        , this.companyId
                        , this.companyName
                        , this.reasonForPayment
                        , createdBy
                        , this.batchStatus
                        , this.eFTTransactionType
                        , this.departmentID
                        , this.dataEntryType
                        , this.beginDate
                        , this.endDate
                        , this.startDayOfMonth
                        , this.endDayOfMonth
                        , this.transactoinFrequency);
            return standingOrderBatchID;

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
