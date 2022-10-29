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
    public enum CodeType
    {
        Return = 2,
        NOC = 3,
        RNOC = 4,
        DishonourReturn = 5,
        Contested = 6
    }
}
