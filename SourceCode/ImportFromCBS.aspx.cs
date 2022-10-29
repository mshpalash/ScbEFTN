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
    public partial class ImportFromCBS : System.Web.UI.Page
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
            dtgCBSFiles.DataSource = ds.GetDataSourceAllType("EFTCBSImport");
            dtgCBSFiles.DataBind();

           
        }


        protected void cbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedCbx = cbxSelectAll.Checked;
            for (int i = 0; i < dtgCBSFiles.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgCBSFiles.Items[i].FindControl("cbxFileCheck");
                cbx.Checked = checkedCbx;
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtgCBSFiles.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgCBSFiles.Items[i].FindControl("cbxFileCheck");
                if (cbx.Checked)
                {
                    string fileName = dtgCBSFiles.DataKeys[i].ToString();
                    string[] splitted = fileName.Split('-');
                    string fileType = splitted[1];
                    if (fileType == "TS")
                    {
                        Response.Redirect("~/ExportTransactionSent.aspx");
                    }
                }
            }
        }
    }
}
