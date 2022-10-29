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
    public partial class InvalidTransactionModifier : System.Web.UI.Page
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
            EFTN.component.InvalidItemDB invalidItemDB = new EFTN.component.InvalidItemDB();
            if (e.CommandName == "Cancel")
            {
                dtgInvalidDFIAccNo.EditItemIndex = -1;
                BindData();
            }
            if (e.CommandName == "Edit")
            {
                dtgInvalidDFIAccNo.EditItemIndex = e.Item.ItemIndex;
                BindData();
            }

            if (e.CommandName == "Update")
            {
                Guid EDRID = (Guid)dtgInvalidDFIAccNo.DataKeys[e.Item.ItemIndex];

                TextBox txtDFIAccountNo = (TextBox)e.Item.FindControl("txtDFIAccountNo");
                TextBox txtAccountNo = (TextBox)e.Item.FindControl("txtAccountNo");
                TextBox txtReceivingBankRoutingNo = (TextBox)e.Item.FindControl("txtReceivingBankRoutingNo");
                TextBox txtPaymentInfo = (TextBox)e.Item.FindControl("txtPaymentInfo");
                TextBox txtIdNumber = (TextBox)e.Item.FindControl("txtIdNumber");
                TextBox txtReceiverName = (TextBox)e.Item.FindControl("txtReceiverName");

                invalidItemDB.UpdateInvalidDTransaction(txtDFIAccountNo.Text.Trim(), txtReceiverName.Text.Trim()
                                                        , txtPaymentInfo.Text.Trim(), txtAccountNo.Text.Trim()
                                                        , txtReceivingBankRoutingNo.Text.Trim()
                                                        , txtIdNumber.Text.Trim(), EDRID);
                dtgInvalidDFIAccNo.EditItemIndex = -1;

                BindData();
            }
            if (e.CommandName == "Delete")
            {
                BindData();
            }

        }

        public void BindData()
        {
            EFTN.component.InvalidItemDB invalidItemDB = new EFTN.component.InvalidItemDB();
            dtgInvalidDFIAccNo.DataSource = invalidItemDB.GetInvalidEDR();
            dtgInvalidDFIAccNo.DataBind();
        }
    }
}
