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
using System.Text;

namespace EFTN
{
    public partial class ExportReturn : System.Web.UI.Page
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
            EFTN.component.ReceivedBatchDB db = new EFTN.component.ReceivedBatchDB();
            dtgBatchReturnSent.DataSource = db.GetBatches_For_RRSent();
            dtgBatchReturnSent.DataBind();
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgBatchReturnSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchReturnSent.Items[i].FindControl("cbxSentBatch");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void btnGenerateXml_Click(object sender, EventArgs e)
        {
            //bool needEnvelope = false;
            //EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            //for (int i = 0; i < dtgBatchReturnSent.Items.Count; i++)
            //{
            //    CheckBox cbx = (CheckBox)dtgBatchReturnSent.Items[i].FindControl("cbxSentBatch");
            //    if (cbx.Checked)
            //    {
            //        needEnvelope = true;
            //        break;
            //    }
            //}
            ////if (needEnvelope)
            ////{
            ////    StringBuilder allXml = new StringBuilder();
            ////    EFTN.BLL.ReturnXML returnXML = new EFTN.BLL.ReturnXML();
            ////    allXml.Append(returnXML.GetFHRXML());

            ////    for (int i = 0; i < dtgBatchReturnSent.Items.Count; i++)
            ////    {
            ////        CheckBox cbx = (CheckBox)dtgBatchReturnSent.Items[i].FindControl("cbxSentBatch");
            ////        if (cbx.Checked)
            ////        {
            ////            Guid transactionID = (Guid)dtgBatchReturnSent.DataKeys[i];
            ////            allXml.Append(returnXML.CreateBatchXML(transactionID));
            ////        }
            ////    }
            ////    allXml.Append(returnXML.GetFCRXML());

            ////    returnXML.CreateReturnXml(allXml.ToString());

            ////    Response.Redirect("ReturnFiles.aspx");
            ////}
            
            //if (needEnvelope)
            //{
            //    for (int i = 0; i < dtgBatchReturnSent.Items.Count; i++)
            //    {
            //        CheckBox cbx = (CheckBox)dtgBatchReturnSent.Items[i].FindControl("cbxSentBatch");
            //        if (cbx.Checked)
            //        {
            //            EFTN.BLL.ReturnXML returnXML = new EFTN.BLL.ReturnXML();
            //            StringBuilder allXml = new StringBuilder();
            //            allXml.Append(returnXML.GetFHRXML());
            //            Guid transactionID = (Guid)dtgBatchReturnSent.DataKeys[i];
            //            allXml.Append(returnXML.CreateBatchXML(transactionID));
            //            allXml.Append(returnXML.GetFCRXML());
            //            returnXML.CreateReturnXml(allXml.ToString(), i + 1);
            //        }
            //    }

            //    Response.Redirect("ReturnFiles.aspx");
            //}             
            

        }

    }
}
