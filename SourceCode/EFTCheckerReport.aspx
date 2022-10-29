<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EFTCheckerReport.aspx.cs"
    Inherits="EFTN.EFTCheckerReport" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Checker Page</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
<script type="text/javascript">
javascript:window.history.forward(1);
</script>
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
<body class="wrap" id="content" onload="MM_preloadImages('images/DetailSettletReportMakerOn.gif','images/SettlementReportMakerOn.gif','images/UpdateSettlementDateMakerOn.gif','images/StatusReportMakerOn.gif')">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Checker Page</div>
            <div>
                <table style="padding-top:15px">
                    <tr height="20px">
                        <td width="30px">
                        </td>
                        <td>
                            <a href="EFTChecker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image10','','images/EFTCheckerOn.gif',1)"><img src="images/EFTChecker.gif" name="Image10" width="149" height="25" border="0" id="Image10" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerEBBS.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image11','','images/EFTCheckerEBBSOn.gif',1)"><img src="images/EFTCheckerEBBS.gif" name="Image11" width="149" height="25" border="0" id="Image11" /></a>
                    
                                                </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerAuthorizer.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image12','','images/EFTCheckerAuthorizerOn.gif',1)"><img src="images/EFTCheckerAuthorizer.gif" name="Image12" width="149" height="25" border="0" id="Image12" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                        <a href="EFTCheckerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image13','','images/ReportOn.gif',1)"><img src="images/Report.gif" name="Image13" width="149" height="25" border="0" id="Image13" /></a>
                                                      
                        </td>                    
                    </tr>
                </table>
            </div>
            <br />            
            <br />
            <div class="boxmodule" style="padding-top:20px; width:950px; height:1350px; margin-top:0px; margin-left:20px">
            <table  cellspacing="30px" cellpadding="30px">
                <tr>
                    <td align="center"><a href="DetailSettlementReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/DetailSettletReportMakerOn.gif',1)"><img src="images/DetailSettlementReportMaker.gif" name="Image1" width="60" height="60" border="0" id="Image1" /></a>
                    <br /><a class="CommandButton" href="DetailSettlementReport.aspx">Detail Settlement Report</a>
                    </td>
                    <td align="center"><a href="SettlementReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/SettlementReportMakerOn.gif',1)"><img src="images/SettlementReportMaker.gif" name="Image2" width="60" height="60" border="0" id="Image2" /></a>
                    <br /><a class="CommandButton" href="SettlementReport.aspx">Settlement Report</a>
                    </td>
                    <td align="center"><a href="UpdateSettlementDate.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/UpdateSettlementDateMakerOn.gif',1)"><img src="images/UpdateSettlementDateMaker.gif" name="Image3" width="60" height="60" border="0" id="Image3" /></a>
                    <br />
                    <a href="UpdateSettlementDate.aspx" class="CommandButton">Update Settlement Date</a>
                    </td>
                    <td align="center"><a href="StatusReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/StatusReportMakerOn.gif',1)"><img src="images/StatusReportMaker.gif" name="Image4" width="60" height="60" border="0" id="Image4" /></a>
                    <br />
                    <a href="StatusReport.aspx" class="CommandButton">Status Report</a>
                    </td>
                    <td align="center"><a href="MakerCheckerLisitingReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/StatusReportMakerOn.gif',1)"><img src="images/StatusReportMaker.gif" name="Image4" width="60" height="60" border="0" id="Img2" /></a>
                    <br />
                    <a href="MakerCheckerLisitingReport.aspx" class="CommandButton">Maker Checker Lisiting Report</a>
                    </td>
                </tr>
                <tr>
                    <td align="center"><a href="BranchWiseSettlementReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image9','','images/SettlementReportMakerOn.gif',1)"><img src="images/SettlementReportMaker.gif" name="Image9" width="60" height="60" border="0" id="Image9" /></a>
                    <br />
                    <a href="BranchWiseSettlementReport.aspx" class="CommandButton">BranchWise Settlement Report</a>
                    </td>                
                     <td align="center"><a href="InwardReturnReportByEffectiveEntryDate.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image8','','images/StatusReportMakerOn.gif',1)"><img src="images/StatusReportMaker.gif" name="Image8" width="60" height="60" border="0" id="Image8" /></a>
                    <br />
                    <a href="InwardReturnReportByEffectiveEntryDate.aspx" class="CommandButton">Return Report By Effective EntryDate</a>
                    </td>
                     <td align="center"><a href="CSVReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image14','','images/StatusReportMakerOn.gif',1)"><img src="images/StatusReportMaker.gif" name="Image14" width="60" height="60" border="0" id="Img14" /></a>
                    <br />
                    <a href="CSVReport.aspx" class="CommandButton">CSV Report</a>
                    </td>  
                     <td align="center"><a href="OutwardBulkFromReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image15','','images/StatusReportMakerOn.gif',1)"><img src="images/StatusReportMaker.gif" name="Image15" width="60" height="60" border="0" id="Img15" /></a>
                    <br />
                    <a href="OutwardBulkFromReport.aspx" class="CommandButton">Outward Bulk From Maker Report</a>
                    </td>
                    <td>
                         <asp:LinkButton ID="linkBtnDetailSettlementReportAck" runat="server" Text="Acknowledgement Recon Report" class="CommandButton" OnClick="linkBtnDetailSettlementReportAck_Click"></asp:LinkButton>
                    
                    </td>
                                                                           
                </tr>
                <tr>
                     

                    <td align="center">
                        <asp:LinkButton ID="linkBtnSTDOLogChecker" runat="server"
                        Text="Standing Order Log" class="CommandButton" 
                            onclick="linkBtnSTDOLogChecker_Click"></asp:LinkButton>
                    </td>
                    <td>
                        <a href="CustomerWiseReport_arc.aspx" class="CommandButton">Customer Wise Report from Archive</a>
                    </td>


                    
                     <td align="center"><a href="DashBoardReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image17','','images/StatusReportMakerOn.gif',1)"><img src="images/StatusReportMaker.gif" name="Image17" width="60" height="60" border="0" id="Img17" /></a>
                    <br />
                    <a href="DashBoardReport.aspx" class="CommandButton">Dash Board</a>
                    </td>
                    

                    <td align="center">
                        <asp:LinkButton ID="linkBtnCBSMissMatchReport" runat="server" Text="CBS Mismatch Report" class="CommandButton" OnClick="linkBtnCBSMissMatchReport_Click"></asp:LinkButton>
                    </td>  
                    <td align="center">
                        <asp:LinkButton ID="linkBtnZoneWiseReport" runat="server" Text="Zone Wise Report" class="CommandButton" OnClick="linkBtnZoneWiseReport_Click"></asp:LinkButton>
                    </td>                                                            
                                 
                </tr>            
                <tr>
                     
                     
                     <td align="center">
                        <asp:LinkButton ID="linkBtnCSVRejectionReport" runat="server" Text="CSV Rejection Report" class="CommandButton" OnClick="linkBtnCSVRejectionReport_Click"></asp:LinkButton>
                    </td> 
                                                                            
                  
                    
                    <td align="center">
                        <asp:LinkButton ID="linkBtnDetailSettlementCharge" runat="server" 
                            Text="Detail Settlement Charge Report" class="CommandButton" 
                            onclick="linkBtnDetailSettlementCharge_Click" ></asp:LinkButton>
                    </td>   
                     <td align="center">
                        <asp:LinkButton ID="linkBtnCustomerWiseReport" runat="server"
                        Text="Customer Wise Report" class="CommandButton" OnClick="linkBtnCustomerWiseReport_Click"></asp:LinkButton>
                    </td>   
                    <td align="center">
                        <asp:LinkButton ID="linkBtnAccountModifiedLog" runat="server"
                        Text="Account Modified Log" class="CommandButton" 
                            onclick="linkBtnAccountModifiedLog_Click"></asp:LinkButton>
                    </td>    
                       <td align="center">
                        <asp:LinkButton ID="linkBtnSCBCardMapper" runat="server"
                        Text="Card Mapper" class="CommandButton" onclick="linkBtnSCBCardMapper_Click"></asp:LinkButton>
                    </td>     
                </tr>
                <tr>
                    <td align="center">
                        <asp:LinkButton ID="linkBtnRejectedDebitTransactionReport" runat="server" 
                            Text="Rejected Debit Transaction Report" class="CommandButton" 
                            onclick="linkBtnRejectedDebitTransactionReport_Click"></asp:LinkButton>
                    </td>
                    <td align="center">
                        <asp:LinkButton ID="linkBtnCardsReport" runat="server" 
                            Text="Cards Report" class="CommandButton" 
                            onclick="linkBtnCardsReport_Click" ></asp:LinkButton>
                    </td>
                    <td align="center">
                        <asp:LinkButton ID="linkBtnReturnFromArchiveDB" runat="server" 
                            Text="Get return from archive database" class="CommandButton" 
                            onclick="linkBtnReturnFromArchiveDB_Click" ></asp:LinkButton>
                    </td>
                    <td align="center">
                        <asp:LinkButton ID="linkBtnSchedulerReport" runat="server" 
                            Text="Scheduler Status Report" class="CommandButton" OnClick="linkBtnSchedulerReport_Click"></asp:LinkButton>
                    </td>
                    <td>
                        <ul>
                            <li><a class="CommandButton" href="DDIReportByEntryDateForChecker.aspx">DDI Report By Entry Date</a></li>
                        </ul>
                    </td>                       
                </tr>
                <tr>
                    <td>
                        <ul>
                            <li><a class="CommandButton" href="DDIReportForChecker.aspx">DDI Report By Settlement Date</a></li>
                        </ul>
                    </td>
                    <td>
                        <ul>
                            <li><a class="CommandButton" href="SchedulerReportChekerExecutionDay.aspx">Scheduler Report By Execution Day</a></li>
                        </ul>
                    </td>
                    <td>
                        <ul>
                            <li><a class="CommandButton" href="NonAckXMLFileList.aspx">NonAck XML FileList</a></li>
                        </ul>
                    </td>
                    <td>
                        <ul>
                            <li><a class="CommandButton" href="BBReconciliataionReportCA.aspx">BB Reconciliataion Report</a></li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:LinkButton ID="linkBtnEFTAdvice" runat="server" Text="Print EFT Advice" class="CommandButton" OnClick="linkBtnEFTAdvice_Click"></asp:LinkButton>
                     </td>
                    <td align="center">
                        <asp:LinkButton ID="linkBtnBACHBranch" runat="server" Text="BACH Branch Report" class="CommandButton" OnClick="linkBtnBACHBranch_Click"></asp:LinkButton>
                    </td>   
                      <td align="center">
                        <asp:LinkButton ID="LinkBtnRFCReport" runat="server" Text="RFC Report" 
                            class="CommandButton" onclick="LinkBtnRFCReport_Click" ></asp:LinkButton>
                        <asp:LinkButton ID="linkBtnEFTChargeReport" runat="server" Text="EFT Charge Report" class="CommandButton" OnClick="linkBtnEFTChargeReport_Click"></asp:LinkButton>
                    </td> 
                    <td align="center">
                        <asp:LinkButton ID="linkBtnAddSummaryReport" runat="server" Text="Additional Summary Report" class="CommandButton" OnClick="linkBtnAddSummaryReport_Click"></asp:LinkButton>
                     </td>
                    <td align="center">
                        <asp:LinkButton ID="linkBtnBranchWiseTransactionStatus" runat="server" Text="BranchWise Transaction Status" class="CommandButton" OnClick="linkBtnBranchWiseTransactionStatus_Click"></asp:LinkButton>
                     </td>
                    <td align="center">
                        <asp:LinkButton ID="linkBtnEFTMonitor" runat="server" Text="EFT Monitoring Report" class="CommandButton" OnClick="linkBtnEFTMonitor_Click"></asp:LinkButton>
                    </td>    
                     <td align="center">
                        <asp:LinkButton ID="linkBtnEFTAdicePrintStatus" runat="server" Text="EFT Advice Print Status" class="CommandButton" OnClick="linkBtnEFTAdicePrintStatus_Click"></asp:LinkButton>
                     </td>







                </tr>
                         
            </table>   
                     
                
            </div>
             <div style="clear: both">
            </div>
            <div style="padding-top:20px; padding-left:220px">
                <table>
                    <tr height="20px">
                        <td width="50px">
                        </td>
                        
                        <td>
                            <a href="ChangeCheckerPassword.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image5','','images/ChangePasswordOn.gif',1)"><img src="images/ChangePassword.gif" name="Image5" width="149" height="25" border="0" id="Image5" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>                      
                        <td>
                            <a href="LogOut.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image6','','images/SignOutOn.gif',1)"><img src="images/SignOut.gif" name="Image6" width="149" height="25" border="0" id="Image6" /></a>
                                                       
                        </td>                    
                    </tr>
                </table>
            </div>
            
            
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
