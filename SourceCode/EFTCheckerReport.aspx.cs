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
using EFTN.component;
using EFTN.Utility;

namespace EFTN
{
    public partial class EFTCheckerReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string originBankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                if (originBankCode.Equals("135")
                    || originBankCode.Equals("185"))
                {
                    linkBtnEFTAdvice.Visible = true;
                    linkBtnEFTAdicePrintStatus.Visible = true;
                }
                else
                {
                    linkBtnEFTAdvice.Visible = false;
                    linkBtnEFTAdicePrintStatus.Visible = false;
                }

                if (originBankCode.Equals("185"))
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                    linkBtnBACHBranch.Visible = true;
                }
                else
                {
                    linkBtnBACHBranch.Visible = false;
                }
                  if (originBankCode.Equals("115"))
                {
                    linkBtnEFTChargeReport.Visible = true;
                }
                else
                {
                    linkBtnEFTChargeReport.Visible = false;
                }
                
                if (originBankCode.Equals("135"))
                {
                    linkBtnAddSummaryReport.Visible = true;
                }
                else
                {
                    linkBtnAddSummaryReport.Visible = false;
                }

                if (originBankCode.Equals(OriginalBankCode.SCB))
                {
                    linkBtnCBSMissMatchReport.Visible = true;
                    linkBtnCSVRejectionReport.Visible = true;
                    linkBtnRejectedDebitTransactionReport.Visible = true;
                    linkBtnCardsReport.Visible = true;
                    linkBtnSCBCardMapper.Visible = true;
                    linkBtnAccountModifiedLog.Visible = true;
                }
                else
                {
                    linkBtnCBSMissMatchReport.Visible = false;
                    linkBtnCSVRejectionReport.Visible = false;
                    linkBtnRejectedDebitTransactionReport.Visible = false;
                    linkBtnCardsReport.Visible = false;
                    linkBtnSCBCardMapper.Visible = false;
                    linkBtnAccountModifiedLog.Visible = false;
                }

                if (originBankCode.Equals(OriginalBankCode.SCB)
                    || originBankCode.Equals(OriginalBankCode.CBL))
                {
                    linkBtnDetailSettlementReportAck.Visible = true;
                }
                else
                {
                    linkBtnDetailSettlementReportAck.Visible = false;
                }

                if (originBankCode.Equals("245"))
                {
                    linkBtnBranchWiseTransactionStatus.Visible = true;
                    linkBtnEFTMonitor.Visible = true;
                    //linkBtnEFTChargeReport.Visible = true;
                }
                else
                {
                    linkBtnBranchWiseTransactionStatus.Visible = false;
                    linkBtnEFTMonitor.Visible = false;
                    //linkBtnEFTChargeReport.Visible = false;
                }
                if (originBankCode.Equals(OriginalBankCode.CBL))
                {
                    LinkBtnRFCReport.Visible = true;
                }
                else
                {
                    LinkBtnRFCReport.Visible = false;
                }
            }
        }

        protected void linkBtnEFTAdvice_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrintCustomerAdvice.aspx");
        }

        protected void linkBtnEFTAdicePrintStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdvicePrintStatus.aspx");
        }

        protected void linkBtnBACHBranch_Click(object sender, EventArgs e)
        {
            Response.Redirect("BACHBranchReport.aspx");
        }

        protected void linkBtnZoneWiseReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("ZoneWiseReport.aspx");
        }

        protected void linkBtnAddSummaryReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdditionalSummaryReport.aspx");
        }

        protected void linkBtnCBSMissMatchReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("CBSMisMatchReport.aspx");
        }

        protected void linkBtnCSVRejectionReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("CSVRejectionReport.aspx");
        }

        protected void linkBtnDetailSettlementReportAck_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetailSettlementReportAck.aspx");
        }

        protected void linkBtnEFTMonitor_Click(object sender, EventArgs e)
        {
            Response.Redirect("MonitorReport.aspx");
        }

        protected void linkBtnBranchWiseTransactionStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("BranchWiseTransactionStatus.aspx");
        }

        protected void linkBtnEFTChargeReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("EFTChargeReport.aspx");
        }

        protected void linkBtnDetailSettlementCharge_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetailSettlementRptForCharges.aspx");
        }

        protected void linkBtnRejectedDebitTransactionReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("RejectedDebitTransactionReport.aspx");
        }

        protected void linkBtnCardsReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetailSettlementReportForCards.aspx");
        }

        protected void linkBtnReturnFromArchiveDB_Click(object sender, EventArgs e)
        {
            ArchiveDatabaseManageDB ArchDB = new ArchiveDatabaseManageDB();
            //ArchDB.GetReturnMisMatchedFromArchive();
            ArchDB.SynchronizeReturnMisMatchedFromArchive();
        }

        protected void linkBtnSCBCardMapper_Click(object sender, EventArgs e)
        {
            Response.Redirect("SCBCardMapper.aspx");
        }

        protected void linkBtnAccountModifiedLog_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccountModifiedLog.aspx");
        }

        protected void LinkBtnRFCReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("RFCSuccessReport.aspx");
        }

        protected void linkBtnCustomerWiseReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerWiseReport.aspx");
        }
        
              protected void linkBtnSTDOLogChecker_Click(object sender, EventArgs e)
        {
            Response.Redirect("StandingOrderLogChecker.aspx");
        }

        protected void linkBtnSchedulerReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("SchedulerReportChecker.aspx");
        }
    }
}
