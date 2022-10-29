<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrancateEFTDatabase.aspx.cs" Inherits="EFTN.TrancateEFTDatabase" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Trancate Database</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header1" runat="server" />
        <div class="Head" align="center">
            Trancate Database</div>
        <div>
            <table cellpadding="5px">
                <tr>
                    <td>
                        <asp:LinkButton ID="btnTrancateEFTDb" runat="server" Text="Trancate Database" CssClass="CommandButton" OnClick="btnTrancateEFTDb_Click"
                            />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkBtnClearFolder" runat="server" Text="Clear Folder" CssClass="CommandButton" OnClick="lnkBtnClearFolder_Click"/>
                    </td>
                </tr>
            </table>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
