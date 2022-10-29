using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using EFTN.Utility;

namespace EFTN.BLL
{
    public class ValidateExelField
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
               
        
        public string ValidateExcelForEFT(string excelFilePath, string SelectedBank, int paymentType)
        {            
            string extension = System.IO.Path.GetExtension(excelFilePath);
            DataTable dtExcel = new DataTable();
            //EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();

            EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();
            //DataTable dtExcel = excelDB.GetData(excelFilePath);

            if (extension.ToLower().Equals(".txt"))
            {
                string delim = "|";
                dtExcel = DelimitedTextReader.ReadFile(excelFilePath, delim);
            }
            else
            {
                dtExcel = excelDB.GetData(excelFilePath);
            }

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
                    accountType = dtExcel.Rows[i]["AccType"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"AccType\" column not found");
                    break;
                }

                string dfiAccNo = string.Empty;
                try
                {
                    dfiAccNo = dtExcel.Rows[i]["BankAccNo"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"BankAccNo\" column not found");
                    break;
                }

                string accNo = string.Empty;
                try
                {
                    accNo = dtExcel.Rows[i]["SenderAccNumber"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"SenderAccNumber\" column not found");
                    break;
                }


                string receivingBankRoutingNo = string.Empty;
                try
                {
                    receivingBankRoutingNo = dtExcel.Rows[i]["ReceivingBankRouting"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"ReceivingBankRouting\" column not found");
                    break;
                }

                //decimal dAmount = Decimal.Parse(dtExcel.Rows[i]["Amount"].ToString().Replace(",", ""));
                string amount = string.Empty;
                //string amount = dAmount.ToString();
                try
                {
                    amount = dtExcel.Rows[i]["Amount"].ToString().Replace(",", "").Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Amount\" column not found");
                    break;
                }

                string idNumber = string.Empty;
                try
                {
                    idNumber = dtExcel.Rows[i]["ReceiverID"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"ReceiverID\" column not found");
                    break;
                }

                string receiverName = string.Empty;
                try
                {
                    receiverName = dtExcel.Rows[i]["ReceiverName"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"ReceiverName\" column not found");
                    break;
                }

                //int approvedBy = 1;
                string paymentInfo = string.Empty;
                try
                {
                    paymentInfo = dtExcel.Rows[i]["Reason"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Reason\" column not found");
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
                    errorString.Append(" - Bank Account No. is not in the correct format or format the cell as 'text' - ");
                    errorString.AppendLine(dfiAccNo);
                    totalError++;
                }

                if (!IsEFTAccountNumber(dfiAccNo.Replace(" ", "")))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Bank Account No. is not in the correct format or format the cell as 'text'- ");
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

                if (SelectedBank.Equals(receivingBankRoutingNo.Substring(0, 3)))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - EFTN does not process ON US transaction - ");
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

                if (paymentInfo.Length > EFTLength.ReasonLength)
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Reason legnth is greater than ");
                    errorString.Append(EFTLength.ReasonLength);
                    errorString.Append("\t");
                    errorString.AppendLine(paymentInfo);
                    totalError++;
                }
                if (!IsEFTAccountNumber(paymentInfo) || paymentInfo.Equals(string.Empty))
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
        //Added new _ 03-Sep-2019
        public DataTable ValidateExcelForEFTNewBACH(string excelFilePath, string SelectedBank, int paymentType)
        {
            CorporateUploadCredit.errorStringForExcelUpload = string.Empty;
            CorporateUploadDebit.errorStringForExcelUpload = string.Empty;
            string extension = System.IO.Path.GetExtension(excelFilePath);
            DataTable dtExcel = new DataTable();
            //EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();


            EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();
            //DataTable dtExcel = excelDB.GetData(excelFilePath);

            if (extension.ToLower().Equals(".txt"))
            {
                string delim = "|";
                dtExcel = DelimitedTextReader.ReadFile(excelFilePath, delim);
            }
            else
            {
                dtExcel = excelDB.GetData(excelFilePath);
            }

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
                    accountType = dtExcel.Rows[i]["AccType"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"AccType\" column not found");
                    break;
                }

                string dfiAccNo = string.Empty;
                try
                {
                    dfiAccNo = dtExcel.Rows[i]["BankAccNo"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"BankAccNo\" column not found");
                    break;
                }

                string accNo = string.Empty;
                try
                {
                    accNo = dtExcel.Rows[i]["SenderAccNumber"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"SenderAccNumber\" column not found");
                    break;
                }


                string receivingBankRoutingNo = string.Empty;
                try
                {
                    receivingBankRoutingNo = dtExcel.Rows[i]["ReceivingBankRouting"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"ReceivingBankRouting\" column not found");
                    break;
                }

                //decimal dAmount = Decimal.Parse(dtExcel.Rows[i]["Amount"].ToString().Replace(",", ""));
                string amount = string.Empty;
                //string amount = dAmount.ToString();
                try
                {
                    amount = dtExcel.Rows[i]["Amount"].ToString().Replace(",", "").Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Amount\" column not found");
                    break;
                }

                string idNumber = string.Empty;
                try
                {
                    idNumber = dtExcel.Rows[i]["ReceiverID"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"ReceiverID\" column not found");
                    break;
                }

                string receiverName = string.Empty;
                try
                {
                    receiverName = dtExcel.Rows[i]["ReceiverName"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"ReceiverName\" column not found");
                    break;
                }

                //int approvedBy = 1;
                string paymentInfo = string.Empty;
                try
                {
                    paymentInfo = dtExcel.Rows[i]["Reason"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Reason\" column not found");
                    break;
                }

                string currency = string.Empty;
                try
                {
                    currency = dtExcel.Rows[i]["Currency"].ToString().Trim();
                }
                catch
                {
                    errorString.AppendLine("\"Currency\" column not found");
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
                       && currency.Equals(string.Empty)
                  )
                {
                    continue;
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
                    errorString.Append(" - Bank Account No. is not in the correct format or format the cell as 'text' - ");
                    errorString.AppendLine(dfiAccNo);
                    totalError++;
                }

                if (!IsEFTAccountNumber(dfiAccNo.Replace(" ", "")))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Bank Account No. is not in the correct format or format the cell as 'text'- ");
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

                if (SelectedBank.Equals(receivingBankRoutingNo.Substring(0, 3)))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - EFTN does not process ON US transaction - ");
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

                if (paymentInfo.Length > EFTLength.ReasonLength)
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Reason legnth is greater than ");
                    errorString.Append(EFTLength.ReasonLength);
                    errorString.Append("\t");
                    errorString.AppendLine(paymentInfo);
                    totalError++;
                }
                if (!IsEFTAccountNumber(paymentInfo) || paymentInfo.Equals(string.Empty))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Payment Info is not in the correct format - ");
                    errorString.AppendLine(paymentInfo);
                    totalError++;
                }
                if (currency.Equals(string.Empty))
                {
                    errorString.Append("Row No. = ");
                    errorString.Append(rowNumber);
                    errorString.Append(" - Currency is not in the correct format! ");
                    errorString.AppendLine(currency);
                    totalError++;
                }
            }

            if (totalError > 0)
            {
                errorString.Append("Total Error = ");
                errorString.AppendLine(totalError.ToString());

                
                CorporateUploadCredit.errorStringForExcelUpload = errorString.ToString();
                CorporateUploadDebit.errorStringForExcelUpload = errorString.ToString();
            }
            return dtExcel;
            //Commented out and added new _ 03-Sep-2019
            //return errorString.ToString();
        }
    }
}
