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

namespace FloraSoft
{
    public partial class SearchBranches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BanksDB db0 = new BanksDB();
                BankList.DataSource = db0.GetAllBanks();
                BankList.DataBind();
                BankList.SelectedValue = "1";
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            BranchesDB db = new BranchesDB();
            MyDataGrid.DataSource = db.GetBranchesByBankID(Int32.Parse(BankList.SelectedValue));
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

        protected void MyDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            BranchesDB db = new BranchesDB();
            if (e.CommandName == "Cancel")
            {
                MyDataGrid.EditItemIndex = -1;
                BindData();
            }
            if (e.CommandName == "Edit")
            {
                MyDataGrid.EditItemIndex = e.Item.ItemIndex;
                BindData();
            }
            if (e.CommandName == "Insert")
            {
                TextBox txtBranchName = (TextBox)e.Item.FindControl("addBranchName");
                TextBox txtRoutingNo  = (TextBox)e.Item.FindControl("addRoutingNo");

                if (EFTN.BLL.RoutingNumberValidator.CheckDigitOk(txtRoutingNo.Text))
                {
                    db.InsertBranches(Int32.Parse(BankList.SelectedValue), txtBranchName.Text, txtRoutingNo.Text);
                    lblErrMsg.Text = "Added successfully";
                    lblErrMsg.ForeColor = System.Drawing.Color.Blue;
                    BindData();
                }
                else
                {
                    lblErrMsg.Text = "Invalid Routing Number. Please insert valid routing number";
                    lblErrMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            if (e.CommandName == "Update")
            {
                int BranchID = (int)MyDataGrid.DataKeys[e.Item.ItemIndex];

                TextBox txtBranchName = (TextBox)e.Item.FindControl("BranchName");
                TextBox txtRoutingNo  = (TextBox)e.Item.FindControl("RoutingNo");

                db.UpdateBranch(BranchID, txtBranchName.Text, txtRoutingNo.Text);

                MyDataGrid.EditItemIndex = -1;

                BindData();
            }
            if (e.CommandName == "Delete")
            {
                BindData();
            }
        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            if (fulExcelFile.HasFile)
            {
                string fileName = fulExcelFile.FileName;
                try
                {
                    //string savePath = Server.MapPath("~/ExcelFiles") + "\\" + Guid.NewGuid().ToString() + fileName;
                    string savePath = ConfigurationManager.AppSettings["EFTExcelFiles"] + Guid.NewGuid().ToString() + fileName;

                    fulExcelFile.SaveAs(savePath);

                    EFTN.BLL.ExcelDataBranch excelObj = new EFTN.BLL.ExcelDataBranch(savePath);
                    DataTable dt = excelObj.EntryData();
                    if (dt.Rows.Count > 0)
                    {
                        SuccessMessage();
                    }
                    else
                    {
                        lblErrMsg.Text = "No Data Found";
                        lblErrMsg.ForeColor = System.Drawing.Color.Red;
                        lblErrMsg.Visible = true;
                    }
                }
                catch
                {
                    FailedMessage();
                }
            }
        }

        private void FailedMessage()
        {
            lblErrMsg.Text = "Failed to upload invalid file. Please upload valid excel file";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }

        private void SuccessMessage()
        {
            lblErrMsg.Text = "uploaded file successfully";
            lblErrMsg.ForeColor = System.Drawing.Color.Blue;
            lblErrMsg.Visible = true;
        }
    }
}