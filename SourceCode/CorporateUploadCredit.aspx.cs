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
using System.Net;

namespace EFTN
{
    public partial class CorporateUploadCredit : System.Web.UI.Page
    {
        DataView dv;
        private static DataTable myDTCorUpload = new DataTable();
        private static Guid TransactionID;
        private static string BatchNumber = string.Empty;
        private static string strDataEntryType = string.Empty;
        private SentBatchDB sentBatchDB = new SentBatchDB();
        private string eftConString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        public static string errorStringForExcelUpload = string.Empty;
        public static DataTable excelDataToUpload = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TransactionID = new Guid();
                sortOrder = "asc";
                CBSSettlementDayDisplay();
                BindChargeCategory();
                BindCBSAccountHitDisplay();
                /** Commented out for uploading multiple currency source file upload    **/
                //Session.Remove("Currency");
                //BindCurrencyTypeDropdownlist();
                //ClearFileUpload();
            }
        }

        /** Commented out for uploading multiple currency source file upload    **/
        //protected void BindCurrencyTypeDropdownlist()
        //{
        //    DataTable dropDownData = new DataTable();
        //    dropDownData = sentBatchDB.GetCurrencyList(eftConString);
        //    CurrencyDdList.DataSource = dropDownData;
        //    CurrencyDdList.DataTextField = "Currency";
        //    CurrencyDdList.DataValueField = "Currency";
        //    CurrencyDdList.DataBind();
        //    CurrencyDdList.SelectedIndex = 0;
        //}
        private void BindCBSAccountHitDisplay()
        {
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            if (SelectedBank.Equals(OriginalBankCode.NRB))
            {
                pnlCBSAccountHit.Visible = true;
            }
            else
            {
                pnlCBSAccountHit.Visible = false;
            }
        }

        private void CBSSettlementDayDisplay()
        {
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            if (SelectedBank.Equals("215"))
            {
                pnlCBSSettlementDay.Visible = true;
            }
            else
            {
                pnlCBSSettlementDay.Visible = false;
            }
        }

        private void BindChargeCategory()
        {
            //string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            //if (SelectedBank.Equals("225"))
            //{
            //    EFTChargeManager eftChargeManager = new EFTChargeManager();

            //    ddListChargeCategoryList.DataSource = eftChargeManager.GetCityChargeDefineListForBulk();
            //    ddListChargeCategoryList.DataTextField = "CityChargeDefineDes";
            //    ddListChargeCategoryList.DataValueField = "CityChargeDefineID";
            //    ddListChargeCategoryList.DataBind();
            //    pnlChargeCategory.Visible = true;
            //    pnlChargeCode.Visible = true;
            //}
            //else
            //{
            pnlChargeCategory.Visible = false;
            pnlChargeCode.Visible = false;
            //}
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

            /** Commented out for uploading multiple currency source file upload    **/
            //Currency For All Scope in this page
            //currency = CurrencyDdList.SelectedValue;
            //Session["Currency"] = currency;

            /*  REMITTANCE PART */
            int isRemittance = 0;
            if (cbxRemittance.Checked)
            {
                isRemittance = 1;
            }

            if (fulExcelFile.HasFile)
            {
                string fileName = string.Empty;
                string filePath = string.Empty;
                int deptID = ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

                string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

                if (rdoButtonFileType.SelectedValue.Equals("normal"))
                {
                    string extension = Path.GetExtension(fulExcelFile.PostedFile.FileName);
                    if (extension.ToLower().Equals(".csv") && ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals(OriginalBankCode.SCB))
                    {

                        fileName = fulExcelFile.PostedFile.FileName;

                        #region OLD_Code_Snippet
                        //string fileCreationTime = File.GetCreationTime(fileName).ToString("yyyyMMddHHmmss");
                        //string fileModificationTime = File.GetLastWriteTime(fileName).ToString("yyyyMMddHHmmss");

                        //if (!fileCreationTime.Equals(fileModificationTime))
                        //{
                        //    FailedMessageForFileModification();
                        //    return;
                        //}
                        #endregion

                        #region Test
                        FileInfo fileInfo = new FileInfo(fileName);
                        if (!fileInfo.CreationTime.Equals(fileInfo.LastWriteTime))
                        {
                            FailedMessageForFileModification();
                            return;
                        }
                        #endregion

                        string fileExtension = Path.GetExtension(fileName);

                        string savePath = ConfigurationManager.AppSettings["EFTExcelFiles"] + Guid.NewGuid().ToString() + fileExtension;

                        try
                        {
                            fulExcelFile.SaveAs(savePath);
                            //code is blocked for SCB second requirement
                            //if (!IsValidCSVDataForSCB(savePath))
                            //{
                            //    System.IO.File.Delete(savePath);
                            //    FailedMessage();
                            //    return;
                            //}

                            int createdBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
                            EFTN.BLL.CsvSCBDataEntryObject csvObj = new EFTN.BLL.CsvSCBDataEntryObject(
                                                    txtCompanyTIN.Text,
                                                    Int32.Parse(ddlPaymentType.SelectedValue),
                                                    txtCompanyName.Text,
                                                    txtReasonForPayment.Text,
                                                    savePath,
                                                    createdBy, ParseData.StringToInt(rdoButtonBatch.SelectedValue)
                                                    , rdoBtnTransactionType.SelectedValue, deptID
                                                    , DataEntryType.CSV
                                                    , ParseData.StringToInt(rdoBtnSettlementDay.SelectedValue), EFTConnectionString);
                            //csvObj.EntryData();  //   This was implemented for distributed batch
                            /***    Stored dataresult into data table to validate on STS New File Format Upload _ 16-01-2018     ***/
                            //Commented out for uploading multiple currencies transactions 
                            //myDTCorUpload = csvObj.EntryData(currency);
                            myDTCorUpload = csvObj.EntryData(createdBy);

                            #region STS update by Junayed 09_Sep_2017
                            /******** Commented out for going reverse back to existing STS process without Distributed BATCH *****/
                            //Response.Redirect("BulkTransactionListCsvSCB.aspx");

                            /********     Uncommented out for going reverse back to existing STS process     ********/
                            //Response.Redirect("BulkTransactionListCsvFilterForBatch.aspx");
                            #endregion

                            /***  Added Validation on STS New File Format Upload _ 10-01-2018   ***/
                            if (myDTCorUpload.Rows.Count.Equals(0))
                            {
                                ShowScopeWiseError(myDTCorUpload);
                            }
                            /***  Uncommented out for going reverse back to existing STS process without Distributed BATCH _ 03-01-2018   ***/
                            else if (myDTCorUpload.Rows.Count > 0)
                            {
                                strDataEntryType = DataEntryType.CSV;
                                TransactionID = csvObj.TransactionID;
                                BindImportedExcel(csvObj.BatchNumber);
                                SuccessMessage();
                            }
                        }
                        catch
                        {
                            InvalidFilePath();
                        }
                    }
                    else if (extension.ToLower().Equals(".txt"))
                    {
                        fileName = fulExcelFile.PostedFile.FileName;

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
                            EFTN.BLL.TextDataEntryObject txtObj = new EFTN.BLL.TextDataEntryObject(
                                                    txtCompanyTIN.Text,
                                                    Int32.Parse(ddlPaymentType.SelectedValue),
                                                    txtCompanyName.Text,
                                                    txtReasonForPayment.Text,
                                                    savePath,
                                                    createdBy, ParseData.StringToInt(rdoButtonBatch.SelectedValue)
                                                    , rdoBtnTransactionType.SelectedValue, deptID, DataEntryType.Text, EFTConnectionString);
                            myDTCorUpload = txtObj.EntryData();
                            if (myDTCorUpload.Rows.Count > 0)
                            {
                                strDataEntryType = DataEntryType.Text;
                                TransactionID = txtObj.TransactionID;
                                BindImportedExcel(txtObj.BatchNumber);
                                SuccessMessage();
                            }
                            else
                            {
                                FailedMessageForTextFileUpload();
                            }

                        }
                        catch
                        {
                            FailedMessageForTextFileUpload();
                        }
                    }


                    /***    Excel Upload Code Snippet   ***/

                    else
                    {
                        if (extension.ToLower().Equals(".xls") || extension.ToLower().Equals(".xlsx"))
                        {
                            fileName = fulExcelFile.PostedFile.FileName;
                        }
                        else
                        {
                            //FailedMessage();
                            CustomizedErrorMessage("Invalid File Extension!");
                            return;
                        }
                        string fileExtension = Path.GetExtension(fileName);

                        string savePath = ConfigurationManager.AppSettings["EFTExcelFiles"] + Guid.NewGuid().ToString() + fileExtension;
                        //string savePath = fileName;

                        
                        try
                        {
                            fulExcelFile.PostedFile.SaveAs(savePath);                          
                            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

                            // Commented and Added new _ 03-Sep-2019
                            //if (!IsValidExcelData(savePath, SelectedBank)) 
                            excelDataToUpload = new DataTable();
                            excelDataToUpload = IsValidExcelDataLatestBACH(savePath, SelectedBank);
                            if (excelDataToUpload.Equals(0) || !errorStringForExcelUpload.Equals(string.Empty))
                            {                                
                                //FailedMessage();
                                CustomizedErrorMessage("Uploaded Excel File's Columns Validation Failed!");
                                File.Delete(savePath);
                                return;
                            }
                            int createdBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
                            EFTN.BLL.ExcelDataBulkEntryObject excelObj = new EFTN.BLL.ExcelDataBulkEntryObject(
                                                    txtCompanyTIN.Text,
                                                    Int32.Parse(ddlPaymentType.SelectedValue),
                                                    txtCompanyName.Text,
                                                    txtReasonForPayment.Text,
                                                    savePath,
                                                    createdBy, ParseData.StringToInt(rdoButtonBatch.SelectedValue)
                                                    , rdoBtnTransactionType.SelectedValue, deptID
                                                    , DataEntryType.Excel, ParseData.StringToInt(rdoBtnSettlementDay.SelectedValue)
                                                    , EFTConnectionString);
                            myDTCorUpload = excelObj.EntryData(SelectedBank, ParseData.StringToInt(rdoButtonCBSAccountWiseHit.SelectedValue),excelDataToUpload,isRemittance);
                            if (myDTCorUpload.Rows.Count > 0)
                            {
                                strDataEntryType = DataEntryType.Excel;
                                TransactionID = excelObj.TransactionID;
                                BindImportedExcel(excelObj.BatchNumber);
                                SuccessMessage();
                            }
                            else if (myDTCorUpload.Rows.Count.Equals(0))
                            {
                                ShowScopeWiseError(myDTCorUpload);
                            }
                            //else
                            //{
                            //    FailedMessage();                                
                            //}

                        }
                        catch (Exception Ex)
                        {
                            //FailedMessage();
                            CustomizedErrorMessage(Ex.Message.ToString());
                        }
                        //finally
                        //{
                        //    //if (System.IO.File.Exists(savePath))
                        //    //{
                        //    CustomizedErrorMessage("System is removing the file.");

                        //    //}
                        //}
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
                    //string savePath = string.Empty;
                    string savePath = ConfigurationManager.AppSettings["EFTExcelFiles"] + Guid.NewGuid().ToString() + extension;
                    //+ extension; Commented out during BACH 2 upgrades instead added .xlsx by default
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

                        //savePath = ConfigurationManager.AppSettings["EFTExcelFiles"] + Guid.NewGuid().ToString() + fileExtension;
                        //fulExcelFile.SaveAs(savePath);



                        if (fileExtension.ToLower().Equals(".txt"))
                        {
                            EFTN.BLL.TextDataEntryObject txtObj = new EFTN.BLL.TextDataEntryObject(
                                txtCompanyTIN.Text,
                                Int32.Parse(ddlPaymentType.SelectedValue),
                                txtCompanyName.Text,
                                txtReasonForPayment.Text,
                                fileName,
                                createdBy, ParseData.StringToInt(rdoButtonBatch.SelectedValue)
                                , rdoBtnTransactionType.SelectedValue, deptID, DataEntryType.EncryptedExcel, EFTConnectionString);
                            myDTCorUpload = txtObj.EntryData();



                            removeDecryptedFile(filePath);
                            removeFile(savePath);

                            if (myDTCorUpload.Rows.Count > 0)
                            {
                                strDataEntryType = DataEntryType.EncryptedExcel;
                                TransactionID = txtObj.TransactionID;
                                BindImportedExcel(txtObj.BatchNumber);
                                SuccessMessage();
                            }
                            else
                            {
                                FailedMessage();
                            }
                        }
                        //////////////////////////////////////////////////////////////////////////
                        else if (fileExtension.ToLower().Equals(".csv"))
                        {
                            //fileName = fulExcelFile.PostedFile.FileName;

                            //string fileExtension = Path.GetExtension(fileName);

                            //string savePath = ConfigurationManager.AppSettings["EFTExcelFiles"] + Guid.NewGuid().ToString() + fileExtension;

                            //try
                            //{
                            //    fulExcelFile.SaveAs(savePath);
                            //string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

                            if (!IsValidCSVDataForEFT(fileName, SelectedBank))
                            {
                                System.IO.File.Delete(fileName);
                                FailedMessage();
                                return;
                            }

                            //int createdBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
                            EFTN.BLL.EFTCSVDataEntryObject csvObj = new EFTN.BLL.EFTCSVDataEntryObject(
                                                    txtCompanyTIN.Text,
                                                    Int32.Parse(ddlPaymentType.SelectedValue),
                                                    txtCompanyName.Text,
                                                    txtReasonForPayment.Text,
                                                    fileName,
                                                    createdBy, ParseData.StringToInt(rdoButtonBatch.SelectedValue)
                                                    , rdoBtnTransactionType.SelectedValue, deptID
                                                    , DataEntryType.CSV, EFTConnectionString);
                            myDTCorUpload = csvObj.EntryData(isRemittance);
                            removeDecryptedFile(filePath);
                            removeFile(savePath);

                            if (myDTCorUpload.Rows.Count > 0)
                            {
                                strDataEntryType = DataEntryType.EncryptedExcel;
                                TransactionID = csvObj.TransactionID;
                                BindImportedExcel(csvObj.BatchNumber);
                                SuccessMessage();
                            }
                            else
                            {
                                FailedMessage();
                            }

                            //}
                            //catch
                            //{
                            //    FailedMessageForGeneralCSVFileUpload();
                            //}
                        }

                        //////////////////////////////////////////////////////////////////////////
                        else
                        {
                            // Commented and Added new _ 03-Sep-2019
                            //if (!IsValidExcelData(savePath, SelectedBank))    
                            excelDataToUpload = new DataTable();                            
                            excelDataToUpload = IsValidExcelDataLatestBACH(fileName, SelectedBank);
                            if (excelDataToUpload.Equals(0) || !errorStringForExcelUpload.Equals(string.Empty))
                            {
                                CustomizedErrorMessage("Uploaded Excel File's Columns Validation Failed!");
                                File.Delete(savePath);
                                return;
                            }

                            EFTN.BLL.ExcelDataBulkEntryObject excelObj = new EFTN.BLL.ExcelDataBulkEntryObject(
                                                    txtCompanyTIN.Text,
                                                    Int32.Parse(ddlPaymentType.SelectedValue),
                                                    txtCompanyName.Text,
                                                    txtReasonForPayment.Text,
                                                    fileName,
                                                    createdBy, ParseData.StringToInt(rdoButtonBatch.SelectedValue)
                                                    , rdoBtnTransactionType.SelectedValue, deptID
                                                    , DataEntryType.EncryptedExcel
                                                    , ParseData.StringToInt(rdoBtnSettlementDay.SelectedValue)
                                                    , EFTConnectionString);
                            myDTCorUpload = excelObj.EntryData(SelectedBank, ParseData.StringToInt(rdoButtonCBSAccountWiseHit.SelectedValue), excelDataToUpload,isRemittance);

                            removeDecryptedFile(filePath);
                            removeFile(savePath);

                            if (myDTCorUpload.Rows.Count > 0)
                            {
                                strDataEntryType = DataEntryType.EncryptedExcel;
                                TransactionID = excelObj.TransactionID;
                                BindImportedExcel(excelObj.BatchNumber);
                                SuccessMessage();
                            }
                            else if (myDTCorUpload.Rows.Count.Equals(0) && myDTCorUpload.Columns.Contains("121"))
                            {
                                ShowScopeWiseError(myDTCorUpload);
                            }
                            else
                            {
                                FailedMessage();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //FailedMessage();
                        CustomizedErrorMessage(ex.Message);
                    }
                }
                //if (ParseData.StringToInt(ddlPaymentType.SelectedValue) == 2)
                //{
                //    TextBox txtIdNumber = (TextBox)FindControl("txtIdNumber");
                //    txtIdNumber.MaxLength = 22;
                //}
                //else
                //{
                //    TextBox txtIdNumber = (TextBox)FindControl("txtIdNumber");
                //    txtIdNumber.MaxLength = 15;
                //}               
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


        #region OldCodeBlock_Before_03-Sep-2019
        private DataTable IsValidExcelDataLatestBACH(string sourcefile, string SelectedBank)
        {
            EFTN.BLL.ValidateExelField vEF = new EFTN.BLL.ValidateExelField();
            DataTable excelData = new DataTable();
            excelData = vEF.ValidateExcelForEFTNewBACH(sourcefile, SelectedBank, ParseData.StringToInt(ddlPaymentType.Text));
            if (!errorStringForExcelUpload.Equals(string.Empty))
            { lblUploadErrMsg.Text = errorStringForExcelUpload; }
            return excelData;
        }
        #endregion
        private bool IsValidCSVDataForSCB(string sourcefile)
        {
            EFTN.BLL.ValidateCSVFieldForSCB vCsvSCB = new EFTN.BLL.ValidateCSVFieldForSCB();
            string errorExcel = string.Empty;
            errorExcel = vCsvSCB.ValidateCSVEFTForSCB(sourcefile, ParseData.StringToInt(ddlPaymentType.Text)).Trim();
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
            //lblMsgBatchNumber.Text = string.Empty;
            lblErrMsg.Text = "Failed to upload the CSV file! File has been modified.";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }

        private void FailedMessage()
        {
            //lblMsgBatchNumber.Text = string.Empty;
            lblErrMsg.Text = "Failed to upload invalid file. Please upload valid excel file. Scroll down to see the error detail.";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }
        private void CustomizedErrorMessage(string message)
        {
            lblErrMsg.Text = message;
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }
        private void FailedMessageForTextFileUpload()
        {
            //lblMsgBatchNumber.Text = string.Empty;
            lblErrMsg.Text = "Failed to upload invalid file. Please upload valid text file";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }

        private void FailedMessageForCSVFileUpload()
        {
            lblErrMsg.Text = string.Empty;
            lblErrMsg.Text = "Invalid or duplicate record. Failed to upload. Please see details in the CSV Reject Report";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }

        private void ShowScopeWiseError(DataTable errorData)
        {
            lblErrMsg.Text = string.Empty;
            if (errorData.Columns.Contains("STS"))
            {
                lblErrMsg.Text = "Old STS format detected! Please upload latest STS CSV file.";
            }
            else if (errorData.Columns.Contains("E11"))
            {
                lblErrMsg.Text = "Could not create temp table!";
            }
            else if (errorData.Columns.Contains("E12"))
            {
                lblErrMsg.Text = "Uploading to temp table and moving transactions to main table failed!";
            }
            else if (errorData.Columns.Contains("999"))
            {
                lblErrMsg.Text = "Invalid STS format detected! Please upload valid STS CSV file.";
            }
            else if (errorData.Columns.Contains("400"))
            {
                lblErrMsg.Text = "Duplicate STS transactions found! Please check with the CSV rejected transaction list.";
            }
            else if (errorData.Columns.Contains("111"))
            {
                lblErrMsg.Text = "Could not upload the STS transactions into csv storage!";
            }
            else if (errorData.Columns.Contains("222"))
            {
                lblErrMsg.Text = "Could not update transaction related informations at csv storage!";
            }
            else if (errorData.Columns.Contains("121"))
            {
                lblErrMsg.Text = "Please make decision for the pending transactions at remaining upload screen!";
            }
            lblErrMsg.ForeColor = System.Drawing.Color.Red;

        }
        private void InvalidFilePath()
        {
            lblErrMsg.Text = string.Empty;
            lblErrMsg.Text = "Invalid File Path! Please check uploaded file path at the server end.";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
        }

        private void FailedMessageForGeneralCSVFileUpload()
        {
            lblErrMsg.Text = string.Empty;
            lblErrMsg.Text = "Invalid Format. Failed to upload";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }

        private void FailedMessageForDuplicate()
        {
            //lblMsgBatchNumber.Text = string.Empty;
            lblErrMsg.Text = "Failed to decrypt duplicate file in the same location";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }

        private void SuccessMessage()
        {
            lblErrMsg.Text = "File has been uploaded successfully.";
            lblErrMsg.ForeColor = System.Drawing.Color.Blue;
            lblErrMsg.Font.Bold = true;
            lblErrMsg.Visible = true;
        }

        private void BindImportedExcel(string batchNumber)
        {
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            //if (SelectedBank.Equals("225"))
            //{
            //    EFTChargeManager eftChargeManager = new EFTChargeManager();
            //    if (ddListChargeCategoryList.SelectedValue.Equals("1"))
            //    {
            //        eftChargeManager.InsertCityChargeCodeByTransactionID(TransactionID, ParseData.StringToInt(ddListChargeCode.SelectedValue));
            //    }
            //    eftChargeManager.InsertChargeDefinitionForCityBankByTransactionID(TransactionID, ParseData.StringToInt(ddListChargeCategoryList.SelectedValue));
            //}
            BatchNumber = batchNumber;
            BindBatchTotal();
            dtgXcelUpload.CurrentPageIndex = 0;

            dv = myDTCorUpload.DefaultView;
            dtgXcelUpload.DataSource = dv;
            dtgXcelUpload.DataBind();

        }

        private void BindBatchTotal()
        {
            // BatchNumber has been commented out due to upload multiple currencies upload BACH II
            //lblMsgBatchNumber.Text = "Batch Number : " + BatchNumber;
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

        protected void dtgXcelUpload_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (strDataEntryType.Equals(DataEntryType.Excel) || strDataEntryType.Equals(DataEntryType.Text))
            {
                EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();

                if (e.CommandName == "Cancel")
                {
                    dtgXcelUpload.EditItemIndex = -1;
                    BindData();
                }
                if (e.CommandName == "Edit")
                {
                    dtgXcelUpload.EditItemIndex = e.Item.ItemIndex;
                    BindData();
                }

                if (e.CommandName == "Update")
                {
                    Guid EDRID = (Guid)dtgXcelUpload.DataKeys[e.Item.ItemIndex];

                    TextBox txtDFIAccountNo = (TextBox)e.Item.FindControl("txtDFIAccountNo");
                    TextBox txtAccountNo = (TextBox)e.Item.FindControl("txtAccountNo");
                    TextBox txtReceivingBankRoutingNo = (TextBox)e.Item.FindControl("txtReceivingBankRoutingNo");
                    TextBox txtPaymentInfo = (TextBox)e.Item.FindControl("txtPaymentInfo");
                    TextBox txtIdNumber = (TextBox)e.Item.FindControl("txtIdNumber");
                    TextBox txtReceiverName = (TextBox)e.Item.FindControl("txtReceiverName");
                    TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");

                    TextBox txtInvoiceNumber = (TextBox)e.Item.FindControl("txtInvoiceNumber");
                    TextBox txtInvoiceDate = (TextBox)e.Item.FindControl("txtInvoiceDate");
                    TextBox txtInvoiceGrossAmount = (TextBox)e.Item.FindControl("txtInvoiceGrossAmount");
                    TextBox txtInvoiceAmountPaid = (TextBox)e.Item.FindControl("txtInvoiceAmountPaid");
                    TextBox txtPurchaseOrder = (TextBox)e.Item.FindControl("txtPurchaseOrder");
                    TextBox txtAdjustmentAmount = (TextBox)e.Item.FindControl("txtAdjustmentAmount");
                    TextBox txtAdjustmentCode = (TextBox)e.Item.FindControl("txtAdjustmentCode");
                    TextBox txtAdjustmentDescription = (TextBox)e.Item.FindControl("txtAdjustmentDescription");

                    sentEDRDB.UpdateSentEDRByEDRSentIDForBulkUpload(EDRID, txtDFIAccountNo.Text.Trim(), txtAccountNo.Text.Trim()
                                                            , ParseData.StringToDecimal(txtAmount.Text.Trim())
                                                            , txtIdNumber.Text.Trim(), txtReceiverName.Text.Trim()
                                                            , txtPaymentInfo.Text.Trim()
                                                            , txtReceivingBankRoutingNo.Text.Trim()
                                                            , txtInvoiceNumber.Text.Trim(), txtInvoiceDate.Text.Trim()
                                                            , ParseData.StringToDecimal(txtInvoiceGrossAmount.Text.Trim())
                                                            , ParseData.StringToDecimal(txtInvoiceAmountPaid.Text.Trim())
                                                            , txtPurchaseOrder.Text.Trim()
                                                            , ParseData.StringToDecimal(txtAdjustmentAmount.Text.Trim())
                                                            , txtAdjustmentCode.Text.Trim()
                                                            , txtAdjustmentDescription.Text.Trim());
                    dtgXcelUpload.EditItemIndex = -1;

                    BindData();
                }
                if (e.CommandName == "Delete")
                {
                    Guid EDRID = (Guid)dtgXcelUpload.DataKeys[e.Item.ItemIndex];
                    sentEDRDB.DeleteTransactionSent(EDRID);
                    BindData();
                }
            }
            else
            {
                lblErrMsg.Text = "You are not allowed to update this batch.";
                lblErrMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void BindData()
        {
            EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
            myDTCorUpload = sentEDRDB.GetSentEDRByTransactionIDForBulk(TransactionID);
            dtgXcelUpload.DataSource = myDTCorUpload;
            dtgXcelUpload.DataBind();
            BindBatchTotal();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int createdBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            if (myDTCorUpload.Rows.Count > 0)
            {
                EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
                EFTCsvScbBulkDB csvDb = new EFTCsvScbBulkDB();
                string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

                #region Commented out as they need to upload multiple currencies dynamically
                //sentEDRDB.UpdateEDRSentto1(TransactionID);
                //BindData(); 
                #endregion
                DeleteAllMessages();
                csvDb.DeleteCsvTempData(EFTConnectionString, createdBy, false);
                Response.Redirect("BulkTransactionList.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            bool isFromCancel = false;
            int createdBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);
            if (myDTCorUpload.Rows.Count > 0)
            {
                #region Commented out as they need to upload multiple currencies dynamically
                //EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
                //sentBatchDB.DeleteBatchSent(TransactionID);
                #endregion
                string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
                isFromCancel = true;
                EFTCsvScbBulkDB csvDb = new EFTCsvScbBulkDB();
                csvDb.DeleteCsvTempData(EFTConnectionString, createdBy, isFromCancel);
                BindData();
                DeleteAllMessages();
            }
        }

        private void DeleteAllMessages()
        {
            //lblMsgBatchNumber.Text = string.Empty;
            lblTotalItem.Text = string.Empty;
            lblTotalAmount.Text = string.Empty;
            lblErrMsg.Text = string.Empty;
        }

        protected void ddListChargeCategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddListChargeCategoryList.SelectedValue.Equals("1"))
            {
                pnlChargeCode.Visible = true;
            }
            else
            {
                pnlChargeCode.Visible = false;
            }
        }
    }
}