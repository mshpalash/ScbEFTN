using System;
using EFTN.component;
using System.Data;
using System.Xml;
using EFTN.Utility;
using System.Configuration;
using System.Net;
using System.IO;

namespace EFTN.BLL
{
    public class FCUBSRTManager
    {
        //public string SendNRBRTEDRXML(Guid EDRID, string bankCode)
        //{
        //    RTServiceTxnXML nRBRTTxnXML = new RTServiceTxnXML();
        //    string eDRSentXMLString = nRBRTTxnXML.CreateRTServiceSentEDRXML(EDRID, bankCode);
        //    return "";
        //}

        public int SendNRBRTReceivedReturnXML(Guid ReturnID, string bankCode, int ApprovedBy)
        {
            RTServiceTxnXML nRBRTTxnXML = new RTServiceTxnXML();
            string remarks = string.Empty;
            string errorMsg = string.Empty;
            string OID = string.Empty;
            string NRBRT_XREF = string.Empty;
            string DepartmentID = string.Empty;
            string IdNumber = string.Empty;
            string RetREMARKS = string.Empty;
            string RetREMARKS1 = string.Empty;
            string ReturnCode = string.Empty;
            string RejectReason = string.Empty;

            int failedTXN = 0;

            if (IsValidCBSAccountForInwardReturnTxn(ReturnID, bankCode, ref remarks, ref errorMsg, ref OID))
            {
                string returnSentXMLString = nRBRTTxnXML.CreateRTServiceReceivedReturnXML(ReturnID, bankCode, OID, 
                                                                ref NRBRT_XREF, ref DepartmentID, ref IdNumber,
                                                                ref RetREMARKS, ref RetREMARKS1, ref ReturnCode,
                                                                ref RejectReason);

                string queryResult = CBSAccountDebit(returnSentXMLString);
                failedTXN = SaveRTServiceResponseForInwardReturn(queryResult, ReturnID, ApprovedBy, NRBRT_XREF, DepartmentID, 
                                                                IdNumber,
                                                                RetREMARKS, RetREMARKS1, ReturnCode,
                                                                RejectReason, bankCode);
            }
            else
            {
                failedTXN = 1;

                if (remarks.Equals(string.Empty))
                {
                    //TO DO 
                    //SEND DATA TO Maker as rejected
                    //send error message from "errorMsg" variable
                    RTServiceDB rtServiceDb = new RTServiceDB();
                    rtServiceDb.InsertFCUBS_Err_For_ReceivedReturn(ReturnID, errorMsg);
                }
                else
                {
                    //DO NOTHING
                }
            }

            return failedTXN;
        }

        public void SendNRBRTReceivedTransactionXML(Guid EDRID, string bankCode, int ApprovedBy, ref int failedCounter)
        {

            string remarks = string.Empty;
            string errorMsg = string.Empty;

            if (bankCode.Equals(OriginalBankCode.NRB) || bankCode.Equals(OriginalBankCode.UCBL))
            {
                if (IsValidCBSAccountForTxn(EDRID, bankCode, ref remarks, ref errorMsg))
                {
                    RTServiceTxnXML nRBRTTxnXML = new RTServiceTxnXML();
                    string RT_XREF = string.Empty;
                    string eDRSentXMLString = nRBRTTxnXML.CreateRTServiceReceivedTransactionXML(EDRID, bankCode, ref RT_XREF);
                    string queryResult = CBSAccountDebit(eDRSentXMLString);
                    SaveRTServiceResponse(queryResult, EDRID, ApprovedBy, RT_XREF, ref failedCounter);

                }
                else
                {
                    if (remarks.Equals(string.Empty))
                    {
                        //TO DO
                        failedCounter++;
                        RTServiceDB rtServiceDb = new RTServiceDB();
                        rtServiceDb.InsertFCUBS_Err_For_ReceivedEDR(EDRID, errorMsg);
                        //SEND DATA TO ERROR TABLE
                        //send error message from "errorMsg" variable
                    }
                    else
                    {
                        //DO NOTHING
                    }
                }
            }
        }

        private bool IsValidCBSAccountForTxn(Guid EDRID, string bankCode, ref string remarks, ref string errorMsg)
        {
            RTServiceAccountManager rtServiceAccountManager = new RTServiceAccountManager();
            RTServiceDB rtServiceDb = new RTServiceDB();
            DataTable dtEDR = new DataTable();
            dtEDR = rtServiceDb.GetReceivedTransactionApprovedForNrbRTService(EDRID);

            string ACC_SERVICE_CHK_BALANCE = ConfigurationManager.AppSettings["ACC_SERVICE_CHK_BALANCE"].ToString();
            string amountFieldName = string.Empty;
            string CreditDebit = string.Empty;

            if (ACC_SERVICE_CHK_BALANCE.Equals("1"))
            {
                amountFieldName = ConfigurationManager.AppSettings["ACC_SERVICE_FIELDNAME_BALANCE"].ToString();
            }

            string AccountNo = string.Empty;
            string Amount = string.Empty;

            AccountNo = dtEDR.Rows[0]["DFIAccountNo"].ToString();
            Amount = dtEDR.Rows[0]["Amount"].ToString();
            CreditDebit = dtEDR.Rows[0]["CreditDebit"].ToString();
            if (CreditDebit.Equals(TransactionCodes.EFTTransactionTypeCredit))
            {
                if (IsValidAccountToProceedDebitTXN(AccountNo, bankCode, Amount, amountFieldName, ref remarks, ref errorMsg))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (IsValidAccountToProceedCreditTXN(AccountNo, bankCode, Amount, amountFieldName, ref remarks, ref errorMsg))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Check the account status to process the transaction for the outward Credit
        /// </summary>
        /// <param name="AccountNo"></param>
        /// <param name="accountStatus"></param>
        /// <param name="bankCode"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private bool IsValidAccountToProceedCreditTXN(string AccountNo, string bankCode, string amount, string amountFieldName, ref string remarks, ref string errorMsg)
        {
            RTServiceAccountManager rtServiceAccountManager = new RTServiceAccountManager();

            string result = rtServiceAccountManager.GetCBSData(AccountNo);

            if (!result.Equals("Failed"))
            {
                                string ECODE = string.Empty;
                string EDESC = string.Empty;
                try
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(result);

                    ECODE = xdoc.GetElementsByTagName("ECODE").Item(0).InnerText;
                    EDESC = xdoc.GetElementsByTagName("EDESC").Item(0).InnerText;
                }
                catch
                {
                }

                if (ECODE.Equals(string.Empty))
                {

                    if (bankCode.Equals(OriginalBankCode.NRB))
                    {
                        return IsValidToProceedTXNForNRB(result, amount, "DR", amountFieldName, ref errorMsg);
                    }
                    else if (bankCode.Equals(OriginalBankCode.UCBL))
                    {
                        return IsValidToProceedTXNForNRB(result, amount, "DR", amountFieldName, ref errorMsg);
                    }
                }
                else
                {
                    errorMsg = ECODE + " - " + EDESC;
                }
            }
            else
            {
                remarks = "Unable to connect";
            }
            return false;
        }


        /// <summary>
        /// Check the account status to process the transaction for the outward Debit
        /// </summary>
        /// <param name="AccountNo"></param>
        /// <param name="accountStatus"></param>
        /// <param name="bankCode"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private bool IsValidAccountToProceedDebitTXN(string AccountNo, string bankCode, string amount, string amountFieldName, ref string remarks, ref string errorMsg)
        {
            RTServiceAccountManager rtServiceAccountManager = new RTServiceAccountManager();

            string result = rtServiceAccountManager.GetCBSData(AccountNo);

            if (!result.Equals("Failed"))
            {

                string ECODE = string.Empty;
                string EDESC = string.Empty;
                try
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(result);

                    ECODE = xdoc.GetElementsByTagName("ECODE").Item(0).InnerText;
                    EDESC = xdoc.GetElementsByTagName("EDESC").Item(0).InnerText;
                }
                catch
                {
                }

                if (ECODE.Equals(string.Empty))
                {
                    if (bankCode.Equals(OriginalBankCode.NRB))
                    {
                        return IsValidToProceedTXNForNRB(result, amount, "CR", amountFieldName, ref errorMsg);
                    }
                    else if (bankCode.Equals(OriginalBankCode.UCBL))
                    {
                        return IsValidToProceedTXNForNRB(result, amount, "CR", amountFieldName, ref errorMsg);
                    }
                }
                else
                {
                    errorMsg = ECODE + " - " + EDESC;
                }
            }
            else
            {
                remarks = "Unable to connect";
            }
            return false;
        }


        /// <summary>
        /// Check the account status for the outward credit transaction and origin bank debit for the NRB
        /// </summary>
        /// <param name="myXml"></param>
        /// <param name="accountStatus"></param>
        /// <param name="strAmount"></param>
        /// <returns></returns>
        private bool IsValidToProceedTXNForNRB(string myXml, string strAmount, string TranType, string amountFieldName, ref string errorMsg)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(myXml);

            RTServiceDB rtServiceDb = new RTServiceDB();
            DataTable dtAccountConfigVar = rtServiceDb.GetFCUBS_Acc_Service_Conf(TranType);

            double Amount = ParseData.StringToDouble(strAmount);

            try
            {
                string fieldStatusName = string.Empty;
                string fieldStatusValue = string.Empty;
                string fieldStatusInXML=string.Empty;
                for (int configVarCount = 0; configVarCount < dtAccountConfigVar.Rows.Count; configVarCount++)
                {
                    fieldStatusName = dtAccountConfigVar.Rows[configVarCount]["FieldStatusName"].ToString();
                    fieldStatusValue = dtAccountConfigVar.Rows[configVarCount]["FieldStatusValue"].ToString();
                    fieldStatusInXML = xdoc.GetElementsByTagName(fieldStatusName).Item(0).InnerText;
                    if (!fieldStatusValue.Equals(fieldStatusInXML))
                    {
                        errorMsg = fieldStatusName + " " + fieldStatusInXML;
                        return false;
                    }
                }
                if (TranType.Equals("DR") && !amountFieldName.Equals(string.Empty))
                {
                    double ACY_AVL_BAL = 0;
                    ACY_AVL_BAL = ParseData.StringToDouble(xdoc.GetElementsByTagName(amountFieldName).Item(0).InnerText);

                    if (ACY_AVL_BAL >= Amount)
                    {
                        return true;
                    }
                    else
                    {
                        errorMsg = ConfigurationManager.AppSettings["ACC_SERVICE_CHK_BALANCE_ErrMsg"].ToString();
                        return false;
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                errorMsg = "Unable to connect to server or Invalid account number.";
                return false;
            }
        }


        public string CBSAccountDebit(string queryString)
        {
            string result = "";
            WebRequest req = null;
            WebResponse rsp = null;
            try
            {
                string uri = ConfigurationManager.AppSettings["CBSDebitURL"];
                req = (HttpWebRequest)WebRequest.Create(uri);
                req.Method = "POST";
                req.ContentType = "text/xml";
                req.Credentials = CredentialCache.DefaultNetworkCredentials;
                StreamWriter writer = new StreamWriter(req.GetRequestStream());
                writer.WriteLine(queryString);
                writer.Close();
                rsp = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(rsp.GetResponseStream());
                result = sr.ReadToEnd();
                sr.Close();

            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// Process the transaction after getting the response from RT Service
        /// </summary>
        /// <param name="queryResult"></param>
        /// <param name="EDRID"></param>
        private void SaveRTServiceResponse(string queryResult, Guid EDRID, int ApprovedBy, string RT_REF, ref int failedCounter)
        {
            string Status = "";
            string XREF = "";
            string FCCREF = "";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(queryResult);
            try
            {
                XREF = doc.GetElementsByTagName("XREF").Item(0).InnerText;
                Status = doc.GetElementsByTagName("EDESC").Item(0).InnerText;
            }
            catch
            {

            }

            if (XREF == string.Empty)
            {
                failedCounter++;
            }
            else
            {
                if (Status == string.Empty)
                {
                    //Transaction successfully completed
                    //TO DO
                    //TAZIM OR MYSELF discussing with TAZIM
                    //Forward transaction to checker authorizer by EDRID and mark the flexcube transaction processed status
                    //that will prevent to resend the transaction to the CBS
                    FCCREF = doc.GetElementsByTagName("FCCREF").Item(0).InnerText;

                    EFTN.component.ApprovedInwardTransactionDB dbAIT = new EFTN.component.ApprovedInwardTransactionDB();
                    dbAIT.UpdateReceivedEDRStatusByChecker_FCUBS(EDRID, ApprovedBy, RT_REF, FCCREF);

                    Status = doc.GetElementsByTagName("WDESC").Item(0).InnerText;
                }
                else
                {
                    //TO DO
                    //TAZIM OR MYSELF discussing with TAZIM
                    //DELETE TRANSACTION FROM THE MAIN TABLE AND TRANSFER TO ERROR TABLE by EDRID
                    failedCounter++;
                    RTServiceDB rtServiceDb = new RTServiceDB();
                    rtServiceDb.InsertFCUBS_Err_For_ReceivedEDR(EDRID, Status);

                }
            }
        }

        private int SaveRTServiceResponseForOutward(string queryResult, Guid EDRID, int ApprovedBy, string NRBRT_XREF)
        {
            string Status = string.Empty;
            string XREF = string.Empty;
            int failedTXN = 0;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(queryResult);
            try
            {
                XREF = doc.GetElementsByTagName("XREF").Item(0).InnerText;
                Status = doc.GetElementsByTagName("EDESC").Item(0).InnerText;
            }
            catch
            {

            }

            if (XREF == string.Empty)
            {
                failedTXN = 1;
            }
            else
            {
                EFTN.component.SentEDRDB sentEdrDB = new EFTN.component.SentEDRDB();
                if (Status == string.Empty)
                {
                    //Transaction successfully completed
                    //TO DO
                    string FCCREF = doc.GetElementsByTagName("FCCREF").Item(0).InnerText;
                    sentEdrDB.Update_EDRSent_Status_FCUBS(EFTN.Utility.TransactionStatus.TransSent_Approved_By_Checker, EDRID, ApprovedBy, FCCREF);
                    
                    Status = doc.GetElementsByTagName("WDESC").Item(0).InnerText;

                    RTServiceDB rtserviceDB = new RTServiceDB();
                    rtserviceDB.UpdateTransactionSentReferenceForFCUBS(EDRID, NRBRT_XREF);
                }
                else
                {
                    //TO DO
                    EFTN.component.RejectReasonByCheckerDB rejectedReasondb = new EFTN.component.RejectReasonByCheckerDB();
                    rejectedReasondb.Insert_RejectReason_ByChecker(EDRID, ("Ref: " + NRBRT_XREF + ": " + Status),
                            (int)EFTN.Utility.ItemType.TransactionSent);

                    sentEdrDB.Update_EDRSent_Status(EFTN.Utility.TransactionStatus.TransSent_Rejected_By_Checker, EDRID, ApprovedBy);
                    failedTXN = 1;
                }
            }
            return failedTXN;
        }

        public int SendRTServiceEDRXMLForOutward(Guid EDRID, string bankCode, int ApprovedBy)
        {
            string remarks = string.Empty;
            string errorMsg = string.Empty;
            string CreditDebit = string.Empty;
            string NRBRT_XREF = string.Empty;
            int failedTXN = 0;

            if (IsValidCBSAccountForOutwardTxn(EDRID, bankCode, ref remarks, ref errorMsg, ref CreditDebit))
            {
                if (CreditDebit.Equals(TransactionCodes.EFTTransactionTypeCredit))
                {
                    RTServiceTxnXML FCUBSRTTxnXML = new RTServiceTxnXML();
                    string eDRSentXMLString = FCUBSRTTxnXML.CreateRTServiceSentEDRXML(EDRID, bankCode, ref NRBRT_XREF);
                    string queryResult = CBSAccountDebit(eDRSentXMLString);
                    failedTXN = SaveRTServiceResponseForOutward(queryResult, EDRID, ApprovedBy, NRBRT_XREF);
                }
                else
                {
                    EFTN.component.SentEDRDB sentEdrDB = new EFTN.component.SentEDRDB();
                    sentEdrDB.Update_EDRSent_Status(EFTN.Utility.TransactionStatus.TransSent_Approved_By_Checker, EDRID, ApprovedBy);
                }
            }
            else
            {
                failedTXN = 1;
                if (remarks.Equals(string.Empty))
                {
                    //TO DO 
                    //SEND DATA TO Maker as rejected
                    //send error message from "errorMsg" variable
                    EFTN.component.RejectReasonByCheckerDB rejectedReasondb = new EFTN.component.RejectReasonByCheckerDB();
                    rejectedReasondb.Insert_RejectReason_ByChecker(EDRID, errorMsg,
                            (int)EFTN.Utility.ItemType.TransactionSent);
                    EFTN.component.SentEDRDB sentEdrDB = new EFTN.component.SentEDRDB();
                    sentEdrDB.Update_EDRSent_Status(EFTN.Utility.TransactionStatus.TransSent_Rejected_By_Checker, EDRID, ApprovedBy);
                }
                else
                {
                    //DO NOTHING
                }
            }
            return failedTXN;
        }

        private bool IsValidCBSAccountForOutwardTxn(Guid EDRID, string bankCode, ref string remarks, ref string errorMsg, ref string CreditDebit)
        {
            RTServiceAccountManager rtServiceAccountManager = new RTServiceAccountManager();
            RTServiceDB rtServiceDb = new RTServiceDB();
            DataTable dtEDR = new DataTable();
            dtEDR = rtServiceDb.GetTransactionSentApprovedForCheckerFCUBSRTService(EDRID);

            string ACC_SERVICE_CHK_BALANCE = ConfigurationManager.AppSettings["ACC_SERVICE_CHK_BALANCE"].ToString();
            string amountFieldName = string.Empty;

            if (ACC_SERVICE_CHK_BALANCE.Equals("1"))
            {
                amountFieldName = ConfigurationManager.AppSettings["ACC_SERVICE_FIELDNAME_BALANCE"].ToString();
            }

            string AccountNo = string.Empty;
            string Amount = string.Empty;

            AccountNo = dtEDR.Rows[0]["AccountNo"].ToString();
            Amount = dtEDR.Rows[0]["Amount"].ToString();
            CreditDebit = dtEDR.Rows[0]["CreditDebit"].ToString();

            if (CreditDebit.Equals(TransactionCodes.EFTTransactionTypeCredit))
            {
                if (IsValidAccountToProceedCreditTXN(AccountNo, bankCode, Amount, amountFieldName, ref remarks, ref errorMsg))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (IsValidAccountToProceedDebitTXN(AccountNo, bankCode, Amount, amountFieldName, ref remarks, ref errorMsg))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int SendRTServiceEDRXMLForOutwardByBatchWise(Guid TransactionID, string bankCode, int ApprovedBy)
        {
            string remarks = string.Empty;
            string errorMsg = string.Empty;
            int goodBatchCount = 0;
            RTServiceDB rtDB = new RTServiceDB();
            DataTable dtDistinctAccount = rtDB.GetDistinctAccount_For_TransactionSent_forFCUBS(TransactionID);

            bool IsValidBatch = true;
            bool IsConnected = true;
            string CreditDebit = string.Empty;

            //should come from the parameter
            int IsAccountWiseCBS = 1;

            SentEDRDB sentEDR = new SentEDRDB();

            IsAccountWiseCBS = sentEDR.GetAccountWiseCBSHitByTransactionID(TransactionID);

            for (int i = 0; i < dtDistinctAccount.Rows.Count; i++)
            {
                string AccountNo = dtDistinctAccount.Rows[i]["AccountNo"].ToString();
                string Amount = dtDistinctAccount.Rows[i]["TotalAmount"].ToString();
                CreditDebit = dtDistinctAccount.Rows[i]["BatchType"].ToString();

                if (IsValidCBSAccountForOutwardTxnForBatch(bankCode, ref remarks, ref errorMsg, CreditDebit, AccountNo, Amount))
                {
                }
                else
                {
                    if (remarks.Equals(string.Empty))
                    {
                        sentEDR.Update_FCUBS_ErrorByTransactionIDAndAccountNo(TransactionID, AccountNo, errorMsg);
                        IsValidBatch = false;
                    }
                    else
                    {
                        IsConnected = false;
                        break;
                        //DO NOTHING
                    }
                }
            }


            if (IsConnected)
            {
                if (IsValidBatch)
                {
                    if (CreditDebit.Equals(TransactionCodes.EFTTransactionTypeCredit))
                    {
                        if (IsAccountWiseCBS == 1)
                        {
                            for (int i = 0; i < dtDistinctAccount.Rows.Count; i++)
                            {
                                string AccountNo = dtDistinctAccount.Rows[i]["AccountNo"].ToString();
                                string Amount = dtDistinctAccount.Rows[i]["TotalAmount"].ToString();
                                string BatchNumber = dtDistinctAccount.Rows[i]["BatchNumber"].ToString().Substring(4, 2) + i.ToString().PadLeft(4, '0');
                                string EntryDesc = dtDistinctAccount.Rows[i]["EntryDesc"].ToString();
                                //string EntryDateTransactionSent = dtDistinctAccount.Rows[i]["EntryDateTransactionSent"].ToString();
                                string EFTEffectiveEntryDate = dtDistinctAccount.Rows[i]["EFTEffectiveEntryDate"].ToString();
                                string FloraReference = string.Empty;
                                RTServiceTxnXML FCUBSRTTxnXML = new RTServiceTxnXML();
                                string eDRSentXMLString = FCUBSRTTxnXML.CreateRTServiceSentEDRXMLForBatchWise(bankCode, AccountNo, Amount, BatchNumber, CreditDebit, EntryDesc, EFTEffectiveEntryDate, ref FloraReference);
                                string queryResult = CBSAccountDebit(eDRSentXMLString);
                                SaveRTServiceResponseForOutwardForBatchWise(queryResult, TransactionID, ApprovedBy, AccountNo, FloraReference);
                            }
                        }
                        else
                        {
                            //Get EDRID by TransactionID
                            DataTable dtEDRID = sentEDR.GetEDRIDByTransactionID(TransactionID);
                            for (int i = 0; i < dtEDRID.Rows.Count; i++)
                            {
                                string edrId = dtEDRID.Rows[i]["EDRID"].ToString();
                                Guid EDRID = new Guid(edrId);
                                //account debit credit
                                int failedTXN = SendRTServiceEDRXMLForOutward(EDRID, bankCode, ApprovedBy);
                            }
                        }
                        goodBatchCount++;
                    }
                    else
                    {
                        EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
                        sentBatchDB.UpdateEDRSentStatusForBatchApproval(EFTN.Utility.TransactionStatus.TransSent_Approved_By_Checker, TransactionID, ApprovedBy);
                        goodBatchCount++;
                    }
                }
                else
                {
                    //if (remarks.Equals(string.Empty))
                    //{
                        //TO DO 
                        //SEND DATA TO Maker as rejected
                        //send error message from "errorMsg" variable
                        EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                        db.Insert_RejectReason_ByChecker(TransactionID, errorMsg,
                                (int)EFTN.Utility.ItemType.TransactionSent);

                        EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
                        sentBatchDB.UpdateEDRSentStatusForBatchApproval(EFTN.Utility.TransactionStatus.TransSent_Rejected_By_Checker, TransactionID, ApprovedBy);
                    //}
                    //else
                    //{
                    //    //DO NOTHING
                    //}
                }//end IsValidBatch
            }//end IsConnected

            return goodBatchCount;
        }


        private bool IsValidCBSAccountForOutwardTxnForBatch(string bankCode, ref string remarks, ref string errorMsg, string CreditDebit, string AccountNo, string Amount)
        {
            RTServiceAccountManager rtServiceAccountManager = new RTServiceAccountManager();
            RTServiceDB rtServiceDb = new RTServiceDB();
            DataTable dtEDR = new DataTable();

            string ACC_SERVICE_CHK_BALANCE = ConfigurationManager.AppSettings["ACC_SERVICE_CHK_BALANCE"].ToString();
            string amountFieldName = string.Empty;

            if (ACC_SERVICE_CHK_BALANCE.Equals("1"))
            {
                amountFieldName = ConfigurationManager.AppSettings["ACC_SERVICE_FIELDNAME_BALANCE"].ToString();
            }

            if (CreditDebit.Equals(TransactionCodes.EFTTransactionTypeCredit))
            {
                if (IsValidAccountToProceedCreditTXNForBatch(AccountNo, bankCode, Amount, amountFieldName, ref remarks, ref errorMsg))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (IsValidAccountToProceedDebitTXNForBatch(AccountNo, bankCode, Amount, amountFieldName, ref remarks, ref errorMsg))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool IsValidAccountToProceedCreditTXNForBatch(string AccountNo, string bankCode, string amount, string amountFieldName, ref string remarks, ref string errorMsg)
        {
            RTServiceAccountManager rtServiceAccountManager = new RTServiceAccountManager();

            string result = rtServiceAccountManager.GetCBSData(AccountNo);

            if (!result.Equals("Failed"))
            {
                string ECODE = string.Empty;
                string EDESC = string.Empty;
                try
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(result);

                    ECODE = xdoc.GetElementsByTagName("ECODE").Item(0).InnerText;
                    EDESC = xdoc.GetElementsByTagName("EDESC").Item(0).InnerText;
                }
                catch
                {
                }

                if (ECODE.Equals(string.Empty))
                {

                    if (bankCode.Equals(OriginalBankCode.NRB))
                    {
                        return IsValidToProceedTXNForNRB(result, amount, "DR", amountFieldName, ref errorMsg);
                    }
                    else if (bankCode.Equals(OriginalBankCode.UCBL))
                    {
                        return IsValidToProceedTXNForNRB(result, amount, "DR", amountFieldName, ref errorMsg);
                    }
                }
                else
                {
                    errorMsg = ECODE + " - " + EDESC;
                }
            }
            else
            {
                remarks = "Unable to connect";
            }
            return false;
        }


        /// <summary>
        /// Check the account status to process the transaction for the outward Debit
        /// </summary>
        /// <param name="AccountNo"></param>
        /// <param name="accountStatus"></param>
        /// <param name="bankCode"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private bool IsValidAccountToProceedDebitTXNForBatch(string AccountNo, string bankCode, string amount, string amountFieldName, ref string remarks, ref string errorMsg)
        {
            RTServiceAccountManager rtServiceAccountManager = new RTServiceAccountManager();

            string result = rtServiceAccountManager.GetCBSData(AccountNo);

            if (!result.Equals("Failed"))
            {

                string ECODE = string.Empty;
                string EDESC = string.Empty;
                try
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(result);

                    ECODE = xdoc.GetElementsByTagName("ECODE").Item(0).InnerText;
                    EDESC = xdoc.GetElementsByTagName("EDESC").Item(0).InnerText;
                }
                catch
                {
                }

                if (ECODE.Equals(string.Empty))
                {
                    if (bankCode.Equals(OriginalBankCode.NRB))
                    {
                        return IsValidToProceedTXNForNRB(result, amount, "CR", amountFieldName, ref errorMsg);
                    }
                    else if (bankCode.Equals(OriginalBankCode.UCBL))
                    {
                        return IsValidToProceedTXNForNRB(result, amount, "CR", amountFieldName, ref errorMsg);
                    }
                }
                else
                {
                    errorMsg = ECODE + " - " + EDESC;
                }
            }
            else
            {
                remarks = "Unable to connect";
            }
            return false;
        }

        private void SaveRTServiceResponseForOutwardForBatchWise(string queryResult, Guid TransactionID, int ApprovedBy, string AccountNo, string FloraReference)
        {
            string Status = string.Empty;
            string XREF = string.Empty;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(queryResult);
            try
            {
                XREF = doc.GetElementsByTagName("XREF").Item(0).InnerText;
                Status = doc.GetElementsByTagName("EDESC").Item(0).InnerText;
            }
            catch
            {

            }

            if (XREF == string.Empty)
            {
            }
            else
            {
                if (Status == string.Empty)
                {
                    //Transaction successfully completed
                    //TO DO
                    EFTN.component.FCUBSTransactionsentBatchDB db = new EFTN.component.FCUBSTransactionsentBatchDB();
                    Status = doc.GetElementsByTagName("WDESC").Item(0).InnerText;
                    string CBSReference = doc.GetElementsByTagName("FCCREF").Item(0).InnerText;
                    db.UpdateEDRSentStatusForBatchApprovalByAccountNo(EFTN.Utility.TransactionStatus.TransSent_Approved_By_Checker, TransactionID, ApprovedBy, AccountNo, CBSReference, FloraReference);
                }
                else
                {
                    //TO DO
                    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                    EFTN.component.RejectReasonByCheckerDB rejectedDB = new EFTN.component.RejectReasonByCheckerDB();
                    rejectedDB.Insert_RejectReason_ByChecker(TransactionID, ("Ref: " + XREF + ": " + Status),
                            (int)EFTN.Utility.ItemType.TransactionSent);

                    db.UpdateEDRSentStatusForBatchApproval(EFTN.Utility.TransactionStatus.TransSent_Rejected_By_Checker, TransactionID, ApprovedBy);
                }
            }
        }


        public string SendRTServiceEDRXMLForOutwardByBatchWiseForDebit(Guid TransactionID, string bankCode, int ApprovedBy)
        {
            string remarks = string.Empty;
            string errorMsg = string.Empty;
            string DisplayMsg = string.Empty;

            RTServiceDB rtDB = new RTServiceDB();
            RTServiceTxnXML FCUBSRTTxnXML = new RTServiceTxnXML();

            //should come from the parameter
            int IsAccountWiseCBS = 1;

            SentEDRDB sentEDR = new SentEDRDB();

            IsAccountWiseCBS = sentEDR.GetAccountWiseCBSHitByTransactionID(TransactionID);

            DataTable dtDistinctAccount = rtDB.GetDistinctAccount_For_TransactionSent_forFCUBS(TransactionID);

            EFTN.component.FCUBSTransactionsentBatchDB db = new EFTN.component.FCUBSTransactionsentBatchDB();

            bool IsValidBatch = true;
            bool IsConnected = true;
            string CreditDebit = string.Empty;
            int totalCBSSuccess = 0;

            for (int i = 0; i < dtDistinctAccount.Rows.Count; i++)
            {
                string AccountNo = dtDistinctAccount.Rows[i]["AccountNo"].ToString();
                string Amount = dtDistinctAccount.Rows[i]["TotalAmount"].ToString();
                CreditDebit = dtDistinctAccount.Rows[i]["BatchType"].ToString();

                if (IsValidCBSAccountForOutwardTxnForBatch(bankCode, ref remarks, ref errorMsg, CreditDebit, AccountNo, Amount))
                {
                }
                else
                {
                    if (remarks.Equals(string.Empty))
                    {
                        IsValidBatch = false;
                    }
                    else
                    {
                        IsConnected = false;
                        //DO NOTHING
                    }
                }
            }


            if (IsConnected)
            {
                if (IsValidBatch)
                {
                    if (IsAccountWiseCBS == 1)
                    {
                        for (int i = 0; i < dtDistinctAccount.Rows.Count; i++)
                        {
                            string AccountNo = dtDistinctAccount.Rows[i]["AccountNo"].ToString();
                            string Amount = dtDistinctAccount.Rows[i]["TotalAmount"].ToString();
                            string BatchNumber = dtDistinctAccount.Rows[i]["BatchNumber"].ToString().Substring(4, 2) + i.ToString().PadLeft(4, '0');
                            string EntryDesc = dtDistinctAccount.Rows[i]["EntryDesc"].ToString();
                            string EFTEffectiveEntryDate = dtDistinctAccount.Rows[i]["EFTEffectiveEntryDate"].ToString();

                            string FloraReference = string.Empty;

                            string eDRSentXMLString = FCUBSRTTxnXML.CreateRTServiceSentEDRXMLForBatchWise(bankCode, AccountNo, Amount, BatchNumber, CreditDebit, EntryDesc, EFTEffectiveEntryDate, ref FloraReference);
                            string queryResult = CBSAccountDebit(eDRSentXMLString);

                            string Status = string.Empty;
                            string XREF = string.Empty;
                            string CBSReference = string.Empty;
                            int CBSStatus = 0;
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(queryResult);
                            try
                            {
                                XREF = doc.GetElementsByTagName("XREF").Item(0).InnerText;
                                Status = doc.GetElementsByTagName("EDESC").Item(0).InnerText;
                                DisplayMsg += Status;
                            }
                            catch
                            {

                            }

                            if (XREF == string.Empty)
                            {
                            }
                            else
                            {
                                if (Status == string.Empty)
                                {
                                    //DisplayMsg += doc.GetElementsByTagName("WDESC").Item(0).InnerText;
                                    CBSReference = doc.GetElementsByTagName("FCCREF").Item(0).InnerText;
                                    //transaction successful insert flora reference number and cbs(FCCREF) reference number
                                    //TransactionID, AccountNo, FloraReference. CBSReference
                                    CBSStatus = 1;
                                }
                                else
                                {
                                    //unsuccessful transaction insert flora re
                                }
                            }
                            totalCBSSuccess += CBSStatus;
                            db.UpdateEDRSentSuccessfulStatusForBatchApprovalByAccountNoForDebit(TransactionID, ApprovedBy, AccountNo, CBSReference, FloraReference, CBSStatus);
                        }
                        int totalCBSfailed = dtDistinctAccount.Rows.Count - totalCBSSuccess;
                        DisplayMsg = "Total " + totalCBSSuccess.ToString() + " transactions successfully executed. and "
                                        + totalCBSfailed.ToString() + " transactions failed to execute.";
                    }
                    else//By transaction wise
                    {
                        //Get EDRID by TransactionID
                        DataTable dtEDRID = sentEDR.GetEDRIDByTransactionID(TransactionID);
                        for (int i = 0; i < dtEDRID.Rows.Count; i++)
                        {
                            string edrId = dtEDRID.Rows[i]["EDRID"].ToString();
                            Guid EDRID = new Guid(edrId);
                            //account debit credit
                            //int failedTXN = SendRTServiceEDRXMLForOutward(EDRID, bankCode, ApprovedBy);
                            string NRBRT_XREF = string.Empty;

                            string eDRSentXMLString = FCUBSRTTxnXML.CreateRTServiceSentEDRXMLByEDRIDForDebit(EDRID, bankCode, ref NRBRT_XREF);
                            string queryResult = CBSAccountDebit(eDRSentXMLString);
                            string Status = string.Empty;
                            string XREF = string.Empty;
                            string CBSReference = string.Empty;
                            int CBSStatus = 0;
                            string FloraReference = string.Empty;

                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(queryResult);
                            try
                            {
                                XREF = doc.GetElementsByTagName("XREF").Item(0).InnerText;
                                Status = doc.GetElementsByTagName("EDESC").Item(0).InnerText;
                                //DisplayMsg += Status;
                            }
                            catch
                            {

                            }

                            if (XREF == string.Empty)
                            {
                            }
                            else
                            {
                                if (Status == string.Empty)
                                {
                                    //DisplayMsg += doc.GetElementsByTagName("WDESC").Item(0).InnerText;
                                    CBSReference = doc.GetElementsByTagName("FCCREF").Item(0).InnerText;
                                    CBSStatus = 1;
                                }
                            }

                            totalCBSSuccess += CBSStatus;
                            FloraReference = NRBRT_XREF;
                            db.UpdateEDRSentSuccessfulStatusForOutwardDebitByEDRID(EDRID, ApprovedBy, CBSReference, FloraReference, CBSStatus);

                            //if (XREF == string.Empty)
                            //{
                            //}
                            //else
                            //{
                            //    if (Status == string.Empty)
                            //    {
                            //        DisplayMsg += doc.GetElementsByTagName("WDESC").Item(0).InnerText;
                            //        CBSReference = doc.GetElementsByTagName("FCCREF").Item(0).InnerText;
                            //        //transaction successful insert flora reference number and cbs(FCCREF) reference number
                            //        //TransactionID, AccountNo, FloraReference. CBSReference
                            //        CBSStatus = 1;
                            //    }
                            //    else
                            //    {
                            //        //unsuccessful transaction insert flora re
                            //    }
                            //}                            
                            //db.UpdateEDRSentSuccessfulStatusForBatchApprovalByAccountNoForDebit(TransactionID, ApprovedBy, AccountNo, CBSReference, FloraReference, CBSStatus);
                        }

                        int totalCBSfailed = dtEDRID.Rows.Count - totalCBSSuccess;
                        DisplayMsg = "Total " + totalCBSSuccess.ToString() + " transactions successfully executed. and "
                                        + totalCBSfailed.ToString() + " transactions failed to execute.";


                    }
                }
                else
                {
                    DisplayMsg = "Invalid items in the batch. The system cannot process this batch";
                }
            }
            else
            {
                DisplayMsg = "Cannot connect to the server. Please try again later";
            }//end IsConnected

            return DisplayMsg;
        }

        public void SynchronizeAccountStatusWithFCUBS(Guid EDRID, string AccountNo, string TranType, string amountFieldName, string connectionString, double Amount)
        {
            RTServiceAccountManager rtServiceAccountManager = new RTServiceAccountManager();
            ReceivedEDRDB receivedEDR = new ReceivedEDRDB();

            string result = rtServiceAccountManager.GetCBSData(AccountNo);
            string AccountStatus = string.Empty;
            string remarks = string.Empty;
            string ReceiverNameFromCBS = string.Empty;
            double ACY_AVL_BAL = 0;

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
                    AccountStatus = ECODE + ": " + EDESC;
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
                            }
                        }


                        if (TranType.Equals("DR") && !amountFieldName.Equals(string.Empty))
                        {
                            ACY_AVL_BAL = ParseData.StringToDouble(xdoc.GetElementsByTagName(amountFieldName).Item(0).InnerText);
                            if (ACY_AVL_BAL < Amount)
                            {
                                AccountStatus += " Insufficient Amount";
                            }
                        }

                        ReceiverNameFromCBS = xdoc.GetElementsByTagName("CUSTNAME").Item(0).InnerText;
                        //Update amount and status in ReceivedEDR Table
                        receivedEDR.UpdateReceivedEDRAmountAndAccountStatusFromCBS(EDRID, 0, AccountStatus, ReceiverNameFromCBS, connectionString);
                    }
                    catch (Exception ex)
                    {
                        AccountStatus = "Account not found";
                        receivedEDR.UpdateReceivedEDRAmountAndAccountStatusFromCBS(EDRID, 0, AccountStatus, ReceiverNameFromCBS, connectionString);
                    }
                }
                else
                {
                    receivedEDR.UpdateReceivedEDRAmountAndAccountStatusFromCBS(EDRID, 0, AccountStatus, ReceiverNameFromCBS, connectionString);
                }
            }
        }


        private bool IsValidCBSAccountForInwardReturnTxn(Guid EDRID, string bankCode, ref string remarks, ref string errorMsg, ref string OID)
        {
            RTServiceAccountManager rtServiceAccountManager = new RTServiceAccountManager();
            RTServiceDB rtServiceDb = new RTServiceDB();
            ReceivedReturnDB receiverReturn = new ReceivedReturnDB();

            DataTable dtEDR = new DataTable();
            dtEDR = receiverReturn.GetReceivedReturn_AccInfo_ForFCUBS(EDRID);

            string ACC_SERVICE_CHK_BALANCE = ConfigurationManager.AppSettings["ACC_SERVICE_CHK_BALANCE"].ToString();
            string amountFieldName = string.Empty;
            string CreditDebit = string.Empty;

            if (ACC_SERVICE_CHK_BALANCE.Equals("1"))
            {
                amountFieldName = ConfigurationManager.AppSettings["ACC_SERVICE_FIELDNAME_BALANCE"].ToString();
            }

            string AccountNo = string.Empty;
            string Amount = string.Empty;

            AccountNo = dtEDR.Rows[0]["AccountNo"].ToString();
            Amount = dtEDR.Rows[0]["Amount"].ToString();
            CreditDebit = dtEDR.Rows[0]["BatchType"].ToString();
            OID = dtEDR.Rows[0]["OID"].ToString();

            if (CreditDebit.Equals(TransactionCodes.EFTTransactionTypeCredit))
            {
                if (IsValidAccountToProceedDebitTXN(AccountNo, bankCode, Amount, amountFieldName, ref remarks, ref errorMsg))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (IsValidAccountToProceedCreditTXN(AccountNo, bankCode, Amount, amountFieldName, ref remarks, ref errorMsg))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        private int SaveRTServiceResponseForInwardReturn(string queryResult, Guid ReturnID, int ApprovedBy, string RT_REF,
                                                                    string DepartmentID,
                                                                    string IdNumber,
                                                                    string REMARKS,
                                                                    string REMARKS1,
                                                                    string ReturnCode,
                                                                    string RejectReason,
                                                                    string bankCode
)
        {
            int failedCounter = 0;
            string Status = "";
            string XREF = "";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(queryResult);
            try
            {
                XREF = doc.GetElementsByTagName("XREF").Item(0).InnerText;
                Status = doc.GetElementsByTagName("EDESC").Item(0).InnerText;
            }
            catch
            {

            }

            if (XREF == string.Empty)
            {
            }
            else
            {
                if (Status == string.Empty)
                {
                    //Transaction successfully completed
                    string FCCREF = doc.GetElementsByTagName("FCCREF").Item(0).InnerText;
                    EFTN.component.ReceivedReturnDB db = new EFTN.component.ReceivedReturnDB();
                    db.Update_ReceivedReturn_Status_ByChecker(EFTN.Utility.TransactionStatus.Return_Received_Approval__Approved_by_checker, ReturnID);
                    //update string RT_REF(Flora reference) CBSReference in the database
                    RTServiceDB rtserviceDB = new RTServiceDB();
                    rtserviceDB.UpdateReceivedReturnReferenceForFCUBS(ReturnID, RT_REF, FCCREF);
                    //Save internet banking return response for NRB Bank
                    /*
                    if (bankCode.Equals(OriginalBankCode.NRB))
                    {
                        if (DepartmentID.Equals("999"))
                        {
                            NrbIBReturn.Service nrbIBreturnService= new NrbIBReturn.Service();
                            bool IBankingAck = false;
                            try
                            {
                                int result = nrbIBreturnService.returnMappFcdbEftnWebService(ApprovedBy.ToString(), IdNumber, "RETURN",
                                                                                RejectReason, ReturnCode,
                                                                                FCCREF, REMARKS, REMARKS1);
                                if (result == 1)
                                {
                                    IBankingAck = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                IBankingAck = false;
                            }
                            finally
                            {
                                if (IBankingAck == false)
                                {
                                    rtserviceDB.UpdateIBankingReturnACKStatus(ReturnID, IBankingAck);
                                }
                            }
                        }
                    }*/
                }
                else
                {
                    //TO DO
                    //DELETE TRANSACTION FROM THE MAIN TABLE AND TRANSFER TO ERROR TABLE by EDRID
                    failedCounter++;
                    RTServiceDB rtServiceDb = new RTServiceDB();
                    rtServiceDb.InsertFCUBS_Err_For_ReceivedReturn(ReturnID, Status);

                }
            }

            return failedCounter;
        }

        public int SendRTServiceEDRXMLForOutwardByBatchWiseUnsuccessfulDebit(Guid TransactionID, string bankCode, int ApprovedBy)
        {
            string remarks = string.Empty;
            string errorMsg = string.Empty;
            int goodBatchCount = 0;
            RTServiceDB rtDB = new RTServiceDB();
            DataTable dtDistinctAccount = rtDB.GetUnsuccessfulTransactionSentDebit_forFCUBS(TransactionID);

            bool IsValidBatch = true;
            bool IsConnected = true;
            string CreditDebit = string.Empty;
            int failedTXN = 0;

            SentEDRDB sentEDR = new SentEDRDB();
            FCUBSTransactionsentBatchDB db = new FCUBSTransactionsentBatchDB();
            RTServiceTxnXML FCUBSRTTxnXML = new RTServiceTxnXML();
            FCUBSRejectedTxnDB fcubsDB = new FCUBSRejectedTxnDB();

            for (int i = 0; i < dtDistinctAccount.Rows.Count; i++)
            {
                string AccountNo = dtDistinctAccount.Rows[i]["AccountNo"].ToString();
                string Amount = dtDistinctAccount.Rows[i]["Amount"].ToString();
                CreditDebit = dtDistinctAccount.Rows[i]["BatchType"].ToString();
                string edrId = dtDistinctAccount.Rows[i]["EDRID"].ToString();
                Guid EDRID = new Guid(edrId);

                if (IsValidCBSAccountForOutwardTxnForBatch(bankCode, ref remarks, ref errorMsg, CreditDebit, AccountNo, Amount))
                {
                }
                else
                {
                    if (remarks.Equals(string.Empty))
                    {
                        //...... save unsuccessful status and reason by EDRID
                        fcubsDB.Update_FCUBS_UnsuccessfulDebitBy_EDRID(EDRID, errorMsg);
                        IsValidBatch = false;
                    }
                    else
                    {
                        IsConnected = false;
                        break;
                        //DO NOTHING
                    }
                }
            }


            if (IsConnected)
            {
                if (IsValidBatch)
                {
                    for (int i = 0; i < dtDistinctAccount.Rows.Count; i++)
                    {
                        string NRBRT_XREF = string.Empty;
                        string AccountNo = dtDistinctAccount.Rows[i]["AccountNo"].ToString();
                        string Amount = dtDistinctAccount.Rows[i]["Amount"].ToString();
                        string BatchNumber = dtDistinctAccount.Rows[i]["BatchNumber"].ToString().Substring(4, 2) + i.ToString().PadLeft(4, '0');
                        string EntryDesc = dtDistinctAccount.Rows[i]["EntryDesc"].ToString();
                        string EFTEffectiveEntryDate = dtDistinctAccount.Rows[i]["EFTEffectiveEntryDate"].ToString();
                        string edrId = dtDistinctAccount.Rows[i]["EDRID"].ToString();
                        Guid EDRID = new Guid(edrId);
                        string FloraReference = string.Empty;
                        string eDRSentXMLString = FCUBSRTTxnXML.CreateRTServiceSentEDRXML(EDRID, bankCode, ref NRBRT_XREF);
                        string queryResult = CBSAccountDebit(eDRSentXMLString);

                        string Status = string.Empty;
                        string XREF = string.Empty;
                        string CBSReference = string.Empty;
                        int CBSStatus = 0;
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(queryResult);
                        try
                        {
                            XREF = doc.GetElementsByTagName("XREF").Item(0).InnerText;
                            Status = doc.GetElementsByTagName("EDESC").Item(0).InnerText;
                            failedTXN++;
                        }
                        catch
                        {

                        }

                        if (XREF == string.Empty)
                        {
                        }
                        else
                        {
                            if (Status == string.Empty)
                            {
                                CBSReference = doc.GetElementsByTagName("FCCREF").Item(0).InnerText;
                                CBSStatus = 1;
                            }
                        }
                        FloraReference = NRBRT_XREF;
                        db.UpdateEDRSentSuccessfulStatusForOutwardDebitByEDRID(EDRID, ApprovedBy, CBSReference, FloraReference, CBSStatus);
                    }
                    goodBatchCount++;
                }
            }//end IsConnected

            return goodBatchCount;
        }


        public void SynchronizeAccountNameWithFCUBSForSentEDR(Guid EDRID, string AccountNo, string connectionString)
        {
            RTServiceAccountManager rtServiceAccountManager = new RTServiceAccountManager();
            SentEDRDB sentEDRDB = new SentEDRDB();

            string result = rtServiceAccountManager.GetCBSData(AccountNo);
            string AccountStatus = string.Empty;
            string remarks = string.Empty;
            string SenderNameFromCBS = "Invalid Account Number";

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
                    sentEDRDB.UpdateSentEDRForReceiverNameFromCBS(EDRID, SenderNameFromCBS, connectionString);
                }
                catch
                {
                }

                if (ECODE.Equals(string.Empty))
                {

                    RTServiceDB rtServiceDb = new RTServiceDB();
                    try
                    {
                        SenderNameFromCBS = xdoc.GetElementsByTagName("CUSTNAME").Item(0).InnerText;
                        //Update amount and status in ReceivedEDR Table
                        sentEDRDB.UpdateSentEDRForReceiverNameFromCBS(EDRID, SenderNameFromCBS, connectionString);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

    }
}