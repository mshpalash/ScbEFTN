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

namespace EFTN.BLL
{
    public class ExcelDataBranch
    {
        private string excelFilePath;
        public string ExcelFilePath
        {
            get { return excelFilePath; }
        }

        public ExcelDataBranch(string ExcelFilePath)
        {
            this.excelFilePath = ExcelFilePath;
        }

        public DataTable EntryData()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = InsertBranch();
            }
            catch
            {

            }
            finally
            {
                System.IO.File.Delete(this.excelFilePath);
            }
            return dt;
        }

        private DataTable InsertBranch()
        {
            EFTN.component.ExcelDB excelDB = new EFTN.component.ExcelDB();
            //DataTable data = excelDB.GetData(this.excelFilePath);
            DataTable data = excelDB.GetDataForBranch(this.excelFilePath);            
            BranchesDB branchesDB = new BranchesDB();
            foreach (DataRow row in data.Rows)
            {
                string routingNumber = row["ROUTINGNO"].ToString();
                string branchName = row["BRANCH NAME"].ToString();

                branchesDB.InsertBranchForBulk(branchName, routingNumber);
            }
            return data;
        }
    }
}
