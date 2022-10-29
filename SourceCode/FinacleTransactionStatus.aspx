<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FinacleTransactionStatus.aspx.cs"
    Inherits="EFTN.FinacleTransactionStatus" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta id="metaref" runat="server" http-equiv="refresh" content="15" />
    <title>Finacle Manager</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    
    <script type="text/javascript">
javascript:window.history.forward(1);

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
                Finacle Manager</div> 
            <div>
                <table style="padding-top: 15px;" width="900px">
                    <tr height="20px" align="center">
                        <td>
                            <a class="CommandButton" href="FinacleSynchronizer.aspx">Finacle Manager</a>
                        </td>
                    </tr>
                </table>
            </div>
                  <div align="center" id="sentDiv" runat="server" style="position: relative; overflow: auto;
                    width: 900px; margin-top: 15px; height: 300px;">
                  <asp:DataGrid ID="dtgISOMessageStatus" AlternatingItemStyle-BackColor="lightyellow" AutoGenerateColumns="false"
                        BorderWidth="0px" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                        GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="#CAD2FD"
                        ItemStyle-CssClass="NormalSmall" runat="server" DataKeyField="MessageID">
                        <Columns>
                            <asp:BoundColumn DataField = "MessageID" HeaderText="MessageID" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "MessageType" HeaderText="MessageType" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "MTI" HeaderText="MTI" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "BitMap" HeaderText="BitMap" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "ActNumber" HeaderText="ActNumber" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "ProcCode" HeaderText="ProcCode" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "TransAmount" HeaderText="TransAmount" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "SystemTraceAuditNo" HeaderText="SystemTraceAuditNo" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "TransDateTime" HeaderText="TransDateTime" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "CaptureDate" HeaderText="CaptureDate" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "FunctionCode" HeaderText="FunctionCode" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "AcquiringInstuteCode" HeaderText="AcquiringInstuteCode" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "RetrievalRefNo" HeaderText="RetrievalRefNo" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "ApprovalCode" HeaderText="ApprovalCode" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "ActionCode" HeaderText="ActionCode" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "DeviceID" HeaderText="DeviceID" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "CardAcceptorIdnt" HeaderText="CardAcceptorIdnt" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "CardAcceptorName" HeaderText="CardAcceptorName" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "TransAmountFees" HeaderText="TransAmountFees" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "AdditionalData" HeaderText="AdditionalData" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "TransCurrency" HeaderText="TransCurrency" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "DebitActNumber" HeaderText="DebitActNumber" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "CreditActNumber" HeaderText="CreditActNumber" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "DeliveryChannelID" HeaderText="DeliveryChannelID" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "TerminalType" HeaderText="TerminalType" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                            <asp:BoundColumn DataField = "ReservedCode" HeaderText="ReservedCode" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                        </Columns>
                        <FooterStyle CssClass="GrayBackWhiteFont" />
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="LightYellow" />
                        <ItemStyle BackColor="#CAD2FD" CssClass="NormalSmall" />
                        <HeaderStyle CssClass="GrayBackWhiteFont" />
                    </asp:DataGrid>
                  </div>
             <div style="clear:both"></div>
       
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
