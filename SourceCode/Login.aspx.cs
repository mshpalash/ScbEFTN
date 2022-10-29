using System;
using System.Configuration;
using System.Web;

namespace FloraSoft
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["MFAValidation"] != null && ConfigurationManager.AppSettings["MFAValidation"] == "1")
                Response.Redirect("loginflora.aspx");

            if (Request.IsAuthenticated)
                Response.Redirect("default.aspx");


            string authMsg = Request.QueryString.Get("csmmsg");
            MyMessage.Text = authMsg;
            bool emptyMessege = string.IsNullOrWhiteSpace(authMsg);
            MyMessage.Visible = !emptyMessege;

            Uri idpUri = new Uri(ConfigurationManager.AppSettings["IdpDestination"]);
            if (emptyMessege && Request.UrlReferrer != null && Request.UrlReferrer.Host == idpUri.Host && Request.UrlReferrer.Port == idpUri.Port)
            {
                SamlLogManager.WriteLog("Idp Response URL : " + Request.UrlReferrer.AbsoluteUri);

                //if (Request.Cookies["IdpRequest"] == null)
                //{
                //    HttpCookie idpCookie = Response.Cookies["IdpRequest"];
                //    idpCookie.Value = Request.UrlReferrer.Host;
                //    idpCookie.Expires = DateTime.Now.AddDays(1);
                //    idpCookie.HttpOnly = true;
                //    idpCookie.Path = "/";
                //    idpCookie.Secure = true;
                //}

                SAMLLogin();
            }

           
           
        }

        protected void btnGenericLogIn_Click(object sender, EventArgs e)
        {
            //if (Request.Cookies["IdpRequest"] != null)
            //    Response.Cookies["IdpRequest"].Expires = DateTime.Now.AddDays(-1);

            string requestUrl = ConfigurationManager.AppSettings["IdpDestination"] + "?" + ConfigurationManager.AppSettings["IdpConsumer"];
            SamlLogManager.WriteLog("Idp Request URL : " + requestUrl);

            Response.Redirect(requestUrl);
        }

        protected void btnGeneralLogIn_Click(object sender, EventArgs e)
        {
            //if (Request.Cookies["IdpRequest"] != null)
            //    Response.Cookies["IdpRequest"].Expires = DateTime.Now.AddDays(-1);

            SAMLLogin();
        }

        private void SAMLLogin()
        {
            var samlEndpoint = ConfigurationManager.AppSettings["SAMLDestination1"];
            var request = new ScbAuthRequest(ConfigurationManager.AppSettings["SAMLIssuer"], ConfigurationManager.AppSettings["SAMLConsumer"]);

            //foreach (string coki in Request.Cookies.AllKeys)
            //{
            //    if (coki.StartsWith("UserIDT"))
            //    {
            //        HttpCookie IDTCookie = Response.Cookies[coki];
            //        IDTCookie.Value = Request.Cookies[coki].Value;
            //        IDTCookie.Expires = DateTime.Now.AddDays(30); ;
            //        IDTCookie.HttpOnly = true;
            //        IDTCookie.Path = "/";
            //        IDTCookie.Secure = true;
            //    }
            //}

            Response.Redirect(request.GetRedirectUrl(samlEndpoint), false);
        }
    }
}

