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
    public class TransactionReceiveForUCBCSVDB
    {
        public DataTable GetEFT_UCB_Credit_TransactionReceivedBySettlementDate(string SettlementJDate, int UserID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            string spName = "EFT_UCB_Credit_TransactionReceivedBySettlementDate";
            SqlDataAdapter myAdapter = new SqlDataAdapter(spName, myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetEFT_UCB_Debit_TransactionReceivedBySettlementDate(string SettlementJDate, int UserID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            string spName = "EFT_UCB_Debit_TransactionReceivedBySettlementDate";
            SqlDataAdapter myAdapter = new SqlDataAdapter(spName, myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}
