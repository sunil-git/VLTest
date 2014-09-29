function CustomerEditAddress() {

    $.mobile.changePage('#customer-address', { transition: 'pop', role: 'dialog' });
}

function RecipientEditAddress() {

    $.mobile.changePage('#Recipient-address', { transition: 'pop', role: 'dialog' });
}

function ValidateConfirm_Recipient() {
    var returnValue = false;
    returnValue = ((ValidateConfirm_RecName(true)) != '' && (ValidateConfirm_RecAddress1(true) != '') && (ValidateConfirm_RecCity(true) != '') && (ValidateConfirm_RecPostCode(true) != '')) ? true : false;
    if (returnValue == true) {
        return true;
    }
    else {
        ShowErrorConfirm_Rec();
        return false;
    }
}

function closePopup() {
    $('.ui-dialog').dialog('close');
}

function popupSuggestedAddress() {

    $.mobile.changePage('#suggest-address', { transition: 'pop', role: 'dialog' });

}
function Closesuggesteddelivery() {
    $('.ui-dialog').dialog('close');
}
function addressnotfound() {
    $.mobile.changePage('#suggest-notfound', { transition: 'pop', role: 'dialog' });
    return true;
}
function EditMessage() {

    $.mobile.changePage('#message', { transition: 'pop', role: 'dialog' });
}

function EditDeliveryInstruction() {

    $.mobile.changePage('#delivery-inst', { transition: 'pop', role: 'dialog' });
}

function DisplayHouseNo() {

    var selText = $("#drpDelIns option:selected").text();
    if (selText == "Leave with neighbour") {
        $("#divHouseNo").show();
    }
    else {
        $("#divHouseNo").hide();
    }
}

function HideHouse() {
    $("#divHouseNo").hide();
}

var customerNameElement = 'txtCustName';
var errorcustomerNameElement = 'ErrorCustomerName';
function ValidateCustomerName(e) {

    var Name = document.getElementById(customerNameElement).value;
    if (Name == '') {
        ShowRowInError(customerNameElement);
        setDisplayState(errorcustomerNameElement, true);
        //document.getElementById(customerFirstNameElement).focus();
    }
    else {
        ShowValid(customerNameElement);
        setDisplayState(errorcustomerNameElement, false);
    }
    return (Name);
}
var customerAddress1Element = 'TxtCustAddr1';
var errorcustomerAddr1 = 'ErrorCustAddr1';
function ValidateCustomerAddr1(e) {

    var Addr1 = document.getElementById(customerAddress1Element).value;

    if (Addr1 == '') {
        ShowRowInError(customerAddress1Element);
        setDisplayState(errorcustomerAddr1, true);
        //document.getElementById(customerFirstNameElement).focus();
    }
    else {
        ShowValid(customerAddress1Element);
        setDisplayState(errorcustomerAddr1, false);
    }
    return (Addr1);
}

var customerCityElement = 'TxtCustTown';
var errorcustomerCityElement = 'ErrorCustCity';
function ValidateCustomerCity(e) {

    var City = document.getElementById(customerCityElement).value;

    if (City == '') {
        ShowRowInError(customerCityElement);
        setDisplayState(errorcustomerCityElement, true);
        //document.getElementById(customerFirstNameElement).focus();
    }
    else {
        ShowValid(customerCityElement);
        setDisplayState(errorcustomerCityElement, false);
    }
    return (City);
}


var customerPostCodeElement = 'TxtCustPostCode';
var errorcustomerPostCodeElement = 'ErrorPostCode';
function ValidateCustomerPostCode(e) {

    var PostCode = document.getElementById(customerPostCodeElement).value;

    if (PostCode == '') {
        ShowRowInError(customerPostCodeElement);
        setDisplayState(errorcustomerPostCodeElement, true);
        //document.getElementById(customerFirstNameElement).focus();
    }
    else {
        ShowValid(customerPostCodeElement);
        setDisplayState(errorcustomerPostCodeElement, false);
    }
    return (PostCode);
}

function ValidateCustForm() {
    var flag = false;
    if (ValidateCustomerName(true)) {
        flag = true;
    }
    else {
       
        return false;
    }
    if (ValidateCustomerAddr1(true)) {
        flag = true;
    }
    else {
       
        return false;
    }
    if (ValidateCustomerCity(true)) {
        flag = true;

    }
    else {
        
        return false;
    }
    if (ValidateCustomerPostCode(true)) {
        flag = true;
    }
    else {
       
        return false;
    }
    return flag;
}


var RecNameElement = 'txtRecName';
var errorRecrNameElement = 'ErrortxtRecName';
function ValidateRecName(e) {

    var Name = document.getElementById(RecNameElement).value;

    if (Name == '') {
        ShowRowInError(RecNameElement);
        setDisplayState(errorRecrNameElement, true);
        //document.getElementById(customerFirstNameElement).focus();
    }
    else {
        ShowValid(RecNameElement);
        setDisplayState(errorRecrNameElement, false);
    }
    return (Name);
}
var RecAddress1Element = 'txtAddress1';
var errorRecAddr1 = 'ErrortxtAddress1';
function ValidateRecAddr1(e) {

    var Addr1 = document.getElementById(RecAddress1Element).value;

    if (Addr1 == '') {
        ShowRowInError(RecAddress1Element);
        setDisplayState(errorRecAddr1, true);
        //document.getElementById(customerFirstNameElement).focus();
    }
    else {
        ShowValid(RecAddress1Element);
        setDisplayState(errorRecAddr1, false);
    }
    return (Addr1);
}


var RecPostCodeElement = 'txtPostCode';
var errorRecPostCodeElement = 'ErrortxtPostCode';
function ValidateRecPostCode(e) {

    var PostCode = document.getElementById(RecPostCodeElement).value;

    if (PostCode == '') {
        ShowRowInError(RecPostCodeElement);
        setDisplayState(errorRecPostCodeElement, true);
        //document.getElementById(customerFirstNameElement).focus();
    }
    else {
        ShowValid(RecPostCodeElement);
        setDisplayState(errorRecPostCodeElement, false);
    }
    return (PostCode);
}

var RecCityElement = 'txtCity';
var errorRecCityElement = 'ErrortxtCity';
function ValidateRecCity(e) {

    var City = document.getElementById(RecCityElement).value;

    if (City == '') {
        ShowRowInError(RecCityElement);
        setDisplayState(errorRecCityElement, true);
        //document.getElementById(customerFirstNameElement).focus();
    }
    else {
        ShowValid(RecCityElement);
        setDisplayState(errorRecCityElement, false);
    }
    return (City);
}


function ValidateRecForm() {
    var flag = false;
    if (ValidateRecName(true)) {

        flag = true;
    }
    else {

        return false;
    }
    if (ValidateRecAddr1(true)) {

        flag = true;
    }
    else {

        return false;
    } if (ValidateRecPostCode(true)) {

        flag = true;
    }
    else {

        return false;
    } if (ValidateRecCity(true)) {

        flag = true;
    }
    else {

        return false;
    }
    return flag;
}


