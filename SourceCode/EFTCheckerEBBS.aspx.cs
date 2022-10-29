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

namespace EFTN
{
    public partial class EFTCheckerEBBS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EFTN.component.ItemCountDB itemCountDB = new EFTN.component.ItemCountDB();

                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }
                
                System.Data.SqlClient.SqlDataReader sqlItemCount = itemCountDB.GetCountsForEBBSChecker(EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value), DepartmentID);
                while (sqlItemCount.Read())
                {
                    if (ParseData.StringToInt(sqlItemCount["OutwardTransactionSent"].ToString()) > 0)
                    {
                        lblCountOutwardTransSent.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountOutwardTransSent.Text = "(" + sqlItemCount["OutwardTransactionSent"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["OutwardReturnSent"].ToString()) > 0)
                    {
                        lblCountOutwardReturnSent.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountOutwardReturnSent.Text = "(" + sqlItemCount["OutwardReturnSent"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["OutwardNOCSent"].ToString()) > 0)
                    {
                        lblCountOutwardNOCSent.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountOutwardNOCSent.Text = "(" + sqlItemCount["OutwardNOCSent"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["InwardTransactionReceived"].ToString()) > 0)
                    {
                        lblCountInwardTransactionSent.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountInwardTransactionSent.Text = "(" + sqlItemCount["InwardTransactionReceived"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["BatchSent"].ToString()) > 0)
                    {
                        lblCountBatchSent.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountBatchSent.Text = "(" + sqlItemCount["BatchSent"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["InwardReturn"].ToString()) > 0)
                    {
                        lblCountInwardReturn.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountInwardReturn.Text = "(" + sqlItemCount["InwardReturn"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["DishonorSent"].ToString()) > 0)
                    {
                        lblCountDishonorSent.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountDishonorSent.Text = "(" + sqlItemCount["DishonorSent"].ToString() + ")";
                }

                sqlItemCount.Close();
                sqlItemCount.Dispose();
            }
        }       
    }
}
