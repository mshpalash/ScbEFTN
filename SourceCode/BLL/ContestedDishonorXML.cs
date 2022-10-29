using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using EFTN.Utility;

namespace EFTN.BLL
{
    public class ContestedDishonorXML
    {
        private int batchCount = 0;
        private int entryAddendaCount = 0;
        private long entryHash = 0;
        private long DebitAmount = 0;
        private long CreditAmount = 0;
        private string transactionCode = string.Empty;
        private int batchNumber = 1;


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

        public string CreateBatchXML(Guid TransactionID)
        {
            StringBuilder strAllXML = new StringBuilder();

            strAllXML.Append(GetBHRXML(TransactionID));
            strAllXML.Append(GetEDRXML(TransactionID));
            strAllXML.Append(GetBCRXML(TransactionID));
            return strAllXML.ToString();

        }

        private string GetBHRXML(Guid TransactionID)
        {
            string bHRXML = string.Empty;
            EFTN.component.ContestedBatchDB contestedBatchDB = new EFTN.component.ContestedBatchDB();

            DataTable dtBHR = new DataTable();
            dtBHR = contestedBatchDB.GetContestedSentByReceivedBatchID(TransactionID);

            //if (sqlDRBHR.Read())
            for (int bhrCount = 0; bhrCount < dtBHR.Rows.Count; bhrCount++)
            {

            //SqlDataReader sqlDRBHR = contestedBatchDB.GetContestedSentByReceivedBatchID(TransactionID);
            //if (sqlDRBHR.Read())
            //{
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
                        + "<BatchNumber>" + this.batchNumber.ToString().PadLeft(6, '0') + "</BatchNumber>"
                        //+ "<BatchNumber>" + dtBHR.Rows[bhrCount]["BatchNumber"].ToString() + "</BatchNumber>"
                        + "</BHR>"
                ;
            }
            return bHRXML;
        }
        
        public string GetEDRXML(Guid TransactionID)
        {
            StringBuilder eDRXML = new StringBuilder();

            EFTN.component.ContestedBatchDB contestedBatchDB = new EFTN.component.ContestedBatchDB();
            //SqlDataReader sqlDREDR = contestedBatchDB.GetContestedSentByReceivedBatchID(TransactionID);
            EFTN.Utility.CheckDigitCalculator checkDigitCalculator = new EFTN.Utility.CheckDigitCalculator();

            DataTable dtEDR = new DataTable();
            dtEDR = contestedBatchDB.GetContestedSentByReceivedBatchID(TransactionID);

            //if (sqlDRBHR.Read())
            for (int edrCount = 0; edrCount < dtEDR.Rows.Count; edrCount++)
            {

            //while (sqlDREDR.Read())
            //{
                this.entryAddendaCount += 2;
                long entryHash = Int64.Parse(dtEDR.Rows[edrCount]["SendingBankRoutNo"].ToString().Substring(0, 8));
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
                                + "<ReceivingBank>" + dtEDR.Rows[edrCount]["SendingBankRoutNo"].ToString().Substring(0, 8) + "</ReceivingBank>"
                                + "<CheckDigit>" + checkDigitCalculator.GetCheckDigit(dtEDR.Rows[edrCount]["SendingBankRoutNo"].ToString()) + "</CheckDigit>"
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
                                    + "<ContestedDishonouredRRC>" + dtEDR.Rows[edrCount]["ContestedDishonouredRRC"].ToString() + "</ContestedDishonouredRRC>"
                                    + "<OriginalTraceNumber>" + dtEDR.Rows[edrCount]["OriginalTraceNumber"].ToString() + "</OriginalTraceNumber>"
                                    + "<OriginalReturnDate>" + DateTime.Now.ToString("yyMMdd") + "</OriginalReturnDate>"
                                    + "<OriginalReceivingBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + "</OriginalReceivingBank>"
                                    + "<OriginalSettlementDate>154</OriginalSettlementDate>"
                                    + "<ReturnTraceNumber>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8)+dtEDR.Rows[edrCount]["ReturnTraceNumber"].ToString().Substring(8) + "</ReturnTraceNumber>"
                                    + "<ReturnSettlementDate>156</ReturnSettlementDate>"
                                    + "<ReturnReasonCode>" + dtEDR.Rows[edrCount]["ReturnCode"].ToString() + "</ReturnReasonCode>"
                                    + "<DishonouredRTN>" + dtEDR.Rows[edrCount]["DishonouredRTN"].ToString() + "</DishonouredRTN>"
                                    + "<DishonouredRSD>158</DishonouredRSD>"
                                    + "<DishonouredRRC>" + dtEDR.Rows[edrCount]["DishonouredRRC"].ToString() + "</DishonouredRRC>"
                                    + "<TraceNumber>" + traceNo + "</TraceNumber>"
                                    + "</ADR>";
                eDRXML.Append(EDRdata + ADRdata);
            }

            return eDRXML.ToString();
        }
        
        private string GetAdrCountXML(string secc)
        {
            string tagXml = "";
            string ADRCountPresent = ConfigurationManager.AppSettings["ADRCountPresent"];
            if (ADRCountPresent.Equals("1"))
            {
                switch (secc)
                {
                    case "CTX":
                        tagXml = "<ADRCount>0</ADRCount>";
                        break;
                }
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

        private string GetBCRXML(Guid TransactionID)
        {
            EFTN.component.ContestedBatchDB contestedBatchDB = new EFTN.component.ContestedBatchDB();
            //string bCRXML = contestedBatchDB.GetBatchControlXMLContested(TransactionID, ConfigurationManager.AppSettings["OriginBank"]);
            //return bCRXML;

            string bCRXML = string.Empty;

            if (transactionCode.Equals("21")
                    || transactionCode.Equals("31")
                    || transactionCode.Equals("41")
                    || transactionCode.Equals("51")
                    )
            {
                bCRXML = contestedBatchDB.GetBatchControlXMLContested(TransactionID, ConfigurationManager.AppSettings["OriginBank"]);
            }
            else
            {
                bCRXML = contestedBatchDB.GetBatchControlXMLContested_ForDebit(TransactionID, ConfigurationManager.AppSettings["OriginBank"]);
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

        public void CreateContestedXml(string xmlData, int contestedDisBatchCount)
        {
            XmlDocument doc = new XmlDocument();
            string headerXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><EFT>";
            string footerXML = "</EFT>";

            doc.LoadXml(headerXML + xmlData + footerXML);
            string PBMPath = ConfigurationManager.AppSettings["EFTContestedExport"];
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            string fileName = "EFT-Contested-D" + DateTime.Now.ToString("yyMMdd") + "-T" + DateTime.Now.ToString("HHmmss") + contestedDisBatchCount.ToString() + ".XML";
            PBMPath = PBMPath + fileName;
            if (System.IO.File.Exists(PBMPath))
            {
                System.IO.File.Delete(PBMPath);
            }
            doc.Save(PBMPath);
        }

        public void SaveDataToDB(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            string allXml = doc.OuterXml;
            allXml = allXml.Replace("<BHR>", "<Bundle><BHR>");
            allXml = allXml.Replace("</BCR>", "</BCR></Bundle>");
            doc.LoadXml(allXml);

            foreach (XmlElement elemBundle in doc.GetElementsByTagName("Bundle"))
            {
                int edrCounter = 0;

                int settlementJDateValue = 0;
                DateTime settlementJdate = new DateTime();

                foreach (XmlElement elemBHR in elemBundle.GetElementsByTagName("BHR"))
                {
                    settlementJDateValue = ParseData.StringToInt(elemBHR.GetElementsByTagName("SettlementJDate").Item(0).InnerText);
                }
                if (settlementJDateValue == 1)
                {
                    settlementJdate = new DateTime(1901, 1, 1);
                }
                else
                {
                    DateTime firstDate = new DateTime(DateTime.Now.Year, 1, 1);
                    settlementJdate = firstDate.AddDays(settlementJDateValue - 1);
                }
                foreach (XmlElement elemEDR in elemBundle.GetElementsByTagName("EDR"))
                {
                    edrCounter++;
                    int adrCounter = 0;
                    string ContestedDishonouredRRC = string.Empty;
                    string DishonouredRTN = string.Empty;

                    foreach (XmlElement elemADR in elemBundle.GetElementsByTagName("ADR"))
                    {
                        adrCounter++;
                        if (edrCounter == adrCounter)
                        {
                            ContestedDishonouredRRC = elemADR.GetElementsByTagName("ContestedDishonouredRRC").Item(0).InnerText;
                            DishonouredRTN = elemADR.GetElementsByTagName("DishonouredRTN").Item(0).InnerText;
                            EFTN.component.ContestedDishonorDB contestedDishonorDB = new EFTN.component.ContestedDishonorDB();
                            Guid contestedID = contestedDishonorDB.InsertReceivedContested(ContestedDishonouredRRC, DishonouredRTN, settlementJdate);
                        }
                    }
                }
            }
            System.IO.File.Delete(filePath);
        }
    }
}