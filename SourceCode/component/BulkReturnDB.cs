using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace FloraSoft
{
    public class BulkReturnDB
    {
        public int InsertReturnFromBulk(  
                                        string Tracenumber,
                                        string AccountNo,
                                        double Amount,
                                        string ReturnCode,
                                        int CreatedBy)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_BulkReturn_Status", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTracenumber = new SqlParameter("@Tracenumber", SqlDbType.NVarChar, 15);
            parameterTracenumber.Value = Tracenumber;
            myCommand.Parameters.Add(parameterTracenumber);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 20);
            parameterAccountNo.Value = AccountNo;
            myCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterReturnCode = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 3);
            parameterReturnCode.Value = ReturnCode;
            myCommand.Parameters.Add(parameterReturnCode);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterSuccessfulReturn = new SqlParameter("@SuccessfulReturn", SqlDbType.Int);
            parameterSuccessfulReturn.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterSuccessfulReturn);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            return (int)parameterSuccessfulReturn.Value;
        }

        public int InsertIFDebitReturn(decimal amount, string accountNo, int createdBy)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertOutwardReturn_For_IF_Transactions", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar, 20);
            parameterAccountNo.Value = accountNo;
            myCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Decimal);
            parameterAmount.Value = amount;
            myCommand.Parameters.Add(parameterAmount);            

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = createdBy;
            myCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterSuccessfulReturn = new SqlParameter("@SuccessfulReturn", SqlDbType.Int);
            parameterSuccessfulReturn.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterSuccessfulReturn);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            return (int)parameterSuccessfulReturn.Value;
        }

        public DataTable GetReceivedEDRForAdminForDebitForSCBForIFReturn(int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ForAdmin_ForDebit_ForSCB_For_IF_Return", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;          

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);        

            myConnection.Open();
            DataTable dt = new DataTable();
            myAdapter.Fill(dt);

            myConnection.Close();
            myAdapter.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public void InsertReturnForIF(Guid edrId, int createdBy)
        {
            SqlConnection sqlConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("EFT_InsertReturn_For_IF", sqlConnection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEdrId = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEdrId.Value = edrId;
            sqlAdapter.SelectCommand.Parameters.Add(parameterEdrId);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = createdBy;
            sqlAdapter.SelectCommand.Parameters.Add(parameterCreatedBy);

            sqlConnection.Open();  
            sqlAdapter.SelectCommand.ExecuteNonQuery();
            sqlConnection.Close();
            sqlAdapter.Dispose();
            sqlConnection.Dispose();          
        }
    }
}
