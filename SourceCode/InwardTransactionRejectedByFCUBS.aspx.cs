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
using EFTN.BLL;

namespace EFTN
{
    public partial class InwardTransactionRejectedByFCUBS : System.Web.UI.Page
    {
        private static DataTable dtInwardTransaction = new DataTable();
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBankList();
                BindData();
                sortOrder = "asc";
            }
        }

        private void BindBankList()
        {
            FloraSoft.BanksDB dbBank = new FloraSoft.BanksDB();
            BankList.DataSource = dbBank.GetAllBanks();
            BankList.DataBind();
            BankList.Items.Add(new System.Web.UI.WebControls.ListItem("All", "0"));
            BankList.SelectedIndex = BankList.Items.Count - 1;
        }

        private void BindData()
        {

            EFTN.component.FCUBSRejectedTxnDB db = new EFTN.component.FCUBSRejectedTxnDB();

            int BranchID = 0;

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);
            }

            if (BankList.SelectedValue.Equals("0"))
            {
                if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    dtInwardTransaction = db.GetReceivedEDR_RejectedByFCUBSChecker_ForDebit(BranchID).Copy();
                }
                else
                {
                    //In CentralInward=1 and DepartmentID is only 0 then able to see the transaction.
                    if (ConfigurationManager.AppSettings["CentralInward"].Equals("1"))
                    {
                        if (ParseData.StringToInt(Request.Cookies["DepartmentID"].Value) == 0)
                        {
                            BranchID = 0;
                            dtInwardTransaction = db.GetReceivedEDR_RejectedByFCUBSChecker(BranchID).Copy();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        dtInwardTransaction = db.GetReceivedEDR_RejectedByFCUBSChecker(BranchID).Copy();
                    }
                }
            }
            else
            {
                if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
                {
                    dtInwardTransaction = db.GetReceivedEDR_RejectedByFCUBSChecker_ForDebit_BankWise(BranchID, BankList.SelectedValue).Copy();
                }
                else
                {
                    //In CentralInward=1 and DepartmentID is only 0 then able to see the transaction.
                    if (ConfigurationManager.AppSettings["CentralInward"].Equals("1"))
                    {
                        if (ParseData.StringToInt(Request.Cookies["DepartmentID"].Value) == 0)
                        {
                            BranchID = 0;
                            dtInwardTransaction = db.GetReceivedEDR_RejectedByFCUBSChecker_BankWise(BranchID, BankList.SelectedValue).Copy();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        dtInwardTransaction = db.GetReceivedEDR_RejectedByFCUBSChecker_BankWise(BranchID, BankList.SelectedValue).Copy();
                    }
                }
            }

            dv = dtInwardTransaction.DefaultView;
            dtgEFTCheckerApproved.CurrentPageIndex = 0;

            dtgEFTCheckerApproved.DataSource = dv;
            dtgEFTCheckerApproved.DataBind();

            if (dtInwardTransaction.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + dtInwardTransaction.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(dtInwardTransaction.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
        }
        
        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void BankList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void dtgEFTCheckerApproved_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = dtInwardTransaction.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgEFTCheckerApproved.DataSource = dv;
            dtgEFTCheckerApproved.DataBind();
        }

        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";

                }
                else
                {
                    ViewState["sortOrder"] = "desc";
                }

                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgEFTCheckerApproved.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerApproved.Items[i].FindControl("cbxCheck");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void dtgEFTCheckerApproved_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTCheckerApproved.CurrentPageIndex = e.NewPageIndex;
            dtgEFTCheckerApproved.DataSource = dtInwardTransaction;
            dtgEFTCheckerApproved.DataBind();
        }

        protected void btnReprocess_Click(object sender, EventArgs e)
        {
            string txnRemarks = "Reprocess";
            TransferFCUBS_Error_To_ReceivedEDRMaker(txnRemarks);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            string txnRemarks = "Return";
            TransferFCUBS_Error_To_ReceivedEDRMaker(txnRemarks);
        }

        private void TransferFCUBS_Error_To_ReceivedEDRMaker(string txnRemarks)
        {
            if (txtRejectedReason.Text.Equals(string.Empty))
            {
                lblNoReturnReason.Text = "Please enter reason";
                lblNoReturnReason.ForeColor = System.Drawing.Color.Red;
                lblNoReturnReason.Visible = true;
                return;
            }
            else
            {
                txnRemarks += ", " + txtRejectedReason.Text.Trim();
            }

            for (int i = 0; i < dtgEFTCheckerApproved.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTCheckerApproved.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    Guid EDRID = (Guid)dtgEFTCheckerApproved.DataKeys[i];
                    EFTN.component.FCUBSRejectedTxnDB db = new EFTN.component.FCUBSRejectedTxnDB();
                    db.TransferFCUBS_Error_To_ReceivedEDR(EDRID, txnRemarks);
                }
            }

            BindData();

        }
    }
}
