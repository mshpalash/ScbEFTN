using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using EFTN.component;
using EFTN.Utility;
using FloraSoft;
using System.Xml;

namespace EFTN
{
    public partial class CityCardsAccountManagement : System.Web.UI.Page
    {


        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            if (fulExcelFile.HasFile)
            {
                string fileName = fulExcelFile.FileName;
                try
                {
                    string filePath = ConfigurationManager.AppSettings["EFTExcelFiles"] + Guid.NewGuid().ToString() + fileName;

                    fulExcelFile.SaveAs(filePath);

                    XmlDocument doc = new XmlDocument();
                    doc.Load(filePath);
                    //string allXml = doc.OuterXml;  //SBK_NUM
                    CityCardManager cityCardManager = new CityCardManager();

                    string itemCount = string.Empty;
                    string totalAmount = string.Empty;

                    foreach (XmlElement elemRoot in doc.GetElementsByTagName("Root"))
                    {
                        foreach (XmlElement elemRecord in elemRoot.GetElementsByTagName("Record"))
                        {
                            string SenderAccNo = elemRecord.GetElementsByTagName("SBK_NUM").Item(0).InnerText.Trim();
                            decimal Amount = Decimal.Parse(elemRecord.GetElementsByTagName("SBK_SUM").Item(0).InnerText.Trim());

                            cityCardManager.InsertCityCards(SenderAccNo, Amount);
                        }
                    }

                    DataTable dtUploadedXMLData = cityCardManager.GetTransactionSentBeforeTransferForCityCards();

                    totalAmount = ParseData.StringToDouble(dtUploadedXMLData.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                    itemCount = dtUploadedXMLData.Rows.Count.ToString();
                    cityCardManager.TransferToTransactionSent_forCityCards_FromTempTable();

                    System.IO.File.Delete(filePath);

                    lblTransactionCount.Text = "Total Transaction : " + itemCount;
                    lblTransactionAmount.Text = "Total Amount : " + totalAmount;

                    if (dtUploadedXMLData.Rows.Count > 0)
                    {
                        dtgXmlUpload.DataSource = dtUploadedXMLData;
                        dtgXmlUpload.DataBind();
                        SuccessMessage();
                    }
                    else
                    {
                        FailedMessage();
                    }
                }
                catch
                {
                    FailedMessage();
                }
            }
        }

        private void FailedMessage()
        {
            txtMsg.Text = "No valid data found to upload. Please upload valid xml file";
            txtMsg.ForeColor = System.Drawing.Color.Red;
            txtMsg.Visible = true;
        }

        private void SuccessMessage()
        {
            txtMsg.Text = "Data successfully uploaded";
            txtMsg.ForeColor = System.Drawing.Color.Blue;
            txtMsg.Visible = true;
        }

    }
}

