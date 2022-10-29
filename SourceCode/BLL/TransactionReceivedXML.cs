using System;
using System.Data;
using System.Configuration;
using System.Xml;
using EFTN.Utility;
using System.Globalization;

namespace EFTN.BLL
{
    public class TransactionReceivedXML
    {
        public void SaveDataToDB(string filePath, string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            string allXml = doc.OuterXml;
            allXml = allXml.Replace("<BHR>", "<Bundle><BHR>");
            allXml = allXml.Replace("</BCR>", "</BCR></Bundle>");
            doc.LoadXml(allXml);
            EFTN.component.EnvelopeDB envelopDB = new EFTN.component.EnvelopeDB();
            string immediateDestination = doc.GetElementsByTagName("ImmediateDestination").Item(0).InnerText;
            string immediateOrigin = doc.GetElementsByTagName("ImmediateOrigin").Item(0).InnerText;

            //BACH 2 Integration
            int session = 0;
            int settlementJDateValue = 0; //added new
            string strSettlementDate = string.Empty;
            DateTime settlementdate = new DateTime();  //added new
            string currency = doc.GetElementsByTagName("Currency").Item(0).InnerText.Trim();
            string xmlSession = doc.GetElementsByTagName("Session").Item(0).InnerText.Trim();
            if (xmlSession != "")
            {
                session = int.Parse(xmlSession);
            }
            //Added New
            settlementJDateValue = int.Parse(doc.GetElementsByTagName("SettlementJDate").Item(0).InnerText);
            strSettlementDate = doc.GetElementsByTagName("SettlementDate").Item(0).InnerText.Trim();
            settlementdate = DateTime.Parse(DateTime.ParseExact(strSettlementDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));

            int envelopID = envelopDB.InsertEnvelopSent(
                                (int)EFTN.Utility.ItemType.TransactionReceived,
                                immediateDestination, immediateOrigin,
                                "", 0);

            EFTN.component.ReceivedBatchDB receivedBatchDB = new EFTN.component.ReceivedBatchDB();
            EFTN.component.ReceivedEDRDB receivedEDRDB = new EFTN.component.ReceivedEDRDB();


            foreach (XmlElement elemBundle in doc.GetElementsByTagName("Bundle"))
            {
                string serviceClassCode = elemBundle.GetElementsByTagName("ServiceClassCode").Item(0).InnerText.Trim();
                string SECC = elemBundle.GetElementsByTagName("SECC").Item(0).InnerText.Trim();
                string batchNumber = elemBundle.GetElementsByTagName("BatchNumber").Item(0).InnerText.Trim();
                //string settlementJDateString = elemBundle.GetElementsByTagName("SettlementJDate").Item(0).InnerText;
                //int settlementJDateValue = EFTN.Utility.ParseData.StringToInt(settlementJDateString);

                string effectiveEntryDateString = elemBundle.GetElementsByTagName("EffectiveEntryDate").Item(0).InnerText.Trim();
                int year = ParseData.StringToInt("20" + effectiveEntryDateString.Substring(0, 2));
                int mon = ParseData.StringToInt(effectiveEntryDateString.Substring(2, 2));
                int day = ParseData.StringToInt(effectiveEntryDateString.Substring(4, 2));
                DateTime effectiveEntryDate = new DateTime(year, mon, day);
                //                DateTime effectiveEntryDate = DateTime.Parse(mon+"/"+day+"/"+year,CultureInfo.InvariantCulture.DateTimeFormat);
                string companyId = elemBundle.GetElementsByTagName("CompanyId").Item(0).InnerText.Trim();
                string companyName = elemBundle.GetElementsByTagName("CompanyName").Item(0).InnerText.Trim();
                string entryDesc = elemBundle.GetElementsByTagName("CompanyEntryDesc").Item(0).InnerText.Trim();

                //int settlementJDateValue = 0;
                //DateTime settlementJdate = new DateTime();

                foreach (XmlElement elemBHR in elemBundle.GetElementsByTagName("BHR"))
                {
                    settlementJDateValue = ParseData.StringToInt(elemBHR.GetElementsByTagName("SettlementJDate").Item(0).InnerText.Trim());
                }
                
                //DateTime firstDate = new DateTime(DateTime.Now.Year, 1, 1);
                //settlementJdate = firstDate.AddDays(settlementJDateValue - 1);
                ////}
                //if (settlementJdate < System.DateTime.Today)
                //{
                //    settlementJdate = settlementJdate.AddYears(1);
                //}



                Guid transactionId = receivedBatchDB.InsertBatchReceived
                                        (
                                            envelopID,
                                            serviceClassCode,
                                            SECC,
                                            batchNumber,
                                            settlementJDateValue,
                                            effectiveEntryDate,
                                            companyId,
                                            companyName,
                                            entryDesc
                                            ,currency
                                            ,session);

                string originBank = elemBundle.GetElementsByTagName("OriginBank").Item(0).InnerText.Trim().Substring(0, 8);
                string idTag = GetIdTag(SECC);
                string nameTag = GetNameTag(SECC);
                foreach (XmlElement elemEDR in elemBundle.GetElementsByTagName("EDR"))
                {
                    string traceNumber = elemEDR.GetElementsByTagName("TraceNumber").Item(0).InnerText.Trim();
                    string transactionCode = elemEDR.GetElementsByTagName("TransactionCode").Item(0).InnerText.Trim();
                    string DFIAccountNum = elemEDR.GetElementsByTagName("DFIAccountNum").Item(0).InnerText.Trim();
                    decimal amount = Decimal.Parse(elemEDR.GetElementsByTagName("Amount").Item(0).InnerText.Trim()) / 100;
                    string ReceivingBank = string.Empty;
                    if (elemEDR.GetElementsByTagName("ReceivingBank").Item(0) != null)
                    {
                        ReceivingBank = elemEDR.GetElementsByTagName("ReceivingBank").Item(0).InnerText.Trim();
                    }
                    string idNumber =
                        (elemEDR.GetElementsByTagName(idTag).Count != 0) ? elemEDR.GetElementsByTagName(idTag).Item(0).InnerText.Trim() : "";
                    string receiverName =
                        (elemEDR.GetElementsByTagName(nameTag).Count != 0) ? elemEDR.GetElementsByTagName(nameTag).Item(0).InnerText.Trim() : "";

                    int statusId = EFTN.Utility.TransactionStatus.TransReceived_Imported;
                    string paymentInfo = "";
                    string invoiceNumber = string.Empty;
                    string invoiceDate = string.Empty;
                    decimal invoiceGrossAmount = 0;
                    decimal invoiceAmountPaid = 0;
                    string purchaseOrder = string.Empty;
                    decimal adjustmentAmount = 0;
                    string adjustmentCode = string.Empty;
                    string adjustmentDescription = string.Empty;

                    try
                    {
                        foreach (XmlElement elemADR in elemBundle.GetElementsByTagName("ADR"))
                        {
                            if (SECC.Equals("CTX"))
                            {
                                if (elemADR.GetElementsByTagName("InvoiceNumber").Item(0) != null)
                                {
                                    invoiceNumber = elemADR.GetElementsByTagName("InvoiceNumber").Item(0).InnerText.Trim();
                                }
                                if (elemADR.GetElementsByTagName("InvoiceDate").Item(0) != null)
                                {
                                    invoiceDate = elemADR.GetElementsByTagName("InvoiceDate").Item(0).InnerText.Trim();
                                }
                                if (elemADR.GetElementsByTagName("InvoiceGrossAmt").Item(0) != null)
                                {
                                    invoiceGrossAmount = ParseData.StringToDecimal(elemADR.GetElementsByTagName("InvoiceGrossAmt").Item(0).InnerText.Trim()) / 100;
                                }
                                if (elemADR.GetElementsByTagName("AmountPaid").Item(0) != null)
                                {
                                    invoiceAmountPaid = ParseData.StringToDecimal(elemADR.GetElementsByTagName("AmountPaid").Item(0).InnerText.Trim()) / 100;
                                }
                                if (elemADR.GetElementsByTagName("PurchaseOrder").Item(0) != null)
                                {
                                    purchaseOrder = elemADR.GetElementsByTagName("PurchaseOrder").Item(0).InnerText.Trim();
                                }
                                if (elemADR.GetElementsByTagName("AdjustmentAmount").Item(0) != null)
                                {
                                    adjustmentAmount = ParseData.StringToDecimal(elemADR.GetElementsByTagName("AdjustmentAmount").Item(0).InnerText.Trim()) / 100;
                                }
                                if (elemADR.GetElementsByTagName("AdjustmentCode").Item(0) != null)
                                {
                                    adjustmentCode = elemADR.GetElementsByTagName("AdjustmentCode").Item(0).InnerText.Trim();
                                }
                                if (elemADR.GetElementsByTagName("AdjustmentDescription").Item(0) != null)
                                {
                                    adjustmentDescription = elemADR.GetElementsByTagName("AdjustmentDescription").Item(0).InnerText.Trim();
                                }
                            }
                            if (elemADR.GetElementsByTagName("PaymentInfo").Item(0) != null)
                            {
                                paymentInfo = elemADR.GetElementsByTagName("PaymentInfo").Item(0).InnerText.Trim();
                            }
                        }
                    }
                    catch
                    {

                    }

                    //receivedBatchDB.InsertReceivedEDRForCTX(
                    receivedEDRDB.InsertReceivedEDR(
                                        transactionId,
                                        traceNumber,
                                        transactionCode,
                                        DFIAccountNum,
                                        originBank,
                                        amount,
                                        idNumber,
                                        receiverName,
                                        statusId,
                                        paymentInfo,
                                        settlementdate,
                                        invoiceNumber,
                                        invoiceDate,
                                        invoiceGrossAmount,
                                        invoiceAmountPaid,
                                        purchaseOrder,
                                        adjustmentAmount,
                                        adjustmentCode,
                                        adjustmentDescription,
                                        ReceivingBank,
                                        fileName);

                }
            }
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
