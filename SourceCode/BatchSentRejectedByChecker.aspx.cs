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
using FloraSoft;
using EFTN.BLL;
using System.Text;

namespace EFTN
{
    public partial class BatchSentRejectedByChecker : System.Web.UI.Page
    {
        private static DataTable myDTBatchSent = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataForRejectedBatchSent();
            }
        }

        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataForRejectedBatchSent();
        }

        private void BindDataForRejectedBatchSent()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }
            
            EFTN.component.SentBatchDB batchDB = new EFTN.component.SentBatchDB();
            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                myDTBatchSent = batchDB.GetBatchesRejectedByCheckerForDebit(DepartmentID);
            }
            else
            {
                myDTBatchSent = batchDB.GetBatchesRejectedByChecker(DepartmentID);
            }
            dtgBatchTransactionSent.DataSource = myDTBatchSent;
            dtgBatchTransactionSent.DataBind();
        }

        protected void dtgBatchTransactionSent_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgBatchTransactionSent.CurrentPageIndex = e.NewPageIndex;
            dtgBatchTransactionSent.DataSource = myDTBatchSent;
            dtgBatchTransactionSent.DataBind();
        }

        protected void cbxAllTransactionSent_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllTransactionSent.Checked;
            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSent.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                    db.DeleteBatchSent(TransactionID);
                }
            }
            BindDataForRejectedBatchSent();
        }
    }
}
