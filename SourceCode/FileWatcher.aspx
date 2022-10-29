<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FileWatcher.aspx.cs" Inherits="EFTN.FileWatcher" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EFT Files</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div align="center">
                <asp:Label ID="lblHead" runat="server" CssClass="Head" /></div>
            <div>
                <table>
                    <tr>
                        <td width="100">
                        </td>
                        <td colspan="3">
                            <asp:DataGrid ID="dtgTransactionSentFiles" runat="Server" Height="0px" BorderWidth="0px"
                                GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1"
                                ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff" ItemStyle-CssClass="Normal"
                                FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader"
                                DataKeyField="FilePath">
                                <Columns>
                                    <asp:TemplateColumn HeaderText="FileName">
                                        <ItemTemplate>
                                            <table class="LightBorderTable">
                                                <tr>
                                                    <td width="30">
                                                    </td>
                                                    <td valign="top" align="Left">
                                                        <asp:CheckBox ID="cbxFileCheck" runat="server" />
                                                    </td>
                                                    <td class="NormalSmall" align="Left" width="400" nowrap>
                                                        <%# DataBinder.Eval(Container.DataItem,"FileName")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbxSelectAll" runat="server" CssClass="NormalBold" Text="Select All"
                                OnCheckedChanged="cbxSelectAll_CheckedChanged" AutoPostBack="true" /></td>
                        <td width="200px" align="center">
                            <asp:LinkButton ID="btnSendToPBM" runat="server" Text="Send to PBM" OnClick="btnSendToPBM_Click"
                                CssClass="CommandButton" /></td>
                        <td>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" CssClass="CommandButton"
                                OnClick="btnDelete_Click" /></td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
