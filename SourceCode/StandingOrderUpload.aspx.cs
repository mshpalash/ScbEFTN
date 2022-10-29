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
using EFTN.BLL;
using EFTN.component;

namespace EFTN
{
    public partial class StandingOrderUpload : System.Web.UI.Page
    {
        DataView dv;
        private static DataTable myDTCorUpload = new DataTable();
        private static Guid StandingOrderBatchID;
        private static string strDataEntryType = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                sortOrder = "asc";
                rdoButtonBatch.Visible = false;
            }
        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            if (rdoBtnTransactionType.SelectedValue.Equals(TransactionCodes.EFTTransactionTypeCredit))
            {
                Session["EFTTransactionType"] = TransactionCodes.EFTTransactionTypeCredit;
            }
            else if (rdoBtnTransactionType.SelectedValue.Equals(TransactionCodes.EFTTransactionTypeDebit))
            {
                Session["EFTTransactionType"] = TransactionCodes.EFTTransactionTypeDebit;
            }
            else
            {
                lblErrMsg.Text = "Select Transaction Type";
                return;
            }

            if (txtCompanyName.Text.Trim().Equals(string.Empty)
                || txtCompanyTIN.Text.Trim().Equals(string.Empty)
                || txtReasonForPayment.Text.Trim().Equals(string.Empty)
                )
            {
                lblErrMsg.Text = "Failed to save. Fill the mandatory fields";
                return;
            }

            if (CalendarFreqStart.SelectedDate.Ticks == 0)
            {
                lblErrMsg.Text = "Failed to save. Select Start Date";
                return;
            }

            if (CalendarFreqEnd.SelectedDate.Ticks == 0)
            {
                lblErrMsg.Text = "Failed to save. Select End Date";
                return;
            }

            if (fulExcelFile.HasFile)
            {
                string fileName = string.Empty;
                string filePath = string.Empty;
                int deptID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

                if (rdoButtonFileType.SelectedValue.Equals("normal"))
                {
                    string extension = Path.GetExtension(fulExcelFile.PostedFile.FileName);

                        if (extension.ToLower().Equals(".xls") || extension.ToLower().Equals(".xlsx"))
                        {
                            fileName = fulExcelFile.PostedFile.FileName;
                        }
                        else
                        {
                            FailedMessage();
                            return;
                        }


                        string fileExtension = Path.GetExtension(fileName);

                        string savePath = ConfigurationManager.AppSettings["EFTExcelFiles"] + Guid.NewGuid().ToString() + fileExtension;

                        try
                        {
                            fulExcelFile.SaveAs(savePath);


                            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

                            if (!IsValidExcelData(savePath, SelectedBank))
                            {
                                System.IO.File.Delete(savePath);
                                FailedMessage();
                                return;
                            }

                            int createdBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
                            EFTN.BLL.ExcelForStandingOrder excelObj = new EFTN.BLL.ExcelForStandingOrder(
                                                    txtCompanyTIN.Text,
                                                    Int32.Parse(ddlPaymentType.SelectedValue),
                                                    txtCompanyName.Text,
                                                    txtReasonForPayment.Text,
                                                    savePath,
                                                    createdBy,
                                                    ParseData.StringToInt(rdoButtonBatch.SelectedValue)
                                                    , rdoBtnTransactionType.SelectedValue, deptID
                                                    , DataEntryType.Excel
                                                    , CalendarFreqStart.SelectedDate
                                                    , CalendarFreqEnd.SelectedDate
                                                    , CalendarFreqStart.SelectedDate.Day
                                                    , CalendarFreqEnd.SelectedDate.Day
                                                    , ParseData.StringToInt(ddListTransactionFrequency.SelectedValue)
                                                    );
                            myDTCorUpload = excelObj.EntryData(SelectedBank);
                            if (myDTCorUpload.Rows.Count > 0)
                            {
                                strDataEntryType = DataEntryType.Excel;
                                StandingOrderBatchID = excelObj.StandingOrderBatchID;
                                BindImportedExcel();
                                SuccessMessage();
                            }
                            else
                            {
                                FailedMessage();
                            }

                        }
                        catch
                        {
                            FailedMessage();
                        }
                }
                else
                {
                    string extension = Path.GetExtension(fulExcelFile.PostedFile.FileName);
                    if (extension.ToLower().Equals(".exl"))
                    {
                        fileName = fulExcelFile.PostedFile.FileName;
                        rdoButtonBatch.SelectedValue = "1";
                    }
                    else
                    {
                        FailedMessage();
                        return;
                    }
                    string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

                    string savePath = ConfigurationManager.AppSettings["EFTExcelFiles"] + Guid.NewGuid().ToString() + extension;
                    try
                    {
                        fulExcelFile.SaveAs(savePath);
                        if (UnZipThis(savePath))
                        {
                            filePath = savePath.Substring(0, savePath.Length - 4);
                            string[] fileNameList = Directory.GetFiles(filePath);
                            fileName = fileNameList[0];
                        }

                        else
                        {
                            FailedMessageForDuplicate();
                            return;
                        }

                        int createdBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

                        string fileExtension = Path.GetExtension(fileName);
                            EFTN.BLL.ExcelForStandingOrder excelObj = new EFTN.BLL.ExcelForStandingOrder(
                                                    txtCompanyTIN.Text,
                                                    Int32.Parse(ddlPaymentType.SelectedValue),
                                                    txtCompanyName.Text,
                                                    txtReasonForPayment.Text,
                                                    fileName,
                                                    createdBy, 
                                                    ParseData.StringToInt(rdoButtonBatch.SelectedValue)
                                                    , rdoBtnTransactionType.SelectedValue
                                                    , deptID
                                                    , DataEntryType.EncryptedExcel
                                                    , CalendarFreqStart.SelectedDate
                                                    , CalendarFreqEnd.SelectedDate
                                                    , CalendarFreqStart.SelectedDate.Day
                                                    , CalendarFreqEnd.SelectedDate.Day
                                                    , ParseData.StringToInt(ddListTransactionFrequency.SelectedValue)
                                                    );
                            myDTCorUpload = excelObj.EntryData(SelectedBank);



                            removeDecryptedFile(filePath);
                            removeFile(savePath);

                            if (myDTCorUpload.Rows.Count > 0)
                            {
                                strDataEntryType = DataEntryType.EncryptedExcel;
                                StandingOrderBatchID = excelObj.StandingOrderBatchID;
                                BindImportedExcel();
                                SuccessMessage();
                            }
                            else
                            {
                                FailedMessage();
                            }
                    }
                    catch
                    {
                        FailedMessage();
                    }
                }
            }
        }

        private bool IsValidExcelData(string sourcefile, string SelectedBank)
        {
            EFTN.BLL.ValidateExelField vEF = new EFTN.BLL.ValidateExelField();
            string errorExcel = string.Empty;
            errorExcel = vEF.ValidateExcelForEFT(sourcefile, SelectedBank, ParseData.StringToInt(ddlPaymentType.Text)).Trim();
            lblUploadErrMsg.Text = errorExcel;
            if (errorExcel.Equals(string.Empty))
            {
                return true;
            }
            return false;
        }
            
        private bool IsValidCSVDataForEFT(string sourcefile, string SelectedBank)
        {
            EFTN.BLL.ValidateCSVFieldForEFT vCsvEFT = new EFTN.BLL.ValidateCSVFieldForEFT();
            string errorExcel = string.Empty;
            errorExcel = vCsvEFT.ValidateCSVField(sourcefile, ParseData.StringToInt(ddlPaymentType.Text), SelectedBank).Trim();
            lblUploadErrMsg.Text = errorExcel;
            if (errorExcel.Equals(string.Empty))
            {
                return true;
            }
            return false;
        }

        private void removeDecryptedFile(string filePath)
        {
            if (!filePath.Equals(string.Empty))
            {
                if (System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.Delete(filePath, true);
                }
            }
        }

        private void removeFile(string filePath)
        {
            if (!filePath.Equals(string.Empty))
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }

        private bool UnZipThis(string sourceFile)
        {
            string destpath = sourceFile.Substring(0, sourceFile.Length - 4);
            using (ZipFile zip = ZipFile.Read(sourceFile))
            {
                try
                {
                    zip.Password = "e#hTgz//23:0@5wPE+6H/TT.0+Am=Y:^I0S68#Ih!F~#uAxPQ^AJ^:9i4B!TVP4y";
                    zip.ExtractAll(destpath);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

        private void FailedMessageForFileModification()
        {
            lblMsgBatchNumber.Text = string.Empty;
            lblErrMsg.Text = "Failed to upload CSV file. File has been modified.";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }

        private void FailedMessage()
        {
            lblMsgBatchNumber.Text = string.Empty;
            lblErrMsg.Text = "Failed to upload invalid file. Please upload valid excel file. Scroll down to see the error detail.";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }

        private void FailedMessageForTextFileUpload()
        {
            lblMsgBatchNumber.Text = string.Empty;
            lblErrMsg.Text = "Failed to upload invalid file. Please upload valid text file";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }

        private void FailedMessageForCSVFileUpload()
        {
            lblMsgBatchNumber.Text = string.Empty;
            lblErrMsg.Text = "Invalid or duplicate record. Failed to upload";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }

        private void FailedMessageForGeneralCSVFileUpload()
        {
            lblMsgBatchNumber.Text = string.Empty;
            lblErrMsg.Text = "Invalid Format. Failed to upload";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }

        private void FailedMessageForDuplicate()
        {
            lblErrMsg.Text = "Failed to decrypt duplicate file in the same location";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }

        private void SuccessMessage()
        {
            lblErrMsg.Text = "uploaded file successfully";
            lblErrMsg.ForeColor = System.Drawing.Color.Blue;
            lblErrMsg.Visible = true;
        }

        private void BindImportedExcel()
        {
            BindBatchTotal();
            dtgXcelUpload.CurrentPageIndex = 0;

            dv = myDTCorUpload.DefaultView;
            dtgXcelUpload.DataSource = dv;
            dtgXcelUpload.DataBind();

            BindStandingOrderDate();
        }

        private void BindStandingOrderDate()
        {
            StandingOrderDB stdDb = new StandingOrderDB();
            dtgStandingOrderDate.DataSource = stdDb.GetStandingOrderDateByStandingOrderBatchID(StandingOrderBatchID);
            dtgStandingOrderDate.DataBind();
        }

        private void BindBatchTotal()
        {
            if (myDTCorUpload.Rows.Count > 0)
            {
                lblTotalItem.Text = "Total Item : " + myDTCorUpload.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + myDTCorUpload.Compute("SUM(Amount)", "").ToString();
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
        }

        protected void dtgXcelUpload_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = myDTCorUpload.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgXcelUpload.DataSource = dv;
            dtgXcelUpload.DataBind();
        }

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

        protected void dtgXcelUpload_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgXcelUpload.CurrentPageIndex = e.NewPageIndex;
            dtgXcelUpload.DataSource = myDTCorUpload;
            dtgXcelUpload.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (myDTCorUpload.Rows.Count > 0)
            {
                StandingOrderDB stdDb = new StandingOrderDB();
                stdDb.DeleteStandingOrderBatch(StandingOrderBatchID);
                myDTCorUpload = stdDb.GetStandingOrderByStandingOrderBatchID(StandingOrderBatchID);
                BindImportedExcel();
                DeleteAllMessages();
            
                lblErrMsg.Text = "Deleted the batch";
                lblErrMsg.ForeColor = System.Drawing.Color.Blue;
                lblErrMsg.Visible = true;
            }
            //INCOMPLETE
            //if (myDTCorUpload.Rows.Count > 0)
            //{
            //    EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
            //    sentBatchDB.DeleteBatchSent(StandingOrderBatchID);
            //    BindData();
            //    DeleteAllMessages();
            //}
        }

        private void DeleteAllMessages()
        {
            lblMsgBatchNumber.Text = string.Empty;
            lblTotalItem.Text = string.Empty;
            lblTotalAmount.Text = string.Empty;
        }
    }
}