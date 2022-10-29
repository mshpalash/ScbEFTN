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
using EFTN.Utility;

namespace EFTN.BLL
{
    public class RNOCReceivedXML
    {
        public void SaveDataToDB(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            string allXml = doc.OuterXml;
            allXml = allXml.Replace("<BHR>", "<Bundle><BHR>");
            allXml = allXml.Replace("</BCR>", "</BCR></Bundle>");
            doc.LoadXml(allXml);

            //System.Text.StringBuilder dataFromXML = new System.Text.StringBuilder();

            foreach (XmlElement elemBundle in doc.GetElementsByTagName("Bundle"))
            {
                string NOCTraceNumber = string.Empty;
                string RefusedCode = string.Empty;
                string CORTraceSeqNum = string.Empty;
                string CorrectedData = string.Empty;
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
                    NOCTraceNumber = elemEDR.GetElementsByTagName("TraceNumber").Item(0).InnerText;
                    edrCounter++;
                    int adrCounter = 0;
                    foreach (XmlElement elemADR in elemBundle.GetElementsByTagName("ADR"))
                    {
                        adrCounter++;
                        if (edrCounter == adrCounter)
                        {
                            RefusedCode = elemADR.GetElementsByTagName("RefusedCORCode").Item(0).InnerText;
                            CORTraceSeqNum = elemADR.GetElementsByTagName("CORTraceSeqNum").Item(0).InnerText;
                            CorrectedData = elemADR.GetElementsByTagName("CorrectedData").Item(0).InnerText;
                            EFTN.component.ReceivedRNOCDB receivedRNOCDB = new EFTN.component.ReceivedRNOCDB();
                            receivedRNOCDB.InsertReceivedRNOC(NOCTraceNumber, RefusedCode, CorrectedData, CORTraceSeqNum, settlementJdate);
                        }
                    }
                }
            }
            //System.IO.File.WriteAllText(@"f:\test db\NOCReceivedXMLTest.txt", dataFromXML.ToString());
            System.IO.File.Delete(filePath);
        }

        private string GetIdTag(string secc)
        {
            string tagName = "";

            switch (secc)
            {
                case "CIE":
                    tagName = "IndividualId";

                    break;

                case "PPD":
                    tagName = "IndividualId";

                    break;

                case "CCD":
                    tagName = "IdNumber";

                    break;

                case "CTX":
                    tagName = "IdNumber";

                    break;

            }
            return tagName;
        }

        private string GetNameTag(string secc)
        {
            string tagName = "";


            switch (secc)
            {
                case "CIE":
                    tagName = "ReceiverName";

                    break;

                case "PPD":
                    tagName = "IndividualName";

                    break;

                case "CCD":
                    tagName = "ReceiverName";

                    break;

                case "CTX":
                    tagName = "ReceivingCompanyId";

                    break;
            }
            return tagName;
        }
    }
}