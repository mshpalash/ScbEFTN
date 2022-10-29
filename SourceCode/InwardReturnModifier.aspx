<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InwardReturnModifier.aspx.cs" Inherits="EFTN.InwardReturnModifier" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script type="text/javascript" language="javascript">

    javascript:window.history.forward(1);

    function onlyACCNumbers(myControl, evt) {
        var e = event || evt;
        var charCode = e.which || e.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        var controlValue = document.getElementById(myControl).value;
        var re = "-";
        while (!IsNumeric(controlValue)) {
            controlValue = controlValue.replace(re, "");
        }
        if (controlValue.length > 12) {
            return false;
        }
        return true;
    }

    function IsPrecisionExists(sText) {

        for (i = 0; i < sText.length; i++) {
            if (sText.charAt(i) == ".") {
                return true;
            }
        }
        return false;
    }
    
    function onlyNumbers(evt) {
        var e = event || evt;
        var charCode = e.which || e.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }
    
    function insertNthChar(string, chr) {
        var output = '';
        for (var i = 0; i < string.length; i++) {
            if (i == 2)
                output += chr;
            else if (i == 9)
                output += chr;
            output += string.charAt(i);
        }

        return output;
    }
    
    function insertDelimeter(myControl) {
        var controlValue = document.getElementById(myControl).value;
        var re = "-";
        while (!IsNumeric(controlValue)) {
            controlValue = controlValue.replace(re, "");
        }
        if (controlValue.length > 11) {
            return false;
        }
        var test = insertNthChar(controlValue, '-');
        document.getElementById(myControl).value = test;
    }

    function IsNumeric(sText) {
        var ValidChars = "0123456789.";
        var IsNumber = true;
        var Char;

        for (i = 0; i < sText.length && IsNumber == true; i++) {
            Char = sText.charAt(i);
            if (ValidChars.indexOf(Char) == -1) {
                IsNumber = false;
            }
        }
        return IsNumber;
    }

    function onlyACCNumbers(myControl, evt) {
        var e = event || evt;
        var charCode = e.which || e.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        var controlValue = document.getElementById(myControl).value;
        var re = "-";
        while (!IsNumeric(controlValue)) {
            controlValue = controlValue.replace(re, "");
        }
        if (controlValue.length > 12) {
            return false;
        }
        return true;
    }

    function validateDataField() {
        var myControl = 'txtAccountNumber';
        var controlValue = document.getElementById(myControl).value;

        if(controlValue.length < 13)
        {
            alert("Invalid Account Number");
            document.getElementById(myControl).focus();
            return false;
        }
        
        var myControlRout = 'txtRoutingNo';
        var controlRoutValue = document.getElementById(myControlRout).value;
        
        if(controlRoutValue.length < 9)
        {
            alert("Invalid Routing Number");
            document.getElementById(myControlRout).focus();
            return false;
        }
        return confirm('Are you sure you want to save?')
    }
    
    function makeUppercase(myControl, evt)
    {
        document.getElementById(myControl).value = document.getElementById(myControl).value.toUpperCase();
    }

    </script>

</head>
<body class="wrap" id="content">
    <form id="form2" method="post" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="CommandButton" align="center">
                <a href="inwardreturnmaker.aspx">Go to Inward Return</a>
            </div>
            <div>
                <table>
                    <tr>
                        <td width="100">
                        </td>
                        <td>
                            <table border="0">
                                <tr>
                                    <td class="NormalBold">
                                        Your Account Number with us :</td>
                                    <td>
                                        <asp:TextBox ID="txtAccountNumber" runat="server" Width="130px" MaxLength="13" oncopy="return false"
                                            oncut="return false" onpaste="return false" OnKeyPress="return onlyACCNumbers(this.name);" />
                                        <asp:RequiredFieldValidator ID="ReqtxtAccountNumber" runat="server" ControlToValidate="txtAccountNumber"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                        <asp:Button ID="CheckeBBS" CausesValidation="false" CssClass="inputlt" Text="Check CBS"
                                            runat="server" OnClick="CheckeBBS_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        Receiving Bank Routing No.:</td>
                                    <td>
                                        <asp:TextBox ID="txtRoutingNo" runat="server" MaxLength="9" Width="80px" CssClass="inputlt"
                                            onkeypress="return onlyNumbers();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRoutingNo"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>                                            
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        Reason For Payment:</td>
                                    <td>
                                        <asp:TextBox ID="txtReasonForPayment" runat="server" Width="100px" MaxLength="80"
                                            OnKeyUp="return makeUppercase(this.name);" />
                                        <asp:RequiredFieldValidator ID="ReqtxtReasonForPayment" runat="server" ControlToValidate="txtReasonForPayment"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        Account Number at the Receiving Bank :</td>
                                    <td>
                                        <asp:TextBox ID="txtDFIAccountNumber" runat="server" Width="130px" MaxLength="17"
                                            OnKeyUp="return makeUppercase(this.name);" />
                                        <asp:RequiredFieldValidator ID="ReqtxtDFIAccountNumber" runat="server" ControlToValidate="txtDFIAccountNumber"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        Amount you are trying to send:</td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" Width="100px" oncopy="return false" oncut="return false"
                                            onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false"
                                            MaxLength="13"></asp:TextBox>
                                        <asp:Label ID="Label50" runat="server" ForeColor="Red" Text="*" />
                                        <asp:RequiredFieldValidator ID="ReqtxtAmount" runat="server" ControlToValidate="txtAmount"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        Receiver's Name:</td>
                                    <td>
                                        <asp:TextBox ID="txtReceiverName" runat="server" Width="150px" MaxLength="15" OnKeyUp="return makeUppercase(this.name);" />
                                        <asp:RequiredFieldValidator ID="ReqtxtReceiverName" runat="server" ControlToValidate="txtReceiverName"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        Entry Desc.:</td>
                                    <td>
                                        <asp:TextBox ID="txtEntryDesc" runat="server" Width="150px" MaxLength="15" OnKeyUp="return makeUppercase(this.name);" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEntryDesc"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        The Account is</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoBtnReceiverAccountType" CssClass="Normal" RepeatDirection="Horizontal"
                                            runat="server">
                                            <asp:ListItem Value="1">Current Account</asp:ListItem>
                                            <asp:ListItem Value="2">Savings Account</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold"> Transaction Type</td>
                                    <td>
                                        <asp:TextBox ID="txtBatchType" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblMsg" CssClass="NormalRed" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>

                                    <td align="center">
                                        <asp:Button ID="btnSaveAndExit" CssClass="inputlt" runat="server" Text="Generate New Transaction"
                                            Width="180px" OnClick="btnSaveAndExit_Click" OnClientClick="return validateDataField()" /><br />
                                    </td>
                                    <td width="20px">
                                    </td>

                                    <td>
                                        <asp:Button ID="btnExit" runat="server" CssClass="inputlt" Text="Exit" OnClick="btnExit_Click"
                                            Width="100px" /><br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <asp:DataGrid ID="dtgMicrInfo" runat="server" DataKeyField="ACCOUNT" AutoGenerateColumns="false"
                                ShowHeader="false">
                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <table class="LightBorderTable" style="background: lightyellow; width: 160px">
                                                <tr>
                                                    <td>
                                                        MASTER:
                                                    </td>
                                                    <td>
                                                        <%#DataBinder.Eval(Container.DataItem, "MASTER")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ACCOUNT:
                                                    </td>
                                                    <td>
                                                        <%#DataBinder.Eval(Container.DataItem, "ACCOUNT")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        CCY:
                                                    </td>
                                                    <td>
                                                        <%#DataBinder.Eval(Container.DataItem, "CCY")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        TITLE:
                                                    </td>
                                                    <td>
                                                        <%#DataBinder.Eval(Container.DataItem, "TITLE")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        PRODUCT:
                                                    </td>
                                                    <td>
                                                        <%#DataBinder.Eval(Container.DataItem, "PRODUCT")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        SEGMENT:
                                                    </td>
                                                    <td>
                                                        <%#DataBinder.Eval(Container.DataItem, "SEGMENT")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        STATUS:
                                                    </td>
                                                    <td>
                                                        <%#DataBinder.Eval(Container.DataItem, "STATUS")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        RISKS:
                                                    </td>
                                                    <td>
                                                        <%#DataBinder.Eval(Container.DataItem, "RISKS")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
