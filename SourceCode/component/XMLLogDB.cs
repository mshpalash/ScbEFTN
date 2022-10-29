using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class XMLLogDB
    {
        public DataTable GetXMLLogOfCurrentDate()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetXMLLogOfCurrentDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public void UpdateXMLogOfCurrentDate(DateTime lastXmlCreationTime)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateXMLogOfCurrentDate", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterLastXmlCreationTime = new SqlParameter("@lastXmlCreationTime", SqlDbType.DateTime);
            parameterLastXmlCreationTime.Value = lastXmlCreationTime;
            myCommand.Parameters.Add(parameterLastXmlCreationTime);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();
        }



    }
}