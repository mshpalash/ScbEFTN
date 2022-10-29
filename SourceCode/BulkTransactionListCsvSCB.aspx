<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkTransactionListCsvSCB.aspx.cs"
    Inherits="EFTN.BulkTransactionListCsvSCB" MaintainScrollPositionOnPostback="true" %>

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
            <div class="Head" align="Center">Uploaded Corporate data to send</div>
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="inputlt" CausesValidation="false" Text="Save" Width="80" OnClientClick="return confirm('Are you sure you want to save these batches?')" OnClick="btnSave_Click" />
                        </td>
                        <td width="80px"></td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" CssClass="inputlt" CausesValidation="false" Text="Cancel" Width="80" OnClientClick="return confirm('Are you sure you want to delete these Batch?')" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="NormalBlue">
                            <asp:Label ID="lblTotalBatch" runat="server"></asp:Label>
                        </td>
                        <td class="NormalBlue">
                            <asp:Label ID="lblTotalTransaction" runat="server"></asp:Label>
                        </td>
                        <td class="NormalBlue">
                            <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblTRSent" runat="server" CssClass="NormalBold" Text="Ready to send batches"></asp:Label>
                            <div style="overflow: scroll; height: 450px;">
                                <table>
                                    <tr>
                                        <td class="NormalBold" align="center">
                                            <asp:DataGrid ID="dtgBatchTransactionSent" runat="Server" BorderWidth="0px" GridLines="None"
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID" AllowPaging="True" OnPageIndexChanged="dtgBatchTransactionSent_PageIndexChanged" PageSize="500">
                                                <AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
                                                <Columns>
                                                    <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="true">
                                                        <HeaderStyle Wrap="True"></HeaderStyle>

                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="BatchType" HeaderText="Type" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>

                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="CompanyName" HeaderText="Company Name" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>

                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SECC" HeaderText="SECC" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>

                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}"
                                                        ItemStyle-Wrap="true" HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>

                                                        <ItemStyle Wrap="True"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="EntryDesc" HeaderText="Description" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>

                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total" ItemStyle-Wrap="False"
                                                        HeaderStyle-Wrap="False">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>

                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}"
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
                        </td>
                    </tr>

                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />

    </form>
</body>
</html>
