<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ExportToCBS.aspx.cs" Inherits="EFTN.ExportToCBS" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Export to CBS</title>
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
                Export to CBS</div>
            <div style="overflow: scroll">
                <table>
                    <tr>
                        <td width="100">
                        </td>
                        <td class="NormalBold">
                            TransactionSent</td>
                        <td width="30">
                        </td>
                        <td class="Normal">
                            <asp:LinkButton ID="btnGenerateFlatFileTransactionSent" runat="server" Text="Generate"
                                CssClass="CommandButton" OnClick="btnGenerateFlatFileTransactionSent_Click" OnClientClick="return confirm('Are you sure you want to generate flat files for CBS?')" /></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="3">
                            <asp:DataGrid ID="dtgTransactionSentCBS" runat="Server" Width="600" BorderWidth="0px"
                                GridLines="None" AutoGenerateColumns="True" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader" ItemStyle-BackColor="#dee9fc"
                                ItemStyle-CssClass="NormalSmall" AlternatingItemStyle-BackColor="#ffffff">
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr height="25">
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td width="100">
                        </td>
                        <td class="NormalBold">
                            Return Sent</td>
                        <td width="30">
                        </td>
                        <td class="Normal">
                            <asp:LinkButton ID="btnGenerateFlatFileReturnSent" runat="server" Text="Generate"
                                CssClass="CommandButton" OnClick="btnGenerateFlatFileTransactionSent_Click" OnClientClick="return confirm('Are you sure you want to generate flat files for CBS?')" /></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="3">
                            <asp:DataGrid ID="dtgReturnSentCBS" runat="Server" Width="600" BorderWidth="0px"
                                GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1"
                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader"
                                ItemStyle-BackColor="#dee9fc" ItemStyle-CssClass="NormalSmall" AlternatingItemStyle-BackColor="#ffffff"
                                DataKeyField="TraceNumber">
                                <Columns>
                                    <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" HeaderStyle-Wrap="true" />
                                    <asp:BoundColumn DataField="DFIAccountNo" HeaderText="AccountNo" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="Amount" DataFormatString="{0:N}" HeaderText="Amount" ItemStyle-Wrap="true" HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="ReturnCode" HeaderText="ReturnCode" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr height="25">
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td width="100">
                        </td>
                        <td class="NormalBold">
                            To send transaction to CBS</td>
                        <td width="30">
                        </td>
                        <td class="Normal">
                            <asp:LinkButton ID="lnkBtnGenerateFlatFileTransactionSentToOwnBank" runat="server"
                                Text="Generate" CssClass="CommandButton" OnClick="lnkBtnGenerateFlatFileTransactionSentToOwnBank_Click"
                                OnClientClick="return confirm('Are you sure you want to generate flat files for CBS?')" /></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="3">
                            <asp:DataGrid ID="dtgTransactionSentToOwnBank" runat="Server" Width="600" BorderWidth="0px"
                                GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1"
                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader"
                                ItemStyle-BackColor="#dee9fc" ItemStyle-CssClass="NormalSmall" AlternatingItemStyle-BackColor="#ffffff"
                                DataKeyField="TraceNumber">
                                <Columns>
                                    <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="DFIAccountNo" HeaderText="DFIAccountNo" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="AccountNo" HeaderText="AccountNo" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="ReceivingBankRoutingNo" HeaderText="ReceivingBankRoutingNo"
                                        ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="Amount" HeaderText="Amount" DataFormatString="{0:N}" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="IdNumber" HeaderText="IdNumber" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="ReceiverName" HeaderText="ReceiverName" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="PaymentInfo" HeaderText="PaymentInfo" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="ReceivingCompanyID" HeaderText="ReceivingCompanyID" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
