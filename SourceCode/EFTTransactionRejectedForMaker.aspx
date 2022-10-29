<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EFTTransactionRejectedForMaker.aspx.cs" Inherits="EFTN.EFTTransactionRejectedForMaker" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Maker Transaction Rejected Page</title>
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
                Transactions Rejected by Checker
            </div>
            <div align="center" id="Div1" runat="server" style="position: relative; overflow: auto; width: 940px; margin-top: 15px; height: 30px;">
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddListTransactionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddListTransactionType_SelectedIndexChanged">
                                <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                <asp:ListItem Value="Debit">Debit</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="NormalBold">
                            <asp:Label ID="lblCurrency" runat="server" Text="Currency"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="CurrencyDdList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CurrencyDdList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="linkBtnFlatFile" Text="Generate Reverse Flat File" ForeColor="Red" OnClick="linkBtnFlatFile_Click"></asp:LinkButton>
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
            <div align="center" id="AvailDiv" runat="server" style="position: relative; overflow: auto; width: 940px; margin-top: 15px; height: 300px;">

                <asp:DataGrid ID="dtgRejectedEDR" AlternatingItemStyle-BackColor="lightyellow" AutoGenerateColumns="false"
                    BorderWidth="0px" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                    GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="#CAD2FD"
                    ItemStyle-CssClass="NormalSmall" runat="server" DataKeyField="EDRID">
                    <Columns>
                        <asp:TemplateColumn HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbxCheck" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <a href='EFTEditTransactionSent.aspx?EDRID=<%#DataBinder.Eval(Container.DataItem, "EDRID")%>&TypeOfPayment=<%#DataBinder.Eval(Container.DataItem, "TypeOfPayment")%>'>Edit</a>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="BankName">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="BranchName">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="A/C No. From BEFTN">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                            </ItemTemplate>
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
                        <asp:TemplateColumn HeaderText="RejectReason">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "RejectReason")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px; height: 30px; margin-left: 15px; margin-bottom: 20px; padding-left: 20px">

                <asp:LinkButton ID="btnCancel" runat="server" Text="Delete from Batch" CssClass="CommandButton"
                    OnClientClick="return confirm('Are you sure you want to delete this item from batch?')" OnClick="btnCancel_Click"></asp:LinkButton>

                <asp:Label ID="lblMsg" runat="server"></asp:Label>

            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />

    </form>
</body>
</html>
