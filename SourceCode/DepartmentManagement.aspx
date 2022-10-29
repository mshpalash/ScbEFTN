<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentManagement.aspx.cs" Inherits="EFTN.DepartmentManagement" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>

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
        <div class="Head" align="center">Department Management</div>
        <div align="center" class="boxmodule" style="width:940px; height:500px; margin-left:20px; padding-top:15px; padding-left:15px; min-height:400px; overflow: auto; margin-bottom:15px; margin-top:20px">
            <asp:datagrid Id="MyDataGrid2"
                HeaderStyle-CssClass="GrayBackWhiteFont" 
                FooterStyle-CssClass="GrayBackWhiteFont" 
                ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc"   
                AlternatingItemStyle-BackColor="#FFFFFF" 
                runat="server" CellSpacing="1"  CellPadding="5" 
                autogeneratecolumns="false"  DataKeyField="DepartmentID" 
                gridlines="None" borderwidth="0px" ShowFooter="true"  Height="0px" 
                OnItemCommand="MyDataGrid2_ItemCommand" >
                <Columns>
                <asp:EditCommandColumn CausesValidation="False" EditText="Edit" UpdateText="Update" CancelText="Cancel"></asp:EditCommandColumn>
                    <asp:TemplateColumn HeaderText="Department Name" HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red" HeaderStyle-Width="60px" FooterStyle-Width="120px" ItemStyle-Width="120px" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "DepartmentName")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="txtDepartmentName" runat="server" Width="120px" 
                             Text='<%#DataBinder.Eval(Container.DataItem,"DepartmentName") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addDepartmentName" Width="120"  Runat="Server" />
                                <asp:RequiredFieldValidator 
                                id="ReqFieldValidatorAddDepartmentName" 
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addDepartmentName"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Parking Account In"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "ParkingAccountIn")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="txtParkingAccountIn" Width="90" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ParkingAccountIn") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addParkingAccountIn" Width="90"  Runat="Server" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatoraddParkingAccountIn" 
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addParkingAccountIn"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Parking Account Out"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "ParkingAccountOut")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="txtParkingAccountOut" Width="90" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ParkingAccountOut") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addParkingAccountOut" Width="90"  Runat="Server" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatoraddParkingAccountOut" 
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addParkingAccountOut"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
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
