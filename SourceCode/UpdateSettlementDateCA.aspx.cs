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
using EFTN.component;
using EFTN.Utility;

namespace EFTN
{
    public partial class UpdateSettlementDateCA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                clnderSettlementDate.SelectedDate = System.DateTime.Now;

                ddlistDay.SelectedValue = System.DateTime.Now.Day.ToString().PadLeft(2, '0');
                ddlistMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlistYear.SelectedValue = System.DateTime.Now.Year.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SettlementDateDB settlementDateDB = new SettlementDateDB();
            if (ddListTransactionType.SelectedValue.Equals("1"))
            {
                settlementDateDB.UpdateSettlementDateForTransactionSent(clnderSettlementDate.SelectedDate
                                                        , ParseData.StringToInt(ddlistDay.SelectedValue)
                                                        , ParseData.StringToInt(ddlistMonth.SelectedValue)
                                                        , ParseData.StringToInt(ddlistYear.SelectedValue)
                                                        );
            }
            else if (ddListTransactionType.SelectedValue.Equals("2"))
            {
                settlementDateDB.UpdateSettlementDateForReturnSent(clnderSettlementDate.SelectedDate
                                                        , ParseData.StringToInt(ddlistDay.SelectedValue)
                                                        , ParseData.StringToInt(ddlistMonth.SelectedValue)
                                                        , ParseData.StringToInt(ddlistYear.SelectedValue)
                                                        );
            }
            else if (ddListTransactionType.SelectedValue.Equals("3"))
            {
                settlementDateDB.UpdateSettlementDateForNOCSent(clnderSettlementDate.SelectedDate
                                        , ParseData.StringToInt(ddlistDay.SelectedValue)
                                        , ParseData.StringToInt(ddlistMonth.SelectedValue)
                                        , ParseData.StringToInt(ddlistYear.SelectedValue)
                                        );
            }
            else if (ddListTransactionType.SelectedValue.Equals("4"))
            {
                settlementDateDB.UpdateSettlementDateForDishonorSent(clnderSettlementDate.SelectedDate
                                        , ParseData.StringToInt(ddlistDay.SelectedValue)
                                        , ParseData.StringToInt(ddlistMonth.SelectedValue)
                                        , ParseData.StringToInt(ddlistYear.SelectedValue)
                                        );
            }

        

            else if (ddListTransactionType.SelectedValue.Equals("6"))
            {
                settlementDateDB.UpdateSettlementDateForReturnReceived(clnderSettlementDate.SelectedDate
                                        , ParseData.StringToInt(ddlistDay.SelectedValue)
                                        , ParseData.StringToInt(ddlistMonth.SelectedValue)
                                        , ParseData.StringToInt(ddlistYear.SelectedValue)
                                        );
            }

            else if (ddListTransactionType.SelectedValue.Equals("7"))
            {
                settlementDateDB.UpdateSettlementDateForNOCReceived(clnderSettlementDate.SelectedDate
                                        , ParseData.StringToInt(ddlistDay.SelectedValue)
                                        , ParseData.StringToInt(ddlistMonth.SelectedValue)
                                        , ParseData.StringToInt(ddlistYear.SelectedValue)
                                        );
            }
            else if (ddListTransactionType.SelectedValue.Equals("8"))
            {
                settlementDateDB.UpdateSettlementDateForReceivedEDR(clnderSettlementDate.SelectedDate
                                        , ParseData.StringToInt(ddlistDay.SelectedValue)
                                        , ParseData.StringToInt(ddlistMonth.SelectedValue)
                                        , ParseData.StringToInt(ddlistYear.SelectedValue)
                                        );
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}
