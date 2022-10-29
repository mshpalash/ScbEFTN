<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CorToInd.aspx.cs" Inherits="EFTN.CorToInd" %>

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
                Corporate-Individual Transaction</div>
            <table>
                <tr>
                    <td width="100">
                    </td>
                    <td>
                        <table border="0"">
                            <tr>
                                <td class="NormalBold">
                                    Your Company TIN :</td>
                                <td>
                                    <asp:TextBox ID="txtCompanyTIN" CssClass="inputlt" runat="server" Width="80px" MaxLength="10"
                                        OnKeyUp="return makeUppercase(this.name);"  OnKeyPress="return onlyAlphaNumeric(this.name);" />
                                    <asp:RequiredFieldValidator ID="ReqtxtCompanyTIN" runat="server" ControlToValidate="txtCompanyTIN"
                                        CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="NormalBold">
                                <td class="NormalBold">
                                    Your Company Name:</td>
                                <td>
                                    <asp:TextBox ID="txtCompanyName" CssClass="inputlt" runat="server" Width="130px"
                                        MaxLength="16" OnKeyUp="return makeUppercase(this.name);"  OnKeyPress="return onlyAlphaNumeric(this.name);" />
                                    <asp:RequiredFieldValidator ID="ReqtxtCompanyName" runat="server" ControlToValidate="txtCompanyName"
                                        CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnCorToIndBatchInfo" CssClass="inputlt" runat="server" Text="NEXT "
                                        Width="80" OnClick="btnCorToIndBatchInfo_Click" /></td>
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
