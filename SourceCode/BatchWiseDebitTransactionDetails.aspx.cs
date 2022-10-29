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
using EFTN.component;
using System.Data.SqlClient;
using EFTN.Utility;
using FloraSoft;
using EFTN.BLL;

namespace EFTN
{
    public partial class BatchWiseDebitTransactionDetails : System.Web.UI.Page
    {
        private static DataTable MyDt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataGrid();
            }
        }

        private void BindDataGrid()
        {
            string strTransactionID = Request.Params["TransactionEDRID"].ToString();
            Guid TransactionID = new Guid(strTransactionID);
            FCUBSTransactionsentBatchDB db = new FCUBSTransactionsentBatchDB();

            //SqlDataReader sqlDRTransaction = db.GetSentEDRByTransactionIDForBatch(EDRID);
            //dtgTransactionDetails.DataSource = sqlDRTransaction;
            //dtgTransactionDetails.DataBind();
            MyDt = db.GetSentEDRByTransactionIDForBatchDebit(TransactionID);
           
            dtgTransactionDetails.DataSource = MyDt;
            try
            {
                dtgTransactionDetails.DataBind();

            }
            catch
            {
                dtgTransactionDetails.CurrentPageIndex = 0;
                dtgTransactionDetails.DataBind();
            }
        }

        protected void dtgTransactionDetails_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgTransactionDetails.CurrentPageIndex = e.NewPageIndex;
            dtgTransactionDetails.DataSource = MyDt;
            dtgTransactionDetails.DataBind();
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            ChangeStatusOfCheckedEDR(EFTN.Utility.TransactionStatus.TransSent_Approved_By_Checker);
        }

        private void ChangeStatusOfCheckedEDR(int statusID)
        {
            //EFTN.Utility.TransactionStatus.TransSent_Approved_By_Checker
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            string transactionID = Request.Params["TransactionEDRID"].ToString();
            Guid TransactionID = new Guid(transactionID);
            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            bool CBSActive = true;
            int goodBatchCount = 0;

            if (bankCode.Equals(OriginalBankCode.UCBL))
            {
                DepartmentsDB departmentDB = new DepartmentsDB();
                DataTable dtDept = departmentDB.EFT_GetDepartmentDetailsByDepartmentID(DepartmentID);

                if (bool.Parse(dtDept.Rows[0]["CBSActive"].ToString()))
                {
                    CBSActive = true;
                }
                else
                {
                    CBSActive = false;
                }
            }

            if (bankCode.Equals(OriginalBankCode.NRB) || (bankCode.Equals(OriginalBankCode.UCBL) && CBSActive))
            {
                FCUBSRTManager fcubsManager = new FCUBSRTManager();
                goodBatchCount += fcubsManager.SendRTServiceEDRXMLForOutwardByBatchWiseUnsuccessfulDebit(TransactionID, bankCode, ApprovedBy);
            }
            else
            {
                EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                db.UpdateEDRSentStatusForBatchApproval(statusID, TransactionID, ApprovedBy);
            }

            Response.Redirect("BatchWiseDebitTransactionDetails.aspx?TransactionEDRID="+transactionID);
        }

    }
}
