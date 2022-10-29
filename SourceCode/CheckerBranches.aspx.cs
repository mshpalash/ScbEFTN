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
    public partial class CheckerBranches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BanksDB db0 = new BanksDB();
                BankList.DataSource = db0.GetAllBanks();
                BankList.DataBind();
                BankList.SelectedValue = "1";
                BindData();
            }
        }

        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    BindData();
        //}
        

        protected void BankList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

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
            MyDataGrid.DataSource = db.GetBranchesByBankIDforAdminChecker(Int32.Parse(BankList.SelectedValue));
            MyDataGrid.DataBind();

            BanksDB banksDB = new BanksDB();
            //System.Data.SqlClient.SqlDataReader sqlDRBranch = banksDB.GetBankCodeByBankID(Int32.Parse(BankList.SelectedValue));
            DataTable dtBR = new DataTable();
            dtBR = banksDB.GetBankCodeByBankID(Int32.Parse(BankList.SelectedValue));

            for (int brCount = 0; brCount < dtBR.Rows.Count; brCount++)
            {
                lblBankCode.Text = "-----Bank Code is " + dtBR.Rows[brCount]["BankCode"].ToString();
            }
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
            //BranchesDB db = new BranchesDB();
            //if (e.CommandName == "Cancel")
            //{
            //    MyDataGrid.EditItemIndex = -1;
            //    BindData();
            //}
            //if (e.CommandName == "Edit")
            //{
            //    MyDataGrid.EditItemIndex = e.Item.ItemIndex;
            //    BindData();
            //}
           
            //if (e.CommandName == "Update")
            //{
            //    int BranchID = ParseData.StringToInt(MyDataGrid.DataKeys[i].ToString());
            //    DropDownList ddlBranchStatus = (DropDownList)e.Item.FindControl("ddlBranchStatus");


            //    db.UpdateBranchbyAdminChecker(BranchID, ddlBranchStatus.Text);

            //    MyDataGrid.EditItemIndex = -1;

            //    BindData();
            //}
        }
    }
}