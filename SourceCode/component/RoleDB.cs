using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace FloraSoft
{
    public class RoleDB
    {
        //public SqlDataReader GetAllRoles()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetRoles", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myConnection.Open();
        //    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return result;
        //}

        public DataTable GetAllRoles()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetRoles", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}

