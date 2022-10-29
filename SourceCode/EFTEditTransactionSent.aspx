<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EFTEditTransactionSent.aspx.cs"
    Inherits="EFTN.EFTEditTransactionSent" %>

<%@ Register Src="modules/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<%@ Register Src="Modules/MakerHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Transaction</title>
    <link href="includes/sitec.css" type="text/css" rel="stylesheet" /><!--[if lt IE 7]><script type="text/javascript" src="includes/unitpngfix.js"></script><![endif]-->
</head>
<body class="wrap" id="content">
    <form id="form1" runat="server">
    <div class="maincontent">
    <uc1:Header ID="Header1" runat="server" />
        <div class="Head" align="center">
            Edit Transaction
        </div>
        <div>
            <asp:Repeater ID="rptEditTransaction" runat="server" >
            
                <ItemTemplate>
                    <table class="NormalBold">
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="lblRejectReason" runat="server" Text='<%#GetFieldName("Reject Reason")%>'></asp:Label>:
                            </td>
                            <td style="width: 80%" colspan="5">
                                <asp:TextBox ID="txtRejectReason" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RejectReason")%>' ReadOnly="true" CssClass="NormalBold"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="lblTraceNumber" runat="server" Text='<%#GetFieldName("Trace Number")%>'></asp:Label>:
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtTraceNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TraceNumber")%>' ReadOnly="true" CssClass="NormalBold"/>
                            </td>
                            <td style="width: 10%">
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblTransactionCode" runat="server" Text='<%#GetFieldName("Transaction Code")%>'></asp:Label>:
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtTransactionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TransactionCode")%>'  ReadOnly="true" CssClass="NormalBold"/>
                            </td>
                            <td style="width: 10%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="lblBankName" runat="server" Text='<%#GetFieldName("Bank Name")%>'></asp:Label>:
                            </td>
                            
                            <td style="width: 80%" colspan="5">
                                <asp:TextBox ID="txtBankName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BankName")%>'
                                    Width="225" ReadOnly="true" CssClass="NormalBold" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="lblBranchName" runat="server" Text="Branch Name"></asp:Label>:
                            </td>
                            
                            <td style="width: 80%" colspan="5">
                                <asp:TextBox ID="txtBranchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BranchName")%>'
                                    Width="225" ReadOnly="true" CssClass="NormalBold" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="lblDFIAccountNo" runat="server" Text='<%#GetFieldName("DFIAccountNo")%>'></asp:Label>:
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtDFIAccountNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DFIAccountNo")%>' CssClass="Normal" />
                            </td>
                            <td style="width: 10%">
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblAccountTypeName" runat="server" Text='<%#GetFieldName("Account Type")%>'></asp:Label>:
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtAccountTypeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AccountTypeName")%>' ReadOnly="true" CssClass="NormalBold" />
                            </td>
                            <td style="width: 10%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="lblAccountNo" runat="server" Text='<%#GetFieldName("AccountNo")%>'></asp:Label>:
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtAccountNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AccountNo")%>'  CssClass="Normal"/>
                            </td>
                            <td style="width: 10%">
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblAmount" runat="server" Text='<%#GetFieldName("Amount")%>'></asp:Label>:
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtAmount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Amount")%>' CssClass="Normal" />
                            </td>
                            <td style="width: 10%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="lblIdNumber" runat="server" Text='<%#GetFieldName("Id Number")%>'></asp:Label>:
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtIdNumber" MaxLength="22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IdNumber")%>' CssClass="Normal" />
                            </td>
                            <td style="width: 10%">
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblReceiverName" runat="server" Text='<%#GetFieldName("Receiver Name")%>'></asp:Label>:
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtReceiverName" MaxLength="22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ReceiverName")%>'  CssClass="Normal"/>
                            </td>
                            <td style="width: 10%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="lblPaymentInfo" runat="server" Text='<%#GetFieldName("Payment Info")%>'></asp:Label>:
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtPaymentInfo" MaxLength="80" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PaymentInfo")%>' CssClass="Normal" />
                            </td>
                            <td style="width: 10%">
                            </td>                            
                            <td style="width: 20%">
                                <asp:Label ID="lblReceivingBankRoutingNo" runat="server" Text='<%#GetFieldName("Receiving BankRouting No")%>'></asp:Label>:
                            </td>
                            <td style="width: 20%" colspan="2">
                                <asp:TextBox ID="txtReceivingBankRoutingNo" MaxLength="9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ReceivingBankRoutingNo")%>' CssClass="Normal" />
                            </td>                            
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="lblSECC" runat="server" Text='<%#GetFieldName("SECC")%>'></asp:Label>:
                            </td>
                            <td style="width: 20%" colspan="2">
                                <asp:TextBox ID="txtSECC" readonly="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SECC")%>' CssClass="Normal" />
                            </td> 
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        
                    </table>
                </ItemTemplate>
            </asp:Repeater>
            <br />
            <asp:Button ID="btnCheckCbsAccount" Text="Check CBS" runat="server" 
                onclick="btnCheckCbsAccount_Click" />
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <br />
            <asp:LinkButton ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"  CssClass="CommandButton"></asp:LinkButton>
            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="CommandButton" ></asp:LinkButton>
        </div>
    </div>
        <uc2:footer ID="Footer1" runat="server" />
        
    </form>
</body>
</html>
