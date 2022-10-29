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

namespace EFTN.component
{
    public class RNOCBatchDB
    {
        //public SqlDataReader GetRNOCBatchData()
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBatches_For_RNOCSent", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetRNOCBatchData()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_For_RNOCSent", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetRNOCBySentBatchID(Guid TransactionID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetRNOCSent_By_SentBatchID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
        //    parameterTransactionID.Value = TransactionID;
        //    myCommand.Parameters.Add(parameterTransactionID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetRNOCBySentBatchID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetRNOCSent_By_SentBatchID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public string GetBatchControlXMLRNOC(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_BatchControlXMLRNOC", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterOriginBank = new SqlParameter("@OriginBank", SqlDbType.VarChar, 9);
            parameterOriginBank.Value = ConfigurationManager.AppSettings["OriginBank"];
            myCommand.Parameters.Add(parameterOriginBank);

            SqlParameter parameterBCRXML = new SqlParameter("@BCRXML", SqlDbType.VarChar, 8000);
            parameterBCRXML.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBCRXML);


            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();

            return parameterBCRXML.Value.ToString();

        }
    }
}
