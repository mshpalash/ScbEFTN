<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ApprovedInwardTransaction.aspx.cs"
    Inherits="EFTN.ApprovedInwardTransaction" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
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
    </script>
</head>
<body class="wrap" id="content" > 

    <form id="form1" runat="server">
    <div class="maincontent">
    
    <uc1:Header ID="Header1" runat="server" />
        <div class="Head" align="center">
            Inward Transaction</div>
        <div>
            <table>
                <tr>
                    <td width="10px">
                    </td>
                    <td>
                        <asp:CheckBox ID="cbxAll" runat="server" Text="Select All" CssClass="NormalBold"
                            AutoPostBack="true" OnCheckedChanged="cbxAll_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td width="10px">
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td colspan="3">
                                    <div id="AvailDiv" runat="server" style="position: relative; overflow: auto; width: 980px;
                                        height: 350px;">
                                        <asp:DataGrid ID="dtgInwardApprovedTransaction" AlternatingItemStyle-BackColor="lightyellow"
                                            AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                            FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                            Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                            DataKeyField="EDRID" Width="980px">
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbxCheck" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="TraceNumber">
                                                    <ItemTemplate>
                                                        <a href="InwardTransactionWithMICR.aspx?inwardTransactionEDRID=<%#DataBinder.Eval(Container.DataItem, "EDRID")%>">
                                                            Check with CBS</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="BankName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ACC/NO From BEFTN">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ACC/NO From CBS">
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
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="NormalBold">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%">
                                    <asp:RadioButtonList ID="rblTransactionDecision" runat="server" RepeatDirection="Horizontal"
                                        CssClass="NormalBold" AutoPostBack="true" OnSelectedIndexChanged="rblTransactionDecision_SelectedIndexChanged">
                                        <asp:ListItem Text="Accept" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="80%">
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblRejectReasonMsg" runat="server" Text="Reject Reason:" Visible="false" />
                                    <asp:TextBox ID="txtRejectReason" TextMode="MultiLine" runat="server" CssClass="NormalBold" Visible="false"
                                        OnKeyUp="return makeUppercase(this.name);" />
                                </td>
                            </tr>
                            <tr>
                                <td width="40%">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="CommandButton"
                                        OnClick="btnSave_Click" OnClientClick="return confirm('Are you sure you want to save?')"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
