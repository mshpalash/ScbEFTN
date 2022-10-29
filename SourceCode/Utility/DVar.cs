namespace EFTN.BLL
{
    public static class DVar
    {
        public static string TransDateTime
        {
            get { return System.DateTime.Now.ToString("yyyyMMddHHmmss"); }
        }
        public static string CaptureDate
        {
            get { return System.DateTime.Now.ToString("yyyyMMdd"); }
        }
        public static string FunctionCode
        {
            get { return "200"; }
        }
        public static string AcqIdentCode
        {
            get { return "12345678901"; }
        }
        public static string DeviceID
        {
            get { return "CBLEFT"; }
        }
        public static string CardAcceptorID
        {
            get { return "CITY"; }
        }
        public static string CardAcceptorName
        {
            get { return "CITY>DHAKA                            BD"; }
        }
        public static string AmountFees
        {
            get { return "00BDTC000000000000000000000001D0000000000000000BDT"; }
        }
        public static string TransCurrency
        {
            get { return "050"; }
        }
        public static string DelvChannelID
        {
            get { return "CBL"; }
        }
        public static string TerminalType
        {
            get { return "EFT"; }
        }
        public static string ReservedCode
        {
            get { return "202759/000630202759"; }
        }
        public static string EftAccountNo
        {
            get { return "1006110020902"; }
        }
    }
}
