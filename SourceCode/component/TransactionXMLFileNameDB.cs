using EFTN.Utility;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class TransactionXMLFileNameDB
    {
        public void InsertTransactionSentXMLFiles(Guid TransactionID,
                                            string BatchNumber,
                                            string TraceNumberStart,
                                            string TraceNumberEnd,
                                            string XmlFileName,
                                            string connectionString,
                                            int totalTransaction,
                                            double totalAmount)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(connectionString);
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertTransactionSentXMLFiles", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterBatchNumber = new SqlParameter("@BatchNumber", SqlDbType.VarChar);
            parameterBatchNumber.Value = BatchNumber;
            myCommand.Parameters.Add(parameterBatchNumber);

            SqlParameter parameterTraceNumberStart = new SqlParameter("@TraceNumberStart", SqlDbType.VarChar);
            parameterTraceNumberStart.Value = TraceNumberStart;
            myCommand.Parameters.Add(parameterTraceNumberStart);

            SqlParameter parameterTraceNumberEnd = new SqlParameter("@TraceNumberEnd", SqlDbType.VarChar);
            parameterTraceNumberEnd.Value = TraceNumberEnd;
            myCommand.Parameters.Add(parameterTraceNumberEnd);

            SqlParameter parameterXmlFileName = new SqlParameter("@XmlFileName", SqlDbType.VarChar);
            parameterXmlFileName.Value = XmlFileName;
            myCommand.Parameters.Add(parameterXmlFileName);

            SqlParameter parameterTotalTransaction = new SqlParameter("@TotalTransaction", SqlDbType.Int);
            parameterTotalTransaction.Value = totalTransaction;
            myCommand.Parameters.Add(parameterTotalTransaction);

            SqlParameter parameterTotalAmount = new SqlParameter("@TotalAmount", SqlDbType.Money);
            parameterTotalAmount.Value = totalAmount;
            myCommand.Parameters.Add(parameterTotalAmount);

            myConnection.Open();
            myCommand.ExecuteReader();
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public void UpdateTransactionSentXMLFilesAcknowledgement(string AckFileName, int session, DateTime settlementDate, string connectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(connectionString);
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateTransactionSentXMLFiles", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterAckFileName = new SqlParameter("@AckFileName", SqlDbType.VarChar);
            parameterAckFileName.Value = AckFileName;
            myCommand.Parameters.Add(parameterAckFileName);

      
            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.Int);
            paramSession.Value = session;
            myCommand.Parameters.Add(paramSession);

            /***    Added new as we got the ACK file with settlement date from BB    ***/
            SqlParameter paramSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.DateTime);
            paramSettlementDate.Value = settlementDate;
            myCommand.Parameters.Add(paramSettlementDate);

            myConnection.Open();
            myCommand.ExecuteReader();
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public int GetLastXmlIdentity(int xmlType)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_GetTransactionTypeWiseXmlIdentity", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterXMLType = new SqlParameter("@XmlType", SqlDbType.Int);
            parameterXMLType.Value = xmlType;
            myCommand.Parameters.Add(parameterXMLType);

            SqlParameter parameterXMLIdentity = new SqlParameter("@XmlIdentity", SqlDbType.Int);
            parameterXMLIdentity.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterXMLIdentity);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            return int.Parse(parameterXMLIdentity.Value.ToString());           
        }
    }
}