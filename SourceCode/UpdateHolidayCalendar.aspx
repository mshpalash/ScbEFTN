<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateHolidayCalendar.aspx.cs" Inherits="EFTN.UpdateHolidayCalendar" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Transaction Received Files</title>
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
            Update Holidays</div>
            
    <div align="center" class="boxmodule" style="padding-top:10px; width:500px; margin-top:10px; height:300px; margin-left:220px">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblHolidayCalendar" runat="server" Text="Update Holiday :"></asp:Label>
                </td>
                <td>
                    <asp:Calendar ID="calendarHoliday" runat="server" BackColor="White" BorderColor="#3366CC" 
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
                    <asp:Label ID="lblDesc" runat="server" Text="Holiday Description :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHolidayDesc" runat="server"></asp:TextBox>
                </td>
            </tr>

        </table>
        
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnUpdate" Text="Update" runat="server" OnClick="btnUpdate_Click" />
                </td>
                <td>
                    <asp:Button ID="btnCancel" Text="Cencel" runat="server" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>        
    </div>
    <div align="center" class="boxmodule" style="padding-top:10px; width:500px; margin-top:10px; height:300px; margin-left:220px">
                <asp:Panel ID="pnlHolidays" runat="server" Height="400px">
                    <asp:DataGrid ID="dtgHolidays" runat="Server" AlternatingItemStyle-BackColor="lightyellow"
                        AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="2"
                        FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" FooterStyle-HorizontalAlign="right"
                        HeaderStyle-CssClass="GrayBackWhiteFont" Height="0px" ItemStyle-BackColor="White"
                                            HeaderStyle-ForeColor="#FFFFFF" 
                        ItemStyle-CssClass="NormalSmall" ShowFooter="true" PagerStyle-Position="Top">
                        <Columns>
                            <asp:TemplateColumn HeaderText="SL.">
                                <ItemTemplate>
                                    <%#Container.ItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CalDate" HeaderText="CalDate"  SortExpression = "CalDate">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Description" HeaderText="Description" SortExpression = "Description">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle CssClass="GrayBackWhiteFont" HorizontalAlign="Right" />
                        <PagerStyle Mode="NumericPages" Position="Top" />
                        <AlternatingItemStyle BackColor="LightYellow" />
                        <ItemStyle BackColor="White" CssClass="NormalSmall" />
                        <HeaderStyle CssClass="GrayBackWhiteFont" />
                    </asp:DataGrid>
                </asp:Panel>
            </div>
</div>
    <uc2:footer ID="Footer1" runat="server" />
    
</form>
</body>
</html>
