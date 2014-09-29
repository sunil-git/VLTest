var recipientNameElement = 'txtRecipientName';
var ErrorrecipientNameElement = 'ErrorRecipientName';
var recipientNameElement1 = 'RecipientName';
var ErrorrecipientNameElement1 = 'ErrorRecipientName1';

var recipientHourseNumberElement = 'recipientAddress1'
var ErrorrecipientHourseNumberElement = 'ErrorRecipientAddress1'
var recipientTownElement = 'recipientTown'
var ErrorrecipientTownElement = 'ErrorRecipientTown'
var recipientPostCodeElement = 'recipientPostCode'
var ErrorRecipientPostCodeElement = 'ErrorRecipientPostCode'

function ValidateRecipientName() {

    var firstName = document.getElementById(recipientNameElement).value.trim();


    if (firstName == '') {
        ShowRowInError(recipientNameElement);
        setDisplayState(ErrorrecipientNameElement, true);
        document.getElementById(recipientNameElement).focus();
    }
    else {
        ShowValid(recipientNameElement);
        setDisplayState(ErrorrecipientNameElement, false);
    }
    return (firstName);
}
function ValidateRecipientName1(e) {

    var firstName = document.getElementById(recipientNameElement1).value.trim();


    if (firstName == '') {
        ShowRowInError(recipientNameElement1);
        setDisplayState(ErrorrecipientNameElement1, true);
        document.getElementById(recipientNameElement1).focus();
    }
    else {
        ShowValid(recipientNameElement1);
        setDisplayState(ErrorrecipientNameElement1, false);
    }
    return (firstName);
}
function validateRecipientHouseNumber(e) {
    var house = getFieldValue(recipientHourseNumberElement);

    if (house == '') {
        ShowRowInError(recipientHourseNumberElement);
        document.getElementById(recipientHourseNumberElement).focus();
        setDisplayState(ErrorrecipientHourseNumberElement, true);
    }
    else {
        ShowValid(recipientHourseNumberElement);
        setDisplayState(ErrorrecipientHourseNumberElement, false);

    }
    return (house);
}
function validateRecipienttown(e) {
    var town = getFieldValue(recipientTownElement);

    if (town == '') {
        ShowRowInError(recipientTownElement);
        document.getElementById(recipientTownElement).focus();
        setDisplayState(ErrorrecipientTownElement, true);
    }
    else {
        ShowValid(recipientTownElement);
        setDisplayState(ErrorrecipientTownElement, false);

    }
    return (town);
}
function validateRecipientPostCode(e) {
    var post = getFieldValue(recipientPostCodeElement);

    if (post == '') {
        ShowRowInError(recipientPostCodeElement);
        document.getElementById(recipientPostCodeElement).focus();
        setDisplayState(ErrorRecipientPostCodeElement, true);
    }
    else {
        ShowValid(recipientPostCodeElement);
        setDisplayState(ErrorRecipientPostCodeElement, false);

    }
    return (post);
}
function SetSameAsCheckedState() {

    var RecipientFirstName = getFieldValue(recipientNameElement);

    if (RecipientFirstName == '') {
        document.getElementById(recipientNameElement).value = '';
        ShowRowInError(recipientNameElement);
        setDisplayState(ErrorrecipientNameElement, true);
        document.getElementById(recipientNameElement).focus();
        document.getElementById('SameAsInvoiceAddress').checked = false;
        return false;
    }
    else {
        ShowValid(recipientNameElement);
        setDisplayState(ErrorrecipientNameElement, false);
        __doPostBack("SameAsInvoiceAddress", '');     
        return true;
    }


}
function Post2Screen() {

    __doPostBack("drpAddressBook", '');
    return true;

}

function setCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}




function DisplayHouseNo() {
    var selText = $("#drpDelIns option:selected").text();
    if (selText == "Leave with neighbour") {
        $("#divHouseNo").show();
    }
    else {
        $("#divHouseNo").hide();
    }
    setCookie("selectedDelIns", selText, 1);
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

function displaySecondScreen() {
    //    __doPostBack('updContent2', '') 
    setDisplayState('content1', false);
    setDisplayState('content2', true);

}
function recEnterAddressManually() {

   
    var RecipientFirstName = getFieldValue(recipientNameElement);

    if (RecipientFirstName == '') {
        document.getElementById(recipientNameElement).value = '';
        ShowRowInError(recipientNameElement);
        setDisplayState(ErrorrecipientNameElement, true);
        document.getElementById(recipientNameElement).focus();       
        return false;
    }
    else {
        ShowValid(recipientNameElement);
        setDisplayState(ErrorrecipientNameElement, false);
        //setDisplayState('content3', true);
        //setDisplayState('content1', false);
        //setDisplayState('content2', true);
        //setDisplayState('divEditBox', false);
      
        //document.getElementById('hdnCustomerFirstname').value = document.getElementById('txtRecipientName').value;
        
        __doPostBack("ManualAddress", '');
        return false;
    }
   
   
    
}
function editAddress() {


    document.getElementById('RecipientName').value = document.getElementById('CustomerFullName').innerHTML;
    document.getElementById('recpientOrganization').value = document.getElementById('spnOrganization').innerHTML;
    document.getElementById('recipientAddress1').value = document.getElementById('spnHouseNumber').innerHTML;
    document.getElementById('recipientAddress2').value = document.getElementById('spnStreet').innerHTML;
    document.getElementById('recipientTown').value = document.getElementById('CustomerPCACity').innerHTML;
    document.getElementById('recipientAddress3').value = document.getElementById('spnDistrict').innerHTML;
    document.getElementById('recipientPostCode').value = document.getElementById('CustomerPCAPostCode').innerHTML;

    document.getElementById("RecipientName").setAttribute("placeholder", "");
    document.getElementById("recpientOrganization").setAttribute("placeholder", "");
    document.getElementById("recipientAddress1").setAttribute("placeholder", "");
    document.getElementById("recipientAddress2").setAttribute("placeholder", "");
    document.getElementById("recipientTown").setAttribute("placeholder", "");
    document.getElementById("recipientAddress3").setAttribute("placeholder", "");
    document.getElementById("recipientPostCode").setAttribute("placeholder", "");

    //alert(document.getElementById('hdnPCAOrganisation').value);
    setDisplayState('content3', true);
   setDisplayState('content1', false);
    setDisplayState('content2', true);
    setDisplayState('divEditBox', false);
    //__doPostBack("EditAddress", '');
    //return false;


}
function SetAddressVerifyVal(val) {
    document.getElementById("hdnAddressVerify").value = val;

}
function CheckCardSelectionChecked() {
    if (document.getElementById('ChooseACard').checked)
        return false;
    else {
        __doPostBack("Occasions", '');
        return false;
    }
}
function AddCardToBasket(productid, price, nocard) {
    //alert(productid + "," + price + "," + nocard);
    try {
        __doPostBack("lnkAddCardDummy", productid + "|" + price + "|" + nocard);
        return false;
    } catch (e) {

    }
}
function SetCheckedState(objId) {

    if (document.getElementById(objId).checked) {
        //setDisplayState('upsells', false);
      document.getElementById('hdnCardSelection').value = "false";
     

    }
    else {
        //setDisplayState('upsells', true);
        document.getElementById('hdnCardSelection').value = "true";
       
    }

}
function SetSelectedIndex(dropdownlist, sVal) {
    var a = document.getElementById(dropdownlist);

    for (i = 0; i < a.length; i++) {
        if (a.options[i].value == sVal) {
            a.selectedIndex = i;
        }

    }

}
function revealSuggestDates(obj) {
    var msg = 'Unfortunately we cant delivery to {' + obj + '} for your selected date. Please select another delivery date from the dropdown.'
    document.getElementById('postcodeMessage').innerHTML = msg;
    //alert(document.getElementById('ContentPlaceHolder1_hdnnonDelPostCode').value);
    var nextdel = document.getElementById('hdnNextDelDate').value;
    //alert(nextdel);
    SetSelectedIndex('ddlSugestedDeliveryDate', nextdel);
    $.mobile.changePage('#SuggestedDate', { transition: 'pop', role: 'dialog' });

}
function revealPostcodeMessage(obj) {
    document.getElementById('lblpostcodeInfo').innerHTML = obj;
    $.mobile.changePage('#postcode-message', { transition: 'pop', role: 'dialog' });
}
function Closesuggesteddelivery() {
    $('.ui-dialog').dialog('close');
}

function continuePyament() {
    var returnValue = false;
    if (document.getElementById('content3').style.display == "none") {

        returnValue = true;
      

    } else {
        returnValue = ((ValidateRecipientName1(true) != '') &&  (validateRecipientHouseNumber(true) != '') && (validateRecipienttown(true) != '') && (validateRecipientPostCode(true) != '')) ? true : false;
        
    }


    //alert(returnValue);
    if (returnValue == true) {
        return returnValue;
    }
    else {
        //ErrorThirdManualScreen();
        return returnValue;
    }
    return returnValue;
}
//function popupSuggestedAddress() {
//    var isAddressVerify = document.getElementById("hdnAddressVerify").value;
//    alert("Hidden Value - " + isAddressVerify);
//    var hasApiAdd = document.getElementById("hdnHasApiAddress").value;
//    alert("API Value - " + hasApiAdd);
//    if (isAddressVerify == 0) {
//        if (hasApiAdd == 1) {
//            document.getElementById('divAddressSuccess').style.display = 'block';
//            document.getElementById('divAddressFailure').style.display = 'none';
//        }
//        else {
//            document.getElementById('divAddressSuccess').style.display = 'none';
//            document.getElementById('divAddressFailure').style.display = 'block';
//        }
//        revealSuggestAddress();
//        return true;
//    }
//}
function CheckListSelectedAdddress() {
    var element = document.getElementById('DrpSuggestedAddress');
    var addressID = element.selectedIndex;
    if (addressID >= 1) {
        return true;
    }
    else {
        alert('Please select an address from list.');
        return false;
    }
}
function popupSuggestedAddress() {
   
    $.mobile.changePage('#suggest-address', { transition: 'pop', role: 'dialog' });
    
}

function popupQuantityVerification() {
    document.getElementById('EditBasketheader').innerHTML = "Quantity verification";
    document.getElementById('RowWarnQty').style.display = "block";
    document.getElementById("RowWarnQty").style.display = "table-row";
    $.mobile.changePage('#EditBasket', { transition: 'pop', role: 'dialog' });
}

function addressnotfound() {
    $.mobile.changePage('#suggest-notfound', { transition: 'pop', role: 'dialog' });
    return true;
}
function CapturePlusCallback(uid, response) {
    try {
        // user has selected an address from Capture+
        //        alert('ddd');
        //        document.getElementById('Span1').innerHTML = "test:";
        //        document.getElementById('sp').innerHTML = "test:";

        var customerAddress = '';
        if (response[6].FormattedValue !== '') {
            document.getElementById('CustomerPCACountry').innerHTML = response[6].FormattedValue;
          
            document.getElementById('hdnPCACountry').value = response[6].FormattedValue;
           
        }

        if (response[3].FormattedValue !== '') {
            customerAddress = response[3].FormattedValue;
            document.getElementById('hdnPCAOrganisation').value = response[3].FormattedValue;
        }

        if (response[0].FormattedValue !== '') {
            customerAddress = customerAddress + response[0].FormattedValue + ",";
            document.getElementById('hdnPCAHouseNumber').value = response[0].FormattedValue;
        }
        if (response[1].FormattedValue !== '') {
            customerAddress = customerAddress + response[1].FormattedValue;
            document.getElementById('hdnPCAStreet').value = response[1].FormattedValue;
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
//        alert(response[0].FormattedValue);
//        alert(response[1].FormattedValue);// street
//        alert(response[2].FormattedValue);
//        alert(response[3].FormattedValue);// organization
//        alert(response[4].FormattedValue);
//        alert(response[5].FormattedValue);

        if (response[5].FormattedValue !== '') {
            document.getElementById('CustomerPCAPostCode').innerHTML = response[5].FormattedValue;
            document.getElementById('hdnPCAPostCode').value = response[5].FormattedValue;
        }
        document.getElementById('CustomerPCAAddress').innerHTML = customerAddress;
        document.getElementById('hdnPCAAddress').value = customerAddress;

        document.getElementById('CustomerFullName').innerHTML = document.getElementById('txtRecipientName').value;

        __doPostBack("CapturePCAData", '');

    } catch (e) {

    }
}
