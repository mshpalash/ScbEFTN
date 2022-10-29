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
using iTextSharp.text;
using iTextSharp.text.pdf;
using EFTN.Utility;
using EFTN.component;

namespace EFTN
{
    public partial class FlatFileForTransactionSentChargeToHUB : System.Web.UI.Page
    {
        private static DataTable myDataTable = new DataTable();
        //private EFTN.BLL.FinacleManager fm;

        private bool firstTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();

                //if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("225"))
                //{
                //    if (fm == null)
                //    {
                //        fm = new EFTN.BLL.FinacleManager();
                //    }
                //    if (!fm.IsConnected)
                //    {
                //        fm.Connect();
                //    }

                //    firstTime = true;
                //}
            }
        }

        void Page_Unload(Object sender, EventArgs e)
        {
            //if (!IsPostBack && fm != null && fm.IsConnected)
            //{
            //    if (firstTime)
            //    {
            //        firstTime = false;
            //    }
            //    else
            //    {
            //        fm.Disconnect();
            //    }
            //}
        }


        private void BindData()
        {
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            if (SelectedBank.Equals("115"))
            {
                EFTN.component.CSVReportDB cSVReportDB = new EFTN.component.CSVReportDB();
                myDataTable = cSVReportDB.EFTGetBatchesForTransactionSentForSCBCSV(
                                                                      ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                    + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
                dtgEFTChecker.DataSource = myDataTable;
                try
                {
                    dtgEFTChecker.DataBind();
                }
                catch
                {
                    dtgEFTChecker.CurrentPageIndex = 0;
                    dtgEFTChecker.DataBind();
                }
            }
        }

        protected void dtgEFTChecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTChecker.CurrentPageIndex = e.NewPageIndex;
            dtgEFTChecker.DataSource = myDataTable;
            dtgEFTChecker.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void dtgEFTChecker_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            EFTN.component.TransactionToCBSDB transactionToCBSDB = new EFTN.component.TransactionToCBSDB();
            DataTable dtFlatFile;
            DataTable dtFlatFileChargeBulk;

            Guid TransactionID = (Guid)dtgEFTChecker.DataKeys[e.Item.ItemIndex];
            
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            SentBatchDB sentBatchDB = new SentBatchDB();

            DataTable dtBatchNumber = sentBatchDB.GetBatchNumberByTransactionID(TransactionID);
            string batchnumber = string.Empty;

            if (dtBatchNumber.Rows.Count > 0)
            {
                batchnumber = dtBatchNumber.Rows[0]["BatchNumber"].ToString();
            }

            if (e.CommandName == "GenerateFlatFile")
            {
                dtFlatFile = transactionToCBSDB.GetHSBCTransactionSentForChargeNonBulk(TransactionID);
                dtFlatFileChargeBulk = transactionToCBSDB.GetHSBCTransactionSentForChargeforBulkOnly(TransactionID);

                GenerateFlatFile(dtFlatFile, dtFlatFileChargeBulk, batchnumber);
            }
        }

        private void GetFinacleInsertMessage(Guid TransactionID)
        {
            //ISOMessageDB isoMessageDB = new ISOMessageDB();
            ////DataTable dtFinacleMessage = isoMessageDB.GetISOMessageByTransactionID(TransactionID, ISOMessageStatus.ReverseMessageSent);
            //dtgISOMessageStatus.DataSource = isoMessageDB.GetISOMessageByTransactionID(TransactionID);
            //dtgISOMessageStatus.DataBind();
        }

        private void GenerateFlatFile(DataTable dt, DataTable dtBulk, string batchnumber)
        {
            EFTN.BLL.FlatFileChargeClient fc = new EFTN.BLL.FlatFileChargeClient();
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            string flatfileResult = fc.CreatFlatFileForTransactionSentChargeToHUB(dt, dtBulk);

            string fileName = string.Empty;
            fileName = "BDOC" + HSBCNamingConvension.GetMonth(System.DateTime.Now.Month) + System.DateTime.Now.Day.ToString().PadLeft(2, '0') + batchnumber.Substring(4, 2);

            //string fileName = "CBS" + "-" + "FlatFile-TS-Charge" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
            Response.Clear();
            Response.AddHeader("content-disposition",
                     "attachment;filename=" + fileName + ".txt");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.text";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite =
                          new HtmlTextWriter(stringWrite);
            Response.Write(flatfileResult.ToString());
            Response.End();

            //Response.Redirect("~/FileWatcherChecker.aspx?PathKey=EFTCBSExport");
        }
    }
}
