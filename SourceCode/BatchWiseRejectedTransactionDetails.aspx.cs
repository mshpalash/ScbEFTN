using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using EFTN.component;
using EFTN.Utility;
using FloraSoft;
using EFTN.BLL;
using System.IO;
using System.Web.UI;

namespace EFTN
{
    public partial class BatchWiseRejectedTransactionDetails : System.Web.UI.Page
    {
        private static DataTable dtCSV = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataGrid();
            }
        }

        private void BindDataGrid()
        {
            string strEDRID = Request.Params["TransactionEDRID"].ToString();
            Guid EDRID = new Guid(strEDRID);
            TransactionSentDB db = new TransactionSentDB();

            //SqlDataReader sqlDRTransaction = db.GetSentEDRByTransactionIDForBatch(EDRID);
            //dtgTransactionDetails.DataSource = sqlDRTransaction;
            //dtgTransactionDetails.DataBind();
            dtCSV = db.GetRejectedSentEDRByTransactionID_forBatch(EDRID);

            dtgTransactionDetails.DataSource = dtCSV;
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
            dtgTransactionDetails.DataSource = dtCSV;
            dtgTransactionDetails.DataBind();
        }

        protected void ExportToExcel_Click(object sender, EventArgs e)
        {
            ExportDataToExcel();
        }

        private void ExportDataToExcel()
        {
            if (dtCSV.Rows.Count > 0)
            {
                string xlsFileName = "Inward" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                string attachment = "attachment; filename=" + xlsFileName + ".csv";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.csv";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                // Create the CSV file to which grid data will be exported. 
                //StreamWriter sw = new StreamWriter();
                int iColCount = dtCSV.Columns.Count;

                // First we will write the headers. 

                for (int i = 1; i < iColCount; i++)
                {
                    sw.Write("\"");
                    sw.Write(dtCSV.Columns[i]);
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
                foreach (DataRow dr in dtCSV.Rows)
                {
                    for (int i = 1; i < iColCount; i++)
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
