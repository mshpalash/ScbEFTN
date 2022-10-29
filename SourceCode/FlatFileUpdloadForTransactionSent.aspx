<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FlatFileUpdloadForTransactionSent.aspx.cs"
    Inherits="EFTN.FlatFileUpdloadForTransactionSent" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/CheckerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flora Limited System</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">

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
            
    javascript:window.history.forward(1);
    
    function makeUppercase(myControl, evt)
    {
        document.getElementById(myControl).value = document.getElementById(myControl).value.toUpperCase();
    } 
    </script>

</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
        <div class="maincontent">
            <uc1:Header ID="Header1" runat="server" />
            <div class="Head" align="center">
                Inward Transaction Approved by Maker</div>
            <div>
                <table style="padding-top:15px">
                    <tr height="20px">
                        <td width="30px">
                        </td>
                        <td>
                            <a href="EFTChecker.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/EFTCheckerOn.gif',1)"><img src="images/EFTChecker.gif" name="Image4" width="149" height="25" border="0" id="Image4" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerEBBS.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image1','','images/EFTCheckerEBBSOn.gif',1)"><img src="images/EFTCheckerEBBS.gif" name="Image1" width="149" height="25" border="0" id="Image1" /></a>
                    
                                                </td>
                        <td width="100px">
                        </td>
                        <td>
                            <a href="EFTCheckerAuthorizer.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image2','','images/EFTCheckerAuthorizerOn.gif',1)"><img src="images/EFTCheckerAuthorizer.gif" name="Image2" width="149" height="25" border="0" id="Image2" /></a>
                                                      
                        </td>
                        <td width="100px">
                        </td>
                        <td>
                        <a href="EFTCheckerReport.aspx" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image3','','images/ReportOn.gif',1)"><img src="images/Report.gif" name="Image3" width="149" height="25" border="0" id="Image3" /></a>
                                                      
                        </td>                    
                    </tr>
                </table>
            </div>
            <br />
                 <table>
                    <tr>
                        <td class="NormalBold" >
                            Please Select your Excel File to Upload<br />
                            <asp:FileUpload CssClass="inputlt" ID="fulExcelFile" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvFulExcelFile" runat="server" ErrorMessage="Browse a file"
                                ControlToValidate="fulExcelFile"   CssClass="NormalRed"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <asp:Button ID="btnUploadExcel" runat="server" CssClass="inputlt" Text="Upload File" Width="80" OnClick="btnUploadExcel_Click" OnClientClick="return confirm('Are you sure you want to import this file?')" />
                           <asp:Label ID="lblErrMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            <br />
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
