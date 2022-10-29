<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CityChargeAccountsManagement.aspx.cs" Inherits="FloraSoft.CityChargeAccountsManagement" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="modules/Header.ascx" TagName="AdminHeader" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora EFTN System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script type="text/javascript" language="javascript">

    javascript:window.history.forward(1);
    
    function onlyNumbers(evt) {
        var e = event || evt;
        var charCode = e.which || e.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }
    
    </script>    
</head>
<body class="wrap" id="content" >
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:AdminHeader ID="AdminHeader" runat="server" />
        <div class="Head" align="center">
            Account's Charge Management</div>
        <div>
        <div style="width:900px" align="center">
            <table>
                <tr>
                    <td align="center">
                        <a href="EFTChargeManagementCity.aspx" class="CommandButton"> Charge Management</a>
                    </td>
                </tr>
            </table>
        </div>        
        <div style="width: 900px" align="center">
            <asp:Panel ID="pnlCityChargeCodeInfo" runat="server">
                        <asp:DataGrid ID="dataGridChargeCodeInfo" HeaderStyle-CssClass="GrayBackWhiteFont" FooterStyle-CssClass="GrayBackWhiteFont"
                            ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#FFFFFF"
                            runat="server" CellSpacing="1" CellPadding="5" AutoGenerateColumns="true"
                            GridLines="None" BorderWidth="0px" ShowFooter="true" Height="0px">
                        </asp:DataGrid>
                
            </asp:Panel>
        </div>
                    <div style="overflow: scroll; height: 450px; width: 900px" align="center">
                        <br /><br />
                        <asp:DataGrid ID="MyDataGrid" HeaderStyle-CssClass="GrayBackWhiteFont" FooterStyle-CssClass="GrayBackWhiteFont"
                            ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#FFFFFF"
                            runat="server" CellSpacing="1" CellPadding="5" AutoGenerateColumns="False" DataKeyField="CityChargeAccID"
                            GridLines="None" BorderWidth="0px" ShowFooter="True" Height="0px" 
                            OnItemCommand="MyDataGrid_ItemCommand">
                            <Columns>
                                <asp:EditCommandColumn CausesValidation="False" EditText="Edit" ItemStyle-Width="30px" UpdateText="Update"
                                    CancelText="Cancel">
                                    <FooterStyle CssClass="red"></FooterStyle>
                                    <ItemStyle CssClass="CommandButton" />
                                </asp:EditCommandColumn>
                                <asp:TemplateColumn HeaderText="AccountNo">
                                    <EditItemTemplate>
                                         <asp:TextBox ID="AccountNo" Width="100px"  runat="server"  onkeypress="return onlyNumbers();"
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "AccountNo") %>'></asp:TextBox>
                                    </EditItemTemplate>                                    
                                    <FooterTemplate>
                                        <asp:TextBox ID="addAccountNo" runat="Server" onkeypress="return onlyNumbers();"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddAccountNo" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addAccountNo" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "AccountNo")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CityChargeCode" ItemStyle-HorizontalAlign="Left"  FooterStyle-CssClass="red" >
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "CityChargeCode")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddListChargeCode" DataSource="<%#GetChargeCodeInfo() %>"
                                        DataTextField="CityChargeCode" DataValueField="CityChargeCode" runat="server">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                   <FooterTemplate>
                                        <asp:DropDownList ID="addCityChargeCode"  DataSource="<%#GetChargeCodeInfo() %>" 
                                        DataTextField="CityChargeCode" DataValueField="CityChargeCode" runat="server">
                                        </asp:DropDownList>
                                    </FooterTemplate>

                                </asp:TemplateColumn>                    
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Button ID="btnDel" CssClass="inputlt" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?')" />
                                    </ItemTemplate>
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
        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
