<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovedNOCChekcer.aspx.cs" Inherits="EFTN.ApprovedNOCChekcer" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script language="javascript" type="text/javascript">
    javascript:window.history.forward(1);
    
    function makeUppercase(myControl, evt)
    {
        document.getElementById(myControl).value = document.getElementById(myControl).value.toUpperCase();
    }
</script>
</head>
<body class="wrap" id="content" >

    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header1" runat="server" />
        <div class="Head" align="center">
            Received NOC Checker</div>
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
                            AutoPostBack="true" OnCheckedChanged="cbxAll_CheckedChanged" />
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
             <div>
                <table>
                    <tr>

                    </tr>
                </table>
            </div>
        <div style="overflow:scroll">
            <table>
                <tr>
                    <td colspan="3">
                        <asp:DataGrid ID="dtgEFTApprovedOfReceivedNOC" runat="Server" Height="0px" 
                                BorderWidth="0px" GridLines="None"
                                AutoGenerateColumns="False"  CellPadding="5" 
                                CellSpacing="1" 
                                ItemStyle-BackColor="#dee9fc"
                                AlternatingItemStyle-BackColor="#ffffff"
                                ItemStyle-CssClass="Normal"
                                FooterStyle-CssClass="GrayBackWhiteFont"
                                HeaderStyle-CssClass="GrayBackWhiteFont" DataKeyField="NOCID" AllowPaging="True" OnPageIndexChanged="dtgEFTApprovedOfReceivedNOC_PageIndexChanged" PageSize="50">
                            <Columns>
                                <asp:TemplateColumn HeaderText="SL.">
                                    <ItemTemplate>
                                        <%#(dtgEFTApprovedOfReceivedNOC.PageSize * dtgEFTApprovedOfReceivedNOC.CurrentPageIndex) + Container.ItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBoxApproveOfNOC" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ReturnTraceNumber" HeaderText="ReturnTraceNumber" >
                                    <HeaderStyle Wrap="True" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ChangeCode" HeaderText="ChangeCode" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CorrectedData" HeaderText="CorrectedData" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OrgTraceNumber" HeaderText="OrgTraceNumber" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TransactionCode" HeaderText="TransactionCode" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DFIAccountNo" HeaderText="DFIAccountNo" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BankName" HeaderText="BankName" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ReceivingBankRoutingNo" HeaderText="ReceivingBankRoutingNo" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Amount" HeaderText="Amount" DataFormatString="{0:N}" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IdNumber" HeaderText="IdNumber" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ReceiverName" HeaderText="ReceiverName" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PaymentInfo" HeaderText="PaymentInfo" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ServiceClassCode" HeaderText="ServiceClassCode" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SECC" HeaderText="SECC" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNumber" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="EffectiveEntryDate" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CompanyId" HeaderText="CompanyId" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CompanyName" HeaderText="CompanyName" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="EntryDesc" HeaderText="EntryDesc" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                            <FooterStyle CssClass="GrayBackWhiteFont" />
                            <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Position="TopAndBottom" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#DEE9FC" CssClass="Normal" />
                            <HeaderStyle CssClass="GrayBackWhiteFont" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="400px">
                <tr>
                    <td align="left">
                        <asp:LinkButton ID="btnApprove" Text="Approve" CssClass="CommandButton" runat="server" OnClick="btnApprove_Click" OnClientClick="return confirm('Are you sure you want to approve?')"></asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="btnReject" Text="Reject" CssClass="CommandButton" runat="server" OnClick="btnReject_Click" OnClientClick="return confirm('Are you sure you want to reject?')"></asp:LinkButton>                        
                    </td>
                        
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Label ID="lblReason" runat="server" Text="Reject Reason :" CssClass="NormalBold"></asp:Label>
                        <asp:TextBox ID="txtRejectReason" TextMode="MultiLine" runat="server" OnKeyUp="return makeUppercase(this.name);"></asp:TextBox>
                    </td>
                </tr>
            </table>        
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
