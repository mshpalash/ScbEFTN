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
    public partial class ExportRNOC : System.Web.UI.Page
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
            EFTN.component.RNOCBatchDB rNOCBatchDB = new EFTN.component.RNOCBatchDB();
            dtgRNOCBatch.DataSource = rNOCBatchDB.GetRNOCBatchData();
            dtgRNOCBatch.DataBind();
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgRNOCBatch.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgRNOCBatch.Items[i].FindControl("cbxRNOCBatch");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void btnGenerateXml_Click(object sender, EventArgs e)
        {
            bool needEnvelope = false;
            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            for (int i = 0; i < dtgRNOCBatch.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgRNOCBatch.Items[i].FindControl("cbxRNOCBatch");
                if (cbx.Checked)
                {
                    needEnvelope = true;
                    break;
                }
            }
            if (needEnvelope)
            {

                for (int i = 0; i < dtgRNOCBatch.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)dtgRNOCBatch.Items[i].FindControl("cbxRNOCBatch");
                    if (cbx.Checked)
                    {
                        EFTN.BLL.RNOCSentXML rNOCSentXML = new EFTN.BLL.RNOCSentXML();
                        StringBuilder allXml = new StringBuilder();
                        allXml.Append(rNOCSentXML.GetFHRXML(i));
                        
                        Guid transactionID = (Guid)dtgRNOCBatch.DataKeys[i];
                        allXml.Append(rNOCSentXML.CreateBatchXML(transactionID));

                        allXml.Append(rNOCSentXML.GetFCRXML());
                        rNOCSentXML.CreateRNOCXml(allXml.ToString(), i+1);
                    }
                }

                Response.Redirect("RNOCFiles.aspx");
                //BindData();
            }

            /*
            if (needEnvelope)
            {
                StringBuilder allXml = new StringBuilder();
                EFTN.BLL.RNOCSentXML rNOCSentXML = new EFTN.BLL.RNOCSentXML();
                allXml.Append(rNOCSentXML.GetFHRXML());

                for (int i = 0; i < dtgRNOCBatch.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)dtgRNOCBatch.Items[i].FindControl("cbxRNOCBatch");
                    if (cbx.Checked)
                    {
                        Guid transactionID = (Guid)dtgRNOCBatch.DataKeys[i];
                        allXml.Append(rNOCSentXML.CreateBatchXML(transactionID));
                    }
                }
                allXml.Append(rNOCSentXML.GetFCRXML());

                rNOCSentXML.CreateRNOCXml(allXml.ToString());
                Response.Redirect("RNOCFiles.aspx");
                //BindData();
            }
             */
        }
    }
}
