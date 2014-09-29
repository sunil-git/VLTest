<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PasswordReminder.ascx.cs" Inherits="Serenataflowers.Controls.PasswordReminder" %>

<div data-role="page" data-theme="a" id="ResetPassword" data-title="Social Accounts">
<script >
    function SentEmail() {
        $.mobile.changePage('#SentLink', { transition: 'pop', role: 'dialog' });

    }
    function SentSMS() {
        $.mobile.changePage('#SentMobile', { transition: 'pop', role: 'dialog' });

    }

    function CloseDialog() {
        $('.ui-dialog').dialog('close');

       // alert('dd');

    }

    function ValidateEmailRmd(e) {
        var email = getFieldValue('CustomerReminderPassword');
        if (email == '') {
            //ShowRowInError(emailElementIDRmd);
            //document.getElementById(emailElementIDRmd).focus();
            //setDisplayState('emailSmall', true);
        }
        else {
            if (validateEmailAddress(email) == false) {
                ShowRowInError('CustomerReminderPassword');
                email = '';
                //document.getElementById('CustomerReminderPassword').focus();
                setDisplayState('ErrorCustomerReminderPassword', true);
            }
            else {
                ShowValid('CustomerReminderPassword');
                setDisplayState('ErrorCustomerReminderPassword', false);
            }
        }
        return (email);
    }

    function ValidateSmsRmd(e) {
        var sms = getFieldValue('CustomerSMSNumber');
        if (sms == '') {
            //ShowRowInError(smsElementIDRmd);
            //document.getElementById(smsElementIDRmd).focus();
            //setDisplayState('ErrorCustomerSMSNumber', true);
        }
        else {
            ShowValid('CustomerSMSNumber');
            setDisplayState('ErrorCustomerSMSNumber', false);
        }
        return (sms);
    }
    function ValidateRmdAllFields() {
        var returnValue = false;
        returnValue = ((ValidateSmsRmd(true) != '') || (ValidateEmailRmd(true) != '')) ? true : false;

        if (returnValue == true) {
            //$('.ui-dialog').dialog('close');
            return returnValue;
        }
        else {
            var isValid = ValidateEmailRmd(true);
            //ErrorShowCust(isValid);
            return returnValue;
        }
        return returnValue;
    }
</script>

	<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:left;">Reset password</h3>
	</div>

		<div data-role="content" data-theme="a">
				  <asp:UpdatePanel ID="udpResetPassword" runat="server" UpdateMode="Conditional">
  <ContentTemplate>

				<label for="password">By Email:</label>
                <textarea rows="1" ID="CustomerReminderPassword"  ClientIDMode="Static" runat="server" onblur="ValidateEmailRmd(true);" placeholder="e.g. mark@mail.com"></textarea>
                <span style="display:none" class="errortext" id="ErrorCustomerReminderPassword">Please enter a valid email address.</span>
               
				
				<h5 style="text-align:center;margin:5px 0 5px 0;">OR</h5>
				

				<label for="password">By SMS:</label>
				<textarea rows="1" ID="CustomerSMSNumber" ClientIDMode="Static" runat="server"  onblur="ValidateSmsRmd(true)"  placeholder="e.g. +447960926611"></textarea>
                 <span style="display:none" class="errortext" id="ErrorCustomerSMSNumber">Please enter your mobile number.</span>

				<br />
                <span style="display:none" class="errortext" id="h4Password" runat="server"></span>

                <asp:Button ID="GeneratePassword" runat="server"  data-theme="d"  Text="Send reset link"  OnClientClick="javascript:return ValidateRmdAllFields();" OnClick="GeneratePassword_Click" />
				
	</ContentTemplate>
           <Triggers>
           <asp:AsyncPostBackTrigger ControlID="GeneratePassword" EventName="Click" />
           </Triggers>
  </asp:UpdatePanel>	
			
		</div>
	
	<div data-role="footer" style="padding:15px 0 15px 0;text-align:center;">		
		
	</div>
</div>
<div data-role="dialog" data-theme="a" id="SentLink" data-title="Password Reset" onclick="CloseDialog();">
<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:left;">Reset password</h3>
	</div>
                    <table  style="color: green; display: block;">
                        <tr>
                            <td>
                                    <asp:Label ID="lblErrorInfo"  ForeColor="green" runat="server" text="We have now sent you an email with a link where you can reset your password." Style="color: green;
                                        font-weight: bold; display: block;"></asp:Label>
                            </td>
                        </tr>
                    </table>
<div data-role="footer" style="padding:15px 0 15px 0;text-align:center;">		
		
	</div>
</div>
<div data-role="dialog" data-theme="a" id="SentMobile" data-title="Password Reset">
<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:left;">Reset password</h3>
	</div>
                    <table  style="color: green; display: block;">
                        <tr>
                            <td>
                                    <asp:Label ID="Label1"  ForeColor="green" runat="server" text="We have now sent you a sms with a link where you can reset your password." Style="color: green;
                                        font-weight: bold; display: block;"></asp:Label>
                            </td>
                        </tr>
                    </table>
<div data-role="footer" style="padding:15px 0 15px 0;text-align:center;">		
		
	</div>
</div>