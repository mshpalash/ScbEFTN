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
    public class EBBSCheckerDishonorSentDB
    {
        public DataTable GetSentDishonorForEBBSChecker(string EffectiveEntryDate, string EndingEffectiveEntryDate, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentDishonor_ForEBBSChecker", myConnection);
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

        public DataTable GetSentDishonorForEBBSCheckerForDebit(string EffectiveEntryDate, string EndingEffectiveEntryDate, int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentDishonor_ForEBBSChecker_ForDebit", myConnection);
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

        //public SqlDataReader GetSentDishonorForEBBSChecker(string EffectiveEntryDate)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetSentDishonor_ForEBBSChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterEffectiveEntryDate = new SqlParameter("@EffectiveEntryDate", SqlDbType.VarChar);
        //    parameterEffectiveEntryDate.Value = EffectiveEntryDate;
        //    myCommand.Parameters.Add(parameterEffectiveEntryDate);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        //public SqlDataReader GetSentDishonorSentForEBBSCheckerByDishonorSentID(Guid DishonoredID)
        //{
        //    // Must enter your connection string
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must write your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetSentDishonorSent_ForEBBSChecker_byDishonorSentID", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterDishonoredID = new SqlParameter("@DishonoredID", SqlDbType.UniqueIdentifier);
        //    parameterDishonoredID.Value = DishonoredID;
        //    myCommand.Parameters.Add(parameterDishonoredID);


        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //    return dr;
        //}

        public DataTable GetSentDishonorSentForEBBSCheckerByDishonorSentID(Guid DishonoredID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentDishonorSent_ForEBBSChecker_byDishonorSentID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterDishonoredID = new SqlParameter("@DishonoredID", SqlDbType.UniqueIdentifier);
            parameterDishonoredID.Value = DishonoredID;
            myAdapter.SelectCommand.Parameters.Add(parameterDishonoredID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateDishonorSentStatusApprovedByEBBSChecker(Guid DishonoredID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_DishonorSent_Status_ApprovedByEBBSChecker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDishonoredID = new SqlParameter("@DishonoredID", SqlDbType.UniqueIdentifier);
            parameterDishonoredID.Value = DishonoredID;
            myCommand.Parameters.Add(parameterDishonoredID);


            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public void UpdateDishonorSentStatusRejectedByEBBSChecker(Guid DishonoredID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_DishonorSent_Status_RejectedByEBBSChecker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDishonoredID = new SqlParameter("@DishonoredID", SqlDbType.UniqueIdentifier);
            parameterDishonoredID.Value = DishonoredID;
            myCommand.Parameters.Add(parameterDishonoredID);

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
        }
    }
}
