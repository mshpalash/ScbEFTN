<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EFTCheckerAuthorizerReport.aspx.cs"
    Inherits="EFTN.EFTCheckerAuthorizerReport" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerAuthorizer.ascx" TagName="HeaderCA" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Checker Page</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
<script type="text/javascript">
javascript:window.history.forward(1);
</script>
<script type="text/javascript">
<!--
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
-->

    </script>
</head>
<body class="wrap" id="content" onload="MM_preloadImages('images/DetailSettletReportMakerOn.gif','images/SettlementReportMakerOn.gif','images/UpdateSettlementDateMakerOn.gif','images/StatusReportMakerOn.gif')">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:HeaderCA ID="Header1" runat="server" />
            <div class="Head" align="center">
                Checker Page</div>
            <br />            
            <br />
            <div class="boxmodule" style="padding-top:20px; width:950px; height:1150px; margin-top:0px; margin-left:20px">
            <table  cellspacing="30px" cellpadding="30px">
                <tr>
                    <td align="center"><a href="DetailSettlementReportCA.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/DetailSettletReportMakerOn.gif',1)"><img src="images/DetailSettlementReportMaker.gif" name="Image1" width="60" height="60" border="0" id="Image1" /></a>
                    <br /><a class="CommandButton" href="DetailSettlementReportCA.aspx">Detail Settlement Report</a>
                    </td>
                    <td align="center"><a href="SettlementReportCA.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/SettlementReportMakerOn.gif',1)"><img src="images/SettlementReportMaker.gif" name="Image2" width="60" height="60" border="0" id="Image2" /></a>
                    <br /><a class="CommandButton" href="SettlementReportCA.aspx">Settlement Report</a>
                    </td>
                    <td align="center"><a href="UpdateSettlementDateCA.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/UpdateSettlementDateMakerOn.gif',1)"><img src="images/UpdateSettlementDateMaker.gif" name="Image3" width="60" height="60" border="0" id="Image3" /></a>
                    <br />
                    <a href="UpdateSettlementDateCA.aspx" class="CommandButton">Update Settlement Date</a>
                    </td>
                    <td align="center"><a href="BranchWiseSettlementReportCA.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image9','','images/SettlementReportMakerOn.gif',1)"><img src="images/SettlementReportMaker.gif" name="Image9" width="60" height="60" border="0" id="Image9" /></a>
                    <br />
                    <a href="BranchWiseSettlementReportCA.aspx" class="CommandButton">BranchWise Settlement Report</a>
                    </td>                                                                          
                </tr>
                <tr>
                     <td align="center">
                        <asp:LinkButton ID="linkBtnEFTAdvice" runat="server" Text="Print EFT Advice" class="CommandButton" OnClick="linkBtnEFTAdvice_Click"></asp:LinkButton>
                     </td>
                     <td align="center">
                        <asp:LinkButton ID="linkBtnEFTAdicePrintStatus" runat="server" Text="EFT Advice Print Status" class="CommandButton" OnClick="linkBtnEFTAdicePrintStatus_Click"></asp:LinkButton>
                     </td>
                     <td align="center"><a href="DashBoardReportCA.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image17','','images/StatusReportMakerOn.gif',1)"><img src="images/StatusReportMaker.gif" name="Image17" width="60" height="60" border="0" id="Img17" /></a>
                    <br />
                    <a href="DashBoardReportCA.aspx" class="CommandButton">Dash Board</a>
                    </td>
                    

                    <td align="center">
                        <asp:LinkButton ID="linkBtnZoneWiseReport" runat="server" Text="Zone Wise Report" class="CommandButton" OnClick="linkBtnZoneWiseReport_Click"></asp:LinkButton>
                    </td>                                                            
                    <td align="center">
                        <asp:LinkButton ID="linkBtnBACHBranch" runat="server" Text="BACH Branch Report" class="CommandButton" OnClick="linkBtnBACHBranch_Click"></asp:LinkButton>
                    </td>                
                </tr>            
                
            </table>           
                
            </div>
             <div style="clear: both">
            </div>

            
            
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
