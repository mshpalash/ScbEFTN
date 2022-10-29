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
using iTextSharp.text;
using iTextSharp.text.pdf;
using EFTN.Utility;

namespace EFTN
{
    public partial class CSVReportViewer : System.Web.UI.Page
    {
        private static DataTable myDataTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();
            }
        }

        private void BindData()
        {
            int DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);

            EFTN.component.CSVReportDB cSVReportDB = new EFTN.component.CSVReportDB();
            myDataTable = cSVReportDB.EFTGetBatchesForTransactionSentForSCBCSV(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            dtgEFTChecker.DataSource = myDataTable;
            try
            {
                dtgEFTChecker.DataBind();

            }
            catch
            {
                dtgEFTChecker.CurrentPageIndex = 0;
                dtgEFTChecker.DataBind();
            }
            //.DataBind();
        }

        protected void dtgEFTChecker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgEFTChecker.CurrentPageIndex = e.NewPageIndex;
            dtgEFTChecker.DataSource = myDataTable;
            dtgEFTChecker.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData();
        }

    }
}
