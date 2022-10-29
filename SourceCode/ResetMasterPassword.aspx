<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetMasterPassword.aspx.cs" Inherits="FloraSoft.ResetMasterPassword" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="modules/SuperAdminHeader.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Management</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]--> 
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header" runat="server" />
                                    <!-- Programmer Code Starts  -->
                                    <div class="Head" align="center">Reset Password</div>
                                    <div align="center">
                                        <table width="500px">
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="Label2" Text="New Password" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="Label3" Text="Confirm New Password" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidatorPassword" runat="server" 
                                                        ErrorMessage="Passwords do not match." 
                                                        ControlToCompare="txtConfirmPassword"
                                                        ControlToValidate="txtNewPassword">                                                        
                                                </asp:CompareValidator></td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                                        OnClientClick="return validateDataField('txtNewPassword')" OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" 
                                                        />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="Msg" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <!--  Programmer Code Ends   -->
        <div>
            <asp:Label runat="server" ID="lblTestCookie"></asp:Label>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
