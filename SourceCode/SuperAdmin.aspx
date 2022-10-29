<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SuperAdmin.aspx.cs" Inherits="EFTN.SuperAdmin" %>

<%@ Register Src="Modules/SuperAdminHeader.ascx" TagName="Header" TagPrefix="uc1" %>
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
                                Admin</div>
            <div class="boxmodule" style="width:820px; margin-top:20px; height:350px">
            <table  cellspacing="25px" cellpadding="25px">
                <tr>
                    <td class="NormalBold" align="center">
                        <asp:DataGrid ID="dtGridSecurityMatrix" runat="Server" BorderWidth="0px" GridLines="None"
                            AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                            FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                            ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" 
                            DataKeyField="FilePath" onitemcommand="dtGridSecurityMatrix_ItemCommand">
                            <Columns>
                                <asp:HyperLinkColumn DataTextField="FileName"></asp:HyperLinkColumn>
                                <asp:ButtonColumn CommandName="DownloadSecurityMatrix" Text="Download" CausesValidation="false"></asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                        <asp:Label ID="lblTransactionError" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="center"><a href="MasterUserManagement.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/UserManagementOn.gif',1)"><img src="images/UserManagement.gif" name="Image1" width="60" height="60" border="0" id="Image1" /></a>
                    <br />
                    <a class="CommandButton" href="MasterUserManagement.aspx">User</a>
                    </td>
                    <td align="center"><a href="MasterUserKillSession.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image5','','images/UserManagementOn.gif',1)"><img src="images/UserManagement.gif" name="Image5" width="60" height="60" border="0" id="Img5" /></a>
                    <br />
                    <a class="CommandButton" href="MasterUserKillSession.aspx">Kill Session</a>
                    </td>
                    <td align="center">
                        <a href="SuperAdminAuditUser.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image6','','images/UserManagementOn.gif',1)">
                            <img src="images/UserManagement.gif" name="Image6" width="60" height="60" border="0"
                                id="Img6" /></a>
                        <br />
                        <a class="CommandButton" href="SuperAdminAuditUser.aspx">Audit User</a>
                    </td>

                </tr>
                <tr>
<%--                    <td align="center"><a href="ChangeMasterUserPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ChangePassOn.gif',1)"><img src="images/ChangePass.gif" name="Image3" width="60" height="60" border="0" id="Img1" /></a>
                    <br />
                    <a href="ChangeMasterUserPassword.aspx" class="CommandButton">Change Password</a>
                    </td>--%>
                    <td align="center"><a href="LogOut.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/ExitOn.gif',1)"><img src="images/Exit.gif" name="Image4" width="60" height="60" border="0" id="Img2" /></a>
                    <br />
                    <a href="LogOut.aspx" class="CommandButton">Sign Out</a>
                    </td>
                </tr>                
            </table>
                
            </div>
           
        </div>
         <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
