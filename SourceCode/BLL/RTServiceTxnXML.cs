using System;
using System.Text;
using System.Data;
using EFTN.component;
using System.Configuration;
using EFTN.Utility;

namespace EFTN.BLL
{
    public class RTServiceTxnXML
    {
        //the following function is actually by TransactionID which sent as EDRID(but it is TransactionID)
        public string CreateRTServiceSentEDRXML(Guid EDRID, string bankCode, ref string NRBRT_XREF)
        {
            StringBuilder eDRXML = new StringBuilder();
            RTServiceDB nrbRTServiceDB = new RTServiceDB();
            //SqlDataReader sqlDREDR = sentEDRDB.GetSentEDRByTransactionID(TransactionID);

            DataTable dtEDR = new DataTable();
            dtEDR = nrbRTServiceDB.GetSentEDRByBatchSentID_forNrbRTServ(EDRID, bankCode);


            StringBuilder strAllXML = new StringBuilder();
            //int transactoinSentXmlFileSize = ParseData.StringToInt(ConfigurationManager.AppSettings["xmlFileSize"]);
            string xmlFileName = string.Empty;
            string strRTServiceContent = string.Empty;

            string NRBRT_TXNACC = string.Empty;
            string NRBRT_TXNAMT = string.Empty;
            string traceNubmer = string.Empty;
            string batchType = string.Empty;
            string EntryDesc = string.Empty;
            DateTime effectiveEntryDate = new DateTime();

            for (int edrCount = 0; edrCount < dtEDR.Rows.Count; edrCount++)
            {
                traceNubmer = dtEDR.Rows[edrCount]["TraceNumber"].ToString();
                batchType = dtEDR.Rows[edrCount]["BatchType"].ToString();
                NRBRT_TXNACC = System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["AccountNo"].ToString()));
                NRBRT_TXNAMT = dtEDR.Rows[edrCount]["Amount"].ToString();
                EntryDesc = dtEDR.Rows[edrCount]["EntryDesc"].ToString();
                effectiveEntryDate = Convert.ToDateTime(dtEDR.Rows[edrCount]["EffectiveEntryDate"]);
            }

            NRBRT_XREF = "EOU" + effectiveEntryDate.ToString("ddMMyy") + traceNubmer;

            if (batchType.Equals(TransactionCodes.EFTTransactionTypeCredit))
            {
                string RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ = ConfigurationManager.AppSettings["RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ"];
                string RT_SERVICE_DR_SOURCE = ConfigurationManager.AppSettings["RT_SERVICE_DR_SOURCE"];
                string RT_SERVICE_DR_UBSCOMP = ConfigurationManager.AppSettings["RT_SERVICE_DR_UBSCOMP"];
                string RT_SERVICE_DR_USERID = ConfigurationManager.AppSettings["RT_SERVICE_DR_USERID"];
                string RT_SERVICE_DR_BRANCH = ConfigurationManager.AppSettings["RT_SERVICE_DR_BRANCH"];
                string RT_SERVICE_DR_MODULEID = ConfigurationManager.AppSettings["RT_SERVICE_DR_MODULEID"];
                string RT_SERVICE_DR_SERVICE = ConfigurationManager.AppSettings["RT_SERVICE_DR_SERVICE"];
                string RT_SERVICE_DR_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_DR_OPERATION"];
                string RT_SERVICE_DR_SOURCE_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_DR_SOURCE_OPERATION"];

                //string RT_SERVICE_DR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_DR_PRD"];
                string RT_SERVICE_DR_PRD = string.Empty;

                if (bankCode.Equals(OriginalBankCode.UCBL))
                {
                    RT_SERVICE_DR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_OUT"];
                }
                else
                {
                    RT_SERVICE_DR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_DR_PRD"];
                }


                strRTServiceContent =
                                    "<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                        "<S:Body>" +
                                            "<CREATETRANSACTION_FSFS_REQ xmlns=\"" + RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ + "\">" +
                                            "<FCUBS_HEADER>" +
                                            "<SOURCE>" + RT_SERVICE_DR_SOURCE + "</SOURCE>" +
                                            "<UBSCOMP>" + RT_SERVICE_DR_UBSCOMP + "</UBSCOMP>" +
                                            "<MSGID/>" +
                                            "<CORRELID/>" +
                                            "<USERID>" + RT_SERVICE_DR_USERID + "</USERID>" +
                                            "<BRANCH>" + RT_SERVICE_DR_BRANCH + "</BRANCH>" +
                                            "<MODULEID>" + RT_SERVICE_DR_MODULEID + "</MODULEID>" +
                                            "<SERVICE>" + RT_SERVICE_DR_SERVICE + "</SERVICE>" +
                                            "<OPERATION>" + RT_SERVICE_DR_OPERATION + "</OPERATION>" +
                                            "<SOURCE_OPERATION>" + RT_SERVICE_DR_SOURCE_OPERATION + "</SOURCE_OPERATION>" +
                                            "<SOURCE_USERID/> " +
                                            "<DESTINATION/> " +
                                            "<MULTITRIPID/> " +
                                            "<FUNCTIONID/> " +
                                            "<ACTION/> " +
                                            "<MSGSTAT/> " +
                                            "<PASSWORD/> " +
                                            "<ADDL>" +
                                                "<PARAM>" +
                                                    "<NAME/> " +
                                                    "<VALUE/> " +
                                                "</PARAM>" +
                                            "</ADDL>" +
                                            "</FCUBS_HEADER>" +
                                            "<FCUBS_BODY>" +
                                                "<Transaction-Details>" +
                                                    "<XREF>" + NRBRT_XREF + "</XREF>" +
                                                    "<FCCREF/>" +
                                                    "<PRD>" + RT_SERVICE_DR_PRD + "</PRD>" +
                                                    "<BRN>" + RT_SERVICE_DR_BRANCH + "</BRN>" +
                                                    "<MODULE>" + RT_SERVICE_DR_MODULEID + "</MODULE>" +
                                                    "<TXNBRN>" + NRBRT_TXNACC.Substring(0, 3) + "</TXNBRN>" +
                                                    "<TXNACC>" + NRBRT_TXNACC + "</TXNACC>" +
                                                    "<TXNCCY>BDT</TXNCCY>" +
                                                    "<TXNAMT>" + NRBRT_TXNAMT + "</TXNAMT>" +
                                                    "<NARRATIVE>" + EntryDesc + "</NARRATIVE>" +
                                                "</Transaction-Details>" +
                                            "</FCUBS_BODY>" +
                                            "</CREATETRANSACTION_FSFS_REQ>" +
                                        "</S:Body>" +
                                    "</S:Envelope>";
            }
            else
            {
                string RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ = ConfigurationManager.AppSettings["RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ"];
                string RT_SERVICE_CR_SOURCE = ConfigurationManager.AppSettings["RT_SERVICE_CR_SOURCE"];
                string RT_SERVICE_CR_UBSCOMP = ConfigurationManager.AppSettings["RT_SERVICE_CR_UBSCOMP"];
                string RT_SERVICE_CR_USERID = ConfigurationManager.AppSettings["RT_SERVICE_CR_USERID"];
                string RT_SERVICE_CR_BRANCH = ConfigurationManager.AppSettings["RT_SERVICE_CR_BRANCH"];
                string RT_SERVICE_CR_MODULEID = ConfigurationManager.AppSettings["RT_SERVICE_CR_MODULEID"];
                string RT_SERVICE_CR_SERVICE = ConfigurationManager.AppSettings["RT_SERVICE_CR_SERVICE"];
                string RT_SERVICE_CR_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_CR_OPERATION"];
                string RT_SERVICE_CR_SOURCE_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_CR_SOURCE_OPERATION"];

                //string RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_PRD"];
                string RT_SERVICE_CR_PRD = string.Empty;

                if (bankCode.Equals(OriginalBankCode.UCBL))
                {
                    RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_DR_OUT"];
                }
                else
                {
                    RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_PRD"];
                }


                strRTServiceContent =
                                        "<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                        "<S:Body>" +
                                            "<CREATETRANSACTION_FSFS_REQ xmlns=\"" + RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ + "\">" +
                                            "<FCUBS_HEADER>" +
                                            "<SOURCE>" + RT_SERVICE_CR_SOURCE + "</SOURCE>" +
                                            "<UBSCOMP>" + RT_SERVICE_CR_UBSCOMP + "</UBSCOMP>" +
                                            "<MSGID/>" +
                                            "<CORRELID/>" +
                                            "<USERID>" + RT_SERVICE_CR_USERID + "</USERID>" +
                                            "<BRANCH>" + RT_SERVICE_CR_BRANCH + "</BRANCH>" +
                                            "<MODULEID>" + RT_SERVICE_CR_MODULEID + "</MODULEID>" +
                                            "<SERVICE>" + RT_SERVICE_CR_SERVICE + "</SERVICE>" +
                                            "<OPERATION>" + RT_SERVICE_CR_OPERATION + "</OPERATION>" +
                                            "<SOURCE_OPERATION>" + RT_SERVICE_CR_SOURCE_OPERATION + "</SOURCE_OPERATION>" +
                                            "<SOURCE_USERID/> " +
                                            "<DESTINATION/> " +
                                            "<MULTITRIPID/> " +
                                            "<FUNCTIONID/> " +
                                            "<ACTION/> " +
                                            "<MSGSTAT/> " +
                                            "<PASSWORD/> " +
                                            "<ADDL>" +
                                                "<PARAM>" +
                                                    "<NAME/> " +
                                                    "<VALUE/> " +
                                                "</PARAM>" +
                                            "</ADDL>" +
                                            "</FCUBS_HEADER>" +
                                            "<FCUBS_BODY>" +
                                                "<Transaction-Details>" +
                                                    "<XREF>" + NRBRT_XREF + "</XREF>" +
                                                    "<FCCREF/>" +
                                                    "<PRD>" + RT_SERVICE_CR_PRD + "</PRD>" +
                                                    "<BRN>" + RT_SERVICE_CR_BRANCH + "</BRN>" +
                                                    "<MODULE>" + RT_SERVICE_CR_MODULEID + "</MODULE>" +
                                                    "<TXNBRN>" + NRBRT_TXNACC.Substring(0, 3) + "</TXNBRN>" +
                                                    "<TXNACC>" + NRBRT_TXNACC + "</TXNACC>" +
                                                    "<TXNCCY>BDT</TXNCCY>" +
                                                    "<TXNAMT>" + NRBRT_TXNAMT + "</TXNAMT>" +
                                                    "<NARRATIVE>" + EntryDesc + "</NARRATIVE>" +
                                                "</Transaction-Details>" +
                                            "</FCUBS_BODY>" +
                                            "</CREATETRANSACTION_FSFS_REQ>" +
                                        "</S:Body>" +
                                    "</S:Envelope>";
            }
            return strRTServiceContent;
        }

        public string CreateRTServiceSentEDRXMLByEDRIDForDebit(Guid EDRID, string bankCode, ref string NRBRT_XREF)
        {
            StringBuilder eDRXML = new StringBuilder();
            RTServiceDB nrbRTServiceDB = new RTServiceDB();
            //SqlDataReader sqlDREDR = sentEDRDB.GetSentEDRByTransactionID(TransactionID);

            DataTable dtEDR = new DataTable();
            dtEDR = nrbRTServiceDB.GetSentEDRByBatchSentID_forNrbRTServ(EDRID, bankCode);


            StringBuilder strAllXML = new StringBuilder();
            //int transactoinSentXmlFileSize = ParseData.StringToInt(ConfigurationManager.AppSettings["xmlFileSize"]);
            string xmlFileName = string.Empty;
            string strRTServiceContent = string.Empty;

            string NRBRT_TXNACC = string.Empty;
            string NRBRT_TXNAMT = string.Empty;
            string traceNubmer = string.Empty;
            string batchType = string.Empty;
            string EntryDesc = string.Empty;
            DateTime SettlementJDate = new DateTime();

            for (int edrCount = 0; edrCount < dtEDR.Rows.Count; edrCount++)
            {
                traceNubmer = dtEDR.Rows[edrCount]["TraceNumber"].ToString();
                batchType = dtEDR.Rows[edrCount]["BatchType"].ToString();
                NRBRT_TXNACC = System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["AccountNo"].ToString()));
                NRBRT_TXNAMT = dtEDR.Rows[edrCount]["Amount"].ToString();
                EntryDesc = dtEDR.Rows[edrCount]["EntryDesc"].ToString();
                SettlementJDate = Convert.ToDateTime(dtEDR.Rows[edrCount]["SettlementJDate"]);
            }

            NRBRT_XREF = "EOU" + SettlementJDate.ToString("ddMMyy") + traceNubmer;

            if (batchType.Equals(TransactionCodes.EFTTransactionTypeCredit))
            {
                string RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ = ConfigurationManager.AppSettings["RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ"];
                string RT_SERVICE_DR_SOURCE = ConfigurationManager.AppSettings["RT_SERVICE_DR_SOURCE"];
                string RT_SERVICE_DR_UBSCOMP = ConfigurationManager.AppSettings["RT_SERVICE_DR_UBSCOMP"];
                string RT_SERVICE_DR_USERID = ConfigurationManager.AppSettings["RT_SERVICE_DR_USERID"];
                string RT_SERVICE_DR_BRANCH = ConfigurationManager.AppSettings["RT_SERVICE_DR_BRANCH"];
                string RT_SERVICE_DR_MODULEID = ConfigurationManager.AppSettings["RT_SERVICE_DR_MODULEID"];
                string RT_SERVICE_DR_SERVICE = ConfigurationManager.AppSettings["RT_SERVICE_DR_SERVICE"];
                string RT_SERVICE_DR_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_DR_OPERATION"];
                string RT_SERVICE_DR_SOURCE_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_DR_SOURCE_OPERATION"];

                //string RT_SERVICE_DR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_DR_PRD"];
                string RT_SERVICE_DR_PRD = string.Empty;

                if (bankCode.Equals(OriginalBankCode.UCBL))
                {
                    RT_SERVICE_DR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_OUT"];
                }
                else
                {
                    RT_SERVICE_DR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_DR_PRD"];
                }


                strRTServiceContent =
                                    "<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                        "<S:Body>" +
                                            "<CREATETRANSACTION_FSFS_REQ xmlns=\"" + RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ + "\">" +
                                            "<FCUBS_HEADER>" +
                                            "<SOURCE>" + RT_SERVICE_DR_SOURCE + "</SOURCE>" +
                                            "<UBSCOMP>" + RT_SERVICE_DR_UBSCOMP + "</UBSCOMP>" +
                                            "<MSGID/>" +
                                            "<CORRELID/>" +
                                            "<USERID>" + RT_SERVICE_DR_USERID + "</USERID>" +
                                            "<BRANCH>" + RT_SERVICE_DR_BRANCH + "</BRANCH>" +
                                            "<MODULEID>" + RT_SERVICE_DR_MODULEID + "</MODULEID>" +
                                            "<SERVICE>" + RT_SERVICE_DR_SERVICE + "</SERVICE>" +
                                            "<OPERATION>" + RT_SERVICE_DR_OPERATION + "</OPERATION>" +
                                            "<SOURCE_OPERATION>" + RT_SERVICE_DR_SOURCE_OPERATION + "</SOURCE_OPERATION>" +
                                            "<SOURCE_USERID/> " +
                                            "<DESTINATION/> " +
                                            "<MULTITRIPID/> " +
                                            "<FUNCTIONID/> " +
                                            "<ACTION/> " +
                                            "<MSGSTAT/> " +
                                            "<PASSWORD/> " +
                                            "<ADDL>" +
                                                "<PARAM>" +
                                                    "<NAME/> " +
                                                    "<VALUE/> " +
                                                "</PARAM>" +
                                            "</ADDL>" +
                                            "</FCUBS_HEADER>" +
                                            "<FCUBS_BODY>" +
                                                "<Transaction-Details>" +
                                                    "<XREF>" + NRBRT_XREF + "</XREF>" +
                                                    "<FCCREF/>" +
                                                    "<PRD>" + RT_SERVICE_DR_PRD + "</PRD>" +
                                                    "<BRN>" + RT_SERVICE_DR_BRANCH + "</BRN>" +
                                                    "<MODULE>" + RT_SERVICE_DR_MODULEID + "</MODULE>" +
                                                    "<TXNBRN>" + NRBRT_TXNACC.Substring(0, 3) + "</TXNBRN>" +
                                                    "<TXNACC>" + NRBRT_TXNACC + "</TXNACC>" +
                                                    "<TXNCCY>BDT</TXNCCY>" +
                                                    "<TXNAMT>" + NRBRT_TXNAMT + "</TXNAMT>" +
                                                    "<NARRATIVE>" + EntryDesc + "</NARRATIVE>" +
                                                "</Transaction-Details>" +
                                            "</FCUBS_BODY>" +
                                            "</CREATETRANSACTION_FSFS_REQ>" +
                                        "</S:Body>" +
                                    "</S:Envelope>";
            }
            else
            {
                string RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ = ConfigurationManager.AppSettings["RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ"];
                string RT_SERVICE_CR_SOURCE = ConfigurationManager.AppSettings["RT_SERVICE_CR_SOURCE"];
                string RT_SERVICE_CR_UBSCOMP = ConfigurationManager.AppSettings["RT_SERVICE_CR_UBSCOMP"];
                string RT_SERVICE_CR_USERID = ConfigurationManager.AppSettings["RT_SERVICE_CR_USERID"];
                string RT_SERVICE_CR_BRANCH = ConfigurationManager.AppSettings["RT_SERVICE_CR_BRANCH"];
                string RT_SERVICE_CR_MODULEID = ConfigurationManager.AppSettings["RT_SERVICE_CR_MODULEID"];
                string RT_SERVICE_CR_SERVICE = ConfigurationManager.AppSettings["RT_SERVICE_CR_SERVICE"];
                string RT_SERVICE_CR_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_CR_OPERATION"];
                string RT_SERVICE_CR_SOURCE_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_CR_SOURCE_OPERATION"];

                //string RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_PRD"];
                string RT_SERVICE_CR_PRD = string.Empty;

                if (bankCode.Equals(OriginalBankCode.UCBL))
                {
                    RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_DR_OUT"];
                }
                else
                {
                    RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_PRD"];
                }


                strRTServiceContent =
                                        "<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                        "<S:Body>" +
                                            "<CREATETRANSACTION_FSFS_REQ xmlns=\"" + RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ + "\">" +
                                            "<FCUBS_HEADER>" +
                                            "<SOURCE>" + RT_SERVICE_CR_SOURCE + "</SOURCE>" +
                                            "<UBSCOMP>" + RT_SERVICE_CR_UBSCOMP + "</UBSCOMP>" +
                                            "<MSGID/>" +
                                            "<CORRELID/>" +
                                            "<USERID>" + RT_SERVICE_CR_USERID + "</USERID>" +
                                            "<BRANCH>" + RT_SERVICE_CR_BRANCH + "</BRANCH>" +
                                            "<MODULEID>" + RT_SERVICE_CR_MODULEID + "</MODULEID>" +
                                            "<SERVICE>" + RT_SERVICE_CR_SERVICE + "</SERVICE>" +
                                            "<OPERATION>" + RT_SERVICE_CR_OPERATION + "</OPERATION>" +
                                            "<SOURCE_OPERATION>" + RT_SERVICE_CR_SOURCE_OPERATION + "</SOURCE_OPERATION>" +
                                            "<SOURCE_USERID/> " +
                                            "<DESTINATION/> " +
                                            "<MULTITRIPID/> " +
                                            "<FUNCTIONID/> " +
                                            "<ACTION/> " +
                                            "<MSGSTAT/> " +
                                            "<PASSWORD/> " +
                                            "<ADDL>" +
                                                "<PARAM>" +
                                                    "<NAME/> " +
                                                    "<VALUE/> " +
                                                "</PARAM>" +
                                            "</ADDL>" +
                                            "</FCUBS_HEADER>" +
                                            "<FCUBS_BODY>" +
                                                "<Transaction-Details>" +
                                                    "<XREF>" + NRBRT_XREF + "</XREF>" +
                                                    "<FCCREF/>" +
                                                    "<PRD>" + RT_SERVICE_CR_PRD + "</PRD>" +
                                                    "<BRN>" + RT_SERVICE_CR_BRANCH + "</BRN>" +
                                                    "<MODULE>" + RT_SERVICE_CR_MODULEID + "</MODULE>" +
                                                    "<TXNBRN>" + NRBRT_TXNACC.Substring(0, 3) + "</TXNBRN>" +
                                                    "<TXNACC>" + NRBRT_TXNACC + "</TXNACC>" +
                                                    "<TXNCCY>BDT</TXNCCY>" +
                                                    "<TXNAMT>" + NRBRT_TXNAMT + "</TXNAMT>" +
                                                    "<NARRATIVE>" + EntryDesc + "</NARRATIVE>" +
                                                "</Transaction-Details>" +
                                            "</FCUBS_BODY>" +
                                            "</CREATETRANSACTION_FSFS_REQ>" +
                                        "</S:Body>" +
                                    "</S:Envelope>";
            }
            return strRTServiceContent;
        }

        public string CreateRTServiceSentEDRXMLForBatchWise(string bankCode, string NRBRT_TXNACC, string NRBRT_TXNAMT, string BatchNumber, string batchType, string EntryDesc, string EFTEffectiveEntryDate, ref string FloraReference)
        {
            StringBuilder eDRXML = new StringBuilder();
            RTServiceDB nrbRTServiceDB = new RTServiceDB();
            //SqlDataReader sqlDREDR = sentEDRDB.GetSentEDRByTransactionID(TransactionID);

            //DataTable dtEDR = new DataTable();
            //dtEDR = nrbRTServiceDB.GetSentEDRByBatchSentID_forNrbRTServ(EDRID, bankCode);


            StringBuilder strAllXML = new StringBuilder();
            //int transactoinSentXmlFileSize = ParseData.StringToInt(ConfigurationManager.AppSettings["xmlFileSize"]);
            string NRBRT_XREF = string.Empty;
            string xmlFileName = string.Empty;
            string strRTServiceContent = string.Empty;

            //string EntryDesc = string.Empty;
            //for (int edrCount = 0; edrCount < dtEDR.Rows.Count; edrCount++)
            //{
            //    traceNubmer = dtEDR.Rows[edrCount]["TraceNumber"].ToString();
            //    batchType = dtEDR.Rows[edrCount]["BatchType"].ToString();
            //    NRBRT_TXNACC = System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["AccountNo"].ToString()));
            //    NRBRT_TXNAMT = dtEDR.Rows[edrCount]["Amount"].ToString();
            //    EntryDesc = dtEDR.Rows[edrCount]["EntryDesc"].ToString();
            //    effectiveEntryDate = Convert.ToDateTime(dtEDR.Rows[edrCount]["EffectiveEntryDate"]);
            //}

            NRBRT_XREF = "EOU" + EFTEffectiveEntryDate + "B" + BatchNumber;
            FloraReference = NRBRT_XREF;
            if (batchType.Equals(TransactionCodes.EFTTransactionTypeCredit))
            {
                string RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ = ConfigurationManager.AppSettings["RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ"];
                string RT_SERVICE_DR_SOURCE = ConfigurationManager.AppSettings["RT_SERVICE_DR_SOURCE"];
                string RT_SERVICE_DR_UBSCOMP = ConfigurationManager.AppSettings["RT_SERVICE_DR_UBSCOMP"];
                string RT_SERVICE_DR_USERID = ConfigurationManager.AppSettings["RT_SERVICE_DR_USERID"];
                string RT_SERVICE_DR_BRANCH = ConfigurationManager.AppSettings["RT_SERVICE_DR_BRANCH"];
                string RT_SERVICE_DR_MODULEID = ConfigurationManager.AppSettings["RT_SERVICE_DR_MODULEID"];
                string RT_SERVICE_DR_SERVICE = ConfigurationManager.AppSettings["RT_SERVICE_DR_SERVICE"];
                string RT_SERVICE_DR_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_DR_OPERATION"];
                string RT_SERVICE_DR_SOURCE_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_DR_SOURCE_OPERATION"];

                //string RT_SERVICE_DR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_DR_PRD"];
                string RT_SERVICE_DR_PRD = string.Empty;

                if (bankCode.Equals(OriginalBankCode.UCBL))
                {
                    RT_SERVICE_DR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_OUT"];
                }
                else
                {
                    RT_SERVICE_DR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_DR_PRD"];
                }


                strRTServiceContent =
                                    "<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                        "<S:Body>" +
                                            "<CREATETRANSACTION_FSFS_REQ xmlns=\"" + RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ + "\">" +
                                            "<FCUBS_HEADER>" +
                                            "<SOURCE>" + RT_SERVICE_DR_SOURCE + "</SOURCE>" +
                                            "<UBSCOMP>" + RT_SERVICE_DR_UBSCOMP + "</UBSCOMP>" +
                                            "<MSGID/>" +
                                            "<CORRELID/>" +
                                            "<USERID>" + RT_SERVICE_DR_USERID + "</USERID>" +
                                            "<BRANCH>" + RT_SERVICE_DR_BRANCH + "</BRANCH>" +
                                            "<MODULEID>" + RT_SERVICE_DR_MODULEID + "</MODULEID>" +
                                            "<SERVICE>" + RT_SERVICE_DR_SERVICE + "</SERVICE>" +
                                            "<OPERATION>" + RT_SERVICE_DR_OPERATION + "</OPERATION>" +
                                            "<SOURCE_OPERATION>" + RT_SERVICE_DR_SOURCE_OPERATION + "</SOURCE_OPERATION>" +
                                            "<SOURCE_USERID/> " +
                                            "<DESTINATION/> " +
                                            "<MULTITRIPID/> " +
                                            "<FUNCTIONID/> " +
                                            "<ACTION/> " +
                                            "<MSGSTAT/> " +
                                            "<PASSWORD/> " +
                                            "<ADDL>" +
                                                "<PARAM>" +
                                                    "<NAME/> " +
                                                    "<VALUE/> " +
                                                "</PARAM>" +
                                            "</ADDL>" +
                                            "</FCUBS_HEADER>" +
                                            "<FCUBS_BODY>" +
                                                "<Transaction-Details>" +
                                                    "<XREF>" + NRBRT_XREF + "</XREF>" +
                                                    "<FCCREF/>" +
                                                    "<PRD>" + RT_SERVICE_DR_PRD + "</PRD>" +
                                                    "<BRN>" + RT_SERVICE_DR_BRANCH + "</BRN>" +
                                                    "<MODULE>" + RT_SERVICE_DR_MODULEID + "</MODULE>" +
                                                    "<TXNBRN>" + NRBRT_TXNACC.Substring(0, 3) + "</TXNBRN>" +
                                                    "<TXNACC>" + NRBRT_TXNACC + "</TXNACC>" +
                                                    "<TXNCCY>BDT</TXNCCY>" +
                                                    "<TXNAMT>" + NRBRT_TXNAMT + "</TXNAMT>" +
                                                    "<NARRATIVE>" + EntryDesc + "</NARRATIVE>" +
                                                "</Transaction-Details>" +
                                            "</FCUBS_BODY>" +
                                            "</CREATETRANSACTION_FSFS_REQ>" +
                                        "</S:Body>" +
                                    "</S:Envelope>";
            }
            else
            {
                string RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ = ConfigurationManager.AppSettings["RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ"];
                string RT_SERVICE_CR_SOURCE = ConfigurationManager.AppSettings["RT_SERVICE_CR_SOURCE"];
                string RT_SERVICE_CR_UBSCOMP = ConfigurationManager.AppSettings["RT_SERVICE_CR_UBSCOMP"];
                string RT_SERVICE_CR_USERID = ConfigurationManager.AppSettings["RT_SERVICE_CR_USERID"];
                string RT_SERVICE_CR_BRANCH = ConfigurationManager.AppSettings["RT_SERVICE_CR_BRANCH"];
                string RT_SERVICE_CR_MODULEID = ConfigurationManager.AppSettings["RT_SERVICE_CR_MODULEID"];
                string RT_SERVICE_CR_SERVICE = ConfigurationManager.AppSettings["RT_SERVICE_CR_SERVICE"];
                string RT_SERVICE_CR_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_CR_OPERATION"];
                string RT_SERVICE_CR_SOURCE_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_CR_SOURCE_OPERATION"];

                //string RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_PRD"];
                string RT_SERVICE_CR_PRD = string.Empty;

                if (bankCode.Equals(OriginalBankCode.UCBL))
                {
                    RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_DR_OUT"];
                }
                else
                {
                    RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_PRD"];
                }


                strRTServiceContent =
                                        "<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                        "<S:Body>" +
                                            "<CREATETRANSACTION_FSFS_REQ xmlns=\"" + RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ + "\">" +
                                            "<FCUBS_HEADER>" +
                                            "<SOURCE>" + RT_SERVICE_CR_SOURCE + "</SOURCE>" +
                                            "<UBSCOMP>" + RT_SERVICE_CR_UBSCOMP + "</UBSCOMP>" +
                                            "<MSGID/>" +
                                            "<CORRELID/>" +
                                            "<USERID>" + RT_SERVICE_CR_USERID + "</USERID>" +
                                            "<BRANCH>" + RT_SERVICE_CR_BRANCH + "</BRANCH>" +
                                            "<MODULEID>" + RT_SERVICE_CR_MODULEID + "</MODULEID>" +
                                            "<SERVICE>" + RT_SERVICE_CR_SERVICE + "</SERVICE>" +
                                            "<OPERATION>" + RT_SERVICE_CR_OPERATION + "</OPERATION>" +
                                            "<SOURCE_OPERATION>" + RT_SERVICE_CR_SOURCE_OPERATION + "</SOURCE_OPERATION>" +
                                            "<SOURCE_USERID/> " +
                                            "<DESTINATION/> " +
                                            "<MULTITRIPID/> " +
                                            "<FUNCTIONID/> " +
                                            "<ACTION/> " +
                                            "<MSGSTAT/> " +
                                            "<PASSWORD/> " +
                                            "<ADDL>" +
                                                "<PARAM>" +
                                                    "<NAME/> " +
                                                    "<VALUE/> " +
                                                "</PARAM>" +
                                            "</ADDL>" +
                                            "</FCUBS_HEADER>" +
                                            "<FCUBS_BODY>" +
                                                "<Transaction-Details>" +
                                                    "<XREF>" + NRBRT_XREF + "</XREF>" +
                                                    "<FCCREF/>" +
                                                    "<PRD>" + RT_SERVICE_CR_PRD + "</PRD>" +
                                                    "<BRN>" + RT_SERVICE_CR_BRANCH + "</BRN>" +
                                                    "<MODULE>" + RT_SERVICE_CR_MODULEID + "</MODULE>" +
                                                    "<TXNBRN>" + NRBRT_TXNACC.Substring(0, 3) + "</TXNBRN>" +
                                                    "<TXNACC>" + NRBRT_TXNACC + "</TXNACC>" +
                                                    "<TXNCCY>BDT</TXNCCY>" +
                                                    "<TXNAMT>" + NRBRT_TXNAMT + "</TXNAMT>" +
                                                    "<NARRATIVE>" + EntryDesc + "</NARRATIVE>" +
                                                "</Transaction-Details>" +
                                            "</FCUBS_BODY>" +
                                            "</CREATETRANSACTION_FSFS_REQ>" +
                                        "</S:Body>" +
                                    "</S:Envelope>";
            }
            return strRTServiceContent;
        }

        public string CreateRTServiceReceivedReturnXML(Guid ReturnID, string bankCode, string OID,
            ref string NRBRT_XREF, 
            ref string DepartmentID,
            ref string IdNumber,
            ref string REMARKS,
            ref string REMARKS1,
            ref string ReturnCode,
            ref string RejectReason
            
            )
        {
            StringBuilder eDRXML = new StringBuilder();
            RTServiceDB nrbRTServiceDB = new RTServiceDB();
            //SqlDataReader sqlDREDR = sentEDRDB.GetSentEDRByTransactionID(TransactionID);

            DataTable dtEDR = new DataTable();
            dtEDR = nrbRTServiceDB.GetReceivedReturn_Approved_ForNrbRTService(ReturnID);


            StringBuilder strAllXML = new StringBuilder();
            //int transactoinSentXmlFileSize = ParseData.StringToInt(ConfigurationManager.AppSettings["xmlFileSize"]);
            string xmlFileName = string.Empty;
            string strRTServiceContent = string.Empty;

            string NRBRT_TXNACC = string.Empty;
            string NRBRT_TXNAMT = string.Empty;
            string traceNubmer = string.Empty;
            string batchType = string.Empty;
            string EntryDesc = string.Empty;
            DateTime effectiveEntryDate = new DateTime();
            string ReceivingBankRoutingNo = string.Empty;
            string NRBRTReferece = string.Empty;
            string NARRATIVE = string.Empty;

            for (int edrCount = 0; edrCount < dtEDR.Rows.Count; edrCount++)
            {
                traceNubmer = dtEDR.Rows[edrCount]["TraceNumber"].ToString();
                batchType = dtEDR.Rows[edrCount]["BatchType"].ToString();
                NRBRT_TXNACC = System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["AccountNo"].ToString()));
                NRBRT_TXNAMT = dtEDR.Rows[edrCount]["Amount"].ToString();
                EntryDesc = dtEDR.Rows[edrCount]["EntryDesc"].ToString();
                effectiveEntryDate = Convert.ToDateTime(dtEDR.Rows[edrCount]["EffectiveEntryDate"]);
                ReceivingBankRoutingNo = dtEDR.Rows[edrCount]["ReceivingBankRoutingNo"].ToString();
                RejectReason = dtEDR.Rows[edrCount]["RejectReason"].ToString();
                NRBRTReferece = dtEDR.Rows[edrCount]["NRBRTReferece"].ToString();
                ReturnCode = dtEDR.Rows[edrCount]["ReturnCode"].ToString();
                NARRATIVE = dtEDR.Rows[edrCount]["NARRATIVE"].ToString();
                DepartmentID = dtEDR.Rows[edrCount]["DepartmentID"].ToString();
                IdNumber = dtEDR.Rows[edrCount]["IdNumber"].ToString();
                REMARKS = dtEDR.Rows[edrCount]["REMARKS"].ToString();
                REMARKS1 = dtEDR.Rows[edrCount]["REMARKS1"].ToString();
            }


            if (batchType.Equals(TransactionCodes.EFTTransactionTypeDebit))
            {
                string RT_SERVICE_DR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_DR_PRD"];

                if (bankCode.Equals(OriginalBankCode.UCBL))
                {
                    RT_SERVICE_DR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_DR_IR"];
                    NRBRT_XREF = "EIRD" + ReceivingBankRoutingNo.Substring(0, 3) + OID.PadLeft(9, '0');
                }
                else if (bankCode.Equals(OriginalBankCode.NRB))
                {
                    NRBRT_XREF = "ER" + ReceivingBankRoutingNo.Substring(0, 3) + OID.PadLeft(11, '0');
                }
                string RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ = ConfigurationManager.AppSettings["RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ"];
                string RT_SERVICE_DR_SOURCE = ConfigurationManager.AppSettings["RT_SERVICE_DR_SOURCE"];
                string RT_SERVICE_DR_UBSCOMP = ConfigurationManager.AppSettings["RT_SERVICE_DR_UBSCOMP"];
                string RT_SERVICE_DR_USERID = ConfigurationManager.AppSettings["RT_SERVICE_DR_USERID"];
                string RT_SERVICE_DR_BRANCH = ConfigurationManager.AppSettings["RT_SERVICE_DR_BRANCH"];
                string RT_SERVICE_DR_MODULEID = ConfigurationManager.AppSettings["RT_SERVICE_DR_MODULEID"];
                string RT_SERVICE_DR_SERVICE = ConfigurationManager.AppSettings["RT_SERVICE_DR_SERVICE"];
                string RT_SERVICE_DR_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_DR_OPERATION"];
                string RT_SERVICE_DR_SOURCE_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_DR_SOURCE_OPERATION"];



                strRTServiceContent =
                                    "<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                        "<S:Body>" +
                                        "<CREATETRANSACTION_FSFS_REQ xmlns=\"" + RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ + "\">" +
                                        "<FCUBS_HEADER>" +
                                        "<SOURCE>" + RT_SERVICE_DR_SOURCE + "</SOURCE>" +
                                        "<UBSCOMP>" + RT_SERVICE_DR_UBSCOMP + "</UBSCOMP>" +
                                        "<MSGID/>" +
                                        "<CORRELID/>" +
                                        "<USERID>" + RT_SERVICE_DR_USERID + "</USERID>" +
                                        "<BRANCH>" + RT_SERVICE_DR_BRANCH + "</BRANCH>" +
                                        "<MODULEID>" + RT_SERVICE_DR_MODULEID + "</MODULEID>" +
                                        "<SERVICE>" + RT_SERVICE_DR_SERVICE + "</SERVICE>" +
                                        "<OPERATION>" + RT_SERVICE_DR_OPERATION + "</OPERATION>" +
                                        "<SOURCE_OPERATION>" + RT_SERVICE_DR_SOURCE_OPERATION + "</SOURCE_OPERATION>" +
                                        "<SOURCE_USERID/> " +
                                        "<DESTINATION/> " +
                                        "<MULTITRIPID/> " +
                                        "<FUNCTIONID/> " +
                                        "<ACTION/> " +
                                        "<MSGSTAT/> " +
                                        "<PASSWORD/> " +
                                        "<ADDL>" +
                                            "<PARAM>" +
                                                "<NAME/> " +
                                                "<VALUE/> " +
                                            "</PARAM>" +
                                        "</ADDL>" +
                                        "</FCUBS_HEADER>" +
                                        "<FCUBS_BODY>" +
                                            "<Transaction-Details>" +
                                                "<XREF>" + NRBRT_XREF + "</XREF>" +
                                                "<FCCREF/>" +
                                                "<PRD>" + RT_SERVICE_DR_PRD + "</PRD>" +
                                                "<BRN>" + RT_SERVICE_DR_BRANCH + "</BRN>" +
                                                "<MODULE>" + RT_SERVICE_DR_MODULEID + "</MODULE>" +
                                                "<TXNBRN>" + NRBRT_TXNACC.Substring(0, 3) + "</TXNBRN>" +
                                                "<TXNACC>" + NRBRT_TXNACC + "</TXNACC>" +
                                                "<TXNCCY>BDT</TXNCCY>" +
                                                "<TXNAMT>" + NRBRT_TXNAMT + "</TXNAMT>" +
                                                "<NARRATIVE>" + NARRATIVE + "</NARRATIVE>" +
                                            "</Transaction-Details>" +
                                        "</FCUBS_BODY>" +
                                        "</CREATETRANSACTION_FSFS_REQ>" +
                                    "</S:Body>" +
                                "</S:Envelope>";
            }
            else
            {
                string RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_PRD"];

                if (bankCode.Equals(OriginalBankCode.UCBL))
                {
                    RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_IR"];
                    NRBRT_XREF = "EIRC" + ReceivingBankRoutingNo.Substring(0, 3) + OID.PadLeft(9, '0');
                }
                else if (bankCode.Equals(OriginalBankCode.NRB))
                {
                    NRBRT_XREF = "ER" + ReceivingBankRoutingNo.Substring(0, 3) + OID.PadLeft(11, '0');
                }

                string RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ = ConfigurationManager.AppSettings["RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ"];
                string RT_SERVICE_CR_SOURCE = ConfigurationManager.AppSettings["RT_SERVICE_CR_SOURCE"];
                string RT_SERVICE_CR_UBSCOMP = ConfigurationManager.AppSettings["RT_SERVICE_CR_UBSCOMP"];
                string RT_SERVICE_CR_USERID = ConfigurationManager.AppSettings["RT_SERVICE_CR_USERID"];
                string RT_SERVICE_CR_BRANCH = ConfigurationManager.AppSettings["RT_SERVICE_CR_BRANCH"];
                string RT_SERVICE_CR_MODULEID = ConfigurationManager.AppSettings["RT_SERVICE_CR_MODULEID"];
                string RT_SERVICE_CR_SERVICE = ConfigurationManager.AppSettings["RT_SERVICE_CR_SERVICE"];
                string RT_SERVICE_CR_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_CR_OPERATION"];
                string RT_SERVICE_CR_SOURCE_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_CR_SOURCE_OPERATION"];


                strRTServiceContent =
                                    "<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                        "<S:Body>" +
                                        "<CREATETRANSACTION_FSFS_REQ xmlns=\"" + RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ + "\">" +
                                        "<FCUBS_HEADER>" +
                                        "<SOURCE>" + RT_SERVICE_CR_SOURCE + "</SOURCE>" +
                                        "<UBSCOMP>" + RT_SERVICE_CR_UBSCOMP + "</UBSCOMP>" +
                                        "<MSGID/>" +
                                        "<CORRELID/>" +
                                        "<USERID>" + RT_SERVICE_CR_USERID + "</USERID>" +
                                        "<BRANCH>" + RT_SERVICE_CR_BRANCH + "</BRANCH>" +
                                        "<MODULEID>" + RT_SERVICE_CR_MODULEID + "</MODULEID>" +
                                        "<SERVICE>" + RT_SERVICE_CR_SERVICE + "</SERVICE>" +
                                        "<OPERATION>" + RT_SERVICE_CR_OPERATION + "</OPERATION>" +
                                        "<SOURCE_OPERATION>" + RT_SERVICE_CR_SOURCE_OPERATION + "</SOURCE_OPERATION>" +
                                        "<SOURCE_USERID/> " +
                                        "<DESTINATION/> " +
                                        "<MULTITRIPID/> " +
                                        "<FUNCTIONID/> " +
                                        "<ACTION/> " +
                                        "<MSGSTAT/> " +
                                        "<PASSWORD/> " +
                                        "<ADDL>" +
                                            "<PARAM>" +
                                                "<NAME/> " +
                                                "<VALUE/> " +
                                            "</PARAM>" +
                                        "</ADDL>" +
                                        "</FCUBS_HEADER>" +
                                        "<FCUBS_BODY>" +
                                            "<Transaction-Details>" +
                                                "<XREF>" + NRBRT_XREF + "</XREF>" +
                                                "<FCCREF/>" +
                                                "<PRD>" + RT_SERVICE_CR_PRD + "</PRD>" +
                                                "<BRN>" + RT_SERVICE_CR_BRANCH + "</BRN>" +
                                                "<MODULE>" + RT_SERVICE_CR_MODULEID + "</MODULE>" +
                                                "<TXNBRN>" + NRBRT_TXNACC.Substring(0, 3) + "</TXNBRN>" +
                                                "<TXNACC>" + NRBRT_TXNACC + "</TXNACC>" +
                                                "<TXNCCY>BDT</TXNCCY>" +
                                                "<TXNAMT>" + NRBRT_TXNAMT + "</TXNAMT>" +
                                                "<NARRATIVE>" + NARRATIVE + "</NARRATIVE>" +
                                            "</Transaction-Details>" +
                                        "</FCUBS_BODY>" +
                                        "</CREATETRANSACTION_FSFS_REQ>" +
                                    "</S:Body>" +
                                "</S:Envelope>";
            }
            return strRTServiceContent;
        }

        public string CreateRTServiceReceivedTransactionXML(Guid EDRID, string bankCode, ref string NRBRT_XREF)
        {
            StringBuilder eDRXML = new StringBuilder();
            RTServiceDB nrbRTServiceDB = new RTServiceDB();
            //SqlDataReader sqlDREDR = sentEDRDB.GetSentEDRByTransactionID(TransactionID);

            DataTable dtEDR = new DataTable();
            dtEDR = nrbRTServiceDB.GetReceivedEDR_ApprovedByMaker_ForNrbRTServ(EDRID);


            StringBuilder strAllXML = new StringBuilder();
            //int transactoinSentXmlFileSize = ParseData.StringToInt(ConfigurationManager.AppSettings["xmlFileSize"]);
            string xmlFileName = string.Empty;
            string strRTServiceContent = string.Empty;

            //string NRBRT_XREF = string.Empty;
            string NRBRT_TXNACC = string.Empty;
            string NRBRT_TXNAMT = string.Empty;
            string traceNubmer = string.Empty;
            string batchType = string.Empty;
            string EntryDesc = string.Empty;
            DateTime effectiveEntryDate = new DateTime();
            string SendingBankRoutNo = string.Empty;
            string ReceiverName = string.Empty;
            string NRBRTReferece = string.Empty;
            string ReturnCode = string.Empty;
            for (int edrCount = 0; edrCount < dtEDR.Rows.Count; edrCount++)
            {
                traceNubmer = dtEDR.Rows[edrCount]["TraceNumber"].ToString();
                batchType = dtEDR.Rows[edrCount]["CreditDebit"].ToString();
                NRBRT_TXNACC = System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["DFIAccountNo"].ToString()));
                NRBRT_TXNAMT = dtEDR.Rows[edrCount]["Amount"].ToString();
                EntryDesc = dtEDR.Rows[edrCount]["EntryDesc"].ToString();
                effectiveEntryDate = Convert.ToDateTime(dtEDR.Rows[edrCount]["SettlementJDate"]);
                SendingBankRoutNo = dtEDR.Rows[edrCount]["SendingBankRoutNo"].ToString();
                ReceiverName = dtEDR.Rows[edrCount]["ReceiverName"].ToString();
            }

            NRBRT_XREF = "EI" + SendingBankRoutNo.Substring(0, 3) + effectiveEntryDate.ToString("ddMM") + traceNubmer;

            if (batchType.Equals(TransactionCodes.EFTTransactionTypeDebit))
            {
                string RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ = ConfigurationManager.AppSettings["RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ"];
                string RT_SERVICE_DR_SOURCE = ConfigurationManager.AppSettings["RT_SERVICE_DR_SOURCE"];
                string RT_SERVICE_DR_UBSCOMP = ConfigurationManager.AppSettings["RT_SERVICE_DR_UBSCOMP"];
                string RT_SERVICE_DR_USERID = ConfigurationManager.AppSettings["RT_SERVICE_DR_USERID"];
                string RT_SERVICE_DR_BRANCH = ConfigurationManager.AppSettings["RT_SERVICE_DR_BRANCH"];
                string RT_SERVICE_DR_MODULEID = ConfigurationManager.AppSettings["RT_SERVICE_DR_MODULEID"];
                string RT_SERVICE_DR_SERVICE = ConfigurationManager.AppSettings["RT_SERVICE_DR_SERVICE"];
                string RT_SERVICE_DR_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_DR_OPERATION"];
                string RT_SERVICE_DR_SOURCE_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_DR_SOURCE_OPERATION"];

                string RT_SERVICE_DR_PRD = string.Empty;

                if (bankCode.Equals(OriginalBankCode.UCBL))
                {
                    RT_SERVICE_DR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_DR_IN"];
                }
                else
                {
                    RT_SERVICE_DR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_DR_PRD"];
                }

                strRTServiceContent =
                                    "<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                        "<S:Body>" +
                                            "<CREATETRANSACTION_FSFS_REQ xmlns=\"" + RT_SERVICE_DR_CREATETRANSACTION_FSFS_REQ + "\">" +
                                            "<FCUBS_HEADER>" +
                                            "<SOURCE>" + RT_SERVICE_DR_SOURCE + "</SOURCE>" +
                                            "<UBSCOMP>" + RT_SERVICE_DR_UBSCOMP + "</UBSCOMP>" +
                                            "<MSGID />" +
                                            "<CORRELID />" +
                                            "<USERID>" + RT_SERVICE_DR_USERID + "</USERID>" +
                                            "<BRANCH>" + RT_SERVICE_DR_BRANCH + "</BRANCH>" +
                                            "<MODULEID>" + RT_SERVICE_DR_MODULEID + "</MODULEID>" +
                                            "<SERVICE>" + RT_SERVICE_DR_SERVICE + "</SERVICE>" +
                                            "<OPERATION>" + RT_SERVICE_DR_OPERATION + "</OPERATION>" +
                                            "<SOURCE_OPERATION>" + RT_SERVICE_DR_SOURCE_OPERATION + "</SOURCE_OPERATION>" +
                                            "<SOURCE_USERID /> " +
                                            "<DESTINATION /> " +
                                            "<MULTITRIPID /> " +
                                            "<FUNCTIONID /> " +
                                            "<ACTION /> " +
                                            "<MSGSTAT /> " +
                                            "<PASSWORD /> " +
                                            "<ADDL>" +
                                                "<PARAM>" +
                                                    "<NAME /> " +
                                                    "<VALUE /> " +
                                                "</PARAM>" +
                                            "</ADDL>" +
                                            "</FCUBS_HEADER>" +
                                            "<FCUBS_BODY>" +
                                                "<Transaction-Details>" +
                                                    "<XREF>" + NRBRT_XREF + "</XREF>" +
                                                    "<FCCREF />" +
                                                    "<PRD>" + RT_SERVICE_DR_PRD + "</PRD>" +
                                                    "<BRN>" + RT_SERVICE_DR_BRANCH + "</BRN>" +
                                                    "<MODULE>" + RT_SERVICE_DR_MODULEID + "</MODULE>" +
                                                    "<TXNBRN>" + NRBRT_TXNACC.Substring(0, 3) + "</TXNBRN>" +
                                                    "<TXNACC>" + NRBRT_TXNACC + "</TXNACC>" +
                                                    "<TXNCCY>BDT</TXNCCY>" +
                                                    "<TXNAMT>" + NRBRT_TXNAMT + "</TXNAMT>" +
                                                    "<NARRATIVE>" + EntryDesc + "</NARRATIVE>" +
                                                "</Transaction-Details>" +
                                            "</FCUBS_BODY>" +
                                            "</CREATETRANSACTION_FSFS_REQ>" +
                                        "</S:Body>" +
                                    "</S:Envelope>";

            }
            else
            {
                string RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ = ConfigurationManager.AppSettings["RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ"];
                string RT_SERVICE_CR_SOURCE = ConfigurationManager.AppSettings["RT_SERVICE_CR_SOURCE"];
                string RT_SERVICE_CR_UBSCOMP = ConfigurationManager.AppSettings["RT_SERVICE_CR_UBSCOMP"];
                string RT_SERVICE_CR_USERID = ConfigurationManager.AppSettings["RT_SERVICE_CR_USERID"];
                string RT_SERVICE_CR_BRANCH = ConfigurationManager.AppSettings["RT_SERVICE_CR_BRANCH"];
                string RT_SERVICE_CR_MODULEID = ConfigurationManager.AppSettings["RT_SERVICE_CR_MODULEID"];
                string RT_SERVICE_CR_SERVICE = ConfigurationManager.AppSettings["RT_SERVICE_CR_SERVICE"];
                string RT_SERVICE_CR_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_CR_OPERATION"];
                string RT_SERVICE_CR_SOURCE_OPERATION = ConfigurationManager.AppSettings["RT_SERVICE_CR_SOURCE_OPERATION"];

                //string RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_PRD"];
                string RT_SERVICE_CR_PRD = string.Empty;

                if (bankCode.Equals(OriginalBankCode.UCBL))
                {
                    RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_IN"];
                }
                else
                {
                    RT_SERVICE_CR_PRD = ConfigurationManager.AppSettings["RT_SERVICE_CR_PRD"];
                }


                strRTServiceContent =
                                        "<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                            "<S:Body>" +                    
                                                "<CREATETRANSACTION_FSFS_REQ xmlns=\"" + RT_SERVICE_CR_CREATETRANSACTION_FSFS_REQ + "\">" +
                                                "<FCUBS_HEADER>" +
                                                "<SOURCE>" + RT_SERVICE_CR_SOURCE + "</SOURCE>" +
                                                "<UBSCOMP>" + RT_SERVICE_CR_UBSCOMP + "</UBSCOMP>" +
                                                "<MSGID />" +
                                                "<CORRELID />" +
                                                "<USERID>" + RT_SERVICE_CR_USERID + "</USERID>" +
                                                "<BRANCH>" + RT_SERVICE_CR_BRANCH + "</BRANCH>" +
                                                "<MODULEID>" + RT_SERVICE_CR_MODULEID + "</MODULEID>" +
                                                "<SERVICE>" + RT_SERVICE_CR_SERVICE + "</SERVICE>" +
                                                "<OPERATION>" + RT_SERVICE_CR_OPERATION + "</OPERATION>" +
                                                "<SOURCE_OPERATION>" + RT_SERVICE_CR_SOURCE_OPERATION + "</SOURCE_OPERATION>" +
                                                "<SOURCE_USERID /> " +
                                                "<DESTINATION /> " +
                                                "<MULTITRIPID /> " +
                                                "<FUNCTIONID /> " +
                                                "<ACTION /> " +
                                                "<MSGSTAT /> " +
                                                "<PASSWORD /> " +
                                                "<ADDL>" +
                                                    "<PARAM>" +
                                                        "<NAME /> " +
                                                        "<VALUE /> " +
                                                    "</PARAM>" +
                                                "</ADDL>" +
                                                "</FCUBS_HEADER>" +
                                                "<FCUBS_BODY>" +
                                                    "<Transaction-Details>" +
                                                        "<XREF>" + NRBRT_XREF + "</XREF>" +
                                                        "<FCCREF />" +
                                                        "<PRD>" + RT_SERVICE_CR_PRD + "</PRD>" +
                                                        "<BRN>" + RT_SERVICE_CR_BRANCH + "</BRN>" +
                                                        "<MODULE>" + RT_SERVICE_CR_MODULEID + "</MODULE>" +
                                                        "<TXNBRN>" + NRBRT_TXNACC.Substring(0, 3) + "</TXNBRN>" +
                                                        "<TXNACC>" + NRBRT_TXNACC + "</TXNACC>" +
                                                        "<TXNCCY>BDT</TXNCCY>" +
                                                        "<TXNAMT>" + NRBRT_TXNAMT + "</TXNAMT>" +
                                                        "<NARRATIVE>" + EntryDesc + "</NARRATIVE>" +
                                                    "</Transaction-Details>" +
                                                "</FCUBS_BODY>" +
                                                "</CREATETRANSACTION_FSFS_REQ>" +
                                            "</S:Body>" +
                                        "</S:Envelope>";

            }
            return strRTServiceContent;
        }
    }
}