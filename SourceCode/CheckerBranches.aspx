<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CheckerBranches.aspx.cs"
    Inherits="FloraSoft.CheckerBranches" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="modules/AdminChecker.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Checker Page</title>
    <link href="includes/sitec.css" rel="stylesheet" type="text/css" />
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header" runat="server" />
            <div class="Head" align="center">
                Admin Checker Branch Management</div>
            <div align="center" class="boxmodule" style="width: 540px; margin-left: 200px; padding-top: 15px;
                padding-left: 15px; min-height: 100px; margin-bottom: 15px; overflow:auto; margin-top: 20px; height: 410px">
                <asp:DropDownList ID="BankList" DataTextField="BankName" DataValueField="BankID"
                    AutoPostBack="true" runat="Server" OnSelectedIndexChanged="BankList_SelectedIndexChanged" />
                <asp:Label ID="lblBankCode" runat="server"></asp:Label>
                <br /><br />
                <table>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbxAll" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAll_CheckedChanged"/>                                    
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                            <asp:Button ID="btnActive" runat="server" Text="ACTIVE" OnClick="btnActive_Click" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                            <asp:Button ID="btnInactive" runat="server" Text="INACTIVE" OnClick="btnInactive_Click" />                        
                        </td>
                    </tr>
                </table>
                <br /><br />
                <asp:DataGrid ID="MyDataGrid" HeaderStyle-CssClass="GrayBackWhiteFont" FooterStyle-CssClass="GrayBackWhiteFont"
                    ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#FFFFFF"
                    runat="server" CellSpacing="1" CellPadding="5" AutoGenerateColumns="false" DataKeyField="BranchID"
                    GridLines="None" BorderWidth="0px" ShowFooter="true" Height="0px" OnItemCommand="MyDataGrid_ItemCommand">
                    <Columns>
                        <asp:TemplateColumn HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbxCheck" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>                        
                        <asp:TemplateColumn HeaderText="Branch Name" FooterStyle-CssClass="red">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Routing No" FooterStyle-CssClass="red">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "RoutingNo")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Branch Status" FooterStyle-CssClass="red">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BranchStatus")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
