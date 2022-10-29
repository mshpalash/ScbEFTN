<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AdditionalSummaryReport.aspx.cs"
    Inherits="EFTN.AdditionalSummaryReport" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
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
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Checker Page</div>
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
            <br />            
            <br />
            <div class="boxmodule" style="padding-top:20px; width:950px; height:630px; margin-top:0px; margin-left:20px">
            <table  cellspacing="30px" cellpadding="30px">
                <tr>
                    <td align="center"><a href="InwardCreditSummaryReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/DetailSettletReportMakerOn.gif',1)"><img src="images/DetailSettlementReportMaker.gif" name="Image1" width="60" height="60" border="0" id="Image1" /></a>
                    <br /><a class="CommandButton" href="InwardCreditSummaryReport.aspx">Inward Credit Summary Report</a>
                    </td>
                    <td align="center"><a href="InwardDebitSummaryReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/SettlementReportMakerOn.gif',1)"><img src="images/SettlementReportMaker.gif" name="Image2" width="60" height="60" border="0" id="Image2" /></a>
                    <br /><a class="CommandButton" href="InwardDebitSummaryReport.aspx">Inward Debit Summary Report</a>
                    </td>

                    <td align="center"><a href="OutwardCreditSummaryReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/StatusReportMakerOn.gif',1)"><img src="images/StatusReportMaker.gif" name="Image4" width="60" height="60" border="0" id="Image4" /></a>
                    <br />
                    <a href="OutwardCreditSummaryReport.aspx" class="CommandButton">Outward Credit Summary Report</a>
                    </td>
                    <td align="center"><a href="OutwardDebitSummaryReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/StatusReportMakerOn.gif',1)"><img src="images/StatusReportMaker.gif" name="Image4" width="60" height="60" border="0" id="Img2" /></a>
                    <br />
                    <a href="OutwardDebitSummaryReport.aspx" class="CommandButton">Outward Debit Summary Report</a>
                    </td>
                    <td align="center"><a href="InwardTransactionStatusReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image9','','images/SettlementReportMakerOn.gif',1)"><img src="images/SettlementReportMaker.gif" name="Image9" width="60" height="60" border="0" id="Image9" /></a>
                    <br />
                    <a href="InwardTransactionStatusReport.aspx" class="CommandButton">Inward Debit/Credit Summary Report</a>
                    </td>                
                </tr>
            </table>           
                
            </div>
             <div style="clear: both">
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
