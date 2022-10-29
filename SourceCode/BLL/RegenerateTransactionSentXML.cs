using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using System.Text;
using EFTN.component;
using System.Data.SqlClient;
using EFTN.Utility;

namespace EFTN.BLL
{
    public class RegenerateTransactionSentXML
    {
        private int batchCount = 0;
        private int entryAddendaCount = 0;
        private long entryHash = 0;
        private long DebitAmount = 0;
        private long CreditAmount = 0;
        private string SECC = string.Empty;
        private int fileCounter = 1;
        //private string SECC = string.Empty;
        public int batchCounterOnlyForBatchCount = 0;

        public string GetTransactionSentXML()
        {
            return string.Empty;
        }

        public string GetFHRXML(int fileID)
        {
            if (fileID > 35)
            {
                fileID = fileID - 35;
            }

            string fileIDModifier = new EFTN.BLL.FileIDModifierCalculator().GetFileIDModifier(fileID);
            string fHRXML = "<FHR>"
                            +"<PriorityCode>01</PriorityCode>"
                            + "<ImmediateDestination>" + ConfigurationManager.AppSettings["BangladeshBankRouting"] + "</ImmediateDestination> "
                            + "<ImmediateOrigin>" + ConfigurationManager.AppSettings["OriginBank"] + "</ImmediateOrigin> "
                            + "<CreationDate>" + DateTime.Now.ToString("yyMMdd") + "</CreationDate> "
                            + "<CreationTime>" + DateTime.Now.ToString("HHmm") + "</CreationTime> "
                            + "<FileIdModifier>" + fileIDModifier + "</FileIdModifier> "
                            + "<FormatCode>1</FormatCode>"
                            + "<ImmediateDestinationName>Bangladesh Bank</ImmediateDestinationName>"
                            + "<ImmediateOriginName>" + ConfigurationManager.AppSettings["ImmediateOriginName"] + "</ImmediateOriginName> "
                            + "<ReferenceCode />"
                            + "</FHR>";
            return fHRXML;
        }

        //public void CreateBatchXML(Guid TransactionID)
        //{
        //    StringBuilder strAllXML = new StringBuilder();
        //    string EDRXML = GetEDRXML(TransactionID);

        //    //Checks whether no transaction in the batch
        //    //if (EDRXML != "")
        //    //{
        //    //    strAllXML.Append(GetBHRXML(TransactionID));
        //    //    strAllXML.Append(EDRXML);
        //    //    strAllXML.Append(GetBCRXML(TransactionID));
        //    //}
        //    //return strAllXML.ToString();
        //    //StringBuilder
        //}

        //private string GetBHRXML(Guid TransactionID)
        //{
        //    string bHRXML = string.Empty;
        //    SentBatchDB sentBatchDB = new SentBatchDB();
        //    SqlDataReader sqlDRBHR = sentBatchDB.GetBatchSent(TransactionID);
        //    if (sqlDRBHR.Read())
        //    {
        //        this.batchCount++;
        //        //this.SECC = 
        //        bHRXML = "<BHR>"
        //                +"<ServiceClassCode>" + sqlDRBHR["ServiceClassCode"].ToString() + "</ServiceClassCode>"
        //                + "<CompanyName>" + sqlDRBHR["CompanyName"].ToString() + "</CompanyName>"
        //                + "<CompanyDiscretionaryData />"
        //                + "<CompanyId>" + sqlDRBHR["CompanyId"].ToString() + "</CompanyId>"
        //                + "<SECC>" + sqlDRBHR["SECC"].ToString() +"</SECC>"
        //                + "<CompanyEntryDesc>" + sqlDRBHR["EntryDesc"].ToString() + "</CompanyEntryDesc>"
        //                + "<CompanyDescDate/>"
        //                //+ "<EffectiveEntryDate>" + DateTime.Now.ToString("yyMMdd") + "</EffectiveEntryDate>"
        //                + "<EffectiveEntryDate>" + DateTime.Now.ToString("yyMMdd") + "</EffectiveEntryDate>"
        //                + "<SettlementJDate></SettlementJDate>"
        //                + "<OrigStatusCode>1</OrigStatusCode>"
        //                + "<OrigBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0,8) + "</OrigBank>"
        //                + "<BatchNumber>" + sqlDRBHR["BatchNumber"].ToString() + "</BatchNumber>"
        //                + "</BHR>"
        //        ;
        //    }
        //    return bHRXML;
        //}

        public string CreateBatchXML(Guid TransactionID, int UserID, string EFTConnectionString)
        {
            StringBuilder eDRXML = new StringBuilder();
            RegenerateOutwardTransactionDB sentEDRDB = new RegenerateOutwardTransactionDB();
            //SqlDataReader sqlDREDR = sentEDRDB.GetSentEDRByTransactionID(TransactionID);
            
            DataTable dtEDR = new DataTable();
            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            dtEDR = sentEDRDB.EFTGetSentEDRByBatchSentIDForRegeneration(TransactionID, bankCode, UserID, EFTConnectionString);


            int itemCounter = 0;
            StringBuilder strAllXML = new StringBuilder();
            int transactoinSentXmlFileSize = ParseData.StringToInt(ConfigurationManager.AppSettings["xmlFileSize"]);
            string xmlFileName = string.Empty;

            for (int edrCount = 0; edrCount < dtEDR.Rows.Count; edrCount++)
            {
                itemCounter++;

                this.entryAddendaCount++;
                long entryHash = Int64.Parse(dtEDR.Rows[edrCount]["ReceivingBankRoutingNo"].ToString().Substring(0, 8));
                this.entryHash += entryHash;
                long amount = (long)(((decimal)dtEDR.Rows[edrCount]["Amount"]) * 100);
                this.CreditAmount += amount;
                string secc = dtEDR.Rows[edrCount]["SECC"].ToString();
                string idXml = GetIdXml(secc, System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["IdNumber"].ToString())));
                string nameXml = GetNameXml(secc, System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["ReceiverName"].ToString())));
                string adrIndicatorValue = "1";

                if (secc.Equals("CTX"))
                {
                    this.SECC = "CTX";
                }
                else
                {
                    this.SECC = string.Empty;
                }

                string traceNo = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + dtEDR.Rows[edrCount]["TraceNumber"].ToString().Substring(8, 7);
                string data = "<EDR>"
                               + "<TransactionCode>" + dtEDR.Rows[edrCount]["TransactionCode"].ToString() + "</TransactionCode>"
                                + "<ReceivingBank>" + dtEDR.Rows[edrCount]["ReceivingBankRoutingNo"].ToString().Substring(0, 8) + "</ReceivingBank>"
                                + "<CheckDigit>" + dtEDR.Rows[edrCount]["ReceivingBankRoutingNo"].ToString().Substring(8) + "</CheckDigit>"
                                + "<DFIAccountNum>" + System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["DFIAccountNo"].ToString())) + "</DFIAccountNum>"
                                + "<Amount>" + amount.ToString().PadLeft(12, '0') + "</Amount>"
                                + idXml
                                + GetAdrCountXML(secc)
                                + nameXml
                                + " <DiscretionaryData /> "
                                + "<ADRIndicator>" + adrIndicatorValue + "</ADRIndicator> "
                                + "<TraceNumber>" + traceNo + "</TraceNumber>"
                                + "</EDR>"
                                ;
                eDRXML.Append(data);

                data = string.Empty;
                string paymentInfoXML = System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["PaymentInfo"].ToString()));

                if (secc.Equals("CTX"))
                {
                    this.entryAddendaCount++;
                    long invoiceGrossAmt = (long)(((decimal)dtEDR.Rows[edrCount]["InvoiceGrossAmt"]) * 100);
                    long amountPaid = (long)(((decimal)dtEDR.Rows[edrCount]["InvoiceAmountPaid"]) * 100);
                    long adjustmentAmount = (long)(((decimal)dtEDR.Rows[edrCount]["AdjustmentAmount"]) * 100);

                    string adjustmentDescriptionXml = System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["AdjustmentDescription"].ToString()));

                    data = "<ADR>"
                                + "<AddendaTypeCode>05</AddendaTypeCode>"
                                + "<PaymentInfo>" + paymentInfoXML + "</PaymentInfo>"
                                + "<AddendaSeqNum>0001</AddendaSeqNum>"
                                + "<EntryDetailSeqNum>" + traceNo + "</EntryDetailSeqNum>"
                                + "<InvoiceNumber>" + System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["InvoiceNumber"].ToString())) + "</InvoiceNumber>"
                                + "<InvoiceDate>" + dtEDR.Rows[edrCount]["InvoiceDate"].ToString() + "</InvoiceDate>"
                                + "<InvoiceGrossAmt>" + invoiceGrossAmt.ToString() + "</InvoiceGrossAmt>"
                                + "<AmountPaid>" + amountPaid.ToString() + "</AmountPaid>"
                                + "<PurchaseOrder>" + System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["PurchaseOrder"].ToString())) + "</PurchaseOrder>"
                                + "<AdjustmentAmount>" + adjustmentAmount.ToString() + "</AdjustmentAmount>"
                                + "<AdjustmentCode>" + dtEDR.Rows[edrCount]["AdjustmentCode"].ToString() + "</AdjustmentCode>"
                                + "<AdjustmentDescription>" + adjustmentDescriptionXml + "</AdjustmentDescription>"
                            + "</ADR>";

                    eDRXML.Append(data);
                }
                else
                {
                    this.entryAddendaCount++;

                    data = "<ADR>"
                                + "<AddendaTypeCode>05</AddendaTypeCode>"
                                + "<PaymentInfo>" + paymentInfoXML + "</PaymentInfo>"
                                + "<AddendaSeqNum>0001</AddendaSeqNum>"
                                + "<EntryDetailSeqNum>" + traceNo + "</EntryDetailSeqNum>"
                            + "</ADR>";

                    eDRXML.Append(data);
                }


                if (itemCounter == transactoinSentXmlFileSize)
                {

                    string bHRXML = string.Empty;
                    SentBatchDB sentBatchDB = new SentBatchDB();
                    //SqlDataReader sqlDRBHR = sentBatchDB.GetBatchSent(TransactionID);

                    DataTable dtBHR = new DataTable();
                    dtBHR = sentBatchDB.GetBatchSent(TransactionID);

                    string strServiceClassCode = string.Empty;//paisi
                    string strCompanyId = string.Empty;//paisi
                    string strBatchNumber = string.Empty;//paisi
                    string strSECC = string.Empty;//paisi
                    string strEntryAddendaCount = string.Empty;
                    string strDebitAmount = string.Empty;
                    string strCreditAmount = string.Empty;
                    string strEntryHash = string.Empty;
                    string strBatchType = string.Empty;

                    //if (sqlDRBHR.Read())
                    for (int bhrCount = 0; bhrCount < dtBHR.Rows.Count; bhrCount++)
                    {
                        //this.batchCount++;
                        strServiceClassCode = dtBHR.Rows[bhrCount]["ServiceClassCode"].ToString();
                        strCompanyId = System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtBHR.Rows[bhrCount]["CompanyId"].ToString()));
                        strBatchNumber = dtBHR.Rows[bhrCount]["BatchNumber"].ToString();
                        strSECC = dtBHR.Rows[bhrCount]["SECC"].ToString();
                        strEntryAddendaCount = this.entryAddendaCount.ToString();
                        strCreditAmount = this.CreditAmount.ToString();
                        strEntryHash = this.entryHash.ToString();
                        strBatchType = dtBHR.Rows[bhrCount]["BatchType"].ToString();

                        //this.SECC = 
                        bHRXML = "<BHR>"
                                + "<ServiceClassCode>" + strServiceClassCode + "</ServiceClassCode>"
                                + "<CompanyName>" + System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtBHR.Rows[bhrCount]["CompanyName"].ToString())) + "</CompanyName>"
                                + "<CompanyDiscretionaryData />"
                                + "<CompanyId>" + strCompanyId + "</CompanyId>"
                                + "<SECC>" + strSECC + "</SECC>"
                                + "<CompanyEntryDesc>" + System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtBHR.Rows[bhrCount]["EntryDesc"].ToString())) + "</CompanyEntryDesc>"
                                + "<CompanyDescDate/>"
                            //+ "<EffectiveEntryDate>" + DateTime.Now.ToString("yyMMdd") + "</EffectiveEntryDate>"
                                + "<EffectiveEntryDate>" + DateTime.Now.ToString("yyMMdd") + "</EffectiveEntryDate>"
                                + "<SettlementJDate></SettlementJDate>"
                                + "<OrigStatusCode>1</OrigStatusCode>"
                                + "<OrigBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + "</OrigBank>"
                                + "<BatchNumber>" + strBatchNumber + "</BatchNumber>"
                                + "</BHR>"
                        ;
                    }
                    //return bHRXML;
                    strAllXML.Append(GetFHRXML(this.fileCounter));
                    strAllXML.Append(bHRXML);
                    strAllXML.Append(eDRXML);

                    strAllXML.Append(GetBCRXML(strSECC, strServiceClassCode, 
                                                strEntryAddendaCount, strEntryHash, strDebitAmount, 
                                                strCreditAmount, strCompanyId, strBatchNumber, strBatchType));
                    strAllXML.Append(GetFCRXML(strBatchType));
                    
                    xmlFileName = CreateTransactionSentXML(strAllXML.ToString());

                    itemCounter = 0;
                    entryAddendaCount = 0;
                    this.CreditAmount = 0;
                    this.entryHash = 0;
                    this.entryAddendaCount = 0;
                    eDRXML.Remove(0, eDRXML.Length);
                    strAllXML.Remove(0, strAllXML.Length);
                }
            }

            if (itemCounter != 0)
            {
                string bHRXML = string.Empty;
                SentBatchDB sentBatchDB = new SentBatchDB();
                //SqlDataReader sqlDRBHR = sentBatchDB.GetBatchSent(TransactionID);
                DataTable dtBHR = new DataTable();
                dtBHR = sentBatchDB.GetBatchSent(TransactionID);

                string strServiceClassCode = string.Empty;//paisi
                string strCompanyId = string.Empty;//paisi
                string strBatchNumber = string.Empty;//paisi
                string strSECC = string.Empty;//paisi
                string strEntryAddendaCount = string.Empty;
                string strDebitAmount = string.Empty;
                string strCreditAmount = string.Empty;
                string strEntryHash = string.Empty;
                string strBatchType = string.Empty;

                //if (sqlDRBHR.Read())

                for (int bhrCount = 0; bhrCount < dtBHR.Rows.Count; bhrCount++)
                {
                    //this.batchCount++;
                    strServiceClassCode = dtBHR.Rows[bhrCount]["ServiceClassCode"].ToString();
                    strCompanyId = System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtBHR.Rows[bhrCount]["CompanyId"].ToString()));
                    strBatchNumber = dtBHR.Rows[bhrCount]["BatchNumber"].ToString();
                    strSECC = dtBHR.Rows[bhrCount]["SECC"].ToString();
                    strEntryAddendaCount = this.entryAddendaCount.ToString();
                    strCreditAmount = this.CreditAmount.ToString();
                    strEntryHash = this.entryHash.ToString();
                    strBatchType = dtBHR.Rows[bhrCount]["BatchType"].ToString();

                    //this.SECC = 
                    bHRXML = "<BHR>"
                            + "<ServiceClassCode>" + strServiceClassCode + "</ServiceClassCode>"
                            + "<CompanyName>" + System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtBHR.Rows[bhrCount]["CompanyName"].ToString())) + "</CompanyName>"
                            + "<CompanyDiscretionaryData />"
                            + "<CompanyId>" + strCompanyId + "</CompanyId>"
                            + "<SECC>" + strSECC + "</SECC>"
                            + "<CompanyEntryDesc>" + System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtBHR.Rows[bhrCount]["EntryDesc"].ToString())) + "</CompanyEntryDesc>"
                            + "<CompanyDescDate/>"
                        //+ "<EffectiveEntryDate>" + DateTime.Now.ToString("yyMMdd") + "</EffectiveEntryDate>"
                            + "<EffectiveEntryDate>" + DateTime.Now.ToString("yyMMdd") + "</EffectiveEntryDate>"
                            + "<SettlementJDate></SettlementJDate>"
                            + "<OrigStatusCode>1</OrigStatusCode>"
                            + "<OrigBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + "</OrigBank>"
                            + "<BatchNumber>" + strBatchNumber + "</BatchNumber>"
                            + "</BHR>"
                    ;
                }
                //return bHRXML;
                strAllXML.Append(GetFHRXML(this.fileCounter));
                strAllXML.Append(bHRXML);
                strAllXML.Append(eDRXML);

                strAllXML.Append(GetBCRXML(strSECC, strServiceClassCode,
                                            strEntryAddendaCount, strEntryHash, strDebitAmount,
                                            strCreditAmount, strCompanyId, strBatchNumber, strBatchType));
                strAllXML.Append(GetFCRXML(strBatchType));
                xmlFileName = CreateTransactionSentXML(strAllXML.ToString());

                itemCounter = 0;
                entryAddendaCount = 0;
                this.CreditAmount = 0;
                this.entryHash = 0;
                this.entryAddendaCount = 0;
                eDRXML.Remove(0, eDRXML.Length);
                strAllXML.Remove(0, strAllXML.Length);
            }
            return xmlFileName;
        }


        private string GetIdXml(string secc,string value)
        {
            string tagName = "";
            string tagXml = "";
            switch (secc)
            {
                case "CIE":
                    tagName = "IndividualId";
                    tagXml = "<" + tagName + ">" + value + "</" + tagName + ">";
                    break;

                case "PPD":
                    tagName = "IndividualId";
                    tagXml = "<" + tagName + ">" + value + "</" + tagName + ">";
                    break;

                case "CCD":
                    tagName = "IdNumber";
                    tagXml = "<" + tagName + ">" + value + "</" + tagName + ">";
                    break;

                case "CTX":
                    tagName = "IdNumber";
                    tagXml = "<" + tagName + ">" + value + "</" + tagName + ">";
                    break;

            }
            return tagXml;
        }

        private string GetNameXml(string secc,string value)
        {
            string tagName = "";
            string tagXml = "";

            switch (secc)
            {
                case "CIE":
                    tagName = "ReceiverName";
                    tagXml = "<" + tagName + ">" + value + "</" + tagName + ">";
                    break;

                case "PPD":
                    tagName = "IndividualName";
                    tagXml = "<" + tagName + ">" + value + "</" + tagName + ">";
                    break;

                case "CCD":
                    tagName = "ReceiverName";
                    tagXml = "<" + tagName + ">" + value + "</" + tagName + ">";
                    break;

                case "CTX":
                    tagName = "ReceivingCompanyId";
                    if (value.Length > 16)
                    {
                        value = value.Substring(0, 16);
                    }
                    tagXml = "<" + tagName + ">" + value + "</" + tagName + ">";
                    break;
            }
            return tagXml;
        }

        private string GetAdrCountXML(string secc)
        {
            string tagXml = "";
            switch (secc)
            {
                case "CTX":
                    tagXml = "<ADRCount>0001</ADRCount>";
                    break;
            }
            return tagXml;
        }

        //private string GetBCRXML(Guid TransactionID)
        //{
        //    SentBatchDB sentBatchDB = new SentBatchDB();

        //    string bCRXML = string.Empty;
        //    if (this.SECC.Equals("CTX"))
        //    {
        //        bCRXML = sentBatchDB.GetBatchControlForCTX(TransactionID);
        //    }
        //    else
        //    {
        //        bCRXML = sentBatchDB.GetBatchControl(TransactionID);
        //    }

        //    return bCRXML;
        //}

        private string GetBCRXML(string SECC, string ServiceClassCode, string entryAddendaCount,
                                string entryHash, string DebitAmount, string CreditAmount,
                                string CompanyId, string BatchNumber, string batchType)
        {
            string bCRXML = string.Empty;

            string entryHashForXML = entryHash;

            if (entryHashForXML.Length > 10)
            {
                entryHashForXML = entryHashForXML.Substring((entryHashForXML.Length - 10), 10);
            }

            if (this.SECC.Equals("CTX"))
            {
                if (batchType.Equals(TransactionCodes.EFTTransactionTypeDebit))
                {
                    bCRXML = "<BCR>"
                             + "<ServiceClassCode>" + ServiceClassCode + "</ServiceClassCode>"
                             + "<EntryAddendaCount>" + entryAddendaCount.PadLeft(6, '0') + "</EntryAddendaCount>"
                             + "<EntryHash>" + entryHashForXML.ToString().PadLeft(10, '0') + "</EntryHash>"
                             + "<TotalDebitAmount>" + CreditAmount.ToString().PadLeft(20, '0') + "</TotalDebitAmount>"
                             + "<TotalCreditAmount>" + DebitAmount.ToString().PadLeft(20, '0') + "</TotalCreditAmount> "
                             + "<CompanyId>" + SpecialCharacterRemover.RemoveSpecialCharacter(CompanyId) + "</CompanyId>"
                             + "<MsgAuthCode></MsgAuthCode>"
                             + "<OriginBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 9) + "</OriginBank>"
                             + "<BatchNumber>" + BatchNumber + "</BatchNumber>"
                             + "</BCR>";
                }
                else
                {
                    bCRXML = "<BCR>"
                             + "<ServiceClassCode>" + ServiceClassCode + "</ServiceClassCode>"
                             + "<EntryAddendaCount>" + entryAddendaCount.PadLeft(6, '0') + "</EntryAddendaCount>"
                             + "<EntryHash>" + entryHashForXML.ToString().PadLeft(10, '0') + "</EntryHash>"
                             + "<TotalDebitAmount>" + DebitAmount.ToString().PadLeft(20, '0') + "</TotalDebitAmount>"
                             + "<TotalCreditAmount>" + CreditAmount.ToString().PadLeft(20, '0') + "</TotalCreditAmount> "
                             + "<CompanyId>" + SpecialCharacterRemover.RemoveSpecialCharacter(CompanyId) + "</CompanyId>"
                             + "<MsgAuthCode></MsgAuthCode>"
                             + "<OriginBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 9) + "</OriginBank>"
                             + "<BatchNumber>" + BatchNumber + "</BatchNumber>"
                             + "</BCR>";
                }
            }
            else
            {
                if (batchType.Equals(TransactionCodes.EFTTransactionTypeDebit))
                {
                    bCRXML = "<BCR>"
                                            + "<ServiceClassCode>" + ServiceClassCode + "</ServiceClassCode>"
                                            + "<EntryAddendaCount>" + entryAddendaCount.ToString().PadLeft(6, '0') + "</EntryAddendaCount>"
                                            + "<EntryHash>" + entryHashForXML.ToString().PadLeft(10, '0') + "</EntryHash>"
                                            + "<TotalDebitAmount>" + CreditAmount.ToString().PadLeft(20, '0') + "</TotalDebitAmount>"
                                            + "<TotalCreditAmount>" + DebitAmount.ToString().PadLeft(20, '0') + "</TotalCreditAmount> "
                                            + "<CompanyId>" + CompanyId + "</CompanyId>"
                                            + "<MsgAuthCode></MsgAuthCode>"
                                            + "<OriginBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 9) + "</OriginBank>"
                                            + "<BatchNumber>" + BatchNumber + "</BatchNumber>"
                                            + "</BCR>";
                }
                else
                {
                    bCRXML = "<BCR>"
                                            + "<ServiceClassCode>" + ServiceClassCode + "</ServiceClassCode>"
                                            + "<EntryAddendaCount>" + entryAddendaCount.ToString().PadLeft(6, '0') + "</EntryAddendaCount>"
                                            + "<EntryHash>" + entryHashForXML.ToString().PadLeft(10, '0') + "</EntryHash>"
                                            + "<TotalDebitAmount>" + DebitAmount.ToString().PadLeft(20, '0') + "</TotalDebitAmount>"
                                            + "<TotalCreditAmount>" + CreditAmount.ToString().PadLeft(20, '0') + "</TotalCreditAmount> "
                                            + "<CompanyId>" + CompanyId + "</CompanyId>"
                                            + "<MsgAuthCode></MsgAuthCode>"
                                            + "<OriginBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 9) + "</OriginBank>"
                                            + "<BatchNumber>" + BatchNumber + "</BatchNumber>"
                                            + "</BCR>";
                }
            }

            return bCRXML;
        }
        //private string GetBCRXML(Guid TransactionID)
        //{
        //    SentBatchDB sentBatchDB = new SentBatchDB();

        //    string bCRXML = string.Empty;

        //    //if (this.SECC.Equals("CTX"))
        //    //{
        //    //   bCRXML = "<BCR>"
        //    //            + "<ServiceClassCode>" + sqlDRBHR["ServiceClassCode"].ToString() + "</ServiceClassCode>"
        //    //            + "<EntryAddendaCount>" + entryAddendaCount.ToString().PadLeft(8, '0') + "</EntryAddendaCount>"
        //    //            + "<EntryHash>" + entryHash.ToString().PadLeft(10, '0') + "</EntryHash>"
        //    //            + "<DebitAmount>" + DebitAmount.ToString().PadLeft(20, '0') + "</DebitAmount>"
        //    //            + "<CreditAmount>" + CreditAmount.ToString().PadLeft(20, '0') + "</CreditAmount> "
        //    //            + "<CompanyId>" + sqlDRBHR["CompanyId"].ToString() + "</CompanyId>"
        //    //            + "<OrigStatusCode>1</OrigStatusCode>"
        //    //            + "<OrigBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 9) + "</OrigBank>"
        //    //            + "<BatchNumber>" + sqlDRBHR["BatchNumber"].ToString() + "</BatchNumber>"
        //    //            + "</BHR>";
        //    //}
        //    //else
        //    //{
        //    //    bCRXML = "<BCR>"
        //    //            + "<ServiceClassCode>" + sqlDRBHR["ServiceClassCode"].ToString() + "</ServiceClassCode>"
        //    //            + "<EntryAddendaCount>" + entryAddendaCount.ToString().PadLeft(8, '0') + "</EntryAddendaCount>"
        //    //            + "<EntryHash>" + entryHash.ToString().PadLeft(10, '0') + "</EntryHash>"
        //    //            + "<DebitAmount>" + DebitAmount.ToString().PadLeft(20, '0') + "</DebitAmount>"
        //    //            + "<CreditAmount>" + CreditAmount.ToString().PadLeft(20, '0') + "</CreditAmount> "
        //    //            + "<CompanyId>" + sqlDRBHR["CompanyId"].ToString() + "</CompanyId>"
        //    //            + "<OrigStatusCode>1</OrigStatusCode>"
        //    //            + "<OrigBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 9) + "</OrigBank>"
        //    //            + "<BatchNumber>" + sqlDRBHR["BatchNumber"].ToString() + "</BatchNumber>"
        //    //            + "</BHR>";
        //    //}

        //    return bCRXML;
        //}
        public string GetFCRXML(string batchType)
        {
            string entryHashForXML = entryHash.ToString();

            if (entryHashForXML.Length > 10)
            {
                entryHashForXML = entryHashForXML.Substring((entryHashForXML.Length - 10), 10);
            }

            string fCRXML = string.Empty;
            if (batchType.Equals(TransactionCodes.EFTTransactionTypeDebit))
            {

                fCRXML = "<FCR>"
                                + "<BatchCount>" + batchCounterOnlyForBatchCount.ToString().PadLeft(6, '0') + "</BatchCount>"
                                + "<EntryAddendaCount>" + entryAddendaCount.ToString().PadLeft(8, '0') + "</EntryAddendaCount>"
                                + "<EntryHash>" + entryHashForXML.ToString().PadLeft(10, '0') + "</EntryHash>"
                                + "<DebitAmount>" + CreditAmount.ToString().PadLeft(20, '0') + "</DebitAmount>"
                                + "<CreditAmount>" + DebitAmount.ToString().PadLeft(20, '0') + "</CreditAmount> "
                                + "</FCR>";
            }
            else
            {
                fCRXML = "<FCR>"
                                + "<BatchCount>" + batchCounterOnlyForBatchCount.ToString().PadLeft(6, '0') + "</BatchCount>"
                                + "<EntryAddendaCount>" + entryAddendaCount.ToString().PadLeft(8, '0') + "</EntryAddendaCount>"
                                + "<EntryHash>" + entryHashForXML.ToString().PadLeft(10, '0') + "</EntryHash>"
                                + "<DebitAmount>" + DebitAmount.ToString().PadLeft(20, '0') + "</DebitAmount>"
                                + "<CreditAmount>" + CreditAmount.ToString().PadLeft(20, '0') + "</CreditAmount> "
                                + "</FCR>";
            }
            return fCRXML;
        }

        private string ADRXML(Guid EDRID)
        {
            //this.entryAddendaCount++;
            string aDRXML = string.Empty;

            return aDRXML;
        }

        public string CreateTransactionSentXML(string xmlData)
        {
            XmlDocument doc = new XmlDocument();
            string headerXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><EFT>";
            string footerXML = "</EFT>";

            doc.LoadXml(headerXML+xmlData+ footerXML);
            string PBMPath = ConfigurationManager.AppSettings["EFTTransactionExport"];
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            string fileName = "EFT-Reg-TS-D" + DateTime.Now.ToString("yyMMdd") + "-T" + DateTime.Now.ToString("HHmmss") + this.fileCounter.ToString() + ".XML";
            PBMPath = PBMPath + fileName;
            if (File.Exists(PBMPath))
            {
                File.Delete(PBMPath);
            }
            this.fileCounter++;
            doc.Save(PBMPath);
            return fileName;
        }
    }
}
