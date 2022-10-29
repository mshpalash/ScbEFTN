<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerEmailChecker.aspx.cs" Inherits="EFTN.CustomerEmailChecker" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/AdminChecker.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Checker Page</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script type="text/javascript">

        function MM_swapImgRestore() { //v3.0
            var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
        }
        function MM_preloadImages() { //v3.0
            var d = document; if (d.images) {
                if (!d.MM_p) d.MM_p = new Array();
                var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                    if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; } 
            }
        }

        function MM_findObj(n, d) { //v4.01
            var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
                d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
            }
            if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
            for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
            if (!x && d.getElementById) x = d.getElementById(n); return x;
        }

        function MM_swapImage() { //v3.0
            var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
                if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
        }

    </script>


</head>
<body class="wrap" id="content" onload="MM_preloadImages('images/UserManagementOn.gif','images/BranchManOn.gif','images/ChangePassOn.gif','images/ExitOn.gif')">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Customar List<br />
                <table>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbxAll" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAll_CheckedChanged"/>                                    
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                            <asp:Button ID="btnActive" runat="server" Text="ACTIVE" OnClick="btnActive_Click" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                            <asp:Button ID="btnInactive" runat="server" Text="INACTIVE" OnClick="btnInactive_Click" />                        
                        </td>
                    </tr>
                </table>
                <div id="divCustemailchecker" style="overflow:scroll; height:350px; width:800px" >
                <asp:DataGrid ID="Custemailchecker" HeaderStyle-CssClass="GrayBackWhiteFont" FooterStyle-CssClass="GrayBackWhiteFont"
                            ItemStyle-CssClass="NormalSmall" ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#FFFFFF"
                            runat="server" CellSpacing="1" CellPadding="5" 
                            AutoGenerateColumns="false" GridLines="None" BorderWidth="0px"  ShowFooter="true" Height="23px" Width="483px" 
                            DataKeyField="CustomerID" AllowPaging="True" 
                            onpageindexchanged="Custemailchecker_PageIndexChanged">
                    <Columns>
                        <asp:TemplateColumn HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbxCheck" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Account Number">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "ACCNo")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="ACCNo0" runat="server" 
                                            Text='<%#DataBinder.Eval(Container.DataItem,"ACCNo") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorACCNo0" CssClass="NormalRed"
                                            runat="server" ControlToValidate="ACCNo" ErrorMessage="*" 
                                            Display="dynamic">
                                        </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Account Holder Name">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "ACCHolderName")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="addAccountHolderName0" Width="85" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ACCHolderName") %>'
                                            MaxLength="9"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="CusEmail1">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CusEmail1")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="addCusEmail6" Width="65" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CusEmail1") %>'
                                            MaxLength="9"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="CusEmail2">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CusEmail2")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="addCusEmail7" Width="65" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CusEmail2") %>'
                                            MaxLength="9"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="CusEmail3">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CusEmail3")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="addCusEmail8" Width="65" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CusEmail3") %>'
                                            MaxLength="9"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="CusEmail4">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CusEmail4")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="addCusEmail9" Width="65" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CusEmail4") %>'
                                            MaxLength="9"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="CusEmail5">

                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CusEmail5")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="addCusEmail10" Width="65" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CusEmail5") %>'
                                            MaxLength="9"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ContactNumber1">

                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "ContactNumber1")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="addContactNumber3" Width="65" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ContactNumber1") %>'
                                            MaxLength="9"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ContactNumber2">

                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "ContactNumber2")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="addContactNumber4" Width="65" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ContactNumber2") %>'
                                            MaxLength="9"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="UptoEbbs">

                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "UptoEbbs")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="addUptoEbbs0" Width="65" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UptoEbbs") %>'
                                            MaxLength="9"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="SusAccNo">

                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "SusAccNo")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="addSusAccNo0" Width="65" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SusAccNo") %>'
                                            MaxLength="9"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="CusAdvice">

                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CusAdvice")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="addCusAdvice0" Width="65" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CusAdvice") %>'
                                            MaxLength="9"></asp:TextBox>
                            </EditItemTemplate>
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