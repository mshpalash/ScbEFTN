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

namespace FloraSoft
{
    public class UtilityDB
    {
        public void ExecuteSQL(string command, string database)
        {
            string conStr = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            using (SqlConnection myConnection = new SqlConnection(conStr))
            {
                using (SqlCommand myCommand = new SqlCommand(command, myConnection))
                {
                    myCommand.CommandType = CommandType.Text;


                    myConnection.Open();

                    myCommand.ExecuteNonQuery();

                    myConnection.Close();
                }
            }

        }

        public void TruncateTable(string table, string database)
        {
            //string command = "TRUNCATE TABLE [" + table + "]";
            string command = "DELETE FROM [" + table + "]";
            ExecuteSQL(command, database);
        }
    }
}
