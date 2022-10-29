<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SchedulerReportTransactionListForChecker.aspx.cs"
    Inherits="EFTN.SchedulerReportTransactionListForChecker" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
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
    <uc1:Header ID="Header" runat="server" />
        <div align="center">
            <table>
                <tr height="10px">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="center"><a href="SchedulerReportChecker.aspx" class="CommandButton">Scheduler Report</a></td>
                </tr>
            </table>
        </div>
    
        <div class="Head" align="center">Standing Order Transaction Status</div>

            <div align="left">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblTRSent" runat="server" CssClass="NormalBold" Text="Batch Details"></asp:Label>
                            <div style="overflow: scroll; height: 250px; width: 800px;">
                                <table>
                                    <tr>
                                        <td class="NormalBold" align="center">
                                            <asp:DataGrid ID="dtgStandingOrderBatch" runat="Server" BorderWidth="0px" GridLines="None"
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="StandingOrderBatchID">
                                                <Columns>
                                                    <asp:BoundColumn DataField = "CompanyId" HeaderText="CompanyId" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "CompanyName" HeaderText="Company Name" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "EntryDesc" HeaderText="Entry Desc" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "BatchType" HeaderText="Batch Type" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "BeginDate" HeaderText="Begin Date" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" DataFormatString="{0:d}" />
                                                    <asp:BoundColumn DataField = "EndDate" HeaderText="End Date" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" DataFormatString="{0:d}" />
                                                    <asp:BoundColumn DataField = "ActiveStatus" HeaderText="ActiveStatus" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "DepartmentName" HeaderText="Department Name" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "TransactionFrequency" HeaderText="TransactionFrequency" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "BatchEntryDate" HeaderText="BatchEntryDate" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "Amount" HeaderText="Amount" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "ReceiverName" HeaderText="ReceiverName" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "PaymentInfo" HeaderText="PaymentInfo" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "DFIAccountNo" HeaderText="DFIAccountNo" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "AccountNo" HeaderText="AccountNo" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "ServiceClassCode" HeaderText="ServiceClassCode" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "SECC" HeaderText="SECC" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "TypeOfPayment" HeaderText="TypeOfPayment" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "DataEntryType" HeaderText="Data Entry Type" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "BatchStatus" HeaderText="Batch Status" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "CreatedBy" HeaderText="CreatedBy" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td class="NormalBold" align="center">
                                            <asp:DataGrid ID="dtgSTDOTransactionSent" runat="Server" BorderWidth="0px" GridLines="None"
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="StandingOrderBatchID">
                                                <Columns>
                                                    <asp:BoundColumn DataField = "EffectiveEntryDate" HeaderText="EffectiveEntryDate" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "BatchStatus" HeaderText="BatchStatus" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                </Columns>
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
