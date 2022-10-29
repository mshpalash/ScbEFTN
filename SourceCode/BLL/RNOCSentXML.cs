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

namespace EFTN.BLL
{
    public class RNOCSentXML
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
                        + "<SECC>NOC</SECC>"
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
            EFTN.component.RNOCBatchDB rNOCBatchDB = new EFTN.component.RNOCBatchDB();
            DataTable dtEDR = rNOCBatchDB.GetRNOCBySentBatchID(TransactionID);
            EFTN.Utility.CheckDigitCalculator checkDigitCalculator = new EFTN.Utility.CheckDigitCalculator();

            //while (sqlDREDR.Read())
            for (int edrCount = 0; edrCount < dtEDR.Rows.Count; edrCount++)
            {
                this.entryAddendaCount += 2;
                long entryHash = Int64.Parse(dtEDR.Rows[edrCount]["ReceivingBankRoutingNo"].ToString().Substring(0, 8));
                this.entryHash += entryHash;
                //long amount = (long)(((decimal)dtEDR.Rows[edrCount]["Amount"]) * 100);
                //this.CreditAmount += amount;
                string secc = dtEDR.Rows[edrCount]["SECC"].ToString();
                string idXml = GetIdXml(secc, dtEDR.Rows[edrCount]["IdNumber"].ToString());
                string nameXml = GetNameXml(secc, dtEDR.Rows[edrCount]["ReceiverName"].ToString());

                string traceNo = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + dtEDR.Rows[edrCount]["TraceNumber"].ToString().Substring(8, 7);

                EFTN.BLL.TransactionCodeGenerator transactionCodeGenerator = new TransactionCodeGenerator();
                string transactionCode = transactionCodeGenerator.GetTransactionCodeForReturn(dtEDR.Rows[edrCount]["TransactionCode"].ToString());

                string EDRdata = "<EDR>"
                               //+ "<TransactionCode>" + dtEDR.Rows[edrCount]["TransactionCode"].ToString() + "</TransactionCode>"
                               + "<TransactionCode>" + transactionCode + "</TransactionCode>"
                               //+ "<TransactionCode>26</TransactionCode>"
                                + "<ReceivingBank>" + dtEDR.Rows[edrCount]["ReceivingBankRoutingNo"].ToString().Substring(0, 8) + "</ReceivingBank>"
                                + "<CheckDigit>" + checkDigitCalculator.GetCheckDigit(dtEDR.Rows[edrCount]["ReceivingBankRoutingNo"].ToString()) + "</CheckDigit>"
                                + "<DFIAccountNum>" + dtEDR.Rows[edrCount]["DFIAccountNo"].ToString() + "</DFIAccountNum>"
                                + "<Amount>" + 0.ToString().PadLeft(12, '0') + "</Amount>"
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
                                    + "<RefusedCORCode>" + dtEDR.Rows[edrCount]["RefusedCORCode"].ToString() + "</RefusedCORCode>"
                                    + "<OriginalEntryTN>" +  ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8)+dtEDR.Rows[edrCount]["OriginalTraceNumber"].ToString().Substring(8) + "</OriginalEntryTN>"
                                    + "<OriginalReceivingBank>" + ConfigurationManager.AppSettings["OriginBank"].Substring(0, 8) + "</OriginalReceivingBank>"
                                    + "<CorrectedData>" +( dtEDR.Rows[edrCount]["CorrectedData"].ToString() == "" ? "abc" : dtEDR.Rows[edrCount]["CorrectedData"].ToString() )+ "</CorrectedData>"
                                    + "<ChangeCode>" + dtEDR.Rows[edrCount]["ChangeCode"].ToString() + "</ChangeCode>"                                   
                                    + "<CORTraceSeqNum>" + traceNo + "</CORTraceSeqNum>"
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
            */
            return tagXml;
        }

        private string GetBCRXML(Guid TransactionID)
        {
            EFTN.component.RNOCBatchDB rNOCBatchDB = new EFTN.component.RNOCBatchDB();
            string bCRXML = rNOCBatchDB.GetBatchControlXMLRNOC(TransactionID);

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

        public void CreateRNOCXml(string xmlData, int rNOCBatchCount)
        {
            XmlDocument doc = new XmlDocument();
            string headerXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><EFT>";
            string footerXML = "</EFT>";

            doc.LoadXml(headerXML + xmlData + footerXML);
            string PBMPath = ConfigurationManager.AppSettings["EFTRNOCExport"];
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            string fileName = "EFT-RNOC-D" + DateTime.Now.ToString("yyMMdd") + "-T" + DateTime.Now.ToString("HHmmss") + rNOCBatchCount.ToString() + ".XML";
            PBMPath = PBMPath + fileName;
            if (System.IO.File.Exists(PBMPath))
            {
                System.IO.File.Delete(PBMPath);
            }
            doc.Save(PBMPath);
        }
    }
}