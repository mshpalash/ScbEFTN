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
using EFTN.component;

namespace EFTN
{
    public partial class ExcelUploadForCBSMissMatchCheck : System.Web.UI.Page
    {
        //DataView dv;

        public static string ddlistTypeGlobal = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');
                //sortOrder = "asc";
            }
        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            string filePath = string.Empty;
            string extension = Path.GetExtension(fulExcelFile.PostedFile.FileName);

            if (extension.ToLower().Equals(".xls") || extension.ToLower().Equals(".xlsx"))
            {
                fileName = fulExcelFile.PostedFile.FileName;

                string fileExtension = Path.GetExtension(fileName);

                string savePath = ConfigurationManager.AppSettings["EFTExcelFiles"] + Guid.NewGuid().ToString() + fileExtension;

                try
                {
                    fulExcelFile.SaveAs(savePath);
                    string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                    InsertExcelData(savePath);
                }
                catch
                {
                    //if (ddlistTypeGlobal.Equals("3"))
                    //{
                    //    lblErrMsg.Text = "Check Report";
                    //}
                    //else
                    //{
                        lblErrMsg.Text = "Failed to upload file. Invalid Data Structure.";
                    //}
                    //FailedMessageForTextFileUpload();
                }
            }
        }

        private void InsertExcelData(string savePath)
        {
            DataTable dtInwardTransaction = new DataTable();
            DataTable dtMisMatchReport = new DataTable();
            DataTable dtMisMatchReportBatchWise = new DataTable();
            CBSMissMatchXLDB CbsMismatchReport = new CBSMissMatchXLDB();

            EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();
            DataTable data = excelDB.GetData(savePath);

            EFTN.component.CBSMissMatchXLDB cBSMissMatchXLDB = new EFTN.component.CBSMissMatchXLDB();
            int currentRowNumber = 1;
            string batchNumber = string.Empty;
            double totalAmount = 0;
            int totalItem = 0;
            string Narration = string.Empty;
            string BEFTNHoldingSuspenseAcc = ConfigurationManager.AppSettings["BEFTNHoldingSuspenseAcc"];
            string DDISuspenseAccount = ConfigurationManager.AppSettings["DDISuspenseAccount"];
            string ddlistFileTypeValue = ddListFileType.SelectedValue;

            ddlistTypeGlobal = ddlistFileTypeValue;
            //if (data.Rows.Count > 0)
            //{
            //    CbsMismatchReport.TruncateMismatchTempTable();
            //}

            CbsMismatchReport.SCBClearMisMatchXLDataTable();
            
            string narrationtotal = string.Empty;

            foreach (DataRow row in data.Rows)
            {
                //foreach start
                if (currentRowNumber > 2)
                {
                    string accountNumber = row["Credit / Debit"].ToString().Trim();
                    string ReceiverName = row["Trn Code"].ToString().Trim();
                    Narration = row["Tre Adv"].ToString().Trim();
                    double amount = ParseData.StringToDouble(EFTVariableParser.ParseEFTAmount(row["Product Code"].ToString()));
                    if (accountNumber.Trim().Equals(BEFTNHoldingSuspenseAcc)
                        || accountNumber.Trim().Equals(DDISuspenseAccount))
                    {
                        //do nothing
                    }
                    else
                    {
                        if (accountNumber.Equals("Totals  :"))
                        {
                            totalAmount = amount;
                            break;
                        }
                        if (!accountNumber.Trim().Equals(string.Empty)
                            && !ReceiverName.Trim().Equals(string.Empty)
                            && !Narration.Trim().Equals(string.Empty)
                            && amount != 0
                            )
                        {

                            if (ddlistFileTypeValue.Equals("4"))
                            {
                                batchNumber = Narration.Substring((Narration.Length - 6), 6);
                                totalItem = ParseData.StringToInt(Narration.Substring(42, 1));
                                cBSMissMatchXLDB.InsertMismatchXLOutwardBatchwise_ForSCB(batchNumber, totalItem, amount, accountNumber);
                            }

                            if (ddlistFileTypeValue.Equals("1"))
                            {
                                string xlTraceNumber = Narration.Substring(5, 15);
                                cBSMissMatchXLDB.InsertMismatchXLInward_ForSCB(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'), xlTraceNumber, accountNumber, amount);
                            }
                            else if (ddlistFileTypeValue.Equals("2"))
                            {
                                string xlTraceNumber = Narration.Substring(5, 15);
                                cBSMissMatchXLDB.InsertMismatchXLOutward_ForSCB(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'), xlTraceNumber, accountNumber, amount);
                            }
                            else if (ddlistFileTypeValue.Equals("3"))
                            {
                                string xlTraceNumber = Narration.Substring(9, 15);
                                //narrationtotal += Narration;
                                //string xlTraceNumber = "275260000011403";
                                cBSMissMatchXLDB.InsertMismatchXLInwardReturn_ForSCB(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                     + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                     + ddlistDay.SelectedValue.PadLeft(2, '0'), xlTraceNumber, accountNumber, amount);


                            }
                        }
                    }
                }
                // end of condition if (currentRowNumber > 2)

                currentRowNumber++;

                //foreach end
            }

            divItemWiseMismatch.Visible = false;
            divBatchWiseMismatch.Visible = false;

            if (ddlistFileTypeValue.Equals("2"))
            {
                dtMisMatchReport = CbsMismatchReport.SCBMisMatch_GetTransactionSent();
                if (dtMisMatchReport.Rows.Count > 0)
                {
                    divItemWiseMismatch.Visible = true;
                    divBatchWiseMismatch.Visible = false;

                    dtgMismatchReport.CurrentPageIndex = 0;
                    dtgMismatchReport.Columns[3].FooterText = "Total Amount";
                    dtgMismatchReport.Columns[1].FooterText = "Total Item :" + dtMisMatchReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgMismatchReport.Columns[4].FooterText = ParseData.StringToDouble(dtMisMatchReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                    lblErrMsg.Text = "Mismatch found.";
                }
                else
                {
                    lblErrMsg.Text = "Uploaded Successfully. No mismatch found.";
                }
                dtgMismatchReport.DataSource = dtMisMatchReport;
                dtgMismatchReport.DataBind();

            }
            else if (ddlistFileTypeValue.Equals("1"))
            {
                dtMisMatchReport = CbsMismatchReport.SCBMisMatch_GetTransactionReceived();
                if (dtMisMatchReport.Rows.Count > 0)
                {
                    divItemWiseMismatch.Visible = true;
                    divBatchWiseMismatch.Visible = false;

                    dtgMismatchReport.CurrentPageIndex = 0;
                    dtgMismatchReport.Columns[3].FooterText = "Total Amount";
                    dtgMismatchReport.Columns[1].FooterText = "Total Item :" + dtMisMatchReport.Compute("COUNT(TraceNumber)", "").ToString();
                    dtgMismatchReport.Columns[4].FooterText = ParseData.StringToDouble(dtMisMatchReport.Compute("SUM(Amount)", "").ToString()).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                    lblErrMsg.Text = "Mismatch found.";
                }
                else
                {
                    lblErrMsg.Text = "Uploaded Successfully. No mismatch found.";
                }
                dtgMismatchReport.DataSource = dtMisMatchReport;
                dtgMismatchReport.DataBind();
            }
            else if (ddlistFileTypeValue.Equals("3"))
            {
                dtMisMatchReport = CbsMismatchReport.SCBMisMatch_GetInwardReturn();
                if (dtMisMatchReport.Rows.Count > 0)
                {
                    divItemWiseMismatch.Visible = true;
                    divBatchWiseMismatch.Visible = false;

                    dtgMismatchReport.CurrentPageIndex = 0;
                    dtgMismatchReport.Columns[3].FooterText = "total amount";
                    dtgMismatchReport.Columns[1].FooterText = "total item :" + dtMisMatchReport.Compute("count(tracenumber)", "").ToString();
                    dtgMismatchReport.Columns[4].FooterText = ParseData.StringToDouble(dtMisMatchReport.Compute("sum(amount)", "").ToString()).ToString("n", System.Globalization.CultureInfo.InvariantCulture);
                    lblErrMsg.Text = "Mismatch found";
                }
                else
                {
                    lblErrMsg.Text = "uploaded successfully. no mismatch found.";
                }
            }
            else if (ddlistFileTypeValue.Equals("4"))
            {
                dtMisMatchReportBatchWise = CbsMismatchReport.GetBatches_ForExcelMismatch(
                                                                 ddlistYear.SelectedValue.PadLeft(4, '0')
                                                               + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                               + ddlistDay.SelectedValue.PadLeft(2, '0'));
                if (dtMisMatchReportBatchWise.Rows.Count > 0)
                {
                    divItemWiseMismatch.Visible = false;
                    divBatchWiseMismatch.Visible = true;

                    dtgMisMatchBatchWise.CurrentPageIndex = 0;
                    lblErrMsg.Text = "Mismatch found.";
                }
                else
                {
                    lblErrMsg.Text = "Uploaded Successfully. No mismatch found.";
                }
                dtgMisMatchBatchWise.DataSource = dtMisMatchReportBatchWise;
                dtgMisMatchBatchWise.DataBind();
            }
        }

        //protected void dtgMismatchReport_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        //{
        //    dtgMismatchReport.CurrentPageIndex = e.NewPageIndex;
        //    dtgMismatchReport.DataSource = dtMisMatchReport;
        //    dtgMismatchReport.DataBind();

        //}
    }
}