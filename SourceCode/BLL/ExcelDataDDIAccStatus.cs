using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FloraSoft;
using EFTN.Utility;

namespace EFTN.BLL
{
    public class ExcelDataDDIAccStatus
    {
        private string excelFilePath;
        public string ExcelFilePath
        {
            get { return excelFilePath; }
        }

        public ExcelDataDDIAccStatus(string ExcelFilePath)
        {
            this.excelFilePath = ExcelFilePath;
        }

        public DataTable EntryData()
        {
            DataTable errDDITable = new DataTable();
            try
            {
                errDDITable = InsertDDIAccountStatus();
            }
            catch(Exception ki)
            {
                var aa = ki.ToString();
            }
            finally
            {
                System.IO.File.Delete(this.excelFilePath);
            }
            return errDDITable;
        }

        private DataTable InsertDDIAccountStatus()
        {
            DDIManager ddiManager = new DDIManager();
            EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();
            //DataTable data = excelDB.GetData(this.excelFilePath);
            DataTable data = excelDB.GetData_DDI(this.excelFilePath);
            DateTime dtExpiry;
            int totalSuccessfull = 0;

            DataTable errTable = GetDDIDataTable();
            int rowNumber = 0;
            foreach (DataRow row in data.Rows)
            {
                rowNumber++;
                string AccountNo = row["AccountNo"].ToString();
                string OtherBankAcNo = row["OtherBankAcNo"].ToString();
                string RoutingNumber = row["RoutingNumber"].ToString();
                string ExpiryDate = row["ExpiryDate"].ToString();
                int AccountExceptionFromExcel = ParseData.StringToInt(row["AccountException"].ToString());

                RoutingNumber = RoutingNumber.PadLeft(9, '0');
                try
                {
                    dtExpiry = DateTime.Parse(ExpiryDate);
                }
                catch
                {
                    dtExpiry = System.DateTime.Now.AddYears(5);
                }

                bool isErrorExist = false;
                string ExceptionMsg = string.Empty;
                
                if (!EFTN.BLL.RoutingNumberValidator.CheckDigitOk(RoutingNumber))
                {
                    isErrorExist = true;
                    ExceptionMsg = "Invalid Routing Number";
                }
                
                if (AccountNo.Equals(string.Empty) || OtherBankAcNo.Equals(string.Empty))
                {
                    isErrorExist = true;
                    ExceptionMsg += "Account Number is empty";
                }

                if (isErrorExist)
                {
                    InsertError(errTable, AccountNo, OtherBankAcNo, RoutingNumber, ExceptionMsg, rowNumber);
                    continue;
                }

                bool AccountException = false;
                if (AccountExceptionFromExcel == 1)
                {
                    AccountException = true;
                }

                int noOfInsert = 0;
                noOfInsert = ddiManager.InsertDDIAccountStatus(AccountNo, OtherBankAcNo, RoutingNumber, dtExpiry, AccountException);
                
                if (noOfInsert == 0)
                {
                    InsertError(errTable, AccountNo, OtherBankAcNo, RoutingNumber, "Already Exists", rowNumber);
                }
                
                totalSuccessfull += noOfInsert;
            }

            return errTable;
        }


        private DataTable InsertError(DataTable errTable, string AccountNo, string OtherBankAcNo, string RoutingNumber, string ExceptionMsg, int rowNumber)
        {
            errTable.Rows.Add(rowNumber,AccountNo, OtherBankAcNo, RoutingNumber, ExceptionMsg);
            return errTable;
        }

        private static DataTable GetDDIDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("RowNumber", typeof(string));
            table.Columns.Add("AccountNo", typeof(string));
            table.Columns.Add("OtherBankAcNo", typeof(string));
            table.Columns.Add("RoutingNumber", typeof(string));
            table.Columns.Add("ExceptionMsg", typeof(string));

            return table;
        }
    }
}
