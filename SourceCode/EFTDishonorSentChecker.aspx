<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EFTDishonorSentChecker.aspx.cs"
    Inherits="EFTN.EFTDishonorSentChecker" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flora Limited System</title>
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
                Dishonor Sent
            </div>
            <br />
            <div align="left" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px; height: 40px; margin-left: 15px">
                <table>
                    <tr>
                        <td width="10"></td>
                        <td>
                            <asp:DropDownList ID="ddListTransactionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddListTransactionType_SelectedIndexChanged">
                                <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                <asp:ListItem Value="Debit">Debit</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbxAll" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAll_CheckedChanged" />
                        </td>
                        <td width="50px"></td>
                        <td>
                            <asp:Label ID="lblTotalItem" runat="server" CssClass="NormalBold">
                            </asp:Label>
                        </td>
                        <td width="50px"></td>
                        <td>
                            <asp:Label ID="lblTotalAmount" runat="server" CssClass="NormalBold">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="overflow: scroll">
                <table>
                    <tr>
                        <td class="NormalBold">Dishonor Sent:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:DataGrid ID="dtgDishonorSentChecker" AlternatingItemStyle-BackColor="lightyellow"
                                            AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1" HeaderStyle-ForeColor="#FFFFFF"
                                            FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                            Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                            DataKeyField="DishonoredID" Width="980px" OnSortCommand="dtgDishonorSentChecker_SortCommand" OnPageIndexChanged="dtgDishonorSentChecker_PageIndexChanged" AllowPaging="True" AllowSorting="True">
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBEFTNList" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="DishonorReason" SortExpression="DishonorReason">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DishonorReason")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="BankName" SortExpression="BankName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="A/C No. From BEFTN" SortExpression="DFIAccountNo">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="A/C No. From CBS" SortExpression="ACCOUNT">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "ACCOUNT")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Currency">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "Currency")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Amount" SortExpression="Amount">
                                                    <ItemTemplate>
                                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ReceiverName" SortExpression="ReceiverName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ReceiverName as of CBS" SortExpression="TITLE">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "TITLE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="STATUS" SortExpression="STATUS">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "STATUS")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="RISKS" SortExpression="RISKS">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "RISKS")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="OrgTraceNumber" SortExpression="OrgTraceNumber">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "OrgTraceNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="TraceNumber" SortExpression="TraceNumber">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "TraceNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Return Reason" SortExpression="RejectReason">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "RejectReason")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="AddendaInfo" SortExpression="AddendaInfo">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "AddendaInfo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="DateOfDeath" SortExpression="DateOfDeath">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DateOfDeath")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="IdNumber" SortExpression="IdNumber">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "IdNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ReceiverName" SortExpression="ReceiverName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Amount" SortExpression="Amount">
                                                    <ItemTemplate>
                                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="OrgSettlementJDate" SortExpression="OrgSettlementJDate">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "OrgSettlementJDate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="SettlementJDate" SortExpression="SettlementJDate">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "SettlementJDate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <FooterStyle CssClass="GrayBackWhiteFont" />
                                            <AlternatingItemStyle BackColor="LightYellow" />
                                            <ItemStyle BackColor="#CAD2FD" CssClass="Normal" />
                                            <HeaderStyle CssClass="GrayBackWhiteFont" ForeColor="White" />
                                            <PagerStyle Mode="NumericPages" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnDishonorApprove" runat="server" Text="Approve" CssClass="CommandButton"
                                OnClick="btnDishonorApprove_Click" OnClientClick="return confirm('Are you sure you want to approve?')" />
                            <asp:LinkButton ID="btnDishonorReject" runat="server" Text="Reject" CssClass="CommandButton"
                                OnClick="btnDishonorReject_Click" OnClientClick="return confirm('Are you sure you want to reject?')" />
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalBold">RejectReason:
                            <asp:TextBox ID="txtDishonorSentRejectReason" TextMode="MultiLine" runat="server" OnKeyUp="return makeUppercase(this.name);" />
                            <asp:Label ID="lblDishonorSentMsg" runat="server" Text="Please put some reason" CssClass="NormalRed"
                                Visible="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
