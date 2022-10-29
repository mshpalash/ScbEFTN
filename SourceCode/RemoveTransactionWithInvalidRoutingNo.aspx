<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RemoveTransactionWithInvalidRoutingNo.aspx.cs" Inherits="EFTN.RemoveTransactionWithInvalidRoutingNo" %>


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
                <label id="lblMsg" runat="server"></label>
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnUpdate" runat="server" Text="Update Transaction For New Routing Number" CssClass="NormalBold"
                                    OnClientClick="return confirm('Are you sure you want to save?')" OnClick="btnUpdate_Click"></asp:LinkButton>
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                                <asp:LinkButton ID="btnUpdateCSVRejection" runat="server" 
                                    Text="Trasfer to CSV Rejection" CssClass="NormalBold"
                                    OnClientClick="return confirm('Are you sure you want to save?')" 
                                    onclick="btnUpdateCSVRejection_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                
                <div style="overflow: scroll">
                    <asp:DataGrid ID="dtgInvalidDFIAccNo" runat="Server" Width="600" BorderWidth="0px"
                        GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                        FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                        ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff"
                        DataKeyField="EDRID" OnItemCommand="dtgInvalidDFIAccNo_ItemCommand" AllowPaging="True" PageSize="500" >
                        <Columns>
                             <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                             <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNumber" ReadOnly="True" >
                                 <HeaderStyle Wrap="False" />
                                 <ItemStyle Wrap="False" />
                             </asp:BoundColumn>
                            <asp:BoundColumn DataField="PaymentInfo" HeaderText="PaymentInfo" ReadOnly="True" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ReceivingBankRoutingNo" HeaderText="ReceivingBankRoutingNo" ReadOnly="True" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>                                                      
                            <asp:BoundColumn DataField="BankName" HeaderText="BankName" ReadOnly="True" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>                                                      
                            <asp:BoundColumn DataField="BranchName" HeaderText="BranchName" ReadOnly="True" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>                                                      
                            <asp:BoundColumn DataField="AccountNo" HeaderText="SenderAccNumber" ReadOnly="True" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>                            
                            <asp:BoundColumn DataField="DFIAccountNo" HeaderText="DFIAccountNo" ReadOnly="True" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ReceiverAccountType" HeaderText="ReceiverAccountType" ReadOnly="True" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Currency" HeaderText="Currency" ReadOnly="True" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Amount" HeaderText="Amount" ReadOnly="True" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IdNumber" HeaderText="IdNumber" ReadOnly="True" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ReceiverName" HeaderText="ReceiverName" ReadOnly="True" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SECC" HeaderText="SECC" ReadOnly="True" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle CssClass="GrayBackWhiteFont" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall" />
                        <HeaderStyle CssClass="GrayBackWhiteFont" />
                        <PagerStyle Mode="NumericPages" />
                    </asp:DataGrid>
                </div>
    
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
