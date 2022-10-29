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
    public class InvalidItemDB
    {
        public DataTable GetInvalidEDR()
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetInvalidEDR", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;


                    //SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
                    //parameterEDRID.Value = EDRID;
                    //myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

                    DataTable dt = new DataTable();

                    myConnection.Open();

                    myAdapter.Fill(dt);

                    myConnection.Close();

                    myAdapter.Dispose();
                    myConnection.Dispose();

                    return dt;
                }
            }
        }

        public void UpdateInvalidDTransaction(  string DFIAccountNo,
                                                string ReceiverName,
                                                string PaymentInfo,
                                                string AccountNo,
                                                string ReceivingBankRoutingNo,
                                                string IdNumber,
                                                Guid EDRID
                                            )
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_UpdateInvalidTransaction", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDFIAccountNo = new SqlParameter("@DFIAccountNo", SqlDbType.VarChar);
            parameterDFIAccountNo.Value = DFIAccountNo;
            myCommand.Parameters.Add(parameterDFIAccountNo);


            SqlParameter parameterReceiverName = new SqlParameter("@ReceiverName", SqlDbType.NVarChar, 22);
            parameterReceiverName.Value = ReceiverName;
            myCommand.Parameters.Add(parameterReceiverName);

            SqlParameter parameterPaymentInfo = new SqlParameter("@PaymentInfo", SqlDbType.NVarChar, 80);
            parameterPaymentInfo.Value = PaymentInfo;
            myCommand.Parameters.Add(parameterPaymentInfo);

            SqlParameter parameterAccountNo = new SqlParameter("@AccountNo", SqlDbType.NVarChar, 17);
            parameterAccountNo.Value = AccountNo;
            myCommand.Parameters.Add(parameterAccountNo);

            SqlParameter parameterReceivingBankRoutingNo = new SqlParameter("@ReceivingBankRoutingNo", SqlDbType.NVarChar, 9);
            parameterReceivingBankRoutingNo.Value = ReceivingBankRoutingNo;
            myCommand.Parameters.Add(parameterReceivingBankRoutingNo);

            SqlParameter parameterIdNumber = new SqlParameter("@IdNumber", SqlDbType.NVarChar, 15);
            parameterIdNumber.Value = IdNumber;
            myCommand.Parameters.Add(parameterIdNumber);



            SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
            parameterEDRID.Value = EDRID;
            myCommand.Parameters.Add(parameterEDRID);


            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public DataTable GetTransactionWithInvalidRoutingNumber()
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSentEDRByTransactionIDForBulk_InvalidRoutNo", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;

                    DataTable dt = new DataTable();

                    myConnection.Open();
                    myAdapter.Fill(dt);
                    myConnection.Close();
                    myAdapter.Dispose();
                    myConnection.Dispose();

                    return dt;
                }
            }
        }

        public void UpdateTransactionStatusForNewRoutingNo()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_Update_RoutingNoStatus", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public void UpdateCSVRejectionFromInvalidRoutingNo()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_Update_CSVRejectionFromInvalidRoutingNo", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myCommand.Dispose();
            myConnection.Dispose();
        }
    }
}
