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
using iTextSharp.text;
using iTextSharp.text.pdf;
using EFTN.component;
using System.IO;
using EFTN.Utility;
using System.Data.SqlClient;

namespace EFTN
{
    public partial class BranchMessages : System.Web.UI.Page
    {
        private static DataTable dtSettlementReport = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            //string reply = "";
            //if (!Page.IsPostBack)
            //{
            //    PasswordPolicy pol = new PasswordPolicy();
            //    reply = pol.IsExpiring(Int32.Parse(Request.Cookies["DaysPassed"].Value));
            //    if (reply != "OK")
            //    {
            //        Msg.Text = reply;
            //        if (reply.IndexOf("expired") > -1)
            //        {
            //            FormsAuthentication.SignOut();
            //            btnContinue.Visible = false;
            //        }
            //    }
            //    BindData();
            //}
            if (!IsPostBack)
            {
                BindData();
            }
            //if ((MyDataList.Items.Count == 0))
            //{
            //    //GoToMyModule();
                
            //}
        }

        private void BindData()
        {
            UserHistoryDB db = new UserHistoryDB();
            DataTable dt = db.GetUnsuccesfullLoginsByUserID(Int32.Parse(Request.Cookies["UserID"].Value), DateTime.Parse(Request.Cookies["LastLoginTime"].Value));
            MyDataList.DataSource = dt;
            MyDataList.DataBind();
            Msg.Text = "Your Last successful login time : " + Request.Cookies["LastLoginTime"].Value;
        }

        protected void linkBtnContinue_Click(object sender, EventArgs e)
        {
            GoToMyModule();
        }

        private void GoToMyModule()
        {
            if ((Request.Cookies["RoleID"].Value == "1"))
            {
                Response.Redirect("Default.aspx");
            }
            if ((Request.Cookies["RoleID"].Value == "2"))
            {
                Response.Redirect("EFTMaker.aspx");
            }
            else if (Request.Cookies["RoleID"].Value == "3")
            {
                Response.Redirect("EFTChecker.aspx");
            }
            else if (Request.Cookies["RoleID"].Value == "4")
            {
                Response.Redirect("AdminChecker.aspx");
            }
            else if (Request.Cookies["RoleID"].Value == "5")
            {
                Response.Redirect("Reconciliator.aspx");
            }
            else if (Request.Cookies["RoleID"].Value == "6")
            {
                Response.Redirect("EFTCheckerAuthorizerAdmin.aspx");
            }
            else if (Request.Cookies["RoleID"].Value == "7")
            {
                Response.Redirect("EFTReportViewer.aspx");
            }
            else if (Request.Cookies["RoleID"].Value == "8")
            {
                Response.Redirect("DetailSettlementReportForClient.aspx");
            }
            else if (Request.Cookies["RoleID"].Value == "9")
            {
                Response.Redirect("SuperAdmin.aspx");
            }
            else if (Request.Cookies["RoleID"].Value == "10")
            {
                Response.Redirect("SuperAdminChecker.aspx");
            }
            else if (Request.Cookies["RoleID"].Value == "12")
            {
                Response.Redirect("EFTCSGReportViewer.aspx");
            }

        }
    }
}