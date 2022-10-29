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
using EFTN.Utility;
using Ionic.Zip;

namespace EFTN
{
    public partial class BulkTransactionEdit : System.Web.UI.Page
    {
        DataView dv;
        private static DataTable myDTCorUpload = new DataTable();
        private static Guid TransactionID;
        private static string BatchNumber = string.Empty;
        private static string strDataEntryType = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string trID = Request.Params["TransactionID"].ToString();
                TransactionID = new Guid(trID);
                BindData();
                sortOrder = "asc";
            }
        }



        //private void FailedMessage()
        //{
        //    lblMsgBatchNumber.Text = string.Empty;
        //    lblErrMsg.Text = "Failed to upload invalid file. Please upload valid excel file";
        //    lblErrMsg.ForeColor = System.Drawing.Color.Red;
        //    lblErrMsg.Visible = true;
        //}

        //private void FailedMessageForDuplicate()
        //{
        //    lblErrMsg.Text = "Failed to decrypt duplicate file in the same location";
        //    lblErrMsg.ForeColor = System.Drawing.Color.Red;
        //    lblErrMsg.Visible = true;
        //}

        //private void SuccessMessage()
        //{
        //    lblErrMsg.Text = "uploaded file successfully";
        //    lblErrMsg.ForeColor = System.Drawing.Color.Blue;
        //    lblErrMsg.Visible = true;
        //}

        private void BindImportedExcel(string batchNumber)
        {
            BatchNumber = batchNumber;
            BindBatchTotal();
            dtgXcelUpload.CurrentPageIndex = 0;

            //dtgXcelUpload.DataSource = myDTCorUpload;
            //dtgXcelUpload.DataBind();

            //dv = db.GetBuildingbyArea(Int32.Parse(AreaList.SelectedValue)).DefaultView;


            //MyDataGrid.DataSource = dv;
            //MyDataGrid.DataBind();
            dv = myDTCorUpload.DefaultView;
            dtgXcelUpload.DataSource = dv;
            dtgXcelUpload.DataBind();

        }

        private void BindBatchTotal()
        {
            //lblMsgBatchNumber.Text = "Batch Number : " + BatchNumber; //Commented out the previous code on BACH II Upgrades
            
            if (myDTCorUpload.Rows.Count > 0)
            {
                lblMsgBatchNumber.Text = "Batch Number : " + myDTCorUpload.Rows[0]["BatchNumber"].ToString();
                lblTotalItem.Text = "Total Item : " + myDTCorUpload.Rows.Count.ToString();
                lblTotalAmount.Text = "Total Amount : " + myDTCorUpload.Compute("SUM(Amount)", "").ToString();
            }
            else
            {
                lblTotalItem.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
            }
        }

        protected void dtgXcelUpload_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            dv = myDTCorUpload.DefaultView;
            dv.Sort = e.SortExpression + " " + sortOrder;
            dtgXcelUpload.DataSource = dv;
            dtgXcelUpload.DataBind();
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


        protected void dtgXcelUpload_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgXcelUpload.CurrentPageIndex = e.NewPageIndex;
            dtgXcelUpload.DataSource = myDTCorUpload;
            dtgXcelUpload.DataBind();
        }


        protected void dtgXcelUpload_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (strDataEntryType.Equals(DataEntryType.Excel) || strDataEntryType.Equals(DataEntryType.Text))
            {
                EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
                lblErrMsg.Text = string.Empty;

                if (e.CommandName == "Cancel")
                {
                    dtgXcelUpload.EditItemIndex = -1;
                    BindData();
                }
                if (e.CommandName == "Edit")
                {
                    dtgXcelUpload.EditItemIndex = e.Item.ItemIndex;
                    BindData();
                }

                if (e.CommandName == "Update")
                {
                    Guid EDRID = (Guid)dtgXcelUpload.DataKeys[e.Item.ItemIndex];

                    TextBox txtDFIAccountNo = (TextBox)e.Item.FindControl("txtDFIAccountNo");
                    TextBox txtAccountNo = (TextBox)e.Item.FindControl("txtAccountNo");
                    TextBox txtReceivingBankRoutingNo = (TextBox)e.Item.FindControl("txtReceivingBankRoutingNo");
                    TextBox txtPaymentInfo = (TextBox)e.Item.FindControl("txtPaymentInfo");
                    TextBox txtIdNumber = (TextBox)e.Item.FindControl("txtIdNumber");
                    TextBox txtReceiverName = (TextBox)e.Item.FindControl("txtReceiverName");
                    TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");

                    TextBox txtInvoiceNumber = (TextBox)e.Item.FindControl("txtInvoiceNumber");
                    TextBox txtInvoiceDate = (TextBox)e.Item.FindControl("txtInvoiceDate");
                    TextBox txtInvoiceGrossAmount = (TextBox)e.Item.FindControl("txtInvoiceGrossAmount");
                    TextBox txtInvoiceAmountPaid = (TextBox)e.Item.FindControl("txtInvoiceAmountPaid");
                    TextBox txtPurchaseOrder = (TextBox)e.Item.FindControl("txtPurchaseOrder");
                    TextBox txtAdjustmentAmount = (TextBox)e.Item.FindControl("txtAdjustmentAmount");
                    TextBox txtAdjustmentCode = (TextBox)e.Item.FindControl("txtAdjustmentCode");
                    TextBox txtAdjustmentDescription = (TextBox)e.Item.FindControl("txtAdjustmentDescription");

                    sentEDRDB.UpdateSentEDRByEDRSentIDForBulkUpload(EDRID, txtDFIAccountNo.Text.Trim(), txtAccountNo.Text.Trim()
                                                            , ParseData.StringToDecimal(txtAmount.Text.Trim())
                                                            , txtIdNumber.Text.Trim(), txtReceiverName.Text.Trim()
                                                            , txtPaymentInfo.Text.Trim()
                                                            , txtReceivingBankRoutingNo.Text.Trim()
                                                            , txtInvoiceNumber.Text.Trim(), txtInvoiceDate.Text.Trim()
                                                            , ParseData.StringToDecimal(txtInvoiceGrossAmount.Text.Trim())
                                                            , ParseData.StringToDecimal(txtInvoiceAmountPaid.Text.Trim())
                                                            , txtPurchaseOrder.Text.Trim()
                                                            , ParseData.StringToDecimal(txtAdjustmentAmount.Text.Trim())
                                                            , txtAdjustmentCode.Text.Trim()
                                                            , txtAdjustmentDescription.Text.Trim());

                    dtgXcelUpload.EditItemIndex = -1;

                    BindData();
                }
                if (e.CommandName == "Delete")
                {
                    if (myDTCorUpload.Rows.Count > 1)
                    {
                        Guid EDRID = (Guid)dtgXcelUpload.DataKeys[e.Item.ItemIndex];
                        sentEDRDB.DeleteTransactionSent(EDRID);
                        BindData();
                    }
                    else
                    {
                        lblErrMsg.ForeColor = System.Drawing.Color.Red;
                        lblErrMsg.Text = "If you want to delete all the item the click cancel button";
                    }
                }
            }
            else
            {
                lblErrMsg.Text = "You are not allowed to update this batch.";
                lblErrMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void BindData()
        {
            EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
            myDTCorUpload = sentEDRDB.GetSentEDRByTransactionIDForBulk(TransactionID);
            dtgXcelUpload.DataSource = myDTCorUpload;
            EFTN.component.SentBatchDB sentBatchDB=new EFTN.component.SentBatchDB();
            DataTable dtSentBatch = sentBatchDB.GetSentBatchByTransactionID(TransactionID);

            strDataEntryType = dtSentBatch.Rows[0]["DataEntryType"].ToString();
            //if(myDTCorUpload.Rows.Count/dtgXcelUpload.PageSize)
            try
            {
                dtgXcelUpload.DataBind();
            }
            catch
            {
                if (dtgXcelUpload.CurrentPageIndex > 0)
                    dtgXcelUpload.CurrentPageIndex = dtgXcelUpload.CurrentPageIndex - 1;
                dtgXcelUpload.DataBind();
            }
            BindBatchTotal();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (myDTCorUpload.Rows.Count > 0)
            {
                EFTN.component.SentEDRDB sentEDRDB = new EFTN.component.SentEDRDB();
                sentEDRDB.UpdateEDRSentto1(TransactionID);
                Response.Redirect("BulkTransactionList.aspx");
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (myDTCorUpload.Rows.Count > 0)
            {
                EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
                sentBatchDB.DeleteBatchSent(TransactionID);
                Response.Redirect("BulkTransactionList.aspx");
            }
        }

        private void DeleteAllMessages()
        {
            lblMsgBatchNumber.Text = string.Empty;
            lblTotalItem.Text = string.Empty;
            lblTotalAmount.Text = string.Empty;
        }

    }
}
