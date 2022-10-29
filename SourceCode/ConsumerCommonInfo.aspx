<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsumerCommonInfo.aspx.cs"
    Inherits="EFTN.ConsumerScreen3" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FLORA BEFTN</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script type="text/javascript" language="javascript">

        javascript: window.history.forward(1);

        function onlyNumbersWithPrecision(myControl, evt) {
            var e = event || evt;
            var charCode = e.which || e.keyCode;
            var controlValue = document.getElementById(myControl).value;
            var maxLengthAfterPrecision = 2;
            if (IsPrecisionExists(controlValue)) {
                for (i = 0; i < controlValue.length; i++) {
                    if (controlValue.charAt(i) == ".") {
                        if (controlValue.length - (i + 1) > maxLengthAfterPrecision - 1) {
                            return false;
                        }
                    }
                }
            } else {
                if (charCode != 46) {
                    if (controlValue.length > 9) {
                        return false;
                    }
                }
            }

            if (charCode == 46) {
                if (IsPrecisionExists(document.getElementById(myControl).value)) {
                    return false;
                } else {
                    return true;
                }
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
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
            if (controlValue.length > 17) {
                return false;
            }
            return true;
        }

        function validateDataField() {
            var myControl = 'txtAccountNumber';
            var controlValue = document.getElementById(myControl).value

            return confirm('Are you sure you want to save?')
        }

        function leapYear(myControl, myControlMonth, myControlDay) {
            if (document.getElementById(myControl).value.length > 3) {
                if (document.getElementById(myControlMonth).value == 2) {
                    var leapYearDate = (document.getElementById(myControl).value % 4);
                    if (leapYearDate != 0) {
                        if (document.getElementById(myControlDay).value > 28)
                            document.getElementById(myControlDay).value = 28;
                    }
                    else
                        if (document.getElementById(myControlDay).value > 29)
                            document.getElementById(myControlDay).value = 29;
                }
                setNonZeroDate(myControlDay);
                setNonZeroMonth(myControlMonth);
            }
        }

        function onlyMonth(myControl, dayControl) {

            var currentMonth = document.getElementById(myControl).value;

            if (currentMonth > 12)
                document.getElementById(myControl).value = 12;

            if (currentMonth == 4 || currentMonth == 6 || currentMonth == 9 || currentMonth == 11) {
                var currentDate = document.getElementById(dayControl).value;
                if (currentDate > 30)
                    document.getElementById(dayControl).value = 30;
            }
            return true;
        }

        function onlyDay(myControl) {
            if (document.getElementById(myControl).value > 31)
                document.getElementById(myControl).value = 31;
            return true;
        }

        function setNonZeroMonth(controlMonth) {
            var currentMonth = document.getElementById(controlMonth).value;
            if (currentMonth == 0)
                document.getElementById(controlMonth).value = 1;
        }

        function setNonZeroDate(controlDate) {
            var currentDate = document.getElementById(controlDate).value;
            if (currentDate == 0)
                document.getElementById(controlDate).value = 1;
        }

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

        function onlyEFTAccountNumber(evt) {
            var e = event || evt;
            var charCode = e.which || e.keyCode;
            //if (charCode > 31 && (charCode < 48 || charCode > 57))
            if ((charCode >= 45 && charCode <= 57)
                || (charCode >= 65 && charCode <= 90)
                || (charCode >= 97 && charCode <= 122)
                || charCode == 32)
                return true;
            return false;
        }

        function onlyEFTSpecialCharacter(evt) {
            var e = event || evt;
            var charCode = e.which || e.keyCode;
            //if (charCode > 31 && (charCode < 48 || charCode > 57))
            if ((charCode >= 45 && charCode <= 58)
                || (charCode >= 65 && charCode <= 90)
                || (charCode >= 97 && charCode <= 122)
                || charCode == 32
                || charCode == 35
                || charCode == 38
                || charCode == 40
                || charCode == 41)
                return true;
            return false;
        }

    </script>

</head>
<body class="wrap" id="content">
    <form id="form2" method="post" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Other Informations
            </div>
            <table>
                <tr>
                    <td width="100"></td>
                    <td>
                        <table>

                            <tr>
                                <td class="NormalBold">Receiver Name:</td>
                                <td>
                                    <asp:TextBox ID="txtReceiverName" CssClass="inputlt" runat="server" Width="130px"
                                        MaxLength="22" OnKeyUp="return makeUppercase(this.name);" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" /><asp:Label ID="Label2"
                                            Visible="false" Text=" Missing" CssClass="NormalRed" runat="server" />
                                    <asp:RequiredFieldValidator ID="ReqtxtReceiverName" runat="server" ControlToValidate="txtReceiverName"
                                        CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">Receiving Bank Name:</td>
                                <td>
                                    <asp:DropDownList ID="ddListReceivingBank" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddListReceivingBank_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">Branch Name:</td>
                                <td>
                                    <asp:DropDownList ID="ddListBranch" runat="server" AutoPostBack="true" DataTextField="BranchName"
                                        DataValueField="RoutingNo" OnSelectedIndexChanged="ddListBranch_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtRoutingNo" runat="server" MaxLength="9" Width="80px" CssClass="inputlt"
                                        onkeypress="return onlyNumbers();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">Your Account Number with us :</td>
                                <td>
                                    <asp:TextBox ID="txtAccountNumber" CssClass="inputlt" runat="server" Width="130px"
                                        MaxLength="17" oncopy="return false" oncut="return false" onpaste="return false"
                                        onkeypress="return onlyEFTSpecialCharacter();" OnKeyUp="return makeUppercase(this.name);" />
                                    <asp:RequiredFieldValidator ID="ReqtxtAccountNumber" runat="server" ControlToValidate="txtAccountNumber"
                                        CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    <asp:Button ID="CheckeBBS" CausesValidation="false" CssClass="inputlt" Text="Check CBS"
                                        runat="server" OnClick="CheckeBBS_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">Transaction Type:</td>
                                <td>
                                    <asp:TextBox ID="txtTransactionType" runat="server" ReadOnly="true" Width="40px" CssClass="auto-style1">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">Currency:</td>
                                <td>
                                    <asp:TextBox ID="txtCurrency" runat="server" ReadOnly="true" Width="30px" CssClass="inputlt">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">Reason For Payment:</td>
                                <td>
                                    <asp:TextBox ID="txtPaymentReason" runat="server" ReadOnly="true" Width="85px" CssClass="inputlt">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">Amout you are trying to send:</td>
                                <td>
                                    <asp:TextBox ID="txtAmount" CssClass="inputlt" runat="server" Width="130px" oncopy="return false"
                                        oncut="return false" onkeypress="return onlyNumbersWithPrecision(this.name);"
                                        onpaste="return false" MaxLength="13"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqtxtAmount" runat="server" ControlToValidate="txtAmount"
                                        CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">Payment Info:</td>
                                <td>
                                    <asp:TextBox ID="txtReasonForPayment" CssClass="inputlt" runat="server" Width="240"
                                        MaxLength="80" OnKeyUp="return makeUppercase(this.name);" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" />
                                    <asp:RequiredFieldValidator ID="ReqtxtReasonForPayment" runat="server" ControlToValidate="txtReasonForPayment"
                                        CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">Account Number at the Receiving Bank :</td>
                                <td>
                                    <asp:TextBox ID="txtDFIAccountNumber" CssClass="inputlt" runat="server" Width="130px"
                                        MaxLength="17" OnKeyUp="return makeUppercase(this.name);" onkeypress="return onlyEFTSpecialCharacter();" />
                                    <asp:RequiredFieldValidator ID="ReqtxtDFIAccountNumber" runat="server" ControlToValidate="txtDFIAccountNumber"
                                        CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td class="NormalBold">Receiver's ID with sender :</td>
                                <td>
                                    <asp:TextBox ID="txtReceiverID" CssClass="inputlt" runat="server" Width="130px" MaxLength="15"
                                        OnKeyUp="return makeUppercase(this.name);" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" />
                                    <asp:RequiredFieldValidator ID="ReqtxtReceiverID" runat="server" ControlToValidate="txtReceiverID"
                                        CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td class="NormalBold">Account Type:</td>
                                <td>
                                    <asp:RadioButtonList ID="rdoBtnReceiverAccountType" CssClass="Normal" RepeatDirection="Horizontal"
                                        runat="server">
                                        <asp:ListItem Value="1">Current Account</asp:ListItem>
                                        <asp:ListItem Value="2">Savings Account</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblMsg" CssClass="NormalRed" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="NormalBold">Check If Remittance</td>
                                <td>
                                    <asp:CheckBox runat="server" ID="cbxRemittance" OnClick="alert('You checked on Remittance!');" /></td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlChargeCategory" runat="server" Visible="false">
                            <table>
                                <tr>
                                    <td class="NormalBold">Select Charge Category:</td>
                                    <td>
                                        <asp:DropDownList ID="ddListChargeCategoryList" runat="server" OnSelectedIndexChanged="ddListChargeCategoryList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlChargeCode" runat="server" Visible="false">
                            <table>
                                <tr>
                                    <td class="NormalBold">Select Charge Code</td>
                                    <td>
                                        <asp:DropDownList ID="ddListChargeCode" runat="server">
                                            <asp:ListItem Text="Charge Code 1" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Charge Code 2" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Charge Code 3" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Charge Code 4" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Charge Code 5" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Charge Code 6" Value="6"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:Panel ID="pnlCTX" runat="server">
                            <table>
                                <tr>
                                    <td class="NormalBold">
                                        <asp:Label ID="lblInvoiceNumber" runat="server" Text="Invoice Number"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInvoiceNumber" runat="server" MaxLength="10" OnKeyUp="return makeUppercase(this.name);" onkeypress="return onlyEFTSpecialCharacter(this.name);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date"></asp:Label>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td class="NormalBold">
                                                    <asp:Label ID="Label5" runat="server" Text="DD"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInvoiceDateDay" runat="server" MaxLength="2" Width="20px" oncopy="return false"
                                                        oncut="return false" onkeypress="return onlyNumbers();" onpaste="return false"
                                                        onkeyup="return onlyDay(this.name);"></asp:TextBox>
                                                </td>
                                                <td class="NormalBold">
                                                    <asp:Label ID="Label6" runat="server" Text="MM"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInvoiceDateMonth" runat="server" MaxLength="2" Width="20px" oncopy="return false"
                                                        oncut="return false" onkeypress="return onlyNumbers();" onpaste="return false"
                                                        onkeyup="return onlyMonth(this.name, 'txtInvoiceDateDay');"></asp:TextBox>
                                                </td>
                                                <td class="NormalBold">
                                                    <asp:Label ID="Label7" runat="server" Text="YYYY"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInvoiceDateYear" runat="server" MaxLength="4" Width="40px" oncopy="return false"
                                                        oncut="return false" onkeypress="return onlyNumbers();" onpaste="return false"
                                                        onkeyup="return leapYear(this.name, 'txtInvoiceDateMonth', 'txtInvoiceDateDay');"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        <asp:Label ID="Label1" runat="server" Text="Invoice Gross Amount"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInvoiceGrossAmount" runat="server" MaxLength="13" onkeypress="return onlyNumbersWithPrecision(this.name);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        <asp:Label ID="Label3" runat="server" Text="Invoice Amount Paid"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInvoiceAmountPaid" runat="server" MaxLength="13" onkeypress="return onlyNumbersWithPrecision(this.name);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        <asp:Label ID="Label4" runat="server" Text="Purchase Order"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPurchaseOrder" runat="server" MaxLength="10" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" OnKeyUp="return makeUppercase(this.name);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        <asp:Label ID="Label8" runat="server" Text="Adjustment Amount"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdjustmentAmount" runat="server" MaxLength="13" onkeypress="return onlyNumbersWithPrecision(this.name);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        <asp:Label ID="Label9" runat="server" Text="Adjustment Code"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdjustmentCode" runat="server" MaxLength="2" onkeypress="return onlyNumbers();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">
                                        <asp:Label ID="Label10" runat="server" Text="Adjustment Description"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdjustmentDescription" runat="server" MaxLength="40" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" OnKeyUp="return makeUppercase(this.name);"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table>
                            <tr>
                                <td style="height: 21px">
                                    <asp:Button ID="btnSaveAndSameBatch" CssClass="inputlt" runat="server" Text="Save"
                                        Width="250px" OnClick="btnSaveAndSameBatch_Click" OnClientClick="return validateDataField()" />
                                </td>
                                <td style="height: 21px">
                                    <asp:Button ID="btnExit" runat="server" CssClass="inputlt" Text="Exit" OnClick="btnExit_Click"
                                        Width="100px" />
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
                                                <td>MASTER:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "MASTER")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>ACCOUNT:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "ACCOUNT")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>CCY:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "CCY")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>TITLE:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "TITLE")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>PRODUCT:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "PRODUCT")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>SEGMENT:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "SEGMENT")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>STATUS:
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "STATUS")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>RISKS:
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
            <br />
            <div>
                <table>
                    <tr>
                        <td class="NormalBold">
                            <asp:Label ID="lblMsgBatchNumber" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalBold">
                            <asp:Label ID="lblTotalItem" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalBold">
                            <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="overflow: scroll; height: 450px; width: 900px">
                <asp:DataGrid ID="dtgXcelUpload" runat="Server" Width="600px" BorderWidth="0px"
                    GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                    FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont" HeaderStyle-ForeColor="#FFFFFF"
                    ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff"
                    DataKeyField="EDRID" OnItemCommand="dtgXcelUpload_ItemCommand" AllowSorting="True" OnSortCommand="dtgXcelUpload_SortCommand" OnPageIndexChanged="dtgXcelUpload_PageIndexChanged" AllowPaging="True" PageSize="500">
                    <Columns>
                        <asp:EditCommandColumn CausesValidation="False" EditText="Edit" UpdateText="Update"
                            CancelText="Cancel">
                            <ItemStyle Width="30px" />
                        </asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="PaymentInfo" SortExpression="PaymentInfo">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "PaymentInfo")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPaymentInfo" MaxLength="80" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PaymentInfo") %>' OnKeyPress="return onlyEFTSpecialCharacter(this.name);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPaymentInfo" CssClass="NormalRed"
                                    runat="server" ControlToValidate="txtPaymentInfo" ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="SenderAccNumber" SortExpression="AccountNo">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "AccountNo")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAccountNo" MaxLength="17" runat="server" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" Text='<%#DataBinder.Eval(Container.DataItem,"AccountNo") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAccountNo" CssClass="NormalRed"
                                    runat="server" ControlToValidate="txtAccountNo" ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="ReceivingBankRoutingNo" SortExpression="ReceivingBankRoutingNo">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "ReceivingBankRoutingNo")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtReceivingBankRoutingNo" MaxLength="9" runat="server" OnKeyPress="return onlyNumbers();" Text='<%#DataBinder.Eval(Container.DataItem,"ReceivingBankRoutingNo") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorReceivingBankRoutingNo" CssClass="NormalRed"
                                    runat="server" ControlToValidate="txtReceivingBankRoutingNo" ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="DFIAccountNo" SortExpression="DFIAccountNo">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDFIAccountNo" MaxLength="17" runat="server" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" Text='<%#DataBinder.Eval(Container.DataItem,"DFIAccountNo") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDFIAccountNo" CssClass="NormalRed"
                                    runat="server" ControlToValidate="txtDFIAccountNo" ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Amount" SortExpression="Amount">
                            <ItemTemplate>
                                <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAmount" MaxLength="13" runat="server" OnKeyPress="return onlyNumbersWithPrecision(this.name);" Text='<%#DataBinder.Eval(Container.DataItem,"Amount") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAmount" CssClass="NormalRed"
                                    runat="server" ControlToValidate="txtAmount" ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="IdNumber" SortExpression="IdNumber">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "IdNumber")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIdNumber" MaxLength="15" runat="server" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" Text='<%#DataBinder.Eval(Container.DataItem,"IdNumber") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorIdNumber" CssClass="NormalRed"
                                    runat="server" ControlToValidate="txtIdNumber" ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="ReceiverName" SortExpression="ReceiverName">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtReceiverName" MaxLength="22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ReceiverName") %>' OnKeyPress="return onlyEFTSpecialCharacter(this.name);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorReceiverName" CssClass="NormalRed"
                                    runat="server" ControlToValidate="txtReceiverName" ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="CTX_InvoiceNumber" SortExpression="CTX_InvoiceNumber">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CTX_InvoiceNumber")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtInvoiceNumber" MaxLength="10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_InvoiceNumber") %>' OnKeyPress="return onlyEFTSpecialCharacter(this.name);"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="CTX_InvoiceDate(yyyyMMdd)" SortExpression="CTX_InvoiceDate">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CTX_InvoiceDate")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtInvoiceDate" MaxLength="10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_InvoiceDate") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="CTX_InvoiceGrossAmount" SortExpression="CTX_InvoiceGrossAmount">
                            <ItemTemplate>
                                <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "CTX_InvoiceGrossAmount"))%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtInvoiceGrossAmount" MaxLength="13" runat="server" OnKeyPress="return onlyNumbersWithPrecision(this.name);" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_InvoiceGrossAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="CTX_InvoiceAmountPaid" SortExpression="CTX_InvoiceAmountPaid">
                            <ItemTemplate>
                                <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "CTX_InvoiceAmountPaid"))%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtInvoiceAmountPaid" MaxLength="13" runat="server" OnKeyPress="return onlyNumbersWithPrecision(this.name);" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_InvoiceAmountPaid") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="CTX_PurchaseOrder" SortExpression="CTX_PurchaseOrder">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CTX_PurchaseOrder")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPurchaseOrder" MaxLength="10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_InvoiceNumber") %>' OnKeyPress="return onlyEFTSpecialCharacter(this.name);"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="CTX_AdjustmentAmount" SortExpression="CTX_AdjustmentAmount">
                            <ItemTemplate>
                                <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "CTX_AdjustmentAmount"))%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAdjustmentAmount" MaxLength="13" runat="server" OnKeyPress="return onlyNumbersWithPrecision(this.name);" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_AdjustmentAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="CTX_AdjustmentCode" SortExpression="CTX_AdjustmentCode">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CTX_AdjustmentCode")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAdjustmentCode" MaxLength="2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_AdjustmentCode") %>' OnKeyPress="return onlyNumbers();"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="CTX_AdjustmentDescription" SortExpression="CTX_AdjustmentDescription">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CTX_AdjustmentDescription")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAdjustmentDescription" MaxLength="40" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_AdjustmentDescription") %>' OnKeyPress="return onlyEFTSpecialCharacter(this.name);"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>


                        <asp:ButtonColumn CommandName="Delete" Text="Delete" CausesValidation="false"></asp:ButtonColumn>

                    </Columns>
                    <FooterStyle CssClass="GrayBackWhiteFont" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall" />
                    <HeaderStyle CssClass="GrayBackWhiteFont" ForeColor="White" />
                </asp:DataGrid>
            </div>
            <br />
        </div>
    </form>
</body>
</html>
