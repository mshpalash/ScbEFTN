using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EFTN.component
{
    public class FCUBSReportDB
    {
        public DataTable GetReportForTransactionReceivedBySettlementDate(string SettlementDate, int BranchID)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_FCUBS_Rpt_TransactionReceivedBySettlementDate";
            }
            else
            {
                spName = "EFT_FCUBS_BranchwiseRpt_TransactionReceivedBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionReceivedBySettlementDateForCredit(string SettlementDate, int BranchID)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_FCUBS_RptCredit_TransactionReceivedBySettlementDate";
            }
            else
            {
                spName = "EFT_FCUBS_BranchwiseRptCredit_TransactionReceivedBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportForTransactionReceivedBySettlementDateForDebit(string SettlementDate, int BranchID)
        {
            string spName = string.Empty;

            if (BranchID == 0)
            {
                spName = "EFT_FCUBS_RptDebit_TransactionReceivedBySettlementDate";
            }
            else
            {
                spName = "EFT_FCUBS_BranchwiseRptDebit_TransactionReceivedBySettlementDate";
            }

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter(spName, myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdpater.SelectCommand.Parameters.Add(parameterSettlementDate);

            if (BranchID != 0)
            {
                SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
                parameterBranchID.Value = BranchID;
                myAdpater.SelectCommand.Parameters.Add(parameterBranchID);
            }

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseTransactionSent(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FCUBS_Rpt_Departmentwise_TransactionSent", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseTransactionSentCredit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FCUBS_RptCredit_Departmentwise_TransactionSent", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseTransactionSentDebit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FCUBS_RptDebit_Departmentwise_TransactionSent", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseInwardReturn(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FCUBS_Rpt_Departmentwise_ReturnReceived", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseInwardReturnCredit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FCUBS_RptCredit_Departmentwise_ReturnReceived", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }

        public DataTable GetReportDepartmentwiseInwardReturnDebit(string SettlementDate, int DepartmentID)
        {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_FCUBS_RptDebit_Departmentwise_ReturnReceived", myConnection);

            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);


            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdapter.Fill(dt);
            myConnection.Close();
            return dt;
        }


        public DataTable GetCBSActiveBranches()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetCBSActiveBranches", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetCBSActiveDepartments()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetCBSActiveDepartments", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

    }
}