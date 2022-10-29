using System;
using EFTN.component;

namespace EFTN
{
    public partial class AccountEnquiry : System.Web.UI.Page
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string oldAccountNo = txtOldAccountNumber.Text.Trim();
            if (oldAccountNo.Equals(string.Empty))
            {
                return;
            }

            AccountEnquiryDB accEnqDB = new AccountEnquiryDB();
            string NewAccountNo = accEnqDB.GetNewAccountNumber(oldAccountNo);
            if (NewAccountNo.Equals(string.Empty))
            {
                lblNewAccountNo.Text=string.Empty;
                lblMsg.Text = "Account not found";
            }
            else
            {
                lblMsg.Text = "Account found";
                lblNewAccountNo.Text = NewAccountNo;
            }
        }
    }
}
