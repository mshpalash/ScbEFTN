<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SearchBranches.aspx.cs" Inherits="FloraSoft.SearchBranches" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="modules/Header.ascx" TagName="AdminHeader" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora EFTN System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body class="wrap" id="content" >
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:AdminHeader ID="AdminHeader" runat="server" />
        <div class="Head" align="center">
            Branch Management</div>
        <div>
                    <div style="overflow: scroll; height: 450px; width: 900px" align="center">
        <asp:DropDownList ID="BankList" DataTextField="BankName" DataValueField="BankID"
                            AutoPostBack="true" runat="Server" />
                        <asp:Label ID="lblBankCode" runat="server"></asp:Label>
                        <br /><br />
                        <asp:DataGrid ID="MyDataGrid" HeaderStyle-CssClass="GrayBackWhiteFont" FooterStyle-CssClass="GrayBackWhiteFont"
                            ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#FFFFFF"
                            runat="server" CellSpacing="1" CellPadding="5" AutoGenerateColumns="false" DataKeyField="BranchID"
                            GridLines="None" BorderWidth="0px" ShowFooter="true" Height="0px" OnItemCommand="MyDataGrid_ItemCommand">
                            <Columns>
                                <asp:EditCommandColumn CausesValidation="False" EditText="Edit" ItemStyle-Width="30px" UpdateText="Update"
                                    CancelText="Cancel">
                                    <FooterStyle CssClass="red"></FooterStyle>
                                    <ItemStyle CssClass="CommandButton" />
                                </asp:EditCommandColumn>
                                <asp:TemplateColumn HeaderText="Branch Name">
                                    <FooterTemplate>
                                        <asp:TextBox ID="addBranchName" runat="Server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddBranchName" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addBranchName" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="BranchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BranchName") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBranchName" CssClass="NormalRed"
                                            runat="server" ControlToValidate="BranchName" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <FooterStyle CssClass="red" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Routing No">
                                    <FooterTemplate>
                                        <asp:TextBox ID="addRoutingNo" Width="65" MaxLength="9" runat="Server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddRoutingNo" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addRoutingNo" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "RoutingNo")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="RoutingNo" Width="65" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RoutingNo") %>'
                                            MaxLength="9"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle CssClass="red" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Branch Status">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "BranchStatus")%>
                                    </ItemTemplate>
                                    <FooterStyle CssClass="red" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <FooterTemplate>
                                        <asp:LinkButton CommandName="Insert" Text="Add" ID="btnAdd" ForeColor="white" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <FooterStyle CssClass="GrayBackWhiteFont" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall" />
                            <HeaderStyle CssClass="GrayBackWhiteFont" />
                        </asp:DataGrid>
        </div>
            <table>
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td class="NormalBold">
                                    Please Select your Excel File to Upload<br />
                                    <asp:FileUpload CssClass="inputlt" ID="fulExcelFile" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnUploadExcel" runat="server" CssClass="inputlt" Text="Upload File"
                                        Width="80" OnClientClick="return confirm('Are you sure you want to import this file?')"
                                        OnClick="btnUploadExcel_Click" />
                                    <asp:Label ID="lblErrMsg" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
