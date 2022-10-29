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
    public class ApprovedNOCDB
    {
        //public SqlDataReader GetApprovedReceivedNOC()
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_ReceivedNOC_Approved_ForChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetApprovedReceivedNOC(int DepartmentID)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ReceivedNOC_Approved_ForChecker", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    parameterDepartmentID.Value = DepartmentID;
                    myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

                    DataTable dt = new DataTable();

                    myConnection.Open();
                    myAdapter.Fill(dt);
                    myConnection.Close();
                    myAdapter.Dispose();
                    myConnection.Dispose();

                    return dt;
                }
            }
        }


        public DataTable GetApprovedReceivedNOCForDebit(int DepartmentID)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ReceivedNOC_Approved_ForCheckerForDebit", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    parameterDepartmentID.Value = DepartmentID;
                    myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

                    DataTable dt = new DataTable();

                    myConnection.Open();
                    myAdapter.Fill(dt);
                    myConnection.Close();
                    myAdapter.Dispose();
                    myConnection.Dispose();

                    return dt;
                }
            }
        }

        //EFT_Update_ReceivedNOC_Status_ByChecker
        //public SqlDataReader UpdateReceivedNOCStatus(int StatusID, Guid NOCID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_Update_ReceivedNOC_Status_ByChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
        //    parameterStatusID.Value = StatusID;
        //    myCommand.Parameters.Add(parameterStatusID);

        //    SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
        //    parameterNOCID.Value = NOCID;
        //    myCommand.Parameters.Add(parameterNOCID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable UpdateReceivedNOCStatus(int StatusID, Guid NOCID, int ApprovedBy)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Update_ReceivedNOC_Status_ByChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myAdapter.SelectCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterNOCID = new SqlParameter("@NOCID", SqlDbType.UniqueIdentifier);
            parameterNOCID.Value = NOCID;
            myAdapter.SelectCommand.Parameters.Add(parameterNOCID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterApprovedBy);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        //public SqlDataReader GetRejetedListOfApprovedNOC()
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetReceivedNOC_ByCheckerRejection", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetRejetedListOfApprovedNOC(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedNOC_ByCheckerRejection", myConnection);
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
    }
}
