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
    public enum ItemType
    {
        TransactionSent=1,
        TransactionReceived=2,
        ReturnSent=3,
        ReturnReceived=4,
        NOCSent=5,
        NOCReceived=6,
        DishonouredSent=7,
        DishonouredReceived=8,
        RNOCSent=9,
        RNOCReceived=10,
        ContestedSent=11,
        ContestedReceived=12
    }
}
