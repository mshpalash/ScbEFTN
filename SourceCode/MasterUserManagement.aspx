<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterUserManagement.aspx.cs" Inherits="FloraSoft.MasterUserManagement" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="modules/SuperAdminHeader.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Management</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]--> 
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header" runat="server" />
        <div class="Head" align="center">User Management</div>
        <div align="center" class="boxmodule" style="width:940px;margin-left:20px; padding-top:15px; padding-left:15px; min-height:400px; overflow: scroll; margin-bottom:15px; margin-top:20px">
        <table>
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
                    <asp:Button ID="btnExportToPDF" runat="server" Text="PDF" OnClick="btnExportToPDF_Click" CausesValidation="false"/>
                </td>
                <td>
                    <asp:Button ID="btnDelUser" runat="server" Text="Delete" CausesValidation="false"
                        onclick="btnDelUser_Click" />
                </td>
                <td>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
            <asp:datagrid Id="MyDataGrid2"
                HeaderStyle-CssClass="GrayBackWhiteFont" 
                FooterStyle-CssClass="GrayBackWhiteFont" 
                ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc"   
                AlternatingItemStyle-BackColor="#FFFFFF" 
                runat="server" CellSpacing="1"  CellPadding="5" 
                autogeneratecolumns="false"  DataKeyField="UserID" 
                gridlines="None" borderwidth="0px" ShowFooter="true"  Height="0px" 
                OnItemCommand="MyDataGrid2_ItemCommand" >
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkUser" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:EditCommandColumn CausesValidation="False" EditText="Edit" UpdateText="Update" CancelText="Cancel"></asp:EditCommandColumn>
                    <asp:TemplateColumn HeaderText="Branch" ItemStyle-HorizontalAlign="Left"  FooterStyle-CssClass="red" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
                         </ItemTemplate>
                         <EditItemTemplate>
                            <asp:DropDownList ID="updateBranch" DataSource="<%#GetBranchListByBankID() %>" Width="150" runat="server"
                               DataTextField="BranchName" DataValueField="BranchID">
                            </asp:DropDownList>
                         </EditItemTemplate>
                         <FooterTemplate>
                            <asp:DropDownList ID="addBranchName" DataSource="<%#GetBranchListByBankID() %>" DataTextField="BranchName"
                                DataValueField="BranchID" runat="server">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateColumn> 
                    <asp:TemplateColumn HeaderText="Role" ItemStyle-HorizontalAlign="Left"  FooterStyle-CssClass="red" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "RoleName")%>
                               <input type="hidden" ID="hdnRoleId" value='<%# DataBinder.Eval(Container.DataItem, "RoleID") %>' runat="server"/>                                
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="Role" Width="150" runat="server" SelectedValue='<%#DataBinder.Eval(Container.DataItem,"RoleID")%>'
                                DataSource="<%#ACH_GetRoles() %>" DataTextField="RoleName" DataValueField="RoleID">
                            </asp:DropDownList>
                        </EditItemTemplate>
                       <FooterTemplate>
                            <asp:DropDownList ID="addRoleName" DataSource="<%#ACH_GetRoles() %>" DataTextField="RoleName"
                                DataValueField="RoleID" runat="server">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateColumn>            
                    <ASP:TemplateColumn HeaderText="Login ID" FooterStyle-CssClass="red"  ItemStyle-HorizontalAlign="Left"
                           HeaderStyle-Width="60px" FooterStyle-Width="60px" ItemStyle-Width="60px" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "LoginID")%>
                         </ItemTemplate>
                         <EditItemTemplate>
                             <asp:TextBox ID="LoginID" Width="60"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LoginID") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                                <asp:TextBox ID="addLoginID"  Width="60"  Runat="Server" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatoraddLoginID" CssClass="NormalRed" runat="server" ControlToValidate="addLoginID"  ErrorMessage="*" Display="dynamic"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </ASP:TemplateColumn>                  
                    <ASP:TemplateColumn HeaderText="Password" FooterStyle-CssClass="red"  HeaderStyle-Width="60px" FooterStyle-Width="60px" ItemStyle-Width="60px">
                         <FooterTemplate>
                                <asp:TextBox ID="addPassword" TextMode="Password" Width="60" Runat="Server" />
                                <asp:RequiredFieldValidator  id="RequiredFieldValidatoraddPassword"  CssClass="NormalRed" runat="server"  ControlToValidate="addPassword" ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </ASP:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Password" ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red">
                        <ItemTemplate>
                            <a href="ResetMasterPassword.aspx?ResetUserID=<%#DataBinder.Eval(Container.DataItem, "UserID")%>">Reset Password</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>                    
                    <ASP:TemplateColumn HeaderText="Name" HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red" HeaderStyle-Width="60px" FooterStyle-Width="120px" ItemStyle-Width="120px" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "UserName")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="UserName" runat="server" Width="120px" 
                             Text='<%#DataBinder.Eval(Container.DataItem,"UserName") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addUserName" Width="120"  Runat="Server" />
                                <asp:RequiredFieldValidator 
                                id="RequiredFieldValidatoraddUserName" 
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addUserName"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </ASP:TemplateColumn>
                    <ASP:TemplateColumn HeaderText="Status"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "UserStatus")%>
                         </ItemTemplate>
                    </ASP:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Department" ItemStyle-HorizontalAlign="Left"  FooterStyle-CssClass="red" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "DepartmentName")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="Department" Width="150" runat="server" SelectedValue='<%#DataBinder.Eval(Container.DataItem,"DepartmentID")%>'
                                DataSource="<%#GetDepartments() %>" DataTextField="DepartmentName" DataValueField="DepartmentID">
                            </asp:DropDownList>
                        </EditItemTemplate>
                       <FooterTemplate>
                            <asp:DropDownList ID="addDepartment" DataSource="<%#GetDepartments() %>" DataTextField="DepartmentName"
                                DataValueField="DepartmentID" runat="server">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateColumn>                    
                   <%-- <ASP:TemplateColumn HeaderText="Department"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Department")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="Department" Width="60" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Department") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addDepartment" Width="60"  Runat="Server" />
                                <asp:RequiredFieldValidator 
                                id="RequiredFieldValidatoraddDepartment" 
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addDepartment"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </ASP:TemplateColumn>--%>
                    <ASP:TemplateColumn HeaderText="Contact No."  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "ContactNo")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="ContactNo" Width="90" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ContactNo") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addContactNo" Width="90"  Runat="Server" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatoraddContactNo" 
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addContactNo"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </ASP:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Is Generic"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "IsGeneric")%>
                         </ItemTemplate>
                       <%-- <EditItemTemplate>
                            <asp:CheckBox ID="chkBoxIsGeneric" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem, "IsGeneric") %>' />
                        </EditItemTemplate>--%>
                         <FooterTemplate>
                                <asp:CheckBox ID="chkBoxIsGeneric" runat="server"/>
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                     <FooterTemplate>
                            <asp:linkButton CommandName="Insert" Text="Add" ID="btnAdd" ForeColor="white"  Runat="server" />
                    </FooterTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:datagrid>
        </div>
        <div>
            <asp:Label runat="server" ID="lblTestCookie"></asp:Label>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
