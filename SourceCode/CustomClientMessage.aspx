<%@ Page Language="C#" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="modules/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Error</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" />
    <link href="includes/css/bootstrap-grid.min.css" rel="stylesheet" />
    <link href="includes/css/bootstrap.css" rel="stylesheet" />
    <!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body class="wrap" id="content">
    <div class="form-control-plaintext" style="height: 550px;">
        <uc1:Header ID="Header1" runat="server" />
        <br />
        <br />
        <p class="text-lg-center text-primary font-weight-bold">Please wait for a while and try again! Or "Please contact system administrator."</p>
        <div class="card border-0" style="align-items: center;">
            <a href="Default.aspx" class="btn btn-info align-content-center stretched-link">Home Page</a>
        </div>
    </div>

    <uc2:footer ID="Footer1" runat="server" />
</body>
</html>
