using System.Data;
using System.Web.UI.WebControls;
using EFTN.Utility;
using FloraSoft;

namespace EFTN.BLL
{
    public class ExcelDataBulkReturn
    {
        private string excelFilePath;
        public string ExcelFilePath
        {
            get { return excelFilePath; }
        }

        public ExcelDataBulkReturn(string ExcelFilePath)
        {
            this.excelFilePath = ExcelFilePath;
        }

        public string EntryData(int CreatedBy)
        {
            string retMsg = string.Empty;
            try
            {
                retMsg = InsertBulkReturn(CreatedBy);
            }
            catch
            {
            }
            finally
            {
                System.IO.File.Delete(this.excelFilePath);
            }
            return retMsg;
        }

        private string InsertBulkReturn(int CreatedBy)
        {
            EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();
            // DataTable data = excelDB.GetCityReturnData(this.excelFilePath);
            DataTable data = excelDB.GetIFReturnData(this.excelFilePath);
            data = GetSpecificDataForIFReturn(data);
            BulkReturnDB bulkReturnDB = new BulkReturnDB();
            int successfulReturn = 0;
            string errMsg = string.Empty;
            int rowNumber = 0;
            foreach (DataRow row in data.Rows)
            {                
                string AccountNo = string.Empty;
                decimal Amount = 0;               
                bool isValid = true;
                rowNumber++;
                try
                {                   
                    AccountNo = row["AccountNo"].ToString().Trim();
                    Amount = decimal.Parse(row["Amount"].ToString().Trim());                   
                }
                catch
                {
                    isValid = false;
                }
                if (isValid)
                {
                    if (AccountNo.Equals(string.Empty))
                    {
                        errMsg += "Invalid Account Number in row no. " + rowNumber + "\n";
                    }
                    else
                    {
                        int retStatus = 0;
                       // retStatus = bulkReturnDB.InsertReturnFromBulk(Tracenumber, AccountNo, Amount, ReturnCode, CreatedBy);
                        retStatus = bulkReturnDB.InsertIFDebitReturn(Amount, AccountNo, CreatedBy);
                        if (retStatus == 1)
                        {
                            successfulReturn += retStatus;
                        }
                        else if (retStatus == 9)
                        {
                            errMsg += "Inward transaction not found with the associated data for row no. " + rowNumber + ", Account: " + AccountNo + ", Amount: " + Amount + "\n";
                        }
                        //else if (retStatus == 2)
                        //{
                        //    errMsg += "Inward transaction already returnd with the following data for row no. " + rowNumber + "\n";                                
                        //}
                    }
                }                
            }
            if (successfulReturn != 0 && successfulReturn != 9)
            {
                errMsg = successfulReturn + " inward transaction successfully returned. \n";
            }
            else
            {
                errMsg = successfulReturn + " transaction returned. "+ errMsg;
            }          
            return errMsg;
        }

        private DataTable GetSpecificDataForIFReturn(DataTable data)
        {
            DataTable filteredData = new DataTable();
            filteredData.Columns.Add("Amount", typeof(decimal));
            filteredData.Columns.Add("AccountNo", typeof(string));           

            foreach (DataRow row in data.Rows)
            {
                string keyValue = row.ItemArray[11].ToString().Trim();
                if (!keyValue.Equals(string.Empty) && !keyValue.Equals("Rejected Debits"))
                {
                    filteredData.Rows.Add(decimal.Parse(row.ItemArray[3].ToString().Trim()), row.ItemArray[11].ToString().Trim());
                }                
            }
            return filteredData;
        }
    }
}
