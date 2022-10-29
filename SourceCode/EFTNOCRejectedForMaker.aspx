<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EFTNOCRejectedForMaker.aspx.cs"
    Inherits="EFTN.EFTNOCRejectedForMaker" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Maker NOC Rejected Page</title>
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
                NOC rejected by Checker</div>
            <br />
            <div align="left" class="boxmodule" style="padding-top: 10px; width: 920px; margin-top: 10px;
                height: 40px; margin-left: 40px">
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddListTransactionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddListTransactionType_SelectedIndexChanged">
                                <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                <asp:ListItem Value="Debit">Debit</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>                
            <div style="overflow: scroll">
                <table>
                    <tr>
                        <td>
                            <asp:DataGrid ID="dtgRejectedNOC" AlternatingItemStyle-BackColor="lightyellow" AutoGenerateColumns="false"
                                BorderWidth="0px" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="#CAD2FD"
                                ItemStyle-CssClass="Normal" runat="server" DataKeyField="NOCID">
                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <a href='EFTEditNOC.aspx?NOCID=<%#DataBinder.Eval(Container.DataItem, "NOCID")%>'>Edit</a>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="BatchNumber">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "BatchNumber")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="RejectReason">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "RejectReason")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="EntryDesc">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "EntryDesc")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="SECC">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "SECC")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="CompanyId">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "CompanyId")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="CompanyName">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "CompanyName")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="EffectiveEntryDate">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "EffectiveEntryDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="DFIAccountNo">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Amount">
                                        <ItemTemplate>
                                            <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="BankName">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="IDNumber">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "IDNumber")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="ReceiverName">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
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
