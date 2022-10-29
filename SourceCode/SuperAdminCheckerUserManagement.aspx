<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SuperAdminCheckerUserManagement.aspx.cs"
    Inherits="FloraSoft.SuperAdminCheckerUserManagement" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="modules/SuperAdminCheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Management</title>
    <link href="includes/sitec.css" rel="stylesheet" type="text/css" />
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header" runat="server" />
            <div class="Head" align="center">
                User Management</div>
            <div align="center" class="boxmodule" style="width:940px;margin-left:20px; padding-top:15px; padding-left:15px; min-height:400px; margin-bottom:15px; margin-top:20px; overflow:auto">
<%--                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblUserStatus" Text="Select User Status" runat="server"></asp:Label>
                        </td>                    
                        <td>
                            <asp:DropDownList ID="ddListUserStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddListUserStatus_SelectedIndexChanged">
                                <asp:ListItem Value="All" Text="All" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="ACTIVE" Text="ACTIVE"></asp:ListItem>
                                <asp:ListItem Value="INACTIVE" Text="INACTIVE"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td> 
                            <asp:CheckBox ID="ckbIsPending" runat="server" Checked="true" Text="Is Pending" AutoPostBack="true" OnCheckedChanged="ckbIsPending_CheckedChanged"/>
                        </td>
                    </tr>
                </table>--%>

                <asp:DataGrid ID="MyDataGrid" HeaderStyle-CssClass="GrayBackWhiteFont" FooterStyle-CssClass="GrayBackWhiteFont"
                    ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#FFFFFF"
                    runat="server" CellSpacing="1" CellPadding="5" AutoGenerateColumns="false" DataKeyField="UserID"
                    GridLines="None" BorderWidth="0px" ShowFooter="true" Height="0px" OnItemCommand="MyDataGrid_ItemCommand">
                    <Columns>
                        <asp:EditCommandColumn CausesValidation="False" EditText="Edit" UpdateText="Update"
                            CancelText="Cancel"></asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Branch" ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Role" ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red">
                            <ItemTemplate>
                                <asp:TextBox ID="RoleName" runat="server" Width="120px" ReadOnly="true"
                                Text='<%#DataBinder.Eval(Container.DataItem, "RoleName")%>'></asp:TextBox>  
                               <input type="hidden" ID="hdnRoleId" value='<%# DataBinder.Eval(Container.DataItem, "RoleID") %>' runat="server"/>                                
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Login ID" FooterStyle-CssClass="red" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-Width="60px" FooterStyle-Width="60px" ItemStyle-Width="60px">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "LoginID")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Password" FooterStyle-CssClass="red" HeaderStyle-Width="60px"
                            FooterStyle-Width="60px" ItemStyle-Width="60px"></asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Name" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red" HeaderStyle-Width="60px"
                            FooterStyle-Width="120px" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:TextBox ID="UserName" runat="server" Width="120px"  ReadOnly="true"
                             Text='<%#DataBinder.Eval(Container.DataItem, "UserName")%>'></asp:TextBox>                                
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Status" ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlUserStatus" runat="server">
                                    <asp:ListItem Value="ACTIVE" Text="ACTIVE"></asp:ListItem>
                                    <asp:ListItem Value="INACTIVE" Text="INACTIVE" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "UserStatus")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="IsPending." ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "IsPending")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Department" ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Department")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Contact No." ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "ContactNo")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Is Generic" ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "IsGeneric")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
            <div>
                <asp:Label runat="server" ID="lblTestCookie"></asp:Label>
                 <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>    
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
