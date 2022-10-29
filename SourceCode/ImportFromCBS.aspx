<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ImportFromCBS.aspx.cs"
    Inherits="EFTN.ImportFromCBS" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Import From CBS</title>
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
                Import From CBS</div>
            <div>
                <table cellpadding="5px">
                    <tr>
                        <td width="100px" rowspan="2">
                        </td>
                        <td colspan="3">
                            <asp:DataGrid ID="dtgCBSFiles" runat="Server" Height="0px" BorderWidth="0px" GridLines="None"
                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-BackColor="#dee9fc"
                                AlternatingItemStyle-BackColor="#ffffff" ItemStyle-CssClass="Normal" FooterStyle-CssClass="GrayBackWhiteFont"
                                HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader" DataKeyField="FilePath">
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
                            <asp:CheckBox ID="cbxSelectAll" runat="server" CssClass="NormalBold" Text="Select All"
                                OnCheckedChanged="cbxSelectAll_CheckedChanged" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:LinkButton ID="btnImport" runat="server" Text="Import" CssClass="CommandButton"
                                OnClick="btnImport_Click" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
