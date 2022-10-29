using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using EFTN.Utility;

namespace EFTN.BLL
{
    public class ValidateCSVFieldForSCB
    {
        private string errorString;

        private bool IsNaturalNumber(String strNumber)
        {
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(strNumber) &&
            objNaturalPattern.IsMatch(strNumber);
        }
        // Function to test for Positive Integers with zero inclusive 
        private bool IsWholeNumber(String strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }
        // Function to Test for Integers both Positive & Negative 
        private bool IsInteger(String strNumber)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
            return !objNotIntPattern.IsMatch(strNumber) && objIntPattern.IsMatch(strNumber);
        }
        // Function to Test for Positive Number both Integer & Real 
        private bool IsPositiveNumber(String strNumber)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return !objNotPositivePattern.IsMatch(strNumber) &&
            objPositivePattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber);
        }
        // Function to test whether the string is valid number or not
        private bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
        }
        // Function To test for Alphabets. 
        private bool IsAlpha(String strToCheck)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }
        // Function to Check for AlphaNumeric.
        private bool IsAlphaNumeric(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        private bool IsAlphaNumericForEFT(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9/.#()&: ,'_%-]"); //comma(,) and appose(') to be Percent(%)considered as per SCB request
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        private bool IsEFTAccountNumber(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9/.#()&:-]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        public string ValidateCSVEFTForSCB(string CSVFilePath, int paymentType)
        {
            string extension = System.IO.Path.GetExtension(CSVFilePath);

            EFTCSVReader eftCSVReader = new EFTCSVReader();
            DataTable dtCSVforSCB = eftCSVReader.ReadCSV(CSVFilePath);

            StringBuilder errorString = new StringBuilder();
            int rowNumber = 1;
            int totalError = 0;

            if (dtCSVforSCB.Rows.Count < 1)
            {
                errorString.AppendLine("Data is not available");
            }

            for (int i = 0; i < dtCSVforSCB.Rows.Count; i++)
            {
                rowNumber++;
                string accountType = string.Empty;
                try
                {
                    accountType = dtCSVforSCB.Rows[i]["Payee Address 1 BO"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Payee Address 1 BO\" column not found");
                    break;
                }

                string dfiAccNo = string.Empty;
                try
                {
                    dfiAccNo = dtCSVforSCB.Rows[i]["Beneficiary Account"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Beneficiary Account\" column not found");
                    break;
                }

                string accNo = string.Empty;
                try
                {
                    accNo = dtCSVforSCB.Rows[i]["Debit Account No."].ToString().Trim().Replace(",", "");

                }
                catch
                {
                    errorString.AppendLine("\"Debit Account No.\" column not found");
                    break;
                }


                string receivingBankRoutingNo = string.Empty;
                try
                {
                    receivingBankRoutingNo = dtCSVforSCB.Rows[i]["Payee Address 2 BO"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Payee Address 2 BO\" column not found");
                    break;
                }

                string amount = string.Empty;

                try
                {
                    amount = dtCSVforSCB.Rows[i]["Invoice Amount"].ToString().Replace(",", "").Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Invoice Amount\" column not found");
                    break;
                }

                string idNumber = string.Empty;
                try
                {
                    idNumber = dtCSVforSCB.Rows[i]["Customer Ref."].ToString().Trim().Replace(",", "");

                    if (paymentType == 2)
                    {
                        if (idNumber.Length > 22)
                        {
                            idNumber = idNumber.Substring(0, 22);
                        }
                    }
                    else
                    {
                        if (idNumber.Length > 15)
                        {
                            idNumber = idNumber.Substring(0, 15);
                        }
                    }
                }
                catch
                {
                    errorString.AppendLine("\"Customer Ref.\" column not found");
                    break;
                }

                string receiverName = string.Empty;
                try
                {
                    receiverName = dtCSVforSCB.Rows[i]["Payee Name"].ToString().Trim().Replace(",", "");

                    if (paymentType == 2)
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
                }
                catch
                {
                    errorString.AppendLine("\"Payee Name\" column not found");
                    break;
                }

                //int approvedBy = 1;
                string paymentInfo = string.Empty;
                try
                {
                    paymentInfo = dtCSVforSCB.Rows[i]["Payee Details 1 BO"].ToString().Trim().Replace(",", "");
                    if (paymentInfo.Length > 80)
                    {
                        paymentInfo = receiverName.Substring(0, 80);
                    }
                }
                catch
                {
                    errorString.AppendLine("\"Payee Details 1 BO\" column not found");
                    break;
                }

                if (
                       paymentInfo.Equals(string.Empty) &&
                       accNo.Equals(string.Empty) &&
                       receivingBankRoutingNo.Equals(string.Empty) &&
                       dfiAccNo.Equals(string.Empty) &&
                       accountType.Equals(string.Empty) &&
                       amount.Equals(string.Empty) &&
                       idNumber.Equals(string.Empty) &&
                       receiverName.Equals(string.Empty)
                  )
                {
                    continue;
                }

               paymentInfo = EFTVariableParser.ParseEFTPaymentInfo(paymentInfo);
               accNo = EFTVariableParser.ParseEFTAccountNumber(accNo);
               receivingBankRoutingNo = EFTVariableParser.ParseEFTRoutingNumber(receivingBankRoutingNo);
               dfiAccNo = EFTVariableParser.ParseEFTAccountNumber(dfiAccNo);
               accountType = EFTVariableParser.ParseEFTAccountNumber(accountType);
               amount = EFTVariableParser.ParseEFTAmount(amount);
               idNumber = EFTVariableParser.ParseEFTReceiverID(idNumber);
               receiverName = EFTVariableParser.ParseEFTName(receiverName);


                if (dfiAccNo.Length > EFTLength.BankAccNoLength)
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Bank Account No. legnth is greater than ");
                    errorString.Append(EFTLength.BankAccNoLength);
                    errorString.Append("\t");
                    errorString.AppendLine(dfiAccNo);
                    totalError++;
                }

                if (dfiAccNo.Equals(string.Empty))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Missing mandatory data [Bank Account No.]");
                    errorString.AppendLine(dfiAccNo);
                    totalError++;
                }

                if (!IsEFTAccountNumber(dfiAccNo.Replace(" ", "")))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Bank Account No. is not in the correct format - ");
                    errorString.AppendLine(dfiAccNo);
                    totalError++;
                }

                if (accNo.Length > EFTLength.SenderAccNumberLength)
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Debit Account No. legnth is greater than ");
                    errorString.Append(EFTLength.SenderAccNumberLength);
                    errorString.Append("\t");
                    errorString.AppendLine(accNo);
                    totalError++;
                }

                if (accNo.Equals(string.Empty))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Missing mandatory data [Debit Account No.] - ");
                    errorString.AppendLine(accNo);
                    totalError++;
                }

                if (!IsEFTAccountNumber(accNo.Replace(" ", "")))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Debit Account No. is not in the correct format - ");
                    errorString.AppendLine(accNo);
                    totalError++;
                }

                if (receivingBankRoutingNo.Length > EFTLength.ReceivingBankRoutingLength)
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Payee Address 2 BO legnth is greater than ");
                    errorString.Append(EFTLength.ReceivingBankRoutingLength);
                    errorString.Append("\t");
                    errorString.AppendLine(receivingBankRoutingNo);
                    totalError++;
                }
                if (!IsWholeNumber(receivingBankRoutingNo) || receivingBankRoutingNo.Equals(string.Empty)
                    || receivingBankRoutingNo.Length != 9)
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Payee Address 2 BO is not in the correct format - ");
                    errorString.AppendLine(receivingBankRoutingNo);
                    totalError++;
                }

                if (!IsPositiveNumber(amount) || amount.Equals(string.Empty))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Invoice Amount is not in the correct format - ");
                    errorString.AppendLine(amount);
                    totalError++;
                }

                if (!IsAlphaNumericForEFT(idNumber))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Receiver ID is not in the correct format - ");
                    errorString.AppendLine(idNumber);
                    totalError++;
                }

                if (receiverName.Equals(string.Empty))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Missing mandatory data [Payee Name]");
                    errorString.AppendLine(receiverName);
                    totalError++;
                }

                if (!IsAlphaNumericForEFT(receiverName) || receiverName.Equals(string.Empty))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Receiver Name is not in the correct format - ");
                    errorString.AppendLine(receiverName);
                    totalError++;
                }

                if (!IsAlphaNumericForEFT(paymentInfo))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Payment Info is not in the correct format - ");
                    errorString.AppendLine(paymentInfo);
                    totalError++;
                }
            }

            if (totalError > 0)
            {
                errorString.Append("Total Error = ");
                errorString.AppendLine(totalError.ToString());
            }

            return errorString.ToString();
        }
    }
}
