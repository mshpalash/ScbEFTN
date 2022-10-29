<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ReinsertReturnMaker.aspx.cs"
    Inherits="EFTN.ReinsertReturnMaker" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Re-Insert Returns</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">
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
                Re-Insert Return</div>
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
            <br />                
                <div>
                    <a class="CommandButton" href="MismatchedInwardReturn.aspx">Mismatched Inward Return</a>
                </div>
                <div align="center" class="boxmodule" style="padding-top: 10px; width: 970px; margin-top: 10px;
                    height: 40px; margin-left: 5px"> 
                <table>
                    <tr align="left">
                        <%--<td width="100">
                            <asp:DropDownList ID="ddListTransactionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddListTransactionType_SelectedIndexChanged">
                                <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                <asp:ListItem Value="Debit">Debit</asp:ListItem>
                            </asp:DropDownList>
                        </td>  --%>
                        <td width="150px">
                            <asp:CheckBox ID="cbxAll" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAll_CheckedChanged"/>
                        </td>
                        <td width="20px">
                        </td>
                        <%--<td>
                            <asp:Label ID="lblTotalItem" runat="server" CssClass="NormalBold">
                            </asp:Label>
                        </td>
                        <td width="50px">
                        </td>
                        <td>
                            <asp:Label ID="lblTotalAmount" runat="server" CssClass="NormalBold">
                            </asp:Label>
                        </td>         --%>
                    </tr>
                </table>
                </div>
                <table>
                    <tr>
  
                    </tr>
                </table>
            <div style="overflow: scroll; height:350px">

                            <table>

                                <tr>
                                    <td colspan="3">
                                        <asp:DataGrid ID="dtgReinsertReturnMaker" AlternatingItemStyle-BackColor="lightyellow"
                                            AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                            FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                            Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                            DataKeyField="OrgTraceNumber" 
                                            OnItemDataBound="dtgReinsertReturnMaker_ItemDataBound" 
                                            HeaderStyle-ForeColor="#FFFFFF" 
                                            AllowPaging="True" AllowSorting="True" 
                                            OnPageIndexChanged="dtgReinsertReturnMaker_PageIndexChanged" 
                                            PageSize="500" OnSortCommand="dtgReinsertReturnMaker_SortCommand">
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBEFTNList" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="SL.">
                                                    <ItemTemplate>
                                                        <%#(dtgReinsertReturnMaker.PageSize * dtgReinsertReturnMaker.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                               <%--<asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <a href="InwardReturnModifier.aspx?inwardReturnID=<%#DataBinder.Eval(Container.DataItem, "ReturnID")%>">
                                                            Re-Generate As New Transaction</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>             --%>                                                                               
                                                <%--<asp:TemplateColumn HeaderText="Sender A/C No." SortExpression = "AccountNo">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "AccountNo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="TITLE" SortExpression = "TITLE">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "TITLE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                                
                                                <asp:TemplateColumn HeaderText="ReturnID" SortExpression = "ReturnID">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "ReturnID")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Entry Date Return Received" SortExpression = "EntryDateReturnReceived">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "EntryDateReturnReceived")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>                                                
                                                <%--<asp:TemplateColumn HeaderText="Return Reason" SortExpression = "RejectReason">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "RejectReason")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                                <%--<asp:TemplateColumn HeaderText="BankName" SortExpression = "BankName">
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
                                                </asp:TemplateColumn>--%>
                                                
                                                <asp:TemplateColumn HeaderText="SettlementJDate" SortExpression = "SettlementJDate">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "SettlementJDate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <%--<asp:TemplateColumn HeaderText="A/C No. From BEFTN" SortExpression = "DFIAccountNo">
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
                                                </asp:TemplateColumn>--%>
                                                <asp:TemplateColumn HeaderText="Amount from XML" SortExpression = "XMLAmount">
                                                    <ItemTemplate>
                                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "XMLAmount"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                              <%--  <asp:TemplateColumn HeaderText="Mismatch Amount"  SortExpression = "MismatchAmount">
                                                    <ItemTemplate>
                                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "MismatchAmount"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>                                                
                                                <asp:TemplateColumn HeaderText="Receiver Name" SortExpression = "ReceiverName">
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
                                                </asp:TemplateColumn>--%>
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
                                                 <asp:TemplateColumn HeaderText="ReturnCode" SortExpression = "ReturnCode">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "ReturnCode")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                               <%-- <asp:TemplateColumn HeaderText="Addenda Info" SortExpression = "AddendaInfo">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "AddendaInfo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="IdNumber" SortExpression = "IdNumber">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "IdNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="DataEntryType" SortExpression = "DataEntryType">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DataEntryType")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>                                                
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
                    <%--<tr>
                        <td class="NormalBold">
                            Info :
                        </td>
                    </tr>--%>

                   <%-- <tr>
                        <td>
                            <asp:RadioButtonList ID="rblDishonorDecision" runat="server" RepeatDirection="Horizontal"
                                CssClass="NormalBold" AutoPostBack="true" OnSelectedIndexChanged="rblDishonorDecision_SelectedIndexChanged">
                                <asp:ListItem Text="Accept" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Dishonor" Value="5"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td class="NormalBold" nowrap ="nowrap">
                            Dishonour Reasons:
                            <asp:DropDownList ID="ddlDishonour" runat="server" DataTextField="RejectReason" DataValueField="RejectReasonCode" />
                        </td>
                        <td class="NormalBold">
                            AddendaInfo:
                            <asp:TextBox ID="txtAddendaInfo" runat="server" OnKeyUp="return makeUppercase(this.name);" OnKeyPress="return onlyAlphaNumeric(this.name);" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="height: 21px">
                            <asp:LinkButton ID="btnSave" runat="server" Text="Re-Insert" CssClass="CommandButton"
                                OnClick="btnSave_Click" OnClientClick="return confirm('Are you sure you want to save?')" />
                        </td>
                        <%--<td>
                            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CssClass="CommandButton"
                                OnClick="btnCancel_Click" />                        
                        </td>--%>
                    </tr>                
                </table>
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
