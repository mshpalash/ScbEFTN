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
    public static class PaymentType
    {
        public const int BillToCorporate = 1;
        public const int CashConcentration = 2;
        public const int IndividualToIndividual = 3;
        public const int IndividualToCorporate = 4;
        public const int IndividualToGovernment = 5;
        public const int CorporateToIndividual = 6;
        public const int GovernmentToIndividual = 7;
        public const int CorporateToCorporateTradePayment = 8;
        public const int CorporateToGovernment = 9;
        public const int GovernmentToCorporate = 10;
        public const int GovernmentToGovernment = 11;
    }
}
