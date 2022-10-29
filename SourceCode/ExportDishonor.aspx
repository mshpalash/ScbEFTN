<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportDishonor.aspx.cs" Inherits="EFTN.ExportDishonor" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Export Dishonor</title>
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
            Export Dishonor
        </div>
     <div>
                <div id="AvailDiv" runat="server" style="position: relative; overflow: auto; width: 750px;
                    height: 200px;">
                    <table>
                        <tr>
                           <td width="100px" rowspan="3"></td>

                            <td class="NormalBold" align="center" colspan="2">
                                <asp:DataGrid ID="dtgBatchDishonorSent" runat="Server" Width="600" BorderWidth="0px"
                                    GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1"
                                    FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader"
                                    ItemStyle-BackColor="#dee9fc"  ItemStyle-CssClass="Normal" AlternatingItemStyle-BackColor="#ffffff" DataKeyField="TransactionID">
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbxSentBatch" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="BatchNumber" HeaderText="BatchNo" ItemStyle-Wrap="False"
                                            HeaderStyle-Wrap="true" />
                                        <asp:BoundColumn DataField="SECC" HeaderText="SECC" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                        <asp:BoundColumn DataField="EffectiveEntryDate" HeaderText="Effective Date" DataFormatString="{0:d}"
                                            ItemStyle-Wrap="true" HeaderStyle-Wrap="False" />
                                        <asp:BoundColumn DataField="EntryDesc" HeaderText="Description" ItemStyle-Wrap="False"
                                            HeaderStyle-Wrap="False" />
                                        <asp:BoundColumn DataField="CompanyId" HeaderText="CompanyId" ItemStyle-Wrap="False"
                                            HeaderStyle-Wrap="False" />
                                        <asp:BoundColumn DataField="CompanyName" HeaderText="CompanyName" ItemStyle-Wrap="False"
                                            HeaderStyle-Wrap="False" />
                                        <asp:BoundColumn DataField="TotalTransactions" HeaderText="Total" ItemStyle-Wrap="False"
                                            HeaderStyle-Wrap="False" />
                                        <asp:BoundColumn DataField="TotalAmount" HeaderText="TotalAmount" DataFormatString="{0:N}"
                                            ItemStyle-Wrap="False" HeaderStyle-Wrap="False" />
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsgExportTransaction" CssClass="NormalRed" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td width="150"><asp:CheckBox ID="cbxAll" runat="server" Text="Select All" CssClass="NormalBold" AutoPostBack="true" OnCheckedChanged="cbxAll_CheckedChanged" /></td>
                                        <td><asp:LinkButton ID="btnGenerateXml" runat="server" Text="Generate Envelope" CssClass="CommandButton" OnClick="btnGenerateXml_Click"
                                         OnClientClick="return confirm('Are you sure you want to generate the envelop to export?')" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
     
    </form>
</body>
</html>
