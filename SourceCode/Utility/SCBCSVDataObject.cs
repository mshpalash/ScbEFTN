using System;
using System.Collections.Generic;

using System.Web;
using System.Text.RegularExpressions;

namespace EFTN.Utility
{
    public class SCBCSVDataObject
    {
        //public void SCBCSVDataObject()
        //{
        //    InitializeObject();
        //}

        public void InitializeObject()
        {
            //set all variable to empty or zero
            this.FlagAccountNoCharacter = 0;
            this.FlagAccountNoLength = 0;
            this.FlagAmountCharacter = 0;
            this.FlagAmountLength = 0;
            this.FlagDFIAccountNoCharacter = 0;
            this.FlagDFIAccountNoLength = 0;
            this.FlagIdNumberCharacter = 0;
            this.FlagIdNumberLength = 0;
            this.FlagReceiverNameCharacter = 0;
            this.FlagReceiverNameLength = 0;
            this.FlagReceivingBankRTCharacter = 0;
            this.FlagReceivingBankRTLength = 0;
            this.FlagReceivingBankRTOnUs = 0;
        }

        private int flagReceivingBankRTCharacter;
        public int FlagReceivingBankRTCharacter
        {
            get { return flagReceivingBankRTCharacter; }
            set { flagReceivingBankRTCharacter = value; }
        }

        private int flagReceivingBankRTLength;
        public int FlagReceivingBankRTLength
        {
            get { return flagReceivingBankRTLength; }
            set { flagReceivingBankRTLength = value; }
        }

        private int flagReceivingBankRTOnUs;
        public int FlagReceivingBankRTOnUs
        {
            get { return flagReceivingBankRTOnUs; }
            set { flagReceivingBankRTOnUs = value; }
        }

        private int flagAmountCharacter;
        public int FlagAmountCharacter
        {
            get { return flagAmountCharacter; }
            set { flagAmountCharacter = value; }
        }

        private int flagAmountLength;
        public int FlagAmountLength
        {
            get { return flagAmountLength; }
            set { flagAmountLength = value; }
        }

        private int flagIdNumberCharacter;
        public int FlagIdNumberCharacter
        {
            get { return flagIdNumberCharacter; }
            set { flagIdNumberCharacter = value; }
        }

        private int flagIdNumberLength;
        public int FlagIdNumberLength
        {
            get { return flagIdNumberLength; }
            set { flagIdNumberLength = value; }
        }

        private int flagReceiverNameCharacter;
        public int FlagReceiverNameCharacter
        {
            get { return flagReceiverNameCharacter; }
            set { flagReceiverNameCharacter = value; }
        }

        private int flagReceiverNameLength;
        public int FlagReceiverNameLength
        {
            get { return flagReceiverNameLength; }
            set { flagReceiverNameLength = value; }
        }

        private int flagDFIAccountNoLength;
        public int FlagDFIAccountNoLength
        {
            get { return flagDFIAccountNoLength; }
            set { flagDFIAccountNoLength = value; }
        }

        private int flagAccountNoLength;
        public int FlagAccountNoLength
        {
            get { return flagAccountNoLength; }
            set { flagAccountNoLength = value; }
        }

        private int flagDFIAccountNoCharacter;
        public int FlagDFIAccountNoCharacter
        {
            get { return flagDFIAccountNoCharacter; }
            set { flagDFIAccountNoCharacter = value; }
        }

        private int flagAccountNoCharacter;
        public int FlagAccountNoCharacter
        {
            get { return flagAccountNoCharacter; }
            set { flagAccountNoCharacter = value; }
        }

        public bool IsNaturalNumber(String strNumber)
        {
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(strNumber) &&
            objNaturalPattern.IsMatch(strNumber);
        }
        // Function to test for Positive Integers with zero inclusive 
        public bool IsWholeNumber(String strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }
        // Function to Test for Integers both Positive & Negative 
        public bool IsInteger(String strNumber)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
            return !objNotIntPattern.IsMatch(strNumber) && objIntPattern.IsMatch(strNumber);
        }
        // Function to Test for Positive Number both Integer & Real 
        public bool IsPositiveNumber(String strNumber)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return !objNotPositivePattern.IsMatch(strNumber) &&
            objPositivePattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber);
        }
        // Function to test whether the string is valid number or not
        public bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
        }
        // Function To test for Alphabets. 
        public bool IsAlpha(String strToCheck)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }
        // Function to Check for AlphaNumeric.
        public bool IsAlphaNumeric(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        public bool IsAlphaNumericForEFT(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9/.#()&: ,'_%-]"); //comma(,) and appose(') to be Percent(%)considered as per SCB request
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        public bool IsEFTAccountNumber(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9/.#()&:-]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }
    }
}