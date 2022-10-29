using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace FloraSoft
{
	public class BranchesDB
	{

		//----------------------------------------------------------------------
		// Method InsertBranches
		//----------------------------------------------------------------------
		public int InsertBranches(int BankID, String BranchName, string RoutingNo)
		{
			SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
			SqlCommand myCommand = new SqlCommand("EFT_InsertBranch", myConnection);
			myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBankID = new SqlParameter("@BankID", SqlDbType.Int,4);
            parameterBankID.Value = BankID;
            myCommand.Parameters.Add(parameterBankID);

            SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.NVarChar,50);
            parameterBranchName.Value = BranchName;
            myCommand.Parameters.Add(parameterBranchName);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.NChar, 9);
            parameterRoutingNo.Value = RoutingNo;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int, 4);
			parameterBranchID.Direction	 =  ParameterDirection.Output;
			myCommand.Parameters.Add(parameterBranchID);

			myConnection.Open();
			myCommand.ExecuteNonQuery();
			myConnection.Close();
			return (int) parameterBranchID.Value;
		}


        public int InsertBranchForBulk(String BranchName, string RoutingNo)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_InsertBranchForBulk", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.NVarChar, 50);
            parameterBranchName.Value = BranchName;
            myCommand.Parameters.Add(parameterBranchName);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.NChar, 9);
            parameterRoutingNo.Value = RoutingNo;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBranchID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            return (int)parameterBranchID.Value;
        }
        //----------------------------------------------------------------------
		// Method UpdateBranches
		//----------------------------------------------------------------------
        public void UpdateBranch(int BranchID, String BranchName, string RoutingNo)
		{

			SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
			SqlCommand myCommand =  new SqlCommand("EFT_UpdateBranch", myConnection);
			myCommand.CommandType = CommandType.StoredProcedure;

			SqlParameter parameterBranchID	= new SqlParameter("@BranchID", SqlDbType.Int,4);
			parameterBranchID.Value	 = BranchID;
			myCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.NVarChar, 50);
            parameterBranchName.Value = BranchName;
            myCommand.Parameters.Add(parameterBranchName);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.NChar, 9);
            parameterRoutingNo.Value = RoutingNo;
            myCommand.Parameters.Add(parameterRoutingNo);

            myConnection.Open();
			myCommand.ExecuteNonQuery();
			myConnection.Close();
		}

        public void UpdateBranchbyAdminChecker(int BranchID, String BranchStatus)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_UpdateBranchbyAdminChecker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            parameterBranchID.Value = BranchID;
            myCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterBranchStatus = new SqlParameter("@BranchStatus", SqlDbType.NVarChar, 50);
            parameterBranchStatus.Value = BranchStatus;
            myCommand.Parameters.Add(parameterBranchStatus);



            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }


        //public SqlDataReader GetBranchesByBankID(int BankID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBranchesByBankID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterBankID = new SqlParameter("@BankID", SqlDbType.Int, 4);
        //    parameterBankID.Value = BankID;
        //    myCommand.Parameters.Add(parameterBankID);

        //    myConnection.Open();
        //    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return result;
        //}

        public DataTable GetBranchesByBankID(int BankID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBranchesByBankID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBankID = new SqlParameter("@BankID", SqlDbType.Int);
            parameterBankID.Value = BankID;
            myAdapter.SelectCommand.Parameters.Add(parameterBankID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetBranchesByBankIDforAdminChecker(int BankID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBranchesByBankIDforAdminChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterBankID = new SqlParameter("@BankID", SqlDbType.Int, 4);
        //    parameterBankID.Value = BankID;
        //    myCommand.Parameters.Add(parameterBankID);

        //    myConnection.Open();
        //    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return result;
        //}

        public DataTable GetBranchesByBankIDforAdminChecker(int BankID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBranchesByBankIDforAdminChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBankID = new SqlParameter("@BankID", SqlDbType.Int);
            parameterBankID.Value = BankID;
            myAdapter.SelectCommand.Parameters.Add(parameterBankID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetInactiveBranches()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetInactiveBranches", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

          

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetBranchesByUserID(int UserID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBranchesByUserID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
        //    parameterUserID.Value = UserID;
        //    myCommand.Parameters.Add(parameterUserID);

        //    myConnection.Open();
        //    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return result;
        //}

        public DataTable GetBranchesByUserID(int UserID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBranchesByUserID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetBranchesByBankCode(string BankCode)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBranchesByBankCode", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NVarChar,3);
        //    parameterBankCode.Value = BankCode;
        //    myCommand.Parameters.Add(parameterBankCode);

        //    myConnection.Open();
        //    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return result;
        //}

        public DataTable GetBranchesByBankCode(string BankCode)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBranchesByBankCode", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetBranchByBankCode(string BankCode)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBranchesByBankCode", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NVarChar, 3);
        //    parameterBankCode.Value = BankCode;
        //    myCommand.Parameters.Add(parameterBankCode);

        //    myConnection.Open();
        //    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return result;
        //}

        public DataTable GetBranchByBankCode(string BankCode)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBranchesByBankCode", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBankBranchByRoutingNumber(string RoutingNo)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBankBranchByRoutingNumber", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.NChar);
            parameterRoutingNo.Value = RoutingNo;
            myAdapter.SelectCommand.Parameters.Add(parameterRoutingNo);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchDetailsByBranchID(int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBranchesDetailsByBranchID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.NChar);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

    }
}

