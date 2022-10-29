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
    public partial class ReceivedNOCChecker : System.Web.UI.Page
    {
        private static DataTable myDTNOCReceived = new DataTable();
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNOCData();
                BindCodeLookUp();
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

        private void BindNOCData()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.ReceivedNOCDB receivedNOCDB = new EFTN.component.ReceivedNOCDB();
            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                myDTNOCReceived = receivedNOCDB.GetReceivedNOCForDebit(DepartmentID);
            }
            else
            {
                myDTNOCReceived = receivedNOCDB.GetReceivedNOC(DepartmentID);
            }

            dv = myDTNOCReceived.DefaultView;
            dtgEFTReceivedNOC.DataSource = dv;
            dtgEFTReceivedNOC.DataBind();


            if (myDTNOCReceived.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + myDTNOCReceived.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + decimal.Parse(myDTNOCReceived.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
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
            BindNOCData();
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgEFTReceivedNOC.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTReceivedNOC.Items[i].FindControl("chkBoxReceivedNOC");
                cbx.Checked = checkAllChecked;
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            int CreatedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            for (int i = 0; i < dtgEFTReceivedNOC.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTReceivedNOC.Items[i].FindControl("chkBoxReceivedNOC");

                if (cbx.Checked)
                {
                    string nocID = dtgEFTReceivedNOC.DataKeys[i].ToString();
                    Guid NOCID = new Guid(nocID);
                    EFTN.component.ReceivedNOCDB receivedNOCDB = new EFTN.component.ReceivedNOCDB();
                    if (radioBtnChecker.SelectedValue.Equals("Approve"))
                    {
                        receivedNOCDB.UpdateReceivedNOCStatus(EFTN.Utility.TransactionStatus.NOC_Received_Approved, NOCID, ddListRNOC.SelectedValue, CreatedBy, CreatedBy, string.Empty);
                    }
                    else if (radioBtnChecker.SelectedValue.Equals("RNOC"))
                    {
                        receivedNOCDB.UpdateReceivedNOCStatus(EFTN.Utility.TransactionStatus.NOC_Received_RNOC, NOCID, ddListRNOC.SelectedValue, CreatedBy, CreatedBy, string.Empty);
                    }
                }
            }

            BindNOCData();
        }

        private void BindCodeLookUp()
        {
            ddListRNOC.Enabled = true;
            EFTN.component.CodeLookUpDB codeDB = new EFTN.component.CodeLookUpDB();
            ddListRNOC.DataSource = codeDB.GetCodeLookUp(4);
            ddListRNOC.DataBind();
        }
    }
}
