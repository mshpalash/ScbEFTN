using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EFTN.component
{
    public class CityRFCDB
    {
        public DataTable GetReport_TransactionSentBySettlementDate_RFCSuccessful(string SettlementDate, int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_TransactionSentBySettlementDate_RFCSuccessful", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReport_TransactionSentBySettlementDate_RFCUNSuccessful(string SettlementDate, int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_TransactionSentBySettlementDate_RFCUNSuccessful", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReport_TransactionSentBySettlementDate_InRetSuccessful(string SettlementDate, int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_TransactionSentBySettlementDate_InRetSuccessful", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetReport_TransactionSentBySettlementDate_InRetUNSuccessful(string SettlementDate, int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_TransactionSentBySettlementDate_InRetUNSuccessful", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetReport_TransactionSentBySettlementDate_IBSSuccessful(string SettlementDate, int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_TransactionSentBySettlementDate_IBSSuccessful", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetReport_TransactionSentBySettlementDate_IBSUNSuccessful(string SettlementDate, int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_TransactionSentBySettlementDate_IBSUNSuccessful", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetReport_TransactionSentBySettlementDate_IBSUNSuccessfulMissing(string SettlementDate, int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_TransactionSentBySettlementDate_IBSUNSuccessfulMissing", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveEntryDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterEffectiveEntryDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveEntryDate);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }




        public DataTable GetScheduleTransactionByEffectiveEntryDate(string EffectiveDate,
                                                                int DepartmentID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Rpt_ScheduleTransactionByEffectiveEntryDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEffectiveDate = new SqlParameter("@EffectiveDate", SqlDbType.VarChar);
            parameterEffectiveDate.Value = EffectiveDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEffectiveDate);

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