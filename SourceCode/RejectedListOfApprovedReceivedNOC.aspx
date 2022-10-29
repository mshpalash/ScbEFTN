<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RejectedListOfApprovedReceivedNOC.aspx.cs" Inherits="EFTN.RejectedListOfApprovedReceivedNOC" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
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
            (NOC) Rejected by checker which were approved my maker</div>
        <div style="overflow:scroll">
            <table>
                <tr>
                    <td colspan="3">
                        <asp:DataGrid ID="dtgEFTRejectedListOfApprovedReceivedNOC" runat="Server" Width="600" BorderWidth="0px"
                            GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1"
                            FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                            ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="NOCID">
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBoxRNOCofNOC" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ChangeCode" HeaderText="ChangeCode" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="CorrectedData" HeaderText="CorrectedData" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" ItemStyle-Wrap="False"
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
                                <asp:BoundColumn DataField="RejectReason" HeaderText="RejectReason" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblRefusedCORCode" CssClass="NormalBold" runat="server" Text="Refused COR Code:" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRefusedCORCode" runat="server" DataTextField="RejectReason"
                            DataValueField="RejectReasonCode">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblCorrectedDataMsg" CssClass="NormalBold" runat="server" Text="Corrected Data:" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtCorrectedData" runat="server" CssClass="NormalBold" OnKeyUp="return makeUppercase(this.name);" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="CommandButton" OnClick="btnSave_Click" OnClientClick="return confirm('Are you sure you want to save?')"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
