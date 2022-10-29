<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterCodeManagement.aspx.cs" Inherits="FloraSoft.MasterCodeManagement" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Master Code Management</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]--> 
    
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
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header" runat="server" />
        <div class="Head" align="center">Master Code Management</div>
        <div align="center">
            <table>
                <tr height="10px">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="center"><a href="EFTChargeManagement.aspx" class="CommandButton">Charge Management</a></td>
                </tr>
            </table>
        </div>        
        <div align="center" class="boxmodule" style="width:940px;margin-left:20px; padding-top:15px; padding-left:15px; min-height:400px; overflow: scroll; margin-bottom:15px; margin-top:20px">

            <asp:datagrid Id="MyDataGrid2"
                HeaderStyle-CssClass="GrayBackWhiteFont" 
                FooterStyle-CssClass="GrayBackWhiteFont" 
                ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc"   
                AlternatingItemStyle-BackColor="#FFFFFF" 
                runat="server" CellSpacing="1"  CellPadding="5" 
                autogeneratecolumns="false"  DataKeyField="ChargeMasterID" 
                gridlines="None" borderwidth="0px" ShowFooter="true"  Height="0px"
                OnItemCommand="MyDataGrid2_ItemCommand">
                <Columns>
                    <asp:TemplateColumn HeaderText="MasterID" HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red" HeaderStyle-Width="60px" FooterStyle-Width="120px" ItemStyle-Width="120px" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "ChargeMasterID")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Master Account No." HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red" HeaderStyle-Width="60px" FooterStyle-Width="120px" ItemStyle-Width="120px" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "MASTER")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="txtACCOUNT" runat="server" Width="120px" 
                             Text='<%#DataBinder.Eval(Container.DataItem,"MASTER") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addACCOUNT" Width="120" MaxLength="7"  Runat="Server" />
                                <asp:RequiredFieldValidator 
                                id="RequiredFieldValidatoraddACCOUNT" 
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addACCOUNT"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Bangladesh Bank Charge" HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red" HeaderStyle-Width="60px" FooterStyle-Width="120px" ItemStyle-Width="120px" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "BBCharge")%>
                        </ItemTemplate>
                         <FooterTemplate>
                            <asp:DropDownList ID="addBBCharge" runat="server">
                                <asp:ListItem Text="Waived" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Charged" Value="1"></asp:ListItem>
                            </asp:DropDownList>                         
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Bank Charge" HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red" HeaderStyle-Width="60px" FooterStyle-Width="120px" ItemStyle-Width="120px" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "BankCharge")%>
                        </ItemTemplate>
                         <FooterTemplate>
                            <asp:DropDownList ID="addBankCharge" runat="server">
                                <asp:ListItem Text="Waived" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Charged" Value="1"></asp:ListItem>
                            </asp:DropDownList>                         
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Bangladesh Bank Charge with VAT" HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red" HeaderStyle-Width="60px" FooterStyle-Width="120px" ItemStyle-Width="120px" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "BBChargeVAT")%>
                        </ItemTemplate>
                         <FooterTemplate>
                            <asp:DropDownList ID="addBBChargeVAT" runat="server">
                                <asp:ListItem Text="Waived" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Charged" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Bank Charge with VAT" HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red" HeaderStyle-Width="60px" FooterStyle-Width="120px" ItemStyle-Width="120px" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "BankChargeVAT")%>
                        </ItemTemplate>
                         <FooterTemplate>
                            <asp:DropDownList ID="addBankChargeVAT" runat="server">
                                <asp:ListItem Text="Waived" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Charged" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                     <FooterTemplate>
                            <asp:linkButton CommandName="Insert" Text="Add" ID="btnAdd" ForeColor="white"  Runat="server" />
                    </FooterTemplate>
                    </asp:TemplateColumn>                    
                </Columns>
            </asp:datagrid>
        </div>
        <div>
            <asp:Label runat="server" ID="lblTestCookie"></asp:Label>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
