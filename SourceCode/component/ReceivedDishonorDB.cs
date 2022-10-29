using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class ReceivedDishonorDB
    {
        public void InsertDishonorReceived( string ReturnTraceNumber,
                                            string DishonorReason,
                                            string AddendaInfo,
                                            DateTime SettlementJDate,
                                            string TraceNumber,
                                            string currency,
                                            int session)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertDishonorReceived", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDishonoredID = new SqlParameter("@DishonoredID", SqlDbType.UniqueIdentifier);
            parameterDishonoredID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterDishonoredID);

            SqlParameter parameterReturnTraceNumber = new SqlParameter("@ReturnTraceNumber", SqlDbType.NVarChar, 15);
            parameterReturnTraceNumber.Value = ReturnTraceNumber;
            myCommand.Parameters.Add(parameterReturnTraceNumber);

            SqlParameter parameterDishonorReason = new SqlParameter("@DishonorReason", SqlDbType.NVarChar, 3);
            parameterDishonorReason.Value = DishonorReason;
            myCommand.Parameters.Add(parameterDishonorReason);

            SqlParameter parameterAddendaInfo = new SqlParameter("@AddendaInfo", SqlDbType.NVarChar, 15);
            parameterAddendaInfo.Value = AddendaInfo;
            myCommand.Parameters.Add(parameterAddendaInfo);

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementJDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
            parameterTraceNumber.Value = TraceNumber;
            myCommand.Parameters.Add(parameterTraceNumber);


            SqlParameter paramCurrency = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            paramCurrency.Value = currency;
            myCommand.Parameters.Add(paramCurrency);

            SqlParameter paramSession = new SqlParameter("@Session", SqlDbType.Int);
            paramSession.Value = session;
            myCommand.Parameters.Add(paramSession);

            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
            //return dr;
        }

        //public SqlDataReader GetReceivedDishonor()
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetReceivedDishonor", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader();
        //    return dr;
        //}

        public DataTable GetReceivedDishonor()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedDishonor", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public SqlDataReader GetSentEDRByReturnTraceNoForDishonorReceived(string ReturnTraceNumber)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_GetSentEDR_By_ReturnTraceNo_For_DishonorReceived", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterReturnTraceNumber = new SqlParameter("@ReturnTraceNumber", SqlDbType.NVarChar, 15);
            parameterReturnTraceNumber.Value = ReturnTraceNumber;
            myCommand.Parameters.Add(parameterReturnTraceNumber);

            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader();
            return dr;
        }

        public void UpdateDishonorStatus(int StatusID,
                                            Guid DishonoredID,
                                            string ContestedDishonoredCode,
                                            int CreatedBy,
                                            int ApprovedBy)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_ReceivedDishonor_Status", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterDishonoredID = new SqlParameter("@DishonoredID", SqlDbType.UniqueIdentifier);
            parameterDishonoredID.Value = DishonoredID;
            myCommand.Parameters.Add(parameterDishonoredID);

            SqlParameter parameterContestedID = new SqlParameter("@ContestedID", SqlDbType.UniqueIdentifier);
            parameterContestedID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterContestedID);

            SqlParameter parameterContestedDishonoredCode = new SqlParameter("@ContestedDishonoredCode", SqlDbType.NVarChar, 3);
            parameterContestedDishonoredCode.Value = ContestedDishonoredCode;
            myCommand.Parameters.Add(parameterContestedDishonoredCode);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myCommand.Parameters.Add(parameterApprovedBy);


            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
            //return dr;
        }
    }
}
