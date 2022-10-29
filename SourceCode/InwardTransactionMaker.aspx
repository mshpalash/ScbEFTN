<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InwardTransactionMaker.aspx.cs"
    Inherits="EFTN.InwardTransactionMaker" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inward Transaction</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script language="javascript" type="text/javascript">
        javascript: window.history.forward(1);

        function makeUppercase(myControl, evt) {
            document.getElementById(myControl).value = document.getElementById(myControl).value.toUpperCase();
        }

        function onlyNumbers(evt) {
            var e = event || evt;
            var charCode = e.which || e.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }


        function onlyAlphaNumeric(evt) {
            var e = event || evt;
            var charCode = e.which || e.keyCode;
            //if (charCode > 31 && (charCode < 48 || charCode > 57))
            if ((charCode >= 48 && charCode <= 57)
                || (charCode >= 65 && charCode <= 90)
                || (charCode >= 97 && charCode <= 122)
                || charCode == 32)
                return true;
            return false;
        }

        function onlySearchParam(evt) {
            var e = event || evt;
            var charCode = e.which || e.keyCode;
            //if (charCode > 31 && (charCode < 48 || charCode > 57))
            if ((charCode >= 45 && charCode <= 58)
                || (charCode >= 65 && charCode <= 90)
                || (charCode >= 97 && charCode <= 122)
                || charCode == 32
                || charCode == 35
                || charCode == 38
                || charCode == 40
                || charCode == 41)
                return true;
            return false;
        }

        function DateOfDeathChange() {

            if ((document.getElementById('ddlReturnChangeCode').value == 'R14') || (document.getElementById('ddlReturnChangeCode').value == 'R15')) {

                //        var testvar = confirm('Are you sure you want to save?');
                //        alert(testvar)
                var dateOfDeath = document.getElementById('txtDateOfDeath').value;

                if (dateOfDeath.length < 6) {
                    alert('Insert Date Of Death');
                    document.getElementById('txtDateOfDeath').focus();
                    return false;
                }
                else {
                    var month = dateOfDeath.substring(2, 4);
                    var day = dateOfDeath.substring(4, 6);

                    if ((month > 12) || (month < 1) || (day > 31) || (day < 1)) {
                        alert('Incorrect date format');
                        document.getElementById('txtDateOfDeath').focus();
                        return false;
                    }
                    return confirm('Are you sure you want to save?');
                }

            } else {
                return confirm('Are you sure you want to save?');
            }
        }


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
            var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2) ; i += 3)
                if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
        }
    </script>
</head>
<body class="wrap" id="content">



    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Inward Transaction
            </div>
            <div>
                <table style="padding-top: 15px;">
                    <tr height="20px">
                        <td width="200px"></td>
                        <td>
                            <a href="EFTMaker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/EFTMakerHomeOn.gif',1)">
                                <img src="images/EFTMakerHome.gif" name="Image1" width="149" height="25" border="0"
                                    id="Image1" /></a>
                        </td>
                        <td width="250px"></td>
                        <td>
                            <a href="EFTMakerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/ReportOn.gif',1)">
                                <img src="images/Report.gif" name="Image2" width="149" height="25" border="0" id="Image2" /></a>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="left" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px; height: 120px; margin-left: 15px; overflow-y: scroll;">
                <table>
                    <tr>
                        <td width="10"></td>
                        <td>
                            <asp:DropDownList ID="BankList" DataTextField="BankName" DataValueField="BankCode"
                                AutoPostBack="true" runat="Server" OnSelectedIndexChanged="BankList_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddListTransactionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddListTransactionType_SelectedIndexChanged">
                                <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                <asp:ListItem Value="Debit">Debit</asp:ListItem>
                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>                      
                        <td>
                            <asp:CheckBox ID="cbxAll" runat="server" Text="Select All" CssClass="NormalBold"
                                AutoPostBack="true" OnCheckedChanged="cbxAll_CheckedChanged" />
                        </td>
                        <td width="15"></td>
                        <td>
                            <asp:Label ID="lblTotalItem" runat="server" CssClass="NormalBold">
                            </asp:Label>
                        </td>
                        <td width="15px"></td>
                        <td>
                            <asp:Label ID="lblTotalAmount" runat="server" CssClass="NormalBold">
                            </asp:Label>
                        </td>                    

                    </tr>
                    <tr>
                        <td width="10"></td>
                        <td>
                            <asp:TextBox ID="txtSearchParam" runat="server" OnKeyPress="return onlySearchParam(this.name);"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                        <td width="15px"></td>
                        <td>
                            <asp:Button ID="btnExpotToCSV" runat="server" Text="CSV File" OnClick="btnExpotToCSV_Click" />
                        </td>
                        <td width="15px"></td>
                        <td>
                            <asp:Button ID="ExpotToPdfBtn" runat="server" Text="PDF" OnClick="ExpotToPdfBtn_Click" />
                        </td>
                    </tr>
                </table>

                <table width="900">
                    <tr>
                        <td width="10"></td>
                        <td class="NormalBold">Select Risk:
                            <asp:CheckBoxList ID="cbxList" runat="server" RepeatColumns="16" RepeatDirection="Vertical" CssClass="NormalBold" OnSelectedIndexChanged="cbxList_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divSearchStatus" runat="server" align="left" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px; height: 50px; margin-left: 15px">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblStatusSearch" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchStatusParam" runat="server" OnKeyPress="return onlySearchParam(this.name);"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:Label ID="lblRiskSearch" runat="server" Text="Risk"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchRiskParam" runat="server" OnKeyPress="return onlySearchParam(this.name);"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSearchStatusAndRisk" runat="server" Text="Search"
                                OnClick="btnSearchStatusAndRisk_Click" />
                        </td>
                        <td class="NormalBold">
                            <asp:Label ID="lblCurrency" runat="server" Text="Currency"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="CurrencyDdList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CurrencyDdList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="NormalBold">
                            <asp:Label ID="lblSession" runat="server" Text="Session"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="SessionDdList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SessionDdList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <table>
                <tr>
                    <td width="15px"></td>
                    <td>
                        <asp:Button ID="RemoveInvalidCharacterFromAccNo" runat="server" Text="Filter Account Number" OnClick="RemoveInvalidCharacterFromAccNo_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSynchronizeCBSAccountInfo" runat="server"
                            Text="Get CBS Account Info" OnClick="btnSynchronizeCBSAccountInfo_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <div style="overflow: scroll; height: 350px">
                <table>
                    <tr>
                        <td width="10px"></td>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="3">
                                        <asp:DataGrid ID="dtgInwardTransactionMaker" AlternatingItemStyle-BackColor="lightyellow"
                                            AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                            FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                                            Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                                            DataKeyField="EDRID" Width="980px" AllowPaging="True" AllowSorting="true"
                                            HeaderStyle-ForeColor="#FFFFFF"
                                            OnPageIndexChanged="dtgInwardTransactionMaker_PageIndexChanged"
                                            PageSize="500" OnSortCommand="dtgInwardTransactionMaker_SortCommand" OnItemCommand="dtgInwardTransactionMaker_ItemCommand">
                                            <Columns>
                                                <asp:EditCommandColumn CausesValidation="False" EditText="Edit"
                                                    UpdateText="Update" CancelText="Cancel"></asp:EditCommandColumn>
                                                <asp:TemplateColumn HeaderText="SL.">
                                                    <ItemTemplate>
                                                        <%#(dtgInwardTransactionMaker.PageSize * dtgInwardTransactionMaker.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbxCheck" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="STATUS" SortExpression="STATUS">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "STATUS")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="RISKS" SortExpression="RISKS">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "RISKS")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="MatchStatus" SortExpression="MatchStatus">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "MatchStatus")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="VACCNO" SortExpression="VACCNO">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "VACCNO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Hold" SortExpression="Hold">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "Hold")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Receiver Name" SortExpression="ReceiverName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ReceiverName as of CBS" SortExpression="TITLE">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "TITLE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="A/C No. For EFT User" SortExpression="DFIAccountNo">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="DFIAccountNo" Width="90" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DFIAccountNo") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="A/C No" SortExpression="DFIAccountNo">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="A/C No. From Original BEFTN" SortExpression="DFIAccountNoAsCBS">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "DFIAccountNoAsCBS")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Settlement JDate" SortExpression="SettlementJDate">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "SettlementJDate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Currency">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "Currency")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Session">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "SessionID")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Amount" SortExpression="Amount">
                                                    <ItemTemplate>
                                                        <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="BankName" SortExpression="BankName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Branch Name" SortExpression="BranchName">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Entry Description" SortExpression="EntryDesc">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "EntryDesc")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                            </Columns>
                                            <FooterStyle CssClass="GrayBackWhiteFont" />
                                            <AlternatingItemStyle BackColor="LightYellow" />
                                            <ItemStyle BackColor="#CAD2FD" CssClass="Normal" />
                                            <HeaderStyle CssClass="GrayBackWhiteFont" ForeColor="White" />
                                            <PagerStyle Mode="NumericPages" Position="TopAndBottom" />
                                        </asp:DataGrid>

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td colspan="3" class="NormalBold">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rblTransactionDecision" runat="server" RepeatDirection="Horizontal"
                                CssClass="NormalBold" AutoPostBack="true" OnSelectedIndexChanged="rblTransactionDecision_SelectedIndexChanged">
                            </asp:RadioButtonList>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlReturnChangeCode" runat="server" DataTextField="RejectReason"
                                DataValueField="RejectReasonCode" OnSelectedIndexChanged="ddlReturnChangeCode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalBold" align="right">
                            <asp:Label ID="lblCorrectedDataMsg" runat="server" Visible="false" />
                            <asp:TextBox ID="txtCorrectedData" runat="server" CssClass="NormalBold" Visible="false"
                                OnKeyUp="return makeUppercase(this.name);" OnKeyPress="return onlyAlphaNumeric(this.name);" />
                        </td>
                        <td class="NormalBold" align="right">
                            <asp:Label ID="lblRoutingNumber" runat="server" Text="Routing Number" Visible="false" />
                            <asp:TextBox ID="txtRoutingNumber" runat="server" CssClass="NormalBold" Visible="false"
                                onkeypress="return onlyNumbers();" MaxLength="9" />
                        </td>
                        <td class="NormalBold" align="right">
                            <asp:Label ID="lblTransactionCode" runat="server" Text="Transaction Code" Visible="false" />
                            <asp:TextBox ID="txtTransactionCode" runat="server" CssClass="NormalBold" Visible="false"
                                onkeypress="return onlyNumbers();" MaxLength="2" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDateOfDeath" runat="server" Text="Date Of Death"></asp:Label>
                            <asp:TextBox ID="txtDateOfDeath" runat="server" onkeypress="return onlyNumbers();" MaxLength="6"></asp:TextBox>
                            <asp:Label ID="lblDateOfDeathInstruction" runat="server" Text="YYMMDD"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDateOfDeathSelectedValue" runat="server" Text="Insert Date of Death if you select 'Beneficiary or Account Holder (Other Than a Representative Payee) Deceased'"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="NormalBold"
                                OnClick="btnSave_Click" OnClientClick="return DateOfDeathChange();"></asp:LinkButton>
                        </td>
                    </tr>

                </table>
            </div>
            <div style="clear: both">
            </div>
            <div style="margin-top: 25px">
                <table style="padding-top: 15px">
                    <tr height="20">
                        <td width="200px"></td>
                        <td>
                            <a href="ChangeMakerPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ChangePasswordOn.gif',1)">
                                <img src="images/ChangePassword.gif" name="Image3" width="149" height="25" border="0"
                                    id="Image3" /></a>
                        </td>
                        <td width="250px"></td>
                        <td>
                            <a href="LogOut.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/SignOutOn.gif',1)">
                                <img src="images/SignOut.gif" name="Image4" width="149" height="25" border="0" id="Image4" /></a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />

    </form>
</body>
</html>
