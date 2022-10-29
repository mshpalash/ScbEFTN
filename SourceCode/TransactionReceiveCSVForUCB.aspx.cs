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
using EFTN.Utility;
using System.IO;

namespace EFTN
{
    public partial class TransactionReceiveCSVForUCB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');
                if (!ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals(OriginalBankCode.UCBL))
                {
                    Response.Redirect("EFTMaker.aspx");
                }
            }

        }


        private DataTable GetData()
        {
            EFTN.component.TransactionReceiveForUCBCSVDB transactionReceivedUCBDB = new EFTN.component.TransactionReceiveForUCBCSVDB();
            DataTable dt;

            int UserID = ParseData.StringToInt(Request.Cookies["UserID"].Value);

            if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            {
                dt = transactionReceivedUCBDB.GetEFT_UCB_Debit_TransactionReceivedBySettlementDate(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'), UserID);
            }
            else
            {
                dt = transactionReceivedUCBDB.GetEFT_UCB_Credit_TransactionReceivedBySettlementDate(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                             + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                             + ddlistDay.SelectedValue.PadLeft(2, '0'), UserID);
            }
            return dt;
        }

        protected void linkBtnGenerateCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = GetData();

            if (dt.Rows.Count > 0)
            {
                string xlsFileName = "TransactionReceived" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
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
