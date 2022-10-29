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
    public partial class EFTMaker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshEFTData();
            }
        }

        private void RefreshEFTData()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.ItemCountDB itemCountDB = new EFTN.component.ItemCountDB();
            System.Data.SqlClient.SqlDataReader sqlItemCount = itemCountDB.GetCountsForInmaker(EFTN.Utility.ParseData.StringToInt(Request.Cookies["BranchID"].Value), DepartmentID);
            while (sqlItemCount.Read())
            {
                if (ParseData.StringToInt(sqlItemCount["InwardTransaction"].ToString()) > 0)
                {
                    lblCountInwardTrans.ForeColor = System.Drawing.Color.Red;
                }
                lblCountInwardTrans.Text = "(" + sqlItemCount["InwardTransaction"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["InwardReturn"].ToString()) > 0)
                {
                    lblCountInwardReturn.ForeColor = System.Drawing.Color.Red;
                }
                lblCountInwardReturn.Text = "(" + sqlItemCount["InwardReturn"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["InwardNOC"].ToString()) > 0)
                {
                    lblCountInwardNOC.ForeColor = System.Drawing.Color.Red;
                }
                lblCountInwardNOC.Text = "(" + sqlItemCount["InwardNOC"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["InwardDishonor"].ToString()) > 0)
                {
                    lblCountInwardDishonor.ForeColor = System.Drawing.Color.Red;
                }
                lblCountInwardDishonor.Text = "(" + sqlItemCount["InwardDishonor"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["RejectedByChecker_TransactionSent"].ToString()) > 0)
                {
                    lblCountRejectTrans.ForeColor = System.Drawing.Color.Red;
                }
                lblCountRejectTrans.Text = "(" + sqlItemCount["RejectedByChecker_TransactionSent"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["RejectedByChecker_InwardTransaction"].ToString()) > 0)
                {
                    lblCountRejectInwardTrans.ForeColor = System.Drawing.Color.Red;
                }
                lblCountRejectInwardTrans.Text = "(" + sqlItemCount["RejectedByChecker_InwardTransaction"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["RejectedByChecker_InwardTransactionForIF"].ToString()) > 0)
                {
                    lblCountRejectInwardForIF.ForeColor = System.Drawing.Color.Red;
                }
                lblCountRejectInwardForIF.Text = "(" + sqlItemCount["RejectedByChecker_InwardTransactionForIF"].ToString() + ")";
                              

                if (ParseData.StringToInt(sqlItemCount["RejectedByChecker_ReturnSent"].ToString()) > 0)
                {
                    lblCountRejectReturn.ForeColor = System.Drawing.Color.Red;
                }
                lblCountRejectReturn.Text = "(" + sqlItemCount["RejectedByChecker_ReturnSent"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["RejectedByChecker_NOCSent"].ToString()) > 0)
                {
                    lblCountRejectNOC.ForeColor = System.Drawing.Color.Red;
                }
                lblCountRejectNOC.Text = "(" + sqlItemCount["RejectedByChecker_NOCSent"].ToString() + ")";


                if (ParseData.StringToInt(sqlItemCount["RejectedByChecker_DishonorSent"].ToString()) > 0)
                {
                    lblCountRejectedReturnReceivedDishonor.ForeColor = System.Drawing.Color.Red;
                }
                lblCountRejectedReturnReceivedDishonor.Text = "(" + sqlItemCount["RejectedByChecker_DishonorSent"].ToString() + ")";


                if (ParseData.StringToInt(sqlItemCount["RejectedByChecker_ReceivedReturnApproved"].ToString()) > 0)
                {
                    lblCountRejectedReturnApproved.ForeColor = System.Drawing.Color.Red;
                }
                lblCountRejectedReturnApproved.Text = "(" + sqlItemCount["RejectedByChecker_ReceivedReturnApproved"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["RejectedByChecker_BatchSent"].ToString()) > 0)
                {
                    lblCountBatchCount.ForeColor = System.Drawing.Color.Red;
                }
                lblCountBatchCount.Text = "(" + sqlItemCount["RejectedByChecker_BatchSent"].ToString() + ")";

                //if (ParseData.StringToInt(sqlItemCount["InvalidItemCount"].ToString()) > 0)
                //{
                //    lblCountInvalidTransactoin.ForeColor = System.Drawing.Color.Red;
                //}
                //lblCountInvalidTransactoin.Text = "(" + sqlItemCount["InvalidItemCount"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["IncompleteBatch"].ToString()) > 0)
                {
                    lblCountUploadedBulkData.ForeColor = System.Drawing.Color.Red;
                }
                lblCountUploadedBulkData.Text = "(" + sqlItemCount["IncompleteBatch"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["RejectedByChecker_ReceivedNOCApproved"].ToString()) > 0)
                {
                    lblCountRejectedReceivedNOC.ForeColor = System.Drawing.Color.Red;
                }
                lblCountRejectedReceivedNOC.Text = "(" + sqlItemCount["RejectedByChecker_ReceivedNOCApproved"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["RejectedByChecker_SentContested"].ToString()) > 0)
                {
                    lblCountRejectedContested.ForeColor = System.Drawing.Color.Red;
                }
                lblCountRejectedContested.Text = "(" + sqlItemCount["RejectedByChecker_SentContested"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["RejectedByChecker_ReceiveDishonor"].ToString()) > 0)
                {
                    lblCountRejectedReceivedDishonor.ForeColor = System.Drawing.Color.Red;
                }
                lblCountRejectedReceivedDishonor.Text = "(" + sqlItemCount["RejectedByChecker_ReceiveDishonor"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["RejectedByChecker_ReceivedRNOC"].ToString()) > 0)
                {
                    lblCountRejectedRNOCSent.ForeColor = System.Drawing.Color.Red;
                }
                lblCountRejectedRNOCSent.Text = "(" + sqlItemCount["RejectedByChecker_ReceivedRNOC"].ToString() + ")";

                if (ParseData.StringToInt(sqlItemCount["InvalidRoutingNumber"].ToString()) > 0)
                {
                    lblCountInvalidRoutingForOutward.ForeColor = System.Drawing.Color.Red;
                }
                lblCountInvalidRoutingForOutward.Text = "(" + sqlItemCount["InvalidRoutingNumber"].ToString() + ")";
            }

            sqlItemCount.Close();
            sqlItemCount.Dispose();

            string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            if (originBank.Equals(OriginalBankCode.SCB))
            {
                divStandingOrder.Visible = true;
                //divRegenerateForAtlasError.Visible = true;
                //ImportDDITransaction();
            }
            else
            {
                divDDI.Visible = false;
                divRegenerateForAtlasError.Visible = false;
                //linkBtnImportTransaction.Visible = false;
                //lblMsgTransactionReceived.Visible = false;
            }

            if (originBank.Equals(OriginalBankCode.UCBL))
            {
                divAccountEnquiry.Visible = true;
            }
            else
            {
                divAccountEnquiry.Visible = false;
            }

            //if (originBank.Equals(OriginalBankCode.UCBL) || originBank.Equals(OriginalBankCode.CBL))
            //{
            //    divBulkReturn.Visible = true;
            //}
            //else
            //{
            //    divBulkReturn.Visible = false;
            //}

            if (originBank.Equals("225"))
            {
                divStandingOrder.Visible = true;
                divInwardReturnRFC.Visible = true;
                divCityCards.Visible = true;

            }
            else if (originBank.Equals("290"))
            {
                divStandingOrder.Visible = true;
                divInwardReturnRFC.Visible = false;
                divCityCards.Visible = false;
            }
            else
            {
                //divStandingOrder.Visible = false;
                divInwardReturnRFC.Visible = false;
                divCityCards.Visible = false;
            }
        }

        protected void linkBtnStandingOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("StandingOrderManagement.aspx");
        }

        //private void ImportDDITransaction()
        //{
        //    EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
        //    dtlImportTransaction.DataSource = ds.GetDataSourceAllType("EFTNDDI"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
        //    dtlImportTransaction.DataBind();
        //    if (dtlImportTransaction.Items.Count == 0)
        //    {
        //        lblMsgTransactionReceived.Text = "No files.";
        //    }
        //}

        protected void linkBtnImportTransaction_Click(object sender, EventArgs e)
        {
            Response.Redirect("DDITransactionManagement.aspx");

            //int userid = parsedata.stringtoint(request.cookies["userid"].value);
            //int departmentid = parsedata.stringtoint(request.cookies["departmentid"].value);

            //ddimanager ddimanager = new ddimanager();
            //ddimanager.importddi(userid, departmentid);
            //refresheftdata();
        }

       

        protected void linkBtnInwardReturnRFC_Click(object sender, EventArgs e)
        {
            Response.Redirect("InwardReturnMakerRFC.aspx");
        }

        protected void linkBtnAccountEnquiry_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccountEnquiry.aspx");
        }

        protected void linkBtnCityCards_Click(object sender, EventArgs e)
        {
            Response.Redirect("CityCardsManagement.aspx");
        }

        protected void linkBtnRegenForAtlasError_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegenerateTransactionByTraceNumber.aspx");
        }

        protected void linkBtnIBankingMaker_Click(object sender, EventArgs e)
        {
            Response.Redirect("iBankingOperatorMaker.aspx");
        }
        protected void linkBtnIBankingRejectedTranRptMaker_Click(object sender, EventArgs e)
        {
            Response.Redirect("iBankingRejectedTransactionReportMaker.aspx");
        }
        protected void linkBtnIBankingCBSRptMaker_Click(object sender, EventArgs e)
        {
            Response.Redirect("iBankingCBSReportMaker.aspx");
        }

        protected void linkBtnEAdviceReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("EAdviceMaker.aspx");
        }
    }
}
