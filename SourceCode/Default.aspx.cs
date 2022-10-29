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
using System.IO;

namespace EFTN
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    WSClient ws = new WSClient();
            //    ConnectionMessage.Visible = (!ws.IsConnected());
            //}
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
            else if (Request.Cookies["RoleID"].Value != "1")
            {
                Response.Redirect("AccessDenied.aspx");
            }

            if (!IsPostBack)
            {
                string originBankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

                if (originBankCode.Equals("185"))
                {
                    linkBtnSignatorManagement.Visible = true;
                    //linkBtnInwardSearch.Visible = true;
                }
                else
                {
                    linkBtnSignatorManagement.Visible = false;
                    //linkBtnInwardSearch.Visible = false;
                }

                if (originBankCode.Equals("225"))
                {
                    linkBtnUpdateWorkingDayForFridaySaturday.Visible = true;
                }
                else
                {
                    linkBtnUpdateWorkingDayForFridaySaturday.Visible = false;
                }
            }
        }


    protected void linkBtnInwardSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("InwardSearchForAdmin.aspx");
    }

    protected void linkBtnSignatorManagement_Click(object sender, EventArgs e)
    {
        Response.Redirect("SignatorManagement.aspx");
    }

    protected void linkBtnUpdateWorkingDayForFridaySaturday_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateWorkingDayOnFridaySaturday.aspx");
    }

    protected void lbtncustemail_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerEmail.aspx");
    }

        //private void BindImportWindows()
        //{
        //    EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
        //    WSClient ws = new WSClient();
        //    dtlImportTransaction.DataSource = ds.GetDataSource("EFTTransactionImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
        //    dtlImportTransaction.DataBind();
        //    if (dtlImportTransaction.Items.Count == 0)
        //    {
        //        lblMsgTransactionReceived.Text = "No files.";
        //    }

        //    dtlImportReturn.DataSource = ds.GetDataSource("EFTReturnImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardReturnPath"], "*.XML");//
        //    dtlImportReturn.DataBind();
        //    if (dtlImportReturn.Items.Count == 0)
        //    {
        //        lblImportReturn.Text = "No files.";
        //    }

        //    dtlAck.DataSource = ds.GetDataSource("EFTOutwardAck");
        //    dtlAck.DataBind();
        //    if (dtlAck.Items.Count == 0)
        //    {
        //        lblMsgAck.Text = "No files.";
        //    }
        //}
        
        //private void BindData()
        //{
        //    EFTN.component.SentBatchDB batchDB = new EFTN.component.SentBatchDB();
        //    dtgBatchTransactionSent.DataSource = batchDB.GetBatches_For_TransactionSent();
        //    dtgBatchTransactionSent.DataBind();
        //}
    }
}
