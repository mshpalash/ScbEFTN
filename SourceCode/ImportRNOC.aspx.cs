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

namespace EFTN
{
    public partial class ImportRNOC : System.Web.UI.Page
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
            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            dtgListOfRNOCXML.DataSource = ds.GetDataSource("EFTRNOCImport");
            dtgListOfRNOCXML.DataBind();

            EFTN.component.ReceivedRNOCDB receivedRNOCDB = new EFTN.component.ReceivedRNOCDB();
            dtgInwardRNOC.DataSource = receivedRNOCDB.GetReceivedRNOC();
            dtgInwardRNOC.DataBind();
        }


        protected void cbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedCbx = cbxSelectAll.Checked;
            for (int i = 0; i < dtgListOfRNOCXML.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgListOfRNOCXML.Items[i].FindControl("cbxFileCheck");
                cbx.Checked = checkedCbx;
            }

        }

        protected void btnImportRNOC_Click(object sender, EventArgs e)
        {
            EFTN.BLL.RNOCReceivedXML rNOCReceivedXML = new EFTN.BLL.RNOCReceivedXML();
            for (int i = 0; i < dtgListOfRNOCXML.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgListOfRNOCXML.Items[i].FindControl("cbxFileCheck");
                if (cbx.Checked)
                {
                    string path = dtgListOfRNOCXML.DataKeys[i].ToString();
                    if (System.IO.File.Exists(path))
                    {
                        rNOCReceivedXML.SaveDataToDB(path);
                    }
                }
            }
            BindData();

        }
    }
}
