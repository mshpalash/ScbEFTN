using System;
using System.Configuration;
using EFTN.Utility;
using EFTN.component;
using System.Data;

namespace EFTN
{
    public partial class CBSRejectedTXNManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                if (originBank.Equals(OriginalBankCode.NRB) || originBank.Equals(OriginalBankCode.UCBL))
                {
                    FCUBSRejectedTxnDB fcbDB = new FCUBSRejectedTxnDB();
                    System.Data.SqlClient.SqlDataReader sqlItemCount = fcbDB.GetCounts_RejectedByFCUBSChecker();

                    while (sqlItemCount.Read())
                    {
                        if (ParseData.StringToInt(sqlItemCount["RejectedReceivedEDR"].ToString()) > 0)
                        {
                            lblReceivedEDRCount.ForeColor = System.Drawing.Color.Red;
                        }
                        lblReceivedEDRCount.Text = "(" + sqlItemCount["RejectedReceivedEDR"].ToString() + ")";

                        if (ParseData.StringToInt(sqlItemCount["RejectedReceivedReturn"].ToString()) > 0)
                        {
                            lblReceivedReturnCount.ForeColor = System.Drawing.Color.Red;
                        }
                        lblReceivedReturnCount.Text = "(" + sqlItemCount["RejectedReceivedReturn"].ToString() + ")";
                    }
                }
                else
                {
                    return;
                }
            }
        }

        protected void linkBtnFCUBSRejectedInwardTXN_Click(object sender, EventArgs e)
        {
            Response.Redirect("InwardTransactionRejectedByFCUBS.aspx");
        }

        protected void linkBtnCreditFCUBSForEftDebitTXN_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreditFCUBSForEftDebitTXN.aspx");
        }

        protected void linkBtnInwardReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InwardReturnRejectedByFCUBS.aspx");
        }

        protected void linkBtnFCUBSReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("FCUBSReport.aspx");
        }

    }
}
