using System;
using System.Data;
using System.Configuration;
using System.IO;
using EFTN.Utility;
using System.Text;


namespace EFTN.BLL
{
    public class TextFileGeneratorToUpload
    {

        public string GenerateTextDataForOutwardTransactionUpload(DataTable dt)
        {
            string delim = "|";

            string result = "Reason|SenderAccNumber|ReceivingBankRouting|BankAccNo|AccType|Amount|ReceiverID|ReceiverName|Currency";

            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                i++;
                string Reason = "BEFTN FROM S2B";
                string SenderAccNumber = row["AccountNo"].ToString();
                string Amount = row["Amount"].ToString();
                string ReceivingBankRouting = row["BankRoutingNo"].ToString();
                string BankAccNo = row["DFIAccountNo"].ToString();
                string AccType = row["AccType"].ToString();
                string ReceiverID = i.ToString();
                string ReceiverName = row["ReceiverName"].ToString();
                string currency = row["Currency"].ToString();

                StringBuilder line = new StringBuilder();
                line.AppendLine();
                line.Append(Reason + delim);
                line.Append(SenderAccNumber + delim);
                line.Append(ReceivingBankRouting + delim);
                line.Append(BankAccNo + delim);
                line.Append(AccType + delim);
                line.Append(Amount + delim);
                line.Append(ReceiverID + delim);
                line.Append(ReceiverName + delim);
                line.Append(currency);

                result += line.ToString();
            }
            return result;
        }
    }
}