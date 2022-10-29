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
    public class CheckDigitCalculator
    {
        public string GetCheckDigit(string value)
        {
            string routingNumber = value.Substring(0, 8);
            char[] digitArray = routingNumber.ToCharArray();
            int sum = 0;
            for (int i = 0; i < digitArray.Length; i++)
            {
                int position = i % 3;
                int digit = (int)digitArray[i] - '0';
                int factor = 0;
                switch (position)
                {
                    case 0:
                        factor = 5;
                        break;
                    case 1:
                        factor = 7;
                        break;
                    case 2:
                        factor = 1;
                        break;
                }
                sum += digit * factor;
            }
            return ((10 - (sum % 10))%10).ToString();
        }
    }
}
