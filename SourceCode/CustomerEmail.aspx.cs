using System;
using System.Web.UI.WebControls;
using System.Configuration;
using FloraSoft;
using EFTN.Utility;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace EFTN
{
    public partial class CustomerEmail : System.Web.UI.Page
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
            Custemail.DataSource = db.GetCustEmail();
            Custemail.DataBind();
        }

        //protected void Custemail_ItemCommand(object source, DataGridCommandEventArgs e)
        //{
        //    CustomerEmailDB db = new CustomerEmailDB();

        //    if (e.CommandName == "Cancel")
        //    {
        //        Custemail.EditItemIndex = -1;
        //        BindData();

        //    }
        //    if (e.CommandName == "Edit")
        //    {
        //        Custemail.EditItemIndex = e.Item.ItemIndex;
        //        BindData();

        //    }

        //    if (e.CommandName == "Update")
        //    {
        //        int CustomerID = (int)Custemail.DataKeys[e.Item.ItemIndex];
        //        TextBox ACCNo = (TextBox)e.Item.FindControl("ACCNo");
        //        TextBox addAccountHolderName = (TextBox)e.Item.FindControl("addAccountHolderName");
        //        TextBox addCusEmail1 = (TextBox)e.Item.FindControl("addCusEmail1");
        //        TextBox addCusEmail2 = (TextBox)e.Item.FindControl("addCusEmail2");
        //        TextBox addCusEmail3 = (TextBox)e.Item.FindControl("addCusEmail3");
        //        TextBox addCusEmail4 = (TextBox)e.Item.FindControl("addCusEmail4");
        //        TextBox addCusEmail5 = (TextBox)e.Item.FindControl("addCusEmail5");
        //        TextBox addContactNumber1 = (TextBox)e.Item.FindControl("addContactNumber1");
        //        TextBox addContactNumber2 = (TextBox)e.Item.FindControl("addContactNumber2");
        //        TextBox addUptoEbbs = (TextBox)e.Item.FindControl("addUptoEbbs");
        //        TextBox addSusAccNo = (TextBox)e.Item.FindControl("addSusAccNo");
        //        TextBox addCusAdvice = (TextBox)e.Item.FindControl("addCusAdvice");
        //        db.UpdateCustEmail(CustomerID, ACCNo.Text, addAccountHolderName.Text, addCusEmail1.Text, addCusEmail2.Text,addCusEmail3.Text, addCusEmail4.Text, addCusEmail5.Text, addContactNumber1.Text, addContactNumber2.Text, addSusAccNo.Text, ParseData.StringToInt(addCusAdvice.Text));
        //        Custemail.EditItemIndex = -1;
        //        lblmessage.Text = "Updated successfully";
        //        lblmessage.ForeColor = System.Drawing.Color.Blue;
        //        BindData();

        //    }
        //}

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            CustomerEmailDB custDB = new CustomerEmailDB();

            string MISACCNo = txtaccno.Text;
            string MISACCHolderName = txtacholdname.Text;
            string MISCusEmail1 = txtem1.Text;
            string MISCusEmail2 = txtem2.Text;
            string MISCusEmail3 = txtem3.Text;
            string MISCusEmail4 = txtem4.Text;
            string MISCusEmail5 = txtem5.Text;
            string MISContactNumber1 = txtconno1.Text;
            string MISContactNumber2 = txtconno2.Text;
            int MISUptoEbbs = 0;
            string MISSusAccNo = txtSusAccNo.Text;
            int MISCusAdvice = ParseData.StringToInt(ddListCusAdvice.SelectedValue);
            int MISCreatedBy = ParseData.StringToInt(Request.Cookies["UserID"].Value);
            StringBuilder MISGenerationTime = new StringBuilder();

            for(int i=0; i<chkBoxList1.Items.Count; i++)
            {
                if (chkBoxList1.Items[i].Selected)
                {
                    if (MISGenerationTime.Length > 0)
                    {
                        MISGenerationTime.Append(",");
                    }
                    MISGenerationTime.Append(chkBoxList1.Items[i].Value);
                }
            }

            for (int i = 0; i < chkBoxList2.Items.Count; i++)
            {
                if (chkBoxList2.Items[i].Selected)
                {
                    if (MISGenerationTime.Length > 0)
                    {
                        MISGenerationTime.Append(",");
                    }
                    MISGenerationTime.Append(chkBoxList2.Items[i].Value);
                }
            }
            if (MISGenerationTime.ToString().Equals(string.Empty))
            {
                lblmessage.Text = "You must select atleast 1 generation time";
                lblmessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            try
            {
                custDB.InsertCustEmail(MISACCNo, MISACCHolderName, MISCusEmail1, MISCusEmail2, MISCusEmail3, MISCusEmail4, MISCusEmail5,
                                       MISContactNumber1, MISContactNumber2, MISUptoEbbs, MISSusAccNo, MISCusAdvice, MISCreatedBy, MISGenerationTime.ToString());
                lblmessage.Text = "Inserted successfully";
                lblmessage.ForeColor = System.Drawing.Color.Blue;
                BindData();
                txtaccno.Text = string.Empty;
                txtacholdname.Text = string.Empty;
                txtem1.Text = string.Empty;
                txtem2.Text = string.Empty;
                txtem3.Text = string.Empty;
                txtem4.Text = string.Empty;
                txtem5.Text = string.Empty;
                txtconno1.Text = string.Empty;
                txtconno2.Text = string.Empty;
                txtSusAccNo.Text = string.Empty;

                for (int i = 0; i < chkBoxList1.Items.Count; i++)
                {
                    chkBoxList1.Items[i].Selected = false;
                    chkBoxList2.Items[i].Selected = false;
                }
                for (int i = 0; i < chkBoxList2.Items.Count; i++)
                {
                    chkBoxList1.Items[i].Selected = false;
                    chkBoxList2.Items[i].Selected = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Custemail_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            Custemail.CurrentPageIndex = e.NewPageIndex;
            BindData();
        }
    }
}