<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ExcelUploadForCBSMissMatchCheck.aspx.cs"
    Inherits="EFTN.ExcelUploadForCBSMissMatchCheck" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">

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
            
    javascript:window.history.forward(1);
    
    function makeUppercase(myControl, evt)
    {
        document.getElementById(myControl).value = document.getElementById(myControl).value.toUpperCase();
    } 
    </script>

</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Inward Transaction Approved by Maker</div>
            <div>
                <table style="padding-top:15px">
                    <tr height="20px">
                        <td width="30px">
                        </td>
                        <td>
                            <a href="EFTChecker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/EFTCheckerOn.gif',1)"><img src="images/EFTChecker.gif" name="Image4" width="149" height="25" border="0" id="Image4" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerEBBS.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/EFTCheckerEBBSOn.gif',1)"><img src="images/EFTCheckerEBBS.gif" name="Image1" width="149" height="25" border="0" id="Image1" /></a>
                    
                                                </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerAuthorizer.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/EFTCheckerAuthorizerOn.gif',1)"><img src="images/EFTCheckerAuthorizer.gif" name="Image2" width="149" height="25" border="0" id="Image2" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                        <a href="EFTCheckerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ReportOn.gif',1)"><img src="images/Report.gif" name="Image3" width="149" height="25" border="0" id="Image3" /></a>
                                                      
                        </td>                    
                    </tr>
                </table>
            </div>
            <br />
                 <table>
                    <tr>
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
                            <asp:Label ID="lblFileType" runat="server" Text="Upload File For : "></asp:Label>
                            <asp:DropDownList ID="ddListFileType" runat="server">
                                <asp:ListItem Text="Inward Transaction Itemwise(Settlement Date)" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Outward Transaction (Entry Date)" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Inward Return (Settlement Date)" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Outward Transaction Batchwise(Entry Date)" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalBold" >
                            Please Select your Excel File to Upload<br />
                            <asp:FileUpload CssClass="inputlt" ID="fulExcelFile" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvFulExcelFile" runat="server" ErrorMessage="Browse a file"
                                ControlToValidate="fulExcelFile"   CssClass="NormalRed"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <asp:Button ID="btnUploadExcel" runat="server" CssClass="inputlt" Text="Upload File" Width="80" OnClick="btnUploadExcel_Click" OnClientClick="return confirm('Are you sure you want to import this file?')" />
                           <asp:Label ID="lblErrMsg" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            <br />
             <br />
                              <div style="overflow: auto; width: 940px; margin-left: 15px" id="divItemWiseMismatch" runat="server">
                <asp:Panel ID="pnlReportSettlement" runat="server" Height="400px" Width="900px">
                    <asp:DataGrid ID="dtgMismatchReport" runat="Server" AlternatingItemStyle-BackColor="lightyellow"
                        AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="2"
                        FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" FooterStyle-HorizontalAlign="right"
                        HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="White"
                        HeaderStyle-ForeColor="#FFFFFF" ItemStyle-CssClass="NormalSmall" ShowFooter="true" 
                        PagerStyle-Position="Top">
                        <Columns>
                            <asp:TemplateColumn HeaderText="SL.">
                                <ItemTemplate>
                                    <%#(dtgMismatchReport.PageSize * dtgMismatchReport.CurrentPageIndex) + Container.ItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
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
                            <asp:BoundColumn DataField="XLAccountNo" HeaderText="XLAccountNo"  SortExpression = "XLAccountNo" ItemStyle-ForeColor="Red" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Amount" DataFormatString="{0:N}" HeaderText="Amount"  SortExpression = "Amount" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="XLAmount" HeaderText="XLAmount"  SortExpression = "XLAmount" ItemStyle-ForeColor="Red" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ReceiverName" HeaderText="Receiver/Payer Name"  SortExpression = "ReceiverName" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                             <asp:BoundColumn DataField="SendingBankRoutNo" HeaderText="SendingBankRoutNo"  SortExpression = "SendingBankRoutNo" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                             <asp:BoundColumn DataField="ReceivingBankRoutNo" HeaderText="ReceivingBankRoutNo"  SortExpression = "ReceivingBankRoutNo" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SettlementJDate" HeaderText="SettlementDate"  SortExpression = "SettlementJDate" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="EntryDate" HeaderText="EntryDate"  SortExpression = "EntryDate" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="AccountMismatch" HeaderText="AccountMismatch"  SortExpression = "AccountMismatch" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AmountMismatch" HeaderText="AmountMismatch"  SortExpression = "AmountMismatch" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RejectReason" HeaderText="RejectReason"  SortExpression = "RejectReason" >
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
            <div style="width: 940px; margin-left: 15px; margin-bottom: 20px; overflow: scroll" id="divBatchWiseMismatch" runat="server">
                <table>
                    <tr>
                        <td class="NormalBold" align="center">
                            <asp:DataGrid ID="dtgMisMatchBatchWise" runat="Server" BorderWidth="0px" GridLines="None"
                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID">
                                <Columns>
                                    <asp:TemplateColumn HeaderText="">
                                        <ItemTemplate>
                                            <a href="BatchWiseMismatchedTransactionDetails.aspx?TransactionEDRID=<%#DataBinder.Eval(Container.DataItem, "TransactionID")%>">
                                                Batch Details</a>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                                    
                                    <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="true" />
                                    <asp:BoundColumn DataField="BatchType" HeaderText="Type" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="CompanyName" HeaderText="Company Name" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="SECC" HeaderText="SECC" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}"
                                        ItemStyle-Wrap="true" HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="EntryDesc" HeaderText="Description" ItemStyle-Wrap="True"
                                        HeaderStyle-Wrap="True" />
                                    <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total" ItemStyle-Wrap="True"
                                        HeaderStyle-Wrap="True" />
                                    <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}"
                                        ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                                    <asp:BoundColumn DataField="ExcelTotalItem" HeaderText="Total Item in Excel" ItemStyle-Wrap="True"
                                        HeaderStyle-Wrap="True" />
                                    <asp:BoundColumn DataField="MismatchItem" HeaderText="Mismatch in Item"
                                        ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                                    <asp:BoundColumn DataField="ExcelTotalAmount" HeaderText="Total Amount in Excel" DataFormatString="{0:N}" 
                                    ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                                    <asp:BoundColumn DataField="MismatchAmount" HeaderText="Mismatch in Amount" DataFormatString="{0:N}"
                                        ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                                    <asp:BoundColumn DataField="OrginialDistAccount" HeaderText="Orginial Distinct Account" 
                                    ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                                    <asp:BoundColumn DataField="ExcelMismatchAccount" HeaderText="Excel Account"
                                        ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                                    <asp:BoundColumn DataField="MismatchAccount" HeaderText="MismatchAccount"
                                        ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                                    <asp:BoundColumn DataField="CreatedBy" HeaderText="Maker"
                                        ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                                    <asp:BoundColumn DataField="ApprovedBy" HeaderText="Checker"
                                        ItemStyle-Wrap="True" HeaderStyle-Wrap="True" />
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
