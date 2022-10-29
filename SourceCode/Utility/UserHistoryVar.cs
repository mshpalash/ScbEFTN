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
    public static class UserHistoryVar
    {
        public const string PASSWORD_CHANGED            = "PASSWORDCHANGED";
        public const string LOCKED_FOR_WRONGPASSWORD    = "LOCKED_WRONGPASSWORD";
        public const string USER_ACTIVE                 = "ACTIVE";
        public const string USER_INACTIVE               = "INACTIVE";
        public const string NEW_USER_CREATED            = "USER_CREATED";
        public const string USER_INFORMATION_UPDATED    = "USER_INFORMATION_UPDATED";
        public const string PASSWORD_RESET              = "PASSWORD_RESET";
        public const string LOGGED_IN                   = "LOGGED_IN";
        public const string STATUS_CHANGED              = "STATUS_CHANGED";
        public const string STATUS_DELETE               = "DELETE";
    }
}
