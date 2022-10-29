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
    public class ContestedBatchDB
    {
        //public SqlDataReader GetContestedSentByReceivedBatchID(Guid TransactionID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetContestedSent_By_ReceivedBatchID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
        //    parameterTransactionID.Value = TransactionID;
        //    myCommand.Parameters.Add(parameterTransactionID);

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader();
        //    return dr;
        //}

        public DataTable GetContestedSentByReceivedBatchID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetContestedSent_By_ReceivedBatchID", myConnection);
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

        public string GetBatchControlXMLContested(Guid TransactionID, string OriginBank)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must check your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_BatchControlXMLContested", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterOriginBank = new SqlParameter("@OriginBank", SqlDbType.NVarChar, 9);
            parameterOriginBank.Value = OriginBank;
            myCommand.Parameters.Add(parameterOriginBank);

            SqlParameter parameterBCRXML = new SqlParameter("@BCRXML", SqlDbType.VarChar, 8000);
            parameterBCRXML.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBCRXML);


            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader();
            myConnection.Close();

            return parameterBCRXML.Value.ToString();
        }

        public string GetBatchControlXMLContested_ForDebit(Guid TransactionID, string OriginBank)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must check your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_BatchControlXMLContested_ForDebit", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterOriginBank = new SqlParameter("@OriginBank", SqlDbType.NVarChar, 9);
            parameterOriginBank.Value = OriginBank;
            myCommand.Parameters.Add(parameterOriginBank);

            SqlParameter parameterBCRXML = new SqlParameter("@BCRXML", SqlDbType.VarChar, 8000);
            parameterBCRXML.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBCRXML);


            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader();
            myConnection.Close();

            return parameterBCRXML.Value.ToString();
        }

    }
}
