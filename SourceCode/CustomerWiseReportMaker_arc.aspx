<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerWiseReportMaker_arc.aspx.cs" Inherits="EFTN.CustomerWiseReportMaker_arc" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customer Wise Report</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">

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
    <form id="form1" method="post" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Authorizer Page
            </div>
            <div>
                <table style="padding-top: 15px">
                    <tr height="20px">
                        <td width="30px"></td>
                        <td>
                            <a href="EFTChecker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image10','','images/EFTCheckerOn.gif',1)">
                                <img src="images/EFTChecker.gif" name="Image10" width="149" height="25" border="0" id="Image10" /></a>

                        </td>
                        <td width="100px"></td>
                        <td>
                            <a href="EFTCheckerEBBS.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image11','','images/EFTCheckerEBBSOn.gif',1)">
                                <img src="images/EFTCheckerEBBS.gif" name="Image11" width="149" height="25" border="0" id="Image11" /></a>

                        </td>
                        <td width="100px"></td>
                        <td>
                            <a href="EFTCheckerAuthorizer.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image12','','images/EFTCheckerAuthorizerOn.gif',1)">
                                <img src="images/EFTCheckerAuthorizer.gif" name="Image12" width="149" height="25" border="0" id="Image12" /></a>

                        </td>
                        <td width="100px"></td>
                        <td>
                            <a href="EFTCheckerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image13','','images/ReportOn.gif',1)">
                                <img src="images/Report.gif" name="Image13" width="149" height="25" border="0" id="Image13" /></a>

                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px; height: 140px; margin-left: 15px">
                <div id="t1" style="margin-left: 105px">
                    <table>
                        <tr>
                            <%--<td class="NormalBold">
                                                <asp:Label ID="lblFromDay" runat="server" Text="From Date"></asp:Label>
                                            </td>--%>
                            <td>
                                <table>
                                    <tr>
                                        <%--<td class="NormalBold">
                                                            <asp:Label ID="lblFromDate" runat="server" Text="From Day"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlistFromDay" runat="server">
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
                                        --%>
                                        <td class="NormalBold">
                                            <asp:Label ID="Label1" runat="server" Text="From Month"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistFromMonth" runat="server">
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
                                            <asp:Label ID="Label46" runat="server" Text="FromYear"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistFromYear" runat="server">
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
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                            </td>

                            <td>
                                <asp:Button ID="ExpotToCSVBtn" runat="server" Text="Export to CSV" OnClick="ExpotToCSVBtn_Click" />
                            </td>
                            <td>
                                <asp:Button ID="ExpotToPDFBtn" runat="server" Text="Export to PDF" OnClick="ExpotToPdfBtn_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="t2" style="margin-left: -160px">
                    <%-- <table style="margin-left: -16px">
                                <tr>--%>
                    <%-- <td class="NormalBold">
                                &nbsp;</td>
                                <td>--%>
                    <table>
                        <tr style="padding-left: 10px">
                            <%--<td class="NormalBold">
                                                    <asp:Label ID="lblToDate" runat="server" Text="To Day"></asp:Label>
                                                </td>
                                                 <td>
                                                    <asp:DropDownList ID="ddlistToDay" runat="server">
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
                                                </td>--%>
                            <%--<td class="NormalBold">
                                                &nbsp;</td>--%>
                            <%--  <td class="NormalBold">
                                                &nbsp;</td> --%>
                            <td class="NormalBold">
                                <asp:Label ID="Label4" runat="server" Text="To Month"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistToMonth" runat="server">
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
                            <%-- <td class="NormalBold">
                                                &nbsp;</td>--%>
                            <td class="NormalBold">&nbsp;</td>
                            <td class="NormalBold">
                                <asp:Label ID="Label5" runat="server" Text="To Year"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistToYear" runat="server" Width="55px">
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
                </div>
                <div id="t3" style="margin-left: -135px">
                    <table>
                        <tr style="float: left">
                            <%--<td class="NormalBold">
                                             &nbsp;
                                        </td>
                                        <td class="NormalBold">
                                                &nbsp;</td>
                                        <td class="NormalBold">
                                        &nbsp;</td>
                            --%>
                            <td style="float: left">
                                <asp:Label ID="Label47" runat="server" Width="90px" Text="Account No"></asp:Label>
                            </td>
                            <td style="float: left">
                                <asp:TextBox ID="txtAccountNo" runat="server" Width="150px">
                                </asp:TextBox>
                            </td>
                            <td style="float: left">
                                <asp:Label ID="Label2" runat="server" Width="90px" Text="Report Option"></asp:Label>
                            </td>
                            <td style="float: left">
                                <asp:DropDownList ID="ddlistReportOption" runat="server" Width="100px">
                                    <asp:ListItem Value="1">All</asp:ListItem>
                                    <asp:ListItem Value="2">Outward</asp:ListItem>
                                    <asp:ListItem Value="3">Inward</asp:ListItem>
                                    <asp:ListItem Value="4">Outward Return</asp:ListItem>
                                    <asp:ListItem Value="5">Inward Retuen</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="float: left">
                                <asp:Label ID="lblbank" runat="server" Width="80px" Text="Bank Name"></asp:Label>
                            </td>
                            <td style="float: left">
                                <asp:DropDownList ID="ddListBank" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>

                    </table>
                    <%--    </td>
                        </tr>
                    </table>--%>
                </div>
                <div id="t4">
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="rdoReportOption" CssClass="Normal" RepeatDirection="Horizontal"
                                    runat="server"
                                    OnSelectedIndexChanged="rdoReportOption_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="1">Client Wise</asp:ListItem>
                                    <asp:ListItem Value="2">Bank Wise</asp:ListItem>
                                    <asp:ListItem Value="3">Month Wise</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Label ID="Label3" runat="server" Width="200px" Text="(From Archive Database)"></asp:Label>
            </div>

            <div id="AvailDiv" runat="server" style="position: relative; overflow: auto; padding-top: 10px; width: 940px; margin-top: 10px; margin-left: 15px">
                <asp:Label ID="lblStatusReport" CssClass="NormalRed" runat="Server"></asp:Label><br />
                <asp:DataGrid ID="dtgCustomerWiseReport_arc" runat="Server" BorderWidth="0px" Width="900px"
                    GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ShowFooter="True"
                    FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader"
                    ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff"
                    ItemStyle-CssClass="Normal" AllowPaging="True" PageSize="500" OnPageIndexChanged="dtgCustomerWiseReport_arc_PageIndexChanged">
                    <AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="SL.">
                            <HeaderStyle Wrap="true" Width="20px" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>
                            <ItemTemplate>
                                <%#(dtgCustomerWiseReport_arc.PageSize * dtgCustomerWiseReport_arc.CurrentPageIndex) + Container.ItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="ClientName" HeaderText="Client" ItemStyle-Wrap="False"
                            HeaderStyle-Wrap="False">
                            <HeaderStyle Wrap="true" Width="100px" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>

                            <ItemStyle Wrap="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Accountno" HeaderText="Account No">
                            <HeaderStyle Width="60px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BankCode" HeaderText="Bank code">
                            <HeaderStyle Width="20px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BankName" HeaderText="Bank Name">
                            <HeaderStyle Width="100px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerSegment" HeaderText="Segment" ItemStyle-Wrap="False"
                            HeaderStyle-Wrap="true">
                            <HeaderStyle Wrap="False" Width="40px" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>

                            <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SEGMENT" HeaderText="Segment Code" ItemStyle-Wrap="true"
                            HeaderStyle-Wrap="true">
                            <HeaderStyle Wrap="true" Width="15px" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>

                            <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Master" HeaderText="Company Master" ItemStyle-Wrap="False"
                            HeaderStyle-Wrap="true">
                            <HeaderStyle Wrap="False" Width="25px" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>

                            <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BDMName" HeaderText="RM/BDMName" ItemStyle-Wrap="False"
                            HeaderStyle-Wrap="true">
                            <HeaderStyle Wrap="False" Width="30px" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>

                            <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NOTranPayment" HeaderText="NO Txn Payment" ItemStyle-Wrap="true"
                            HeaderStyle-Wrap="true">
                            <HeaderStyle Wrap="true" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                Width="10px"></HeaderStyle>

                            <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TotalPayment" DataFormatString="{0:0.00}" HeaderText="Total Payment Amount" ItemStyle-Wrap="False"
                            HeaderStyle-Wrap="true">
                            <HeaderStyle Wrap="False" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                Width="100px"></HeaderStyle>

                            <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NOTranCollection" HeaderText="NO Txn Collection" ItemStyle-Wrap="true"
                            HeaderStyle-Wrap="true">
                            <HeaderStyle Wrap="true" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                Width="10px"></HeaderStyle>

                            <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TotalCollection" DataFormatString="{0:0.00}"
                            HeaderText="Total Collection Amount" ItemStyle-Wrap="False"
                            HeaderStyle-Wrap="true">
                            <HeaderStyle Wrap="False" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                Width="100px"></HeaderStyle>

                            <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>

                    <FooterStyle CssClass="GrayBackWhiteFont"></FooterStyle>

                    <HeaderStyle CssClass="GrayBackWhiteFontFixedHeader"></HeaderStyle>

                    <ItemStyle BackColor="#DEE9FC" CssClass="Normal"></ItemStyle>
                    <PagerStyle Mode="NumericPages" Position="Top" />
                </asp:DataGrid>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />


    </form>
</body>
</html>
