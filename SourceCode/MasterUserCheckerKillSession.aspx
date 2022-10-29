<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterUserCheckerKillSession.aspx.cs" Inherits="FloraSoft.MasterUserCheckerKillSession" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="modules/SuperAdminCheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Management</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]--> 
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header" runat="server" />
        <div class="Head" align="center">Kill Session</div>
        <div align="center" class="boxmodule" style="width:940px;margin-left:20px; padding-top:15px; padding-left:15px; min-height:400px; overflow: scroll; margin-bottom:15px; margin-top:20px">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblUserStatus" Text="Enter Login ID" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLoginID" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnUnlocked" runat="server" Text="Kill Session" 
                        CausesValidation="false" onclick="btnUnlocked_Click" />
                </td>
             <%--   <td>
                    <asp:Button ID="btnKillSession" runat="server" Text="Kill Session" 
                        CausesValidation="false" onclick="btnKillSession_Click" />
                </td>--%>
                  
                <td>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </td>
            </tr>
          <%--   <tr>
                 <td>
                    <asp:Label ID="lblunlockedGlobalvariable" Text="Button for Reset Other Operator Generating File " runat="server"></asp:Label>
                </td>
                  <td >
                    <asp:Button  ID="btn_unlocked_global_veriable" runat="server" Text="Unlocked Global veriable" 
                        CausesValidation="false" onclick="btn_unlocked_global_veriable_Click" />
                </td>
            </tr>--%>
        </table>
            
        </div>
        <div>
            <asp:Label runat="server" ID="lblTestCookie"></asp:Label>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
