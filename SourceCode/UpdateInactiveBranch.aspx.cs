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

namespace FloraSoft
{
    public partial class UpdateInactiveBranch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    BindData();
        //}
        

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < MyDataGrid.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)MyDataGrid.Items[i].FindControl("cbxCheck");
                cbx.Checked = checkAllChecked;
            }
        }

        private void BindData()
        {
            BranchesDB db = new BranchesDB();
            MyDataGrid.DataSource = db.GetInactiveBranches();
            MyDataGrid.DataBind();
        }

        
        protected void btnActive_Click(object sender, EventArgs e)
        {
            BranchesDB db = new BranchesDB();

            for (int i = 0; i < MyDataGrid.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)MyDataGrid.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    int BranchID = ParseData.StringToInt(MyDataGrid.DataKeys[i].ToString());
                    db.UpdateBranchbyAdminChecker(BranchID, "ACTIVE");
                }
            }
            cbxAll.Checked = false;
            BindData();
        }

        protected void btnInactive_Click(object sender, EventArgs e)
        {
            BranchesDB db = new BranchesDB();

            for (int i = 0; i < MyDataGrid.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)MyDataGrid.Items[i].FindControl("cbxCheck");
                if (cbx.Checked)
                {
                    int BranchID = ParseData.StringToInt(MyDataGrid.DataKeys[i].ToString());
                    db.UpdateBranchbyAdminChecker(BranchID, "INACTIVE");
                }
            }
            cbxAll.Checked = false;
            BindData();
        }

        protected void MyDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }
    }
}