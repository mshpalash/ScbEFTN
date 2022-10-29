namespace EFTN.Utility
{
    public class StandingOrderTransactionFrequency
    {
        public static int GetTransactionFrequency(string strTransFreq)
        {
            int transactionFreq = 0;
            switch (strTransFreq)
            {
                case "D": transactionFreq = 1;
                    break;
                case "W": transactionFreq = 7;
                    break;
                case "F": transactionFreq = 15;
                    break;
                case "M": transactionFreq = 30;
                    break;
                case "Q": transactionFreq = 90;
                    break;
                case "H": transactionFreq = 180;
                    break;
                case "Y": transactionFreq = 365;
                    break;
            }

            return transactionFreq;
        }
    }
}