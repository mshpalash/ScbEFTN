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
    public partial class ExportNOC : System.Web.UI.Page
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
            dtgBatchNOCSent.DataSource = db.GetBatches_For_NOCSent();
            dtgBatchNOCSent.DataBind();
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgBatchNOCSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchNOCSent.Items[i].FindControl("cbxSentBatch");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void btnGenerateXml_Click(object sender, EventArgs e)
        {
            //bool needEnvelope = false;
            //EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            //for (int i = 0; i < dtgBatchNOCSent.Items.Count; i++)
            //{
            //    CheckBox cbx = (CheckBox)dtgBatchNOCSent.Items[i].FindControl("cbxSentBatch");
            //    if (cbx.Checked)
            //    {
            //        needEnvelope = true;
            //        break;
            //    }
            //}
            //if (needEnvelope)
            //{

            //    for (int i = 0; i < dtgBatchNOCSent.Items.Count; i++)
            //    {
            //        CheckBox cbx = (CheckBox)dtgBatchNOCSent.Items[i].FindControl("cbxSentBatch");
            //        if (cbx.Checked)
            //        {
            //            EFTN.BLL.NOCSentXML nOCSentXML = new EFTN.BLL.NOCSentXML();
            //            StringBuilder allXml = new StringBuilder();
            //            allXml.Append(nOCSentXML.GetFHRXML());

            //            Guid transactionID = (Guid)dtgBatchNOCSent.DataKeys[i];
            //            allXml.Append(nOCSentXML.CreateBatchXML(transactionID));

            //            allXml.Append(nOCSentXML.GetFCRXML());
            //            nOCSentXML.CreateNOCXml(allXml.ToString(), i + 1);
            //        }
            //    }

            //    Response.Redirect("NOCFiles.aspx");
            //}
            /*
            if (needEnvelope)
            {
                StringBuilder allXml = new StringBuilder();
                EFTN.BLL.NOCSentXML nOCSentXML = new EFTN.BLL.NOCSentXML();
                allXml.Append(nOCSentXML.GetFHRXML());

                for (int i = 0; i < dtgBatchNOCSent.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)dtgBatchNOCSent.Items[i].FindControl("cbxSentBatch");
                    if (cbx.Checked)
                    {
                        Guid transactionID = (Guid)dtgBatchNOCSent.DataKeys[i];
                        allXml.Append(nOCSentXML.CreateBatchXML(transactionID));
                    }
                }
                allXml.Append(nOCSentXML.GetFCRXML());

                nOCSentXML.CreateNOCXml(allXml.ToString());
                Response.Redirect("NOCFiles.aspx");
            }
             */


        }
    }
}
