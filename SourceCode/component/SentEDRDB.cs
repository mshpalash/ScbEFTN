using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using EFTN.Utility;


namespace EFTN.component
{
    public class SentEDRDB
    {
        //public SqlDataReader GetSentEDR()
        //{
        //    SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandText = "EFT_GetSentEDR";
        //    command.CommandType = CommandType.StoredProcedure;

        //    connection.Open();

        //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
        //    //connection.Dispose();
        //    //connection.Dispose();
        //    return reader;
        //}

        public Guid InsertTransactionSent(
            Guid transactionID,
            string transactionCode,
            int receiverAccountType,
            int typeOfPayment,
            string DFIAccountNo,
            string accountNo,
            string receivingBankRoutingNo,
            decimal amount,
            string IdNumber,
            string receiverName,
            int statusID,
            int createdBy,
            string paymentInfo,
            int DepartmentID,
            string ConnectionString
            ,int isRemittance)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertTransactionSent";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramEDRID);

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Value = transactionID;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramTransactionCode = new SqlParameter("@TransactionCode", SqlDbType.NVarChar, 3);
            paramTransactionCode.Value = transactionCode;
            command.Parameters.Add(paramTransactionCode);

            SqlParameter paramReceiverAccountType = new SqlParameter("@ReceiverAccountType", SqlDbType.TinyInt);
            paramReceiverAccountType.Value = receiverAccountType;
            command.Parameters.Add(paramReceiverAccountType);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = typeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);

            SqlParameter paramDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            paramDFIAccountNo.Value = DFIAccountNo;
            command.Parameters.Add(paramDFIAccountNo);

            SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            paramAccountNo.Value = accountNo;
            command.Parameters.Add(paramAccountNo);

            SqlParameter paramReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            paramReceivingBankRoutingNo.Value = receivingBankRoutingNo;
            command.Parameters.Add(paramReceivingBankRoutingNo);

            SqlParameter paramAmount = new SqlParameter("@Amount", SqlDbType.Money);
            paramAmount.Value = amount;
            command.Parameters.Add(paramAmount);

            SqlParameter paramIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, EFTLength.ReceiverIDLengthForCIE);
            paramIdNumber.Value = IdNumber;
            command.Parameters.Add(paramIdNumber);

            SqlParameter paramReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            paramReceiverName.Value = receiverName;
            command.Parameters.Add(paramReceiverName);

            SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
            paramStatusID.Value = statusID;
            command.Parameters.Add(paramStatusID);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
            paramCreatedBy.Value = createdBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter paramPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            paramPaymentInfo.Value = paymentInfo;
            command.Parameters.Add(paramPaymentInfo);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            command.Parameters.Add(parameterDepartmentID);

            SqlParameter paramRemittance = new SqlParameter("@IsRemittance", SqlDbType.Int);
            paramRemittance.Value = isRemittance;
            command.Parameters.Add(paramRemittance);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();

            return (Guid)paramEDRID.Value;
        }

        public Guid InsertTransactionSentForPubaliText(
            Guid transactionID,
            string transactionCode,
            int receiverAccountType,
            int typeOfPayment,
            string DFIAccountNo,
            string accountNo,
            string receivingBankRoutingNo,
            decimal amount,
            string IdNumber,
            string receiverName,
            int statusID,
            int createdBy,
            string paymentInfo,
            int DepartmentID,
            string CVSID,
            string ConnectionString)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertTransactionSentForPubaliText";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramEDRID);

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Value = transactionID;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramTransactionCode = new SqlParameter("@TransactionCode", SqlDbType.NVarChar, 3);
            paramTransactionCode.Value = transactionCode;
            command.Parameters.Add(paramTransactionCode);

            SqlParameter paramReceiverAccountType = new SqlParameter("@ReceiverAccountType", SqlDbType.TinyInt);
            paramReceiverAccountType.Value = receiverAccountType;
            command.Parameters.Add(paramReceiverAccountType);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = typeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);

            SqlParameter paramDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            paramDFIAccountNo.Value = DFIAccountNo;
            command.Parameters.Add(paramDFIAccountNo);

            SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            paramAccountNo.Value = accountNo;
            command.Parameters.Add(paramAccountNo);

            SqlParameter paramReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            paramReceivingBankRoutingNo.Value = receivingBankRoutingNo;
            command.Parameters.Add(paramReceivingBankRoutingNo);

            SqlParameter paramAmount = new SqlParameter("@Amount", SqlDbType.Money);
            paramAmount.Value = amount;
            command.Parameters.Add(paramAmount);

            SqlParameter paramIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, EFTLength.ReceiverIDLengthForCIE);
            paramIdNumber.Value = IdNumber;
            command.Parameters.Add(paramIdNumber);

            SqlParameter paramReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            paramReceiverName.Value = receiverName;
            command.Parameters.Add(paramReceiverName);

            SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
            paramStatusID.Value = statusID;
            command.Parameters.Add(paramStatusID);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
            paramCreatedBy.Value = createdBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter paramPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            paramPaymentInfo.Value = paymentInfo;
            command.Parameters.Add(paramPaymentInfo);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            command.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterCVSID = new SqlParameter("@CVSID", SqlDbType.VarChar);
            parameterCVSID.Value = CVSID;
            command.Parameters.Add(parameterCVSID);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();

            return (Guid)paramEDRID.Value;
        }

        public Guid InsertTransactionSentforCTX(
            Guid transactionID,
            string transactionCode,
            int receiverAccountType,
            int typeOfPayment,
            string DFIAccountNo,
            string accountNo,
            string receivingBankRoutingNo,
            decimal amount,
            string IdNumber,
            string receiverName,
            int statusID,
            int createdBy,
            string paymentInfo,
            string invoiceNumber,
            string invoiceDate,
            decimal invoiceGrossAmount,
            decimal invoiceAmountPaid,
            string purchaseOrder,
            decimal adjustmentAmount,
            string adjustmentCode,
            string adjustmentDescription,
            int DepartmentID,
            string ConnectionString
            ,int isRemittance
    )
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertTransactionSentforCTX";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramEDRID);

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Value = transactionID;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramTransactionCode = new SqlParameter("@TransactionCode", SqlDbType.NVarChar, 3);
            paramTransactionCode.Value = transactionCode;
            command.Parameters.Add(paramTransactionCode);

            SqlParameter paramReceiverAccountType = new SqlParameter("@ReceiverAccountType", SqlDbType.TinyInt);
            paramReceiverAccountType.Value = receiverAccountType;
            command.Parameters.Add(paramReceiverAccountType);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = typeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);

            SqlParameter paramDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            paramDFIAccountNo.Value = DFIAccountNo;
            command.Parameters.Add(paramDFIAccountNo);

            SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            paramAccountNo.Value = accountNo;
            command.Parameters.Add(paramAccountNo);

            SqlParameter paramReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            paramReceivingBankRoutingNo.Value = receivingBankRoutingNo;
            command.Parameters.Add(paramReceivingBankRoutingNo);

            SqlParameter paramAmount = new SqlParameter("@Amount", SqlDbType.Money);
            paramAmount.Value = amount;
            command.Parameters.Add(paramAmount);

            SqlParameter paramIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 15);
            paramIdNumber.Value = IdNumber;
            command.Parameters.Add(paramIdNumber);

            SqlParameter paramReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, EFTLength.ReceiverNameLengthForCTX);
            paramReceiverName.Value = receiverName;
            command.Parameters.Add(paramReceiverName);

            SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
            paramStatusID.Value = statusID;
            command.Parameters.Add(paramStatusID);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
            paramCreatedBy.Value = createdBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter paramPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            paramPaymentInfo.Value = paymentInfo;
            command.Parameters.Add(paramPaymentInfo);

            SqlParameter paraminvoiceNumber = new SqlParameter("@CTX_InvoiceNumber", SqlDbType.NVarChar, 10);
            paraminvoiceNumber.Value = invoiceNumber;
            command.Parameters.Add(paraminvoiceNumber);

            SqlParameter paraminvoiceDate = new SqlParameter("@CTX_InvoiceDate", SqlDbType.NVarChar, 8);
            paraminvoiceDate.Value = invoiceDate;
            command.Parameters.Add(paraminvoiceDate);

            SqlParameter paraminvoiceGrossAmount = new SqlParameter("@CTX_InvoiceGrossAmount", SqlDbType.Money);
            paraminvoiceGrossAmount.Value = invoiceGrossAmount;
            command.Parameters.Add(paraminvoiceGrossAmount);


            SqlParameter paraminvoiceAmountPaid = new SqlParameter("@CTX_InvoiceAmountPaid", SqlDbType.Money);
            paraminvoiceAmountPaid.Value = invoiceAmountPaid;
            command.Parameters.Add(paraminvoiceAmountPaid);


            SqlParameter parampurchaseOrder = new SqlParameter("@CTX_PurchaseOrder", SqlDbType.NVarChar, 10);
            parampurchaseOrder.Value = purchaseOrder;
            command.Parameters.Add(parampurchaseOrder);

            SqlParameter paramadjustmentAmount = new SqlParameter("@CTX_AdjustmentAmount", SqlDbType.Money);
            paramadjustmentAmount.Value = adjustmentAmount;
            command.Parameters.Add(paramadjustmentAmount);

            SqlParameter paramadjustmentCode = new SqlParameter("@CTX_AdjustmentCode", SqlDbType.NVarChar, 2);
            paramadjustmentCode.Value = adjustmentCode;
            command.Parameters.Add(paramadjustmentCode);


            SqlParameter paramadjustmentDescription = new SqlParameter("@CTX_AdjustmentDescription", SqlDbType.NVarChar, 40);
            paramadjustmentDescription.Value = adjustmentDescription;
            command.Parameters.Add(paramadjustmentDescription);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            command.Parameters.Add(parameterDepartmentID);

            SqlParameter paramRemittance = new SqlParameter("@IsRemittance", SqlDbType.Int);
            paramRemittance.Value = isRemittance;
            command.Parameters.Add(paramRemittance);


            connection.Open();

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return (Guid)paramEDRID.Value;
        }

        public int InsertTransactionSentforCTXForSCBDebit(
            Guid transactionID,
            string transactionCode,
            int receiverAccountType,
            int typeOfPayment,
            string DFIAccountNo,
            string accountNo,
            string receivingBankRoutingNo,
            decimal amount,
            string IdNumber,
            string receiverName,
            int statusID,
            int createdBy,
            string paymentInfo,
            string invoiceNumber,
            string invoiceDate,
            decimal invoiceGrossAmount,
            decimal invoiceAmountPaid,
            string purchaseOrder,
            decimal adjustmentAmount,
            string adjustmentCode,
            string adjustmentDescription,
            int DepartmentID,
            string ConnectionString
            ,int isRemittance
    )
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertTransactionSentforCTX_ForSCBDebit";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Direction = ParameterDirection.Output;
            command.Parameters.Add(parameterEDRID);

            SqlParameter parameterDebitInsert = new SqlParameter("@DebitInsert", SqlDbType.Int);
            parameterDebitInsert.Direction = ParameterDirection.Output;
            command.Parameters.Add(parameterDebitInsert);

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Value = transactionID;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramTransactionCode = new SqlParameter("@TransactionCode", SqlDbType.NVarChar, 3);
            paramTransactionCode.Value = transactionCode;
            command.Parameters.Add(paramTransactionCode);

            SqlParameter paramReceiverAccountType = new SqlParameter("@ReceiverAccountType", SqlDbType.TinyInt);
            paramReceiverAccountType.Value = receiverAccountType;
            command.Parameters.Add(paramReceiverAccountType);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = typeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);

            SqlParameter paramDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            paramDFIAccountNo.Value = DFIAccountNo;
            command.Parameters.Add(paramDFIAccountNo);

            SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            paramAccountNo.Value = accountNo;
            command.Parameters.Add(paramAccountNo);

            SqlParameter paramReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            paramReceivingBankRoutingNo.Value = receivingBankRoutingNo;
            command.Parameters.Add(paramReceivingBankRoutingNo);

            SqlParameter paramAmount = new SqlParameter("@Amount", SqlDbType.Money);
            paramAmount.Value = amount;
            command.Parameters.Add(paramAmount);

            SqlParameter paramIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 15);
            paramIdNumber.Value = IdNumber;
            command.Parameters.Add(paramIdNumber);

            SqlParameter paramReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, EFTLength.ReceiverNameLengthForCTX);
            paramReceiverName.Value = receiverName;
            command.Parameters.Add(paramReceiverName);

            SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
            paramStatusID.Value = statusID;
            command.Parameters.Add(paramStatusID);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
            paramCreatedBy.Value = createdBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter paramPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            paramPaymentInfo.Value = paymentInfo;
            command.Parameters.Add(paramPaymentInfo);

            SqlParameter paraminvoiceNumber = new SqlParameter("@CTX_InvoiceNumber", SqlDbType.NVarChar, 10);
            paraminvoiceNumber.Value = invoiceNumber;
            command.Parameters.Add(paraminvoiceNumber);

            SqlParameter paraminvoiceDate = new SqlParameter("@CTX_InvoiceDate", SqlDbType.NVarChar, 8);
            paraminvoiceDate.Value = invoiceDate;
            command.Parameters.Add(paraminvoiceDate);

            SqlParameter paraminvoiceGrossAmount = new SqlParameter("@CTX_InvoiceGrossAmount", SqlDbType.Money);
            paraminvoiceGrossAmount.Value = invoiceGrossAmount;
            command.Parameters.Add(paraminvoiceGrossAmount);


            SqlParameter paraminvoiceAmountPaid = new SqlParameter("@CTX_InvoiceAmountPaid", SqlDbType.Money);
            paraminvoiceAmountPaid.Value = invoiceAmountPaid;
            command.Parameters.Add(paraminvoiceAmountPaid);


            SqlParameter parampurchaseOrder = new SqlParameter("@CTX_PurchaseOrder", SqlDbType.NVarChar, 10);
            parampurchaseOrder.Value = purchaseOrder;
            command.Parameters.Add(parampurchaseOrder);

            SqlParameter paramadjustmentAmount = new SqlParameter("@CTX_AdjustmentAmount", SqlDbType.Money);
            paramadjustmentAmount.Value = adjustmentAmount;
            command.Parameters.Add(paramadjustmentAmount);

            SqlParameter paramadjustmentCode = new SqlParameter("@CTX_AdjustmentCode", SqlDbType.NVarChar, 2);
            paramadjustmentCode.Value = adjustmentCode;
            command.Parameters.Add(paramadjustmentCode);


            SqlParameter paramadjustmentDescription = new SqlParameter("@CTX_AdjustmentDescription", SqlDbType.NVarChar, 40);
            paramadjustmentDescription.Value = adjustmentDescription;
            command.Parameters.Add(paramadjustmentDescription);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            command.Parameters.Add(parameterDepartmentID);

            SqlParameter paramRemittance = new SqlParameter("@IsRemittance", SqlDbType.Int);
            paramRemittance.Value = isRemittance;
            command.Parameters.Add(paramRemittance);

            connection.Open();

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return (int)parameterDebitInsert.Value;
        }

        //public DataSet GetSentEDR_ForChecker()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ForChecker", myConnection);


        //    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    DataSet ds = new DataSet();
        //    myConnection.Open();
        //    myAdapter.Fill(ds);
        //    myConnection.Close();
        //    myAdapter.Dispose();
        //    myConnection.Dispose();
        //    return ds;
        //}


        public void InsertTransactionSentForCSVSCB(
            Guid transactionID,
            string transactionCode,
            int receiverAccountType,
            int typeOfPayment,
            string DFIAccountNo,
            string accountNo,
            string receivingBankRoutingNo,
            decimal amount,
            string IdNumber,
            string receiverName,
            int statusID,
            int createdBy,
            string paymentInfo,
            int DepartmentID,
            string CustomerID,
            string BatchReference,
            string PaymentReference,
            string ConnectionString)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertTransactionSentForCSVSCB";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramEDRID);

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Value = transactionID;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramTransactionCode = new SqlParameter("@TransactionCode", SqlDbType.NVarChar, 3);
            paramTransactionCode.Value = transactionCode;
            command.Parameters.Add(paramTransactionCode);

            SqlParameter paramReceiverAccountType = new SqlParameter("@ReceiverAccountType", SqlDbType.TinyInt);
            paramReceiverAccountType.Value = receiverAccountType;
            command.Parameters.Add(paramReceiverAccountType);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = typeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);

            SqlParameter paramDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            paramDFIAccountNo.Value = DFIAccountNo;
            command.Parameters.Add(paramDFIAccountNo);

            SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            paramAccountNo.Value = accountNo;
            command.Parameters.Add(paramAccountNo);

            SqlParameter paramReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            paramReceivingBankRoutingNo.Value = receivingBankRoutingNo;
            command.Parameters.Add(paramReceivingBankRoutingNo);

            SqlParameter paramAmount = new SqlParameter("@Amount", SqlDbType.Money);
            paramAmount.Value = amount;
            command.Parameters.Add(paramAmount);

            SqlParameter paramIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, EFTLength.ReceiverIDLengthForCIE);
            paramIdNumber.Value = IdNumber;
            command.Parameters.Add(paramIdNumber);

            SqlParameter paramReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            paramReceiverName.Value = receiverName;
            command.Parameters.Add(paramReceiverName);

            SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
            paramStatusID.Value = statusID;
            command.Parameters.Add(paramStatusID);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
            paramCreatedBy.Value = createdBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter paramPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            paramPaymentInfo.Value = paymentInfo;
            command.Parameters.Add(paramPaymentInfo);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            command.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterCustomerID = new SqlParameter("@CustomerID", SqlDbType.NVarChar, 50);
            parameterCustomerID.Value = CustomerID;
            command.Parameters.Add(parameterCustomerID);

            SqlParameter parameterBatchReference = new SqlParameter("@BatchReference", SqlDbType.NVarChar, 50);
            parameterBatchReference.Value = BatchReference;
            command.Parameters.Add(parameterBatchReference);

            SqlParameter parameterPaymentReference = new SqlParameter("@PaymentReference", SqlDbType.NVarChar, 50);
            parameterPaymentReference.Value = PaymentReference;
            command.Parameters.Add(parameterPaymentReference);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();

            //return (Guid)paramEDRID.Value;
        }


        public void InsertTransactionSentForCSVSCBNonBDT(
            Guid transactionID,
            string transactionCode,
            int receiverAccountType,
            int typeOfPayment,
            string DFIAccountNo,
            string accountNo,
            string receivingBankRoutingNo,
            decimal amount,
            string IdNumber,
            string receiverName,
            int statusID,
            int createdBy,
            string paymentInfo,
            int DepartmentID,
            string CustomerID,
            string BatchReference,
            string PaymentReference,
            string CurrencyCode,
            string ConnectionString)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertTransactionSentForCSVSCB_NonBDT";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramEDRID);

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Value = transactionID;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramTransactionCode = new SqlParameter("@TransactionCode", SqlDbType.NVarChar, 3);
            paramTransactionCode.Value = transactionCode;
            command.Parameters.Add(paramTransactionCode);

            SqlParameter paramReceiverAccountType = new SqlParameter("@ReceiverAccountType", SqlDbType.TinyInt);
            paramReceiverAccountType.Value = receiverAccountType;
            command.Parameters.Add(paramReceiverAccountType);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = typeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);

            SqlParameter paramDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            paramDFIAccountNo.Value = DFIAccountNo;
            command.Parameters.Add(paramDFIAccountNo);

            SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            paramAccountNo.Value = accountNo;
            command.Parameters.Add(paramAccountNo);

            SqlParameter paramReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            paramReceivingBankRoutingNo.Value = receivingBankRoutingNo;
            command.Parameters.Add(paramReceivingBankRoutingNo);

            SqlParameter paramAmount = new SqlParameter("@Amount", SqlDbType.Money);
            paramAmount.Value = amount;
            command.Parameters.Add(paramAmount);

            SqlParameter paramIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 15);
            paramIdNumber.Value = IdNumber;
            command.Parameters.Add(paramIdNumber);

            SqlParameter paramReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            paramReceiverName.Value = receiverName;
            command.Parameters.Add(paramReceiverName);

            SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
            paramStatusID.Value = statusID;
            command.Parameters.Add(paramStatusID);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
            paramCreatedBy.Value = createdBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter paramPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            paramPaymentInfo.Value = paymentInfo;
            command.Parameters.Add(paramPaymentInfo);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            command.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterCustomerID = new SqlParameter("@CustomerID", SqlDbType.NVarChar, 50);
            parameterCustomerID.Value = CustomerID;
            command.Parameters.Add(parameterCustomerID);

            SqlParameter parameterBatchReference = new SqlParameter("@BatchReference", SqlDbType.NVarChar, 50);
            parameterBatchReference.Value = BatchReference;
            command.Parameters.Add(parameterBatchReference);

            SqlParameter parameterPaymentReference = new SqlParameter("@PaymentReference", SqlDbType.NVarChar, 50);
            parameterPaymentReference.Value = PaymentReference;
            command.Parameters.Add(parameterPaymentReference);

            SqlParameter parameterCurrencyCode = new SqlParameter("@CurrencyCode", SqlDbType.NVarChar, 2);
            parameterCurrencyCode.Value = CurrencyCode;
            command.Parameters.Add(parameterCurrencyCode);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();

            //return (Guid)paramEDRID.Value;
        }
        public DataTable GetSentEDR_ForChecker(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ForChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public DataSet GetSentEDR_ArrovedByChecker(string EffectiveEntryDate)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ApprovedByChecker", myConnection);

        //    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
        //    parameterEffectiveEntryDate.Value = EffectiveEntryDate;
        //    myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

        //    DataSet ds = new DataSet();
        //    myConnection.Open();
        //    myAdapter.Fill(ds);
        //    myConnection.Close();
        //    myAdapter.Dispose();
        //    myConnection.Dispose();
        //    return ds;
        //}

        public DataTable GetSentEDR_ApprovedByChecker(string EffectiveEntryDate, string EndingEffectiveEntryDate, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ApprovedByChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterEndingEffectiveEntryDate = new SqlParameter("@EndingEffectiveEntryDate", SqlDbType.VarChar);
            parameterEndingEffectiveEntryDate.Value = EndingEffectiveEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndingEffectiveEntryDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentEDR_ArrovedByCheckerBySettlementDate(string SettlementJDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ApprovedByChecker_BySettlementJDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.VarChar);
            parameterSettlementJDate.Value = SettlementJDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementJDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentEDR_ArrovedByCheckerDT(string EffectiveEntryDate)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetSentEDR_ApprovedByChecker", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = EffectiveEntryDate;
            myCommand.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDR_ApprovedByMaker(string TransactionReceivedDate, string EndingTransactionReceivedDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMaker", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter paramTransactionReceivedDate = new SqlParameter("@TransactionReceivedDate", SqlDbType.NVarChar, 8);
            paramTransactionReceivedDate.Value = TransactionReceivedDate;
            myCommand.SelectCommand.Parameters.Add(paramTransactionReceivedDate);

            SqlParameter parameterEndingTransactionReceivedDate = new SqlParameter("@EndingTransactionReceivedDate", SqlDbType.VarChar);
            parameterEndingTransactionReceivedDate.Value = EndingTransactionReceivedDate;
            myCommand.SelectCommand.Parameters.Add(parameterEndingTransactionReceivedDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myCommand.SelectCommand.Parameters.Add(parameterBranchID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }


        public DataTable GetReceivedEDR_ApprovedByMaker_ForDebit(string TransactionReceivedDate, string EndingTransactionReceivedDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMaker_ForDebit", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter paramTransactionReceivedDate = new SqlParameter("@TransactionReceivedDate", SqlDbType.NVarChar, 8);
            paramTransactionReceivedDate.Value = TransactionReceivedDate;
            myCommand.SelectCommand.Parameters.Add(paramTransactionReceivedDate);

            SqlParameter parameterEndingTransactionReceivedDate = new SqlParameter("@EndingTransactionReceivedDate", SqlDbType.VarChar);
            parameterEndingTransactionReceivedDate.Value = EndingTransactionReceivedDate;
            myCommand.SelectCommand.Parameters.Add(parameterEndingTransactionReceivedDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myCommand.SelectCommand.Parameters.Add(parameterBranchID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }
        public DataTable GetReceivedEDR_ApprovedByMakerBySettlementJDate(string TransReceivedSettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMakerBySettlementJDate", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter paramTransReceivedSettlementDate = new SqlParameter("@TransReceivedSettlementDate", SqlDbType.NVarChar, 8);
            paramTransReceivedSettlementDate.Value = TransReceivedSettlementDate;
            myCommand.SelectCommand.Parameters.Add(paramTransReceivedSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myCommand.SelectCommand.Parameters.Add(parameterBranchID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetReceivedEDR_ApprovedByMakerBySettlementJDate_ForDebit(string TransReceivedSettlementDate, int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myCommand = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMakerBySettlementJDate_ForDebit", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter paramTransReceivedSettlementDate = new SqlParameter("@TransReceivedSettlementDate", SqlDbType.NVarChar, 8);
            paramTransReceivedSettlementDate.Value = TransReceivedSettlementDate;
            myCommand.SelectCommand.Parameters.Add(paramTransReceivedSettlementDate);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myCommand.SelectCommand.Parameters.Add(parameterBranchID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }
        //public SqlDataReader GetSentEDRApprovedByCheckerByEDRID(Guid EDRID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetSentEDR_ApprovedByCheckerByEDRID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
        //    parameterEDRID.Value = EDRID;
        //    myCommand.Parameters.Add(parameterEDRID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    //myCommand.Dispose();
        //    //myConnection.Dispose();
        //    return dr;
        //}

        public DataTable GetSentEDRApprovedByCheckerByEDRID(Guid EDRID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ApprovedByCheckerByEDRID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
            return myDT;
        }

        //public SqlDataReader GetBatchesPrintVoucher_For_TransactionSent_forEBBSCheckerbyTransactionID(Guid TransactionID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetBatchesPrintVoucher_For_TransactionSent_forEBBSCheckerbyTransactionID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
        //    parameterTransactionID.Value = TransactionID;
        //    myCommand.Parameters.Add(parameterTransactionID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    //myCommand.Dispose();
        //    //myConnection.Dispose();
        //    return dr;
        //}

        public DataTable GetBatchesPrintVoucher_For_TransactionSent_forEBBSCheckerbyTransactionID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetBatchesPrintVoucher_For_TransactionSent_forEBBSCheckerbyTransactionID", myConnection);
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

        public void Update_EDRSent_Status(int statusId, Guid EDRID, int ApprovedBy)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_Update_EDRSent_Status";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
            paramStatusID.Value = statusId;
            command.Parameters.Add(paramStatusID);

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Value = EDRID;
            command.Parameters.Add(paramEDRID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            command.Parameters.Add(parameterApprovedBy);

            connection.Open();

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public void Update_EDRSent_Status_FCUBS(int statusId, Guid EDRID, int ApprovedBy, string CBSReference)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_Update_EDRSent_Status_FCUBS";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
            paramStatusID.Value = statusId;
            command.Parameters.Add(paramStatusID);

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Value = EDRID;
            command.Parameters.Add(paramEDRID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            command.Parameters.Add(parameterApprovedBy);

            SqlParameter parameterCBSReference = new SqlParameter("@CBSReference", SqlDbType.VarChar, 20);
            parameterCBSReference.Value = CBSReference;
            command.Parameters.Add(parameterCBSReference);


            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public void EFT_Update_EDRSent_Status_ForSCBGenDebit(int statusId, Guid EDRID, int ApprovedBy)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_Update_EDRSent_Status_forSCBGenDebit";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
            paramStatusID.Value = statusId;
            command.Parameters.Add(paramStatusID);

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Value = EDRID;
            command.Parameters.Add(paramEDRID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            command.Parameters.Add(parameterApprovedBy);

            connection.Open();

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        //public SqlDataReader GetSentEDR_ByCheckerRejection()
        //{
        //    SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandText = "EFT_GetSentEDR_ByCheckerRejection";
        //    command.CommandType = CommandType.StoredProcedure;

        //    connection.Open();

        //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
        //    //command.Dispose();
        //    //connection.Dispose();
        //    return reader;
        //}

        public DataTable GetSentEDR_ByCheckerRejection(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ByCheckerRejection", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentEDR_ByCheckerRejectionCredit(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ByCheckerRejectionCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentEDR_ByCheckerRejectionDebit(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ByCheckerRejectionDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentEDR_ByCheckerRejectionForReverseFlatFile(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ByCheckerRejectionForReverseFlatFile", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentEDR_ByCheckerRejectionForReverseFlatFileCredit(int DepartmentID,string Currency)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ByCheckerRejectionForReverseFlatFileCredit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);


            SqlParameter parameterCurrency= new SqlParameter("@Currency", SqlDbType.VarChar,3);
            parameterCurrency.Value = Currency;
            myAdapter.SelectCommand.Parameters.Add(parameterCurrency);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentEDR_ByCheckerRejectionForReverseFlatFileDebit(int DepartmentID, string Currency)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_ByCheckerRejectionForReverseFlatFileDebit", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdapter.SelectCommand.Parameters.Add(parameterCurrency);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetSentEDR_By_EDRSentID(Guid EDRID)
        //{
        //    SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandText = "EFT_GetSentEDR_By_EDRSentID";
        //    command.CommandType = CommandType.StoredProcedure;

        //    SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
        //    paramEDRID.Value = EDRID;
        //    command.Parameters.Add(paramEDRID);

        //    connection.Open();

        //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //    //command.Dispose();
        //    //connection.Dispose();
        //    return reader;
        //}

        public DataTable GetSentEDR_By_EDRSentID(Guid EDRID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_By_EDRSentID", myConnection);
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

        public void UpdateSentEDR_By_EDRSentID
         (
            Guid EDRID,
            string DFIAccountNo,
            string accountNo,
            decimal amount,
            string IdNumber,
            string receiverName,
            string paymentInfo,
            string ReceivingBankRoutingNo
         )
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_UpdateSentEDR_By_EDRSentID";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Value = EDRID;
            command.Parameters.Add(paramEDRID);

            SqlParameter paramDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            paramDFIAccountNo.Value = DFIAccountNo;
            command.Parameters.Add(paramDFIAccountNo);

            SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            paramAccountNo.Value = accountNo;
            command.Parameters.Add(paramAccountNo);

            SqlParameter paramAmount = new SqlParameter("@Amount", SqlDbType.Money);
            paramAmount.Value = amount;
            command.Parameters.Add(paramAmount);

            SqlParameter paramIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, EFTLength.ReceiverIDLengthForCIE);
            paramIdNumber.Value = IdNumber;
            command.Parameters.Add(paramIdNumber);

            SqlParameter paramReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            paramReceiverName.Value = receiverName;
            command.Parameters.Add(paramReceiverName);


            SqlParameter paramPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            paramPaymentInfo.Value = paymentInfo;
            command.Parameters.Add(paramPaymentInfo);

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            command.Parameters.Add(parameterReceivingBankRoutingNo);

            connection.Open();

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public void UpdateSentEDRByEDRSentIDForBulkUpload
         (
            Guid EDRID,
            string DFIAccountNo,
            string accountNo,
            decimal amount,
            string IdNumber,
            string receiverName,
            string paymentInfo,
            string ReceivingBankRoutingNo,
            string CTX_InvoiceNumber,
            string CTX_InvoiceDate,
            decimal CTX_InvoiceGrossAmount,
            decimal CTX_InvoiceAmountPaid,
            string CTX_PurchaseOrder,
            decimal CTX_AdjustmentAmount,
            string CTX_AdjustmentCode,
            string CTX_AdjustmentDescription
         )
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_UpdateSentEDR_By_EDRSentIDForBulk";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Value = EDRID;
            command.Parameters.Add(paramEDRID);

            SqlParameter paramDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            paramDFIAccountNo.Value = DFIAccountNo;
            command.Parameters.Add(paramDFIAccountNo);

            SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            paramAccountNo.Value = accountNo;
            command.Parameters.Add(paramAccountNo);

            SqlParameter paramAmount = new SqlParameter("@Amount", SqlDbType.Money);
            paramAmount.Value = amount;
            command.Parameters.Add(paramAmount);

            SqlParameter paramIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, EFTLength.ReceiverIDLengthForCIE);
            paramIdNumber.Value = IdNumber;
            command.Parameters.Add(paramIdNumber);

            SqlParameter paramReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            paramReceiverName.Value = receiverName;
            command.Parameters.Add(paramReceiverName);


            SqlParameter paramPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            paramPaymentInfo.Value = paymentInfo;
            command.Parameters.Add(paramPaymentInfo);

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            command.Parameters.Add(parameterReceivingBankRoutingNo);

            SqlParameter parameterCTX_InvoiceNumber = new SqlParameter("@CTX_InvoiceNumber", SqlDbType.NVarChar, 10);
            parameterCTX_InvoiceNumber.Value = CTX_InvoiceNumber;
            command.Parameters.Add(parameterCTX_InvoiceNumber);

            SqlParameter parameterCTX_InvoiceDate = new SqlParameter("@CTX_InvoiceDate", SqlDbType.NVarChar, 8);
            parameterCTX_InvoiceDate.Value = CTX_InvoiceDate;
            command.Parameters.Add(parameterCTX_InvoiceDate);

            SqlParameter parameterCTX_InvoiceGrossAmount = new SqlParameter("@CTX_InvoiceGrossAmount", SqlDbType.Money);
            parameterCTX_InvoiceGrossAmount.Value = CTX_InvoiceGrossAmount;
            command.Parameters.Add(parameterCTX_InvoiceGrossAmount);

            SqlParameter parameterCTX_InvoiceAmountPaid = new SqlParameter("@CTX_InvoiceAmountPaid", SqlDbType.Money);
            parameterCTX_InvoiceAmountPaid.Value = CTX_InvoiceAmountPaid;
            command.Parameters.Add(parameterCTX_InvoiceAmountPaid);

            SqlParameter parameterCTX_PurchaseOrder = new SqlParameter("@CTX_PurchaseOrder", SqlDbType.NVarChar, 10);
            parameterCTX_PurchaseOrder.Value = CTX_PurchaseOrder;
            command.Parameters.Add(parameterCTX_PurchaseOrder);

            SqlParameter parameterCTX_AdjustmentAmount = new SqlParameter("@CTX_AdjustmentAmount", SqlDbType.Money);
            parameterCTX_AdjustmentAmount.Value = CTX_AdjustmentAmount;
            command.Parameters.Add(parameterCTX_AdjustmentAmount);

            SqlParameter parameterCTX_AdjustmentCode = new SqlParameter("@CTX_AdjustmentCode", SqlDbType.NVarChar, 2);
            parameterCTX_AdjustmentCode.Value = CTX_AdjustmentCode;
            command.Parameters.Add(parameterCTX_AdjustmentCode);

            SqlParameter parameterCTX_AdjustmentDescription = new SqlParameter("@CTX_AdjustmentDescription", SqlDbType.NVarChar, 40);
            parameterCTX_AdjustmentDescription.Value = CTX_AdjustmentDescription;
            command.Parameters.Add(parameterCTX_AdjustmentDescription);

            connection.Open();

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        //public SqlDataReader GetSentEDRByTransactionID(Guid transactionID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("[EFT_GetSentEDR_By_BatchSentID]", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
        //    parameterTransactionID.Value = transactionID;
        //    myCommand.Parameters.Add(parameterTransactionID);

        //    SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
        //    parameterBankCode.Value = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
        //    myCommand.Parameters.Add(parameterBankCode);

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    //myCommand.Dispose();
        //    //myConnection.Dispose();
        //    return dr;
        //}

        public DataTable GetSentEDRByTransactionID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_By_BatchSentID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NChar);
            parameterBankCode.Value = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
            myAdapter.SelectCommand.Parameters.Add(parameterBankCode);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetUploadedEDRListByTransactionID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDRByTransactionID", myConnection);
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

        public DataTable GetSentEDRByTransactionIDForBulk(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDRByTransactionIDForBulk", myConnection);
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
        public DataTable GetTransactionsBasedOnCsvUpload(string sqlConnection, int createdBy, string bulkType)
        {
            SqlConnection myConnection = new SqlConnection(sqlConnection);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetTransactionsBasedOnCsvUpload", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter paramUser = new SqlParameter("@UserId", SqlDbType.Int);
            paramUser.Value = createdBy;
            myAdapter.SelectCommand.Parameters.Add(paramUser);

            SqlParameter paramBulkType = new SqlParameter("@BulkType", SqlDbType.VarChar,5);
            paramBulkType.Value = bulkType;
            myAdapter.SelectCommand.Parameters.Add(paramBulkType);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
        public void UpdateEDRSentto1000(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateEDRSentto1000", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
        }

        public void UpdateEDRSentto1(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateEDRSentto1", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
        }


        //public SqlDataReader GetSentEDRByTraceNoForNOCReceived(string traceNumber)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("[EFT_GetSentEDR_By_TraceNo_For_NOCReceived]", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
        //    parameterTraceNumber.Value = traceNumber;
        //    myCommand.Parameters.Add(parameterTraceNumber);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    //myCommand.Dispose();
        //    //myConnection.Dispose();
        //    return dr;
        //}

        public DataTable GetSentEDRByTraceNoForNOCReceived(string TraceNumber)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_By_TraceNo_For_NOCReceived", myConnection);
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

        //public SqlDataReader GetSentEDRByTraceNoForReturnReceived(string traceNumber)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("[EFT_GetSentEDR_By_TraceNo_For_ReturnReceived]", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
        //    parameterTraceNumber.Value = traceNumber;
        //    myCommand.Parameters.Add(parameterTraceNumber);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    //myCommand.Dispose();
        //    //myConnection.Dispose();
        //    return dr;
        //}

        public DataTable GetSentEDRByTraceNoForReturnReceived(string TraceNumber)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDR_By_TraceNo_For_ReturnReceived", myConnection);
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

        public DataTable GetTransactionSentFlatFileData()
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_FlatFile_GetTransactionSent", myConnection))
                {
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

        public void DeleteTransactionSent(Guid EDRID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_delete_TransactionSent", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public void DeleteTransactionSentForIB(Guid EDRID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_delete_TransactionSentForIB", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public DataTable GetSentEDRForSCB_CSV(
                                string CustomerID,
                                string BatchReference,
                                string PaymentReference
                              )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDRForSCB_CSV", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterCustomerID = new SqlParameter("@CustomerID", SqlDbType.NVarChar, 50);
            parameterCustomerID.Value = CustomerID;
            myAdapter.SelectCommand.Parameters.Add(parameterCustomerID);

            SqlParameter parameterBatchReference = new SqlParameter("@BatchReference", SqlDbType.NVarChar, 50);
            parameterBatchReference.Value = BatchReference;
            myAdapter.SelectCommand.Parameters.Add(parameterBatchReference);

            SqlParameter parameterPaymentReference = new SqlParameter("@PaymentReference", SqlDbType.NVarChar, 50);
            parameterPaymentReference.Value = PaymentReference;
            myAdapter.SelectCommand.Parameters.Add(parameterPaymentReference);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetSentEDRByTransactionIDForManualEntry(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDRByTransactionIDForManualEntry", myConnection);
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

        public DataTable InsertTransactionSentforRejectedCSVTransactionsOfSCB(
                                                    Guid TransactionID,
                                                    string TransactionCode,
                                                    string DFIAccountNo,
                                                    string AccountNo,
                                                    string ReceivingBankRoutingNo,
                                                    double Amount,
                                                    string IdNumber,
                                                    string ReceiverName,
                                                    string PaymentInfo,
                                                    int CreatedBy,
                                                    string CustomerID,
                                                    string BatchReference,
                                                    string PaymentReference,
                                                    int FlagReceivingBankRT,
                                                    int FlagAmount,
                                                    int FlagIdNumber,
                                                    int FlagReceiverName,
                                                    int FlagDFIAccountNoLength,
                                                    int FlagAccountNoLength
                                                )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertTransactionSentforRejectedCSVTransactions", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);
            SqlParameter parameterTransactionCode = new SqlParameter("@TransactionCode", SqlDbType.NVarChar, 3);
            parameterTransactionCode.Value = TransactionCode;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionCode);

            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 50);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterDFIAccountNo);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 50);
            parameterAccountNo.Value = AccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 50);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            myAdapter.SelectCommand.Parameters.Add(parameterReceivingBankRoutingNo);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myAdapter.SelectCommand.Parameters.Add(parameterAmount);

            SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 50);
            parameterIdNumber.Value = IdNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterIdNumber);

            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 50);
            parameterReceiverName.Value = ReceiverName;
            myAdapter.SelectCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 100);
            parameterPaymentInfo.Value = PaymentInfo;
            myAdapter.SelectCommand.Parameters.Add(parameterPaymentInfo);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterCustomerID = new SqlParameter("@CustomerID", SqlDbType.NVarChar, 50);
            parameterCustomerID.Value = CustomerID;
            myAdapter.SelectCommand.Parameters.Add(parameterCustomerID);

            SqlParameter parameterBatchReference = new SqlParameter("@BatchReference", SqlDbType.NVarChar, 50);
            parameterBatchReference.Value = BatchReference;
            myAdapter.SelectCommand.Parameters.Add(parameterBatchReference);

            SqlParameter parameterPaymentReference = new SqlParameter("@PaymentReference", SqlDbType.NVarChar, 50);
            parameterPaymentReference.Value = PaymentReference;
            myAdapter.SelectCommand.Parameters.Add(parameterPaymentReference);

            SqlParameter parameterFlagReceivingBankRT = new SqlParameter("@FlagReceivingBankRT", SqlDbType.Int);
            parameterFlagReceivingBankRT.Value = FlagReceivingBankRT;
            myAdapter.SelectCommand.Parameters.Add(parameterFlagReceivingBankRT);

            SqlParameter parameterFlagAmount = new SqlParameter("@FlagAmount", SqlDbType.Int);
            parameterFlagAmount.Value = FlagAmount;
            myAdapter.SelectCommand.Parameters.Add(parameterFlagAmount);

            SqlParameter parameterFlagIdNumber = new SqlParameter("@FlagIdNumber", SqlDbType.Int);
            parameterFlagIdNumber.Value = FlagIdNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterFlagIdNumber);

            SqlParameter parameterFlagReceiverName = new SqlParameter("@FlagReceiverName", SqlDbType.Int);
            parameterFlagReceiverName.Value = FlagReceiverName;
            myAdapter.SelectCommand.Parameters.Add(parameterFlagReceiverName);

            SqlParameter parameterFlagDFIAccountNoLength = new SqlParameter("@FlagDFIAccountNoLength", SqlDbType.Int);
            parameterFlagDFIAccountNoLength.Value = FlagDFIAccountNoLength;
            myAdapter.SelectCommand.Parameters.Add(parameterFlagDFIAccountNoLength);

            SqlParameter parameterFlagAccountNoLength = new SqlParameter("@FlagAccountNoLength", SqlDbType.Int);
            parameterFlagAccountNoLength.Value = FlagAccountNoLength;
            myAdapter.SelectCommand.Parameters.Add(parameterFlagAccountNoLength);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public Guid InsertTransactionSentForExim(
                    Guid transactionID,
                    string transactionCode,
                    int receiverAccountType,
                    int typeOfPayment,
                    string DFIAccountNo,
                    string accountNo,
                    string receivingBankRoutingNo,
                    decimal amount,
                    string IdNumber,
                    string receiverName,
                    int statusID,
                    int createdBy,
                    string paymentInfo,
                    int DepartmentID,
                    string EximReference,
                    string ConnectionString)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertTransactionSentForEXIM";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramEDRID);

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Value = transactionID;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramTransactionCode = new SqlParameter("@TransactionCode", SqlDbType.NVarChar, 3);
            paramTransactionCode.Value = transactionCode;
            command.Parameters.Add(paramTransactionCode);

            SqlParameter paramReceiverAccountType = new SqlParameter("@ReceiverAccountType", SqlDbType.TinyInt);
            paramReceiverAccountType.Value = receiverAccountType;
            command.Parameters.Add(paramReceiverAccountType);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = typeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);

            SqlParameter paramDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            paramDFIAccountNo.Value = DFIAccountNo;
            command.Parameters.Add(paramDFIAccountNo);

            SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            paramAccountNo.Value = accountNo;
            command.Parameters.Add(paramAccountNo);

            SqlParameter paramReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            paramReceivingBankRoutingNo.Value = receivingBankRoutingNo;
            command.Parameters.Add(paramReceivingBankRoutingNo);

            SqlParameter paramAmount = new SqlParameter("@Amount", SqlDbType.Money);
            paramAmount.Value = amount;
            command.Parameters.Add(paramAmount);

            SqlParameter paramIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, EFTLength.ReceiverIDLengthForCIE);
            paramIdNumber.Value = IdNumber;
            command.Parameters.Add(paramIdNumber);

            SqlParameter paramReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            paramReceiverName.Value = receiverName;
            command.Parameters.Add(paramReceiverName);

            SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
            paramStatusID.Value = statusID;
            command.Parameters.Add(paramStatusID);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
            paramCreatedBy.Value = createdBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter paramPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            paramPaymentInfo.Value = paymentInfo;
            command.Parameters.Add(paramPaymentInfo);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            command.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterEximReference = new SqlParameter("@EximReference", SqlDbType.VarChar);
            parameterEximReference.Value = EximReference;
            command.Parameters.Add(parameterEximReference);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();

            return (Guid)paramEDRID.Value;
        }

        public Guid InsertTransactionSentforCTXForExim(
                Guid transactionID,
                string transactionCode,
                int receiverAccountType,
                int typeOfPayment,
                string DFIAccountNo,
                string accountNo,
                string receivingBankRoutingNo,
                decimal amount,
                string IdNumber,
                string receiverName,
                int statusID,
                int createdBy,
                string paymentInfo,
                string invoiceNumber,
                string invoiceDate,
                decimal invoiceGrossAmount,
                decimal invoiceAmountPaid,
                string purchaseOrder,
                decimal adjustmentAmount,
                string adjustmentCode,
                string adjustmentDescription,
                int DepartmentID,
                string EximReference,
                string ConnectionString
            )
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "EFT_InsertTransactionSentforCTX_ForExim";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            paramEDRID.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramEDRID);

            SqlParameter paramTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            paramTransactionID.Value = transactionID;
            command.Parameters.Add(paramTransactionID);

            SqlParameter paramTransactionCode = new SqlParameter("@TransactionCode", SqlDbType.NVarChar, 3);
            paramTransactionCode.Value = transactionCode;
            command.Parameters.Add(paramTransactionCode);

            SqlParameter paramReceiverAccountType = new SqlParameter("@ReceiverAccountType", SqlDbType.TinyInt);
            paramReceiverAccountType.Value = receiverAccountType;
            command.Parameters.Add(paramReceiverAccountType);

            SqlParameter paramTypeOfPayment = new SqlParameter("@TypeOfPayment", SqlDbType.TinyInt);
            paramTypeOfPayment.Value = typeOfPayment;
            command.Parameters.Add(paramTypeOfPayment);

            SqlParameter paramDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.NVarChar, 17);
            paramDFIAccountNo.Value = DFIAccountNo;
            command.Parameters.Add(paramDFIAccountNo);

            SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            paramAccountNo.Value = accountNo;
            command.Parameters.Add(paramAccountNo);

            SqlParameter paramReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            paramReceivingBankRoutingNo.Value = receivingBankRoutingNo;
            command.Parameters.Add(paramReceivingBankRoutingNo);

            SqlParameter paramAmount = new SqlParameter("@Amount", SqlDbType.Money);
            paramAmount.Value = amount;
            command.Parameters.Add(paramAmount);

            SqlParameter paramIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 15);
            paramIdNumber.Value = IdNumber;
            command.Parameters.Add(paramIdNumber);

            SqlParameter paramReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, EFTLength.ReceiverNameLengthForCTX);
            paramReceiverName.Value = receiverName;
            command.Parameters.Add(paramReceiverName);

            SqlParameter paramStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
            paramStatusID.Value = statusID;
            command.Parameters.Add(paramStatusID);

            SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
            paramCreatedBy.Value = createdBy;
            command.Parameters.Add(paramCreatedBy);

            SqlParameter paramPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            paramPaymentInfo.Value = paymentInfo;
            command.Parameters.Add(paramPaymentInfo);

            SqlParameter paraminvoiceNumber = new SqlParameter("@CTX_InvoiceNumber", SqlDbType.NVarChar, 10);
            paraminvoiceNumber.Value = invoiceNumber;
            command.Parameters.Add(paraminvoiceNumber);

            SqlParameter paraminvoiceDate = new SqlParameter("@CTX_InvoiceDate", SqlDbType.NVarChar, 8);
            paraminvoiceDate.Value = invoiceDate;
            command.Parameters.Add(paraminvoiceDate);

            SqlParameter paraminvoiceGrossAmount = new SqlParameter("@CTX_InvoiceGrossAmount", SqlDbType.Money);
            paraminvoiceGrossAmount.Value = invoiceGrossAmount;
            command.Parameters.Add(paraminvoiceGrossAmount);


            SqlParameter paraminvoiceAmountPaid = new SqlParameter("@CTX_InvoiceAmountPaid", SqlDbType.Money);
            paraminvoiceAmountPaid.Value = invoiceAmountPaid;
            command.Parameters.Add(paraminvoiceAmountPaid);


            SqlParameter parampurchaseOrder = new SqlParameter("@CTX_PurchaseOrder", SqlDbType.NVarChar, 10);
            parampurchaseOrder.Value = purchaseOrder;
            command.Parameters.Add(parampurchaseOrder);

            SqlParameter paramadjustmentAmount = new SqlParameter("@CTX_AdjustmentAmount", SqlDbType.Money);
            paramadjustmentAmount.Value = adjustmentAmount;
            command.Parameters.Add(paramadjustmentAmount);

            SqlParameter paramadjustmentCode = new SqlParameter("@CTX_AdjustmentCode", SqlDbType.NVarChar, 2);
            paramadjustmentCode.Value = adjustmentCode;
            command.Parameters.Add(paramadjustmentCode);


            SqlParameter paramadjustmentDescription = new SqlParameter("@CTX_AdjustmentDescription", SqlDbType.NVarChar, 40);
            paramadjustmentDescription.Value = adjustmentDescription;
            command.Parameters.Add(paramadjustmentDescription);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            command.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterEximReference = new SqlParameter("@EximReference", SqlDbType.VarChar);
            parameterEximReference.Value = EximReference;
            command.Parameters.Add(parameterEximReference);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();

            return (Guid)paramEDRID.Value;
        }


        public void Update_FCUBS_ErrorByTransactionIDAndAccountNo(Guid TransactionID,
                                                                    string AccountNo,
                                                                    string CBSErrorMsg)
        {
            SqlConnection connection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = connection;
            myCommand.CommandText = "EFT_Update_FCUBS_ErrorByTransactionIDAndAccountNo";
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
            parameterAccountNo.Value = AccountNo;
            myCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterCBSErrorMsg = new SqlParameter("@CBSErrorMsg", SqlDbType.VarChar);
            parameterCBSErrorMsg.Value = CBSErrorMsg;
            myCommand.Parameters.Add(parameterCBSErrorMsg);

            connection.Open();

            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public DataTable GetEDRIDByTransactionID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetEDRByTransactionID", myConnection);
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

        public int GetAccountWiseCBSHitByTransactionID(Guid TransactionID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetAccountWiseCBSHitByTransactionID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterAccountWiseCBSHit = new SqlParameter("@AccountWiseCBSHit", SqlDbType.Int);
            parameterAccountWiseCBSHit.Direction = ParameterDirection.Output;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountWiseCBSHit);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();

            return (int)parameterAccountWiseCBSHit.Value;
        }

        public string GetSentEDRAccountNo_By_EDRID(Guid EDRID, string connectionString)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            SqlCommand myCommand = new SqlCommand("EFT_GetSentEDRAccountNo_By_EDRID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandTimeout = 600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar, 17);
            parameterAccountNo.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterAccountNo);

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            string DFIAccountNo = parameterAccountNo.Value.ToString();

            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

            return DFIAccountNo;
        }

        public void UpdateSentEDRForReceiverNameFromCBS(Guid EDRID, string SenderNameFromCBS, string connectionString)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateSentEDRAccountNameFromCBS", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterSenderNameFromCBS = new SqlParameter("@SenderNameFromCBS", SqlDbType.VarChar);
            parameterSenderNameFromCBS.Value = SenderNameFromCBS;
            myCommand.Parameters.Add(parameterSenderNameFromCBS);

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Close();
            myConnection.Dispose();
        }


        public void UpdateEDRSentFromCSVAndDeleteTempData(Guid TransactionID )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand sqlCommand = new SqlCommand("EFT_UpdateEDRSentTo_1_And_DeleteTempCSV_Data", myConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;

            sqlCommand.Parameters.Add(parameterTransactionID);
            myConnection.Open();
            sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                     
            myConnection.Close();
        }

    }
}
