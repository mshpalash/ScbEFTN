<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EFTEditNOC.aspx.cs" Inherits="EFTN.EFTEditNOC" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit NOC</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Edit NOC
            </div>
            <div>
                <asp:Repeater ID="rptEditNOC" runat="server">
                    <ItemTemplate>
                        <table class="NormalBold">
                            <tr>
                                <td style="width: 20%">
                                    <asp:Label ID="lblRejectReason" runat="server" Text='<%#GetFieldName("RejectReason")%>'></asp:Label>:
                                </td>
                                <td style="width: 80%" colspan="5">
                                    <asp:TextBox ID="txtRejectReason" TextMode="MultiLine" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RejectReason")%>'
                                        ReadOnly="true" CssClass="NormalBold" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <asp:Label ID="lblTraceNumber" runat="server" Text='<%#GetFieldName("TraceNumber")%>'></asp:Label>:
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtTraceNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TraceNumber")%>'
                                        ReadOnly="true" CssClass="NormalBold" />
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 20%">
                                    <asp:Label ID="lblTransactionCode" runat="server" Text='<%#GetFieldName("ChangeCode")%>'></asp:Label>:
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtTransactionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ChangeCode")%>'
                                        ReadOnly="true" CssClass="NormalBold" />
                                </td>
                                <td style="width: 10%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <asp:Label ID="lblReturnReason" runat="server" Text='<%#GetFieldName("ReturnReason")%>'></asp:Label>:
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtReturnReason" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ReturnReason")%>'
                                        ReadOnly="true" CssClass="NormalBold" Width="500" />
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 20%">
                                    <asp:Label ID="lblCorrectedData" runat="server" Text='<%#GetFieldName("CorrectedData")%>'></asp:Label>:
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtCorrectedData" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CorrectedData")%>'
                                        ReadOnly="true" CssClass="NormalBold" />
                                </td>
                                <td style="width: 10%">
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
                <table>
                    <tr>
                        <td width="20%">
                            <asp:RadioButtonList ID="rblTransactionDecision" runat="server" RepeatDirection="Horizontal"
                                CssClass="NormalBold" AutoPostBack="true" OnSelectedIndexChanged="rblTransactionDecision_SelectedIndexChanged">
                                <asp:ListItem Text="Accept" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Return" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Notify Change" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlReturnChangeCode" runat="server" DataTextField="RejectReason"
                                DataValueField="RejectReasonCode">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCorrectedDataMsg" runat="server" Text="Corrected Data:" Visible="false" />
                            <asp:TextBox ID="txtCorrectedData" runat="server" CssClass="NormalBold" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="CommandButton"
                                OnClick="btnSave_Click" OnClientClick="return confirm('Are you sure you want to save?')"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
