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
using iTextSharp.text;
using iTextSharp.text.pdf;
using EFTN.Utility;
using EFTN.component;

namespace EFTN
{
    public partial class FinacleTransactionReceivedStatus : System.Web.UI.Page
    {
        private static DataTable myDataTable = new DataTable();
        //private EFTN.BLL.FinacleManager fm;


        protected void Page_Load(object sender, EventArgs e)
        {
            Guid TransactionID = new Guid(Request.Params["TransactionID"]);
            GetFinacleInsertMessage(TransactionID);
        }


        private void GetFinacleInsertMessage(Guid TransactionID)
        {
            if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("225"))
            {
                ISOMessageDB isoMessageDB = new ISOMessageDB();
                dtgISOMessageStatus.DataSource = isoMessageDB.GetISOMessageByTransactionID(TransactionID);
                dtgISOMessageStatus.DataBind();
            }
        }
    }
}
