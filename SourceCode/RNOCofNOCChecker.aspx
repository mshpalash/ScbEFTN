<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RNOCofNOCChecker.aspx.cs" Inherits="EFTN.RNOCofNOCChecker" %>

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
<body class="wrap" id="content">

    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header1" runat="server" />
        <div class="Head" align="center">
            RNOC of Received NOC Checker</div>
        <div style="overflow:scroll">
            <table>
                <tr>
                    <td colspan="3">
                        <asp:DataGrid ID="dtgEFTRNOCOfReceivedNOC" runat="Server" Height="0px" 
                                BorderWidth="0px" GridLines="None"
                                AutoGenerateColumns="False"  CellPadding="5" 
                                CellSpacing="1" 
                                ItemStyle-BackColor="#dee9fc"
                                AlternatingItemStyle-BackColor="#ffffff"
                                ItemStyle-CssClass="Normal"
                                FooterStyle-CssClass="GrayBackWhiteFont"
                                HeaderStyle-CssClass="GrayBackWhiteFont" DataKeyField="DishonoredID">
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBoxRNOCofNOC" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ReturnTraceNumber" HeaderText="ReturnTraceNumber" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="true" />
                                <asp:BoundColumn DataField="ChangeCode" HeaderText="ChangeCode" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="CorrectedData" HeaderText="CorrectedData" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="DishonorReason" HeaderText="DishonorReason" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="AddendaInfo" HeaderText="AddendaInfo" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="OrgTraceNumber" HeaderText="OrgTraceNumber" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="TransactionCode" HeaderText="TransactionCode" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="DFIAccountNo" HeaderText="DFIAccountNo" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="BankName" HeaderText="BankName" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="ReceivingBankRoutingNo" HeaderText="ReceivingBankRoutingNo" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="Amount" HeaderText="Amount" DataFormatString="{0:N}" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="IdNumber" HeaderText="IdNumber" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="ReceiverName" HeaderText="ReceiverName" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="PaymentInfo" HeaderText="PaymentInfo" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="ServiceClassCode" HeaderText="ServiceClassCode" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="SECC" HeaderText="SECC" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNumber" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="EffectiveEntryDate" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="CompanyId" HeaderText="CompanyId" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="CompanyName" HeaderText="CompanyName" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="EntryDesc" HeaderText="EntryDesc" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                            </Columns>
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
                        <asp:TextBox ID="txtRejectReason" runat="server" OnKeyUp="return makeUppercase(this.name);"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
