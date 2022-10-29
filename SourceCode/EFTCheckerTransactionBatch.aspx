<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EFTCheckerTransactionBatch.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="EFTN.EFTCheckerTransactionBatch" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    javascript: window.history.forward(1);

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

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta id="metaref" runat="server" http-equiv="refresh" content="60" />
    <title>Checker Page</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Checker Page
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
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsgExportTransaction" CssClass="NormalBlue" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbxAllTransactionSent" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAllTransactionSent_CheckedChanged" />
                        </td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblTRSent" runat="server" CssClass="NormalBold" Text="Transaction Sent"></asp:Label>
                            <div style="overflow: scroll; height: 250px;">
                                <table>
                                    <tr>
                                        <td class="NormalBold" align="center">
                                            <asp:DataGrid ID="dtgBatchTransactionSent" runat="Server" BorderWidth="0px" GridLines="None"
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID">
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbxSentBatchTS" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="">
                                                        <ItemTemplate>
                                                            <a href="BatchWiseTransactionDetails.aspx?TransactionEDRID=<%#DataBinder.Eval(Container.DataItem, "TransactionID")%>">Batch Details</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="true" />
                                                    <asp:BoundColumn DataField="BatchType" HeaderText="Type" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
<%--                                                    <asp:BoundColumn DataField="TxnType" HeaderText="TxnType" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />--%>
                                                    <asp:BoundColumn DataField="DataEntryType" HeaderText="File Type" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />                                                    <asp:BoundColumn DataField="CompanyName" HeaderText="Company Name" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="SECC" HeaderText="SECC" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}"
                                                        ItemStyle-Wrap="true" HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="EntryDesc" HeaderText="Description" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="Currency" HeaderText="Currency" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}"
                                                        ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="LoginID" HeaderText="Maker ID"
                                                        ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>

                </table>
            </div>
            <br />
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsgExportTransactionSts" CssClass="NormalBlue" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalBold">
                            <asp:Label ID="lblTotalItemSts" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalBold">
                            <asp:Label ID="lblTotalAmountSts" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbxAllTransactionSentSts" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAllTransactionSentSts_CheckedChanged" />
                        </td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="NormalBold" Text="STS Transaction Sent"></asp:Label>
                            <div style="overflow: scroll; height: 250px;">
                                <table>
                                    <tr>
                                        <td class="NormalBold" align="center">
                                            <asp:DataGrid ID="dtgBatchTransactionSentSts" runat="Server" BorderWidth="0px" GridLines="None"
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID" OnSortCommand="dtgBatchTransactionSentSts_SortCommand"
                                                AllowPaging="True" PageSize="500" AllowSorting="True" OnPageIndexChanged="dtgBatchTransactionSentSts_PageIndexChanged">
                                                <AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="SL.">
                                                        <ItemTemplate>
                                                            <%#(dtgBatchTransactionSentSts.PageSize * dtgBatchTransactionSentSts.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbxSentBatchTSSts" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="">
                                                        <ItemTemplate>
                                                            <a href="BatchWiseTransactionDetails.aspx?TransactionEDRID=<%#DataBinder.Eval(Container.DataItem, "TransactionID")%>">Batch Details</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="true">
                                                        <HeaderStyle Wrap="True"></HeaderStyle>

                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="BatchType" HeaderText="Type" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>

                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
<%--                                                    <asp:BoundColumn DataField="TxnType" HeaderText="TxnType" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />--%>
                                                    <asp:BoundColumn DataField="DataEntryType" HeaderText="File Type" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />                                                    <asp:BoundColumn DataField="CompanyName" HeaderText="Company Name" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="CompanyName" HeaderText="Company Name" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>

                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SECC" HeaderText="SECC" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>

                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}"
                                                        ItemStyle-Wrap="true" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>

                                                        <ItemStyle Wrap="True"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="EntryDesc" HeaderText="Description" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>

                                                    <asp:BoundColumn DataField="Currency" HeaderText="Currency" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="False"></asp:BoundColumn>

                                                    <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>

                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}"
                                                        ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>

                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="LoginID" HeaderText="Maker ID"
                                                        ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                </Columns>

                                                <FooterStyle CssClass="GrayBackWhiteFont"></FooterStyle>

                                                <HeaderStyle CssClass="GrayBackWhiteFont"></HeaderStyle>

                                                <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall"></ItemStyle>
                                                <PagerStyle Mode="NumericPages" Position="TopAndBottom" />
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>

                </table>
            </div>
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsgExportTransactionStd" CssClass="NormalBlue" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalBold">
                            <asp:Label ID="lblTotalItemStd" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalBold">
                            <asp:Label ID="lblTotalAmountStd" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbxAllTransactionSentStd" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAllTransactionSentStd_CheckedChanged" />
                        </td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" CssClass="NormalBold" Text="Standing Order Transaction Sent"></asp:Label>
                            <div style="overflow: scroll; height: 250px;">
                                <table>
                                    <tr>
                                        <td class="NormalBold" align="center">
                                            <asp:DataGrid ID="dtgBatchTransactionSentStd" runat="Server" BorderWidth="0px" GridLines="None"
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID">
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="SL.">
                                                        <ItemTemplate>
                                                            <%#(dtgBatchTransactionSentStd.PageSize * dtgBatchTransactionSentStd.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbxSentBatchTSStdOrder" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="">
                                                        <ItemTemplate>
                                                            <a href="BatchWiseTransactionDetails.aspx?TransactionEDRID=<%#DataBinder.Eval(Container.DataItem, "TransactionID")%>">Batch Details</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="true" />
                                                    <asp:BoundColumn DataField="BatchType" HeaderText="Type" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
<%--                                                     <asp:BoundColumn DataField="TxnType" HeaderText="TxnType" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />--%>
                                                    <asp:BoundColumn DataField="DataEntryType" HeaderText="File Type" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />                                                    <asp:BoundColumn DataField="CompanyName" HeaderText="Company Name" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                   <asp:BoundColumn DataField="CompanyName" HeaderText="Company Name" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="SECC" HeaderText="SECC" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}"
                                                        ItemStyle-Wrap="true" HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="EntryDesc" HeaderText="Description" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="Currency" HeaderText="Currency" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}"
                                                        ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="LoginID" HeaderText="Maker ID"
                                                        ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>

                </table>
            </div>
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td width="50px">
                                        <asp:LinkButton ID="btnAccept" runat="server" Text="Accept" CssClass="CommandButton"
                                            OnClientClick="return confirm('Are you sure you want to accept?')" OnClick="btnAccept_Click"></asp:LinkButton>
                                    </td>
                                    <td width="80px"></td>
                                    <td>
                                        <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="CommandButton"
                                            OnClientClick="return confirm('Are you sure you want to reject?')" OnClick="btnReject_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRejectInstruction" runat="server" CssClass="NormalBold" Text="For a rejection item(s) please give a reason"></asp:Label>
                            <asp:Label ID="lblNoReturnReason" runat="server" Text="Please Enter a return reason"
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <asp:TextBox ID="txtRejectedReason" runat="server" Width="300" MaxLength="50" OnKeyUp="return makeUppercase(this.name);" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
