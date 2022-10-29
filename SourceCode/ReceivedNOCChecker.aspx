<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ReceivedNOCChecker.aspx.cs"
    Inherits="EFTN.ReceivedNOCChecker" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script language="javascript" type="text/javascript">
    javascript:window.history.forward(1);
</script>
</head>
<body class="wrap" id="content">

<form id="form1" runat="server">
<div class="maincontent">
<uc1:Header ID="Header1" runat="server" />
        <div class="Head" align="center">
            Received NOC Checker</div>
            
            <br />                

                <div align="center" class="boxmodule" style="padding-top: 10px; width: 970px; margin-top: 10px;
                    height: 40px; margin-left: 5px"> 
                <table>
                    <tr align="left">
                        <td width="100">
                            <asp:DropDownList ID="ddListTransactionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddListTransactionType_SelectedIndexChanged">
                                <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                <asp:ListItem Value="Debit">Debit</asp:ListItem>
                            </asp:DropDownList>
                        </td>  
                        <td width="150px">
                            <asp:CheckBox ID="cbxAll" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAll_CheckedChanged"/>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                            <asp:Label ID="lblTotalItem" runat="server" CssClass="NormalBold">
                            </asp:Label>
                        </td>
                        <td width="50px">
                        </td>
                        <td>
                            <asp:Label ID="lblTotalAmount" runat="server" CssClass="NormalBold">
                            </asp:Label>
                        </td>         
                    </tr>
                </table>
                </div>
                            
            <div id="AvailDiv" runat="server" style="position: relative; overflow: auto; width: 950px;
                height: 380px;">
            <table>
                <tr>
                    <td colspan="3">
                        <asp:DataGrid ID="dtgEFTReceivedNOC" runat="Server" Height="0px" 
                                BorderWidth="0px" GridLines="None"
                                AutoGenerateColumns="False"  CellPadding="5" 
                                CellSpacing="1" 
                                ItemStyle-BackColor="#dee9fc"
                                AlternatingItemStyle-BackColor="#ffffff"
                                ItemStyle-CssClass="Normal"
                                FooterStyle-CssClass="GrayBackWhiteFont"
                                HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader"  DataKeyField="NOCID">
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBoxReceivedNOC" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="OrgTraceNumber" HeaderText="OrgTraceNumber" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="true" />
                                <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="CorrectedData" HeaderText="CorrectedData" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="ChangeCode" HeaderText="ChangeCode" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:TemplateColumn HeaderText="BankName" SortExpression = "BankName">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="BranchName" SortExpression = "BranchName">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                                
                                <asp:TemplateColumn HeaderText="OrgSettlementJDate" SortExpression = "OrgSettlementJDate">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "OrgSettlementJDate")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="SettlementJDate" SortExpression = "SettlementJDate">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "SettlementJDate")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="A/C No. From BEFTN" SortExpression = "DFIAccountNo">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="A/C No. From CBS" SortExpression = "ACCOUNT">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "ACCOUNT")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Amount" SortExpression = "Amount">
                                    <ItemTemplate>
                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Receiver Name" SortExpression = "ReceiverName">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ReceiverName as of CBS" SortExpression = "TITLE">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "TITLE")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="STATUS" SortExpression = "STATUS">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "STATUS")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="RISKS" SortExpression = "RISKS">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "RISKS")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="radioBtnChecker" runat="server" CssClass="NormalBold" RepeatDirection="Horizontal">
                            <asp:ListItem Value="Approve">Approve</asp:ListItem>
                            <asp:ListItem Value="RNOC">Refuse to Notification Of Change</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td valign="bottom">
                        <asp:DropDownList ID="ddListRNOC" runat="server" 
                            DataTextField="RejectReason" 
                            DataValueField="RejectReasonCode">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:LinkButton ID="btnSave" CssClass="CommandButton" Text="Save" runat="server" OnClick="btnSave_Click" OnClientClick="return confirm('Are you sure you want to save?')"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
</div>
    <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
