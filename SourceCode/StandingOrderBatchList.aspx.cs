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
    public partial class StandingOrderBatchList : System.Web.UI.Page
    {
        private static DataTable dtStandingOrderBatchList = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataForTransactionSent();
            }
        }

        private void BindDataForTransactionSent()
        {
            //int DepartmentID = 0;
            //if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            //{
            //    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            //}
            EFTN.component.StandingOrderDB STODB = new EFTN.component.StandingOrderDB();
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            dtStandingOrderBatchList = STODB.GetAllActiveStandingOrderList(UserID);
            dtgStandingOrderBatch.DataSource = dtStandingOrderBatchList;
            dtgStandingOrderBatch.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EFTN.component.StandingOrderDB STODB = new EFTN.component.StandingOrderDB();
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            dtStandingOrderBatchList = STODB.GetAllActiveStandingOrderListSearch(UserID, txtSearchParam.Text.Trim());
            if (dtStandingOrderBatchList.Rows.Count > 0)
            {
                dtgStandingOrderBatch.CurrentPageIndex = 0;
            }
            dtgStandingOrderBatch.DataSource = dtStandingOrderBatchList;
            dtgStandingOrderBatch.DataBind();
        }


        protected void dtgStandingOrderBatch_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgStandingOrderBatch.CurrentPageIndex = e.NewPageIndex;
            dtgStandingOrderBatch.DataSource = dtStandingOrderBatchList;
            dtgStandingOrderBatch.DataBind();
        }

        //protected void btnExpotToCSV_Click(object sender, EventArgs e)
        //{
        //    EFTN.component.StandingOrderDB STODB = new EFTN.component.StandingOrderDB();
        //    int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
        //    dtStandingOrderBatchList = STODB.GetAllActiveStandingOrderList(UserID);
        //    DataTable myDt = dtStandingOrderBatchList;

        //    if (myDt.Rows.Count > 0)
        //    {
        //        string xlsFileName = "StandingOrderBatchListReport" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
        //        string attachment = "attachment; filename=" + xlsFileName + ".csv";

        //        Response.ClearContent();
        //        Response.AddHeader("content-disposition", attachment);
        //        Response.ContentType = "application/vnd.csv";

        //        StringWriter sw = new StringWriter();
        //        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //        // Create the CSV file to which grid data will be exported. 
        //        //StreamWriter sw = new StreamWriter();
        //        int iColCount = myDt.Columns.Count;

        //        // First we will write the headers. 

        //        for (int i = 0; i < iColCount; i++)
        //        {
        //            sw.Write("\"");
        //            sw.Write(myDt.Columns[i]);
        //            if (i < iColCount - 1)
        //            {
        //                sw.Write("\",");
        //                //sw.Write(";");
        //            }
        //        }

        //        if (iColCount > 0)
        //        {
        //            sw.Write("\"");
        //        }
        //        sw.Write(sw.NewLine);

        //        // Now write all the rows. 
        //        foreach (DataRow dr in myDt.Rows)
        //        {
        //            for (int i = 0; i < iColCount; i++)
        //            {
        //                if (!Convert.IsDBNull(dr[i]))
        //                {
        //                    sw.Write("\"");
        //                    sw.Write(dr[i].ToString());
        //                }
        //                if (i < iColCount - 1)
        //                {
        //                    sw.Write("\",");
        //                }
        //            }
        //            if (iColCount > 0)
        //            {
        //                sw.Write("\"");
        //            }
        //            sw.Write(sw.NewLine);
        //        }

        //        Response.Write(sw.ToString());
        //        Response.End();
        //    }
        //}

        protected void btnExpotSearchToCSV_Click(object sender, EventArgs e)
        {
            EFTN.component.StandingOrderDB STODB = new EFTN.component.StandingOrderDB();
            int UserID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            dtStandingOrderBatchList = STODB.GetAllActiveStandingOrderListSearch(UserID, txtSearchParam.Text.Trim());
            DataTable myDt = dtStandingOrderBatchList;

            if (myDt.Rows.Count > 0)
            {
                string xlsFileName = "StandingOrderBatchListReport" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                string attachment = "attachment; filename=" + xlsFileName + ".csv";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.csv";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                // Create the CSV file to which grid data will be exported. 
                //StreamWriter sw = new StreamWriter();
                int iColCount = myDt.Columns.Count;

                // First we will write the headers. 

                for (int i = 0; i < iColCount; i++)
                {
                    sw.Write("\"");
                    sw.Write(myDt.Columns[i]);
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
                foreach (DataRow dr in myDt.Rows)
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
