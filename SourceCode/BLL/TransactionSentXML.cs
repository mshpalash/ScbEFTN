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
    public class TransactionSentXML
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

        public string GetFHRXML(int fileID, ref DateTime lastXmlCreationTime, ref DateTime newXMLCreationTime, string currency)
        {
            /*
            if (fileID > 35)
            {
                fileID = fileID - 35;
            }

            string fileIDModifier = new EFTN.BLL.FileIDModifierCalculator().GetFileIDModifier(fileID);
            */

            //////////////////////////////////////////////////////////////FILE ID MODIFIER CHANGE START
            int fileIDModifierNext = (fileID % 35);
            if (fileIDModifierNext == 0)
            {
                fileIDModifierNext = 35;
            }

            int fileIDModifierTime = ((fileID - 1) / 35);
            //Global.lastFileCreatedTime = System.DateTime.Now.AddMinutes(fileIDModifierTime).ToString("yyyyMMddHHmm");
            newXMLCreationTime = lastXmlCreationTime.AddMinutes(fileIDModifierTime);
            Global.lastFileCreatedTime = newXMLCreationTime.ToString("yyyyMMddHHmm");

            string fileIDModifier = new EFTN.BLL.FileIDModifierCalculator().GetFileIDModifier(fileIDModifierNext);
            //////////////////////////////////////////////////////////////FILE ID MODIFIER CHANGE END
            string fHRXML = "<FHR>"
                            + "<PriorityCode>01</PriorityCode>"
                            + "<ImmediateDestination>" + ConfigurationManager.AppSettings["BangladeshBankRouting"] + "</ImmediateDestination> "
                            + "<ImmediateOrigin>" + ConfigurationManager.AppSettings["OriginBank"] + "</ImmediateOrigin> "
                            + "<CreationDate>" + DateTime.Now.ToString("yyMMdd") + "</CreationDate> "
                            //+ "<CreationTime>" + DateTime.Now.ToString("HHmm") + "</CreationTime> "
                            + "<CreationTime>" + Global.lastFileCreatedTime.Substring(8, 4) + "</CreationTime> "
                            + "<FileIdModifier>" + fileIDModifier + "</FileIdModifier> "
                            + "<FormatCode>1</FormatCode>"
                            + "<ImmediateDestinationName>Bangladesh Bank</ImmediateDestinationName>"
                            + "<ImmediateOriginName>" + ConfigurationManager.AppSettings["ImmediateOriginName"] + "</ImmediateOriginName> "
                            + "<ReferenceCode />"
                            + "<Currency>" + currency + "</Currency> "
                            + "<Session></Session>"
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

        public string CreateBatchXML(Guid TransactionID, ref DateTime lastXmlCreationTime, ref DateTime newXMLCreationTime)
        {
            StringBuilder eDRXML = new StringBuilder();
            SentEDRDB sentEDRDB = new SentEDRDB();
            //SqlDataReader sqlDREDR = sentEDRDB.GetSentEDRByTransactionID(TransactionID);

            DataTable dtEDR = new DataTable();
            dtEDR = sentEDRDB.GetSentEDRByTransactionID(TransactionID);


            int itemCounter = 0;
            StringBuilder strAllXML = new StringBuilder();
            int transactoinSentXmlFileSize = ParseData.StringToInt(ConfigurationManager.AppSettings["xmlFileSize"]);
            string xmlFileName = string.Empty;

            /*  Remittance Upgrades 23 FEB 2020  */
            //string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8);
            string originBank = string.Empty;
            int isRemittance = 0;   // it would be 1 if remittance
            string remittanceRoutingNo = ConfigurationManager.AppSettings["RemittanceRouteNo"];
            if (dtEDR.Rows.Count > 0)
            {
                if (dtEDR.Rows[0]["SendingBankRoutingNumber"].ToString().Equals(remittanceRoutingNo))
                {
                    isRemittance = 1;
                    originBank = remittanceRoutingNo.Substring(0, 8);
                }
                else
                {
                    originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8);
                }

            }

            //////////////////////////////////////////////////////////////////////////////////////////////
            string TraceNumberStart = string.Empty;
            string TraceNumberEnd = string.Empty;
            TransactionXMLFileNameDB txnXMLFileNameDB = new TransactionXMLFileNameDB();
            string connectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            //////////////////////////////////////////////////////////////////////////////////////////////

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

                string traceNo = originBank + dtEDR.Rows[edrCount]["TraceNumber"].ToString().Substring(8, 7);
                //////////////////////////////////////////////////////////////////////////////////////////////
                if (TraceNumberStart.Equals(string.Empty))
                {
                    TraceNumberStart = traceNo;
                }
                TraceNumberEnd = traceNo;
                //////////////////////////////////////////////////////////////////////////////////////////////

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
                                //+ "<OrigBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + "</OrigBank>"
                                + "<OrigBank>" + originBank + "</OrigBank>"     // Remittance Upgrades
                                + "<BatchNumber>" + strBatchNumber + "</BatchNumber>"
                                + "</BHR>"
                        ;
                    }
                    //return bHRXML;
                    string currency = dtBHR.Rows[0]["Currency"].ToString();
                    strAllXML.Append(GetFHRXML(this.fileCounter, ref lastXmlCreationTime, ref newXMLCreationTime, currency));
                    strAllXML.Append(bHRXML);
                    strAllXML.Append(eDRXML);

                    strAllXML.Append(GetBCRXML(strSECC, strServiceClassCode,
                                                strEntryAddendaCount, strEntryHash, strDebitAmount,
                                                strCreditAmount, strCompanyId, strBatchNumber, strBatchType, isRemittance));
                    strAllXML.Append(GetFCRXML(strBatchType));

                    xmlFileName = CreateTransactionSentXML(strAllXML.ToString(), currency);

                    //////////////////////////////////////////////////////////////////////////////////////////////
                    //SAVING XML FILE NAME HERE FOR EACH FILE
                    txnXMLFileNameDB.InsertTransactionSentXMLFiles(TransactionID, strBatchNumber, TraceNumberStart, TraceNumberEnd, xmlFileName, connectionString, itemCounter, this.CreditAmount);
                    //After saving the XML file name clear TraceNumberStart
                    TraceNumberStart = String.Empty;
                    //////////////////////////////////////////////////////////////////////////////////////////////

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
                            //+ "<OrigBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + "</OrigBank>"
                            + "<OrigBank>" + originBank + "</OrigBank>"     // Remittance Upgrades
                            + "<BatchNumber>" + strBatchNumber + "</BatchNumber>"
                            + "</BHR>"
                    ;
                }
                //return bHRXML;
                string currency = dtBHR.Rows[0]["Currency"].ToString();
                strAllXML.Append(GetFHRXML(this.fileCounter, ref lastXmlCreationTime, ref newXMLCreationTime, currency));
                strAllXML.Append(bHRXML);
                strAllXML.Append(eDRXML);

                strAllXML.Append(GetBCRXML(strSECC, strServiceClassCode,
                                            strEntryAddendaCount, strEntryHash, strDebitAmount,
                                            strCreditAmount, strCompanyId, strBatchNumber, strBatchType, isRemittance));
                strAllXML.Append(GetFCRXML(strBatchType));
                xmlFileName = CreateTransactionSentXML(strAllXML.ToString(), currency);
                //////////////////////////////////////////////////////////////////////////////////////////////
                //SAVING XML FILE NAME HERE FOR EACH FILE
                txnXMLFileNameDB.InsertTransactionSentXMLFiles(TransactionID, strBatchNumber, TraceNumberStart, TraceNumberEnd, xmlFileName, connectionString, itemCounter, this.CreditAmount);
                //After saving the XML file name clear TraceNumberStart
                TraceNumberStart = String.Empty;
                //////////////////////////////////////////////////////////////////////////////////////////////
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


        private string GetIdXml(string secc, string value)
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

        private string GetNameXml(string secc, string value)
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
                    // tagXml = "<ADRCount>0001</ADRCount>";// Commented out due to BB PBM Change dated 11-12-2018
                    tagXml = "<ADRCount>001</ADRCount>";
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
                                string CompanyId, string BatchNumber, string batchType, int isRemittance)
        {
            string bCRXML = string.Empty;
            string originBank = string.Empty;

            if (isRemittance.Equals(1))
            {
                originBank = ConfigurationManager.AppSettings["RemittanceRouteNo"];
            }
            else
            {
                originBank = ConfigurationManager.AppSettings["OriginBank"];
            }
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
                             //+ "<OriginBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 9) + "</OriginBank>"  // Remittance Upgrades
                             + "<OriginBank>" + originBank + "</OriginBank>"
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
                             //+ "<OriginBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 9) + "</OriginBank>"  // Remittance Upgrades
                             + "<OriginBank>" + originBank + "</OriginBank>"
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
                                            //+ "<OriginBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 9) + "</OriginBank>"  // Remittance Upgrades
                                            + "<OriginBank>" + originBank + "</OriginBank>"
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
                                            //+ "<OriginBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 9) + "</OriginBank>"  // Remittance Upgrades
                                            + "<OriginBank>" + originBank + "</OriginBank>"
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

        public string CreateTransactionSentXML(string xmlData, string currency)
        {
            XmlDocument doc = new XmlDocument();
            string headerXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><EFT>";
            string footerXML = "</EFT>";

            #region Added New due file naming change from central bank
            string fileName = string.Empty;
            TransactionXMLFileNameDB transactionXmlDb = new TransactionXMLFileNameDB();
            #endregion

            doc.LoadXml(headerXML + xmlData + footerXML);
            string PBMPath = ConfigurationManager.AppSettings["EFTTransactionExport"];
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;

            //Commented out the previous code block according to the change from central bank on file naming convention
            //string fileName = "EFT-TS-D" + DateTime.Now.ToString("yyMMdd") + "-T" + DateTime.Now.ToString("HHmmss") + this.fileCounter.ToString() + ".XML";
            string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            int lastIdentity = transactionXmlDb.GetLastXmlIdentity(1);
            string fileIdentity = lastIdentity.ToString().PadLeft(9, '0');
            if (string.IsNullOrEmpty(fileIdentity))
            {
            }
            else
            {
                fileName = "EFT_" + DateTime.Now.ToString("yyyyMMdd") + "_" + originBank + "_1" + fileIdentity + "_" + currency + ".XML";
            }

            PBMPath = PBMPath + fileName;
            if (File.Exists(PBMPath))
            {
                File.Delete(PBMPath);
            }
            this.fileCounter++;
            doc.Save(PBMPath);
            return fileName;
        }

        public bool IsXMLAlreadyCreatedForTheBatch(Guid TransactionID)
        {
            TransactionSentDB txnSentDB = new TransactionSentDB();

            DataTable dtSentXMLName = txnSentDB.GetXMLFileNameByTransactionID(TransactionID);

            bool isFileExist = false;

            for (int xmlFileCount = 0; xmlFileCount < dtSentXMLName.Rows.Count; xmlFileCount++)
            {
                string strFileName = dtSentXMLName.Rows[xmlFileCount]["XMLFileName"].ToString();

                if (strFileName.Trim().Equals(string.Empty))
                {
                    isFileExist = false;
                }
                else
                {
                    isFileExist = true;
                }
                //if(XMLFileName)
            }
            return isFileExist;
        }
    }
}
