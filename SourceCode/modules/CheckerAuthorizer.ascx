<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="CheckerAuthorizer.ascx.cs" Inherits="EFTN.modules.CheckerAuthorizer" %>

<table cellspacing="1" border="0" cellpadding="0" style="border-collapse: collapse;">
    <tr>
        <td colspan="3" >
            <a href="../EFTCheckerAuthorizerAdmin.aspx"><img src="../images/FloraEFTSystem.jpg" border="0" alt="" width="989px" />
            </a>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="3">
            <asp:Label ID="WelcomeMsg" runat="server" CssClass="NormalBold" Text="Welcome"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblWarningMsg" runat="server" Font-Bold="true"></asp:Label>
        </td>
    </tr>
    <tr align="right">
        <td align="left" style="height:40px">
            <a href="../ChangeAuthorizerPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image5','','images/ChangePasswordOn.gif',1)"><img src="images/ChangePassword.gif" name="Image5" width="149" height="25" border="0" id="Image5" /></a>
            
        </td>
        <td>
            <asp:LinkButton ID="linkBtnCAReport" runat="server" Text="Report" 
                class="CommandButton" onclick="linkBtnCAReport_Click"></asp:LinkButton>
        </td>

        <td align="right" style="height:40px">
            <a href="../LogOut.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image6','','images/SignOutOn.gif',1)"><img src="images/SignOut.gif" name="Image6" width="149" height="25" border="0" id="Img1" /></a>
        </td>
    </tr>
</table>
<table cellspacing="1" border="0" cellpadding="0" style="border-collapse: collapse;">

    </table>
