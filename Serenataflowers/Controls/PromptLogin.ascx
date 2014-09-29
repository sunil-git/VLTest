<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PromptLogin.ascx.cs" Inherits="Serenataflowers.Controls.PromptLogin" %>

<div data-role="page" data-theme="a" id="PromptLogin" data-title="Prompt Login">
<script type="text/javascript">
    function ValidateLoginEmail() {

        var emailValue = document.getElementById('LoginEmailAddress').value;

        if (emailValue == '') {

            // document.getElementById('divEmail').setAttribute("class", "row error");
            ShowRowInError('LoginEmailAddress');
            setDisplayState('ErrorLoginEmailAddress', true);
            //document.getElementById('LoginEmailAddress').focus();
            setDisplayState('ErrorSignIn', false);
        }
        else {
            if (EmailAddress(emailValue) == false) {
                ShowRowInError('LoginEmailAddress');
                setDisplayState('ErrorLoginEmailAddress', true);
                // document.getElementById('LoginEmailAddress').focus();
                setDisplayState('ErrorSignIn', false);
            }
            else {
                //document.getElementById('divEmail').setAttribute("class", "row");
                ShowValid('LoginEmailAddress');
                setDisplayState('ErrorLoginEmailAddress', false);
                document.getElementById('CustomerReminderPassword').value = document.getElementById('LoginEmailAddress').value;
                setDisplayState('ErrorSignIn', false);
            }

        }

    }
    function ValidateloginPassword() {
        var loginpwd = document.getElementById('LoginPassword').value;
        if (loginpwd == '') {
            ShowRowInError('LoginPassword');
            setDisplayState('ErrorLoginPassword', true);
            //document.getElementById('LoginPassword').focus();
            setDisplayState('ErrorSignIn', false);
        }
        else {
            ShowValid('LoginPassword');
            setDisplayState('ErrorLoginPassword', false);
            setDisplayState('ErrorSignIn', false);
        }
    }
    function ValidateloginDetails() {

        var emailValue = document.getElementById('LoginEmailAddress').value;
        var passwordValue = document.getElementById('LoginPassword').value;

        if (EmailAddress(emailValue) == false) {

            // document.getElementById('divEmail').setAttribute("class", "row error");
            ShowRowInError('LoginEmailAddress');
            setDisplayState('ErrorLoginEmailAddress', true);
            //setDisplayState('ErrorSignIn', false);
            return false
        }
        else if (emailValue == '') {
            //document.getElementById('divEmail').setAttribute("class", "row error");
            ShowRowInError('LoginEmailAddress');
            setDisplayState('ErrorLoginEmailAddress', true);
            //setDisplayState('ErrorSignIn', false);
            return false
        }
        else if (passwordValue == '') {
            //document.getElementById('divPassword').setAttribute("class", "row error");
            ShowRowInError('LoginPassword');
            setDisplayState('ErrorLoginPassword', true);
            //setDisplayState('ErrorSignIn', false);
            return false
        }
        else {
            //document.getElementById('divEmail').setAttribute("class", "row");
            //document.getElementById('divPassword').setAttribute("class", "row");
            ShowValid('LoginPassword');
            setDisplayState('ErrorLoginPassword', false);
            //setDisplayState('ErrorSignIn', false);
            
            
            return true
        }

    }
    function noThanks() {
        $('.ui-dialog').dialog('close');
    }
    function EmailAddress(addr) {

        if (addr == '') {
            return false;
        }
        if (addr == '') return true;
        var invalidChars = '\/\'\\ ";:?!()[]\{\}^|';
        for (i = 0; i < invalidChars.length; i++) {
            if (addr.indexOf(invalidChars.charAt(i), 0) > -1) {
                return false;
            }
        }
        for (i = 0; i < addr.length; i++) {
            if (addr.charCodeAt(i) > 127) {

                return false;
            }
        }

        var atPos = addr.indexOf('@', 0);
        if (atPos == -1) {

            return false;
        }
        if (atPos == 0) {

            return false;
        }
        if (addr.indexOf('@', atPos + 1) > -1) {

            return false;
        }
        if (addr.indexOf('.', atPos) == -1) {

            return false;
        }
        if (addr.indexOf('@.', 0) != -1) {

            return false;
        }
        if (addr.indexOf('.@', 0) != -1) {

            return false;
        }
        if (addr.indexOf('..', 0) != -1) {

            return false;
        }
        var suffix = addr.substring(addr.lastIndexOf('.') + 1);
        if (suffix.length != 2 && suffix != 'com' && suffix != 'net' && suffix != 'org' && suffix != 'edu' && suffix != 'int' && suffix != 'mil' && suffix != 'gov' & suffix != 'arpa' && suffix != 'biz' && suffix != 'aero' && suffix != 'name' && suffix != 'coop' && suffix != 'info' && suffix != 'pro' && suffix != 'museum') {

            return false;
        }
        return true;
    }
    function displayInvalidLogin()
    {
        setDisplayState('ErrorSignIn', true);
    }
    //*****************
    function handleTyping(e) {
        setTimeout(function () { handleTypingDelayed(e) }, 500);
    }

    function handleTypingDelayed(e) {

        var text = document.getElementById('hiddenfield').value;
        var stars = document.getElementById('hiddenfield').value.length;
        unicode = eval(unicode);
        var unicode = e.keyCode ? e.keyCode : e.charCode;
        //alert(unicode);
        if ((unicode >= 65 && unicode <= 90)
            || (unicode >= 97 && unicode <= 122)
                || (unicode >= 48 && unicode <= 57)) {
            text = text + String.fromCharCode(unicode);
            //alert('text'+text);
            stars += 1;
        }
        else if (unicode==46)
        {
            text = '';
            stars = 0;
            //alert('text' + text);
            //alert('star' + text);
        }
         else {
            text = text.substring(0, text.length - 1);
            stars -= 1;
            //alert('text' + text);
            //alert('star' + text);

        }

        if ((document.getElementById('LoginPassword').value) == "") {

            document.getElementById('hiddenfield').value = "";
        }

        document.getElementById('hiddenfield').value = text;
        document.getElementById('LoginPassword').value = generateStars(stars);
    }

    function generateStars(n) {
        var stars = '';
        for (var i = 0; i < n; i++) {
            stars += '●';
        }
        return stars;
    }



</script>


<style type="text/css">

.mask
{

    
    -webkit-text-security: disc;

-moz-text-security: disc;

text-security: disc;
}

</style>

<!-- header -->
	<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:center;">Login</h3>
	</div>
<!-- header -->
<!-- /content -->

	<div data-role="content" data-theme="a" id="divPromptLogin">
    <h6 style="margin:0 0 0 0;color: rgb(138, 160, 100);">Your email is already used at Serenata, please login to get access to your address book and bypass your own address details.</h6>
    <br />
       <asp:UpdatePanel runat="server" ID="upPromptLogin" UpdateMode="Conditional">
    <ContentTemplate>
		<label for="name">Email:</label>
     
		<textarea rows="1"  ID="LoginEmailAddress" ClientIDMode="Static" runat="server"  onblur="ValidateLoginEmail();"></textarea>
        <span style="display:none" class="errortext" id="ErrorLoginEmailAddress">Please enter a valid email address.</span>

        <label for="name">Password:</label>
     <textarea rows="1" cols="10" id="hiddenfield" ClientIDMode="Static"  runat="server" style="display:none"></textarea>

		<%--<textarea rows="1" cols="1" ID="LoginPassword" ClientIDMode="Static"  runat="server" onKeyUp="handleTyping(event)" onblur="ValidateloginPassword();"></textarea>--%>
        <input type="password" ID="LoginPassword" ClientIDMode="Static" runat="server" onblur="ValidateloginPassword();" />
<%--        <textarea rows="1" cols="1" ID="LoginPassword" ClientIDMode="Static"  runat="server" class="mask" onblur="ValidateloginPassword();"></textarea>--%>


          
              
                            <span style="display:none" class="errortext" autocomplete="off" id="ErrorLoginPassword">Please enter Password.</span>
			                <span class="errortext"  id="ErrorSignIn"  style="display:none">Invalid Login</span>
		<div style="clear:both;"></div>
		
		<div align="center" style="margin:5px 0 5px 0;">
            <table style="width: 100%; border:0px" >
                <tr>
                    <td>
                          <asp:Button ID="btnLogin" runat="server"  data-theme="d"  Text="Login" OnClick="btnLogin_Click"   OnClientClick="return ValidateloginDetails();" />
                    </td>
                    <td>
                    <a href="" data-role="button" onclick="noThanks();">No, thanks</a> 
                     
                    </td>
                                       
                </tr>

            </table>
      
         
         </div>
         <div style="clear:both;"></div>
       
         <div style="text-align:right;margin-top:-10px">
         <a href ="#ResetPassword" data-rel="dialog" data-transition="pop" style="font-size:small">Forgotten Password</a></div>

			</ContentTemplate>
             <Triggers>           
            <asp:AsyncPostBackTrigger ControlID="btnLogin" EventName="Click" />          
               
              
           </Triggers>
</asp:UpdatePanel>
		</div>
           
				
			
	</div>

    

	