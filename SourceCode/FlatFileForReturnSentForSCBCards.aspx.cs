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
    public partial class FlatFileForReturnSentForSCBCards : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');
                BindData();
            }

        }

        private void BindData()
        {
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            if (SelectedBank.Equals(OriginalBankCode.SCB))
            {
                dtgReturnSent.DataSource = GetReturnSentForCardsForSCB();
                dtgReturnSent.DataBind();
            }
        }

        private DataTable GetReturnSentForCardsForSCB()
        {
            EFTN.component.TransactionReceivedForCards transactionToCBSDB = new EFTN.component.TransactionReceivedForCards();
            return transactionToCBSDB.GetFlatFileForReturnSent_ForCardsForSCB(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                         + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                         + ddlistDay.SelectedValue.PadLeft(2, '0'));
        }

        protected void btnGenerateFlatFileForReturnSentForCard_Click(object sender, EventArgs e)
        {
            EFTN.component.TransactionReceivedForCards transactionToCBSDB = new EFTN.component.TransactionReceivedForCards();
            DataTable dt;
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            if (SelectedBank.Equals("215"))
            {
                dt = GetReturnSentForCardsForSCB();
                EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();

                string flatfileResult = fc.CreateFlatFileForReturnSentForCardsSCB(dt);
                string fileName = "Cards" + "-" + "FlatFile-RS" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
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
    }
}
