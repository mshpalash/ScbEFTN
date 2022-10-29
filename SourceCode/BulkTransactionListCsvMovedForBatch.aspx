<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkTransactionListCsvMovedForBatch.aspx.cs"
    Inherits="EFTN.BulkTransactionListCsvMovedForBatch" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script language="javascript" type="text/javascript">
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

        function onlyNumbers(evt) {
            var e = event || evt;
            var charCode = e.which || e.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

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
            <div class="Head" align="center">Create Batch For Moved Transactions</div>
            <div align="center">
                <table style="">        
                   <%-- <tr>
                        <td>
                            <asp:Label ID="lblAccountNo" runat="server" Text="Account Number" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAccountNo" runat="server" Width="200" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>--%>
                   <%-- <tr>
                        <td>
                            <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name: " Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomerName" runat="server" Width="200" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <asp:Label ID="lblEntryDetail" runat="server" Text="Entry Description: " Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntryDetail" runat="server" Width="200"></asp:TextBox>
                        </td>
                    </tr>           
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="inputlt" CausesValidation="false" Text="Create Batch" Width="205" OnClientClick="return confirm('Are you sure you want to craete batch?')" OnClick="btnSave_Click" />
                        </td>                        
                    </tr>
                </table>
                <table>
                    <tr>                        
                        <td class="NormalBlue">
                            <asp:Label ID="lblTotalMovedTransaction" runat="server"></asp:Label>
                        </td>
                        <td class="NormalBlue">
                            <asp:Label ID="lblMovedTotalAmount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <%--<asp:Label ID="lblGenerateBatch" runat="server" CssClass="NormalBold" Text="Generate Batch For Filtered Transactions"></asp:Label>--%>
                            <div style="overflow: scroll; height: 450px;">
                                <table>
                                     <%--<tr>
                                        <td align="left">
                                            <asp:CheckBox ID="cbxAll" runat="server" Text="Select All" CssClass="NormalBold"
                                                AutoPostBack="true" OnCheckedChanged="cbxAll_CheckedChanged" /> &nbsp;&nbsp;
                                            <a href="BulkTransactionListCsvMovedForBatch.aspx">Create Batch Of Moved Transactions </a>
                                        </td>                                       
                                    </tr>--%>
                                    <tr>
                                        <td class="NormalBold" align="center">
                                            <asp:DataGrid ID="dtgMovedBatchTransactionSent" runat="Server" BorderWidth="0px" GridLines="None"
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="OID" AllowPaging="True" OnPageIndexChanged="dtgMovedBatchTransactionSent_PageIndexChanged" PageSize="500" >
                                                <AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
                                                <Columns>
                                                   <%-- <asp:TemplateColumn HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckMovedList" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn> --%>       
                                                    
                                                    <asp:BoundColumn DataField="OID" HeaderText="OID" Visible="false" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="true">
                                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>                                           

                                                     <asp:BoundColumn DataField="Debit Account No." HeaderText="Account No." ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>

                                                    <asp:BoundColumn DataField="Customer Name" HeaderText="Customer Name" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="true">
                                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>                                                  

                                                    <asp:BoundColumn DataField="Payee Details 1 BO" HeaderText="Payment Info" DataFormatString="{0:N}"
                                                        ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>

                                                    <asp:BoundColumn DataField="Customer ID" HeaderText="Customer ID" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="true">
                                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>

                                                    <asp:BoundColumn DataField="Batch Ref." HeaderText="Batch Ref." ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>

                                                    <asp:BoundColumn DataField="Invoice Amount" HeaderText="Invoice Amount" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>

                                                    <asp:BoundColumn DataField="Payee Name" HeaderText="Payee Name" DataFormatString="{0:d}"
                                                        ItemStyle-Wrap="true" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="True"></ItemStyle>
                                                    </asp:BoundColumn>

                                                    <asp:BoundColumn DataField="Beneficiary Account" HeaderText="Beneficiary Acc." ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>

                                                    <asp:BoundColumn DataField="Customer Ref." HeaderText="Customer Ref." ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>

                                                    <asp:BoundColumn DataField="Payment Ref." HeaderText="Payment Ref." DataFormatString="{0:N}"
                                                        ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>                                                     

                                                </Columns>

                                                <FooterStyle CssClass="GrayBackWhiteFont"></FooterStyle>

                                                <HeaderStyle CssClass="GrayBackWhiteFont"></HeaderStyle>

                                                <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall"></ItemStyle>
                                                <PagerStyle Mode="NumericPages" Position="TopAndBottom" />
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <%-- <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCustEntryDescription" runat="server" Text="Entry Description" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCustEntryDescription" runat="server" Width="100"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" CssClass="inputlt" CausesValidation="false" Text="Create Batch" Width="120" OnClientClick="return confirm('Are you sure you want to create batch?')" OnClick="btnSave_Click" />
                                        </td>
                                    </tr>
                                </table>--%>
                            </div>
                        </td>
                    </tr>

                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
