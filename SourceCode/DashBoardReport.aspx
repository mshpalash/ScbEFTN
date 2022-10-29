<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashBoardReport.aspx.cs" Inherits="EFTN.DashBoardReport" %>
<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Dash Board Report Page</title>
 <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
               Dash Board Report</div>
            <div>
                <table style="padding-top: 15px">
                    <tr height="20px">
                        <td width="30px">
                        </td>
                        <td>
                            <a href="EFTChecker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image10','','images/EFTCheckerOn.gif',1)">
                                <img src="images/EFTChecker.gif" name="Image10" width="149" height="25" border="0"
                                    id="Image10" /></a>
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerEBBS.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image11','','images/EFTCheckerEBBSOn.gif',1)">
                                <img src="images/EFTCheckerEBBS.gif" name="Image11" width="149" height="25" border="0"
                                    id="Image11" /></a>
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerAuthorizer.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image12','','images/EFTCheckerAuthorizerOn.gif',1)">
                                <img src="images/EFTCheckerAuthorizer.gif" name="Image12" width="149" height="25"
                                    border="0" id="Image12" /></a>
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image13','','images/ReportOn.gif',1)">
                                <img src="images/Report.gif" name="Image13" width="149" height="25" border="0" id="Image13" /></a>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px;
                height: 40px; margin-left: 15px">
                <table>
                    <tr>
                        <td class="NormalBold">
                            <asp:Label ID="lblDay" runat="server" Text="Date"></asp:Label>
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
                                    <td style="width: 62px">
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
                        <td>
                            <asp:DropDownList ID="ddlDashBoard" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDashBoard_SelectedIndexChanged">
                                <asp:ListItem Value="1">Inward</asp:ListItem>
                                <asp:ListItem Value="2">Outward</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="ddlDashBoardReport" runat="server">
                                <asp:ListItem Value="1">Inward Transaction</asp:ListItem>
                                <asp:ListItem Value="2">Return Sent</asp:ListItem>
                                <asp:ListItem Value="3">NOC Sent</asp:ListItem>
                                <asp:ListItem Value="4">Inward Dishonor</asp:ListItem>
                                <asp:ListItem Value="5">Contested Sent</asp:ListItem>
                                <asp:ListItem Value="6">Inward RNOC</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddldashBoardOutward" runat="server">
                                <asp:ListItem Value="7">Transaction Sent</asp:ListItem>
                                <asp:ListItem Value="8">Inward Return</asp:ListItem>
                                <asp:ListItem Value="9">Inward NOC</asp:ListItem>
                                <asp:ListItem Value="10">Dishonor Sent</asp:ListItem>
                                <asp:ListItem Value="11">Inward Contested</asp:ListItem>
                                <asp:ListItem Value="12">RNOC Sent</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>
                        </td>
                        <td>
                            <asp:Button ID="ExpotToPdfBtn" runat="server" Text="Export to PDF" OnClick="ExpotToPdfBtn_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="overflow: auto; width: 940px; height: 450px; margin-left: 100px; margin-left: 15px">
                <asp:Label ID="lblStatusReport" CssClass="NormalRed" runat="Server"></asp:Label><br />
                <asp:DataGrid ID="dtgDashBoardReport" runat="Server" Width="600" BorderWidth="0px"
                    GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1"
                    ShowFooter="true" FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                    ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" ItemStyle-CssClass="Normal">
                    <Columns>
                        <asp:BoundColumn DataField="BranchName" HeaderText="Branch Name" ItemStyle-Wrap="True"
                            HeaderStyle-Wrap="False" />
                             <asp:BoundColumn DataField="DepartmentName" HeaderText="DepartmentName" ItemStyle-Wrap="True"
                            HeaderStyle-Wrap="False" />
                        <asp:BoundColumn DataField="RoutingNo" HeaderText="Routing No" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="IMItem" HeaderText="Total Items in Maker" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="IMAmt" HeaderText="Total Amount in Maker" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="IMBDAItem" HeaderText="By Default Approved Item" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="IMBDAAmt" HeaderText="By Default Approved Amount" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="ICItem" HeaderText="Total Items in Checker" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="ICAmt" HeaderText="Total Amount in Checker" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="IEBBSItem" HeaderText="Total Items in CBS" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="IEBBSAmt" HeaderText="Total Amount in CBS" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="IEBBSAItem" HeaderText="Total Items Approved By CBS" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="IEBBSAAmt" HeaderText="Total Amount Approved By CBS" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="CAItem" HeaderText="Total Items in Checker Authorizer" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="CAAAmt" HeaderText="Total Amounts Checker Authorizer" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="CAPItem" HeaderText="Total Item Sent By Checker Authorizer" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                        <asp:BoundColumn DataField="CAPAAmt" HeaderText="Total Amount Sent By Checker Authorizer" ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
