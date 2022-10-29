using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace FloraSoft
{
    public class CityDebitAccountDB
    {
        public DataTable GetCityDebitAccount(string ConnectionString)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetCityDebitAccount", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public int InsertCityDebitAccount(string ConnectionString, string AccountNo,
                                    string OtherBankAcNo,
                                    string RoutingNumber,
                                    string AccountName
                                )
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertCityDebitAccount", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
            parameterAccountNo.Value = AccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterOtherBankAcNo = new SqlParameter("@OtherBankAcNo", SqlDbType.VarChar);
            parameterOtherBankAcNo.Value = OtherBankAcNo;
            myAdapter.SelectCommand.Parameters.Add(parameterOtherBankAcNo);

            SqlParameter parameterRoutingNumber = new SqlParameter("@RoutingNumber", SqlDbType.NChar);
            parameterRoutingNumber.Value = RoutingNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterRoutingNumber);

            SqlParameter parameterAccountName = new SqlParameter("@AccountName", SqlDbType.VarChar);
            parameterAccountName.Value = AccountName;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountName);

            SqlParameter parameterInsertResult = new SqlParameter("@InsertResult", SqlDbType.Int);
            parameterInsertResult.Direction = ParameterDirection.Output;
            myAdapter.SelectCommand.Parameters.Add(parameterInsertResult);


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            return (int)parameterInsertResult.Value;
        }


        public void UpdateCityDebitAccountStatus(string ConnectionString, int AccountID,
                                                    string AccountNo,
                                                    string OtherBankAcNo,
                                                    string RoutingNumber,
                                                    string AccountName
                                                )
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateCityDebitAccountStatus", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterAccountID = new SqlParameter("@AccountID", SqlDbType.Int);
            parameterAccountID.Value = AccountID;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountID);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
            parameterAccountNo.Value = AccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterOtherBankAcNo = new SqlParameter("@OtherBankAcNo", SqlDbType.VarChar);
            parameterOtherBankAcNo.Value = OtherBankAcNo;
            myAdapter.SelectCommand.Parameters.Add(parameterOtherBankAcNo);

            SqlParameter parameterRoutingNumber = new SqlParameter("@RoutingNumber", SqlDbType.NChar);
            parameterRoutingNumber.Value = RoutingNumber;
            myAdapter.SelectCommand.Parameters.Add(parameterRoutingNumber);

            SqlParameter parameterAccountName = new SqlParameter("@AccountName", SqlDbType.VarChar);
            parameterAccountName.Value = AccountName;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountName);


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void DeleteCityDebitAccountInfo(int AccountID,
                                      string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("EFT_DeleteCityDebitAccountInfo", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterAccountID = new SqlParameter("@AccountID", SqlDbType.Int);
            parameterAccountID.Value = AccountID;
            myCommand.Parameters.Add(parameterAccountID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
        }
    }
}

