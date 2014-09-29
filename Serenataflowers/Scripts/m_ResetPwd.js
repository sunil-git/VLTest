function ValidateResetPassword() {
    var loginpwd = document.getElementById('NewPassword').value;
    if (loginpwd == '') {
        ShowRowInError('NewPassword');
        setDisplayState('SmallResetNewPwd', true);
        document.getElementById('NewPassword').focus();
    }
    else {
        ShowValid('NewPassword');
        setDisplayState('SmallResetNewPwd', false);
    }
}

function ValidateConfirmPassword() {
    var loginpwd = document.getElementById('ConfirmPassword').value;
    if (loginpwd == '') {
        ShowRowInError('ConfirmPassword');
        setDisplayState('SmallResetConPwd', true);
        document.getElementById('ConfirmPassword').focus();
    }
    else {
        ShowValid('ConfirmPassword');
        setDisplayState('SmallResetConPwd', false);
    }
}


function ValidateResetPwdDetails() {
    var passwordValue = document.getElementById('NewPassword').value;
    var confirmValue = document.getElementById('ConfirmPassword').value;
    if (passwordValue == '') {
        ShowRowInError('NewPassword');
        setDisplayState('SmallResetNewPwd', true);
        document.getElementById('NewPassword').focus();
        return false
    }
    else if (confirmValue == '') {
        ShowRowInError('ConfirmPassword');
        setDisplayState('SmallResetConPwd', true);
        document.getElementById('ConfirmPassword').focus();
        return false
    }
    else if (passwordValue != confirmValue) {
        ShowRowInError('ConfirmPassword');
        setDisplayState('SmallResetConPwd', false);
        setDisplayState('pwdSmall', true);
        document.getElementById('ConfirmPassword').focus();
        return false
    }
    else {
        ShowValid('ConfirmPassword');
        ShowValid('NewPassword');
        setDisplayState('SmallResetNewPwd', false);
        setDisplayState('pwdSmall', false);
        setDisplayState('SmallResetConPwd', false);
        return true
    }
}
