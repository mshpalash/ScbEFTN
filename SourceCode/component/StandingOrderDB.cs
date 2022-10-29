using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class StandingOrderDB
    {
        public Guid InsertStandingOrderBATCH(
                                                string ServiceClassCode,
                                                string SECC,
                                                int TypeOfPayment,
                                                DateTime BatchEntryDate,
                                                string CompanyId,
                                                string CompanyName,
                                                string EntryDesc,
                                                int CreatedBy,
                                                int BatchStatus,
                                                string BatchType,
                                                int DepartmentID,
                                                string DataEntryType,
                                                DateTime BeginDate,
                                                DateTime EndDate,
                                                int StartDayOfMonth,
                                                int EndDayOfMonth,
                                                int TransactionFrequency
                                            )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertStandingOrderBATCH", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Direction = ParameterDirection.Output;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);

            SqlParameter parameterServiceClassCode = new SqlParameter("@ServiceClassCode", SqlDbType.NVarChar, 3);
            parameterServiceClassCode.Value = ServiceClassCode;
            myAdapter.SelectCommand.Parameters.Add(parameterServiceClassCode);

            SqlParameter parameterSECC = new SqlParameter("@SECC", SqlDbType.NVarChar, 3);
            parameterSECC.Value = SECC;
            myAdapter.SelectCommand.Parameters.Add(parameterSECC);

            SqlParameter parameterTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            parameterTypeOfPayment.Value = TypeOfPayment;
            myAdapter.SelectCommand.Parameters.Add(parameterTypeOfPayment);

            SqlParameter parameterBatchEntryDate = new SqlParameter("@BatchEntryDate", SqlDbType.DateTime);
            parameterBatchEntryDate.Value = BatchEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBatchEntryDate);

            SqlParameter parameterCompanyId = new SqlParameter("@CompanyId", SqlDbType.NVarChar, 10);
            parameterCompanyId.Value = CompanyId;
            myAdapter.SelectCommand.Parameters.Add(parameterCompanyId);

            SqlParameter parameterCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
            parameterCompanyName.Value = CompanyName;
            myAdapter.SelectCommand.Parameters.Add(parameterCompanyName);

            SqlParameter parameterEntryDesc = new SqlParameter("@EntryDesc", SqlDbType.NVarChar, 10);
            parameterEntryDesc.Value = EntryDesc;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDesc);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterBatchStatus = new SqlParameter("@BatchStatus", SqlDbType.Int);
            parameterBatchStatus.Value = BatchStatus;
            myAdapter.SelectCommand.Parameters.Add(parameterBatchStatus);

            SqlParameter parameterBatchType = new SqlParameter("@BatchType", SqlDbType.NVarChar, 6);
            parameterBatchType.Value = BatchType;
            myAdapter.SelectCommand.Parameters.Add(parameterBatchType);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterDataEntryType = new SqlParameter("@DataEntryType", SqlDbType.NVarChar, 6);
            parameterDataEntryType.Value = DataEntryType;
            myAdapter.SelectCommand.Parameters.Add(parameterDataEntryType);

            SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.DateTime);
            parameterBeginDate.Value = BeginDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.DateTime);
            parameterEndDate.Value = EndDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDate);

            SqlParameter parameterStartDayOfMonth = new SqlParameter("@StartDayOfMonth", SqlDbType.Int);
            parameterStartDayOfMonth.Value = StartDayOfMonth;
            myAdapter.SelectCommand.Parameters.Add(parameterStartDayOfMonth);

            SqlParameter parameterEndDayOfMonth = new SqlParameter("@EndDayOfMonth", SqlDbType.Int);
            parameterEndDayOfMonth.Value = EndDayOfMonth;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDayOfMonth);

            SqlParameter parameterTransactionFrequency = new SqlParameter("@TransactionFrequency", SqlDbType.Int);
            parameterTransactionFrequency.Value = TransactionFrequency;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionFrequency);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();

            return (Guid)parameterStandingOrderBatchID.Value;
        }

        public Guid InsertStandingOrderEDR(
                                            Guid StandingOrderBatchID,
                                            string TransactionCode,
                                            int ReceiverAccountType,
                                            int TypeOfPayment,
                                            string DFIAccountNo,
                                            string AccountNo,
                                            string ReceivingBankRoutingNo,
                                            decimal Amount,
                                            string IdNumber,
                                            string ReceiverName,
                                            int StatusID,
                                            int CreatedBy,
                                            string PaymentInfo,
                                            int DepartmentID
                                          )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertStandingOrderEDR", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterStandingOrderEDRID = new SqlParameter("@StandingOrderEDRID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderEDRID.Direction = ParameterDirection.Output;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderEDRID);

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Value = StandingOrderBatchID;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);

            SqlParameter parameterTransactionCode = new SqlParameter("@TransactionCode", SqlDbType.NVarChar, 3);
            parameterTransactionCode.Value = TransactionCode;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionCode);

            SqlParameter parameterReceiverAccountType = new SqlParameter("@ReceiverAccountType", SqlDbType.TinyInt);
            parameterReceiverAccountType.Value = ReceiverAccountType;
            myAdapter.SelectCommand.Parameters.Add(parameterReceiverAccountType);

            SqlParameter parameterTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            parameterTypeOfPayment.Value = TypeOfPayment;
            myAdapter.SelectCommand.Parameters.Add(parameterTypeOfPayment);

            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 16);
            parameterAccountNo.Value = AccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            myAdapter.SelectCommand.Parameters.Add(parameterReceivingBankRoutingNo);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myAdapter.SelectCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 22);
            parameterIdNumber.Value = IdNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterIdNumber);

            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            parameterReceiverName.Value = ReceiverName;
            myAdapter.SelectCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myAdapter.SelectCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            parameterPaymentInfo.Value = PaymentInfo;
            myAdapter.SelectCommand.Parameters.Add(parameterPaymentInfo);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();

            return (Guid)parameterStandingOrderEDRID.Value;
        }


        public Guid InsertStandingOrderRFC(string DFIAccountNo,
                            string AccountNo,
                            string ReceivingBankRoutingNo,
                            decimal Amount,
                            string ReceiverName,
                            int CreatedBy,
                            DateTime BeginDate,
                            DateTime EndDate,
                            int TransactionFrequency,
                            string SECC
                            )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertStandingOrderBatchAndEDR_forRFC_City", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 16);
            parameterAccountNo.Value = AccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            myAdapter.SelectCommand.Parameters.Add(parameterReceivingBankRoutingNo);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myAdapter.SelectCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            parameterReceiverName.Value = ReceiverName;
            myAdapter.SelectCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterSECC = new SqlParameter("@SECC", SqlDbType.NVarChar, 3);
            parameterSECC.Value = SECC;
            myAdapter.SelectCommand.Parameters.Add(parameterSECC);

            SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.DateTime);
            parameterBeginDate.Value = BeginDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.DateTime);
            parameterEndDate.Value = EndDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDate);

            SqlParameter parameterTransactionFrequency = new SqlParameter("@TransactionFrequency", SqlDbType.Int);
            parameterTransactionFrequency.Value = TransactionFrequency;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionFrequency);

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Direction = ParameterDirection.Output;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            return (Guid)parameterStandingOrderBatchID.Value;
        }


        public Guid InsertStandingOrderRFC_City(string DFIAccountNo,
                    string AccountNo,
                    string ReceivingBankRoutingNo,
                    decimal Amount,
                    string ReceiverName,
                    int CreatedBy,
                    DateTime BeginDate,
                    string CBLloanACNumber,
                    string FileRefNumber,
                    DateTime EndDate,
                    int TransactionFrequency,
                    string SECC,
                    Guid BundleID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertStandingOrderBatchAndEDR_forRFC_City", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 16);
            parameterAccountNo.Value = AccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            myAdapter.SelectCommand.Parameters.Add(parameterReceivingBankRoutingNo);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myAdapter.SelectCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            parameterReceiverName.Value = ReceiverName;
            myAdapter.SelectCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterSECC = new SqlParameter("@SECC", SqlDbType.NVarChar, 3);
            parameterSECC.Value = SECC;
            myAdapter.SelectCommand.Parameters.Add(parameterSECC);

            SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.DateTime);
            parameterBeginDate.Value = BeginDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterCBLloanACNumber = new SqlParameter("@CBLloanACNumber", SqlDbType.VarChar);
            parameterCBLloanACNumber.Value = CBLloanACNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterCBLloanACNumber);

            SqlParameter parameterFileRefNumber = new SqlParameter("@FileRefNumber", SqlDbType.VarChar);
            parameterFileRefNumber.Value = FileRefNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterFileRefNumber);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.DateTime);
            parameterEndDate.Value = EndDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDate);

            SqlParameter parameterTransactionFrequency = new SqlParameter("@TransactionFrequency", SqlDbType.Int);
            parameterTransactionFrequency.Value = TransactionFrequency;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionFrequency);

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Direction = ParameterDirection.Output;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);

            SqlParameter parameterBundleID = new SqlParameter("@BundleID", SqlDbType.UniqueIdentifier);
            parameterBundleID.Value = BundleID;
            myAdapter.SelectCommand.Parameters.Add(parameterBundleID);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            return (Guid)parameterStandingOrderBatchID.Value;
        }
        public Guid InsertStandingOrderBATCHDate(
                                                    Guid StandingOrderBatchID,
                                                    DateTime EffectiveEntryDate
                                                )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertStandingOrderBATCHDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterStandingOrderBatchDetailsID = new SqlParameter("@StandingOrderBatchDetailsID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchDetailsID.Direction = ParameterDirection.Output;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchDetailsID);

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Value = StandingOrderBatchID;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.DateTime);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();

            return (Guid)parameterStandingOrderBatchDetailsID.Value;
        }

        public DataTable GetStandingOrderByStandingOrderBatchID(Guid StandingOrderBatchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetStandingOrdersByStandingOrderBatchID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Value = StandingOrderBatchID;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetStandingOrderBatchDateAlreadyExecuted(Guid StandingOrderBatchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetStandingOrderBatchDateAlreadyExecuted", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Value = StandingOrderBatchID;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateStandingOrderDate(Guid StandingOrderBatchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateStandingOrderDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Value = StandingOrderBatchID;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable GetStandingOrderDateByStandingOrderBatchID(Guid StandingOrderBatchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetStandingOrderDateByStandingOrderBatchID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Value = StandingOrderBatchID;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void DeleteStandingOrderBatch(Guid StandingOrderBatchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DeleteStandingOrderBatch", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Value = StandingOrderBatchID;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable GetAllActiveStandingOrderList23423()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetAllActiveStandingOrderList", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetAllActiveStandingOrderList(int UserID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetAllActiveStandingOrderList", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetAllStandingOrderRejectedList(int UserID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetAllStandingOrderRejectedList", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetAllActiveStandingOrderListBySTDOBatchID(Guid StandingOrderBatchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetAllActiveStandingOrderListBySTDOBatchID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSTDOBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterSTDOBatchID.Value = StandingOrderBatchID;
            myAdapter.SelectCommand.Parameters.Add(parameterSTDOBatchID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetAllActiveStandingOrderListSearch(int UserID, string SearchParam)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetAllActiveStandingOrderListSearch", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterSearchParam = new SqlParameter("@SearchParam", SqlDbType.NVarChar, 50);
            parameterSearchParam.Value = SearchParam;
            myAdapter.SelectCommand.Parameters.Add(parameterSearchParam);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public void InsertTransactionSentForRFCReturn(Guid ReturnID,
                                int UserID, decimal ResentAmount)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertTransactionSent_forRFCReturn", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterReturnID = new SqlParameter("@ReturnID", SqlDbType.UniqueIdentifier);
            parameterReturnID.Value = ReturnID;
            myAdapter.SelectCommand.Parameters.Add(parameterReturnID);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterResentAmount = new SqlParameter("@ResentAmount", SqlDbType.Money);
            parameterResentAmount.Value = ResentAmount;
            myAdapter.SelectCommand.Parameters.Add(parameterResentAmount);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void UpdateStandingOrderBatchStatus(Guid StandingOrderBatchID, string ActiveStatus, int CreatedBy)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateStandingOrderBatchStatus", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Value = StandingOrderBatchID;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);

            SqlParameter parameterActiveStatus = new SqlParameter("@ActiveStatus", SqlDbType.VarChar, 50);
            parameterActiveStatus.Value = ActiveStatus;
            myAdapter.SelectCommand.Parameters.Add(parameterActiveStatus);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterCreatedBy);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable GetAllStandingOrderList()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetAllStandingOrderList", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DateTime GetFirstInstallmentDateByStandingOrderBatchID(Guid standingOrderBatchId)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("[EFT_GetFirstInstallmentDateByStandingOrderBatchID]", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;
            SqlParameter paramStdoBatchId = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            paramStdoBatchId.Value = standingOrderBatchId;
            myAdapter.SelectCommand.Parameters.Add(paramStdoBatchId);

            SqlParameter paramFirstInstallmentDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.DateTime);
            paramFirstInstallmentDate.Direction = ParameterDirection.Output;
            myAdapter.SelectCommand.Parameters.Add(paramFirstInstallmentDate);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            return (DateTime)paramFirstInstallmentDate.Value;
        }

        public DataTable GetAllStandingOrderListbyDateRange(string BeginDate, string EndDate, int UserID, string BatchStatus)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetAllStandingOrderListbyDateRange", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.VarChar);
            parameterBeginDate.Value = BeginDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.VarChar);
            parameterEndDate.Value = EndDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDate);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterBatchStatus = new SqlParameter("@BatchStatus", SqlDbType.VarChar);
            parameterBatchStatus.Value = BatchStatus;
            myAdapter.SelectCommand.Parameters.Add(parameterBatchStatus);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetAllStandingOrderListbyExecutionDateRange(string BeginDate, string EndDate, int UserID, string BatchStatus)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetAllStandingOrderListbyExecutionDateRange", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.VarChar);
            parameterBeginDate.Value = BeginDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.VarChar);
            parameterEndDate.Value = EndDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDate);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterBatchStatus = new SqlParameter("@BatchStatus", SqlDbType.VarChar);
            parameterBatchStatus.Value = BatchStatus;
            myAdapter.SelectCommand.Parameters.Add(parameterBatchStatus);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetAllStandingOrderListbyDateRangeWithSearchParam(string BeginDate, string EndDate, int UserID, string SearchParam, string BatchStatus)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetAllStandingOrderListbyDateRangeSearchParam", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.VarChar);
            parameterBeginDate.Value = BeginDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.VarChar);
            parameterEndDate.Value = EndDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDate);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterSearchParam = new SqlParameter("@SearchParam", SqlDbType.NVarChar, 50);
            parameterSearchParam.Value = SearchParam;
            myAdapter.SelectCommand.Parameters.Add(parameterSearchParam);

            SqlParameter parameterBatchStatus = new SqlParameter("@BatchStatus", SqlDbType.VarChar);
            parameterBatchStatus.Value = BatchStatus;
            myAdapter.SelectCommand.Parameters.Add(parameterBatchStatus);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public DataTable GetStandingOrderBatchByBundleID(Guid BundleID)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetStandingOrderBatchByBundleID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBundleID = new SqlParameter("@BundleID", SqlDbType.UniqueIdentifier);
            parameterBundleID.Value = BundleID;
            myAdapter.SelectCommand.Parameters.Add(parameterBundleID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetStandingOrderBatchByStandingOrderBatchID(Guid StandingOrderBatchID)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetStandingOrderBatchByStandingOrderBatchID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Value = StandingOrderBatchID;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public void DeleteStandingOrderBatchByBundleID(Guid BundleID)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DeleteStandingOrderBatchByBundleID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBundleID = new SqlParameter("@BundleID", SqlDbType.UniqueIdentifier);
            parameterBundleID.Value = BundleID;
            myAdapter.SelectCommand.Parameters.Add(parameterBundleID);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public Guid InsertStandingOrderForSCBDebit(string DFIAccountNo,
                    string AccountNo,
                    string ReceivingBankRoutingNo,
                    decimal Amount,
                    string ReceiverName,
                    int CreatedBy,
                    DateTime BeginDate,
                    DateTime EndDate,
                    int TransactionFrequency,
                    string SECC,
                    Guid BundleID,
                    string OrderingCustomer,
                    //string StandingOrderReference,
                    string CustomerLetterReference,
                    bool Charge,
                    string CompanyId,
                    string CompanyName,
                    string EntryDesc,
                    string PaymentInfo,
                    int DepartmentID,
                    string Currency
                    )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertStandingOrderBatchAndEDREntryDateForSCB_Debit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 16);
            parameterAccountNo.Value = AccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            myAdapter.SelectCommand.Parameters.Add(parameterReceivingBankRoutingNo);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myAdapter.SelectCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            parameterReceiverName.Value = ReceiverName;
            myAdapter.SelectCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterSECC = new SqlParameter("@SECC", SqlDbType.NVarChar, 3);
            parameterSECC.Value = SECC;
            myAdapter.SelectCommand.Parameters.Add(parameterSECC);

            SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.DateTime);
            parameterBeginDate.Value = BeginDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.DateTime);
            parameterEndDate.Value = EndDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDate);

            SqlParameter parameterTransactionFrequency = new SqlParameter("@TransactionFrequency", SqlDbType.Int);
            parameterTransactionFrequency.Value = TransactionFrequency;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionFrequency);

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Direction = ParameterDirection.Output;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);

            SqlParameter parameterBundleID = new SqlParameter("@BundleID", SqlDbType.UniqueIdentifier);
            parameterBundleID.Value = BundleID;
            myAdapter.SelectCommand.Parameters.Add(parameterBundleID);

            SqlParameter parameterOrderingCustomer = new SqlParameter("@OrderingCustomer", SqlDbType.NVarChar, 50);
            parameterOrderingCustomer.Value = OrderingCustomer;
            myAdapter.SelectCommand.Parameters.Add(parameterOrderingCustomer);

            SqlParameter parameterCustomerLetterReference = new SqlParameter("@CustomerLetterReference", SqlDbType.NVarChar, 50);
            parameterCustomerLetterReference.Value = CustomerLetterReference;
            myAdapter.SelectCommand.Parameters.Add(parameterCustomerLetterReference);

            SqlParameter parameterCharge = new SqlParameter("@Charge", SqlDbType.Bit);
            parameterCharge.Value = Charge;
            myAdapter.SelectCommand.Parameters.Add(parameterCharge);

            SqlParameter parameterCompanyId = new SqlParameter("@CompanyId", SqlDbType.NVarChar, 10);
            parameterCompanyId.Value = CompanyId;
            myAdapter.SelectCommand.Parameters.Add(parameterCompanyId);

            SqlParameter parameterCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
            parameterCompanyName.Value = CompanyName;
            myAdapter.SelectCommand.Parameters.Add(parameterCompanyName);

            SqlParameter parameterEntryDesc = new SqlParameter("@EntryDesc", SqlDbType.NVarChar, 10);
            parameterEntryDesc.Value = EntryDesc;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDesc);

            SqlParameter parameterPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            parameterPaymentInfo.Value = PaymentInfo;
            myAdapter.SelectCommand.Parameters.Add(parameterPaymentInfo);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdapter.SelectCommand.Parameters.Add(parameterCurrency);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            return (Guid)parameterStandingOrderBatchID.Value;
        }

        public Guid InsertStandingOrderForSCBCredit(string DFIAccountNo,
                                                        string AccountNo,
                                                        string ReceivingBankRoutingNo,
                                                        decimal Amount,
                                                        string ReceiverName,
                                                        int CreatedBy,
                                                        DateTime BeginDate,
                                                        DateTime EndDate,
                                                        int TransactionFrequency,
                                                        string SECC,
                                                        Guid BundleID,
                                                        string OrderingCustomer,
                                                        //string StandingOrderReference,
                                                        string CustomerLetterReference,
                                                        bool Charge,
                                                        string CompanyId,
                                                        string CompanyName,
                                                        string EntryDesc,
                                                        string PaymentInfo,
                                                        int DepartmentID,
                                                        string Currency
                                                    )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertStandingOrderBatchAndEDREntryDateForSCB_Credit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 16);
            parameterAccountNo.Value = AccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            myAdapter.SelectCommand.Parameters.Add(parameterReceivingBankRoutingNo);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myAdapter.SelectCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            parameterReceiverName.Value = ReceiverName;
            myAdapter.SelectCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterSECC = new SqlParameter("@SECC", SqlDbType.NVarChar, 3);
            parameterSECC.Value = SECC;
            myAdapter.SelectCommand.Parameters.Add(parameterSECC);

            SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.DateTime);
            parameterBeginDate.Value = BeginDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.DateTime);
            parameterEndDate.Value = EndDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDate);

            SqlParameter parameterTransactionFrequency = new SqlParameter("@TransactionFrequency", SqlDbType.Int);
            parameterTransactionFrequency.Value = TransactionFrequency;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionFrequency);

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Direction = ParameterDirection.Output;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);

            SqlParameter parameterBundleID = new SqlParameter("@BundleID", SqlDbType.UniqueIdentifier);
            parameterBundleID.Value = BundleID;
            myAdapter.SelectCommand.Parameters.Add(parameterBundleID);

            SqlParameter parameterOrderingCustomer = new SqlParameter("@OrderingCustomer", SqlDbType.NVarChar, 50);
            parameterOrderingCustomer.Value = OrderingCustomer;
            myAdapter.SelectCommand.Parameters.Add(parameterOrderingCustomer);

            SqlParameter parameterCustomerLetterReference = new SqlParameter("@CustomerLetterReference", SqlDbType.NVarChar, 50);
            parameterCustomerLetterReference.Value = CustomerLetterReference;
            myAdapter.SelectCommand.Parameters.Add(parameterCustomerLetterReference);

            SqlParameter parameterCharge = new SqlParameter("@Charge", SqlDbType.Bit);
            parameterCharge.Value = Charge;
            myAdapter.SelectCommand.Parameters.Add(parameterCharge);

            SqlParameter parameterCompanyId = new SqlParameter("@CompanyId", SqlDbType.NVarChar, 10);
            parameterCompanyId.Value = CompanyId;
            myAdapter.SelectCommand.Parameters.Add(parameterCompanyId);

            SqlParameter parameterCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
            parameterCompanyName.Value = CompanyName;
            myAdapter.SelectCommand.Parameters.Add(parameterCompanyName);

            SqlParameter parameterEntryDesc = new SqlParameter("@EntryDesc", SqlDbType.NVarChar, 10);
            parameterEntryDesc.Value = EntryDesc;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDesc);

            SqlParameter parameterPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            parameterPaymentInfo.Value = PaymentInfo;
            myAdapter.SelectCommand.Parameters.Add(parameterPaymentInfo);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);


            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar ,3);
            parameterCurrency.Value = Currency;
            myAdapter.SelectCommand.Parameters.Add(parameterCurrency);


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            return (Guid)parameterStandingOrderBatchID.Value;
        }


        public void UpdateStandingOrderBatchAndEDREntryDateForSCB(
                                                                    string DFIAccountNo,
                                                                    string AccountNo,
                                                                    string ReceivingBankRoutingNo,
                                                                    double Amount,
                                                                    string ReceiverName,
                                                                    string IdNumber,
                                                                    string PaymentInfo,
                                                                    DateTime BeginDate,
                                                                    DateTime EndDate,
                                                                    Guid StandingOrderBatchID,
                                                                    string OrderingCustomer,
                                                                    string CustomerLetterReference,
                                                                    string CompanyId,
                                                                    string CompanyName,
                                                                    string EntryDesc,
                                                                    int CreatedBy
                                                                )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateStandingOrderBatchAndEDREntryDateForSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 16);
            parameterAccountNo.Value = AccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            myAdapter.SelectCommand.Parameters.Add(parameterReceivingBankRoutingNo);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myAdapter.SelectCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            parameterReceiverName.Value = ReceiverName;
            myAdapter.SelectCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 22);
            parameterIdNumber.Value = IdNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterIdNumber);

            SqlParameter parameterPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            parameterPaymentInfo.Value = PaymentInfo;
            myAdapter.SelectCommand.Parameters.Add(parameterPaymentInfo);

            SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.DateTime);
            parameterBeginDate.Value = BeginDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.DateTime);
            parameterEndDate.Value = EndDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDate);

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Value = StandingOrderBatchID;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);

            SqlParameter parameterOrderingCustomer = new SqlParameter("@OrderingCustomer", SqlDbType.NVarChar, 50);
            parameterOrderingCustomer.Value = OrderingCustomer;
            myAdapter.SelectCommand.Parameters.Add(parameterOrderingCustomer);

            SqlParameter parameterCustomerLetterReference = new SqlParameter("@CustomerLetterReference", SqlDbType.NVarChar, 50);
            parameterCustomerLetterReference.Value = CustomerLetterReference;
            myAdapter.SelectCommand.Parameters.Add(parameterCustomerLetterReference);

            SqlParameter parameterCompanyId = new SqlParameter("@CompanyId", SqlDbType.NVarChar, 10);
            parameterCompanyId.Value = CompanyId;
            myAdapter.SelectCommand.Parameters.Add(parameterCompanyId);

            SqlParameter parameterCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
            parameterCompanyName.Value = CompanyName;
            myAdapter.SelectCommand.Parameters.Add(parameterCompanyName);

            SqlParameter parameterEntryDesc = new SqlParameter("@EntryDesc", SqlDbType.NVarChar, 10);
            parameterEntryDesc.Value = EntryDesc;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDesc);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterCreatedBy);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void DeleteStandingOrderBatchByStandingOrderBatchID(Guid StandingOrderBatchID, int CreatedBy)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DeleteStandingOrderBatchByStandingOrderBatchID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Value = StandingOrderBatchID;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterCreatedBy);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable GetStandingOrderLogByDateRange(string BeginDate, string EndDate)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetStandingOrderLogByDateRange", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBeginDate = new SqlParameter("@LogEntryDateStart", SqlDbType.VarChar);
            parameterBeginDate.Value = BeginDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterEndDate = new SqlParameter("@LogEntryDateEnd", SqlDbType.VarChar);
            parameterEndDate.Value = EndDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDate);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetStandingOrderDateByStandingOrderBatchIDForReport(Guid StandingOrderBatchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetStandingOrderDateByStandingOrderBatchIDForReport", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterStandingOrderBatchID = new SqlParameter("@StandingOrderBatchID", SqlDbType.UniqueIdentifier);
            parameterStandingOrderBatchID.Value = StandingOrderBatchID;
            myAdapter.SelectCommand.Parameters.Add(parameterStandingOrderBatchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

    }
}
