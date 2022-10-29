<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BranchMessages.aspx.cs" Inherits="EFTN.BranchMessages" %>
<%@ Register Src="modules/CommonHeader.ascx" TagName="CoomonHeader" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Messages: Flora EFT System</title>
    <link href="includes/sitec.css" rel="stylesheet" type="text/css" /> 
</head>
<body topmargin="0" leftmargin="0">
    <form id="form1" runat="server">
     <uc1:CoomonHeader ID="BranchHeader1" runat="server" /><br />
    <a href="Default.aspx"></a>
        <div class="Head" align="center">Login Info</div>
        <div><br />
            <table>
                <tr>
                    <td width="80"></td>
                    <td><asp:Label ID="Msg" runat="server" CssClass="NormalBlue"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <!----------------------->
                        <asp:DataList ID="MyDataList" runat="server" CellSpacing="0"  CellPadding="0"  ShowFooter="true" Height="0"
	                            HeaderStyle-CssClass="GrayBackWhiteFont"  
	                            FooterStyle-CssClass="GrayBackWhiteFont"
	                            ItemStyle-BackColor="LightYellow" 
	                            AlternatingItemStyle-BackColor="#FFFFFF">
	                            <ItemTemplate>
	                                <table cellpadding="0" cellspacing="0" border="0"  Class="LightBorderTable" style="width:920px">
	                                    <tr>
	                                        <td class="NormalBold"><%#DataBinder.Eval(Container.DataItem, "HistoryTime")%></td>
	                                        <td class="NormalBold"><%#DataBinder.Eval(Container.DataItem, "ChangeType")%></td>
	                                        <td class="NormalBold"><%#DataBinder.Eval(Container.DataItem, "IPAddress")%></td>
	                                    </tr>
	                               </table>
	                            </ItemTemplate>
	                    </asp:DataList>
                        <!----------------------->
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:LinkButton ID="linkBtnContinue" Visible="true" runat="server" Text="Continue" 
                            class="CommandButton" onclick="linkBtnContinue_Click"></asp:LinkButton>
                    </td>
                </tr>
            </table>
            
        </div>
    </form>
</body>
</html>

