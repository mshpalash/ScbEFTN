using System.Data;
using System.Configuration;
using EFTN.CBLWebServiceReference;

namespace EFTN.BLL
{
    public class FlatFileClientCity
    {
        public string CreatFlatFileForTransactionSent(DataTable dt)
        {
            string result = string.Empty;

            double totalDebitAmount = 0;

            string Indicator = string.Empty;
            string TranType = string.Empty;
            string TranSubType = string.Empty;
            string Account = string.Empty;
            string CurCode = string.Empty;
            string ServiceOutlet = string.Empty;

            string Amount = string.Empty;
            string PartTranType = string.Empty;
            string TypeOfDemands = string.Empty;
            string valueDate = string.Empty;
            string FlowID = string.Empty;
            string DemandDate = string.Empty;
            string TranFlag = string.Empty;
            string TranEndInd = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                Indicator = row["Indicator"].ToString();
                TranType = row["TranType"].ToString();
                TranSubType = row["TranSubType"].ToString();
                Account = row["Account"].ToString();
                CurCode = row["CurCode"].ToString();
                ServiceOutlet = row["ServiceOutlet"].ToString();

                Amount = row["Amount"].ToString();
                PartTranType = row["PartTranType"].ToString();
                TypeOfDemands = row["TypeOfDemands"].ToString();
                valueDate = row["valueDate"].ToString();
                FlowID = row["FlowID"].ToString();
                DemandDate = row["DemandDate"].ToString();
                TranFlag = row["TranFlag"].ToString();
                TranEndInd = row["TranEndInd"].ToString();

                totalDebitAmount += EFTN.Utility.ParseData.StringToDouble(Amount.Trim());

                string line = "";
                line += (Indicator);
                line += (TranType);
                line += (TranSubType);
                line += (Account);
                line += (CurCode);
                line += (ServiceOutlet);
                line += (Amount);
                line += (PartTranType);
                line += (TypeOfDemands);
                line += (valueDate);
                line += (FlowID);
                line += (DemandDate);
                line += (TranFlag);
                line += (TranEndInd);

                if (result.Equals(string.Empty))
                {
                    result += line;
                }
                else
                {
                    result += "\n" + line;
                }
            }

            string RFCDebitAccount = ConfigurationManager.AppSettings["RFCDebitAccount"].PadRight(16, ' ');

            string lineDebit = "";
            lineDebit += (Indicator);
            lineDebit += (TranType);
            lineDebit += (TranSubType);
            lineDebit += (RFCDebitAccount);
            lineDebit += (CurCode);
            lineDebit += (ServiceOutlet);
            lineDebit += (totalDebitAmount.ToString("0.00").PadLeft(17, '0'));
            lineDebit += ("D");
            lineDebit += (TypeOfDemands);
            lineDebit += (valueDate);
            lineDebit += ("     ");
            lineDebit += (DemandDate);
            lineDebit += (TranFlag);
            lineDebit += (TranEndInd);

            if (!result.Equals(string.Empty))
            {
                result += "\n" + lineDebit;
            }
            //string lineDedit = "";
            //string EFTDebitNarration = "EFTIM" + "-BBAccount";
            //string emptyString = string.Empty;

            //EFTDebitNarration = EFTDebitNarration.PadRight(30, ' ');
            //lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"].PadRight(16, ' ') + delim);
            //lineDedit += ("BDT" + delim);
            //lineDedit += (ConfigurationManager.AppSettings["SolID"].PadRight(8, ' ') + delim);
            //lineDedit += ("D" + delim);
            //lineDedit += (totalDebitAmount.ToString("0.00").PadLeft(17, ' ') + delim);
            //lineDedit += (EFTDebitNarration + delim);
            //lineDedit += ("021  " + delim); //TransactionCode
            //lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"].PadRight(75, ' ') + delim); //IDNumber
            //lineDedit += "BDT";
            //lineDedit += (emptyString.PadRight(20, ' ') + delim);
            //lineDedit += (System.DateTime.Now.ToString("dd-MM-yyyy") + delim);

            //if (!result.Equals(string.Empty))
            //{
            //    result += "\n" + lineDedit;
            //}

            return result;
        }

        public string CreatFlatFileForReturnReceivedIB(DataTable dt, string valueDate, string cbl_UserName, string cbl_Password)
        {
            string result = string.Empty;

            double totalDebitAmount = 0;

            string BankName = string.Empty;
            string BranchName = string.Empty;
            string TraceNumber = string.Empty;
            string TransactionCode = string.Empty;
            string AccountNo = string.Empty;
            string BankRoutingNo = string.Empty;

            string Amount = string.Empty;
            string Trantype = string.Empty;
            string Trancode = string.Empty;
            string TransactionDetail = string.Empty;
            string FlowID = string.Empty;
            string DemandDate = string.Empty;
            string TranFlag = string.Empty;
            string TranEndInd = string.Empty;
            string blankSpace = string.Empty;

            GetAccountDetailsResponse accountDetailsResponse = new GetAccountDetailsResponse();

            foreach (DataRow row in dt.Rows)
            {
                BankName = row["BankName"].ToString();
                BranchName = row["BranchName"].ToString();
                TraceNumber = row["TraceNumber"].ToString();
                TransactionCode = row["TransactionCode"].ToString();
                AccountNo = row["AccountNo"].ToString();
                BankRoutingNo = row["BankRoutingNo"].ToString();

                Amount = row["Amount"].ToString();
                Trantype = row["Trantype"].ToString();
                Trancode = row["Trancode"].ToString();
                TransactionDetail = row["TransactionDetail"].ToString();

                totalDebitAmount += EFTN.Utility.ParseData.StringToDouble(Amount.Trim());

                accountDetailsResponse = GetAccountDetailsFromCityCBS(cbl_UserName, cbl_Password, AccountNo);

                string SOL_ID = accountDetailsResponse.responseData.solId;

                string line = "";
                line += AccountNo.PadRight(16, ' ');
                line += "BDT";
                line += SOL_ID.PadRight(8, ' ');
                line += Trantype.PadRight(1, ' ');
                line += Amount.PadLeft(17, ' ');
                line += TransactionDetail.PadRight(30, ' ');
                line += Trancode.PadRight(5, ' ');
                line += blankSpace.PadRight(76, ' ');
                line += "BDT";
                line += blankSpace.PadRight(20, ' ');
                line += valueDate;
                if (result.Equals(string.Empty))
                {
                    result += line;
                }
                else
                {
                    result += "\n" + line;
                }
            }

            string IBDebitAccount = ConfigurationManager.AppSettings["IBDebitAccount"].PadRight(16, ' ');
            string SOL_ID_Debit = "100";
            string DebitTransactionDetail = "OPSD IBANKING EFT RETURN";
            string debitTransactionCode = "021";

            string lineDebit = "";
            lineDebit += IBDebitAccount.PadRight(16, ' ');
            lineDebit += "BDT";
            lineDebit += SOL_ID_Debit.PadRight(8, ' ');
            lineDebit += "D";
            lineDebit += totalDebitAmount.ToString().PadLeft(17, ' ');
            lineDebit += DebitTransactionDetail.PadRight(30, ' ');
            lineDebit += debitTransactionCode.PadRight(5, ' ');
            lineDebit += blankSpace.PadRight(76, ' ');
            lineDebit += "BDT";
            lineDebit += blankSpace.PadRight(20, ' ');
            lineDebit += valueDate;

            if (!result.Equals(string.Empty))
            {
                result = lineDebit + "\n" + result + "\n";
            }

            return result;
        }

        private GetAccountDetailsResponse GetAccountDetailsFromCityCBS(string UserName, string Pwd, string AccountNumber)
        {
            //string param = "{'username':'" + UserName + "', 'password':'" + Pwd + "', 'currencyCode':'BDT', 'debitAccount':'" + DebitActNo + "','transactionAmount': '" + transactionAmount + "', 'creditAccount':'" + CreditActNo + "','remarks':'" + remarks + "'}";
            //WriteLog(param);

            CBLWebServices myService = new CBLWebServices();
            GetAccountDetailsRequest accountDetailsRequest = new GetAccountDetailsRequest();
            accountDetailsRequest.username = UserName;
            accountDetailsRequest.password = Pwd;
            accountDetailsRequest.accountNumber = AccountNumber;

            return myService.getAccountDetails(accountDetailsRequest);
        }

    }
}