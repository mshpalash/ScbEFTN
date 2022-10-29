using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class ISOMessageDB
    {
        public Guid InsertBatchSent
           (
               int envelopID,
               string serviceClassCode,
               string SECC,
               int typeOfPayment,
               DateTime effectiveEntryDate,
               string companyId,
               string companyName,
               string entryDesc,
               int createdBy, int BatchStatus,
               string BatchType,
               int DepartmentID,
               string DataEntryType
           )
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertBatchSent";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramEnvelopID = new SqlParameter("@EnvelopID", SqlDbType.Int, 4);
            paramEnvelopID.Value = envelopID;
            command.Parameters.Add(paramEnvelopID);

            SqlParameter paramServiceClassCode = new SqlParameter("@ServiceClassCode", SqlDbType.NVarChar, 3);
            paramServiceClassCode.Value = serviceClassCode;
            command.Parameters.Add(paramServiceClassCode);

            SqlParameter paramSECC = new SqlParameter("@SECC", SqlDbType.NVarChar, 3);
            paramSECC.Value = SECC;
            command.Parameters.Add(paramSECC);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = typeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);

            SqlParameter paramEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.DateTime);
            paramEffectiveEntryDate.Value = effectiveEntryDate;
            command.Parameters.Add(paramEffectiveEntryDate);

            SqlParameter paramCompanyId = new SqlParameter("@CompanyId", SqlDbType.NVarChar, 10);
            paramCompanyId.Value = companyId;
            command.Parameters.Add(paramCompanyId);

            SqlParameter paramCompanyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 16);
            paramCompanyName.Value = companyName;
            command.Parameters.Add(paramCompanyName);

            SqlParameter paramEntryDesc = new SqlParameter("@EntryDesc", SqlDbType.NVarChar, 10);
            paramEntryDesc.Value = entryDesc;
            command.Parameters.Add(paramEntryDesc);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
            paramCreatedBy.Value = createdBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter parameterBatchStatus = new SqlParameter("@BatchStatus", SqlDbType.Int);
            parameterBatchStatus.Value = BatchStatus;
            command.Parameters.Add(parameterBatchStatus);

            SqlParameter parameterBatchType = new SqlParameter("@BatchType", SqlDbType.NVarChar, 6);
            parameterBatchType.Value = BatchType;
            command.Parameters.Add(parameterBatchType);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            command.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterDataEntryType = new SqlParameter("@DataEntryType", SqlDbType.NVarChar, 6);
            parameterDataEntryType.Value = DataEntryType;
            command.Parameters.Add(parameterDataEntryType);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return (Guid)paramTransactionID.Value;
        }

        public void InsertISOMessege(
            Guid EDRID,
            int MessageType,
            string MTI,
            byte[] BitMap,
            string ActNumber,
            string ProcCode,
            string TransAmount,
            string SystemTraceAuditNo,
            string TransDateTime,
            string CaptureDate,
            string FunctionCode,
            string AcquiringInstuteCode,
            string RetrievalRefNo,
            string ApprovalCode,
            string ActionCode,
            string DeviceID,
            string CardAcceptorIdnt,
            string CardAcceptorName,
            string TransAmountFees,
            string AdditionalData,
            string TransCurrency,
            string DebitActNumber,
            string CreditActNumber,
            string DeliveryChannelID,
            string TerminalType,
            string ReservedCode)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ISOMessegeInsert", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterMessageType = new SqlParameter("@MessageType", SqlDbType.Int);
            parameterMessageType.Value = MessageType;
            myAdapter.SelectCommand.Parameters.Add(parameterMessageType);

            SqlParameter parameterMTI = new SqlParameter("@MTI", SqlDbType.VarChar);
            parameterMTI.Value = MTI;
            myAdapter.SelectCommand.Parameters.Add(parameterMTI);

            SqlParameter parameterBitMap = new SqlParameter("@BitMap", SqlDbType.Binary);
            parameterBitMap.Value = BitMap;
            myAdapter.SelectCommand.Parameters.Add(parameterBitMap);

            SqlParameter parameterActNumber = new SqlParameter("@ActNumber", SqlDbType.VarChar);
            parameterActNumber.Value = ActNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterActNumber);

            SqlParameter parameterProcCode = new SqlParameter("@ProcCode", SqlDbType.VarChar);
            parameterProcCode.Value = ProcCode;
            myAdapter.SelectCommand.Parameters.Add(parameterProcCode);

            SqlParameter parameterTransAmount = new SqlParameter("@TransAmount", SqlDbType.VarChar);
            parameterTransAmount.Value = TransAmount;
            myAdapter.SelectCommand.Parameters.Add(parameterTransAmount);

            SqlParameter parameterSystemTraceAuditNo = new SqlParameter("@SystemTraceAuditNo", SqlDbType.VarChar);
            parameterSystemTraceAuditNo.Value = SystemTraceAuditNo;
            myAdapter.SelectCommand.Parameters.Add(parameterSystemTraceAuditNo);

            SqlParameter parameterTransDateTime = new SqlParameter("@TransDateTime", SqlDbType.VarChar);
            parameterTransDateTime.Value = TransDateTime;
            myAdapter.SelectCommand.Parameters.Add(parameterTransDateTime);

            SqlParameter parameterCaptureDate = new SqlParameter("@CaptureDate", SqlDbType.VarChar);
            parameterCaptureDate.Value = CaptureDate;
            myAdapter.SelectCommand.Parameters.Add(parameterCaptureDate);

            SqlParameter parameterFunctionCode = new SqlParameter("@FunctionCode", SqlDbType.VarChar);
            parameterFunctionCode.Value = FunctionCode;
            myAdapter.SelectCommand.Parameters.Add(parameterFunctionCode);

            SqlParameter parameterAcquiringInstuteCode = new SqlParameter("@AcquiringInstuteCode", SqlDbType.VarChar);
            parameterAcquiringInstuteCode.Value = AcquiringInstuteCode;
            myAdapter.SelectCommand.Parameters.Add(parameterAcquiringInstuteCode);

            SqlParameter parameterRetrievalRefNo = new SqlParameter("@RetrievalRefNo", SqlDbType.VarChar);
            parameterRetrievalRefNo.Value = RetrievalRefNo;
            myAdapter.SelectCommand.Parameters.Add(parameterRetrievalRefNo);

            SqlParameter parameterApprovalCode = new SqlParameter("@ApprovalCode", SqlDbType.VarChar);
            parameterApprovalCode.Value = ApprovalCode;
            myAdapter.SelectCommand.Parameters.Add(parameterApprovalCode);

            SqlParameter parameterActionCode = new SqlParameter("@ActionCode", SqlDbType.VarChar);
            parameterActionCode.Value = ActionCode;
            myAdapter.SelectCommand.Parameters.Add(parameterActionCode);

            SqlParameter parameterDeviceID = new SqlParameter("@DeviceID", SqlDbType.VarChar);
            parameterDeviceID.Value = DeviceID;
            myAdapter.SelectCommand.Parameters.Add(parameterDeviceID);

            SqlParameter parameterCardAcceptorIdnt = new SqlParameter("@CardAcceptorIdnt", SqlDbType.VarChar);
            parameterCardAcceptorIdnt.Value = CardAcceptorIdnt;
            myAdapter.SelectCommand.Parameters.Add(parameterCardAcceptorIdnt);

            SqlParameter parameterCardAcceptorName = new SqlParameter("@CardAcceptorName", SqlDbType.VarChar);
            parameterCardAcceptorName.Value = CardAcceptorName;
            myAdapter.SelectCommand.Parameters.Add(parameterCardAcceptorName);

            SqlParameter parameterTransAmountFees = new SqlParameter("@TransAmountFees", SqlDbType.VarChar);
            parameterTransAmountFees.Value = TransAmountFees;
            myAdapter.SelectCommand.Parameters.Add(parameterTransAmountFees);

            SqlParameter parameterAdditionalData = new SqlParameter("@AdditionalData", SqlDbType.VarChar);
            parameterAdditionalData.Value = AdditionalData;
            myAdapter.SelectCommand.Parameters.Add(parameterAdditionalData);

            SqlParameter parameterTransCurrency = new SqlParameter("@TransCurrency", SqlDbType.VarChar);
            parameterTransCurrency.Value = TransCurrency;
            myAdapter.SelectCommand.Parameters.Add(parameterTransCurrency);

            SqlParameter parameterDebitActNumber = new SqlParameter("@DebitActNumber", SqlDbType.VarChar);
            parameterDebitActNumber.Value = DebitActNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterDebitActNumber);

            SqlParameter parameterCreditActNumber = new SqlParameter("@CreditActNumber", SqlDbType.VarChar);
            parameterCreditActNumber.Value = CreditActNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterCreditActNumber);

            SqlParameter parameterDeliveryChannelID = new SqlParameter("@DeliveryChannelID", SqlDbType.VarChar);
            parameterDeliveryChannelID.Value = DeliveryChannelID;
            myAdapter.SelectCommand.Parameters.Add(parameterDeliveryChannelID);

            SqlParameter parameterTerminalType = new SqlParameter("@TerminalType", SqlDbType.VarChar);
            parameterTerminalType.Value = TerminalType;
            myAdapter.SelectCommand.Parameters.Add(parameterTerminalType);

            SqlParameter parameterReservedCode = new SqlParameter("@ReservedCode", SqlDbType.VarChar);
            parameterReservedCode.Value = ReservedCode;
            myAdapter.SelectCommand.Parameters.Add(parameterReservedCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            //return myDT;
        }

        public DataTable GetISOMessageInfo( byte TranType,
                                            byte FlowType)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ISOMessageInfoGet", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTranType = new SqlParameter("@TranType", SqlDbType.TinyInt);
            parameterTranType.Value = TranType;
            myAdapter.SelectCommand.Parameters.Add(parameterTranType);

            SqlParameter parameterFlowType = new SqlParameter("@FlowType", SqlDbType.TinyInt);
            parameterFlowType.Value = FlowType;
            myAdapter.SelectCommand.Parameters.Add(parameterFlowType);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable ISOMessageGet(int ISOStatus)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ISOMessageGet", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterISOStatus = new SqlParameter("@ISOStatus", SqlDbType.Int);
            parameterISOStatus.Value = ISOStatus;
            myAdapter.SelectCommand.Parameters.Add(parameterISOStatus);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void ISOMessageUpdate(
                                     int MessageType,
                                     string SystemTraceAuditNo,
                                     string CaptureDate,
                                     string ActionCode,int ISOStatus)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ISOMessageUpdate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterISOStatus = new SqlParameter("@ISOStatus", SqlDbType.Int);
            parameterISOStatus.Value = ISOStatus;
            myAdapter.SelectCommand.Parameters.Add(parameterISOStatus);

            SqlParameter parameterMessageType = new SqlParameter("@MessageType", SqlDbType.Int);
            parameterMessageType.Value = MessageType;
            myAdapter.SelectCommand.Parameters.Add(parameterMessageType);

            SqlParameter parameterSystemTraceAuditNo = new SqlParameter("@SystemTraceAuditNo", SqlDbType.VarChar);
            parameterSystemTraceAuditNo.Value = SystemTraceAuditNo;
            myAdapter.SelectCommand.Parameters.Add(parameterSystemTraceAuditNo);

            SqlParameter parameterCaptureDate = new SqlParameter("@CaptureDate", SqlDbType.VarChar);
            parameterCaptureDate.Value = CaptureDate;
            myAdapter.SelectCommand.Parameters.Add(parameterCaptureDate);

            SqlParameter parameterActionCode = new SqlParameter("@ActionCode", SqlDbType.VarChar);
            parameterActionCode.Value = ActionCode;
            myAdapter.SelectCommand.Parameters.Add(parameterActionCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myAdapter.Dispose();
            myConnection.Close();
            //return myDT;
        }

        public DataTable GetISOMessageByTransactionID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ISOMessageGetByTransactionID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}
