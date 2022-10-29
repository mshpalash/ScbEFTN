using System;
using System.Data;
using System.Configuration;
using EFTN.Utility;
using System.Xml;
using System.IO;
using System.Web.UI;

namespace EFTN
{
    public partial class CityCardsFailedList : System.Web.UI.Page
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {

            DataTable dtCardsFailedData = GetData();

            string itemCount = string.Empty;
            string totalAmount = string.Empty;

            totalAmount = ParseData.StringToDouble(dtCardsFailedData.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            itemCount = dtCardsFailedData.Rows.Count.ToString();

            lblTransactionCount.Text = "Total Transaction : " + itemCount;
            lblTransactionAmount.Text = "Total Amount : " + totalAmount;

            if (dtCardsFailedData.Rows.Count > 0)
            {
                dtgXmlUpload.DataSource = dtCardsFailedData;
                dtgXmlUpload.DataBind();
            }
        }

        private DataTable GetData()
        {
            CityCardManager cityCardManager = new CityCardManager();
            string EntryDate = ddlistYear.SelectedValue.PadLeft(4, '0')
                                                            + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                            + ddlistDay.SelectedValue.PadLeft(2, '0');

            return cityCardManager.GetCityCardsFailedTransaction(EntryDate);
        }

        protected void btnExpotToCSV_Click(object sender, EventArgs e)
        {
            DataTable dtCardsFailedData = GetData();

            if (dtCardsFailedData.Rows.Count > 0)
            {
                string xlsFileName = "CardsFailedTransaction" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                string attachment = "attachment; filename=" + xlsFileName + ".csv";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.csv";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                // Create the CSV file to which grid data will be exported. 
                //StreamWriter sw = new StreamWriter();
                int iColCount = dtCardsFailedData.Columns.Count;

                // First we will write the headers. 

                for (int i = 1; i < iColCount; i++)
                {
                    sw.Write("\"");
                    sw.Write(dtCardsFailedData.Columns[i]);
                    if (i < iColCount - 1)
                    {
                        sw.Write("\",");
                        //sw.Write(";");
                    }
                }

                if (iColCount > 0)
                {
                    sw.Write("\"");
                }
                sw.Write(sw.NewLine);

                // Now write all the rows. 
                foreach (DataRow dr in dtCardsFailedData.Rows)
                {
                    for (int i = 1; i < iColCount; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            sw.Write("\"");
                            sw.Write(dr[i].ToString());
                        }
                        if (i < iColCount - 1)
                        {
                            sw.Write("\",");
                        }
                    }
                    if (iColCount > 0)
                    {
                        sw.Write("\"");
                    }
                    sw.Write(sw.NewLine);
                }

                Response.Write(sw.ToString());
                Response.End();
            }

        }
    }
}

