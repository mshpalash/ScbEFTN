<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EFTChargeManagementCity.aspx.cs" Inherits="EFTN.EFTChargeManagementCity" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="Modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Charge Management</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]--><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
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
<body class="wrap" id="content" onload="MM_preloadImages('images/UserManagementOn.gif','images/BranchManOn.gif','images/ChangePassOn.gif','images/ExitOn.gif')">
    <form id="form1" runat="server">
        <div class="maincontent" style="height:550px">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Charge Management</div>
            <div class="boxmodule" style="width:800px; margin-top:20px; height:200px">
            <table  cellspacing="25px" cellpadding="25px">
                <tr> 
                    <td align="center"><a href="CityChargeAccountsManagement.aspx" class="CommandButton">Accounts Charge Management</a></td>
                    <td align="center"><a href="CityChargeDepartmentManagement.aspx" class="CommandButton">Departments Charge Management</a></td>
                    <td align="center"><a href="CityChargeCodeManagement.aspx" class="CommandButton">ChargeCode Charge Management</a></td>
                </tr>
                <tr>
                </tr>
            </table>
                
            </div>
           
        </div>
         <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
