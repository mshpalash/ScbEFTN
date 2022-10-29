using System;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using EFTN.BLL;

namespace EFTN.component
{
    public class ExcelEFTBulkDB
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

        public void CreateTempSentEDRTable(string TableName, string constr)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand myCommand = new SqlCommand("EFT_CreateTempSentEDRTable", con);
                myCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter parameterTableName = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
                parameterTableName.Value = TableName;
                myCommand.Parameters.Add(parameterTableName);

                con.Open();
                myCommand.ExecuteNonQuery();
                con.Close();

            }
        }

        public void UploadBulkExcelDataToTempTableWithoutColumnMapping(string fileName, string TableName, string sqlServerConnectionString)
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

            OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$] where LTRIM(RTRIM(Reason)) not in ('') and LTRIM(RTRIM(SenderAccNumber)) NOT IN ('') and LTRIM(RTRIM(ReceivingBankRouting)) NOT IN ('') and LTRIM(RTRIM(BankAccNo)) NOT IN ('') and LTRIM(RTRIM(AccType)) NOT IN ('') and LTRIM(RTRIM(Amount)) NOT IN ('') and LTRIM(RTRIM(ReceiverName)) NOT IN ('') and LTRIM(RTRIM(ReceiverID)) NOT IN ('')", connection);
            //OleDbCommand cmd = new OleDbCommand(Query, Oledbcon);
            OleDbDataAdapter ObjAdapter1 = new OleDbDataAdapter(cmd);
            connection.Open();
            DbDataReader dr = cmd.ExecuteReader();
            SqlBulkCopy bulkinsert = new SqlBulkCopy(sqlServerConnectionString);
            bulkinsert.DestinationTableName = TableName;
            bulkinsert.WriteToServer(dr);
            connection.Close();
        }

        #region LiveExcelUploadMethodBackup
        //public void UploadBulkExcelDataToTempTable(string fileName, string TableName, string sqlServerConnectionString)
        //{
        //    string fileExtension = System.IO.Path.GetExtension(fileName);
        //    string connectionString = string.Empty;
        //    if (fileExtension.ToLower().Equals(".xls"))
        //    {
        //        connectionString = ConfigurationManager.AppSettings["ExcelConnectionString"];
        //    }
        //    else
        //    {
        //        connectionString = ConfigurationManager.AppSettings["ExcelConnectionStringForXLSX"];
        //    }
        //    OleDbConnection connection = new OleDbConnection(connectionString + fileName + "'");

        //    OleDbCommand cmd = new OleDbCommand("select  * from [Sheet1$] where LTRIM(RTRIM(Reason)) not in ('') and LTRIM(RTRIM(SenderAccNumber)) NOT IN ('') and LTRIM(RTRIM(ReceivingBankRouting)) NOT IN ('') and LTRIM(RTRIM(BankAccNo)) NOT IN ('') and LTRIM(RTRIM(AccType)) NOT IN ('') and LTRIM(RTRIM(Amount)) NOT IN ('') and LTRIM(RTRIM(ReceiverName)) NOT IN ('') and LTRIM(RTRIM(ReceiverID)) NOT IN ('')", connection);
        //    //OleDbCommand cmd = new OleDbCommand(Query, Oledbcon);
        //    connection.Open();
        //    DataSet ds = new DataSet();
        //    OleDbDataAdapter ObjAdapter1 = new OleDbDataAdapter(cmd);

        //    //DbDataReader dr = cmd.ExecuteReader();

        //    ObjAdapter1.Fill(ds);

        //    DataTable Exceldt = ds.Tables[0];

        //    SqlBulkCopy bulkinsert = new SqlBulkCopy(sqlServerConnectionString);
        //    bulkinsert.DestinationTableName = TableName;

        //    bulkinsert.ColumnMappings.Add("Reason", "Reason");
        //    bulkinsert.ColumnMappings.Add("SenderAccNumber", "SenderAccNumber");
        //    bulkinsert.ColumnMappings.Add("ReceivingBankRouting", "ReceivingBankRouting");
        //    bulkinsert.ColumnMappings.Add("BankAccNo", "BankAccNo");
        //    bulkinsert.ColumnMappings.Add("AccType", "AccType");
        //    bulkinsert.ColumnMappings.Add("Amount", "Amount");
        //    bulkinsert.ColumnMappings.Add("ReceiverName", "ReceiverName");
        //    bulkinsert.ColumnMappings.Add("ReceiverID", "ReceiverID");
        //    try
        //    {
        //        bulkinsert.WriteToServer(Exceldt);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}
        #endregion

      

        public void UpdateTempSentEDRColumn
         (
            Guid TransactionID,
            int CreatedBy,
            string TransactionType,
            int TypeOfPayment,
            int DepartmentID,
            string myConnection,
            string TableName          
         )
        {
            SqlConnection connection = new SqlConnection(myConnection);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_UpdateTempSentEDRColumn";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Value = TransactionID;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            paramCreatedBy.Value = CreatedBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter paramTransactionType = new SqlParameter("@TransactionType", SqlDbType.VarChar, 10);
            paramTransactionType.Value = TransactionType;
            command.Parameters.Add(paramTransactionType);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = TypeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);


            SqlParameter paramDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            paramDepartmentID.Value = DepartmentID;
            command.Parameters.Add(paramDepartmentID);

            SqlParameter parameterTableName = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
            parameterTableName.Value = TableName;
            command.Parameters.Add(parameterTableName);        

            connection.Open();

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public void TransferTransactionSent_FromBulkEntry(string TableName, string CreatedBy,  string constr, int isRemittance)
        {
            SqlConnection connection = new SqlConnection(constr);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertTransactionSent_FromBulkEntry";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTableName = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
            parameterTableName.Value = TableName;
            command.Parameters.Add(parameterTableName);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            paramCreatedBy.Value = CreatedBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter paramRemittance = new SqlParameter("@IsRemittance", SqlDbType.VarChar,1);
            paramRemittance.Value = isRemittance.ToString();
            command.Parameters.Add(paramRemittance);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public void DropTempSentEDRTable(string TableName, string constr)
        {
            SqlConnection connection = new SqlConnection(constr);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_DropTempSentEDRTable";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTableName = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
            parameterTableName.Value = TableName;
            command.Parameters.Add(parameterTableName);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }
    }
}