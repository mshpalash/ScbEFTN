using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace EFTN.component
{
    public class EAdviceDB
    {
        public DataTable GetEAdviceReport(string TransactionEntryDate, string criteria)
        {


            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));

            SqlCommand myCmd = new SqlCommand("EFT_EAdviceGetReport", myConnection);
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = myCmd;

            myCmd.CommandType = CommandType.StoredProcedure;
            myCmd.CommandTimeout = 3600;

            SqlParameter parameterTransactionEntryDate = new SqlParameter("@TransactionEntryDate", SqlDbType.VarChar);
            parameterTransactionEntryDate.Value = TransactionEntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionEntryDate);

            SqlParameter parameterCriteria = new SqlParameter("@Criteria", SqlDbType.VarChar, 22);
            parameterCriteria.Value = criteria;
            myAdapter.SelectCommand.Parameters.Add(parameterCriteria);

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