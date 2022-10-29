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
    public partial class SendIBSTransactionToCityCBS : System.Web.UI.Page
    {
        private static DataTable myDataTable = new DataTable();
        //private EFTN.BLL.FinacleManager fm;

        private bool firstTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("225"))
                {
                    Response.Redirect("~/Default.aspx");
                }
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

            EFTN.component.CityIBSDB cityIBSDB = new EFTN.component.CityIBSDB();
            myDataTable = cityIBSDB.GetBatchesForTransactionSentForIBSCity(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'));
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
            CityIBSDB cityIBSDB = new CityIBSDB();
            DataTable dtFlatFile;
            Guid TransactionID = (Guid)dtgEFTChecker.DataKeys[e.Item.ItemIndex];

            if (e.CommandName == "SendTransactionToCBS")
            {
                dtFlatFile = cityIBSDB.GetBatchesForTransactionSentForIBSCityByTransactionID(TransactionID);

                if (dtFlatFile.Rows.Count > 0)
                {
                    UpdateCBS(dtFlatFile);
                }
            }
        }

        private void UpdateCBS(DataTable dt)
        {
            int n = dt.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                string transactionAmount = dt.Rows[i]["TotalAmount"].ToString();
                string remarks = (string)dt.Rows[i]["BatchNumber"];
                Guid transactionID = (Guid)dt.Rows[i]["TransactionID"];

                SendToCBS(transactionAmount, remarks);
            }
        }

        private void SendToCBS(string transactionAmount, string remarks)
        {
            string response = "";
            string UserName = ConfigurationManager.AppSettings["UserName"];
            string Pwd = ConfigurationManager.AppSettings["Pwd"];
            string DebitActNo = ConfigurationManager.AppSettings["DebitActNo"];
            string CreditActNo = ConfigurationManager.AppSettings["CreditActNo"];

            string param = "{'username':'" + UserName + "', 'password':'" + Pwd + "', 'currencyCode':'BDT', 'debitAccount':'" + DebitActNo + "','transactionAmount': '" + transactionAmount + "', 'creditAccount':'" + CreditActNo + "','remarks':'" + remarks + "'}";

            //CBLWebServices.cityService ct = new CBLWebServices.cityService();
            //response = ct.doFinacleTransaction(param);
        }
    }
}
