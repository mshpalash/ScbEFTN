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

namespace EFTN
{
    public partial class FlatFilesForReturnReceivedToHUB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }

        }

        private void BindData()
        {
            //string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            //if (SelectedBank.Equals("215"))
            //{
                if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    EFTN.component.HUBCommunicationDB hUBCommunicationDB = new EFTN.component.HUBCommunicationDB();
                    dtgTransactionSentToOwnBank.DataSource = hUBCommunicationDB.GetFlatFileForDebitReturnReceivedForHSBC_HUB();
                    dtgTransactionSentToOwnBank.DataBind();
                }
                else
                {
                    EFTN.component.HUBCommunicationDB hUBCommunicationDB = new EFTN.component.HUBCommunicationDB();
                    dtgTransactionSentToOwnBank.DataSource = hUBCommunicationDB.GetFlatFileForCreditReturnReceivedForHSBC_HUB();
                    dtgTransactionSentToOwnBank.DataBind();
                }
            //}
        }

        protected void linkBtnFlatFileReturnReceived_Click(object sender, EventArgs e)
        {

            EFTN.component.HUBCommunicationDB hUBCommunicationDB = new EFTN.component.HUBCommunicationDB();
            DataTable dt;
            //string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            string fileName = string.Empty;

            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                dt = hUBCommunicationDB.GetFlatFileForDebitReturnReceivedForHSBC_HUB();
                fileName = "BDRD" + HSBCNamingConvension.GetMonth(System.DateTime.Now.Month) + System.DateTime.Now.Day.ToString().PadLeft(2, '0') + System.DateTime.Now.Second.ToString().PadLeft(2, '0');
            }
            else
            {
                dt = hUBCommunicationDB.GetFlatFileForCreditReturnReceivedForHSBC_HUB();
                fileName = "BDRC" + HSBCNamingConvension.GetMonth(System.DateTime.Now.Month) + System.DateTime.Now.Day.ToString().PadLeft(2, '0') + System.DateTime.Now.Second.ToString().PadLeft(2, '0');
            }
            EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();

            string flatfileResult = fc.CreatFlatFileForTransactionSentToHUB(dt);
            //string fileName = "CBS" + "-" + "FlatFile-RR" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
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

            //Response.Redirect("~/FileWatcherChecker.aspx?PathKey=EFTCBSExport");
        }

        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
