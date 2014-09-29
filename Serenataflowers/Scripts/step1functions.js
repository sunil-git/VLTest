var IsAddressValid = false;
var IsPostCodeValid = false;
function DisplayAddressFields() {
   // document.getElementById('divPostCodes').style.display = 'none';
    document.getElementById('divAddressFields').style.display = 'block';
    //document.getElementById('divBusinessLocator').style.display = 'none';
   // document.getElementById('divPostCode_Address').style.display = 'none';
   // document.getElementById('divSelectAddressMsg').style.display = 'none';
    document.getElementById('divErrorMsg').style.display = 'none';
    //document.getElementById('recipname').setAttribute("class", "divrow45");
    document.getElementById('txtCountry_field').setAttribute("readonly", "true");
    SetaddressVerifyVal('0');
    return false;
}
function HideAddressFields() {
    //document.getElementById('divPostCodes').style.display = 'block';
    document.getElementById('divAddressFields').style.display = 'none';
   // document.getElementById('divBusinessLocator').style.display = 'none';
    document.getElementById('divErrorMsg').style.display = 'none';
    return false;
}
function ApplyErrorCSS(id) {
    document.getElementById(id).className = "errorMsg";
}
function ValidtePostCode(obj) {
    var p = obj;
    var pp = '';
    obj = obj.value;
    var objExp = new RegExp("[a-zA-Z0-9 ]");
    if (obj.length > 0) {
        pp = '';
        for (c = 0; c < obj.length; c++) {
            if (!objExp.test(obj.charAt(c) || obj.charAt(c) == ' ')) {
                continue;
            }
            else {
                pp = pp + obj.charAt(c);
            }
        }
        p.value = pp;
    }
}

function ValidtePostCode() {
    var checkString = document.getElementById('txtfind_postcode').value;
    var obj = new RegExp("[a-zA-Z0-9 ]");
    if (checkString != "") {
        if (!obj.test(checkString)) {
            //alert("Please enter only letter and numeric characters");
            document.getElementById('txtfind_postcode').value = checkString.substring(0, checkString.length - 1);
            //return (false);
        }
    }
}

//Added validation for UK Postcode format.
//Date:12-July-2012
function postcode_validateoOnBlur() {
    var postcode = document.getElementById('txtfind_postcode').value;
    if (postcode != "") {
        var regPostcode = /^([a-zA-Z]){1}([0-9][0-9]|[0-9]|[a-zA-Z][0-9][a-zA-Z]|[a-zA-Z][0-9][0-9]|[a-zA-Z][0-9]){1}([ ])([0-9][a-zA-z][a-zA-z]){1}$/;
        if (regPostcode.test(postcode) == false) {
            //            document.getElementById("spnErrorMsg").innerHTML = "Invalid Postcode"; ;
            //            document.getElementById('divErrorMsg').style.display = 'block';
            //            document.getElementById('divSelectAddressMsg').style.display = 'none';
            //            document.getElementById('divPostCode_Address').style.display = 'none';
            //  document.getElementById('spnErrorMsg').style.color = 'orange';
            //  document.getElementById('divRPostCode').setAttribute("class", "divrow dottedline");
            document.getElementById('spnTxtPostCode').style.display = 'block';
            document.getElementById('spnTxtPostCode').textContent = "Please enter valid postcode.";
            document.getElementById('spnTxtPostCode').style.display = 'block';
            document.getElementById('spnTxtPostCode').setAttribute("class", "redtxt");
            document.getElementById('spnTxtPostCode').style.color = "#fff";
            document.getElementById('divRPostCode').setAttribute("class", "reqdottedline");
            document.getElementById('divSelectAddressMsg').style.display = 'none';
            document.getElementById('divPostCode_Address').style.display = 'none';
            return false;
        }
        else {
            document.getElementById('divRPostCode').setAttribute("class", "divrow dottedline");
            document.getElementById('spnTxtPostCode').style.display = 'none';
            return true;
        }
    }
    else {
        document.getElementById('divRPostCode').setAttribute("class", "divrow dottedline");
        document.getElementById('spnTxtPostCode').style.display = 'none';
    }
}
function postcode_validate() 
{
    var postcode = document.getElementById('txtfind_postcode').value;
    if (postcode != "") {
        // var regPostcode = /^([a-zA-Z]){1}([0-9][0-9]|[0-9]|[a-zA-Z][0-9][a-zA-Z]|[a-zA-Z][0-9][0-9]|[a-zA-Z][0-9]){1}([ ])([0-9][a-zA-z][a-zA-z]){1}$/;
        var regPostcode = UKpostcode_validate(postcode)
        if (regPostcode.test(postcode) == false) {
            document.getElementById('spnTxtPostCode').textContent = "Please enter valid postcode.";
            document.getElementById('spnTxtPostCode').style.display = 'block';
            document.getElementById('spnTxtPostCode').setAttribute("class", "redtxt");
            document.getElementById('spnTxtPostCode').style.color = "#fff";
            document.getElementById('divRPostCode').setAttribute("class", "reqdottedline");
            document.getElementById('divSelectAddressMsg').style.display = 'none';
            document.getElementById('divPostCode_Address').style.display = 'none';
            return false;
        }
        else {
            document.getElementById('divRPostCode').setAttribute("class", "divrow dottedline");
            document.getElementById('spnTxtPostCode').style.display = 'none';
            return true;
        }
    }
    document.getElementById('spnTxtPostCode').style.display = 'block';
    document.getElementById('spnTxtPostCode').textContent = "Please enter the postcode.";
    document.getElementById('spnTxtPostCode').style.display = 'block';
    document.getElementById('spnTxtPostCode').setAttribute("class", "redtxt");
    document.getElementById('spnTxtPostCode').style.color = "#fff";
    document.getElementById('divRPostCode').setAttribute("class", "reqdottedline");
    document.getElementById('divSelectAddressMsg').style.display = 'none';
    document.getElementById('divPostCode_Address').style.display = 'none';
    return false;
}


function ValidtePostCodefields(obj) {
    var p = obj;
    var pp = '';
    obj = obj.value;
    var objExp = new RegExp("[a-zA-Z0-9 ]");
    if (obj.length > 0) {
        pp = '';
        for (c = 0; c < obj.length; c++) {
            if (!objExp.test(obj.charAt(c) || obj.charAt(c) == ' ')) {
                continue;
            }
            else {
                pp = pp + obj.charAt(c);
            }
        }
        p.value = pp;
    }
}

function ValidateCityOrganisation() {
    //debugger;
    var orgNameLen = document.getElementById('txtBusinessLocaterOrg').value.length;
    var cityNameLen = document.getElementById('txtBusinessLocaterPostCode').value.length;

    if (document.getElementById('txtBusinessLocaterOrg').value == "e.g. Serenata Flowers") {
        document.getElementById('txtBusinessLocaterOrg').value = '';
    }
    if (document.getElementById('txtBusinessLocaterPostCode').value == "e.g London") {
        document.getElementById('txtBusinessLocaterPostCode').value = '';
    }

    var obj = new RegExp("[a-zA-Z0-9 ]");
    if (orgNameLen > 0 && cityNameLen > 0) {
        if (obj.test(document.getElementById('txtBusinessLocaterOrg').value)) {
            if (obj.test(document.getElementById('txtBusinessLocaterPostCode').value))
                return true;
            else {
                //   alert("Organisation Name invalid");
                document.getElementById('divBOLocotor').setAttribute("class", "divrow dottedline");
                document.getElementById('divCityTown').setAttribute("class", "recipentslbl100 reqdottedline");
                return false;
            }
        }
        else {
            // alert("City Name invalid");
            document.getElementById('divBOLocotor').setAttribute("class", "reqdottedline");
            if (document.getElementById('txtBusinessLocaterPostCode').value == 0) {
                document.getElementById('divCityTown').setAttribute("class", "recipentslbl100 reqdottedline");
            }
            else {
                document.getElementById('divCityTown').setAttribute("class", "recipentslbl100");
            }
            return false;
        }

    }
    else {
        //alert("Fields required");
        if (orgNameLen == 0) {
            document.getElementById('divBOLocotor').setAttribute("class", "reqdottedline");
        }
        else {
            document.getElementById('divBOLocotor').setAttribute("class", "divrow dottedline");
        }

        if (cityNameLen == 0) {
            document.getElementById('divCityTown').setAttribute("class", "recipentslbl100 reqdottedline");
        }
        else {
            document.getElementById('divCityTown').setAttribute("class", "recipentslbl100");
        }
        return false;
    }
}


function DisplayBusinessLocator() {
    document.getElementById('divPostCodes').style.display = 'none';
    document.getElementById('divAddressFields').style.display = 'none';
    document.getElementById('divBusinessLocator').style.display = 'block';
    document.getElementById('divErrorMsg').style.display = 'none';
    return false;
}
function chkDelivaryChecked(flag) {

    if (flag == true) {
        document.getElementById('divDelivery').style.display = 'block';
    }
    else {
        document.getElementById('divDelivery').style.display = 'none';
    }

}
function chkGiftChecked(flag) {

    if (flag == true) {
        document.getElementById('divGiftMessage').style.display = 'block';
    }
    else {
        document.getElementById('divGiftMessage').style.display = 'none';
    }

}

function ValidateAddressFields() {
    //debugger;
    ErrorShow();
    var obj = ValidateAddressField();
    if (obj == "ValidationTrue") {
        IsAddressValid = true;
        return true;
    }
    else {
        document.getElementById(obj.id).focus();
        return false;
    }
}

function ValidateAddressField() {
    //debugger;

    if (document.getElementById('txtFirstName').value.length > 0) {
        if (document.getElementById('txtAddressLine1').value.length > 0) {
            if (document.getElementById('txtTownCity').value.length > 0) {
                if (document.getElementById('txtPostCode').value.length > 0) {
                    var postCodePattern = new RegExp("[a-zA-Z0-9]");
                    if (postCodePattern.test(document.getElementById('txtPostCode').value)) {
                        if (document.getElementById('chkBxDelivery_instruction_check').checked) {
                            if (document.getElementById('drpDelivery_Instructions').value == "Please select an option")
                            //return false;
                                return document.getElementById('drpDelivery_Instructions');
                        }
                        if (document.getElementById('chkBxCard_message_check').checked) {
                            if (document.getElementById('txtCard_message').value.length == 0)
                            //return false;
                                return document.getElementById('txtCard_message');
                        }
                    }
                    else {
                        //return false;
                        return document.getElementById('txtPostCode');
                    }
                }
                else {
                    //ApplyErrorCSS('divPostCode1');
                    //return false;
                    return document.getElementById('txtPostCode');

                }
            }
            else {
                //ApplyErrorCSS('divCity');
                //return false;
                return document.getElementById('txtTownCity');

            }
        }
        else {
            // ApplyErrorCSS('divAddressLine1');
            //return false;
            return document.getElementById('txtAddressLine1');

        }
    }
    else {

        //ApplyErrorCSS('divRecipient');      
        return document.getElementById('txtFirstName');
        //return false;
    }

    return "ValidationTrue";

}
function ErrorShow() {
    //showMessage();
    changeRecipientStyle();
    changeAddressStyle();
    changeTownStyle();
    changeDivDeliveryStyle();
    changeDivGiftMessageStyle();
    var postCodePattern = new RegExp("[a-zA-Z0-9]");
    if (document.getElementById('txtPostCode').value.length == 0 || !postCodePattern.test(document.getElementById('txtPostCode').value)) {
        document.getElementById('divPostCode').setAttribute("class", "reqdottedline");
    }
    else {
        document.getElementById('divPostCode').setAttribute("class", "divrow dottedline");
    }
}

function validateField(obj) {
    //debugger;
    var objValue = document.getElementById(obj.id).value;
    var objId = document.getElementById(obj.id).id;
    var divId = objId.replace("txt", "div");
    document.getElementById(obj.id).value = trim(objValue);
    objValue = document.getElementById(obj.id).value;
    if (objValue != "") {
        if (objId == "txtPostCode") {
            var postcode = document.getElementById('txtPostCode').value;
            var retrurVal = UKpostcode_validate(postcode)
            if (retrurVal == true) {
                document.getElementById(divId).setAttribute("class", "divrow dottedline");
            }
            else {
                document.getElementById(divId).setAttribute("class", "reqdottedline");
                document.getElementById('divPostCode1').style.display = 'block';
                document.getElementById('divPostCode1').setAttribute("class", "redtxt");
                document.getElementById('divPostCode1').style.color = "#fff";
            }

        }
        else {
            document.getElementById(divId).setAttribute("class", "divrow dottedline");
        }
    }
    else {
        document.getElementById(divId).setAttribute("class", "reqdottedline");
        if (objId == "txtFirstName") {
            document.getElementById('spnFirstName').textContent = "Please enter the fullname of the recipient";
            document.getElementById('spnFirstName').style.display = 'block';
            document.getElementById('spnFirstName').setAttribute("class", "redtxt");
            document.getElementById('spnFirstName').style.color = "#fff";

        }
        
        //UKpostcode_validate(postcode)
    }
    //showMessage();
}

function validateGiftMessage(obj) {
    //debugger;
    var objValue = document.getElementById(obj.id).value;
    var objId = document.getElementById(obj.id).id;
    if (objValue != "") {
        if (objId == "txtDelivery_instructions") {
            document.getElementById('divDelivery').setAttribute("class", "divrow dottedline");
        }
        else {
            document.getElementById('divGiftMessage').setAttribute("class", "divrow dottedline");
        }
    }
    else {
        if (objId == "txtDelivery_instructions") {
            document.getElementById('divDelivery').setAttribute("class", "reqdottedlineMessage");
        }
        else {
            document.getElementById('divGiftMessage').setAttribute("class", "reqdottedlineMessage");
        }
    }
    //showMessage();
}

function showMessage() {

    document.getElementById('spnFirstName').textContent = "e.g. Mary Smith";
    document.getElementById('spnFirstName').setAttribute("class", "infotext");
    document.getElementById('divFirstName').setAttribute("class", "divrow dottedline");
    document.getElementById('spnFirstName').style.color = "#8E8E8E";
    document.getElementById('spnFirstName').style.display = 'inline';

}

function changeRecipientStyle() {  
    if (document.getElementById('txtFirstName').value.length == 0) {
        document.getElementById('divFirstName').setAttribute("class", "reqdottedline");
    }
    else {
        document.getElementById('divFirstName').setAttribute("class", "divrow dottedline");
    }
}
function changeAddressStyle() {
    if (document.getElementById('txtAddressLine1').value.length == 0) {
        document.getElementById('divAddressLine1').setAttribute("class", "reqdottedline");
    }
    else {
        document.getElementById('divAddressLine1').setAttribute("class", "divrow dottedline");
    }
}

function changeTownStyle() {
    if (document.getElementById('txtTownCity').value.length == 0) {
        document.getElementById('divTownCity').setAttribute("class", "reqdottedline");
    }
    else {
        document.getElementById('divTownCity').setAttribute("class", "divrow dottedline");
    }
}

function changeDivDeliveryStyle() {
    if (document.getElementById('drpDelivery_Instructions').value == "Please select an option") {
        document.getElementById('divDelivery').setAttribute("class", "reqdottedlineMessage");
    }
    else {
        document.getElementById('divDelivery').setAttribute("class", "divrow dottedline");
    }
}

function changeDivGiftMessageStyle() {
    if (document.getElementById('txtCard_message').value.length == 0) {
        document.getElementById('divGiftMessage').setAttribute("class", "reqdottedlineMessage");
    }
    else {
        document.getElementById('divGiftMessage').setAttribute("class", "divrow dottedline");
    }
}

function checkAbove(objId) {
    if (objId == 'txtAddressLine1') {
        //showMessage();
    }
    else if (objId == 'txtTownCity') {
        //showMessage();
        changeAddressStyle();
    }
    else if (objId == 'txtPostCode') {
        //showMessage();
        changeAddressStyle();
        changeTownStyle();
    }
}

function ValidateBusinessFields(obj) {
    var fieldValue = document.getElementById(obj.id).value
    if (obj.id == "txtBusinessLocaterOrg") {
        if (fieldValue != "") {
            document.getElementById('divBOLocotor').setAttribute("class", "divrow dottedline");
        }
        else {
            document.getElementById('divBOLocotor').setAttribute("class", "reqdottedline");
        }
    }
    else {
        if (fieldValue != "") {
            document.getElementById('divCityTown').setAttribute("class", "recipentslbl100");
        }
        else {
            document.getElementById('divCityTown').setAttribute("class", "recipentslbl100 reqdottedline");
        }
    }
}

function trim(s) {
    var l = 0; var r = s.length - 1;
    while (l < s.length && s[l] == ' ')
    { l++; }
    while (r > l && s[r] == ' ')
    { r -= 1; }
    return s.substring(l, r + 1);
}
///added postcode validation for UK
function UKpostcode_validate(toCheck) {

    //var regPostcode = /^([a-zA-Z]){1}([0-9][0-9]|[0-9]|[a-zA-Z][0-9][a-zA-Z]|[a-zA-Z][0-9][0-9]|[a-zA-Z][0-9]){1}([ ])([0-9][a-zA-z][a-zA-z]){1}$/;
    // Permitted letters depend upon their position in the postcode.
    var alpha1 = "[abcdefghijklmnoprstuwyz]";                       // Character 1
    var alpha2 = "[abcdefghklmnopqrstuvwxy]";                       // Character 2
    var alpha3 = "[abcdefghjkpmnrstuvwxy]";                         // Character 3
    var alpha4 = "[abehmnprvwxy]";                                  // Character 4
    var alpha5 = "[abdefghjlnpqrstuwxyz]";                          // Character 5
    var BFPOa5 = "[abdefghjlnpqrst]";                               // BFPO alpha5
    var BFPOa6 = "[abdefghjlnpqrstuwzyz]";                          // BFPO alpha6

    // Array holds the regular expressions for the valid postcodes
    var pcexp = new Array();

    // BFPO postcodes
    pcexp.push(new RegExp("^(bf1)(\\s*)([0-6]{1}" + BFPOa5 + "{1}" + BFPOa6 + "{1})$", "i"));

    // Expression for postcodes: AN NAA, ANN NAA, AAN NAA, and AANN NAA
    pcexp.push(new RegExp("^(" + alpha1 + "{1}" + alpha2 + "?[0-9]{1,2})(\\s*)([0-9]{1}" + alpha5 + "{2})$", "i"));

    // Expression for postcodes: ANA NAA
    pcexp.push(new RegExp("^(" + alpha1 + "{1}[0-9]{1}" + alpha3 + "{1})(\\s*)([0-9]{1}" + alpha5 + "{2})$", "i"));

    // Expression for postcodes: AANA  NAA
    pcexp.push(new RegExp("^(" + alpha1 + "{1}" + alpha2 + "{1}" + "?[0-9]{1}" + alpha4 + "{1})(\\s*)([0-9]{1}" + alpha5 + "{2})$", "i"));

    // Exception for the special postcode GIR 0AA
    pcexp.push(/^(GIR)(\s*)(0AA)$/i);

    // Standard BFPO numbers
    pcexp.push(/^(bfpo)(\s*)([0-9]{1,4})$/i);

    // c/o BFPO numbers
    pcexp.push(/^(bfpo)(\s*)(c\/o\s*[0-9]{1,3})$/i);

    // Overseas Territories
    pcexp.push(/^([A-Z]{4})(\s*)(1ZZ)$/i);

    // Anguilla
    pcexp.push(/^(ai-2640)$/i);

    // Load up the string to check
    var postCode = toCheck;

    // Assume we're not going to find a valid postcode
    var valid = false;

    // Check the string against the types of post codes
    for (var i = 0; i < pcexp.length; i++) {

        if (pcexp[i].test(postCode)) {

            // The post code is valid - split the post code into component parts
            pcexp[i].exec(postCode);

            // Copy it back into the original string, converting it to uppercase and inserting a space 
            // between the inward and outward codes
            postCode = RegExp.$1.toUpperCase() + " " + RegExp.$3.toUpperCase();

            // If it is a BFPO c/o type postcode, tidy up the "c/o" part
            postCode = postCode.replace(/C\/O\s*/, "c/o ");

            // If it is the Anguilla overseas territory postcode, we need to treat it specially
            if (toCheck.toUpperCase() == 'AI-2640') { postCode = 'AI-2640' };

            // Load new postcode back into the form element
            valid = true;

            // Remember that we have found that the code is valid and break from loop
            break;
        }
    }

    // Return with either the reformatted valid postcode or the original invalid postcode
    if (valid) { return true; } else return false;
}
