<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EFTMaker.aspx.cs" Inherits="EFTN.EFTMaker" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Maker Page</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
    <script type="text/javascript">
<!--
    function MM_swapImgRestore() { //v3.0
        var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
    }
    function MM_preloadImages() { //v3.0
        var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
            var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
                if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
    }

    function MM_findObj(n, d) { //v4.01
        var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
            d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
        if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
        for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
        if(!x && d.getElementById) x=d.getElementById(n); return x;
    }

    function MM_swapImage() { //v3.0
        var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
            if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
    }
    -->

    </script>

</head>
<body class="wrap" id="content" onload="MM_preloadImages('images/EFTMakerHomeOn.gif','images/ReportOn.gif','images/ChangePasswordOn.gif','images/SignOutOn.gif')">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Maker Page
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
                        <td></td>
                        <td align="right">
                            <a href="BranchMessages.aspx">
                                <img src="images/Reture.jpg" id="ImgMsg" /></a>
                        </td>

                    </tr>
                </table>
            </div>
            <br />
            <table align="center">

                <td>
                    <div>
                        <div id="divDDI" runat="server">
                            <asp:LinkButton ID="linkBtnImportTransaction" runat="server" class="CommandButton"
                                Text="DDI Transaction Management" OnClick="linkBtnImportTransaction_Click"></asp:LinkButton>
                        </div>
                        <div id="divStandingOrder" runat="server">
                            <asp:LinkButton ID="linkBtnStandingOrder" Visible="true" runat="server" class="CommandButton"
                                Text="Standing Order" OnClick="linkBtnStandingOrder_Click"></asp:LinkButton>
                        </div>                       
                        <div id="divInwardReturnRFC" runat="server">
                            <asp:LinkButton ID="linkBtnInwardReturnRFC" runat="server" class="CommandButton"
                                Text="Return For RFC" OnClick="linkBtnInwardReturnRFC_Click"></asp:LinkButton>
                        </div>
                        <div id="divCityCards" runat="server">
                            <asp:LinkButton ID="linkBtnCityCards" runat="server" class="CommandButton"
                                Text="Cards Management" OnClick="linkBtnCityCards_Click"></asp:LinkButton>
                        </div>
                        <div id="divAccountEnquiry" runat="server">
                            <asp:LinkButton ID="linkBtnAccountEnquiry" runat="server"
                                Text="Account Enquiry" class="CommandButton"
                                OnClick="linkBtnAccountEnquiry_Click"></asp:LinkButton>
                        </div>
                        <div id="divRegenerateForAtlasError" runat="server">
                            <asp:LinkButton ID="linkBtnRegenForAtlasError" runat="server" Text="Regenerate For Atlas Error"
                                class="CommandButton" OnClick="linkBtnRegenForAtlasError_Click">
                            </asp:LinkButton>
                        </div>
                    </div>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <div>
                        <div id="divIBankingOperator" runat="server">
                            <asp:LinkButton ID="linkBtnIBankingMaker" runat="server" Text="i-Banking Operator"
                                class="CommandButton" OnClick="linkBtnIBankingMaker_Click">
                            </asp:LinkButton>
                        </div>
                        <div id="divIBankingRejectedTranRpt" runat="server">
                            <asp:LinkButton ID="linkBtnIBankingRejectedTranRptMaker" runat="server" Text="i-Banking Rejected Transaction Report"
                                class="CommandButton" OnClick="linkBtnIBankingRejectedTranRptMaker_Click">
                            </asp:LinkButton>
                        </div>
                        <div id="divIBankingCBSRpt" runat="server">
                            <asp:LinkButton ID="linkBtnIBankingCBSRptMaker" runat="server" Text="i-Banking CBS Status Report"
                                class="CommandButton" OnClick="linkBtnIBankingCBSRptMaker_Click">
                            </asp:LinkButton>
                        </div>

                    </div>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <div>
                        <div id="divEAdviceReport" runat="server">
                            <asp:LinkButton ID="linkBtnEAdviceReport" runat="server" Text="EAdvice Report"
                                class="CommandButton" OnClick="linkBtnEAdviceReport_Click">
                            </asp:LinkButton>
                        </div>
                    </div>
                </td>
            </table>
            <br />
            <div class="boxmodule" style="float: left; padding-top: 15px; padding-bottom: 10px; width: 310px; padding-left: 15px; height: 295px; margin-left: 15px">
                <ul>
                    <li><a class="CommandButton" href="InwardTransactionMaker.aspx">Inward transactions
                        (Imported New)<asp:Label ID="lblCountInwardTrans" runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="InwardReturnMaker.aspx">Inward Return<asp:Label
                        ID="lblCountInwardReturn" runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="ReceivedNOCChecker.aspx">Inward NOC<asp:Label
                        ID="lblCountInwardNOC" runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="InwardDishonor.aspx">Inward Dishonor<asp:Label
                        ID="lblCountInwardDishonor" runat="server"></asp:Label></a></li>
                    <%--                    <li><a class="CommandButton" href="InvalidTransactionModifier.aspx">Invalid Item Modifier<asp:Label
                        ID="lblCountInvalidTransactoin" runat="server"></asp:Label></a></li>--%>
                    <li><a class="CommandButton" href="RemoveTransactionWithInvalidRoutingNo.aspx">Invalid Transaction for Wrong Routing Number<asp:Label
                        ID="lblCountInvalidRoutingForOutward" runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="ReinsertReturnMaker.aspx">Unidentified Returned Transaction<asp:Label
                        ID="Label1" runat="server"></asp:Label></a></li>
                </ul>
            </div>
            <div class="boxmodule" style="float: left; padding-top: 15px; padding-bottom: 30px; padding-left: 15px; margin-left: 15px; width: 230px; height: 425px;">
                <ul>
                    <li><a class="CommandButton" href="CorporateUploadCredit.aspx">Entry Form for CREDIT (Bulk Upload)</a></li>
                    <li><a class="CommandButton" href="CorporateUploadDebit.aspx" style="color: red">Entry Form for DEBIT (Bulk Upload)</a></li>
                    <li><a class="CommandButton" href="ConsumerType.aspx">Entry Form (Manual)</a></li>
                    <li><a class="CommandButton" href="BulkTransactionList.aspx">Remaining to send Uploaded Bulk data<asp:Label ID="lblCountUploadedBulkData" runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="InwardSearch.aspx">Inward Search</a></li>
                    <li><a class="CommandButton" href="OutwardSearch.aspx">Outward Search</a></li>
                    <li><a class="CommandButton" href="FlatFilesForTransactionReceived.aspx">Flat Files For Transaction Received</a></li>
                    <li><a class="CommandButton" href="FlatFileForTransactionSent.aspx">Flat Files For Transaction Sent</a></li>

                    <li><a class="CommandButton" href="FlatFilesForReturnReceived.aspx">Flat Files For Return Received</a></li>
                    <li><a class="CommandButton" href="FlatFilesForCSVRejection.aspx">Flat Files For CSV Rejection</a></li>
                    <li><a class="CommandButton" href="FlatFileForTransactionReceivedForCards.aspx">Flat Files For Transaction Received For Cards</a></li>
                    <li><a class="CommandButton" href="FlatFileForReturnSentForSCBCards.aspx">Flat Files For Return Sent For Cards</a></li>
                    <li><a class="CommandButton" href="MICRSync.aspx">CBS Synchronization</a></li>
                    <li><a class="CommandButton" href="SendIBSTransactionToCityCBS.aspx">Send IBS Transaction To CBS</a></li>
                    <li><a class="CommandButton" href="TransactionReceiveCSVForUCB.aspx">Transaction Received CSV</a></li>

                </ul>
            </div>
            <div class="boxmodule" style="float: left; padding-top: 15px; padding-bottom: 10px; width: 310px; padding-left: 15px; height: 295px; margin-left: 15px">
                <ul class="CommandButton">
                    <li><a class="CommandButton" href="EFTTransactionRejectedForMaker.aspx">Rejected Transaction Sent<asp:Label
                        ID="lblCountRejectTrans" runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="BatchSentRejectedByChecker.aspx">Rejected Batch Sent
                        <asp:Label ID="lblCountBatchCount" runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="InwardTransactionRejectedByChecker.aspx">Inward Approved (Rejected By Checker)<asp:Label
                        ID="lblCountRejectInwardTrans" runat="server"></asp:Label></a></li>
                     <li><a class="CommandButton" href="InwardTransactionRejectedByCheckerForIF.aspx">Inward Approved Rejected By Checker For IF<asp:Label
                        ID="lblCountRejectInwardForIF" runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="EFTReturnRejectedForMaker.aspx">Rejected Return Sent<asp:Label ID="lblCountRejectReturn"
                        runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="EFTNOCRejectedForMaker.aspx">Rejected NOC Sent<asp:Label ID="lblCountRejectNOC"
                        runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="RejectedReceivedReturnDishonorSent.aspx">Rejected Received Return Dishonor
                        Sent<asp:Label ID="lblCountRejectedReturnReceivedDishonor" runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="RejectedReceivedReturnApproved.aspx">Rejected Received Return Approved
                        <asp:Label ID="lblCountRejectedReturnApproved" runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="RejectedListOfApprovedReceivedNOC.aspx">Rejected Received NOC Approved
                        <asp:Label ID="lblCountRejectedReceivedNOC" runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="ListOfDishonorRejectedByChecker.aspx">Rejected Received Dishonor Approved
                        <asp:Label ID="lblCountRejectedReceivedDishonor" runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="ListOfContestedRejectedByChecker.aspx">Rejected Received Dishonor Contested
                        <asp:Label ID="lblCountRejectedContested" runat="server"></asp:Label></a></li>
                    <li><a class="CommandButton" href="RNOCofReceivedNOCRejectedByChecker.aspx">Rejected RNOC Sent
                        <asp:Label ID="lblCountRejectedRNOCSent" runat="server"></asp:Label></a></li>
                </ul>
            </div>
            <div style="clear: both">
            </div>
            <br />
            <div style="margin-top: 25px">
                <table style="padding-top: 15px">
                    <tr height="20">
                        <td width="200px"></td>
                        <td>
<%--                            <a href="ChangeMakerPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ChangePasswordOn.gif',1)">
                                <img src="images/ChangePassword.gif" name="Image3" width="149" height="25" border="0"
                                    id="Image3" /></a>--%>
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
