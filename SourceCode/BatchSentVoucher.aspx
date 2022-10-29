<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchSentVoucher.aspx.cs"
    Inherits="EFTN.BatchSentVoucher" %>

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

</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header" runat="server" />
            <div class="Head" align="center">
                Batch Sent Voucher
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
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px; height: 40px; margin-left: 15px">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsgExportTransaction" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbxAllTransactionSent" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAllTransactionSent_CheckedChanged" />
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td style="width: 20px"></td>
                        <td>
                            <asp:LinkButton ID="PrintVoucherBtn" runat="server" CssClass="CommandButton" Text="Print Vouchers"
                                OnClick="PrintVoucherBtn_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblTRSent" runat="server" CssClass="NormalBold" Text="Transaction Sent"></asp:Label>
                            <div align="center" id="Div1" runat="server" style="position: relative; overflow: auto; width: 940px; margin-top: 15px; height: 300px;">
                                <table>
                                    <tr>
                                        <td class="NormalBold" align="center">
                                            <asp:DataGrid ID="dtgBatchTransactionSent" runat="Server" BorderWidth="0px" GridLines="None"
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID" AllowPaging="True" OnPageIndexChanged="dtgBatchTransactionSent_PageIndexChanged" PageSize="500">
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="SL.">
                                                        <ItemTemplate>
                                                            <%#(dtgBatchTransactionSent.PageSize * dtgBatchTransactionSent.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbxSentBatchTS" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="">
                                                        <ItemTemplate>
                                                            <a href="BatchWiseTransactionDetailsForCBSChecker.aspx?TransactionEDRID=<%#DataBinder.Eval(Container.DataItem, "TransactionID")%>">Batch Details</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo">
                                                        <HeaderStyle Wrap="True" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="BatchType" HeaderText="Type" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="CompanyName" HeaderText="Company Name">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SECC" HeaderText="SECC">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="True" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="EntryDesc" HeaderText="Description">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Currency" HeaderText="Currency">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <FooterStyle CssClass="GrayBackWhiteFont" />
                                                <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Position="TopAndBottom" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall" />
                                                <HeaderStyle CssClass="GrayBackWhiteFont" />
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>

                </table>
            </div>
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px; height: 40px; margin-left: 15px">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsgExportTransactionSTS" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbxAllTransactionSentSTS" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAllTransactionSentSTS_CheckedChanged" />
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td style="width: 20px"></td>
                        <td>
                            <asp:LinkButton ID="PrintVoucherBtnSTS" runat="server" CssClass="CommandButton" Text="Print Vouchers" OnClick="PrintVoucherBtnSTS_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblSTSTxn" runat="server" CssClass="NormalBold" Text="STS Transaction Sent"></asp:Label>
                            <div align="center" id="Div2" runat="server" style="position: relative; overflow: auto; width: 940px; margin-top: 15px; height: 300px;">
                                <table>
                                    <tr>
                                        <td class="NormalBold" align="center">
                                            <asp:DataGrid ID="dtgBatchTransactionSentSTS" runat="Server" BorderWidth="0px" GridLines="None"
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID" AllowPaging="True" PageSize="500" OnPageIndexChanged="dtgBatchTransactionSentSTS_PageIndexChanged">
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="SL.">
                                                        <ItemTemplate>
                                                            <%#(dtgBatchTransactionSentSTS.PageSize * dtgBatchTransactionSentSTS.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbxSentBatchTSSTS" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="">
                                                        <ItemTemplate>
                                                            <a href="BatchWiseTransactionDetailsForCBSChecker.aspx?TransactionEDRID=<%#DataBinder.Eval(Container.DataItem, "TransactionID")%>">Batch Details</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo">
                                                        <HeaderStyle Wrap="True" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="BatchType" HeaderText="Type" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="CompanyName" HeaderText="Company Name">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SECC" HeaderText="SECC">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="True" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="EntryDesc" HeaderText="Description">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Currency" HeaderText="Currency">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <FooterStyle CssClass="GrayBackWhiteFont" />
                                                <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Position="TopAndBottom" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall" />
                                                <HeaderStyle CssClass="GrayBackWhiteFont" />
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>

                </table>
            </div>
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px; height: 40px; margin-left: 15px">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsgExportTransactionSTDO" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbxAllTransactionSentSTDO" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAllTransactionSentSTDO_CheckedChanged" />
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td style="width: 20px"></td>
                        <td>
                            <asp:LinkButton ID="PrintVoucherBtnSTDO" runat="server" CssClass="CommandButton" Text="Print Vouchers" OnClick="PrintVoucherBtnSTDO_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblSTDTxn" runat="server" CssClass="NormalBold" Text="STDO Transaction Sent"></asp:Label>
                            <div align="center" id="Div3" runat="server" style="position: relative; overflow: auto; width: 940px; margin-top: 15px; height: 300px;">
                                <table>
                                    <tr>
                                        <td class="NormalBold" align="center">
                                            <asp:DataGrid ID="dtgBatchTransactionSentSTDO" runat="Server" BorderWidth="0px" GridLines="None"
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID" AllowPaging="True" PageSize="500" OnPageIndexChanged="dtgBatchTransactionSentSTDO_PageIndexChanged">
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="SL.">
                                                        <ItemTemplate>
                                                            <%#(dtgBatchTransactionSentSTDO.PageSize * dtgBatchTransactionSentSTDO.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbxSentBatchTSSTDO" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="">
                                                        <ItemTemplate>
                                                            <a href="BatchWiseTransactionDetailsForCBSChecker.aspx?TransactionEDRID=<%#DataBinder.Eval(Container.DataItem, "TransactionID")%>">Batch Details</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo">
                                                        <HeaderStyle Wrap="True" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="BatchType" HeaderText="Type" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField="CompanyName" HeaderText="Company Name">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SECC" HeaderText="SECC">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="True" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="EntryDesc" HeaderText="Description">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Currency" HeaderText="Currency">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <FooterStyle CssClass="GrayBackWhiteFont" />
                                                <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Position="TopAndBottom" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall" />
                                                <HeaderStyle CssClass="GrayBackWhiteFont" />
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>

                </table>
            </div>
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px; height: 80px; margin-left: 15px; margin-bottom: 20px;">
                <table>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnAccept" runat="server" Text="Accept" CssClass="CommandButton"
                                OnClientClick="return confirm('Are you sure you want to accept the batch?')" OnClick="btnAccept_Click"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="CommandButton"
                                OnClientClick="return confirm('Are you sure you want to reject the batch?')" OnClick="btnReject_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRejectInstruction" runat="server" CssClass="NormalBold" Text="For a rejection item(s) please give a reason"></asp:Label>
                            <asp:Label ID="lblNoReturnReason" runat="server" Text="Please Enter a return reason"
                                Visible="false"></asp:Label>
                        </td>
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
