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
using EFTN.component;

namespace EFTN
{
    public partial class FlatFileForTransactionReceivedForCards : System.Web.UI.Page
    {
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');
                BindSessionDropdownlist();
                BindCurrencyTypeDropdownlist();
                BindData();
            }

        }

        private void BindSessionDropdownlist()
        {
            DataTable dropDownData = new DataTable();
            dropDownData = sentBatchDB.GetSessions(eftConString);
            SessionDdList.DataSource = dropDownData;
            SessionDdList.DataTextField = "SessionID";
            SessionDdList.DataValueField = "SessionID";
            SessionDdList.DataBind();
        }
        protected void BindCurrencyTypeDropdownlist()
        {
            DataTable dropDownData = new DataTable();
            dropDownData = sentBatchDB.GetCurrencyList(eftConString);
            CurrencyDdList.DataSource = dropDownData;
            CurrencyDdList.DataTextField = "Currency";
            CurrencyDdList.DataValueField = "Currency";
            CurrencyDdList.DataBind();
            CurrencyDdList.SelectedIndex = 0;
        }
        private void BindData()
        {
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            if (SelectedBank.Equals(OriginalBankCode.SCB))
            {
                dtgTransactionSentToOwnBank.DataSource = GetTransactionReceivedForCards(CurrencyDdList.SelectedValue, SessionDdList.SelectedValue);
                dtgTransactionSentToOwnBank.DataBind();
            }
        }

        private DataTable GetTransactionReceivedForCards(string currency, string session)
        {
            EFTN.component.TransactionReceivedForCards transactionToCBSDB = new EFTN.component.TransactionReceivedForCards();
            return transactionToCBSDB.GetFlatFileForTransactionReceived_ForCards(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                         + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                         + ddlistDay.SelectedValue.PadLeft(2, '0')
                                                         , currency, int.Parse(session));
        }

        protected void btnGenerateFlatFileTransactionReceived_Click(object sender, EventArgs e)
        {
            EFTN.component.TransactionReceivedForCards transactionToCBSDB = new EFTN.component.TransactionReceivedForCards();
            DataTable dt;
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            if (SelectedBank.Equals("215"))
            {
                dt = GetTransactionReceivedForCards(CurrencyDdList.SelectedValue, SessionDdList.SelectedValue);
                EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();

                string flatfileResult = fc.CreateFlatFileForTransactionReceivedForCardsSCB(dt);
                string fileName = "Cards" + "-" + "FlatFile-TR" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
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

            //Response.Redirect("~/FileWatcherChecker.aspx?PathKey=EFTCBSExport");
        }

        protected void SessionDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void CurrencyDdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
