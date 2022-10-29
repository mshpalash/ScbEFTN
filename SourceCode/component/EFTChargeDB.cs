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
    public class EFTChargeDB
    {
        public void UpdateChargeForSCB(Guid EDRID, string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateChargeForSCB", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable GetEDRIDByTransactionID(Guid TransactionID, string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetEDRByTransactionID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetChargeACCOUNT(string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetChargeACCOUNT", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetChargeMASTER(string ConnectionString)
        {
            // Must enter your connection string
            // Must write your procedure name 
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));

            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetChargeMASTER", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetChargeProductCode(string ConnectionString)
        {
            // Must enter your connection string
            // Must write your procedure name 
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));

            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetChargeProductCode", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetChargeSegment(string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));

            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetChargeSegment", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


        public void InsertChargeAccount(string ACCOUNT,int BBCharge,int BankCharge,int BBChargeVAT,int BankChargeVAT,int UserID, string ConnectionString

)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));

            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertChargeAccount", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterACCOUNT = new SqlParameter("@ACCOUNT", SqlDbType.NVarChar, 17);
            parameterACCOUNT.Value = ACCOUNT;
            myAdapter.SelectCommand.Parameters.Add(parameterACCOUNT);

            SqlParameter parameterBBCharge = new SqlParameter("@BBCharge", SqlDbType.Int);
            parameterBBCharge.Value = BBCharge;
            myAdapter.SelectCommand.Parameters.Add(parameterBBCharge);

            SqlParameter parameterBankCharge = new SqlParameter("@BankCharge", SqlDbType.Int);
            parameterBankCharge.Value = BankCharge;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCharge);

            SqlParameter parameterBBChargeVAT = new SqlParameter("@BBChargeVAT", SqlDbType.Int);
            parameterBBChargeVAT.Value = BBChargeVAT;
            myAdapter.SelectCommand.Parameters.Add(parameterBBChargeVAT);

            SqlParameter parameterBankChargeVAT = new SqlParameter("@BankChargeVAT", SqlDbType.Int);
            parameterBankChargeVAT.Value = BankChargeVAT;
            myAdapter.SelectCommand.Parameters.Add(parameterBankChargeVAT);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
        }

        public void InsertChargeMaster(string MASTER,int BBCharge,int BankCharge,int BBChargeVAT,int BankChargeVAT,int UserID,string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));

            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertChargeMaster", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterMASTER = new SqlParameter("@MASTER", SqlDbType.NVarChar, 7);
            parameterMASTER.Value = MASTER;
            myAdapter.SelectCommand.Parameters.Add(parameterMASTER);

            SqlParameter parameterBBCharge = new SqlParameter("@BBCharge", SqlDbType.Int);
            parameterBBCharge.Value = BBCharge;
            myAdapter.SelectCommand.Parameters.Add(parameterBBCharge);

            SqlParameter parameterBankCharge = new SqlParameter("@BankCharge", SqlDbType.Int);
            parameterBankCharge.Value = BankCharge;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCharge);

            SqlParameter parameterBBChargeVAT = new SqlParameter("@BBChargeVAT", SqlDbType.Int);
            parameterBBChargeVAT.Value = BBChargeVAT;
            myAdapter.SelectCommand.Parameters.Add(parameterBBChargeVAT);

            SqlParameter parameterBankChargeVAT = new SqlParameter("@BankChargeVAT", SqlDbType.Int);
            parameterBankChargeVAT.Value = BankChargeVAT;
            myAdapter.SelectCommand.Parameters.Add(parameterBankChargeVAT);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
        }

        public void InsertChargeProductCode(string ProductCode, int BBCharge, int BankCharge, int BBChargeVAT, int BankChargeVAT, int UserID, string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));

            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertChargeProductCode", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterProductCode = new SqlParameter("@ProductCode", SqlDbType.NVarChar, 10);
            parameterProductCode.Value = ProductCode;
            myAdapter.SelectCommand.Parameters.Add(parameterProductCode);

            SqlParameter parameterBBCharge = new SqlParameter("@BBCharge", SqlDbType.Int);
            parameterBBCharge.Value = BBCharge;
            myAdapter.SelectCommand.Parameters.Add(parameterBBCharge);

            SqlParameter parameterBankCharge = new SqlParameter("@BankCharge", SqlDbType.Int);
            parameterBankCharge.Value = BankCharge;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCharge);

            SqlParameter parameterBBChargeVAT = new SqlParameter("@BBChargeVAT", SqlDbType.Int);
            parameterBBChargeVAT.Value = BBChargeVAT;
            myAdapter.SelectCommand.Parameters.Add(parameterBBChargeVAT);

            SqlParameter parameterBankChargeVAT = new SqlParameter("@BankChargeVAT", SqlDbType.Int);
            parameterBankChargeVAT.Value = BankChargeVAT;
            myAdapter.SelectCommand.Parameters.Add(parameterBankChargeVAT);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
        }

        public void InsertSCBChargeAccounts(string ACCOUNT,int UserID,string ChargeAccount,string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));

            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertSCBChargeAccounts", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterACCOUNT = new SqlParameter("@ACCOUNT", SqlDbType.VarChar);
            parameterACCOUNT.Value = ACCOUNT;
            myAdapter.SelectCommand.Parameters.Add(parameterACCOUNT);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterChargeAccount = new SqlParameter("@ChargeAccount", SqlDbType.VarChar);
            parameterChargeAccount.Value = ChargeAccount;
            myAdapter.SelectCommand.Parameters.Add(parameterChargeAccount);


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
        }

        public void InsertChargeSEGMENT(string SEGMENT,int BBCharge,int BankCharge,int BBChargeVAT,int BankChargeVAT,int UserID,string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));

            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertChargeSEGMENT", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterSEGMENT = new SqlParameter("@SEGMENT", SqlDbType.VarChar);
            parameterSEGMENT.Value = SEGMENT;
            myAdapter.SelectCommand.Parameters.Add(parameterSEGMENT);

            SqlParameter parameterBBCharge = new SqlParameter("@BBCharge", SqlDbType.Int);
            parameterBBCharge.Value = BBCharge;
            myAdapter.SelectCommand.Parameters.Add(parameterBBCharge);

            SqlParameter parameterBankCharge = new SqlParameter("@BankCharge", SqlDbType.Int);
            parameterBankCharge.Value = BankCharge;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCharge);

            SqlParameter parameterBBChargeVAT = new SqlParameter("@BBChargeVAT", SqlDbType.Int);
            parameterBBChargeVAT.Value = BBChargeVAT;
            myAdapter.SelectCommand.Parameters.Add(parameterBBChargeVAT);

            SqlParameter parameterBankChargeVAT = new SqlParameter("@BankChargeVAT", SqlDbType.Int);
            parameterBankChargeVAT.Value = BankChargeVAT;
            myAdapter.SelectCommand.Parameters.Add(parameterBankChargeVAT);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable GetSCBChargeAccounts(string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSCBChargeAccounts", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateChargeForHSBC(Guid EDRID, string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateChargeForHSBC", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
        }

        public void InsertCityChargeDefineByEDRID(Guid EDRID, int CityChargeDefineID, string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertCityChargeDefineByEDRID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterCityChargeDefineID = new SqlParameter("@CityChargeDefineID", SqlDbType.Int);
            parameterCityChargeDefineID.Value = CityChargeDefineID;
            myAdapter.SelectCommand.Parameters.Add(parameterCityChargeDefineID);


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
        }

        public void InsertCityChargeDefineByTransactionID(Guid TransactionID, int CityChargeDefineID, string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertCityChargeDefineByTransactionID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterCityChargeDefineID = new SqlParameter("@CityChargeDefineID", SqlDbType.Int);
            parameterCityChargeDefineID.Value = CityChargeDefineID;
            myAdapter.SelectCommand.Parameters.Add(parameterCityChargeDefineID);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
        }

        public void InsertCityChargeCodeByTransactionID(Guid TransactionID, int CityChargeCode, string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertCityChargeCodeByTransactionID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterTransactionID = new SqlParameter("@TransactionID", SqlDbType.UniqueIdentifier);
            parameterTransactionID.Value = TransactionID;
            myAdapter.SelectCommand.Parameters.Add(parameterTransactionID);

            SqlParameter parameterCityChargeCode = new SqlParameter("@CityChargeCode", SqlDbType.Int);
            parameterCityChargeCode.Value = CityChargeCode;
            myAdapter.SelectCommand.Parameters.Add(parameterCityChargeCode);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
        }

        public void InsertCityChargeCodeByEDRID(Guid EDRID, int CityChargeCode, string ConnectionString)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertCityChargeCodeByEDRID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            SqlParameter parameterCityChargeCode = new SqlParameter("@CityChargeCode", SqlDbType.Int);
            parameterCityChargeCode.Value = CityChargeCode;
            myAdapter.SelectCommand.Parameters.Add(parameterCityChargeCode);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable GetCityChargeDefineList(string ConnectionString)
        {
            // Must enter your connection string

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetCityChargeDefineList", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetCityChargeDefineListForBulk(string ConnectionString)
        {
            // Must enter your connection string

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetCityChargeDefineListForBulk", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateChargeForCity(Guid EDRID, string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateChargeFoRCity", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void InsertCityChargeAccount(string ConnectionString,
                                        string AccountNo,
                                        int CityChargeCode,
                                        int UserID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertCityChargeAccount", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            parameterAccountNo.Value = AccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterCityChargeCode = new SqlParameter("@CityChargeCode", SqlDbType.Int);
            parameterCityChargeCode.Value = CityChargeCode;
            myAdapter.SelectCommand.Parameters.Add(parameterCityChargeCode);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myAdapter.SelectCommand.Parameters.Add(parameterUserID);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void UpdateCityChargeAccount(string ConnectionString,
                                        int CityChargeAccID,
                                        int CityChargeCode,
                                        string AccountNo)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateCityChargeAccount", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterCityChargeAccID = new SqlParameter("@CityChargeAccID", SqlDbType.Int);
            parameterCityChargeAccID.Value = CityChargeAccID;
            myAdapter.SelectCommand.Parameters.Add(parameterCityChargeAccID);

            SqlParameter parameterCityChargeCode = new SqlParameter("@CityChargeCode", SqlDbType.Int);
            parameterCityChargeCode.Value = CityChargeCode;
            myAdapter.SelectCommand.Parameters.Add(parameterCityChargeCode);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            parameterAccountNo.Value = AccountNo;
            myAdapter.SelectCommand.Parameters.Add(parameterAccountNo);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void UpdateDepartmentChargeForCity(string ConnectionString,
                                            int DepartmentID,
                                            string CityBBChargeAcc,
                                            string CityBankChargeAcc,
                                            string CityBBChargeVATAcc,
                                            string CityBankChargeVATAcc,
                                            string CityChargeWaveAcc,
                                            string CityVATWaveAcc,
                                            int CityChargeCode,
                                            int CreatedBy)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateDepartmentChargeForCity", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            SqlParameter parameterCityBBChargeAcc = new SqlParameter("@CityBBChargeAcc", SqlDbType.NVarChar, 17);
            parameterCityBBChargeAcc.Value = CityBBChargeAcc;
            myAdapter.SelectCommand.Parameters.Add(parameterCityBBChargeAcc);

            SqlParameter parameterCityBankChargeAcc = new SqlParameter("@CityBankChargeAcc", SqlDbType.NVarChar, 17);
            parameterCityBankChargeAcc.Value = CityBankChargeAcc;
            myAdapter.SelectCommand.Parameters.Add(parameterCityBankChargeAcc);

            SqlParameter parameterCityBBChargeVATAcc = new SqlParameter("@CityBBChargeVATAcc", SqlDbType.NVarChar, 17);
            parameterCityBBChargeVATAcc.Value = CityBBChargeVATAcc;
            myAdapter.SelectCommand.Parameters.Add(parameterCityBBChargeVATAcc);

            SqlParameter parameterCityBankChargeVATAcc = new SqlParameter("@CityBankChargeVATAcc", SqlDbType.NVarChar, 17);
            parameterCityBankChargeVATAcc.Value = CityBankChargeVATAcc;
            myAdapter.SelectCommand.Parameters.Add(parameterCityBankChargeVATAcc);

            SqlParameter parameterCityChargeWaveAcc = new SqlParameter("@CityChargeWaveAcc", SqlDbType.NVarChar, 17);
            parameterCityChargeWaveAcc.Value = CityChargeWaveAcc;
            myAdapter.SelectCommand.Parameters.Add(parameterCityChargeWaveAcc);

            SqlParameter parameterCityVATWaveAcc = new SqlParameter("@CityVATWaveAcc", SqlDbType.NVarChar, 17);
            parameterCityVATWaveAcc.Value = CityVATWaveAcc;
            myAdapter.SelectCommand.Parameters.Add(parameterCityVATWaveAcc);

            SqlParameter parameterCityChargeCode = new SqlParameter("@CityChargeCode", SqlDbType.Int);
            parameterCityChargeCode.Value = CityChargeCode;
            myAdapter.SelectCommand.Parameters.Add(parameterCityChargeCode);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myAdapter.SelectCommand.Parameters.Add(parameterCreatedBy);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public DataTable GetCityChargeAccounts(string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetCityChargeAccounts", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetCityChargeCodeInfo(string ConnectionString)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetCityChargeCodeInfo", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void InsertCityChargeCodeInfo(string ConnectionString,
                                                double BBCharge,
                                                double BankCharge,
                                                double BBChargeVAT,
                                                double BankChargeVAT,
                                                double ChargeWave,
                                                double VATWave
                                              )
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_InsertCityChargeCodeInfo", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterBBCharge = new SqlParameter("@BBCharge", SqlDbType.Money);
            parameterBBCharge.Value = BBCharge;
            myAdapter.SelectCommand.Parameters.Add(parameterBBCharge);

            SqlParameter parameterBankCharge = new SqlParameter("@BankCharge", SqlDbType.Money);
            parameterBankCharge.Value = BankCharge;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCharge);

            SqlParameter parameterBBChargeVAT = new SqlParameter("@BBChargeVAT", SqlDbType.Money);
            parameterBBChargeVAT.Value = BBChargeVAT;
            myAdapter.SelectCommand.Parameters.Add(parameterBBChargeVAT);

            SqlParameter parameterBankChargeVAT = new SqlParameter("@BankChargeVAT", SqlDbType.Money);
            parameterBankChargeVAT.Value = BankChargeVAT;
            myAdapter.SelectCommand.Parameters.Add(parameterBankChargeVAT);

            SqlParameter parameterChargeWave = new SqlParameter("@ChargeWave", SqlDbType.Money);
            parameterChargeWave.Value = ChargeWave;
            myAdapter.SelectCommand.Parameters.Add(parameterChargeWave);

            SqlParameter parameterVATWave = new SqlParameter("@VATWave", SqlDbType.Money);
            parameterVATWave.Value = VATWave;
            myAdapter.SelectCommand.Parameters.Add(parameterVATWave);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void UpdateCityChargeCodeInfo(  string ConnectionString,
                                                    int CityChargeCode,
                                                    double BBCharge,
                                                    double BankCharge,
                                                    double BBChargeVAT,
                                                    double BankChargeVAT,
                                                    double ChargeWave,
                                                    double VATWave
                                                )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateCityChargeCodeInfo", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterCityChargeCode = new SqlParameter("@CityChargeCode", SqlDbType.Int);
            parameterCityChargeCode.Value = CityChargeCode;
            myAdapter.SelectCommand.Parameters.Add(parameterCityChargeCode);

            SqlParameter parameterBBCharge = new SqlParameter("@BBCharge", SqlDbType.Money);
            parameterBBCharge.Value = BBCharge;
            myAdapter.SelectCommand.Parameters.Add(parameterBBCharge);

            SqlParameter parameterBankCharge = new SqlParameter("@BankCharge", SqlDbType.Money);
            parameterBankCharge.Value = BankCharge;
            myAdapter.SelectCommand.Parameters.Add(parameterBankCharge);

            SqlParameter parameterBBChargeVAT = new SqlParameter("@BBChargeVAT", SqlDbType.Money);
            parameterBBChargeVAT.Value = BBChargeVAT;
            myAdapter.SelectCommand.Parameters.Add(parameterBBChargeVAT);

            SqlParameter parameterBankChargeVAT = new SqlParameter("@BankChargeVAT", SqlDbType.Money);
            parameterBankChargeVAT.Value = BankChargeVAT;
            myAdapter.SelectCommand.Parameters.Add(parameterBankChargeVAT);

            SqlParameter parameterChargeWave = new SqlParameter("@ChargeWave", SqlDbType.Money);
            parameterChargeWave.Value = ChargeWave;
            myAdapter.SelectCommand.Parameters.Add(parameterChargeWave);

            SqlParameter parameterVATWave = new SqlParameter("@VATWave", SqlDbType.Money);
            parameterVATWave.Value = VATWave;
            myAdapter.SelectCommand.Parameters.Add(parameterVATWave);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }

        public void DeleteEFTCityChargeAccount(string ConnectionString, int CityChargeAccID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConnectionString));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_DeleteEFT_CityChargeAccount", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterCityChargeAccID = new SqlParameter("@CityChargeAccID", SqlDbType.Int);
            parameterCityChargeAccID.Value = CityChargeAccID;
            myAdapter.SelectCommand.Parameters.Add(parameterCityChargeAccID);

            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
        }
    }
}
