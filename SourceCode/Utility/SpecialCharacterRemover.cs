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
    public static class SpecialCharacterRemover
    {
        public static string RemoveMultipleSpaceWithSingleSpace(string strData)
        {
            string strResult = System.Text.RegularExpressions.Regex.Replace(strData, @"\s{1,}", " ");
            return strResult;
        }

        public static string RemoveSpecialCharacter(string strData)
        {
            string strResult = strData.Replace("&apos;", "");
            strResult = System.Text.RegularExpressions.Regex.Replace(strResult, @"\s{1,}", " ");
            return strResult;
        }

        public static string RemoveAllSpecialCharacter(string strData)
        {
            string strResult = strData.Replace("&apos;", "");
            strResult = System.Text.RegularExpressions.Regex.Replace(strResult, @"\s{1,}", " ");
            return strResult;
        }
    }
}