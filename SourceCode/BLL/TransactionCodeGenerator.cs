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
    public class TransactionCodeGenerator
    {
        public string GetTransactionCodeForReturn(string oldTranscode)
        {
            string retTranscode = oldTranscode;
            switch (oldTranscode)
            {
                case "22":
                    retTranscode= "21";
                    break;
                case "24":
                    retTranscode = "21";
                    break;
                case "29":
                    retTranscode = "26";
                    break;
                case "32":
                    retTranscode = "31";
                    break;
                case "27":
                    retTranscode = "26";
                    break;
                case "37":
                    retTranscode = "36";
                    break;
                case "42":
                    retTranscode = "41";
                    break;
                case "52":
                    retTranscode = "51";
                    break;
                case "55":
                    retTranscode = "56";
                    break;
            }
            return retTranscode;
        }
    }
}
