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
using System.IO;
using EFTN.Utility;
using Ionic.Zip;

namespace EFTN
{
    public partial class StandingOrderTransactionListForChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string trID = Request.Params["StandingOrderBatchID"].ToString();
                Guid standingOrderBatchID = new Guid(trID);

                BindDataForTransactionSent(standingOrderBatchID);
                BindDataForSTDOBatchDetails(standingOrderBatchID);
            }
        }

        private void BindDataForTransactionSent(Guid standingOrderBatchID)
        {
            //int DepartmentID = 0;
            //if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            //{
            //    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            //}
            EFTN.component.StandingOrderDB STODB = new EFTN.component.StandingOrderDB();

            dtgSTDOTransactionSent.DataSource = STODB.GetStandingOrderDateByStandingOrderBatchID(standingOrderBatchID);
            dtgSTDOTransactionSent.DataBind();
        }
        private void BindDataForSTDOBatchDetails(Guid standingOrderBatchID)
        {
            //int DepartmentID = 0;
            //if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            //{
            //    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            //}
            EFTN.component.StandingOrderDB STODB = new EFTN.component.StandingOrderDB();

            dtgStandingOrderBatch.DataSource = STODB.GetAllActiveStandingOrderListBySTDOBatchID(standingOrderBatchID);
            dtgStandingOrderBatch.DataBind();
        }
    }
}
