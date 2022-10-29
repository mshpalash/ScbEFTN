<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StandingOrderLogChecker.aspx.cs"
    Inherits="EFTN.StandingOrderLogChecker" MaintainScrollPositionOnPostback="true" %>

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
    <uc1:Header ID="Header1" runat="server" />
    
        <div class="Head" align="Center">Standing Order Log</div>
        <div class="boxmodule" style="padding-top:20px; width:750px; height:auto; margin-top:0px; margin-left:20px">
            <table>
                        <tr>
                            <td class="NormalBold">
                                <asp:Label ID="lblDay" runat="server" Text="Batch Entry Date Begin"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td class="NormalBold">
                                            <asp:Label ID="lblDate" runat="server" Text="Day"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistDay" runat="server">
                                                <asp:ListItem>01</asp:ListItem>
                                                <asp:ListItem>02</asp:ListItem>
                                                <asp:ListItem>03</asp:ListItem>
                                                <asp:ListItem>04</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>06</asp:ListItem>
                                                <asp:ListItem>07</asp:ListItem>
                                                <asp:ListItem>08</asp:ListItem>
                                                <asp:ListItem>09</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                <asp:ListItem>24</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>26</asp:ListItem>
                                                <asp:ListItem>27</asp:ListItem>
                                                <asp:ListItem>28</asp:ListItem>
                                                <asp:ListItem>29</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>31</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="NormalBold">
                                            <asp:Label ID="Label1" runat="server" Text="Month"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistMonth" runat="server">
                                                <asp:ListItem Value="1">Jan</asp:ListItem>
                                                <asp:ListItem Value="2">Feb</asp:ListItem>
                                                <asp:ListItem Value="3">Mar</asp:ListItem>
                                                <asp:ListItem Value="4">Apr</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">Jun</asp:ListItem>
                                                <asp:ListItem Value="7">Jul</asp:ListItem>
                                                <asp:ListItem Value="8">Aug</asp:ListItem>
                                                <asp:ListItem Value="9">Sep</asp:ListItem>
                                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                                <asp:ListItem Value="12">Dec</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="NormalBold">
                                            <asp:Label ID="Label46" runat="server" Text="Year"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistYear" runat="server">
                                                <asp:ListItem>2011</asp:ListItem>
                                                <asp:ListItem>2012</asp:ListItem>
                                                <asp:ListItem>2013</asp:ListItem>
                                                <asp:ListItem>2014</asp:ListItem>
                                                <asp:ListItem>2015</asp:ListItem>
                                                <asp:ListItem>2016</asp:ListItem>
                                                <asp:ListItem>2017</asp:ListItem>
                                                <asp:ListItem>2018</asp:ListItem>
                                                <asp:ListItem>2019</asp:ListItem>
                                                <asp:ListItem>2020</asp:ListItem>
                                                <asp:ListItem>2021</asp:ListItem>
                                                <asp:ListItem>2023</asp:ListItem>
                                                <asp:ListItem>2024</asp:ListItem>
                                                <asp:ListItem>2025</asp:ListItem>
                                                <asp:ListItem>2026</asp:ListItem>
                                                <asp:ListItem>2027</asp:ListItem>
                                                <asp:ListItem>2028</asp:ListItem>
                                                <asp:ListItem>2029</asp:ListItem>
                                                <asp:ListItem>2030</asp:ListItem>
                                                <asp:ListItem>2031</asp:ListItem>
                                                <asp:ListItem>2032</asp:ListItem>
                                                <asp:ListItem>2033</asp:ListItem>
                                                <asp:ListItem>2034</asp:ListItem>
                                                <asp:ListItem>2035</asp:ListItem>
                                                <asp:ListItem>2036</asp:ListItem>
                                                <asp:ListItem>2037</asp:ListItem>
                                                <asp:ListItem>2038</asp:ListItem>
                                                <asp:ListItem>2039</asp:ListItem>
                                                <asp:ListItem>2040</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEndDate" runat="server" Text="Batch Entry Date End"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td class="NormalBold">
                                            <asp:Label ID="Label2" runat="server" Text="Day"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistDayEnd" runat="server">
                                                <asp:ListItem>01</asp:ListItem>
                                                <asp:ListItem>02</asp:ListItem>
                                                <asp:ListItem>03</asp:ListItem>
                                                <asp:ListItem>04</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>06</asp:ListItem>
                                                <asp:ListItem>07</asp:ListItem>
                                                <asp:ListItem>08</asp:ListItem>
                                                <asp:ListItem>09</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                <asp:ListItem>24</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>26</asp:ListItem>
                                                <asp:ListItem>27</asp:ListItem>
                                                <asp:ListItem>28</asp:ListItem>
                                                <asp:ListItem>29</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>31</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="NormalBold">
                                            <asp:Label ID="Label6" runat="server" Text="Month"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistMonthEnd" runat="server">
                                                <asp:ListItem Value="1">Jan</asp:ListItem>
                                                <asp:ListItem Value="2">Feb</asp:ListItem>
                                                <asp:ListItem Value="3">Mar</asp:ListItem>
                                                <asp:ListItem Value="4">Apr</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">Jun</asp:ListItem>
                                                <asp:ListItem Value="7">Jul</asp:ListItem>
                                                <asp:ListItem Value="8">Aug</asp:ListItem>
                                                <asp:ListItem Value="9">Sep</asp:ListItem>
                                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                                <asp:ListItem Value="12">Dec</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="NormalBold">
                                            <asp:Label ID="Label7" runat="server" Text="Year"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistYearEnd" runat="server">
                                                <asp:ListItem>2011</asp:ListItem>
                                                <asp:ListItem>2012</asp:ListItem>
                                                <asp:ListItem>2013</asp:ListItem>
                                                <asp:ListItem>2014</asp:ListItem>
                                                <asp:ListItem>2015</asp:ListItem>
                                                <asp:ListItem>2016</asp:ListItem>
                                                <asp:ListItem>2017</asp:ListItem>
                                                <asp:ListItem>2018</asp:ListItem>
                                                <asp:ListItem>2019</asp:ListItem>
                                                <asp:ListItem>2020</asp:ListItem>
                                                <asp:ListItem>2021</asp:ListItem>
                                                <asp:ListItem>2023</asp:ListItem>
                                                <asp:ListItem>2024</asp:ListItem>
                                                <asp:ListItem>2025</asp:ListItem>
                                                <asp:ListItem>2026</asp:ListItem>
                                                <asp:ListItem>2027</asp:ListItem>
                                                <asp:ListItem>2028</asp:ListItem>
                                                <asp:ListItem>2029</asp:ListItem>
                                                <asp:ListItem>2030</asp:ListItem>
                                                <asp:ListItem>2031</asp:ListItem>
                                                <asp:ListItem>2032</asp:ListItem>
                                                <asp:ListItem>2033</asp:ListItem>
                                                <asp:ListItem>2034</asp:ListItem>
                                                <asp:ListItem>2035</asp:ListItem>
                                                <asp:ListItem>2036</asp:ListItem>
                                                <asp:ListItem>2037</asp:ListItem>
                                                <asp:ListItem>2038</asp:ListItem>
                                                <asp:ListItem>2039</asp:ListItem>
                                                <asp:ListItem>2040</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td colspan="4">
                            </td>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                    onclick="btnSubmit_Click" />
                            </td>
                        </tr>                        
                    </table>

        <br />
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblTRSent" runat="server" CssClass="NormalBold" Text="Ready to send batches"></asp:Label>
                            <div style="overflow: scroll; height: 250px; width: 800px;">
                                <table>
                                    <tr>
                                        <td class="NormalBold" align="center">
                                            <asp:DataGrid ID="dtgStandingOrderLog" runat="Server" BorderWidth="0px" GridLines="None"
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="OID"
                                                AllowPaging="true" PageSize="500" OnPageIndexChanged="dtgStandingOrderLog_PageIndexChanged">
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="SL.">
                                                        <ItemTemplate>
                                                            <%#(dtgStandingOrderLog.PageSize * dtgStandingOrderLog.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>                                                  
                                                    <asp:BoundColumn DataField = "OID" HeaderText="OID" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "LogEntryDate" HeaderText="LogEntryDate" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "StandingOrderBatchID" HeaderText="StandingOrderBatchID" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "UserName" HeaderText="UserName" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                    <asp:BoundColumn DataField = "LogDescription" HeaderText="LogDescription" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                                </Columns>

<FooterStyle CssClass="GrayBackWhiteFont"></FooterStyle>

<HeaderStyle CssClass="GrayBackWhiteFont"></HeaderStyle>

<ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall"></ItemStyle>
                                                <PagerStyle Mode="NumericPages" />
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
