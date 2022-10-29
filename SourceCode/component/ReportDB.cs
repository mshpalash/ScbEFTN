using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class ReportDB
    {
        public DataTable GetStatusReport(int Day, int Month, int Year, string currency, string session)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetStatusReport", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();


            connection.Open();


            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            paramSession.Value = session;
            myAdapter.SelectCommand.Parameters.Add(paramSession);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }

        public DataTable GetSettlementReport(int Day, int Month, int Year, string currency, string session)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetSettlementReport", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 3600;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;
            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            paramSession.Value = session;
            myAdapter.SelectCommand.Parameters.Add(paramSession);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public DataTable GetBranchWiseSettlementReport(int Day, int Month, int Year, string BankCode)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetSettlementReportBranchwise", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();


            connection.Open();


            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public DataTable GetReturnReceivedByTransactionSentEntryDate(string EntryDateTransactionSent, int DepartmentID, string currency, string session)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_ReturnReceivedByTransactionSentEntryDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateTransactionSent = new SqlParameter("@EntryDateTransactionSent", SqlDbType.VarChar);
            parameterEntryDateTransactionSent.Value = EntryDateTransactionSent;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDateTransactionSent);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.Int);
            parameterSession.Value = int.Parse(session);
            myAdapter.SelectCommand.Parameters.Add(parameterSession);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public DataTable GetBulkTransactionSent(string EffectiveEntryDate, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_GetBulkTransactionSent_FromMaker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBulkTransactionSenDetails(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_BulkTransactionFromMakerDetails", myConnection);
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

        public DataTable GetReturnReceivedBySettlementJDate(string SettlementDate,
                                                            int DepartmentID, string currency, string session)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_ReturnReceivedBySJDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(parameterCurrency);

            SqlParameter parameterSession = new SqlParameter("@Session", SqlDbType.Int);
            parameterSession.Value = int.Parse(session);
            myAdapter.SelectCommand.Parameters.Add(parameterSession);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //Outward maker checker Listing
        public DataTable GetOutwardWithMakerChecker(string EntryDateTransactionSent, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_TransactionSent_Maker_Checker ", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateTransactionSent = new SqlParameter("@EntryDateTransactionSent", SqlDbType.VarChar);
            parameterEntryDateTransactionSent.Value = EntryDateTransactionSent;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDateTransactionSent);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public DataTable GetOutwardReturnWithMakerChecker(string EntryDateReturnSent)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_ReturnSent_Maker_Checker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateReturnSent = new SqlParameter("@EntryDateReturnSent", SqlDbType.VarChar);
            parameterEntryDateReturnSent.Value = EntryDateReturnSent;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDateReturnSent);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public DataTable GetOutwardNOCWithMakerChecker(string EntryDateNOCSent)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_NOCSent_Maker_Checker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateNOCSent = new SqlParameter("@EntryDateNOCSent", SqlDbType.VarChar);
            parameterEntryDateNOCSent.Value = EntryDateNOCSent;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDateNOCSent);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public DataTable GetOutwardRNOCWithMakerChecker(string EntryDateRNOC)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_RNOCSent__Maker_Checker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateRNOC = new SqlParameter("@EntryDateRNOC", SqlDbType.VarChar);
            parameterEntryDateRNOC.Value = EntryDateRNOC;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDateRNOC);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public DataTable GetOutwardDishonorWithMakerChecker(string EntryDateDishonorSent)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_DishonorSent_Maker_Checker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateDishonorSent = new SqlParameter("@EntryDateDishonorSent", SqlDbType.VarChar);
            parameterEntryDateDishonorSent.Value = EntryDateDishonorSent;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDateDishonorSent);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public DataTable GetOutwardContestedWithMakerChecker(string EntryDateContestedSent)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_ContestedSent__Maker_Checker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateContestedSent = new SqlParameter("@EntryDateContestedSent", SqlDbType.VarChar);
            parameterEntryDateContestedSent.Value = EntryDateContestedSent;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDateContestedSent);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //Inward maker checker Listing
        public DataTable GetInwardTransactionApproveWithMakerChecker(string EntryDateTransactionReceived)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_TransactionReceived_Approved_Maker_Checker ", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateTransactionReceived = new SqlParameter("@EntryDateTransactionReceived", SqlDbType.VarChar);
            parameterEntryDateTransactionReceived.Value = EntryDateTransactionReceived;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDateTransactionReceived);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public DataTable GetInwardReturnWithMakerChecker(string EntryDateReturnReceived, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_ReturnReceived_Maker_Checker ", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateReturnReceived = new SqlParameter("@EntryDateReturnReceived", SqlDbType.VarChar);
            parameterEntryDateReturnReceived.Value = EntryDateReturnReceived;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDateReturnReceived);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public DataTable GetInwardNOCWithMakerChecker(string EntryDateNOCReceived, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_NOCReceived_Maker_Checker ", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateNOCReceived = new SqlParameter("@EntryDateNOCReceived", SqlDbType.VarChar);
            parameterEntryDateNOCReceived.Value = EntryDateNOCReceived;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDateNOCReceived);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public DataTable GetInwardRNOCWithMakerChecker(string EntryDateRNOCReceived)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_RNOCReceived_Maker_Checker ", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateRNOCReceived = new SqlParameter("@EntryDateRNOCReceived", SqlDbType.VarChar);
            parameterEntryDateRNOCReceived.Value = EntryDateRNOCReceived;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDateRNOCReceived);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public DataTable GetInwardDishonorWithMakerChecker(string EntryDateDishonorReceived)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_DishonorReceived_Maker_Checker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateDishonorReceived = new SqlParameter("@EntryDateDishonorReceived", SqlDbType.VarChar);
            parameterEntryDateDishonorReceived.Value = EntryDateDishonorReceived;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDateDishonorReceived);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public DataTable GetInwardContestedWithMakerChecker(string EntryDateContestedReceived)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_ContestedReceived_Maker_Checker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEntryDateContestedReceived = new SqlParameter("@EntryDateContestedReceived", SqlDbType.VarChar);
            parameterEntryDateContestedReceived.Value = EntryDateContestedReceived;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDateContestedReceived);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //Dash Board Report

        public DataTable GetBranchWiseInwardTransactionMonitorReport(int Day, int Month, int Year, string BankCode)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportBranchwise", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }

        public DataTable GetMonitorReportBranchwiseCreditForUCB(int Day, int Month, int Year, string BankCode)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportBranchwiseCreditForUCB", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public DataTable GetMonitorReportBranchwiseDebitForUCB(int Day, int Month, int Year, string BankCode)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportBranchwiseDebitForUCB", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }

        public DataTable GetBranchWiseReturnSentMonitorReport(int Day, int Month, int Year, string BankCode)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportBranchwiseForReturnSent", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public DataTable GetBranchWiseNOCSentMonitorReport(int Day, int Month, int Year, string BankCode)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportBranchwiseForNOCSent", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public DataTable GetBranchWiseInwardDishonorMonitorReport(int Day, int Month, int Year, string BankCode)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportBranchwiseForInwardDishonor", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public DataTable GetBranchWiseContestedSentMonitorReport(int Day, int Month, int Year, string BankCode)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportBranchwiseForContestedSent", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public DataTable GetBranchWiseInwardRNOCMonitorReport(int Day, int Month, int Year, string BankCode)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportBranchwiseForInwardRNOC", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }

        //Dash Board Report For Outward
        public DataTable GetMonitorReportDepartmentwiseForOutwardReport(int Day, int Month, int Year)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportDepartmentwiseForOutward", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public DataTable GetMonitorReportDepartmentwiseCreditForUCB(int Day, int Month, int Year)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportDepartmentwiseCreditForUCB", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public DataTable GetMonitorReportDepartmentwiseDebitForUCB(int Day, int Month, int Year)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportDepartmentwiseDebitForUCB", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }

        public DataTable GetMonitorReportDepartmentwiseForInwardReturn(int Day, int Month, int Year)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportDepartmentwiseForInwardReturn", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);


            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public DataTable GetMonitorReportDepartmentwiseForInwardNOC(int Day, int Month, int Year)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportDepartmentwiseForInwardNOC", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);



            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public DataTable GetMonitorReportDepartmentwiseForOutwardRNOC(int Day, int Month, int Year)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportDepartmentwiseForOutwardRNOC", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);


            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public DataTable GetMonitorReportDepartmentwiseForOutwardDishonor(int Day, int Month, int Year)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportDepartmentwiseForOutwardDishonor", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);



            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
        public DataTable GetMonitorReportDepartmentwiseForInwardContested(int Day, int Month, int Year)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportDepartmentwiseForInwardContested", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);


            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }

        public DataTable GetSettlementReportForBranches(int Day, int Month, int Year, int UserID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSettlementReport_forBranches", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);

            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);

            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetMonitorReportDepartmentwiseforHOLD(int Day, int Month, int Year)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetMonitorReportDepartmentwiseforHOLD", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();


            connection.Open();


            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }

        public DataTable GeTransactionAckStatusReport(string Day, string Month, string Year, int ackStatus)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_GetTransactionAckStatus", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;

            DataTable dt = new DataTable();


            connection.Open();


            string Entrydate = Year + Month + Day;


            SqlParameter parameterEntryDate = new SqlParameter("@FileEntryDate", SqlDbType.VarChar, 8);
            parameterEntryDate.Value = Entrydate;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDate);

            SqlParameter parameterAckStatus = new SqlParameter("@AckStatus", SqlDbType.Int, 4);
            parameterAckStatus.Value = ackStatus;
            myAdapter.SelectCommand.Parameters.Add(parameterAckStatus);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }

        public DataTable GetCustomerWiseReport(string FromDate, string Todate, string AccountNO, int ReportType, int UserID,string Currency)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_CustomerTransactionReport", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterFromDate = new SqlParameter("@FromDate", SqlDbType.VarChar);
            parameterFromDate.Value = FromDate;
            myAdapter.SelectCommand.Parameters.Add(parameterFromDate);

            SqlParameter parameterTodate = new SqlParameter("@Todate", SqlDbType.VarChar);
            parameterTodate.Value = Todate;
            myAdapter.SelectCommand.Parameters.Add(parameterTodate);

            SqlParameter parameterAccountNO = new SqlParameter("@AccountNO", SqlDbType.VarChar);
            parameterAccountNO.Value = AccountNO;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNO);

            SqlParameter parameterReportType = new SqlParameter("@ReportType", SqlDbType.Int);
            parameterReportType.Value = ReportType;
            myAdapter.SelectCommand.Parameters.Add(parameterReportType);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);


            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar,3);
            parameterCurrency.Value = Currency;
            myAdapter.SelectCommand.Parameters.Add(parameterCurrency);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetCustomerWiseReport_BankWise(string FromDate, string Todate, string AccountNO, string BankCode, int ReportType, int UserID,string Currency)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_CustomerTransactionReport_Bankwise", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterFromDate = new SqlParameter("@FromDate", SqlDbType.VarChar);
            parameterFromDate.Value = FromDate;
            myAdapter.SelectCommand.Parameters.Add(parameterFromDate);

            SqlParameter parameterTodate = new SqlParameter("@Todate", SqlDbType.VarChar);
            parameterTodate.Value = Todate;
            myAdapter.SelectCommand.Parameters.Add(parameterTodate);

            SqlParameter parameterAccountNO = new SqlParameter("@AccountNO", SqlDbType.VarChar);
            parameterAccountNO.Value = AccountNO;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNO);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.VarChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterReportType = new SqlParameter("@ReportType", SqlDbType.Int);
            parameterReportType.Value = ReportType;
            myAdapter.SelectCommand.Parameters.Add(parameterReportType);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdapter.SelectCommand.Parameters.Add(parameterCurrency);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetCustomerWiseReport_MonthWise(string FromMonth, string TOMonth, string FromYear, string TOYear, string AccountNO, string BankCode, int ReportType, int UserID,string Currency)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_CustomerTransactionReport_Monthwise", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterFromMonth = new SqlParameter("@FromMonth", SqlDbType.VarChar);
            parameterFromMonth.Value = FromMonth;
            myAdapter.SelectCommand.Parameters.Add(parameterFromMonth);

            SqlParameter parameterTOMonth = new SqlParameter("@TOMonth", SqlDbType.VarChar);
            parameterTOMonth.Value = TOMonth;
            myAdapter.SelectCommand.Parameters.Add(parameterTOMonth);

            SqlParameter parameterFromYear = new SqlParameter("@FromYear", SqlDbType.VarChar);
            parameterFromYear.Value = FromYear;
            myAdapter.SelectCommand.Parameters.Add(parameterFromYear);

            SqlParameter parameterTOYear = new SqlParameter("@TOYear", SqlDbType.VarChar);
            parameterTOYear.Value = TOYear;
            myAdapter.SelectCommand.Parameters.Add(parameterTOYear);

            SqlParameter parameterAccountNO = new SqlParameter("@AccountNO", SqlDbType.VarChar);
            parameterAccountNO.Value = AccountNO;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNO);

            SqlParameter parameterReportType = new SqlParameter("@ReportType", SqlDbType.Int);
            parameterReportType.Value = ReportType;
            myAdapter.SelectCommand.Parameters.Add(parameterReportType);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdapter.SelectCommand.Parameters.Add(parameterCurrency);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetCustomerWiseReport_arc(string FromMonth,

string TOMonth,

string FromYear,

string TOYear,

string AccountNO,

int ReportType,

int UserID)
        {
            /***  Archive Connection String  ***/
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString_Arch"]));
             
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_CustomerTransactionReport_Archive", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterFromMonth = new SqlParameter("@FromMonth", SqlDbType.VarChar);
            parameterFromMonth.Value = FromMonth;
            myAdapter.SelectCommand.Parameters.Add(parameterFromMonth);

            SqlParameter parameterTOMonth = new SqlParameter("@TOMonth", SqlDbType.VarChar);
            parameterTOMonth.Value = TOMonth;
            myAdapter.SelectCommand.Parameters.Add(parameterTOMonth);

            SqlParameter parameterFromYear = new SqlParameter("@FromYear", SqlDbType.VarChar);
            parameterFromYear.Value = FromYear;
            myAdapter.SelectCommand.Parameters.Add(parameterFromYear);

            SqlParameter parameterTOYear = new SqlParameter("@TOYear", SqlDbType.VarChar);
            parameterTOYear.Value = TOYear;
            myAdapter.SelectCommand.Parameters.Add(parameterTOYear);

            SqlParameter parameterAccountNO = new SqlParameter("@AccountNO", SqlDbType.VarChar);
            parameterAccountNO.Value = AccountNO;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNO);

            SqlParameter parameterReportType = new SqlParameter("@ReportType", SqlDbType.Int);
            parameterReportType.Value = ReportType;
            myAdapter.SelectCommand.Parameters.Add(parameterReportType);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetCustomerWiseReport_BankWise_arc(string FromMonth,

string TOMonth,

string FromYear,

string TOYear,

string AccountNO,

string BankCode,

int ReportType,

int UserID)
        {
            /***  Archive Connection String  ***/
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString_Arch"]));
           
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_CustomerTransactionReport_Bankwise_Archive", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterFromMonth = new SqlParameter("@FromMonth", SqlDbType.VarChar);
            parameterFromMonth.Value = FromMonth;
            myAdapter.SelectCommand.Parameters.Add(parameterFromMonth);

            SqlParameter parameterTOMonth = new SqlParameter("@TOMonth", SqlDbType.VarChar);
            parameterTOMonth.Value = TOMonth;
            myAdapter.SelectCommand.Parameters.Add(parameterTOMonth);

            SqlParameter parameterFromYear = new SqlParameter("@FromYear", SqlDbType.VarChar);
            parameterFromYear.Value = FromYear;
            myAdapter.SelectCommand.Parameters.Add(parameterFromYear);

            SqlParameter parameterTOYear = new SqlParameter("@TOYear", SqlDbType.VarChar);
            parameterTOYear.Value = TOYear;
            myAdapter.SelectCommand.Parameters.Add(parameterTOYear);

            SqlParameter parameterAccountNO = new SqlParameter("@AccountNO", SqlDbType.VarChar);
            parameterAccountNO.Value = AccountNO;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNO);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.VarChar);
            parameterBankCode.Value = BankCode;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterReportType = new SqlParameter("@ReportType", SqlDbType.Int);
            parameterReportType.Value = ReportType;
            myAdapter.SelectCommand.Parameters.Add(parameterReportType);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetCustomerWiseReport_MonthWise_arc(string FromMonth,

string TOMonth,

string FromYear,

string TOYear,

string AccountNO,

int ReportType,

int UserID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_CustomerTransactionReport_Monthwise_Archive", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterFromMonth = new SqlParameter("@FromMonth", SqlDbType.VarChar);
            parameterFromMonth.Value = FromMonth;
            myAdapter.SelectCommand.Parameters.Add(parameterFromMonth);

            SqlParameter parameterTOMonth = new SqlParameter("@TOMonth", SqlDbType.VarChar);
            parameterTOMonth.Value = TOMonth;
            myAdapter.SelectCommand.Parameters.Add(parameterTOMonth);

            SqlParameter parameterFromYear = new SqlParameter("@FromYear", SqlDbType.VarChar);
            parameterFromYear.Value = FromYear;
            myAdapter.SelectCommand.Parameters.Add(parameterFromYear);

            SqlParameter parameterTOYear = new SqlParameter("@TOYear", SqlDbType.VarChar);
            parameterTOYear.Value = TOYear;
            myAdapter.SelectCommand.Parameters.Add(parameterTOYear);

            SqlParameter parameterAccountNO = new SqlParameter("@AccountNO", SqlDbType.VarChar);
            parameterAccountNO.Value = AccountNO;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNO);

            SqlParameter parameterReportType = new SqlParameter("@ReportType", SqlDbType.Int);
            parameterReportType.Value = ReportType;
            myAdapter.SelectCommand.Parameters.Add(parameterReportType);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetBBReconReport(int Day, int Month, int Year, string currency, string session, int IsDailyVoucher, int IsFlatFile)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand command = new SqlCommand("EFT_BB_ReconReport", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = command;
            DataTable dt = new DataTable();
            connection.Open();

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int, 4);
            parameterDay.Value = Day;
            myAdapter.SelectCommand.Parameters.Add(parameterDay);


            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myAdapter.SelectCommand.Parameters.Add(parameterMonth);


            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYear.Value = Year;
            myAdapter.SelectCommand.Parameters.Add(parameterYear);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@SessionID", SqlDbType.Int);
            paramSession.Value = session;
            myAdapter.SelectCommand.Parameters.Add(paramSession);

            SqlParameter paramIsDailyVoucher = new SqlParameter("@IsDailyVoucher ", SqlDbType.Int);
            paramIsDailyVoucher.Value = IsDailyVoucher;
            myAdapter.SelectCommand.Parameters.Add(paramIsDailyVoucher);

            SqlParameter paramIsFlatFile = new SqlParameter("@IsFlatFile ", SqlDbType.Int);
            paramIsFlatFile.Value = IsFlatFile;
            myAdapter.SelectCommand.Parameters.Add(paramIsFlatFile);

            myAdapter.Fill(dt);

            connection.Close();
            command.Dispose();
            connection.Dispose();

            return dt;
        }
    }
}
