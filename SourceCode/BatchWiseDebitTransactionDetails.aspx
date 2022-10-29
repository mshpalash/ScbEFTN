<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchWiseDebitTransactionDetails.aspx.cs" Inherits="EFTN.BatchWiseDebitTransactionDetails" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta id="metaref" runat="server" http-equiv="refresh" content="60" />
    <title>Checker Page</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
                        <div>
                <table style="padding-top:15px" >
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
            <div align="center">
                <a href="CreditFCUBSForEftDebitTXN.aspx" class="CommandButton"> Debit Transaction Batch List</a>
            </div>
            <div class="Head" align="center">
                Transaction Details</div>
                <br />
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td width="700px">
                                        <asp:LinkButton ID="btnAccept" runat="server" Text="Reprocess Unsuccessful Transaction" CssClass="CommandButton"
                                            OnClientClick="return confirm('Are you sure you want to accept?')" OnClick="btnAccept_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsg" runat="server"
                                Visible="false"></asp:Label>                            
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div align="center">
                <asp:DataGrid ID="dtgTransactionDetails" runat="Server" Width="600" BorderWidth="0px"
                    GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                    FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader"
                    ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" AllowPaging="True" 
                 OnPageIndexChanged="dtgTransactionDetails_PageIndexChanged" 
                 PageSize="50"  >
                    <Columns>
                        <asp:TemplateColumn HeaderText="SL.">
                            <ItemTemplate>
                                <%#(dtgTransactionDetails.PageSize * dtgTransactionDetails.CurrentPageIndex) + Container.ItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateColumn>                       
                        <asp:BoundColumn DataField="AccountTypeName" HeaderText="Account Type" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="True" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DFIAccountNo" HeaderText="DFI Account No" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="AccountNo" HeaderText="Senders' Account No" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BankName" HeaderText="Bank Name" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Amount" HeaderText="Amount" DataFormatString="{0:N}" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ReceiverName" HeaderText="Receiver Name" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CBSStatus" HeaderText="CBS Status" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:BoundColumn>
                    </Columns>
                    <FooterStyle CssClass="GrayBackWhiteFont" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall" />
                    <HeaderStyle CssClass="GrayBackWhiteFontFixedHeader" />
                </asp:DataGrid>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
