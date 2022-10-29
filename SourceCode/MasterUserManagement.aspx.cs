using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using EFTN.component;
using EFTN.Utility;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EFTNAccelerator;

namespace FloraSoft
{
    public partial class MasterUserManagement : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
           // Response.Redirect("AccessDenied.aspx");
            BindData();
        }
        private void BindData()
        {
            UserDB db = new UserDB();


            MyDataGrid2.DataSource = db.GetAllUsers(ddListUserStatus.SelectedValue, 0);
            MyDataGrid2.DataBind();

            if (ConfigurationManager.AppSettings["MFAValidation"] != null && ConfigurationManager.AppSettings["MFAValidation"] == "0")
            {
                //MyDataGrid2.Columns[5].Visible = false;
                MyDataGrid2.Columns[6].Visible = false;
         

            }
        }

        protected void ddListUserStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        public DataTable ACH_GetRoles()
        {
            RoleDB oDB = new RoleDB();
            return oDB.GetAllRoles();
        }

        public DataTable GetDepartments()
        {
            DepartmentsDB deptDB = new DepartmentsDB();
            return deptDB.GetDepartments();
        }

        public DataTable GetBranchListByBankID()
        {
            BranchesDB branchDB = new BranchesDB();
            return branchDB.GetBranchByBankCode(ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3));
            //return branchDB.GetBranchesByBankID(1);
        }
        protected void MyDataGrid2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            UserDB db = new UserDB();
            //if (Request.Cookies["RoleID"].Value == "9")
            //{
            //    Response.Redirect("AccessDenied.aspx");
            //}
            if (e.CommandName == "Cancel")
            {
                MyDataGrid2.EditItemIndex = -1;
            }
            if (e.CommandName == "Edit")
            {
                MyDataGrid2.EditItemIndex = e.Item.ItemIndex;
            }
            if (e.CommandName == "Insert")
            {
                #region  User Licensing Block
                int totalNoOfUser = db.GetTotalNoOfUser();
                if (UserLicense.IsOverToAdd(totalNoOfUser))
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "User insertion failed: Please renew user license agreement";
                    return;
                }
                #endregion

                bool isGeneric = false;

                TextBox txtUsername = (TextBox)e.Item.FindControl("addUsername");
                DropDownList ddlDepartment  = (DropDownList)e.Item.FindControl("addDepartment");
                TextBox txtContactNo        = (TextBox)e.Item.FindControl("addContactNo");
                TextBox txtLoginID          = (TextBox)e.Item.FindControl("addLoginID");

                TextBox txtPassword = (TextBox)e.Item.FindControl("addPassword");
                DropDownList txtUserRole    = (DropDownList)e.Item.FindControl("addRoleName");
                DropDownList ddlBranchID    = (DropDownList)e.Item.FindControl("addBranchName");
                CheckBox chkBoxIsGeneric = (CheckBox)e.Item.FindControl("chkBoxIsGeneric");

                isGeneric = chkBoxIsGeneric.Checked;


                    int UserID = db.InsertUser(txtUsername.Text, ddlDepartment.SelectedItem.Text, txtContactNo.Text, txtLoginID.Text, txtPassword.Text, Int32.Parse(txtUserRole.SelectedValue), Int32.Parse(ddlBranchID.SelectedValue), Int32.Parse(Context.User.Identity.Name), GetIPAddress(), EFTN.Utility.ParseData.StringToInt(ddlDepartment.SelectedValue), isGeneric);

                UserHistoryDB userHistoryDB = new UserHistoryDB();


                userHistoryDB.InsertUserHistory(ParseData.StringToInt(Request.Cookies["UserID"].Value),
                                                  GetIPAddress()
                                                , UserHistoryVar.NEW_USER_CREATED
                                                , UserID
                                                , ParseData.StringToInt(txtUserRole.SelectedValue)
                                                , ParseData.StringToInt(ddlBranchID.SelectedValue)
                                                , txtUsername.Text
                                                , ParseData.StringToInt(ddlDepartment.SelectedValue)
                                                , txtContactNo.Text
                                                , txtLoginID.Text
                                                , db.Encrypt(txtPassword.Text)
                                                , UserHistoryVar.USER_INACTIVE);

                lblMsg.Text = "User Created";
            }
            if (e.CommandName == "Update")
            {
                int UserID                  = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];
                TextBox txtUsername         = (TextBox)e.Item.FindControl("Username");
                DropDownList ddlDepartment = (DropDownList)e.Item.FindControl("Department");
                TextBox txtContactNo        = (TextBox)e.Item.FindControl("ContactNo");
                TextBox txtLoginID          = (TextBox)e.Item.FindControl("LoginID");
                DropDownList txtUserRole    = (DropDownList)e.Item.FindControl("Role");
                DropDownList ddlBranchID    = (DropDownList)e.Item.FindControl("updateBranch");
                int IsPending = 1;
                db.UpdateUser(UserID, txtUsername.Text, ddlDepartment.SelectedItem.Text, txtContactNo.Text, txtLoginID.Text, Int32.Parse(txtUserRole.SelectedValue), Int32.Parse(ddlBranchID.SelectedValue), Int32.Parse(Context.User.Identity.Name), GetIPAddress(), EFTN.Utility.ParseData.StringToInt(ddlDepartment.SelectedValue), IsPending);
                MyDataGrid2.EditItemIndex   = -1;


                UserHistoryDB userHistoryDB = new UserHistoryDB();
                userHistoryDB.InsertUserHistory(ParseData.StringToInt(Request.Cookies["UserID"].Value),
                            GetIPAddress()
                                                , UserHistoryVar.USER_INFORMATION_UPDATED
                                                , UserID
                                                , ParseData.StringToInt(txtUserRole.SelectedValue)
                                                , ParseData.StringToInt(ddlBranchID.SelectedValue)
                                                , txtUsername.Text
                                                , ParseData.StringToInt(ddlDepartment.SelectedValue)
                                                , txtContactNo.Text
                                                , txtLoginID.Text
                                                , ""
                                                , UserHistoryVar.USER_INACTIVE);
                lblMsg.Text = "Information Updated";                
            }
        }

        private DataTable GetData()
        {
            UserDB db = new UserDB();
            return db.GetAllUsersforRpt(ddListUserStatus.SelectedValue,0);
        }


        //protected void ExpotToPdfBtn_Click(object sender, EventArgs e)
        //{
        //    string FileName = "UserList-Report" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        //    PrintPDF(FileName);

        //}
        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {
            string FileName = "UserList-Report" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            PrintPDF(FileName);
        }

        private void PrintPDF(string FileName)
        {
            DataTable dt = GetData();

            if (dt.Rows.Count == 0)
            {
                return;
            }

            Response.ClearContent();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

            Document document = new Document(PageSize.A4.Rotate(), 10, 10, 8, 8);
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

            Font fnt = new Font(Font.HELVETICA, 8);
            Font fntblue = new Font(Font.HELVETICA, 8);
            fntblue.Color = new Color(0, 0, 255);
            Font fntbld = new Font(Font.HELVETICA, 8);
            fntbld.SetStyle(Font.BOLD);
            Font headerFont = new Font(Font.HELVETICA, 15);

            string spacer = "            -              ";

            string str = spacer;
            str = str + ConfigurationManager.AppSettings["OriginBankName"] + " All Rights Reserved" + spacer;
            str = str + "Confidential: internal use only" + spacer;
            str = str + "Powered By Flora Limited";

            HeaderFooter footer = new HeaderFooter(new Phrase(str, fnt), false);
            footer.Alignment = Element.ALIGN_CENTER;

            document.Footer = footer;
            document.Open();

            string LogoImage = Server.MapPath("images") + "\\SCBLogo.JPG";

            iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(LogoImage);
            jpeg.Alignment = Element.ALIGN_RIGHT;


            PdfPCell logo = new PdfPCell();
            logo.BorderWidth = 0;
            logo.Colspan = 2;
            logo.AddElement(jpeg);

            ////////////////////////////////
            iTextSharp.text.pdf.PdfPTable headertable = new iTextSharp.text.pdf.PdfPTable(3);
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable.DefaultCell.VerticalAlignment = Cell.ALIGN_BOTTOM;
            headertable.DefaultCell.Padding = 0;
            headertable.WidthPercentage = 99;
            headertable.DefaultCell.Border = 0;
            float[] widthsAtHeader = { 40, 40, 20 };
            headertable.SetWidths(widthsAtHeader);

           
             //   string settlementDate = ddlistDay.SelectedValue + "/" + ddlistMonth.SelectedValue.PadLeft(2, '0') + "/" + ddlistYear.SelectedValue;
                headertable.AddCell(new Phrase("User List Report: " , headerFont));

          
            
            headertable.AddCell(new Phrase("Report Generated Time: " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), headerFont));
            //headertable.AddCell(new Phrase(""));
            headertable.AddCell(logo); ;
            document.Add(headertable);

            //string SelectedBank = string.Empty;

            string SelectedBank = ConfigurationManager.AppSettings["OriginBank"].Substring(0, 3);

            float[] headerwidths;
            int NumberOfPdfColumn = 0;
            
            
                headerwidths = new float[] { 15,15,15,15,10,15,15};
                NumberOfPdfColumn = 7;
            

            document.Add(new iTextSharp.text.Paragraph(" "));
            iTextSharp.text.pdf.PdfPTable datatable = new iTextSharp.text.pdf.PdfPTable(NumberOfPdfColumn);
            datatable.DefaultCell.Padding = 4;
            datatable.DefaultCell.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            //float[] headerwidths = { 10, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };
            //float[] headerwidths = { 10, 10, 4, 8, 4, 8, 8, 8, 8, 8, 8, 8 };//Only For JANATA BANK
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 99;

            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            datatable.DefaultCell.BorderWidth = 0.5f;
            datatable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //------------------------------------------

            //PdfPCell c0;
           
            //    c0 = new PdfPCell(new iTextSharp.text.Phrase("User Name", fnt));
            
            ////PdfPCell c0 = new PdfPCell(new iTextSharp.text.Phrase("Bank Name", fnt));
            //c0.BorderWidth = 0.5f;
            //c0.HorizontalAlignment = Cell.ALIGN_LEFT;
            //c0.BackgroundColor = new iTextSharp.text.Color(200, 200, 200);
            //c0.BorderColor = new iTextSharp.text.Color(200, 200, 200);
            //c0.Padding = 4;

            //datatable.AddCell(c0);

            datatable.AddCell(new iTextSharp.text.Phrase("User Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Department Name", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Role", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("User ID", fnt));
          
            datatable.AddCell(new iTextSharp.text.Phrase("Status", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Create Date", fnt));
            datatable.AddCell(new iTextSharp.text.Phrase("Last Login", fnt));



            datatable.HeaderRows = 1;
            datatable.DefaultCell.BackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            datatable.DefaultCell.BorderWidth = 0.25f;


            for (int i = 0; i < dt.Rows.Count; i++)
            {


                PdfPCell c1 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["UserName"], fnt));
                c1.BorderWidth = 0.5f;
                c1.HorizontalAlignment = Cell.ALIGN_LEFT;
                c1.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c1.Padding = 1;
                datatable.AddCell(c1);

                PdfPCell c2 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["DepartmentName"], fnt));
                c2.BorderWidth = 0.5f;
                c2.HorizontalAlignment = Cell.ALIGN_LEFT;
                c2.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c2.Padding = 4;
                datatable.AddCell(c2);

                PdfPCell c3 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["RoleName"], fnt));
                c3.BorderWidth = 0.5f;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c3.Padding = 4;
                datatable.AddCell(c3);

                PdfPCell c4 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["LoginID"], fnt));
                c4.BorderWidth = 0.5f;
                c4.HorizontalAlignment = Cell.ALIGN_LEFT;
                c4.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c4.Padding = 4;
                datatable.AddCell(c4);


                PdfPCell c6 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["UserStatus"], fnt));
                c6.BorderWidth = 0.5f;
                c6.HorizontalAlignment = Cell.ALIGN_LEFT;
                c6.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c6.Padding = 4;
                datatable.AddCell(c6);

                PdfPCell c7 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["CreateDate"], fnt));
                c7.BorderWidth = 0.5f;
                c7.HorizontalAlignment = Cell.ALIGN_LEFT;
                c7.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c7.Padding = 4;
                datatable.AddCell(c7);

                PdfPCell c8 = new PdfPCell(new iTextSharp.text.Phrase((string)dt.Rows[i]["LastLogin"], fnt));
                c8.BorderWidth = 0.5f;
                c8.HorizontalAlignment = Cell.ALIGN_LEFT;
                c8.BorderColor = new iTextSharp.text.Color(200, 200, 200);
                c8.Padding = 4;
                datatable.AddCell(c8);

            }

           


            /////////////////////////////////
            document.Add(datatable);

            document.Close();
            Response.End();

        }

        protected void btnDelUser_Click(object sender, EventArgs e)
        {
            UserDB userDB = new UserDB();

            for (int i = 0; i < MyDataGrid2.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)MyDataGrid2.Items[i].FindControl("chkUser");

                if (cbx.Checked)
                {
                    int UserID = (int)MyDataGrid2.DataKeys[i];
                    userDB.DeleteUsers(UserID);

                    HtmlInputHidden hdnID = MyDataGrid2.Items[i].FindControl("hdnRoleId") as HtmlInputHidden;
                    int roleId = Convert.ToInt32(hdnID.Value);

                    UserHistoryDB userHistoryDB = new UserHistoryDB();
                    userHistoryDB.InsertUserHistory(ParseData.StringToInt(Request.Cookies["UserID"].Value),
                                GetIPAddress()
                                                    , UserHistoryVar.USER_INFORMATION_UPDATED
                                                    , UserID
                                                    , roleId
                                                    , 0
                                                    , ""
                                                    , 0 
                                                    , ""
                                                    , ""
                                                    , ""
                                                    , UserHistoryVar.STATUS_DELETE);
                    lblMsg.Text = "Information Updated";


                }
            }
        }

        private string GetIPAddress()
        {
            string ipaddress;
            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];

            return ipaddress;
        }
    }
}

