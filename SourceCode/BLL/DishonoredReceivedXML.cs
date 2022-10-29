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
    public class DishonoredReceivedXML
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
            string currency = doc.GetElementsByTagName("Currency").Item(0).InnerText.Trim();
            string sessionId = doc.GetElementsByTagName("Session").Item(0).InnerText.Trim();
            int session = 0;
            if (sessionId != null)
            {
                session = int.Parse(sessionId);
            }
            foreach (XmlElement elemBundle in doc.GetElementsByTagName("Bundle"))
            {
                string ReturnTraceNumber = string.Empty;
                string DishonorReason = string.Empty;
                string AddendaInfo = string.Empty;
                string TraceNumber = string.Empty;

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

                foreach (XmlElement elemADR in elemBundle.GetElementsByTagName("ADR"))
                {
                    ReturnTraceNumber = elemADR.GetElementsByTagName("ReturnTraceNumber").Item(0).InnerText;
                    DishonorReason = elemADR.GetElementsByTagName("DishonouredReturnReason").Item(0).InnerText;
                    AddendaInfo = elemADR.GetElementsByTagName("AddendaInformation").Item(0).InnerText;
                    TraceNumber = elemADR.GetElementsByTagName("TraceNumber").Item(0).InnerText;

                    EFTN.component.ReceivedDishonorDB receivedDishonorDB = new EFTN.component.ReceivedDishonorDB();
                    receivedDishonorDB.InsertDishonorReceived(ReturnTraceNumber, DishonorReason, AddendaInfo, settlementJdate, TraceNumber, currency, session);
                }
            }
            System.IO.File.Delete(filePath);
        }
    }
}
