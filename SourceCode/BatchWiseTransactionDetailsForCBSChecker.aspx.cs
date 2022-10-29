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
using System.Data.SqlClient;
using EFTN.Utility;
using FloraSoft;
using EFTN.BLL;

namespace EFTN
{
    public partial class BatchWiseTransactionDetailsForCBSChecker : System.Web.UI.Page
    {
        private static DataTable MyDt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                if (SelectedBank.Equals(OriginalBankCode.NRB) || SelectedBank.Equals(OriginalBankCode.UCBL))
                {
                    divDelete.Visible = true;
                }
                else
                {
                    divDelete.Visible = false;
                }
                BindDataGrid();
            }
        }

        private void BindDataGrid()
        {
            string strEDRID = Request.Params["TransactionEDRID"].ToString();
            Guid EDRID = new Guid(strEDRID);
            TransactionSentDB db = new TransactionSentDB();

            MyDt = db.GetSentEDRByTransactionIDForBatchDataset(EDRID);
           
            dtgTransactionDetails.DataSource = MyDt;
            try
            {
                dtgTransactionDetails.DataBind();

            }
            catch
            {
                dtgTransactionDetails.CurrentPageIndex = 0;
                dtgTransactionDetails.DataBind();
            }
        }

        protected void dtgTransactionDetails_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgTransactionDetails.CurrentPageIndex = e.NewPageIndex;
            dtgTransactionDetails.DataSource = MyDt;
            dtgTransactionDetails.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();
            for (int i = 0; i < dtgTransactionDetails.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgTransactionDetails.Items[i].FindControl("cbxSentBatchTS");
                if (cbx.Checked)
                {
                    Guid edrId = (Guid)dtgTransactionDetails.DataKeys[i];
                    edrDB.DeleteTransactionSentForIB(edrId);
                }
            }

            BindDataGrid();
        }
    }
}
