<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RejectedReceivedReturnApproved.aspx.cs"
    Inherits="EFTN.RejectedReceivedReturnApproved" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
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
    </script>

</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Return Received Approved by Maker but Rejected by Checker</div>
            <div style="overflow: scroll">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td width="150">
                                        <asp:CheckBox ID="cbxAll" runat="server" Text="Select All" CssClass="NormalBold"
                                            AutoPostBack="true" OnCheckedChanged="cbxAll_CheckedChanged"/></td>
                                </tr>                                
                                <tr>
                                    <td>
                                        <asp:DataGrid ID="dtgApprovedReturnChecker" AlternatingItemStyle-BackColor="lightyellow"
                                            AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                            FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                            Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                            DataKeyField="ReturnID" Width="980px">
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBEFTNList" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                              <asp:TemplateColumn HeaderText="Rejected Reason By Checker">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "RejectedReason")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                 <asp:TemplateColumn HeaderText="BankName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="A/C No. From BEFTN">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="A/C No. From CBS">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "ACCOUNT")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ReceiverName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ReceiverName as of CBS">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "TITLE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="STATUS">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "STATUS")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="RISKS">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "RISKS")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="OrgTraceNumber">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "OrgTraceNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="TraceNumber">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "TraceNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Return Reason">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "RejectReason")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="AddendaInfo">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "AddendaInfo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="DateOfDeath">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DateOfDeath")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                              
                                                <asp:TemplateColumn HeaderText="IdNumber">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "IdNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ReceiverName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                              
                                                <asp:TemplateColumn HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                              <asp:TemplateColumn HeaderText="OrgSettlementJDate">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "OrgSettlementJDate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                              <asp:TemplateColumn HeaderText="SettlementJDate">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "SettlementJDate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rblDishonorDecision" runat="server" RepeatDirection="Horizontal"
                                CssClass="NormalBold" AutoPostBack="true" OnSelectedIndexChanged="rblDishonorDecision_SelectedIndexChanged">
                                <asp:ListItem Text="Accept" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Dishonor" Value="5"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="NormalBold" nowrap>
                            Dishonour Reasons:
                            <asp:DropDownList ID="ddlDishonour" runat="server" DataTextField="RejectReason" DataValueField="RejectReasonCode" />
                        </td>
                        <td class="NormalBold">
                            AddendaInfo:
                            <asp:TextBox ID="txtAddendaInfo" runat="server" OnKeyUp="return makeUppercase(this.name);" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px">
                            <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="CommandButton"
                                OnClick="btnSave_Click" OnClientClick="return confirm('Are you sure you want to save?')" />
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CssClass="CommandButton"
                                OnClick="btnCancel_Click" />
                        </td>

                    </tr>   
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
