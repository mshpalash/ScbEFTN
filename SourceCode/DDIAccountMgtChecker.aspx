<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DDIAccountMgtChecker.aspx.cs" Inherits="EFTN.DDIAccountMgtChecker" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>DDI Account Management</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]--> 
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header" runat="server" />
        <div class="Head" align="center">DDI Account Status Management</div>
            <div align="left" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px;
                height: 100px; margin-left: 15px">
                <table>
                    <tr>
                        <td width="10">
                        </td>
                        <td>
                            <asp:CheckBox ID="cbxAll" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" oncheckedchanged="cbxAll_CheckedChanged"/>
                        </td>
                        <td width="15px">                        
                        </td>
                        <td>
                            <asp:Button ID="btnActive" runat="server" Text="Active" 
                                onclick="btnActive_Click" />
                        </td>
                        <td width="15px">
                        </td>
                        <td>
                            <asp:Button ID="btnInactive" runat="server" Text="Inactive" 
                                onclick="btnInactive_Click" />
                        </td>
                       <td width="15px">
                        </td>                        
                    </tr>
                </table>
                <asp:Label runat="server" ID="txtMsg"></asp:Label>
            </div>        
        <div align="center" class="boxmodule" style="width:940px; height:500px; margin-left:20px; padding-top:15px; padding-left:15px; min-height:400px; overflow: auto; overflow: scroll; margin-bottom:15px; margin-top:20px">
            <asp:datagrid Id="MyDataGrid2"
                HeaderStyle-CssClass="GrayBackWhiteFont" 
                FooterStyle-CssClass="GrayBackWhiteFont" 
                ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc"   
                AlternatingItemStyle-BackColor="#FFFFFF" 
                runat="server" CellSpacing="1"  CellPadding="5" 
                autogeneratecolumns="false"  DataKeyField="AccountID" 
                gridlines="None" borderwidth="0px" ShowFooter="true"  Height="0px" 
                OnItemCommand="MyDataGrid2_ItemCommand" AllowPaging="true" PageSize="500" >
                <Columns>
                    <asp:TemplateColumn HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxCheck" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>                
                    <asp:TemplateColumn HeaderText="SCB Credit Account No."  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "AccountNo")%>
                         </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Other Bank Account No"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "OtherBankAcNo")%>
                         </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="RoutingNumber"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "RoutingNumber")%>
                         </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Expiry Date"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "ExpiryDate")%>
                         </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Account Status"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "AccountStatus")%>
                         </ItemTemplate>
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
