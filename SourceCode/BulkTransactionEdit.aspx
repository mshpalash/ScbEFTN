<%@ Page Language="C#" AutoEventWireup="true" Codebehind="BulkTransactionEdit.aspx.cs"
    Inherits="EFTN.BulkTransactionEdit" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script language="javascript" type="text/javascript">
    javascript:window.history.forward(1);
        
        function onlyNumbersWithPrecision(myControl, evt) {
        var e = event || evt;
        var charCode = e.which || e.keyCode;
        var controlValue = document.getElementById(myControl).value;
        var maxLengthAfterPrecision = 2;
        if (IsPrecisionExists(controlValue)) {            
            for (i = 0; i < controlValue.length; i++) {
                    if (controlValue.charAt(i) == ".") {
                        if(controlValue.length-(i+1) > maxLengthAfterPrecision-1){
                            return false;
                        }
                    }
            }
        }else{
            if (charCode != 46) {
                if(controlValue.length > 9)
                {
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
<body class="wrap" id="content" >

    <form id="form2" method="post" runat="server">    
    <div class="maincontent">
    <uc1:Header ID="Header1" runat="server" />
        <div class="Head" align="Center">Uploaded Corporate data</div>
            <div style="width:900px" align="center">
                <table>
                    <tr>
                        <td align="center">
                            <a href="BulkTransactionList.aspx" class="CommandButton"> List of Batches</a>
                        </td>
                    </tr>
                </table>
            </div>

        <div>
        <table>
            <tr>
                <td width="100"></td>
                <td>
                    <div>
                        <table>
                            <%--  Commented out cause they have decided to upload multiple currencies batch dynamically   --%>
                           <%--  <tr>
                                <td>
                                   <asp:Button ID="btnSave" runat="server" CssClass="inputlt" CausesValidation="false" Text="Save" Width="80" OnClientClick="return confirm('Are you sure you want to save this data?')" OnClick="btnSave_Click" />                                
                                </td>
                                <td width="80px">
                                </td>
                                <td>
                                   <asp:Button ID="btnCancel" runat="server" CssClass="inputlt" CausesValidation="false" Text="Cancel" Width="80" OnClientClick="return confirm('Are you sure you want to delete this Batch?')" OnClick="btnCancel_Click"/>                                
                                </td>
                            </tr>--%>
                        </table>
                        <table>
                            <tr>
                                <td class="NormalBold">
                                    <asp:Label id="lblMsgBatchNumber" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">
                                    <asp:Label id="lblTotalItem" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">
                                    <asp:Label id="lblTotalAmount" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblErrMsg" runat="server"></asp:Label>
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
                            <asp:TemplateColumn HeaderText="PaymentInfo" SortExpression = "PaymentInfo">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "PaymentInfo")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtPaymentInfo" MaxLength="80" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PaymentInfo") %>' OnKeyPress="return onlyAlphaNumeric(this.name);" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPaymentInfo" CssClass="NormalRed"
                                        runat="server" ControlToValidate="txtPaymentInfo" ErrorMessage="*" Display="dynamic">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="SenderAccNumber" SortExpression = "AccountNo" >   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "AccountNo")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtAccountNo" MaxLength="17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AccountNo") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAccountNo" CssClass="NormalRed"
                                        runat="server" ControlToValidate="txtAccountNo" ErrorMessage="*" Display="dynamic">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="ReceivingBankRoutingNo" SortExpression = "ReceivingBankRoutingNo">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ReceivingBankRoutingNo")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtReceivingBankRoutingNo" MaxLength="9" runat="server" OnKeyPress="return onlyNumbers();" Text='<%#DataBinder.Eval(Container.DataItem,"ReceivingBankRoutingNo") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorReceivingBankRoutingNo" CssClass="NormalRed"
                                        runat="server" ControlToValidate="txtReceivingBankRoutingNo" ErrorMessage="*" Display="dynamic">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="DFIAccountNo" SortExpression = "DFIAccountNo" >   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtDFIAccountNo" MaxLength="17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DFIAccountNo") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDFIAccountNo" CssClass="NormalRed"
                                        runat="server" ControlToValidate="txtDFIAccountNo" ErrorMessage="*" Display="dynamic">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                           
                             <asp:TemplateColumn HeaderText="Currency" SortExpression = "Currency" >   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Currency")%>
                                </ItemTemplate>                                 
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Amount" SortExpression = "Amount">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Amount")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtAmount" MaxLength="13" runat="server" OnKeyPress="return onlyNumbersWithPrecision(this.name);" Text='<%#DataBinder.Eval(Container.DataItem,"Amount") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAmount" CssClass="NormalRed"
                                        runat="server" ControlToValidate="txtAmount" ErrorMessage="*" Display="dynamic">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                
                            <asp:TemplateColumn HeaderText="IdNumber" SortExpression = "IdNumber">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "IdNumber")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtIdNumber" MaxLength="22" runat="server" OnKeyPress="return onlyNumbers();" Text='<%#DataBinder.Eval(Container.DataItem,"IdNumber") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorIdNumber" CssClass="NormalRed"
                                        runat="server" ControlToValidate="txtIdNumber" ErrorMessage="*" Display="dynamic">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                                            
                            <asp:TemplateColumn HeaderText="ReceiverName" SortExpression = "ReceiverName">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtReceiverName" MaxLength="22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ReceiverName") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorReceiverName" CssClass="NormalRed"
                                        runat="server" ControlToValidate="txtReceiverName" ErrorMessage="*" Display="dynamic">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="CTX_InvoiceNumber" SortExpression = "CTX_InvoiceNumber">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CTX_InvoiceNumber")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtInvoiceNumber" MaxLength="10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_InvoiceNumber") %>' OnKeyPress="return onlyAlphaNumeric(this.name);" ></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="CTX_InvoiceDate" SortExpression = "CTX_InvoiceDate">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CTX_InvoiceDate")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtInvoiceDate" MaxLength="10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_InvoiceDate") %>' ></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                                       
                            <asp:TemplateColumn HeaderText="CTX_InvoiceGrossAmount" SortExpression = "CTX_InvoiceGrossAmount">   
                                <ItemTemplate>
                                    <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "CTX_InvoiceGrossAmount"))%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtInvoiceGrossAmount" MaxLength="13" runat="server" OnKeyPress="return onlyNumbersWithPrecision(this.name);" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_InvoiceGrossAmount") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                                       
                            <asp:TemplateColumn HeaderText="CTX_InvoiceAmountPaid" SortExpression = "CTX_InvoiceAmountPaid">   
                                <ItemTemplate>
                                    <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "CTX_InvoiceAmountPaid"))%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtInvoiceAmountPaid" MaxLength="13" runat="server" OnKeyPress="return onlyNumbersWithPrecision(this.name);" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_InvoiceAmountPaid") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="CTX_PurchaseOrder" SortExpression = "CTX_PurchaseOrder">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CTX_PurchaseOrder")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtPurchaseOrder" MaxLength="10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_InvoiceNumber") %>' OnKeyPress="return onlyAlphaNumeric(this.name);" ></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                                       
                            <asp:TemplateColumn HeaderText="CTX_AdjustmentAmount" SortExpression = "CTX_AdjustmentAmount">   
                                <ItemTemplate>
                                    <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "CTX_AdjustmentAmount"))%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtAdjustmentAmount" MaxLength="13" runat="server" OnKeyPress="return onlyNumbersWithPrecision(this.name);" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_AdjustmentAmount") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="CTX_AdjustmentCode" SortExpression = "CTX_AdjustmentCode">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CTX_AdjustmentCode")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtAdjustmentCode" MaxLength="2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_AdjustmentCode") %>' OnKeyPress="return onlyNumbers();" ></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="CTX_AdjustmentDescription" SortExpression = "CTX_AdjustmentDescription">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CTX_AdjustmentDescription")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtAdjustmentDescription" MaxLength="40" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CTX_AdjustmentDescription") %>'></asp:TextBox>
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
               </td>
            </tr>
        </table>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
