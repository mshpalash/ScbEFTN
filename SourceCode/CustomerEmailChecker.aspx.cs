using System;
using System.Web.UI.WebControls;
using FloraSoft;
using EFTN.Utility;

namespace EFTN
{
    public partial class CustomerEmailChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        private void BindData()
        {
            CustomerEmailDB db = new CustomerEmailDB();
            Custemailchecker.DataSource = db.GetCustEmail();
            Custemailchecker.DataBind();
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < Custemailchecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)Custemailchecker.Items[i].FindControl("cbxCheck");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void btnActive_Click(object sender, EventArgs e)
        {
            CustomerEmailDB db = new CustomerEmailDB();

            for (int i = 0; i < Custemailchecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)Custemailchecker.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    int CustomerID = ParseData.StringToInt(Custemailchecker.DataKeys[i].ToString());
                    int ApprovedBy = ParseData.StringToInt(Request.Cookies["UserID"].Value);
                    db.UpdateCustEmailChecker(CustomerID, "ACTIVE", ApprovedBy);
                }
            }
            cbxAll.Checked = false;
            BindData();
        }

        protected void btnInactive_Click(object sender, EventArgs e)
        {
            CustomerEmailDB db = new CustomerEmailDB();

            for (int i = 0; i < Custemailchecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)Custemailchecker.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    int CustomerID = ParseData.StringToInt(Custemailchecker.DataKeys[i].ToString());
                    int ApprovedBy = ParseData.StringToInt(Request.Cookies["UserID"].Value);
                    db.UpdateCustEmailChecker(CustomerID, "INACTIVE", ApprovedBy);
                }
            }
            cbxAll.Checked = false;
            BindData();
        }

        protected void Custemailchecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            Custemailchecker.CurrentPageIndex = e.NewPageIndex;
            BindData();
        }
    }
}