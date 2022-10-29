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
using EFTN.BLL;
using FloraSoft;

namespace EFTN
{
    public partial class EFTCheckerTransactionSent : System.Web.UI.Page
    {
        private static DataTable MyDt = new DataTable();
        DataView dv;

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
                BindAccountNumberValidationButton();
                BindData();
                lblNoReturnReason.Visible = false;
                sortOrder = "asc";
            }
        }

        protected void dtgEFTChecker_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = MyDt.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgEFTChecker.DataSource = dv;
            dtgEFTChecker.DataBind();
        }

        private void BindData()
        {
            EFTN.component.SentEDRDB edrDB = new EFTN.component.SentEDRDB();
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            MyDt = edrDB.GetSentEDR_ForChecker(DepartmentID);
            dtgEFTChecker.DataSource = MyDt;

            dv = MyDt.DefaultView;
            dtgEFTChecker.CurrentPageIndex = 0;

            dtgEFTChecker.DataSource = dv;
            dtgEFTChecker.DataBind();

            BindBatchTotal();
        }

        private void BindBatchTotal()
        {
            if (MyDt.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + MyDt.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + ParseData.StringToDouble(MyDt.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            int failedTXNCounter = ChangeStatusOfCheckedEDR(EFTN.Utility.TransactionStatus.TransSent_Approved_By_Checker);

            if (failedTXNCounter > 0)
            {
                txtRejectedReason.Text = failedTXNCounter + " Transactions failed to send";
                lblNoReturnReason.Visible = true;
            }
            else
            {
                txtRejectedReason.Text = "";
                lblNoReturnReason.Visible = false;
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (txtRejectedReason.Text != "")
            {
                lblNoReturnReason.Visible = false;
                EnterRejectReason();
                ChangeStatusOfCheckedEDR(EFTN.Utility.TransactionStatus.TransSent_Rejected_By_Checker);
                txtRejectedReason.Text = "";
            }
            else
            {
                lblNoReturnReason.Visible = true;
            }
        }

        protected void btnAcceptAll_Click(object sender, EventArgs e)
        {
            int failedTXNCounter = ChangeStatusOfCheckedEDRForAll(EFTN.Utility.TransactionStatus.TransSent_Approved_By_Checker);
            if (failedTXNCounter > 0)
            {
                txtRejectedReason.Text = failedTXNCounter + " Transactions failed to send";
                lblNoReturnReason.Visible = true;
            }
            else
            {
                txtRejectedReason.Text = "";
                lblNoReturnReason.Visible = false;
            }
        }

        protected void btnRejectAll_Click(object sender, EventArgs e)
        {
            if (txtRejectedReason.Text != "")
            {
                lblNoReturnReason.Visible = false;
                EnterRejectReasonForAll();
                ChangeStatusOfCheckedEDRForAll(EFTN.Utility.TransactionStatus.TransSent_Rejected_By_Checker);
                txtRejectedReason.Text = "";
            }
            else
            {
                lblNoReturnReason.Visible = true;
            }
        }

        private int ChangeStatusOfCheckedEDRForAll(int statusID)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            //EFTChargeManager eftChargeManager = new EFTChargeManager();
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            bool CBSActive = true;
            int failedTXNCounter = 0;

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

            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                string edrId = dtgEFTChecker.DataKeys[i].ToString();
                Guid EDRID = new Guid(edrId);

                EFTN.component.SentEDRDB db = new EFTN.component.SentEDRDB();
                if ((bankCode.Equals(OriginalBankCode.NRB) || bankCode.Equals(OriginalBankCode.UCBL)) && CBSActive)
                {
                    try
                    {
                        if (statusID.Equals(EFTN.Utility.TransactionStatus.TransSent_Approved_By_Checker))
                        {
                            failedTXNCounter += UpdateTransactionStatusFor_FCUBS_RTService(EDRID, bankCode, ApprovedBy);
                        }
                        else
                        {
                            db.Update_EDRSent_Status(statusID, EDRID, ApprovedBy);
                        }
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
                else if (bankCode.Equals(OriginalBankCode.SCB) && DepartmentID == 3)// for scb
                {
                    db.EFT_Update_EDRSent_Status_ForSCBGenDebit(statusID, EDRID, ApprovedBy);
                }
                else
                {
                    db.Update_EDRSent_Status(statusID, EDRID, ApprovedBy);
                }
                //eftChargeManager.UpdateEFTNChargeByEDRID(EDRID, connectionString, bankCode);//charge is effected from here
            }
            BindData();
            return failedTXNCounter;
        }

        private void EnterRejectReason()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    string edrId = dtgEFTChecker.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                    db.Insert_RejectReason_ByChecker(EDRID, rejectReason,
                            (int)EFTN.Utility.ItemType.TransactionSent);
                }
            }

        }

        private void EnterRejectReasonForAll()
        {
            string rejectReason = txtRejectedReason.Text;
            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                string edrId = dtgEFTChecker.DataKeys[i].ToString();
                Guid EDRID = new Guid(edrId);

                EFTN.component.RejectReasonByCheckerDB db = new EFTN.component.RejectReasonByCheckerDB();
                db.Insert_RejectReason_ByChecker(EDRID, rejectReason,
                        (int)EFTN.Utility.ItemType.TransactionSent);
            }

        }

        private int ChangeStatusOfCheckedEDR(int statusID)
        {
            int ApprovedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            //EFTChargeManager eftChargeManager = new EFTChargeManager();
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            string bankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            bool CBSActive = true;
            int failedTXNCounter = 0;

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

            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    string edrId = dtgEFTChecker.DataKeys[i].ToString();
                    Guid EDRID = new Guid(edrId);

                    EFTN.component.SentEDRDB db = new EFTN.component.SentEDRDB();
                    if ((bankCode.Equals(OriginalBankCode.NRB) || bankCode.Equals(OriginalBankCode.UCBL)) && CBSActive)
                    {
                        try
                        {
                            if (statusID.Equals(EFTN.Utility.TransactionStatus.TransSent_Approved_By_Checker))
                            {
                                failedTXNCounter += UpdateTransactionStatusFor_FCUBS_RTService(EDRID, bankCode, ApprovedBy);
                            }
                            else
                            {
                                db.Update_EDRSent_Status(statusID, EDRID, ApprovedBy);
                            }
                        }
                        catch (Exception ex)
                        {
                            break;
                        }
                    }
                    else if (bankCode.Equals(OriginalBankCode.SCB) && DepartmentID == 3)// for scb
                    {
                        db.EFT_Update_EDRSent_Status_ForSCBGenDebit(statusID, EDRID, ApprovedBy);
                    }
                    else
                    {
                        db.Update_EDRSent_Status(statusID, EDRID, ApprovedBy);
                    }

                    //db.Update_EDRSent_Status(statusID, EDRID, ApprovedBy);
                    //eftChargeManager.UpdateEFTNChargeByEDRID(EDRID, connectionString, bankCode);//charge is effected from here
                }
            }
            BindData();
            return failedTXNCounter;
        }

        private int UpdateTransactionStatusFor_FCUBS_RTService(Guid EDRID, string bankCode, int ApprovedBy)
        {
            FCUBSRTManager nRBRTManager = new FCUBSRTManager();
            return nRBRTManager.SendRTServiceEDRXMLForOutward(EDRID, bankCode, ApprovedBy);
        }

        protected void dtgEFTChecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTChecker.CurrentPageIndex = e.NewPageIndex;
            dtgEFTChecker.DataSource = MyDt;
            dtgEFTChecker.DataBind();
        }

        protected void btnSynchronizeCBSAccountInfo_Click(object sender, EventArgs e)
        {
            int cbxCounter = 0;
            EFTN.component.SentEDRDB sentEDRDB = new component.SentEDRDB();

            FCUBSRTManager fcubsManager = new FCUBSRTManager();
            string connectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

            for (int i = 0; i < dtgEFTChecker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgEFTChecker.Items[i].FindControl("CheckBEFTNList");
                if (cbx.Checked)
                {
                    Guid EDRID = (Guid)dtgEFTChecker.DataKeys[i];
                    string AccountNo = sentEDRDB.GetSentEDRAccountNo_By_EDRID(EDRID, connectionString);
                    fcubsManager.SynchronizeAccountNameWithFCUBSForSentEDR(EDRID, AccountNo, connectionString);
                    //changedStatus = db.UpdateEDRReceivedStatus(edrId, statusID, returnCode, CreatedBy, correctedData);
                    cbxCounter++;
                }
            }
            if (cbxCounter < 1)
            {
                txtRejectedReason.Text = "Please select item";
                txtRejectedReason.ForeColor = System.Drawing.Color.Red;
                txtRejectedReason.Visible = true;
            }
            BindData();
        }

        private void BindAccountNumberValidationButton()
        {
            string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            if (originBank.Equals(OriginalBankCode.UCBL) || originBank.Equals(OriginalBankCode.NRB))
            {
                btnSynchronizeCBSAccountInfo.Visible = true;
            }
            else
            {
                btnSynchronizeCBSAccountInfo.Visible = false;
            }
        }

    }
}
