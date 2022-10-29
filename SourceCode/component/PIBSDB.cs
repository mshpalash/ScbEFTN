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
using System.Data.OracleClient;

namespace EFTN.component
{
    public class PIBSDB
    {

        public DataTable GetRecievedEDRForAdmin()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ForAdminForPubali", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetOutwardReturnFromPIBS()
        {
            string oracleConnectionString = ConfigurationManager.AppSettings["PIBSConnectionString"];
            OracleConnection OraCon = new OracleConnection(oracleConnectionString);

            string command = "SELECT * FROM TBEFTN_SENT_RETURN WHERE rownum <= 30 and SentToFlora = 0 ORDER BY rownum";
            OracleCommand cmd = new OracleCommand(command, OraCon);
            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            OraCon.Close();
            OraCon.Dispose();
            return dt;
        }

        public DataTable GetApprovedInwardTransactionFromPIBS()
        {
            string oracleConnectionString = ConfigurationManager.AppSettings["PIBSConnectionString"];
            OracleConnection OraCon = new OracleConnection(oracleConnectionString);

            string command = "SELECT TRANSACTIONID EDRID, STATUSCODE STATUSID FROM TBEFTN_INWARD_TRN WHERE SentToFlora = 0 and STATUSCODE = 6";
            OracleCommand cmd = new OracleCommand(command, OraCon);
            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            OraCon.Close();
            OraCon.Dispose();
            return dt;
        }

        public int UpdateApprovedInwardTransactionPulledFromPIBS()
        {
            string oracleConnectionString = ConfigurationManager.AppSettings["PIBSConnectionString"];
            OracleConnection conn = new OracleConnection(oracleConnectionString);

            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = "UPDATE TBEFTN_INWARD_TRN SET SentToFlora = 1 WHERE SentToFlora = 0";
            int UpdatedResult = cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
            return UpdatedResult;
        }

        public int UpdateOutwardReturnPulledFromPIBS(string oracleConnectionString, string EDRID)
        {
            OracleConnection conn = new OracleConnection(oracleConnectionString);

            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = "UPDATE TBEFTN_SENT_RETURN SET SentToFlora = 1 WHERE EDRID = '" + EDRID + "'";
            int UpdatedResult = cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
            return UpdatedResult;
        }

        public void InsertImportedReturnSent_fromPIBS(Guid EDRID,
                                                        string ReturnCode,
                                                        string ReturnReasonDesc)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_ImportReturnSent_fromPIBS", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterReturnCode = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 3);
            parameterReturnCode.Value = ReturnCode;
            myCommand.Parameters.Add(parameterReturnCode);

            SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            parameterReturnID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterReturnID);

            SqlParameter parameterReturnReasonDesc = new SqlParameter("@ReturnReasonDesc", SqlDbType.NVarChar, 6);
            parameterReturnReasonDesc.Value = ReturnReasonDesc;
            myCommand.Parameters.Add(parameterReturnReasonDesc);


            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void UpdateApprovedReceivedEDRFromPIBS(int StatusID,
                                                                Guid EDRID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_ApprovedReceivedEDR_FromPIBS", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
            myConnection.Close();
            myConnection.Dispose();
        }
    }
}
