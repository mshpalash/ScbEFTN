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
    public class PrintCustomerAdviceDB
    {
        public DataTable GetBranchesByRoutingNo(string RoutingNoWebconfig,
                                                    int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBranchesByRoutingNo", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterRoutingNoWebconfig = new SqlParameter("@RoutingNoWebconfig", SqlDbType.NVarChar, 9);
            parameterRoutingNoWebconfig.Value = RoutingNoWebconfig;
            myAdapter.SelectCommand.Parameters.Add(parameterRoutingNoWebconfig);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchesByRoutingNoForRupali(string RoutingNoWebconfig,
                                                        int BranchID,
                                                        int DhakaBranch)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBranchesByRoutingNo_forRupaliAdv", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterRoutingNoWebconfig = new SqlParameter("@RoutingNoWebconfig", SqlDbType.NVarChar, 9);
            parameterRoutingNoWebconfig.Value = RoutingNoWebconfig;
            myAdapter.SelectCommand.Parameters.Add(parameterRoutingNoWebconfig);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterDhakaBranch = new SqlParameter("@DhakaBranch", SqlDbType.Int);
            parameterDhakaBranch.Value = DhakaBranch;
            myAdapter.SelectCommand.Parameters.Add(parameterDhakaBranch);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReceivedEDRForBranchAdvice(int BranchID,
                                                        int Credit,
                                                        string TransReceivedSettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ForBranchAdvice", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterCredit = new SqlParameter("@Credit", SqlDbType.Int);
            parameterCredit.Value = Credit;
            myAdapter.SelectCommand.Parameters.Add(parameterCredit);

            SqlParameter parameterTransReceivedSettlementDate = new SqlParameter("@TransReceivedSettlementDate", SqlDbType.VarChar);
            parameterTransReceivedSettlementDate.Value = TransReceivedSettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterTransReceivedSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReceivedEDRForBranchAdviceForRupali(int BranchID,
                                                        int Credit,
                                                        string TransReceivedSettlementDate,
                                                        int UserID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ForBranchAdviceForRupali", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterCredit = new SqlParameter("@Credit", SqlDbType.Int);
            parameterCredit.Value = Credit;
            myAdapter.SelectCommand.Parameters.Add(parameterCredit);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterTransReceivedSettlementDate = new SqlParameter("@TransReceivedSettlementDate", SqlDbType.VarChar);
            parameterTransReceivedSettlementDate.Value = TransReceivedSettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterTransReceivedSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetReceivedEDRForVoucher(  int BranchID,
                                                    int Credit,
                                                    string TransReceivedSettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ForVoucher", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterCredit = new SqlParameter("@Credit", SqlDbType.Int);
            parameterCredit.Value = Credit;
            myAdapter.SelectCommand.Parameters.Add(parameterCredit);

            SqlParameter parameterTransReceivedSettlementDate = new SqlParameter("@TransReceivedSettlementDate", SqlDbType.VarChar);
            parameterTransReceivedSettlementDate.Value = TransReceivedSettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterTransReceivedSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void InsertAdvicePrintTrack(int BranchID,
                                            DateTime AdvicePrintTime,
                                            DateTime SettlementJDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertAdvicePrintTrack", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterAdvicePrintTime = new SqlParameter("@AdvicePrintTime", SqlDbType.DateTime);
            parameterAdvicePrintTime.Value = AdvicePrintTime;
            myCommand.Parameters.Add(parameterAdvicePrintTime);

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementJDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable GetBranchAdvicePrintTrackByDateForNoneData(string SettlementDate, string BankCode)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBranchAdvicePrintTrackByDateForNoneData", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchAdvicePrintTrackByDateForData(string SettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBranchAdvicePrintTrackByDateForData", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchIDForTxnReceivedBySettlementDateForAdvice(string SettlementDate, int DhakaBranch)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranchID_TransactionReceivedBySettlementDate_ForAdvice", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterDhakaBranch = new SqlParameter("@DhakaBranch", SqlDbType.Int);
            parameterDhakaBranch.Value = DhakaBranch;
            myAdapter.SelectCommand.Parameters.Add(parameterDhakaBranch);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchIDForReturnSentBySettlementDateForAdvice(string SettlementDate, int DhakaBranch)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranchID_ReturnSentBySettlementDate_ForAdvice", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterDhakaBranch = new SqlParameter("@DhakaBranch", SqlDbType.Int);
            parameterDhakaBranch.Value = DhakaBranch;
            myAdapter.SelectCommand.Parameters.Add(parameterDhakaBranch);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchIDForReturnReceivedBySettlementDateForAdvice(string SettlementDate, int DhakaBranch)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranchID_ReturnReceivedBySettlementDate_ForAdvice", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterDhakaBranch = new SqlParameter("@DhakaBranch", SqlDbType.Int);
            parameterDhakaBranch.Value = DhakaBranch;
            myAdapter.SelectCommand.Parameters.Add(parameterDhakaBranch);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchIDForTXNSentBySettlementDateForAdvice(string SettlementDate, int DhakaBranch)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranchID_TransactionSentBySettlementDate_ForAdvice", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterDhakaBranch = new SqlParameter("@DhakaBranch", SqlDbType.Int);
            parameterDhakaBranch.Value = DhakaBranch;
            myAdapter.SelectCommand.Parameters.Add(parameterDhakaBranch);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReceivedEDRForBranchAdviceExcel(int BranchID,
                                                            int Credit,
                                                            string TransReceivedSettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ForBranchAdviceExcel", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterCredit = new SqlParameter("@Credit", SqlDbType.Int);
            parameterCredit.Value = Credit;
            myAdapter.SelectCommand.Parameters.Add(parameterCredit);

            SqlParameter parameterTransReceivedSettlementDate = new SqlParameter("@TransReceivedSettlementDate", SqlDbType.VarChar);
            parameterTransReceivedSettlementDate.Value = TransReceivedSettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterTransReceivedSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetTransactionSentForBranchAdviceForRupali(int BranchID,
                                                                int Credit,
                                                                string TransReceivedSettlementDate,
                                                                int UserID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetTransactionSent_ForBranchAdviceForRupali", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterCredit = new SqlParameter("@Credit", SqlDbType.Int);
            parameterCredit.Value = Credit;
            myAdapter.SelectCommand.Parameters.Add(parameterCredit);

            SqlParameter parameterTransReceivedSettlementDate = new SqlParameter("@TransReceivedSettlementDate", SqlDbType.VarChar);
            parameterTransReceivedSettlementDate.Value = TransReceivedSettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterTransReceivedSettlementDate);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReturnSentForBranchAdviceForRupali(int BranchID,
                                                                int Credit,
                                                                string TransReceivedSettlementDate,
                                                                int UserID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReturnSent_ForBranchAdviceForRupali", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterCredit = new SqlParameter("@Credit", SqlDbType.Int);
            parameterCredit.Value = Credit;
            myAdapter.SelectCommand.Parameters.Add(parameterCredit);

            SqlParameter parameterTransReceivedSettlementDate = new SqlParameter("@TransReceivedSettlementDate", SqlDbType.VarChar);
            parameterTransReceivedSettlementDate.Value = TransReceivedSettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterTransReceivedSettlementDate);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReturnReceivedForBranchAdviceForRupali(int BranchID,
                                                            int Credit,
                                                            string TransReceivedSettlementDate,
                                                            int UserID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedReturn_ForBranchAdviceForRupali", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterCredit = new SqlParameter("@Credit", SqlDbType.Int);
            parameterCredit.Value = Credit;
            myAdapter.SelectCommand.Parameters.Add(parameterCredit);

            SqlParameter parameterTransReceivedSettlementDate = new SqlParameter("@TransReceivedSettlementDate", SqlDbType.VarChar);
            parameterTransReceivedSettlementDate.Value = TransReceivedSettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterTransReceivedSettlementDate);

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
