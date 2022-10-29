using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using EFTN.component;
using EFTN.Utility;
using FloraSoft;

namespace EFTN
{
    public partial class SCBCardMapper : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            BindData();
        }
        private void BindData()
        {
            SCBCardMapperDB scbCardMapper = new SCBCardMapperDB();
            MyDataGrid2.DataSource = scbCardMapper.GetBitNumber();
            MyDataGrid2.DataBind();
        }

        protected void MyDataGrid2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            SCBCardMapperDB scbCardMapper = new SCBCardMapperDB();

            if (e.CommandName == "Insert")
            {
             
                string BitNumber = string.Empty;
                TextBox txtRoutingNumber = (TextBox)e.Item.FindControl("addBitNumber");
                BitNumber = txtRoutingNumber.Text.Trim();

                scbCardMapper.InsertBitNumber(BitNumber);
                txtMsg.Text = "Inserted Successfully";
                txtMsg.ForeColor = System.Drawing.Color.Blue;
            }
            if (e.CommandName == "Delete")
            {
                int OID = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];

                scbCardMapper.DeleteBitNumber(OID);

                txtMsg.Text = "Deleted Successfully";
                txtMsg.ForeColor = System.Drawing.Color.Blue;
            }
        }
    }
}

