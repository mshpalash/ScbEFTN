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
    public class FileIDModifierCalculator
    {
        public string GetFileIDModifier()
        {
            Random rand = new Random();

            int code = rand.Next(35);
            if (code >=  26)
            {
                return (code-26).ToString();
            }
            else
            {
                int modifier = 'A' + code;
                return ((char) modifier).ToString();
            }
        }

        public string GetFileIDModifier(int code)
        {
            //Random rand = new Random();

            //int code = rand.Next(35);
            if (code >= 26)
            {
                return (code - 26).ToString();
            }
            else
            {
                int modifier = 'A' + code;
                return ((char)modifier).ToString();
            }
        }
    }
}
