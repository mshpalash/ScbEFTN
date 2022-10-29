<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InwardDishonor.aspx.cs"
    Inherits="EFTN.InwardDishonor" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Inward Dishonor</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">
        javascript: window.history.forward(1);
    </script>

</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Inward Dishonor
            </div>
            <div style="overflow: scroll">
                <table>
                    <tr>
                        <td width="100"></td>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="3">
                                        <asp:DataGrid ID="dtgInwardDishonorList" AlternatingItemStyle-BackColor="lightyellow"
                                            AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                            FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                            Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                            DataKeyField="DishonoredID">
                                            <Columns>
                                                <asp:BoundColumn DataField="AddendaInfo" HeaderText="AddendaInfo" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="ReturnTraceNumber" HeaderText="ReturnTraceNumber" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="Currency" HeaderText="Currency" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="SessionID" HeaderText="Session" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="StatusID" HeaderText="StatusID" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="DishonorIdentity" HeaderText="DishonorIdentity" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:TemplateColumn HeaderText="DateOfDeath">
                                                    <ItemTemplate>
                                                        <a href="InwardDishonor.aspx?ReturnTraceNumber=<%#DataBinder.Eval(Container.DataItem, "ReturnTraceNumber")%>">Check</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Info :
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:DataGrid ID="dtgMatchedSentEDR" AlternatingItemStyle-BackColor="lightyellow"
                                            AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                            FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                            Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                            DataKeyField="EDRID">
                                            <Columns>
                                                <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="TransactionCode" HeaderText="TransactionCode" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="DFIAccountNo" HeaderText="DFIAccountNo" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="BankName" HeaderText="BankName" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="SendingBankRoutNo" HeaderText="SendingBankRoutNo" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="Currency" HeaderText="Currency" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="SessionID" HeaderText="Session" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="Amount" DataFormatString="{0:N}" HeaderText="Amount" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="IdNumber" HeaderText="IdNumber" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="ReceiverName" HeaderText="ReceiverName" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="PaymentInfo" HeaderText="PaymentInfo" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="ReturnTraceNumber" HeaderText="ReturnTraceNumber" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                                <asp:BoundColumn DataField="ReturnCode" HeaderText="ReturnCode" ItemStyle-Wrap="False"
                                                    HeaderStyle-Wrap="False" />
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="NormalBold" nowrap>Contested Reasons:
                                        <asp:DropDownList ID="ddlContested" runat="server" DataTextField="RejectReason" DataValueField="RejectReasonCode" />
                                    </td>
                                    <td class="NormalBold"></td>
                                </tr>
                                <tr>
                                    <td style="height: 21px">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="CommandButton"
                                                        OnClick="btnApprove_Click" OnClientClick="return confirm('Are you sure you want to approve?')" />
                                                </td>
                                                <td width="20px"></td>
                                                <td>
                                                    <asp:LinkButton ID="btnContested" runat="server" Text="Contested" CssClass="CommandButton"
                                                        OnClick="btnContested_Click" OnClientClick="return confirm('Are you sure you want to contest?')" />
                                                </td>
                                            </tr>
                                        </table>
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
