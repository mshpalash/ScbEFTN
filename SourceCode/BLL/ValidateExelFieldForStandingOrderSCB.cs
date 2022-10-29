using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using EFTN.Utility;

namespace EFTN.BLL
{
    public class ValidateExelFieldForStandingOrderSCB
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
            Regex objAlphaNumericPattern = new Regex("[^a-z.,() A-Z0-9-]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        private bool IsEFTAccountNumber(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9/.#()&: -]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        public string ValidateExcelForEFTStandingOrderSCB(string excelFilePath, string SelectedBank, int paymentType)
        {
            //string extension = System.IO.Path.GetExtension(excelFilePath);
            DataTable dtExcel = new DataTable();
            //EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();


            EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();
            //DataTable dtExcel = excelDB.GetData(excelFilePath);

            //if (extension.ToLower().Equals(".txt"))
            //{
            //    string delim = "|";
            //    dtExcel = DelimitedTextReader.ReadFile(excelFilePath, delim);
            //}
            //else
            //{
            dtExcel = excelDB.GetData(excelFilePath);
            //}

            StringBuilder errorString = new StringBuilder();
            int rowNumber = 1;
            int totalError = 0;

            if (dtExcel.Rows.Count < 1)
            {
                errorString.AppendLine("Data is not available in Sheet1");
                errorString.AppendLine("or Sheet1 not found");
                errorString.AppendLine("or rename your data sheet as Sheet1.");
            }

            for (int i = 0; i < dtExcel.Rows.Count; i++)
            //foreach (DataRow row in data.Rows)
            {
                rowNumber++;
                string accountType = string.Empty;
                try
                {
                    accountType = dtExcel.Rows[i]["Account Type"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Account Type\" column not found");
                    break;
                }

                string dfiAccNo = string.Empty;
                try
                {
                    dfiAccNo = dtExcel.Rows[i]["DFI Account No"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"DFI Account No\" column not found");
                    break;
                }

                string accNo = string.Empty;
                try
                {
                    accNo = dtExcel.Rows[i]["Sender Account No"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Sender Account No\" column not found");
                    break;
                }


                string receivingBankRoutingNo = string.Empty;
                try
                {
                    receivingBankRoutingNo = dtExcel.Rows[i]["Other Bank Routing Number"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Other Bank Routing Number\" column not found");
                    break;
                }

                //decimal dAmount = Decimal.Parse(dtExcel.Rows[i]["Amount"].ToString().Replace(",", ""));
                string amount = string.Empty;
                //string amount = dAmount.ToString();
                try
                {
                    amount = dtExcel.Rows[i]["Installment Amount"].ToString().Replace(",", "").Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Installment Amount\" column not found");
                    break;
                }

                string idNumber = dfiAccNo;

                string receiverName = string.Empty;
                try
                {
                    receiverName = dtExcel.Rows[i]["Customer Name with Other Bank"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Customer Name with Other Bank\" column not found");
                    break;
                }

                string strTransFreq = string.Empty;
                int transactionFrequency = 0;
                try
                {
                    strTransFreq = dtExcel.Rows[i]["Installment Frequency"].ToString().Trim();
                    transactionFrequency = StandingOrderTransactionFrequency.GetTransactionFrequency(strTransFreq);
                }
                catch
                {
                    errorString.AppendLine("\"Installment Frequency\" column not found");
                    break;
                }


                string strBegDate = "D";
                try
                {
                    strBegDate = dtExcel.Rows[i]["Installment Start Date"].ToString().Trim();
                    if (strBegDate.Equals(string.Empty))
                    {
                        if (
                            //paymentInfo.Equals(string.Empty) &&
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

                        errorString.Append("Row No. = ");
                        errorString.Append(rowNumber);
                        errorString.AppendLine(" - Invalid \"Installment Start Date\"");
                    }
                    DateTime stdBeginDate = DateTime.Parse(strBegDate);
                    if (stdBeginDate <= System.DateTime.Today)
                    {
                        errorString.Append("Row No. = ");
                        errorString.Append(rowNumber);
                        errorString.AppendLine(" - \"Installment Start Date\" should be greater than current date");
                    }
                }
                catch
                {
                    if (strBegDate.Equals("D"))
                    {
                        errorString.AppendLine("\"Installment Start Date\" column not found");
                        break;
                    }
                }

                string strEndDate = "D";
                try
                {
                    strEndDate = dtExcel.Rows[i]["Installment Expairy Date"].ToString().Trim();
                    if (strEndDate.Equals(string.Empty))
                    {
                        if (
                            //paymentInfo.Equals(string.Empty) &&
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
                        errorString.Append("Row No. = ");
                        errorString.Append(rowNumber);
                        errorString.AppendLine(" - Invalid \"Installment Expairy Date\"");
                    }
                    DateTime stdEndDate = DateTime.Parse(strEndDate);
                    if (stdEndDate <= System.DateTime.Today)
                    {
                        errorString.Append("Row No. = ");
                        errorString.Append(rowNumber);
                        errorString.AppendLine(" - \"Installment Expiry Date\" should be greater than current date");
                    }
                }
                catch
                {
                    if (strEndDate.Equals("D"))
                    {
                        errorString.AppendLine("\"Installment Expairy Date\" column not found");
                        break;
                    }
                }

                //int approvedBy = 1;
                //string paymentInfo = "Standing Order";

                if (
                    //paymentInfo.Equals(string.Empty) &&
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

                if (transactionFrequency == 0)
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Installment Frequency is not in the norrect format - ");
                    errorString.AppendLine(strTransFreq);
                    totalError++;
                }

                if (accountType.ToUpper().StartsWith("C") || accountType.ToUpper().StartsWith("S"))
                {
                }
                else
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Account Type is not in the norrect format - ");
                    errorString.AppendLine(accountType);
                    totalError++;
                }


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
                //if (!IsWholeNumber(dfiAccNo) || dfiAccNo.Equals(string.Empty))
                if (dfiAccNo.Equals(string.Empty))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Bank Account No. is not in the correct format - ");
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
                    errorString.Append(" - Sender Account No. legnth is greater than ");
                    errorString.Append(EFTLength.SenderAccNumberLength);
                    errorString.Append("\t");
                    errorString.AppendLine(accNo);
                    totalError++;
                }
                //if (!IsWholeNumber(accNo) || accNo.Equals(string.Empty))
                if (accNo.Equals(string.Empty))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Sender Account No. is not in the correct format - ");
                    errorString.AppendLine(accNo);
                    totalError++;
                }

                if (!IsEFTAccountNumber(accNo.Replace(" ", "")))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Bank Account No. is not in the correct format - ");
                    errorString.AppendLine(accNo);
                    totalError++;
                }

                if (receivingBankRoutingNo.Length > EFTLength.ReceivingBankRoutingLength)
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Receiving Bank Routing No. legnth is greater than ");
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
                    errorString.Append(" - Receiving Bank Routing No. is not in the correct format - ");
                    errorString.AppendLine(receivingBankRoutingNo);
                    totalError++;
                }

                if (!IsPositiveNumber(amount) || amount.Equals(string.Empty))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Amount is not in the correct format - ");
                    errorString.AppendLine(amount);
                    totalError++;
                }
                if (paymentType == 2)
                {
                    if (idNumber.Length > EFTLength.ReceiverIDLengthForCIE)
                    {
                        errorString.Append("Row No. = ");
                        errorString.Append(rowNumber);
                        errorString.Append(" - ReceiverID legnth is greater than ");
                        errorString.Append(EFTLength.ReceiverIDLengthForCIE);
                        errorString.Append("\t");
                        errorString.AppendLine(idNumber);
                        totalError++;
                    }
                }
                else
                {
                    if (idNumber.Length > EFTLength.ReceiverIDLength)
                    {
                        errorString.Append("Row No. = ");
                        errorString.Append(rowNumber);
                        errorString.Append(" - ReceiverID legnth is greater than ");
                        errorString.Append(EFTLength.ReceiverIDLength);
                        errorString.Append("\t");
                        errorString.AppendLine(idNumber);
                        totalError++;
                    }
                }
                if (!IsEFTAccountNumber(idNumber) || idNumber.Equals(string.Empty))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Receiver ID is not in the correct format - ");
                    errorString.AppendLine(idNumber);
                    totalError++;
                }

                if (!SelectedBank.Equals("135"))
                {
                    if (paymentType == 2)
                    {
                        if (receiverName.Length > EFTLength.ReceiverNameLengthForCIE)
                        {
                            errorString.Append("Row No. = ");
                            errorString.Append(rowNumber);
                            errorString.Append(" - Receiver Name legnth is greater than ");
                            errorString.Append(EFTLength.ReceiverNameLengthForCIE);
                            errorString.Append("\t");
                            errorString.AppendLine(receiverName);
                            totalError++;
                        }
                    }
                    else
                    {
                        if (receiverName.Length > EFTLength.ReceiverNameLength)
                        {
                            errorString.Append("Row No. = ");
                            errorString.Append(rowNumber);
                            errorString.Append(" - Receiver Name legnth is greater than ");
                            errorString.Append(EFTLength.ReceiverNameLength);
                            errorString.Append("\t");
                            errorString.AppendLine(receiverName);
                            totalError++;
                        }
                    }
                }
                //if (!IsAlphaNumericForEFT(receiverName) || receiverName.Equals(string.Empty))
                if (receiverName.Equals(string.Empty))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Receiver Name is not in the correct format - ");
                    errorString.AppendLine(receiverName);
                    totalError++;
                }

                if (!IsEFTAccountNumber(receiverName) || receiverName.Equals(string.Empty))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Receiver Name is not in the correct format - ");
                    errorString.AppendLine(receiverName);
                    totalError++;
                }

                //if (paymentInfo.Length > EFTLength.ReasonLength)
                //{
                //    errorString.Append("Row No. = ");
                //    errorString.Append(rowNumber);
                //    errorString.Append(" - Reason legnth is greater than ");
                //    errorString.Append(EFTLength.ReasonLength);
                //    errorString.Append("\t");
                //    errorString.AppendLine(paymentInfo);
                //    totalError++;
                //}
                //if (!IsEFTAccountNumber(paymentInfo) || paymentInfo.Equals(string.Empty))
                //{
                //    errorString.Append("Row No. = ");
                //    errorString.Append(rowNumber);
                //    errorString.Append(" - Payment Info is not in the correct format - ");
                //    errorString.AppendLine(paymentInfo);
                //    totalError++;
                //}
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
