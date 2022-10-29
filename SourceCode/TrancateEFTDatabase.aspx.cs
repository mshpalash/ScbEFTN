using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EFTN.component;

namespace EFTN
{
    public partial class TrancateEFTDatabase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTrancateEFTDb_Click(object sender, EventArgs e)
        {
            //TrancateDatabase trancDB = new TrancateDatabase();
            //trancDB.TrancateEFTDB();
        }

        protected void lnkBtnClearFolder_Click(object sender, EventArgs e)
        {
            //string fileType = "EFTTransactionExport";
            //string path = ConfigurationManager.AppSettings[fileType];
            //string[] transactionSentFiles = System.IO.Directory.GetFiles(path, "*.XML");
            //foreach (string childFile in transactionSentFiles)
            //{
            //    System.IO.File.Delete(childFile);
            //}

            //fileType = "EFTReturnExport";
            //path = ConfigurationManager.AppSettings[fileType];
            //transactionSentFiles = System.IO.Directory.GetFiles(path, "*.XML");
            //foreach (string childFile in transactionSentFiles)
            //{
            //    System.IO.File.Delete(childFile);
            //}

            //fileType = "EFTDishonouredReturnExport";
            //path = ConfigurationManager.AppSettings[fileType];
            //transactionSentFiles = System.IO.Directory.GetFiles(path, "*.XML");
            //foreach (string childFile in transactionSentFiles)
            //{
            //    System.IO.File.Delete(childFile);
            //}

            //fileType = "EFTContestedExport";
            //path = ConfigurationManager.AppSettings[fileType];
            //transactionSentFiles = System.IO.Directory.GetFiles(path, "*.XML");
            //foreach (string childFile in transactionSentFiles)
            //{
            //    System.IO.File.Delete(childFile);
            //}

            //fileType = "EFTNOCExport";
            //path = ConfigurationManager.AppSettings[fileType];
            //transactionSentFiles = System.IO.Directory.GetFiles(path, "*.XML");
            //foreach (string childFile in transactionSentFiles)
            //{
            //    System.IO.File.Delete(childFile);
            //}

            //fileType = "EFTRNOCExport";
            //path = ConfigurationManager.AppSettings[fileType];
            //transactionSentFiles = System.IO.Directory.GetFiles(path, "*.XML");
            //foreach (string childFile in transactionSentFiles)
            //{
            //    System.IO.File.Delete(childFile);
            //}

            //fileType = "EFTCBSExport";
            //path = ConfigurationManager.AppSettings[fileType];
            //transactionSentFiles = System.IO.Directory.GetFiles(path, "*.XML");
            //foreach (string childFile in transactionSentFiles)
            //{
            //    System.IO.File.Delete(childFile);
            //}
        }
    }
}
