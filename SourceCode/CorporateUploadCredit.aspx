<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorporateUploadCredit.aspx.cs"
    Inherits="EFTN.CorporateUploadCredit" MaintainScrollPositionOnPostback="true" %>

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
            <div class="Head" align="Center" style="background-color: forestgreen">
                <h1 style="color: white">Upload Corporate data for CREDIT</h1>
            </div>

            <div>
                <table>
                    <tr>
                        <td width="100"></td>
                        <td>
                            <table>
                                <%-- <tr>
                                    <td class="NormalBold">Currency type
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CurrencyDdList" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <%--<a href="BulkTransactionListCsvFilterForBatch.aspx">Filter Bulk Transactions For Batch</a>--%>
                                    <td class="NormalBold">Your Company TIN:</td>
                                    <td>
                                        <asp:TextBox ID="txtCompanyTIN" runat="server" MaxLength="16" Width="135px" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" />
                                        <asp:Label ID="lblcompanyTIN" runat="server" CssClass="NormalRed" Text="*"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Type of Payment:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlPaymentType" runat="server">
                                            <asp:ListItem Selected="True" Text="Corp to Ind - PPD" Value="6"></asp:ListItem>
                                            <asp:ListItem Selected="False" Text="Corporate To Corporate (Cash Concentration)  - CCD" Value="1"></asp:ListItem>
                                            <asp:ListItem Selected="False" Text="Bill To Corporate (Trade Payment) - CTX" Value="8"></asp:ListItem>
                                            <asp:ListItem Selected="False" Text="Individual To Individual (Trade Payment) - CIE" Value="2"></asp:ListItem>

                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Your Comapany Name:</td>
                                    <td>
                                        <asp:TextBox ID="txtCompanyName" runat="server" Width="135px" MaxLength="16" OnKeyUp="return makeUppercase(this.name);" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" />
                                        <asp:Label ID="lblCompanyName" runat="server" CssClass="NormalRed" Text="*"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Reason for Payment:</td>
                                    <td>
                                        <asp:TextBox ID="txtReasonForPayment" runat="server" Width="135px" MaxLength="10" OnKeyUp="return makeUppercase(this.name);" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" />
                                        <asp:Label ID="lblReason" runat="server" CssClass="NormalRed" Text="*"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td class="NormalBold"></td>
                                    <td class="NormalBold">
                                        <asp:RadioButtonList ID="rdoBtnTransactionType" RepeatDirection="Horizontal" runat="server" Visible="false">
                                            <asp:ListItem Value="Credit" Selected="True">Credit</asp:ListItem>
                                            <asp:ListItem Value="Debit">Debit</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlCBSSettlementDay" runat="server">
                                <table>
                                    <tr>
                                        <td class="NormalBold">[Settlement On: (Only for Debit Transaction)</td>
                                        <td class="NormalBold">
                                            <asp:RadioButtonList ID="rdoBtnSettlementDay" RepeatDirection="Horizontal" runat="server">
                                                <asp:ListItem Value="1" Selected="True">Day 1</asp:ListItem>
                                                <asp:ListItem Value="2">Day 2</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td class="NormalBold">]</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table>
                                <tr>
                                    <td class="NormalBold">Upload for :</td>
                                    <td class="NormalBold">
                                        <asp:RadioButtonList ID="rdoButtonFileType" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Normal File" Value="normal" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Encrypted File" Value="ecrypted"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalBold">Check If Remittance</td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxRemittance" OnClick="alert('You checked on Remittance!');" /></td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td class="NormalBold">Transaction Mode:</td>
                                    <td class="NormalBold">
                                        <asp:RadioButtonList ID="rdoButtonBatch" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Item wise" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Batch wise" Value="1" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlCBSAccountHit" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td class="NormalBold">CBS Account Hit:</td>
                                        <td class="NormalBold">
                                            <asp:RadioButtonList ID="rdoButtonCBSAccountWiseHit" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Account Wise Consulated TXN" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Individual TXN" Value="0"></asp:ListItem>
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
                                    <td class="NormalBold">Please Select your Excel File to Upload<br />
                                        <asp:FileUpload CssClass="inputlt" ID="fulExcelFile" runat="server" ClientIDMode="Static" />
                                        <asp:RequiredFieldValidator ID="rfvFulExcelFile" runat="server" ErrorMessage="Browse a file"
                                            ControlToValidate="fulExcelFile" CssClass="NormalRed"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnUploadExcel" runat="server" CssClass="inputlt" Text="Upload File" Width="80" OnClick="btnUploadExcel_Click" OnClientClick="return confirm('Are you sure you want to import this file?')" />
                                        <asp:Label ID="lblErrMsg" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" CssClass="inputlt" CausesValidation="false" Text="Save" Width="80" OnClientClick="return confirm('Are you sure you want to save this data?')" OnClick="btnSave_Click" />
                                    </td>
                                    <td width="80px"></td>
                                    <td>
                                        <asp:Button ID="btnCancel" runat="server" CssClass="inputlt" CausesValidation="false" Text="Cancel" Width="80" OnClientClick="return confirm('Are you sure you want to delete this Batch?')" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div>
                                <table>
                                    <%-- Batch Number has been commented out due to multiple currencies upload BACH II 05-03-2018 --%>
                                    <%--<tr>
                                        <td class="NormalBold">
                                            <asp:Label ID="lblMsgBatchNumber" runat="server"></asp:Label>
                                        </td>
                                    </tr>--%>
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

                                        <asp:TemplateColumn HeaderText="Currency" SortExpression="Currency">
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "Currency")%>
                                            </ItemTemplate>
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
                                                <asp:TextBox ID="txtIdNumber" runat="server" MaxLength="22" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" Text='<%#DataBinder.Eval(Container.DataItem,"IdNumber") %>'></asp:TextBox>
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

                                        <asp:TemplateColumn HeaderText="CTX_InvoiceDate" SortExpression="CTX_InvoiceDate">
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
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblBulkUploadError" runat="server" Text="Error List"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="lblUploadErrMsg" ReadOnly="true" runat="server" TextMode="MultiLine" Width="900px" Height="150px"></asp:TextBox>
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
