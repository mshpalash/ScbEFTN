<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RegenerateTransactionByTraceNumber.aspx.cs"
    Inherits="EFTN.RegenerateTransactionByTraceNumber" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inward Transaction</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script language="javascript" type="text/javascript">
    javascript:window.history.forward(1);
    
    function makeUppercase(myControl, evt)
    {
        document.getElementById(myControl).value = document.getElementById(myControl).value.toUpperCase();
    }
    
    function onlyNumbers(evt) {
        var e = event || evt;
        var charCode = e.which || e.keyCode;
        if (charCode >= 48 && charCode <= 57)
            return false;
        return true;
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
        
    function onlySearchParam(evt) {
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
    
    function DateOfDeathChange(){
    
        if(document.getElementById('ddlReturnChangeCode').value == 'R15'){
        
//        var testvar = confirm('Are you sure you want to save?');
//        alert(testvar)
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
            <div class="Head" align="center">Outward Transaction
            </div>
            <div id="divSearchStatus" runat="server" align="left" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px;
                height: 100px; margin-left: 15px">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblSearchXML" runat="server" Text="Enter XML Name : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchXML" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearchXML" runat="server" Text="Import XML" 
                                onclick="btnSearchXML_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblSearchBeg" runat="server" Text="TraceNumber Begin"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchBeginTraceNumber" runat="server" OnKeyPress="return onlyNumbers(this.name);"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:Label ID="lblSearchEnd" runat="server" Text="End"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchEndTraceNumber" runat="server" OnKeyPress="return onlyNumbers(this.name);"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" />
                        </td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnGenerateText" runat="server" Text="Generate Text" 
                                onclick="btnGenerateText_Click" />
                        </td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" onclick="btnDelete_Click"
                              OnClientClick="return confirm('Are you sure you want to delete data?')" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="11">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="11">
                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="AvailDiv" runat="server" style="position: relative; overflow: scroll; width: 950px;
                height: 380px;">
                <table>
                    <tr>
                        <td>
                            <asp:DataGrid ID="dtgEFTAtlasData" AlternatingItemStyle-BackColor="lightyellow" AutoGenerateColumns="false"
                                BorderWidth="0px" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="#CAD2FD"
                                ItemStyle-CssClass="Normal" runat="server"
                                HeaderStyle-ForeColor="#FFFFFF" 
                                PageSize="500">
                                <Columns>
                                    <asp:BoundColumn DataField = "TransactionCode" HeaderText="TransactionCode" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "DFIAccountNo" HeaderText="DFIAccountNo" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "BankRoutingNo" HeaderText="BankRoutingNo" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "Amount" HeaderText="Amount" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "AccountNo" HeaderText="AccountNo" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "PaymentInfo" HeaderText="PaymentInfo" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "IdNumber" HeaderText="IdNumber" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "ReceiverName" HeaderText="ReceiverName" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "Currency" HeaderText="Currency" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                </Columns>
                                <FooterStyle CssClass="GrayBackWhiteFont" />
                                <PagerStyle Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="LightYellow" />
                                <ItemStyle BackColor="#CAD2FD" CssClass="NormalSmall" />
                                <HeaderStyle CssClass="GrayBackWhiteFont" />
                            </asp:DataGrid>
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
                            <a href="ChangeMakerPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ChangePasswordOn.gif',1)">
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
