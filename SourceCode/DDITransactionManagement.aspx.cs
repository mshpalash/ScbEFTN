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
using EFTN.Utility;

namespace EFTN
{
    public partial class DDITransactionManagement : System.Web.UI.Page
    {
        private static DataTable myDTReturnReceived = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("215"))
                {
                    ImportDDITransaction();
                    BindDDICounter();
                }
                else
                {
                    divDDI.Visible = false;
                    //linkBtnImportTransaction.Visible = false;
                    //lblMsgTransactionReceived.Visible = false;
                }
            }
        }

        private void BindDDICounter()
        {
            DDIManager ddiManager = new DDIManager();
            DataTable dt = ddiManager.GetDDITransactionCounter();
            int returnCounter = ParseData.StringToInt(dt.Rows[0]["ReturnCounter"].ToString());
            lblDDIReturnCounter.Text = "(" + returnCounter.ToString() + ")";
            if (returnCounter > 0)
            {
                lblDDIReturnCounter.ForeColor = System.Drawing.Color.Red;
            }
        }


        protected void linkBtnImportTransaction_Click(object sender, EventArgs e)
        {
            int UserID = ParseData.StringToInt(Request.Cookies["UserID"].Value);
            int DepartmentID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            DDIManager ddiManager = new DDIManager();
            ddiManager.ImportDDI(UserID, DepartmentID);
            ImportDDITransaction();

        }

        private void ImportDDITransaction()
        {
            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            dtlImportTransaction.DataSource = ds.GetDataSourceAllType("EFTNDDI"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
            dtlImportTransaction.DataBind();
            if (dtlImportTransaction.Items.Count == 0)
            {
                lblMsgTransactionReceived.Text = "No files.";
            }
        }

        private void BindReturnData()
        {
            DDIManager ddiManager = new DDIManager();
            myDTReturnReceived = ddiManager.GetReturnReceivedForDDI();
            dtgInwardReturnMaker.DataSource = myDTReturnReceived;
            dtgInwardReturnMaker.DataBind();
        }

        protected void linkBtnGenerateReturn_Click(object sender, EventArgs e)
        {
            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            DataTable dtDDIReturn = ds.GetDataSourceAllType("EFTNDDIUpload");

            if (dtDDIReturn.Rows.Count > 0)
            {
                lblErrMsg.Text = "Already a file exists in RCMS upload folder. Please try again later.";
                lblErrMsg.ForeColor = System.Drawing.Color.Red;
                lblErrMsg.Visible = true;
                return;
            }
            else
            {
                BindReturnData();
                if (dtgInwardReturnMaker.Items.Count > 0)
                {
                    lblErrMsg.Text = "Please complete the inward return process then generate return file for RCMS.";
                    lblErrMsg.ForeColor = System.Drawing.Color.Red;
                    lblErrMsg.Visible = true;
                    return;
                }
                
                DDIManager ddiManager = new DDIManager();
                lblErrMsg.Text = ddiManager.GenerateDDIReturn();
                ddiManager.RunCommandPrompt();
            }
        }

        protected void dtgInwardReturnMaker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgInwardReturnMaker.CurrentPageIndex = e.NewPageIndex;
            dtgInwardReturnMaker.DataSource = myDTReturnReceived;
            dtgInwardReturnMaker.DataBind();
        }

    }
}
