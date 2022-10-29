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
    public class RoutingNumberValidator
    {
        public static bool CheckDigitOk(String RoutingNo)
        {
            int wght = 571;
            if (RoutingNo.Length != 9)
            {
                return false;
            }
            int length = (int)Math.Log10(wght) + 1;
            int[] WghtArray = new int[length];
            for (int i = length - 1; i >= 0 && wght > 0; i--)
            {
                WghtArray[i] = wght % 10;
                wght /= 10;
            }
            int num = 0;
            for (int i = 0; i < RoutingNo.Length - 1; i++)
            {
                num += (int)(RoutingNo[i] - '0') * WghtArray[i % length];
            }
            num = (10 - num % 10) % 10;
            return (num == (int)(RoutingNo[RoutingNo.Length - 1] - '0'));
        }
    }
}
