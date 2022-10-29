using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class iBankingDB
    {
        public DataTable GetibankingEDRDataforChecker()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_iBankingGetEDRforChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            DataTable myDT = new DataTable();
            myConnection.Open();
            try
            {
                myAdapter.Fill(myDT);

            }
            catch (Exception ex)
            {
                //log

            }
            finally
            {
                myConnection.Close();
            }
            return myDT;
        }

        public DataTable GetibankingEDRDataforMaker()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_iBankingGetEDRforMaker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            DataTable myDT = new DataTable();
            myConnection.Open();
            try
            {
                myAdapter.Fill(myDT);

            }
            catch (Exception ex)
            {
                //log

            }

            finally
            {
                myConnection.Close();
            }
            return myDT;
        }

        public void AcceptTransactionsByMaker(Guid IBTID, int CreatedBy)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand myCmd = new SqlCommand("EFT_iBankingAcceptEDRbyMaker", myConnection);
            myCmd.CommandType = CommandType.StoredProcedure;
            myCmd.CommandTimeout = 3600;

            SqlParameter parameterIBTID = new SqlParameter("@IBTID", SqlDbType.UniqueIdentifier);
            parameterIBTID.Value = IBTID;
            myCmd.Parameters.Add(parameterIBTID);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myCmd.Parameters.Add(parameterCreatedBy);

            myConnection.Open();
            try
            {
                myCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                //
            }
            finally
            {
                myConnection.Close();
            }
        }

        public void DeleteTransactionsByMaker(Guid IBTID, int deletedBy, string reason)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand myCmd = new SqlCommand("EFT_iBankingDeleteEDRbyMaker", myConnection);
            myCmd.CommandType = CommandType.StoredProcedure;
            myCmd.CommandTimeout = 3600;

            SqlParameter parameterIBTID = new SqlParameter("@IBTID", SqlDbType.UniqueIdentifier);
            parameterIBTID.Value = IBTID;
            myCmd.Parameters.Add(parameterIBTID);

            SqlParameter parameterdeletedBy = new SqlParameter("@deletedBy", SqlDbType.Int);
            parameterdeletedBy.Value = deletedBy;
            myCmd.Parameters.Add(parameterdeletedBy);

            SqlParameter parameterReason = new SqlParameter("@Reason", SqlDbType.VarChar);
            parameterReason.Value = reason;
            myCmd.Parameters.Add(parameterReason);

            myConnection.Open();
            try
            {
                myCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                //
            }
            finally
            {
                myConnection.Close();
            }
        }

        public void DeleteTransactionsByChecker(Guid IBTID, int deletedBy, string reason)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand myCmd = new SqlCommand("EFT_iBankingDeleteEDRbyChecker", myConnection);
            myCmd.CommandType = CommandType.StoredProcedure;
            myCmd.CommandTimeout = 3600;

            SqlParameter parameterIBTID = new SqlParameter("@IBTID", SqlDbType.UniqueIdentifier);
            parameterIBTID.Value = IBTID;
            myCmd.Parameters.Add(parameterIBTID);

            SqlParameter parameterdeletedBy = new SqlParameter("@deletedBy", SqlDbType.Int);
            parameterdeletedBy.Value = deletedBy;
            myCmd.Parameters.Add(parameterdeletedBy);

            SqlParameter parameterReason = new SqlParameter("@Reason", SqlDbType.VarChar);
            parameterReason.Value = reason;
            myCmd.Parameters.Add(parameterReason);

            myConnection.Open();
            try
            {
                myCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                //
            }
            finally
            {
                myConnection.Close();
            }
        }
        public void AcceptTransactionsByChecker(Guid IBTID, int CheckedBy)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand myCmd = new SqlCommand("EFT_iBankingAcceptEDRbyChecker", myConnection);
            myCmd.CommandType = CommandType.StoredProcedure;
            myCmd.CommandTimeout = 3600;

            SqlParameter parameterIBTID = new SqlParameter("@IBTID", SqlDbType.UniqueIdentifier);
            parameterIBTID.Value = IBTID;
            myCmd.Parameters.Add(parameterIBTID);

            SqlParameter parameterCheckedBy = new SqlParameter("@CheckedBy", SqlDbType.Int);
            parameterCheckedBy.Value = CheckedBy;
            myCmd.Parameters.Add(parameterCheckedBy);

            myConnection.Open();
            try
            {
                myCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                //
            }
            finally
            {
                myConnection.Close();
            }
        }

        public DataTable GetibankingRejectedTransactionReport(string TransactionEntryDate)
        {


            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand myCmd = new SqlCommand("EFT_iBankingRejectedTransactionReport", myConnection);
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = myCmd;

            myCmd.CommandType = CommandType.StoredProcedure;
            myCmd.CommandTimeout = 3600;

            SqlParameter parameterTransactionEntryDate = new SqlParameter("@TransactionEntryDate", SqlDbType.VarChar);
            parameterTransactionEntryDate.Value = TransactionEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionEntryDate);
            myConnection.Open();

            DataTable myDT = new DataTable();

            try
            {
                myAdapter.Fill(myDT);
            }
            catch (Exception ex)
            {

                //
            }
            finally
            {
                myConnection.Close();
            }
            
           
            return myDT;
        }

        public DataTable GetibankingCBSReport(string TransactionDate,int CBSResponse)
        {


            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand myCmd = new SqlCommand("EFT_iBankingCBSReport", myConnection);
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = myCmd;

            myCmd.CommandType = CommandType.StoredProcedure;
            myCmd.CommandTimeout = 3600;

            SqlParameter parameterTransactionDate = new SqlParameter("@TransactionDate", SqlDbType.VarChar);
            parameterTransactionDate.Value = TransactionDate;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionDate);
     

            SqlParameter parametercbsresponse = new SqlParameter("@cbsresponse", SqlDbType.Int);
            parametercbsresponse.Value = CBSResponse;
            myAdapter.SelectCommand.Parameters.Add(parametercbsresponse);


            myConnection.Open();

            DataTable myDT = new DataTable();

            try
            {
                myAdapter.Fill(myDT);
            }
            catch (Exception ex)
            {

                //
            }
            finally
            {
                myConnection.Close();
            }


            return myDT;
        }
    }
}