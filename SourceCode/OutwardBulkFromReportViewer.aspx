<%@ Page Language="C#" AutoEventWireup="true" Codebehind="OutwardBulkFromReportViewer.aspx.cs" Inherits="EFTN.OutwardBulkFromReportViewer"%>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/ReportViewerHeader.ascx" TagName="Header1" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> Report</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">
javascript:window.history.forward(1);

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
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header1 ID="HeaderViewer" runat="server" />
            <div class="Head" align="center">
                Report Viewer</div>

            <br />
            <div align="center" class="NormalBold" c style="font-size: medium">
                Outward Bulk From Report</div>
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px;
                height: 40px; margin-left: 15px">
                <table>
                    <tr>
                        <td class="NormalBold" style="width: 117px">
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
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </td>
                        <td>
                            <asp:Button ID="ExpotToPdfBtn" runat="server" Text="Export to PDF" OnClick="ExpotToPdfBtn_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
            <asp:DataGrid ID="dtgOutwardBulkUploadFromMaker" runat="Server" AlternatingItemStyle-BackColor="lightyellow"
                        AutoGenerateColumns="False" BorderWidth="0px" CellPadding="5" CellSpacing="2"
                        FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" FooterStyle-HorizontalAlign="right"
                        HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="White"
                                            HeaderStyle-ForeColor="#FFFFFF" 
                        ItemStyle-CssClass="NormalSmall" ShowFooter="True" PagerStyle-Position="Top"  PageSize="8" AllowPaging="True" AllowSorting="True" OnPageIndexChanged="dtgOutwardBulkUploadFromMaker_PageIndexChanged">
                        <Columns>
                            <asp:TemplateColumn HeaderText="SL.">
                                <ItemTemplate>
                                    <%#(dtgOutwardBulkUploadFromMaker.PageSize * dtgOutwardBulkUploadFromMaker.CurrentPageIndex) + Container.ItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="">
                                <ItemTemplate>
                                    <a href="OutwardBulkTransactionDetails.aspx?TransactionEDRID=<%#DataBinder.Eval(Container.DataItem, "TransactionID")%>">
                                        Batch Details</a>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNumber"  SortExpression = "BatchNumber" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                             <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="EntryDateTransactionSent" DataFormatString="{0:d}" SortExpression = "EntryDateTransactionSent" >
                                 <HeaderStyle Wrap="False" />
                                 <ItemStyle Wrap="False" />
                             </asp:BoundColumn>
                             <asp:BoundColumn DataField="EntryDesc" HeaderText="EntryDesc" SortExpression="EntryDesc">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CompanyId" HeaderText="CompanyId" SortExpression="CompanyId">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SECC" HeaderText="SECC" SortExpression="SECC">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TotalTransactions" HeaderText="TotalTransactions"  SortExpression = "TotalTransactions" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BatchType" HeaderText="BatchType"  SortExpression = "BatchType" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="UserName" HeaderText="Maker Name"  SortExpression = "BatchType" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                           <asp:BoundColumn DataField="TotalAmount" DataFormatString="{0:N}" HeaderText="TotalBatchAmount"  SortExpression = "TotalAmount" >
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
            
            </div>
            <div style="padding-top:20px; padding-left:220px">
                <table>
                    <tr height="20px">
                        <td width="50px">
                        </td>
                        
                        <td>
                            <a href="ChangeReportViewerPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image5','','images/ChangePasswordOn.gif',1)"><img src="images/ChangePassword.gif" name="Image5" width="149" height="25" border="0" id="Image5" /></a>
                                                      
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
