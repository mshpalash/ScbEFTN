<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvalidTransactionModifier.aspx.cs" Inherits="EFTN.InvalidTransactionModifier" %>


<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Invalid DFIAccount Number</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">
    
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
    </script>

</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Invalid Transaction</div>
                <br />
                <div style="overflow: scroll">
                    <asp:DataGrid ID="dtgInvalidDFIAccNo" runat="Server" Width="600" BorderWidth="0px"
                        GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                        FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                        ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff"
                        DataKeyField="EDRID" OnItemCommand="dtgInvalidDFIAccNo_ItemCommand" >
                        <Columns>
                            <asp:EditCommandColumn CausesValidation="False" EditText="Edit" ItemStyle-Width="30px" UpdateText="Update"
                                    CancelText="Cancel">
                            </asp:EditCommandColumn>
                            <asp:TemplateColumn HeaderText="PaymentInfo">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "PaymentInfo")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtPaymentInfo" MaxLength="80" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PaymentInfo") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPaymentInfo" CssClass="NormalRed"
                                        runat="server" ControlToValidate="txtPaymentInfo" ErrorMessage="*" Display="dynamic">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="SenderAccNumber">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "AccountNo")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtAccountNo" MaxLength="17" runat="server" OnKeyPress="return onlyNumbers();" Text='<%#DataBinder.Eval(Container.DataItem,"AccountNo") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAccountNo" CssClass="NormalRed"
                                        runat="server" ControlToValidate="txtAccountNo" ErrorMessage="*" Display="dynamic">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="ReceivingBankRoutingNo">   
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
                            
                            <asp:TemplateColumn HeaderText="DFIAccountNo">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtDFIAccountNo" MaxLength="17" runat="server" OnKeyPress="return onlyNumbers();" Text='<%#DataBinder.Eval(Container.DataItem,"DFIAccountNo") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDFIAccountNo" CssClass="NormalRed"
                                        runat="server" ControlToValidate="txtDFIAccountNo" ErrorMessage="*" Display="dynamic">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:BoundColumn DataField="ReceiverAccountType" HeaderText="ReceiverAccountType" ItemStyle-Wrap="False"
                                HeaderStyle-Wrap="False" ReadOnly="true" />
                            <asp:BoundColumn DataField="Amount" HeaderText="Amount" ItemStyle-Wrap="False"
                                HeaderStyle-Wrap="False" ReadOnly="true" />
                                
                            <asp:TemplateColumn HeaderText="IdNumber">   
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "IdNumber")%>
                                </ItemTemplate> 
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtIdNumber" MaxLength="15" runat="server" OnKeyPress="return onlyNumbers();" Text='<%#DataBinder.Eval(Container.DataItem,"IdNumber") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorIdNumber" CssClass="NormalRed"
                                        runat="server" ControlToValidate="txtIdNumber" ErrorMessage="*" Display="dynamic">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                                            
                            <asp:TemplateColumn HeaderText="ReceiverName">   
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

                        </Columns>
                    </asp:DataGrid>
                </div>
    
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
