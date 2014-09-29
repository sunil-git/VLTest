<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_resetpassword.aspx.cs" Inherits="Serenataflowers.Checkout.m_resetpassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Controls/CheckoutFooter.ascx" TagName="CheckoutFooter" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/m_Terms.ascx" TagName="Terms" TagPrefix="uc9" %>
<%@ Register Src="~/Controls/m_Privacy.ascx" TagName="Privacy" TagPrefix="uc10" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8" />  
 <meta name="viewport" content="width=device-width, initial-scale=1.0" />
 <title>Serenata Flowers - Reset password</title>
<link rel="stylesheet" href="https://code.jquery.com/mobile/1.3.1/jquery.mobile.structure-1.3.1.min.css" />
<script src="https://code.jquery.com/jquery-1.10.0.min.js"></script>
<script>
    $(document).bind("mobileinit", function () {
        $.mobile.ajaxEnabled = false;
        $.mobile.loadingMessage = false;
        $.mobile.pageLoadErrorMessage = false;
        $.mobile.pushStateEnabled = false;
    });
 </script>
<script src="https://code.jquery.com/mobile/1.3.1/jquery.mobile-1.3.1.min.js"></script> 
<link rel="stylesheet" href="../stylesheets/serenata-flowers.min.css">	

<link rel="stylesheet" href="../stylesheets/custom.css">
<script src="../Scripts/commonfunctions.js"></script> 
<script src="../Scripts/m_ResetPwd.js"></script> 
<script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        function bindEvents() {
            $(document).trigger('create');
        }

        bindEvents();

        //Re-bind for callbacks     
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            bindEvents();
        });
     </script>
    <script type="text/javascript">
    function UpdatedPassword() {
        $.mobile.changePage('#DisMessage', { transition: 'pop', role: 'dialog' });

    }
    </script>
    <div data-role="page" id="reset-password" data-title="Reset password">


	
	<div data-role="header" data-theme="c" id="header">
		<a href="../Default.aspx" data-role="none"><div id="logo-checkout-flowers"></div></a>		
	</div>


	<div data-role="content" data-theme="c">	 
				  <asp:UpdatePanel ID="udpResetPassword" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
		
			<h3 style="text-align:left;color:#8aa064;">Reset password</h3>		
			
			<label for=""><span style="color:red;">*</span>New Password:</label>
			<asp:TextBox TextMode="Password" ID="NewPassword" ClientIDMode="Static" runat="server" MaxLength="20" onblur="ValidateResetPassword();"></asp:TextBox>
            <span style="display:none" class="errortext" id="SmallResetNewPwd">Please enter new password.</span>

			<label for=""><span style="color:red;">*</span>Confirm Password:</label>
			<asp:TextBox TextMode="Password" ID="ConfirmPassword"  ClientIDMode="Static" runat="server" MaxLength="20" onblur="ValidateConfirmPassword();"></asp:TextBox>
                        <span style="display:none" class="errortext" id="SmallResetConPwd">Please re-enter new password.</span>
                        <span style="display:none" class="errortext" id="pwdSmall">The passwords you have entered does not match.</span>	
 <br />
			<asp:Button  ID="ResetPassword" runat="server" data-theme="d" Text="Submit" OnClientClick="javascript: return ValidateResetPwdDetails();" OnClick="ResetPassword_Click" />
	</ContentTemplate>
  <Triggers>
           <asp:AsyncPostBackTrigger ControlID="ResetPassword" EventName="Click" />
           </Triggers>
  </asp:UpdatePanel>	
		
	</div>

	<div data-role="footer" data-theme="c" style="padding:0 15px 0 15px;">
	 <uc2:CheckoutFooter ID="Footer" runat="server" />
	</div>  
   
</div> 
<div data-role="dialog" data-theme="a" id="DisMessage" data-title="Password Reset">
<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:left;">Reset password</h3>
	</div>
                    <table  style="color: green; display: block;">
                        <tr>
                            <td>
                                    <asp:Label ID="Label1"  ForeColor="green" runat="server" text="New password updated successfully, please login with new password. " Style="color: green;
                                        font-weight: bold; display: block;"></asp:Label>
                            </td>
                        </tr>
                    </table>
<div data-role="footer" style="padding:15px 0 15px 0;text-align:center;">		
		
	</div>
</div>
 <uc9:Terms ID="Termsandcondition" runat="server" />
 <uc10:Privacy ID="Privacy" runat="server" /> 
    </form>
</body>
</html>
