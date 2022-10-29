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
    public partial class ExportContestedDishonorXML : System.Web.UI.Page
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
            dtgBatchContestedDishonor.DataSource = db.GetBatchesForContestedSent();
            dtgBatchContestedDishonor.DataBind();
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgBatchContestedDishonor.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchContestedDishonor.Items[i].FindControl("cbxSentBatch");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void btnGenerateXml_Click(object sender, EventArgs e)
        {
            bool needEnvelope = false;
            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            for (int i = 0; i < dtgBatchContestedDishonor.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchContestedDishonor.Items[i].FindControl("cbxSentBatch");
                if (cbx.Checked)
                {
                    needEnvelope = true;
                    break;
                }
            }

            if (needEnvelope)
            {

                for (int i = 0; i < dtgBatchContestedDishonor.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)dtgBatchContestedDishonor.Items[i].FindControl("cbxSentBatch");
                    if (cbx.Checked)
                    {
                        EFTN.BLL.ContestedDishonorXML contestedDishonorXML = new EFTN.BLL.ContestedDishonorXML();
                        StringBuilder allXml = new StringBuilder();
                        allXml.Append(contestedDishonorXML.GetFHRXML(i));

                        Guid transactionID = (Guid)dtgBatchContestedDishonor.DataKeys[i];
                        allXml.Append(contestedDishonorXML.CreateBatchXML(transactionID));

                        allXml.Append(contestedDishonorXML.GetFCRXML());
                        contestedDishonorXML.CreateContestedXml(allXml.ToString(), i + 1);
                    }
                }

                BindData();
                //Response.Redirect("ReturnFiles.aspx");
            }

            /*
            if (needEnvelope)
            {
                StringBuilder allXml = new StringBuilder();
                EFTN.BLL.ContestedDishonorXML contestedDishonorXML = new EFTN.BLL.ContestedDishonorXML();
                allXml.Append(contestedDishonorXML.GetFHRXML());

                for (int i = 0; i < dtgBatchContestedDishonor.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)dtgBatchContestedDishonor.Items[i].FindControl("cbxSentBatch");
                    if (cbx.Checked)
                    {
                        Guid transactionID = (Guid)dtgBatchContestedDishonor.DataKeys[i];
                        allXml.Append(contestedDishonorXML.CreateBatchXML(transactionID));
                    }
                }
                allXml.Append(contestedDishonorXML.GetFCRXML());

                contestedDishonorXML.CreateContestedXml(allXml.ToString());
                BindData();
                //Response.Redirect("ReturnFiles.aspx");
            }
             */
        }
    }
}
