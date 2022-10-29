<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ImportRNOC.aspx.cs" Inherits="EFTN.ImportRNOC" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Received NOC Files</title>
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
                Received NOC Files</div>
            <div>
                <table cellpadding="5px">
                    <tr>
                        <td width="100px" rowspan="3">
                        </td>
                        <td colspan="3">
                            <asp:DataGrid ID="dtgListOfRNOCXML" runat="Server" Height="0px" BorderWidth="0px"
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
                            <asp:CheckBox ID="cbxSelectAll" runat="server" CssClass="NormalBold" Text="Select All"
                                OnCheckedChanged="cbxSelectAll_CheckedChanged" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:LinkButton ID="btnImportRNOC" runat="server" Text="Import RNOC" CssClass="CommandButton"
                                OnClick="btnImportRNOC_Click" OnClientClick="return confirm('Are you sure you want to import?')" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblImportedData" runat="server" Text=" Imported Data:" CssClass="NormalBold" />
                            <asp:DataGrid ID="dtgInwardRNOC" AlternatingItemStyle-BackColor="lightyellow" AutoGenerateColumns="false"
                                BorderWidth="0px" CellPadding="5" CellSpacing="1" FooterStyle-CssClass="GrayBackWhiteFont"
                                GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="#CAD2FD"
                                ItemStyle-CssClass="Normal" runat="server" DataKeyField="RNOCID">
                                <Columns>
                                    <asp:TemplateColumn HeaderText="NOC TraceNumber">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "NOCTraceNumber")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="CORTraceSeqNum">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "CORTraceSeqNum")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="CorrectedData">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "CorrectedData")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="StatusID">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "StatusID")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="RefusedCode">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "RefusedCode")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
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
