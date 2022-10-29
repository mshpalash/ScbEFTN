<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FloraSoft.Login" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">    
    <title>FLORA BEFTN</title>
    <link href= "includes/sitec.css" type="text/css" rel="stylesheet"/>
</head>
<body  class="wrap" id="content">
    <form id="form1" method="post"  runat="server">
    <div class="maincontent">
    <br>
            <br>
			<br>
			<br>
			<br>
    <center>
     <table cellpadding="0" cellspacing="0" border="0"
            style="BACKGROUND-IMAGE: url(Images/SCBLBankLogin.gif); background-repeat:no-repeat" >
					<tr height="155">
						<td width="110"></td>
						<td width="200"></td>
						<td width="110"></td>
					</tr>
					<tr height="30">
						<td></td>
						<td>
                            <asp:Button ID="btnGenericLogIn" runat="server" Width="100%" Height="22px" Text="Generic User / Idp Login" OnClick="btnGenericLogIn_Click" BackColor="Red" CssClass="hCursor" ForeColor="White" />
						</td>
						<td>

                         </td>
					</tr>
					<tr height="20">
						<td></td>
						<td>
                            <asp:Button ID="btnGeneralLogIn" runat="server" Width="100%" Height="22px" Text="General User / SSO Login" OnClick="btnGeneralLogIn_Click" BackColor="Green" CssClass="hCursor" ForeColor="White" />  
						</td>
						<td>

                        </td>
					</tr>
					<tr height="35">
						<td colspan="3" align="center">
                            <asp:Label ID="MyMessage" ForeColor="Red" CssClass ="NormalRed" runat="server"></asp:Label>
						</td>
					</tr>
					<tr height="30">
					    <td></td>
                        <td colspan="3"></td>
                        <td></td>
                    </tr>
    </table>
    <%--<div visible="false" id="divConcurrent" runat="server">
        <table>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblContinueLogin" runat="server" Text="You are logged in from different location. Do you want to proceed?"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnProceed" runat="server" Text="Yes" 
                        onclick="btnProceed_Click" />
                </td>
                <td width="10px">
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="No" onclick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>--%>
    </center>
    </div>
        <uc1:footer ID="Footer1" runat="server" />
        <%--<input type="hidden" id="hiddenLoginFailed" runat="server" />--%>
        <%--<input type="hidden" id="lastLoginID" runat="server" />--%>
        <%--<input type="hidden" id="loginPass" runat="server" />--%>            
    </form>
</body>
</html>
