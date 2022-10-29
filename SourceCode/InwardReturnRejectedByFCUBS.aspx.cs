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
using EFTN.BLL;
using EFTN.Utility;
using FloraSoft;

namespace EFTN
{
    public partial class InwardReturnRejectedByFCUBS : System.Web.UI.Page
    {
        private static DataTable myDTReturnReceived = new DataTable();
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                sortOrder = "asc";
            }
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

        private void BindData()
        {
            EFTN.component.FCUBSRejectedTxnDB fCUBSRejectedTxnDB = new EFTN.component.FCUBSRejectedTxnDB();
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }
            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                myDTReturnReceived = fCUBSRejectedTxnDB.GetReceivedReturn_Debit_RejectedBy_FCUBS(DepartmentID);
            }
            else
            {
                myDTReturnReceived = fCUBSRejectedTxnDB.GetReceivedReturn_RejectedBy_FCUBS(DepartmentID);
            }

            dv = myDTReturnReceived.DefaultView;
            dtgApprovedReturnChecker.DataSource = dv;
            dtgApprovedReturnChecker.DataBind();

            if (myDTReturnReceived.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + myDTReturnReceived.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(myDTReturnReceived.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
            cbxAll.Checked = false;
        }


        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void UpdateReceivedReturn()
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            bool CBSActive = true;

            if (bankCode.Equals(OriginalBankCode.UCBL))
            {
                int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                DepartmentsDB departmentDB = new DepartmentsDB();
                DataTable dtDept = departmentDB.EFT_GetDepartmentDetailsByDepartmentID(DepartmentID);

                if (bool.Parse(dtDept.Rows[0]["CBSActive"].ToString()))
                {
                    CBSActive = true;
                }
                else
                {
                    CBSActive = false;
                }
            }
            
            EFTN.component.ReceivedReturnDB db = new EFTN.component.ReceivedReturnDB();
            for (int i = 0; i < dtgApprovedReturnChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgApprovedReturnChecker.Items[i].FindControl("CheckBEFTNList");
                if (cbx.Checked)
                {
                    Guid returnID = (Guid)dtgApprovedReturnChecker.DataKeys[i];
                }
            }
            BindData();
        }


        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgApprovedReturnChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgApprovedReturnChecker.Items[i].FindControl("CheckBEFTNList");
                cbx.Checked = checkAllChecked;
            }
        }

            
        protected void dtgApprovedReturnChecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgApprovedReturnChecker.CurrentPageIndex = e.NewPageIndex;
            dtgApprovedReturnChecker.DataSource = myDTReturnReceived;
            dtgApprovedReturnChecker.DataBind();
        }

        protected void dtgApprovedReturnChecker_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = myDTReturnReceived.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgApprovedReturnChecker.DataSource = dv;
            dtgApprovedReturnChecker.DataBind();
        }

        protected void linkBtnReprocess_Click(object sender, EventArgs e)
        {
            TransferFCUBS_Error_To_ReceivedEDRMaker("Reprocess");
        }


        private void TransferFCUBS_Error_To_ReceivedEDRMaker(string txnRemarks)
        {
            for (int i = 0; i < dtgApprovedReturnChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgApprovedReturnChecker.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    Guid ReturnID = (Guid)dtgApprovedReturnChecker.DataKeys[i];
                    EFTN.component.FCUBSRejectedTxnDB db = new EFTN.component.FCUBSRejectedTxnDB();
                    db.TransferFCUBS_Err_To_ReceivedReturn(ReturnID, txnRemarks);
                }
            }

            BindData();

        }


    }
}
