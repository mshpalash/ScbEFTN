using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Data.SqlClient;
using System.Text;
using EFTN.Utility;

namespace EFTN.BLL
{
    public class DishonorSentXML
    {
        private int batchCount = 0;
        private int entryAddendaCount = 0;
        private long entryHash = 0;
        private long DebitAmount = 0;
        private long CreditAmount = 0;
        private string transactionCode = string.Empty;

        public string GetFHRXML(int fileID, string currency)
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
                            + "<Currency>" + currency + "</Currency> "
                            + "<Session></Session>"
                            + "</FHR>";
            return fHRXML;
        }
        public string CreateBatchXML(Guid TransactionID, string fileName)
        {
            StringBuilder strAllXML = new StringBuilder();

            strAllXML.Append(GetBHRXML(TransactionID));
            strAllXML.Append(GetEDRXML(TransactionID));
            strAllXML.Append(GetBCRXML(TransactionID, fileName));
            return strAllXML.ToString();

        }

        private string GetBHRXML(Guid TransactionID)
        {
            string bHRXML = string.Empty;
            EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
            //SqlDataReader sqlDRBHR = sentBatchDB.GetBatchSent(TransactionID);

            DataTable dtBHR = new DataTable();
            dtBHR = sentBatchDB.GetBatchSent(TransactionID);

            //if (sqlDRBHR.Read())
            for (int bhrCount = 0; bhrCount < dtBHR.Rows.Count; bhrCount++)
            {
                this.batchCount++;
                bHRXML = "<BHR>"
                        + "<ServiceClassCode>" + dtBHR.Rows[bhrCount]["ServiceClassCode"].ToString() + "</ServiceClassCode>"
                        + "<CompanyName>" + dtBHR.Rows[bhrCount]["CompanyName"].ToString() + "</CompanyName>"
                        + "<CompanyDiscretionaryData />"
                        + "<CompanyId>" + dtBHR.Rows[bhrCount]["CompanyId"].ToString() + "</CompanyId>"
                        + "<SECC>" + dtBHR.Rows[bhrCount]["SECC"].ToString() + "</SECC>"
                        + "<CompanyEntryDesc>" + dtBHR.Rows[bhrCount]["EntryDesc"].ToString() + "</CompanyEntryDesc>"
                        + "<CompanyDescDate/>"
                        + "<EffectiveEntryDate>" + DateTime.Now.ToString("yyMMdd") + "</EffectiveEntryDate>"
                        + "<SettlementJDate></SettlementJDate>"
                        + "<OrigStatusCode>1</OrigStatusCode>"
                        + "<OrigBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + "</OrigBank>"
                        //+ "<BatchNumber>" + this.batchCount.ToString().PadLeft(6, '0') + "</BatchNumber>"
                        + "<BatchNumber>" + dtBHR.Rows[bhrCount]["BatchNumber"].ToString() + "</BatchNumber>"
                        + "</BHR>"
                ;
            }
            return bHRXML;
        }

        public string GetEDRXML(Guid TransactionID)
        {
            StringBuilder eDRXML = new StringBuilder();
            EFTN.component.SentDishonorDB SentDishonorDB = new EFTN.component.SentDishonorDB();
            DataTable dtEDR = SentDishonorDB.GetDishonorSent_By_SentBatchID(TransactionID);

            EFTN.Utility.CheckDigitCalculator checkDigitCalculator = new EFTN.Utility.CheckDigitCalculator();

            //while (sqlDREDR.Read())
            for (int edrCount = 0; edrCount < dtEDR.Rows.Count; edrCount++)
            {
                this.entryAddendaCount += 2;
                long entryHash = Int64.Parse(dtEDR.Rows[edrCount]["ReceivingBankRoutingNo"].ToString().Substring(0, 8));
                this.entryHash += entryHash;
                long amount = (long)(((decimal)dtEDR.Rows[edrCount]["Amount"]) * 100);
                this.CreditAmount += amount;
                string secc = dtEDR.Rows[edrCount]["SECC"].ToString();
                string idXml = GetIdXml(secc, dtEDR.Rows[edrCount]["IdNumber"].ToString());
                string nameXml = GetNameXml(secc, dtEDR.Rows[edrCount]["ReceiverName"].ToString());

                string traceNo = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + dtEDR.Rows[edrCount]["TraceNumber"].ToString().Substring(8, 7);

                EFTN.BLL.TransactionCodeGenerator transactionCodeGenerator = new TransactionCodeGenerator();
                transactionCode = transactionCodeGenerator.GetTransactionCodeForReturn(dtEDR.Rows[edrCount]["TransactionCode"].ToString());

                string EDRdata = "<EDR>"
                               + "<TransactionCode>" + transactionCode + "</TransactionCode>"
                                + "<ReceivingBank>" + dtEDR.Rows[edrCount]["ReceivingBankRoutingNo"].ToString().Substring(0, 8) + "</ReceivingBank>"
                                + "<CheckDigit>" + checkDigitCalculator.GetCheckDigit(dtEDR.Rows[edrCount]["ReceivingBankRoutingNo"].ToString()) + "</CheckDigit>"
                                + "<DFIAccountNum>" + dtEDR.Rows[edrCount]["DFIAccountNo"].ToString() + "</DFIAccountNum>"
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
                                    + "<DishonouredReturnReason>" + dtEDR.Rows[edrCount]["DishonorReason"].ToString() + "</DishonouredReturnReason>"
                                    + "<OriginalTraceNumber>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + dtEDR.Rows[edrCount]["OriginalTraceNumber"].ToString().Substring(8) + "</OriginalTraceNumber>"
                                    + "<OriginalReceivingBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + "</OriginalReceivingBank>"
                                    + "<ReturnTraceNumber>" + dtEDR.Rows[edrCount]["ReturnTraceNumber"].ToString() + "</ReturnTraceNumber>"
                                    + "<ReturnSettlementDate>090</ReturnSettlementDate>"
                                    + "<ReturnReason>" + dtEDR.Rows[edrCount]["ReturnCode"].ToString() + "</ReturnReason>"
                                    + "<AddendaInformation>" + dtEDR.Rows[edrCount]["AddendaInfo"].ToString() + "</AddendaInformation>"

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

        private string GetBCRXML(Guid TransactionID, string fileName)
        {
            EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
            SetSettlementDate setSettlementDate = new SetSettlementDate();
            DateTime DishonorSentSettlementDate = setSettlementDate.GetOutwardTransactionSettlementDate();

            //string bCRXML = sentBatchDB.GetBatchControlXMLDishonor(TransactionID, DishonorSentSettlementDate);

            //return bCRXML;


            string bCRXML = string.Empty;

            if (transactionCode.Equals("21")
                    || transactionCode.Equals("31")
                    || transactionCode.Equals("41")
                    || transactionCode.Equals("51")
                    )
            {
                bCRXML = sentBatchDB.GetBatchControlXMLDishonor(TransactionID, DishonorSentSettlementDate, fileName);
            }
            else
            {
                bCRXML = sentBatchDB.GetBatchControlXMLDishonor_ForDebit(TransactionID, DishonorSentSettlementDate, fileName);
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

        #region OLD_Code_Block_for_CreateDishonorSentXml
        //public void CreateDishonorSentXml(string xmlData, int disHonorBatchCount)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    string headerXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><EFT>";
        //    string footerXML = "</EFT>";

        //    doc.LoadXml(headerXML + xmlData + footerXML);
        //    string PBMPath = ConfigurationManager.AppSettings["EFTDishonouredReturnExport"];
        //    DateTime dt = new DateTime();
        //    dt = System.DateTime.Now;
        //    string fileName = "EFT-DsH-D" + DateTime.Now.ToString("yyMMdd") + "-T" + DateTime.Now.ToString("HHmmss") + disHonorBatchCount.ToString() + ".XML";
        //    PBMPath = PBMPath + fileName;
        //    if (System.IO.File.Exists(PBMPath))
        //    {
        //        System.IO.File.Delete(PBMPath);
        //    }
        //    doc.Save(PBMPath);

        //}
        #endregion

        public void CreateDishonorSentXml(string xmlData, string xmlFileName)
        {
            XmlDocument doc = new XmlDocument();
            string headerXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><EFT>";
            string footerXML = "</EFT>";

            doc.LoadXml(headerXML + xmlData + footerXML);
            string PBMPath = ConfigurationManager.AppSettings["EFTDishonouredReturnExport"];
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            //string fileName = "EFT-DsH-D" + DateTime.Now.ToString("yyMMdd") + "-T" + DateTime.Now.ToString("HHmmss") + disHonorBatchCount.ToString() + ".XML";
            string fileName = xmlFileName;
            PBMPath = PBMPath + fileName;
            if (System.IO.File.Exists(PBMPath))
            {
                System.IO.File.Delete(PBMPath);
            }
            doc.Save(PBMPath);

        }
    }
}
