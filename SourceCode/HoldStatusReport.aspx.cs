using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI.WebControls;
using EFTN.component;
using EFTN.Utility;

namespace EFTN
{
    public partial class HoldStatusReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ReportDB reportDB = new ReportDB();
            DataTable dt = GetData();
            if (dt.Rows.Count > 0)
            {
                BindStatusReport(dt);
            }
        }

        private void BindStatusReport(DataTable dt)
        {
            dtgStatusReport.DataSource = dt;
            dtgStatusReport.DataBind();
        }

        private DataTable GetData()
        {
            ReportDB reportDB = new ReportDB();
            return reportDB.GetMonitorReportDepartmentwiseforHOLD(ParseData.StringToInt(ddlistDay.SelectedValue),
                                                   ParseData.StringToInt(ddlistMonth.SelectedValue),
                                                   ParseData.StringToInt(ddlistYear.SelectedValue));
        }

        protected void btnExpotToCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = GetData();

            if (dt.Rows.Count > 0)
            {
                string xlsFileName = "HoldStatusReport" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
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
