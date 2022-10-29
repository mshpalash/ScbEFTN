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
    public class OutwardSearchDB
    {
        //public SqlDataReader GetOutwardSearchResult(string BeginDate,
        //                                            string EndDate,
        //                                            string ReceivingBankRoutingNo,
        //                                            string DFIAccountNo,
        //                                            string ReceiverName,
        //                                            double Amount,
        //                                            string AccountNo,
        //                                            string IdNumber,
        //                                            string CompanyName,
        //                                            string BankName)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must check your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_SearchOutward", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.VarChar);
        //    parameterBeginDate.Value = BeginDate;
        //    myCommand.Parameters.Add(parameterBeginDate);

        //    SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.VarChar);
        //    parameterEndDate.Value = EndDate;
        //    myCommand.Parameters.Add(parameterEndDate);

        //    SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
        //    parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
        //    myCommand.Parameters.Add(parameterReceivingBankRoutingNo);

        //    SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
        //    parameterDFIAccountNo.Value = DFIAccountNo;
        //    myCommand.Parameters.Add(parameterDFIAccountNo);

        //    SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
        //    parameterReceiverName.Value = ReceiverName;
        //    myCommand.Parameters.Add(parameterReceiverName);

        //    SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
        //    parameterAmount.Value = Amount;
        //    myCommand.Parameters.Add(parameterAmount);

        //    SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
        //    parameterAccountNo.Value = AccountNo;
        //    myCommand.Parameters.Add(parameterAccountNo);

        //    SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 15);
        //    parameterIdNumber.Value = IdNumber;
        //    myCommand.Parameters.Add(parameterIdNumber);

        //    SqlParameter parameterCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
        //    parameterCompanyName.Value = CompanyName;
        //    myCommand.Parameters.Add(parameterCompanyName);

        //    SqlParameter parameterBankName = new SqlParameter("@BankName", SqlDbType.NVarChar, 50);
        //    parameterBankName.Value = BankName;
        //    myCommand.Parameters.Add(parameterBankName);

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader();
        //    return dr;
        //}

        public SqlDataReader GetOutwardSearchResultForCSV(string BeginDate,
                                                    string EndDate,
                                                    string ReceivingBankRoutingNo,
                                                    string DFIAccountNo,
                                                    string ReceiverName,
                                                    double Amount,
                                                    double MaxAmount,
                                                    string AccountNo,
                                                    string IdNumber,
                                                    string CompanyName,
                                                    string BankName,
                                                    string TraceNumber,
                                                    string BatchNumber,
                                                    string SearchType,
                                                    int CreditDebit,
                                                    string PaymentInfo,
                                                    int DepartmentID,
                                                    string bankCode,
                                                    int UserID,
                                                    bool isFromArchive
            ,string currency
            ,string session
            )
        {
            string spName = string.Empty;
            //if (bankCode.Equals("225"))
            //{
            //    if (SearchType.Equals("1"))
            //    {
            //        spName = "EFT_SearchOutward_ByEntryDate";
            //    }
            //    else if (SearchType.Equals("2"))
            //    {
            //        spName = "EFT_SearchOutward_Return_byEntryDate";
            //    }
            //    else
            //    {
            //        spName = "EFT_SearchOutward_NOC_ByEntryDate";
            //    }
            //}
            //else
            //{
            //if (SearchType.Equals("1"))
            //{
            //    spName = "EFT_SearchOutward";
            //}
            //else if (SearchType.Equals("2"))
            //{
            //    spName = "EFT_SearchOutward_Return";
            //}
            //else
            //{
            //    spName = "EFT_SearchOutward_NOC";
            //}
            //}
            if (isFromArchive == false)
            {
                if (SearchType.Equals("1"))
                {
                    spName = "EFT_SearchOutward";
                }
                else if (SearchType.Equals("2"))
                {
                    spName = "EFT_SearchOutward_Return";
                }
                else
                {
                    spName = "EFT_SearchOutward_NOC";
                }
            }
            else
            {
                if (SearchType.Equals("1"))
                {
                    spName = "EFT_SearchOutward_arc";
                }
                else if (SearchType.Equals("2"))
                {
                    spName = "EFT_SearchOutward_Return_arc";
                }
                else
                {
                    spName = "EFT_SearchOutward_NOC";
                }
            }

            SqlConnection myConnection;
            if (isFromArchive)
            {
                myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString_Arch"]));
            }
            else
            {
                myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            }
            // Must enter your connection string
            //SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            //SqlDataAdapter myAdapter = new SqlDataAdapter(spName, myConnection);
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand(spName, myConnection);


            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.VarChar);
            parameterBeginDate.Value = BeginDate;
            myCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.VarChar);
            parameterEndDate.Value = EndDate;
            myCommand.Parameters.Add(parameterEndDate);

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            myCommand.Parameters.Add(parameterReceivingBankRoutingNo);

            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            parameterReceiverName.Value = ReceiverName;
            myCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterMaxAmount = new SqlParameter("@MaxAmount", SqlDbType.Money);
            parameterMaxAmount.Value = MaxAmount;
            myCommand.Parameters.Add(parameterMaxAmount);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            parameterAccountNo.Value = AccountNo;
            myCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 15);
            parameterIdNumber.Value = IdNumber;
            myCommand.Parameters.Add(parameterIdNumber);

            SqlParameter parameterCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
            parameterCompanyName.Value = CompanyName;
            myCommand.Parameters.Add(parameterCompanyName);

            SqlParameter parameterBankName = new SqlParameter("@BankName", SqlDbType.NVarChar, 50);
            parameterBankName.Value = BankName;
            myCommand.Parameters.Add(parameterBankName);

            SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
            parameterTraceNumber.Value = TraceNumber;
            myCommand.Parameters.Add(parameterTraceNumber);

            SqlParameter parameterBatchNumber = new SqlParameter("@BatchNumber", SqlDbType.NVarChar, 6);
            parameterBatchNumber.Value = BatchNumber;
            myCommand.Parameters.Add(parameterBatchNumber);

            SqlParameter parameterCreditDebit = new SqlParameter("@CreditDebit", SqlDbType.Int);
            parameterCreditDebit.Value = CreditDebit;
            myCommand.Parameters.Add(parameterCreditDebit);

            SqlParameter parameterPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            parameterPaymentInfo.Value = PaymentInfo;
            myCommand.Parameters.Add(parameterPaymentInfo);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.VarChar, 3);
            paramSession.Value = session;
            myCommand.Parameters.Add(paramSession);

            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        public DataTable GetOutwardSearchResult(string BeginDate,
                                                    string EndDate,
                                                    string ReceivingBankRoutingNo,
                                                    string DFIAccountNo,
                                                    string ReceiverName,
                                                    double Amount,
                                                    double MaxAmount,
                                                    string AccountNo,
                                                    string IdNumber,
                                                    string CompanyName,
                                                    string BankName,
                                                    string TraceNumber,
                                                    string BatchNumber,
                                                    string SearchType,
                                                    int CreditDebit,
                                                    string PaymentInfo,
                                                    int DepartmentID,
                                                    string bankCode,
                                                    int UserID,
                                                    bool isFromArchive,
                                                    string currency,
                                                    string session
                                               )
        {
            string spName = string.Empty;
            //if (bankCode.Equals("225"))
            //{
            //    if (SearchType.Equals("1"))
            //    {
            //        spName = "EFT_SearchOutward_ByEntryDate";
            //    }
            //    else if (SearchType.Equals("2"))
            //    {
            //        spName = "EFT_SearchOutward_Return_byEntryDate";
            //    }
            //    else
            //    {
            //        spName = "EFT_SearchOutward_NOC_ByEntryDate";
            //    }
            //}
            //else
            //{
            if (isFromArchive == false)
            {
                if (SearchType.Equals("1"))
                {
                    spName = "EFT_SearchOutward";
                }
                else if (SearchType.Equals("2"))
                {
                    spName = "EFT_SearchOutward_Return";
                }
                else
                {
                    spName = "EFT_SearchOutward_NOC";
                }
            }
            else
            {
                if (SearchType.Equals("1"))
                {
                    spName = "EFT_SearchOutward_arc";
                   // spName = "EFT_SearchOutward_FromArchive";
                }
                else if (SearchType.Equals("2"))
                {
                    spName = "EFT_SearchOutward_Return_arc";
                }
                else
                {
                    spName = "EFT_SearchOutward_NOC";
                }

            }
            //}

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

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            myAdapter.SelectCommand.Parameters.Add(parameterReceivingBankRoutingNo);

            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            parameterReceiverName.Value = ReceiverName;
            myAdapter.SelectCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myAdapter.SelectCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterMaxAmount = new SqlParameter("@MaxAmount", SqlDbType.Money);
            parameterMaxAmount.Value = MaxAmount;
            myAdapter.SelectCommand.Parameters.Add(parameterMaxAmount);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            parameterAccountNo.Value = AccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 15);
            parameterIdNumber.Value = IdNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterIdNumber);

            SqlParameter parameterCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
            parameterCompanyName.Value = CompanyName;
            myAdapter.SelectCommand.Parameters.Add(parameterCompanyName);

            SqlParameter parameterBankName = new SqlParameter("@BankName", SqlDbType.NVarChar, 50);
            parameterBankName.Value = BankName;
            myAdapter.SelectCommand.Parameters.Add(parameterBankName);

            SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
            parameterTraceNumber.Value = TraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterTraceNumber);

            SqlParameter parameterBatchNumber = new SqlParameter("@BatchNumber", SqlDbType.NVarChar, 6);
            parameterBatchNumber.Value = BatchNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterBatchNumber);

            SqlParameter parameterCreditDebit = new SqlParameter("@CreditDebit", SqlDbType.Int);
            parameterCreditDebit.Value = CreditDebit;
            myAdapter.SelectCommand.Parameters.Add(parameterCreditDebit);

            SqlParameter parameterPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            parameterPaymentInfo.Value = PaymentInfo;
            myAdapter.SelectCommand.Parameters.Add(parameterPaymentInfo);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

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

        public DataTable GetOutwardSearchResultForBranchWise(string BeginDate,
                                                    string EndDate,
                                                    string ReceivingBankRoutingNo,
                                                    string DFIAccountNo,
                                                    string ReceiverName,
                                                    double Amount,
                                                    double MaxAmount,
                                                    string AccountNo,
                                                    string IdNumber,
                                                    string CompanyName,
                                                    string BankName,
                                                    string TraceNumber,
                                                    string BatchNumber,
                                                    string SearchType,
                                                    int CreditDebit,
                                                    string PaymentInfo,
                                                    int DepartmentID,
                                                    string bankCode,
                                                    int UserID,
                                                    string currency,
                                                    string session
                                               )
        {
            string spName = string.Empty;
            //if (bankCode.Equals("225"))
            //{
            //    if (SearchType.Equals("1"))
            //    {
            //        spName = "EFT_SearchOutward_ByEntryDate";
            //    }
            //    else if (SearchType.Equals("2"))
            //    {
            //        spName = "EFT_SearchOutward_Return_byEntryDate";
            //    }
            //    else
            //    {
            //        spName = "EFT_SearchOutward_NOC_ByEntryDate";
            //    }
            //}
            //else
            //{
            if (SearchType.Equals("1"))
            {
                spName = "EFT_SearchOutward_ForBranchWise";
            }
            else if (SearchType.Equals("2"))
            {
                spName = "EFT_SearchOutward_Return_ForBranchWise";
            }
            else
            {
                spName = "EFT_SearchOutward_NOC";
            }
            //}

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

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            myAdapter.SelectCommand.Parameters.Add(parameterReceivingBankRoutingNo);

            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            parameterReceiverName.Value = ReceiverName;
            myAdapter.SelectCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myAdapter.SelectCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterMaxAmount = new SqlParameter("@MaxAmount", SqlDbType.Money);
            parameterMaxAmount.Value = MaxAmount;
            myAdapter.SelectCommand.Parameters.Add(parameterMaxAmount);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            parameterAccountNo.Value = AccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 15);
            parameterIdNumber.Value = IdNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterIdNumber);

            SqlParameter parameterCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
            parameterCompanyName.Value = CompanyName;
            myAdapter.SelectCommand.Parameters.Add(parameterCompanyName);

            SqlParameter parameterBankName = new SqlParameter("@BankName", SqlDbType.NVarChar, 50);
            parameterBankName.Value = BankName;
            myAdapter.SelectCommand.Parameters.Add(parameterBankName);

            SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
            parameterTraceNumber.Value = TraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterTraceNumber);

            SqlParameter parameterBatchNumber = new SqlParameter("@BatchNumber", SqlDbType.NVarChar, 6);
            parameterBatchNumber.Value = BatchNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterBatchNumber);

            SqlParameter parameterCreditDebit = new SqlParameter("@CreditDebit", SqlDbType.Int);
            parameterCreditDebit.Value = CreditDebit;
            myAdapter.SelectCommand.Parameters.Add(parameterCreditDebit);

            SqlParameter parameterPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            parameterPaymentInfo.Value = PaymentInfo;
            myAdapter.SelectCommand.Parameters.Add(parameterPaymentInfo);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);


            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myAdapter.SelectCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.VarChar, 2);
            paramSession.Value = session;
            myAdapter.SelectCommand.Parameters.Add(paramSession);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        //public SqlDataReader GetOutwardSearchReturnRecordByTraceNumber(string TraceNumber)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must check your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_OutwardSearch_GetReturnRecordByTraceNumber", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
        //    parameterTraceNumber.Value = TraceNumber;
        //    myCommand.Parameters.Add(parameterTraceNumber);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader();
        //    return dr;
        //}

        public DataTable GetOutwardSearchReturnRecordByTraceNumber(string TraceNumber)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_OutwardSearch_GetReturnRecordByTraceNumber", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
            parameterTraceNumber.Value = TraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterTraceNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetOutwardSearchNOCByTraceNumber(string TraceNumber)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must check your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_OutwardSearch_GetNOCByTraceNumber", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
        //    parameterTraceNumber.Value = TraceNumber;
        //    myCommand.Parameters.Add(parameterTraceNumber);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader();
        //    return dr;
        //}

        public DataTable GetOutwardSearchNOCByTraceNumber(string TraceNumber)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_OutwardSearch_GetNOCByTraceNumber", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
            parameterTraceNumber.Value = TraceNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterTraceNumber);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetOutwardSearchDishonorRecordByReturnID(Guid ReturnID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must check your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_OutwardSearch_GetDishonorRecordByReturnID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
        //    parameterReturnID.Value = ReturnID;
        //    myCommand.Parameters.Add(parameterReturnID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader();
        //    return dr;
        //}

        public DataTable GetOutwardSearchDishonorRecordByReturnID(Guid ReturnID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_OutwardSearch_GetDishonorRecordByReturnID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            parameterReturnID.Value = ReturnID;
            myAdapter.SelectCommand.Parameters.Add(parameterReturnID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetOutwardSearchRNOCByNOCID(Guid NOCID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must check your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_OutwardSearch_GetRNOCByNOCID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
        //    parameterNOCID.Value = NOCID;
        //    myCommand.Parameters.Add(parameterNOCID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader();
        //    return dr;
        //}

        public DataTable GetOutwardSearchRNOCByNOCID(Guid NOCID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_OutwardSearch_GetRNOCByNOCID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
            parameterNOCID.Value = NOCID;
            myAdapter.SelectCommand.Parameters.Add(parameterNOCID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetTransactionSentAdviceReport(Guid EDRID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_RptAdv_forTransactionSent", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
        //    parameterEDRID.Value = EDRID;
        //    myCommand.Parameters.Add(parameterEDRID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetTransactionSentAdviceReport(Guid EDRID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_RptAdv_forTransactionSent", myConnection);
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