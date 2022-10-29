using System;
using System.Web.UI;
using FloraSoft;
using EFTN.Utility;
using EFTN.component;
using EFTN.BLL;
using System.Configuration;
namespace EFTN
{
    public partial class ChangeAdminCheckerPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["MFAValidation"] != null && ConfigurationManager.AppSettings["MFAValidation"] == "0")
                Response.Redirect("default.aspx");

            Response.Cookies["RemainingDayToChangePassword"].Value = "0";
            if (!IsPostBack)
            {
                BindPasswordPolicy();
            }
        }

        public void BindPasswordPolicy()
        {
            PassPolicyManager policyManager = new PassPolicyManager();
            string passwordPolicy = policyManager.GetPasswordPolicy();
            txtPasswordPolicyAll.Text = passwordPolicy;
            txtPasswordPolicyAll.ForeColor = System.Drawing.Color.Blue;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Msg.Text = "";
            if (Page.IsValid)
            {
                Msg.ForeColor = System.Drawing.Color.Red;
                if (string.IsNullOrEmpty(txtNewPassword.Text))
                {
                    Msg.Text = "* Enter a new Password";
                }
                else if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    Msg.Text = "* Passwords didn't match";
                }
                else
                {
                    //UsersDB db = new UsersDB();
                    UserDB db = new UserDB();

                    PasswordPolicy policy = db.GetPasswordPolicy();

                    int numUpperLetter = 0;
                    int numLowerLetter = 0;
                    int numAlphabets = 0;
                    int numNumerics = 0;
                    int numSpecialChars = 0;
                    int numberOfLastPasswordsToAvoid = 0;
                    int status;
                    for (int i = 0; i < txtNewPassword.Text.Length; i++)
                    {
                        char nextChar = txtNewPassword.Text[i];
                        if (Char.IsDigit(nextChar))
                        {
                            numNumerics++;
                        }
                        else if (Char.IsUpper(nextChar))
                        {
                            numUpperLetter++;
                            numAlphabets++;
                        }
                        else if (Char.IsLower(nextChar))
                        {
                            numLowerLetter++;
                            numAlphabets++;
                        }
                        else if (HasSpecialCharacters(nextChar.ToString()))
                        {
                            numSpecialChars++;
                        }
                    }

                    if (txtNewPassword.Text.Length < policy.MinPasswordLength)
                    {
                        Msg.Text = "* Password should be minimum " +
                            policy.MinPasswordLength + " character length.";
                    }
                    else if (numAlphabets < policy.MinNumberOfAlphabets)
                    {
                        Msg.Text = "* password should have minimum " +
                            policy.MinNumberOfAlphabets + " alphabetic characters";
                    }
                    else if (numNumerics < policy.MinNumberOfNumerics)
                    {
                        Msg.Text = "* password should have minimum " +
                            policy.MinNumberOfNumerics + " numeric characters";
                    }
                    else if (numUpperLetter < policy.MinNumberOfUpperChar)
                    {
                        Msg.Text = "* password should have minimum " +
                            policy.MinNumberOfUpperChar + " upper letters";
                    }
                    else if (numLowerLetter < policy.MinNumberOfLowerChar)
                    {
                        Msg.Text = "* password should have minimum " +
                            policy.MinNumberOfLowerChar + " lower letters";
                    }
                    else if (numSpecialChars < policy.MinNumberOfSpecialChars)
                    {
                        Msg.Text = "* password should have minimum " +
                            policy.MinNumberOfSpecialChars + " special characters";
                    }
                    else if ((status = db.ChangePassword(Int32.Parse(Context.User.Identity.Name), db.Encrypt(txtOldPassword.Text), db.Encrypt(txtNewPassword.Text), GetIPAddress(), policy.NumberOfLastPasswordsToAvoid)) == 0)
                    {
                        Msg.Text = "* Your old password is incorrect";
                    }
                    else if ((status = db.ChangePassword(Int32.Parse(Context.User.Identity.Name), db.Encrypt(txtOldPassword.Text), db.Encrypt(txtNewPassword.Text), GetIPAddress(), policy.NumberOfLastPasswordsToAvoid)) == 2)
                    {
                        Msg.Text = "* Password already used last " + policy.NumberOfLastPasswordsToAvoid + " times three times";
                    }
                    else
                    {
                        UserHistoryDB userHistoryDB = new UserHistoryDB();
                        userHistoryDB.InsertUserHistory(ParseData.StringToInt(Request.Cookies["UserID"].Value),
                                                          GetIPAddress()
                                                        , UserHistoryVar.PASSWORD_CHANGED
                                                        , ParseData.StringToInt(Request.Cookies["UserID"].Value)
                                                        , ParseData.StringToInt(Request.Cookies["RoleID"].Value)
                                                        , ParseData.StringToInt(Request.Cookies["BranchID"].Value)
                                                        , Request.Cookies["UserName"].Value
                                                        , ParseData.StringToInt(Request.Cookies["DepartmentID"].Value)
                                                        , "0"
                                                        , Request.Cookies["LoginID"].Value
                                                        , db.Encrypt(txtNewPassword.Text)
                                                        , UserHistoryVar.USER_ACTIVE);
                        Response.Redirect("LogOut.aspx");
                        //Msg.ForeColor = System.Drawing.Color.Blue;
                        //Msg.Text = "Password changed successfully";
                        //Response.Cookies["RemainingDayToChangePassword"].Value = "0";

                    }
                }

                //UserDB userDB = new UserDB();
                //int Status = userDB.ChangePassword(Int32.Parse(Context.User.Identity.Name), userDB.Encrypt(txtOldPassword.Text), userDB.Encrypt(txtNewPassword.Text), Request.UserHostAddress);
                //if (Status == 1)
                //{
                //    Msg.Text = "Password Successfully Changed.";
                //    Msg.ForeColor = System.Drawing.Color.Blue;
                //}
                //if (Status == 0)
                //{
                //    Msg.Text = "Old Password was not correct.";
                //    Msg.ForeColor = System.Drawing.Color.Red;
                //}
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
            //Response.Redirect("AdminChecker.aspx");
        }

        private string GetIPAddress()
        {
            string ipaddress;
            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];

            return ipaddress;
        }

        public bool HasSpecialCharacters(string str)
        {
            string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialCharactersArray = specialCharacters.ToCharArray();

            int index = str.IndexOfAny(specialCharactersArray);
            //index == -1 no special characters
            if (index == -1)
                return false;
            else
                return true;
        }
    }
}
