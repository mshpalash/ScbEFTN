using System;
using System.Data;
using System.Configuration;
using System.IO;
using EFTN.Utility;


namespace EFTN.BLL
{
    public class FlatFileClient
    {
        public void CreatFlatFile(DataTable dt, string path, string fileNamePrefix)
        {
            DateTime currentDate = DateTime.Now;
            string fileName = "CBS" + "-" + fileNamePrefix + "-D" + currentDate.ToString("yyMMdd") + "-T" + currentDate.ToString("HHmmss") + ".TXT";
            FileInfo fi = new FileInfo(path + fileName);
            StreamWriter writer = fi.CreateText();
            string delim = ConfigurationManager.AppSettings["Delim"];

            foreach (DataRow row in dt.Rows)
            {
                string line = "";
                foreach (DataColumn col in dt.Columns)
                {
                    string value = row[col.ColumnName].ToString();
                    line += (value + delim);
                }
                writer.WriteLine(line);
            }
            writer.Close();
        }

        public string CreatFlatFileForTransactionReceived(DataTable dt, string TransactionType, int DepartmentID)
        {
            string delim = ConfigurationManager.AppSettings["Delim"];
            string fcyHeader = string.Empty;
            string result = string.Empty;
            string masterNo = string.Empty;

            if (ConfigurationManager.AppSettings["ImmediateOriginName"].Equals("SCB"))
            {
                if (TransactionType.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    //string fcyHeaderAmount= dt.Compute("SUM(Amount)", "").ToString();
                    string fcyHeaderAmount = GetTotalAmount(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string CurrencyCode = row["CurrencyCode"].ToString();
                        string AccountNo = row["AccountNo"].ToString();
                        string Amount = row["Amount"].ToString();
                        string DorC = row["DorC"].ToString();
                        string TranType = row["TranType"].ToString();
                        string valueDate = row["valueDate"].ToString();
                        string Narration1 = row["Narration1"].ToString();

                        if (!CurrencyCode.Equals("00"))
                        {
                            fcyHeader = "00" + ConfigurationManager.AppSettings["OriginatorID"] + valueDate + fcyHeaderAmount.PadLeft(15, '0') + DorC;
                        }

                        string lineDedit = "";
                        string line = "";
                        lineDedit += (CurrencyCode + delim);
                        if (CurrencyCode.Equals("00"))
                        {
                            line = "";
                            line += (CurrencyCode + delim);
                            line += (AccountNo + delim);
                            line += (Amount + delim);
                            line += (DorC + delim);
                            line += (TranType + delim);
                            line += (valueDate + delim);
                            line += (Narration1 + delim);

                            //lineDedit = "";
                            //lineDedit += (CurrencyCode + delim);


                            if (DepartmentID == 2)
                            {
                                lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBankPDC"] + delim);
                            }

                            else
                            {
                                lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"] + delim);
                            }

                            //lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"] + delim);
                            lineDedit += (Amount + delim);
                            lineDedit += ("C" + delim);
                            lineDedit += ("512" + delim);
                            lineDedit += (valueDate + delim);
                            lineDedit += (Narration1 + delim);
                        }
                        else
                        {
                            string Narrative1 = row["A"].ToString();
                            string Narrative2 = row["B"].ToString();
                            masterNo = row["MASTER"].ToString();
                            AccountNo = CurrencyCode + AccountNo;

                            line = string.Empty;
                            line += (CurrencyCode + delim);
                            line += (AccountNo + delim);
                            line += (Amount + delim);
                            line += (DorC + delim);
                            line += (Narrative1 + delim);
                            line += (Narrative2 + delim);
                            line += (valueDate + delim);
                            line += (TranType + delim);
                            line += (masterNo + delim);


                            lineDedit += (CurrencyCode + ConfigurationManager.AppSettings["FCYSuspenceAcc"] + delim);
                            lineDedit += (Amount + delim);
                            lineDedit += ("C" + delim);
                            lineDedit += (Narrative1 + delim);
                            lineDedit += (Narrative2 + delim);
                            lineDedit += (valueDate + delim);
                            lineDedit += ("512" + delim);
                            lineDedit += (masterNo + delim);


                        }


                        //if (result.Equals(string.Empty))
                        //{
                        //    result += line + "\n" + lineDedit;
                        //}

                        //else
                        //{
                        //    result += "\n" + line + "\n" + lineDedit;
                        //}

                        if (result.Equals(string.Empty) && !fcyHeader.Equals(string.Empty))
                        {
                            result += fcyHeader + "\n" + lineDedit + "\n" + line;
                        }
                        else if (result.Equals(string.Empty))
                        {
                            result += lineDedit + "\n" + line;
                        }
                        else
                        {
                            result += "\n" + lineDedit + "\n" + line;
                        }

                    }
                }
                else
                {

                    string fcyHeaderAmount = GetTotalAmount(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string CurrencyCode = row["CurrencyCode"].ToString();
                        string AccountNo = row["AccountNo"].ToString();
                        string Amount = row["Amount"].ToString();
                        string DorC = row["DorC"].ToString();
                        string TranType = row["TranType"].ToString();
                        string valueDate = row["valueDate"].ToString();
                        string Narration1 = row["Narration1"].ToString();

                        // string fcyHeaderAmount = row["Amount"].ToString();

                        if (!CurrencyCode.Equals("00"))
                        {
                            fcyHeader = "00" + ConfigurationManager.AppSettings["OriginatorID"] + valueDate + fcyHeaderAmount.PadLeft(15, '0') + DorC;
                        }

                        string lineDedit = "";
                        string line = "";
                        lineDedit += (CurrencyCode + delim);
                        if (CurrencyCode.Equals("00"))
                        {
                            if (DepartmentID == 2)
                            {
                                lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBankPDC"] + delim);
                            }
                            else
                            {
                                lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"] + delim);
                            }
                            //lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"] + delim);
                            lineDedit += (Amount + delim);
                            lineDedit += ("D" + delim);
                            lineDedit += (ConfigurationManager.AppSettings["TransTypeDebit"] + delim);
                            lineDedit += (valueDate + delim);
                            lineDedit += (Narration1 + delim);

                            line = string.Empty;
                            line += (CurrencyCode + delim);
                            line += (AccountNo + delim);
                            line += (Amount + delim);
                            line += (DorC + delim);
                            line += (TranType + delim);
                            line += (valueDate + delim);
                            line += (Narration1 + delim);
                        }
                        else
                        {

                            string Narrative1 = row["A"].ToString();
                            string Narrative2 = row["B"].ToString();
                            masterNo = row["MASTER"].ToString();
                            AccountNo = CurrencyCode + AccountNo;

                            lineDedit += (CurrencyCode + ConfigurationManager.AppSettings["FCYSuspenceAcc"] + delim);
                            lineDedit += (Amount + delim);
                            lineDedit += ("D" + delim);
                            lineDedit += (Narrative1 + delim);
                            lineDedit += (Narrative2 + delim);
                            lineDedit += (valueDate + delim);
                            lineDedit += (ConfigurationManager.AppSettings["TransTypeDebit"] + delim);
                            lineDedit += (masterNo + delim);

                            line = string.Empty;
                            line += (CurrencyCode + delim);
                            line += (AccountNo + delim);
                            line += (Amount + delim);
                            line += (DorC + delim);
                            line += (Narrative1 + delim);
                            line += (Narrative2 + delim);
                            line += (valueDate + delim);
                            line += (TranType + delim);
                            line += (masterNo + delim);
                        }



                        //if (result.Equals(string.Empty))
                        //{
                        //    result += lineDedit + "\n" + line;
                        //}

                        //else
                        //{
                        //    result += "\n" + lineDedit + "\n" + line;
                        //}

                        if (result.Equals(string.Empty) && !fcyHeader.Equals(string.Empty))
                        {
                            result += fcyHeader + "\n" + lineDedit + "\n" + line;
                        }
                        else if (result.Equals(string.Empty))
                        {
                            result += lineDedit + "\n" + line;
                        }
                        else
                        {
                            result += "\n" + lineDedit + "\n" + line;
                        }

                    }
                }
            }
            return result;
        }

        private string GetTotalAmount(DataTable dt)
        {
            decimal totalAmount = 0;
            foreach (DataRow item in dt.Rows)
            {
                decimal amount = decimal.Parse(item["Amount"].ToString());
                totalAmount = totalAmount + amount;
            }
            return totalAmount.ToString();
        }

        private string GetTotalAmount_BB_FCY(DataTable dt)
        {
            decimal totalAmount = 0;
            foreach (DataRow item in dt.Rows)
            {
                decimal amount = decimal.Parse(item["FCYHeaderAmount"].ToString());
                totalAmount = totalAmount + amount;
            }
            return totalAmount.ToString();
        }

        public string CreatFlatFileForTransactionReceivedDebitReverse(DataTable dt)
        {
            string delim = ConfigurationManager.AppSettings["Delim"];
            string fcyHeader = string.Empty;
            string masterNo = string.Empty;

            string DebitAccountNo = ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"];
            string CreditAccountNo = ConfigurationManager.AppSettings["ComputerSuspenseAccount"];

            string result = string.Empty;

            //string fcyHeaderAmount = dt.Compute("SUM(Amount)", "").ToString();
            string fcyHeaderAmount = GetTotalAmount(dt);

            foreach (DataRow row in dt.Rows)
            {
                string CurrencyCode = row["CurrencyCode"].ToString();
                //string AccountNo = row["AccountNo"].ToString();
                string DebitAccountNoForFCY = row["AccountNo"].ToString();
                string AccountNo = DebitAccountNo;
                string Amount = row["Amount"].ToString();
                string DorC = row["DorC"].ToString();
                string TranType = row["TranType"].ToString();
                string valueDate = row["valueDate"].ToString();
                string Narration1 = row["Narration1"].ToString();


                if (!CurrencyCode.Equals("00"))
                {
                    fcyHeader = "00" + ConfigurationManager.AppSettings["OriginatorID"] + valueDate + fcyHeaderAmount.PadLeft(15, '0') + DorC;
                }

                string line = "";
                string lineDedit = "";

                if (CurrencyCode.Equals("00"))
                {
                    //string line = "";
                    line += (CurrencyCode + delim);
                    line += (AccountNo + delim);
                    line += (Amount + delim);
                    line += (DorC + delim);
                    line += (TranType + delim);
                    line += (valueDate + delim);
                    line += (Narration1 + delim);

                    //string lineDedit = "";
                    lineDedit += (CurrencyCode + delim);


                    lineDedit += (CreditAccountNo + delim);

                    //lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"] + delim);
                    lineDedit += (Amount + delim);
                    lineDedit += ("C" + delim);
                    lineDedit += ("512" + delim);
                    lineDedit += (valueDate + delim);
                    lineDedit += (Narration1 + delim);

                }
                else
                {

                    string Narrative1 = row["A"].ToString();
                    string Narrative2 = row["B"].ToString();
                    masterNo = row["MASTER"].ToString();
                    // AccountNo = CurrencyCode + AccountNo; //It was set earlier this way
                    AccountNo = CurrencyCode + ConfigurationManager.AppSettings["FCYSuspenceAcc"] + delim;

                    //line = string.Empty;
                    line += (CurrencyCode + delim);
                    line += (AccountNo + delim);
                    line += (Amount + delim);
                    line += (DorC + delim);
                    line += (Narrative1 + delim);
                    line += (Narrative2 + delim);
                    line += (valueDate + delim);
                    line += (TranType + delim);
                    line += (masterNo + delim);

                    lineDedit += (CurrencyCode + delim);
                    lineDedit += (CurrencyCode + DebitAccountNoForFCY);
                    lineDedit += (Amount + delim);
                    lineDedit += ("C" + delim);
                    lineDedit += (Narrative1 + delim);
                    lineDedit += (Narrative2 + delim);
                    lineDedit += (valueDate + delim);
                    lineDedit += ("512" + delim);
                    lineDedit += (masterNo + delim);

                }

                //if (result.Equals(string.Empty))
                //{
                //    result += line + "\n" + lineDedit;
                //}

                //else
                //{
                //    result += "\n" + line + "\n" + lineDedit;
                //}

                if (result.Equals(string.Empty) && !fcyHeader.Equals(string.Empty))
                {
                    result += fcyHeader + "\n" + lineDedit + "\n" + line;
                }
                else if (result.Equals(string.Empty))
                {
                    result += lineDedit + "\n" + line;
                }
                else
                {
                    result += "\n" + lineDedit + "\n" + line;
                }
            }

            return result;
        }

        public string CreatFlatFileForReturnReceivedForSCB(DataTable dt, string TransactionType, int DepartmentID)
        {
            string delim = ConfigurationManager.AppSettings["Delim"];
            string fcyHeader = string.Empty;
            string result = string.Empty;
            string masterNo = string.Empty;


            if (ConfigurationManager.AppSettings["ImmediateOriginName"].Equals("SCB"))
            {
                string DDISuspenseAccount = ConfigurationManager.AppSettings["DDISuspenseAccount"];

                if (TransactionType.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    //string fcyHeaderAmount = dt.Compute("SUM(Amount)", "").ToString();
                    string fcyHeaderAmount = GetTotalAmount(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string CurrencyCode = row["CurrencyCode"].ToString();
                        string AccountNo = string.Empty;// = row["AccountNo"].ToString();
                        string Amount = row["Amount"].ToString();
                        string DorC = row["DorC"].ToString();
                        string TranType = row["TranType"].ToString();
                        string valueDate = row["valueDate"].ToString();
                        string Narration1 = row["Narration1"].ToString();
                        string DDIEDRID = row["DDIEDRID"].ToString();

                        if (!CurrencyCode.Equals("00"))
                        {
                            fcyHeader = "00" + ConfigurationManager.AppSettings["OriginatorID"] + valueDate + fcyHeaderAmount.PadLeft(15, '0') + DorC;
                        }

                        if (DDIEDRID.Equals("0"))
                        {
                            AccountNo = row["AccountNo"].ToString();
                        }
                        else
                        {
                            AccountNo = DDISuspenseAccount;
                        }

                        string lineDedit = "";
                        string line = "";
                        lineDedit += (CurrencyCode + delim);

                        if (CurrencyCode.Equals("00"))
                        {
                            line = "";
                            line += (CurrencyCode + delim);
                            line += (AccountNo + delim);
                            line += (Amount + delim);
                            line += (DorC + delim);
                            line += (TranType + delim);
                            line += (valueDate + delim);
                            line += (Narration1 + delim);

                            // lineDedit = "";
                            //lineDedit += (CurrencyCode + delim);

                            if (DepartmentID == 2)
                            {
                                lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBankPDC"] + delim);
                            }

                            else
                            {
                                lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"] + delim);
                            }

                            //lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"] + delim);
                            lineDedit += (Amount + delim);
                            lineDedit += ("C" + delim);
                            lineDedit += ("512" + delim);
                            lineDedit += (valueDate + delim);
                            lineDedit += (Narration1 + delim);

                        }
                        else
                        {
                            string Narrative1 = row["A"].ToString();
                            string Narrative2 = row["B"].ToString();
                            masterNo = row["MASTER"].ToString();
                            AccountNo = CurrencyCode + AccountNo;

                            line = string.Empty;
                            line += (CurrencyCode + delim);
                            line += (AccountNo + delim);
                            line += (Amount + delim);
                            line += (DorC + delim);
                            line += (Narrative1 + delim);
                            line += (Narrative2 + delim);
                            line += (valueDate + delim);
                            line += (TranType + delim);
                            line += (masterNo + delim);


                            lineDedit += (CurrencyCode + ConfigurationManager.AppSettings["FCYSuspenceAcc"] + delim);
                            lineDedit += (Amount + delim);
                            lineDedit += ("C" + delim);
                            lineDedit += (Narrative1 + delim);
                            lineDedit += (Narrative2 + delim);
                            lineDedit += (valueDate + delim);
                            lineDedit += ("512" + delim);
                            lineDedit += (masterNo + delim);

                        }



                        //if (result.Equals(string.Empty))
                        //{
                        //    result += line + "\n" + lineDedit;
                        //}

                        //else
                        //{
                        //    result += "\n" + line + "\n" + lineDedit;
                        //}

                        if (result.Equals(string.Empty) && !fcyHeader.Equals(string.Empty))
                        {
                            result += fcyHeader + "\n" + lineDedit + "\n" + line;
                        }
                        else if (result.Equals(string.Empty))
                        {
                            result += lineDedit + "\n" + line;
                        }
                        else
                        {
                            result += "\n" + lineDedit + "\n" + line;
                        }

                    }
                }
                else
                {
                    //string fcyHeaderAmount = dt.Compute("SUM(Amount)", "").ToString();
                    string fcyHeaderAmount = GetTotalAmount(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string CurrencyCode = row["CurrencyCode"].ToString();
                        string AccountNo = row["AccountNo"].ToString();
                        string Amount = row["Amount"].ToString();
                        string DorC = row["DorC"].ToString();
                        string TranType = row["TranType"].ToString();
                        string valueDate = row["valueDate"].ToString();
                        string Narration1 = row["Narration1"].ToString();


                        if (!CurrencyCode.Equals("00"))
                        {
                            fcyHeader = "00" + ConfigurationManager.AppSettings["OriginatorID"] + valueDate + fcyHeaderAmount.PadLeft(15, '0') + DorC;
                        }

                        string lineDedit = "";
                        string line = "";
                        lineDedit += (CurrencyCode + delim);


                        if (CurrencyCode.Equals("00"))
                        {
                            //lineDedit += (CurrencyCode + delim);
                            if (DepartmentID == 2)
                            {
                                lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBankPDC"] + delim);
                            }

                            else
                            {
                                lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"] + delim);
                            }

                            //lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"] + delim);
                            lineDedit += (Amount + delim);
                            lineDedit += ("D" + delim);
                            lineDedit += (ConfigurationManager.AppSettings["TransTypeDebit"] + delim);
                            lineDedit += (valueDate + delim);
                            lineDedit += (Narration1 + delim);

                            line = "";
                            line += (CurrencyCode + delim);
                            line += (AccountNo + delim);
                            line += (Amount + delim);
                            line += (DorC + delim);
                            line += (TranType + delim);
                            line += (valueDate + delim);
                            line += (Narration1 + delim);
                        }
                        else
                        {
                            string Narrative1 = row["A"].ToString();
                            string Narrative2 = row["B"].ToString();
                            masterNo = row["MASTER"].ToString();
                            AccountNo = CurrencyCode + AccountNo;

                            lineDedit += (CurrencyCode + ConfigurationManager.AppSettings["FCYSuspenceAcc"] + delim);
                            lineDedit += (Amount + delim);
                            lineDedit += ("D" + delim);
                            lineDedit += (Narrative1 + delim);
                            lineDedit += (Narrative2 + delim);
                            lineDedit += (valueDate + delim);
                            lineDedit += (ConfigurationManager.AppSettings["TransTypeDebit"] + delim);
                            lineDedit += (masterNo + delim);


                            line = string.Empty;
                            line += (CurrencyCode + delim);
                            line += (AccountNo + delim);
                            line += (Amount + delim);
                            line += (DorC + delim);
                            line += (Narrative1 + delim);
                            line += (Narrative2 + delim);
                            line += (valueDate + delim);
                            line += (TranType + delim);
                            line += (masterNo + delim);

                        }

                        //if (result.Equals(string.Empty))
                        //{
                        //    result += lineDedit + "\n" + line;
                        //}

                        //else
                        //{
                        //    result += "\n" + lineDedit + "\n" + line;
                        //}

                        if (result.Equals(string.Empty) && !fcyHeader.Equals(string.Empty))
                        {
                            result += fcyHeader + "\n" + lineDedit + "\n" + line;
                        }
                        else if (result.Equals(string.Empty))
                        {
                            result += lineDedit + "\n" + line;
                        }
                        else
                        {
                            result += "\n" + lineDedit + "\n" + line;
                        }


                    }
                }
            }
            return result;
        }

        public string CreatFlatFileForCSVRejection(DataTable dt, string TransactionType)
        {
            string delim = ConfigurationManager.AppSettings["Delim"];
            string fcyHeader = string.Empty;

            string masterNo = string.Empty;

            string result = string.Empty;

            if (ConfigurationManager.AppSettings["ImmediateOriginName"].Equals("SCB"))
            {
                if (TransactionType.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    //string fcyHeaderAmountm = dt.Compute("SUM(Amount)", "").ToString();
                    string fcyHeaderAmount = GetTotalAmount(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string CurrencyCode = row["CurrencyCode"].ToString();
                        string AccountNo = row["AccountNo"].ToString();
                        string Amount = row["Amount"].ToString();
                        string DorC = row["DorC"].ToString();
                        string TranType = row["TranType"].ToString();
                        string valueDate = row["valueDate"].ToString();
                        string Narration1 = row["Narration1"].ToString();
                        string dataEntryType = row["DataEntryType"].ToString();

                        if (!CurrencyCode.Equals("00"))
                        {
                            fcyHeader = "00" + ConfigurationManager.AppSettings["OriginatorID"] + valueDate + fcyHeaderAmount.PadLeft(15, '0') + DorC;
                        }


                        string line = "";
                        string lineDedit = "";
                        lineDedit += (CurrencyCode + delim);

                        if (CurrencyCode.Equals("00"))
                        {
                            line += (CurrencyCode + delim);
                            line += (AccountNo + delim);
                            line += (Amount + delim);
                            line += (DorC + delim);
                            line += (TranType + delim);
                            line += (valueDate + delim);
                            line += (Narration1 + delim);

                            //string lineDedit = "";
                            // lineDedit += (CurrencyCode + delim);
                            lineDedit += (ConfigurationManager.AppSettings["SuspenseAccNoAtOrgBank"] + delim);
                            lineDedit += (Amount + delim);
                            lineDedit += ("C" + delim);
                            lineDedit += ("512" + delim);
                            lineDedit += (valueDate + delim);
                            lineDedit += (Narration1 + delim);
                        }
                        else
                        {
                            if (dataEntryType.Equals(".csv"))
                            {
                                lineDedit += (CurrencyCode + ConfigurationManager.AppSettings["FCYSuspenceAccSTS"] + delim);
                            }
                            string Narrative1 = row["A"].ToString();
                            string Narrative2 = row["B"].ToString();
                            masterNo = row["MASTER"].ToString();
                            AccountNo = (CurrencyCode + AccountNo).ToString().Substring(0, 13);


                            //line = string.Empty;
                            line += (CurrencyCode + delim);
                            line += (AccountNo + delim);
                            line += (Amount + delim);
                            line += (DorC + delim);
                            line += (Narrative1 + delim);
                            line += (Narrative2 + delim);
                            line += (valueDate + delim);
                            line += (TranType + delim);
                            line += (masterNo + delim);


                            //lineDedit += (CurrencyCode + ConfigurationManager.AppSettings["FCYSuspenceAcc"] + delim);
                            lineDedit += (Amount + delim);
                            lineDedit += ("C" + delim);
                            lineDedit += (Narrative1 + delim);
                            lineDedit += (Narrative2 + delim);
                            lineDedit += (valueDate + delim);
                            lineDedit += ("512" + delim);
                            lineDedit += (masterNo + delim);


                        }



                        //if (result.Equals(string.Empty))
                        //{
                        //    result += line + "\n" + lineDedit;
                        //}

                        //else
                        //{
                        //    result += "\n" + line + "\n" + lineDedit;
                        //}

                        if (result.Equals(string.Empty) && !fcyHeader.Equals(string.Empty))
                        {
                            result += fcyHeader + "\n" + lineDedit + "\n" + line;
                        }
                        else if (result.Equals(string.Empty))
                        {
                            result += lineDedit + "\n" + line;
                        }
                        else
                        {
                            result += "\n" + lineDedit + "\n" + line;
                        }

                    }
                }
                else
                {
                    //string fcyHeaderAmount = dt.Compute("SUM(Amount)", "").ToString();
                    string fcyHeaderAmount = GetTotalAmount(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string CurrencyCode = row["CurrencyCode"].ToString();
                        string AccountNo = row["AccountNo"].ToString();
                        string Amount = row["Amount"].ToString();
                        string DorC = row["DorC"].ToString();
                        string TranType = row["TranType"].ToString();
                        string valueDate = row["valueDate"].ToString();
                        string Narration1 = row["Narration1"].ToString();
                        string dataEntryType = row["DataEntryType"].ToString();


                        if (!CurrencyCode.Equals("00"))
                        {
                            fcyHeader = "00" + ConfigurationManager.AppSettings["OriginatorID"] + valueDate + fcyHeaderAmount.PadLeft(15, '0') + DorC;
                        }

                        string lineDedit = "";
                        string line = "";
                        lineDedit += (CurrencyCode + delim);

                        if (CurrencyCode.Equals("00"))
                        {
                            //lineDedit += (CurrencyCode + delim);
                            lineDedit += (ConfigurationManager.AppSettings["SuspenseAccNoAtOrgBank"] + delim);
                            lineDedit += (Amount + delim);
                            lineDedit += ("D" + delim);
                            lineDedit += (ConfigurationManager.AppSettings["TransTypeDebit"] + delim);
                            lineDedit += (valueDate + delim);
                            lineDedit += (Narration1 + delim);

                            //string line = "";
                            line += (CurrencyCode + delim);
                            line += (AccountNo + delim);
                            line += (Amount + delim);
                            line += (DorC + delim);
                            line += (TranType + delim);
                            line += (valueDate + delim);
                            line += (Narration1 + delim);
                        }
                        else
                        {
                            if (dataEntryType.Equals(".csv"))
                            {
                                lineDedit += (CurrencyCode + ConfigurationManager.AppSettings["FCYSuspenceAccSTS"] + delim);
                            }
                            string Narrative1 = row["A"].ToString();
                            string Narrative2 = row["B"].ToString();
                            masterNo = row["MASTER"].ToString();
                            AccountNo = (CurrencyCode + AccountNo).ToString().Substring(0, 13);

                            //line = string.Empty;


                            // lineDedit += (CurrencyCode + ConfigurationManager.AppSettings["FCYSuspenceAcc"] + delim);
                            lineDedit += (Amount + delim);
                            lineDedit += ("D" + delim);
                            lineDedit += (Narrative1 + delim);
                            lineDedit += (Narrative2 + delim);
                            lineDedit += (valueDate + delim);
                            lineDedit += (ConfigurationManager.AppSettings["TransTypeDebit"] + delim);
                            lineDedit += (masterNo + delim);


                            line += (CurrencyCode + delim);
                            line += (AccountNo + delim);
                            line += (Amount + delim);
                            line += (DorC + delim);
                            line += (Narrative1 + delim);
                            line += (Narrative2 + delim);
                            line += (valueDate + delim);
                            line += (TranType + delim);
                            line += (masterNo + delim);


                        }

                        //if (result.Equals(string.Empty))
                        //{
                        //    result += lineDedit + "\n" + line;
                        //}

                        //else
                        //{
                        //    result += "\n" + lineDedit + "\n" + line;
                        //}

                        if (result.Equals(string.Empty) && !fcyHeader.Equals(string.Empty))
                        {
                            result += fcyHeader + "\n" + lineDedit + "\n" + line;
                        }
                        else if (result.Equals(string.Empty))
                        {
                            result += lineDedit + "\n" + line;
                        }
                        else
                        {
                            result += "\n" + lineDedit + "\n" + line;
                        }

                    }
                }
            }
            return result;
        }

        public string CreatFlatFileForTransactionSent(DataTable dt, int DepartmentID)
        {
            string delim = ConfigurationManager.AppSettings["Delim"];
            string fcyHeader = string.Empty;
            string result = string.Empty;
            string masterNo = string.Empty;
            string SuspenseAccNoAtOrgBank = ConfigurationManager.AppSettings["SuspenseAccNoAtOrgBank"];
            string DDISuspenseAccount = ConfigurationManager.AppSettings["DDISuspenseAccount"];

            //string fcyHeaderAmount = dt.Compute("SUM(Amount)", "").ToString();
            string fcyHeaderAmount = GetTotalAmount(dt);

            foreach (DataRow row in dt.Rows)
            {
                string CurrencyCode = row["CurrencyCode"].ToString();
                string AccountNo = string.Empty;
                string DataEntryType = row["DataEntryType"].ToString();
                if (DataEntryType.Equals(".csv"))
                {
                    AccountNo = SuspenseAccNoAtOrgBank;
                }
                else if (DataEntryType.Equals("DDI"))
                {
                    AccountNo = DDISuspenseAccount;
                }
                else
                {
                    AccountNo = row["AccountNo"].ToString();
                }
                //string AccountNo = row["AccountNo"].ToString();
                string Amount = row["Amount"].ToString();
                string DorC = row["DorC"].ToString();
                string TranType = row["TranType"].ToString();
                string valueDate = row["valueDate"].ToString();
                string Narration1 = row["Narration1"].ToString();


                if (!CurrencyCode.Equals("00"))
                {
                    fcyHeader = "00" + ConfigurationManager.AppSettings["OriginatorID"] + valueDate + fcyHeaderAmount.PadLeft(15, '0') + DorC;
                }

                string lineDedit = "";
                string line = "";
                lineDedit += (CurrencyCode + delim);

                if (CurrencyCode.Equals("00"))
                {
                    if (DepartmentID == 2)
                    {
                        lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBankPDC"] + delim);

                    }
                    else
                    {
                        lineDedit += (ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"] + delim);
                    }

                    lineDedit += (Amount + delim);
                    if (DorC.Equals("C"))
                    {
                        lineDedit += ("D" + delim);
                        lineDedit += ("612" + delim);
                    }
                    else
                    {
                        lineDedit += ("C" + delim);
                        lineDedit += ("512" + delim);
                    }
                    //lineDedit += (ConfigurationManager.AppSettings["TransTypeDebit"] + delim);
                    lineDedit += (valueDate + delim);
                    lineDedit += (Narration1 + delim);

                    line = string.Empty;
                    line += (CurrencyCode + delim);
                    line += (AccountNo + delim);
                    line += (Amount + delim);
                    line += (DorC + delim);
                    line += (TranType + delim);
                    line += (valueDate + delim);
                    line += (Narration1 + delim);
                }
                else
                {
                    //lineDedit += (GetSuspenseAccountBasedOnCurrencyCode(CurrencyCode) + delim)                    
                    string Narrative1 = row["A"].ToString();
                    string Narrative2 = row["B"].ToString();
                    masterNo = row["MASTER"].ToString();
                    //if (DataEntryType.Equals(".csv"))
                    //{
                    //    lineDedit += (CurrencyCode + ConfigurationManager.AppSettings["FCYSuspenceAccSTS"] + delim);
                    //}
                    //else
                    //{
                    lineDedit += (CurrencyCode + ConfigurationManager.AppSettings["FCYSuspenceAcc"] + delim);
                    //  }

                    AccountNo = (CurrencyCode + AccountNo).ToString().Substring(0, 13);
                    lineDedit += (Amount + delim);
                    if (DorC.Equals("C"))
                    {
                        lineDedit += ("D" + delim);
                        lineDedit += (Narrative1 + delim);
                        lineDedit += (Narrative2 + delim);
                        lineDedit += (valueDate + delim);
                        lineDedit += ("612" + delim);
                        lineDedit += (masterNo + delim);
                    }
                    else
                    {
                        lineDedit += ("C" + delim);
                        lineDedit += (Narrative1 + delim);
                        lineDedit += (Narrative2 + delim);
                        lineDedit += (valueDate + delim);
                        lineDedit += ("512" + delim);
                        lineDedit += (masterNo + delim);
                    }
                    //lineDedit += (ConfigurationManager.AppSettings["TransTypeDebit"] + delim);
                    //lineDedit += (valueDate + delim);
                    //lineDedit += (Narration1 + delim);

                    line = string.Empty;
                    line += (CurrencyCode + delim);
                    line += (AccountNo + delim);
                    line += (Amount + delim);
                    line += (DorC + delim);
                    line += (Narrative1 + delim);
                    line += (Narrative2 + delim);
                    line += (valueDate + delim);
                    line += (TranType + delim);
                    line += (masterNo + delim);
                }

                //if (result.Equals(string.Empty))
                //{
                //    result += lineDedit + "\n" + line;
                //}

                //else
                //{
                //    result += "\n" + lineDedit + "\n" + line;
                //}
                if (result.Equals(string.Empty) && !fcyHeader.Equals(string.Empty))
                {
                    result += fcyHeader + "\n" + lineDedit + "\n" + line;
                }
                else if (result.Equals(string.Empty))
                {
                    result += lineDedit + "\n" + line;
                }
                else
                {
                    result += "\n" + lineDedit + "\n" + line;
                }

            }

            return result;
        }

        public string CreatFlatFileForBB_Recon(DataTable dt)
        {
            string delim = ConfigurationManager.AppSettings["Delim"];
            string fcyHeader = string.Empty;
            string result = string.Empty;
            string masterNo = string.Empty;
            //string SuspenseAccNoAtOrgBank = ConfigurationManager.AppSettings["SuspenseAccNoAtOrgBank"];
            //string DDISuspenseAccount = ConfigurationManager.AppSettings["DDISuspenseAccount"];

            //string fcyHeaderAmount = dt.Compute("SUM(Amount)", "").ToString();


            string fcyHeaderAmount = GetTotalAmount_BB_FCY(dt);
            //string line = "";

            foreach (DataRow row in dt.Rows)
            {
                string CurrencyCode = row["CCY"].ToString();

                //if (row["TotalAmount"].ToString() != string.Empty)
                //{


                    string AccountNo = string.Empty;
                    //string DataEntryType = row["DataEntryType"].ToString();

                    AccountNo = row["ReconAcc"].ToString();

                    //string AccountNo = row["AccountNo"].ToString();
                    string Amount = row["Amount"].ToString();
                    string DorC = row["TrnCode"].ToString();
                    //string TranType = row["TranType"].ToString();
                    string valueDate = row["valuedate"].ToString();
                    string Narration1 = row["Narration"].ToString();

                string fcyDorC;
                if (DorC.Equals("Cr."))
                {
                    fcyDorC = "C";
                }
                else
                {
                    fcyDorC = "D";
                }

                    if (!CurrencyCode.Equals("00"))
                {
                    fcyHeader = "00" + ConfigurationManager.AppSettings["OriginatorID"] + valueDate + fcyHeaderAmount.PadLeft(15, '0') + fcyDorC;
                }

                //string line = "";
                string line = "";
                line += (CurrencyCode + delim);

                    if (CurrencyCode.Equals("00"))
                    {
                        line += AccountNo;
                        line += (Amount + delim);
                        if (DorC.Equals("Cr."))
                        {
                            line += ("D" + delim);
                            line += ("612" + delim);
                            //line += row["Debit"].ToString();
                        }
                        else
                        {
                            line += ("C" + delim);
                            line += ("512" + delim);
                            //line += row["Credit"].ToString();
                        }

                        line += (valueDate + delim);
                        line += (Narration1 + delim);

               
                    }
                    else
                    {

                        string Narrative1 = row["Narration"].ToString();
                        string Narrative2 = row["Narration1"].ToString();

                    masterNo = row["MASTER"].ToString();



                        AccountNo = (CurrencyCode + AccountNo).ToString().Substring(0, 13);
                        //line += (CurrencyCode + delim);
                        line += (AccountNo + delim);
                        line += (Amount + delim);
                        if (DorC.Equals("Cr."))
                        {
                            line += ("D" + delim);
                            line += (Narrative1 + delim);
                            line += (Narrative2 + delim);
                            line += (valueDate + delim);
                            line += ("612" + delim);
                            line += (masterNo + delim);
                        }
                        else
                        {
                            line += ("C" + delim);
                            line += (Narrative1 + delim);
                            line += (Narrative2 + delim);
                            line += (valueDate + delim);
                            line += ("512" + delim);
                            line += (masterNo + delim);
                        }

                        //line = string.Empty;
                        //line += (CurrencyCode + delim);
                        //line += (AccountNo + delim);
                        //line += (Amount + delim);
                        //line += (DorC + delim);
                        //line += (Narrative1 + delim);
                        //line += (Narrative2 + delim);
                        //line += (valueDate + delim);
                        //line += (TranType + delim);
                        //line += (masterNo + delim);
                }

                //}

                //if (CurrencyCode != string.Empty)
                //{
                    if (result.Equals(string.Empty) && !fcyHeader.Equals(string.Empty))
                    {
                        result += fcyHeader + "\n" + line;
                    }
                    else if (result.Equals(string.Empty))
                    {
                        result += line;
                    }
                    else
                    {
                        result += "\n" + line ;
                    }
                //}

            }


            return result;
        }

        public string CreatFlatFileForTransactionSentReverseForRejectedCredit(DataTable dt, int DepartmentID)
        {
            string delim = ConfigurationManager.AppSettings["Delim"];
            string fcyHeader = string.Empty;
            string masterNo = string.Empty;
            string result = string.Empty;

            string DebitAccountNo = ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"];
            string CreditAccountNo = ConfigurationManager.AppSettings["ComputerSuspenseAccount"];

            //string fcyHeaderAmount = dt.Compute("SUM(Amount)", "").ToString();
            string fcyHeaderAmount = GetTotalAmount(dt);

            //string SuspenseAccNoAtOrgBank = ConfigurationManager.AppSettings["SuspenseAccNoAtOrgBank"];
            //string DDISuspenseAccount = ConfigurationManager.AppSettings["DDISuspenseAccount"];

            foreach (DataRow row in dt.Rows)
            {
                string CurrencyCode = row["CurrencyCode"].ToString();
                string AccountNo = string.Empty;
                string DataEntryType = row["DataEntryType"].ToString();

                AccountNo = DebitAccountNo;

                //string AccountNo = row["AccountNo"].ToString();
                string Amount = row["Amount"].ToString();
                string DorC = row["DorC"].ToString();
                string TranType = row["TranType"].ToString();
                string valueDate = row["valueDate"].ToString();
                string Narration1 = row["Narration1"].ToString();

                if (!CurrencyCode.Equals("00"))
                {
                    fcyHeader = "00" + ConfigurationManager.AppSettings["OriginatorID"] + valueDate + fcyHeaderAmount.PadLeft(15, '0') + DorC;
                }


                string lineDedit = "";
                string line = "";
                lineDedit += (CurrencyCode + delim);

                if (CurrencyCode.Equals("00"))
                {
                    //lineDedit += (CurrencyCode + delim);
                    lineDedit += (CreditAccountNo + delim);

                    lineDedit += (Amount + delim);
                    if (DorC.Equals("C"))
                    {
                        lineDedit += ("D" + delim);
                        lineDedit += ("612" + delim);
                    }
                    else
                    {
                        lineDedit += ("C" + delim);
                        lineDedit += ("512" + delim);
                    }
                    //lineDedit += (ConfigurationManager.AppSettings["TransTypeDebit"] + delim);
                    lineDedit += (valueDate + delim);
                    lineDedit += (Narration1 + delim);

                    //string line = "";
                    line += (CurrencyCode + delim);
                    line += (AccountNo + delim);
                    line += (Amount + delim);
                    line += (DorC + delim);
                    line += (TranType + delim);
                    line += (valueDate + delim);
                    line += (Narration1 + delim);
                }
                else
                {

                    string Narrative1 = row["A"].ToString();
                    string Narrative2 = row["B"].ToString();
                    masterNo = row["MASTER"].ToString();
                    AccountNo = CurrencyCode + AccountNo;

                    lineDedit += (CurrencyCode + ConfigurationManager.AppSettings["FCYSuspenceAcc"] + delim);
                    lineDedit += (Amount + delim);
                    if (DorC.Equals("C"))
                    {
                        lineDedit += ("D" + delim);
                        lineDedit += (Narrative1 + delim);
                        lineDedit += (Narrative2 + delim);
                        lineDedit += (valueDate + delim);
                        lineDedit += ("612" + delim);
                        lineDedit += (masterNo + delim);
                    }
                    else
                    {
                        lineDedit += ("C" + delim);
                        lineDedit += (Narrative1 + delim);
                        lineDedit += (Narrative2 + delim);
                        lineDedit += (valueDate + delim);
                        lineDedit += ("512" + delim);
                        lineDedit += (masterNo + delim);
                    }
                    //lineDedit += (ConfigurationManager.AppSettings["TransTypeDebit"] + delim);
                    //lineDedit += (valueDate + delim);
                    //lineDedit += (Narration1 + delim);

                    line = string.Empty;
                    line += (CurrencyCode + delim);
                    line += (AccountNo + delim);
                    line += (Amount + delim);
                    line += (DorC + delim);
                    line += (Narrative1 + delim);
                    line += (Narrative2 + delim);
                    line += (valueDate + delim);
                    line += (TranType + delim);
                    line += (masterNo + delim);

                }

                //if (result.Equals(string.Empty))
                //{
                //    result += lineDedit + "\n" + line;
                //}

                //else
                //{
                //    result += "\n" + lineDedit + "\n" + line;
                //}

                if (result.Equals(string.Empty) && !fcyHeader.Equals(string.Empty))
                {
                    result += fcyHeader + "\n" + lineDedit + "\n" + line;
                }
                else if (result.Equals(string.Empty))
                {
                    result += lineDedit + "\n" + line;
                }
                else
                {
                    result += "\n" + lineDedit + "\n" + line;
                }
            }
            return result;
        }

        public string CreatFlatFileForTransactionSentReverseForRejectedDebit(DataTable dt, int DepartmentID)
        {
            string delim = ConfigurationManager.AppSettings["Delim"];
            string fcyHeader = string.Empty;
            string masterNo = string.Empty;
            string result = string.Empty;

            string CreditAccountNo = ConfigurationManager.AppSettings["BangladeshBankAccNoAtOrgBank"];
            string DebitAccountNo = ConfigurationManager.AppSettings["ComputerSuspenseAccount"];

            //string fcyHeaderAmount = dt.Compute("SUM(Amount)", "").ToString();
            string fcyHeaderAmount = GetTotalAmount(dt);

            //string SuspenseAccNoAtOrgBank = ConfigurationManager.AppSettings["SuspenseAccNoAtOrgBank"];
            //string DDISuspenseAccount = ConfigurationManager.AppSettings["DDISuspenseAccount"];

            foreach (DataRow row in dt.Rows)
            {
                string CurrencyCode = row["CurrencyCode"].ToString();
                string AccountNo = string.Empty;
                string DataEntryType = row["DataEntryType"].ToString();

                AccountNo = DebitAccountNo;

                //string AccountNo = row["AccountNo"].ToString();
                string Amount = row["Amount"].ToString();
                string DorC = row["DorC"].ToString();
                string TranType = row["TranType"].ToString();
                string valueDate = row["valueDate"].ToString();
                string Narration1 = row["Narration1"].ToString();

                if (!CurrencyCode.Equals("00"))
                {
                    fcyHeader = "00" + ConfigurationManager.AppSettings["OriginatorID"] + valueDate + fcyHeaderAmount.PadLeft(15, '0') + DorC;
                }


                string lineDedit = "";
                string line = "";

                lineDedit += (CurrencyCode + delim);

                if (CurrencyCode.Equals("00"))
                {
                    //lineDedit += (CurrencyCode + delim);
                    lineDedit += (CreditAccountNo + delim);

                    lineDedit += (Amount + delim);
                    if (DorC.Equals("C"))
                    {
                        lineDedit += ("D" + delim);
                        lineDedit += ("612" + delim);
                    }
                    else
                    {
                        lineDedit += ("C" + delim);
                        lineDedit += ("512" + delim);
                    }
                    //lineDedit += (ConfigurationManager.AppSettings["TransTypeDebit"] + delim);
                    lineDedit += (valueDate + delim);
                    lineDedit += (Narration1 + delim);

                    //string line = "";
                    line += (CurrencyCode + delim);
                    line += (AccountNo + delim);
                    line += (Amount + delim);
                    line += (DorC + delim);
                    line += (TranType + delim);
                    line += (valueDate + delim);
                    line += (Narration1 + delim);

                }
                else
                {
                    string Narrative1 = row["A"].ToString();
                    string Narrative2 = row["B"].ToString();
                    masterNo = row["MASTER"].ToString();
                    AccountNo = CurrencyCode + AccountNo;

                    lineDedit += (CurrencyCode + ConfigurationManager.AppSettings["FCYSuspenceAcc"] + delim);
                    lineDedit += (Amount + delim);
                    if (DorC.Equals("C"))
                    {
                        lineDedit += ("D" + delim);
                        lineDedit += (Narrative1 + delim);
                        lineDedit += (Narrative2 + delim);
                        lineDedit += (valueDate + delim);
                        lineDedit += ("612" + delim);
                        lineDedit += (masterNo + delim);
                    }
                    else
                    {
                        lineDedit += ("C" + delim);
                        lineDedit += (Narrative1 + delim);
                        lineDedit += (Narrative2 + delim);
                        lineDedit += (valueDate + delim);
                        lineDedit += ("512" + delim);
                        lineDedit += (masterNo + delim);
                    }
                    //lineDedit += (ConfigurationManager.AppSettings["TransTypeDebit"] + delim);
                    //lineDedit += (valueDate + delim);
                    //lineDedit += (Narration1 + delim);

                    line = string.Empty;
                    line += (CurrencyCode + delim);
                    line += (AccountNo + delim);
                    line += (Amount + delim);
                    line += (DorC + delim);
                    line += (Narrative1 + delim);
                    line += (Narrative2 + delim);
                    line += (valueDate + delim);
                    line += (TranType + delim);
                    line += (masterNo + delim);

                }

                //if (result.Equals(string.Empty))
                //{
                //    result += lineDedit + "\n" + line;
                //}

                //else
                //{
                //    result += "\n" + lineDedit + "\n" + line;
                //}

                if (result.Equals(string.Empty) && !fcyHeader.Equals(string.Empty))
                {
                    result += fcyHeader + "\n" + lineDedit + "\n" + line;
                }
                else if (result.Equals(string.Empty))
                {
                    result += lineDedit + "\n" + line;
                }
                else
                {
                    result += "\n" + lineDedit + "\n" + line;
                }
            }

            return result;
        }
        public string CreatFlatFileForTransactionSentToHUB(DataTable dt)
        {
            string delim = ConfigurationManager.AppSettings["Delim"];

            string result = string.Empty;

            //double totalDebitAmount = 0;

            foreach (DataRow row in dt.Rows)
            {
                string CurrencyCode = row["CurrencyCode"].ToString();
                string AccountNo = row["AccountNo"].ToString();
                string Amount = row["Amount"].ToString();
                string DorC = row["DorC"].ToString();
                string TranType = row["TranType"].ToString();
                string valueDate = row["valueDate"].ToString();
                string Narration1 = row["Narration1"].ToString();

                //totalDebitAmount += EFTN.Utility.ParseData.StringToDouble(Amount.Trim());

                //if (AccountNo.Trim().Length == 13)
                //{
                string line = "";
                line += (CurrencyCode + delim);
                line += (AccountNo + delim);
                line += (Amount + delim);
                line += (DorC + delim);
                line += (TranType + delim);
                line += (valueDate + delim);
                line += (Narration1 + delim);


                if (result.Equals(string.Empty))
                {
                    result += line;
                }

                else
                {
                    result += "\n" + line;
                }
                //}
            }
            return result;
        }

        public string CreatFlatFileForTransactionReceivedToHUB(DataTable dt)
        {
            //string delim = ConfigurationManager.AppSettings["Delim"];

            string result = string.Empty;

            //double totalDebitAmount = 0;

            foreach (DataRow row in dt.Rows)
            {
                string CurrencyCode = row["CurrencyCode"].ToString();
                string AccountNo = row["DFIAccountNo"].ToString();
                string Amount = row["Amount"].ToString();
                string DorC = row["DorC"].ToString();
                string TranType = row["TranType"].ToString();
                string valueDate = row["valueDate"].ToString();
                string Narration1 = row["Narration1"].ToString();

                //totalDebitAmount += EFTN.Utility.ParseData.StringToDouble(Amount.Trim());

                //if (AccountNo.Trim().Length == 13)
                //{
                string line = "";
                line += (CurrencyCode);
                line += (AccountNo);
                line += (Amount);
                line += (DorC);
                line += (TranType);
                line += (valueDate);
                line += (Narration1);


                if (result.Equals(string.Empty))
                {
                    result += line;
                }

                else
                {
                    result += "\n" + line;
                }
                //}
            }
            return result;
        }

        public string CreatFlatFileForReturnReceivedForPubali(DataTable dt)
        {
            string delim = ConfigurationManager.AppSettings["TextDelim"];

            string result = "CBSID" + delim + "BankName" + delim + "BranchName" + delim + "SECC" + delim
                            + "TraceNumber" + delim + "TransactionCode" + delim + "DFIAccountNo" + delim
                            + "BankRoutingNo" + delim + "Amount" + delim + "IdNumber" + delim + "ReceiverName"
                            + delim + "CompanyName" + delim + "RejectReasonCode" + delim + "EntryDesc" + delim;

            foreach (DataRow row in dt.Rows)
            {
                string CBSID = row["CBSID"].ToString();
                string BankName = row["BankName"].ToString();
                string BranchName = row["BranchName"].ToString();
                string SECC = row["SECC"].ToString();
                string TraceNumber = row["TraceNumber"].ToString();
                string TransactionCode = row["TransactionCode"].ToString();
                string DFIAccountNo = row["DFIAccountNo"].ToString();
                string BankRoutingNo = row["BankRoutingNo"].ToString();
                string Amount = row["Amount"].ToString();
                string IdNumber = row["IdNumber"].ToString();
                string ReceiverName = row["ReceiverName"].ToString();
                string CompanyName = row["CompanyName"].ToString();
                string RejectReasonCode = row["RejectReasonCode"].ToString();
                string EntryDesc = row["EntryDesc"].ToString();

                string line = "";
                line += (CBSID + delim);
                line += (BankName + delim);
                line += (BranchName + delim);
                line += (SECC + delim);
                line += (TraceNumber + delim);
                line += (TransactionCode + delim);
                line += (DFIAccountNo + delim);
                line += (BankRoutingNo + delim);
                line += (Amount + delim);
                line += (IdNumber + delim);
                line += (ReceiverName + delim);
                line += (CompanyName + delim);
                line += (RejectReasonCode + delim);
                line += (EntryDesc + delim);


                if (result.Equals(string.Empty))
                {
                    result += line + "\n";
                }

                else
                {
                    result += "\n" + line;
                }
            }
            return result;
        }

        public string CreateFlatFileForTransactionReceivedForCardsSCB(DataTable dt)
        {
            string delim = ConfigurationManager.AppSettings["Delim"];
            string CreditAccountNo = ConfigurationManager.AppSettings["CardsCreditAccount"];
            string result = string.Empty;
            long totalAmountInPaisa = 0;
            string valueDate = string.Empty;
            string blankSpace = string.Empty.PadLeft(12, ' ');
            int noOfTransactions = 0;


            string debitPrefix = "02";
            string debitCurrencyCode = "00";
            string debitAccountNo = ConfigurationManager.AppSettings["CardsDebitAccount"];
            //string debitAmount = totalAmountInPaisa.ToString();
            string debitDorC = "D";
            string debitNarration1 = "BEFTN Credit Card Bill";
            string debitExternalRefNo = "";
            //string debitvalueDate = valueDate;
            string debitTransactionCode = "612";
            string debitProductCode = "";
            string debitMasterNo = "";

            foreach (DataRow row in dt.Rows)
            {
                noOfTransactions++;
                string Prefix = row["Prefix"].ToString();
                string CurrencyCode = row["CurrencyCode"].ToString();
                string Amount = row["Amount"].ToString();
                string DorC = row["DorC"].ToString();
                string Narration1 = row["Narration1"].ToString();
                string ExternalRefNo = row["ExternalRefNo"].ToString();
                valueDate = row["valueDate"].ToString();
                string TransactionCode = row["TransactionCode"].ToString();
                string ProductCode = row["ProductCode"].ToString();
                string MasterNo = row["MasterNo"].ToString();

                totalAmountInPaisa += ParseData.StringToLong(Amount);

                string line = string.Empty;
                string lineDebit = string.Empty;

                line += Prefix;
                line += CurrencyCode;
                line += CreditAccountNo;
                line += Amount;
                line += DorC;
                line += Narration1;
                line += ExternalRefNo;
                line += blankSpace;
                line += valueDate;
                line += TransactionCode;
                line += ProductCode;
                line += MasterNo;

                if (result.Equals(string.Empty))
                {
                    result += line;
                }

                else
                {
                    result += "\n" + line;
                }

                lineDebit += debitPrefix.PadLeft(2, '0');
                lineDebit += debitCurrencyCode.PadLeft(2, '0');
                lineDebit += debitAccountNo.PadLeft(11, '0');
                lineDebit += Amount.PadLeft(13, '0');
                lineDebit += debitDorC;
                lineDebit += Narration1.PadRight(28, ' ');
                lineDebit += debitExternalRefNo.PadLeft(16, ' ');
                lineDebit += blankSpace;
                lineDebit += valueDate;
                lineDebit += debitTransactionCode;
                lineDebit += debitProductCode.PadLeft(3, ' ');
                lineDebit += debitMasterNo.PadLeft(7, ' ');

                result += "\n" + lineDebit;

            }

            string footerPrefix = "01";
            string footerOrginatorID = ConfigurationManager.AppSettings["CardsOriginatorID"].PadLeft(4, ' ');
            string footerBatchNo = "001";
            string footerBatchEntryDate = System.DateTime.Now.ToString("yyMMdd");
            //string footerBatchTotal = debitAmount.PadLeft(15, '0');
            string footerBatchTotal = totalAmountInPaisa.ToString().PadLeft(15, '0');
            string footerDorC = "D";
            string footerNoOfTransactions = noOfTransactions.ToString().PadLeft(5, '0');

            string lineFooter = string.Empty;

            lineFooter += footerPrefix;
            lineFooter += footerOrginatorID;
            lineFooter += footerBatchNo;
            lineFooter += footerBatchEntryDate;
            lineFooter += footerBatchTotal;
            lineFooter += footerDorC;
            lineFooter += footerNoOfTransactions;

            result += "\n" + lineFooter;

            return result;
        }

        public string CreateFlatFileForReturnSentForCardsSCB(DataTable dt)
        {
            string CreditAccountNo = ConfigurationManager.AppSettings["CardsCreditAccount"];
            string debitAccountNo = ConfigurationManager.AppSettings["CardsDebitAccount"];
            string result = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                string CurrencyCode = row["CurrencyCode"].ToString();
                string Amount = row["Amount"].ToString();
                string valueDate = row["valueDate"].ToString();
                string Narration1 = row["Narration1"].ToString();
                string Narration2 = row["Narration2"].ToString();
                string Narration3 = row["Narration3"].ToString();


                string line = string.Empty;
                string lineDebit = string.Empty;

                line += CurrencyCode;
                line += debitAccountNo;
                line += Amount;
                line += "C";
                line += "512";
                line += valueDate;
                line += Narration1.PadRight(35, ' ');
                line += Narration2.PadRight(35, ' ');
                line += Narration3.PadRight(35, ' ');

                if (result.Equals(string.Empty))
                {
                    result += line;
                }

                else
                {
                    result += "\n" + line;
                }

                lineDebit += CurrencyCode;
                lineDebit += CreditAccountNo;
                lineDebit += Amount;
                lineDebit += "D";
                lineDebit += "612";
                lineDebit += valueDate;
                lineDebit += Narration1.PadRight(35, ' ');
                lineDebit += Narration2.PadRight(35, ' ');
                lineDebit += Narration3.PadRight(35, ' ');

                result += "\n" + lineDebit;
            }
            return result;
        }

    }
}