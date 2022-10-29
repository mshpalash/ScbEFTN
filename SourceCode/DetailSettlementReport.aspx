<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DetailSettlementReport.aspx.cs"
    Inherits="EFTN.DetailSettlementReport" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="modules/CheckerHeader.ascx" TagName="EFTCheckerHeader" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora EFTN System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script type="text/javascript">

        function MM_swapImgRestore() { //v3.0
          var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
        }
        function MM_preloadImages() { //v3.0
          var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
            var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
            if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
        }

        function MM_findObj(n, d) { //v4.01
          var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
            d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
          if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
          for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
          if(!x && d.getElementById) x=d.getElementById(n); return x;
        }

        function MM_swapImage() { //v3.0
          var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
           if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
        }

</script>    
</head>
<body class="wrap" id="content">
    <form id="form2" method="post" runat="server">
        <div class="maincontent">
            <uc1:EFTCheckerHeader ID="EFTHeader1" runat="server" />
            <div class="Head" align="center">
                Detail Settlement Report for Checker</div>
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
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 960px; margin-top: 10px;
                height: 75px; margin-left: 5px">
                <table>
                    <tr>
                        <td class="NormalBold" style="height: 46px">
                            <asp:Label ID="lblDay" runat="server" Text="Settlement Date"></asp:Label>
                        </td>
                        <td style="height: 46px">
                            <table>
                                <tr>
                                    <td class="NormalBold" style="height: 21px">
                                        <asp:Label ID="lblDate" runat="server" Text="Day"></asp:Label>
                                    </td>
                                    <td style="height: 21px">
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
                                    <td class="NormalBold" style="height: 21px">
                                        <asp:Label ID="Label1" runat="server" Text="Month"></asp:Label>
                                    </td>
                                    <td style="height: 21px">
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
                                    <td class="NormalBold" style="height: 21px">
                                        <asp:Label ID="Label46" runat="server" Text="Year"></asp:Label>
                                    </td>
                                    <td style="height: 21px">
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
                        <td style="height: 46px">
                            <asp:DropDownList ID="ddListReportType" runat="server">
                                <asp:ListItem Value="12" Text="Outward Transaction (Initial Transaction)" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Inward Transaction All(Initial Transaction)"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Inward Transaction  Approved Only (Initial Transaction)"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Inward Received  Approved By Default(Initial Transaction)"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Outward Return "></asp:ListItem>
                                <asp:ListItem Value="5" Text="Inward Return "></asp:ListItem>
                                <asp:ListItem Value="6" Text="Outward NOC "></asp:ListItem>
                                <asp:ListItem Value="8" Text="Inward NOC"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Branchwise Transaction Received All (PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Branchwise Inward Transaction (Initial) Approved (PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Outward Dishonor"></asp:ListItem>
                                <asp:ListItem Value="20" Text="Outward RNOC"></asp:ListItem>
                                <asp:ListItem Value="21" Text="Inward Contested"></asp:ListItem>
                                <asp:ListItem Value="22" Text="Inward Dishonor"></asp:ListItem>
                                <asp:ListItem Value="23" Text="Inward RNOC"></asp:ListItem>
                                <asp:ListItem Value="24" Text="Outward Contested"></asp:ListItem>
                                <asp:ListItem Value="25" Text="Departmentwise Transaction Sent(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="26" Text="Departmentwise Inward Return(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="27" Text="Departmentwise Inward NOC(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="28" Text="Departmentwise Outward RNOC(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="29" Text="Departmentwise Outward Dishonor(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="30" Text="Departmentwise Inward Contested (PDF Only)"></asp:ListItem>
                                 <asp:ListItem Value="31" Text="Branchwise Outward Return(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="32" Text="Branchwise Outwrad NOC(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="33" Text="Branchwise Inward RNOC(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="34" Text="Branchwise Inward Dishonor(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="35" Text="Branchwise Outward Contested (PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="2000" Text="-----------------------------------------------"></asp:ListItem>
                                 
                                 <asp:ListItem Value="2000" Text="CREDIT ONLY"></asp:ListItem>
                                <asp:ListItem Value="112" Text="Outward Transaction (Initial Transaction) "></asp:ListItem>
                                <asp:ListItem Value="102" Text="Inward Transaction All(Initial Transaction) "></asp:ListItem>
                                <asp:ListItem Value="103" Text="Inward Transaction  Approved Only (Initial Transaction) "></asp:ListItem>
                                <asp:ListItem Value="107" Text="Inward Received  Approved By Default(Initial Transaction) "></asp:ListItem>
                                <asp:ListItem Value="104" Text="Outward Return "></asp:ListItem>
                                <asp:ListItem Value="105" Text="Inward Return "></asp:ListItem>
                                <asp:ListItem Value="106" Text="Outward NOC "></asp:ListItem>
                                <asp:ListItem Value="108" Text="Inward NOC"></asp:ListItem>
                                <asp:ListItem Value="113" Text="Outward Dishonor"></asp:ListItem>
                                <asp:ListItem Value="120" Text="Outward RNOC"></asp:ListItem>
                                 <asp:ListItem Value="121" Text="Inward Contested"></asp:ListItem>
                                 <asp:ListItem Value="122" Text="Inward Dishonor"></asp:ListItem>
                                 <asp:ListItem Value="123" Text="Inward RNOC"></asp:ListItem>
                                 <asp:ListItem Value="124" Text="Outward Contested"></asp:ListItem>
                                 <asp:ListItem Value="109" Text="Branchwise Transaction Received All (PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="110" Text="Branchwise Inward Transaction (Initial) Approved (PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="125" Text="Departmentwise Transaction Sent(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="126" Text="Departmentwise Inward Return(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="127" Text="Departmentwise Inward NOC(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="128" Text="Departmentwise Outward RNOC(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="129" Text="Departmentwise Outward Dishonor(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="130" Text="Departmentwise Inward Contested (PDF Only)"></asp:ListItem>
                                 <asp:ListItem Value="131" Text="Branchwise Outward Return(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="132" Text="Branchwise Outwrad NOC(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="133" Text="Branchwise Inward RNOC(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="134" Text="Branchwise Inward Dishonor(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="135" Text="Branchwise Outward Contested (PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="2000" Text="-----------------------------------------------"></asp:ListItem>
                                
                                 <asp:ListItem Value="2000" Text="DEBIT ONLY"></asp:ListItem>
                                 <asp:ListItem Value="212" Text="Outward Transaction (Initial Transaction) " ></asp:ListItem>
                                <asp:ListItem Value="202" Text="Inward Transaction All(Initial Transaction) "></asp:ListItem>
                                <asp:ListItem Value="203" Text="Inward Transaction  Approved Only (Initial Transaction)"></asp:ListItem>
                                <asp:ListItem Value="207" Text="Inward Received  Approved By Default(Initial Transaction)"></asp:ListItem>
                                <asp:ListItem Value="204" Text="Outward Return"></asp:ListItem>
                                <asp:ListItem Value="205" Text="Inward Return"></asp:ListItem>
                                <asp:ListItem Value="206" Text="Outward NOC"></asp:ListItem>
                                <asp:ListItem Value="208" Text="Inward NOC"></asp:ListItem> 
                                <asp:ListItem Value="213" Text="Outward Dishonor"></asp:ListItem>
                                <asp:ListItem Value="220" Text="Outward RNOC"></asp:ListItem>
                                 <asp:ListItem Value="221" Text="Inward Contested"></asp:ListItem>
                                 <asp:ListItem Value="222" Text="Inward Dishonor"></asp:ListItem>
                                 <asp:ListItem Value="223" Text="Inward RNOC"></asp:ListItem>
                                 <asp:ListItem Value="224" Text="Outward Contested"></asp:ListItem>
                                <asp:ListItem Value="209" Text="Branchwise Transaction Received All (PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="210" Text="Branchwise Inward Transaction (Initial) Approved (PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="225" Text="Departmentwise Transaction Sent(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="226" Text="Departmentwise Inward Return(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="227" Text="Departmentwise Inward NOC(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="228" Text="Departmentwise Outward RNOC(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="229" Text="Departmentwise Outward Dishonor(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="230" Text="Departmentwise Inward Contested (PDF Only)"></asp:ListItem>
                                 <asp:ListItem Value="231" Text="Branchwise Outward Return(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="232" Text="Branchwise Outwrad NOC(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="233" Text="Branchwise Inward RNOC(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="234" Text="Branchwise Inward Dishonor(PDF Only)"></asp:ListItem>
                                <asp:ListItem Value="235" Text="Branchwise Outward Contested (PDF Only)"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="height: 46px">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </td>
                        <td style="height: 46px">
                            <asp:Button ID="ExpotToPdfBtn" runat="server" Text="PDF" OnClick="ExpotToPdfBtn_Click" />
                        </td>
                        <td style="height: 46px">
                            <asp:Button ID="ExpotToExcelbtn" runat="server" Text="CSV File" OnClick="ExpotToExcelbtn_Click" />
                        </td>
                    </tr>
                </table>

                 <div align="center">
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
            </div>
            <div style="overflow: auto; width: 940px; margin-left: 100px; margin-left: 15px">
                <asp:Panel ID="pnlReportSettlement" runat="server" Height="400px" Width="900px">
                    <asp:DataGrid ID="dtgSettlementReport" runat="Server" AlternatingItemStyle-BackColor="lightyellow"
                        AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="2"
                        FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" FooterStyle-HorizontalAlign="right"
                        HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="White"
                                            HeaderStyle-ForeColor="#FFFFFF" 
                        ItemStyle-CssClass="NormalSmall" ShowFooter="true" PagerStyle-Position="Top"
                         OnPageIndexChanged="dtgSettlementReport_PageIndexChanged" PageSize="500"
                         AllowPaging="True" AllowSorting="True" OnSortCommand="dtgSettlementReport_SortCommand">
                        <Columns>
                            <asp:TemplateColumn HeaderText="SL.">
                                <ItemTemplate>
                                    <%#(dtgSettlementReport.PageSize * dtgSettlementReport.CurrentPageIndex) + Container.ItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
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
                            <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber"  SortExpression = "TraceNumber" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TransactionCode" HeaderText="TransactionCode"  SortExpression = "TransactionCode" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DFIAccountNo" HeaderText="DFIAccountNo"  SortExpression = "DFIAccountNo" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BankRoutingNo" HeaderText="BankRoutingNo"  SortExpression = "BankRoutingNo" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Amount" DataFormatString="{0:N}" HeaderText="Amount"  SortExpression = "Amount" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IdNumber" HeaderText="IdNumber"  SortExpression = "IdNumber" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ReceiverName" HeaderText="Receiver/Payer Name"  SortExpression = "ReceiverName" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CompanyName" HeaderText="CompanyName"  SortExpression = "CompanyName" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>      
                            <asp:BoundColumn DataField="EntryDesc" HeaderText="EntryDesc"  SortExpression = "CompanyName" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>                           
                            <asp:BoundColumn DataField="RejectReason" HeaderText="RejectReason/CorrectedData"  SortExpression = "RejectReason" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>

                               <asp:BoundColumn DataField="Currency" HeaderText="Currency"  SortExpression = "Currency" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>                                
                            <asp:BoundColumn DataField="SessionID" HeaderText="Session"  SortExpression = "SessionID" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle CssClass="GrayBackWhiteFont" HorizontalAlign="Right" />
                        <PagerStyle Mode="NumericPages" Position="Top" />
                        <AlternatingItemStyle BackColor="LightYellow" />
                        <ItemStyle BackColor="White" CssClass="NormalSmall" />
                        <HeaderStyle CssClass="GrayBackWhiteFont" />
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
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
