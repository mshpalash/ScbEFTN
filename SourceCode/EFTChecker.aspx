<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EFTChecker.aspx.cs" Inherits="EFTN.EFTChecker" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Checker Page</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
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
            var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2) ; i += 3)
                if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
        }
    </script>
</head>
<body class="wrap" id="content" onload="MM_preloadImages('images/EFTCheckerOn.gif','images/EFTCheckerEBBSOn.gif','images/EFTCheckerAuthorizerOn.gif','images/ReportOn.gif','images/ChangePasswordOn.gif','images/SignOutOn.gif')">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Checker Page
            </div>
            <div>
                <table style="padding-top: 15px">
                    <tr height="20px">
                        <td width="30px"></td>
                        <td>
                            <a href="EFTChecker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/EFTCheckerOn.gif',1)">
                                <img src="images/EFTChecker.gif" name="Image4" width="149" height="25" border="0" id="Image4" /></a>

                        </td>
                        <td width="90px"></td>
                        <td>
                            <a href="EFTCheckerEBBS.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/EFTCheckerEBBSOn.gif',1)">
                                <img src="images/EFTCheckerEBBS.gif" name="Image1" width="149" height="25" border="0" id="Image1" /></a>

                        </td>
                        <td width="90px"></td>
                        <td>
                            <a href="EFTCheckerAuthorizer.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/EFTCheckerAuthorizerOn.gif',1)">
                                <img src="images/EFTCheckerAuthorizer.gif" name="Image2" width="149" height="25" border="0" id="Image2" /></a>

                        </td>
                        <td width="90px"></td>
                        <td>
                            <a href="EFTCheckerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ReportOn.gif',1)">
                                <img src="images/Report.gif" name="Image3" width="149" height="25" border="0" id="Image3" /></a>
                        </td>
                        <td width="20px"></td>
                        <td align="right">
                            <a href="BranchMessages.aspx">
                                <img src="images/Reture.jpg" id="ImgMsg" /></a>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <div id="divFCUBSTrMgt" runat="server" align="center">
               <a class="CommandButton" href="CBSRejectedTXNManagement.aspx">FCUBS Transaction Management<asp:Label ID="Label1" runat="server"></asp:Label></a>
                
            </div>
            <div id="divIBankingOperator" runat="server" align="center">
                <asp:LinkButton ID="linkBtnIBankingChecker" runat="server" Text="i-Banking Operator"
                    class="CommandButton" OnClick="linkBtnIBankingMaker_Click">
                </asp:LinkButton>
            </div>
            <div id="divIBankingRejectedTranRpt" runat="server" align="center">
                <asp:LinkButton ID="linkBtnIBankingRejectedTranRptMaker" runat="server" Text="i-Banking Rejected Transaction Report"
                    class="CommandButton" OnClick="linkBtnIBankingRejectedTranRptMaker_Click">
                </asp:LinkButton>
            </div>
            <div id="divIBankingCBSRpt" runat="server" align="center">
                <asp:LinkButton ID="linkBtnIBankingCBSRptMaker" runat="server" Text="i-Banking CBS Status Report"
                    class="CommandButton" OnClick="linkBtnIBankingCBSRptMaker_Click">
                </asp:LinkButton>
            </div>
            <div id="divStdOBatch" runat="server" align="center">
                <a class="CommandButton" href="StandingOrderBatchListChecker.aspx">Standing Order Batch List</a>
            </div>
            <div id="divBulkReturn" runat="server" align="center">
                <asp:LinkButton ID="linkBtnBulkReturn" runat="server" class="CommandButton"
                    Text="Process Outward Debit Return" OnClick="linkBtnBulkReturn_Click"></asp:LinkButton>
            </div>
            <div>

                <div class="boxmodule" style="float: left; padding-top: 10px; padding-bottom: 10px; height: 250px;">
                    <ul>
                        <li><a class="CommandButton" href="EFTCheckerTransactionSent.aspx">Transaction Sent<asp:Label ID="lblCountOutwardTransSent" runat="server"></asp:Label></a></li>
                        <li><a class="CommandButton" href="EFTCheckerTransactionBatch.aspx">Transaction Batch Wise<asp:Label ID="lblCountOutwardBatchSent" runat="server"></asp:Label></a></li>
                        <li><a class="CommandButton" href="InwardTransactionReturnChecker.aspx">Return Sent<asp:Label ID="lblCountOutwardReturnSent" runat="server"></asp:Label></a></li>
                        <li><a class="CommandButton" href="EFTDishonorSentChecker.aspx">Dishonour Sent<asp:Label ID="lblCountOutwardDishonorSent" runat="server"></asp:Label></a></li>
                        <li><a class="CommandButton" href="InwardTransactionNOCChecker.aspx">NOC Sent<asp:Label ID="lblCountOutwardNOCSent" runat="server"></asp:Label></a></li>
                        <li><a class="CommandButton" href="RNOCofNOCChecker.aspx">RNOC Sent<asp:Label ID="lblCountOutwardRNOCSent" runat="server"></asp:Label></a></li>
                        <li><a class="CommandButton" href="ContestedDishonor.aspx">Contested Dishonor<asp:Label ID="lblCountOutwardContested" runat="server"></asp:Label></a></li>
                        <li><a class="CommandButton" href="RegenerateOutwardTransactionChecker.aspx">Regenerate OutwardTransaction<asp:Label ID="lblRegenerate" runat="server"></asp:Label></a></li>
                    </ul>

                </div>
                <div class="boxmodule" style="float: left; padding-top: 10px; padding-bottom: 10px; height: 250px;">
                    <ul>
                        <li><a class="CommandButton" href="InwardTransactionApprovedChecker.aspx">Approved Inward Transaction<asp:Label ID="lblCountInwardTransactionApproved" runat="server"></asp:Label></a></li>
                        <li><a class="CommandButton" href="InwardReturnApprovedByMaker.aspx">Approved Inward Return<asp:Label ID="lblCountInwardReturnApproved" runat="server"></asp:Label></a></li>
                        <li><a class="CommandButton" href="ApprovedNOCChekcer.aspx">Approved Received NOC<asp:Label ID="lblCountApprovedReceivedNOC" runat="server"></asp:Label></a></li>
                        <li><a class="CommandButton" href="ApprovedDishonor.aspx">Approved Dishonor<asp:Label ID="lblCountApprovedDishonor" runat="server"></asp:Label></a></li>
                        <li><a class="CommandButton" href="InwardSearchForChecker.aspx">Inward Search</a></li>
                        <li><a class="CommandButton" href="OutwardSearchForChecker.aspx">Outward Search</a></li>
                        <li><a class="CommandButton" href="FlatFileUpdloadForTransactionSent.aspx">Flat File Upload</a></li>
                        <li><a class="CommandButton" href="ExcelUploadForCBSMissMatchCheck.aspx">Upload Excel for CBS Mismatch</a></li>
                        <li><a class="CommandButton" href="DDIAccountMgtChecker.aspx">DDI Account Management</a></li>
                    </ul>

                </div>
            </div>
            <div style="clear: both"></div>
            <div style="padding-top: 20px; padding-left: 220px">
                <table>
                    <tr height="20px">
                        <td width="50px"></td>

                        <td>
                            <%--<a href="ChangeCheckerPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image5','','images/ChangePasswordOn.gif',1)">
                                <img src="images/ChangePassword.gif" name="Image5" width="149" height="25" border="0" id="Image5" /></a>--%>

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
        <uc2:footer ID="Footer1" runat="server" />

    </form>

</body>
</html>
