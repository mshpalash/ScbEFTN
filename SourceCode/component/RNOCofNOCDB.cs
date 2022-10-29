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
    public class RNOCofNOCDB
    {
        //public SqlDataReader GetRNOCofReceivedNOC()
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_ReceivedNOC_RNOC_ForChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetRNOCofReceivedNOC()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ReceivedNOC_RNOC_ForChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateSentRNOCStatus(int StatusID, Guid RNOCID, int ApprovedBy)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_SentRNOC_Status_ByChecker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterRNOCID = new SqlParameter("@RNOCID", SqlDbType.UniqueIdentifier);
            parameterRNOCID.Value = RNOCID;
            myCommand.Parameters.Add(parameterRNOCID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myCommand.Parameters.Add(parameterApprovedBy);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
            myConnection.Close();
            myConnection.Dispose();
        }

        //public SqlDataReader GetRNOCofNOCRejetedByChecker()
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetRNOCSent_ByCheckerRejection", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetRNOCofNOCRejetedByChecker(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetRNOCSent_ByCheckerRejection", myConnection);
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

        public void UpdateRNOCSentStatusByMaker(int StatusID,
                                                         Guid NOCID,
                                                         string RefusedCORCode,
                                                         string CorrectedData)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_RNOCSent_Status_ByMaker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
            parameterNOCID.Value = NOCID;
            myCommand.Parameters.Add(parameterNOCID);

            SqlParameter parameterRefusedCORCode = new SqlParameter("@RefusedCORCode", SqlDbType.NVarChar, 3);
            parameterRefusedCORCode.Value = RefusedCORCode;
            myCommand.Parameters.Add(parameterRefusedCORCode);

            SqlParameter parameterCorrectedData = new SqlParameter("@CorrectedData", SqlDbType.NVarChar, 30);
            parameterCorrectedData.Value = CorrectedData;
            myCommand.Parameters.Add(parameterCorrectedData);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
            myConnection.Close();
            myConnection.Dispose();
        }


        public void CancelRNOCAndApproveAsNOC(int StatusID, Guid NOCID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_CancelRNOCRejectedByCheckerAndApprove_ByMaker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myCommand.Parameters.Add(parameterStatusID); 
            
            SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
            parameterNOCID.Value = NOCID;
            myCommand.Parameters.Add(parameterNOCID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
            myConnection.Close();
            myConnection.Dispose();
        }
    }
}
