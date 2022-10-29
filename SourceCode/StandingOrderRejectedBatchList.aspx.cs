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
    public partial class StandingOrderRejectedBatchList : System.Web.UI.Page
    {
        private static DataTable dtStandingOrderBatchRejectedList = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataForRejectBatchList();
            }
        }

        private void BindDataForRejectBatchList()
        {
            //int DepartmentID = 0;
            //if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            //{
            //    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            //}
            EFTN.component.StandingOrderDB STODB = new EFTN.component.StandingOrderDB();
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            dtStandingOrderBatchRejectedList = STODB.GetAllStandingOrderRejectedList(UserID);
            dtgStandingOrderBatch.DataSource = dtStandingOrderBatchRejectedList;
            dtgStandingOrderBatch.DataBind();
        }

        protected void dtgStandingOrderBatch_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgStandingOrderBatch.CurrentPageIndex = e.NewPageIndex;
            dtgStandingOrderBatch.DataSource = dtStandingOrderBatchRejectedList;
            dtgStandingOrderBatch.DataBind();
        }

        protected void linkBtnDelete_Click(object sender, EventArgs e)
        {
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            EFTN.component.StandingOrderDB stdDB = new EFTN.component.StandingOrderDB();
            for (int i = 0; i < dtgStandingOrderBatch.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgStandingOrderBatch.Items[i].FindControl("cbxSentBatchTS");
                if (cbx.Checked)
                {
                    Guid standingOrderBatchID = (Guid)dtgStandingOrderBatch.DataKeys[i];
                    stdDB.DeleteStandingOrderBatchByStandingOrderBatchID(standingOrderBatchID, UserID);
                }
            }

            BindDataForRejectBatchList();
        }
    }
}
