<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkReturnUpload.aspx.cs" Inherits="EFTN.BulkReturnUpload" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FLORA BEFTN</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                IF Debit Return Upload
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <%--<table>
                                <tr>
                                    <td class="NormalBold">
                                        <asp:Label ID="lblCurrency" runat="server" Text="Select Currency"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CurrencyDdList" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>                                    
                                </tr>
                            </table>--%>
                            <table>
                                <tr>
                                    <td class="NormalBold">Please Select your Excel File to Upload<br />
                                        <asp:FileUpload CssClass="inputlt" ID="fulExcelFile" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnUploadExcel" runat="server" CssClass="inputlt" Text="Upload File"
                                            Width="80" OnClientClick="return confirm('Are you sure you want to import this file?')"
                                            OnClick="btnUploadExcel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="lblErrMsg" runat="server" BackColor="WhiteSmoke" Width="900px" Height="80px" ReadOnly="true" style="text-align:center"></asp:TextBox>
                            <%--<asp:TextBox ID="lblErrMsg" runat="server" Width="500px" Height="200px" TextMode="MultiLine"></asp:TextBox>--%>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="overflow: scroll; height: 350px">
                <table>
                    <tr>
                        <td width="10px"></td>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="3">
                                        <asp:DataGrid ID="dtgInwardTransactionMaker" AlternatingItemStyle-BackColor="lightyellow"
                                            AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                            FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                            Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                            DataKeyField="EDRID" Width="980px" AllowPaging="True" AllowSorting="true"
                                            HeaderStyle-ForeColor="#FFFFFF"
                                            OnPageIndexChanged="dtgInwardTransactionMaker_PageIndexChanged"
                                            PageSize="500" OnSortCommand="dtgInwardTransactionMaker_SortCommand">
                                            <Columns>
                                               <%-- <asp:EditCommandColumn CausesValidation="False" EditText="Edit"
                                                    UpdateText="Update" CancelText="Cancel"></asp:EditCommandColumn>--%>
                                                <asp:TemplateColumn HeaderText="SL.">
                                                    <ItemTemplate>
                                                        <%#(dtgInwardTransactionMaker.PageSize * dtgInwardTransactionMaker.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbxCheck" runat="server" />
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
                                                <%--<asp:TemplateColumn HeaderText="MatchStatus" SortExpression="MatchStatus">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "MatchStatus")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                                <asp:TemplateColumn HeaderText="VACCNO" SortExpression="VACCNO">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "VACCNO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%-- <asp:TemplateColumn HeaderText="Hold" SortExpression="Hold">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "Hold")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                                <asp:TemplateColumn HeaderText="Receiver Name" SortExpression="ReceiverName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ReceiverName as of CBS" SortExpression="TITLE">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "TITLE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="A/C No. For EFT User" SortExpression="DFIAccountNo">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="DFIAccountNo" Width="90" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DFIAccountNo") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="A/C No" SortExpression="DFIAccountNo">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="A/C No. From Original BEFTN" SortExpression="DFIAccountNoAsCBS">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DFIAccountNoAsCBS")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Settlement JDate" SortExpression="SettlementJDate">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "SettlementJDate")%>
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
                                                <asp:TemplateColumn HeaderText="Amount" SortExpression="Amount">
                                                    <ItemTemplate>
                                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="BankName" SortExpression="BankName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Branch Name" SortExpression="BranchName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Entry Description" SortExpression="EntryDesc">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "EntryDesc")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                 <asp:TemplateColumn HeaderText="IdNumber" SortExpression="IdNumber">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "IdNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                  <asp:TemplateColumn HeaderText="CompanyName" SortExpression="CompanyName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "CompanyName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                  <asp:TemplateColumn HeaderText="EffectiveEntryDate" SortExpression="EffectiveEntryDate">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "EffectiveEntryDate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                 <asp:TemplateColumn HeaderText="EntryDateTransactionReceived" SortExpression="EntryDateTransactionReceived">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "EntryDateTransactionReceived")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                            </Columns>
                                            <FooterStyle CssClass="GrayBackWhiteFont" />
                                            <AlternatingItemStyle BackColor="LightYellow" />
                                            <ItemStyle BackColor="#CAD2FD" CssClass="Normal" />
                                            <HeaderStyle CssClass="GrayBackWhiteFont" ForeColor="White" />
                                            <PagerStyle Mode="NumericPages" Position="TopAndBottom" />
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
                        <td width="40%">
                            <asp:LinkButton ID="btnSave" runat="server" Text="Return" CssClass="NormalBold"
                                OnClick="btnSave_Click" OnClientClick="return confirm('Are you sure you want to return?')"></asp:LinkButton>
                        </td>
                        <td width="10"></td>
                        <td>
                            <asp:Button ID="bdnAccept" runat="server" Text="Accept" CssClass="NormalBlue" OnClick="bdnAccept_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
