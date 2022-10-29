using System;

namespace EFTN.Utility
{
    public class NumberToWordConverter
    {
        public static string GetAmountInWords(string AmountInNumber)
        {
            double amountInDouble = ParseData.StringToDouble(AmountInNumber);
            AmountInNumber = amountInDouble.ToString();

            if (AmountInNumber == "")
            {
                return "";
            }
            string HundredCrore = "";
            string Crore = "";
            string Lac = "";
            string Thousand = "";
            string Hundred = "";
            string Ten = "";
            string Paisa = "";

            string[] a = AmountInNumber.Split('.');
            int l = a[0].Length;
            if (l > 10)
            {
                return "Too long";
            }
            if (l > 9)
            {
                HundredCrore = a[0].Substring(0, l - 9);
                a[0] = a[0].Substring(l - 9, 9);
                l = a[0].Length;
            }

            if (l > 7)
            {
                Crore = a[0].Substring(0, l - 7);
                a[0] = a[0].Substring(l - 7, 7);
                l = a[0].Length;
            }
            if (l > 5)
            {
                Lac = a[0].Substring(0, l - 5);
                a[0] = a[0].Substring(l - 5, 5);
                l = a[0].Length;
            }
            if (l > 3)
            {
                Thousand = a[0].Substring(0, l - 3);
                a[0] = a[0].Substring(l - 3, 3);
                l = a[0].Length;
            }
            if (l > 2)
            {
                Hundred = a[0].Substring(0, l - 2);
                a[0] = a[0].Substring(l - 2);
                l = a[0].Length;
            }
            Ten = a[0];

            HundredCrore = GetWord(HundredCrore.PadLeft(2,'0'));
            if (HundredCrore != "")
            {
                HundredCrore = HundredCrore + " Hundred";
            }

            Crore = GetWord(Crore.PadLeft(2, '0'));
            if (Crore != "")
            {
                Crore = Crore + " Crore";
            }

            Lac = GetWord(Lac.PadLeft(2, '0'));
            if (Lac != "")
            {
                Lac = Lac + " Lac";
            }

            Thousand = GetWord(Thousand.PadLeft(2, '0'));
            if (Thousand != "")
            {
                Thousand = Thousand + " Thousand";
            }

            Hundred = GetWord(Hundred.PadLeft(2, '0'));
            if (Hundred != "")
            {
                Hundred = Hundred + " Hundred";
            }
            Ten = GetWord(Ten.PadLeft(2, '0'));

            if (a.Length > 1)
            {
                Paisa = a[1].PadRight(2, '0');
                Paisa = GetWord(Paisa);
                if (Paisa != "")
                {
                    Paisa = "and " + Paisa + " Paisa";
                }
            }
            string returnstring = HundredCrore + " "+ Crore + " " + Lac + " " + Thousand + " " + Hundred.Trim() + " " + Ten + " Taka " + Paisa + " Only";
            return returnstring.Trim().Replace("   ", " ").Replace("  ", " ");
        }

        private static string GetWord(string ij)
        {
            string word = "";
            string i = ij.Substring(0, 1);
            string j = ij.Substring(1, 1);
            if (i == "1")
            {
                switch (ij)
                {
                    case "10":
                        word = "Ten";
                        break;
                    case "11":
                        word = "Eleven";
                        break;
                    case "12":
                        word = "Twelve";
                        break;
                    case "13":
                        word = "Thirten";
                        break;
                    case "14":
                        word = "Fourteen";
                        break;
                    case "15":
                        word = "Fifteen";
                        break;
                    case "16":
                        word = "Sixteen";
                        break;
                    case "17":
                        word = "Seventeen";
                        break;
                    case "18":
                        word = "Eighteen";
                        break;
                    case "19":
                        word = "Nineteen";
                        break;
                }
            }
            else
            {
                switch (i)
                {
                    case "2":
                        word = "Twenty ";
                        break;
                    case "3":
                        word = "Thirty ";
                        break;
                    case "4":
                        word = "Forty ";
                        break;
                    case "5":
                        word = "Fifty ";
                        break;
                    case "6":
                        word = "Sixty ";
                        break;
                    case "7":
                        word = "Seventy ";
                        break;
                    case "8":
                        word = "Eighty ";
                        break;
                    case "9":
                        word = "Ninety ";
                        break;
                }

                switch (j)
                {
                    case "1":
                        word = word + " One";
                        break;
                    case "2":
                        word = word + " Two";
                        break;
                    case "3":
                        word = word + " Three";
                        break;
                    case "4":
                        word = word + " Four";
                        break;
                    case "5":
                        word = word + " Five";
                        break;
                    case "6":
                        word = word + " Six";
                        break;
                    case "7":
                        word = word + "Seven";
                        break;
                    case "8":
                        word = word + " Eight";
                        break;
                    case "9":
                        word = word + " Nine";
                        break;
                }
            }
            return word;
        }
    }
}
