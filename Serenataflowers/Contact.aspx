<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Serenataflowers.Contact" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="Controls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="Controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Literal ID="ltTitle" runat="server"></asp:Literal>
    <title id="Title1" runat="server" visible="false"></title>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" name="viewport" />
    <link href="~/Styles/m_style.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
<script type="text/javascript">

    function ValidateEmail1() {
        var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        if (document.getElementById('txtEmail').value.length > 0) {
            if (emailPattern.test(document.getElementById('txtEmail').value)) {
                document.getElementById('divEmail').setAttribute("class", "divrow dottedline");
                if (document.getElementById('drpQuery').value != '-1') {


                    if (document.getElementById('txtMessage').value != '' && document.getElementById('txtName').value != '') {
                        document.getElementById('divQuery').setAttribute("class", "divrow dottedline");
                        document.getElementById('divName').setAttribute("class", "divrow dottedline");
                        document.getElementById('txtMessage').setAttribute("class", "divrow dottedline");
                        return true;
                    }
                    else {
                        if (document.getElementById('txtMessage').value == '') {
                            document.getElementById('txtMessage').setAttribute("class", "reqdottedline");
                            document.getElementById('divName').setAttribute("class", "reqdottedline");

                        } else {
                            document.getElementById('divName').setAttribute("class", "reqdottedline");
                            document.getElementById('txtMessage').setAttribute("class", "reqdottedline");
                        }
                        return false;
                    }
                }
                else {
                    document.getElementById('divQuery').setAttribute("class", "reqdottedline");
                    return false;
                }

            }
            else {
                document.getElementById('divEmail').setAttribute("class", "reqdottedline");
                document.getElementById('divQuery').setAttribute("class", "reqdottedline");
                document.getElementById('txtMessage').setAttribute("class", "reqdottedline");
                document.getElementById('divName').setAttribute("class", "reqdottedline");
                return false;
            }
        }
        else {
            document.getElementById('divEmail').setAttribute("class", "reqdottedline");
            return false;
        }

    }
    function ValidateEmail() {
        var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        if (document.getElementById('txtEmail').value.length > 0) {
            if (!emailPattern.test(document.getElementById('txtEmail').value)) {
                document.getElementById('divEmail').setAttribute("class", "reqdottedline");
                // return false;
            }
        }
        if (document.getElementById('txtEmail').value != '' && document.getElementById('txtMessage').value != '' && document.getElementById('txtName').value != '' && document.getElementById('drpQuery').value != '-1') {

            document.getElementById('divQuery').setAttribute("class", "divrow dottedline");
            document.getElementById('divName').setAttribute("class", "divrow dottedline");
            document.getElementById('txtMessage').setAttribute("class", "divrow dottedline");
            document.getElementById('txtEmail').setAttribute("class", "divrow dottedline");
            if (!emailPattern.test(document.getElementById('txtEmail').value)) {
                document.getElementById('divEmail').setAttribute("class", "reqdottedline");
                return false;
            }
        }
        else {
            if (document.getElementById('txtEmail').value == '')
                document.getElementById('divEmail').setAttribute("class", "reqdottedline");
            else
                if (!emailPattern.test(document.getElementById('txtEmail').value)) {
                    document.getElementById('divEmail').setAttribute("class", "reqdottedline");
                    return false;
                }
                else
                    document.getElementById('txtEmail').setAttribute("class", "divrow dottedline");

            //document.getElementById('divEmail').setAttribute("class", "divrow dottedline");
            if (document.getElementById('txtName').value == '')
                document.getElementById('divName').setAttribute("class", "reqdottedline");
            else
                document.getElementById('divName').setAttribute("class", "divrow dottedline");
            if (document.getElementById('txtMessage').value == '')
                document.getElementById('txtMessage').setAttribute("class", "reqdottedline");
            else
                document.getElementById('txtMessage').setAttribute("class", "divrow dottedline");
            if (document.getElementById('drpQuery').value == '-1')
                document.getElementById('divQuery').setAttribute("class", "reqdottedline");
            else
                document.getElementById('divQuery').setAttribute("class", "divrow dottedline");
            return false;
        }
    }
    function ValidateEmailOnBlur() {
        var emailPatterns = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        if (document.getElementById('txtEmail').value.length > 0) {
            if (emailPatterns.test(document.getElementById('txtEmail').value)) {
                document.getElementById('divEmail').setAttribute("class", "divrow dottedline");
                return true;
            }
            else {
                document.getElementById('divEmail').setAttribute("class", "reqdottedline");
                return false;
            }
        }
        else {
            document.getElementById('divEmail').setAttribute("class", "reqdottedline");
            return false;
        }
    }
    function validateDropDown() {
        if (document.getElementById('drpQuery').value != '-1') {
            document.getElementById('divQuery').setAttribute("class", "divrow dottedline");
            return true;
        }
        else {
            document.getElementById('divQuery').setAttribute("class", "reqdottedline");
            return false;
        }

    }
</script>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="page_wrapper">
        <div id="content_normal">
            <!--Header DIV Start-->
            <div id="header">
                <uc1:Header ID="Header1" runat="server" EnableUrl="true" />
            </div>
            <!--Header DIV End-->
            <!--CONTENET DIV Start-->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <h2 style="font-size: 16px;">
                        <asp:Label ID="lblMessage" runat="server" Text="Please contact us for further details by filling in the form below"></asp:Label>
                    </h2>
                    <br />
                    <div id="para" style="font: 11px arial,verdana,sans-serif; color: #666666">
                        <asp:Label ID="lblpara" runat="server" Visible="false" Text="In order to deal with your enquiry more quickly, we recommend you use our online customer services system. This will allow us to provide a much better service, 24-7 coverage and faster response times using our highly trained staff. However, should you wish to speak with us, please call 0800 0470311."></asp:Label>
                    </div>
                    <div class="clearleft">
                    </div>
                    <div class="clearleft">
                    </div>
                    <div id="divName" class="divrow dottedline">
                        <div>
                            <span class="redtxt">*</span> Name:&nbsp;
                        </div>
                        <div>
                            <asp:TextBox ID="txtName" runat="server"  onblur="ValidateEmail()"></asp:TextBox>
                            <span id="spnNameErrMsg" class="redtxt" style="display: none">Enter valid Name</span>
                        </div>
                    </div>
                    <div id="divEmail" class="divrow dottedline">
                        <div>
                            <span class="redtxt">*</span> Email:&nbsp;
                        </div>
                        <div>
                            <asp:TextBox ID="txtEmail" runat="server" class="required email" onblur="ValidateEmailOnBlur()"></asp:TextBox>
                            <span id="spnEmailErrMsg" class="redtxt" style="display: none">Enter valid email</span>
                        </div>
                    </div>
                    <div class="divrow dottedline">
                        <div>
                            Phone:&nbsp;
                        </div>
                        <div>
                            <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="divrow dottedline">
                        <div>
                            Order number:&nbsp;
                        </div>
                        <div>
                            <asp:TextBox ID="txtOrdernumber" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divQuery" class="divrow dottedline">
                        <div>
                            <span class="redtxt">*</span> Question relates to:&nbsp;
                        </div>
                        <div>
                            <asp:DropDownList ID="drpQuery" runat="server" onblur="validateDropDown()">
                                <asp:ListItem Selected="True" Value="-1">Please choose a reason</asp:ListItem>
                                <asp:ListItem Value="13524">Problem ordering online</asp:ListItem>
                                <asp:ListItem Value="14931">Product related enquiry</asp:ListItem>
                                <asp:ListItem Value="14932">General help and advice</asp:ListItem>
                                <asp:ListItem Value="13526">Wedding flowers enquiry</asp:ListItem>
                                <asp:ListItem>Funeral flowers enquiry</asp:ListItem>
                                <asp:ListItem Value="13529">Corporate flowers enquiry</asp:ListItem>
                                <asp:ListItem Value="13525">Track my order</asp:ListItem>
                                <asp:ListItem Value="14926">Late delivery of flowers</asp:ListItem>
                                <asp:ListItem Value="14927">Non-delivery of flowers</asp:ListItem>
                                <asp:ListItem Value="14928">Wrong flowers delivered</asp:ListItem>
                                <asp:ListItem Value="14972">Product substitution</asp:ListItem>
                                <asp:ListItem Value="13525">Other enquiry</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="divMessage" class="divrow dottedline">
                        <div style="width:65%;">
                            <span class="redtxt">*</span> write your message below...
                        </div>
                        <div style="padding-bottom: 10px; padding-top: 5px; width: 100%;">
                            <asp:TextBox ID="txtMessage" runat="server" Width="100%" Height="200px" onblur="ValidateEmail()" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearleft">
                    </div>
                    <br />
                    <div class="divrow1">
                        <div>
                            <asp:Button CssClass="submitbtn" ID="btnSubmit" runat="server" Text="" OnClick="btnSubmit_Click"
                                OnClientClick="javascript:return ValidateEmail();" />
                        </div>
                    </div>
                    <div class="clearleft">
                    </div>
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--CONTENET DIV End-->
            <!--Footer DIV Start-->
            <div id="FooterDiv">
                <uc2:Footer ID="Footer1" runat="server" />
            </div>
            <!--Footer DIV End-->
        </div>
    </div>
    </form>
</body>
</html>
