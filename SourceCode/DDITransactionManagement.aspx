<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DDITransactionManagement.aspx.cs" Inherits="EFTN.DDITransactionManagement" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta id="metaref" runat="server" http-equiv="refresh" content="120" />
    <title>DDI Transaction Management</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]--><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script type="text/javascript">
<!--
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
-->

</script>
</head>
<body class="wrap" id="content" onload="MM_preloadImages('images/UserManagementOn.gif','images/BranchManOn.gif','images/ChangePassOn.gif','images/ExitOn.gif')">
    <form id="form1" runat="server">
            <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Maker Page</div>
            <div>
                <table style="padding-top: 15px;">
                    <tr height="20px">
                        <td width="200px">
                        </td>
                        <td>
                            <a href="EFTMaker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/EFTMakerHomeOn.gif',1)">
                                <img src="images/EFTMakerHome.gif" name="Image1" width="149" height="25" border="0"
                                    id="Image1" /></a>
                        </td>
                        <td width="250px">
                        </td>
                        <td>
                            <a href="EFTMakerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/ReportOn.gif',1)">
                                <img src="images/Report.gif" name="Image2" width="149" height="25" border="0" id="Image2" /></a>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="divDDI" class="boxmodule" style="float: left; padding-top: 15px; padding-bottom: 10px;
                width: 800px; padding-left: 15px; height: 295px; margin-left: 15px" runat="server">
                <table>
                    <tr>
                        <td valign="top" class="NormalBold" bgcolor="aliceblue" style="border: solid 1px #666666;
                            width: 200px" align="center">
                            <asp:LinkButton ID="linkBtnImportTransaction" runat="server" Text="Import DDI Transaction"
                                OnClientClick="return confirm('Are you sure you want to import?')"
                                OnClick="linkBtnImportTransaction_Click"></asp:LinkButton>
                            <br />
                            <asp:DataList ID="dtlImportTransaction" runat="Server" ItemStyle-CssClass="Normal"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" BorderWidth="0" CellSpacing="1"
                                CellPadding="2">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"FileName")%>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:Label ID="lblMsgTransactionReceived" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                            <asp:LinkButton ID="linkBtnGenerateReturn" runat="server" CssClass="CommandButton"
                                Text="Generate Flat Files" onclick="linkBtnGenerateReturn_Click"></asp:LinkButton>
                            <asp:Label ID="lblDDIReturnCounter" runat="server"></asp:Label>
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                            <ul>
                                <li><a class="CommandButton" href="DDIBatchDetails.aspx">DDI Batch Details</a></li>
                            </ul>
                        </td>
                        <td>
                            <ul>
                                <li><a class="CommandButton" href="DDIAccountManagement.aspx">DDI Account Management</a></li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ul>
                                <li><a class="CommandButton" href="DDIReport.aspx">Report</a></li>
                            </ul>
                        </td>
                        <td>
                            <ul>
                                <li><a class="CommandButton" href="DDIReportByEntryDate.aspx">Transaction Status By Entry Date</a></li>
                            </ul>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblErrMsg" runat="server"></asp:Label>
                <asp:Panel ID="pnlReturn" runat="server" ScrollBars="Auto">

                            <table>

                                <tr>
                                    <td>
                                        <asp:DataGrid ID="dtgInwardReturnMaker" AlternatingItemStyle-BackColor="lightyellow"
                                            AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                            FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                            Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                            DataKeyField="ReturnID" 
                                            HeaderStyle-ForeColor="#FFFFFF" 
                                            AllowPaging="True" AllowSorting="True" 
                                            OnPageIndexChanged="dtgInwardReturnMaker_PageIndexChanged" 
                                            PageSize="500">
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="SL.">
                                                    <ItemTemplate>
                                                        <%#(dtgInwardReturnMaker.PageSize * dtgInwardReturnMaker.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="BankName" SortExpression = "BankName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="OrgSettlementJDate" SortExpression = "OrgSettlementJDate">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "OrgSettlementJDate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn HeaderText="SettlementJDate" SortExpression = "SettlementJDate">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "SettlementJDate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn HeaderText="A/C No. From BEFTN" SortExpression = "DFIAccountNo">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Amount" SortExpression = "Amount">
                                                    <ItemTemplate>
                                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Receiver Name" SortExpression = "ReceiverName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="OrgTraceNumber" SortExpression = "OrgTraceNumber">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "OrgTraceNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Trace Number" SortExpression = "TraceNumber">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "TraceNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <FooterStyle CssClass="GrayBackWhiteFont" />
                                            <PagerStyle Mode="NumericPages" />
                                            <AlternatingItemStyle BackColor="LightYellow" />
                                            <ItemStyle BackColor="#CAD2FD" CssClass="Normal" />
                                            <HeaderStyle CssClass="GrayBackWhiteFont" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>

                            </table>
                </asp:Panel>
            </div>
            <div style="clear: both">
            </div>
            <br />
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
