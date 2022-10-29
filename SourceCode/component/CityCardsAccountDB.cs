using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace FloraSoft
{
    public class CityCardsAccountDB
    {

        public void InsertTempCityCards(string ConnectionString, string SenderAccNo, decimal Amount)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertTempCityCards", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSenderAccNo = new SqlParameter("@SenderAccNo", SqlDbType.NVarChar, 17);
            parameterSenderAccNo.Value = SenderAccNo;
            myAdapter.SelectCommand.Parameters.Add(parameterSenderAccNo);

            SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Money);
            parameterAmount.Value = Amount;
            myAdapter.SelectCommand.Parameters.Add(parameterAmount);


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable Get_XMLFile_ForTransactionSent_CityCards(string ConnectionString, string SettlementDate)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_XMLFile_GetTransactionSent_CityCards", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.VarChar);
            parameterSettlementDate.Value = SettlementDate;
            myAdapter.SelectCommand.Parameters.Add(parameterSettlementDate);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public DataTable GetCityCardsCustomer(string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Get_CityCardsCustomer", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void InsertCityCardsCustomer(string ConnectionString,
                                            string CustomerCardNo,
                                            string BankAccNo,
                                            string OtherBankAccNo,
                                            string OtherBankAccName,
                                            string RoutingNo)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Insert_CityCardsCustomer", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterCustomerCardNo = new SqlParameter("@CustomerCardNo", SqlDbType.VarChar);
            parameterCustomerCardNo.Value = CustomerCardNo;
            myAdapter.SelectCommand.Parameters.Add(parameterCustomerCardNo);

            SqlParameter parameterBankAccNo = new SqlParameter("@BankAccNo", SqlDbType.VarChar);
            parameterBankAccNo.Value = BankAccNo;
            myAdapter.SelectCommand.Parameters.Add(parameterBankAccNo);

            SqlParameter parameterOtherBankAccNo = new SqlParameter("@OtherBankAccNo", SqlDbType.VarChar);
            parameterOtherBankAccNo.Value = OtherBankAccNo;
            myAdapter.SelectCommand.Parameters.Add(parameterOtherBankAccNo);

            SqlParameter parameterOtherBankAccName = new SqlParameter("@OtherBankAccName", SqlDbType.VarChar);
            parameterOtherBankAccName.Value = OtherBankAccName;
            myAdapter.SelectCommand.Parameters.Add(parameterOtherBankAccName);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.VarChar);
            parameterRoutingNo.Value = RoutingNo;
            myAdapter.SelectCommand.Parameters.Add(parameterRoutingNo);


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void UpdateCityCardsCustomer(string ConnectionString,
                                            int CustomerID,
                                            string CustomerCardNo,
                                            string BankAccNo,
                                            string OtherBankAccNo,
                                            string OtherBankAccName,
                                            string RoutingNo
                                            )
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Update_CityCardsCustomer", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterCustomerID = new SqlParameter("@CustomerID", SqlDbType.Int);
            parameterCustomerID.Value = CustomerID;
            myAdapter.SelectCommand.Parameters.Add(parameterCustomerID);

            SqlParameter parameterCustomerCardNo = new SqlParameter("@CustomerCardNo", SqlDbType.VarChar);
            parameterCustomerCardNo.Value = CustomerCardNo;
            myAdapter.SelectCommand.Parameters.Add(parameterCustomerCardNo);

            SqlParameter parameterBankAccNo = new SqlParameter("@BankAccNo", SqlDbType.VarChar);
            parameterBankAccNo.Value = BankAccNo;
            myAdapter.SelectCommand.Parameters.Add(parameterBankAccNo);

            SqlParameter parameterOtherBankAccNo = new SqlParameter("@OtherBankAccNo", SqlDbType.VarChar);
            parameterOtherBankAccNo.Value = OtherBankAccNo;
            myAdapter.SelectCommand.Parameters.Add(parameterOtherBankAccNo);

            SqlParameter parameterOtherBankAccName = new SqlParameter("@OtherBankAccName", SqlDbType.VarChar);
            parameterOtherBankAccName.Value = OtherBankAccName;
            myAdapter.SelectCommand.Parameters.Add(parameterOtherBankAccName);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.VarChar);
            parameterRoutingNo.Value = RoutingNo;
            myAdapter.SelectCommand.Parameters.Add(parameterRoutingNo);


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void DeleteCityCardsCustomer(string ConnectionString, int CustomerID)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_Delete_CityCardsCustomer", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterCustomerID = new SqlParameter("@CustomerID", SqlDbType.Int);
            parameterCustomerID.Value = CustomerID;
            myAdapter.SelectCommand.Parameters.Add(parameterCustomerID);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void TransferToTransactionSent_forCityCards_FromTempTable(string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_TransferToTransactionSent_forCityCards", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable GetTransactionSentBeforeTransfer_forCityCards(string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetTransactionSentBeforeTransfer_forCityCards", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetFailedCardsByEntryDate(string ConnectionString, string EntryDate)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetFailedCards", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEntryDate = new SqlParameter("@EntryDate", SqlDbType.NVarChar, 8);
            parameterEntryDate.Value = EntryDate;
            myAdapter.SelectCommand.Parameters.Add(parameterEntryDate);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

    }
}

