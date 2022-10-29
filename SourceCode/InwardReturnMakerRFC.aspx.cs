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
    public partial class InwardReturnMakerRFC : System.Web.UI.Page
    {
        private static DataTable myDTReturnReceived = new DataTable();
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();

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

        private void BindData()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.ReceivedReturnRFCDB receivedReturnDB = new EFTN.component.ReceivedReturnRFCDB();
            string settlementDate = ddlistYear.SelectedValue.PadLeft(4, '0')
                                    + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                    + ddlistDay.SelectedValue.PadLeft(2, '0');

            myDTReturnReceived = receivedReturnDB.GetReceivedReturnForRFC(settlementDate);

            dv = myDTReturnReceived.DefaultView;
            dtgInwardReturnMaker.DataSource = dv;
            dtgInwardReturnMaker.DataBind();
        }


        protected void dtgInwardReturnMaker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgInwardReturnMaker.CurrentPageIndex = e.NewPageIndex;
            dtgInwardReturnMaker.DataSource = myDTReturnReceived;
            dtgInwardReturnMaker.DataBind();
        }

        protected void dtgInwardReturnMaker_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = myDTReturnReceived.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgInwardReturnMaker.DataSource = dv;
            dtgInwardReturnMaker.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void dtgInwardReturnMaker_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            DataRowView test;
            if (e.Item != null && e.Item.DataItem is DataRowView)
            {
                test = (DataRowView)e.Item.DataItem;
                if (test.Row["MismatchAmount"].ToString() == "yes")
                {
                    e.Item.BackColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}
