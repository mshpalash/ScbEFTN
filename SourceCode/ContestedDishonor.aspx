<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ContestedDishonor.aspx.cs"
    Inherits="EFTN.ContestedDishonor" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Inward Dishonor</title>
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
                Contested Dishonor</div>
            <div style="overflow:scroll">
                            <table>
                                <tr>
                                    <td colspan="3">
                                        <asp:DataGrid ID="dtgContestedDishonorList" AlternatingItemStyle-BackColor="lightyellow"
                                            AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                            FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                            Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                            DataKeyField="EDRID">
                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkBoxContestedDishonor" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="DishonoredID" HeaderText="DishonoredID" ItemStyle-Wrap="False"
                                                    Visible="false" HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="ContestedID" HeaderText="ContestedID" ItemStyle-Wrap="False"
                                                    Visible="false" HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="OrgTraceNumber" HeaderText="OrgTraceNumber" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="TransactionCode" HeaderText="TransactionCode" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="DFIAccountNo" HeaderText="DFIAccountNo" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="BankName" HeaderText="BankName" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="SendingBankRoutNo" HeaderText="SendingBankRoutNo" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="Amount" HeaderText="Amount" DataFormatString="{0:N}" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="IdNumber" HeaderText="IdNumber" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="ReceiverName" HeaderText="ReceiverName" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="PaymentInfo" HeaderText="PaymentInfo" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="ReturnTraceNumber" HeaderText="ReturnTraceNumber" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="DishonorReason" HeaderText="DishonorReason" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="ReturnCode" HeaderText="ReturnCode" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="DishonorTraceNumber" HeaderText="DishonorTraceNumber"
                                                    ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="ContestedDishonoredCode" HeaderText="ContestedDishonoredCode"
                                                    ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" ItemStyle-Wrap="False"
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
                                    <td class="NormalBold">
                                        Info :
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 21px">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="CommandButton"
                                                        OnClick="btnApprove_Click" OnClientClick="return confirm('Are you sure you want to approve?')" />
                                                </td>
                                                <td width="20px">
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="CommandButton"
                                                        OnClick="btnReject_Click" OnClientClick="return confirm('Are you sure you want to reject?')" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>                
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
