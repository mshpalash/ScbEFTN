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
    public class ClientDB
    {
        public DataTable EFTRptSenderAccountNumberWiseForTransactionSent(
                                                                         string SettlementDate,
                                                                         string SenderAccountNumber
                                                                        )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_TransactionSent", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTRptSenderAccountNumberWiseForTransactionSentCredit(
                                                                                string SettlementDate,
                                                                                string SenderAccountNumber
                                                                              )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_TransactionSentCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTRptSenderAccountNumberWiseForTransactionSentDebit(
                                                                              string SettlementDate,
                                                                              string SenderAccountNumber
                                                                             )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_TransactionSentDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTRptSenderAccountNumberWiseForReturnReceived(
                                                                        string SettlementDate,
                                                                        string SenderAccountNumber
                                                                       )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_ReturnReceived", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTRptSenderAccountNumberWiseForReturnReceivedCredit(
                                                                                string SettlementDate,
                                                                                string SenderAccountNumber
                                                                             )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_ReturnReceivedCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTRptSenderAccountNumberWiseForReturnReceivedDebit(
                                                                                string SettlementDate,
                                                                                string SenderAccountNumber
                                                                             )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_ReturnReceivedDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTRptSenderAccountNumberWiseForNOCReceivedCredit(
                                                                                string SettlementDate,
                                                                                string SenderAccountNumber
                                                                             )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_NOCReceivedCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTRptSenderAccountNumberWiseForNOCReceivedDebit(
                                                                                string SettlementDate,
                                                                                string SenderAccountNumber
                                                                             )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_NOCReceivedDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTRptSenderAccountNumberWiseForTransactionReceivedCredit(
                                                                                string SettlementDate,
                                                                                string SenderAccountNumber
                                                                             )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_TransactionReceivedCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTRptSenderAccountNumberWiseForTransactionReceivedDebit(
                                                                                string SettlementDate,
                                                                                string SenderAccountNumber
                                                                             )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_TransactionReceivedDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTRptSenderAccountNumberWiseForNOCSentCredit(
                                                                        string SettlementDate,
                                                                        string SenderAccountNumber
                                                                      )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_NOCSentCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTRptSenderAccountNumberWiseForNOCSentDebit(
                                                                        string SettlementDate,
                                                                        string SenderAccountNumber
                                                                     )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_NOCSentDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTRptSenderAccountNumberWiseForReturnSentCredit(
                                                                        string SettlementDate,
                                                                        string SenderAccountNumber
                                                                      )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_ReturnSentCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable EFTRptSenderAccountNumberWiseForReturnSentDebit(
                                                                        string SettlementDate,
                                                                        string SenderAccountNumber
                                                                     )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_SenderAccountNumberWise_For_ReturnSentDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterSenderAccountNumber = new SqlParameter("@SenderAccountNumber", SqlDbType.NVarChar, 17);
            parameterSenderAccountNumber.Value = SenderAccountNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccountNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}
