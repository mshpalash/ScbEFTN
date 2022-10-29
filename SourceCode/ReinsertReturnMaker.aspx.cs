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
    public partial class ReinsertReturnMaker : System.Web.UI.Page
    {
        private static DataTable myDTReturnReceived = new DataTable();
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                //rblDishonorDecision.SelectedValue = "1";
                //ddlDishonour.Enabled = false;
                //txtAddendaInfo.Visible = false;
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
            //int DepartmentID = 0;
            //if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            //{
            //    DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            //}

            EFTN.component.ReceivedReturnDB receivedReturnDB = new EFTN.component.ReceivedReturnDB();
            //if (ddListTransactionType.SelectedValue.Equals(EFTN.Utility.TransactionCodes.EFTTransactionTypeDebit))
            //{
            //    myDTReturnReceived = receivedReturnDB.GetReceivedReturn_ForDebit(DepartmentID);
            //}
            //else
            //{
            //    myDTReturnReceived = receivedReturnDB.GetReceivedReturn(DepartmentID);
            //}

            myDTReturnReceived = receivedReturnDB.GetReceivedReturn_Missing();

            dv = myDTReturnReceived.DefaultView;
            dtgReinsertReturnMaker.DataSource = dv;
            dtgReinsertReturnMaker.DataBind();

            //EFTN.component.CodeLookUpDB codeLookUpDb = new EFTN.component.CodeLookUpDB();
            //ddlDishonour.DataSource = codeLookUpDb.GetCodeLookUp((int)EFTN.Utility.CodeType.DishonourReturn);
            //ddlDishonour.DataBind();

            //if (myDTReturnReceived.Rows.Count > 0)
            //{
            //    lblTotalItem.Text = "Total Item : " + myDTReturnReceived.Rows.Count.ToString();
            //    lblTotalAmount.Text = "Total Amount : " + decimal.Parse(myDTReturnReceived.Compute("SUM(Amount)", "").ToString()).ToString("0.00");
            //}
            //else
            //{
            //    lblTotalItem.Text = string.Empty;
            //    lblTotalAmount.Text = string.Empty;
            //}
            cbxAll.Checked = false;
        }

        protected void ddListTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void dtgReinsertReturnMaker_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgReinsertReturnMaker.CurrentPageIndex = e.NewPageIndex;
            dtgReinsertReturnMaker.DataSource = myDTReturnReceived;
            dtgReinsertReturnMaker.DataBind();
        }

        protected void dtgReinsertReturnMaker_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = myDTReturnReceived.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgReinsertReturnMaker.DataSource = dv;
            dtgReinsertReturnMaker.DataBind();
        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/EFTMaker.aspx");
        //}

        //protected void rblDishonorDecision_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int type = Int32.Parse(rblDishonorDecision.SelectedValue);
        //    switch (type)
        //    {
        //        case 1:
        //            ddlDishonour.Enabled = false;
        //            txtAddendaInfo.Visible = false;
        //            break;
        //        case 5:
        //            ddlDishonour.Enabled = true;
        //            txtAddendaInfo.Visible = true;
        //            break;
        //    }
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //int type = Int32.Parse(rblDishonorDecision.SelectedValue);
            for (int i = 0; i < dtgReinsertReturnMaker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgReinsertReturnMaker.Items[i].FindControl("CheckBEFTNList");

                if (cbx.Checked)
                {
                    //string returnID = dtgReinsertReturnMaker.DataKeys[i].ToString();
                    //Guid ReturnID = new Guid(returnID);
                                        //switch (type)
                    //{
                    //    case 1:
                    //        ChangeStatus(EFTN.Utility.TransactionStatus.Return_Received_Approved, ReturnID, "", "");
                    //        break;
                    //    case (int)EFTN.Utility.CodeType.DishonourReturn:
                    //ChangeStatus(EFTN.Utility.TransactionStatus.Return_Received_Dishonor, ReturnID, ddlDishonour.SelectedValue, txtAddendaInfo.Text);

                    //        break;
                    //}

                    string OrgTraceNumber = dtgReinsertReturnMaker.DataKeys[i].ToString();
                    EFTN.component.ReceivedReturnDB db = new EFTN.component.ReceivedReturnDB();
                    db.InsertReceivedReturn_Batch_FromArchive(OrgTraceNumber);
                    db.InsertReceivedReturn_FromArchive(OrgTraceNumber);
                }
            }
            BindData();
        }

        //private void ChangeStatus(int statusID, Guid returnID, string dishonorCode, string addendaInfo)
        //{
        //    EFTN.component.ReceivedReturnDB db = new EFTN.component.ReceivedReturnDB();
        //    int CreatedBy = EFTN.Utility.ParseData.StringToInt(Request.Cookies["UserID"].Value);

        //    db.Update_ReceivedReturn_Status(statusID, returnID, dishonorCode, CreatedBy, addendaInfo);
        //}

        protected void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAll.Checked;
            for (int i = 0; i < dtgReinsertReturnMaker.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgReinsertReturnMaker.Items[i].FindControl("CheckBEFTNList");
                cbx.Checked = checkAllChecked;
            }
        }



        protected void dtgReinsertReturnMaker_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            //DataRowView test;
            //if (e.Item != null && e.Item.DataItem is DataRowView)
            //{
            //    test = (DataRowView)e.Item.DataItem;
            //    if (test.Row["MismatchAmount"].ToString() == "yes")
            //    {
            //        e.Item.BackColor = System.Drawing.Color.Red;
            //    }
            //}
           
        }
    }

}
