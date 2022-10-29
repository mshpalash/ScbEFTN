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


namespace EFTN
{
    public partial class NonAckXMLFileList : System.Web.UI.Page
    {
        private static DataTable dtNonAckFileList = new DataTable();
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

        protected void dtgSettlementReport_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = dtNonAckFileList.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgSettlementReport.DataSource = dv;
            dtgSettlementReport.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindSeachData();
        }

        private void BindSeachData()
        {
            NonAckFileListDB nonAckFileListDB = new NonAckFileListDB();

            if (ddListReportType.SelectedValue.Equals("1"))
            {
                dtNonAckFileList = nonAckFileListDB.GetNonAckTransactionSentXMLListByEntryDate(
                                                                   ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                 + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                 + ddlistDay.SelectedValue.PadLeft(2, '0'));

                if (dtNonAckFileList.Rows.Count > 0)
                {
                    dtgSettlementReport.CurrentPageIndex = 0;
                    dtgSettlementReport.DataSource = dtNonAckFileList;
                }
            }
            //else if (ddListReportType.SelectedValue.Equals("2"))
            //{
            //    dtSettlementReport = detailSettlementReportDB.NonAckFileListDB(
            //                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
            //                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
            //                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), DepartmentID);
            //    if (dtSettlementReport.Rows.Count > 0)
            //    {
            //        dtgSettlementReport.CurrentPageIndex = 0;
            //        dtgSettlementReport.DataSource = dtSettlementReport;
            //    }
            //}
            dtgSettlementReport.DataBind();
        }

        protected void dtgSettlementReport_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgSettlementReport.CurrentPageIndex = e.NewPageIndex;
            dtgSettlementReport.DataSource = dtNonAckFileList;
            dtgSettlementReport.DataBind();
        }

        protected void btnResend_Click(object sender, EventArgs e)
        {
            string sourceFilePath = ConfigurationManager.AppSettings["EFTTransactionExport"] + "bak\\" + System.DateTime.Now.ToString("yyyyMMdd");
            string destFilePath = ConfigurationManager.AppSettings["EFTTransactionExport"];

            string selectedDate = ddlistYear.SelectedValue.PadLeft(4, '0')
                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                + ddlistDay.SelectedValue.PadLeft(2, '0');

            if (System.DateTime.Now.ToString("yyyyMMdd").Equals(selectedDate))
            {
                if (Directory.Exists(sourceFilePath))
                {
                    for (int i = 0; i < dtgSettlementReport.Items.Count; i++)
                    {
                        CheckBox cbx = (CheckBox)dtgSettlementReport.Items[i].FindControl("cbxTSXMLList");
                        if (cbx.Checked)
                        {
                            string xmlFileNameToResend = dtgSettlementReport.DataKeys[i].ToString();
                            FileInfo fInfo = new FileInfo(sourceFilePath + "\\" + xmlFileNameToResend);
                            //simply use MoveTo function to move it to the main folder
                            try
                            {
                                fInfo.MoveTo(destFilePath + xmlFileNameToResend);
                            }
                            catch (Exception ex)
                            {
                                lblMsg.Text = ex.Message + ": File Name : "+ xmlFileNameToResend; ;
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = "Directory not found for the current date";
                }
            }
            else
            {
                lblMsg.Text = "Resend only Today's file";
            }
            BindSeachData();
        }
    }
}
