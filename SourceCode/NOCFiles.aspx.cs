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
using System.IO;
using FloraSoft;

namespace EFTN
{
    public partial class NOCFiles : System.Web.UI.Page
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
            dtgNOCFiles.DataSource = ds.GetDataSource("EFTNOCExport");
            dtgNOCFiles.DataBind();
        }


        protected void cbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedCbx = cbxSelectAll.Checked;
            for (int i = 0; i < dtgNOCFiles.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgNOCFiles.Items[i].FindControl("cbxFileCheck");
                cbx.Checked = checkedCbx;
            }

        }

        protected void btnSendToPBM_Click(object sender, EventArgs e)
        {
            WSClient ws = new WSClient();
            for (int i = dtgNOCFiles.Items.Count - 1; i > -1; i--)
            {
                CheckBox cbx = (CheckBox)dtgNOCFiles.Items[i].FindControl("cbxFileCheck");
                if (cbx.Checked)
                {
                    string filePath = dtgNOCFiles.DataKeys[i].ToString();
                    FileInfo fi = new FileInfo(filePath);
                    ws.SendSingleFileAndBackUp(filePath, ConfigurationManager.AppSettings["PBMEFTOutPath"] + "\\" + fi.Name);

                }
            }
            BindData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtgNOCFiles.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgNOCFiles.Items[i].FindControl("cbxFileCheck");
                if (cbx.Checked)
                {
                    string filePath = dtgNOCFiles.DataKeys[i].ToString();
                    System.IO.File.Delete(filePath);
                }
            }
            BindData();
        }
    }
}
