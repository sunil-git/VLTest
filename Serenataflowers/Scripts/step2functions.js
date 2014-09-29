function OnShowBusinesslocatorClick() {
    document.getElementById('div2').style.display = 'block';
    document.getElementById('DivFindBusiness').style.display = 'block';
    document.getElementById('DivFindTown').style.display = 'block';
    document.getElementById('DivBusinessLink').style.display = 'block';
    document.getElementById('DivResidential').style.display = 'block';
    document.getElementById('DivErrorMsgFindPostcode').style.display = 'none';
    document.getElementById('div3').style.display = 'none';
    //document.getElementById('div1').style.display = 'none';
    return false;
}
function OnShowResidentialPropertylocatorClick() {
    //document.getElementById('div1').style.display = 'block';
    document.getElementById('DivFindpostcode').style.display = 'block';
    document.getElementById('DivErrorMsgFindPostcode').style.display = 'none';
    document.getElementById('div2').style.display = 'none';
    document.getElementById('div3').style.display = 'none';
    return false;
}
//It will be use later for ddlAddress onchange
//function validateDdlValue(obj) {
//    if (document.getElementById(obj.id).value == "0") return false;
//    else return true;
//}
function OnShowResidentialManualClick() {
    //document.getElementById('div1').style.display = 'none';
   // document.getElementById('DivFindpostcode').style.display = 'none';
    document.getElementById('DivResidentialmanual').style.display = 'block';
    //document.getElementById('DivBusinessmanual').style.display = 'block';
    document.getElementById('DivErrorMsgFindPostcode').style.display = 'none';
    document.getElementById('div2').style.display = 'none';
    document.getElementById('div3').style.display = 'block';
    document.getElementById('DivAddressResult').style.display = 'none';
    document.getElementById('DivAddressResultMsg').style.display = 'none';
    document.getElementById('organisation_field').value = '';
    document.getElementById('txtAddressline1').value = '';
    document.getElementById('street_field').value = '';
    document.getElementById('txtCitytown').value = '';
    document.getElementById('txtPostcodefield').value = '';
    document.getElementById('district_field').value = '';
    document.getElementById('mobile_field').value = '';
    document.getElementById('promo_email_field').checked = 'true';
    return false;
}
function RetainedBillingInfo() {
    //document.getElementById('div1').style.display = 'none';
    //document.getElementById('DivFindpostcode').style.display = 'none';
    document.getElementById('DivResidentialmanual').style.display = 'block';
    //document.getElementById('DivBusinessmanual').style.display = 'block';
    document.getElementById('DivErrorMsgFindPostcode').style.display = 'none';
    document.getElementById('div2').style.display = 'none';
    document.getElementById('div3').style.display = 'block';
    document.getElementById('DivAddressResult').style.display = 'none';
    document.getElementById('DivAddressResultMsg').style.display = 'none';
    document.getElementById('promo_email_field').checked = 'true';
    return false;
}
function OnShowEntermanuallyClick() {
    document.getElementById('div3').style.display = 'block';
    //document.getElementById('div2').style.display = 'none';
    //document.getElementById('div1').style.display = 'none';
    document.getElementById('DivErrorMsgFindPostcode').style.display = 'none';
    document.getElementById('DivBusinessLink').style.display = 'block';
    document.getElementById('DivResidential').style.display = 'block';
    document.getElementById('DivFindBusiness').style.display = 'none';
    document.getElementById('DivFindTown').style.display = 'none';
    document.getElementById('DivErrorMsg').style.display = 'none';
    document.getElementById('promo_email_field').checked = 'true';
    return false;
}
function chkDeliveryChecked(flag) {
    if (flag == true) {
        //document.getElementById('div1').style.display = 'none';
        document.getElementById('DivFindpostcode').style.display = 'none';
        document.getElementById('DivResidentialmanual').style.display = 'block';
        document.getElementById('DivBusinessmanual').style.display = 'block';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('div3').style.display = 'block';
        document.getElementById('promo_email_field').checked = 'true';
    }
    else {
        //document.getElementById('div1').style.display = 'block';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('div3').style.display = 'none';
    }
    //return false;
}
function OnShowBusinessButtonClick() {
    debugger;
    if (ValidateCityOrganisation()) {
        //if (document.getElementById('find_business_name').value.length == 0) return false;
        //if (document.getElementById('find_town').value.length == 0) return false;
        document.getElementById('div2').style.display = 'block';
        //document.getElementById('div1').style.display = 'none';
        document.getElementById('DivFindpostcode').style.display = 'none';
        document.getElementById('DivResidentialmanual').style.display = 'block';
        document.getElementById('DivBusinessmanual').style.display = 'block';
        document.getElementById('div3').style.display = 'none';
        return true;
    }
    else {
        return false;
    }
}
function ValidateAddressFields() {
    //debugger;
    ShowErrorMsg();
    var obj = ValidateAddressField();
    if (obj == "ValidationTrue") {
        return true;
    }
    else {
        document.getElementById(obj.id).focus();
        return false;
    }
}
function ValidateAddressField() {
    //debugger;
    var phonePattern = new RegExp("[0-9]");
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (document.getElementById('txtFirstName').value.length > 0) {
        if (document.getElementById('txtLastName').value.length > 0) {
            if (document.getElementById('txtEmail').value.length > 0) {
                if (emailPattern.test(document.getElementById('txtEmail').value)) {
                    if (document.getElementById('txtAddressline1').value.length > 0) {
                        if (document.getElementById('txtCitytown').value.length > 0) {
                            if (document.getElementById('txtPostcodefield').value.length > 0) {
                                var postCodePattern = new RegExp("[a-zA-Z0-9]");
                                if (postCodePattern.test(document.getElementById('txtPostcodefield').value)) {
                                    if (document.getElementById('txtContacttel').value.length > 0) {
                                        if (phonePattern.test(document.getElementById('txtContacttel').value)) {
                                            return "ValidationTrue"; // All Validations is true
                                        }
                                        else return document.getElementById('txtContacttel'); //phone pattern test is false
                                    }
                                    else return document.getElementById('txtContacttel'); //phone field length == 0
                                }
                                else return document.getElementById('txtPostcodefield'); //postcode pattern test is false
                            }
                            else return document.getElementById('txtPostcodefield'); //postcode field length == 0
                        }
                        else return document.getElementById('txtCitytown'); //CityTown field length == 0
                    }
                    else return document.getElementById('txtAddressline1'); //AddressLine1 field length == 0
                }
                else return document.getElementById('txtEmail'); //email pattern test is false
            }
            else return document.getElementById('txtEmail'); //Email field length == 0
        }
        else return document.getElementById('txtLastName'); //Last Name lenght==0
    }
    else return document.getElementById('txtFirstName'); // First Name length==0
}
function ShowErrorMsg() {
    if (document.getElementById('txtFirstName').value.length == 0) document.getElementById('divFirstName').setAttribute("class", "reqdottedline");
    else document.getElementById('divFirstName').setAttribute("class", "divrow dottedline");
    if (document.getElementById('txtLastName').value.length == 0) document.getElementById('divLastName').setAttribute("class", "reqdottedline");
    else document.getElementById('divLastName').setAttribute("class", "divrow dottedline");
   
    if (document.getElementById('txtEmail').value.length == 0) document.getElementById('divEmail').setAttribute("class", "reqdottedline");
    else document.getElementById('divEmail').setAttribute("class", "divrow dottedline");
    if (document.getElementById('txtAddressline1').value.length == 0) document.getElementById('divAddressline1').setAttribute("class", "reqdottedline");
    else document.getElementById('divAddressline1').setAttribute("class", "divrow dottedline");
    if (document.getElementById('txtCitytown').value.length == 0) document.getElementById('divCitytown').setAttribute("class", "reqdottedline");
    else document.getElementById('divCitytown').setAttribute("class", "divrow dottedline");
    if (document.getElementById('txtPostcodefield').value.length == 0) document.getElementById('divPostcodefield').setAttribute("class", "reqdottedline");
    else document.getElementById('divPostcodefield').setAttribute("class", "divrow dottedline");
//    if (document.getElementById('txtContacttel').value.length == 0) document.getElementById('divContacttel').setAttribute("class", "reqdottedline");
//    else document.getElementById('divContacttel').setAttribute("class", "divrow dottedline");
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (!emailPattern.test(document.getElementById('txtEmail').value)) document.getElementById('divEmail').setAttribute("class", "reqdottedline");
    else document.getElementById('divEmail').setAttribute("class", "divrow dottedline");
//    var phonePattern = new RegExp("[0-9]");
//    // /^(\+\d)*\s*(\(\d{3}\)\s*)*\d{3}(-{0,1}|\s{0,1})\d{2}(-{0,1}|\s{0,1})\d{2}$/;
//    if (!phonePattern.test(document.getElementById('txtContacttel').value)) document.getElementById('divContacttel').setAttribute("class", "reqdottedline");
//    else document.getElementById('divContacttel').setAttribute("class", "divrow dottedline");
    var postCodePattern = new RegExp("[a-zA-Z0-9]");
    if (!postCodePattern.test(document.getElementById('txtPostcodefield').value)) document.getElementById('divPostcodefield').setAttribute("class", "reqdottedline");
    else document.getElementById('divPostcodefield').setAttribute("class", "divrow dottedline");
    //    if (document.getElementById('voucher_code').value.length > 0) {
    //        var voucherExp = new RegExp("[a-zA-Z0-9]");
    //        if (!voucherExp.test(document.getElementById('voucher_code').value)) {
    //            document.getElementById('spnVoucher').innerText = 'Enter valid Voucher';
    //        }
    //        else document.getElementById('spnVoucher').innerText = '';
    //    }
    //    else document.getElementById('spnVoucher').innerText = '';
}
function ValidatePostCode(obj) {
    var len = document.getElementById('find_postcode').value.length;
    validateField(obj);
    if (len > 0) {
        var obj = new RegExp("[a-zA-Z0-9 ]");
        if (obj.test(document.getElementById('find_postcode').value)) {
            return true;
        }
        else return false;
    }
    else return false;
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
function Validetephone(obj) {

    var p = obj;
    var pp = '';
    obj = obj.value;

    if (obj.length > 0) {
        pp = '';
        for (c = 0; c < obj.length; c++) {
            if (isNaN(obj.charAt(c)) || obj.charAt(c) == ' ') {
                continue;
            }
            else {
                pp = pp + obj.charAt(c);
            }
        }
        p.value = pp;
    }
}
function formatdiv() {

    document.getElementById('divFname').setAttribute("class", "divrow25");
    document.getElementById('divlname').setAttribute("class", "divrow25");
    document.getElementById('divename').setAttribute("class", "divrow25");
    document.getElementById('divblanlk').setAttribute("class", "divrow25");
    document.getElementById('divfAdd').setAttribute("class", "divrow25");
    document.getElementById('divblank1').setAttribute("class", "divrow25");
    document.getElementById('divblank2').setAttribute("class", "divrow25");

    return false;
}
function validateField(obj) {
    var objValue = document.getElementById(obj.id).value;
    var objId = document.getElementById(obj.id).id;
    var divId = objId.replace("txt", "div");
    document.getElementById(obj.id).value = trim(objValue);
    objValue = document.getElementById(obj.id).value;
    if (obj.type == "text") {
        if (objValue != "") {
            document.getElementById(divId).setAttribute("class", "divrow dottedline");
        }
        else {
            document.getElementById(divId).setAttribute("class", "reqdottedline");
        }
    }
    else {
        if (document.getElementById('txtFirstName').value == "") {
            document.getElementById('divFirstName').setAttribute("class", "reqdottedline");
        }
        else {
            document.getElementById('divFirstName').setAttribute("class", "divrow dottedline");
        }
        if (document.getElementById('txtLastName').value == "") {
            document.getElementById('divLastName').setAttribute("class", "reqdottedline");
        }
        else {
            document.getElementById('divLastName').setAttribute("class", "divrow dottedline");
        }
        if (document.getElementById('txtEmail').value == "") {
            document.getElementById('divEmail').setAttribute("class", "reqdottedline");
        }
        else {
            document.getElementById('divEmail').setAttribute("class", "divrow dottedline");
        }
    }
}


function validateEmail(addr) {

    if (addr == '') {
        document.getElementById('divEmail').setAttribute("class", "reqdottedline"); ;
        return false;
    }
    if (addr == '') return true;
    var invalidChars = '\/\'\\ ";:?!()[]\{\}^|';
    for (i = 0; i < invalidChars.length; i++) {
        if (addr.indexOf(invalidChars.charAt(i), 0) > -1) {
            document.getElementById('divEmail').setAttribute("class", "reqdottedline"); ;
            return false;
        }
    }
    for (i = 0; i < addr.length; i++) {
        if (addr.charCodeAt(i) > 127) {
            document.getElementById('divEmail').setAttribute("class", "reqdottedline"); ;
            return false;
        }
    }

    var atPos = addr.indexOf('@', 0);
    if (atPos == -1) {
        document.getElementById('divEmail').setAttribute("class", "reqdottedline"); ;
        return false;
    }
    if (atPos == 0) {
        document.getElementById('divEmail').setAttribute("class", "reqdottedline"); ;
        return false;
    }
    if (addr.indexOf('@', atPos + 1) > -1) {
        document.getElementById('divEmail').setAttribute("class", "reqdottedline"); ; ;
        return false;
    }
    if (addr.indexOf('.', atPos) == -1) {
        document.getElementById('divEmail').setAttribute("class", "reqdottedline"); ;
        return false;
    }
    if (addr.indexOf('@.', 0) != -1) {
        document.getElementById('divEmail').setAttribute("class", "reqdottedline"); ;
        return false;
    }
    if (addr.indexOf('.@', 0) != -1) {
        document.getElementById('divEmail').setAttribute("class", "reqdottedline"); ;
        return false;
    }
    if (addr.indexOf('..', 0) != -1) {
        document.getElementById('divEmail').setAttribute("class", "reqdottedline"); ;
        return false;
    }
    var suffix = addr.substring(addr.lastIndexOf('.') + 1);
    if (suffix.length != 2 && suffix != 'com' && suffix != 'net' && suffix != 'org' && suffix != 'edu' && suffix != 'int' && suffix != 'mil' && suffix != 'gov' & suffix != 'arpa' && suffix != 'biz' && suffix != 'aero' && suffix != 'name' && suffix != 'coop' && suffix != 'info' && suffix != 'pro' && suffix != 'museum') {
        document.getElementById('divEmail').setAttribute("class", "reqdottedline"); ;
        return false;
    }
    return true;
}

function ValidateEmails() {
    var emailValue = trim(document.getElementById('txtEmail').value);
    document.getElementById('txtEmail').value = emailValue;
    if (validateEmail(emailValue) == false) {


        document.getElementById('divEmail').setAttribute("class", "reqdottedline");

        return false
    }
    else {
        document.getElementById('divEmail').setAttribute("class", "divrow dottedline");
    }
    return true
}

function ValidateCityOrganisation() {
    //debugger;
    var orgNameLen = document.getElementById('find_business_name').value.length;
    var cityNameLen = document.getElementById('find_town').value.length;

    if (document.getElementById('find_business_name').value == "e.g. Serenata Flowers") {
        document.getElementById('find_business_name').value = '';
    }
    if (document.getElementById('find_town').value == "e.g London") {
        document.getElementById('find_town').value = '';
    }

    var obj = new RegExp("[a-zA-Z0-9 ]");
    if (orgNameLen > 0 && cityNameLen > 0) {
        if (obj.test(document.getElementById('find_business_name').value)) {
            if (obj.test(document.getElementById('find_town').value))
                return true;
            else {
                //   alert("Organisation Name invalid");
                document.getElementById('DivFindBusiness').setAttribute("class", "divrow dottedline");
                document.getElementById('divCityTown').setAttribute("class", "recipentslbl100 reqdottedline");
                return false;
            }
        }
        else {
            // alert("City Name invalid");
            document.getElementById('DivFindBusiness').setAttribute("class", "reqdottedline");
            if (document.getElementById('find_town').value == 0) {
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
            document.getElementById('DivFindBusiness').setAttribute("class", "reqdottedline");
        }
        else {
            document.getElementById('DivFindBusiness').setAttribute("class", "divrow dottedline");
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


function ValidateBusinessFields(obj) {
    var fieldValue = document.getElementById(obj.id).value
    if (obj.id == "find_business_name") {
        if (fieldValue != "") {
            document.getElementById('DivFindBusiness').setAttribute("class", "divrow dottedline");
        }
        else {
            document.getElementById('DivFindBusiness').setAttribute("class", "reqdottedline");
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


//Added validation for UK Postcode format.
//Date:12-July-2012
function postcode_validateoOnBlur() {
    var postcode = document.getElementById('find_postcode').value;
    if (postcode != "") {
        //var regPostcode = /^([a-zA-Z]){1}([0-9][0-9]|[0-9]|[a-zA-Z][0-9][a-zA-Z]|[a-zA-Z][0-9][0-9]|[a-zA-Z][0-9]){1}([ ])([0-9][a-zA-z][a-zA-z]){1}$/;
        var regPostcode = UKpostcode_validate(postcode)
        if (regPostcode == false) {            
            document.getElementById('spnTxtPostCode').style.display = 'block';
            document.getElementById('spnTxtPostCode').textContent = "Please enter valid postcode.";
            document.getElementById('spnTxtPostCode').style.display = 'block';
            document.getElementById('spnTxtPostCode').setAttribute("class", "redtxt");
            document.getElementById('spnTxtPostCode').style.color = "#fff";
            document.getElementById('DivFindpostcode').setAttribute("class", "reqdottedline");
            document.getElementById('DivAddressResult').style.display = 'none';
            document.getElementById('DivAddressResultMsg').style.display = 'none';
            return false;
        }
        else {
            document.getElementById('DivFindpostcode').setAttribute("class", "divrow dottedline");
            document.getElementById('spnTxtPostCode').style.display = 'none';
            return true;
        }
    }
    else {
        document.getElementById('DivFindpostcode').setAttribute("class", "divrow dottedline");
        document.getElementById('spnTxtPostCode').style.display = 'none';
    }
}



function postcode_validate() {
    var postcode = document.getElementById('find_postcode').value;
    if (postcode != "") {
        var regPostcode = UKpostcode_validate(postcode) ///^([a-zA-Z]){1}([0-9][0-9]|[0-9]|[a-zA-Z][0-9][a-zA-Z]|[a-zA-Z][0-9][0-9]|[a-zA-Z][0-9]){1}([ ])([0-9][a-zA-z][a-zA-z]){1}$/;
        if (regPostcode == false) {
            document.getElementById('spnTxtPostCode').textContent = "Please enter valid postcode.";
            document.getElementById('spnTxtPostCode').style.display = 'block';
            document.getElementById('spnTxtPostCode').setAttribute("class", "redtxt");
            document.getElementById('spnTxtPostCode').style.color = "#fff";
            document.getElementById('DivFindpostcode').setAttribute("class", "reqdottedline");
            document.getElementById('DivAddressResult').style.display = 'none';
            document.getElementById('DivAddressResultMsg').style.display = 'none';
            return false;
        }
        else {
            document.getElementById('DivFindpostcode').setAttribute("class", "divrow dottedline");
            document.getElementById('spnTxtPostCode').style.display = 'none';
            return true;
        }
    }
    document.getElementById('spnTxtPostCode').style.display = 'block';
    document.getElementById('spnTxtPostCode').textContent = "Please enter the postcode.";
    document.getElementById('spnTxtPostCode').style.display = 'block';
    document.getElementById('spnTxtPostCode').setAttribute("class", "redtxt");
    document.getElementById('spnTxtPostCode').style.color = "#fff";
    document.getElementById('DivFindpostcode').setAttribute("class", "reqdottedline");
    document.getElementById('DivAddressResult').style.display = 'none';
    document.getElementById('DivAddressResultMsg').style.display = 'none';
    return false;
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