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
using EFTN.component;
using EFTN.BLL;

namespace EFTN
{
    public partial class RegenerateOutwardTransaction : System.Web.UI.Page
    {
        private static DataTable myDataTable = new DataTable();
        //private EFTN.BLL.FinacleManager fm;

        private bool firstTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("215"))
                {
                    Response.Redirect("Default.aspx");
                }

                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();

                //if (ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3).Equals("225"))
                //{
                //    if (fm == null)
                //    {
                //        fm = new EFTN.BLL.FinacleManager();
                //    }
                //    if (!fm.IsConnected)
                //    {
                //        fm.Connect();
                //    }

                //    firstTime = true;
                //}
            }
        }

        void Page_Unload(Object sender, EventArgs e)
        {
            //if (!IsPostBack && fm != null && fm.IsConnected)
            //{
            //    if (firstTime)
            //    {
            //        firstTime = false;
            //    }
            //    else
            //    {
            //        fm.Disconnect();
            //    }
            //}
        }


        private void BindData()
        {
            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

            RegenerateOutwardTransactionDB regOutTxnDB = new RegenerateOutwardTransactionDB();
            myDataTable = regOutTxnDB.EFTGetBatchesForTransactionSentRegeneration(
                                                                  ddlistYear.SelectedValue.PadLeft(4, '0')
                                                                + ddlistMonth.SelectedValue.PadLeft(2, '0')
                                                                + ddlistDay.SelectedValue.PadLeft(2, '0'), EFTConnectionString);
            dtgRegenerateOutward.DataSource = myDataTable;
            try
            {
                dtgRegenerateOutward.DataBind();
            }
            catch
            {
                dtgRegenerateOutward.CurrentPageIndex = 0;
                dtgRegenerateOutward.DataBind();
            }
        }

        protected void dtgRegenerateOutward_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dtgRegenerateOutward.CurrentPageIndex = e.NewPageIndex;
            dtgRegenerateOutward.DataSource = myDataTable;
            dtgRegenerateOutward.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnGenerateXmlForTransactionSent_Click(object sender, EventArgs e)
        {
            string lastFileIDModifierTime = string.Empty;

            if (Global.lastFileCreatedTime.Equals(string.Empty) || Global.lastFileCreatedTime == null)
            {
                Global.lastFileCreatedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
            }
            else
            {
                lastFileIDModifierTime = Global.lastFileCreatedTime;
            }

            string currentTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
            
            if (lastFileIDModifierTime.Equals(currentTime))
            {
                lblMsgExportTransaction.Text = "Please click after 1 minute to generate new file";
                return;
            }
            else
            {
                Global.lastFileCreatedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
                lblMsgExportTransaction.Text = "";
            }


            int enevelopeID = -1;
            bool needEnvelope = false;
            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            for (int i = 0; i < dtgRegenerateOutward.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgRegenerateOutward.Items[i].FindControl("cbxSentBatchTS");
                if (cbx.Checked)
                {
                    needEnvelope = true;
                    break;
                }
            }
            if (needEnvelope)
            {
                int UserID = ParseData.StringToInt(Request.Cookies["UserID"].Value);
                string EFTConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
                int checkedItem = 0;
                RegenerateTransactionSentXML transactionSentXML = new RegenerateTransactionSentXML();

                for (int i = 0; i < dtgRegenerateOutward.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)dtgRegenerateOutward.Items[i].FindControl("cbxSentBatchTS");
                    if (cbx.Checked)
                    {
                        checkedItem++;
                        if (checkedItem > 35)
                        {
                            lblMsgExportTransaction.Text = "Maximum 35 batch can be created at a time";
                            break;
                        }
                        Guid transactionID = (Guid)dtgRegenerateOutward.DataKeys[i];


                        transactionSentXML.batchCounterOnlyForBatchCount++;
                        string createdXMLFileName = transactionSentXML.CreateBatchXML(transactionID, UserID, EFTConnectionString);
                        SetSettlementDate setSettlementDate = new SetSettlementDate();
                        DateTime TrSentSettlementDate = setSettlementDate.GetOutwardTransactionSettlementDate();
                        db.UpdateBatchSent(transactionID, 1, TrSentSettlementDate, createdXMLFileName);
                    }
                }
            }
            lblMsgExportTransaction.Text = "File Generated Successfully";
            cbxAllTransactionSent.Checked = false;
            AlterCheckBoxSelect(false);
        }

        protected void cbxAllTransactionSent_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllTransactionSent.Checked;
            AlterCheckBoxSelect(checkAllChecked);
        }

        private void AlterCheckBoxSelect(bool checkAllChecked)
        {
            for (int i = 0; i < dtgRegenerateOutward.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgRegenerateOutward.Items[i].FindControl("cbxSentBatchTS");
                cbx.Checked = checkAllChecked;
                if (i > 33)
                {
                    lblMsgExportTransaction.Text = "Maximum 35 batch can be selected at a time";
                    break;
                }
            }
        }

    }
}
