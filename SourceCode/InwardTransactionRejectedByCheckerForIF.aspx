<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InwardTransactionRejectedByCheckerForIF.aspx.cs"
    Inherits="EFTN.InwardTransactionRejectedByCheckerForIF" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inward Transaction</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">
        javascript: window.history.forward(1);
        function makeUppercase(myControl, evt) {
            document.getElementById(myControl).value = document.getElementById(myControl).value.toUpperCase();
        }
    </script>

</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Debit Returned Transactions For IF
            </div>

            <div>
                <table>
                    <tr height="60px">
                        <td width="10px"></td>
                        <td colspan="2">
                            <asp:LinkButton ID="btnGenerateRevarseFlatFile" runat="server" Text="Generate Reverse Flat File" CssClass="CommandButton" ForeColor="Red"
                                OnClientClick="return confirm('Are you sure you want to save?')" OnClick="btnGenerateRevarseFlatFile_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td width="10px"></td>
                        <%--<td>
                            <asp:DropDownList ID="ddListTransactionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddListTransactionType_SelectedIndexChanged">
                                <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                <asp:ListItem Value="Debit">Debit</asp:ListItem>
                            </asp:DropDownList>
                        </td>--%>
                        <td class="NormalBold">
                            <asp:Label ID="lblCurrency" runat="server" Text="Currency"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="CurrencyDdList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CurrencyDdList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="NormalBold">
                            <asp:Label ID="lblSession" runat="server" Text="Session"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="SessionDdList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SessionDdList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbxAll" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAll_CheckedChanged" />
                        </td>

                    </tr>
                </table>
            </div>
            <br />
            <div>
                <table>
                    <tr>
                        <td class="NormalBold">
                            <asp:Label ID="lblTotalItem" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalBold">
                            <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td width="10px"></td>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="3">
                                        <div id="AvailDiv" runat="server" style="overflow: scroll;">
                                            <asp:DataGrid ID="dtgInwardTransactionMaker" AlternatingItemStyle-BackColor="lightyellow"
                                                AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                                FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                                DataKeyField="EDRID" Width="950px">
                                                <Columns>                                                 
                                                    <asp:TemplateColumn HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbxCheck" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="TraceNumber">
                                                        <ItemTemplate>
                                                            <a href="InwardTransactionWithMICR.aspx?inwardTransactionEDRID=<%#DataBinder.Eval(Container.DataItem, "EDRID")%>">Check with CBS</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="BankName">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="A/C No. From BEFTN">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="DFIAccountNo" Width="90" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DFIAccountNo") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="A/C No. From CBS">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "ACCOUNT")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Currency">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "Currency")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Session">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "SessionID")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="ReceiverName">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="ReceiverName as of CBS">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "TITLE")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATUS">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "STATUS")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="RISKS">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "RISKS")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="RejectedReason">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "RejectReason")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="NormalBold">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rblTransactionDecision" runat="server" RepeatDirection="Horizontal"
                                            CssClass="NormalBold" AutoPostBack="true" OnSelectedIndexChanged="rblTransactionDecision_SelectedIndexChanged">
                                            <asp:ListItem Text="Accept" Value="1"></asp:ListItem>
                                          <%--  <asp:ListItem Text="Return" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Notify Change" Value="3"></asp:ListItem>--%>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td width="80%"></td>
                                </tr>
                               <%-- <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlReturnChangeCode" runat="server" DataTextField="RejectReason"
                                            DataValueField="RejectReasonCode">
                                        </asp:DropDownList>
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td>
                                        <asp:Label ID="lblCorrectedDataMsg" runat="server" Text="Corrected Data:" Visible="false" />
                                        <asp:TextBox ID="txtCorrectedData" runat="server" CssClass="NormalBold" Visible="false"
                                            OnKeyUp="return makeUppercase(this.name);" />
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td width="40%">
                                        <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="CommandButton"
                                            OnClick="btnSave_Click" OnClientClick="return confirm('Are you sure you want to save?')"></asp:LinkButton>
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
