using System;
using System.Data;
using System.Configuration;
using System.Xml;
using EFTN.Utility;
namespace EFTN.BLL
{
    public class ReturnReceivedXML
    {
        public void SaveToDB(string filePath, string fileName, string currency, int session)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            string allXml = doc.OuterXml;
            allXml = allXml.Replace("<BHR>", "<Bundle><BHR>");
            allXml = allXml.Replace("</BCR>", "</BCR></Bundle>");
            doc.LoadXml(allXml);
            string strSettlementDate = string.Empty;
            DateTime settlementdate = new DateTime();
            EFTN.component.ReceivedReturnDB receivedReturnDB = new EFTN.component.ReceivedReturnDB();
            foreach (XmlElement elemBundle in doc.GetElementsByTagName("Bundle"))
            {
                //int settlementJDateValue = 0;
                //DateTime settlementJdate = new DateTime();
                //DateTime creationDate = System.DateTime.Now;

                // New change for BACH 2 LIVE Inward Return Settlement Date                
                strSettlementDate = doc.GetElementsByTagName("SettlementDate").Item(0).InnerText.Trim();                
                settlementdate = DateTime.Parse(DateTime.ParseExact(strSettlementDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));

                //if (strSettlementDate != null)
                //{
                //    settlementJdate = DateTime.Parse(DateTime.ParseExact(strSettlementDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd")); 
                //}

                //foreach (XmlElement elemBHR in elemBundle.GetElementsByTagName("BHR"))
                //{
                //    string strCreationDate = elemBHR.GetElementsByTagName("EffectiveEntryDate").Item(0).InnerText;
                //    settlementJdate = new DateTime(
                //                                    ParseData.StringToInt(System.DateTime.Now.Year.ToString().Substring(0, 2)
                //                                        + strCreationDate.Substring(0, 2))
                //                              , ParseData.StringToInt(strCreationDate.Substring(2, 2))
                //                              , ParseData.StringToInt(strCreationDate.Substring(4, 2)));


                //}

                //SetSettlementDate setSettlementDate = new SetSettlementDate();
                //if (session != 1)
                //{
                //    settlementJdate = setSettlementDate.GetInwardSettlementDate(settlementJdate);
                //}
                foreach (XmlElement elemEDR in elemBundle.GetElementsByTagName("EDR"))
                {
                    decimal amount = Decimal.Parse(elemEDR.GetElementsByTagName("Amount").Item(0).InnerText) / 100;

                    XmlElement elemADR = (XmlElement)elemEDR.NextSibling;
                    if (elemADR.Name == "ADR")
                    {
                        string originalTraceNumber = elemADR.GetElementsByTagName("OriginalTraceNumber").Item(0).InnerText;
                        string traceNumber = elemADR.GetElementsByTagName("TraceNumber").Item(0).InnerText;
                        string returnCode = elemADR.GetElementsByTagName("ReturnReason").Item(0).InnerText;
                        string addendaInfo = elemADR.GetElementsByTagName("AddendaInformation").Item(0).InnerText;
                        string dateOfDeath = elemADR.GetElementsByTagName("DateOfDeath").Item(0).InnerText;


                        receivedReturnDB.InsertReturnReceived(
                                            originalTraceNumber,
                                            traceNumber,
                                            returnCode,
                                            addendaInfo,
                                            dateOfDeath,
                                            settlementdate,
                                            amount,
                                            fileName
                                            , currency
                                            , session);
                    }
                }
            }

            System.IO.File.Delete(filePath);

        }
    }
}
