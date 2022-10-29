<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CityChargeDepartmentManagement.aspx.cs" Inherits="EFTN.CityChargeDepartmentManagement" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Departments Charge Management</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
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
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]--> 
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header" runat="server" />
        <div class="Head" align="center">Department's Charge Management</div>
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
            <asp:Panel ID="pnlCityChargeDepartmentInfo" runat="server">
                        <asp:DataGrid ID="dataGridChargeCodeInfo" HeaderStyle-CssClass="GrayBackWhiteFont" FooterStyle-CssClass="GrayBackWhiteFont"
                            ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#FFFFFF"
                            runat="server" CellSpacing="1" CellPadding="5" AutoGenerateColumns="true"
                            GridLines="None" BorderWidth="0px" ShowFooter="true" Height="0px">
                        </asp:DataGrid>
                
            </asp:Panel>
        </div>        
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
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CityBBChargeAcc" ItemStyle-HorizontalAlign="Left"  FooterStyle-CssClass="red" >
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "CityBBChargeAcc")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="addCityBBChargeAcc" MaxLength="17" runat="server" oncopy="return false" oncut="return false"
                                                    onkeypress="return onlyNumbers();" onpaste="return false" Text='<%#DataBinder.Eval(Container.DataItem,"CityBBChargeAcc") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CityBankChargeAcc" ItemStyle-HorizontalAlign="Left"  FooterStyle-CssClass="red" >
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "CityBankChargeAcc")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="addCityBankChargeAcc" MaxLength="17" runat="server" oncopy="return false" oncut="return false" onkeypress="return onlyNumbers();" onpaste="return false" Text='<%#DataBinder.Eval(Container.DataItem,"CityBankChargeAcc") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="CityBBChargeVATAcc" ItemStyle-HorizontalAlign="Left"  FooterStyle-CssClass="red" >
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "CityBBChargeVATAcc")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="addCityBBChargeVATAcc" MaxLength="17" runat="server" oncopy="return false" oncut="return false" onkeypress="return onlyNumbers();" onpaste="return false" Text='<%#DataBinder.Eval(Container.DataItem,"CityBBChargeVATAcc") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="CityBankChargeVATAcc" ItemStyle-HorizontalAlign="Left"  FooterStyle-CssClass="red" >
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "CityBankChargeVATAcc")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="addCityBankChargeVATAcc" MaxLength="17" runat="server" oncopy="return false" oncut="return false" onkeypress="return onlyNumbers();" onpaste="return false" Text='<%#DataBinder.Eval(Container.DataItem,"CityBankChargeVATAcc") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="CityChargeWaveAcc" ItemStyle-HorizontalAlign="Left"  FooterStyle-CssClass="red" >
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "CityChargeWaveAcc")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="addCityChargeWaveAcc" MaxLength="17" runat="server" oncopy="return false" oncut="return false" onkeypress="return onlyNumbers();" onpaste="return false" Text='<%#DataBinder.Eval(Container.DataItem,"CityChargeWaveAcc") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="CityVATWaveAcc" ItemStyle-HorizontalAlign="Left"  FooterStyle-CssClass="red" >
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "CityVATWaveAcc")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="addCityVATWaveAcc" MaxLength="17" runat="server" oncopy="return false" oncut="return false" onkeypress="return onlyNumbers();" onpaste="return false" Text='<%#DataBinder.Eval(Container.DataItem,"CityVATWaveAcc") %>'></asp:TextBox>
                                    </EditItemTemplate>
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
