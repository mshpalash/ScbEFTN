<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateGLAccountChecker.aspx.cs" Inherits="EFTN.UpdateGLAccountChecker" %>


<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="Modules/AdminChecker.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Invalid DFIAccount Number</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->

    <script language="javascript" type="text/javascript">
    
    function onlyACCNumbers(myControl, evt) {
        var e = event || evt;
        var charCode = e.which || e.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        var controlValue = document.getElementById(myControl).value;
        var re = "-";
        while (!IsNumeric(controlValue)) {
            controlValue = controlValue.replace(re, "");
        }
        if (controlValue.length > 12) {
            return false;
        }
        return true;
    }
        
    function onlyNumbers(evt) {
        var e = event || evt;
        var charCode = e.which || e.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
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
                Update GL Account
                <br />
                <br />
                    <asp:DataGrid ID="dtgGLAccNo" runat="Server" Width="200" BorderWidth="0px"
                        GridLines="None" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" ItemStyle-CssClass="NormalSmall"
                        FooterStyle-CssClass="GrayBackWhiteFont" HeaderStyle-CssClass="GrayBackWhiteFont"
                        ItemStyle-BackColor="#dee9fc" AlternatingItemStyle-BackColor="#ffffff"
                        OnItemCommand="dtgInvalidDFIAccNo_ItemCommand" >
                        <Columns>
                            <asp:EditCommandColumn CausesValidation="False" EditText="Edit" ItemStyle-Width="30px" UpdateText="Update"
                                    CancelText="Cancel">
                            </asp:EditCommandColumn>
                           
                            <asp:BoundColumn DataField="AccountNo" HeaderText="AccountNo" ItemStyle-Wrap="False"
                                HeaderStyle-Wrap="False" ReadOnly="true" />
                            <asp:TemplateColumn HeaderText="Status" ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="red">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem Value="ACTIVE" Text="ACTIVE"></asp:ListItem>
                                        <asp:ListItem Value="INACTIVE" Text="INACTIVE" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "AccStatus")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    </div>
        </div>
        <uc2:footer ID="Footer1" runat="server" />
    </form>
</body>
</html>
