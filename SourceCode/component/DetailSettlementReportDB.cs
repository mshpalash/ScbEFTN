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
    public class DetailSettlementReportDB
    {
        public DataTable GetReportForTransactionReceivedBySettlementDate(string SettlementDate, int BranchID)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_Rpt_TransactionReceivedBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRpt_TransactionReceivedBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionReceivedBySettlementDateWithCurrencyAndSession(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_Rpt_TransactionReceivedBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRpt_TransactionReceivedBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionReceivedBySettlementDateForCredit(string SettlementDate, int BranchID)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptCredit_TransactionReceivedBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptCredit_TransactionReceivedBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionReceivedBySettlementDateForCreditWithCurrencyAndSession(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptCredit_TransactionReceivedBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptCredit_TransactionReceivedBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionReceivedBySettlementDateForDebit(string SettlementDate, int BranchID)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptDebit_TransactionReceivedBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptDebit_TransactionReceivedBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionReceivedBySettlementDateForDebitWithCurrencyAndSession(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptDebit_TransactionReceivedBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptDebit_TransactionReceivedBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }


        public DataTable GetReportForDishonorSentBySettlementDate(string SettlementDate, int DepartmentID)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_Rpt_DishonorSentBySettlementDate";
            }
            else
            {
                spName = "EFT_DepartmentwiseRpt_DishonorSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);


            if (DepartmentID != 0)
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            }


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForDishonorSentBySettlementDateForCredit(string SettlementDate, int DepartmentID)
        {
            string spName = string.Empty;

            spName = "EFT_DepartmentwiseRptCredit_DishonorSentBySettlementDate";

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForDishonorSentBySettlementDateForDebit(string SettlementDate, int DepartmentID)
        {
            string spName = string.Empty;

            spName = "EFT_DepartmentwiseRptDebit_DishonorSentBySettlementDate";

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionSentBySettlementDate(string SettlementDate, int DepartmentID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_Rpt_TransactionSentBySettlementDate";
            }
            else
            {
                spName = "EFT_DepartmentwiseRpt_TransactionSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (DepartmentID != 0)
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            }


            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionSentBySettlementDateWithCurrencyAndSession(string SettlementDate, int DepartmentID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_Rpt_TransactionSentBySettlementDate";
            }
            else
            {
                spName = "EFT_DepartmentwiseRpt_TransactionSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (DepartmentID != 0)
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }


        public DataTable GetReportForTransactionSentBySettlementDateForCharges(string SettlementDate, int DepartmentID)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_Rpt_TransactionSentBySettlementDateForCharges";
            }
            else
            {
                spName = "EFT_DepartmentwiseRpt_TransactionSentBySettlementDateForCharges";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (DepartmentID != 0)
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportAckForTransactionSentBySettlementDate(string SettlementDate, int DepartmentID,string Currency,string SessionID)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_RptAck_TransactionSentBySettlementDate";
            }
            else
            {
                spName = "EFT_DepartmentwiseRptAck_TransactionSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSessionID = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSessionID.Value = SessionID;
            myAdpater.SelectCommand.Parameters.Add(parameterSessionID);

            if (DepartmentID != 0)
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportNonAckForTransactionSentBySettlementDate(string SettlementDate, int DepartmentID, string Currency, string SessionID)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_RptNonAck_TransactionSentBySettlementDate";
            }
            else
            {
                spName = "EFT_DepartmentwiseRptNonAck_TransactionSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSessionID = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSessionID.Value = SessionID;
            myAdpater.SelectCommand.Parameters.Add(parameterSessionID);



            if (DepartmentID != 0)
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionSentBySettlementDateForCredit(string SettlementDate, int DepartmentID, string currency, string session)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_RptCredit_TransactionSentBySettlementDate";
            }
            else
            {
                spName = "EFT_DepartmentwiseRptCredit_TransactionSentBySettlementDate";
            }
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);
            if (DepartmentID != 0)
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSessionID = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSessionID.Value = session;
            myAdpater.SelectCommand.Parameters.Add(parameterSessionID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionSentBySettlementDateForCreditWithCurrencyAndSession(string SettlementDate, int DepartmentID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_RptCredit_TransactionSentBySettlementDate";
            }
            else
            {
                spName = "EFT_DepartmentwiseRptCredit_TransactionSentBySettlementDate";
            }
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);
            if (DepartmentID != 0)
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportAckForTransactionSentBySettlementDateForCredit(string SettlementDate, int DepartmentID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_RptAckCredit_TransactionSentBySettlementDate";
            }
            else
            {
                spName = "EFT_DepartmentwiseRptAckCredit_TransactionSentBySettlementDate";
            }
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);
            if (DepartmentID != 0)
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionSentBySettlementDateForDebit(string SettlementDate, int DepartmentID)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_RptDebit_TransactionSentBySettlementDate";
            }
            else
            {
                spName = "EFT_DepartmentwiseRptDebit_TransactionSentBySettlementDate";
            }
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);
            if (DepartmentID != 0)
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionSentBySettlementDateForDebitWithCurrencyAndSession(string SettlementDate, int DepartmentID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_RptDebit_TransactionSentBySettlementDate";
            }
            else
            {
                spName = "EFT_DepartmentwiseRptDebit_TransactionSentBySettlementDate";
            }
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);
            if (DepartmentID != 0)
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }
        public DataTable GetReportAckForTransactionSentBySettlementDateForDebit(string SettlementDate, int DepartmentID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_RptAckDebit_TransactionSentBySettlementDate";
            }
            else
            {
                spName = "EFT_DepartmentwiseRptAckDebit_TransactionSentBySettlementDate";
            }
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);
            if (DepartmentID != 0)
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }


        public DataTable GetReportForTransactionReceivedBySettlementDateApprovedOnly(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_Rpt_TransactionReceivedBySettlementDate_ApprovedOnly";
            }
            else
            {
                spName = "EFT_BranchwiseRpt_TransactionReceivedBySettlementDate_ApprovedOnly";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }


            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionReceviedBySettlementDateApprovedOnlyForCredit(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptCredit_TransactionReceivedBySettlementDate_ApprovedOnly";
            }
            else
            {
                spName = "EFT_BranchwiseRptCredit_TransactionReceivedBySettlementDate_ApprovedOnly";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionReceivedBySettlementDateApprovedOnlyForDebit(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptDebit_TransactionReceivedBySettlementDate_ApprovedOnly";
            }
            else
            {
                spName = "EFT_BranchwiseRptDebit_TransactionReceivedBySettlementDate_ApprovedOnly";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForReturnSentBySettlementDate(string SettlementDate, int BranchID)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_Rpt_ReturnSentBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRpt_ReturnSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForReturnSentBySettlementDateWithCurrencyAndSession(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_Rpt_ReturnSentBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRpt_ReturnSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportAckForReturnSentBySettlementDate(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptAck_ReturnSentBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptAck_ReturnSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }
        public DataTable GetReportNonAckForReturnSentBySettlementDate(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptNonAck_ReturnSentBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptNonAck_ReturnSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForReturnSentBySettlementDateForCredit(string SettlementDate, int BranchID)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptCredit_ReturnSentBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptCredit_ReturnSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForReturnSentBySettlementDateForCreditWithCurrencyAndSession(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptCredit_ReturnSentBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptCredit_ReturnSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }
        public DataTable GetReportAckForReturnSentBySettlementDateForCredit(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptAckCredit_ReturnSentBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptAckCredit_ReturnSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForReturnSentBySettlementDateForDebit(string SettlementDate, int BranchID)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptDebit_ReturnSentBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptDebit_ReturnSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            //if (BranchID != 0)
            //{
            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            //}

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForReturnSentBySettlementDateForDebitWithCurrencyAndSession(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptDebit_ReturnSentBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptDebit_ReturnSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            //if (BranchID != 0)
            //{
            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            //}

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }
        public DataTable GetReportAckForReturnSentBySettlementDateForDebit(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_RptAckDebit_ReturnSentBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptAckDebit_ReturnSentBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            //if (BranchID != 0)
            //{
            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            //}

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForReturnReceivedBySettlementDate(string SettlementDate, int BranchID)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_Rpt_ReturnReceivedBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRpt_ReturnReceivedBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForReturnReceivedBySettlementDateWithCurrencyAndSession(string SettlementDate, int BranchID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_Rpt_ReturnReceivedBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRpt_ReturnReceivedBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForReturnReceivedBySettlementDateForCredit(string SettlementDate, int DepartmentID)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_RptCredit_ReturnReceivedBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptCredit_ReturnReceivedBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (DepartmentID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForReturnReceivedBySettlementDateForCreditWithCurrencyAndSession(string SettlementDate, int DepartmentID, string Currency, string Session)
        {
            string spName = string.Empty;

            if (DepartmentID == 0)
            {
                spName = "EFT_RptCredit_ReturnReceivedBySettlementDate";
            }
            else
            {
                spName = "EFT_BranchwiseRptCredit_ReturnReceivedBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (DepartmentID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = DepartmentID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForReturnReceivedBySettlementDateForDebit(string SettlementDate, int DepartmentID)
        {
            string spName = string.Empty;

            //if (DepartmentID == 0)
            //{
            //    spName = "EFT_RptDebit_ReturnReceivedBySettlementDate";
            //}
            //else
            //{
            spName = "EFT_BranchwiseRptDebit_ReturnReceivedBySettlementDate";
            //}

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            //if (DepartmentID != 0)
            //{
            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = DepartmentID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            //}

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForReturnReceivedBySettlementDateForDebitWithCurrencyAndSession(string SettlementDate, int DepartmentID, string Currency, string Session)
        {
            string spName = string.Empty;

            //if (DepartmentID == 0)
            //{
            //    spName = "EFT_RptDebit_ReturnReceivedBySettlementDate";
            //}
            //else
            //{
            spName = "EFT_BranchwiseRptDebit_ReturnReceivedBySettlementDate";
            //}

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            //if (DepartmentID != 0)
            //{
            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = DepartmentID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            //}

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }


        public DataTable GetReportForNOCReceivedBySettlementDate(string SettlementDate, int DepartmentID)
        {
            string spName = string.Empty;

            //if (DepartmentID == 0)
            //{
            //    spName = "EFT_Rpt_NOCReceivedBySettlementDate";
            //}
            //else
            //{
            spName = "EFT_DepartmentwiseRpt_NOCReceivedBySettlementDate";
            //}
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            //if (DepartmentID != 0)
            //{
            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            //}

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForNOCReceivedBySettlementDateForCredit(string SettlementDate, int DepartmentID)
        {
            string spName = string.Empty;

            //if (DepartmentID == 0)
            //{
            //    spName = "EFT_RptCredit_NOCReceivedBySettlementDate";
            //}
            //else
            //{
            spName = "EFT_DepartmentwiseRptCredit_NOCReceivedBySettlementDate";
            //}
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;


            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            //if (DepartmentID != 0)
            //{
            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            //}

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForNOCReceivedBySettlementDateForDebit(string SettlementDate, int DepartmentID)
        {

            string spName = string.Empty;

            //if (DepartmentID == 0)
            //{
            //    spName = "EFT_RptDebit_NOCReceivedBySettlementDate";
            //}
            //else
            //{
            spName = "EFT_DepartmentwiseRptDebit_NOCReceivedBySettlementDate";
            //}
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            //if (DepartmentID != 0)
            //{
            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdpater.SelectCommand.Parameters.Add(parameterDepartmentID);
            //}


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetRNOCSentBySettlementDate(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_RNOCSent_By_SettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetDepartmentwiseRNOCSentCredit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DepartmentwiseRptCredit_RNOCSent_By_SettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetDepartmentwiseRNOCSentDebit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DepartmentwiseRptDebit_RNOCSent_By_SettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForNOCSentBySettlementDate(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_Rpt_NOCSentBySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }
        public DataTable GetReportAckForNOCSentBySettlementDate(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptAck_NOCSentBySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }
        public DataTable GetReportNonAckForNOCSentBySettlementDate(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptNonAck_NOCSentBySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForNOCSentBySettlementDateForCredit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptCredit_NOCSentBySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportAckForNOCSentBySettlementDateForCredit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptAckCredit_NOCSentBySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForNOCSentBySettlementDateForDebit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptDebit_NOCSentBySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportAckForNOCSentBySettlementDateForDebit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptAckDebit_NOCSentBySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionReceivedBySettlementDateApprovedByDefault(string SettlementDate, string Currency, string Session)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_Rpt_TransactionReceivedBySettlementDate_ApprovedByDefault", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionReceivedBySettlementDateApprovedByDefaultForCredit(string SettlementDate, string Currency, string Session)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptCredit_TransactionReceivedBySettlementDate_ApprovedByDefault", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionReceivedBySettlementDateApprovedByDefaultForDebit(string SettlementDate, string Currency, string Session)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptDebit_TransactionReceivedBySettlementDate_ApprovedByDefault", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);
            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@SessionID", SqlDbType.VarChar, 3);
            parameterSession.Value = Session;
            myAdpater.SelectCommand.Parameters.Add(parameterSession);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetBranchForTransactionReceivedBySettlementDate(string SettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_TransactionReceivedBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForTransactionReceivedBySettlementDateForCredit(string SettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranchCredit_TransactionReceivedBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForTransactionReceivedBySettlementDateForDebit(string SettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranchDebit_TransactionReceivedBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForTransactionReceivedBySettlementDate_ApprovedOnly(string SettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_TransactionReceivedBySettlementDate_ApprovedOnly", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForTransactionReceivedBySettlementDate_ApprovedOnlyForCredit(string SettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranchCredit_TransactionReceivedBySettlementDate_ApprovedOnly", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForTransactionReceivedBySettlementDate_ApprovedOnlyForDebit(string SettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranchDebit_TransactionReceivedBySettlementDate_ApprovedOnly", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReceivedContestedBySettlementDate(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DepartmentwiseRpt_ReceivedContestedBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReceivedCreditContestedBySettlementDate(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DepartmentwiseRptCredit_ReceivedContestedBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReceivedDebitContestedBySettlementDate(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DepartmentwiseRptDebit_ReceivedContestedBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportInwardDishonorBySettlementDate(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_Rpt_GetReceivedDishonor_BySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportCreditInwardDishonorBySettlementDate(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptCredit_GetReceivedDishonor_BySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDebitInwardDishonorBySettlementDate(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptDebit_GetReceivedDishonor_BySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportInwardRNOCBySettlementDate(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_Rpt_GetReceivedRNOCBySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportCreditInwardRNOCBySettlementDate(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptCredit_GetReceivedRNOCBySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDebitInwardRNOCBySettlementDate(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptDebit_GetReceivedRNOCBySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportSentContestedBySettlementDate(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_Rpt_GetContestedSent_BySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportCreditSentContestedBySettlementDate(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptCredit_GetContestedSent_BySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDebitSentContestedBySettlementDate(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_RptDebit_GetContestedSent_BySettlementDate", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdpater.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        // For Departmentwise Report PDF only
        // For Departmentwise Report PDF only
        public DataTable GetDepartmentForTransactionSentBySettlementDate(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_TransactionSentBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetDepartmentForTransactionSentBySettlementDateForDebit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_TransactionSentBySettlementDateForDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetDepartmentForTransactionSentBySettlementDateForCredit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_TransactionSentBySettlementDateForCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReportDepartmentwiseTransactionSent(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_Departmentwise_TransactionSent", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetDepartmentForInwardReturnBySettlementDate(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_InwardReturnBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReportDepartmentwiseInwardReturn(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_Departmentwise_ReturnReceived", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetDepartmentForInwardNOCBySettlementDate(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_InwardNOCBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReportDepartmentwiseInwardNOC(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_Departmentwise_NOCReceived", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetDepartmentForOutwardRNOCBySettlementDate(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_OutwardRNOCBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReportDepartmentwiseOutwardRNOC(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_Departmentwise_RNOCSent", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetDepartmentForOutwardDishonorBySettlementDate(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_OutwardDishonorBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReportDepartmentwiseOutwardDishonor(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_Departmentwise_DishonorSent", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetDepartmentForInwradContestedBySettlementDate(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_InwardCntestedBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReportDepartmentwiseInwradContested(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_Departmentwise_ReceivedContested", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }


        // For Branchwise Report PDF only
        public DataTable GetBranchForReturnSentBySettlementDate(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_ReturnSentBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReportBranchwiseReturnSent(string SettlementDate, int BranchID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRpt_ReturnSentBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetBranchForInwardDishonortBySettlementDate(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_InwardDishonorBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReportBranchwiseInwardDishonor(string SettlementDate, int BranchID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRpt_InwardDishonorBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetBranchForContestedSentBySettlementDate(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_ContestedSentBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReportBranchwiseContestedSent(string SettlementDate, int BranchID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRpt_ContestedSentBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetBranchForNOCSentBySettlementDate(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_NOCSentBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReportBranchwiseNOCSent(string SettlementDate, int BranchID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRpt_NOCSentBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetBranchInwardRNOCBySettlementDate(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_InwardRNOCBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReportBranchwiseInwardRNOC(string SettlementDate, int BranchID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRpt_InwardRNOCBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }


        // For Departmentwise Credit Debit Report PDF only
        public DataTable GetReportDepartmentwiseTransactionSentCredit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptCredit_Departmentwise_TransactionSent", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseTransactionSentDebit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptDebit_Departmentwise_TransactionSent", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseInwardReturnCredit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptCredit_Departmentwise_ReturnReceived", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseInwardReturnDebit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptDebit_Departmentwise_ReturnReceived", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseInwardNOCCredit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptCredit_Departmentwise_NOCReceived", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseInwardNOCDebit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptDebit_Departmentwise_NOCReceived", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseRNOCSentCredit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptCredit_Departmentwise_RNOCSent", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseRNOCSentDebit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptDebit_Departmentwise_RNOCSent", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseDishonorSentCredit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptCredit_Departmentwise_DishonorSent", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseDishonorSentDebit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptDebit_Departmentwise_DishonorSent", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseInwardContestedCredit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptCreate_Departmentwise_ReceivedContested", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseInwardContestedDebit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptDebit_Departmentwise_ReceivedContested", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }


        // For Branchwise Credit Debit Report PDF only
        public DataTable GetReportBranchwiseReturnSentCredit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRptCredit_ReturnSentBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportBranchwiseReturnSentDebit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRptDebit_ReturnSentBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportBranchwiseNOCSentCredit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRptCredit_NOCSentBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportBranchwiseNOCSentDebit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRptDebit_NOCSentBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportBranchwiseInwardRNOCCredit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRptCredit_InwardRNOCBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportBranchwiseInwardRNOCDebit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRptDebit_InwardRNOCBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportBranchwiseInwardDishonorCredit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRptCredit_InwardDishonorBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportBranchwiseInwardDishonorDebit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRptDebit_InwardDishonorBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportBranchwiseContestedSentCredit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRptCredit_ContestedSentBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportBranchwiseeContestedSentDebit(string SettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_BranchwiseRptDebit_ContestedSentBySettlementDate", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }


        // For Distinct Department For Credit Debit Report PDF only
        public DataTable GetDepartmentForInwardReturnBySettlementDateForCredit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_InwardReturnBySettlementDateCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetDepartmentForInwardReturnBySettlementDateForDebit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_InwardReturnBySettlementDateDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetDepartmentForInwardNOCBySettlementDateForCredit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_InwardNOCBySettlementDateCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetDepartmentForInwardNOCBySettlementDateForDebit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_InwardNOCBySettlementDateDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetDepartmentForOutwardRNOCBySettlementDateForCredit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_OutwardRNOCBySettlementDateCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetDepartmentForOutwardRNOCBySettlementDateForDebit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_OutwardRNOCBySettlementDateDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetDepartmentForOutwardDishonorBySettlementDateForCredit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_OutwardDishonorBySettlementDateCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetDepartmentForOutwardDishonorBySettlementDateForDebit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_OutwardDishonorBySettlementDateDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetDepartmentForInwradContestedBySettlementDateForCredit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_InwardCntestedBySettlementDateCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetDepartmentForInwradContestedBySettlementDateForDebit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctDepartment_InwardCntestedBySettlementDateDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        // For Distinct Branch For Credit Debit Report PDF only
        public DataTable GetBranchForReturnSentBySettlementDateForCredit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_ReturnSentBySettlementDateCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForReturnSentBySettlementDateForDebit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_ReturnSentBySettlementDateDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForNOCSentBySettlementDateForCredit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_NOCSentBySettlementDateCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForNOCSentBySettlementDateForDebit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_NOCSentBySettlementDateDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForInwardRNOCBySettlementDateForCredit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_InwardRNOCBySettlementDateCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForInwardRNOCBySettlementDateForDebit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_InwardRNOCBySettlementDateDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForInwardDishonorBySettlementDateForCredit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_InwardDishonorBySettlementDateCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForInwardDishonorBySettlementDateForDebit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_InwardDishonorBySettlementDateDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForContestedSentBySettlementDateForCredit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_ContestedSentBySettlementDateCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBranchForContestedSentBySettlementDateForDebit(string SettlementDate)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DistinctBranch_ContestedSentBySettlementDateDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetTransactionReceivedBySettlementDate_ForBachBranch(string SettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_TransactionReceivedBySettlementDate_ForBachBranch", myConnection);
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

        public DataTable GetTransactionReceivedBySettlementDate_ForNoneBachBranch(string SettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_TransactionReceivedBySettlementDate_ForNoneBachBranch", myConnection);
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
    }
}
