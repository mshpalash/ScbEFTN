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
    public partial class UpdateGLAccountChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void dtgInvalidDFIAccNo_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            EFTN.component.GLAccountDB gLAccountDB = new EFTN.component.GLAccountDB();
            if (e.CommandName == "Cancel")
            {
                dtgGLAccNo.EditItemIndex = -1;
                BindData();
            }
            if (e.CommandName == "Edit")
            {
                dtgGLAccNo.EditItemIndex = e.Item.ItemIndex;
                BindData();
            }

            if (e.CommandName == "Update")
            {
                DropDownList ddlStatus = (DropDownList)e.Item.FindControl("ddlStatus");

                gLAccountDB.UpdateGLAccStatus(ddlStatus.SelectedValue);
                dtgGLAccNo.EditItemIndex = -1;

                BindData();
            }
            if (e.CommandName == "Delete")
            {
                BindData();
            }

        }

        public void BindData()
        {
            EFTN.component.GLAccountDB gLAccountDB = new EFTN.component.GLAccountDB();
            dtgGLAccNo.DataSource = gLAccountDB.GetGLAccount();
            dtgGLAccNo.DataBind();
        }
    }
}
