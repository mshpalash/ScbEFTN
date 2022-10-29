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
    public class CBSMissMatchXLDB
    {
        public void InsertMismatchXLInwardReturn_ForSCB(string XLSettlementJDate,string XLTraceNumber,string XLAccountNo,double XLAmount)

        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertMismatchXLInwardReturn_ForSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterXLSettlementJDate = new SqlParameter("@XLSettlementJDate", SqlDbType.VarChar);
            parameterXLSettlementJDate.Value = XLSettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterXLSettlementJDate);

            SqlParameter parameterXLTraceNumber = new SqlParameter("@XLTraceNumber", SqlDbType.NVarChar, 15);
            parameterXLTraceNumber.Value = XLTraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterXLTraceNumber);

            SqlParameter parameterXLAccountNo = new SqlParameter("@XLAccountNo", SqlDbType.NVarChar, 17);
            parameterXLAccountNo.Value = XLAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterXLAccountNo);

            SqlParameter parameterXLAmount = new SqlParameter("@XLAmount", SqlDbType.Money);
            parameterXLAmount.Value = XLAmount;
            myAdapter.SelectCommand.Parameters.Add(parameterXLAmount);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();

            //myConnection.Open();
            //SqlDataReader dr = myAdapter.SelectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            //return dr;
        }
        
        public void InsertMismatchXLOutward_ForSCB(string XLEntryDate,string XLTraceNumber,string XLAccountNo,double XLAmount)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertMismatchXLOutward_ForSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterXLEntryDate = new SqlParameter("@XLEntryDate", SqlDbType.VarChar);
            parameterXLEntryDate.Value = XLEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterXLEntryDate);

            SqlParameter parameterXLTraceNumber = new SqlParameter("@XLTraceNumber", SqlDbType.NVarChar, 15);
            parameterXLTraceNumber.Value = XLTraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterXLTraceNumber);

            SqlParameter parameterXLAccountNo = new SqlParameter("@XLAccountNo", SqlDbType.NVarChar, 17);
            parameterXLAccountNo.Value = XLAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterXLAccountNo);

            SqlParameter parameterXLAmount = new SqlParameter("@XLAmount", SqlDbType.Money);
            parameterXLAmount.Value = XLAmount;
            myAdapter.SelectCommand.Parameters.Add(parameterXLAmount);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();

            //myConnection.Open();
            //SqlDataReader dr = myAdapter.SelectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            //return dr;
        }

        public void InsertMismatchXLInward_ForSCB(string XLSettlementJDate,string XLTraceNumber,string XLAccountNo,double XLAmount)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertMismatchXLInward_ForSCB", myConnection);
            //SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertMismatchXLInward_ForSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterXLSettlementJDate = new SqlParameter("@XLSettlementJDate", SqlDbType.VarChar);
            parameterXLSettlementJDate.Value = XLSettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterXLSettlementJDate);

            SqlParameter parameterXLTraceNumber = new SqlParameter("@XLTraceNumber", SqlDbType.NVarChar, 15);
            parameterXLTraceNumber.Value = XLTraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterXLTraceNumber);

            SqlParameter parameterXLAccountNo = new SqlParameter("@XLAccountNo", SqlDbType.NVarChar, 17);
            parameterXLAccountNo.Value = XLAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterXLAccountNo);

            SqlParameter parameterXLAmount = new SqlParameter("@XLAmount", SqlDbType.Money);
            parameterXLAmount.Value = XLAmount;
            myAdapter.SelectCommand.Parameters.Add(parameterXLAmount);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();

            //myConnection.Open();
            //SqlDataReader dr = myAdapter.SelectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            //return dr;
        }

        public DataTable GetMismatchReportTransactionSentByEntryDateTransactionSent(string EntryDateTransactionSent)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_Rpt_TransactionSent_SCBMM", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateTransactionSent = new SqlParameter("@EntryDateTransactionSent", SqlDbType.VarChar, 8);
            parameterEntryDateTransactionSent.Value = EntryDateTransactionSent;
            myAdpater.SelectCommand.Parameters.Add(parameterEntryDateTransactionSent);
           

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetMismatchReportTransactionReceivedBySettlementJDate(string SettlementJDate)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_Rpt_TransactionReceived_SCBMM", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.VarChar, 8);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementJDate);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetMismatchReportInwardReturnBySettlementJDate(string SettlementJDate)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_Rpt_InwardReturn_SCBMM", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.VarChar, 8);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementJDate);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable InsertMismatchXLOutwardBatchwise_ForSCB(string XLBatchNumber,
                                                                    int XLTotalItem,
                                                                    double XLTotalAmount,
                                                                    string XLAccountNo)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertMismatchXLOutwardBatchwise_ForSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterXLBatchNumber = new SqlParameter("@XLBatchNumber", SqlDbType.VarChar);
            parameterXLBatchNumber.Value = XLBatchNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterXLBatchNumber);

            SqlParameter parameterXLTotalItem = new SqlParameter("@XLTotalItem", SqlDbType.Int);
            parameterXLTotalItem.Value = XLTotalItem;
            myAdapter.SelectCommand.Parameters.Add(parameterXLTotalItem);

            SqlParameter parameterXLTotalAmount = new SqlParameter("@XLTotalAmount", SqlDbType.Money);
            parameterXLTotalAmount.Value = XLTotalAmount;
            myAdapter.SelectCommand.Parameters.Add(parameterXLTotalAmount);

            SqlParameter parameterXLAccountNo = new SqlParameter("@XLAccountNo", SqlDbType.NVarChar, 17);
            parameterXLAccountNo.Value = XLAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterXLAccountNo);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBatches_ForExcelMismatch(string EffectiveEntryDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatches_ForExcelMismatch", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void SCBClearMisMatchXLDataTable()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_SCBClearMisMatchXLDataTable", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable SCBMisMatch_GetTransactionSent()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_SCBMisMatch_GetTransactionSent", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable SCBMisMatch_GetTransactionReceived()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_SCBMisMatch_GetTransactionReceived", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable SCBMisMatch_GetInwardReturn()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_SCBMisMatch_GetInwardReturn", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }
    }
}
