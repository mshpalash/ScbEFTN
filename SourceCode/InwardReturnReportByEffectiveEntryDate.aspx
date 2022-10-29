<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InwardReturnReportByEffectiveEntryDate.aspx.cs" Inherits="EFTN.InwardReturnReportByEffectiveEntryDate" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>BranchWise Settlement Report</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Authorizer Page</div>
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
            <br />
            <div align="center" class="NormalBold"c style="font-size:medium">Inward Return Report By Effective Entry Date Of Transaction Sent</div>            
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px;
                height: 70px; margin-left: 15px">
                <table>
                    <tr>
                        <td class="NormalBold" style="width: 147px">
                            <asp:DropDownList ID="ddListReportType" runat="server">
                                <asp:ListItem Value="1" Text="Entry Date of Transaction Sent" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Settlement Date of Inward Return"></asp:ListItem>
                            </asp:DropDownList>
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
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </td>
                        <td>
                            <asp:Button ID="ExpotToPdfBtn" runat="server" Text="Export to PDF" OnClick="ExpotToPdfBtn_Click"/>
                        </td>
                        <td>
                            <asp:Button ID="btnExpotToCSV" runat="server" Text="Export to CSV" OnClick="btnExpotToCSV_Click"/>                        
                        </td>                        
                    </tr>
                </table>
                 <table style="margin-left:28px;">
                        <tr>
                            <td class="NormalBold">
                                <asp:Label ID="lblCurrency" runat="server" Text="Currency"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="CurrencyDdList" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="NormalBold">
                                <asp:Label ID="lblSession" runat="server" Text="Session"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="SessionDdList" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
            </div>
          
             <div style="overflow: auto; width: 940px; margin-left: 100px; margin-left: 15px">
                <asp:Panel ID="pnlInwardtReturnRptByEffectiveEntryDate" runat="server" Height="400px" Width="900px">
                    <asp:DataGrid ID="dtgInwardtReturnRptByEffectiveEntryDate" runat="Server" AlternatingItemStyle-BackColor="lightyellow"
                        AutoGenerateColumns="False" BorderWidth="0px" CellPadding="5" CellSpacing="2"
                        FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" FooterStyle-HorizontalAlign="right"
                        HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="White"
                                            HeaderStyle-ForeColor="#FFFFFF" 
                        ItemStyle-CssClass="NormalSmall" ShowFooter="True" PagerStyle-Position="Top" OnPageIndexChanged="dtgInwardtReturnRptByEffectiveEntryDate_PageIndexChanged" PageSize="20" AllowPaging="True" AllowSorting="True">
                        <Columns>
                            <asp:TemplateColumn HeaderText="SL.">
                                <ItemTemplate>
                                    <%#(dtgInwardtReturnRptByEffectiveEntryDate.PageSize * dtgInwardtReturnRptByEffectiveEntryDate.CurrentPageIndex) + Container.ItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                             <asp:BoundColumn DataField="EntryDateTransactionSent" HeaderText="EntryDateTransactionSent" DataFormatString="{0:d}" SortExpression = "EntryDateTransactionSent" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                           
                            <asp:BoundColumn DataField="SettlementJDate" HeaderText="SettlementDate" DataFormatString="{0:d}" SortExpression = "SettlementJDate" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                           
                            <asp:BoundColumn DataField="EntryDateReturnReceived" HeaderText="DateOfReturnReceived" DataFormatString="{0:d}" SortExpression = "EntryDateReturnReceived" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BankName" HeaderText="BankName"  SortExpression = "BankName">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BranchName" HeaderText="BranchName" SortExpression = "BranchName">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SECC" HeaderText="SECC"  SortExpression = "SECC">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                              <asp:BoundColumn DataField="ReceiverName" HeaderText="ReceiverName"  SortExpression = "ReceiverName" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                             <asp:BoundColumn DataField="DFIAccountNo" HeaderText="DFIAccountNo"  SortExpression = "DFIAccountNo" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                             <asp:BoundColumn DataField="AccountNo" HeaderText="Sender AccountNo"  SortExpression = "DFIAccountNo" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Amount" DataFormatString="{0:N}" HeaderText="Amount"  SortExpression = "Amount" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Currency" HeaderText="Currency">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn> 
                             <asp:BoundColumn DataField="SessionID" HeaderText="Session">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>   
                            <asp:BoundColumn DataField="CompanyName" HeaderText="CompanyName"  SortExpression = "CompanyName" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>   
                            <asp:BoundColumn DataField="IdNumber" HeaderText="IdNumber"  SortExpression = "IdNumber" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                                                                                    
                            <asp:BoundColumn DataField="RejectReason" HeaderText="RejectReason"  SortExpression = "RejectReason" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TransactionCode" HeaderText="TransactionCode"  SortExpression = "TransactionCode" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                           
                            <asp:BoundColumn DataField="BankRoutingNo" HeaderText="BankRoutingNo"  SortExpression = "BankRoutingNo" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                          
                          
                        </Columns>
                        <FooterStyle CssClass="GrayBackWhiteFont" HorizontalAlign="Right" />
                        <PagerStyle Mode="NumericPages" Position="Top" />
                        <AlternatingItemStyle BackColor="LightYellow" />
                        <ItemStyle BackColor="White" CssClass="NormalSmall" />
                        <HeaderStyle CssClass="GrayBackWhiteFont" ForeColor="White" />
                    </asp:DataGrid>
                </asp:Panel>
            </div>
             <div style="padding-top:20px; padding-left:220px">
                <table>
                    <tr height="20px">
                        <td width="50px">
                        </td>
                        
                        <td>
                            <a href="ChangeCheckerPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image5','','images/ChangePasswordOn.gif',1)"><img src="images/ChangePassword.gif" name="Image5" width="149" height="25" border="0" id="Image5" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>                      
                        <td>
                            <a href="LogOut.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image6','','images/SignOutOn.gif',1)"><img src="images/SignOut.gif" name="Image6" width="149" height="25" border="0" id="Image6" /></a>
                                                       
                        </td>                    
                    </tr>
                </table>
            </div>
     <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>