<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsumerType.aspx.cs" Inherits="EFTN.ConsumerScreen" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">
        javascript: window.history.forward(1);


        function makeUppercase(myControl, evt) {
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
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div>
                <div class="Head" align="center">
                    Manual Entry
                </div>
                <table>
                    <tr>
                        <td width="100"></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="NormalBold">Is it a Credit or Debit Transaction?</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoBtnTransactionType" RepeatDirection="Horizontal" CssClass="Normal" runat="server">
                                            <asp:ListItem Value="Credit" Selected="True">Credit</asp:ListItem>
                                            <asp:ListItem Value="Debit">Debit</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">This account is an:</td>
                                    <td>
                                        <asp:RadioButtonList ID="SenderType" RepeatDirection="Horizontal" CssClass="Normal"
                                            runat="server">
                                            <asp:ListItem Value="1" Selected="True">Individual</asp:ListItem>
                                            <asp:ListItem Value="2">Corporate</asp:ListItem>
                                            <asp:ListItem Value="2">Government</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Receiver's account type</td>
                                    <td>
                                        <asp:RadioButtonList ID="ReceiverType" RepeatDirection="Horizontal" CssClass="Normal"
                                            runat="server">
                                            <asp:ListItem Value="1" Selected="True">Individual</asp:ListItem>
                                            <asp:ListItem Value="2">Corporate</asp:ListItem>
                                            <asp:ListItem Value="2">Government</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Currency type
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CurrencyDdList" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Reason for Payment</td>
                                    <td>
                                        <asp:TextBox ID="txtEntryDesc" MaxLength="10" Width="100px" runat="server" CssClass="inputlt"
                                            OnKeyUp="return makeUppercase(this.name);" OnKeyPress="return onlyAlphaNumeric(this.name);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ReqEntryDesc" runat="server" ControlToValidate="txtEntryDesc"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblMsg" CssClass="NormalRed" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="Button2" CssClass="inputlt" runat="server" Text="Next " Width="80"
                                            OnClick="Button2_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
