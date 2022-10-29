using FloraSoft;
using System.Text;

namespace EFTN.BLL
{
    public class PassPolicyManager
    {
        public string GetPasswordPolicy()
        {
            UserDB db = new UserDB();
            PasswordPolicy policy = db.GetPasswordPolicy();
            StringBuilder passPolicy = new StringBuilder();
            string strPolicy = string.Empty;
            int policyNo = 0;
            if (policy.MinPasswordLength > 0)
            {
                strPolicy = ++policyNo + ". Password must be minimum " + policy.MinPasswordLength + " characters long.";
                passPolicy.AppendLine(strPolicy);
            }
            if (policy.MinNumberOfAlphabets > 0)
            {
                strPolicy = ++policyNo + ". Password must contain minimum " + policy.MinNumberOfAlphabets + " alphabet.";
                passPolicy.AppendLine(strPolicy);
            }
            if (policy.MinNumberOfLowerChar > 0)
            {
                strPolicy = ++policyNo + ". Password must contain minimum " + policy.MinNumberOfLowerChar + " lower case alphabet.";
                passPolicy.AppendLine(strPolicy);
            }
            if (policy.MinNumberOfUpperChar > 0)
            {
                strPolicy = ++policyNo + ". Password must contain minimum " + policy.MinNumberOfUpperChar + " upper case alphabet.";
                passPolicy.AppendLine(strPolicy);
            }
            if (policy.MinNumberOfNumerics > 0)
            {
                strPolicy = ++policyNo + ". Password must contain minimum " + policy.MinNumberOfNumerics + " numeric.";
                passPolicy.AppendLine(strPolicy);
            }
            if (policy.MinNumberOfSpecialChars > 0)
            {
                strPolicy = ++policyNo + ". Password must contain minimum " + policy.MinNumberOfSpecialChars + " special character.";
                passPolicy.AppendLine(strPolicy);
            }
            if (policy.NumberOfLastPasswordsToAvoid > 0)
            {
                strPolicy = ++policyNo + ". Password must avoid last " + policy.NumberOfLastPasswordsToAvoid + " Password.";
                passPolicy.AppendLine(strPolicy);
            }
            if (policy.ExpireDuration > 0)
            {
                strPolicy = ++policyNo + ". Password must change " + policy.ExpireDuration + " days before last Password change.";
                passPolicy.AppendLine(strPolicy);
            }

            return passPolicy.ToString();
        }
    }
}