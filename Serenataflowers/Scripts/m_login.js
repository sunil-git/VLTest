
function ValidateLoginEmail() {
   
    var emailValue = document.getElementById('LoginEmailAddress').value;

    if (emailValue=='') {

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
         setDisplayState('ErrorSignIn', false); 
        return false
    }
    else if (emailValue == '')
    {
        //document.getElementById('divEmail').setAttribute("class", "row error");
        ShowRowInError('LoginEmailAddress');
        setDisplayState('ErrorLoginEmailAddress', true);
        setDisplayState('ErrorSignIn', false); 
        return false
    }
    else if (passwordValue == '') {
        //document.getElementById('divPassword').setAttribute("class", "row error");
        ShowRowInError('LoginPassword');
        setDisplayState('ErrorLoginPassword', true);
        setDisplayState('ErrorSignIn', false);        
        return false
    }
    else {
        //document.getElementById('divEmail').setAttribute("class", "row");
        //document.getElementById('divPassword').setAttribute("class", "row");
        ShowValid('LoginPassword');
        setDisplayState('ErrorLoginPassword', false);
        setDisplayState('ErrorSignIn', false); 
        return true
    }
    
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
        $('.ui-dialog').dialog('close');
        return returnValue;
    }
    else {
        var isValid = ValidateEmailRmd(true);
        //ErrorShowCust(isValid);
        return returnValue;
    }
    return returnValue;
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

function SocialLoad() {
    // get user info
    //gigya.socialize.getUserInfo({ callback: renderUI });

    // register for connect status changes
    gigya.socialize.addEventHandlers({ onLogin: renderUI, onLogout: renderUI });

}

