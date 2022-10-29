using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FloraSoft;
using System.Data.SqlClient;
using EFTN.component;

namespace EFTN
{
    public partial class MICRSync : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["ImmediateOriginName"].Equals("SCB"))
            {
                SynchronizeSCBMICRData();
                SynchronizeVirtualAccountInfo();
                SychronizeSCBChargeAccountInMICR();
                Response.Redirect("EFTMaker.aspx");
            }
            //else if (ConfigurationManager.AppSettings["ImmediateOriginName"].Equals("CBL"))
            //{
            //    UtilityDB dbUtil = new UtilityDB();
            //    dbUtil.TruncateTable("EFT_MICRAccountInfo", "EFT");


            //    string constr = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            //    SqlBulkCopy bc = new SqlBulkCopy(constr);

            //    MICROracleDB mICROracleDB = new MICROracleDB();

            //    DataTable dt = mICROracleDB.GetMICRData();

            //    bc.DestinationTableName = "EFT_MICRAccountInfo";

            //    bc.WriteToServer(dt);

            //    dt.Dispose();

            //    Response.Redirect("EFTMaker.aspx");
            // }
        }

        private static void SynchronizeSCBMICRData()
        {
            UtilityDB dbUtil = new UtilityDB();
            dbUtil.TruncateTable("EFT_MICRAccountInfo", "EFT");
            string constr = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlBulkCopy bc = new SqlBulkCopy(constr);
            //MICRDB micrDB = new MICRDB(); // Commented out on 23-Feb-2019
            //string command = "SELECT MASTER, ACCOUNT, CCY, TITLE, PRODUCT, PRODUCTNAME, SEGMENT, SEGMENTNAME, STATUS, RISKS, ADDRESS, ARMCODE, ARMNAME, EMAIL, ChargeAccount, MASTER2, TITLE2, FULLNO, CCYNAME FROM dbo.ACCOUNTINFO";
            //string command = "SELECT MASTER, ACCOUNT, CCY, TITLE, PRODUCT, PRODUCTNAME, SEGMENT, SEGMENTNAME, STATUS, RISKS, ADDRESS, ARMCODE, ARMNAME, EMAIL, CCYNAME, FULLNO  FROM dbo.ACCOUNTINFO";
            //string command = "SELECT MASTER, ACCOUNT, CCY, TITLE, PRODUCT, PRODUCTNAME, SEGMENT, SEGMENTNAME, STATUS, RISKS, ADDRESS, ARMCODE, ARMNAME, EMAIL, CCYNAME, FULLNO  FROM dbo.ACCOUNTINFOEFT";
            string command = "SELECT MASTER, ACCOUNT, CCY, TITLE, PRODUCT, PRODUCTNAME, SEGMENT, SEGMENTNAME, STATUS, RISKS, ADDRESS, ARMCODE, ARMNAME, EMAIL, FULLNO,CCYNAME  FROM dbo.ACCOUNTINFOEFT";

            #region OLD_MICR_DB_Connection using dll provided from SCB_Commented out on 23-Feb-2019
            //DataTable dt = micrDB.GetMICRData(command); 
            #endregion

            #region New_MICR_DB_Connection_Implemented on 23-Feb-2019_Requested SCB on this date
            DataTable micrData = GetMICRDataFromMICRDB(command);
            #endregion

            bc.DestinationTableName = "EFT_MICRAccountInfo";

            bc.WriteToServer(micrData);

            micrData.Dispose();
        }
        private static DataTable GetMICRDataFromMICRDB(string command)
        {
            SqlConnection sqlConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["MICRConnectionString"]));
            SqlCommand cmd = new SqlCommand(command, sqlConnection);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            sqlConnection.Close();
            sqlConnection.Dispose();
            return dt;
        }
        private static void SynchronizeVirtualAccountInfo()
        {
            UtilityDB dbUtil = new UtilityDB();
            dbUtil.TruncateTable("EFT_VAccountInfo", "EFT");


            string constr = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlBulkCopy bc = new SqlBulkCopy(constr);

            MICRDB micrDB = new MICRDB();
            //string command = "SELECT ACCOUNTNO, VACCNO, FULLVANO, MATCHTYPE, CCY FROM dbo.VAccountInfo";//change the table name
            string command = "SELECT ACCOUNTNO, VACCNO, FULLVANO, MATCHTYPE, CCY FROM dbo.VAccountInfoEFT";//change the table name

            #region OLD_MICR_DB_Connection using dll provided from SCB_Commented out on 23-Feb-2019
            //DataTable dt = micrDB.GetVirtualAccountData(command);//change in the function 
            #endregion
            #region New_MICR_DB_Connection_Implemented on 23-Feb-2019_Requested SCB on this date
            DataTable vaData = GetMICRDataFromMICRDB(command);
            #endregion

            bc.DestinationTableName = "EFT_VAccountInfo";

            bc.WriteToServer(vaData);

            vaData.Dispose();
        }


        private void SychronizeSCBChargeAccountInMICR()
        {
            MICRDB micrDB = new MICRDB();
            micrDB.UpdateMICRChargeAccounts();
        }

    }
}
