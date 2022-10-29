using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace EFTN
{
    public class Global : System.Web.HttpApplication
    {
        public static string lastFileCreatedTime = string.Empty;
        public static string lastImportedTime = string.Empty;
        public static bool isValid;

        public static string fileGenerateStatus = string.Empty;
        public static string returnFileGenerateStatus = string.Empty;

        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                if (EFTNAccelerator.FloraEFTNAccelerator.IsEFTExpired())
                {
                    Response.End();
                }
            }
            catch
            {
                Response.End();
            }
            /*EFT Security*/
            //try
            //{
            //    //if (!EFTN.BLL.EFTApplicationRun.RunEFTApplication())
            //    //{
            //    //    Response.End();
            //    //}
            //    if (!EFTN.BLL.EFTApplicationRun.EFTSecurityCheck())
            //    {
            //        isValid = false;
            //    }
            //    else
            //    {
            //        isValid = true;
            //    }

            //    //if (!isValid)
            //    //{
            //    //    Response.End();
            //    //}
            //}
            //catch
            //{
            //}
            /*****************************For HSBC************************/
            //try
            //{
            //    if (!EFTN.BLL.EFTApplicationRun.EFTSecurityCheck())
            //    {
            //        isValid = false;
            //    }
            //    else
            //    {
            //        isValid = true;
            //    }

            //    if (!isValid)
            //    {
            //        Response.End();
            //    }
            //}
            //catch
            //{
            //}
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            //if (!isValid)
            //{
            //    Response.End();
            //}
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                if (Session["usernamekey"] != null)
                {
                    string cacheKey = Session["usernamekey"].ToString();
                    if ((string)HttpContext.Current.Cache[cacheKey] != Session.SessionID)
                    {
                        FormsAuthentication.SignOut();
                        Session.Abandon();
                        Response.Redirect("Login.aspx");
                    }

                    string user = (string)HttpContext.Current.Cache[cacheKey];
                }
            }
        }
    }
}