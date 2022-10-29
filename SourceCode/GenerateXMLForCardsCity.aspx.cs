using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EFTN.Utility;
using System.Xml;
using System.Text;

namespace EFTN
{
    public partial class GenerateXMLForCardsCity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');
            }

        }

        private DataTable GetTransactionSentForCards()
        {
            CityCardManager cityCardManager = new CityCardManager();

            return cityCardManager.GetCityCardsTransactionSentForXML(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                         + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                         + ddlistDay.SelectedValue.PadLeft(2, '0'));
        }

        protected void btnGenerateXMLTransactionSent_Click(object sender, EventArgs e)
        {
            DataTable dtExportTransaction;

            dtExportTransaction = GetTransactionSentForCards();

            GenerateXMLFile(dtExportTransaction);
        }

        private void GenerateXMLFile(DataTable dt)
        {

            XmlDocument doc = new XmlDocument();
            string headerXML = "<?xml version=\"1.0\" encoding=\"cp866\" ?>";
            headerXML = headerXML + "<Root>";
            string footerXML = "</Root>";
            string xmlData = GetXMLDataFromDataTable(dt);

            doc.LoadXml(headerXML + xmlData + footerXML);
            //string PBMPath = ConfigurationManager.AppSettings["EFTTransactionExport"];

            string fileName = "EFT-TS-Cards-D" + DateTime.Now.ToString("yyMMdd") + "-T" + DateTime.Now.ToString("HHmmss") + ".xml";
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8);

            doc.WriteTo(writer);
            writer.Flush();
            Response.Clear();
            byte[] byteArray = stream.ToArray();
            Response.AppendHeader("Content-Disposition", "filename=" + fileName);
            Response.AppendHeader("Content-Length", byteArray.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(byteArray);
            writer.Close();
        }

        private string GetXMLDataFromDataTable(DataTable data)
        {
            string AccountNo = string.Empty;
            string Amount = string.Empty;
            StringBuilder xmlRecords = new StringBuilder();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                AccountNo = (string)data.Rows[i]["AccountNo"];
                Amount = ParseData.StringToDouble((data.Rows[i]["Amount"]).ToString()).ToString();//(string)data.Rows[i]["Amount"];
                string xmlData = string.Empty;

                xmlData = "<Record>"
                               + "<SBK_NUM>" + AccountNo + "</SBK_NUM>"
                               + "<SBK_MBR>0</SBK_MBR>"
                               + "<SBK_SUM>" + Amount + "</SBK_SUM>"
                               + "<SBK_ACUR>50</SBK_ACUR>"
                               + "<SBK_SHR>Payment</SBK_SHR>"
                               + "<SBK_FLR>Auto Debit Instruction---by fund transfer</SBK_FLR>"
                         + "</Record>";

                xmlRecords.Append(xmlData);
            }

            return xmlRecords.ToString();
        }

        //EFTN.component.TransactionReceivedForCards transactionToCBSDB = new EFTN.component.TransactionReceivedForCards();
        //DataTable dt;
        //string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

        //if (SelectedBank.Equals("215"))
        //{
        //    dt = GetTransactionReceivedForCards();
        //    EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();

        //    string flatfileResult = fc.CreateFlatFileForTransactionReceivedForCardsSCB(dt);
        //    string fileName = "Cards" + "-" + "FlatFile-TR" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
        //    Response.Clear();
        //    Response.AddHeader("content-disposition",
        //             "attachment;filename=" + fileName + ".txt");
        //    Response.Charset = "";
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentType = "application/vnd.text";

        //    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        //    System.Web.UI.HtmlTextWriter htmlWrite =
        //                  new HtmlTextWriter(stringWrite);
        //    Response.Write(flatfileResult.ToString());
        //    Response.End();
        //}

        //Response.Redirect("~/FileWatcherChecker.aspx?PathKey=EFTCBSExport");
    }
}
