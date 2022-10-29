<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="EFTN.Messages" %>

<%@ Register Src="modules/Header.ascx" TagName="AdminHeader" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Messages: Flora EFT System</title>
    <link href="includes/sitec.css" rel="stylesheet" type="text/css" /> 
</head>
<body topmargin="0" leftmargin="0">
    <form id="form1" runat="server">
    <uc1:AdminHeader ID="AdminHeader1" runat="server" />
    <a href="Default.aspx"></a>
        <div class="Head" align="center">Messages</div>
        <div>
            <table>
                <tr>
                    <td width="80"></td>
                    <td>
                        <!----------------------->
                        <a href="EditMessage.aspx" class="CommandButton">Add New Message</a>
                        <asp:datagrid Id="MyDataGrid"
	                            HeaderStyle-CssClass="GrayBackWhiteFont"  
	                            FooterStyle-CssClass="GrayBackWhiteFont" 
	                            ItemStyle-CssClass="NormalSmall" 
	                            ItemStyle-BackColor="#dee9fc"   
	                            AlternatingItemStyle-BackColor="#FFFFFF" 
	                            runat="server" CellSpacing="1"  CellPadding="5" 
	                            autogeneratecolumns="false" 
            	                gridlines="None" borderwidth="0px" 
            	                ShowFooter="true"  Height="0px" 
            	                 OnItemCommand="MyDataGrid_ItemCommand" DataKeyField="MessageID">
            	                <Columns>
                                    <asp:BoundColumn DataField = "BankCode"         HeaderText="BankCode"     ItemStyle-Wrap ="false"  HeaderStyle-Wrap="False" />       
                                    <asp:BoundColumn DataField = "MessageDate"      HeaderText="Entry Date"   ItemStyle-Wrap ="false"  HeaderStyle-Wrap="False" />       
                                    <asp:BoundColumn DataField = "MessageFrom"      HeaderText="From"         ItemStyle-Wrap ="false"  HeaderStyle-Wrap="False" ItemStyle-Width="150px" />       
                                    <asp:BoundColumn DataField = "BranchName"       HeaderText="Branch"       ItemStyle-Wrap ="false"  HeaderStyle-Wrap="False" />       
                                    <asp:BoundColumn DataField = "ExpiryDate"       HeaderText="Expiry Date"  ItemStyle-Wrap ="false"  HeaderStyle-Wrap="False" />       
                                    <asp:BoundColumn DataField = "DaysLeft"         HeaderText="Days Left"    ItemStyle-Wrap ="false"  HeaderStyle-Wrap="False" />       
                                    <asp:BoundColumn DataField = "MessageText"      HeaderText="Message"      ItemStyle-Wrap ="true"   HeaderStyle-Wrap="False" ItemStyle-Width="450px"  ItemStyle-CssClass="Normal" />       
                                    <asp:ButtonColumn CommandName="ExpireMessage" Text="Expire Now" CausesValidation="false"></asp:ButtonColumn>
            	                </Columns>
	                        </asp:datagrid>
                        <!----------------------->
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

