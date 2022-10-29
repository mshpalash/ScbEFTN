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
    public class ReceivedRNOCDB
    {
        //public SqlDataReader GetReceivedRNOC()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetReceivedRNOC", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetReceivedRNOC()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedRNOC", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void InsertReceivedRNOC(string NOCTraceNumber,
                                       string RefusedCode,
                                       string CorrectedData,
                                       string CORTraceSeqNum,
                                       DateTime SettlementJDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertReceivedRNOC", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterRNOCID = new SqlParameter("@RNOCID", SqlDbType.UniqueIdentifier);
            parameterRNOCID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRNOCID);

            SqlParameter parameterNOCTraceNumber = new SqlParameter("@NOCTraceNumber", SqlDbType.NVarChar, 15);
            parameterNOCTraceNumber.Value = NOCTraceNumber;
            myCommand.Parameters.Add(parameterNOCTraceNumber);

            SqlParameter parameterRefusedCode = new SqlParameter("@RefusedCode", SqlDbType.NVarChar, 3);
            parameterRefusedCode.Value = RefusedCode;
            myCommand.Parameters.Add(parameterRefusedCode);

            SqlParameter parameterCorrectedData = new SqlParameter("@CorrectedData", SqlDbType.NVarChar, 30);
            parameterCorrectedData.Value = CorrectedData;
            myCommand.Parameters.Add(parameterCorrectedData);

            SqlParameter parameterCORTraceSeqNum = new SqlParameter("@CORTraceSeqNum", SqlDbType.NVarChar, 7);
            parameterCORTraceSeqNum.Value = CORTraceSeqNum;
            myCommand.Parameters.Add(parameterCORTraceSeqNum);

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementJDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
        }
    }
}
