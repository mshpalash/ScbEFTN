<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iBankingOperatorChecker.aspx.cs"  MaintainScrollPositionOnPostback="true"  Inherits="EFTN.iBankingController" %>


<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            <uc1:Header ID="Header" runat="server" />
            <div class="Head" align="center">
                iBanking Controller Page</div>
            
            <br />
            <div align="left" class="boxmodule" style="padding-top: 10px; width: 890px; margin-top: 10px;
                    margin-left: 15px; margin-bottom: 10px; padding-left:20px; height: 50px;">
                <table>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalBold">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalBold">
                            &nbsp;</td>
                    </tr>
                </table>
            </div>            
            <div id="AvailDiv" runat="server" style="position: relative; overflow: scroll; width: 950px;
                height: 380px;">
                <table>
                    <tr>
                        <td colspan="3">
                            <asp:DataGrid ID="iBankingOperatorGrid" AlternatingItemStyle-BackColor="lightyellow" AutoGenerateColumns="false"
                                BorderWidth="0px" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="#CAD2FD"
                                ItemStyle-CssClass="Normal" runat="server" DataKeyField="IBTID" 
                                AllowPaging="true" OnPageIndexChanged="iBankingOperator_PageIndexChanged" 
                                HeaderStyle-ForeColor="#FFFFFF" 
                                PageSize="500" AllowSorting="true" OnSortCommand="iBankingOperator_SortCommand"  OnItemDataBound="iBankingOperatorGrid_ItemDataBound">
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBEFTNList" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Serial No.">
                                        <ItemTemplate>
                                            <%#(iBankingOperatorGrid.PageSize * iBankingOperatorGrid.CurrentPageIndex) + Container.ItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <%--<asp:TemplateColumn  SortExpression = "SN" HeaderText="Serial No.">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "SN")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>--%>
                                    <asp:TemplateColumn  SortExpression = "RiskBit" HeaderText="RiskBit">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "RiskBit")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn  SortExpression = "DuplicateBit" HeaderText="DuplicateBit">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "DuplicateBit")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn  SortExpression = "Over500KBit" HeaderText="Over500KBit">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Over500KBit")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn  SortExpression = "Dr_AC_Number" HeaderText="Acc. No">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Dr_AC_Number")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                      <asp:TemplateColumn  SortExpression = "Transaction_Date" HeaderText="Entry Date">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Transaction_Date")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn  SortExpression = "Dr_AC_CCY" HeaderText="Currency">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Dr_AC_CCY")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn  SortExpression = "Transaction_Amount" HeaderText="Amount">
                                        <ItemTemplate>
                                            <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Transaction_Amount"))%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn  SortExpression = "Risks" HeaderText="Risks">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Risks")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                    
                                    <asp:TemplateColumn  SortExpression = "Bene_AC_Number" HeaderText="Beneficiary Acc. No">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Bene_AC_Number")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                    
                                    <asp:TemplateColumn  SortExpression = "Bene_AC_Type" HeaderText="Beneficiary Acc Type">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Bene_AC_Type")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                    
                                    <asp:TemplateColumn  SortExpression = "Bene_Name" HeaderText="Bene_Name">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Bene_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn  SortExpression = "Bene_Bank_Branch_Name" HeaderText="Bene_Bank_Branch_Name">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Bene_Bank_Branch_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn  SortExpression = "Bene_Branch_Routing_Number" HeaderText="Bene_Branch_Routing_Number">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Bene_Branch_Routing_Number")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn  SortExpression = "Reason" HeaderText="Reason">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Reason")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn  SortExpression = "RecieverID" HeaderText="RecieverID">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "RecieverID")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                    
                                    <asp:TemplateColumn  SortExpression = "Errorcode" HeaderText="Errorcode">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Errorcode")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                      
                                    <asp:TemplateColumn  SortExpression = "ErrorDescription" HeaderText="ErrorDescription">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "ErrorDescription")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                                    
                                    
                                   
                                                                                                                             
                                                                                                          
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
            <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px;
                    height: 100px; margin-left: 15px; margin-bottom: 20px; padding-left:20px">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td width="50px">
                                        <asp:LinkButton ID="btnAccept" runat="server" Text="Accept" CssClass="CommandButton"
                                            OnClick="btnAccept_Click" OnClientClick="return confirm('Are you sure you want to accept?')"></asp:LinkButton>
                                    </td>
                                    <td width="50px"></td>
                                    <td>
                                        <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="CommandButton"
                                            OnClick="btnReject_Click" OnClientClick="return confirm('Are you sure you want to reject?')"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnAcceptAll" runat="server" Text="Accept All Transactions" CssClass="CommandButton"
                                            OnClientClick="return confirm('Are you sure you want to accept all transaction?')" OnClick="btnAcceptAll_Click"></asp:LinkButton>
                                    </td>
                                    <td width="50px"></td>
                                    <td>
                                        <asp:LinkButton ID="btnRejectAll" runat="server" Text="Reject All Transactions" CssClass="CommandButton"
                                            OnClientClick="return confirm('Are you sure you want to reject all transaction?')" OnClick="btnRejectAll_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>                        
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <asp:Label ID="lblRejectInstruction" runat="server" CssClass="NormalBold" Text="For a rejection item(s) please give a reason"></asp:Label></td>
                        <td colspan="2" width="60%">
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <asp:TextBox ID="txtRejectedReason" runat="server" Width="300" MaxLength="50" OnKeyUp="return makeUppercase(this.name);" TextMode="MultiLine"></asp:TextBox></td>
                        <td colspan="2" width="60%" style="color: Red; font-weight: bold;">
                            <asp:Label ID="lblNoReturnReason" runat="server" Text="Please Enter a return reason"
                                Visible="false" ForeColor="red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>