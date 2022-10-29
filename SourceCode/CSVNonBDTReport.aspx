<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CSVNonBDTReport.aspx.cs"
    Inherits="EFTN.CSVNonBDTReport" %>

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
            <div style="width:900px" align="center">
                <table>
                    <tr>
                        <td align="center">
                            <a href="CSVReport.aspx" class="CommandButton"> CSV Report</a>
                        </td>
                    </tr>
                </table>
            </div>            
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 960px; margin-top: 10px;
                height: 40px; margin-left: 5px">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="ExpotToExcelbtn" runat="server" Text="CSV File" OnClick="ExpotToExcelbtn_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="overflow: auto; width: 940px; margin-left: 100px; margin-left: 15px">
                <asp:Panel ID="pnlReportSettlement" runat="server" Height="400px" Width="900px">
                    <asp:DataGrid ID="dtgSettlementReport" runat="Server" AlternatingItemStyle-BackColor="lightyellow"
                        AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="2"
                        FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" FooterStyle-HorizontalAlign="right"
                        HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="White"
                                            HeaderStyle-ForeColor="#FFFFFF" 
                        ItemStyle-CssClass="NormalSmall" ShowFooter="true" PagerStyle-Position="Top" OnPageIndexChanged="dtgSettlementReport_PageIndexChanged" PageSize="50" AllowPaging="True" AllowSorting="True" OnSortCommand="dtgSettlementReport_SortCommand">
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
                            <asp:BoundColumn DataField="ReceiverName" HeaderText="ReceiverName"  SortExpression = "ReceiverName" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CompanyName" HeaderText="CompanyName"  SortExpression = "CompanyName" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>                                
                            <asp:BoundColumn DataField="CustomerID" HeaderText="CustomerID"  SortExpression = "CustomerID" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BatchReference" HeaderText="BatchReference"  SortExpression = "BatchReference" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PaymentReference" HeaderText="PaymentReference"  SortExpression = "PaymentReference" >
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
