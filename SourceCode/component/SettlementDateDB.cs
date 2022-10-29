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
    public class SettlementDateDB
    {
        public void UpdateSettlementDateForTransactionSent(
                                        DateTime SettlementDate,
                                        int Day,
                                        int Month,
                                        int Year
                    )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must check your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateSettlementDate_EDRSent", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int);
            parameterDay.Value = Day;
            myCommand.Parameters.Add(parameterDay);

            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int);
            parameterMonth.Value = Month;
            myCommand.Parameters.Add(parameterMonth);

            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
            parameterYear.Value = Year;
            myCommand.Parameters.Add(parameterYear);


            myConnection.Open();
            myCommand.ExecuteReader();
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public void UpdateSettlementDateForNOCSent(
                                DateTime SettlementDate,
                                int Day,
                                int Month,
                                int Year
                    )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must check your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateSettlementDate_NOCSent", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int);
            parameterDay.Value = Day;
            myCommand.Parameters.Add(parameterDay);

            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int);
            parameterMonth.Value = Month;
            myCommand.Parameters.Add(parameterMonth);

            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
            parameterYear.Value = Year;
            myCommand.Parameters.Add(parameterYear);


            myConnection.Open();
            myCommand.ExecuteReader();
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public void UpdateSettlementDateForReturnSent(
                                DateTime SettlementDate,
                                int Day,
                                int Month,
                                int Year
                    )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must check your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateSettlementDate_ReturnSent", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int);
            parameterDay.Value = Day;
            myCommand.Parameters.Add(parameterDay);

            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int);
            parameterMonth.Value = Month;
            myCommand.Parameters.Add(parameterMonth);

            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
            parameterYear.Value = Year;
            myCommand.Parameters.Add(parameterYear);


            myConnection.Open();
            myCommand.ExecuteReader();
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public DataTable GetSettlementDateForTransaction(string CalDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetDate", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterCalDate = new SqlParameter("@CalDate", SqlDbType.NVarChar, 8);
            parameterCalDate.Value = CalDate;
            myAdapter.SelectCommand.Parameters.Add(parameterCalDate);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public void UpdateSettlementDateForDishonorSent(
                                DateTime SettlementDate,
                                int Day,
                                int Month,
                                int Year
                    )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must check your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateSettlementDate_DishonorSent", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int);
            parameterDay.Value = Day;
            myCommand.Parameters.Add(parameterDay);

            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int);
            parameterMonth.Value = Month;
            myCommand.Parameters.Add(parameterMonth);

            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
            parameterYear.Value = Year;
            myCommand.Parameters.Add(parameterYear);


            myConnection.Open();
            myCommand.ExecuteReader();
            myCommand.Dispose();
            myConnection.Dispose();
        }



        public void UpdateSettlementDateForReturnSentDuringXMLGeneration(
                                DateTime SettlementDate,
                                int Day,
                                int Month,
                                int Year
                    )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must check your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateSettlementDate_ReturnSentDuringXMLGeneration", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int);
            parameterDay.Value = Day;
            myCommand.Parameters.Add(parameterDay);

            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int);
            parameterMonth.Value = Month;
            myCommand.Parameters.Add(parameterMonth);

            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
            parameterYear.Value = Year;
            myCommand.Parameters.Add(parameterYear);


            myConnection.Open();
            myCommand.ExecuteReader();
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public DateTime GetSettlementJDateByEntryDate(DateTime EntryDate)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_GetSettlementJDateByEntryDate", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandTimeout = 600;

            SqlParameter parameterEntryDate = new SqlParameter("@EntryDate", SqlDbType.DateTime);
            parameterEntryDate.Value = EntryDate;
            myCommand.Parameters.Add(parameterEntryDate);

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.DateTime);
            parameterSettlementJDate.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterSettlementJDate);

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            DateTime dtSettlementJDate = (DateTime) parameterSettlementJDate.Value;

            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

            return dtSettlementJDate;
        }

        public void UpdateSettlementDateForReceivedEDR(
                                        DateTime SettlementDate,
                                        int Day,
                                        int Month,
                                        int Year
                    )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must check your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateSettlementDate_ReceivedEDR", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int);
            parameterDay.Value = Day;
            myCommand.Parameters.Add(parameterDay);

            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int);
            parameterMonth.Value = Month;
            myCommand.Parameters.Add(parameterMonth);

            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
            parameterYear.Value = Year;
            myCommand.Parameters.Add(parameterYear);


            myConnection.Open();
            myCommand.ExecuteReader();
            myCommand.Dispose();
            myConnection.Dispose();
        }
        

        public void UpdateSettlementDateForReturnReceived(
                                        DateTime SettlementDate,
                                        int Day,
                                        int Month,
                                        int Year
                    )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must check your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateSettlementDate_ReceivedReturn", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int);
            parameterDay.Value = Day;
            myCommand.Parameters.Add(parameterDay);

            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int);
            parameterMonth.Value = Month;
            myCommand.Parameters.Add(parameterMonth);

            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
            parameterYear.Value = Year;
            myCommand.Parameters.Add(parameterYear);


            myConnection.Open();
            myCommand.ExecuteReader();
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public void UpdateSettlementDateForNOCReceived(
                                        DateTime SettlementDate,
                                        int Day,
                                        int Month,
                                        int Year
                    )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must check your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateSettlementDate_ReceivedNOC", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            SqlParameter parameterDay = new SqlParameter("@Day", SqlDbType.Int);
            parameterDay.Value = Day;
            myCommand.Parameters.Add(parameterDay);

            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int);
            parameterMonth.Value = Month;
            myCommand.Parameters.Add(parameterMonth);

            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
            parameterYear.Value = Year;
            myCommand.Parameters.Add(parameterYear);


            myConnection.Open();
            myCommand.ExecuteReader();
            myCommand.Dispose();
            myConnection.Dispose();
        }



    }
}
