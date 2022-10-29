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
using EFTN.Utility;
//using Microsoft.SqlServer.Dts.Runtime;
using EFTN.BLL;

namespace EFTN
{
    public partial class EFTDataTransferToPIBS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    Application app = new Application();
            //    Package package = app.LoadPackage("D:\\PBM\\EFT\\EFTtoPIBSPackage.dtsx", null);
            //    DTSExecResult result = package.Execute();

            //    Msg.Text = result.ToString();
            //}
            //catch(Exception ex)
            //{
            //    Msg.Text = ex.Message;
            //}
        }

        protected void linkBtnInsertInwardTransaction_Click(object sender, EventArgs e)
        {
            PIBSManager pibsManager = new PIBSManager();
            int noOfItem = pibsManager.InsertInwardTransactionToPIBS();
            Msg.Text = noOfItem.ToString() + " inward items inserted";
        }

        protected void linkBtnSendOutwardReturnFromPIBStoFlora_Click(object sender, EventArgs e)
        {
            PIBSManager pibsManager = new PIBSManager();
            int updatedItem = pibsManager.SendOutwardReturnFromPIBStoFlora();
            if (updatedItem != -1)
            {
                Msg.Text = updatedItem + " Outward Return Updated from PIBS Successfully";
                Msg.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                Msg.Text = "Mismatched Outward Return with the PIBS and Flora EFT";
                Msg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void linkBtnSendApprovedInwardTxnFromPIBStoFlora_Click(object sender, EventArgs e)
        {
            
            PIBSManager pibsManager = new PIBSManager();
            int updatedItem = pibsManager.UpdateApprovedInwardTransactionByPIBSToFlora();
            if (updatedItem != -1)
            {
                Msg.Text = updatedItem+ " Approved Inward Transactions Updated from PIBS Successfully";
                Msg.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                Msg.Text = "Mismatched Approved Inward Transaction with the PIBS and Flora EFT";
                Msg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
