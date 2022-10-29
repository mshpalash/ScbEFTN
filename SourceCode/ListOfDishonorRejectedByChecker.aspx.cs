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
    public partial class ListOfDishonorRejectedByChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDishonorListRejectedByChecker();
            }
        }

        private void BindDishonorListRejectedByChecker()
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
            EFTN.component.ApprovedDishonorDB approvedDishonorDB = new EFTN.component.ApprovedDishonorDB();
            dtgListOfDishonorRejectedByChecker.DataSource = approvedDishonorDB.GetRejectedReceivedDishonorWhichWereApprovedByMaker(BranchID);
            dtgListOfDishonorRejectedByChecker.DataBind();
        }
    }
}
