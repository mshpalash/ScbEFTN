<%@ Page Language="C#" AutoEventWireup="true" Codebehind="BatchSentRejectedByChecker.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="EFTN.BatchSentRejectedByChecker" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


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

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta id="metaref" runat="server" http-equiv="refresh" content="60" />
    <title>Batch Rejected by Checker</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header" runat="server" />
            <div class="Head" align="center">
                Checker Page</div>
            <div>
                <table style="padding-top: 15px;">
                    <tr height="20px">
                        <td width="200px">
                        </td>
                        <td>
                            <a href="EFTMaker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/EFTMakerHomeOn.gif',1)"><img src="images/EFTMakerHome.gif" name="Image1" width="149" height="25" border="0" id="Image1" /></a>
                            
                        </td>
                        <td width="250px">
                        </td>
                        <td>
                        <a href="EFTMakerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/ReportOn.gif',1)"><img src="images/Report.gif" name="Image2" width="149" height="25" border="0" id="Image2" /></a>
                            
                        </td>                        
                    </tr>
                </table>
            </div>          
            <br />
            <div align="left" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px;
                height: 40px; margin-left: 15px">
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddListTransactionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddListTransactionType_SelectedIndexChanged">
                                <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                <asp:ListItem Value="Debit">Debit</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <br />            
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsgExportTransaction" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbxAllTransactionSent" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAllTransactionSent_CheckedChanged" />
                        </td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblTRSent" runat="server" CssClass="NormalBold" Text="Transaction Sent"></asp:Label>
                            <div style="overflow: scroll; height: 250px;">
                                <table>
                                    <tr>
                                        <td class="NormalBold" align="center">
                                            <asp:DataGrid ID="dtgBatchTransactionSent" runat="Server" BorderWidth="0px" GridLines="None"
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID" AllowPaging="True" OnPageIndexChanged="dtgBatchTransactionSent_PageIndexChanged" PageSize="50">
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="SL.">
                                                        <ItemTemplate>
                                                            <%#(dtgBatchTransactionSent.PageSize * dtgBatchTransactionSent.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbxSentBatchTS" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="">
                                                        <ItemTemplate>
                                                            <a href="BatchWiseRejectedTransactionDetails.aspx?TransactionEDRID=<%#DataBinder.Eval(Container.DataItem, "TransactionID")%>">
                                                                Batch Details</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>                                                    
                                                    <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo" >
                                                        <HeaderStyle Wrap="True" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="CompanyName" HeaderText="Company Name" >
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SECC" HeaderText="SECC" >
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}" >
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="True" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="EntryDesc" HeaderText="Description" >
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Currency" HeaderText="Currency" >
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total" >
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}" >
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="RejectReason" HeaderText="Batch Reject Reason" >
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <FooterStyle CssClass="GrayBackWhiteFont" />
                                                <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Position="TopAndBottom" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall" />
                                                <HeaderStyle CssClass="GrayBackWhiteFont" />
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>

                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete Batch" CssClass="CommandButton"
                                OnClientClick="return confirm('Are you sure you want to Delete the batches?')" OnClick="btnDelete_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
