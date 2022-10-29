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
using System.Data.SqlClient;

namespace EFTN
{
    public partial class InwardDishonor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            EFTN.component.ReceivedDishonorDB receivedReturnDB = new EFTN.component.ReceivedDishonorDB();
            dtgInwardDishonorList.DataSource = receivedReturnDB.GetReceivedDishonor();
            dtgInwardDishonorList.DataBind();

            EFTN.component.CodeLookUpDB codeLookUpDb = new EFTN.component.CodeLookUpDB();
            ddlContested.DataSource = codeLookUpDb.GetCodeLookUp((int)EFTN.Utility.CodeType.Contested);
            ddlContested.DataBind();   


            if (Request.Params["ReturnTraceNumber"] != null)
            {
                EFTN.component.ReceivedDishonorDB receivedDishonorDB = new EFTN.component.ReceivedDishonorDB();
                dtgMatchedSentEDR.DataSource = receivedDishonorDB.GetSentEDRByReturnTraceNoForDishonorReceived(Request.Params["ReturnTraceNumber"]);
                dtgMatchedSentEDR.DataBind();

            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            UpdateInwardDishonor("Approve");
        }

        protected void btnContested_Click(object sender, EventArgs e)
        {
            UpdateInwardDishonor("Contested");
        }

        private void UpdateInwardDishonor(string makerDecision)
        {
            int CreatedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

            if (Request.Params["ReturnTraceNumber"] != null)
            {
                //ChangeStatus(EFTN.Utility.TransactionStatus.Dishonor_Received_Approved, new Guid(Request.Params["ReturnID"]), "", "");
                EFTN.component.ReceivedDishonorDB receivedDishonorDB = new EFTN.component.ReceivedDishonorDB();
                SqlDataReader sqlDR = receivedDishonorDB.GetSentEDRByReturnTraceNoForDishonorReceived(Request.Params["ReturnTraceNumber"]);
                int StatusID;
                Guid DishonoredID = new Guid();
                //string ContestedDishonoredCode;

                if (sqlDR.Read())
                {
                    DishonoredID = (Guid)sqlDR["DishonoredID"];
                    if (makerDecision.Equals("Approve"))
                    {
                        StatusID = EFTN.Utility.TransactionStatus.Dishonor_Received_Approved;
                    }
                    else
                    {
                        StatusID = EFTN.Utility.TransactionStatus.Dishonor_Received_Contested;
                    }

                    UpdateDisHonoredStatus(StatusID, DishonoredID, ddlContested.SelectedValue, CreatedBy, CreatedBy);
                    sqlDR.Close();
                    sqlDR.Dispose();
                }
            }
            Response.Redirect("InwardDishonor.aspx");
        }

        private void UpdateDisHonoredStatus(int StatusID,
                                            Guid DishonoredID,
                                            string ContestedDishonoredCode,
                                            int CreatedBy,
                                            int ApprovedBy)
        {
            EFTN.component.ReceivedDishonorDB receivedDishonorDB = new EFTN.component.ReceivedDishonorDB();
            receivedDishonorDB.UpdateDishonorStatus(StatusID, DishonoredID, ContestedDishonoredCode, CreatedBy, ApprovedBy);
        }
    }
}
