<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuperAdminCheckerAuditUser.aspx.cs"
    Inherits="FloraSoft.SuperAdminCheckerAuditUser" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="modules/SuperAdminCheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Management</title>
    <link href="includes/sitec.css" rel="stylesheet" type="text/css" />
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
    <div class="maincontent">
        <uc1:Header ID="Header" runat="server" />
        <div class="Head" align="center">
            User Audit Report</div>
        <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px;
            height: 90px; margin-left: 15px">
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td class="NormalBold">
                                    <asp:Label ID="lblDay" runat="server" Text="Begin Date"></asp:Label>
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
                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date"></asp:Label>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td class="NormalBold">
                                                <asp:Label ID="Label2" runat="server" Text="Day"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlistDayEnd" runat="server">
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
                                                <asp:Label ID="Label6" runat="server" Text="Month"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlistMonthEnd" runat="server">
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
                                                <asp:Label ID="Label7" runat="server" Text="Year"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlistYearEnd" runat="server">
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
                        </table>
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnExpotToExcel" runat="server" Text="CSV File" 
                            onclick="btnExpotToExcel_Click" />
                    </td>
                   <td>
                    <asp:Button ID="btnExportToPDF" runat="server" Text="PDF" OnClick="btnExportToPDF_Click" CausesValidation="false"/>
                </td>                    

                </tr>
            </table>
        </div>
        <div align="center" class="boxmodule" style="position: relative; overflow: auto; padding-top: 10px; width: 940px; margin-top: 10px;
            height: 400px; margin-left: 15px">
            <asp:DataGrid ID="MyDataGrid" HeaderStyle-CssClass="GrayBackWhiteFont" FooterStyle-CssClass="GrayBackWhiteFont"
                ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#CAD2FD" AlternatingItemStyle-BackColor="#FFFFFF"
                runat="server" CellSpacing="1" CellPadding="5" AutoGenerateColumns="false"
                GridLines="None" BorderWidth="0px" ShowFooter="true" Height="0px" HeaderStyle-ForeColor="#FFFFFF"
                AllowPaging="True" AllowSorting="True" 
                OnPageIndexChanged="MyDataGrid_PageIndexChanged" 
                OnSortCommand="MyDataGrid_SortCommand" PageSize="500">
                <Columns>
                    <asp:BoundColumn DataField="HistoryTime" HeaderText="ActionTime" ItemStyle-Wrap="False"
                        HeaderStyle-Wrap="False" SortExpression="HistoryTime" />
                    <asp:BoundColumn DataField="IPAddress" HeaderText="IPAddress" ItemStyle-Wrap="False"
                        HeaderStyle-Wrap="False" SortExpression="IPAddress" />
                    <asp:BoundColumn DataField="ActionName" HeaderText="ActionName" ItemStyle-Wrap="False"
                        HeaderStyle-Wrap="False" SortExpression="ActionName" />
                    <asp:BoundColumn DataField="RoleName" HeaderText="RoleName" ItemStyle-Wrap="False"
                        HeaderStyle-Wrap="False" SortExpression="RoleName" />
                    <asp:BoundColumn DataField="UserName" HeaderText="UserName" ItemStyle-Wrap="False"
                        HeaderStyle-Wrap="False" SortExpression="UserName" />
                    <asp:BoundColumn DataField="LoginID" HeaderText="LoginID" ItemStyle-Wrap="False"
                        HeaderStyle-Wrap="False" SortExpression="LoginID" />
                    <asp:BoundColumn DataField="UserStatus" HeaderText="UserStatus" ItemStyle-Wrap="False"
                        HeaderStyle-Wrap="False" SortExpression="UserStatus" />
                </Columns>
            </asp:DataGrid>
        </div>
        <div>
            <asp:Label runat="server" ID="lblTestCookie"></asp:Label>
        </div>
    </div>
    <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
