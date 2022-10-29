using System;
using System.Configuration;
using System.Web.UI.WebControls;
using EFTN.Utility;
using EFTN.BLL;
using FloraSoft;
using System.Data;

namespace EFTN
{
    public partial class EFTCheckerTransactionBatch : System.Web.UI.Page
    {
        private static DataTable MyDtSTS = new DataTable();
        DataView dvSTS;
        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";

                }
                else
                {
                    ViewState["sortOrder"] = "desc";
                }

                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataForTransactionSent();
                BindDataForTransactionSentSts();
                BindDataForTransactionSentStd();
                sortOrder = "asc";
            }
        }

        private void BindDataForTransactionSent()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.SentBatchDB batchDB = new EFTN.component.SentBatchDB();
            dtgBatchTransactionSent.DataSource = batchDB.GetBatchesForTransactionSentForChecker(DepartmentID);
            dtgBatchTransactionSent.DataBind();
        }

        private void BindBatchTotalSts(DataTable MyDt)
        {
            if (MyDt.Rows.Count > 0)
            {
                lblTotalItemSts.Text = "Total Item : " + MyDt.Compute("SUM(TotalTransactions)", "").ToString();
                lblTotalAmountSts.Text = "Total Amount : " + ParseData.StringToDouble(MyDt.Compute("SUM(TotalAmount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                lblTotalItemSts.Text = string.Empty;
                lblTotalAmountSts.Text = string.Empty;
            }
        }

        private void BindBatchTotalStd(DataTable MyDt)
        {
            if (MyDt.Rows.Count > 0)
            {
                lblTotalItemStd.Text = "Total Item : " + MyDt.Compute("SUM(TotalTransactions)", "").ToString();
                lblTotalAmountStd.Text = "Total Amount : " + ParseData.StringToDouble(MyDt.Compute("SUM(TotalAmount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                lblTotalItemStd.Text = string.Empty;
                lblTotalAmountStd.Text = string.Empty;
            }
        }
        private void BindDataForTransactionSentSts()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.SentBatchDB batchDB = new EFTN.component.SentBatchDB();
            //DataTable myDt = new DataTable();
            MyDtSTS = batchDB.GetBatchesForTransactionSentForCheckerSts(DepartmentID);
            dtgBatchTransactionSentSts.DataSource = MyDtSTS;
            dtgBatchTransactionSentSts.DataBind();
            BindBatchTotalSts(MyDtSTS);
        }

        private void BindDataForTransactionSentStd()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.SentBatchDB batchDB = new EFTN.component.SentBatchDB();
            DataTable myDt = new DataTable();
            myDt = batchDB.GetBatchesForTransactionSentForCheckerStdOrder(DepartmentID);
            dtgBatchTransactionSentStd.DataSource = myDt;
            dtgBatchTransactionSentStd.DataBind();
            BindBatchTotalStd(myDt);
        }

        protected void cbxAllTransactionSent_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllTransactionSent.Checked;
            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void cbxAllTransactionSentSts_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllTransactionSentSts.Checked;
            for (int i = 0; i < dtgBatchTransactionSentSts.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentSts.Items[i].FindControl("cbxSentBatchTSSts");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void cbxAllTransactionSentStd_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllTransactionSentStd.Checked;
            for (int i = 0; i < dtgBatchTransactionSentStd.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentStd.Items[i].FindControl("cbxSentBatchTSStdOrder");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            ChangeStatusOfCheckedEDR(EFTN.Utility.TransactionStatus.TransSent_Approved_By_Checker);
            txtRejectedReason.Text = "";
            lblNoReturnReason.Visible = false;
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (txtRejectedReason.Text != "")
            {
                lblNoReturnReason.Visible = false;
                EnterRejectReason();
                EnterRejectReasonForSts();
                EnterRejectReasonForStandingOrder();
                ChangeStatusOfCheckedEDR(EFTN.Utility.TransactionStatus.TransSent_Rejected_By_Checker);
                txtRejectedReason.Text = "";
            }
            else
            {
                lblNoReturnReason.Visible = true;
            }
        }

        private void EnterRejectReason()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSent.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(TransactionID, rejectReason,
                            (int)EFTN.Utility.ItemType.TransactionSent);
                }
            }
        }

        private void EnterRejectReasonForSts()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgBatchTransactionSentSts.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentSts.Items[i].FindControl("cbxSentBatchTSSts");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSentSts.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(TransactionID, rejectReason,
                            (int)EFTN.Utility.ItemType.TransactionSent);
                }
            }
        }

        private void EnterRejectReasonForStandingOrder()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgBatchTransactionSentStd.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentStd.Items[i].FindControl("cbxSentBatchTSStdOrder");

                if (cbx.Checked)
                {
                    string transactionID = dtgBatchTransactionSentStd.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(TransactionID, rejectReason,
                            (int)EFTN.Utility.ItemType.TransactionSent);
                }
            }
        }

        private void ChangeStatusOfCheckedEDR(int statusID)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            //EFTChargeManager eftChargeManager = new EFTChargeManager();
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);


            bool CBSActive = true;

            if (bankCode.Equals(OriginalBankCode.UCBL))
            {
                DepartmentsDB departmentDB = new DepartmentsDB();
                DataTable dtDept = departmentDB.EFT_GetDepartmentDetailsByDepartmentID(DepartmentID);

                if (bool.Parse(dtDept.Rows[0]["CBSActive"].ToString()))
                {
                    CBSActive = true;
                }
                else
                {
                    CBSActive = false;
                }
            }
            else if (bankCode.Equals(OriginalBankCode.NRB))
            {
                string CBSIntegrationActive = ConfigurationManager.AppSettings["CBSIntegrationActive"];

                if (CBSIntegrationActive.Equals("1"))
                {
                    CBSActive = true;
                }
                else
                {
                    CBSActive = false;
                }
            }

            UpdateStatusForAllTypeOfTransactions(statusID, ApprovedBy, DepartmentID, bankCode, CBSActive);
            UpdateStatusForStsTransactions(statusID, ApprovedBy, DepartmentID, bankCode, CBSActive);
            UpdateStatusForStandingOrderTransactions(statusID, ApprovedBy, DepartmentID, bankCode, CBSActive);

            BindDataForTransactionSent();
            BindDataForTransactionSentSts();
            BindDataForTransactionSentStd();
        }

        private void UpdateStatusForAllTypeOfTransactions(int statusID, int ApprovedBy, int DepartmentID, string bankCode, bool CBSActive)
        {
            int goodBatchCount = 0;
            int totalSelectedBatch = 0;
            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");

                if (cbx.Checked)
                {
                    totalSelectedBatch++;
                    string transactionID = dtgBatchTransactionSent.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                    if (bankCode.Equals(OriginalBankCode.SCB) && DepartmentID == 3)// for scb
                    {
                        goodBatchCount += db.UpdateEDRSentStatusForBatchApprovalForSCB(statusID, TransactionID, ApprovedBy);
                    }
                    //Commented Out FEB_2018 BACH II Upgrades
                    //else if ((bankCode.Equals(OriginalBankCode.NRB) || bankCode.Equals(OriginalBankCode.UCBL)) && CBSActive)
                    //{
                    //    FCUBSRTManager fcubsManager = new FCUBSRTManager();
                    //    goodBatchCount += fcubsManager.SendRTServiceEDRXMLForOutwardByBatchWise(TransactionID, bankCode, ApprovedBy);
                    //}
                    else
                    {
                        db.UpdateEDRSentStatusForBatchApproval(statusID, TransactionID, ApprovedBy);
                    }
                    //eftChargeManager.UdpateEFTNChargeBYTransactionID(TransactionID, connectionString, bankCode);
                }
            }
            //Commented Out FEB_2018 BACH II Upgrades
            //if (bankCode.Equals("215") || bankCode.Equals(OriginalBankCode.NRB) || (bankCode.Equals(OriginalBankCode.UCBL) && CBSActive))

            if (bankCode.Equals("215"))
            {
                //Commented out and added the followings FEB_2018 
                lblMsgExportTransaction.Text = "Total " + goodBatchCount + " batches updated successfully and " + (totalSelectedBatch - goodBatchCount) + " batches rejected.";
                if (goodBatchCount > 0)
                {
                    lblMsgExportTransaction.Text = "Total " + goodBatchCount + " batches updated successfully and " + (totalSelectedBatch - goodBatchCount) + " batches rejected.";
                }
                else
                {
                    lblMsgExportTransaction.Text = "Total " + totalSelectedBatch + " batches updated successfully!";
                }
            }
        }

        private void UpdateStatusForStsTransactions(int statusID, int ApprovedBy, int DepartmentID, string bankCode, bool CBSActive)
        {
            int goodBatchCount = 0;
            int totalSelectedBatch = 0;
            for (int i = 0; i < dtgBatchTransactionSentSts.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentSts.Items[i].FindControl("cbxSentBatchTSSts");

                if (cbx.Checked)
                {
                    totalSelectedBatch++;
                    string transactionID = dtgBatchTransactionSentSts.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                    if (bankCode.Equals(OriginalBankCode.SCB) && DepartmentID == 3)// for scb
                    {
                        goodBatchCount += db.UpdateEDRSentStatusForBatchApprovalForSCB(statusID, TransactionID, ApprovedBy);
                    }
                    //Commented Out FEB_2018 BACH II Upgrades
                    //else if ((bankCode.Equals(OriginalBankCode.NRB) || bankCode.Equals(OriginalBankCode.UCBL)) && CBSActive)
                    //{
                    //    FCUBSRTManager fcubsManager = new FCUBSRTManager();
                    //    goodBatchCount += fcubsManager.SendRTServiceEDRXMLForOutwardByBatchWise(TransactionID, bankCode, ApprovedBy);
                    //}
                    else
                    {
                        db.UpdateEDRSentStatusForBatchApproval(statusID, TransactionID, ApprovedBy);
                    }
                    //eftChargeManager.UdpateEFTNChargeBYTransactionID(TransactionID, connectionString, bankCode);
                }
            }
            //Commented Out FEB_2018 BACH II Upgrades
            //if (bankCode.Equals("215") || bankCode.Equals(OriginalBankCode.NRB) || (bankCode.Equals(OriginalBankCode.UCBL) && CBSActive))

            if (bankCode.Equals("215"))
            {
                // Commented out and added the followings FEB_2018
                if (goodBatchCount > 0)
                {
                    lblMsgExportTransactionSts.Text = "Total " + goodBatchCount + " batches updated successfully and " + (totalSelectedBatch - goodBatchCount) + " batches rejected.";
                }
                else
                {
                    lblMsgExportTransactionSts.Text = "Total " + totalSelectedBatch + " batches updated successfully!";
                }
            }
        }

        private void UpdateStatusForStandingOrderTransactions(int statusID, int ApprovedBy, int DepartmentID, string bankCode, bool CBSActive)
        {
            int goodBatchCount = 0;
            int totalSelectedBatch = 0;
            for (int i = 0; i < dtgBatchTransactionSentStd.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSentStd.Items[i].FindControl("cbxSentBatchTSStdOrder");

                if (cbx.Checked)
                {
                    totalSelectedBatch++;
                    string transactionID = dtgBatchTransactionSentStd.DataKeys[i].ToString();
                    Guid TransactionID = new Guid(transactionID);

                    EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
                    if (bankCode.Equals(OriginalBankCode.SCB) && DepartmentID == 3)// for scb
                    {
                        goodBatchCount += db.UpdateEDRSentStatusForBatchApprovalForSCB(statusID, TransactionID, ApprovedBy);
                    }
                    //Commented out FEB_2018 BACH II Upgrades
                    //else if ((bankCode.Equals(OriginalBankCode.NRB) || bankCode.Equals(OriginalBankCode.UCBL)) && CBSActive)
                    //{
                    //    FCUBSRTManager fcubsManager = new FCUBSRTManager();
                    //    goodBatchCount += fcubsManager.SendRTServiceEDRXMLForOutwardByBatchWise(TransactionID, bankCode, ApprovedBy);
                    //}
                    else
                    {
                        db.UpdateEDRSentStatusForBatchApproval(statusID, TransactionID, ApprovedBy);
                    }
                    //eftChargeManager.UdpateEFTNChargeBYTransactionID(TransactionID, connectionString, bankCode);
                }
            }
            //Commented Out BACH II Upgrades FEB_2018
            //if (bankCode.Equals("215") || bankCode.Equals(OriginalBankCode.NRB) || (bankCode.Equals(OriginalBankCode.UCBL) && CBSActive))
            if (bankCode.Equals("215"))
            {
                //Commented out and added the followings FEB_2018
                //lblMsgExportTransactionStd.Text = "Total " + goodBatchCount + " batches updated successfully and " + (totalSelectedBatch - goodBatchCount) + " batches rejected";
                if (goodBatchCount > 0)
                {
                    lblMsgExportTransactionStd.Text = "Total " + goodBatchCount + " batches updated successfully and " + (totalSelectedBatch - goodBatchCount) + " batches rejected.";
                }
                else
                {
                    lblMsgExportTransactionStd.Text = "Total " + goodBatchCount + " batches updated successfully!";
                }
            }
        }

        protected void dtgBatchTransactionSentSts_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dvSTS = MyDtSTS.DefaultView;
            dvSTS.Sort = e.SortExpression + " " + sortOrder;
            dtgBatchTransactionSentSts.DataSource = dvSTS;
            dtgBatchTransactionSentSts.DataBind();
        }

        protected void dtgBatchTransactionSentSts_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgBatchTransactionSentSts.CurrentPageIndex = e.NewPageIndex;
            dtgBatchTransactionSentSts.DataSource = MyDtSTS;
            dtgBatchTransactionSentSts.DataBind();
        }
    }
}
