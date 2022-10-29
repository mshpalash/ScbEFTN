using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace FloraSoft
{
    public class SamlLogManager
    {
        public static void WriteLog(string Msg)
        {
            //FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["SamlLogPath"]) + "\\SAML_LOG_" + System.DateTime.Today.ToString("yyyyMMdd") + ".log", FileMode.OpenOrCreate, FileAccess.Write);
            FileStream fs = new FileStream(ConfigurationManager.AppSettings["SamlLogPath"] +"\\SAML_LOG_" + System.DateTime.Today.ToString("yyyyMMdd") + ".log", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(System.DateTime.Now.ToString() + " : " + Msg);
            sw.Close();
            sw.Dispose();
            fs.Close();
            fs.Dispose();
        }
    }
}