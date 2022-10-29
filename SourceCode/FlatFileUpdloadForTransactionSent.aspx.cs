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
using System.IO;

namespace EFTN
{
    public partial class FlatFileUpdloadForTransactionSent : System.Web.UI.Page
    {
        private static DataTable dtInwardTransaction = new DataTable();
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            string filePath = string.Empty;
            string extension = Path.GetExtension(fulExcelFile.PostedFile.FileName);

            if (extension.ToLower().Equals(".txt"))
            {
                fileName = fulExcelFile.PostedFile.FileName;

                string fileExtension = Path.GetExtension(fileName);

                string savePath = ConfigurationManager.AppSettings["EFTExcelFiles"] + Guid.NewGuid().ToString() + fileExtension;

                try
                {
                    fulExcelFile.SaveAs(savePath);
                    string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

                    string delim = ConfigurationManager.AppSettings["TextDelim"];
                    DataTable data = DelimitedTextReader.ReadFile(savePath, delim);

                    EFTN.component.HUBCommunicationDB hUBCommunicationDB = new EFTN.component.HUBCommunicationDB();
                    foreach (DataRow row in data.Rows)
                    {
                        string TraceNumber = row["TraceNumber"].ToString();
                        string Result = EFTVariableParser.ParseEFTAccountNumber(row["Result"].ToString().Trim());

                        hUBCommunicationDB.UploadTransactionSentResultFromHUBToEFT(TraceNumber, Result);
                    }
                    lblErrMsg.Text = "Uploaded Successfully";
                }
                catch
                {
                    lblErrMsg.Text = "Failed to upload file";
                    //FailedMessageForTextFileUpload();
                }
            }
        }
    }
}