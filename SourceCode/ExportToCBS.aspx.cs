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

namespace EFTN
{
    public partial class ExportToCBS : System.Web.UI.Page
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
            EFTN.component.SentEDRDB db = new EFTN.component.SentEDRDB();
            dtgTransactionSentCBS.DataSource = db.GetTransactionSentFlatFileData();
            dtgTransactionSentCBS.DataBind();

            EFTN.component.SentReturnDB sentReturnDB = new EFTN.component.SentReturnDB();
            dtgReturnSentCBS.DataSource = sentReturnDB.GetReturnSentFlatFileData();
            dtgReturnSentCBS.DataBind();

            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            dtgTransactionSentToOwnBank.DataSource = transactionToCBSDB.GetTransactionToSendCBS();
            dtgTransactionSentToOwnBank.DataBind();
        }

        protected void btnGenerateFlatFileTransactionSent_Click(object sender, EventArgs e)
        {
            EFTN.component.SentEDRDB db = new EFTN.component.SentEDRDB();
            DataTable dt = db.GetTransactionSentFlatFileData();
            EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();
            //fc.CreatFlatFile(dt, ConfigurationManager.AppSettings["EFTCBSExport"], "TS");

            //Response.Redirect("~/FileWatcher.aspx?PathKey=EFTCBSExport");

            //EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            //DataTable dt = transactionToCBSDB.GetTransactionReceivedFlatFile();
            //EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }
            string flatfileResult = fc.CreatFlatFileForTransactionSent(dt, DepartmentID);
            string fileName = "CBS" + "-" + "FlatFile-TS" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
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

        protected void btnGenerateFlatFileReturnSent_Click(object sender, EventArgs e)
        {
            EFTN.component.SentReturnDB sentReturnDB = new EFTN.component.SentReturnDB();
            DataTable dt = sentReturnDB.GetReturnSentFlatFileData();
            EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();
            fc.CreatFlatFile(dt, ConfigurationManager.AppSettings["EFTCBSExport"], "Ret");
            Response.Redirect("~/FileWatcher.aspx?PathKey=EFTCBSExport");

        }

        protected void lnkBtnGenerateFlatFileTransactionSentToOwnBank_Click(object sender, EventArgs e)
        {
            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            DataTable dt = transactionToCBSDB.GetTransactionToSendCBS();
            transactionToCBSDB.UpdateTransactionToSendCBS();
            EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();
            fc.CreatFlatFile(dt, ConfigurationManager.AppSettings["EFTCBSExport"], "STCBS");
            Response.Redirect("~/FileWatcher.aspx?PathKey=EFTCBSExport");
        }
    }
}
