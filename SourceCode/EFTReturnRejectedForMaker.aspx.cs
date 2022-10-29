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
    public partial class EFTReturnRejectedForMaker : System.Web.UI.Page
    {
        private static DataTable myDTReturnSent = new DataTable();

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

            EFTN.component.SentReturnDB sentReturnDB = new EFTN.component.SentReturnDB();
            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                myDTReturnSent = sentReturnDB.GetSentRRByCheckerRejectionForDebit(BranchID);
            }
            else
            {
                myDTReturnSent = sentReturnDB.GetSentRRByCheckerRejection(BranchID);
            }
            dtgRejectedReturn.DataSource = myDTReturnSent;
            dtgRejectedReturn.DataBind();
        }
    }
}
