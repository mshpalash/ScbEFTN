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
using EFTN.BLL;
using System.Text;
using EFTN.Utility;
using System.Xml;
using System.IO;
using EFTN.component;

namespace EFTN
{
    public partial class EFTCheckerAuthorizerAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string accessPermission = ConfigurationManager.AppSettings["CA"];
            if (accessPermission.Equals("1"))
            {
                //Response.Write("<script>alert('You are not allowed to access this page');   javascript:window.history.forward(1);</script>");
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            if (!IsPostBack)
            {
                string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                if (originBank.Equals(OriginalBankCode.HSBC))
                {
                    Response.Redirect("EFTCheckerAuthorizerSpAdmin.aspx");
                }
                BindImportWindows();
            }
        }

        private void BindImportWindows()
        {
            //Import Data List
            ImportTransaction();
            ImportReturn();
            ImportNOC();
            ImportDishonor();
            ImportRNOC();
            ImportContested();
            ImportTransactionError();
            ImportTransactionAcknowledgement();
            //Export Data List
            BindDataForTransactionSent();
            BindDataForReturnSent();
            BindDataForNOCSent();
            BindDataForRNOCSent();
            BindDataForDishonorSent();
            BindDataForDishonorContestedSent();
            //BindDataForContested();
        }

        private void ImportRNOC()
        {
            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            dtListRNOC.DataSource = ds.GetDataSource("EFTRNOCImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardRNOCPath"], "*.XML");//
            dtListRNOC.DataBind();
            if (dtListRNOC.Items.Count == 0)
            {
                lblRNOC.Text = "No files.";
            }
        }

        private void ImportDishonor()
        {
            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            dtListDishonor.DataSource = ds.GetDataSource("EFTDishonouredReturnImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardDishonorReturnPath"], "*.XML");//
            dtListDishonor.DataBind();
            if (dtListDishonor.Items.Count == 0)
            {
                lblDishonor.Text = "No files.";
            }
        }

        private void ImportContested()
        {
            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            dtListContested.DataSource = ds.GetDataSource("EFTContestedImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardContestedPath"], "*.XML");//
            dtListContested.DataBind();
            if (dtListContested.Items.Count == 0)
            {
                lblContested.Text = "No files.";
            }
        }

        private void ImportNOC()
        {
            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            dtlImportNOC.DataSource = ds.GetDataSource("EFTNOCImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardNOCPath"], "*.XML");//
            dtlImportNOC.DataBind();
            if (dtlImportReturn.Items.Count == 0)
            {
                lblImportNOC.Text = "No files.";
            }
        }

        private void ImportReturn()
        {
            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            dtlImportReturn.DataSource = ds.GetDataSource("EFTReturnImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardReturnPath"], "*.XML");//
            dtlImportReturn.DataBind();
            if (dtlImportReturn.Items.Count == 0)
            {
                lblImportReturn.Text = "No files.";
            }
        }

        private void ImportTransaction()
        {
            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            dtlImportTransaction.DataSource = ds.GetDataSource("EFTTransactionImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
            dtlImportTransaction.DataBind();
            if (dtlImportTransaction.Items.Count == 0)
            {
                lblMsgTransactionReceived.Text = "No files.";
            }
        }

        private void ImportTransactionError()
        {
            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            dtGridTransactionError.DataSource = ds.GetDataSourceAllType("EFTExpTransactionError"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
            dtGridTransactionError.DataBind();
            if (dtGridTransactionError.Items.Count == 0)
            {
                lblTransactionError.Text = "No files.";
            }
        }

        private void ImportTransactionAcknowledgement()
        {
            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            dtGridTransSentACK.DataSource = ds.GetDataSourceAllType("EFTImportAcknowledgement"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
            dtGridTransSentACK.DataBind();
            if (dtGridTransSentACK.Items.Count == 0)
            {
                lblTransSentACK.Text = "No files.";
            }
        }

        protected void dtGridTransactionError_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string errorFileName = dtGridTransactionError.DataKeys[e.Item.ItemIndex].ToString();
            int LastIndex = errorFileName.LastIndexOf("\\");
            string saveAs = errorFileName.Substring(LastIndex + 1, (errorFileName.Length - LastIndex - 1));
            if (e.CommandName == "DownloadTRErrorFiles")
            {
                Response.ClearContent();
                Response.AddHeader("content-disposition",
                         "attachment;filename=" + saveAs);

                Response.ContentType = "xml/ack";

                string filecontent = File.ReadAllText(errorFileName);
                Response.Write(filecontent);
                Response.End();
            }

            if (e.CommandName == "RemoveTRErrorFiles")
            {
                if (System.IO.File.Exists(errorFileName))
                {
                    System.IO.File.Delete(errorFileName);
                }
            }
            ImportTransactionError();
        }


        protected void lnkBtnImportTransSentACK_Click(object sender, EventArgs e)
        {
            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            int session = 0;
            DateTime settlementDate = new DateTime();
            string settlementDateValue = string.Empty;
            DataTable dtTransactionACKImport = ds.GetACKDataSource("EFTImportAcknowledgement"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
            if (dtTransactionACKImport.Rows.Count > 0)
            {
                EFTN.BLL.TransactionAcknowledgement transactionAcknowledgement = new EFTN.BLL.TransactionAcknowledgement();
                string connectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

                for (int i = 0; i < dtTransactionACKImport.Rows.Count; i++)
                {
                    DataRow dtTrImportRow = dtTransactionACKImport.Rows[i];
                    string filePath = dtTrImportRow["FilePath"].ToString();
                    string fileName = dtTrImportRow["FileName"].ToString();
                    string fileTypeIdenfier = fileName.Substring(17, 10);
                    if (System.IO.File.Exists(filePath))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(filePath);
                        session = int.Parse(doc.GetElementsByTagName("Session").Item(0).InnerText.Trim());
                        settlementDateValue = doc.GetElementsByTagName("SettlementDate").Item(0).InnerText;
                        settlementDate = DateTime.Parse(DateTime.ParseExact(settlementDateValue, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
                        if (fileTypeIdenfier.StartsWith("1"))
                        {
                            if (session > 0)
                            {
                                transactionAcknowledgement.SaveDataToTransactionSentAcknowledgement(fileName, session, settlementDate, connectionString);
                            }
                            System.IO.File.Delete(filePath);
                        }
                        if (fileTypeIdenfier.StartsWith("2"))
                        {
                            if (session > 0)
                            {
                                transactionAcknowledgement.SaveDataToReturnSent(fileName, session, settlementDate);
                            }
                            System.IO.File.Delete(filePath);
                        }
                        if (fileTypeIdenfier.StartsWith("4"))
                        {
                            if (session > 0)
                            {
                                transactionAcknowledgement.SaveDataToNOCSent(fileName, session);
                            }
                            System.IO.File.Delete(filePath);
                        }
                    }
                }
            }
            ImportTransactionAcknowledgement();

        }

        protected void linkBtnImportTransaction_Click(object sender, EventArgs e)
        {

            if (Global.lastImportedTime.Equals(string.Empty) || Global.lastImportedTime == null)
            {
                Global.lastImportedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
            }
            else
            {
                string lastImportedTime = Global.lastImportedTime;

                string currentTime = System.DateTime.Now.ToString("yyyyMMddHHmm");

                if (lastImportedTime.Equals(currentTime))
                {
                    lblMsgTransactionReceived.Text = "Please import after 1 minute";
                    return;
                }
                else
                {
                    Global.lastImportedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
                    lblMsgTransactionReceived.Text = "";
                }
            }


            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            DataTable dtTransactionImport = ds.GetDataSource("EFTTransactionImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
            if (dtTransactionImport.Rows.Count > 0)
            {
                EFTN.BLL.TransactionReceivedXML transactionReceivedXML = new EFTN.BLL.TransactionReceivedXML();
                for (int i = 0; i < dtTransactionImport.Rows.Count; i++)
                {
                    DataRow dtTrImportRow = dtTransactionImport.Rows[i];
                    string filepath = dtTrImportRow["FilePath"].ToString();
                    string fileName = dtTrImportRow["FileName"].ToString();
                    if (System.IO.File.Exists(filepath))
                    {
                        transactionReceivedXML.SaveDataToDB(filepath, fileName);
                    }
                }
            }
            ImportTransaction();
        }

        protected void linkBtnImportReturn_Click(object sender, EventArgs e)
        {
            if (Global.lastImportedTime.Equals(string.Empty) || Global.lastImportedTime == null)
            {
                Global.lastImportedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
            }
            else
            {
                string lastImportedTime = Global.lastImportedTime;

                string currentTime = System.DateTime.Now.ToString("yyyyMMddHHmm");

                if (lastImportedTime.Equals(currentTime))
                {
                    lblImportReturn.Text = "Please import after 1 minute";
                    return;
                }
                else
                {
                    Global.lastImportedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
                    lblImportReturn.Text = "";
                }
            }


            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            DataTable dtReturnImport = ds.GetDataSource("EFTReturnImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
            if (dtReturnImport.Rows.Count > 0)
            {
                EFTN.BLL.ReturnReceivedXML returnReceivedXML = new EFTN.BLL.ReturnReceivedXML();
                EFTN.BLL.NOCReceivedXML nOCReceivedXML = new EFTN.BLL.NOCReceivedXML();
                for (int i = 0; i < dtReturnImport.Rows.Count; i++)
                {
                    DataRow dtRtImportRow = dtReturnImport.Rows[i];
                    string filepath = dtRtImportRow["FilePath"].ToString();
                    string fileName = dtRtImportRow["FileName"].ToString();

                    if (System.IO.File.Exists(filepath))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(filepath);
                        string allXml = doc.OuterXml;
                        allXml = allXml.Replace("<BHR>", "<Bundle><BHR>");
                        allXml = allXml.Replace("</BCR>", "</BCR></Bundle>");
                        doc.LoadXml(allXml);
                        string fileType = string.Empty;
                        string currency = doc.GetElementsByTagName("Currency").Item(0).InnerText.Trim();
                        int session = int.Parse(doc.GetElementsByTagName("Session").Item(0).InnerText.Trim());
                        foreach (XmlElement elemBundle in doc.GetElementsByTagName("Bundle"))
                        {
                            foreach (XmlElement elemBHR in elemBundle.GetElementsByTagName("BHR"))
                            {
                                string SECC = elemBHR.GetElementsByTagName("SECC").Item(0).InnerText;
                                if (SECC.Equals("NOC"))
                                {
                                    fileType = "NOC";
                                }
                            }
                        }
                        if (fileType.Equals("NOC"))
                        {
                            nOCReceivedXML.SaveDataToDB(filepath, fileName);
                        }
                        else
                        {
                            returnReceivedXML.SaveToDB(filepath, fileName, currency, session);
                        }
                    }
                }
            }
            ImportReturn();
        }

        protected void linkBtnImportNOC_Click(object sender, EventArgs e)
        {
            if (Global.lastImportedTime.Equals(string.Empty) || Global.lastImportedTime == null)
            {
                Global.lastImportedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
            }
            else
            {
                string lastImportedTime = Global.lastImportedTime;

                string currentTime = System.DateTime.Now.ToString("yyyyMMddHHmm");

                if (lastImportedTime.Equals(currentTime))
                {
                    lblImportNOC.Text = "Please import after 1 minute";
                    return;
                }
                else
                {
                    Global.lastImportedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
                    lblImportNOC.Text = "";
                }
            }


            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            DataTable dtNOCImport = ds.GetDataSource("EFTNOCImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
            if (dtNOCImport.Rows.Count > 0)
            {
                EFTN.BLL.NOCReceivedXML nOCReceivedXML = new EFTN.BLL.NOCReceivedXML();
                for (int i = 0; i < dtNOCImport.Rows.Count; i++)
                {
                    DataRow dtNOCImportRow = dtNOCImport.Rows[i];
                    string filepath = dtNOCImportRow["FilePath"].ToString();
                    string fileName = dtNOCImportRow["FileName"].ToString();
                    if (System.IO.File.Exists(filepath))
                    {
                        nOCReceivedXML.SaveDataToDB(filepath, fileName);
                    }
                }
            }
            ImportNOC();
        }

        protected void linkBtnContested_Click(object sender, EventArgs e)
        {
            if (Global.lastImportedTime.Equals(string.Empty) || Global.lastImportedTime == null)
            {
                Global.lastImportedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
            }
            else
            {
                string lastImportedTime = Global.lastImportedTime;

                string currentTime = System.DateTime.Now.ToString("yyyyMMddHHmm");

                if (lastImportedTime.Equals(currentTime))
                {
                    lblContested.Text = "Please import after 1 minute";
                    return;
                }
                else
                {
                    Global.lastImportedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
                    lblContested.Text = "";
                }
            }


            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            DataTable dtContestedImport = ds.GetDataSource("EFTContestedImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
            if (dtContestedImport.Rows.Count > 0)
            {
                EFTN.BLL.ContestedDishonorXML contestedDishonorXML = new EFTN.BLL.ContestedDishonorXML();
                for (int i = 0; i < dtContestedImport.Rows.Count; i++)
                {
                    DataRow dtContestedImportRow = dtContestedImport.Rows[i];
                    string filepath = dtContestedImportRow["FilePath"].ToString();
                    if (System.IO.File.Exists(filepath))
                    {
                        contestedDishonorXML.SaveDataToDB(filepath);
                    }
                }
            }
            ImportContested();
        }

        protected void linkBtnDishonor_Click(object sender, EventArgs e)
        {
            if (Global.lastImportedTime.Equals(string.Empty) || Global.lastImportedTime == null)
            {
                Global.lastImportedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
            }
            else
            {
                string lastImportedTime = Global.lastImportedTime;

                string currentTime = System.DateTime.Now.ToString("yyyyMMddHHmm");

                if (lastImportedTime.Equals(currentTime))
                {
                    lblDishonor.Text = "Please import after 1 minute";
                    return;
                }
                else
                {
                    Global.lastImportedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
                    lblDishonor.Text = "";
                }
            }


            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            DataTable dtDishonorImport = ds.GetDataSource("EFTDishonouredReturnImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
            if (dtDishonorImport.Rows.Count > 0)
            {
                EFTN.BLL.DishonoredReceivedXML dishonoredReceivedXML = new EFTN.BLL.DishonoredReceivedXML();
                for (int i = 0; i < dtDishonorImport.Rows.Count; i++)
                {
                    DataRow dtDishonorImportRow = dtDishonorImport.Rows[i];
                    string filepath = dtDishonorImportRow["FilePath"].ToString();
                    if (System.IO.File.Exists(filepath))
                    {
                        dishonoredReceivedXML.SaveDataToDB(filepath);
                    }
                }
            }
            ImportDishonor();
        }

        protected void linkBtnRNOC_Click(object sender, EventArgs e)
        {
            if (Global.lastImportedTime.Equals(string.Empty) || Global.lastImportedTime == null)
            {
                Global.lastImportedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
            }
            else
            {
                string lastImportedTime = Global.lastImportedTime;

                string currentTime = System.DateTime.Now.ToString("yyyyMMddHHmm");

                if (lastImportedTime.Equals(currentTime))
                {
                    lblRNOC.Text = "Please import after 1 minute";
                    return;
                }
                else
                {
                    Global.lastImportedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
                    lblRNOC.Text = "";
                }
            }


            EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
            WSClient ws = new WSClient();
            DataTable dtRNOCImport = ds.GetDataSource("EFTRNOCImport"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
            if (dtRNOCImport.Rows.Count > 0)
            {
                EFTN.BLL.RNOCReceivedXML rNOCReceivedXML = new EFTN.BLL.RNOCReceivedXML();
                for (int i = 0; i < dtRNOCImport.Rows.Count; i++)
                {
                    DataRow dtRNOCImportRow = dtRNOCImport.Rows[i];
                    string filepath = dtRNOCImportRow["FilePath"].ToString();
                    if (System.IO.File.Exists(filepath))
                    {
                        rNOCReceivedXML.SaveDataToDB(filepath);
                    }
                }
            }
            ImportRNOC();
        }

        private void BindDataForTransactionSent()
        {
            int DepartmentID = 0;
            if (ConfigurationManager.AppSettings["DepartmentWise"].Equals("1"))
            {
                DepartmentID = EFTN.Utility.ParseData.StringToInt(Request.Cookies["DepartmentID"].Value);
            }

            EFTN.component.SentBatchDB batchDB = new EFTN.component.SentBatchDB();
            dtgBatchTransactionSent.DataSource = batchDB.GetBatches_For_TransactionSent(DepartmentID);
            dtgBatchTransactionSent.DataBind();
        }

        protected void cbxAllTransactionSent_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllTransactionSent.Checked;
            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");
                cbx.Checked = checkAllChecked;
                //if (i > 34)
                //{
                //    lblMsgExportTransaction.Text = "Maximum 35 batch can be selected at a time";
                //    break;
                //}
            }
        }

        protected void btnGenerateXmlForTransactionSent_Click(object sender, EventArgs e)
        {
            /*
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
            */
            DateTime lastXmlCreationTime = new DateTime();
            DateTime newXMLCreationTime = new DateTime();
            XMLLogDB xmlLogDB = new XMLLogDB();

            if (!Global.fileGenerateStatus.Equals("LOCKED"))
            {
                Global.fileGenerateStatus = "LOCKED";
                lblMsgExportTransaction.Text = string.Empty;
                //Get the Last XML file Generation Time from the Database and then add 1 minute and generate xml file
                DataTable dtXmlLog = xmlLogDB.GetXMLLogOfCurrentDate();
                lastXmlCreationTime = DateTime.Parse(dtXmlLog.Rows[0]["XmlDate"].ToString());
                newXMLCreationTime = lastXmlCreationTime = lastXmlCreationTime.AddMinutes(1);
            }
            else
            {
                lblMsgExportTransaction.Text = "Other operator is generating file. Please try after few minutes";
                return;
            }


            int enevelopeID = -1;
            bool needEnvelope = false;
            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");
                if (cbx.Checked)
                {
                    needEnvelope = true;
                    break;
                }
            }
            if (needEnvelope)
            {
                TransactionSentXML transactionSentXML = new TransactionSentXML();
                enevelopeID = CreateEnvelopeForTransactionSent();
                for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
                {
                    transactionSentXML.batchCounterOnlyForBatchCount = 0;
                    CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");
                    if (cbx.Checked)
                    {
                        Guid transactionID = (Guid)dtgBatchTransactionSent.DataKeys[i];
                        if (!transactionSentXML.IsXMLAlreadyCreatedForTheBatch(transactionID))
                        {
                            transactionSentXML.batchCounterOnlyForBatchCount++;
                            string createdXMLFileName = transactionSentXML.CreateBatchXML(transactionID, ref lastXmlCreationTime, ref newXMLCreationTime);
                            SetSettlementDate setSettlementDate = new SetSettlementDate();
                            DateTime TrSentSettlementDate = setSettlementDate.GetOutwardTransactionSettlementDate();
                            db.UpdateBatchSent(transactionID, enevelopeID, TrSentSettlementDate, createdXMLFileName);
                        }
                    }
                }
            }

            xmlLogDB.UpdateXMLogOfCurrentDate(newXMLCreationTime);
            Global.fileGenerateStatus = string.Empty;
            BindDataForTransactionSent();
        }

        private int CreateEnvelopeForTransactionSent()
        {
            EFTN.component.EnvelopeDB db = new EFTN.component.EnvelopeDB();
            return db.InsertEnvelopSent(
                    (int)EFTN.Utility.ItemType.TransactionSent,
                     ConfigurationManager.AppSettings["OriginBank"],
                     ConfigurationManager.AppSettings["BangladeshBankRouting"],
                     "",
                     0);
        }

        private void BindDataForReturnSent()
        {
            EFTN.component.ReceivedBatchDB db = new EFTN.component.ReceivedBatchDB();
            dtgBatchReturnSent.DataSource = db.GetBatches_For_RRSent();
            dtgBatchReturnSent.DataBind();
        }

        protected void cbxAllReturn_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllReturn.Checked;
            for (int i = 0; i < dtgBatchReturnSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchReturnSent.Items[i].FindControl("cbxSentBatchReturn");
                cbx.Checked = checkAllChecked;
                if (i > 34)
                {
                    lblMsgExportReturn.Text = "Maximum 35 batch can be selected at a time";
                    break;
                }
            }
        }

        protected void btnGenerateXmlForReturn_Click(object sender, EventArgs e)
        {
            /*
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
                lblMsgExportReturn.Text = "Please click after 1 minute to generate new file";
                return;
            }
            else
            {
                Global.lastFileCreatedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
                lblMsgExportReturn.Text = "";
            }
            */
            DateTime lastXmlCreationTime = new DateTime();
            DateTime newXMLCreationTime = new DateTime();
            XMLLogDB xmlLogDB = new XMLLogDB();

            if (!Global.fileGenerateStatus.Equals("LOCKED"))
            {
                Global.fileGenerateStatus = "LOCKED";
                lblMsgExportTransaction.Text = string.Empty;
                //Get the Last XML file Generation Time from the Database and then add 1 minute and generate xml file
                DataTable dtXmlLog = xmlLogDB.GetXMLLogOfCurrentDate();
                lastXmlCreationTime = DateTime.Parse(dtXmlLog.Rows[0]["XmlDate"].ToString());
                newXMLCreationTime = lastXmlCreationTime = lastXmlCreationTime.AddMinutes(1);
            }
            else
            {
                lblMsgExportTransaction.Text = "Other operator is generating file. Please try after few minutes";
                return;
            }
            //////////////////////////////////////////////////////////////FILE ID MODIFIER CHANGE END

            bool needEnvelope = false;
            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            for (int i = 0; i < dtgBatchReturnSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchReturnSent.Items[i].FindControl("cbxSentBatchReturn");
                if (cbx.Checked)
                {
                    needEnvelope = true;
                    break;
                }
            }

            if (needEnvelope)
            {
                int batchCounterOnlyForBatchCount = 0;
                string currency = dtgBatchReturnSent.Items[0].Cells[5].Text.Trim();
                //string session = dtgBatchReturnSent.Items[0].Cells[5].Text.Trim();
                string session = string.Empty;

                #region Added New Due to file naming change from Central Bank
                TransactionXMLFileNameDB transactionXmlDb = new TransactionXMLFileNameDB();
                string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                #endregion


                for (int i = 0; i < dtgBatchReturnSent.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)dtgBatchReturnSent.Items[i].FindControl("cbxSentBatchReturn");
                    if (cbx.Checked)
                    {
                        StringBuilder allXml = new StringBuilder();
                        EFTN.BLL.ReturnXML returnXML = new EFTN.BLL.ReturnXML();
                        allXml.Append(returnXML.GetFHRXML(batchCounterOnlyForBatchCount, ref lastXmlCreationTime, ref newXMLCreationTime, currency, session));
                        int returnBatchCount = i + 1;
                        //Commented out according to Central Bank's Recommendation on April 2018
                        //string fileName = "EFT-Ret-D" + DateTime.Now.ToString("yyMMdd") + "-T" + DateTime.Now.ToString("HHmmss") + returnBatchCount.ToString() + ".XML";
                        int lastIdentity = transactionXmlDb.GetLastXmlIdentity(2);
                        string fileIdentity = lastIdentity.ToString().PadLeft(9, '0');
                        string fileName = "EFT_" + DateTime.Now.ToString("yyyyMMdd") + "_" + originBank + "_2" + fileIdentity + "_" + currency + ".XML";

                        Guid transactionID = (Guid)dtgBatchReturnSent.DataKeys[i];
                        allXml.Append(returnXML.CreateBatchXML(transactionID, fileName));
                        allXml.Append(returnXML.GetFCRXML());
                        returnXML.CreateReturnXml(allXml.ToString(), fileName);
                        batchCounterOnlyForBatchCount++;

                        if (batchCounterOnlyForBatchCount > 34)
                        {
                            break;
                        }
                    }
                }
            }
            xmlLogDB.UpdateXMLogOfCurrentDate(newXMLCreationTime);
            Global.fileGenerateStatus = string.Empty;
            BindDataForReturnSent();
        }

        private void BindDataForNOCSent()
        {
            EFTN.component.ReceivedBatchDB db = new EFTN.component.ReceivedBatchDB();
            dtgBatchNOCSent.DataSource = db.GetBatches_For_NOCSent();
            dtgBatchNOCSent.DataBind();
        }

        protected void cbxAllNOCExport_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllNOCExport.Checked;
            for (int i = 0; i < dtgBatchNOCSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchNOCSent.Items[i].FindControl("cbxSentBatchNOC");
                cbx.Checked = checkAllChecked;
                if (i > 34)
                {
                    lblExportNOC.Text = "Maximum 35 batch can be selected at a time";
                    break;
                }
            }
        }

        protected void btnGenerateXmlForNOC_Click(object sender, EventArgs e)
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
                lblExportNOC.Text = "Please click after 1 minute to generate new file";
                return;
            }
            else
            {
                Global.lastFileCreatedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
                lblExportNOC.Text = "";
            }

            bool needEnvelope = false;
            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            for (int i = 0; i < dtgBatchNOCSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchNOCSent.Items[i].FindControl("cbxSentBatchNOC");
                if (cbx.Checked)
                {
                    needEnvelope = true;
                    break;
                }
            }
            if (needEnvelope)
            {
                int batchCounterOnlyForBatchCount = 0;
                for (int i = 0; i < dtgBatchNOCSent.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)dtgBatchNOCSent.Items[i].FindControl("cbxSentBatchNOC");
                    if (cbx.Checked)
                    {
                        StringBuilder allXml = new StringBuilder();
                        EFTN.BLL.NOCSentXML nOCSentXML = new EFTN.BLL.NOCSentXML();
                        allXml.Append(nOCSentXML.GetFHRXML(batchCounterOnlyForBatchCount));
                        int nocBatchCount = i + 1;
                        string XMLFileName = "EFT-NOC-D" + DateTime.Now.ToString("yyMMdd") + "-T" + DateTime.Now.ToString("HHmmss") + nocBatchCount.ToString() + ".XML";

                        Guid transactionID = (Guid)dtgBatchNOCSent.DataKeys[i];
                        allXml.Append(nOCSentXML.CreateBatchXML(transactionID, XMLFileName));

                        allXml.Append(nOCSentXML.GetFCRXML());
                        nOCSentXML.CreateNOCXml(allXml.ToString(), XMLFileName);
                        batchCounterOnlyForBatchCount++;

                        if (batchCounterOnlyForBatchCount > 34)
                        {
                            break;
                        }
                    }
                }

            }

            BindDataForNOCSent();
        }

        private void BindDataForDishonorSent()
        {
            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            dtgBatchDishonorSent.DataSource = db.GetBatches_For_DishonorSent();
            dtgBatchDishonorSent.DataBind();
        }

        protected void cbxAllDihonorSent_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllDihonorSent.Checked;
            for (int i = 0; i < dtgBatchDishonorSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchDishonorSent.Items[i].FindControl("cbxSentBatchDishonor");
                cbx.Checked = checkAllChecked;
                if (i > 34)
                {
                    lblExportNOC.Text = "Maximum 35 batch can be selected at a time";
                    break;
                }
            }
        }

        protected void btnGenerateXmlForDishonor_Click(object sender, EventArgs e)
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
                lblExportDishonor.Text = "Please click after 1 minute to generate new file";
                return;
            }
            else
            {
                Global.lastFileCreatedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
                lblExportDishonor.Text = "";
            }

            bool needEnvelope = false;
            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            for (int i = 0; i < dtgBatchDishonorSent.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchDishonorSent.Items[i].FindControl("cbxSentBatchDishonor");
                if (cbx.Checked)
                {
                    needEnvelope = true;
                    break;
                }
            }

            if (needEnvelope)
            {
                #region Added New Due to file naming change from Central Bank
                TransactionXMLFileNameDB transactionXmlDb = new TransactionXMLFileNameDB();
                string originBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);
                #endregion
                int batchCounterOnlyForBatchCount = 0;
                string currency = dtgBatchDishonorSent.Items[0].Cells[5].Text.Trim();

                for (int i = 0; i < dtgBatchDishonorSent.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)dtgBatchDishonorSent.Items[i].FindControl("cbxSentBatchDishonor");
                    if (cbx.Checked)
                    {
                        EFTN.BLL.DishonorSentXML dishonorSentXML = new EFTN.BLL.DishonorSentXML();
                        System.Text.StringBuilder allXml = new System.Text.StringBuilder();
                        allXml.Append(dishonorSentXML.GetFHRXML(batchCounterOnlyForBatchCount, currency));
                        #region Added New due to Central Bank's file naming change
                        int lastIdentity = transactionXmlDb.GetLastXmlIdentity(3);
                        string fileIdentity = lastIdentity.ToString().PadLeft(9, '0');
                        string fileName = "EFT_" + DateTime.Now.ToString("yyyyMMdd") + "_" + originBank + "_3" + fileIdentity + ".XML";
                        #endregion
                        Guid transactionID = (Guid)dtgBatchDishonorSent.DataKeys[i];
                        allXml.Append(dishonorSentXML.CreateBatchXML(transactionID, fileName));
                        allXml.Append(dishonorSentXML.GetFCRXML());
                        //Commented out the old code block
                        //dishonorSentXML.CreateDishonorSentXml(allXml.ToString(), i + 1);
                        dishonorSentXML.CreateDishonorSentXml(allXml.ToString(), fileName);
                        batchCounterOnlyForBatchCount++;

                        if (batchCounterOnlyForBatchCount > 34)
                        {
                            break;
                        }
                    }
                }

            }
            BindDataForDishonorSent();
        }

        private void BindDataForDishonorContestedSent()
        {
            EFTN.component.ReceivedBatchDB db = new EFTN.component.ReceivedBatchDB();
            dtgBatchContestedDishonor.DataSource = db.GetBatchesForContestedSent();
            dtgBatchContestedDishonor.DataBind();
        }

        protected void cbxAllContestedSent_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllContestedSent.Checked;
            for (int i = 0; i < dtgBatchContestedDishonor.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchContestedDishonor.Items[i].FindControl("cbxSentBatchContested");
                cbx.Checked = checkAllChecked;
                if (i > 34)
                {
                    lblExportNOC.Text = "Maximum 35 batch can be selected at a time";
                    break;
                }
            }
        }

        protected void btnGenerateXmlForContestedSent_Click(object sender, EventArgs e)
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
                lblDishonorContested.Text = "Please click after 1 minute to generate new file";
                return;
            }
            else
            {
                Global.lastFileCreatedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
                lblDishonorContested.Text = "";
            }


            bool needEnvelope = false;
            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            for (int i = 0; i < dtgBatchContestedDishonor.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgBatchContestedDishonor.Items[i].FindControl("cbxSentBatchContested");
                if (cbx.Checked)
                {
                    needEnvelope = true;
                    break;
                }
            }

            if (needEnvelope)
            {
                int batchCounterOnlyForBatchCount = 0;

                for (int i = 0; i < dtgBatchContestedDishonor.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)dtgBatchContestedDishonor.Items[i].FindControl("cbxSentBatchContested");
                    if (cbx.Checked)
                    {
                        EFTN.BLL.ContestedDishonorXML contestedDishonorXML = new EFTN.BLL.ContestedDishonorXML();
                        StringBuilder allXml = new StringBuilder();
                        allXml.Append(contestedDishonorXML.GetFHRXML(batchCounterOnlyForBatchCount));

                        Guid transactionID = (Guid)dtgBatchContestedDishonor.DataKeys[i];
                        allXml.Append(contestedDishonorXML.CreateBatchXML(transactionID));

                        allXml.Append(contestedDishonorXML.GetFCRXML());
                        contestedDishonorXML.CreateContestedXml(allXml.ToString(), i + 1);
                        batchCounterOnlyForBatchCount++;

                        if (batchCounterOnlyForBatchCount > 34)
                        {
                            break;
                        }
                    }
                }

                BindDataForDishonorContestedSent();
            }
        }

        private void BindDataForRNOCSent()
        {
            EFTN.component.RNOCBatchDB rNOCBatchDB = new EFTN.component.RNOCBatchDB();
            dtgRNOCBatch.DataSource = rNOCBatchDB.GetRNOCBatchData();
            dtgRNOCBatch.DataBind();
        }

        protected void cbxAllRNOCSent_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAllChecked = cbxAllRNOCSent.Checked;
            for (int i = 0; i < dtgRNOCBatch.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgRNOCBatch.Items[i].FindControl("cbxRNOCBatch");
                cbx.Checked = checkAllChecked;
                if (i > 34)
                {
                    lblExportNOC.Text = "Maximum 35 batch can be selected at a time";
                    break;
                }
            }
        }

        protected void linkBtnGenerateXMLForRNOCSent_Click(object sender, EventArgs e)
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
                lblExportRNOC.Text = "Please click after 1 minute to generate new file";
                return;
            }
            else
            {
                Global.lastFileCreatedTime = System.DateTime.Now.ToString("yyyyMMddHHmm");
                lblExportRNOC.Text = "";
            }


            bool needEnvelope = false;
            EFTN.component.SentBatchDB db = new EFTN.component.SentBatchDB();
            for (int i = 0; i < dtgRNOCBatch.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)dtgRNOCBatch.Items[i].FindControl("cbxRNOCBatch");
                if (cbx.Checked)
                {
                    needEnvelope = true;
                    break;
                }
            }
            if (needEnvelope)
            {
                int batchCounterOnlyForBatchCount = 0;

                for (int i = 0; i < dtgRNOCBatch.Items.Count; i++)
                {
                    CheckBox cbx = (CheckBox)dtgRNOCBatch.Items[i].FindControl("cbxRNOCBatch");
                    if (cbx.Checked)
                    {
                        EFTN.BLL.RNOCSentXML rNOCSentXML = new EFTN.BLL.RNOCSentXML();
                        StringBuilder allXml = new StringBuilder();
                        allXml.Append(rNOCSentXML.GetFHRXML(batchCounterOnlyForBatchCount));

                        Guid transactionID = (Guid)dtgRNOCBatch.DataKeys[i];
                        allXml.Append(rNOCSentXML.CreateBatchXML(transactionID));

                        allXml.Append(rNOCSentXML.GetFCRXML());
                        rNOCSentXML.CreateRNOCXml(allXml.ToString(), i + 1);
                        batchCounterOnlyForBatchCount++;

                        if (batchCounterOnlyForBatchCount > 34)
                        {
                            break;
                        }
                    }
                }
                BindDataForRNOCSent();
            }

        }

        protected void btnDeleteBatchSent_Click(object sender, EventArgs e)
        {
            TransactionSentXML transactionSentXML = new TransactionSentXML();

            for (int i = 0; i < dtgBatchTransactionSent.Items.Count; i++)
            {
                transactionSentXML.batchCounterOnlyForBatchCount = 0;
                CheckBox cbx = (CheckBox)dtgBatchTransactionSent.Items[i].FindControl("cbxSentBatchTS");
                if (cbx.Checked)
                {
                    Guid transactionID = (Guid)dtgBatchTransactionSent.DataKeys[i];
                    if (!transactionSentXML.IsXMLAlreadyCreatedForTheBatch(transactionID))
                    {
                        EFTN.component.SentBatchDB sentBatchDB = new EFTN.component.SentBatchDB();
                        sentBatchDB.DeleteBatchSent(transactionID);
                        BindDataForTransactionSent();
                    }
                }
            }
        }
    }
}