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
    public class ItemCountDB
    {
        public SqlDataReader GetCountsForInmaker(int BranchID, int DepartmentID)
        {
            string spName = string.Empty;

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("0"))
            {
                spName = "EFT_GetCountsforInmaker";
            }
            else
            {
                spName = "EFT_Branchwise_GetCountsforInmaker";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand(spName, myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myCommand.Parameters.Add(parameterBranchID);

                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myCommand.Parameters.Add(parameterDepartmentID);
            }


            if (ConfigurationManager.AppSettings["BranchWise"].Equals("0"))
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myCommand.Parameters.Add(parameterDepartmentID);
            }

            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        public SqlDataReader GetCountsForInChecker(int BranchID, int DepartmentID)
        {
            string spName = string.Empty;

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("0"))
            {
                spName = "EFT_GetCountsforInChecker";
            }
            else
            {
                spName = "EFT_Branchwise_GetCountsforInChecker";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand(spName, myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myCommand.Parameters.Add(parameterBranchID);
            }

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("0"))
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myCommand.Parameters.Add(parameterDepartmentID);
            }

            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        public SqlDataReader GetCountsForEBBSChecker(int BranchID, int DepartmentID)
        {
            string spName = string.Empty;

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("0"))
            {
                spName = "EFT_GetCountsforEBBSChecker";
            }
            else
            {
                spName = "EFT_Branchwise_GetCountsforEBBSChecker";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand(spName, myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("1"))
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myCommand.Parameters.Add(parameterBranchID);
            }

            if (ConfigurationManager.AppSettings["BranchWise"].Equals("0"))
            {
                SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameterDepartmentID.Value = DepartmentID;
                myCommand.Parameters.Add(parameterDepartmentID);
            }

            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
    }
}
