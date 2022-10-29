using System;
using System.Data;
using System.Configuration;
using System.Web.Security;
using EFTN.Utility;
using EFTN.component;
using EFTNAccelerator;
using System.Web;

namespace FloraSoft
{
    public partial class LoginFlora : System.Web.UI.Page
    {
        //private void Test()
        //{
        //    FloraSoft.UserDB db = new UserDB();
        //    db.Encrypt("123456");
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["MFAValidation"] != null && ConfigurationManager.AppSettings["MFAValidation"] == "0")
                Response.Redirect("login.aspx");
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            //Test();
            string username = UserName.Text;
            string userpass = Pass.Text;
            loginPass.Value = userpass;
            lastLoginID.Value = username;
            string key = username;// +userpass;
            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Cache[key] != null)
                {
                    if ((string)HttpContext.Current.Cache[key] != Session.SessionID)
                    {
                        MyMessage.Text = "You are already logged in from different location";
                        return;
                        //divConcurrent.Visible = true;
                    }
                    else
                    {
                        ContinueLogin(username, userpass);
                    }
                }
                else
                {
                    ContinueLogin(username, userpass);
                }
            }
            else
            {
                ContinueLogin(username, userpass);
            }

            //ContinueLogin(username, userpass);
        }

        private void ContinueLogin(string UserName, string Pass)
        {
            string UserID = "0";

            UserDB db = new UserDB();
            UserInfo uinfo = new UserInfo();

            string myIPAddress = GetIpAddress();
            uinfo = db.Login(UserName, Pass, myIPAddress);
            UserID = uinfo.UserID;
            string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            if (UserID == "0")
            {
                MyMessage.Text = uinfo.LoginMsg+ " UserID will be locked after 3 unsuccessful login.";
                if (lastLoginID.Value.Equals(UserName) && !lastLoginID.Value.Equals(string.Empty))
                {
                    int loginTime = ParseData.StringToInt(hiddenLoginFailed.Value) + 1;
                    hiddenLoginFailed.Value = loginTime.ToString();
                }
                else
                {
                    hiddenLoginFailed.Value = "1";
                }
                if (ParseData.StringToInt(hiddenLoginFailed.Value) == 3)
                {
                    db.UpdateUserByLoginID(UserName, "INACTIVE");
                    UserHistoryDB userHistoryDB = new UserHistoryDB();
                    userHistoryDB.InsertUserHistory(0, myIPAddress
                                                    , UserHistoryVar.LOCKED_FOR_WRONGPASSWORD, 0, 0, 0, UserName
                                                    , 0, "0", UserName, "", "INACTIVE");
                    MyMessage.Text = "UserID Locked. Please contact with application administrator";
                    hiddenLoginFailed.Value = "";
                }
                lastLoginID.Value = UserName;
                divConcurrent.Visible = false;
            }
            else
            {
                string key = UserName;// +Pass;


                string concurrentLogIn = (string)HttpContext.Current.Cache[key];

                TimeSpan TimeOut = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
                HttpContext.Current.Cache.Insert(key,
                    Session.SessionID,
                    null,
                    DateTime.MaxValue,
                    TimeOut,
                    System.Web.Caching.CacheItemPriority.NotRemovable,
                    null);

                Session["usernamekey"] = key;

                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("215"))
                {
                    int totalNoOfUser = db.GetTotalNoOfUser();
                    if (UserLicense.IsInvalideUser(totalNoOfUser))
                    {
                        MyMessage.Text = "Login failed: Please renew user license agreement";
                        return;
                    }
                }
                int remainingDayToChangePassword = 0;

                if (originBank.Equals(OriginalBankCode.SCB) //SCB
                    //|| ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("115") //HSBC
                    //|| ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("225") //THE CITY BANK
                    //|| ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("245") //UCBL
                    )
                {
                    UserHistoryDB userHistoryDB = new UserHistoryDB();
                    //CHANGED
                    //userHistoryDB.InsertUserHistory(0, myIPAddress
                    //            , UserHistoryVar.LOGGED_IN, uinfo.UserID, 0, 0, UserName
                    //            , 0, "0", UserName, "", "ACTIVE");

                    userHistoryDB.InsertUserHistory(0, myIPAddress
                                , UserHistoryVar.LOGGED_IN, ParseData.StringToInt(uinfo.UserID)
                                , ParseData.StringToInt(uinfo.RoleID), ParseData.StringToInt(uinfo.BranchID)
                                , uinfo.UserName, ParseData.StringToInt(uinfo.DepartmentID)
                                , "", uinfo.LoginID, "", "ACTIVE");


                    DataTable dtUserLastPassHistory = userHistoryDB.GetLastPasswordChangeHistoryByUserID(ParseData.StringToInt(uinfo.UserID));
                    PasswordPolicy policy = db.GetPasswordPolicy();

                    if (dtUserLastPassHistory.Rows.Count > 0)
                    {
                        DateTime dtimeLastPasswordChanged = DateTime.Parse(dtUserLastPassHistory.Rows[0]["HistoryTime"].ToString());
                        DateTime expireDateOfChangePassword = dtimeLastPasswordChanged.AddDays(policy.ExpireDuration);

                        if (expireDateOfChangePassword > System.DateTime.Now)
                        {
                            TimeSpan datediff = (expireDateOfChangePassword - System.DateTime.Now);
                            remainingDayToChangePassword = datediff.Days;
                        }
                        else
                        {
                            remainingDayToChangePassword = 0;
                        }
                    }
                    else
                    {
                        remainingDayToChangePassword = 0;
                    }
                }

                Response.Cookies["RemainingDayToChangePassword"].Value = remainingDayToChangePassword.ToString();

                //if (remainingDayToChangePassword > 0)
                //{
                Response.Cookies["RoleID"].Value = uinfo.RoleID;
                Response.Cookies["RoleName"].Value = uinfo.RoleName;
                Response.Cookies["BankCode"].Value = uinfo.BankCode;
                Response.Cookies["UserName"].Value = uinfo.UserName;
                Response.Cookies["BranchID"].Value = uinfo.BranchID;
                Response.Cookies["BranchName"].Value = uinfo.BranchName;
                Response.Cookies["BankName"].Value = uinfo.BankName;
                Response.Cookies["UserID"].Value = uinfo.UserID;
                Response.Cookies["DepartmentID"].Value = uinfo.DepartmentID;
                Response.Cookies["DepartmentName"].Value = uinfo.DepartmentName;
                Response.Cookies["LoginID"].Value = uinfo.LoginID;
                Response.Cookies["LastLoginTime"].Value = uinfo.LastLoginTime;

                FormsAuthentication.RedirectFromLoginPage(UserID, false);
                if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("2"))
                {
                    Response.Redirect("ChangeMakerPassword.aspx");
                }
                else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("3"))
                {
                    Response.Redirect("ChangeCheckerPassword.aspx");
                }
                else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("4"))
                {
                    Response.Redirect("ChangeAdminCheckerPassword.aspx");
                }
                else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("5"))
                {
                    Response.Redirect("ChangeReconciliatorPassword.aspx");
                }
                else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("1"))
                {
                    Response.Redirect("ChangeUserPassword.aspx");
                }
                else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("9"))
                {
                    Response.Redirect("ChangeMasterUserPassword.aspx");
                }
                else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("10"))
                {
                    Response.Redirect("ChangeSuperAdminCheckerPassword.aspx");
                }
                else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("12"))
                {
                    Response.Redirect("EFTCSGReportViewer.aspx");
                }
                else
                {
                    Response.Redirect("BranchMessages.aspx");
                }
                //}
                //else
                //{
                //    MyMessage.Text = "Your Password has been expired. Contact with your admininstrator";
                //    return;                    
                //}
            }
        }

        private string GetIpAddress()
        {
            string ipaddress;
            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];

            return ipaddress;
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            ContinueLogin(lastLoginID.Value, loginPass.Value);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}

