<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeReportViewerPassword.aspx.cs"Inherits="EFTN.ChangeReportViewerPassword" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="modules/ReportViewerHeader.ascx" TagName="EFTReportViewerHeader"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora EFTN System</title>
    <link href="includes/sitec.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">


        function ClientValidate(source, clientside_arguments) {
            if (clientside_arguments.Value.length >= 6) {
                clientside_arguments.IsValid = true;
            }
            else { clientside_arguments.IsValid = false };
        }

        function trim(stringToTrim) {
            return stringToTrim.replace(/^\s+|\s+$/g, "");
        }
        function ltrim(stringToTrim) {
            return stringToTrim.replace(/^\s+/, "");
        }
        function rtrim(stringToTrim) {
            return stringToTrim.replace(/\s+$/, "");
        }

        function validateDataField(controlPasswordToValidate) {

            if (trim(document.getElementById(controlPasswordToValidate).value) == '') {
                alert('Insert Password');
                document.getElementById(controlPasswordToValidate).focus();
                return false;
            } else if (document.getElementById(controlPasswordToValidate).value.length < 6) {
                alert('Insert atleast 6 character');
                document.getElementById(controlPasswordToValidate).focus();
                return false;
            }
        }
    </script>
</head>
<body class="wrap" id="content">
    <form id="form2" method="post" runat="server">
    <div class="maincontent">
        <uc1:EFTReportViewerHeader ID="EFTHeader1" runat="server" />
        <div class="Head" align="center">
            Change Password</div>
        <div align="center" class="boxmodule" style="padding-top: 10px; width: 350px; margin-top: 10px;
            height: 100px; margin-left: 250px; padding-left: 15px">
            <div align="center">
                <table width="500px">
                    <tr align="left">
                        <td>
                            <asp:Label Text="Old Password" runat="server" ID="lbloldpass"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <asp:Label Text="New Password" runat="server" ID="lbnewpass"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <asp:Label Text="Confirm New Password" runat="server" ID="lblconfpass"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ErrorMessage="Passwords do not match."
                                ControlToCompare="txtConfirmPassword" ControlToValidate="txtNewPassword">                                                        
                            </asp:CompareValidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                OnClientClick="return validateDataField('txtNewPassword')" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="Msg" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
            <asp:Panel ID="pnlPasswordPolicy" runat="server" GroupingText="Password Policy" Font-Italic="true"
                Font-Bold="true" Width="602px" ForeColor="Blue">
                <asp:TextBox ID="txtPasswordPolicyAll" runat="server" TextMode="MultiLine" ReadOnly="true"
                    Height="150px" Width="600px"></asp:TextBox>
            </asp:Panel>
        </div>
    </div>
    <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
