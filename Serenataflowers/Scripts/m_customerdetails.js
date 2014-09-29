
var customerFirstNameElement = 'CustomerFirstName';
var errorCustomerFirstNameElement = 'ErrorCustomerFirstName';

var customerEmailElement = 'CustomerEmail';
var errorEmailElement = 'ErrorCustomerEmail';
var customermobilecodeElement = 'CustomerCountryCode'
var errorcustomermobilecodeElement = 'ErrorCustomerCountryCode'
var mobileNumberElement = 'CustomerMobileNumber'
var errormobileNumberElement = 'ErrorCustomerMobileNumber'
var customerPasswordElement = 'CustomerPassword'
var customerConfirmPasswordElement = 'CustomerConfirmPassword'
var customerManualEditFirstNameElement = 'ManualEditFirstName'
var errorManualEditFirstNameElement = 'ErrorManualEditFirstName'
var ManualManualEditEmailElement = 'ManualEditEmail'
var errorManualManualEditEmailElement = 'ErrorManualEditEmail'
var ManualEditCustomerPasswordElement = 'ManualEditCustomerPassword'
var errorManualEditCustomerPasswordElement = 'errorManualEditCustomerPassword'
var PCAEditFirstNameElement = 'PCAEditFirstName'
var ErrorPCAEditFirstNameElement = 'ErrorPCAEditFirstName'
var PCAEditEmailElement = 'PCAEditEmail'
var ErrorPCAEditEmailElement = 'ErrorPCAEditEmail'
var PCAEditCustomerHourseNumberElement = 'PCAEditCustomerHourseNumber'
var ErrorPCAEditCustomerHourseNumberElement = 'ErrorPCAEditCustomerHourseNumber'
var PCAEditCustomerCityElement = 'PCAEditCustomerCity'
var ErrorPCAEditCustomerCityElement = 'ErrorPCAEditCustomerCity'
var PCAEditCustomerPostCodeElement = 'PCAEditCustomerPostCode'
var ErrorPCAEditCustomerPostCodeElement = 'ErrorPCAEditCustomerPostCode'
var PCAEditPasswordElement = 'PCAEditPassword'
var ErrorPCAEditPasswordElement = 'ErrorPCAEditPassword'
var ManualEditCustomerHouseNumberElement = 'ManualEditCustomerHouseNumber'
var ErrorManualEditCustomerHouseNumberElement = 'ErrorManualEditCustomerHouseNumber'
var ManualEditCustomerCityElement = 'ManualEditCustomerCity'
var ErrorManualEditCustomerCityElement = 'ErrorManualEditCustomerCity'
var ManualEditCustomerPostCodeElement = 'ManualEditCustomerPostCode'
var ErrorManualEditCustomerPostCodeElement = 'ErrorManualEditCustomerPostCode'

var CountryCodeElement = 'CountryCode'
var ErrorCountryCodeElement = 'ErrorCountryCode'






function ValidateFirstNameCust(e) {

    var firstName = document.getElementById(customerFirstNameElement).value;

    if (firstName == '') {
        ShowRowInError(customerFirstNameElement);
        setDisplayState(errorCustomerFirstNameElement, true);
        //document.getElementById(customerFirstNameElement).focus();
    }
    else {
        ShowValid(customerFirstNameElement);
        setDisplayState(errorCustomerFirstNameElement, false);
    }
    return (firstName);
}
function ValidateManualEditFirstName(e) {

    var firstName = document.getElementById(customerManualEditFirstNameElement).value;

    if (firstName == '') {
        ShowRowInError(customerManualEditFirstNameElement);
        setDisplayState(errorManualEditFirstNameElement, true);
        //document.getElementById(customerManualEditFirstNameElement).focus();
    }
    else {
        ShowValid(customerManualEditFirstNameElement);
        setDisplayState(errorManualEditFirstNameElement, false);
    }
    return (firstName);
}
function ValidatePCAEditFirstName(e) {

    var firstName = document.getElementById(PCAEditFirstNameElement).value;

    if (firstName == '') {
        ShowRowInError(PCAEditFirstNameElement);
        setDisplayState(ErrorPCAEditFirstNameElement, true);
        //document.getElementById(PCAEditFirstNameElement).focus();
    }
    else {
        ShowValid(PCAEditFirstNameElement);
        setDisplayState(ErrorPCAEditFirstNameElement, false);
    }
    return (firstName);
}

function ValidateManualEditEmail() {

    var email = document.getElementById(ManualManualEditEmailElement).value;

    if (email == '') {
        ShowRowInError(ManualManualEditEmailElement);
        setDisplayState(errorManualManualEditEmailElement, true);
        //document.getElementById(ManualManualEditEmailElement).focus();
        return false;
    }
    else {

        if (validateEmailAddress(email) == false) {
            ShowRowInError(ManualManualEditEmailElement);
            email = '';
            setDisplayState(errorManualManualEditEmailElement, true);

        }
        else {
            ShowValid(ManualManualEditEmailElement);
            setDisplayState(errorManualManualEditEmailElement, false);
            return true;
        }
    }
}
function ValidatePCAEditEmail() {

    var email = document.getElementById(PCAEditEmailElement).value;

    if (email == '') {
        ShowRowInError(PCAEditEmailElement);
        setDisplayState(ErrorPCAEditEmailElement, true);
        //document.getElementById(PCAEditEmailElement).focus();
        return false;
    }
    else {

        if (validateEmailAddress(email) == false) {
            ShowRowInError(PCAEditEmailElement);
            email = '';
            setDisplayState(ErrorPCAEditEmailElement, true);

        }
        else {
            ShowValid(PCAEditEmailElement);
            setDisplayState(ErrorPCAEditEmailElement, false);
            return true;
        }
    }
}


function ValidateManualEditPassword(e) {
    var password = document.getElementById(ManualEditCustomerPasswordElement).value;

    if (password == '') {
        ShowRowInError(ManualEditCustomerPasswordElement);
        setDisplayState(errorManualEditCustomerPasswordElement, true);
    }
    else {

        ShowValid(ManualEditCustomerPasswordElement);
        setDisplayState(errorManualEditCustomerPasswordElement, false);

    }
    return (password);
}
function ValidatePCAEditPassword(e) {
    var password = document.getElementById(PCAEditPasswordElement).value;

    if (password == '') {
        ShowRowInError(PCAEditPasswordElement);
        setDisplayState(ErrorPCAEditPasswordElement, true);
    }
    else {

        ShowValid(PCAEditPasswordElement);
        setDisplayState(ErrorPCAEditPasswordElement, false);

    }
    return (password);
}

function ValidateCorrectEmail() {

  

    var email = document.getElementById(customerEmailElement).value;

    if (email == '') {
        ShowRowInError(customerEmailElement);
        setDisplayState(errorEmailElement, true);
        //document.getElementById(customerEmailElement).focus();
        return false;
    }
    else {

        if (validateEmailAddress(email) == false) {
            ShowRowInError('CustomerEmail');
            email = '';
            setDisplayState('ErrorCustomerEmail', true);
            return false;
        }
        else {
            ShowValid(customerEmailElement);
            setDisplayState(errorEmailElement, false);
            document.getElementById('LoginEmailAddress').value = document.getElementById(customerEmailElement).value;
            //callservermethod();
            return true;
        }
    }
}
function VerifyETEmailAddress() {



    var email = document.getElementById(customerEmailElement).value;
  
    if (email == '') {
        ShowRowInError(customerEmailElement);
        setDisplayState(errorEmailElement, true);
        //document.getElementById(customerEmailElement).focus();
        return false;
    }
    else {

        if (validateEmailAddress(email) == false) {
            ShowRowInError('CustomerEmail');
            email = '';
            setDisplayState('ErrorCustomerEmail', true);
            return false;
        }
        else {
            ShowValid(customerEmailElement);
            setDisplayState(errorEmailElement, false);
            document.getElementById('LoginEmailAddress').value = document.getElementById(customerEmailElement).value;
            document.getElementById('CustomerReminderPassword').value = document.getElementById('LoginEmailAddress').value;           
            callservermethod();
            return true;
        }
    }
}

function ValidatePassword(e) {
    var password = document.getElementById(customerPasswordElement).value;

    if (password == '') {
        ShowRowInError(customerPasswordElement);
        setDisplayState('ErrorCustomerPassword', true);
    }
    else {

        ShowValid(customerPasswordElement);
        setDisplayState('ErrorCustomerPassword', false);

    }
    return (password);
}
function ValidateConfirmPassword(e) {
    var confirmPassword = document.getElementById(customerConfirmPasswordElement).value;

    if (confirmPassword == '') {
        ShowRowInError(customerConfirmPasswordElement);
        setDisplayState('ErrorCustomerConfirmPassword', true);
    }
    else {
        var pwd = document.getElementById(customerConfirmPasswordElement).value;
        var confirmPwd = document.getElementById(customerPasswordElement).value;
        setDisplayState('ErrorCustomerConfirmPassword', false);
        if (pwd == confirmPwd) {
            ShowValid(customerConfirmPasswordElement);
            setDisplayState('ErrorPasswordNotMatched', false);
        }
        else {

            ShowRowInError(customerConfirmPasswordElement);
            setDisplayState('ErrorPasswordNotMatched', true);

        }


    }
    return (confirmPassword);
}
function clickEnterAddressManually() {

    if (CheckGuest() == 'yes') {
        if ((ValidateFirstNameCust(true) != '') && (ValidateCorrectEmail() != '')) {
            setDisplayState('content3', true);
            setDisplayState('divManually', true);

            setDisplayState('content1', false);
            setDisplayState('content2', false);
            setDisplayState('ManualfieldCustomerPassword', false);

            document.getElementById('DisplayCustomerFullname').innerHTML = document.getElementById('CustomerFirstName').value + "  " + document.getElementById('CustomerLastName').value;
            document.getElementById('DisplayCustomerAddress').innerHTML = document.getElementById('CustomerEmail').value;
            document.getElementById('ManualEditCustomerHouseNumber').focus();

        } else {
            setFocusonEmailFirstName();
        }

    }
    else {
        if ((ValidateFirstNameCust(true) != '') && (ValidateCorrectEmail() != '') && (ValidatePassword(true) != '') && (ValidateConfirmPassword(true) != '')) {
            setDisplayState('content3', true);
            setDisplayState('divManually', true);
            setDisplayState('content1', false);
            setDisplayState('content2', false);
            setDisplayState('ManualfieldCustomerPassword', true);
            document.getElementById('DisplayCustomerFullname').innerHTML = document.getElementById('CustomerFirstName').value + "  " + document.getElementById('CustomerLastName').value;
            document.getElementById('DisplayCustomerAddress').innerHTML = document.getElementById('CustomerEmail').value;
            document.getElementById('DisplayCustomerPassword').innerHTML = document.getElementById('CustomerPassword').value;
            document.getElementById('ManualEditCustomerHouseNumber').focus();
        }
        else {
            setFocusonEmailFirstName();
        }

    }

    setDisplayState('ManualSave', true);
    setDisplayState('PCASave', false);
    document.getElementById('hdnEnterAddManually').value = "true";
}
function setFocusonEmailFirstName() {
    var firstName = document.getElementById(customerFirstNameElement).value;
    var email = document.getElementById(customerEmailElement).value;
    if (firstName == '') {
        ShowRowInError(customerFirstNameElement);
        setDisplayState(errorCustomerFirstNameElement, true);
        // document.getElementById(customerFirstNameElement).focus();
    }
    else {
        ShowValid(customerFirstNameElement);
        setDisplayState(errorCustomerFirstNameElement, false);
    }

    if (email == '') {
        ShowRowInError(customerEmailElement);
        setDisplayState(errorEmailElement, true);
        // document.getElementById(customerEmailElement).focus();
    }
    else {

        if (validateEmailAddress(email) == false) {
            ShowRowInError('CustomerEmail');
            email = '';
            setDisplayState('ErrorCustomerEmail', true);
            // document.getElementById(customerEmailElement).focus();
        }
        else {
            ShowValid(customerEmailElement);
            setDisplayState(errorEmailElement, false);
        }
    }
    ///**************
    if (CheckGuest() != 'yes') {
        var pwd = getFieldValue(customerPasswordElement);
        var conPwd = getFieldValue(customerConfirmPasswordElement);
        if (pwd == '') {
            ShowRowInError(customerPasswordElement);
            setDisplayState('ErrorCustomerPassword', true);
            //document.getElementById(customerPasswordElement).focus();
        }
        else {
            ShowValid(customerPasswordElement);
            setDisplayState('ErrorCustomerPassword', false);
        }
        if (conPwd == '') {
            ShowRowInError(customerConfirmPasswordElement);
            setDisplayState('ErrorCustomerConfirmPassword', true);
            //document.getElementById(customerConfirmPasswordElement).focus();
        }
        else {
            setDisplayState('ErrorCustomerConfirmPassword', false);
            ShowValid(customerConfirmPasswordElement);
        }
        if (pwd != '' && conPwd != '') {
            if (pwd == conPwd) {
                ShowValid(customerPasswordElement);
                ShowValid(customerConfirmPasswordElement);
                setDisplayState('ErrorPasswordNotMatched', false);
                setDisplayState('ErrorCustomerConfirmPassword', false);
            } else {
                ShowRowInError(customerConfirmPasswordElement);
                //document.getElementById(customerConfirmPasswordElement).focus();
                setDisplayState('ErrorPasswordNotMatched', true);
            }

        }
        else if (pwd == '') {
            ShowRowInError(customerPasswordElement);
            setDisplayState('ErrorCustomerPassword', true);
            //document.getElementById(customerPasswordElement).focus();
        } else if (conPwd == '') {
            ShowRowInError(customerConfirmPasswordElement);
            setDisplayState('ErrorCustomerConfirmPassword', true);
            //document.getElementById(customerConfirmPasswordElement).focus();
        }
        else {
            ShowValid(customerPasswordElement);
            ShowValid(ConfirmPasswordElementIDCust);
            setDisplayState('ErrorPasswordNotMatched', false);
            setDisplayState('ErrorCustomerConfirmPassword', false);
        }
    }
}
function ValidateMobCodeCust(e) {
    var mobilecode = document.getElementById(customermobilecodeElement).value;

    if (mobilecode == '') {
        ShowRowInError(customermobilecodeElement);
        //document.getElementById(customermobilecodeElement).focus();
        setDisplayState(errorcustomermobilecodeElement, true);
    }
    else {
        ShowValid(customermobilecodeElement);
        setDisplayState(errorcustomermobilecodeElement, false);

    }
    return (mobilecode);
}
function ValidateCountryCode(e) {
    var mobilecode = document.getElementById(CountryCodeElement).value;

    if (mobilecode == '') {
        ShowRowInError(CountryCodeElement);
        //document.getElementById(CountryCodeElement).focus();
        setDisplayState(ErrorCountryCodeElement, true);
    }
    else {
        ShowValid(CountryCodeElement);
        setDisplayState(ErrorCountryCodeElement, false);

    }
    return (mobilecode);
}
function ValidateMobileNumberCust(e) {
    var mobileNumber = getFieldValue(mobileNumberElement);

    if (mobileNumber == '') {
        ShowRowInError(mobileNumberElement);
        setDisplayState(errormobileNumberElement, true);

    }
    else {

        ShowValid(mobileNumberElement);
        setDisplayState(errormobileNumberElement, false);
    }
    return (mobileNumber);
}
function ValidteCustomerNumberfields(obj) {
    var p = obj;
    var pp = '';
    obj = obj.value;
    var objExp = new RegExp("[0-9 ()+-]");
    if (obj.length > 0) {
        pp = '';
        for (c = 0; c < obj.length; c++) {
            if (!objExp.test(obj.charAt(c) || obj.charAt(c) == ' ')) {
                pp = "";
                continue;
            }
            else {
                pp = pp + obj.charAt(c);
            }
        }
        p.value = pp;
    }
}
function hideVoucherError() {
    setDisplayState('errorCustomerVoucherCode', false);
}
function hideDiscountVoucherError() {
    setDisplayState('ErrorVoucherCode', false);
}
function clickManualEdit() {
    setDisplayState('divManualEdit', true);
    document.getElementById('ManualEditFirstName').value = document.getElementById('CustomerFirstName').value;
    document.getElementById('ManualEditLastName').value = document.getElementById('CustomerLastName').value;

    document.getElementById('ManualEditEmail').value = document.getElementById('CustomerEmail').value;
    ShowValid(customerManualEditFirstNameElement);
    setDisplayState(errorManualEditFirstNameElement, false);
    ShowValid(ManualManualEditEmailElement);
    setDisplayState(errorManualManualEditEmailElement, false);
    if (CheckGuest() != 'yes') {
        setDisplayState('divManualCustomerPassword', true);
        document.getElementById('ManualEditCustomerPassword').value = document.getElementById('CustomerPCAPassword').value;

    } else {
        setDisplayState('divManualCustomerPassword', false);
    }
    document.getElementById('hdnEnterManualyEdit').value = "true";

}
function clickPCAEdit() {
    setDisplayState('divPCAEdit', true);
    setDisplayState('content3', true);
    setDisplayState('divManually', false);
    setDisplayState('divManualEdit', false);
    setDisplayState('content1', false);
    setDisplayState('content2', false);
    document.getElementById('PCAEditFirstName').value = document.getElementById('hdnCustomerFirstname').value;
    document.getElementById('PCAEditLastName').value = document.getElementById('hdnCustomerLastName').value;
    document.getElementById('PCAEditEmail').value = document.getElementById('CustomerEmailAddress').innerHTML;
    document.getElementById('PCAEditCustomerHourseNumber').value = document.getElementById('hdnPCAHouseNumber').value;
    document.getElementById('PCAEditCustomerDistrict').value = document.getElementById('hdnPCADistrict').value;
    document.getElementById('PCAEditCustomerCity').value = document.getElementById('hdnPCACity').value;
    document.getElementById('PCAEditCustomerPostCode').value = document.getElementById('hdnPCAPostCode').value;
    if (CheckGuest() != 'yes') {
        setDisplayState('divPCAEditPassword', true);
        document.getElementById('PCAEditPassword').value = document.getElementById('CustomerPCAPassword').innerHTML;

    } else {
        setDisplayState('divPCAEditPassword', false);
    }
    setDisplayState('PCASave', true);
    setDisplayState('ManualSave', false);
    document.getElementById('hdnPCAEdit').value = "true";

}

function ValidateSecondScreen() {
    var returnValue = false;
    returnValue = (ValidateMobCodeCust(true) != '') && (ValidateMobileNumberCust(true) != '') ? true : false;

    if (returnValue == true) {
        return returnValue;
    }
    else {
        ErrorSecondScreen();
        return returnValue;
    }
    return returnValue;
}
function ErrorSecondScreen() {
    var mobilecode = document.getElementById(customermobilecodeElement).value;

    if (mobilecode == '') {
        ShowRowInError(customermobilecodeElement);
        // document.getElementById(customermobilecodeElement).focus();
        setDisplayState(errorcustomermobilecodeElement, true);
    }
    else {
        ShowValid(customermobilecodeElement);
        setDisplayState(errorcustomermobilecodeElement, false);

    }
    var mobileNumber = getFieldValue(mobileNumberElement);

    if (mobileNumber == '') {
        ShowRowInError(mobileNumberElement);
        setDisplayState(errormobileNumberElement, true);

    }
    else {

        ShowValid(mobileNumberElement);
        setDisplayState(errormobileNumberElement, false);
    }
}
function CheckGuest() {
    var hu = window.location.search.substring(1);
    var gy = hu.split("&");
    var ft;
    for (i = 0; i < gy.length; i++) {
        ft = gy[i].split("=");
        if (ft[0] == 'guest') {
            return ft[1];
        }
    }
}


function ValidatePCAEditCustomerHouseNumber(e) {
    var house = getFieldValue(PCAEditCustomerHourseNumberElement);

    if (house == '') {
        ShowRowInError(PCAEditCustomerHourseNumberElement);
        //document.getElementById(PCAEditCustomerHourseNumberElement).focus();
        setDisplayState(ErrorPCAEditCustomerHourseNumberElement, true);
    }
    else {
        ShowValid(PCAEditCustomerHourseNumberElement);
        setDisplayState(ErrorPCAEditCustomerHourseNumberElement, false);

    }
    return (house);
}
function ValidateManualEditCustomerHouseNumber(e) {
    var house = getFieldValue(ManualEditCustomerHouseNumberElement);

    if (house == '') {
        ShowRowInError(ManualEditCustomerHouseNumberElement);
        //document.getElementById(ManualEditCustomerHouseNumberElement).focus();
        setDisplayState(ErrorManualEditCustomerHouseNumberElement, true);
    }
    else {
        ShowValid(ManualEditCustomerHouseNumberElement);
        setDisplayState(ErrorManualEditCustomerHouseNumberElement, false);

    }
    return (house);
}
function ValidatePCAEditCustomerCity(e) {

    var city = getFieldValue(PCAEditCustomerCityElement);
    if (city == '') {
        ShowRowInError(PCAEditCustomerCityElement);
        //document.getElementById(PCAEditCustomerCityElement).focus();
        setDisplayState(ErrorPCAEditCustomerCityElement, true);

    }
    else {
        ShowValid(PCAEditCustomerCityElement);
        setDisplayState(ErrorPCAEditCustomerCityElement, false);
    }
    return (city);
}
function ValidateManualEditCustomerCity(e) {

    var city = getFieldValue(ManualEditCustomerCityElement);
    if (city == '') {
        ShowRowInError(ManualEditCustomerCityElement);
        //document.getElementById(ManualEditCustomerCityElement).focus();
        setDisplayState(ErrorManualEditCustomerCityElement, true);

    }
    else {
        ShowValid(ManualEditCustomerCityElement);
        setDisplayState(ErrorManualEditCustomerCityElement, false);
    }
    return (city);
}
function ValidatePCAEditCustomerPostCode(e) {
    var postcode = getFieldValue(PCAEditCustomerPostCodeElement);

    if (postcode == '') {
        ShowRowInError(PCAEditCustomerPostCodeElement);
        setDisplayState(ErrorPCAEditCustomerPostCodeElement, true);
    }
    else {
        ShowValid(PCAEditCustomerPostCodeElement);
        setDisplayState(ErrorPCAEditCustomerPostCodeElement, false);
    }
    return (postcode);
}
function ValidateManualEditCustomerPostCode(e) {
    var postcode = getFieldValue(ManualEditCustomerPostCodeElement);

    if (postcode == '') {
        ShowRowInError(ManualEditCustomerPostCodeElement);
        setDisplayState(ErrorManualEditCustomerPostCodeElement, true);
    }
    else {
        ShowValid(ManualEditCustomerPostCodeElement);
        setDisplayState(ErrorManualEditCustomerPostCodeElement, false);
    }
    return (postcode);
}
function ValidateMobileNumber(e) {

    var mobile = getFieldValue('MobileNumber');

    if (mobile == '') {
        ShowRowInError('MobileNumber');
        setDisplayState('ErrorMobileNumber', true);
    }
    else {
        ShowValid('MobileNumber');
        setDisplayState('ErrorMobileNumber', false);
    }
    return (mobile);

}
function ValidateThirdScreenManualSave() {
    var returnValue = false;
    if (document.getElementById('divManualEdit').style.display == "none") {

        returnValue = ((ValidateManualEditCustomerHouseNumber(true) != '') && (ValidateManualEditCustomerCity(true) != '') && (ValidateManualEditCustomerPostCode(true) != '') && (ValidateCountryCode(true) != '') && (ValidateMobileNumber(true) != '')) ? true : false;

    } else {
        returnValue = ((ValidateManualEditCustomerHouseNumber(true) != '') && (ValidateManualEditCustomerCity(true) != '') && (ValidateManualEditCustomerPostCode(true) != '') && (ValidateManualEditFirstName(true) != '') && (ValidateManualEditEmail(true) != '') && (ValidateCountryCode(true) != '') && (ValidateMobileNumber(true) != '')) ? true : false;

    }


   
    if (returnValue == true) {
        return returnValue;
    }
    else {
        //ErrorThirdManualScreen();
        return returnValue;
    }
    return returnValue;
}
function ValidateThirdScreenPCASave() {
    var returnValue = false;
    if (document.getElementById('divPCAEdit').style.display != "none") {
        returnValue = ((ValidatePCAEditFirstName(true) != '') && (ValidatePCAEditEmail(true) != '') && (ValidatePCAEditCustomerHouseNumber(true) != '') && (ValidatePCAEditCustomerCity(true) != '') && (ValidatePCAEditCustomerPostCode(true) != '') && (ValidateCountryCode(true) != '') && (ValidateMobileNumber(true) != '')) ? true : false;

    } else {

        returnValue = true;
    }


   
    if (returnValue == true) {
        __doPostBack("SavePCADetails", ''); 
        return false;
    }
    else {
        //ErrorThirdManualScreen();
        return returnValue;
    }
    return returnValue;
}
function GetDiscountFocus() {
    document.getElementById('DiscountVoucherCode').focus();
}


function CapturePlusCallback(uid, response) {
    try {
        // user has selected an address from Capture+

        var customerAddress = '';
        if (response[6].FormattedValue !== '') {
            document.getElementById('CustomerPCACountry').innerHTML = response[6].FormattedValue;
            document.getElementById('hdnPCACountry').value = response[6].FormattedValue;

        }

        if (response[3].FormattedValue !== '') {
            customerAddress = response[3].FormattedValue;
            document.getElementById('hdnPCAOrganisation').value = response[3].FormattedValue;
            //alert(response[3].FormattedValue);
        }


        if (response[0].FormattedValue !== '') {
            customerAddress = customerAddress + response[0].FormattedValue + ",";
            document.getElementById('hdnPCAHouseNumber').value = response[0].FormattedValue;
        }

        if (response[1].FormattedValue !== '') {
            customerAddress = customerAddress + response[1].FormattedValue;
            document.getElementById('hdnPCAStreet').value = response[1].FormattedValue;
            //alert(response[1].FormattedValue);
        }

        if (response[2].FormattedValue !== '') {
            customerAddress = customerAddress + response[2].FormattedValue + ",";
            document.getElementById('hdnPCADistrict').value = response[2].FormattedValue;
        }
        if (response[4].FormattedValue !== '') {
            customerAddress = customerAddress + response[4].FormattedValue;
            document.getElementById('hdnPCACity').value = response[4].FormattedValue;
            document.getElementById('CustomerPCACity').innerHTML = response[4].FormattedValue;
            // alert(response[4].FormattedValue);
            //alert(document.getElementById('CustomerPCACity').innerHTML);
        }
        if (response[5].FormattedValue !== '') {
            document.getElementById('CustomerPCAPostCode').innerHTML = response[5].FormattedValue;
            document.getElementById('hdnPCAPostCode').value = response[5].FormattedValue;
        }
        document.getElementById('CustomerPCAAddress').innerHTML = customerAddress;
        document.getElementById('hdnPCAAddress').value = customerAddress;

        document.getElementById('CustomerFullName').innerHTML = document.getElementById('CustomerFirstName').value + "  " + document.getElementById('CustomerLastName').value;
        document.getElementById('hdnCustomerFirstname').value = document.getElementById('CustomerFirstName').value;
        document.getElementById('hdnCustomerLastName').value = document.getElementById('CustomerLastName').value;

        document.getElementById('CustomerEmailAddress').innerHTML = document.getElementById('CustomerEmail').value;



        //setDisplayState('content1', false);
        //setDisplayState('content2', true);


//        alert(document.getElementById('hdnPCAOrganisation').value); 
//        alert(document.getElementById('hdnPCAHouseNumber').value); 
//        alert(document.getElementById('hdnPCAStreet').value); //
//        alert(document.getElementById('hdnPCADistrict').value); // organization
//        alert(document.getElementById('hdnPCACity').value);
//        alert(document.getElementById('hdnPCAPostCode').value);

        if (CheckGuest() != 'yes') {
            setDisplayState('fieldPCAPassword', true);
            document.getElementById('CustomerPCAPassword').innerHTML = document.getElementById('CustomerPassword').value;

        } else {
            setDisplayState('fieldPCAPassword', false);
        }
        __doPostBack("CapturePCAData", '');

        // removePostCodeAttribute();
    } catch (e) {

    }
}

function displaySecondScreen() {
    //__doPostBack('updContent2', '') 
    setDisplayState('content1', false);
    setDisplayState('content3', false);
    setDisplayState('content2', true);
}



function validateEmailAddress(addr) {

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
function isVisible(elem) {
    var $elem = $(elem);
    //First check if elem is hidden through css as this is not very costly:
    if ($elem.getStyle('display') == 'none' || $elem.getStyle('visibility') == 'hidden') {
        //elem is set through CSS stylesheet or inline to invisible
        return false;
    }
    //Now check for the elem being outside of the viewport
    var $elemOffset = $elem.viewportOffset();
    if ($elemOffset.left < 0 || $elemOffset.top < 0) {
        //elem is left of or above viewport
        return false;
    }
    var vp = document.viewport.getDimensions();
    if ($elemOffset.left > vp.width || $elemOffset.top > vp.height) {
        //elem is below or right of vp
        return false;
    }
    //Now check for elements positioned on top:
    //TODO: build check for this using prototype...
    //Neither of these was true, so the elem was visible:
    return true;
}
//*****************
function PasswordTyping(e) {
    setTimeout(function () { PasswordTypingDelayed(e) }, 500);
}

function PasswordTypingDelayed(e) {

    var text = document.getElementById('textareaCustomerPassword').value;
    var stars = document.getElementById('textareaCustomerPassword').value.length;
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
    else if (unicode == 46) {
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

    document.getElementById('textareaCustomerPassword').value = text;
    document.getElementById('CustomerPassword').value = displayStars(stars);
}
function ConfirmPasswordTyping(e) {
    setTimeout(function () { ConfirmPasswordTypingDelayed(e) }, 500);
}

function ConfirmPasswordTypingDelayed(e) {

    var text = document.getElementById('textareaCustomerConfirmPassword').value;
    var stars = document.getElementById('textareaCustomerConfirmPassword').value.length;
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
    else if (unicode == 46) {
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

    document.getElementById('textareaCustomerConfirmPassword').value = text;
    document.getElementById('CustomerConfirmPassword').value = displayStars(stars);
}
function displayStars(n) {
    var stars = '';
    for (var i = 0; i < n; i++) {
        stars += '*';
    }
    return stars;
}


function numbersOnly(e) {
    var evt = (e) ? e : window.event;
    var key = (evt.keyCode) ? evt.keyCode : evt.which;


    if (key != null) {
        key = parseInt(key, 10);


        if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
            if (!isUserFriendlyChar(key))
                return false;
        }
        else {
            if (evt.shiftKey)
                return false;
        }
    }


    return true;
}


function isUserFriendlyChar(val) {
    // Backspace, Tab, Enter, Insert, and Delete
    if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46)
        return true;


    // Ctrl, Alt, CapsLock, Home, End, and Arrows
    if ((val > 16 && val < 21) || (val > 34 && val < 41))
        return true;


    // The rest
    return false;
}