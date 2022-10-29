<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CityCardsAccountManagement.aspx.cs" Inherits="EFTN.CityCardsAccountManagement" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Cards Account Management</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]--> 
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header" runat="server" />
        <div align="center">
            <table>
                <tr height="10px">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="center"><a href="CityCardsManagement.aspx" class="CommandButton">Cards Management</a></td>
                </tr>
            </table>
        </div>
        <div class="Head" align="center">Cards Transactions Upload</div>
        <div>
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
                            Width="80" 
                            OnClientClick="return confirm('Are you sure you want to import this file?')" 
                            onclick="btnUploadExcel_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblTransactionCount" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblTransactionAmount" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" style="width:800px;">
            <asp:Label runat="server" ID="txtMsg" Font-Size="Large"></asp:Label>
        </div>
        <br />

                            <div style="overflow: scroll; height: 450px; width: 900px">
                    <asp:DataGrid ID="dtgXmlUpload" runat="Server" Width="600px" BorderWidth="0px"
                        GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                        FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont" HeaderStyle-ForeColor="#FFFFFF"
                        ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff"
                        DataKeyField="CityCardEDRID"  AllowPaging="True" PageSize="500">
                        <Columns>
                            <asp:TemplateColumn HeaderText="SenderAccNo" SortExpression = "SenderAccNo" >   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "SenderAccNo")%>
                                </ItemTemplate> 
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="DFIAccNo" SortExpression = "DFIAccNo" >   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "DFIAccNo")%>
                                </ItemTemplate> 
                            </asp:TemplateColumn>
                           
                            <asp:TemplateColumn HeaderText="Amount" SortExpression = "Amount">   
                                <ItemTemplate>
                                    <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                </ItemTemplate> 
                            </asp:TemplateColumn>
                                
                            <asp:TemplateColumn HeaderText="ReceiverName" SortExpression = "ReceiverName">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                </ItemTemplate> 
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="RoutingNo" SortExpression = "RoutingNo">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "RoutingNo")%>
                                </ItemTemplate> 
                            </asp:TemplateColumn>                            
                        </Columns>
                        <FooterStyle CssClass="GrayBackWhiteFont" />
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall" />
                        <HeaderStyle CssClass="GrayBackWhiteFont" ForeColor="White" />
                    </asp:DataGrid>
                    </div>

    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
