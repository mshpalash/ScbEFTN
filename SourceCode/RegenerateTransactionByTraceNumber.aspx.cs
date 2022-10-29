using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using EFTN.Utility;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EFTN.BLL;
using EFTN.component;
using System.Web;

namespace EFTN
{
    public partial class RegenerateTransactionByTraceNumber : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string beginTraceNumber = txtSearchBeginTraceNumber.Text.Trim();
            string endTraceNumber = txtSearchEndTraceNumber.Text.Trim();

            RegenerateOutwardTransactionDB regDB = new RegenerateOutwardTransactionDB();
            DataTable dtSearchData = regDB.Get_SCBAtlasError_TransactionSentByTraceNumber(beginTraceNumber, endTraceNumber);
            dtgEFTAtlasData.DataSource = dtSearchData;
            dtgEFTAtlasData.DataBind();

            int TotalItem = dtSearchData.Rows.Count;
            if (TotalItem > 0)
            {
                lblTotal.Text = "Total No. of Transaction = " + TotalItem.ToString() + " and Total Amount = " + ParseData.StringToDouble(dtSearchData.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                lblTotal.Text = string.Empty;
            }
        }

        protected void btnGenerateText_Click(object sender, EventArgs e)
        {
            string beginTraceNumber = txtSearchBeginTraceNumber.Text.Trim();
            string endTraceNumber = txtSearchEndTraceNumber.Text.Trim();

            RegenerateOutwardTransactionDB regDB = new RegenerateOutwardTransactionDB();

            DataTable dt = regDB.Get_SCBAtlasError_TransactionSentByTraceNumber(beginTraceNumber, endTraceNumber);
            GenerateTextFile(dt);
            lblMsg.Text = "New Transaction generated from tracenumber " + beginTraceNumber + " to " + endTraceNumber;
        }

        private void GenerateTextFile(DataTable dt)
        {
            EFTN.BLL.TextFileGeneratorToUpload tfg = new EFTN.BLL.TextFileGeneratorToUpload();

            string flatfileResult = tfg.GenerateTextDataForOutwardTransactionUpload(dt);

            string fileName = "CBS" + "-" + "TextFileTo-TS" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
            Response.Clear();
            Response.AddHeader("content-disposition",
                     "attachment;filename=" + fileName + ".txt");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.text";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite =
                          new HtmlTextWriter(stringWrite);
            Response.Write(flatfileResult.ToString());
            Response.End();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string beginTraceNumber = txtSearchBeginTraceNumber.Text.Trim();
            string endTraceNumber = txtSearchEndTraceNumber.Text.Trim();
            int UserID = ParseData.StringToInt(Request.Cookies["UserID"].Value);

            RegenerateOutwardTransactionDB regDB = new RegenerateOutwardTransactionDB();

            regDB.DeleteSCBAtlas_TransactionSentByTraceNumber(beginTraceNumber, endTraceNumber, UserID);

            lblMsg.Text = "Transaction deleted from tracenumber " + beginTraceNumber + " to " + endTraceNumber;
        }

        protected void btnSearchXML_Click(object sender, EventArgs e)
        {
            try
            {
                string fileExtension = Path.GetExtension(txtSearchXML.Text);
                if (!fileExtension.ToUpper().Equals(".XML"))
                {
                    lblMsg.Text = "Invalied file extension";
                    return;
                }
                string path = ConfigurationManager.AppSettings["EFTTransactionExport"] + "bak\\";

                string errorFileName = path + txtSearchXML.Text;
                int LastIndex = errorFileName.LastIndexOf("\\");

                string saveAs = errorFileName.Substring(LastIndex + 1, (errorFileName.Length - LastIndex - 1));
                string filecontent = File.ReadAllText(errorFileName);
                if (filecontent.Equals(string.Empty))
                {
                    lblMsg.Text = "No record found";
                    return;
                }

                Response.ClearContent();
                Response.AddHeader("content-disposition",
                         "attachment;filename=" + saveAs);

                Response.ContentType = "xml/ack";

                Response.Write(filecontent);
                Response.End();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}
