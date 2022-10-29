<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlatFileForTransactionSent.aspx.cs"
    Inherits="EFTN.FlatFileForTransactionSent" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Export to CBS</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">
        javascript: window.history.forward(1);
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
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Export to CBS
            </div>
            <div>
                <table style="padding-top: 15px;">
                    <tr height="20px">
                        <td width="200px"></td>
                        <td>
                            <a href="EFTMaker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/EFTMakerHomeOn.gif',1)">
                                <img src="images/EFTMakerHome.gif" name="Image1" width="149" height="25" border="0"
                                    id="Image1" /></a>
                        </td>
                        <td width="250px">&nbsp;</td>
                        <td>
                            <a href="EFTMakerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/ReportOn.gif',1)">
                                <img src="images/Report.gif" name="Image2" width="149" height="25" border="0" id="Image2" /></a>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 10px; width: 940px; margin-top: 10px; height: 40px; margin-left: 15px">
                <table>
                    <tr>
                        <td>
                            <asp:LinkButton ID="linkBtnTransactionSentCharge" Visible="false" runat="server" Text="Transaction Sent Charge Flat File" class="CommandButton" OnClick="linkBtnTransactionSentCharge_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px; height: 40px; margin-left: 15px">
                <table>
                    <tr>
                        <td width="10px"></td>
                        <td class="NormalBold">
                            <asp:Label ID="lblDay" runat="server" Text="Effective Entry Date"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td class="NormalBold">
                                        <asp:Label ID="lblDate" runat="server" Text="Day"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistDay" runat="server">
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="NormalBold">
                                        <asp:Label ID="Label1" runat="server" Text="Month"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistMonth" runat="server">
                                            <asp:ListItem Value="1">Jan</asp:ListItem>
                                            <asp:ListItem Value="2">Feb</asp:ListItem>
                                            <asp:ListItem Value="3">Mar</asp:ListItem>
                                            <asp:ListItem Value="4">Apr</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">Jun</asp:ListItem>
                                            <asp:ListItem Value="7">Jul</asp:ListItem>
                                            <asp:ListItem Value="8">Aug</asp:ListItem>
                                            <asp:ListItem Value="9">Sep</asp:ListItem>
                                            <asp:ListItem Value="10">Oct</asp:ListItem>
                                            <asp:ListItem Value="11">Nov</asp:ListItem>
                                            <asp:ListItem Value="12">Dec</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="NormalBold">
                                        <asp:Label ID="Label46" runat="server" Text="Year"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistYear" runat="server">
                                            <asp:ListItem>2011</asp:ListItem>
                                            <asp:ListItem>2012</asp:ListItem>
                                            <asp:ListItem>2013</asp:ListItem>
                                            <asp:ListItem>2014</asp:ListItem>
                                            <asp:ListItem>2015</asp:ListItem>
                                            <asp:ListItem>2016</asp:ListItem>
                                            <asp:ListItem>2017</asp:ListItem>
                                            <asp:ListItem>2018</asp:ListItem>
                                            <asp:ListItem>2019</asp:ListItem>
                                            <asp:ListItem>2020</asp:ListItem>
                                            <asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                            <asp:ListItem>2024</asp:ListItem>
                                            <asp:ListItem>2025</asp:ListItem>
                                            <asp:ListItem>2026</asp:ListItem>
                                            <asp:ListItem>2027</asp:ListItem>
                                            <asp:ListItem>2028</asp:ListItem>
                                            <asp:ListItem>2029</asp:ListItem>
                                            <asp:ListItem>2030</asp:ListItem>
                                            <asp:ListItem>2031</asp:ListItem>
                                            <asp:ListItem>2032</asp:ListItem>
                                            <asp:ListItem>2033</asp:ListItem>
                                            <asp:ListItem>2034</asp:ListItem>
                                            <asp:ListItem>2035</asp:ListItem>
                                            <asp:ListItem>2036</asp:ListItem>
                                            <asp:ListItem>2037</asp:ListItem>
                                            <asp:ListItem>2038</asp:ListItem>
                                            <asp:ListItem>2039</asp:ListItem>
                                            <asp:ListItem>2040</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                            </table>

                        </td>
                        <td class="NormalBold">
                            <asp:Label ID="lblSession" runat="server" Text="Session"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="SessionDdList" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnExportCSV" runat="server" Text="CSV" OnClick="btnExportCSV_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" id="AvailDiv" runat="server" style="position: relative; overflow: auto; width: 900px; margin-top: 15px; height: 300px;"
                title="Normal Transaction">
                <asp:Label ID="lblNormalTransaction" runat="server" Text="Normal Transaction"></asp:Label>
                <asp:DataGrid ID="dtgEFTChecker" AlternatingItemStyle-BackColor="lightyellow" AutoGenerateColumns="false"
                    BorderWidth="0px" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                    GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="#CAD2FD"
                    ItemStyle-CssClass="NormalSmall" runat="server" DataKeyField="TransactionID"
                    OnItemCommand="dtgEFTChecker_ItemCommand">
                    <Columns>
                        <asp:TemplateColumn HeaderText="SL.">
                            <ItemTemplate>
                                <%#(dtgEFTChecker.PageSize * dtgEFTChecker.CurrentPageIndex) + Container.ItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="BatchNumber">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BatchNumber")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="EffectiveEntryDate">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "EffectiveEntryDate")%>
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
                        <asp:TemplateColumn HeaderText="BatchType">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BatchType")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="EntryDesc">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "EntryDesc")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DataEntryType">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "DataEntryType")%>
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
                        <asp:TemplateColumn HeaderText="TotalTransactions">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "TotalTransactions")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="TotalAmount">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "TotalAmount")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:ButtonColumn CommandName="GenerateFlatFile" Text="Generate Flat File" CausesValidation="false"></asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="GenerateReverseFlatFile" Text="Generate Reverse Flat File" CausesValidation="false"></asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="GenerateAccumulatedFlatFile" Text="Generate Accumulated Flat File" CausesValidation="false"></asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="GenerateReverseAccumulatedFlatFile" Text="Generate Reverse Accumulated Flat File" CausesValidation="false"></asp:ButtonColumn>

                    </Columns>
                    <FooterStyle CssClass="GrayBackWhiteFont" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="LightYellow" />
                    <ItemStyle BackColor="#CAD2FD" CssClass="NormalSmall" />
                    <HeaderStyle CssClass="GrayBackWhiteFont" />
                </asp:DataGrid>
            </div>
            <div align="center" id="divSts" runat="server" style="position: relative; overflow: auto; width: 900px; margin-top: 15px; height: 300px;"
                title="STS Transaction">
                <asp:Label ID="lblSTSTXN" runat="server" Text="STS Transaction"></asp:Label>
                <asp:DataGrid ID="dtgTransactionListOfSTS" AlternatingItemStyle-BackColor="lightyellow" AutoGenerateColumns="false"
                    BorderWidth="0px" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                    GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="#CAD2FD"
                    ItemStyle-CssClass="NormalSmall" runat="server" DataKeyField="TransactionID" OnItemCommand="dtgTransactionListOfSTS_ItemCommand">
                    <Columns>
                        <asp:TemplateColumn HeaderText="SL.">
                            <ItemTemplate>
                                <%#(dtgTransactionListOfSTS.PageSize * dtgTransactionListOfSTS.CurrentPageIndex) + Container.ItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="false">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "PrintFlag")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="BatchNumber">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BatchNumber")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="EffectiveEntryDate">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "EffectiveEntryDate")%>
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
                        <asp:TemplateColumn HeaderText="BatchType">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BatchType")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="EntryDesc">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "EntryDesc")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DataEntryType">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "DataEntryType")%>
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
                        <asp:TemplateColumn HeaderText="TotalTransactions">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "TotalTransactions")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>                      
                        <asp:TemplateColumn HeaderText="TotalAmount">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "TotalAmount")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:ButtonColumn CommandName="GenerateFlatFileSTS" Text="Generate Flat File" CausesValidation="false"></asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="GenerateReverseFlatFileSTS" Text="Generate Reverse Flat File" CausesValidation="false"></asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="GenerateFlatFileSTSAccumulated" Text="Generate Accumulated Flat File" CausesValidation="false"></asp:ButtonColumn>
                    </Columns>
                    <FooterStyle CssClass="GrayBackWhiteFont" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="LightYellow" />
                    <ItemStyle BackColor="#CAD2FD" CssClass="NormalSmall" />
                    <HeaderStyle CssClass="GrayBackWhiteFont" />
                </asp:DataGrid>
            </div>
            <div align="center" id="divSTDO" runat="server" style="position: relative; overflow: auto; width: 900px; margin-top: 15px; height: 300px;"
                title="Standing Order Transaction">
                <asp:Label ID="lblSTDO" runat="server" Text="Standing Order Transaction"></asp:Label>
                &nbsp;&nbsp;
                    <asp:LinkButton ID="linkBtnAccumulateSTDOrderFlatFile" runat="server" CssClass="CommandButton" OnClick="linkBtnAccumulateSTDOrderFlatFile_Click">Click Here To Generate Accumulated Flat File</asp:LinkButton>
                <asp:DataGrid ID="dtgTransactionListOfSTDOrder" AlternatingItemStyle-BackColor="lightyellow" AutoGenerateColumns="false"
                    BorderWidth="0px" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                    GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="#CAD2FD"
                    ItemStyle-CssClass="NormalSmall" runat="server" DataKeyField="TransactionID" OnItemCommand="dtgTransactionListOfSTDOrder_ItemCommand">
                    <Columns>
                        <asp:TemplateColumn HeaderText="SL.">
                            <ItemTemplate>
                                <%#(dtgTransactionListOfSTDOrder.PageSize * dtgTransactionListOfSTDOrder.CurrentPageIndex) + Container.ItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="BatchNumber">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BatchNumber")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="EffectiveEntryDate">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "EffectiveEntryDate")%>
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
                        <asp:TemplateColumn HeaderText="BatchType">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BatchType")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="EntryDesc">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "EntryDesc")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DataEntryType">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "DataEntryType")%>
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
                        <asp:TemplateColumn HeaderText="TotalTransactions">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "TotalTransactions")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>                     
                        <asp:TemplateColumn HeaderText="TotalAmount">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "TotalAmount")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:ButtonColumn CommandName="GenerateFlatFileSTDOrder" Text="Generate Flat File" CausesValidation="false"></asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="GenerateReverseFlatFileSTOrder" Text="Generate Reverse Flat File" CausesValidation="false"></asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="GenerateAccumulatedFlatFileSTDOrder" Text="Generate Accumulated Flat File" CausesValidation="false"></asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="GenerateReverseAccumulatedFlatFileSTDOrder" Text="Generate Reverse Accumulated Flat File" CausesValidation="false"></asp:ButtonColumn>
                    </Columns>
                    <FooterStyle CssClass="GrayBackWhiteFont" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="LightYellow" />
                    <ItemStyle BackColor="#CAD2FD" CssClass="NormalSmall" />
                    <HeaderStyle CssClass="GrayBackWhiteFont" />
                </asp:DataGrid>
            </div>
            <div style="clear: both"></div>
            <div style="margin-top: 25px">
                <table style="padding-top: 15px">
                    <tr height="20">
                        <td width="200px"></td>
                        <td>
                            <a href="ChangeMakerPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ChangePasswordOn.gif',1)">
                                <img src="images/ChangePassword.gif" name="Image3" width="149" height="25" border="0"
                                    id="Image3" /></a>
                        </td>
                        <td width="250px"></td>
                        <td>
                            <a href="LogOut.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/SignOutOn.gif',1)">
                                <img src="images/SignOut.gif" name="Image4" width="149" height="25" border="0" id="Image4" /></a>
                        </td>
                    </tr>
                </table>
            </div>

            <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
