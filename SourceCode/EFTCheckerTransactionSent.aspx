<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EFTCheckerTransactionSent.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="EFTN.EFTCheckerTransactionSent" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">
        javascript: window.history.forward(1);

        function makeUppercase(myControl, evt) {
            document.getElementById(myControl).value = document.getElementById(myControl).value.toUpperCase();
        }

        function MM_swapImgRestore() { //v3.0
            var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
        }
        function MM_preloadImages() { //v3.0
            var d = document; if (d.images) {
                if (!d.MM_p) d.MM_p = new Array();
                var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                    if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
            }
        }

        function MM_findObj(n, d) { //v4.01
            var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
                d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
            }
            if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
            for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
            if (!x && d.getElementById) x = d.getElementById(n); return x;
        }

        function MM_swapImage() { //v3.0
            var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2) ; i += 3)
                if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
        }
    </script>

</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header" runat="server" />
            <div class="Head" align="center">
                Transaction sent Checker page
            </div>
            <div>
                <table style="padding-top: 15px">
                    <tr height="20px">
                        <td width="30px"></td>
                        <td>
                            <a href="EFTChecker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/EFTCheckerOn.gif',1)">
                                <img src="images/EFTChecker.gif" name="Image4" width="149" height="25" border="0" id="Image4" /></a>

                        </td>
                        <td width="100px"></td>
                        <td>
                            <a href="EFTCheckerEBBS.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/EFTCheckerEBBSOn.gif',1)">
                                <img src="images/EFTCheckerEBBS.gif" name="Image1" width="149" height="25" border="0" id="Image1" /></a>

                        </td>
                        <td width="100px"></td>
                        <td>
                            <a href="EFTCheckerAuthorizer.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/EFTCheckerAuthorizerOn.gif',1)">
                                <img src="images/EFTCheckerAuthorizer.gif" name="Image2" width="149" height="25" border="0" id="Image2" /></a>

                        </td>
                        <td width="100px"></td>
                        <td>
                            <a href="EFTCheckerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ReportOn.gif',1)">
                                <img src="images/Report.gif" name="Image3" width="149" height="25" border="0" id="Image3" /></a>

                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div align="left" class="boxmodule" style="padding-top: 10px; width: 890px; margin-top: 10px; margin-left: 15px; margin-bottom: 10px; padding-left: 20px; height: 50px;">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSynchronizeCBSAccountInfo" runat="server"
                                Text="Get CBS Account Info" OnClick="btnSynchronizeCBSAccountInfo_Click" />
                        </td>
                    </tr>
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
            <div id="AvailDiv" runat="server" style="position: relative; overflow: scroll; width: 950px; height: 380px;">
                <table>
                    <tr>
                        <td colspan="3">
                            <asp:DataGrid ID="dtgEFTChecker" AlternatingItemStyle-BackColor="lightyellow" AutoGenerateColumns="false"
                                BorderWidth="0px" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="#CAD2FD"
                                ItemStyle-CssClass="Normal" runat="server" DataKeyField="EDRID"
                                AllowPaging="true" OnPageIndexChanged="dtgEFTChecker_PageIndexChanged"
                                HeaderStyle-ForeColor="#FFFFFF"
                                PageSize="500" AllowSorting="true" OnSortCommand="dtgEFTChecker_SortCommand">
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBEFTNList" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="SL.">
                                        <ItemTemplate>
                                            <%#(dtgEFTChecker.PageSize * dtgEFTChecker.CurrentPageIndex) + Container.ItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="EffectiveEntryDate" HeaderText="Entry Date">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "EffectiveEntryDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="TxnType">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "TxnType")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="DataEntryType">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "DataEntryType")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="STATUS" HeaderText="STATUS">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "STATUS")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="RISKS" HeaderText="RISKS">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "RISKS")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="PaymentInfo" HeaderText="Reason">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "PaymentInfo")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ReceivingBankRoutingNo" HeaderText="ReceivingBank RoutingNo">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "ReceivingBankRoutingNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="BankName" HeaderText="BankName">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="BranchName" HeaderText="BranchName">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="AccountNo" HeaderText="A/C No. From BEFTN">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "AccountNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ACCOUNT" HeaderText="A/C No. From CBS">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "ACCOUNT")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="DFIAccountNo" HeaderText="DFIAccountNo">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="AccountType" HeaderText="Account Type">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "AccountType")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Currency">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Currency")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Amount" HeaderText="Amount">
                                        <ItemTemplate>
                                            <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="IdNumber" HeaderText="ReceiverID">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "IdNumber")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ReceiverName" HeaderText="Receiver Name">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="TITLE" HeaderText="Sender Name as of CBS">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "TITLE")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="RejectReason" HeaderText="RejectReason">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "RejectReason")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="EntryDesc" HeaderText="EntryDesc">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "EntryDesc")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="CompanyName">
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "CompanyName")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn> 
                                    <asp:TemplateColumn SortExpression="SECC" HeaderText="SECC">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "SECC")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="NameOfCreatedBy" HeaderText="Maker">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "NameOfCreatedBy")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="NameOfApprovedBy" HeaderText="Checker">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "NameOfApprovedBy")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <FooterStyle CssClass="GrayBackWhiteFont" />
                                <PagerStyle Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="LightYellow" />
                                <ItemStyle BackColor="#CAD2FD" CssClass="NormalSmall" />
                                <HeaderStyle CssClass="GrayBackWhiteFont" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px; height: 100px; margin-left: 15px; margin-bottom: 20px; padding-left: 20px">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td width="50px">
                                        <asp:LinkButton ID="btnAccept" runat="server" Text="Accept" CssClass="CommandButton"
                                            OnClick="btnAccept_Click" OnClientClick="return confirm('Are you sure you want to accept?')"></asp:LinkButton>
                                    </td>
                                    <td width="50px"></td>
                                    <td>
                                        <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="CommandButton"
                                            OnClick="btnReject_Click" OnClientClick="return confirm('Are you sure you want to reject?')"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnAcceptAll" runat="server" Text="Accept All Transactions" CssClass="CommandButton"
                                            OnClientClick="return confirm('Are you sure you want to accept all transaction?')" OnClick="btnAcceptAll_Click"></asp:LinkButton>
                                    </td>
                                    <td width="50px"></td>
                                    <td>
                                        <asp:LinkButton ID="btnRejectAll" runat="server" Text="Reject All Transactions" CssClass="CommandButton"
                                            OnClientClick="return confirm('Are you sure you want to reject all transaction?')" OnClick="btnRejectAll_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <asp:Label ID="lblRejectInstruction" runat="server" CssClass="NormalBold" Text="For a rejection item(s) please give a reason"></asp:Label></td>
                        <td colspan="2" width="60%"></td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <asp:TextBox ID="txtRejectedReason" runat="server" Width="300" MaxLength="50" OnKeyUp="return makeUppercase(this.name);" TextMode="MultiLine"></asp:TextBox></td>
                        <td colspan="2" width="60%" style="color: Red; font-weight: bold;">
                            <asp:Label ID="lblNoReturnReason" runat="server" Text="Please Enter a return reason"
                                Visible="false" ForeColor="red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
