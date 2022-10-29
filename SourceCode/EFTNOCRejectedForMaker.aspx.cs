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

namespace EFTN
{
    public partial class EFTNOCRejectedForMaker : System.Web.UI.Page
    {
        private static DataTable myDTNOCSent = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }


        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            int BranchID = 0;

            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            if (DepartmentID == 0)
            {
                BranchID = 0;
            }
            else if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                BranchID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value);
            }

            EFTN.component.SentNOCDB sentNOCDB = new EFTN.component.SentNOCDB();
            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                myDTNOCSent = sentNOCDB.GetSentNOCByCheckerRejectionForDebit(BranchID);
            }
            else
            {
                myDTNOCSent = sentNOCDB.GetSentNOCByCheckerRejection(BranchID);
            }
            dtgRejectedNOC.DataSource = myDTNOCSent;
            dtgRejectedNOC.DataBind();

        }
    }
}
