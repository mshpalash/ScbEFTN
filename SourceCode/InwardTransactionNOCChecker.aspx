<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InwardTransactionNOCChecker.aspx.cs"
    Inherits="EFTN.InwardTransactionNOCChecker" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script language="javascript" type="text/javascript">
    javascript:window.history.forward(1);
    
    function makeUppercase(myControl, evt)
    {
        document.getElementById(myControl).value = document.getElementById(myControl).value.toUpperCase();
    }
    
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
<body class="wrap" id="content">

    

    <form id="form1" runat="server">    
    <div class="maincontent">
     <uc1:Header ID="Header1" runat="server" />
        <div class="Head" align="center">
            Outward NOC Checker page</div>
            <div>
                <table style="padding-top:15px">
                    <tr height="20px">
                        <td width="30px">
                        </td>
                        <td>
                            <a href="EFTChecker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/EFTCheckerOn.gif',1)"><img src="images/EFTChecker.gif" name="Image4" width="149" height="25" border="0" id="Image4" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerEBBS.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/EFTCheckerEBBSOn.gif',1)"><img src="images/EFTCheckerEBBS.gif" name="Image1" width="149" height="25" border="0" id="Image1" /></a>
                    
                                                </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerAuthorizer.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/EFTCheckerAuthorizerOn.gif',1)"><img src="images/EFTCheckerAuthorizer.gif" name="Image2" width="149" height="25" border="0" id="Image2" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                        <a href="EFTCheckerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ReportOn.gif',1)"><img src="images/Report.gif" name="Image3" width="149" height="25" border="0" id="Image3" /></a>
                                                      
                        </td>                    
                    </tr>
                </table>
            </div>
            <br />               
            <div align="left" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px;
                height: 40px; margin-left: 15px">
                <table>
                    <tr>
                        <td width="10">
                        </td>
                        <td>
                            <asp:DropDownList ID="ddListTransactionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddListTransactionType_SelectedIndexChanged">
                                <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                <asp:ListItem Value="Debit">Debit</asp:ListItem>
                            </asp:DropDownList>
                        </td>                         
                        <td>
                            <asp:CheckBox ID="cbxAll" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAll_CheckedChanged" />
                        </td>
                        <td width="50px">                        
                        </td>
                        <td>
                            <asp:Label ID="lblTotalItem" runat="server" CssClass="NormalBold">
                            </asp:Label>
                        </td>
                        <td width="50px">
                        </td>
                        <td>
                            <asp:Label ID="lblTotalAmount" runat="server" CssClass="NormalBold">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        <div style="overflow:scroll">
            <table>
                <tr>
                    <td colspan="3">
                        <asp:DataGrid ID="dtgEFTCheckerNOC" AlternatingItemStyle-BackColor="lightyellow"
                            AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                            FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                            Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                            DataKeyField="NOCID" Width="980px" AllowPaging="True" AllowSorting="True" 
                            HeaderStyle-ForeColor="#FFFFFF" 
                            OnPageIndexChanged="dtgEFTCheckerNOC_PageIndexChanged" 
                            PageSize="500" OnSortCommand="dtgEFTCheckerNOC_SortCommand">
                            <Columns>
                                    <asp:TemplateColumn HeaderText="SL.">
                                        <ItemTemplate>
                                            <%#(dtgEFTCheckerNOC.PageSize * dtgEFTCheckerNOC.CurrentPageIndex) + Container.ItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBEFTNList" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Settlement Date" SortExpression = "SettlementJDate">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "SettlementJDate")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Effective Entry Date" SortExpression = "EffectiveEntryDate">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "EffectiveEntryDate")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="BatchNumber" SortExpression = "BatchNumber">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "BatchNumber")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NOC Reason" SortExpression = "RejectReason">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "RejectReason")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Corrected Data" SortExpression = "CorrectedData">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "CorrectedData")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="BankName" SortExpression = "BankName">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Branch Name" SortExpression = "BranchName">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="A/C No. From BEFTN" SortExpression = "DFIAccountNo">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="A/C No. From CBS" SortExpression = "ACCOUNT">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "ACCOUNT")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Amount" SortExpression = "Amount">
                                    <ItemTemplate>
                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ReceiverName" SortExpression = "ReceiverName">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ReceiverName as of CBS" SortExpression = "TITLE">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "TITLE")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="STATUS" SortExpression = "STATUS">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "STATUS")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="RISKS" SortExpression = "RISKS">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "RISKS")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Trace Number" SortExpression = "TraceNumber">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "TraceNumber")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn> 
                                  <asp:TemplateColumn HeaderText="Reject Reason" SortExpression = "RejectedReason">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "RejectedReason")%>
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
                </div>
                <div>
                <table>
                <tr>
                    <td width="20%">
                        <table>
                            <tr>
                                <td width="50px">
                                    <asp:LinkButton ID="btnAccept" runat="server" Text="Accept" OnClick="btnAccept_Click"
                                        CssClass="CommandButton" OnClientClick="return confirm('Are you sure you want to accept?')"></asp:LinkButton>
                                </td>
                                <td width="50px"></td>
                                <td>
                                    <asp:LinkButton ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click"
                                        CssClass="CommandButton" OnClientClick="return confirm('Are you sure you want to reject?')"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
                <tr>
                    <td width="40%">
                        <asp:Label ID="lblRejectInstruction" runat="server" CssClass="NormalBold" Text="For a rejection item(s) please give a reason"></asp:Label></td>

                </tr>
                <tr>
                    <td width="40%">
                        <asp:TextBox ID="txtRejectedReason" TextMode="MultiLine" runat="server" Width="300" MaxLength="50" OnKeyUp="return makeUppercase(this.name);"></asp:TextBox></td>

                </tr>
                <tr>
                    <td width="40%" style="color: Red; font-weight: bold;">
                        <asp:Label ID="lblNoReturnReason" runat="server" Text="Please Enter a return reason"
                            Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="clear:both"></div>       
            <div style="padding-top:20px; padding-left:220px">
                <table>
                    <tr height="20px">
                        <td width="50px">
                        </td>
                        
                        <td>
                            <a href="ChangeCheckerPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image5','','images/ChangePasswordOn.gif',1)"><img src="images/ChangePassword.gif" name="Image5" width="149" height="25" border="0" id="Image5" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>                      
                        <td>
                            <a href="LogOut.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image6','','images/SignOutOn.gif',1)"><img src="images/SignOut.gif" name="Image6" width="149" height="25" border="0" id="Image6" /></a>
                                                       
                        </td>                    
                    </tr>
                </table>
            </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
       
    </form>
</body>
</html>
