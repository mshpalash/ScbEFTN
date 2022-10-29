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
using FloraSoft;
using System.IO;

namespace EFTN
{
    public partial class SuperAdminChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string originBankCode = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

                if (originBankCode.Equals("215"))
                {
                    BindSecurityMatrix();
                }
            }
        }

        private void BindSecurityMatrix()
        {
            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            dtGridSecurityMatrix.DataSource = ds.GetSecurityMatrix("EFTNSecurityMatrix"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
            dtGridSecurityMatrix.DataBind();
            if (dtGridSecurityMatrix.Items.Count == 0)
            {
                lblTransactionError.Text = "No files.";
            }
        }

        protected void dtGridSecurityMatrix_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string errorFileName = dtGridSecurityMatrix.DataKeys[e.Item.ItemIndex].ToString();
            int LastIndex = errorFileName.LastIndexOf("\\");
            string saveAs = errorFileName.Substring(LastIndex + 1, (errorFileName.Length - LastIndex - 1));
            if (e.CommandName == "DownloadSecurityMatrix")
            {
                //Response.ClearContent();
                //Response.AddHeader("content-disposition",
                //         "attachment;filename=" + saveAs);

                //Response.ContentType = "csv";

                //string filecontent = File.ReadAllText(errorFileName);
                //Response.Write(filecontent);
                //Response.End();

                byte[] Data = File.ReadAllBytes(errorFileName);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.BufferOutput = true;
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ContentType = @"application/csv";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(saveAs));
                HttpContext.Current.Response.OutputStream.Write(Data, 0, Data.Length);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.Close();
                File.Delete(saveAs);
            }
        }
    }
}
