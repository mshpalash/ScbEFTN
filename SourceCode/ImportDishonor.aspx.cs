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
    public partial class ImportDishonor : System.Web.UI.Page
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
            dtgListOfDishonorXML.DataSource = ds.GetDataSource("EFTDishonouredReturnImport");
            dtgListOfDishonorXML.DataBind();

            EFTN.component.ReceivedDishonorDB receivedDishonorDB = new EFTN.component.ReceivedDishonorDB();
            dtgInwardDishonor.DataSource = receivedDishonorDB.GetReceivedDishonor();
            dtgInwardDishonor.DataBind();
        }

        protected void cbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedCbx = cbxSelectAll.Checked;
            for (int i = 0; i < dtgListOfDishonorXML.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgListOfDishonorXML.Items[i].FindControl("cbxFileCheck");
                cbx.Checked = checkedCbx;
            }
        }

        protected void btnImportDishonor_Click(object sender, EventArgs e)
        {
            EFTN.BLL.DishonoredReceivedXML dishonoredReceivedXML = new EFTN.BLL.DishonoredReceivedXML();
            for (int i = 0; i < dtgListOfDishonorXML.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgListOfDishonorXML.Items[i].FindControl("cbxFileCheck");
                if (cbx.Checked)
                {
                    string path = dtgListOfDishonorXML.DataKeys[i].ToString();
                    if (System.IO.File.Exists(path))
                    {
                        dishonoredReceivedXML.SaveDataToDB(path);
                    }
                }
            }
            BindData();
            cbxSelectAll.Checked = false;
        }
    }
}
