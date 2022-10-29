<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StandingOrderEntryCredit.aspx.cs" Inherits="EFTN.StandingOrderEntryCredit" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora Limited System</title>
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

        //    function onlyACCNumbers(myControl, evt) {
        //        var e = event || evt;
        //        var charCode = e.which || e.keyCode;
        //        if (charCode > 31 && (charCode < 47 || charCode > 57))
        //            return false;

        //        var controlValue = document.getElementById(myControl).value;
        //        var re = "-";
        //        while (!IsNumeric(controlValue)) {
        //            controlValue = controlValue.replace(re, "");
        //        }
        //        if (controlValue.length > 11) {
        //            return false;
        //        }
        //        return true;
        //    }

        function validateDataField() {
            var myControl = 'txtAccountNumber';
            var controlValue = document.getElementById(myControl).value;

            //        if(controlValue.length < 13)
            //        {
            //            alert("Invalid Account Number");
            //            document.getElementById(myControl).focus();
            //            return false;
            //        }

            var myControlRout = 'txtRoutingNo';
            var controlRoutValue = document.getElementById(myControlRout).value;

            if (controlRoutValue.length < 9) {
                alert("Invalid Routing Number");
                document.getElementById(myControlRout).focus();
                return false;
            }
            return confirm('Are you sure you want to save?')
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
            <div align="center">
                <table>
                    <tr height="10px">
                        <td></td>
                    </tr>
                    <tr>
                        <td align="center"><a href="StandingOrderManagement.aspx" class="CommandButton">Standing Order Management</a></td>
                    </tr>
                </table>
            </div>
            <div class="Head" align="Center">
                Standing Order Credit
            </div>
            <div>
                <table>
                    <tr>
                        <td width="100"></td>
                        <td>
                            <table border="0">
                                <tr>
                                    <td class="NormalBold">Company ID</td>
                                    <td>
                                        <asp:TextBox ID="txtCompanyID" runat="server" Width="150px" MaxLength="10" onkeypress="return onlyEFTSpecialCharacter();" OnKeyUp="return makeUppercase(this.name);" />
                                        <asp:RequiredFieldValidator ID="ReqtxtCompanyID" runat="server" ControlToValidate="txtCompanyID"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Company Name:</td>
                                    <td>
                                        <asp:TextBox ID="txtCompanyName" runat="server" Width="150px" MaxLength="16" onkeypress="return onlyEFTSpecialCharacter();" OnKeyUp="return makeUppercase(this.name);" />
                                        <asp:RequiredFieldValidator ID="ReqtxtCompanyName" runat="server" ControlToValidate="txtCompanyName"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Company Entry Description:</td>
                                    <td>
                                        <asp:TextBox ID="txtCompanyEntryDescription" runat="server" Width="150px" MaxLength="10" onkeypress="return onlyEFTSpecialCharacter();" OnKeyUp="return makeUppercase(this.name);" />
                                        <asp:RequiredFieldValidator ID="ReqtxtCompanyEntryDescription" runat="server" ControlToValidate="txtCompanyEntryDescription"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Receiver's Name:</td>
                                    <td>
                                        <asp:TextBox ID="txtReceiverName" runat="server" Width="150px" MaxLength="15" onkeypress="return onlyEFTSpecialCharacter();" OnKeyUp="return makeUppercase(this.name);" />
                                        <asp:RequiredFieldValidator ID="ReqtxtReceiverName" runat="server" ControlToValidate="txtReceiverName"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Receiver's ID:</td>
                                    <td>
                                        <asp:TextBox ID="txtReceiverID" runat="server" Width="150px" MaxLength="22" onkeypress="return onlyAlphaNumeric();" OnKeyUp="return makeUppercase(this.name);" />
                                        <asp:RequiredFieldValidator ID="ReqTxtReceiverID" runat="server" ControlToValidate="txtReceiverID"
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
                                    <td class="NormalBold">Currency type
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CurrencyDdList" runat="server"></asp:DropDownList>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Your Account Number with us :</td>
                                    <td>
                                        <asp:TextBox ID="txtAccountNumber" runat="server" Width="130px" MaxLength="17" oncopy="return false"
                                            oncut="return false" onpaste="return false" onkeypress="return onlyEFTSpecialCharacter();" />
                                        <asp:RequiredFieldValidator ID="ReqtxtAccountNumber" runat="server" ControlToValidate="txtAccountNumber"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                        <asp:Button ID="CheckeBBS" CausesValidation="false" CssClass="inputlt" Text="Check CBS"
                                            runat="server" OnClick="CheckeBBS_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Transaction Amount:</td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" Width="100px" oncopy="return false" oncut="return false"
                                            onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false"
                                            MaxLength="13"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ReqtxtAmount" runat="server" ControlToValidate="txtAmount"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Reason For Payment:</td>
                                    <td>
                                        <asp:TextBox ID="txtReasonForPayment" runat="server" Width="240px" MaxLength="80"
                                            OnKeyUp="return makeUppercase(this.name);" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" />
                                        <asp:RequiredFieldValidator ID="ReqtxtReasonForPayment" runat="server" ControlToValidate="txtReasonForPayment"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Account Number at the Receiving Bank :</td>
                                    <td>
                                        <asp:TextBox ID="txtDFIAccountNumber" runat="server" Width="130px" MaxLength="17" onkeypress="return onlyEFTSpecialCharacter();" />
                                        <asp:RequiredFieldValidator ID="ReqtxtDFIAccountNumber" runat="server" ControlToValidate="txtDFIAccountNumber"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>


                                <tr>
                                    <td class="NormalBold">The Account is</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoBtnReceiverAccountType" CssClass="Normal" RepeatDirection="Horizontal"
                                            runat="server">
                                            <asp:ListItem Value="1" Selected="True">Current Account</asp:ListItem>
                                            <asp:ListItem Value="2">Savings Account</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="NormalBold">Standing Order Start Date:
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td class="NormalBold">
                                                                            <asp:Label ID="lblDate" runat="server" Text="Day"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlistDay" runat="server">
                                                                                <asp:ListItem>01</asp:ListItem>
                                                                                <asp:ListItem>02</asp:ListItem>
                                                                                <asp:ListItem>03</asp:ListItem>
                                                                                <asp:ListItem>04</asp:ListItem>
                                                                                <asp:ListItem>05</asp:ListItem>
                                                                                <asp:ListItem>06</asp:ListItem>
                                                                                <asp:ListItem>07</asp:ListItem>
                                                                                <asp:ListItem>08</asp:ListItem>
                                                                                <asp:ListItem>09</asp:ListItem>
                                                                                <asp:ListItem>10</asp:ListItem>
                                                                                <asp:ListItem>11</asp:ListItem>
                                                                                <asp:ListItem>12</asp:ListItem>
                                                                                <asp:ListItem>13</asp:ListItem>
                                                                                <asp:ListItem>14</asp:ListItem>
                                                                                <asp:ListItem>15</asp:ListItem>
                                                                                <asp:ListItem>16</asp:ListItem>
                                                                                <asp:ListItem>17</asp:ListItem>
                                                                                <asp:ListItem>18</asp:ListItem>
                                                                                <asp:ListItem>19</asp:ListItem>
                                                                                <asp:ListItem>20</asp:ListItem>
                                                                                <asp:ListItem>21</asp:ListItem>
                                                                                <asp:ListItem>22</asp:ListItem>
                                                                                <asp:ListItem>23</asp:ListItem>
                                                                                <asp:ListItem>24</asp:ListItem>
                                                                                <asp:ListItem>25</asp:ListItem>
                                                                                <asp:ListItem>26</asp:ListItem>
                                                                                <asp:ListItem>27</asp:ListItem>
                                                                                <asp:ListItem>28</asp:ListItem>
                                                                                <asp:ListItem>29</asp:ListItem>
                                                                                <asp:ListItem>30</asp:ListItem>
                                                                                <asp:ListItem>31</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="NormalBold">
                                                                            <asp:Label ID="Label1" runat="server" Text="Month"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlistMonth" runat="server">
                                                                                <asp:ListItem Value="1">Jan</asp:ListItem>
                                                                                <asp:ListItem Value="2">Feb</asp:ListItem>
                                                                                <asp:ListItem Value="3">Mar</asp:ListItem>
                                                                                <asp:ListItem Value="4">Apr</asp:ListItem>
                                                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                                                <asp:ListItem Value="6">Jun</asp:ListItem>
                                                                                <asp:ListItem Value="7">Jul</asp:ListItem>
                                                                                <asp:ListItem Value="8">Aug</asp:ListItem>
                                                                                <asp:ListItem Value="9">Sep</asp:ListItem>
                                                                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                                                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                                                                <asp:ListItem Value="12">Dec</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="NormalBold">
                                                                            <asp:Label ID="Label46" runat="server" Text="Year"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlistYear" runat="server">
                                                                                <asp:ListItem>2011</asp:ListItem>
                                                                                <asp:ListItem>2012</asp:ListItem>
                                                                                <asp:ListItem>2013</asp:ListItem>
                                                                                <asp:ListItem>2014</asp:ListItem>
                                                                                <asp:ListItem>2015</asp:ListItem>
                                                                                <asp:ListItem>2016</asp:ListItem>
                                                                                <asp:ListItem>2017</asp:ListItem>
                                                                                <asp:ListItem>2018</asp:ListItem>
                                                                                <asp:ListItem>2019</asp:ListItem>
                                                                                <asp:ListItem>2020</asp:ListItem>
                                                                                <asp:ListItem>2021</asp:ListItem>
                                                                                <asp:ListItem>2022</asp:ListItem>                                                                                                                                                                <asp:ListItem>2023</asp:ListItem>
                                                                                <asp:ListItem>2024</asp:ListItem>
                                                                                <asp:ListItem>2025</asp:ListItem>
                                                                                <asp:ListItem>2026</asp:ListItem>
                                                                                <asp:ListItem>2027</asp:ListItem>
                                                                                <asp:ListItem>2028</asp:ListItem>
                                                                                <asp:ListItem>2029</asp:ListItem>
                                                                                <asp:ListItem>2030</asp:ListItem>
                                                                                <asp:ListItem>2031</asp:ListItem>
                                                                                <asp:ListItem>2032</asp:ListItem>
                                                                                <asp:ListItem>2033</asp:ListItem>
                                                                                <asp:ListItem>2034</asp:ListItem>
                                                                                <asp:ListItem>2035</asp:ListItem>
                                                                                <asp:ListItem>2036</asp:ListItem>
                                                                                <asp:ListItem>2037</asp:ListItem>
                                                                                <asp:ListItem>2038</asp:ListItem>
                                                                                <asp:ListItem>2039</asp:ListItem>
                                                                                <asp:ListItem>2040</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>

                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="NormalBold">Standing Order End Date:
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td class="NormalBold">
                                                    <asp:Label ID="Label2" runat="server" Text="Day"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlistDayEnd" runat="server">
                                                        <asp:ListItem>01</asp:ListItem>
                                                        <asp:ListItem>02</asp:ListItem>
                                                        <asp:ListItem>03</asp:ListItem>
                                                        <asp:ListItem>04</asp:ListItem>
                                                        <asp:ListItem>05</asp:ListItem>
                                                        <asp:ListItem>06</asp:ListItem>
                                                        <asp:ListItem>07</asp:ListItem>
                                                        <asp:ListItem>08</asp:ListItem>
                                                        <asp:ListItem>09</asp:ListItem>
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>11</asp:ListItem>
                                                        <asp:ListItem>12</asp:ListItem>
                                                        <asp:ListItem>13</asp:ListItem>
                                                        <asp:ListItem>14</asp:ListItem>
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>16</asp:ListItem>
                                                        <asp:ListItem>17</asp:ListItem>
                                                        <asp:ListItem>18</asp:ListItem>
                                                        <asp:ListItem>19</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>21</asp:ListItem>
                                                        <asp:ListItem>22</asp:ListItem>
                                                        <asp:ListItem>23</asp:ListItem>
                                                        <asp:ListItem>24</asp:ListItem>
                                                        <asp:ListItem>25</asp:ListItem>
                                                        <asp:ListItem>26</asp:ListItem>
                                                        <asp:ListItem>27</asp:ListItem>
                                                        <asp:ListItem>28</asp:ListItem>
                                                        <asp:ListItem>29</asp:ListItem>
                                                        <asp:ListItem>30</asp:ListItem>
                                                        <asp:ListItem>31</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="NormalBold">
                                                    <asp:Label ID="Label6" runat="server" Text="Month"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlistMonthEnd" runat="server">
                                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                                        <asp:ListItem Value="4">Apr</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">Jun</asp:ListItem>
                                                        <asp:ListItem Value="7">Jul</asp:ListItem>
                                                        <asp:ListItem Value="8">Aug</asp:ListItem>
                                                        <asp:ListItem Value="9">Sep</asp:ListItem>
                                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="NormalBold">
                                                    <asp:Label ID="Label7" runat="server" Text="Year"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlistYearEnd" runat="server">
                                                        <asp:ListItem>2011</asp:ListItem>
                                                        <asp:ListItem>2012</asp:ListItem>
                                                        <asp:ListItem>2013</asp:ListItem>
                                                        <asp:ListItem>2014</asp:ListItem>
                                                        <asp:ListItem>2015</asp:ListItem>
                                                        <asp:ListItem>2016</asp:ListItem>
                                                        <asp:ListItem>2017</asp:ListItem>
                                                        <asp:ListItem>2018</asp:ListItem>
                                                        <asp:ListItem>2019</asp:ListItem>
                                                        <asp:ListItem>2020</asp:ListItem>
                                                        <asp:ListItem>2021</asp:ListItem>
                                                        <asp:ListItem>2022</asp:ListItem>                                                                                                                                                                <asp:ListItem>2023</asp:ListItem>
                                                        <asp:ListItem>2023</asp:ListItem>
                                                        <asp:ListItem>2024</asp:ListItem>
                                                        <asp:ListItem>2025</asp:ListItem>
                                                        <asp:ListItem>2026</asp:ListItem>
                                                        <asp:ListItem>2027</asp:ListItem>
                                                        <asp:ListItem>2028</asp:ListItem>
                                                        <asp:ListItem>2029</asp:ListItem>
                                                        <asp:ListItem>2030</asp:ListItem>
                                                        <asp:ListItem>2031</asp:ListItem>
                                                        <asp:ListItem>2032</asp:ListItem>
                                                        <asp:ListItem>2033</asp:ListItem>
                                                        <asp:ListItem>2034</asp:ListItem>
                                                        <asp:ListItem>2035</asp:ListItem>
                                                        <asp:ListItem>2036</asp:ListItem>
                                                        <asp:ListItem>2037</asp:ListItem>
                                                        <asp:ListItem>2038</asp:ListItem>
                                                        <asp:ListItem>2039</asp:ListItem>
                                                        <asp:ListItem>2040</asp:ListItem>
                                                        <asp:ListItem>2041</asp:ListItem>
                                                        <asp:ListItem>2042</asp:ListItem>
                                                        <asp:ListItem>2043</asp:ListItem>
                                                        <asp:ListItem>2044</asp:ListItem>
                                                        <asp:ListItem>2045</asp:ListItem>
                                                        <asp:ListItem>2046</asp:ListItem>
                                                        <asp:ListItem>2047</asp:ListItem>
                                                        <asp:ListItem>2048</asp:ListItem>
                                                        <asp:ListItem>2049</asp:ListItem>
                                                        <asp:ListItem>2050</asp:ListItem>
                                                        <asp:ListItem>2051</asp:ListItem>
                                                        <asp:ListItem>2052</asp:ListItem>
                                                        <asp:ListItem>2053</asp:ListItem>
                                                        <asp:ListItem>2054</asp:ListItem>
                                                        <asp:ListItem>2055</asp:ListItem>
                                                        <asp:ListItem>2056</asp:ListItem>
                                                        <asp:ListItem>2057</asp:ListItem>
                                                        <asp:ListItem>2058</asp:ListItem>
                                                        <asp:ListItem>2059</asp:ListItem>
                                                        <asp:ListItem>2060</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="NormalBold">Transaction Frequency
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddListTransactionFrequency" runat="server">
                                            <asp:ListItem Value="30" Text="Monthly"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Daily"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="Weekly"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="Fortnightly"></asp:ListItem>
                                            <asp:ListItem Value="90" Text="Quarter-Yearly"></asp:ListItem>
                                            <asp:ListItem Value="180" Text="Half-Yearly"></asp:ListItem>
                                            <asp:ListItem Value="365" Text="Yearly"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Ordering Customer
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOrderingCustomer" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ReqtxtOrderingCustomer" runat="server" ControlToValidate="txtOrderingCustomer"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <%-- <tr>
                                    <td class="NormalBold">
                                        Standing Order Reference
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStandingOrderReference" runat="server"></asp:TextBox>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="NormalBold">Customer Letter Reference No
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCustomerLetterReferenceNo" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ReqtxtCustomerLetterReferenceNo" runat="server" ControlToValidate="txtCustomerLetterReferenceNo"
                                            CssClass="NormalRed" ErrorMessage="* Missing"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblMsg" CssClass="NormalRed" runat="server"></asp:Label>
                                    </td>
                                </tr>

                            </table>
                            <asp:Panel ID="pnlSTDCharge" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td class="NormalBold">Charge
                                        </td>
                                        <td class="NormalBold">
                                            <asp:RadioButtonList ID="rdoBtnCharge" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
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
                            <table>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSaveAndSameBatch" Width="100px" CssClass="inputlt" runat="server"
                                            Text="Save" OnClick="btnSaveAndSameBatch_Click"
                                            OnClientClick="return validateDataField()" /><br />
                                    </td>
                                    <%--                                    <td>
                                        <asp:Button ID="btnExit" runat="server" CssClass="inputlt" Text="Exit" OnClick="btnExit_Click"
                                            Width="100px" /><br />
                                    </td>--%>
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
                    <tr>
                        <td width="100"></td>
                        <td colspan="2">
                            <asp:Panel ID="pnlRout" runat="server" GroupingText="Just for checking routing number(Optional)" CssClass="NormalBold">
                                <table border="0" bgcolor="silver">
                                    <tr>
                                        <td class="NormalBold">Routing Number:</td>
                                        <td>
                                            <asp:TextBox ID="txtRoutnoChk" runat="server" Width="130px" MaxLength="9" oncopy="return false"
                                                oncut="return false" onpaste="return false" />
                                            <asp:Button ID="BtnRoutChk" CausesValidation="false" CssClass="inputlt" Text="Check Routing Number"
                                                runat="server" OnClick="BtnRoutChk_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="NormalBold" colspan="2">
                                            <asp:Label ID="lblBank" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="NormalBold" colspan="2">
                                            <asp:Label ID="lblBranch" runat="Server"></asp:Label></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
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
            <div style="overflow: scroll; height: 80px; width: 900px">
                <asp:DataGrid ID="dtgXcelUpload" runat="Server" Width="600px" BorderWidth="0px"
                    GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                    FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont" HeaderStyle-ForeColor="#FFFFFF"
                    ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff"
                    DataKeyField="StandingOrderBatchID" PageSize="500">
                    <Columns>
                        <asp:TemplateColumn HeaderText="BeginDate" SortExpression="BeginDate">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BeginDate")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="EndDate" SortExpression="EndDate">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "EndDate")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DFIAccountNo" SortExpression="DFIAccountNo">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Currency" SortExpression="Currency">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Currency")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="AccountNo" SortExpression="AccountNo">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "AccountNo")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Amount" SortExpression="Amount">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Amount")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="IdNumber" SortExpression="IdNumber">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "IdNumber")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ReceiverName" SortExpression="ReceiverName">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="PaymentInfo" SortExpression="PaymentInfo">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "PaymentInfo")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="OrderingCustomer" SortExpression="OrderingCustomer">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "OrderingCustomer")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="StandingOrderReference" SortExpression="StandingOrderReference">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "StandingOrderReference")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="CustomerLetterReference" SortExpression="CustomerLetterReference">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CustomerLetterReference")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Charge" SortExpression="Charge">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Charge")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="BatchType" SortExpression="BatchType">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BatchType")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <FooterStyle CssClass="GrayBackWhiteFont" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall" />
                    <HeaderStyle CssClass="GrayBackWhiteFont" ForeColor="White" />
                </asp:DataGrid>
            </div>
            <div style="overflow: scroll; height: 250px; width: 900px">
                <asp:DataGrid ID="dtgStandingOrderDate" runat="Server" Width="600px" BorderWidth="0px"
                    GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                    FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont" HeaderStyle-ForeColor="#FFFFFF"
                    ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff"
                    DataKeyField="StandingOrderBatchDetailsID" PageSize="500">
                    <Columns>
                        <asp:TemplateColumn HeaderText="EffectiveEntryDate">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "EffectiveEntryDate")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
            <br />
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
