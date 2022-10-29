<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EFTCheckerAuthorizer.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="EFTN.EFTCheckerAuthorizer" %>

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
    <meta id="metaref" runat="server" http-equiv="refresh" content="600" />
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
            <br />
            <div style="overflow: scroll">
                <table width="900px" cellspacing="30">
                    <tr>
                        <td valign="top" class="NormalBold" bgcolor="aliceblue" style="border: solid 1px #666666; width: 200px"
                            align="center">
                            <asp:LinkButton ID="linkBtnImportTransaction" runat="server" Text="Import Transaction"
                                OnClientClick="return confirm('Are you sure you want to import?')"
                                OnClick="linkBtnImportTransaction_Click"></asp:LinkButton>
                            <br />
                            <asp:DataList ID="dtlImportTransaction" runat="Server" ItemStyle-CssClass="Normal"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" BorderWidth="0" CellSpacing="1"
                                CellPadding="2">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"FileName")%>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:Label ID="lblMsgTransactionReceived" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width: 10px"></td>
                        <td valign="top" class="NormalBold" style="border: solid 1px #666666; width: 200px; text-align: center; background-color: lightyellow">
                            <asp:LinkButton ID="linkBtnImportReturn" runat="server" Text="Import Return"
                                OnClientClick="return confirm('Are you sure you want to import?')"
                                OnClick="linkBtnImportReturn_Click"></asp:LinkButton>
                            <br />
                            <asp:DataList ID="dtlImportReturn" runat="Server" ItemStyle-CssClass="Normal" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Wrap="false" BorderWidth="0" CellSpacing="1" CellPadding="2">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"FileName")%>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:Label ID="lblImportReturn" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width: 10px"></td>
                        <td valign="top" class="NormalBold" bgcolor="aliceblue" style="border: solid 1px #666666; width: 200px"
                            align="center">
                            <asp:LinkButton ID="linkBtnImportNOC" runat="server" Text="Import NOC"
                                OnClientClick="return confirm('Are you sure you want to import?')"
                                OnClick="linkBtnImportNOC_Click"></asp:LinkButton>
                            <br />
                            <asp:DataList ID="dtlImportNOC" runat="Server" ItemStyle-CssClass="Normal" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-BackColor="LightYellow" ItemStyle-Wrap="false" BorderWidth="0" CellSpacing="1"
                                CellPadding="2">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"FileName")%>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:Label ID="lblImportNOC" CssClass="NormalRed" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="NormalBold" bgcolor="aliceblue" style="border: solid 1px #666666; width: 200px"
                            align="center">
                            <asp:LinkButton ID="linkBtnContested" runat="server" Text="Import Contested"
                                OnClientClick="return confirm('Are you sure you want to import?')"
                                OnClick="linkBtnContested_Click"></asp:LinkButton>
                            <br />
                            <asp:DataList ID="dtListContested" runat="Server" ItemStyle-CssClass="Normal"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" BorderWidth="0" CellSpacing="1"
                                CellPadding="2">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"FileName")%>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:Label ID="lblContested" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width: 10px"></td>
                        <td valign="top" class="NormalBold" style="border: solid 1px #666666; width: 200px; text-align: center; background-color: lightyellow">
                            <asp:LinkButton ID="linkBtnDishonor" runat="server" Text="Import Dishonor"
                                OnClientClick="return confirm('Are you sure you want to import?')"
                                OnClick="linkBtnDishonor_Click"></asp:LinkButton>
                            <br />
                            <asp:DataList ID="dtListDishonor" runat="Server" ItemStyle-CssClass="Normal" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Wrap="false" BorderWidth="0" CellSpacing="1" CellPadding="2">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"FileName")%>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:Label ID="lblDishonor" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width: 10px"></td>
                        <td valign="top" class="NormalBold" bgcolor="aliceblue" style="border: solid 1px #666666; width: 200px"
                            align="center">
                            <asp:LinkButton ID="linkBtnRNOC" runat="server" Text="Import RNOC"
                                OnClientClick="return confirm('Are you sure you want to import?')"
                                OnClick="linkBtnRNOC_Click"></asp:LinkButton>
                            <br />
                            <asp:DataList ID="dtListRNOC" runat="Server" ItemStyle-CssClass="Normal" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-BackColor="LightYellow" ItemStyle-Wrap="false" BorderWidth="0" CellSpacing="1"
                                CellPadding="2">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"FileName")%>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:Label ID="lblRNOC" CssClass="NormalRed" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="overflow: scroll">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Font-Bold="true" ForeColor="Red" Text="Error Files"></asp:Label>
                                        <div style="overflow: scroll; height: 200px;">
                                            <table>
                                                <tr>
                                                    <td class="NormalBold" align="center">
                                                        <asp:DataGrid ID="dtGridTransactionError" runat="Server" BorderWidth="0px" GridLines="None"
                                                            AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                            FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                            ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="FilePath" OnItemCommand="dtGridTransactionError_ItemCommand">
                                                            <Columns>
                                                                <asp:HyperLinkColumn DataTextField="FileName"></asp:HyperLinkColumn>
                                                                <asp:ButtonColumn CommandName="DownloadTRErrorFiles" Text="Download" CausesValidation="false"></asp:ButtonColumn>
                                                                <asp:ButtonColumn CommandName="RemoveTRErrorFiles" Text="Delete" CausesValidation="false"></asp:ButtonColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                        <asp:Label ID="lblTransactionError" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>

                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Blue" Text="Transaction Sent Acknowledgement"></asp:Label>
                                        <div style="overflow: scroll; height: 200px;">
                                            <table>
                                                <tr>
                                                    <td class="NormalBold" align="center">
                                                        <asp:DataGrid ID="dtGridTransSentACK" runat="Server" BorderWidth="0px" GridLines="None"
                                                            AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                            FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                            ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="FilePath">
                                                            <Columns>
                                                                <asp:HyperLinkColumn DataTextField="FileName"></asp:HyperLinkColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                        <asp:Label ID="lblTransSentACK" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkBtnImportTransSentACK" CssClass="CommandButton" runat="server" Text="Import Acknowledgement" OnClick="lnkBtnImportTransSentACK_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td bgcolor="#ccccff">
                            <table border="1px">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTRSent" runat="server" CssClass="NormalBold" Text="Transaction Sent"></asp:Label>
                                        <div style="overflow: scroll; height: 200px;">
                                            <table>
                                                <tr>
                                                    <td class="NormalBold" align="center">
                                                        <asp:DataGrid ID="dtgBatchTransactionSent" runat="Server" BorderWidth="0px" GridLines="None"
                                                            AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                            FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                            ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID" AllowPaging="True" OnPageIndexChanged="dtgBatchTransactionSent_PageIndexChanged" PageSize="500">
                                                            <AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
                                                            <Columns>
                                                                <asp:TemplateColumn>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbxSentBatchTS" runat="server" />
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
                                                                    HeaderStyle-Wrap="False">
                                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                                </asp:BoundColumn>

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
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="lblMsgExportTransaction" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="cbxAllTransactionSent" runat="server" Text="Select All" CssClass="NormalBold"
                                                        AutoPostBack="true" OnCheckedChanged="cbxAllTransactionSent_CheckedChanged" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnGenerateXmlForTransactionSent" runat="server" Text="Generate Envelope"
                                                        CssClass="CommandButton" OnClientClick="return confirm('Are you sure you want to generate the envelop to export?')"
                                                        OnClick="btnGenerateXmlForTransactionSent_Click" />
                                                </td>
                                                <td></td>
                                                <td>
                                                    <asp:LinkButton ID="btnDeleteBatchSent" runat="server" Text="Delete" 
                                                        CssClass="CommandButton"
                                                        OnClientClick="return confirm('Are you sure you want to delete the batch?')"
                                                        OnClick="btnDeleteBatchSent_Click"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td bgcolor="#ffffcc">
                            <table border="1px">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" CssClass="NormalBold" Text="Return Sent"></asp:Label>
                                        <div style="overflow: scroll; height: 200px;">
                                            <table>
                                                <tr>
                                                    <td class="NormalBold" align="center">
                                                        <asp:DataGrid ID="dtgBatchReturnSent" runat="Server" BorderWidth="0px" GridLines="None"
                                                            AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                                            HeaderStyle-CssClass="GrayBackWhiteFont" ItemStyle-BackColor="#dee9fc"
                                                            ItemStyle-CssClass="Normal" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID">
                                                            <Columns>
                                                                <asp:TemplateColumn>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbxSentBatchReturn" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="true" />
                                                                <asp:BoundColumn DataField="SECC" HeaderText="SECC" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}"
                                                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="EntryDesc" HeaderText="Description" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="False" />
                                                               <%-- <asp:BoundColumn DataField="SessionID" HeaderText="Session" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="False" />--%>
                                                                <asp:BoundColumn DataField="Currency" HeaderText="Currency" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}"
                                                                    ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblMsgExportReturn" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="cbxAllReturn" runat="server" Text="Select All" CssClass="NormalBold"
                                                        AutoPostBack="true" OnCheckedChanged="cbxAllReturn_CheckedChanged" />
                                                    <asp:LinkButton ID="btnGenerateXmlForReturn" runat="server" Text="Generate Envelope"
                                                        CssClass="CommandButton" OnClientClick="return confirm('Are you sure you want to generate the envelop to export?')"
                                                        OnClick="btnGenerateXmlForReturn_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td bgcolor="#ffffff">
                            <table border="1px">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" CssClass="NormalBold" Text="NOC Sent"></asp:Label>
                                        <div id="Div2" runat="server" style="overflow: scroll; height: 200px;">
                                            <table>
                                                <tr>
                                                    <td class="NormalBold" align="center">
                                                        <asp:DataGrid ID="dtgBatchNOCSent" runat="Server" BorderWidth="0px" GridLines="None"
                                                            AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                                            HeaderStyle-CssClass="GrayBackWhiteFont" ItemStyle-BackColor="#dee9fc"
                                                            ItemStyle-CssClass="Normal" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID">
                                                            <Columns>
                                                                <asp:TemplateColumn>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbxSentBatchNOC" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="true" />
                                                                <asp:BoundColumn DataField="SECC" HeaderText="SECC" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}"
                                                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="EntryDesc" HeaderText="Description" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}"
                                                                    ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblExportNOC" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="cbxAllNOCExport" runat="server" Text="Select All" CssClass="NormalBold"
                                            AutoPostBack="true" OnCheckedChanged="cbxAllNOCExport_CheckedChanged" />
                                        <asp:LinkButton ID="btnGenerateXmlForNOC" runat="server" Text="Generate Envelope"
                                            CssClass="CommandButton" OnClientClick="return confirm('Are you sure you want to generate the envelop to export?')"
                                            OnClick="btnGenerateXmlForNOC_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#cccccc">
                            <table border="1px">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" CssClass="NormalBold" Text="Dishonor Sent"></asp:Label>
                                        <div id="Div3" runat="server" style="overflow: scroll; height: 200px;">
                                            <table>
                                                <tr>
                                                    <td class="NormalBold" align="center" colspan="2">
                                                        <asp:DataGrid ID="dtgBatchDishonorSent" runat="Server" BorderWidth="0px" GridLines="None"
                                                            AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                                            HeaderStyle-CssClass="GrayBackWhiteFont" ItemStyle-BackColor="#dee9fc"
                                                            ItemStyle-CssClass="Normal" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID">
                                                            <Columns>
                                                                <asp:TemplateColumn>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbxSentBatchDishonor" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="true" />
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
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblExportDishonor" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="cbxAllDihonorSent" runat="server" Text="Select All" CssClass="NormalBold"
                                            AutoPostBack="true" OnCheckedChanged="cbxAllDihonorSent_CheckedChanged" />
                                        <asp:LinkButton ID="btnGenerateXmlForDishonor" runat="server" Text="Generate Envelope"
                                            CssClass="CommandButton" OnClientClick="return confirm('Are you sure you want to generate the envelop to export?')"
                                            OnClick="btnGenerateXmlForDishonor_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td bgcolor="#9999ff">
                            <table border="1px">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" CssClass="NormalBold" Text="RNOC Sent"></asp:Label>
                                        <div id="Div5" runat="server" style="overflow: scroll; height: 200px;">
                                            <table>
                                                <tr>
                                                    <td class="NormalBold" align="center" colspan="2">
                                                        <asp:DataGrid ID="dtgRNOCBatch" runat="Server" BorderWidth="0px" GridLines="None"
                                                            AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                                            HeaderStyle-CssClass="GrayBackWhiteFont" ItemStyle-BackColor="#dee9fc"
                                                            ItemStyle-CssClass="Normal" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID">
                                                            <Columns>
                                                                <asp:TemplateColumn>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbxRNOCBatch" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="SECC" HeaderText="SECC" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}"
                                                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="EntryDesc" HeaderText="Description" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}"
                                                                    ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblExportRNOC" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="cbxAllRNOCSent" runat="server" Text="Select All" CssClass="NormalBold"
                                            AutoPostBack="true" OnCheckedChanged="cbxAllRNOCSent_CheckedChanged" />
                                        <asp:LinkButton ID="linkBtnGenerateXMLForRNOCSent" runat="server" Text="Generate Envelope"
                                            CssClass="CommandButton" OnClientClick="return confirm('Are you sure you want to generate the envelop to export?')"
                                            OnClick="linkBtnGenerateXMLForRNOCSent_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td bgcolor="#ffccff">
                            <table border="1px">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" CssClass="NormalBold" Text="Dishonor Contested Sent"></asp:Label>
                                        <div style="overflow: scroll; height: 200px;">
                                            <table>
                                                <tr>
                                                    <td class="NormalBold" align="center" colspan="2">
                                                        <asp:DataGrid ID="dtgBatchContestedDishonor" runat="Server" BorderWidth="0px" GridLines="None"
                                                            AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                                            HeaderStyle-CssClass="GrayBackWhiteFont" ItemStyle-BackColor="#dee9fc"
                                                            ItemStyle-CssClass="Normal" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID">
                                                            <Columns>
                                                                <asp:TemplateColumn>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbxSentBatchContested" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="true" />
                                                                <asp:BoundColumn DataField="SECC" HeaderText="SECC" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}"
                                                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="EntryDesc" HeaderText="Description" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total" ItemStyle-Wrap="False"
                                                                    HeaderStyle-Wrap="False" />
                                                                <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}"
                                                                    ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblDishonorContested" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="cbxAllContestedSent" runat="server" Text="Select All" CssClass="NormalBold"
                                            AutoPostBack="true" OnCheckedChanged="cbxAllContestedSent_CheckedChanged" />
                                        <asp:LinkButton ID="btnGenerateXmlForContestedSent" runat="server" Text="Generate Envelope"
                                            CssClass="CommandButton" OnClientClick="return confirm('Are you sure you want to generate the envelop to export?')"
                                            OnClick="btnGenerateXmlForContestedSent_Click" />
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
