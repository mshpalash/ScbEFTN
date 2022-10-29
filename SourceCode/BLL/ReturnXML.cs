using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Data.SqlClient;
using System.Text;
using EFTN.Utility;

namespace EFTN.BLL
{
    public class ReturnXML
    {
        private int batchCount = 0;
        private int entryAddendaCount = 0;
        private long entryHash = 0;
        private long DebitAmount = 0;
        private long CreditAmount = 0;
        private int batchNumber = 1;
        private string transactionCode = string.Empty;


        public string GetFHRXML(int fileID, ref DateTime lastXmlCreationTime, ref DateTime newXMLCreationTime, string currency, string session)
        {
            /*
            if (fileID > 35)
            {
                fileID = fileID - 35;
            }
            */
            int fileIDModifierNext = (fileID % 35);
            if (fileIDModifierNext == 0)
            {
                fileIDModifierNext = 35;
            }

            int fileIDModifierTime = ((fileID - 1) / 35);
            //Global.lastFileCreatedTime = System.DateTime.Now.AddMinutes(fileIDModifierTime).ToString("yyyyMMddHHmm");
            newXMLCreationTime = lastXmlCreationTime.AddMinutes(fileIDModifierTime);
            Global.lastFileCreatedTime = newXMLCreationTime.ToString("yyyyMMddHHmm");

            string fileIDModifier = new EFTN.BLL.FileIDModifierCalculator().GetFileIDModifier(fileID);
            string fHRXML = "<FHR>"
                            + "<PriorityCode>01</PriorityCode>"
                            + "<ImmediateDestination>" + ConfigurationManager.AppSettings["BangladeshBankRouting"] + "</ImmediateDestination> "
                            + "<ImmediateOrigin>" + ConfigurationManager.AppSettings["OriginBank"] + "</ImmediateOrigin> "
                            + "<CreationDate>" + DateTime.Now.ToString("yyMMdd") + "</CreationDate> "
                            + "<CreationTime>" + Global.lastFileCreatedTime.Substring(8, 4) + "</CreationTime> "
                            + "<FileIdModifier>" + fileIDModifier + "</FileIdModifier> "
                            + "<FormatCode>1</FormatCode>"
                            + "<ImmediateDestinationName>Bangladesh Bank</ImmediateDestinationName>"
                            + "<ImmediateOriginName>" + ConfigurationManager.AppSettings["ImmediateOriginName"] + "</ImmediateOriginName> "
                            + "<ReferenceCode />"
                            + "<Currency>" + currency + "</Currency> "
                            + "<Session>" + session + "</Session>"
                            + "</FHR>";
            return fHRXML;
        }
        public string CreateBatchXML(Guid TransactionID, string xmlFileName)
        {
            StringBuilder strAllXML = new StringBuilder();

            strAllXML.Append(GetBHRXML(TransactionID));
            strAllXML.Append(GetEDRXML(TransactionID));
            strAllXML.Append(GetBCRXML(TransactionID, xmlFileName));
            return strAllXML.ToString();

        }

        private string GetBHRXML(Guid TransactionID)
        {
            string bHRXML = string.Empty;
            EFTN.component.ReceivedBatchDB receivedBatchDB = new EFTN.component.ReceivedBatchDB();
            //SqlDataReader sqlDRBHR = receivedBatchDB.GetBatchReceived_By_BatchID(TransactionID);

            DataTable dtBR = new DataTable();
            dtBR = receivedBatchDB.GetBatchReceived_By_BatchID(TransactionID);


            for (int bhrCount = 0; bhrCount < dtBR.Rows.Count; bhrCount++)
            {
                this.batchCount++;
                bHRXML = "<BHR>"
                        + "<ServiceClassCode>" + ConfigurationManager.AppSettings["ServiceClassCode"].ToString() + "</ServiceClassCode>"
                        + "<CompanyName>" + System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtBR.Rows[bhrCount]["CompanyName"].ToString())) + "</CompanyName>"
                        + "<CompanyDiscretionaryData />"
                        + "<CompanyId>" + System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtBR.Rows[bhrCount]["CompanyId"].ToString())) + "</CompanyId>"
                        + "<SECC>" + dtBR.Rows[bhrCount]["SECC"].ToString() + "</SECC>"
                        + "<CompanyEntryDesc>" + System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtBR.Rows[bhrCount]["EntryDesc"].ToString())) + "</CompanyEntryDesc>"
                        + "<CompanyDescDate/>"
                        + "<EffectiveEntryDate>" + DateTime.Now.ToString("yyMMdd") + "</EffectiveEntryDate>"
                        + "<SettlementJDate></SettlementJDate>" //changed on 2011-02-24 by tazim
                                                                //+ "<SettlementJDate>" + (System.DateTime.Now.DayOfYear).ToString().PadLeft(3, '0') + "</SettlementJDate>"
                        + "<OrigStatusCode>1</OrigStatusCode>"
                        + "<OrigBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + "</OrigBank>"
                        + "<BatchNumber>" + this.batchNumber.ToString().PadLeft(6, '0') + "</BatchNumber>"
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
            EFTN.component.SentReturnDB sentReturnDB = new EFTN.component.SentReturnDB();
            DataTable dtEDR = sentReturnDB.GetSentRR_By_ReceivedBatchID(TransactionID);
            EFTN.Utility.CheckDigitCalculator checkDigitCalculator = new EFTN.Utility.CheckDigitCalculator();

            //while (sqlDREDR.Read())
            for (int edrCount = 0; edrCount < dtEDR.Rows.Count; edrCount++)
            {
                this.entryAddendaCount += 2;
                long entryHash = Int64.Parse(dtEDR.Rows[edrCount]["SendingBankRoutNo"].ToString().Substring(0, 8));
                this.entryHash += entryHash;
                long amount = (long)(((decimal)dtEDR.Rows[edrCount]["Amount"]) * 100);
                this.CreditAmount += amount;
                string secc = dtEDR.Rows[edrCount]["SECC"].ToString();
                string idXml = GetIdXml(secc, System.Security.SecurityElement.Escape(dtEDR.Rows[edrCount]["IdNumber"].ToString()));
                string nameXml = GetNameXml(secc, System.Security.SecurityElement.Escape(dtEDR.Rows[edrCount]["ReceiverName"].ToString().Trim().Replace("(", "").Replace(")", "")));

                string traceNo = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + dtEDR.Rows[edrCount]["TraceNumber"].ToString().Substring(8, 7);

                EFTN.BLL.TransactionCodeGenerator transactionCodeGenerator = new TransactionCodeGenerator();
                transactionCode = transactionCodeGenerator.GetTransactionCodeForReturn(dtEDR.Rows[edrCount]["TransactionCode"].ToString());
                string dateOfDeath = string.Empty;
                if (dtEDR.Rows[edrCount]["ReturnCode"].ToString().Equals("R15") || dtEDR.Rows[edrCount]["ReturnCode"].ToString().Equals("R14"))
                {
                    dateOfDeath = "<DateOfDeath>" + dtEDR.Rows[edrCount]["DateOfDeath"].ToString() + "</DateOfDeath>";
                }
                else
                {
                    dateOfDeath = "<DateOfDeath />";
                }


                string EDRdata = "<EDR>"
                               + "<TransactionCode>" + transactionCode + "</TransactionCode>"
                                + "<ReceivingBank>" + dtEDR.Rows[edrCount]["SendingBankRoutNo"].ToString().Substring(0, 8) + "</ReceivingBank>"
                                + "<CheckDigit>" + checkDigitCalculator.GetCheckDigit(dtEDR.Rows[edrCount]["SendingBankRoutNo"].ToString()) + "</CheckDigit>"
                                + "<DFIAccountNum>" + System.Security.SecurityElement.Escape(SpecialCharacterRemover.RemoveSpecialCharacter(dtEDR.Rows[edrCount]["DFIAccountNo"].ToString())) + "</DFIAccountNum>"
                                + "<Amount>" + amount.ToString().PadLeft(12, '0') + "</Amount>"
                                + idXml
                                + GetAdrCountXML(secc)
                                + nameXml
                                + " <DiscretionaryData /> "
                                + "<ADRIndicator>1</ADRIndicator> "
                                + "<TraceNumber>" + traceNo + "</TraceNumber>"
                                + "</EDR>"
                                ;
                string ADRdata = "<ADR>"
                                    + "<AddendaTypeCode>99</AddendaTypeCode>"
                                    + "<ReturnReason>" + dtEDR.Rows[edrCount]["ReturnCode"].ToString() + "</ReturnReason>"
                                    + "<OriginalTraceNumber>" + dtEDR.Rows[edrCount]["OriginalTraceNumber"].ToString() + "</OriginalTraceNumber>"
                                    + dateOfDeath
                                    + "<OriginalReceivingBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + "</OriginalReceivingBank>"
                                    + "<AddendaInformation>1234</AddendaInformation>"
                                    + "<TraceNumber>" + traceNo + "</TraceNumber>"
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
                    tagXml = "<ADRCount>0</ADRCount>";
                    break;
            }
            return tagXml;
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

        private string GetBCRXML(Guid TransactionID, string xmlFileName)
        {
            SetSettlementDate setSettlementDate = new SetSettlementDate();
            DateTime ReturnSentSettlementDate = setSettlementDate.GetOutwardTransactionSettlementDate();

            EFTN.component.ReceivedBatchDB receivedBatchDB = new EFTN.component.ReceivedBatchDB();

            string bCRXML = string.Empty;

            if (transactionCode.Equals("21")
                    || transactionCode.Equals("31")
                    || transactionCode.Equals("41")
                    || transactionCode.Equals("51")
                    )
            {
                bCRXML = receivedBatchDB.GetBatchControlXMLReturn(TransactionID, ReturnSentSettlementDate, xmlFileName);
            }
            else
            {
                bCRXML = receivedBatchDB.GetBatchControlXMLReturn_ForDebit(TransactionID, ReturnSentSettlementDate, xmlFileName);
            }

            return bCRXML;
        }

        public string GetFCRXML()
        {
            string fCRXML = string.Empty;
            if (transactionCode.Equals("21")
                    || transactionCode.Equals("31")
                    || transactionCode.Equals("41")
                    || transactionCode.Equals("51")
                    )
            {
                fCRXML = "<FCR>"
                                + "<BatchCount>000001</BatchCount>"
                                + "<EntryAddendaCount>" + entryAddendaCount.ToString().PadLeft(8, '0') + "</EntryAddendaCount>"
                                + "<EntryHash>" + entryHash.ToString().PadLeft(10, '0') + "</EntryHash>"
                                + "<DebitAmount>" + DebitAmount.ToString().PadLeft(20, '0') + "</DebitAmount>"
                                + "<CreditAmount>" + CreditAmount.ToString().PadLeft(20, '0') + "</CreditAmount> "
                                + "</FCR>";
            }
            else
            {
                fCRXML = "<FCR>"
                                + "<BatchCount>000001</BatchCount>"
                                + "<EntryAddendaCount>" + entryAddendaCount.ToString().PadLeft(8, '0') + "</EntryAddendaCount>"
                                + "<EntryHash>" + entryHash.ToString().PadLeft(10, '0') + "</EntryHash>"
                                + "<DebitAmount>" + CreditAmount.ToString().PadLeft(20, '0') + "</DebitAmount>"
                                + "<CreditAmount>" + DebitAmount.ToString().PadLeft(20, '0') + "</CreditAmount> "
                                + "</FCR>";
            }

            return fCRXML;
        }

        public void CreateReturnXml(string xmlData, string fileName)
        {
            XmlDocument doc = new XmlDocument();
            string headerXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><EFT>";
            string footerXML = "</EFT>";

            doc.LoadXml(headerXML + xmlData + footerXML);
            string PBMPath = ConfigurationManager.AppSettings["EFTReturnExport"];
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            //string fileName = "EFT-Ret-D" + DateTime.Now.ToString("yyMMdd") + "-T" + DateTime.Now.ToString("HHmmss") + returnBatchCount.ToString() + ".XML";
            PBMPath = PBMPath + fileName;
            if (System.IO.File.Exists(PBMPath))
            {
                System.IO.File.Delete(PBMPath);
            }
            doc.Save(PBMPath);

        }
    }
}
