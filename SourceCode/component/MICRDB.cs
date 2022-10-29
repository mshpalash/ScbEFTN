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
using System.IO;

namespace FloraSoft
{
    public class MICRDB
    {
        public DataTable GetMICRData(string command)
        {
            string MICRConnectionString = ConfigurationManager.AppSettings["MICRConnectionString"];
            
            string[] val = MICRConnectionString.Split(';');

            string ServerIP = val[0].Replace("server=", "");
            string DatabaseName = val[1].Replace("database=", "");
            string userID = val[2].Replace("uid=", "");
            //string pass = val[3].Replace("password=", "");

            string micrPassword = MICREncrypt.MICREncrypt.GetDatabasePassword(ServerIP);// micREncrypt. = new MICREncrypt.MICREncrypt();


            string value = "server=" + ServerIP + ";database=" + DatabaseName + ";uid=" + userID + ";password=" + micrPassword;

            //WriteLog(value);

            using (SqlConnection myConnection = new SqlConnection(value))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter(command, myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.Text;
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

        public DataTable GetVirtualAccountData(string command)
        {
            string MICRConnectionString = ConfigurationManager.AppSettings["MICRConnectionString"];
            
            string[] val = MICRConnectionString.Split(';');

            string ServerIP = val[0].Replace("server=", "");
            string DatabaseName = val[1].Replace("database=", "");
            string userID = val[2].Replace("uid=", "");
            //string pass = val[3].Replace("password=", "");

            string micrPassword = MICREncrypt.MICREncrypt.GetDatabasePassword(ServerIP);// micREncrypt. = new MICREncrypt.MICREncrypt();


            string value = "server=" + ServerIP + ";database=" + DatabaseName + ";uid=" + userID + ";password=" + micrPassword;

            //WriteLog(value);

            using (SqlConnection myConnection = new SqlConnection(value))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter(command, myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.Text;
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

        public DataTable GetMICRAccountInfo(string accountNumber, string CCY)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetMICRAccountInfo", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;


                    SqlParameter paramAccountNumber = new SqlParameter("@AccountNumber", SqlDbType.VarChar, 17);
                    paramAccountNumber.Value = accountNumber;
                    myAdapter.SelectCommand.Parameters.Add(paramAccountNumber);

                    SqlParameter paramCCY = new SqlParameter("@CCY", SqlDbType.VarChar, 2);
                    paramCCY.Value = CCY;
                    myAdapter.SelectCommand.Parameters.Add(paramCCY);

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

        public DataTable GetReceivedEDRByEDRIDForMICR(Guid EDRID)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_byEDRID_forMICR", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;


                    SqlParameter parameterEDRID  = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier); 
                    parameterEDRID.Value = EDRID;
                    myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

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

        public DataTable GetReceivedEDRByEDRIDForMICRForChecker(Guid EDRID)
        {
            using (SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetReceivedEDR_byEDRID_forMICR_forChecker", myConnection))
                {
                    myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myAdapter.SelectCommand.CommandTimeout = 3600;


                    SqlParameter parameterEDRID = new SqlParameter("@EDRID", SqlDbType.UniqueIdentifier);
                    parameterEDRID.Value = EDRID;
                    myAdapter.SelectCommand.Parameters.Add(parameterEDRID);

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

        public void UpdateMICRChargeAccounts()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_UpdateMICRChargeAccounts", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            myConnection.Open();
            myAdapter.SelectCommand.ExecuteNonQuery();
            //myAdapter.Fill(myDT);
            myConnection.Close();
            myConnection.Dispose();
        }

        private void WriteLog(string Msg)
        {
            FileStream fs = new FileStream(ConfigurationManager.AppSettings["LogPath"] + "\\EXP" + System.DateTime.Today.ToString("yyyyMMdd") + ".log", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(System.DateTime.Now.ToString() + ": " + Msg);
            sw.Close();
            sw.Dispose();
            fs.Close();
            fs.Dispose();
        }

    }
}
