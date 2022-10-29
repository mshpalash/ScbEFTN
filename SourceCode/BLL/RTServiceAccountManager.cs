using System;
using System.Net;
using System.Configuration;
using System.IO;

namespace EFTN.BLL
{
    public class RTServiceAccountManager
    {

        public string GetCBSData(string AccountNo)
        {
            string BrnCd = AccountNo.Substring(0, 3);
            string ActNo = AccountNo;
            string result = "";
            WebRequest req = null;
            WebResponse rsp = null;
            try
            {
                string uri = ConfigurationManager.AppSettings["CBSAccountURL"];
                req = (HttpWebRequest)WebRequest.Create(uri);
                req.Method = "POST";
                req.ContentType = "text/xml";
                req.Credentials = CredentialCache.DefaultNetworkCredentials;
                StreamWriter writer = new StreamWriter(req.GetRequestStream());
                writer.WriteLine(GetAccountInquiryXML(BrnCd, ActNo));
                writer.Close();
                rsp = (HttpWebResponse)req.GetResponse();

                StreamReader sr = new StreamReader(rsp.GetResponseStream());
                result = sr.ReadToEnd();
                sr.Close();

            }
            catch (Exception ex)
            {
                result = "Failed";
            }
            finally
            {
                if (!result.Equals("Failed"))
                {
                    if (req != null) req.GetRequestStream().Close();
                    if (rsp != null) rsp.GetResponseStream().Close();
                }
            }
            return result;
        }

        private string GetAccountInquiryXML(string BranchCode, string AccountNo)
        {
            string str =
                "<env:Envelope xmlns:env=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                 "<env:Header/>" +
                 "<env:Body>" +
                  "<QUERYCUSTACC_IOFS_REQ xmlns=\"http://fcubs.ofss.com/service/FCUBSAccService\">" +
                   "<FCUBS_HEADER>" +
                    "<SOURCE>" + ConfigurationManager.AppSettings["RT_SERVICE_ACC_SOURCE"] + "</SOURCE>" +
                    "<UBSCOMP>FCUBS</UBSCOMP>" +
                    "<MSGID/>" +
                    "<CORRELID/>" +
                    "<USERID>SYSTEM</USERID>" +
                    "<BRANCH>" + BranchCode + "</BRANCH>" +
                    "<MODULEID>ST</MODULEID>" +
                    "<SERVICE>FCUBSAccService</SERVICE>" +
                    "<OPERATION>QueryCustAcc</OPERATION>" +
                    "<SOURCE_OPERATION>QueryCustAcc</SOURCE_OPERATION>" +
                    "<SOURCE_USERID/>" +
                    "<DESTINATION/>" +
                    "<MULTITRIPID/>" +
                    "<FUNCTIONID/>" +
                    "<ACTION/>" +
                    "<MSGSTAT>SUCCESS</MSGSTAT>" +
                    "<PASSWORD/>" +
                    "<ADDL>" +
                     "<PARAM>" +
                      "<NAME/>" +
                      "<VALUE/>" +
                     "</PARAM>" +
                    "</ADDL>" +
                   "</FCUBS_HEADER>" +
                  "<FCUBS_BODY>" +
                    "<Cust-Account-IO>" +
                     "<BRN>" + BranchCode + "</BRN>" +
                     "<ACC>" + AccountNo + "</ACC>" +
                     "<CRS_STST_REQD>N</CRS_STST_REQD>" +
                    "</Cust-Account-IO>" +
                   "</FCUBS_BODY>" +
                  "</QUERYCUSTACC_IOFS_REQ>" +
                 "</env:Body>" +
                "</env:Envelope>";

            return str;
        }

        private string GetTextFromXMLFile(string file)
        {
            StreamReader reader = new StreamReader(file);
            string ret = reader.ReadToEnd();
            reader.Close();
            return ret;
        }
    }
}
