using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;

namespace EFTN.component
{
    public class MICROracleDB
    {
        public DataTable GetMICRData()
        {
            OracleConnection OraCon = new OracleConnection(ConfigurationManager.AppSettings["MICRConnectionString"]);
            OraCon.Open();
            //////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////
            ///////please confirm the view name/////////
            //////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////

            string command = "SELECT MASTER, ACCOUNT, CCY, TITLE, PRODUCT, SEGMENT, STATUS, RISKS, ADDRESS FROM dbo.ACCOUNTINFO";
            OracleCommand cmd = new OracleCommand(command, OraCon);
            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            OraCon.Close();
            OraCon.Dispose();
            return dt;
        }
    }
}
