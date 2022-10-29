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
using EFTN.component;

namespace EFTN
{
    public partial class ConsumerScreen : System.Web.UI.Page
    {
        private string currency = string.Empty;
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session.Remove("Currency");
                BindCurrencyTypeDropdownlist();
            }
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
        protected void Button2_Click(object sender, EventArgs e)
        {
            Session["EntryDesc"] = txtEntryDesc.Text.Trim();
            currency = CurrencyDdList.SelectedValue;
            Session["Currency"] = currency;
            if (rdoBtnTransactionType.SelectedValue.Equals("Debit") && SenderType.SelectedValue.Equals("1"))
            {
                lblMsg.Text = "Individual cannot send debit transaction";
            }
            else
            {
                Session["EFTTransactionType"] = rdoBtnTransactionType.SelectedValue;

                if (SenderType.SelectedValue == "1" && ReceiverType.SelectedValue == "1")
                {
                    Response.Redirect("IndToInd.aspx");
                }

                else if (SenderType.SelectedValue == "1" && ReceiverType.SelectedValue == "2")
                {
                    Response.Redirect("IndToCor.aspx");
                }

                else if (SenderType.SelectedValue == "2" && ReceiverType.SelectedValue == "1")
                {
                    Response.Redirect("CorToInd.aspx");
                }

                else if (SenderType.SelectedValue == "2" && ReceiverType.SelectedValue == "2")
                {
                    Response.Redirect("CorToCor.aspx");
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    if (SenderType.SelectedValue.Equals(""))
                    {
                        lblMsg.Text = "Select Sender Type";
                    }
                    else if (ReceiverType.SelectedValue.Equals(""))
                    {
                        lblMsg.Text = "Select Receiver Type";
                    }
                }
            }

        }
    }
}
