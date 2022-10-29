<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerEmail.aspx.cs" Inherits="EFTN.CustomerEmail" %>
<%@ Register Src="Modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customar Email</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]--><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 302px;
            margin-top: 0px;
        }
        .style2
        {
            width: 158px;
        }
    </style>
</head>
<body class="wrap" id="content" onload="MM_preloadImages('images/UserManagementOn.gif','images/BranchManOn.gif','images/ChangePassOn.gif','images/ExitOn.gif')">
    <form id="form1" runat="server">
        <div class="maincontent" style="height: 950px">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Customar Email
            </div>
            <div class="boxmodule" style="width: 820px; margin-top: 20px; height: 750px">
                <table class="style1">
                    <tr>
                        <td class="style2">Account Number:</td>
                        <td>
                            <asp:TextBox ID="txtaccno" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server"
                                ControlToValidate="txtaccno"
                                ErrorMessage="Account Number is a required field."
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Account Holder Name:</td>
                        <td>
                            <asp:TextBox ID="txtacholdname" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtaccno"
                                ErrorMessage="Account Holder Name is a required field."
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Email 1:</td>
                        <td>
                            <asp:TextBox ID="txtem1" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtem1" ErrorMessage="Invalid Email Address"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtaccno"
                                ErrorMessage="Email 1 is a required field."
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Email 2:</td>
                        <td>
                            <asp:TextBox ID="txtem2" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtem2" ErrorMessage="Invalid Email Address"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Email 3:</td>
                        <td>
                            <asp:TextBox ID="txtem3" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtem3" ErrorMessage="Invalid Email Address"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Email 4:</td>
                        <td>
                            <asp:TextBox ID="txtem4" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtem4" ErrorMessage="Invalid Email Address"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Email 5:</td>
                        <td>
                            <asp:TextBox ID="txtem5" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtem5" ErrorMessage="Invalid Email Address"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">ContactNumber 1:</td>
                        <td>
                            <asp:TextBox ID="txtconno1" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                ControlToValidate="txtaccno"
                                ErrorMessage="ContactNumber 1 is a required field."
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">ContactNumber 2:</td>
                        <td>
                            <asp:TextBox ID="txtconno2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">SusAccNo:</td>
                        <td>
                            <asp:TextBox ID="txtSusAccNo" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ControlToValidate="txtaccno"
                                ErrorMessage="SusAccNo is a required field."
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">CusAdvice:</td>
                        <td>
                            <asp:DropDownList ID="ddListCusAdvice" runat="server">
                                <asp:ListItem Text="Only MIS" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Only Advice" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Advice and MIS" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Generation Time
                        </td>
                        <td width="600px">
                            <asp:CheckBoxList ID="chkBoxList1" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="10:00" Value="1000"></asp:ListItem>
                                <asp:ListItem Text="10:30" Value="1030"></asp:ListItem>
                                <asp:ListItem Text="11:00" Value="1100"></asp:ListItem>
                                <asp:ListItem Text="11:30" Value="1130"></asp:ListItem>
                                <asp:ListItem Text="12:00" Value="1200"></asp:ListItem>
                                <asp:ListItem Text="12:30" Value="1230"></asp:ListItem>
                                <asp:ListItem Text="13:00" Value="1300"></asp:ListItem>
                                <asp:ListItem Text="13:30" Value="1330"></asp:ListItem>
                                <asp:ListItem Text="14:00" Value="1400"></asp:ListItem>
                                <asp:ListItem Text="14:30" Value="1430"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">(MIS will be generated at the selected times)
                        </td>
                        <td width="600px">
                            <asp:CheckBoxList ID="chkBoxList2" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="15:00" Value="1500"></asp:ListItem>
                                <asp:ListItem Text="15:30" Value="1530"></asp:ListItem>
                                <asp:ListItem Text="16:00" Value="1600"></asp:ListItem>
                                <asp:ListItem Text="16:30" Value="1630"></asp:ListItem>
                                <asp:ListItem Text="17:00" Value="1700"></asp:ListItem>
                                <asp:ListItem Text="17:30" Value="1730"></asp:ListItem>
                                <asp:ListItem Text="18:00" Value="1800"></asp:ListItem>
                                <asp:ListItem Text="18:30" Value="1830"></asp:ListItem>
                                <asp:ListItem Text="19:00" Value="1900"></asp:ListItem>
                                <asp:ListItem Text="20:00" Value="2000"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit"
                                OnClick="btnsubmit_Click" />
                            <br />
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>

                <div id="diustEmail" style="overflow: scroll; height: 300px; width: 800px">
                    <asp:DataGrid ID="Custemail" HeaderStyle-CssClass="FixedHeader"
                        FooterStyle-CssClass="GrayBackWhiteFont" overflow="auto"
                        ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#FFFFFF"
                        runat="server" CellSpacing="1" CellPadding="5"
                        AutoGenerateColumns="false"
                        GridLines="None" BorderWidth="0px" ShowFooter="true" Height="23px"
                        Width="815px"
                        DataKeyField="CustomerID" AllowPaging="True" PageSize="5"
                        OnPageIndexChanged="Custemail_PageIndexChanged">
                        <Columns>
                            <asp:TemplateColumn >
                                <ItemTemplate>
                                    <a href='CustomerEmailEdit.aspx?CustomerID=<%#DataBinder.Eval(Container.DataItem, "CustomerID")%>'>Edit</a>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Account Number">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ACCNo")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Account Holder Name">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ACCHolderName")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CusEmail1">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CusEmail1")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CusEmail2">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CusEmail2")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CusEmail3">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CusEmail3")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CusEmail4">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CusEmail4")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CusEmail5">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CusEmail5")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ContactNumber1">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ContactNumber1")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ContactNumber2">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ContactNumber2")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="UptoEbbs">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "UptoEbbs")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SusAccNo">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "SusAccNo")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CusAdvice">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CusAdvice")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Status">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ActiveStatus")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="MISGenerationTime">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MISGenerationTime")%>
                                </ItemTemplate>
                                <FooterStyle CssClass="red" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn></asp:TemplateColumn>
                        </Columns>
                        <FooterStyle CssClass="GrayBackWhiteFont" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#DEE9FC" CssClass="NormalSmall" />
                        <HeaderStyle CssClass="GrayBackWhiteFont" />
                        <PagerStyle Mode="NumericPages" />
                    </asp:DataGrid>
                </div>
            </div>

        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>

