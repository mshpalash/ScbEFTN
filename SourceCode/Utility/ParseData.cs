using System;
using System.Collections.Generic;

using System.Web;

namespace EFTN.Utility
{
    public class ParseData
    {
        public static double StringToDouble(string stringValue)
        {
            if (IsDouble(stringValue))
            {
                return double.Parse(stringValue);
            }
            else
            {
                return 0;
            }
        }

        public static int StringToInt(string stringValue)
        {
            if (IsInt(stringValue))
            {
                return int.Parse(stringValue);
            }
            else
            {
                return 0;
            }
        }

        public static long StringToLong(string stringValue)
        {
            if (IsLong(stringValue))
            {
                return long.Parse(stringValue);
            }
            else
            {
                return 0;
            }
        }

        public static decimal StringToDecimal(string stringValue)
        {
            if (IsDecimal(stringValue))
            {
                return decimal.Parse(stringValue);
            }
            else
            {
                return 0;
            }
        }

        private static bool IsDouble(object Expression)
        {
            double retNum;
            return Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        private static bool IsInt(object Expression)
        {
            int retNum;
            return int.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        private static bool IsLong(object Expression)
        {
            long retNum;
            return long.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        private static bool IsDecimal(object Expression)
        {
            Decimal retNum;
            return Decimal.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

    }
}
