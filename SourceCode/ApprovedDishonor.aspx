<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovedDishonor.aspx.cs" Inherits="EFTN.ApprovedDishonor" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Inward Dishonor</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body  class="wrap" id="content">
<script language="javascript" type="text/javascript">
    javascript:window.history.forward(1);
</script>
    <form id="form1" runat="server"><div class="maincontent">
        <uc1:Header ID="Header1" runat="server" />
        <div class="Head" align="center">
            Approved Dishonor</div>
        <div style="overflow:scroll">
            <table>
                <tr>
                    <td colspan="3">
                        <asp:DataGrid ID="dtgApprovedDishonorList" AlternatingItemStyle-BackColor="lightyellow"
                            AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                            FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                            Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                            DataKeyField="DishonoredID" AllowPaging="True" PageSize="50" OnPageIndexChanged="dtgApprovedDishonorList_PageIndexChanged">
                            <Columns>
                                <asp:TemplateColumn HeaderText="SL.">
                                    <ItemTemplate>
                                        <%#(dtgApprovedDishonorList.PageSize * dtgApprovedDishonorList.CurrentPageIndex) + Container.ItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBoxApprovedDishonor" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DishonoredID" HeaderText="DishonoredID" Visible="False" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TransactionCode" HeaderText="TransactionCode" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DFIAccountNo" HeaderText="DFIAccountNo" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BankName" HeaderText="BankName" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SendingBankRoutNo" HeaderText="SendingBankRoutNo" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                 <asp:BoundColumn DataField="Currency" HeaderText="Currency">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SessionID" HeaderText="Session">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Amount" HeaderText="Amount" DataFormatString="{0:N}">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IdNumber" HeaderText="IdNumber" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ReceiverName" HeaderText="ReceiverName" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PaymentInfo" HeaderText="PaymentInfo" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ReturnTraceNumber" HeaderText="ReturnTraceNumber" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DishonorReason" HeaderText="DishonorReason" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ReturnCode" HeaderText="ReturnCode" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DishonorTraceNumber" HeaderText="DishonorTraceNumber" >
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                            <FooterStyle CssClass="GrayBackWhiteFont" />
                            <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Position="TopAndBottom" />
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
                    <td style="height: 21px">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="CommandButton" OnClick="btnApprove_Click" OnClientClick="return confirm('Are you sure you want to approve?')"/>
                                </td>
                                <td width="20px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="CommandButton" OnClick="btnReject_Click" OnClientClick="return confirm('Are you sure you want to reject?')"/>
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
