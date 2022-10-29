using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Configuration;
using EFTNAccelerator;
using EFTN.Utility;
using EFTN.component;
using System.Web.Caching;

namespace FloraSoft
{
    public class SamlConsume : IHttpHandler, IRequiresSessionState
    {
        HttpContext samlContext;
        string csmmsg;

        public SamlConsume() {}

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string urlHost = context.Request.UrlReferrer != null ? context.Request.UrlReferrer.Host : context.Request.Url.Host;

            switch (context.Request.HttpMethod)
            {
                case "POST":
                    SamlLogManager.WriteLog("RESPONSE FROM : " + urlHost);  
                
                    //string samlCertificate1 = ConfigurationManager.AppSettings["SAMLCertificate1"];

                    Response samlResponse = new Response(context.Request.Form["SAMLResponse"]);
                    
                    if (samlResponse.IsValid())
                    {
                        string logInID;

                        //Uri idpUri = new Uri(ConfigurationManager.AppSettings["IdpDestination"]);
                        //if (context.Request.Cookies["IdpRequest"] != null)
                        //{
                        //    logInID = samlResponse.GetNameID();
                        //    SamlLogManager.WriteLog("RESPONSE NAMEID : " + logInID);

                        //    logInID = logInID.Substring(logInID.Length - 7);
                        //}
                        //else
                        //{
                        //    logInID = samlResponse.GetNameID();
                        //    SamlLogManager.WriteLog("RESPONSE NAMEID : " + logInID);
                        //}

                        logInID = samlResponse.GetNameID();
                        SamlLogManager.WriteLog("RESPONSE NAMEID : " + logInID);

                        samlContext = context;
                        ContinueLogin(logInID);

                        SamlLogManager.WriteLog("Local DB Check : " + csmmsg);
                        if (string.IsNullOrWhiteSpace(csmmsg))
                            //context.Response.Redirect("~/default.aspx");
                            context.Response.Redirect("~/BranchMessages.aspx");
                        context.Response.Redirect(string.Format("~/Login?csmmsg={0}", csmmsg),true);
                    }
                    break;
            }

            context.Response.Redirect(string.Format("~/Login?csmmsg={0} User Authentication Failed", urlHost.Split('.')[1]));
        }

        private void LocalDBLogin(string LoginID)
        {
            //string username = UserName.Text;
            //string userpass = Pass.Text;
            //loginPass.Value = userpass;
            //lastLoginID.Value = username;
            string key = LoginID;
            if (samlContext.Session != null)
            {
                if (samlContext.Cache[key] != null)
                {
                    if ((string)samlContext.Cache[key] != samlContext.Session.SessionID)
                    {
                        //divConcurrent.Visible = true;
                        csmmsg = "You are already logged in from different location";
                        //ContinueLogin(LoginID);
                        return;
                    }
                    else
                    {
                        ContinueLogin(LoginID);
                    }
                }
                else
                {
                    ContinueLogin(LoginID);
                }
            }
            else
            {
                ContinueLogin(LoginID);
            }
        }

        private void ContinueLogin(string UserName)
        {
            string UserID = "0";

            UserDB db = new UserDB();
            UserInfo uinfo = new UserInfo();

            string myIPAddress = GetIpAddress();
            string myUserIdentity;

            HttpCookie ckUserIDT = samlContext.Request.Cookies["UserIDT" + UserName];
            if (ckUserIDT != null)
                myUserIdentity = ckUserIDT.Value;
            else
                myUserIdentity = Guid.NewGuid().ToString().Replace("-", string.Empty);

            uinfo = db.SAMLLogin(UserName, myIPAddress, myUserIdentity);
            UserID = uinfo.UserID;
            SamlLogManager.WriteLog("DB User ID : " + UserID);
            string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            //SamlLogManager.WriteLog("Origin Bank : " + originBank);


            if (UserID == "0")
            {
                csmmsg = uinfo.LoginMsg;
                //csmmsg = string.Format("{0} UserID will be locked after 3 unsuccessful login", (string.IsNullOrWhiteSpace(uinfo.LoginMsg) ? "Invalid User!" : uinfo.LoginMsg));

                //SamlLogManager.WriteLog("DB Log Msg : " + csmmsg);
                //if (lastLoginID.Value.Equals(UserName) && !lastLoginID.Value.Equals(string.Empty))
                //{
                //    int loginTime = ParseData.StringToInt(hiddenLoginFailed.Value) + 1;
                //    hiddenLoginFailed.Value = loginTime.ToString();
                //}
                //else
                //{
                //    hiddenLoginFailed.Value = "1";
                //}
                //if (ParseData.StringToInt(hiddenLoginFailed.Value) == 3)
                //{
                //    db.UpdateUserByLoginID(UserName, "INACTIVE");
                //    UserHistoryDB userHistoryDB = new UserHistoryDB();
                //    userHistoryDB.InsertUserHistory(0, myIPAddress
                //                                    , UserHistoryVar.LOCKED_FOR_WRONGPASSWORD, 0, 0, 0, UserName
                //                                    , 0, "0", UserName, "", "INACTIVE");
                //    MyMessage.Text = "UserID Locked. Please contact with application administrator";
                //    hiddenLoginFailed.Value = "";
                //}
                //lastLoginID.Value = UserName;
                //divConcurrent.Visible = false;
            }
            else
            {
                string key = UserName;// +Pass;


                //string concurrentLogIn = (string)samlContext.Cache[key];

                //TimeSpan TimeOut = new TimeSpan(0, 0, samlContext.Session.Timeout, 0, 0);
                //samlContext.Cache.Insert(key,
                //    samlContext.Session.SessionID,
                //    null,
                //    DateTime.MaxValue,
                //    TimeOut,
                //    System.Web.Caching.CacheItemPriority.NotRemovable,
                //    null);

                //samlContext.Session["usernamekey"] = key;

                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("215"))
                {
                    int totalNoOfUser = db.GetTotalNoOfUser();
                    if (UserLicense.IsInvalideUser(totalNoOfUser))
                    {
                        csmmsg = "Login failed: Please renew user license agreement";
                    }
                }
                //int remainingDayToChangePassword = 0;

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


                    //DataTable dtUserLastPassHistory = userHistoryDB.GetLastPasswordChangeHistoryByUserID(ParseData.StringToInt(uinfo.UserID));
                    //PasswordPolicy policy = db.GetPasswordPolicy();

                    //if (dtUserLastPassHistory.Rows.Count > 0)
                    //{
                    //    DateTime dtimeLastPasswordChanged = DateTime.Parse(dtUserLastPassHistory.Rows[0]["HistoryTime"].ToString());
                    //    DateTime expireDateOfChangePassword = dtimeLastPasswordChanged.AddDays(policy.ExpireDuration);

                    //    if (expireDateOfChangePassword > System.DateTime.Now)
                    //    {
                    //        TimeSpan datediff = (expireDateOfChangePassword - System.DateTime.Now);
                    //        remainingDayToChangePassword = datediff.Days;
                    //    }
                    //    else
                    //    {
                    //        remainingDayToChangePassword = 0;
                    //    }
                    //}
                    //else
                    //{
                    //    remainingDayToChangePassword = 0;
                    //}
                }

                //Response.Cookies["RemainingDayToChangePassword"].Value = remainingDayToChangePassword.ToString();

                //if (remainingDayToChangePassword > 0)
                //{
                samlContext.Response.Cookies["RoleID"].Value = uinfo.RoleID;
                samlContext.Response.Cookies["RoleName"].Value = uinfo.RoleName;
                samlContext.Response.Cookies["BankCode"].Value = uinfo.BankCode;
                samlContext.Response.Cookies["UserName"].Value = uinfo.UserName;
                samlContext.Response.Cookies["BranchID"].Value = uinfo.BranchID;
                samlContext.Response.Cookies["BranchName"].Value = uinfo.BranchName;
                samlContext.Response.Cookies["BankName"].Value = uinfo.BankName;
                samlContext.Response.Cookies["UserID"].Value = uinfo.UserID;
                samlContext.Response.Cookies["DepartmentID"].Value = uinfo.DepartmentID;
                samlContext.Response.Cookies["DepartmentName"].Value = uinfo.DepartmentName;
                samlContext.Response.Cookies["LoginID"].Value = uinfo.LoginID;
                samlContext.Response.Cookies["LastLoginTime"].Value = uinfo.LastLoginTime;
                if (ckUserIDT == null)
                {
                    ckUserIDT = samlContext.Response.Cookies["UserIDT" + UserName];
                    ckUserIDT.Value = myUserIdentity;
                    ckUserIDT.Expires = DateTime.Now.AddDays(30);
                    ckUserIDT.HttpOnly = true;
                    ckUserIDT.Path = "/;SameSite=None";
                    ckUserIDT.Secure = true;
                }
                else
                    ckUserIDT.Expires = DateTime.Now.AddDays(30);

                SamlLogManager.WriteLog("Set Cookies : True");

                FormsAuthentication.RedirectFromLoginPage(UserID, false);
                //if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("2"))
                //{
                //    Response.Redirect("ChangeMakerPassword.aspx");
                //}
                //else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("3"))
                //{
                //    Response.Redirect("ChangeCheckerPassword.aspx");
                //}
                //else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("4"))
                //{
                //    Response.Redirect("ChangeAdminCheckerPassword.aspx");
                //}
                //else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("5"))
                //{
                //    Response.Redirect("ChangeReconciliatorPassword.aspx");
                //}
                //else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("1"))
                //{
                //    Response.Redirect("ChangeUserPassword.aspx");
                //}
                //else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("9"))
                //{
                //    Response.Redirect("ChangeMasterUserPassword.aspx");
                //}
                //else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("10"))
                //{
                //    Response.Redirect("ChangeSuperAdminCheckerPassword.aspx");
                //}
                //else if (remainingDayToChangePassword == 0 && uinfo.RoleID.Equals("12"))
                //{
                //    Response.Redirect("EFTCSGReportViewer.aspx");
                //}
                //else
                //{
                //    Response.Redirect("BranchMessages.aspx");
                //}
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
            ipaddress = samlContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = samlContext.Request.ServerVariables["REMOTE_ADDR"];

            return ipaddress;
        }


        private string GetDeviceName()
        {
            string DeviceName;
            string[] computer_name = System.Net.Dns.GetHostEntry(
            samlContext.Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
            DeviceName = computer_name[0].ToString();
            if (DeviceName == "" || DeviceName == null)
                DeviceName = samlContext.Request.ServerVariables["REMOTE_ADDR"];

            //string DeviceName;
            //DeviceName = samlContext.InetAddress.getLocalHost().getHostName();

            //string DeviceName;
            ////DeviceName = System.Net.Dns.GetHostName().ToString(); 
            //DeviceName = (System.Net.Dns.GetHostEntry(samlContext.Request.ServerVariables["REMOTE_ADDR"]).HostName);
            //if (DeviceName == "" || DeviceName == null)
            //    DeviceName = samlContext.Request.ServerVariables["REMOTE_ADDR"];

            return DeviceName;
        }
    }
}