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
    public class AccountModificationDB
    {
        public DataTable GetAccountModificationLog(string ModDate ,string Currency)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdpater = new SqlDataAdapter("EFT_GetAccModLog", myConnection);

            myAdpater.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdpater.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterModDate = new SqlParameter("@ModDate", SqlDbType.VarChar, 8);
            parameterModDate.Value = ModDate;
            myAdpater.SelectCommand.Parameters.Add(parameterModDate);

            SqlParameter parameterCurrency= new SqlParameter("@Currency", SqlDbType.VarChar, 3);
            parameterCurrency.Value = Currency;
            myAdpater.SelectCommand.Parameters.Add(parameterCurrency);

            DataTable dt = new DataTable();
            myConnection.Open();
            myAdpater.Fill(dt);
            myConnection.Close();
            return dt;
        }
    }
}
