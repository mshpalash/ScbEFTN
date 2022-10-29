<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MismatchedInwardReturn.aspx.cs"
    Inherits="EFTN.MismatchedInwardReturn" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mismatched Inward Returns</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Mismatched Inward Return</div>
                <div>
                    <a class="CommandButton" href="InwardReturnMaker.aspx">Inward Return Makern</a>
                </div>
            <div style="overflow: scroll">
                <table>
                    <tr>
                        <td colspan="3">
                            <asp:DataGrid ID="dtgInwardReturnMaker" AlternatingItemStyle-BackColor="lightyellow"
                                AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                DataKeyField="ReturnID">
                                <Columns>
                                    <asp:BoundColumn DataField = "EntryDateReturnReceived" HeaderText="EntryDateReturnReceived" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "OrgTraceNumber" HeaderText="OrgTraceNumber" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "TraceNumber" HeaderText="TraceNumber" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "ReturnCode" HeaderText="ReturnCode" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "StatusID" HeaderText="StatusID" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "AddendaInfo" HeaderText="AddendaInfo" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "DateOfDeath" HeaderText="DateOfDeath" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "SettlementJDate" HeaderText="SettlementJDate" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "PrintFlag" HeaderText="PrintFlag" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "EBBSCheckerStatus" HeaderText="EBBSCheckerStatus" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "CheckerStatus" HeaderText="CheckerStatus" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
                                    <asp:BoundColumn DataField = "XMLAmount" DataFormatString="{0:N}" HeaderText="XMLAmount" ItemStyle-Wrap ="False"  HeaderStyle-Wrap="False" />
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
