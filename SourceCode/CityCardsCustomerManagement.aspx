<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CityCardsCustomerManagement.aspx.cs" Inherits="EFTN.CityCardsCustomerManagement" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Cards Customer Management</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]--> 
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header" runat="server" />
        <div align="center">
            <table>
                <tr height="10px">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="center"><a href="CityCardsManagement.aspx" class="CommandButton">Cards Management</a></td>
                </tr>
            </table>
        </div>

        <div class="Head" align="center">City Cards Customer Management</div>
        <div id="divUploadCustomer" visible="false" runat="server">
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
                autogeneratecolumns="false"  DataKeyField="CustomerID" 
                gridlines="None" borderwidth="0px" ShowFooter="true"  Height="0px" 
                OnItemCommand="MyDataGrid2_ItemCommand" >
                <Columns>
                <asp:EditCommandColumn CausesValidation="False" EditText="Edit" UpdateText="Update" CancelText="Cancel"></asp:EditCommandColumn>
                     <asp:TemplateColumn HeaderText="Customer Card No."  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "CustomerCardNo")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="txtCustomerCardNo" Width="90" MaxLength="17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CustomerCardNo") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addCustomerCardNo" Width="90"  Runat="Server" MaxLength="17"/>
                                <asp:RequiredFieldValidator id="RequiredFieldValidatoraddCustomerCardNo" 
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addCustomerCardNo"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="City Account No."  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "BankAccNo")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="txtAccountNo" Width="90" MaxLength="17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BankAccNo") %>'></asp:TextBox>
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
                            <%#DataBinder.Eval(Container.DataItem, "OtherBankAccNo")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="txtOtherBankAcNo" MaxLength="17" Width="90" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OtherBankAccNo") %>'></asp:TextBox>
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
                    <asp:TemplateColumn HeaderText="Receiver Account Name"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "OtherBankAccName")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="txtAccountName" Width="90" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OtherBankAccName") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="addAccountName" Width="90"  Runat="Server" MaxLength="9"/>
                                <asp:RequiredFieldValidator id="RequiredFieldValidatoraddAccountName" 
                                CssClass="NormalRed" runat="server" 
                                ControlToValidate="addAccountName"
                                ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                          </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="RoutingNo"  ItemStyle-HorizontalAlign="Left" 
                                   FooterStyle-CssClass="red" >
                         <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "RoutingNo")%>
                         </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="txtRoutingNumber" Width="90" MaxLength="9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RoutingNo") %>'></asp:TextBox>
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
                    <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:LinkButton CommandName="Delete" Text="Delete" ID="btnDel" ForeColor="Blue"  Runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to Delete The Customer Card Info?')"></asp:LinkButton>
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

        <div>
            <asp:Label runat="server" ID="lblTestCookie"></asp:Label>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
