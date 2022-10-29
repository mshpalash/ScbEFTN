using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EFTN.Utility;
using EFTN.component;

namespace EFTN
{
    public partial class FlatFileForTransactionSentCity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();
            }
        }

        void Page_Unload(Object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CityIBSDB cityIBSDb = new CityIBSDB();
            DataTable dtFlatFile = cityIBSDb.GetTransactionSent_RFCDebit_FlatFile(
                                                                      ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                    + ddlistDay.SelectedValue.PadLeft(2, '0'));
            GenerateFlatFile(dtFlatFile);
        }

        private void GenerateFlatFile(DataTable dt)
        {
            EFTN.BLL.FlatFileClientCity fc = new EFTN.BLL.FlatFileClientCity();
            //int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            string flatfileResult = fc.CreatFlatFileForTransactionSent(dt);

            string fileName = string.Empty;
            fileName = "RFCFlatFile-" + System.DateTime.Now.ToString("yyyyMMdd");

            //string fileName = "CBS" + "-" + "FlatFile-TS-Charge" + "-D" + System.DateTime.Now.ToString("yyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss");
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
    }
}
