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
    public partial class CustomerEmailEdit : System.Web.UI.Page
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
            int customerID = ParseData.StringToInt(Request.Params["CustomerID"]);
            if (customerID == 0)
            {
                Response.Redirect("CustomerEmail.aspx");
            }
            DataTable dt = db.GetCustEmailByCustomerId(customerID);

            txtaccno.Text = dt.Rows[0]["ACCNo"].ToString();
            txtacholdname.Text= dt.Rows[0]["ACCHolderName"].ToString();
            txtem1.Text= dt.Rows[0]["CusEmail1"].ToString();
            txtem2.Text= dt.Rows[0]["CusEmail2"].ToString();
            txtem3.Text= dt.Rows[0]["CusEmail3"].ToString();
            txtem4.Text= dt.Rows[0]["CusEmail4"].ToString();
            txtem5.Text= dt.Rows[0]["CusEmail5"].ToString();
            txtconno1.Text= dt.Rows[0]["ContactNumber1"].ToString();
            txtconno2.Text= dt.Rows[0]["Contactnumber2"].ToString();

            txtSusAccNo.Text= dt.Rows[0]["SusAccNo"].ToString();

           // ddListCusAdvice.SelectedValue= dt.Rows[0]["ACCNo"].ToString();
            lblGenTime.Text = dt.Rows[0]["MISGenerationTime"].ToString();
        }

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
            string MISSusAccNo = txtSusAccNo.Text;
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
                int customerID = ParseData.StringToInt(Request.Params["CustomerID"]);

                custDB.UpdateCustEmail(customerID, MISACCNo, MISACCHolderName, MISCusEmail1, MISCusEmail2, MISCusEmail3, MISCusEmail4,
                                       MISCusEmail5, MISContactNumber1, MISContactNumber2, MISSusAccNo,MISCreatedBy, MISGenerationTime.ToString());
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
                lblmessage.ForeColor = System.Drawing.Color.Red;
                lblmessage.Text = ex.Message;
            }
            Response.Redirect("CustomerEmail.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerEmail.aspx");
        }
    }
}