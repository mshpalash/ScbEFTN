using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using EFTN.Utility;
namespace EFTN.component
{
    public class SearchReceivedEDRDB
    {
        //search-done
        public DataTable GetReceivedEDRForAdmin(string BankCode, string SearchParam)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_SearchGetReceivedEDRForAdmin", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            command.Parameters.Add(parameterBankCode);


            SqlParameter parameterSearchParam = new SqlParameter("@SearchParam", SqlDbType.NVarChar, 50);
            parameterSearchParam.Value = SearchParam;
            command.Parameters.Add(parameterSearchParam);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            adapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;            
        }

        //search
        public DataTable GetReceivedEDRForAdmin(int BranchID, string BankCode, string SearchParam)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_SearchGetReceivedEDRForAdminbyBranches", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEffectiveBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterEffectiveBranchID.Value = BranchID;
            myCommand.SelectCommand.Parameters.Add(parameterEffectiveBranchID);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myCommand.SelectCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterSearchParam = new SqlParameter("@SearchParam", SqlDbType.NVarChar, 50);
            parameterSearchParam.Value = SearchParam;
            myCommand.SelectCommand.Parameters.Add(parameterSearchParam);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

        //search
        public DataTable GetReceivedEDRForAdmin_ForDebit(string BankCode, string SearchParam)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_SearchGetReceivedEDR_ForAdmin_ForDebit", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            command.Parameters.Add(parameterBankCode);

            SqlParameter parameterSearchParam = new SqlParameter("@SearchParam", SqlDbType.NVarChar, 50);
            parameterSearchParam.Value = SearchParam;
            command.Parameters.Add(parameterSearchParam);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            adapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }

        //search
        public DataTable GetReceivedEDRForAdmin_ForDebit(int BranchID, string BankCode, string SearchParam)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_SearchGetReceivedEDR_ForAdmin_byBranches_ForDebit", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEffectiveBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterEffectiveBranchID.Value = BranchID;
            myCommand.SelectCommand.Parameters.Add(parameterEffectiveBranchID);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myCommand.SelectCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterSearchParam = new SqlParameter("@SearchParam", SqlDbType.NVarChar, 50);
            parameterSearchParam.Value = SearchParam;
            myCommand.SelectCommand.Parameters.Add(parameterSearchParam);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

        //search
        public DataTable GetReceivedEDRForAdmin_ForALL(string BankCode, string SearchParam)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_SearchGetReceivedEDR_ForAdmin_ForAll", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterSearchParam = new SqlParameter("@SearchParam", SqlDbType.NVarChar, 50);
            parameterSearchParam.Value = SearchParam;
            myAdapter.SelectCommand.Parameters.Add(parameterSearchParam);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //search
        public DataTable EFT_GetReceivedEDR_ForAdmin_byBranches_ForAll(int BranchID, string BankCode, string SearchParam)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_SearchGetReceivedEDRForAdminByBranches_ForAll", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterSearchParam = new SqlParameter("@SearchParam", SqlDbType.NVarChar, 50);
            parameterSearchParam.Value = SearchParam;
            myAdapter.SelectCommand.Parameters.Add(parameterSearchParam);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}