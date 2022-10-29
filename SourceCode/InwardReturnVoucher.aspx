<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InwardReturnVoucher.aspx.cs"
    Inherits="EFTN.InwardReturnVoucher" MaintainScrollPositionOnPostback="true" %>

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
                Received Return Approved by Checker</div>
            <div>
                <table style="padding-top:15px">
                    <tr height="20px">
                        <td width="30px">
                        </td>
                        <td>
                            <a href="EFTChecker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image10','','images/EFTCheckerOn.gif',1)"><img src="images/EFTChecker.gif" name="Image10" width="149" height="25" border="0" id="Image10" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerEBBS.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image11','','images/EFTCheckerEBBSOn.gif',1)"><img src="images/EFTCheckerEBBS.gif" name="Image11" width="149" height="25" border="0" id="Image11" /></a>
                    
                                                </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerAuthorizer.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image12','','images/EFTCheckerAuthorizerOn.gif',1)"><img src="images/EFTCheckerAuthorizer.gif" name="Image12" width="149" height="25" border="0" id="Image12" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                        <a href="EFTCheckerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image13','','images/ReportOn.gif',1)"><img src="images/Report.gif" name="Image13" width="149" height="25" border="0" id="Image13" /></a>
                                                      
                        </td>                    
                    </tr>
                </table>
            </div>
            <div align="left" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px;
                height: 70px; margin-left: 15px">
                <table>
                    <tr>
                        <td width="10"></td>
                        <td width="100">
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
                        
                        <td width="50px">
                        </td>

                        <td style="width: 200px">
                            <asp:LinkButton ID="PrintVoucherBtn" runat="server" CssClass="CommandButton" Text="Print Vouchers" OnClick="PrintVoucherBtn_Click"></asp:LinkButton>                                    
                        </td>                        
                    </tr>
                </table>
                 <table>
                    <tr>
                        <td width="10"></td>
                        <td class="NormalBold">
                            <asp:Label ID="lblCurrency" runat="server" Text="Currency"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="CurrencyDdList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CurrencyDdList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="10px"></td>
                        <td class="NormalBold">
                            <asp:Label ID="lblSession" runat="server" Text="Session"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="SessionDdList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SessionDdList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div style="overflow: scroll">
                <table>
                    <tr>
                        <td colspan="2">
                            <asp:DataGrid ID="dtgApprovedReturnChecker" AlternatingItemStyle-BackColor="lightyellow"
                                AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                DataKeyField="ReturnID" Width="980px"
                                HeaderStyle-ForeColor="#FFFFFF" 
                                AllowPaging="true" PageSize="500" 
                                OnPageIndexChanged="dtgApprovedReturnChecker_PageIndexChanged" 
                                AllowSorting="True" OnSortCommand="dtgApprovedReturnChecker_SortCommand">
                                <Columns>
                                    <asp:TemplateColumn HeaderText="SL.">
                                        <ItemTemplate>
                                            <%#(dtgApprovedReturnChecker.PageSize * dtgApprovedReturnChecker.CurrentPageIndex) + Container.ItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                
                                    <asp:TemplateColumn HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBEFTNList" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="BankName" SortExpression = "BankName">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="BranchName" SortExpression = "BranchName">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
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
                                    <asp:TemplateColumn HeaderText="ACC.NO" SortExpression = "DFIAccountNo">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Currency">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Currency")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Session">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "SessionID")%>
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
               
                                   
                                    <asp:TemplateColumn HeaderText="OrgTraceNumber" SortExpression = "OrgTraceNumber">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "OrgTraceNumber")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="TraceNumber" SortExpression = "TraceNumber">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "TraceNumber")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Return Reason" SortExpression = "RejectReason">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "RejectReason")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="AddendaInfo" SortExpression = "AddendaInfo">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "AddendaInfo")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="DateOfDeath" SortExpression = "DateOfDeath">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "DateOfDeath")%>
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
                                <AlternatingItemStyle BackColor="LightYellow" />
                                <ItemStyle BackColor="#CAD2FD" CssClass="Normal" />
                                <HeaderStyle CssClass="GrayBackWhiteFont" />
                            </asp:DataGrid>

                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px;
                height: 80px; margin-left: 15px">
                <table>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnInwardReturnApproved" runat="server" Text="Approve" CssClass="CommandButton"
                                OnClick="btnInwardReturnApproved_Click" OnClientClick="return confirm('Are you sure you want to approve?')" />
                        </td>
                        <td width="50px">
                        </td>
                        <td>
                            <asp:LinkButton ID="btnInwardRetrunReject" runat="server" Text="Reject" CssClass="CommandButton"
                                OnClick="btnInwardRetrunReject_Click" OnClientClick="return confirm('Are you sure you want to reject?')" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td class="NormalBold">
                            RejectReason:
                            <asp:TextBox ID="txtReceivedReturnApprovedRejectReason" TextMode="MultiLine" runat="server" OnKeyUp="return makeUppercase(this.name);" />
                            <asp:Label ID="lblMsgApproved" runat="server" Text="Please put some reason" CssClass="NormalRed"
                                Visible="false" />
                        </td>
                    </tr>                
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
