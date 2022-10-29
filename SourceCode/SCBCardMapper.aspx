<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SCBCardMapper.aspx.cs" Inherits="EFTN.SCBCardMapper" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="~/modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>

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
        <div class="Head" align="center">Card Mapper</div>
        
        <div align="center" style="width:940px; height:350px; overflow:scroll; ">
            <asp:datagrid Id="MyDataGrid2"
                HeaderStyle-CssClass="GrayBackWhiteFont" 
                FooterStyle-CssClass="GrayBackWhiteFont" 
                ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc"   
                AlternatingItemStyle-BackColor="#FFFFFF" 
                runat="server" CellSpacing="1"  CellPadding="5" 
                autogeneratecolumns="false"  DataKeyField="OID" 
                gridlines="None" borderwidth="0px" ShowFooter="true"  Height="0px" 
                OnItemCommand="MyDataGrid2_ItemCommand" >
                <Columns>
                
                    <asp:TemplateColumn HeaderText="Bin Number"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "BitNumber")%>
                         </ItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addBitNumber" Width="90"  Runat="Server" MaxLength="9"/>
                                <asp:RequiredFieldValidator id="RequiredFieldValidatoraddBitNumber"
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addBitNumber"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:LinkButton CommandName="Delete" Text="Delete" ID="btnDel" ForeColor="Blue"  Runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to Delete The Bit Number?')"></asp:LinkButton>
                            </ItemTemplate>
                         <FooterTemplate>
                                <asp:linkButton CommandName="Insert" Text="Add" ID="btnAdd" ForeColor="white"  Runat="server" />
                        </FooterTemplate>
                    </asp:TemplateColumn>

                </Columns>
            </asp:datagrid>
        </div>
        <div align="center" style="width:800px;">
            <asp:Label runat="server" ID="txtMsg" Font-Size="Large"></asp:Label>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
