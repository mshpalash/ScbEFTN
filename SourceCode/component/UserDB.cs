using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using EFTN.Utility;

namespace FloraSoft
{
    public class UserInfo
    {
        public string UserID;
        public string RoleID;
        public string BranchID;
        public string BankCode;
        public string UserName;
        public string RoleName;
        public string BranchName;
        public string BankName;
        public string DepartmentID;
        public string DepartmentName;
        public string LoginID;
        public string LastLoginTime;
        public string LoginMsg;
    }

    public class PasswordPolicy
    {
        public int MinPasswordLength;
        public int MinNumberOfAlphabets;
        public int MinNumberOfNumerics;
        public int MinNumberOfSpecialChars;
        public int NumberOfLastPasswordsToAvoid;
        public int MinNumberOfUpperChar;
        public int MinNumberOfLowerChar;
        public int ExpireDuration;
    }

    public class UserDB
    {
        public UserInfo SAMLLogin(string LoginID, string IPAddress, string DeviceName)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_UserSamlLogin", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.NVarChar, 50);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            SqlParameter parameterDeviceName = new SqlParameter("@DeviceName", SqlDbType.VarChar);
            parameterDeviceName.Value = DeviceName;
            myCommand.Parameters.Add(parameterDeviceName);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoleID);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            parameterBranchID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
            parameterUserName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterRoleName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
            parameterRoleName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoleName);

            SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.NVarChar, 50);
            parameterBranchName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBranchName);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NVarChar, 3);
            parameterBankCode.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterBankName = new SqlParameter("@BankName", SqlDbType.NVarChar, 50);
            parameterBankName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBankName);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterDepartmentName = new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 50);
            parameterDepartmentName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterDepartmentName);

            SqlParameter parameterUserRoleID = new SqlParameter("@UserRoleID", SqlDbType.Int);
            parameterUserRoleID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserRoleID);

            SqlParameter parameterLastLoginTime = new SqlParameter("@LastLoginTime", SqlDbType.DateTime);
            parameterLastLoginTime.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterLastLoginTime);

            SqlParameter parameterLoginMsg = new SqlParameter("@LoginMsg", SqlDbType.VarChar, 100);
            parameterLoginMsg.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterLoginMsg);

            //SqlParameter parameterLastLogInDeviceName = new SqlParameter("@LastLogInDeviceName", SqlDbType.VarChar, 50);
            //parameterLastLogInDeviceName.Direction = ParameterDirection.Output;
            //myCommand.Parameters.Add(parameterLastLogInDeviceName);


            myConnection.Open();
            myCommand.ExecuteNonQuery();

            UserInfo ui = new UserInfo();
            ui.UserID = parameterUserID.Value.ToString();
            ui.RoleID = parameterRoleID.Value.ToString();
            ui.BranchID = parameterBranchID.Value.ToString();
            ui.BankCode = (string)parameterBankCode.Value;
            ui.UserName = (string)parameterUserName.Value;
            ui.RoleName = (string)parameterRoleName.Value;
            ui.BranchName = (string)parameterBranchName.Value;
            ui.BankName = (string)parameterBankName.Value;
            ui.DepartmentID = parameterDepartmentID.Value.ToString();
            ui.DepartmentName = parameterDepartmentName.Value.ToString();
            ui.LoginID = LoginID;
            ui.LastLoginTime = parameterLastLoginTime.Value.ToString();
            ui.LoginMsg = (string)parameterLoginMsg.Value;

            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

            return ui;
        }
        
        public static void Logout(string LoginID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_Logout", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserId = new SqlParameter("@LoginID", SqlDbType.VarChar);
            paramUserId.Value = LoginID;
            myCommand.Parameters.Add(paramUserId);

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }

        public UserInfo Login(string LoginID, string Password, string IPAddress)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_UserLogin", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.NVarChar, 50);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
            parameterPassword.Value = Encrypt(Password);
            myCommand.Parameters.Add(parameterPassword);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoleID);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            parameterBranchID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
            parameterUserName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterRoleName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
            parameterRoleName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoleName);

            SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.NVarChar, 50);
            parameterBranchName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBranchName);

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.NVarChar, 3);
            parameterBankCode.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBankCode);

            SqlParameter parameterBankName = new SqlParameter("@BankName", SqlDbType.NVarChar, 50);
            parameterBankName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBankName);
            
            SqlParameter parameterDepartmentID  = new SqlParameter("@DepartmentID", SqlDbType.Int); 
            parameterDepartmentID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterDepartmentName  = new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 50); 
            parameterDepartmentName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterDepartmentName);

            SqlParameter parameterUserRoleID = new SqlParameter("@UserRoleID", SqlDbType.Int);
            parameterUserRoleID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserRoleID);

            SqlParameter parameterLastLoginTime = new SqlParameter("@LastLoginTime", SqlDbType.DateTime);
            parameterLastLoginTime.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterLastLoginTime);

            SqlParameter parameterLoginMsg = new SqlParameter("@LoginMsg", SqlDbType.VarChar,50);
            parameterLoginMsg.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterLoginMsg);
            

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            UserInfo ui = new UserInfo();
            ui.UserID           = parameterUserID.Value.ToString();
            ui.RoleID           = parameterRoleID.Value.ToString();
            ui.BranchID         = parameterBranchID.Value.ToString();
            ui.BankCode         = (string)parameterBankCode.Value;
            ui.UserName         = (string)parameterUserName.Value;
            ui.RoleName         = (string) parameterRoleName.Value;
            ui.BranchName       = (string) parameterBranchName.Value;
            ui.BankName         = (string)parameterBankName.Value;
            ui.DepartmentID     = parameterDepartmentID.Value.ToString();
            ui.DepartmentName   = parameterDepartmentName.Value.ToString();
            ui.LoginID          = LoginID;
            ui.LastLoginTime    = parameterLastLoginTime.Value.ToString();
            ui.LoginMsg         = parameterLoginMsg.Value.ToString();

            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

            return ui;
        }
        public SqlDataReader GetAllUsers(string UserStatus,int IsPending)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_GetUsers", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserStatus = new SqlParameter("@UserStatus", SqlDbType.VarChar, 8);
            parameterUserStatus.Value = UserStatus;
            myCommand.Parameters.Add(parameterUserStatus);

            SqlParameter parameterIsPending = new SqlParameter("@IsPending", SqlDbType.Int);
            parameterIsPending.Value = IsPending;
            myCommand.Parameters.Add(parameterIsPending);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result; 
        }


        public DataTable GetAllUsersforRpt(string UserStatus,int IsPending)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
  
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_GetUsers", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

  
            SqlParameter parameterUserStatus = new SqlParameter("@UserStatus", SqlDbType.VarChar, 8);
            parameterUserStatus.Value = UserStatus;
            myAdpater.SelectCommand.Parameters.Add(parameterUserStatus);

            SqlParameter parameterIsPending = new SqlParameter("@IsPending", SqlDbType.Int);
            parameterIsPending.Value = IsPending;
            myAdpater.SelectCommand.Parameters.Add(parameterIsPending);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }
        public int InsertUser(String UserName, String Department, String ContactNo, String LoginID, String Password, int UserRole, int branchID, int EnteredBy, string IPAddress, int DepartmentID, bool IsGeneric)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_InsertUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
            parameterUserName.Value = UserName;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterDepartment = new SqlParameter("@Department", SqlDbType.NVarChar, 50);
            parameterDepartment.Value = Department;
            myCommand.Parameters.Add(parameterDepartment);

            SqlParameter parameterContactNo = new SqlParameter("@ContactNo", SqlDbType.NVarChar, 50);
            parameterContactNo.Value = ContactNo;
            myCommand.Parameters.Add(parameterContactNo);

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.NVarChar, 50);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
            parameterPassword.Value = Encrypt(Password);
            myCommand.Parameters.Add(parameterPassword);

            SqlParameter parameterUserRole = new SqlParameter("@UserRole", SqlDbType.NVarChar, 50);
            parameterUserRole.Value = UserRole;
            myCommand.Parameters.Add(parameterUserRole);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = branchID;
            myCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterEnteredBy = new SqlParameter("@EnteredBy", SqlDbType.Int, 4);
            parameterEnteredBy.Value = EnteredBy;
            myCommand.Parameters.Add(parameterEnteredBy); 
            
            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.NVarChar, 40);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterIsGeneric = new SqlParameter("@IsGeneric ", SqlDbType.Bit);
            parameterIsGeneric.Value = IsGeneric;
            myCommand.Parameters.Add(parameterIsGeneric);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            return (int)parameterUserID.Value;
        }

        public void UpdateUser(int UserID, String UserName, String Department, String ContactNo, String LoginID, int UserRole, int branchID, int EnteredBy, string IPAddress, int DepartmentID,int IsPending)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_UpdateUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.NVarChar, 50);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
            parameterUserName.Value = UserName;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterDepartment = new SqlParameter("@Department", SqlDbType.NVarChar, 50);
            parameterDepartment.Value = Department;
            myCommand.Parameters.Add(parameterDepartment);

            SqlParameter parameterContactNo = new SqlParameter("@ContactNo", SqlDbType.NVarChar, 50);
            parameterContactNo.Value = ContactNo;
            myCommand.Parameters.Add(parameterContactNo);

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.NVarChar, 50);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterUserRole = new SqlParameter("@UserRole", SqlDbType.NVarChar, 50);
            parameterUserRole.Value = UserRole;
            myCommand.Parameters.Add(parameterUserRole);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = branchID;
            myCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterEnteredBy = new SqlParameter("@EnteredBy", SqlDbType.Int, 4);
            parameterEnteredBy.Value = EnteredBy;
            myCommand.Parameters.Add(parameterEnteredBy);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.NVarChar, 40);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            SqlParameter parameterIsPending = new SqlParameter("@IsPending", SqlDbType.Int);
            parameterIsPending.Value = IsPending;
            myCommand.Parameters.Add(parameterIsPending);            

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

        }

        public void UpdateUserbyAdminChecker(int UserID, string UserStatus)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_UpdateUserbyAdminChecker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.NVarChar, 50);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);



            SqlParameter parameterUserStatus = new SqlParameter("@UserStatus", SqlDbType.NVarChar, 50);
            parameterUserStatus.Value = UserStatus;
            myCommand.Parameters.Add(parameterUserStatus);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

        }
        public void ChangeUserStatus(int UserID, string UserStatus, int EnteredBy, string IPAddress)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_ChangeUserStatus", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.NVarChar, 50);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterUserStatus = new SqlParameter("@UserStatus", SqlDbType.NVarChar, 50);
            parameterUserStatus.Value = UserStatus;
            myCommand.Parameters.Add(parameterUserStatus);

            SqlParameter parameterEnteredBy = new SqlParameter("@EnteredBy", SqlDbType.Int,4);
            parameterEnteredBy.Value = EnteredBy;
            myCommand.Parameters.Add(parameterEnteredBy);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.NVarChar, 40);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

        }
        public SqlDataReader GetAllUsers(int BankID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_GetUsers", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBankID = new SqlParameter("@BankID", SqlDbType.Int, 4);
            parameterBankID.Value = BankID;
            myCommand.Parameters.Add(parameterBankID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public SqlDataReader GetUserList()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_GetUserList", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public SqlDataReader GetSingleUser(int userID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_GetSingleUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.NVarChar);
            parameterUserID.Value = userID;
            myCommand.Parameters.Add(parameterUserID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }

        public int ChangePassword(int UserID, String OldPassword, String NewPassword, String IPAddress, int NumberOfLastPasswordsToAvoid)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_ChangePassword", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int,4);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterOldPassword = new SqlParameter("@OldPassword", SqlDbType.NVarChar, 50);
            parameterOldPassword.Value = OldPassword;
            myCommand.Parameters.Add(parameterOldPassword);

            SqlParameter parameterNewPassword = new SqlParameter("@NewPassword", SqlDbType.NVarChar, 50);
            parameterNewPassword.Value = NewPassword;
            myCommand.Parameters.Add(parameterNewPassword);

            SqlParameter parameterNumberOfLastPasswordsToAvoid = new SqlParameter("@NumberOfLastPasswordsToAvoid", SqlDbType.Int);
            parameterNumberOfLastPasswordsToAvoid.Value = NumberOfLastPasswordsToAvoid;
            myCommand.Parameters.Add(parameterNumberOfLastPasswordsToAvoid);

            SqlParameter parameterStatus = new SqlParameter("@Status", SqlDbType.Int);
            parameterStatus.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterStatus);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            return (int)parameterStatus.Value;
        }

        public int ResetPassword(int userID, string password)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_ResetPassword", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = userID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
            parameterPassword.Value = Encrypt(password);
            myCommand.Parameters.Add(parameterPassword);

            SqlParameter parameterStatus = new SqlParameter("@Result", SqlDbType.Int);
            parameterStatus.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterStatus);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            return (int)parameterStatus.Value;
        }

        public string Encrypt(string cleanString)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public PasswordPolicy GetPasswordPolicy()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_GetPasswordPolicy", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterPasswordPolicyID = new SqlParameter("@PasswordPolicyID", SqlDbType.Int);
            parameterPasswordPolicyID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterPasswordPolicyID);

            SqlParameter parameterMinPasswordLength = new SqlParameter("@MinPasswordLength", SqlDbType.Int);
            parameterMinPasswordLength.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterMinPasswordLength);

            SqlParameter parameterMinNumberOfAlphabets = new SqlParameter("@MinNumberOfAlphabets", SqlDbType.Int);
            parameterMinNumberOfAlphabets.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterMinNumberOfAlphabets);

            SqlParameter parameterMinNumberOfNumerics = new SqlParameter("@MinNumberOfNumerics", SqlDbType.Int);
            parameterMinNumberOfNumerics.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterMinNumberOfNumerics);

            SqlParameter parameterMinNumberOfSpecialChars = new SqlParameter("@MinNumberOfSpecialChars", SqlDbType.Int);
            parameterMinNumberOfSpecialChars.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterMinNumberOfSpecialChars);

            SqlParameter parameterExpireDuration = new SqlParameter("@ExpireDuration", SqlDbType.Int);
            parameterExpireDuration.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterExpireDuration);

            SqlParameter parameterNumberOfLastPasswordsToAvoid = new SqlParameter("@NumberOfLastPasswordsToAvoid", SqlDbType.Int);
            parameterNumberOfLastPasswordsToAvoid.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterNumberOfLastPasswordsToAvoid);

            SqlParameter parameterMinNumberOfUpperChar = new SqlParameter("@MinNumberOfUpperChar", SqlDbType.Int);
            parameterMinNumberOfUpperChar.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterMinNumberOfUpperChar);

            SqlParameter parameterMinNumberOfLowerChar = new SqlParameter("@MinNumberOfLowerChar", SqlDbType.Int);
            parameterMinNumberOfLowerChar.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterMinNumberOfLowerChar);

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            PasswordPolicy passwordPolicy = new PasswordPolicy();

            passwordPolicy.MinPasswordLength = ParseData.StringToInt(parameterMinPasswordLength.Value.ToString());
            passwordPolicy.MinNumberOfAlphabets = ParseData.StringToInt(parameterMinNumberOfAlphabets.Value.ToString());
            passwordPolicy.MinNumberOfNumerics = ParseData.StringToInt(parameterMinNumberOfNumerics.Value.ToString());
            passwordPolicy.MinNumberOfSpecialChars = ParseData.StringToInt(parameterMinNumberOfSpecialChars.Value.ToString());
            passwordPolicy.NumberOfLastPasswordsToAvoid = ParseData.StringToInt(parameterNumberOfLastPasswordsToAvoid.Value.ToString());
            passwordPolicy.MinNumberOfUpperChar = ParseData.StringToInt(parameterMinNumberOfUpperChar.Value.ToString());
            passwordPolicy.MinNumberOfLowerChar = ParseData.StringToInt(parameterMinNumberOfLowerChar.Value.ToString());
            passwordPolicy.ExpireDuration = ParseData.StringToInt(parameterExpireDuration.Value.ToString());

            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

            return passwordPolicy;
        }

        public void UpdateUserByLoginID(string LoginID,
                                             string UserStatus
                                            )
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            //SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateUserByLoginID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.NVarChar, 50);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterUserStatus = new SqlParameter("@UserStatus", SqlDbType.NVarChar, 50);
            parameterUserStatus.Value = UserStatus;
            myCommand.Parameters.Add(parameterUserStatus);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }

        public void DeleteUsers(int UserID)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_DeleteUsers", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);


            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }

        public int GetTotalNoOfUser()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("Select Count(*) From dbo.EFT_User WHERE UserStatus = 'ACTIVE'", myConnection);
            myCommand.CommandType = CommandType.Text;

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            int totalUser = (int)myCommand.ExecuteScalar();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
            return totalUser;
        }
    }
}


    

