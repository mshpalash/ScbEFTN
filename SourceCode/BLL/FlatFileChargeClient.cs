using System;
using System.Data;
using System.Configuration;
using System.IO;


namespace EFTN.BLL
{
    public class FlatFileChargeClient
    {
        public string CreatFlatFileForTransactionSentCharge(DataTable dt, int DepartmentID)
        {
            string delim = ConfigurationManager.AppSettings["Delim"];

            string result = string.Empty;


            if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("215"))
  
            {
                foreach (DataRow row in dt.Rows)
                {
                    string AccountNo = row["AccountNo"].ToString();
                    string ChargeAmount = row["ChargeAmount"].ToString();
                    string VATAmount = row["VATAmount"].ToString();
                    string DorC = row["DorC"].ToString();
                    string Narration1 = row["Narration1"].ToString();
                    string Narration2 = row["Narration2"].ToString();
                    string valueDate = row["valueDate"].ToString();
                    string Currency = row["Currency"].ToString();
                    string ChargeCode = row["ChargeCode"].ToString();
                    string ChargeCode2 = row["ChargeCode2"].ToString();


                    string line1 = "";
                    string line2 = "";

                    line1 += AccountNo;
                    line1 += ChargeAmount;
                    line1 += Narration1;
                    line1 += DorC;
                    line1 += valueDate;
                    line1 += Currency;
                    line1 += ChargeCode;

                    line2 += AccountNo;
                    line2 += VATAmount;
                    line2 += Narration2;
                    line2 += DorC;
                    line2 += valueDate;
                    line2 += Currency;
                    line2 += ChargeCode2;

                    if (result.Equals(string.Empty))
                    {
                        result += line1 + "\n" + line2;
                    }

                    else
                    {
                        result += "\n" + line1 + "\n" + line2;
                    }
                }
            }

                //if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("115"))
            else if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("225"))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string AccountNo = row["AccountNo"].ToString();
                    string Currency = row["Currency"].ToString();
                    string SolID = row["SolID"].ToString();
                    string DorC = row["DorC"].ToString();
                    string ChargeAmount = row["ChargeAmount"].ToString();
                    string VATAmount = row["VATAmount"].ToString();
                    string Narration = row["Narration"].ToString();
                    string TransactionCode = row["TransactionCode"].ToString();
                    string refSpace = row["ref"].ToString();
                    string ChargeDate = row["ChargeDate"].ToString();
                   // string ChargeCode2 = row["ChargeCode2"].ToString();


                    string line1 = "";
                    

                    line1 += AccountNo;
                    line1 += Currency;
                    line1 += SolID;
                    line1 += DorC;
                    line1 += ChargeAmount;
                    line1 += VATAmount;


                    line1 += Narration;
                    line1 += TransactionCode;
                    line1 += refSpace;
                    line1 += ChargeDate;

                    
                    if (result.Equals(string.Empty))
                    {
                        result += line1;
                    }

                    else
                    {
                        result += "\n" + line1 ;
                    }
                }
            }
            
            return result;
        }

        public string CreatFlatFileForTransactionSentChargeToHUB(DataTable dt, DataTable dtCharge)
        {
            string delim = ConfigurationManager.AppSettings["Delim"];

            string result = string.Empty;


                foreach (DataRow row in dt.Rows)
                {
                    string CurrencyCode = row["CurrencyCode"].ToString();
                    string AccountNo = row["AccountNo"].ToString();
                    string SpecialAccount = row["SpecialAccount"].ToString();
                    string DorC = row["DorC"].ToString();
                    string TranType = row["TranType"].ToString();
                    string valueDate = row["valueDate"].ToString();
                    string Narration1 = row["Narration1"].ToString();
                    string BBCharge = row["BankCharge"].ToString();
                    string BBChargeVAT = row["BankChargeVAT"].ToString();
                    string TotalCharge = row["TotalCharge"].ToString();
                    string HSBCBankCommissionContribution = row["HSBCBankCommissionContribution"].ToString();
                    string HSBCBankVATContribution = row["HSBCBankVATContribution"].ToString();
                    string HSBCBankComContACNo = row["HSBCBankComContACNo"].ToString();
                    string HSBCBankVATContACNo = row["HSBCBankVATContACNo"].ToString();


                    string line1 = string.Empty;
                    string line2 = string.Empty;
                    string line3 = string.Empty;

                    string finalLine = string.Empty;

                    if (SpecialAccount.Equals(string.Empty))
                    {
                        line1 += CurrencyCode;
                        line1 += AccountNo;
                        line1 += TotalCharge;
                        line1 += DorC;
                        line1 += TranType;
                        line1 += valueDate;
                        line1 += Narration1;

                        finalLine = line1;
                    }
                    else
                    {
                        line1 += CurrencyCode;
                        line1 += AccountNo;
                        line1 += TotalCharge;
                        line1 += DorC;
                        line1 += TranType;
                        line1 += valueDate;
                        line1 += Narration1;

                        line2 += CurrencyCode;
                        line2 += HSBCBankComContACNo;
                        line2 += HSBCBankCommissionContribution;
                        line2 += DorC;
                        line2 += TranType;
                        line2 += valueDate;
                        line2 += Narration1;

                        line3 += CurrencyCode;
                        line3 += HSBCBankVATContACNo;
                        line3 += HSBCBankVATContribution;
                        line3 += DorC;
                        line3 += TranType;
                        line3 += valueDate;
                        line3 += Narration1;

                        finalLine = line1 + "\n" + line2 + "\n" + line3;
                    }

                    if (result.Equals(string.Empty))
                    {
                        result += finalLine;
                    }

                    else
                    {
                        result += "\n" + finalLine;
                    }

                    //if (result.Equals(string.Empty))
                    //{
                    //    result += line1 + "\n" + line2 + "\n" + line3;
                    //}

                    //else
                    //{
                    //    result += "\n" + line1 + "\n" + line2 + "\n" + line3;
                    //}
                }

                foreach (DataRow row in dtCharge.Rows)
                {
                    string CurrencyCode = row["CurrencyCode"].ToString();
                    string AccountNo = row["AccountNo"].ToString();
                    string SpecialAccount = row["SpecialAccount"].ToString();
                    string DorC = row["DorC"].ToString();
                    string TranType = row["TranType"].ToString();
                    string valueDate = row["valueDate"].ToString();
                    string Narration1 = row["Narration1"].ToString();
                    string BBCharge = row["BankCharge"].ToString();
                    string BBChargeVAT = row["BankChargeVAT"].ToString();
                    string TotalCharge = row["TotalCharge"].ToString();
                    string HSBCBankCommissionContribution = row["HSBCBankCommissionContribution"].ToString();
                    string HSBCBankVATContribution = row["HSBCBankVATContribution"].ToString();
                    string HSBCBankComContACNo = row["HSBCBankComContACNo"].ToString();
                    string HSBCBankVATContACNo = row["HSBCBankVATContACNo"].ToString();


                    string line1 = "";
                    string line2 = "";
                    string line3 = "";

                    if (SpecialAccount.Equals(string.Empty))
                    {
                        line1 += CurrencyCode;
                        line1 += AccountNo;
                        line1 += TotalCharge;
                        line1 += DorC;
                        line1 += TranType;
                        line1 += valueDate;
                        line1 += Narration1;
                    }
                    else
                    {
                        line1 += CurrencyCode;
                        line1 += AccountNo;
                        line1 += TotalCharge;
                        line1 += DorC;
                        line1 += TranType;
                        line1 += valueDate;
                        line1 += Narration1;

                        line2 += CurrencyCode;
                        line2 += HSBCBankComContACNo;
                        line2 += HSBCBankCommissionContribution;
                        line2 += DorC;
                        line2 += TranType;
                        line2 += valueDate;
                        line2 += Narration1;

                        line3 += CurrencyCode;
                        line3 += HSBCBankVATContACNo;
                        line3 += HSBCBankVATContribution;
                        line3 += DorC;
                        line3 += TranType;
                        line3 += valueDate;
                        line3 += Narration1;
                    }

                    if (result.Equals(string.Empty))
                    {
                        result += line1 + "\n" + line2 + "\n" + line3;
                    }

                    else
                    {
                        result += "\n" + line1 + "\n" + line2 + "\n" + line3;
                    }
                }


            return result;
        }
    }
}
