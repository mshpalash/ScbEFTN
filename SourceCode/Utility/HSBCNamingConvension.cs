using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace EFTN.Utility
{
    public class HSBCNamingConvension
    {
        public static string GetMonth(int inMonth)
        {
            string outMonth = string.Empty;

            switch (inMonth)
            {
                case 1:
                    outMonth = "0A";
                    break;
                case 2:
                    outMonth = "0B";
                    break;
                case 3:
                    outMonth = "0C";
                    break;
                case 4:
                    outMonth = "0D";
                    break;
                case 5:
                    outMonth = "0E";
                    break;
                case 6:
                    outMonth = "0F";
                    break;
                case 7:
                    outMonth = "0G";
                    break;
                case 8:
                    outMonth = "0H";
                    break;
                case 9:
                    outMonth = "0I";
                    break;
                case 10:
                    outMonth = "0J";
                    break;
                case 11:
                    outMonth = "0K";
                    break;
                case 12:
                    outMonth = "0L";
                    break;
            }

            return outMonth;
        }
    }
}
