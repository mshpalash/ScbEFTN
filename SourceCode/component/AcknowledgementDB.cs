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
    public class AcknowledgementDB
    {
        public void ImportAckTransactionSent(string AckXmlName)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_ImportAckTransactionSent", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterAckXmlName = new SqlParameter("@AckXmlName", SqlDbType.NVarChar, 200);
            parameterAckXmlName.Value = AckXmlName;
            myCommand.Parameters.Add(parameterAckXmlName);


            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myConnection.Dispose();
        }

        public void ImportAckReturnSent(string AckXmlName, int session, DateTime settlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_ImportAckReturnSent", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterAckXmlName = new SqlParameter("@AckXmlName", SqlDbType.NVarChar, 200);
            parameterAckXmlName.Value = AckXmlName;
            myCommand.Parameters.Add(parameterAckXmlName);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.TinyInt);
            paramSession.Value = session;
            myCommand.Parameters.Add(paramSession);


            /***    Added new as we got the ACK file with settlement date from BB    ***/
            SqlParameter paramSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.DateTime);
            paramSettlementDate.Value = settlementDate;
            myCommand.Parameters.Add(paramSettlementDate);

            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myConnection.Dispose();
        }

        public void ImportAckNOCSent(string AckXmlName, int session)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_ImportAckNOCSent", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterAckXmlName = new SqlParameter("@AckXmlName", SqlDbType.NVarChar, 200);
            parameterAckXmlName.Value = AckXmlName;
            myCommand.Parameters.Add(parameterAckXmlName);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.TinyInt);
            paramSession.Value = session;
            myCommand.Parameters.Add(paramSession);


            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myConnection.Dispose();
        }

        //Added New on BACH II upgrade on April 2018
        public void ImportAckDishonorSent(string AckXmlName, int session)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_ImportAckDishonorSent", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterAckXmlName = new SqlParameter("@AckXmlName", SqlDbType.NVarChar, 200);
            parameterAckXmlName.Value = AckXmlName;
            myCommand.Parameters.Add(parameterAckXmlName);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.TinyInt);
            paramSession.Value = session;
            myCommand.Parameters.Add(paramSession);

            //SqlParameter paramSettlementDate = new SqlParameter("@SettlementJDate", SqlDbType.DateTime);
            //paramSettlementDate.Value = settlementJDate;
            //myCommand.Parameters.Add(paramSettlementDate);

            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myConnection.Dispose();
        }
    }
}
