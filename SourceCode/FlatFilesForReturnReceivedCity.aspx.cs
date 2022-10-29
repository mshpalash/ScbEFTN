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
    public partial class FlatFilesForReturnReceivedCity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');

                BindData();
            }

        }

        private void BindData()
        {
            EFTN.component.FlatFileCityDB flatfilecityDB = new EFTN.component.FlatFileCityDB();
            dtgTransactionReturnReceivedIB.DataSource = flatfilecityDB.GetFlatFileForReturnReceivedIB(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                         + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                         + ddlistDay.SelectedValue.PadLeft(2, '0'));
            dtgTransactionReturnReceivedIB.DataBind();
        }

        protected void linkBtnFlatFileReturnReceived_Click(object sender, EventArgs e)
        {
            DataTable dt;

            string CBL_WebService_UserName = ConfigurationManager.AppSettings["CBLWebServiceUserName"];
            string CBL_WebService_Password = ConfigurationManager.AppSettings["CBLWebServicePassword"];

            EFTN.component.FlatFileCityDB flatfilecityDB = new EFTN.component.FlatFileCityDB();
            dt = flatfilecityDB.GetFlatFileForReturnReceivedIB(ddlistYear.SelectedValue.PadLeft(4, '0')
                                                         + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                         + ddlistDay.SelectedValue.PadLeft(2, '0'));

            string valueDate = ddlistDay.SelectedValue.PadLeft(2, '0') + "/"
                                + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/"
                                + ddlistYear.SelectedValue.PadLeft(4, '0');

            EFTN.BLL.FlatFileClientCity fc = new EFTN.BLL.FlatFileClientCity();

            string flatfileResult = fc.CreatFlatFileForReturnReceivedIB(dt, valueDate, CBL_WebService_UserName, CBL_WebService_Password);
            string fileName = "upldtranpc";
            Response.Clear();
            Response.AddHeader("content-disposition",
                     "attachment;filename=" + fileName + ".txt");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.text";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite =
                          new HtmlTextWriter(stringWrite);
            Response.Write(flatfileResult.ToString());
            Response.End();
        }

        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
