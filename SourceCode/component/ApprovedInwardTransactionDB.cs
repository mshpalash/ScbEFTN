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
    public class ApprovedInwardTransactionDB
    {

        //public SqlDataReader GetReceivedEDRApprovedByMakerForChecker()
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetReceivedEDR_ApprovedByMaker_ForChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetReceivedEDRApprovedByMakerForChecker(int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_ApprovedByMaker_ForChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateReceivedEDRStatus(int StatusID,
                                            Guid EDRID
                                           )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_ReceivedEDR_Status", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);



            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();           
        }

        public void UpdateReceivedEDRStatusApprovedByChecker(Guid EDRID, int ApprovedBy)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_ReceivedEDR_Status_ApprovedByChecker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myCommand.Parameters.Add(parameterApprovedBy);

            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
        }


        public void UpdateReceivedEDRStatusByChecker_FCUBS(Guid EDRID, int ApprovedBy, string FloraReference, string CBSReference)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_ReceivedEDR_Status_ByChecker_FCUBS", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myCommand.Parameters.Add(parameterApprovedBy);

            SqlParameter parameterCBSReference = new SqlParameter("@CBSReference", SqlDbType.VarChar, 50);
            parameterCBSReference.Value = CBSReference;
            myCommand.Parameters.Add(parameterCBSReference);

            SqlParameter parameterFloraReference = new SqlParameter("@FloraReference", SqlDbType.VarChar, 50);
            parameterFloraReference.Value = FloraReference;
            myCommand.Parameters.Add(parameterFloraReference);

            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
        }
    }
}
