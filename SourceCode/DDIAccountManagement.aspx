<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DDIAccountManagement.aspx.cs" Inherits="EFTN.DDIAccountManagement" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>DDI Account Management</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]--> 
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header" runat="server" />
        <div class="Head" align="center">DDI Account Management</div>
        <div>
            <table>
                <tr>
                    <td class="NormalBold">
                        Please Select your Excel File to Upload<br />
                        <asp:FileUpload CssClass="inputlt" ID="fulExcelFile" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnUploadExcel" runat="server" CssClass="inputlt" Text="Upload File"
                            Width="80" 
                            OnClientClick="return confirm('Are you sure you want to import this file?')" 
                            onclick="btnUploadExcel_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" style="width:940px; height:350px; overflow:scroll; ">
            <asp:datagrid Id="MyDataGrid2"
                HeaderStyle-CssClass="GrayBackWhiteFont" 
                FooterStyle-CssClass="GrayBackWhiteFont" 
                ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc"   
                AlternatingItemStyle-BackColor="#FFFFFF" 
                runat="server" CellSpacing="1"  CellPadding="5" 
                autogeneratecolumns="false"  DataKeyField="AccountID" 
                gridlines="None" borderwidth="0px" ShowFooter="true"  Height="0px" 
                OnItemCommand="MyDataGrid2_ItemCommand" >
                <Columns>
                <asp:EditCommandColumn CausesValidation="False" EditText="Edit" UpdateText="Update" CancelText="Cancel"></asp:EditCommandColumn>
                    <asp:TemplateColumn HeaderText="SCB Credit Account No."  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "AccountNo")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="txtAccountNo" Width="90" MaxLength="17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AccountNo") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addAccountNo" Width="90"  Runat="Server" MaxLength="17"/>
                                <asp:RequiredFieldValidator id="RequiredFieldValidatoraddAccountNo" 
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addAccountNo"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Other Bank Account No"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "OtherBankAcNo")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="txtOtherBankAcNo" MaxLength="17" Width="90" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OtherBankAcNo") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addOtherBankAcNo" Width="90"  Runat="Server" MaxLength="17"/>
                                <asp:RequiredFieldValidator id="RequiredFieldValidatoraddOtherBankAcNo" 
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addOtherBankAcNo"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="RoutingNumber"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "RoutingNumber")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="txtRoutingNumber" Width="90" MaxLength="9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RoutingNumber") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addRoutingNumber" Width="90"  Runat="Server" MaxLength="9"/>
                                <asp:RequiredFieldValidator id="RequiredFieldValidatoraddRoutingNumber" 
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addRoutingNumber"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Expiry Date (MM-DD-YYYY)"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "ExpiryDate")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="txtExpiryDate" Width="90" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ExpiryDate") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addExpiryDate" Width="90"  Runat="Server" />MM-DD-YYYY
                                <asp:RequiredFieldValidator id="RequiredFieldValidatoraddExpiryDate" 
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addExpiryDate"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Exception"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "AccountException")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkBoxException" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem, "AccountException") %>' />
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:CheckBox ID="chkBoxADDException" runat="server"/>
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:LinkButton CommandName="Delete" Text="Delete" ID="btnDel" ForeColor="Blue"  Runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to Delete The Account Info?')"></asp:LinkButton>
                            </ItemTemplate>
                         <FooterTemplate>
                                <asp:linkButton CommandName="Insert" Text="Add" ID="btnAdd" ForeColor="white"  Runat="server" />
                        </FooterTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:datagrid>
        </div>
        <div align="center" style="width:800px;">
            <asp:Label runat="server" ID="txtMsg" Font-Size="Large"></asp:Label>
        </div>
        <div align="center" style="width:940px; height:350px; overflow:scroll; ">
            <asp:datagrid Id="dtgErrGrid"
                HeaderStyle-CssClass="GrayBackWhiteFont" 
                FooterStyle-CssClass="GrayBackWhiteFont" 
                ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc"   
                AlternatingItemStyle-BackColor="#FFFFFF" 
                runat="server" CellSpacing="1"  CellPadding="5" 
                autogeneratecolumns="false"  DataKeyField="RowNumber" 
                gridlines="None" borderwidth="0px" ShowFooter="true"  Height="0px" >
                <Columns>
                    <asp:TemplateColumn HeaderText="Row No."  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "RowNumber")%>
                         </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="SCB Credit Account No."  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "AccountNo")%>
                         </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Other Bank Account No"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "OtherBankAcNo")%>
                         </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="RoutingNumber"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "RoutingNumber")%>
                         </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Exception Message"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "ExceptionMsg")%>
                         </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:datagrid>
        </div>

        <div>
            <asp:Label runat="server" ID="lblTestCookie"></asp:Label>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
