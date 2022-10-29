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
    public partial class EFTChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                if (originBank.Equals(OriginalBankCode.NRB) || originBank.Equals(OriginalBankCode.UCBL))
                {
                    divFCUBSTrMgt.Visible = true;
                }
                else
                {
                    divFCUBSTrMgt.Visible = false;
                }

                if (originBank.Equals(OriginalBankCode.CBL) || originBank.Equals(OriginalBankCode.SCB))
                {
                    divStdOBatch.Visible = true;
                }
                else
                {
                    divStdOBatch.Visible = false;
                }

                EFTN.component.ItemCountDB itemCountDB = new EFTN.component.ItemCountDB();
            
                int DepartmentID = 0;
                if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
                {
                    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
                }

                System.Data.SqlClient.SqlDataReader sqlItemCount = itemCountDB.GetCountsForInChecker(EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value), DepartmentID);

                while (sqlItemCount.Read())
                {
                    if (ParseData.StringToInt(sqlItemCount["TransactionSent"].ToString()) > 0)
                    {
                        lblCountOutwardTransSent.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountOutwardTransSent.Text = "(" + sqlItemCount["TransactionSent"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["BatchSent"].ToString()) > 0)
                    {
                        lblCountOutwardBatchSent.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountOutwardBatchSent.Text = "(" + sqlItemCount["BatchSent"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["ReturnSent"].ToString()) > 0)
                    {
                        lblCountOutwardReturnSent.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountOutwardReturnSent.Text = "(" + sqlItemCount["ReturnSent"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["NOCSent"].ToString()) > 0)
                    {
                        lblCountOutwardNOCSent.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountOutwardNOCSent.Text = "(" + sqlItemCount["NOCSent"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["DishonorSent"].ToString()) > 0)
                    {
                        lblCountOutwardDishonorSent.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountOutwardDishonorSent.Text = "(" + sqlItemCount["DishonorSent"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["RNOCSent"].ToString()) > 0)
                    {
                        lblCountOutwardRNOCSent.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountOutwardRNOCSent.Text = "(" + sqlItemCount["RNOCSent"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["ContestedSent"].ToString()) > 0)
                    {
                        lblCountOutwardContested.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountOutwardContested.Text = "(" + sqlItemCount["ContestedSent"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["InwardTransactionApproved"].ToString()) > 0)
                    {
                        lblCountInwardTransactionApproved.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountInwardTransactionApproved.Text = "(" + sqlItemCount["InwardTransactionApproved"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["InwardReturnApproved"].ToString()) > 0)
                    {
                        lblCountInwardReturnApproved.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountInwardReturnApproved.Text = "(" + sqlItemCount["InwardReturnApproved"].ToString() + ")";

                    if (ParseData.StringToInt(sqlItemCount["DishonorReceived"].ToString()) > 0)
                    {
                        lblCountApprovedDishonor.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountApprovedDishonor.Text = "(" + sqlItemCount["DishonorReceived"].ToString() + ")";


                    if (ParseData.StringToInt(sqlItemCount["ReceivedNOCApproved"].ToString()) > 0)
                    {
                        lblCountApprovedReceivedNOC.ForeColor = System.Drawing.Color.Red;
                    }
                    lblCountApprovedReceivedNOC.Text = "(" + sqlItemCount["ReceivedNOCApproved"].ToString() + ")";
                }

                sqlItemCount.Close();
                sqlItemCount.Dispose();
            }
        }

        protected void linkBtnIBankingMaker_Click(object sender, EventArgs e)
        {
            Response.Redirect("iBankingOperatorChecker.aspx");
        }
        protected void linkBtnIBankingRejectedTranRptMaker_Click(object sender, EventArgs e)
        {
            Response.Redirect("iBankingRejectedTransactionReportChecker.aspx");
        }
        protected void linkBtnIBankingCBSRptMaker_Click(object sender, EventArgs e)
        {
            Response.Redirect("iBankingCBSReportChecker.aspx");
        }
        protected void linkBtnBulkReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("BulkReturnUpload.aspx");
        }
    }
}
