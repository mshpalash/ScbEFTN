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
    public static class TransactionStatus
    {
        public const int TransSent_Data_Entered = 1;
        public const int TransSent_Rejected_By_Checker = 2;
        public const int TransSent_Approved_By_Checker = 3;
        public const int TransSent_XMLCreated = 4;
        public const int TransReceived_Imported = 5;
        public const int TransReceived_Approved = 6;
        public const int TransReceived_Reject_NOC = 7;
        public const int TransReceived_Reject_RR = 8;
        public const int RRSent_Rejected_By_Checker = 9;
        public const int RRSentApprovedByChecker = 10;
        public const int RRSent_XML_Created = 11;
        public const int NOCSent_Rejected_By_Checker = 12;
        public const int NOCSent_Approved_By_Checker = 13;
        public const int NOCSent_XML_Created = 14;
        public const int Return_Received_Imported = 15;
        public const int Return_Received_Approved = 16;
        public const int Return_Received_Dishonor = 17;
        public const int Return_Received_Rejected_By_Checker = 18;
        public const int Return_Received_Dishonor_Approved_by_checker = 19;
        public const int Return_Received_Approval__Approved_by_checker = 20;
        public const int Dishonor_sent_XML_Created = 21;
        public const int NOC__Received_Imported = 22;
        public const int NOC_Received_Approved = 23;
        public const int NOC_Received_RNOC = 24;
        public const int NOC_Received_Rejected_By_Checker = 25;
        public const int NOC_Received_RNOC_Approved_by_checker = 26;
        public const int NOC__Received_Approval__Approved_by_checker = 27;
        public const int RNOC__sent_XML_Created = 28;

        public const int RNOC_Received_Imported = 29;
        public const int Dishonor_Received_Imported = 30;
        public const int Dishonor_Received_Approved = 31;
        public const int Dishonor_Received_Contested = 32;
        public const int Dishonor_Received_Rejected_By_Checker = 33;
        public const int DishonorReceived_Contested_Approved_by_checker = 34;
        public const int Dishonor_Received_Approval_Approved_by_checker = 35;
        public const int Contested_sent_XML_Created = 36;
        public const int Contested_Received_Imported = 37;
        public const int Transaction_Received_Approved_By_Checker = 38;
        public const int Transaction_Received_Rejected_By_Checker = 39;
        public const int Rejected_By_Checker_Return_Received_Approved = 40;
    }
}
