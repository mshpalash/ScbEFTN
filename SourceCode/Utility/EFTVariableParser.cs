using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace EFTN.Utility
{
    public class EFTVariableParser
    {
        public static string ParseEFTAccountNumber(string accountNumber)
        {
            string retAccountNumber = Regex.Replace(accountNumber.Replace("%", "").Replace("'", "").Replace("-", "").Replace("_", "").Replace("`", "").Replace("~", "").Replace("!", "").Replace("@", "").Replace("$", "").Replace("^", "").Replace("*", "").Replace("+", "").Replace("=", "").Replace("[", "").Replace("{", "").Replace("]", "").Replace("}", "").Replace("|", "").Replace("\\", "").Replace("'", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("/", "").Replace(",", "").Trim(), @"\s{2,}", " ");
            return retAccountNumber;
        }

        public static string ParseEFTRoutingNumber(string routingNumber)
        {
            string retRoutingNumber = Regex.Replace(routingNumber.Replace("%", "").Replace("'", "").Replace("-", "").Replace("_", "").Replace("`", "").Replace("~", "").Replace("!", "").Replace("@", "").Replace("$", "").Replace("^", "").Replace("*", "").Replace("+", "").Replace("=", "").Replace("[", "").Replace("{", "").Replace("]", "").Replace("}", "").Replace("|", "").Replace("\\", "").Replace("'", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("/", "").Replace(",", "").Trim(), @"\s{1,}", "").PadLeft(9, '0').Trim();
            return retRoutingNumber;
        }

        public static string ParseEFTAmount(string amount)
        {
            string retAmount = Regex.Replace(amount.Replace("%", "").Replace("'", "").Replace("-", "").Replace("_", "").Replace("`", "").Replace("~", "").Replace("!", "").Replace("@", "").Replace("$", "").Replace("^", "").Replace("*", "").Replace("+", "").Replace("=", "").Replace("[", "").Replace("{", "").Replace("]", "").Replace("}", "").Replace("|", "").Replace("\\", "").Replace("'", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace(",", "").Replace(";", "").Trim(), @"\s{1,}", "");
            return retAmount;
        }

        public static string ParseEFTReceiverID(string receiverID)
        {
            string retReceiverID = Regex.Replace(receiverID.Replace("%", "").Replace("'", "").Replace("-", "").Replace("_", "").Replace("`", "").Replace("~", "").Replace("!", "").Replace("@", "").Replace("$", "").Replace("^", "").Replace("*", "").Replace("+", "").Replace("=", "").Replace("[", "").Replace("{", "").Replace("]", "").Replace("}", "").Replace("|", "").Replace("\\", "").Replace("'", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace(",", "").Replace(";", "").Trim(), @"\s{1,}", " ");
            return retReceiverID;
        }

        public static string ParseEFTName(string name)
        {
            string retName = Regex.Replace(name.Replace("%", "").Replace("'", "").Replace("-", "").Replace("_", "").Replace("`", "").Replace("~", "").Replace("!", "").Replace("@", "").Replace("$", "").Replace("^", "").Replace("*", "").Replace("+", "").Replace("=", "").Replace("[", "").Replace("{", "").Replace("]", "").Replace("}", "").Replace("|", "").Replace("\\", "").Replace("'", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace(",", "").Replace(";", "").Trim(), @"\s{1,}", " ");
            return retName;
        }

        public static string ParseEFTPaymentInfo(string name)
        {
            string retName = GetAlphaNumericDataWithSingleSpace(name);
            return retName;
        }

        private static string GetAlphaNumericDataWithSingleSpace(string input)
        {
            string clean = Regex.Replace(input, @"[^a-zA-Z0-9\s]", " ");
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);
            return regex.Replace(clean, @" ").Trim();
        }
    }
}
