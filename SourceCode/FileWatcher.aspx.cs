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
    public partial class FileWatcher : System.Web.UI.Page
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
            if (Request.Params["PathKey"] != null)
            {
                
                string pathKey = Request.Params["PathKey"];
                lblHead.Text = pathKey;
                EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
                dtgTransactionSentFiles.DataSource = ds.GetDataSourceAllType(pathKey);
                dtgTransactionSentFiles.DataBind();
            }
        }


        protected void cbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedCbx = cbxSelectAll.Checked;
            for (int i = 0; i < dtgTransactionSentFiles.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgTransactionSentFiles.Items[i].FindControl("cbxFileCheck");
                cbx.Checked = checkedCbx;
            }

        }

        protected void btnSendToPBM_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtgTransactionSentFiles.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgTransactionSentFiles.Items[i].FindControl("cbxFileCheck");
                if (cbx.Checked)
                {
                    string filePath = dtgTransactionSentFiles.DataKeys[i].ToString();
                    System.IO.File.Delete(filePath);
                }
            }
            BindData();
        }
    }
}
