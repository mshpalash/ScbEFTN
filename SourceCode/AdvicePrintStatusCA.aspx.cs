using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI.WebControls;
using EFTN.component;
using EFTN.Utility;
using FloraSoft;


namespace EFTN
{
    public partial class AdvicePrintStatusCA : System.Web.UI.Page
    {
        private static DataTable dtAdviceStatus = new DataTable();
        DataView dv;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = DateTime.Now.Day.ToString().PadLeft(2,'0');
                ddlistMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = DateTime.Now.Year.ToString().PadLeft(4, '0');
                sortOrder = "asc";
            }
        }

        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";

                }
                else
                {
                    ViewState["sortOrder"] = "desc";
                }

                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }

        protected void dtgAdvicePrintStatus_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = dtAdviceStatus.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgAdvicePrintStatus.DataSource = dv;
            dtgAdvicePrintStatus.DataBind();
        }

        protected void btnPrintAdvice_Click(object sender, EventArgs e)
        {
            PrintCustomerAdviceDB printCustAdvDB = new PrintCustomerAdviceDB();
            string settlementJDate = ddlistYear.SelectedValue + ddlistMonth.SelectedValue.PadLeft(2, '0') + ddlistDay.SelectedValue.PadLeft(2, '0');
            dtAdviceStatus = printCustAdvDB.GetBranchAdvicePrintTrackByDateForData(settlementJDate);
            dtgAdvicePrintStatus.CurrentPageIndex = 0;
            dtgAdvicePrintStatus.DataSource = dtAdviceStatus;
            dtgAdvicePrintStatus.DataBind();
        }

        protected void btnNonPrintAdvice_Click(object sender, EventArgs e)
        {
            PrintCustomerAdviceDB printCustAdvDB = new PrintCustomerAdviceDB();
            string settlementJDate = ddlistYear.SelectedValue + ddlistMonth.SelectedValue.PadLeft(2, '0') + ddlistDay.SelectedValue.PadLeft(2, '0');
            string originBankRoutingNo = ConfigurationManager.AppSettings["OriginBank"].ToString();
            string BankCode = originBankRoutingNo.Substring(0, 3);

            dtAdviceStatus = printCustAdvDB.GetBranchAdvicePrintTrackByDateForNoneData(settlementJDate, BankCode);
            dtgAdvicePrintStatus.CurrentPageIndex = 0;
            dtgAdvicePrintStatus.DataSource = dtAdviceStatus;
            dtgAdvicePrintStatus.DataBind();
        }

        protected void dtgAdvicePrintStatus_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgAdvicePrintStatus.CurrentPageIndex = e.NewPageIndex;
            dtgAdvicePrintStatus.DataSource = dtAdviceStatus;
            dtgAdvicePrintStatus.DataBind();
        }
    }
}
