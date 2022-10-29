<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EFTDishonorRejectedForMaker.aspx.cs"
    Inherits="EFTN.EFTDishonorRejectedForMaker" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Maker Dishonor Rejected Page</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Transactions Rejected by Checker</div>
            <div style="overflow: scroll">
                <table>
                    <tr>
                        <td>
                            <asp:DataGrid ID="dtgRejectedEDR" AlternatingItemStyle-BackColor="lightyellow" AutoGenerateColumns="false"
                                BorderWidth="0px" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="#CAD2FD"
                                ItemStyle-CssClass="Normal" runat="server" DataKeyField="EDRID">
                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <a href='EFTDishonorSentChecker.aspx?EDRID=<%#DataBinder.Eval(Container.DataItem, "EDRID")%>&TypeOfPayment=<%#DataBinder.Eval(Container.DataItem, "TypeOfPayment")%>'>
                                                Edit</a>
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
