<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Default.aspx.cs" Inherits="EFTN._Default" %>

<%@ Register Src="Modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EFT Admin</title>
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
        <div class="maincontent" style="height:750px">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Admin Maker</div>
            <div class="boxmodule" style="width:820px; margin-top:20px; height:600px">
            <table  cellspacing="25px" cellpadding="25px">
                <tr>
                    <td align="center"><a href="UserManagement.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/UserManagementOn.gif',1)"><img src="images/UserManagement.gif" name="Image1" width="60" height="60" border="0" id="Image1" /></a>
                    <br />
                    <a class="CommandButton" href="UserManagement.aspx">User</a>
                    </td>
                    <td align="center"><a href="Branches.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/BranchManOn.gif',1)"><img src="images/BranchMan.gif" name="Image2" width="60" height="60" border="0" id="Image2" /></a>
                    <br />
                    <a class="CommandButton" href="Branches.aspx">Branches</a>
                    </td>
                    <td align="center"><a href="DepartmentManagement.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image6','','images/BranchManOn.gif',1)"><img src="images/BranchMan.gif" name="Image6" width="60" height="60" border="0" id="Img6" /></a>
                    <br />
                    <a class="CommandButton" href="DepartmentManagement.aspx">Department</a>
                    </td>
                    <td align="center"><a href="UpdateGLAccount.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image5','','images/BranchManOn.gif',1)"><img src="images/BranchMan.gif" name="Image5" width="60" height="60" border="0" id="Img5" /></a>
                    <br />
                    <a class="CommandButton" href="UpdateGLAccount.aspx">GL Account</a>
                    </td>
                    <td align="center"><a href="UpdateHolidayCalendar.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image16','','images/UpdateSettlementDateMakerOn.gif',1)"><img src="images/UpdateSettlementDateMaker.gif" name="Image16" width="60" height="60" border="0" id="Img16" /></a>
                    <br />
                    <a href="UpdateHolidayCalendar.aspx" class="CommandButton">Update Holidays</a>
                    </td>  
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="linkBtnUpdateWorkingDayForFridaySaturday" runat="server"
                                         Text="Update working day for Friday and Saturday" class="CommandButton" 
                            onclick="linkBtnUpdateWorkingDayForFridaySaturday_Click"></asp:LinkButton>
                    </td>
                    <td align="center"><a href="RegenerateOutwardTransaction.aspx" class="CommandButton">Regenerate Outward Transaction</a></td>
                    <td align="center"><a href="Messages.aspx" class="CommandButton">Branch Messages</a></td>
                    <td>
                        <asp:LinkButton ID="linkBtnInwardSearch" runat="server" 
                            Text="Inward Transaction Search" class="CommandButton" 
                            onclick="linkBtnInwardSearch_Click"></asp:LinkButton>
                    </td>
                    <td><asp:LinkButton ID="linkBtnSignatorManagement" runat="server" 
                            Text="Signature Management" class="CommandButton" 
                            onclick="linkBtnSignatorManagement_Click"></asp:LinkButton>
                            </td>
                   
                </tr>                
                <tr>
                    <td>
                        <a href="CustomerEmail.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ChangePassOn.gif',1)">
                        <img src="images/png/Mail_Icon.png" name="Image17" width="60" height="60" 
                            border="0" id="Img17" /></a><br />
                        <asp:LinkButton ID="lbtncustemail" runat="server" class="CommandButton" Text="Customar E-mail" onclick="lbtncustemail_Click"></asp:LinkButton>
                    </td>
 
                    <%--<td align="center"><a href="ChangeUserPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ChangePassOn.gif',1)"><img src="images/ChangePass.gif" name="Image3" width="60" height="60" border="0" id="Img1" /></a>
                    <br />
                    <a href="ChangeUserPassword.aspx" class="CommandButton">Change Password</a>
                    </td>--%>
                    <td align="center"><a href="LogOut.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/ExitOn.gif',1)"><img src="images/Exit.gif" name="Image4" width="60" height="60" border="0" id="Img2" /></a>
                    <br />
                    <a href="LogOut.aspx" class="CommandButton">Sign Out</a>
                    </td>
                    <td>&nbsp;</td>
                    <td></td>
                    <td></td>
                </tr>                
            </table>
                
            </div>
           
        </div>
         <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
