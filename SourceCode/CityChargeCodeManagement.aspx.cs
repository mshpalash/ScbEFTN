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
using EFTN.BLL;
using EFTN.Utility;

namespace FloraSoft
{
    public partial class CityChargeCodeManagement : System.Web.UI.Page
    {
        private void BindChargeCodeInfo()
        {
            EFTChargeManager eftChargeManager = new EFTChargeManager();
            dataGridChargeCodeInfo.DataSource = eftChargeManager.GetCityChargeCodeInfo();
            dataGridChargeCodeInfo.DataBind();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            BindChargeCodeInfo();
        }

        protected void dataGridChargeCodeInfo_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            BranchesDB db = new BranchesDB();
            if (e.CommandName == "Cancel")
            {
                dataGridChargeCodeInfo.EditItemIndex = -1;
            }
            if (e.CommandName == "Edit")
            {
                dataGridChargeCodeInfo.EditItemIndex = e.Item.ItemIndex;
            }
            if (e.CommandName == "Insert")
            {
                TextBox txtBBCharge = (TextBox)e.Item.FindControl("addBBCharge");
                TextBox txtBankCharge = (TextBox)e.Item.FindControl("addBankCharge");
                TextBox txtBBChargeVAT = (TextBox)e.Item.FindControl("addBBChargeVAT");
                TextBox txtBankChargeVAT = (TextBox)e.Item.FindControl("addBankChargeVAT");
                TextBox txtChargeWave = (TextBox)e.Item.FindControl("addChargeWave");
                TextBox txtVATWave = (TextBox)e.Item.FindControl("addVATWave");
                
                double dBBCharge        =  ParseData.StringToDouble(txtBBCharge.Text);
                double dBankCharge      =  ParseData.StringToDouble(txtBankCharge.Text);
                double dBBChargeVAT     =  ParseData.StringToDouble(txtBBChargeVAT.Text);
                double dBankChargeVAT   =  ParseData.StringToDouble(txtBankChargeVAT.Text);
                double dChargeWave      =  ParseData.StringToDouble(txtChargeWave.Text);
                double dVATWave         = ParseData.StringToDouble(txtVATWave.Text);

                EFTChargeManager eftChargeManager = new EFTChargeManager();
                eftChargeManager.InsertCityChargeCodeInfo(dBBCharge, dBankCharge, dBBChargeVAT, dBankChargeVAT, dChargeWave, dVATWave);
                dataGridChargeCodeInfo.EditItemIndex = -1;
            }
            if (e.CommandName == "Update")
            {
                int CityChargeCode = (int)dataGridChargeCodeInfo.DataKeys[e.Item.ItemIndex];
                TextBox txtBBCharge = (TextBox)e.Item.FindControl("BBCharge");
                TextBox txtBankCharge = (TextBox)e.Item.FindControl("BankCharge");
                TextBox txtBBChargeVAT = (TextBox)e.Item.FindControl("BBChargeVAT");
                TextBox txtBankChargeVAT = (TextBox)e.Item.FindControl("BankChargeVAT");
                TextBox txtChargeWave = (TextBox)e.Item.FindControl("ChargeWave");
                TextBox txtVATWave = (TextBox)e.Item.FindControl("VATWave");

                double dBBCharge = ParseData.StringToDouble(txtBBCharge.Text);
                double dBankCharge = ParseData.StringToDouble(txtBankCharge.Text);
                double dBBChargeVAT = ParseData.StringToDouble(txtBBChargeVAT.Text);
                double dBankChargeVAT = ParseData.StringToDouble(txtBankChargeVAT.Text);
                double dChargeWave = ParseData.StringToDouble(txtChargeWave.Text);
                double dVATWave = ParseData.StringToDouble(txtVATWave.Text);

                EFTChargeManager eftChargeManager = new EFTChargeManager();
                eftChargeManager.UpdateCityChargeCodeInfo(CityChargeCode, dBBCharge, dBankCharge, dBBChargeVAT, dBankChargeVAT, dChargeWave, dVATWave);
                dataGridChargeCodeInfo.EditItemIndex = -1;
            }
        }
    }
}