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
    public class ExcelDataCityDebitAccStatus
    {
        private string excelFilePath;
        public string ExcelFilePath
        {
            get { return excelFilePath; }
        }

        public ExcelDataCityDebitAccStatus(string ExcelFilePath)
        {
            this.excelFilePath = ExcelFilePath;
        }

        public DataTable EntryData()
        {
            DataTable errCityTable = new DataTable();
            try
            {
                errCityTable = InsertCityDebitAccountInfo();
            }
            catch
            {

            }
            finally
            {
                System.IO.File.Delete(this.excelFilePath);
            }
            return errCityTable;
        }

        private DataTable InsertCityDebitAccountInfo()
        {
            CityDebitAccountManager cityDebitAccountManager = new CityDebitAccountManager();
            EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();
            DataTable data = excelDB.GetData(this.excelFilePath);
            int totalSuccessfull = 0;

            DataTable errTable = GetCityDebitAccountDataTable();
            int rowNumber = 0;
            foreach (DataRow row in data.Rows)
            {
                rowNumber++;
                string AccountNo = row["AccountNo"].ToString();
                string OtherBankAcNo = row["OtherBankAcNo"].ToString();
                string RoutingNumber = row["RoutingNumber"].ToString();
                string AccountName = row["AccountName"].ToString();

                RoutingNumber = RoutingNumber.PadLeft(9, '0');

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
                    InsertError(errTable, AccountNo, OtherBankAcNo, RoutingNumber, AccountName, ExceptionMsg, rowNumber);
                    continue;
                }


                int noOfInsert = 0;
                noOfInsert = cityDebitAccountManager.InsertCityDebitAccount(AccountNo, OtherBankAcNo, RoutingNumber, AccountName);
                
                if (noOfInsert == 0)
                {
                    InsertError(errTable, AccountNo, OtherBankAcNo, RoutingNumber, AccountName, "Already Exists", rowNumber);
                }
                
                totalSuccessfull += noOfInsert;
            }

            return errTable;
        }


        private DataTable InsertError(DataTable errTable, string AccountNo, string OtherBankAcNo, string RoutingNumber, string AccountName, string ExceptionMsg, int rowNumber)
        {
            errTable.Rows.Add(rowNumber,AccountNo, OtherBankAcNo,AccountName, RoutingNumber, ExceptionMsg);
            return errTable;
        }

        private static DataTable GetCityDebitAccountDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("RowNumber", typeof(string));
            table.Columns.Add("AccountNo", typeof(string));
            table.Columns.Add("OtherBankAcNo", typeof(string));
            table.Columns.Add("AccountName", typeof(string));
            table.Columns.Add("RoutingNumber", typeof(string));
            table.Columns.Add("ExceptionMsg", typeof(string));

            return table;
        }
    }
}
