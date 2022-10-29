using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Data.SqlClient;
using System.Text;
using EFTN.Utility;
namespace EFTN.BLL
{
    public class NOCSentXML
    {
        private int batchCount = 0;
        private int entryAddendaCount = 0;
        private long entryHash = 0;
        private long DebitAmount = 0;
        private long CreditAmount = 0;


        public string GetFHRXML(int fileID)
        {
            if (fileID > 35)
            {
                fileID = fileID - 35;
            }
            string fileIDModifier = new EFTN.BLL.FileIDModifierCalculator().GetFileIDModifier(fileID);
            string fHRXML = "<FHR>"
                            + "<PriorityCode>01</PriorityCode>"
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

        public string CreateBatchXML(Guid TransactionID, string XMLFileName)
        {
            StringBuilder strAllXML = new StringBuilder();

            strAllXML.Append(GetBHRXML(TransactionID));
            strAllXML.Append(GetEDRXML(TransactionID));
            strAllXML.Append(GetBCRXML(TransactionID, XMLFileName));
            return strAllXML.ToString();

        }

        private string GetBHRXML(Guid TransactionID)
        {
            string bHRXML = string.Empty;
            
            EFTN.component.ReceivedBatchDB receivedBatchDB = new EFTN.component.ReceivedBatchDB();
            //SqlDataReader sqlDRBHR = receivedBatchDB.GetBatchReceived_By_BatchID(TransactionID);
            //if (sqlDRBHR.Read())
            //{
            DataTable dtBR = new DataTable();
            dtBR = receivedBatchDB.GetBatchReceived_By_BatchID(TransactionID);


            for (int bhrCount = 0; bhrCount < dtBR.Rows.Count; bhrCount++)
            {
                this.batchCount++;
                bHRXML = "<BHR>"
                        + "<ServiceClassCode>" + ConfigurationManager.AppSettings["ServiceClassCode"].ToString() + "</ServiceClassCode>"
                        + "<CompanyName>" +  System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtBR.Rows[bhrCount]["CompanyName"].ToString())) + "</CompanyName>"
                        + "<CompanyDiscretionaryData />"
                        + "<CompanyId>" +  System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtBR.Rows[bhrCount]["CompanyId"].ToString())) + "</CompanyId>"
                        + "<SECC>NOC</SECC>"
                        + "<CompanyEntryDesc>" +  System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtBR.Rows[bhrCount]["EntryDesc"].ToString())) + "</CompanyEntryDesc>"
                        + "<CompanyDescDate/>"
                        + "<EffectiveEntryDate>" + DateTime.Now.ToString("yyMMdd") + "</EffectiveEntryDate>"
                        + "<SettlementJDate></SettlementJDate>" //changed on 2011-02-24 by tazim
                        //+ "<SettlementJDate>" + (System.DateTime.Now.DayOfYear).ToString().PadLeft(3, '0') + "</SettlementJDate>"
                        + "<OrigStatusCode>1</OrigStatusCode>"
                        + "<OrigBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + "</OrigBank>"
                        + "<BatchNumber>" + this.batchCount.ToString().PadLeft(6, '0') + "</BatchNumber>"
                        //+ "<BatchNumber>" + dtBR.Rows[bhrCount]["BatchNumber"].ToString() + "</BatchNumber>"
                        + "</BHR>"
                ;
            }
            dtBR.Dispose();
            return bHRXML;
        }
        public string GetEDRXML(Guid TransactionID)
        {
            StringBuilder eDRXML = new StringBuilder();
            EFTN.component.SentNOCDB sentNOCDB = new EFTN.component.SentNOCDB();
            DataTable dtEDR = sentNOCDB.GetSentNOCByReceivedBatchID(TransactionID);
            EFTN.Utility.CheckDigitCalculator checkDigitCalculator = new EFTN.Utility.CheckDigitCalculator();

            //while (sqlDREDR.Read())
            for (int edrCount = 0; edrCount < dtEDR.Rows.Count; edrCount++)
            {
                this.entryAddendaCount += 2;
                long entryHash = Int64.Parse(dtEDR.Rows[edrCount]["SendingBankRoutNo"].ToString().Substring(0, 8));
                this.entryHash += entryHash;
                //long amount = (long)(((decimal)dtEDR.Rows[edrCount]["Amount"]) * 100);
                //this.CreditAmount += amount;
                string secc = dtEDR.Rows[edrCount]["SECC"].ToString();
                string idXml = GetIdXml(secc,  System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["IdNumber"].ToString())));
                string nameXml = GetNameXml(secc, System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["ReceiverName"].ToString().Trim())));

                string traceNo = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + dtEDR.Rows[edrCount]["TraceNumber"].ToString().Substring(8, 7);

                EFTN.BLL.TransactionCodeGenerator transactionCodeGenerator = new TransactionCodeGenerator();
                string transactionCode = transactionCodeGenerator.GetTransactionCodeForReturn(dtEDR.Rows[edrCount]["TransactionCode"].ToString());

                string EDRdata = "<EDR>"
                               //+ "<TransactionCode>" + dtEDR.Rows[edrCount]["TransactionCode"].ToString() + "</TransactionCode>"
                                + "<TransactionCode>" + transactionCode + "</TransactionCode>"
                               //+ "<TransactionCode>26</TransactionCode>"
                                + "<ReceivingBank>" + dtEDR.Rows[edrCount]["SendingBankRoutNo"].ToString().Substring(0, 8) + "</ReceivingBank>"
                                + "<CheckDigit>" + checkDigitCalculator.GetCheckDigit(dtEDR.Rows[edrCount]["SendingBankRoutNo"].ToString()) + "</CheckDigit>"
                                + "<DFIAccountNum>" + System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["DFIAccountNo"].ToString())) + "</DFIAccountNum>"
                                + "<Amount>" +0.ToString().PadLeft(12, '0') + "</Amount>"
                                + idXml
                                + GetAdrCountXML(secc)
                                + nameXml
                                + " <DiscretionaryData /> "
                                + "<ADRIndicator>1</ADRIndicator> "
                                + "<TraceNumber>" + traceNo + "</TraceNumber>"
                                + "</EDR>"
                                ;
                string ADRdata = "<ADR>"
                                    + "<AddendaTypeCode>98</AddendaTypeCode>"
                                    + "<ChangeCode>" + dtEDR.Rows[edrCount]["ChangeCode"].ToString() + "</ChangeCode>"
                                    + "<OriginalEntryTN>" + dtEDR.Rows[edrCount]["OriginalTraceNumber"].ToString() + "</OriginalEntryTN>"
                                    + "<OriginalReceivingBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + "</OriginalReceivingBank>"
                                    + "<CorrectedData>" + (dtEDR.Rows[edrCount]["CorrectedData"].ToString() == "" ? "abc" : System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["CorrectedData"].ToString())))+ "</CorrectedData>"
                                    + "<AddendaSeqNum>0001</AddendaSeqNum>"
                                    + "<EntryDetailSeqNum>" + traceNo + "</EntryDetailSeqNum>"
                                    + "</ADR>";
                eDRXML.Append(EDRdata + ADRdata);

            }

            return eDRXML.ToString();

        }

        private string GetAdrCountXML(string secc)
        {
            string tagXml = "";
            switch (secc)
            {
                case "CTX":
                    tagXml = "";
                    break;
            }
            return tagXml;
        }

        private string GetIdXml(string secc, string value)
        {
            string tagName = "";
            string tagXml = "";

            tagName = "IndividualId";
            tagXml = "<" + tagName + ">" + value + "</" + tagName + ">";
            /*
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
             * */
            return tagXml;
        }

        private string GetNameXml(string secc, string value)
        {
            string tagName = "";
            string tagXml = "";
            tagName = "IndividualName";
            tagXml = "<" + tagName + ">" + value + "</" + tagName + ">";
            /*
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
                    tagXml = "<" + tagName + ">" + value + "</" + tagName + ">";
                    break;
            }
             * */
            return tagXml;
        }

        private string GetBCRXML(Guid TransactionID, string XMLFileName)
        {
            SetSettlementDate setSettlementDate = new SetSettlementDate();
            DateTime NOCSentSettlementDate = setSettlementDate.GetOutwardTransactionSettlementDate();

            EFTN.component.ReceivedBatchDB receivedBatchDB = new EFTN.component.ReceivedBatchDB();
            string bCRXML = receivedBatchDB.GetBatchControlXMLNOC(TransactionID, NOCSentSettlementDate, XMLFileName);

            return bCRXML;
        }

        public string GetFCRXML()
        {
            string fCRXML = "<FCR>"
                            + "<BatchCount>000001</BatchCount>"
                            + "<EntryAddendaCount>" + entryAddendaCount.ToString().PadLeft(8, '0') + "</EntryAddendaCount>"
                            + "<EntryHash>" + entryHash.ToString().PadLeft(10, '0') + "</EntryHash>"
                            + "<DebitAmount>" + DebitAmount.ToString().PadLeft(20, '0') + "</DebitAmount>"
                            + "<CreditAmount>" + CreditAmount.ToString().PadLeft(20, '0') + "</CreditAmount> "
                            + "</FCR>";

            return fCRXML;
        }

        public void CreateNOCXml(string xmlData, string fileName)
        {
            XmlDocument doc = new XmlDocument();
            string headerXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><EFT>";
            string footerXML = "</EFT>";

            doc.LoadXml(headerXML + xmlData + footerXML);
            string PBMPath = ConfigurationManager.AppSettings["EFTNOCExport"];
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            //string fileName = "EFT-NOC-D" + DateTime.Now.ToString("yyMMdd") + "-T" + DateTime.Now.ToString("HHmmss") + nocBatchCount.ToString() + ".XML";
            PBMPath = PBMPath + fileName;
            if (System.IO.File.Exists(PBMPath))
            {
                System.IO.File.Delete(PBMPath);
            }
            doc.Save(PBMPath);
        }
    }
}
