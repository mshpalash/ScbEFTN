<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InwardTransactionApprovedChecker.aspx.cs"
    Inherits="EFTN.InwardTransactionApprovedChecker" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">

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
                Inward Transaction Approved by Maker
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
            <div align="left" class="boxmodule" style="padding-top: 10px; width: 970px; margin-top: 10px; height: 80px; margin-left: 15px">
                <table>
                    <tr>
                        <%--<td width="10"></td>--%>
                        <td>
                            <asp:DropDownList ID="BankList" DataTextField="BankName" DataValueField="BankCode"
                                AutoPostBack="true" runat="Server" OnSelectedIndexChanged="BankList_SelectedIndexChanged" />
                        </td>
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
                        <td width="5px"></td>
                        <td>
                            <asp:Label ID="lblTotalItem" runat="server" CssClass="NormalBold">
                            </asp:Label>
                        </td>
                        <td width="10px"></td>
                        <td>
                            <asp:Label ID="lblTotalAmount" runat="server" CssClass="NormalBold">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
               <table width="1000">
                    <tr>
                        <td width="10"></td>
                        <td class="NormalBold">Select Risk:
                            <asp:CheckBoxList ID="cbxList" runat="server" RepeatColumns="16" RepeatDirection="Vertical" CssClass="NormalBold" OnSelectedIndexChanged="cbxList_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="AvailDiv" runat="server" style="position: relative; overflow: auto; width: 950px; height: 380px;">
                <table>
                    <tr>
                        <td colspan="3">
                            <asp:DataGrid ID="dtgEFTCheckerApproved" AlternatingItemStyle-BackColor="lightyellow"
                                AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                DataKeyField="EDRID" Width="980px" AllowPaging="True"
                                HeaderStyle-ForeColor="#FFFFFF"
                                OnPageIndexChanged="dtgEFTCheckerApproved_PageIndexChanged"
                                PageSize="500" AllowSorting="True" OnSortCommand="dtgEFTCheckerApproved_SortCommand">
                                <Columns>
                                    <asp:TemplateColumn HeaderText="SL.">
                                        <ItemTemplate>
                                            <%#(dtgEFTCheckerApproved.PageSize * dtgEFTCheckerApproved.CurrentPageIndex) + Container.ItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbxCheck" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Check with CBS">
                                        <ItemTemplate>
                                            <a href="InwardTransactionWithMICRForChecker.aspx?inwardTransactionEDRID=<%#DataBinder.Eval(Container.DataItem, "EDRID")%>">Check with CBS</a>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Account No. Changed By EFT User" SortExpression="AccountChangeStatus" ItemStyle-ForeColor="Red" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "AccountChangeStatus")%>
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
                                    <asp:TemplateColumn HeaderText="Effective Entry Date" SortExpression="EffectiveEntryDate">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "EffectiveEntryDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Settlement JDate" SortExpression="SettlementJDate">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "SettlementJDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="EntryDate Transaction Received" SortExpression="EntryDateTransactionReceived">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "EntryDateTransactionReceived")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="A/C No. For EFT User" SortExpression="DFIAccountNo">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Amount" SortExpression="Amount">
                                        <ItemTemplate>
                                            <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
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
                                    <asp:TemplateColumn HeaderText="A/C No. From Original BEFTN" SortExpression="DFIAccountNoAsCBS">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "DFIAccountNoAsCBS")%>
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
                                    <asp:TemplateColumn HeaderText="Session">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "SessionID")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="CCY">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "CCY")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Entry Desc">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "EntryDesc")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                  
                                    <asp:TemplateColumn HeaderText="CompanyName">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "CompanyName")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="Trace Number" SortExpression="TraceNumber">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "TraceNumber")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Batch Number" SortExpression="BatchNumber">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "BatchNumber")%>
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
                                <AlternatingItemStyle BackColor="LightYellow" />
                                <ItemStyle BackColor="#CAD2FD" CssClass="Normal" />
                                <HeaderStyle CssClass="GrayBackWhiteFont" />
                                <PagerStyle Mode="NumericPages" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnAccept" runat="server" Text="Accept" CssClass="CommandButton"
                                            OnClientClick="return confirm('Are you sure you want to accept?')" OnClick="btnAccept_Click"></asp:LinkButton></td>
                                    <td width="50"></td>
                                    <td>
                                        <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="CommandButton"
                                            OnClientClick="return confirm('Are you sure you want to reject?')" OnClick="btnReject_Click"></asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <asp:Label ID="lblRejectInstruction" runat="server" CssClass="NormalBold" Text="For a rejection item(s) please give a reason"></asp:Label></td>
                        <td colspan="2" width="60%"></td>
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
