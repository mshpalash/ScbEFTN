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
    public class UserHistoryDB
    {
        public void InsertUserHistory( int EnteredBy,
                                                string IPAddress,
                                                string ChangeType,
                                                int UserID,
                                                int UserRole,
                                                int BranchID,
                                                string UserName,
                                                int DepartmentID,
                                                string ContactNo,
                                                string LoginID,
                                                string Password,
                                                string UserStatus
                                              )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertUserHistory", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEnteredBy = new SqlParameter("@EnteredBy", SqlDbType.Int);
            parameterEnteredBy.Value = EnteredBy;
            myCommand.Parameters.Add(parameterEnteredBy);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            SqlParameter parameterChangeType = new SqlParameter("@ChangeType", SqlDbType.VarChar);
            parameterChangeType.Value = ChangeType;
            myCommand.Parameters.Add(parameterChangeType);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterUserRole = new SqlParameter("@UserRole", SqlDbType.Int);
            parameterUserRole.Value = UserRole;
            myCommand.Parameters.Add(parameterUserRole);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            parameterUserName.Value = UserName;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterDepartment = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartment.Value = DepartmentID;
            myCommand.Parameters.Add(parameterDepartment);

            SqlParameter parameterContactNo = new SqlParameter("@ContactNo", SqlDbType.VarChar);
            parameterContactNo.Value = ContactNo;
            myCommand.Parameters.Add(parameterContactNo);

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.VarChar);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterPassword = new SqlParameter("@Password", SqlDbType.VarChar);
            parameterPassword.Value = Password;
            myCommand.Parameters.Add(parameterPassword);

            SqlParameter parameterUserStatus = new SqlParameter("@UserStatus", SqlDbType.VarChar);
            parameterUserStatus.Value = UserStatus;
            myCommand.Parameters.Add(parameterUserStatus);


            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }

        public DataTable GetLastPasswordChangeHistoryByUserID(int UserID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetLastPasswordChangeHistoryByUserID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetUnsuccesfullLoginsByUserID(int UserID, DateTime LastLoginTime)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetUnsuccesfullLogins", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterLastLoginTime = new SqlParameter("@LastLoginTime", SqlDbType.DateTime);
            parameterLastLoginTime.Value = LastLoginTime;
            myAdapter.SelectCommand.Parameters.Add(parameterLastLoginTime);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetUserHistoryByDate(string BeginDate, string EndDate)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetUserHistoryByDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBeginDate = new SqlParameter("@BeginDate", SqlDbType.VarChar);
            parameterBeginDate.Value = BeginDate;
            myAdapter.SelectCommand.Parameters.Add(parameterBeginDate);

            SqlParameter parameterEndDate = new SqlParameter("@EndDate", SqlDbType.VarChar);
            parameterEndDate.Value = EndDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEndDate);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }
    }
}
