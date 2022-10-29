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
using EFTN.component;

namespace EFTN
{
    public partial class SchedulerReportChekerExecutionDay : System.Web.UI.Page
    {
        private static DataTable dtStandingOrderBatchList = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();

                ddlistDayEnd.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonthEnd.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYearEnd.SelectedValue = System.DateTime.Now.Year.ToString();
            }
        }

        //private void BindDataForTransactionSent()
        //{
        //    EFTN.component.StandingOrderDB STODB = new EFTN.component.StandingOrderDB();

        //    dtgStandingOrderBatch.DataSource = STODB.GetAllStandingOrderList();
        //    dtgStandingOrderBatch.DataBind();
        //}

        //protected void linkBtnActive_Click(object sender, EventArgs e)
        //{
        //    UpdateStandingOrderBatchStatus("ACTIVE");
        //}

        //protected void linkBtnInactive_Click(object sender, EventArgs e)
        //{

        //    UpdateStandingOrderBatchStatus("INACTIVE");
        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            dtStandingOrderBatchList = LoadStandingOrderBatch();
            dtgStandingOrderBatch.DataSource = dtStandingOrderBatchList;
            dtgStandingOrderBatch.DataBind();

        }

        private DataTable LoadStandingOrderBatch()
        {
            string BeginDate = ddlistYear.SelectedValue.PadLeft(4, '0')
                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                + ddlistDay.SelectedValue.PadLeft(2, '0');

            string EndDate = ddlistYearEnd.SelectedValue.PadLeft(4, '0')
                                + ddlistMonthEnd.SelectedValue.PadLeft(2, '0')
                                + ddlistDayEnd.SelectedValue.PadLeft(2, '0');

            StandingOrderDB stdOrderdb = new StandingOrderDB();
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            return stdOrderdb.GetAllStandingOrderListbyExecutionDateRange(BeginDate, EndDate, UserID, ddListActiveStatus.SelectedValue);
        }

        protected void dtgStandingOrderBatch_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgStandingOrderBatch.CurrentPageIndex = e.NewPageIndex;
            dtgStandingOrderBatch.DataSource = dtStandingOrderBatchList;
            dtgStandingOrderBatch.DataBind();
        }

        protected void btnCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = LoadStandingOrderBatch();

            if (dt.Rows.Count > 0)
            {
                string xlsFileName = "SchedulerReportExecutionDay" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                string attachment = "attachment; filename=" + xlsFileName + ".csv";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.csv";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                // Create the CSV file to which grid data will be exported. 
                //StreamWriter sw = new StreamWriter();
                int iColCount = dt.Columns.Count;

                // First we will write the headers. 

                for (int i = 0; i < iColCount; i++)
                {
                    sw.Write("\"");
                    sw.Write(dt.Columns[i]);
                    if (i < iColCount - 1)
                    {
                        sw.Write("\",");
                        //sw.Write(";");
                    }
                }

                if (iColCount > 0)
                {
                    sw.Write("\"");
                }
                sw.Write(sw.NewLine);

                // Now write all the rows. 
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < iColCount; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            sw.Write("\"");
                            sw.Write(dr[i].ToString());
                        }
                        if (i < iColCount - 1)
                        {
                            sw.Write("\",");
                        }
                    }
                    if (iColCount > 0)
                    {
                        sw.Write("\"");
                    }
                    sw.Write(sw.NewLine);
                }

                Response.Write(sw.ToString());
                Response.End();
            }
        }
    }
}
