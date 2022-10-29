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
    public class TrancateDatabase
    {
        public void TrancateEFTDB()
        {
            TrancateEFTDBForDishonor();
            TrancateEFTDBForNOC();
            TrancateEFTDBForReturn();
            TrancateEFTDBTransactionReceived();
            TrancateEFTDBTransactionSent();
        }

        private void TrancateEFTDBForDishonor()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommandForDishonor = new SqlCommand("EFT_ClearDatabaseForDishonor", myConnection);
            myCommandForDishonor.CommandType = CommandType.StoredProcedure;


            myConnection.Open();
            myCommandForDishonor.ExecuteReader(CommandBehavior.CloseConnection);
        }

        private void TrancateEFTDBForNOC()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommandForNOC = new SqlCommand("EFT_ClearDatabaseForNOC", myConnection);
            myCommandForNOC.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            myCommandForNOC.ExecuteReader(CommandBehavior.CloseConnection);
        }

        private void TrancateEFTDBForReturn()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommandForReturn = new SqlCommand("EFT_ClearDatabaseForReturn", myConnection);
            myCommandForReturn.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            myCommandForReturn.ExecuteReader(CommandBehavior.CloseConnection);
        }

        private void TrancateEFTDBTransactionReceived()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommandForTransactionReceived = new SqlCommand("EFT_ClearDatabaseForTransactionReceived", myConnection);
            myCommandForTransactionReceived.CommandType = CommandType.StoredProcedure;
            myConnection.Open();
            myCommandForTransactionReceived.ExecuteReader(CommandBehavior.CloseConnection);
        }

        private void TrancateEFTDBTransactionSent()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommandForTransactionSent = new SqlCommand("EFT_ClearDatabaseForTransactionSent", myConnection);
            myCommandForTransactionSent.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            myCommandForTransactionSent.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}
