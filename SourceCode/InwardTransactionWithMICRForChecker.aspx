<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InwardTransactionWithMICRForChecker.aspx.cs"
    Inherits="EFTN.InwardTransactionWithMICRForChecker" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Approved Inward Transaction</title>
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
                <a class="CommandButton" href="InwardTransactionApprovedChecker.aspx">Inward transactions</a>
            </div>
            <div>
                <table style="vertical-align: top">
                    <tr>
                        <td>
                            <table class="LightBorderTable" style="background: lightCyan; width: 160px">
                            </table>
                        </td>
                        <asp:DataGrid ID="dtgMicrInfo" runat="server" DataKeyField="ACCOUNT" AutoGenerateColumns="false"
                            ShowHeader="false">
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <table class="LightBorderTable" style="background: lightCyan; width: 160px">
                                            <tr>
                                                <td>
                                                    TraceNumber:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "TraceNumber")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    TransactionCode:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "TransactionCode")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    DFIAccountNo:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    BankName:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    SendingBankRoutNo:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "SendingBankRoutNo")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Amount:
                                                </td>
                                                <td>
                                                    <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    IdNumber:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "IdNumber")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    ReceiverName:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    PaymentInfo:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "PaymentInfo")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    ServiceClassCode:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "ServiceClassCode")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    SECC:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "SECC")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    BatchNumber:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "BatchNumber")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    EffectiveEntryDate:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "EffectiveEntryDate")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    CompanyId:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "CompanyId")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    CompanyName:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "CompanyName")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    EntryDesc:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "EntryDesc")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    SettlementJDate:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "SettlementJDate")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <table class="LightBorderTable" style="background: lightyellow; width: 160px">
                                            <tr>
                                                <td>
                                                    MASTER:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "MASTER")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    ACCOUNT:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "ACCOUNT")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    CCY:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "CCY")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    TITLE:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "TITLE")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    PRODUCT:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "PRODUCT")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    SEGMENT:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "SEGMENT")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    STATUS:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "STATUS")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    RISKS:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "RISKS")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </tr>
                </table>                
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div>
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnAccept" runat="server" Text="Accept" CssClass="CommandButton"
                                            OnClientClick="return confirm('Are you sure you want to accept?')" OnClick="btnAccept_Click"></asp:LinkButton></td>
                                    <td width="50">
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="CommandButton"
                                            OnClientClick="return confirm('Are you sure you want to reject?')" OnClick="btnReject_Click"></asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <asp:Label ID="lblRejectInstruction" runat="server" CssClass="NormalBold" Text="For a rejection item(s) please give a reason"></asp:Label></td>
                        <td colspan="2" width="60%">
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <asp:TextBox ID="txtRejectedReason" TextMode="MultiLine" runat="server" Width="300" MaxLength="50" OnKeyUp="return makeUppercase(this.name);"></asp:TextBox></td>
                        <td colspan="2" width="60%">
                            <asp:Label ID="lblNoReturnReason" runat="server" Text="Please Enter a return reason"
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
