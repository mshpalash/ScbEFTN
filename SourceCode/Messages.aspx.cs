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
using FloraSoft;

namespace EFTN
{
    public partial class Messages : System.Web.UI.Page
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
            MessageDB db = new MessageDB();
            MyDataGrid.DataSource = db.GetAllMessages();
            MyDataGrid.DataBind();
        }

        protected void MyDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            int MessageID = (int)MyDataGrid.DataKeys[e.Item.ItemIndex];
            if (e.CommandName == "ExpireMessage")
            {
                MessageDB msgDb = new MessageDB();
                msgDb.ExpireMessage(MessageID);
            }

            BindData();
        }
    }

}