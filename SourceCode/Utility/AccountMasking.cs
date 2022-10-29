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
    public class AccountMasking
    {
        public static string MaskAccount(string accountNumber)
        {
            string maskAccNumber = string.Empty;
            if (ConfigurationManager.AppSettings["ImmediateOriginName"].Equals("SCB"))
            {
                if (accountNumber.Length == 11)
                {
                    maskAccNumber += accountNumber.Substring(0, 2);
                    maskAccNumber += "-";
                    maskAccNumber += accountNumber.Substring(2, 7);
                    maskAccNumber += "-";
                    maskAccNumber += accountNumber.Substring(9, 2);
                }
                else
                {
                    return accountNumber;
                }
            }
            else
            {
                return accountNumber;
            }
            return maskAccNumber;
        }
    }
}
