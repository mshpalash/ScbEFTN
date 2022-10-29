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
using EFTN.component;
using System.Data.OracleClient;
using EFTN.Utility;

namespace EFTN.BLL
{
    public class PIBSManager
    {
        public int InsertInwardTransactionToPIBS()
        {
            int noOfItem = 0;
            if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("175"))
            {

                PIBSDB pibsDB= new PIBSDB();

                DataTable dt = pibsDB.GetRecievedEDRForAdmin();

                SendToPIBS(dt);
                noOfItem = dt.Rows.Count;
            }
            return noOfItem;
        }

        private void SendToPIBS(DataTable dt)
        {

            string oracleConnectionString = ConfigurationManager.AppSettings["PIBSConnectionString"];
            OracleConnection conn = new OracleConnection(oracleConnectionString);

            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Guid EDRID = (Guid)dt.Rows[i]["EDRID"];
                string TraceNumber = (string)dt.Rows[i]["TraceNumber"];
                string TransactionCode = (string)dt.Rows[i]["TransactionCode"];
                string DFIAccountNo = (string)dt.Rows[i]["DFIAccountNo"];
                string SendingBankRoutNo = (string)dt.Rows[i]["SendingBankRoutNo"];
                string ReceivingBankRoutNo = (string)dt.Rows[i]["ReceivingBankRoutNo"];
                double Amount = ParseData.StringToDouble(dt.Rows[i]["Amount"].ToString());
                //double Amount = dt.Rows[i]["Amount"];
                string IdNumber = (string)dt.Rows[i]["IdNumber"];
                string ReceiverName = (string)dt.Rows[i]["ReceiverName"];
                string PaymentInfo = (string)dt.Rows[i]["PaymentInfo"];
                string ServiceClassCode = (string)dt.Rows[i]["ServiceClassCode"];
                string SECC = (string)dt.Rows[i]["SECC"];
                DateTime SettlementJDate = DateTime.Parse(dt.Rows[i]["SettlementJDate"].ToString());
                DateTime EntryDateTransactionReceived = DateTime.Parse( dt.Rows[i]["EntryDateTransactionReceived"].ToString());

                string EntryDesc = (string)dt.Rows[i]["EntryDesc"];
                string CompanyId = (string)dt.Rows[i]["CompanyId"];
                string CompanyName = (string)dt.Rows[i]["CompanyName"];

                OracleParameter paramEDRID = new OracleParameter("EDRID", OracleType.VarChar);
                paramEDRID.Value = EDRID.ToString();
                paramEDRID.Direction = ParameterDirection.Input;

                OracleParameter paramTraceNumber = new OracleParameter("TRACENUMBER", OracleType.VarChar);
                paramTraceNumber.Value = TraceNumber;
                paramTraceNumber.Direction = ParameterDirection.Input;

                OracleParameter paramTransactionCode = new OracleParameter("TRANSACTIONCODE", OracleType.VarChar);
                paramTransactionCode.Value = TransactionCode;
                paramTransactionCode.Direction = ParameterDirection.Input;

                OracleParameter paramDFIAccountNo = new OracleParameter("DFIACCOUNTNO", OracleType.VarChar);
                paramDFIAccountNo.Value = DFIAccountNo;
                paramDFIAccountNo.Direction = ParameterDirection.Input;

                OracleParameter paramSendingBankRoutNo = new OracleParameter("SENDINGBANKROUTNO", OracleType.VarChar);
                paramSendingBankRoutNo.Value = SendingBankRoutNo;
                paramSendingBankRoutNo.Direction = ParameterDirection.Input;

                OracleParameter paramReceivingBankRoutNo = new OracleParameter("RECEIVINGBANKROUTNO", OracleType.VarChar);
                paramReceivingBankRoutNo.Value = ReceivingBankRoutNo;
                paramReceivingBankRoutNo.Direction = ParameterDirection.Input;

                OracleParameter paramAmount = new OracleParameter("AMOUNT", OracleType.Number);
                paramAmount.Value = Amount;
                paramAmount.Direction = ParameterDirection.Input;

                OracleParameter paramIdNumber = new OracleParameter("IDNUMBER", OracleType.VarChar);
                paramIdNumber.Value = IdNumber;
                paramIdNumber.Direction = ParameterDirection.Input;

                OracleParameter paramReceiverName = new OracleParameter("RECEIVERNAME", OracleType.VarChar);
                paramReceiverName.Value = ReceiverName;
                paramReceiverName.Direction = ParameterDirection.Input;

                OracleParameter paramPaymentInfo = new OracleParameter("PAYMENTINFO", OracleType.VarChar);
                paramPaymentInfo.Value = PaymentInfo;
                paramPaymentInfo.Direction = ParameterDirection.Input;

                OracleParameter paramServiceClassCode = new OracleParameter("SERVICECLASSCODE", OracleType.VarChar);
                paramServiceClassCode.Value = ServiceClassCode;
                paramServiceClassCode.Direction = ParameterDirection.Input;

                OracleParameter paramSECC = new OracleParameter("SECC", OracleType.VarChar);
                paramSECC.Value = SECC;
                paramSECC.Direction = ParameterDirection.Input;

                OracleParameter paramSettlementJDate = new OracleParameter("SETTLEMENTJDATE", OracleType.DateTime);
                paramSettlementJDate.Value = SettlementJDate;
                paramSettlementJDate.Direction = ParameterDirection.Input;

                OracleParameter paramEntryDateTransactionReceived = new OracleParameter("ENTRYDATETRANSACTIONRECEIVED", OracleType.DateTime);
                paramEntryDateTransactionReceived.Value = EntryDateTransactionReceived;
                paramEntryDateTransactionReceived.Direction = ParameterDirection.Input;

                OracleParameter paramEntryDesc = new OracleParameter("ENTRYDESC", OracleType.VarChar);
                paramEntryDesc.Value = EntryDesc;
                paramEntryDesc.Direction = ParameterDirection.Input;

                OracleParameter paramCompanyID = new OracleParameter("COMPANYID", OracleType.VarChar);
                paramCompanyID.Value = CompanyId;
                paramCompanyID.Direction = ParameterDirection.Input;

                OracleParameter paramCompanyName = new OracleParameter("COMPANYNAME", OracleType.VarChar);
                paramCompanyName.Value = CompanyName;
                paramCompanyName.Direction = ParameterDirection.Input;

                cmd.CommandText = "TBEFTN_SP_INSERT_INWARD_TRN";
                cmd.Parameters.Add(paramEDRID);
                cmd.Parameters.Add(paramTraceNumber);
                cmd.Parameters.Add(paramTransactionCode);
                cmd.Parameters.Add(paramDFIAccountNo);
                cmd.Parameters.Add(paramSendingBankRoutNo);
                cmd.Parameters.Add(paramReceivingBankRoutNo);
                cmd.Parameters.Add(paramAmount);
                cmd.Parameters.Add(paramIdNumber);
                cmd.Parameters.Add(paramReceiverName);
                cmd.Parameters.Add(paramPaymentInfo);
                cmd.Parameters.Add(paramServiceClassCode);
                cmd.Parameters.Add(paramSECC);
                cmd.Parameters.Add(paramSettlementJDate);
                cmd.Parameters.Add(paramEntryDateTransactionReceived);
                cmd.Parameters.Add(paramEntryDesc);
                cmd.Parameters.Add(paramCompanyID);
                cmd.Parameters.Add(paramCompanyName);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                //Only for test
                cmd.Parameters.Remove(paramEDRID);
                cmd.Parameters.Remove(paramTraceNumber);
                cmd.Parameters.Remove(paramTransactionCode);
                cmd.Parameters.Remove(paramDFIAccountNo);
                cmd.Parameters.Remove(paramSendingBankRoutNo);
                cmd.Parameters.Remove(paramReceivingBankRoutNo);
                cmd.Parameters.Remove(paramAmount);
                cmd.Parameters.Remove(paramIdNumber);
                cmd.Parameters.Remove(paramReceiverName);
                cmd.Parameters.Remove(paramPaymentInfo);
                cmd.Parameters.Remove(paramServiceClassCode);
                cmd.Parameters.Remove(paramSECC);
                cmd.Parameters.Remove(paramSettlementJDate);
                cmd.Parameters.Remove(paramEntryDateTransactionReceived);
                cmd.Parameters.Remove(paramEntryDesc);
                cmd.Parameters.Remove(paramCompanyID);
                cmd.Parameters.Remove(paramCompanyName);
            }

            conn.Dispose();
        }

        public int SendOutwardReturnFromPIBStoFlora()
        {
            DataTable dtOutwardReturnFromPIBS = GetOutwardReturnFromPIBS();
            int noOfReturnSentToFlora = SendOutwardReturnToFlora(dtOutwardReturnFromPIBS);
            int noOfItemReturnItemUpdatedInPIBS = UpdateOutwardReturnResultFromPIBS(dtOutwardReturnFromPIBS);

            if (noOfItemReturnItemUpdatedInPIBS == noOfReturnSentToFlora)
            {
                return noOfItemReturnItemUpdatedInPIBS;
            }
            else
            {
                return -1;
            }
        }

        private int UpdateOutwardReturnResultFromPIBS(DataTable dtOutwardReturnFromPIBS)
        {
            PIBSDB pibsDB = new PIBSDB();
            string oracleConnectionString = ConfigurationManager.AppSettings["PIBSConnectionString"];
            int returnCount = 0;
            foreach (DataRow row in dtOutwardReturnFromPIBS.Rows)
            {
                string EDRID = row["EDRID"].ToString();
                int totalRowUpdated = pibsDB.UpdateOutwardReturnPulledFromPIBS(oracleConnectionString, EDRID);
                returnCount += totalRowUpdated;
            }

            return returnCount;
        }

        private DataTable GetOutwardReturnFromPIBS()
        {
            PIBSDB pibsDB = new PIBSDB();

            DataTable dtOutwardReturn = pibsDB.GetOutwardReturnFromPIBS();

            return dtOutwardReturn;
        }

        private int SendOutwardReturnToFlora(DataTable dtOutwardReturn)
        {
            PIBSDB pibsDB = new PIBSDB();
            int returnCount = 0;
            foreach (DataRow row in dtOutwardReturn.Rows)
            {
                Guid EDRID = new Guid(row["EDRID"].ToString()); //(Guid)row["EDRID"];
                string ReturnCode = row["ReturnCode"].ToString().Trim();
                string ReturnReasonDesc = row["ReturnReasonDesc"].ToString().Trim();

                pibsDB.InsertImportedReturnSent_fromPIBS(EDRID, ReturnCode, ReturnReasonDesc);
                returnCount++;
            }

            return returnCount;
        }

        public int UpdateApprovedInwardTransactionByPIBSToFlora()
        {
            DataTable dtApprovedInwardTransactionFromPIBS = GetApprovedInwardTransactionFromPIBSToFlora();
            int noOfApprovedInwardTransactionToFlora = SendUpdateApprovedInwardTransactionToFlora(dtApprovedInwardTransactionFromPIBS);
            int noOfItemInwardTransactionApprovedUpdatedInPIBS = UpdateApprovedInwardTransactionResultFromPIBS();

            if (noOfApprovedInwardTransactionToFlora == noOfItemInwardTransactionApprovedUpdatedInPIBS)
            {
                return noOfApprovedInwardTransactionToFlora;
            }
            else
            {
                return -1;
            }
        }

        private DataTable GetApprovedInwardTransactionFromPIBSToFlora()
        {
            PIBSDB pibsDB = new PIBSDB();

            DataTable dtApprovedInwardTransaction = pibsDB.GetApprovedInwardTransactionFromPIBS();

            return dtApprovedInwardTransaction;
        }

        private int SendUpdateApprovedInwardTransactionToFlora(DataTable dtApprovedInwardTransaction)
        {
            PIBSDB pibsDB = new PIBSDB();
            int approvedCount = 0;
            foreach (DataRow row in dtApprovedInwardTransaction.Rows)
            {
                Guid EDRID = new Guid(row["EDRID"].ToString()); //(Guid)row["EDRID"];
                int StatusID = ParseData.StringToInt(row["STATUSID"].ToString().Trim());

                pibsDB.UpdateApprovedReceivedEDRFromPIBS(StatusID, EDRID);
                approvedCount++;
            }

            return approvedCount;
        }

        private int UpdateApprovedInwardTransactionResultFromPIBS()
        {
            PIBSDB pibsDB = new PIBSDB();
            int totalRowUpdated = pibsDB.UpdateApprovedInwardTransactionPulledFromPIBS();
            return totalRowUpdated;
        }


    }
}
