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
    public static class DataEntryType
    {
        public const string Manual = "Manual";
        public const string Excel = "Excel";
        public const string EncryptedExcel = ".exl";
        public const string CSV = ".csv";
        public const string Text = ".txt";
    }
}
