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
    public partial class ExportDishonor : System.Web.UI.Page
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
            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            dtgBatchDishonorSent.DataSource = db.GetBatches_For_DishonorSent();
            dtgBatchDishonorSent.DataBind();
        }

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgBatchDishonorSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchDishonorSent.Items[i].FindControl("cbxSentBatch");
                cbx.Checked = checkAllChecked;
            }
        }

        protected void btnGenerateXml_Click(object sender, EventArgs e)
        {
            bool needEnvelope = false;
            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            for (int i = 0; i < dtgBatchDishonorSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchDishonorSent.Items[i].FindControl("cbxSentBatch");
                if (cbx.Checked)
                {
                    needEnvelope = true;
                    break;
                }
            }

            if (needEnvelope)
            {
                #region Added New Due to file naming change from Central Bank
                TransactionXMLFileNameDB transactionXmlDb = new TransactionXMLFileNameDB();
                string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                #endregion
                for (int i = 0; i < dtgBatchDishonorSent.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)dtgBatchDishonorSent.Items[i].FindControl("cbxSentBatch");
                    if (cbx.Checked)
                    {
                        EFTN.BLL.DishonorSentXML dishonorSentXML = new EFTN.BLL.DishonorSentXML();
                        System.Text.StringBuilder allXml = new System.Text.StringBuilder();
                        allXml.Append(dishonorSentXML.GetFHRXML(i, string.Empty));
                        #region Added New due to Central Bank's file naming change
                        int lastIdentity = transactionXmlDb.GetLastXmlIdentity(3);
                        string fileIdentity = lastIdentity.ToString().PadLeft(9, '0');
                        string fileName = "EFT_" + DateTime.Now.ToString("yyyyMMdd") + "_" + originBank + "_3" + fileIdentity + ".XML";
                        #endregion
                        Guid transactionID = (Guid)dtgBatchDishonorSent.DataKeys[i];
                        allXml.Append(dishonorSentXML.CreateBatchXML(transactionID,fileName));

                        allXml.Append(dishonorSentXML.GetFCRXML());
                        //Commented out the old code block
                        //dishonorSentXML.CreateDishonorSentXml(allXml.ToString(), i + 1);
                        dishonorSentXML.CreateDishonorSentXml(allXml.ToString(), fileName);
                    }
                }
                Response.Redirect("DishonorSentFiles.aspx");

            }

            /*
            if (needEnvelope)
            {
                System.Text.StringBuilder allXml = new System.Text.StringBuilder();
                EFTN.BLL.DishonorSentXML dishonorSentXML = new EFTN.BLL.DishonorSentXML();
                allXml.Append(dishonorSentXML.GetFHRXML());

                for (int i = 0; i < dtgBatchDishonorSent.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)dtgBatchDishonorSent.Items[i].FindControl("cbxSentBatch");
                    if (cbx.Checked)
                    {
                        Guid transactionID = (Guid)dtgBatchDishonorSent.DataKeys[i];
                        allXml.Append(dishonorSentXML.CreateBatchXML(transactionID));
                    }
                }
                allXml.Append(dishonorSentXML.GetFCRXML());
                dishonorSentXML.CreateDishonorSentXml(allXml.ToString());
                Response.Redirect("DishonorSentFiles.aspx");
                //returnXML.CreateReturnXml(allXml.ToString());

                //BindData();

            }
             */

        }

    }
}
