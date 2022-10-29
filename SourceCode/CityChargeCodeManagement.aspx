<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CityChargeCodeManagement.aspx.cs" Inherits="FloraSoft.CityChargeCodeManagement" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="modules/Header.ascx" TagName="AdminHeader" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora EFTN System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script type="text/javascript" language="javascript">

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

    </script>
    
</head>
<body class="wrap" id="content" >
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:AdminHeader ID="AdminHeader" runat="server" />
        <div class="Head" align="center">
            Charge Code Management</div>
        <div style="width:900px" align="center">
            <table>
                <tr>
                    <td align="center">
                        <a href="EFTChargeManagementCity.aspx" class="CommandButton"> Charge Management</a>
                    </td>
                </tr>
            </table>
        </div>            
        <div style="width: 900px" align="center">
                    <asp:Panel ID="pnlChargeCode" runat="server" ScrollBars="Horizontal" Width="900px">
                        <asp:DataGrid ID="dataGridChargeCodeInfo" HeaderStyle-CssClass="GrayBackWhiteFont" FooterStyle-CssClass="GrayBackWhiteFont"
                            ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#FFFFFF"
                            runat="server" CellSpacing="1" CellPadding="2" AutoGenerateColumns="false" DataKeyField="CityChargeCode"
                            BorderWidth="0px" ShowFooter="true" Height="0px" OnItemCommand="dataGridChargeCodeInfo_ItemCommand">
                            <Columns>
                                <asp:EditCommandColumn CausesValidation="False" EditText="Edit" ItemStyle-Width="30px" UpdateText="Update"
                                    CancelText="Cancel">
                                    <FooterStyle CssClass="red"></FooterStyle>
                                    <ItemStyle CssClass="CommandButton" />
                                </asp:EditCommandColumn>
                                <asp:TemplateColumn HeaderText="City Charge Code">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "CityChargeCode")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Bangladesh Bank Charge">
                                    <ItemTemplate>
                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "BBCharge"))%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                         <asp:TextBox ID="BBCharge" Width="60"  runat="server" 
                                                    oncopy="return false" oncut="return false"
                                                    onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false"
                                                    Text='<%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "BBCharge")) %>'></asp:TextBox>
                                    </EditItemTemplate>                                    
                                    <FooterTemplate>
                                        <asp:TextBox ID="addBBCharge" runat="Server" oncopy="return false" oncut="return false"
                                                    onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBBCharge" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addBBCharge" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Bank Charge">
                                    <ItemTemplate>
                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "BankCharge"))%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                         <asp:TextBox ID="BankCharge" Width="60"  runat="server" 
                                                    oncopy="return false" oncut="return false"
                                                    onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false"
                                                    Text='<%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "BankCharge")) %>'></asp:TextBox>
                                    </EditItemTemplate>                                    
                                    <FooterTemplate>
                                        <asp:TextBox ID="addBankCharge" runat="Server" oncopy="return false" oncut="return false"
                                                    onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBankCharge" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addBankCharge" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Bangladesh Bank Charge VAT">
                                    <ItemTemplate>
                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "BBChargeVAT"))%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                         <asp:TextBox ID="BBChargeVAT" Width="60"  runat="server" 
                                                    oncopy="return false" oncut="return false"
                                                    onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false"
                                                    Text='<%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "BBChargeVAT")) %>'></asp:TextBox>
                                    </EditItemTemplate>                                    
                                    <FooterTemplate>
                                        <asp:TextBox ID="addBBChargeVAT" runat="Server" oncopy="return false" oncut="return false"
                                                    onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBBChargeVAT" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addBBChargeVAT" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                                                                              
                                <asp:TemplateColumn HeaderText="Bank Charge VAT">
                                    <ItemTemplate>
                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "BankChargeVAT"))%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                         <asp:TextBox ID="BankChargeVAT" Width="60"  runat="server" 
                                                    oncopy="return false" oncut="return false"
                                                    onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false"
                                                    Text='<%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "BankChargeVAT"))%>'></asp:TextBox>
                                    </EditItemTemplate>                                    
                                    <FooterTemplate>
                                        <asp:TextBox ID="addBankChargeVAT" runat="Server" oncopy="return false" oncut="return false"
                                                    onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBankChargeVAT" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addBankChargeVAT" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                                                                              
                                <asp:TemplateColumn HeaderText="Charge Wave">
                                    <ItemTemplate>
                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "ChargeWave"))%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                         <asp:TextBox ID="ChargeWave" Width="60"  runat="server" 
                                                    oncopy="return false" oncut="return false"
                                                    onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false"
                                                    Text='<%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "ChargeWave"))%>'></asp:TextBox>
                                    </EditItemTemplate>                                    
                                    <FooterTemplate>
                                        <asp:TextBox ID="addChargeWave" runat="Server" oncopy="return false" oncut="return false"
                                                    onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorChargeWave" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addChargeWave" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                                                                              
                                <asp:TemplateColumn HeaderText="VAT Wave">
                                    <ItemTemplate>
                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "VATWave"))%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                         <asp:TextBox ID="VATWave" Width="60"  runat="server" 
                                                    oncopy="return false" oncut="return false"
                                                    onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false"
                                                    Text='<%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "VATWave"))%>'></asp:TextBox>
                                    </EditItemTemplate>                                    
                                    <FooterTemplate>
                                        <asp:TextBox ID="addVATWave" runat="Server" oncopy="return false" oncut="return false"
                                                    onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorVATWave" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addVATWave" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                                                                              
                                <asp:TemplateColumn>                                
                                    <FooterTemplate>
                                        <asp:LinkButton CommandName="Insert" Text="Add" ID="btnAdd" ForeColor="white" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <FooterStyle CssClass="GrayBackWhiteFont" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall" />
                            <HeaderStyle CssClass="GrayBackWhiteFont" />
                        </asp:DataGrid>
                    </asp:Panel>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
