using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FloraSoft
{
    public class BanksDB
    {
        public int InsertBank(String BankCode, String BankName)
		{
			SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
			SqlCommand myCommand = new SqlCommand("EFT_InsertBank", myConnection);
			myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NVarChar, 3);
            parameterBankCode.Value = BankCode;
            myCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterBankName = new SqlParameter("@BankName", SqlDbType.NVarChar, 50);
			parameterBankName.Value	 = BankName;
			myCommand.Parameters.Add(parameterBankName);

			SqlParameter parameterBankID	= new SqlParameter("@BankID", SqlDbType.Int,4);
			parameterBankID.Direction	 =  ParameterDirection.Output;
			myCommand.Parameters.Add(parameterBankID);

			myConnection.Open();
			myCommand.ExecuteNonQuery();
			myConnection.Close();
			return (int) parameterBankID.Value;
		}

        public void UpdateBank(int BankID, String BankCode, String BankName)
		{

			SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_UpdateBank", myConnection);
			myCommand.CommandType = CommandType.StoredProcedure;

			SqlParameter parameterBankID	= new SqlParameter("@BankID", SqlDbType.Int,4);
			parameterBankID.Value	 = BankID;
			myCommand.Parameters.Add(parameterBankID);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NVarChar, 3);
            parameterBankCode.Value = BankCode;
            myCommand.Parameters.Add(parameterBankCode);

			SqlParameter parameterBankName	= new SqlParameter("@BankName", SqlDbType.NVarChar,50);
			parameterBankName.Value	 = BankName;
			myCommand.Parameters.Add(parameterBankName);

            myConnection.Open();
			myCommand.ExecuteNonQuery();
			myConnection.Close();
            myConnection.Dispose();
		}

        //public SqlDataReader GetAllBanks()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBanks", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myConnection.Open();
        //    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return result;
        //}

        public DataTable GetAllBanks()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBanks", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetInwardIssuingBanks()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetInwardIssuingBanks", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myConnection.Open();
        //    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return result;
        //}

        public DataTable GetInwardIssuingBanks()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetInwardIssuingBanks", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetOutwardIssuingBanks()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetOutwardIssuingBanks", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myConnection.Open();
        //    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return result;
        //}

        public DataTable GetOutwardIssuingBanks()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetOutwardIssuingBanks", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetBankCodeByBankID(int BankID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBankCodeByBankID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterBankID = new SqlParameter("@BankID", SqlDbType.Int, 4);
        //    parameterBankID.Value = BankID;
        //    myCommand.Parameters.Add(parameterBankID);

        //    myConnection.Open();
        //    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return result;

        //}

        public DataTable GetBankCodeByBankID(int BankID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBankCodeByBankID", myConnection);
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

        //public SqlDataReader GetBankByBankID(int BankID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBankByBankID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterBankID = new SqlParameter("@BankID", SqlDbType.Int, 4);
        //    parameterBankID.Value = BankID;
        //    myCommand.Parameters.Add(parameterBankID);

        //    myConnection.Open();
        //    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return result;

        //}

        public DataTable GetBankByBankID(int BankID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBankByBankID", myConnection);
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
    }
}

