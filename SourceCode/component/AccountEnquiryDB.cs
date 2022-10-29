using System.Data.SqlClient;
using System.Data;
using System.Configuration;



namespace EFTN.component
{
    public class AccountEnquiryDB
    {
        public string GetNewAccountNumber(string OldAccountNo)
        {// Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_GetNewAccountNumber", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterOldAccountNo = new SqlParameter("@OldAccountNo", SqlDbType.VarChar, 15);
            parameterOldAccountNo.Value = OldAccountNo;
            myCommand.Parameters.Add(parameterOldAccountNo);

            SqlParameter parameterNewAccountNo = new SqlParameter("@NewAccountNo", SqlDbType.VarChar, 16);
            parameterNewAccountNo.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterNewAccountNo);


            myConnection.Open();
            myCommand.ExecuteNonQuery();

            string NewAccountNo = parameterNewAccountNo.Value.ToString();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

            return NewAccountNo;
        }

        public string GetCCY_Code(string CCY)
        {
            string CCYCode = "00";
            switch (CCY)
            {
                case "BDT":
                    CCYCode = "00";
                    break;
                case "USD":
                    CCYCode = "01";
                    break;
                case "CAD":
                    CCYCode = "02";
                    break;
                case "JPY":
                    CCYCode = "15";
                    break;
                case "EUR":
                    CCYCode = "16";
                    break;
                case "GBP":
                    CCYCode = "80";
                    break;
            }
            return CCYCode;
        }
    }

   
}