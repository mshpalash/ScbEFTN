<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListOfContestedRejectedByChecker.aspx.cs" Inherits="EFTN.ListOfContestedRejectedByChecker" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script language="javascript" type="text/javascript">
    javascript:window.history.forward(1);
</script>
</head>
<body class="wrap" id="content">

    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header1" runat="server" />
        <div class="Head" align="center">
            (Contested List) Rejected by checker which were approved by maker</div>
        <div style="overflow:scroll">
            <table>
                <tr>
                    <td colspan="3">
                        <asp:DataGrid ID="dtgListOfContestedRejectedByChecker" runat="Server" Width="600"
                            BorderWidth="0px" GridLines="None" AutoGenerateColumns="False" CellPadding="5"
                            CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                            ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="DishonoredID">
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBoxDishonorList" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="TransactionCode" HeaderText="TransactionCode" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="DFIAccountNo" HeaderText="DFIAccountNo" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="BankName" HeaderText="BankName" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="SendingBankRoutNo" HeaderText="SendingBankRoutNo"
                                    ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="Amount" HeaderText="Amount" DataFormatString="{0:N}" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="IdNumber" HeaderText="IdNumber" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="ReceiverName" HeaderText="ReceiverName" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="PaymentInfo" HeaderText="PaymentInfo" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                                <asp:BoundColumn DataField="ReturnTraceNumber" HeaderText="ReturnTraceNumber" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="true" />
                                <asp:BoundColumn DataField="DishonorReason" HeaderText="DishonorReason" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="true" />
                                <asp:BoundColumn DataField="ReturnCode" HeaderText="ReturnCode" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="true" />
                                <asp:BoundColumn DataField="DishonorTraceNumber" HeaderText="DishonorTraceNumber" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="true" />
                                <asp:BoundColumn DataField="ContestedDishonoredCode" HeaderText="ContestedDishonoredCode" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />                                    
                                <asp:BoundColumn DataField="TraceNumber" HeaderText="TraceNumber" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False" />
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
