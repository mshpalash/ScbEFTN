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
    public static class TransactionCodes
    {
        public const int AutomatedReturnOrNotificationOfChange = 21;
        public const int CreditCurrentAcc = 22;
        public const int PrenotificationOfDemandCreditAuthorization = 23;
        public const int ZeroTakaWithRemittanceData= 24;
        public const int CreditSavingsAcc= 32;
        public const int DebitCurrentAcc = 27;
        public const int DebitSavingsAcc = 37;

        public const string EFTTransactionTypeDebit = "Debit";
        public const string EFTTransactionTypeCredit = "Credit";

    }
}
