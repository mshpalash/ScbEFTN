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
    public class SetSettlementDate
    {

        public DateTime GetOutwardTransactionSettlementDate()
        {
            DateTime settlementDate = System.DateTime.Today;
            EFTN.component.SettlementDateDB settlementDateDB = new EFTN.component.SettlementDateDB();
            settlementDate = settlementDateDB.GetSettlementJDateByEntryDate(System.DateTime.Today);
            return settlementDate;
        }

        public DateTime GetInwardSettlementDate(DateTime settlementDate)
        {
            EFTN.component.SettlementDateDB settlementDateDB = new EFTN.component.SettlementDateDB();
            DateTime returnSettlementDate = settlementDateDB.GetSettlementJDateByEntryDate(settlementDate);
            return returnSettlementDate;
        }


        //public DateTime GetInwardSettlementDate(DateTime settlementDate)
        //{

        //    DateTime returnSettlementDate = settlementDate.AddDays(1);
        //    string returnDate = string.Empty;


        //    bool returnCondition = true;
        //    while (returnCondition)
        //    {
        //        returnDate = returnSettlementDate.DayOfWeek.ToString();
        //        if (returnDate.Equals("Friday"))
        //        {
        //            returnSettlementDate = returnSettlementDate.AddDays(2);
        //        }

        //        EFTN.component.SettlementDateDB settlementDateDB = new EFTN.component.SettlementDateDB();
        //        DataTable dtSettlementDate = settlementDateDB.GetSettlementDateForTransaction(
        //                                        returnSettlementDate.Year.ToString().PadLeft(4, '0')
        //                                        + returnSettlementDate.Month.ToString().PadLeft(2, '0')
        //                                        + returnSettlementDate.Day.ToString().PadLeft(2, '0'));
        //        if (dtSettlementDate.Rows.Count > 0)
        //        {
        //            returnSettlementDate = returnSettlementDate.AddDays(1);
        //        }
        //        else
        //        {
        //            returnCondition = false;
        //        }
        //    }


        //    return returnSettlementDate;
        //}


        //public DateTime GetOutwardTransactionSettlementDate()
        //{
        //    DateTime settlementDate = System.DateTime.Today;
        //    DateTime TrSentSettlementDate = settlementDate.AddDays(1);
        //    string TrSentDate = string.Empty;


        //    bool TrSentCondition = true;
        //    while (TrSentCondition)
        //    {
        //        TrSentDate = TrSentSettlementDate.DayOfWeek.ToString();
        //        if (TrSentDate.Equals("Friday"))
        //        {
        //            TrSentSettlementDate = TrSentSettlementDate.AddDays(2);
        //        }

        //        EFTN.component.SettlementDateDB settlementDateDB = new EFTN.component.SettlementDateDB();
        //        DataTable dtSettlementDate = settlementDateDB.GetSettlementDateForTransaction(
        //                                        TrSentSettlementDate.Year.ToString().PadLeft(4, '0')
        //                                        + TrSentSettlementDate.Month.ToString().PadLeft(2, '0')
        //                                        + TrSentSettlementDate.Day.ToString().PadLeft(2, '0'));
        //        if (dtSettlementDate.Rows.Count > 0)
        //        {
        //            TrSentSettlementDate = TrSentSettlementDate.AddDays(1);
        //        }
        //        else
        //        {
        //            TrSentCondition = false;
        //        }
        //    }


        //    return TrSentSettlementDate;
        //}
    }
}
