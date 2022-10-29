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
    public class ReceivedNOCDB
    {
        public void InsertReceivedNOC(string OrgTraceNumber,
                                      string TraceNumber,
                                      string ChangeCode,
                                      string CorrectedData,
                                      DateTime SettlementJDate,
                                      string XMLFileName)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_InsertNOCReceived", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
            parameterNOCID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterNOCID);

            SqlParameter parameterOrgTraceNumber = new SqlParameter("@OrgTraceNumber", SqlDbType.NVarChar, 15);
            parameterOrgTraceNumber.Value = OrgTraceNumber;
            myCommand.Parameters.Add(parameterOrgTraceNumber);

            SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
            parameterTraceNumber.Value = TraceNumber;
            myCommand.Parameters.Add(parameterTraceNumber);

            SqlParameter parameterChangeCode = new SqlParameter("@ChangeCode", SqlDbType.NVarChar, 3);
            parameterChangeCode.Value = ChangeCode;
            myCommand.Parameters.Add(parameterChangeCode);

            SqlParameter parameterCorrectedData = new SqlParameter("@CorrectedData", SqlDbType.NVarChar, 30);
            parameterCorrectedData.Value = CorrectedData;
            myCommand.Parameters.Add(parameterCorrectedData);

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementJDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterXMLFileName = new SqlParameter("@XMLFileName", SqlDbType.NVarChar, 200);
            parameterXMLFileName.Value = XMLFileName;
            myCommand.Parameters.Add(parameterXMLFileName);

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        //public SqlDataReader GetReceivedNOC()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetReceivedNOC", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myCommand.CommandType = CommandType.StoredProcedure;
        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetReceivedNOC(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedNOC", myConnection);
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



        public DataTable GetReceivedNOCForDebit(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedNOCForDebit", myConnection);
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

        //public SqlDataReader GetReceivedNOCByStatusID(int StatusID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_GetReceivedNOCByStatusID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
        //    parameterStatusID.Value = StatusID;
        //    myCommand.Parameters.Add(parameterStatusID);

        //    myCommand.CommandType = CommandType.StoredProcedure;
        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetReceivedNOCByStatusID(int StatusID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedNOCByStatusID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myAdapter.SelectCommand.Parameters.Add(parameterStatusID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader UpdateReceivedNOCStatus(int StatusID,
        //                                            Guid NOCID,
        //                                            string RefusedCORCode,
        //                                            int CreatedBy,
        //                                            int ApprovedBy,
        //                                            string CorrectedData)
        //{

        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_Update_ReceivedNOC_Status", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
        //    parameterStatusID.Value = StatusID;
        //    myCommand.Parameters.Add(parameterStatusID);

        //    SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
        //    parameterNOCID.Value = NOCID;
        //    myCommand.Parameters.Add(parameterNOCID);

        //    SqlParameter parameterRNOCID = new SqlParameter("@RNOCID", SqlDbType.UniqueIdentifier);
        //    parameterRNOCID.Direction = ParameterDirection.Output;
        //    myCommand.Parameters.Add(parameterRNOCID);

        //    SqlParameter parameterRefusedCORCode = new SqlParameter("@RefusedCORCode", SqlDbType.NVarChar, 3);
        //    parameterRefusedCORCode.Value = RefusedCORCode;
        //    myCommand.Parameters.Add(parameterRefusedCORCode);

        //    SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
        //    parameterCreatedBy.Value = CreatedBy;
        //    myCommand.Parameters.Add(parameterCreatedBy);

        //    SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
        //    parameterApprovedBy.Value = ApprovedBy;
        //    myCommand.Parameters.Add(parameterApprovedBy);

        //    SqlParameter parameterCorrectedData = new SqlParameter("@CorrectedData", SqlDbType.NVarChar, 30);
        //    parameterCorrectedData.Value = CorrectedData;
        //    myCommand.Parameters.Add(parameterCorrectedData);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public void UpdateReceivedNOCStatus(int StatusID,
                                                    Guid NOCID,
                                                    string RefusedCORCode,
                                                    int CreatedBy,
                                                    int ApprovedBy,
                                                    string CorrectedData)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Update_ReceivedNOC_Status", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myAdapter.SelectCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
            parameterNOCID.Value = NOCID;
            myAdapter.SelectCommand.Parameters.Add(parameterNOCID);

            SqlParameter parameterRNOCID = new SqlParameter("@RNOCID", SqlDbType.UniqueIdentifier);
            parameterRNOCID.Direction = ParameterDirection.Output;
            myAdapter.SelectCommand.Parameters.Add(parameterRNOCID);

            SqlParameter parameterRefusedCORCode = new SqlParameter("@RefusedCORCode", SqlDbType.NVarChar, 3);
            parameterRefusedCORCode.Value = RefusedCORCode;
            myAdapter.SelectCommand.Parameters.Add(parameterRefusedCORCode);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterApprovedBy);

            SqlParameter parameterCorrectedData = new SqlParameter("@CorrectedData", SqlDbType.NVarChar, 30);
            parameterCorrectedData.Value = CorrectedData;
            myAdapter.SelectCommand.Parameters.Add(parameterCorrectedData);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
        }

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

        //public SqlDataReader GetSentEDRByTraceNoForNOCReceived(string TraceNumber)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetSentEDR_By_TraceNo_For_NOCReceived", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterTraceNumber = new SqlParameter("@TraceNumber", SqlDbType.NVarChar, 15);
        //    parameterTraceNumber.Value = TraceNumber;
        //    myCommand.Parameters.Add(parameterTraceNumber);

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        //Creation Date: 04-06-2011

        public DataTable ReceivedNOCApprovedForEBBSCheckerBySettlementDate(string NOCReceivedSettlementDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ReceivedNOC_Approved_ForEBBSCheckerBySettlementDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterNOCReceivedSettlementDate = new SqlParameter("@NOCReceivedSettlementDate", SqlDbType.VarChar);
            parameterNOCReceivedSettlementDate.Value = NOCReceivedSettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterNOCReceivedSettlementDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}
