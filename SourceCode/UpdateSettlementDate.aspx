<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateSettlementDate.aspx.cs" Inherits="EFTN.UpdateSettlementDate" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Transaction Received Files</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script language="javascript" type="text/javascript">
    javascript:window.history.forward(1);
</script>
</head>
<body class="wrap" id="content">
   
<form id="form1" runat="server">
<div class="maincontent">
<uc1:Header ID="Header1" runat="server" />
<div class="Head" align="center">
            Authorizer Page</div>
            <div>
                <table style="padding-top:15px">
                    <tr height="20px">
                        <td width="30px">
                        </td>
                        <td>
                            <a href="EFTChecker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image10','','images/EFTCheckerOn.gif',1)"><img src="images/EFTChecker.gif" name="Image10" width="149" height="25" border="0" id="Image10" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerEBBS.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image11','','images/EFTCheckerEBBSOn.gif',1)"><img src="images/EFTCheckerEBBS.gif" name="Image11" width="149" height="25" border="0" id="Image11" /></a>
                    
                                                </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerAuthorizer.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image12','','images/EFTCheckerAuthorizerOn.gif',1)"><img src="images/EFTCheckerAuthorizer.gif" name="Image12" width="149" height="25" border="0" id="Image12" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                        <a href="EFTCheckerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image13','','images/ReportOn.gif',1)"><img src="images/Report.gif" name="Image13" width="149" height="25" border="0" id="Image13" /></a>
                                                      
                        </td>                    
                    </tr>
                </table>
            </div>
    <div align="center" class="boxmodule" style="padding-top:10px; width:500px; margin-top:10px; height:300px; margin-left:220px">
        <table>
            <tr>
                <td align="right">
                    <asp:Label ID="lblTransactionType" runat="server" Text="Transaction Type :"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddListTransactionType" runat="server">
                        <asp:ListItem Text="Transaction Sent" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Return Sent" Value="2"></asp:ListItem>
                        <asp:ListItem Text="NOC Sent" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Dishonor Sent" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Inward Transaction" Value="8"></asp:ListItem>
                        <asp:ListItem Text="Return Received" Value="6"></asp:ListItem>
                        <asp:ListItem Text="NOC Received " Value="7"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblEntryDate" runat="server" Text="Date (EntryDate for Outward/ SettelementDate for Inward) :"></asp:Label>
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
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSettlementDate" runat="server" Text="Update Settelment Date to :"></asp:Label>
                </td>
                <td>
                    <asp:Calendar ID="clnderSettlementDate" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px">
                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                        <WeekendDayStyle BackColor="#CCCCFF" />
                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True"
                            Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                    </asp:Calendar>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    
                </td>
            </tr>
        </table>
    </div>
    <div align="center" class="boxmodule" style="padding-top:10px; width:300px; margin-top:10px; height:40px; margin-left:330px">
    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnUpdate" Text="Update" runat="server" OnClick="btnUpdate_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancel" Text="Cencel" runat="server" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
    </div>
</div>
    <uc2:footer ID="Footer1" runat="server" />
    
</form>
</body>
</html>
