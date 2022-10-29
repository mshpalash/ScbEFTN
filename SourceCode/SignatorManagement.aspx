<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignatorManagement.aspx.cs" Inherits="EFTN.SignatorManagement" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Signature Management</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script language="javascript" type="text/javascript">
    javascript:window.history.forward(1);
</script>
</head>
<body class="wrap" id="content">
   
<form id="form1" runat="server">
<div class="maincontent">
    <uc1:Header ID="Header" runat="server" /><div class="Head" align="center">
            Signature Management</div>
            
    <div align="center" class="boxmodule" style="padding-top:10px; width:500px; margin-top:10px; height:300px; margin-left:220px">
       
        <table>
            <tr>
                <td>
                        <asp:Calendar ID="calendarSignature" runat="server" BackColor="White" BorderColor="#3366CC" 
                            BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" 
                            Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px" SelectionMode="DayWeekMonth">
                            
                            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                            <WeekendDayStyle BackColor="#CCCCFF" />
                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True"
                                Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                        </asp:Calendar>
                </td>
            </tr>        
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Counter Signature"></asp:Label>
                    <asp:DropDownList ID="ddListSignatorCounter" runat="server">
                        <asp:ListItem Value="Mohammad Morshedur Rahaman" Selected="True">Mohammad Morshedur Rahaman</asp:ListItem>
                        <asp:ListItem Value="Mohammd Anwar Hossain">Mohammd Anwar Hossain</asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Signature"></asp:Label>
                    <asp:DropDownList ID="ddListSignator" runat="server">
                        <asp:ListItem Value="Mohammad Morshedur Rahaman" Selected="True">Mohammad Morshedur Rahaman</asp:ListItem>
                        <asp:ListItem Value="Mohammd Anwar Hossain">Mohammd Anwar Hossain</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnGenerate" runat="server" onclick="btnGenerate_Click" 
                        Text="Generate Signature" />
                </td>
            </tr>
            <tr>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </tr>
        </table>       
    </div>
</div>
    <uc2:footer ID="Footer1" runat="server" />
    
</form>
</body>
</html>
