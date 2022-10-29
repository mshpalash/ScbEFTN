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
    public class InwardSearchDB
    {
        //public SqlDataReader GetInwardSearchResult(string BeginDate,
        //                                            string EndDate,
        //                                            string SendingBankRoutNo,
        //                                            string DFIAccountNo,
        //                                            string ReceiverName,
        //                                            double Amount,
        //                                            string IdNumber,
        //                                            string CompanyName,
        //                                            string BankName,
        //                                            string ReceivingBankRoutNo,
        //                                            string BranchName)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_SearchInward", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.VarChar);
        //    parameterBeginDate.Value = BeginDate;
        //    myCommand.Parameters.Add(parameterBeginDate);

        //    SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.VarChar);
        //    parameterEndDate.Value = EndDate;
        //    myCommand.Parameters.Add(parameterEndDate);

        //    SqlParameter parameterSendingBankRoutNo = new SqlParameter("@SendingBankRoutNo", SqlDbType.NVarChar, 9);
        //    parameterSendingBankRoutNo.Value = SendingBankRoutNo;
        //    myCommand.Parameters.Add(parameterSendingBankRoutNo);

        //    SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
        //    parameterDFIAccountNo.Value = DFIAccountNo;
        //    myCommand.Parameters.Add(parameterDFIAccountNo);

        //    SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 15);
        //    parameterReceiverName.Value = ReceiverName;
        //    myCommand.Parameters.Add(parameterReceiverName);

        //    SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
        //    parameterAmount.Value = Amount;
        //    myCommand.Parameters.Add(parameterAmount);

        //    SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 15);
        //    parameterIdNumber.Value = IdNumber;
        //    myCommand.Parameters.Add(parameterIdNumber);

        //    SqlParameter parameterCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
        //    parameterCompanyName.Value = CompanyName;
        //    myCommand.Parameters.Add(parameterCompanyName);

        //    SqlParameter parameterBankName = new SqlParameter("@BankName", SqlDbType.NVarChar, 50);
        //    parameterBankName.Value = BankName;
        //    myCommand.Parameters.Add(parameterBankName);

        //    SqlParameter parameterReceivingBankRoutNo = new SqlParameter("@ReceivingBankRoutNo", SqlDbType.NVarChar, 9);
        //    parameterReceivingBankRoutNo.Value = ReceivingBankRoutNo;
        //    myCommand.Parameters.Add(parameterReceivingBankRoutNo);

        //    SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.NVarChar, 50);
        //    parameterBranchName.Value = BranchName;
        //    myCommand.Parameters.Add(parameterBranchName);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetInwardSearchResult(string BeginDate,
                                                    string EndDate,
                                                    string SendingBankRoutNo,
                                                    string DFIAccountNo,
                                                    string ReceiverName,
                                                    double Amount,
                                                    double MaxAmount,
                                                    string IdNumber,
                                                    string CompanyName,
                                                    string BankName,
                                                    string ReceivingBankRoutNo,
                                                    string BranchName,
                                                    string TraceNumber,
                                                    string BatchNumber,
                                                    string SearchType,
                                                    string PaymentInfo,
                                                    int CreditDebit,
                                                    int UserID,
                                                    bool isFromArchive,
                                                    string currency,
                                                    string session

            )
        {
            string spName = string.Empty;
            if (isFromArchive == false)
            {
                if (SearchType.Equals("1"))
                {
                    spName = "EFT_SearchInward";
                }
                else if (SearchType.Equals("2"))
                {
                    spName = "EFT_SearchInward_Return";
                }
                else if (SearchType.Equals("3"))
                {
                    spName = "EFT_SearchInward_NOC";
                }
                else if (SearchType.Equals("100"))
                {
                    spName = "EFT_SearchInwardTransaction_ForReturn";
                }
            }
            else
            {
                if (SearchType.Equals("1"))
                {
                    spName = "EFT_SearchInward_arc";
                }
                else if (SearchType.Equals("2"))
                {
                    spName = "EFT_SearchInward_Return_arc";
                }
                else if (SearchType.Equals("3"))
                {
                    spName = "EFT_SearchInward_NOC";
                }
                else if (SearchType.Equals("100"))
                {
                    spName = "EFT_SearchInwardTransaction_ForReturn";
                }
            }

            /***  New Connection Type for archive data search  ***/
            SqlConnection myConnection;
            if (isFromArchive)
            {
                myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString_Arch"]));
            }
            else
            {
                myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            }
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter(spName, myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.VarChar);
            parameterBeginDate.Value = BeginDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.VarChar);
            parameterEndDate.Value = EndDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDate);

            SqlParameter parameterSendingBankRoutNo = new SqlParameter("@SendingBankRoutNo", SqlDbType.NVarChar, 9);
            parameterSendingBankRoutNo.Value = SendingBankRoutNo;
            myAdapter.SelectCommand.Parameters.Add(parameterSendingBankRoutNo);

            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 15);
            parameterReceiverName.Value = ReceiverName;
            myAdapter.SelectCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myAdapter.SelectCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterMaxAmount = new SqlParameter("@MaxAmount", SqlDbType.Money);
            parameterMaxAmount.Value = MaxAmount;
            myAdapter.SelectCommand.Parameters.Add(parameterMaxAmount);

            SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 15);
            parameterIdNumber.Value = IdNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterIdNumber);

            SqlParameter parameterCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
            parameterCompanyName.Value = CompanyName;
            myAdapter.SelectCommand.Parameters.Add(parameterCompanyName);

            SqlParameter parameterBankName = new SqlParameter("@BankName", SqlDbType.NVarChar, 50);
            parameterBankName.Value = BankName;
            myAdapter.SelectCommand.Parameters.Add(parameterBankName);

            SqlParameter parameterReceivingBankRoutNo = new SqlParameter("@ReceivingBankRoutNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutNo.Value = ReceivingBankRoutNo;
            myAdapter.SelectCommand.Parameters.Add(parameterReceivingBankRoutNo);

            SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.NVarChar, 50);
            parameterBranchName.Value = BranchName;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchName);

            SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
            parameterTraceNumber.Value = TraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterTraceNumber);

            SqlParameter parameterBatchNumber = new SqlParameter("@BatchNumber", SqlDbType.NVarChar, 6);
            parameterBatchNumber.Value = BatchNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterBatchNumber);

            SqlParameter parameterPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            parameterPaymentInfo.Value = PaymentInfo;
            myAdapter.SelectCommand.Parameters.Add(parameterPaymentInfo);

            SqlParameter parameterCreditDebit = new SqlParameter("@CreditDebit", SqlDbType.Int);
            parameterCreditDebit.Value = CreditDebit;
            myAdapter.SelectCommand.Parameters.Add(parameterCreditDebit);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            paramSession.Value = session;
            myAdapter.SelectCommand.Parameters.Add(paramSession);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetInwardSearchResultForBranchWise(string BeginDate,
                                                    string EndDate,
                                                    string SendingBankRoutNo,
                                                    string DFIAccountNo,
                                                    string ReceiverName,
                                                    double Amount,
                                                    double MaxAmount,
                                                    string IdNumber,
                                                    string CompanyName,
                                                    string BankName,
                                                    string ReceivingBankRoutNo,
                                                    string BranchName,
                                                    string TraceNumber,
                                                    string BatchNumber,
                                                    string SearchType,
                                                    string PaymentInfo,
                                                    int CreditDebit,
                                                    int UserID,
                                                    string currency,
                                                    string session
            )
        {
            string spName = string.Empty;
            if (SearchType.Equals("1"))
            {
                spName = "EFT_SearchInward_ForBranchWise";
            }
            else if (SearchType.Equals("2"))
            {
                spName = "EFT_SearchInward_Return_ForBranchWise";
            }
            else if (SearchType.Equals("3"))
            {
                spName = "EFT_SearchInward_NOC";
            }
            else if (SearchType.Equals("100"))
            {
                spName = "EFT_SearchInwardTransaction_ForReturn";
            }
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter(spName, myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.VarChar);
            parameterBeginDate.Value = BeginDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.VarChar);
            parameterEndDate.Value = EndDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDate);

            SqlParameter parameterSendingBankRoutNo = new SqlParameter("@SendingBankRoutNo", SqlDbType.NVarChar, 9);
            parameterSendingBankRoutNo.Value = SendingBankRoutNo;
            myAdapter.SelectCommand.Parameters.Add(parameterSendingBankRoutNo);

            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 15);
            parameterReceiverName.Value = ReceiverName;
            myAdapter.SelectCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myAdapter.SelectCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterMaxAmount = new SqlParameter("@MaxAmount", SqlDbType.Money);
            parameterMaxAmount.Value = MaxAmount;
            myAdapter.SelectCommand.Parameters.Add(parameterMaxAmount);

            SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 15);
            parameterIdNumber.Value = IdNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterIdNumber);

            SqlParameter parameterCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
            parameterCompanyName.Value = CompanyName;
            myAdapter.SelectCommand.Parameters.Add(parameterCompanyName);

            SqlParameter parameterBankName = new SqlParameter("@BankName", SqlDbType.NVarChar, 50);
            parameterBankName.Value = BankName;
            myAdapter.SelectCommand.Parameters.Add(parameterBankName);

            SqlParameter parameterReceivingBankRoutNo = new SqlParameter("@ReceivingBankRoutNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutNo.Value = ReceivingBankRoutNo;
            myAdapter.SelectCommand.Parameters.Add(parameterReceivingBankRoutNo);

            SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.NVarChar, 50);
            parameterBranchName.Value = BranchName;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchName);

            SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
            parameterTraceNumber.Value = TraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterTraceNumber);

            SqlParameter parameterBatchNumber = new SqlParameter("@BatchNumber", SqlDbType.NVarChar, 6);
            parameterBatchNumber.Value = BatchNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterBatchNumber);

            SqlParameter parameterPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            parameterPaymentInfo.Value = PaymentInfo;
            myAdapter.SelectCommand.Parameters.Add(parameterPaymentInfo);

            SqlParameter parameterCreditDebit = new SqlParameter("@CreditDebit", SqlDbType.Int);
            parameterCreditDebit.Value = CreditDebit;
            myAdapter.SelectCommand.Parameters.Add(parameterCreditDebit);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            paramSession.Value = session;
            myAdapter.SelectCommand.Parameters.Add(paramSession);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetInwardSearchReturnRecordByEDRID(Guid EDRID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_InwardSearch_GetReturnRecordByEDRID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
        //    parameterEDRID.Value = EDRID;
        //    myCommand.Parameters.Add(parameterEDRID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetInwardSearchReturnRecordByEDRID(Guid EDRID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InwardSearch_GetReturnRecordByEDRID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetInwardSearchNOCByEDRID(Guid EDRID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_InwardSearch_GetNOCByEDRID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
        //    parameterEDRID.Value = EDRID;
        //    myCommand.Parameters.Add(parameterEDRID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetInwardSearchNOCByEDRID(Guid EDRID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InwardSearch_GetNOCByEDRID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetInwardSearchDishonorRecordByTraceNumber(string ReturnTraceNumber)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_InwardSearch_GetDishonorRecordByTraceNumber", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterReturnTraceNumber = new SqlParameter("@ReturnTraceNumber", SqlDbType.NVarChar, 15);
        //    parameterReturnTraceNumber.Value = ReturnTraceNumber;
        //    myCommand.Parameters.Add(parameterReturnTraceNumber);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetInwardSearchDishonorRecordByTraceNumber(string ReturnTraceNumber)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InwardSearch_GetDishonorRecordByTraceNumber", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterReturnTraceNumber = new SqlParameter("@ReturnTraceNumber", SqlDbType.NVarChar, 15);
            parameterReturnTraceNumber.Value = ReturnTraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterReturnTraceNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetInwardSearchRNOCByTraceNumber(string NOCTraceNumber)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_InwardSearch_GetRNOCRecordByTraceNumber", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterNOCTraceNumber = new SqlParameter("@NOCTraceNumber", SqlDbType.NVarChar, 15);
        //    parameterNOCTraceNumber.Value = NOCTraceNumber;
        //    myCommand.Parameters.Add(parameterNOCTraceNumber);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetInwardSearchRNOCByTraceNumber(string NOCTraceNumber)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InwardSearch_GetRNOCRecordByTraceNumber", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterNOCTraceNumber = new SqlParameter("@NOCTraceNumber", SqlDbType.NVarChar, 15);
            parameterNOCTraceNumber.Value = NOCTraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterNOCTraceNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable RptAdv_forTransactionReceived(Guid EDRID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptAdv_forTransactionReceived", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}
