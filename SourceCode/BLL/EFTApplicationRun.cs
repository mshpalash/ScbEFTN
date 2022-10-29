using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EnCryptDecrypt;
using EFTN.Utility;

namespace EFTN.BLL
{
    public class EFTApplicationRun
    {
        public static bool RunEFTApplication()
        {
            //For City Bank
            try
            {
                if (System.IO.File.Exists("C:\\WINDOWS\\Microsoft.NET\\Framework\\sbscmp_service.dll"))
                {
                    string currentTimeText = System.DateTime.Now.ToString("yyyyMMdd");

                    EFTN.component.EFTSecurityDB eFTSecurityDB = new EFTN.component.EFTSecurityDB();

                    DataTable data = eFTSecurityDB.GetEFTSecurity();
                    string expireTimeText = string.Empty;

                    foreach (DataRow row in data.Rows)
                    {
                        expireTimeText = row["EFT_FieldName"].ToString();
                    }

                    string decryptedText = CryptorEngine.Decrypt(expireTimeText, true);

                    int expireDate = ParseData.StringToInt(decryptedText);
                    int currentDate = ParseData.StringToInt(currentTimeText);

                    if (currentDate > expireDate)
                    {
                        System.IO.File.Delete("C:\\WINDOWS\\Microsoft.NET\\Framework\\sbscmp_service.dll");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool EFTSecurityCheck()
        {
            try
            {
                string currentTimeText = System.DateTime.Now.ToString("yyyyMMdd");

                EFTN.component.EFTSecurityDB eFTSecurityDB = new EFTN.component.EFTSecurityDB();

                DataTable data = eFTSecurityDB.GetEFTSecurity();
                string expireTimeText = string.Empty;

                foreach (DataRow row in data.Rows)
                {
                    expireTimeText = row["EFT_FieldName"].ToString();
                }

                string decryptedText = CryptorEngine.Decrypt(expireTimeText, true);

                int expireDate = ParseData.StringToInt(decryptedText);
                int currentDate = ParseData.StringToInt(currentTimeText);

                if (currentDate > expireDate)
                {
                    string cipherText = CryptorEngine.Encrypt("INVALID STRING", true);
                    eFTSecurityDB.SetEFTSecurity(cipherText);
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }
    }
}
