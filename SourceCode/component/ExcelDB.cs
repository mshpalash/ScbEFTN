using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;


namespace EFTN.component
{
    public class ExcelDB
    {       
        public DataTable GetData(string fileName)
        {
            string fileExtension = System.IO.Path.GetExtension(fileName);           
            string connectionString = string.Empty;            
            if (fileExtension.ToLower().Equals(".xls"))
            {
                connectionString = ConfigurationManager.AppSettings["ExcelConnectionString"];
            }           
            else
            {
                connectionString = ConfigurationManager.AppSettings["ExcelConnectionStringForXLSX"];               
            }
            OleDbConnection connection = new OleDbConnection(connectionString + fileName + "'");
            OleDbCommand command = new OleDbCommand();
            //command.CommandText = "SELECT * FROM [Sheet1$]";
            command.CommandText = "SELECT  * from [Sheet1$] WHERE LTRIM(RTRIM(Reason)) not in ('') and LTRIM(RTRIM(SenderAccNumber)) NOT IN ('') and LTRIM(RTRIM(ReceivingBankRouting)) NOT IN ('') and LTRIM(RTRIM(BankAccNo)) NOT IN ('') and LTRIM(RTRIM(AccType)) NOT IN ('') and LTRIM(RTRIM(Amount)) NOT IN ('') and LTRIM(RTRIM(ReceiverName)) NOT IN ('') and LTRIM(RTRIM(ReceiverID)) NOT IN ('') and LTRIM(RTRIM(Currency)) NOT IN ('')";
            command.Connection = connection;
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command;
            try
            {
                connection.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public DataTable GetData_DDI(string fileName)
        {
            string fileExtension = System.IO.Path.GetExtension(fileName);
            string connectionString = string.Empty;
            if (fileExtension.ToLower().Equals(".xls"))
            {
                connectionString = ConfigurationManager.AppSettings["ExcelConnectionString"];
            }
            else
            {
                connectionString = ConfigurationManager.AppSettings["ExcelConnectionStringForXLSX"];
            }
            OleDbConnection connection = new OleDbConnection(connectionString + fileName + "'");
            OleDbCommand command = new OleDbCommand();
            //command.CommandText = "SELECT * FROM [Sheet1$]";
            command.CommandText = "SELECT  * from [Sheet1$]";
            command.Connection = connection;
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command;
            try
            {
                connection.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public DataTable GetDataForBranch(string fileName)
        {
            string fileExtension = System.IO.Path.GetExtension(fileName);
            string connectionString = string.Empty;
            if (fileExtension.ToLower().Equals(".xls"))
            {
                connectionString = ConfigurationManager.AppSettings["ExcelConnectionString"];
            }
            else
            {
                connectionString = ConfigurationManager.AppSettings["ExcelConnectionStringForXLSX"];
            }
            OleDbConnection connection = new OleDbConnection(connectionString + fileName + "'");
            OleDbCommand command = new OleDbCommand();
            command.CommandText = "SELECT * FROM [Sheet1$]";           
            command.Connection = connection;
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command;
            try
            {
                connection.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public DataTable GetCityReturnData(string fileName)
        {
            string fileExtension = System.IO.Path.GetExtension(fileName);
            string connectionString = string.Empty;
            if (fileExtension.ToLower().Equals(".xls"))
            {
                connectionString = ConfigurationManager.AppSettings["ExcelConnectionString"];
            }
            else
            {
                connectionString = ConfigurationManager.AppSettings["ExcelConnectionStringForXLSX"];
            }
            OleDbConnection connection = new OleDbConnection(connectionString + fileName + "'");

            OleDbCommand command = new OleDbCommand();
            command.CommandText = "SELECT bank_sl, bank_ac_no, refund_amt, ReturnCode FROM [Sheet1$]";
            command.Connection = connection;

            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command;

            try
            {
                connection.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public DataTable GetIFReturnData(string fileName)
        {
            string fileExtension = System.IO.Path.GetExtension(fileName);
            string connectionString = string.Empty;
            if (fileExtension.ToLower().Equals(".xls"))
            {
                connectionString = ConfigurationManager.AppSettings["ExcelConnectionString"];
            }
            else
            {
                connectionString = ConfigurationManager.AppSettings["ExcelConnectionStringForXLSX"];
            }
            OleDbConnection connection = new OleDbConnection(connectionString + fileName + "'");

            OleDbCommand command = new OleDbCommand();
            //command.CommandText = "SELECT bank_sl, bank_ac_no, refund_amt, ReturnCode FROM [Sheet1$]";
            command.CommandText = "SELECT * FROM [Sheet1$]";
            command.Connection = connection;

            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command;

            try
            {
                connection.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
    }
}
