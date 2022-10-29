<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FlatFilesForReturnReceivedToHUB.aspx.cs"
    Inherits="EFTN.FlatFilesForReturnReceivedToHUB" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Export to CBS</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">
    javascript:window.history.forward(1);
    </script>
    
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
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Export to CBS</div> 
            <div>
                <table style="padding-top: 15px;">
                    <tr height="20px">
                        <td width="200px">
                        </td>
                        <td>
                            <a href="EFTMaker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/EFTMakerHomeOn.gif',1)">
                                <img src="images/EFTMakerHome.gif" name="Image1" width="149" height="25" border="0"
                                    id="Image1" /></a>
                        </td>
                        <td width="250px">
                        </td>
                        <td>
                            <a href="EFTMakerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/ReportOn.gif',1)">
                                <img src="images/Report.gif" name="Image2" width="149" height="25" border="0" id="Image2" /></a>
                        </td>
                    </tr>
                </table>
            </div>
            
            <div align="center" class="boxmodule" style="padding-top:10px; width:400px; margin-top:10px; height:40px; margin-left:300px">
            <table>
                    <tr>
                        <td>
                        </td>
                        <td class="NormalBold">
                            Received Transactions</td>
                        <td width="30"></td>
                        <td>
                            <asp:DropDownList ID="ddListTransactionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddListTransactionType_SelectedIndexChanged">
                                <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                <asp:ListItem Value="Debit">Debit</asp:ListItem>
                            </asp:DropDownList>
                        </td>                        
                        <td width="30"></td>
                        <td class="Normal">
                            <asp:LinkButton ID="linkBtnFlatFileReturnReceived" runat="server" Text="Generate"
                                CssClass="CommandButton" OnClientClick="return confirm('Are you sure you want to generate flat files for CBS?')" OnClick="linkBtnFlatFileReturnReceived_Click"
                                /></td>
                    </tr>
            </table>
            </div>
            <div align="center" id="AvailDiv" runat="server" style="position: relative; overflow: auto;
                    width: 900px; margin-top: 15px; height: 300px;">
               <asp:DataGrid ID="dtgTransactionSentToOwnBank" runat="Server" Width="600" BorderWidth="0px"
                                GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1"
                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader"
                                ItemStyle-BackColor="#dee9fc" ItemStyle-CssClass="NormalSmall" AlternatingItemStyle-BackColor="#ffffff">
                                <Columns>
                                    <asp:BoundColumn DataField="CurrencyCode" HeaderText="CurrencyCode" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="AccountNo" HeaderText="AccountNo" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="Amount" HeaderText="Amount" DataFormatString="{0:N}" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="DorC" HeaderText="DorC" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="TranType" HeaderText="TranType" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="valueDate" HeaderText="valueDate" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="Narration1" HeaderText="Narration With Return Reason" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                </Columns>
                            </asp:DataGrid>
            </div>
            
             <div style="clear:both"></div>       
            <div style="margin-top: 25px">
                <table style="padding-top: 15px">
                    <tr height="20">
                        <td width="200px">
                        </td>
                        <td>
                            <a href="ChangeMakerPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ChangePasswordOn.gif',1)">
                                <img src="images/ChangePassword.gif" name="Image3" width="149" height="25" border="0"
                                    id="Image3" /></a>
                        </td>
                        <td width="250px">
                        </td>
                        <td>
                            <a href="LogOut.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/SignOutOn.gif',1)">
                                <img src="images/SignOut.gif" name="Image4" width="149" height="25" border="0" id="Image4" /></a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
