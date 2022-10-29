<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RNOCFiles.aspx.cs" Inherits="EFTN.RNOCFiles" %>
<%@ Register Src="Modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RNOC Files</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body topmargin=0 leftmargin=0>
<script language="javascript" type="text/javascript">
    javascript:window.history.forward(1);
</script>
    <form id="form1" runat="server">
        <uc1:Header ID="Header1" runat="server" />
        <div class="Head" align="center">
            RNOC Sent Files</div>
        <div>
            <div>
                <table cellpadding="5px">
                    <tr>
                        <td width="100px" rowspan="2"></td>
                        <td colspan="3">
                                <asp:DataGrid ID="dtgRNOCFiles" runat="Server" Height="0px" 
                                BorderWidth="0px" GridLines="None"
                                AutoGenerateColumns="False"  CellPadding="5" 
                                CellSpacing="1" 
                                 ItemStyle-BackColor="#dee9fc"
                                 AlternatingItemStyle-BackColor="#ffffff"
                                  ItemStyle-CssClass="Normal"
                                FooterStyle-CssClass="GrayBackWhiteFont"
                                HeaderStyle-CssClass="GrayBackWhiteFontFixedHeader" 
                                     DataKeyField="FilePath"
                                    >
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
                    <tr >
                        <td>
                            <asp:CheckBox ID="cbxSelectAll" runat="server"  CssClass="NormalBold" Text="Select All" OnCheckedChanged="cbxSelectAll_CheckedChanged" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:LinkButton ID="btnSendToPBM" runat="server" Text="Send to PBM" OnClick="btnSendToPBM_Click"  CssClass="CommandButton" OnClientClick="return confirm('Are you sure you want to send to PBM?')"/>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete"  CssClass="CommandButton" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete?')" />
                        </td>
                    </tr>
                </table>
            </div>
    </form>
</body>
</html>
