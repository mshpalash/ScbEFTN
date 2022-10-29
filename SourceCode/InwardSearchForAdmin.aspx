<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InwardSearchForAdmin.aspx.cs" Inherits="EFTN.InwardSearchForAdmin" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Inward Search</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script type="text/javascript" language="javascript">

    function onlyNumbers(evt) {
        var e = event || evt;
        var charCode = e.which || e.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }

    function onlyNumbersWithPrecision(myControl, evt) {
        var e = event || evt;
        var charCode = e.which || e.keyCode;
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

    function leapYear(myControl, myControlMonth, myControlDay) {
        if (document.getElementById(myControl).value.length > 3) {
            if (document.getElementById(myControlMonth).value == 2) {
                var leapYearDate = (document.getElementById(myControl).value % 4);
                if (leapYearDate != 0) {
                    if (document.getElementById(myControlDay).value > 28)
                        document.getElementById(myControlDay).value = 28;
                }
                else
                    if (document.getElementById(myControlDay).value > 29)
                    document.getElementById(myControlDay).value = 29;
            }
        }
    }

    function onlyMonth(myControl, dayControl) {

        var currentMonth = document.getElementById(myControl).value;

        if (currentMonth > 12)
            document.getElementById(myControl).value = 12;

        if (currentMonth == 4 || currentMonth == 6 || currentMonth == 9 || currentMonth == 11) {
            var currentDate = document.getElementById(dayControl).value;
            if (currentDate > 30)
                document.getElementById(dayControl).value = 30;
        }
        return true;
    }

    function onlyDay(myControl) {
        if (document.getElementById(myControl).value > 31)
            document.getElementById(myControl).value = 31;
        return true;
    }

    function setNonZeroMonth(controlMonth) {
        var currentMonth = document.getElementById(controlMonth).value;
        if (currentMonth == 0)
            document.getElementById(controlMonth).value = 1;
    }

    function setNonZeroDate(controlDate) {
        var currentDate = document.getElementById(controlDate).value;
        if (currentDate == 0)
            document.getElementById(controlDate).value = 1;
    }

    function trim(stringToTrim) {
        return stringToTrim.replace(/^\s+|\s+$/g, "");
    }
    function ltrim(stringToTrim) {
        return stringToTrim.replace(/^\s+/, "");
    }
    function rtrim(stringToTrim) {
        return stringToTrim.replace(/\s+$/, "");
    }

function DateOfDeathChange(){
    
        if(document.getElementById('ddlReturnChangeCode').value == 'R15'){
        
                var dateOfDeath = document.getElementById('txtDateOfDeath').value;
 
                if(dateOfDeath.length<6){
                    alert('Insert Date Of Death');
                    document.getElementById('txtDateOfDeath').focus();
                    return false;
                }
                else{
                    var month = dateOfDeath.substring(2,4);
                    var day = dateOfDeath.substring(4,6);
                  
                    if( (month>12) || (month<1) || (day>31) || (day<1)){
                        alert('Incorrect date format');
                        document.getElementById('txtDateOfDeath').focus();
                        return false;
                    }
                    return confirm('Are you sure you want to save?');
                }

        }else{
            return confirm('Are you sure you want to save?');
        }
    }
    
function validateDataField() {
}    
    </script>

    <script type="text/javascript">

function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}
function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}

    </script>

</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Inward Search<br />
            </div>

            <div class="boxmodule" style="padding-top: 10px; width: 700px; margin-top: 10px;
                height: 460px; margin-bottom: 20px;">
                <table width="700px">
                    <tr>
                        <td style="width: 101px">
                        </td>
                        <td valign="top" class="NormalBold">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblReceivingBankRoutingNo" runat="server" Text="Receiving Bank Routing No."></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReceivingBankRoutingNo" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblBranchName" runat="server" Text="Receiving Branch Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBranchName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSendingBankRoutNo" runat="server" Text="Sending Bank RoutNo"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSendingBankRoutNo" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblBankName" runat="server" Text="Sending Bank Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBankName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDFIAccountNo" runat="server" Text="DFI Account No."></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDFIAccountNo" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblReceiverName" runat="server" Text="Receiver Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReceiverName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAmount" runat="server" Text="Amount Range"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" MaxLength="12" oncopy="return false" oncut="return false"
                                            onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false">
                                        </asp:TextBox>
                                        <asp:Label ID="Label8" runat="server" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtMaxAmount" runat="server" MaxLength="12" oncopy="return false" oncut="return false"
                                            onkeypress="return onlyNumbersWithPrecision(this.name);" onpaste="return false">
                                        </asp:TextBox>                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblIdNumber" runat="server" Text="ID Number"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIdNumber" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTraceNumber" runat="server" Text="Trace Number"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTraceNumber" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblBatchNumber" runat="server" Text="Batch Number"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBatchNumber" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPaymentInfo" runat="server" Text="Payment Info"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPaymentInfo" runat="server"></asp:TextBox>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCreditDebit" runat="server" Text="CreditDebit"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddListTransactionType" runat="server">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="1">Credit</asp:ListItem>
                                            <asp:ListItem Value="2">Debit</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>                                                               
                                <tr>
                                    <td>
                                        <asp:Label ID="lblBeginDate" runat="server" Text="Settlement Begin Date"></asp:Label>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td class="NormalBold">
                                                    <asp:Label ID="Label3" runat="server" Text="Day"></asp:Label>
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
                                                    <asp:Label ID="Label4" runat="server" Text="Month"></asp:Label>
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
                                                    <asp:Label ID="Label5" runat="server" Text="Year"></asp:Label>
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
                                        <asp:Label ID="Label1" runat="server" Text="Settlement End Date"></asp:Label>
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
                                </tr>
                            </table>
                        </td>
                        <td style="width: 101px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 101px">
                        </td>
                        <td align="center" class="NormalBold">
                            <asp:RadioButtonList ID="rdoBtnSearchType" RepeatDirection="Horizontal" runat="server" Visible="false">
                                <asp:ListItem Value="100" Selected="True">Inward Transaction</asp:ListItem>
                            </asp:RadioButtonList>

                        </td>
                        <td style="width: 101px">
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="center" class="NormalBold">
                            <asp:CheckBox ID="chkBoxArchive" runat="server" Text="From Archive" />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                OnClientClick="return validateDataField()" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </div>
                <div style="padding-top: 10px; width: 550px; margin-top: 10px;
                    margin-bottom: 20px; padding-left: 100px;">
                    <asp:Label ID="lblSearchMsg" runat="server" ForeColor="Red"></asp:Label>
                </div>             
                <div class="boxmodule" style="padding-top: 10px; width: 830px; margin-top: 10px;
                    height: 100px; margin-bottom: 20px;">
                    <table>
                        <tr>
                            <td class="NormalBold">
                                Bank Name:</td>
                            <td>
                                <asp:DropDownList ID="ddListReceivingBank" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddListReceivingBank_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnRecBank" Text="Add To Search" runat="server" OnClick="btnRecBank_Click"  />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="NormalBold">
                                Branch Name:</td>
                            <td>
                                <asp:DropDownList ID="ddListBranch" runat="server" AutoPostBack="true" DataTextField="BranchName"
                                    DataValueField="RoutingNo" OnSelectedIndexChanged="ddListBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnBranch" Text="Add To Search" runat="server" OnClick="btnBranch_Click" />
                            </td>                            
                            <td></td>
                        </tr>
                        <tr>
                            <td class="NormalBold">
                                Routing No.:</td>
                            <td>
                                <asp:TextBox ID="txtRoutingNo" runat="server" MaxLength="9" Width="80px" CssClass="inputlt"
                                    onkeypress="return onlyNumbers();"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnRoutNo" Text="Add To Receiving Bank RoutNo." Width="220px" runat="server" OnClick="btnRoutNo_Click"  />
                            </td>                            
                            <td>
                                <asp:Button ID="btnSendBankRoutNo" Text="Add To Sending Bank RoutNo." Width="200px" runat="server" OnClick="btnSendBankRoutNo_Click"  />
                            </td>
                        </tr>                
                    </table>
                </div>            
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px;
                height: 60px; margin-left: 15px">
                <table>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbxSelectAll" runat="server" CssClass="NormalBold" Text="Select All"
                                AutoPostBack="true" OnCheckedChanged="cbxSelectAll_CheckedChanged"/>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                            <asp:LinkButton ID="PrintVoucherBtn" runat="server" CssClass="CommandButton" Text="Print Selected Customer Advice"
                                OnClick="PrintVoucherBtn_Click"></asp:LinkButton>
                        </td>
                        <td width="20px">
                        </td>                        
                        <td>
                            <asp:LinkButton ID="lnkBtnPrintAllSearchResult" runat="server" CssClass="CommandButton" Text="Print Customer Advice For All Search Result" OnClick="lnkBtnPrintAllSearchResult_Click"></asp:LinkButton>
                        </td>
                        <td width="20px">
                        </td>                        
                        <td>
                            <asp:Button ID="btnExpotToCSV" runat="server" Text="CSV File" OnClick="btnExpotToCSV_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td width="20px">
                        </td>
                        <td align="left">
                            <asp:LinkButton ID="lnkBtnPrintDetailReport" runat="server" CssClass="CommandButton" Text="Print All Search Result Details" OnClick="lnkBtnPrintDetailReport_Click"
                                ></asp:LinkButton>
                        </td>
                        <td width="20px">
                        </td>                        
                        <td>
                            <asp:LinkButton ID="lnkBtnPrintDetailSettlementReportFormat" runat="server" CssClass="CommandButton" OnClick="lnkBtnPrintDetailSettlementReportFormat_Click"
                                Text="Print Details Settlement Report Format"></asp:LinkButton></td>
                        <td width="20px">
                        </td>                        
                        <td>
                        </td>
                    </tr>
                    
                </table>
            </div>
            <br />

            <asp:Label ID="lblReturnMsg" runat="server" Font-Bold="True" ForeColor="Blue" Visible="False"
                Text="It has not been returned"></asp:Label>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:DataGrid ID="dtgReturnRecord" runat="Server" Width="600" BorderWidth="0px" GridLines="None"
                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader" ItemStyle-BackColor="#dee9fc"
                                Caption="Return Record" ItemStyle-CssClass="Normal" AlternatingItemStyle-BackColor="#ffffff"
                                DataKeyField="ReturnID" Font-Bold="True">
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Dishonor">
                                        <ItemTemplate>
                                            <a href="InwardSearchForChecker.aspx?inwardSearchReturnID=<%#DataBinder.Eval(Container.DataItem, "ReturnID")%>">
                                                Dishonor</a>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                   
                                    <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ReturnCode" HeaderText="ReturnCode">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                  
                                    
                                    
                                     <asp:BoundColumn DataField="SettlementJDate" HeaderText="SettlementJDate">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    
                                    
                                </Columns>
                                <FooterStyle CssClass="GrayBackWhiteFont" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#DEE9FC" CssClass="Normal" />
                                <HeaderStyle CssClass="GrayBackWhiteFontFixedHeader" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Label ID="lblNOCMsg" runat="server" Font-Bold="True" ForeColor="Blue" Visible="False"
                Text="It has not been notified for change"></asp:Label>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:DataGrid ID="dtgNOCRecord" runat="Server" Width="600" BorderWidth="0px" GridLines="None"
                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader" ItemStyle-BackColor="#dee9fc"
                                Caption="NOC Record" ItemStyle-CssClass="Normal" AlternatingItemStyle-BackColor="#ffffff"
                                DataKeyField="NOCID" Font-Bold="True">
                                <Columns>
                                    <asp:TemplateColumn HeaderText="RNOC">
                                        <ItemTemplate>
                                            <a href="InwardSearchForChecker.aspx?inwardSearchNOCID=<%#DataBinder.Eval(Container.DataItem, "NOCID")%>">
                                                RNOC</a>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                   
                                    <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CorrectedData" HeaderText="CorrectedData">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ChangeCode" HeaderText="ChangeCode">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    
                                     <asp:BoundColumn DataField="SettlementJDate" HeaderText="SettlementJDate">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                </Columns>
                                <FooterStyle CssClass="GrayBackWhiteFont" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#DEE9FC" CssClass="Normal" />
                                <HeaderStyle CssClass="GrayBackWhiteFontFixedHeader" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Label ID="lblDishonorMsg" runat="server" Font-Bold="True" ForeColor="Blue" Visible="False"
                Text="The return has not been dishonored"></asp:Label>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:DataGrid ID="dtgDishonorRecord" runat="Server" Width="600" BorderWidth="0px"
                                GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1"
                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader"
                                ItemStyle-BackColor="#dee9fc" Caption="Dishonor Record" ItemStyle-CssClass="Normal"
                                AlternatingItemStyle-BackColor="#ffffff" DataKeyField="DishonoredID" Font-Bold="True">
                                <Columns>
                                    <asp:BoundColumn DataField="DishonorReason" HeaderText="DishonorReason" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="AddendaInfo" HeaderText="AddendaInfo" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="DishonorIdentity" HeaderText="DishonorIdentity" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                </Columns>
                                <FooterStyle CssClass="GrayBackWhiteFont" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#DEE9FC" CssClass="Normal" />
                                <HeaderStyle CssClass="GrayBackWhiteFontFixedHeader" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Label ID="lblRNOC" runat="server" Font-Bold="True" ForeColor="Blue" Visible="False"
                Text="The NOC has not been refused"></asp:Label>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:DataGrid ID="dtgRNOC" runat="Server" Width="600" BorderWidth="0px" GridLines="None"
                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader" ItemStyle-BackColor="#dee9fc"
                                Caption="RNOC Record" ItemStyle-CssClass="Normal" AlternatingItemStyle-BackColor="#ffffff"
                                DataKeyField="RNOCID" Font-Bold="True">
                                <Columns>
                                    <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="RefusedCORCode" HeaderText="RefusedCORCode" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="CorrectedData" HeaderText="CorrectedData" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField="RNOCIdentity" HeaderText="RNOCIdentity" ItemStyle-Wrap="False"
                                        HeaderStyle-Wrap="False" />
                                </Columns>
                                <FooterStyle CssClass="GrayBackWhiteFont" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#DEE9FC" CssClass="Normal" />
                                <HeaderStyle CssClass="GrayBackWhiteFontFixedHeader" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Blue" Text="Results Not Found"
                Visible="False"></asp:Label><br />
            <div style="overflow: scroll">
                <table>
                    <tr>
                        <td width="100">
                        </td>
                        <td class="NormalBold" align="center" colspan="2">
                            <asp:DataGrid ID="dtgInwardSearch" runat="Server" Width="600" BorderWidth="0px" GridLines="None"
                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                HeaderStyle-CssClass="GrayBackWhiteFont" ItemStyle-BackColor="#dee9fc" ItemStyle-CssClass="Normal"
                                AlternatingItemStyle-BackColor="#ffffff" DataKeyField="EDRID" AllowPaging="True" OnPageIndexChanged="dtgInwardSearch_PageIndexChanged" PageSize="100">
                                <Columns>
                                    <asp:TemplateColumn HeaderText="SL.">
                                        <ItemTemplate>
                                            <%#(dtgInwardSearch.PageSize * dtgInwardSearch.CurrentPageIndex) + Container.ItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                
                                    <asp:TemplateColumn HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkBoxTransReceived" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="BankName" HeaderText="BankName" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BranchName" HeaderText="BranchName" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DFIAccountNo" HeaderText="DFIAccountNo" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Amount" HeaderText="Amount" DataFormatString="{0:N}" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IdNumber" HeaderText="IdNumber" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ReceiverName" HeaderText="ReceiverName" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="EffectiveEntryDate" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="EntryDateTransactionReceived" HeaderText="EntryDate" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SettlementJDate" HeaderText="SettlementDate" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CompanyName" HeaderText="CompanyName" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="EntryDesc" HeaderText="EntryDesc" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PaymentInfo" HeaderText="PaymentInfo" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>                                    
                                    <asp:BoundColumn DataField="SECC" HeaderText="SECC" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ServiceClassCode" HeaderText="ServiceClassCode" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNumber" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CompanyId" HeaderText="CompanyId" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TransactionCode" HeaderText="TransactionCode" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SendingBankRoutNo" HeaderText="SendingBankRoutNo" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NameOfCreatedBy" HeaderText="Maker" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NameOfApprovedBy" HeaderText="Checker" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundColumn>                                    
                                </Columns>
                                <FooterStyle CssClass="GrayBackWhiteFont" />
                                <PagerStyle Mode="NumericPages" HorizontalAlign="Left" Position="TopAndBottom" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#DEE9FC" CssClass="Normal" />
                                <HeaderStyle CssClass="GrayBackWhiteFont" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlReturnChangeCode" runat="server" DataTextField="RejectReason"
                                DataValueField="RejectReasonCode">
                            </asp:DropDownList>
                        </td>
                    </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDateOfDeath" runat="server"  Text="Date Of Death"></asp:Label>
                        <asp:TextBox ID="txtDateOfDeath" runat="server" onkeypress="return onlyNumbers();" MaxLength="6"></asp:TextBox>
                        <asp:Label ID="lblDateOfDeathInstruction" runat="server"  Text="YYMMDD"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDateOfDeathSelectedValue" runat="server" Text="Insert Date of Death if you select 'Beneficiary or Account Holder (Other Than a Representative Payee) Deceased'"></asp:Label>
                    </td>
                </tr>                    
                <tr>
                    <td width="40%">
                        <asp:LinkButton ID="btnSave" runat="server" Text="Click to Return" CssClass="NormalBold"
                            OnClick="btnSave_Click" OnClientClick="return DateOfDeathChange();"></asp:LinkButton>
                    </td>
                </tr>                    
                </table>
            </div>      
            <div style="clear: both">
            </div>
            <div style="margin-top: 25px">
                <table style="padding-top: 15px">
                    <tr height="20">
                        <td width="200px">
                        </td>
                        <td>
                            <a href="ChangeCheckerPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ChangePasswordOn.gif',1)">
                                <img src="images/ChangePassword.gif" name="Image3" width="149" height="25" border="0"
                                    id="Image3" /></a>
                        </td>
                        <td width="250px">
                        </td>
                        <td>
                            <a href="LogOut.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/SignOutOn.gif',1)">
                                <img src="images/SignOut.gif" name="Image4" width="149" height="25" border="0" id="Image4" /></a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
