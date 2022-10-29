<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DishonorSentVoucher.aspx.cs"
    Inherits="EFTN.DishonorSentVoucher" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">
        javascript: window.history.forward(1);

        function makeUppercase(myControl, evt) {
            document.getElementById(myControl).value = document.getElementById(myControl).value.toUpperCase();
        }
    </script>
    <script type="text/javascript">
        javascript: window.history.forward(1);

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
            <uc1:Header ID="Header" runat="server" />
            <div class="Head" align="center">
                Dishonor Sent Voucher
            </div>
            <div>
                <table style="padding-top: 15px">
                    <tr height="20px">
                        <td width="30px"></td>
                        <td>
                            <a href="EFTChecker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/EFTCheckerOn.gif',1)">
                                <img src="images/EFTChecker.gif" name="Image4" width="149" height="25" border="0" id="Image4" /></a>

                        </td>
                        <td width="100px"></td>
                        <td>
                            <a href="EFTCheckerEBBS.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/EFTCheckerEBBSOn.gif',1)">
                                <img src="images/EFTCheckerEBBS.gif" name="Image1" width="149" height="25" border="0" id="Image1" /></a>

                        </td>
                        <td width="100px"></td>
                        <td>
                            <a href="EFTCheckerAuthorizer.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/EFTCheckerAuthorizerOn.gif',1)">
                                <img src="images/EFTCheckerAuthorizer.gif" name="Image2" width="149" height="25" border="0" id="Image2" /></a>

                        </td>
                        <td width="100px"></td>
                        <td>
                            <a href="EFTCheckerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ReportOn.gif',1)">
                                <img src="images/Report.gif" name="Image3" width="149" height="25" border="0" id="Image3" /></a>

                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px; height: 90px; margin-left: 15px">
                    <table>
                        <tr>
                            <td class="NormalBold">
                                <asp:Label ID="lblDay" runat="server" Text="Effective Entry Begin Date"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td class="NormalBold">
                                            <asp:Label ID="lblDate" runat="server" Text="Day"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistDay" runat="server">
                                                <asp:ListItem>01</asp:ListItem>
                                                <asp:ListItem>02</asp:ListItem>
                                                <asp:ListItem>03</asp:ListItem>
                                                <asp:ListItem>04</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>06</asp:ListItem>
                                                <asp:ListItem>07</asp:ListItem>
                                                <asp:ListItem>08</asp:ListItem>
                                                <asp:ListItem>09</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                <asp:ListItem>24</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>26</asp:ListItem>
                                                <asp:ListItem>27</asp:ListItem>
                                                <asp:ListItem>28</asp:ListItem>
                                                <asp:ListItem>29</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>31</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="NormalBold">
                                            <asp:Label ID="Label1" runat="server" Text="Month"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistMonth" runat="server">
                                                <asp:ListItem Value="1">Jan</asp:ListItem>
                                                <asp:ListItem Value="2">Feb</asp:ListItem>
                                                <asp:ListItem Value="3">Mar</asp:ListItem>
                                                <asp:ListItem Value="4">Apr</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">Jun</asp:ListItem>
                                                <asp:ListItem Value="7">Jul</asp:ListItem>
                                                <asp:ListItem Value="8">Aug</asp:ListItem>
                                                <asp:ListItem Value="9">Sep</asp:ListItem>
                                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                                <asp:ListItem Value="12">Dec</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="NormalBold">
                                            <asp:Label ID="Label46" runat="server" Text="Year"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistYear" runat="server">
                                                <asp:ListItem>2011</asp:ListItem>
                                                <asp:ListItem>2012</asp:ListItem>
                                                <asp:ListItem>2013</asp:ListItem>
                                                <asp:ListItem>2014</asp:ListItem>
                                                <asp:ListItem>2015</asp:ListItem>
                                                <asp:ListItem>2016</asp:ListItem>
                                                <asp:ListItem>2017</asp:ListItem>
                                                <asp:ListItem>2018</asp:ListItem>
                                                <asp:ListItem>2019</asp:ListItem>
                                                <asp:ListItem>2020</asp:ListItem>
                                                <asp:ListItem>2021</asp:ListItem>
                                                <asp:ListItem>2023</asp:ListItem>
                                                <asp:ListItem>2024</asp:ListItem>
                                                <asp:ListItem>2025</asp:ListItem>
                                                <asp:ListItem>2026</asp:ListItem>
                                                <asp:ListItem>2027</asp:ListItem>
                                                <asp:ListItem>2028</asp:ListItem>
                                                <asp:ListItem>2029</asp:ListItem>
                                                <asp:ListItem>2030</asp:ListItem>
                                                <asp:ListItem>2031</asp:ListItem>
                                                <asp:ListItem>2032</asp:ListItem>
                                                <asp:ListItem>2033</asp:ListItem>
                                                <asp:ListItem>2034</asp:ListItem>
                                                <asp:ListItem>2035</asp:ListItem>
                                                <asp:ListItem>2036</asp:ListItem>
                                                <asp:ListItem>2037</asp:ListItem>
                                                <asp:ListItem>2038</asp:ListItem>
                                                <asp:ListItem>2039</asp:ListItem>
                                                <asp:ListItem>2040</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEndDate" runat="server" Text="Effective Entry End Date"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td class="NormalBold">
                                            <asp:Label ID="Label2" runat="server" Text="Day"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistDayEnd" runat="server">
                                                <asp:ListItem>01</asp:ListItem>
                                                <asp:ListItem>02</asp:ListItem>
                                                <asp:ListItem>03</asp:ListItem>
                                                <asp:ListItem>04</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>06</asp:ListItem>
                                                <asp:ListItem>07</asp:ListItem>
                                                <asp:ListItem>08</asp:ListItem>
                                                <asp:ListItem>09</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                <asp:ListItem>24</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>26</asp:ListItem>
                                                <asp:ListItem>27</asp:ListItem>
                                                <asp:ListItem>28</asp:ListItem>
                                                <asp:ListItem>29</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>31</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="NormalBold">
                                            <asp:Label ID="Label6" runat="server" Text="Month"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistMonthEnd" runat="server">
                                                <asp:ListItem Value="1">Jan</asp:ListItem>
                                                <asp:ListItem Value="2">Feb</asp:ListItem>
                                                <asp:ListItem Value="3">Mar</asp:ListItem>
                                                <asp:ListItem Value="4">Apr</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">Jun</asp:ListItem>
                                                <asp:ListItem Value="7">Jul</asp:ListItem>
                                                <asp:ListItem Value="8">Aug</asp:ListItem>
                                                <asp:ListItem Value="9">Sep</asp:ListItem>
                                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                                <asp:ListItem Value="12">Dec</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="NormalBold">
                                            <asp:Label ID="Label7" runat="server" Text="Year"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistYearEnd" runat="server">
                                                <asp:ListItem>2011</asp:ListItem>
                                                <asp:ListItem>2012</asp:ListItem>
                                                <asp:ListItem>2013</asp:ListItem>
                                                <asp:ListItem>2014</asp:ListItem>
                                                <asp:ListItem>2015</asp:ListItem>
                                                <asp:ListItem>2016</asp:ListItem>
                                                <asp:ListItem>2017</asp:ListItem>
                                                <asp:ListItem>2018</asp:ListItem>
                                                <asp:ListItem>2019</asp:ListItem>
                                                <asp:ListItem>2020</asp:ListItem>
                                                <asp:ListItem>2021</asp:ListItem>
                                                <asp:ListItem>2023</asp:ListItem>
                                                <asp:ListItem>2024</asp:ListItem>
                                                <asp:ListItem>2025</asp:ListItem>
                                                <asp:ListItem>2026</asp:ListItem>
                                                <asp:ListItem>2027</asp:ListItem>
                                                <asp:ListItem>2028</asp:ListItem>
                                                <asp:ListItem>2029</asp:ListItem>
                                                <asp:ListItem>2030</asp:ListItem>
                                                <asp:ListItem>2031</asp:ListItem>
                                                <asp:ListItem>2032</asp:ListItem>
                                                <asp:ListItem>2033</asp:ListItem>
                                                <asp:ListItem>2034</asp:ListItem>
                                                <asp:ListItem>2035</asp:ListItem>
                                                <asp:ListItem>2036</asp:ListItem>
                                                <asp:ListItem>2037</asp:ListItem>
                                                <asp:ListItem>2038</asp:ListItem>
                                                <asp:ListItem>2039</asp:ListItem>
                                                <asp:ListItem>2040</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td colspan="4"></td>
                            <td>
                                <asp:DropDownList ID="ddListTransactionType" runat="server">
                                    <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                    <asp:ListItem Value="Debit">Debit</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                            </td>

                            <td width="15px"></td>
                            <td>
                                <asp:LinkButton ID="PrintVoucherBtn" runat="server" CssClass="CommandButton" Text="Print Vouchers" OnClick="PrintVoucherBtn_Click"></asp:LinkButton>
                            </td>
                            <td width="15px"></td>
                            <td>
                                <asp:Button ID="btnExpotToCSV" runat="server" Text="CSV File" OnClick="btnExpotToCSV_Click" />
                            </td>
                        </tr>
                    </table>

                    <table>
                        <tr align="left">
                            <td>
                                <asp:CheckBox ID="cbxSelectAll" runat="server" CssClass="NormalBold" Text="Select All" AutoPostBack="true" OnCheckedChanged="cbxSelectAll_CheckedChanged" />
                            </td>
                            <td width="20px"></td>
                            <td>
                                <asp:Label ID="lblTotalItem" runat="server" CssClass="NormalBold">
                                </asp:Label>
                            </td>
                            <td width="50px"></td>
                            <td>
                                <asp:Label ID="lblTotalAmount" runat="server" CssClass="NormalBold">
                                </asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div align="center" id="AvailDiv" runat="server" style="position: relative; overflow: auto; width: 900px; margin-top: 15px; height: 300px;">
                    <asp:DataGrid ID="dtgDishonorSentChecker" AlternatingItemStyle-BackColor="lightyellow"
                        AutoGenerateColumns="false" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                        FooterStyle-CssClass="GrayBackWhiteFont" GridLines="None" HeaderStyle-CssClass="GrayBackWhiteFont"
                        Height="0px" ItemStyle-BackColor="#CAD2FD" ItemStyle-CssClass="Normal" runat="server"
                        DataKeyField="DishonoredID" Width="980px" AllowPaging="True" OnPageIndexChanged="dtgDishonorSentChecker_PageIndexChanged" PageSize="50">
                        <Columns>
                            <asp:TemplateColumn HeaderText="SL.">
                                <ItemTemplate>
                                    <%#(dtgDishonorSentChecker.PageSize * dtgDishonorSentChecker.CurrentPageIndex) + Container.ItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBEFTNList" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DishonorReason">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "DishonorReason")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="BankName">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "BankName")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="A/C No.From BEFTN">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="A/C No. From CBS">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ACCOUNT")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Currency">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Currency")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Amount">
                                <ItemTemplate>
                                    <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ReceiverName">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ReceiverName as of CBS">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "TITLE")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="STATUS">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "STATUS")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="RISKS">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "RISKS")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="OrgTraceNumber">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "OrgTraceNumber")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TraceNumber">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "TraceNumber")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Return Reason">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "RejectReason")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="AddendaInfo">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "AddendaInfo")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DateOfDeath">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "DateOfDeath")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="IdNumber">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "IdNumber")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ReceiverName">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Amount">
                                <ItemTemplate>
                                    <%#string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Amount"))%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="OrgSettlementJDate">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "OrgSettlementJDate")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="SettlementJDate">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "SettlementJDate")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Print Status">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "PrintFlag")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle CssClass="GrayBackWhiteFont" />
                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Position="TopAndBottom" />
                        <AlternatingItemStyle BackColor="LightYellow" />
                        <ItemStyle BackColor="#CAD2FD" CssClass="Normal" />
                        <HeaderStyle CssClass="GrayBackWhiteFont" />
                    </asp:DataGrid>
                </div>
                <div align="center" class="boxmodule" style="padding-top: 10px; width: 940px; margin-top: 10px; height: 50px; margin-left: 15px; margin-bottom: 20px; padding-left: 20px">
                    <table>
                        <tr>
                            <td width="50px">
                                <asp:LinkButton ID="btnAccept" runat="server" Text="Accept" CssClass="CommandButton"
                                    OnClientClick="return confirm('Are you sure you want to accept?')" OnClick="btnAccept_Click"></asp:LinkButton>
                            </td>
                            <td width="100px">
                                <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="CommandButton"
                                    OnClientClick="return confirm('Are you sure you want to reject?')" OnClick="btnReject_Click"></asp:LinkButton>
                            </td>
                            <td width="300px">
                                <asp:Label ID="lblRejectInstruction" runat="server" CssClass="NormalBold" Text="For a rejection item(s) please give a reason"></asp:Label></td>
                            <td width="300px">
                                <asp:TextBox ID="txtRejectedReason" TextMode="MultiLine" runat="server" Width="300" MaxLength="50" OnKeyUp="return makeUppercase(this.name);"></asp:TextBox></td>
                            <td style="color: Red; font-weight: bold;">
                                <asp:Label ID="lblNoReturnReason" runat="server" ForeColor="red" Text="Please Enter a return reason"
                                    Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>

                <div style="clear: both"></div>
                <div style="padding-top: 20px; padding-left: 220px">
                    <table>
                        <tr height="20px">
                            <td width="50px"></td>

                            <td>
                                <a href="ChangeCheckerPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image5','','images/ChangePasswordOn.gif',1)">
                                    <img src="images/ChangePassword.gif" name="Image5" width="149" height="25" border="0" id="Image5" /></a>

                            </td>
                            <td width="100px"></td>
                            <td>
                                <a href="LogOut.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image6','','images/SignOutOn.gif',1)">
                                    <img src="images/SignOut.gif" name="Image6" width="149" height="25" border="0" id="Image6" /></a>

                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
