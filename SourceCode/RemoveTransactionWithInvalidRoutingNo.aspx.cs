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
    public partial class RemoveTransactionWithInvalidRoutingNo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("215"))
                {
                    btnUpdateCSVRejection.Visible = true;
                }
                else
                {
                    btnUpdateCSVRejection.Visible = false;
                }
                BindData();
            }
        }

        protected void dtgInvalidDFIAccNo_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            EFTN.component.InvalidItemDB invalidItemDB = new EFTN.component.InvalidItemDB();
            if (e.CommandName == "Delete")
            {
                Guid EDRID = (Guid)dtgInvalidDFIAccNo.DataKeys[e.Item.ItemIndex];
                EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
                sentEDRDB.DeleteTransactionSent(EDRID);
                BindData();
            }

        }

        public void BindData()
        {
            EFTN.component.InvalidItemDB invalidItemDB = new EFTN.component.InvalidItemDB();
            dtgInvalidDFIAccNo.DataSource = invalidItemDB.GetTransactionWithInvalidRoutingNumber();
            dtgInvalidDFIAccNo.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            EFTN.component.InvalidItemDB invalidItemDB = new EFTN.component.InvalidItemDB();
            invalidItemDB.UpdateTransactionStatusForNewRoutingNo();
            BindData();
        }

        protected void btnUpdateCSVRejection_Click(object sender, EventArgs e)
        {
            EFTN.component.InvalidItemDB invalidItemDB = new EFTN.component.InvalidItemDB();
            invalidItemDB.UpdateCSVRejectionFromInvalidRoutingNo();
            BindData();
        }
    }
}
