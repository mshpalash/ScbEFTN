using System;
using System.Xml;
using System.Data;
using EFTN.component;

namespace EFTN.BLL
{
    public class FCUBSAccountStatusManager
    {
        public bool IsValidAcc;

        public string GetFCUBSAccountStatus(string AccountNo)
        {
            RTServiceAccountManager rtServiceAccountManager = new RTServiceAccountManager();

            string result = rtServiceAccountManager.GetCBSData(AccountNo);
            string AccountStatus = "CBS Account Details : ";
            string TranType = "CR";
            IsValidAcc = true;

            if (!result.Equals("Failed"))
            {
                string ECODE = string.Empty;
                string EDESC = string.Empty;
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(result);

                try
                {
                    ECODE = xdoc.GetElementsByTagName("ECODE").Item(0).InnerText;
                    EDESC = xdoc.GetElementsByTagName("EDESC").Item(0).InnerText;
                    AccountStatus += ECODE + ": " + EDESC;
                    IsValidAcc = false;
                }
                catch
                {
                }

                if (ECODE.Equals(string.Empty))
                {

                    RTServiceDB rtServiceDb = new RTServiceDB();
                    DataTable dtAccountConfigVar = rtServiceDb.GetFCUBS_Acc_Service_Conf(TranType);


                    try
                    {
                        AccountStatus += xdoc.GetElementsByTagName("ADESC").Item(0).InnerText + ", "
                                        + xdoc.GetElementsByTagName("ACCLS").Item(0).InnerText + " ";

                        string fieldStatusName = string.Empty;
                        string fieldStatusValue = string.Empty;
                        string fieldStatusInXML = string.Empty;

                        for (int configVarCount = 0; configVarCount < dtAccountConfigVar.Rows.Count; configVarCount++)
                        {
                            fieldStatusName = dtAccountConfigVar.Rows[configVarCount]["FieldStatusName"].ToString();
                            fieldStatusValue = dtAccountConfigVar.Rows[configVarCount]["FieldStatusValue"].ToString();
                            fieldStatusInXML = xdoc.GetElementsByTagName(fieldStatusName).Item(0).InnerText;
                            if (!fieldStatusValue.Equals(fieldStatusInXML))
                            {
                                if (!AccountStatus.Equals(string.Empty))
                                {
                                    AccountStatus += ", ";
                                }
                                AccountStatus += fieldStatusName + " " + fieldStatusInXML;
                                IsValidAcc = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        IsValidAcc = false;
                        AccountStatus = "Account not found";
                    }
                }
            }
            else
            {
                IsValidAcc = false;
                AccountStatus = "CBS Connection Error";
            }
            return AccountStatus;
        }
    }
}