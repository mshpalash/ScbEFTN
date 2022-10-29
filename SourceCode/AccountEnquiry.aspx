<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AccountEnquiry.aspx.cs" Inherits="EFTN.AccountEnquiry" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">
    javascript:window.history.forward(1);
    
    function makeUppercase(myControl, evt)
    {
        document.getElementById(myControl).value = document.getElementById(myControl).value.toUpperCase();
    }

    function onlyAlphaNumeric(evt) {
        var e = event || evt;
        var charCode = e.which || e.keyCode;
        //if (charCode > 31 && (charCode < 48 || charCode > 57))
        if ((charCode >= 48 && charCode <= 57) 
            || (charCode >= 65 && charCode <= 90) 
            || (charCode >= 97 && charCode <= 122)
            || charCode == 32)
            return true;
        return false;
    }

    </script>

</head>
<body class="wrap" id="content">
    <form id="form2" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server" />
        <div class="maincontent">
            <div class="Head" align="center">
                Account Enquiry</div>
            <table>
                <tr>
                    <td width="100">
                    </td>
                    <td>
                        <table border="0"">
                            <tr>
                                <td class="NormalBold">
                                    <asp:Label ID="label1" runat="server" Text="Enter Old Account Number :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOldAccountNumber" runat="server" Width="180px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="NormalBold">
                                <td class="NormalBold">
                                    <asp:Label ID="label2" runat="server" Text="New Account Number :"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNewAccountNo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label CssClass="NormalRed" ID="lblMsg" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
