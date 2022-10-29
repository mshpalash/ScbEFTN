using System;
using System.Data;
using System.Web.UI.WebControls;

namespace EFTN
{
    public partial class iBankingController : System.Web.UI.Page
    {
        private static DataTable MyDt = new DataTable();
        DataView dv;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
                lblNoReturnReason.Visible = false;
                sortOrder = "asc";
            }

        }
        private void BindData()
        {
            EFTN.component.iBankingDB ibankingEDRDB = new EFTN.component.iBankingDB();


            MyDt = ibankingEDRDB.GetibankingEDRDataforChecker();
            iBankingOperatorGrid.DataSource = MyDt;

            dv = MyDt.DefaultView;
            iBankingOperatorGrid.CurrentPageIndex = 0;

            iBankingOperatorGrid.DataSource = dv;
            iBankingOperatorGrid.DataBind();

        }

        protected void iBankingOperator_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            iBankingOperatorGrid.CurrentPageIndex = e.NewPageIndex;
            iBankingOperatorGrid.DataSource = MyDt;
            iBankingOperatorGrid.DataBind();
        }

        protected void iBankingOperator_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = MyDt.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            iBankingOperatorGrid.DataSource = dv;
            iBankingOperatorGrid.DataBind();
        }

        protected void btnAcceptAll_Click(object sender, EventArgs e)
        {
            int CheckedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            //string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < iBankingOperatorGrid.Items.Count; i++)
            {
                string edrId = iBankingOperatorGrid.DataKeys[i].ToString();
                Guid IBTID = new Guid(edrId);

                EFTN.component.iBankingDB ibankingEDRDB = new EFTN.component.iBankingDB();
                //db.GetEDRSentApprovedByEBBSChecker(EDRID, ApprovedBy);
                ibankingEDRDB.AcceptTransactionsByChecker(IBTID, CheckedBy);


            }
            BindData();
        }

        protected void btnRejectAll_Click(object sender, EventArgs e)
        {
            if (txtRejectedReason.Text != "")
            {
                lblNoReturnReason.Visible = false;
                string rejectedReason = txtRejectedReason.Text;
                int DeletedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
                //string rejectReason = txtRejectedReason.Text;
                for (int i = 0; i < iBankingOperatorGrid.Items.Count; i++)
                {

                    string edrId = iBankingOperatorGrid.DataKeys[i].ToString();
                    Guid IBTID = new Guid(edrId);

                    EFTN.component.iBankingDB ibankingEDRDB = new EFTN.component.iBankingDB();
                    //db.GetEDRSentApprovedByEBBSChecker(EDRID, ApprovedBy);
                    ibankingEDRDB.DeleteTransactionsByChecker(IBTID, DeletedBy, rejectedReason);


                }

                txtRejectedReason.Text = "";
            }
            else
            {
                lblNoReturnReason.Visible = true;
            }
            BindData();
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (txtRejectedReason.Text != "")
            {
                lblNoReturnReason.Visible = false;
                string rejectedReason = txtRejectedReason.Text;
                int DeletedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
                //string rejectReason = txtRejectedReason.Text;
                for (int i = 0; i < iBankingOperatorGrid.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)iBankingOperatorGrid.Items[i].FindControl("CheckBEFTNList");

                    if (cbx.Checked)
                    {
                        string edrId = iBankingOperatorGrid.DataKeys[i].ToString();
                        Guid IBTID = new Guid(edrId);

                        EFTN.component.iBankingDB ibankingEDRDB = new EFTN.component.iBankingDB();
                        //db.GetEDRSentApprovedByEBBSChecker(EDRID, ApprovedBy);
                        ibankingEDRDB.DeleteTransactionsByChecker(IBTID, DeletedBy, rejectedReason);

                    }
                }

                txtRejectedReason.Text = "";
            }
            else
            {
                lblNoReturnReason.Visible = true;
            }
            BindData();
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            int CheckedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            //string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < iBankingOperatorGrid.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)iBankingOperatorGrid.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    string edrId = iBankingOperatorGrid.DataKeys[i].ToString();
                    Guid IBTID = new Guid(edrId);

                    EFTN.component.iBankingDB ibankingEDRDB = new EFTN.component.iBankingDB();
                    //db.GetEDRSentApprovedByEBBSChecker(EDRID, ApprovedBy);
                    ibankingEDRDB.AcceptTransactionsByChecker(IBTID, CheckedBy);

                }
            }
            BindData();

        }

        protected void iBankingOperatorGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            DataRowView test;
            if (e.Item != null && e.Item.DataItem is DataRowView)
            {
                test = (DataRowView)e.Item.DataItem;

                if (test.Row["RiskBit"].ToString().ToUpper() == "CHECK")
                {
                    e.Item.Cells[2].Font.Bold = true;
                    e.Item.Cells[2].Font.Size = 14;
                    e.Item.Cells[2].BackColor = System.Drawing.Color.Red;
                }
                if (test.Row["DuplicateBit"].ToString().ToUpper() == "CHECK")
                {
                    //e.Item.BackColor = System.Drawing.Color.Orange;
                    e.Item.Cells[3].Font.Bold = true;
                    e.Item.Cells[3].Font.Size = 14;
                    e.Item.Cells[3].BackColor = System.Drawing.Color.Orange;

                }
                if (test.Row["Over500KBit"].ToString().ToUpper() == "CHECK")
                {
                    //e.Item.BackColor = System.Drawing.Color.Yellow;
                    e.Item.Cells[4].Font.Bold = true;
                    e.Item.Cells[4].Font.Size = 14;
                    e.Item.Cells[4].BackColor = System.Drawing.Color.Yellow;

                }
            }
        }
    }
}