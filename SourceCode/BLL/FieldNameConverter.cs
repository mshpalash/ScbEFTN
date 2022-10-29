using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace EFTN.BLL
{
    public class FieldNameConverter
    {
        public static string GetFieldName(string fieldColumn, int paymentType)
        {
            EFTN.component.FieldNameDB db = new EFTN.component.FieldNameDB();
            return db.GetFieldName(paymentType, fieldColumn);
        }
    }
}
