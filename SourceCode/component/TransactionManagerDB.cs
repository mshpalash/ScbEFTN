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
    public class TransactionManagerDB
    {
        public void DeleteInwardBySettlementDate(int InwardType,
                                                 string SettlementDate,
                                                 int DeleteUser)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_DeleteInwardBySettlementDate", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterInwardType = new SqlParameter("@InwardType", SqlDbType.Int);
            parameterInwardType.Value = InwardType;
            myCommand.Parameters.Add(parameterInwardType);

            SqlParameter parameterSettlementDate = new SqlParameter("@SettlementDate", SqlDbType.NVarChar, 8);
            parameterSettlementDate.Value = SettlementDate;
            myCommand.Parameters.Add(parameterSettlementDate);

            SqlParameter parameterDeleteUser = new SqlParameter("@DeleteUser", SqlDbType.Int);
            parameterDeleteUser.Value = DeleteUser;
            myCommand.Parameters.Add(parameterDeleteUser);

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}
