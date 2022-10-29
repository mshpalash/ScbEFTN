<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StandingOrderUpload.aspx.cs"
    Inherits="EFTN.StandingOrderUpload" MaintainScrollPositionOnPostback="true" %>

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
<body class="wrap" id="content" >

    <form id="form2" method="post" runat="server">    
        <div class="maincontent">
        <uc1:Header ID="Header1" runat="server" />
        <div align="center">
            <table>
                <tr height="10px">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="center"><a href="StandingOrderManagement.aspx" class="CommandButton">Standing Order Management</a></td>
                </tr>
            </table>
        </div>
            <div class="Head" align="Center">Upload Standing Order data</div>

            <div>
            <table>
                <tr>
                    <td width="100"></td>
                    <td>
                        <table>
                            <tr>
                                <td  class="NormalBold">Your Company TIN:</td>
                                <td >
                                    <asp:TextBox ID="txtCompanyTIN" runat="server" MaxLength="16" Width="135px"  OnKeyPress="return onlyEFTSpecialCharacter(this.name);" />
                                    <asp:Label ID="lblcompanyTIN" runat="server" CssClass="NormalRed" Text="*"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">Type of Payment:</td>
                                <td>
                                    <asp:DropDownList ID="ddlPaymentType" runat="server">
                                     <asp:ListItem Selected ="True"  Text="Corp to Ind - PPD" Value="6"></asp:ListItem>
                                     <asp:ListItem Selected ="False" Text = "Corporate To Corporate (Cash Concentration)  - CCD"    Value = "1" ></asp:ListItem> 
                                     <asp:ListItem  Selected ="False" Text = "Individual To Individual (Trade Payment) - CIE"    Value = "2"></asp:ListItem> 
                                      
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">Your Comapany Name:</td>
                                <td >
                                    <asp:TextBox ID="txtCompanyName" runat="server" Width="135px" MaxLength="16" OnKeyUp="return makeUppercase(this.name);" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" />
                                    <asp:Label ID="lblCompanyName" runat="server" CssClass="NormalRed" Text="*"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td class="NormalBold">Reason for Payment:</td>
                                <td>
                                    <asp:TextBox ID="txtReasonForPayment" runat="server"  MaxLength="10" Width="135px" OnKeyUp="return makeUppercase(this.name);" OnKeyPress="return onlyEFTSpecialCharacter(this.name);" />
                                    <asp:Label ID="lblReason" runat="server" CssClass="NormalRed" Text="*"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td class="NormalBold">Transaction Type:</td>
                                <td class="NormalBold">
                                    <asp:RadioButtonList ID="rdoBtnTransactionType" RepeatDirection="Horizontal" runat="server">
                                        <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                        <asp:ListItem Value="Debit">Debit</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
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
                        </table>                    
                        <table>
                            <tr>
                                <td class="NormalBold">
                                    <asp:RadioButtonList ID="rdoButtonBatch" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Item wise" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Batch wise" Value="1" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td class="NormalBold">Transaction Frequency : </td>
                                <td>
                                    <asp:DropDownList ID="ddListTransactionFrequency" runat="server">
                                        <asp:ListItem Value="1" Text="Dailly"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="Weekly"></asp:ListItem>
                                        <asp:ListItem Value="15" Text="Fortnightly"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="Monthly"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="Quarterly"></asp:ListItem>
                                        <asp:ListItem Value="180" Text="Half Yearly"></asp:ListItem>
                                        <asp:ListItem Value="365" Text="Yearly"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date : "></asp:Label>
                                </td>
                                <td>
                                    <asp:Calendar ID="CalendarFreqStart" runat="server" BackColor="#FFFFCC" 
                                        BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                                        Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="163px" 
                                        ShowGridLines="True" Width="189px">
                                        <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                        <SelectorStyle BackColor="#FFCC66" />
                                        <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                        <OtherMonthDayStyle ForeColor="#CC9966" />
                                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                        <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                        <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                                            ForeColor="#FFFFCC" />
                                    </asp:Calendar>
                                </td>
                                <td>
                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date : "></asp:Label>
                                </td>
                                <td>
                                    <asp:Calendar ID="CalendarFreqEnd" runat="server" BackColor="#FFFFCC" 
                                        BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                                        Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="163px" 
                                        ShowGridLines="True" Width="189px">
                                        <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                        <SelectorStyle BackColor="#FFCC66" />
                                        <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                        <OtherMonthDayStyle ForeColor="#CC9966" />
                                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                        <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                        <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                                            ForeColor="#FFFFCC" />
                                    </asp:Calendar>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td class="NormalBold" >
                                    Please Select your Excel File to Upload<br />
                                    <asp:FileUpload CssClass="inputlt" ID="fulExcelFile" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvFulExcelFile" runat="server" ErrorMessage="Browse a file"
                                        ControlToValidate="fulExcelFile"   CssClass="NormalRed"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   <asp:Button ID="btnUploadExcel" runat="server" CssClass="inputlt" Text="Upload File" Width="80" OnClick="btnUploadExcel_Click" OnClientClick="return confirm('Are you sure you want to import this file?')" />
                                   <asp:Label ID="lblErrMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                                <td width="80px">
                                </td>
                                <td>
                                   <asp:Button ID="btnCancel" runat="server" CssClass="inputlt" CausesValidation="false" Text="Cancel" Width="80" OnClientClick="return confirm('Are you sure you want to delete this Batch?')" OnClick="btnCancel_Click"/>                                
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div>
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
                            </table>
                        </div>
                        <div style="overflow: scroll; height: 450px; width: 900px">
                        <asp:DataGrid ID="dtgXcelUpload" runat="Server" Width="600px" BorderWidth="0px"
                            GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                            FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont" HeaderStyle-ForeColor="#FFFFFF"
                            ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff"
                            DataKeyField="StandingOrderEDRID" AllowSorting="True" OnSortCommand="dtgXcelUpload_SortCommand" OnPageIndexChanged="dtgXcelUpload_PageIndexChanged" AllowPaging="True" PageSize="500">
                            <Columns>
                                <asp:TemplateColumn HeaderText="PaymentInfo" SortExpression = "PaymentInfo">   
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "PaymentInfo")%>
                                    </ItemTemplate> 
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SenderAccNumber" SortExpression = "AccountNo" >   
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "AccountNo")%>
                                    </ItemTemplate> 
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ReceivingBankRoutingNo" SortExpression = "ReceivingBankRoutingNo">   
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "ReceivingBankRoutingNo")%>
                                    </ItemTemplate> 
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DFIAccountNo" SortExpression = "DFIAccountNo" >   
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                    </ItemTemplate> 
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Amount" SortExpression = "Amount">   
                                    <ItemTemplate>
                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                    </ItemTemplate> 
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="IdNumber" SortExpression = "IdNumber">   
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "IdNumber")%>
                                    </ItemTemplate> 
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ReceiverName" SortExpression = "ReceiverName">   
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
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
