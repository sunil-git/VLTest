//var rootUrl = 'https://checkout.serenata.co.uk/';

function ddrivetip(a, b, c) {
}
function hideddrivetip() {
}

function NextUrlIndex() {
    var floatNum = Math.random() * 50000;
    var intNum = Math.floor(floatNum);
    return intNum;
}

function myAlert(str) {
    // alert(str);
}

function AddEvent(element, eventType, handler, useCapture) {
    var attached = false;

    if (element != null) {
        if (element.addEventListener) {
            element.addEventListener(eventType, handler, useCapture);
            attached = true;
        }
        else if (element.attachEvent) {
            element.attachEvent("on" + eventType, handler);
            attached = true;
        }
        else {
            attached = false;
        }
    }

    return attached;
}
//
// common
//
function CheckPostcode(postcode) {
    var format = /^([A-PR-UWYZ]\d\d?\d[ABD-HJLNP-UW-Z]{2}|[A-PR-UWYZ][A-HK-Y]\d\d?\d[ABD-HJLNP-UW-Z]{2}|[A-PR-UWYZ]\d[A-HJKSTUW]\d[ABD-HJLNP-UW-Z]{2}|[A-PR-UWYZ][A-HK-Y]\d[A-HJKRSTUW]\d[ABD-HJLNP-UW-Z]{2}|GIR0AA)$/;

    // Format the postcode
    postcode = postcode.replace(' ', '');
    postcode = postcode.toUpperCase();

    return (format.test(postcode)) ? true : false;
}

//
// common
//
function GetXmlHttpObject(handler) {
    var isIE = (navigator.userAgent.indexOf('MSIE') >= 0) ? 1 : 0;
    var isIE5 = (navigator.appVersion.indexOf("MSIE 5.5") != -1) ? 1 : 0;
    var isOpera = ((navigator.userAgent.indexOf("Opera6") != -1) || (navigator.userAgent.indexOf("Opera/6") != -1)) ? 1 : 0;
    //netscape, safari, mozilla behave the same??? 
    var isNetscape = (navigator.userAgent.indexOf('Netscape') >= 0) ? 1 : 0;
    var localXmlHttp = null;

    // Depending on the browser, try to create the xmlHttp object 
    if (isIE) {
        // The object to create depends on version of IE 
        // If it isn't ie5, then default to the Msxml2.XMLHTTP object 
        var strObjName = (isIE5) ? 'Microsoft.XMLHTTP' : 'Msxml2.XMLHTTP';

        //Attempt to create the object 
        try {
            localXmlHttp = new ActiveXObject(strObjName);

            if (handler != null) {
                localXmlHttp.onreadystatechange = handler;
            }
        }
        catch (e) {
            // Object creation error 
            alert('IE detected, but object could not be created. Verify that active scripting and activeX controls are enabled');
            return;
        }
    }
    else if (isOpera) {
        // Opera has some issues with xmlHttp object functionality 
        //alert('Opera detected. The page may not behave as expected.'); 
        //return; 
        return Sarissa.getXmlHttpRequest();
    }
    else {
        // Mozilla | Netscape | Safari 
        localXmlHttp = new XMLHttpRequest();

        if (handler != null) {
            localXmlHttp.onload = handler;
            localXmlHttp.onerror = handler;
        }
    }

    // Return the instantiated object 
    return localXmlHttp;
}
function setClassForElement(elementname, classname) {
    element = document.getElementById(elementname);
    element.className = classname;
}

function setChecked(elem, check) {
    var element = document.getElementById(elem);
    element.checked = check;
}

function GetXmlObject() {
    var msie = (window.ActiveXObject) ? true : false;
    var moz = (document.implementation && document.implementation.createDocument) ? true : false;
    if (moz) {
        //return document.implementation.createDocument("", "", null);
        return new Sarissa.getDomDocument();
    }
    else if (msie) {
        return new ActiveXObject('Microsoft.XMLDOM');
    }
}
function setDisplayState(elementName, displayStyle) {
    var element = document.getElementById(elementName);
    if (element) {
        if (displayStyle) {
            displayStr = 'block';
        }
        else {
            displayStr = 'none';
        }
        element.style.display = displayStr;
    }
    else {
        //alert(elementName + " doesn't exist");
    }
}

function setFieldValue(field, value) {
    var element = document.getElementById(field);
    element.value = value;
}

function setInnerHTML(field, value) {
    var element = document.getElementById(field);
    element.innerHTML = value;
}

function GetElementFromEventData(e) {
    return e.target || e.srcElement;
}

function AddEventToElement(field, eventName, func) {
    var element = document.getElementById(field);
    if (element) {
        var attached = AddEvent(element, eventName, func);
    }
}

function DisplayErrorMessageOnRow(rowID, messageFieldID, message) {
    ///alert(rowID);
    var element = document.getElementById(rowID);
    element.className = 'error';

    var element = document.getElementById(messageFieldID);
    element.className = 'errorText';
    element.innerHTML = "::" + message;
}

function getFieldValue(fieldName) {
    var retVal = '';
    var element = document.getElementById(fieldName);
    if (element) {
        retVal = element.value.trim();
    }
    else {
        retVal = '';
    }
    return retVal;
}

function GetMandatoryFieldValue(currentFieldID) {
    var element = document.getElementById(currentFieldID);

    var value = '';
    if (element == null) {
        value = '';
        alert(currentFieldID);
    }
    else {

        value = element.value;
    }

    return value;
}


function GetMandatoryFieldIDByState(state, currentFieldID, normalFieldID, errorFieldID, rowID, message1, message2) {
    var element = document.getElementById(currentFieldID);
    var value = element.value;

    if (state == false) {
        element.className = 'error';
        var element = document.getElementById(errorFieldID);
        element.className = 'errorText';
        element.innerHTML = message1;

    }
    else {
        element.className = '';
        var element = document.getElementById(errorFieldID);
        element.className = 'infoText';
        element.innerHTML = message2;

    }
    currentFieldID = normalFieldID;
    return currentFieldID;
}
function CheckEnter(e) {
    var characterCode = null;

    // If which property of event object is supported (NN4)
    if (e && e.which) {
        // Character code is contained in NN4's which property
        characterCode = e.which;
    }
    else {
        // Character code is contained in IE's keyCode property
        characterCode = e.keyCode;
    }

    // If generated character code is equal to ascii 13 (if enter key)
    if (characterCode == 13) {
        return true;
    }
    else {
        return false;
    }
}

function GetMandatoryFieldID(currentFieldID, normalFieldID, errorFieldID, rowID, message1, message2) {
    var element = document.getElementById(currentFieldID);
    var value = element.value;


    if (value == '') {
        element.className = 'error';
        var element = document.getElementById(errorFieldID);
        element.className = 'errorText';
        element.innerHTML = message1;

    }
    else {
        element.className = '';
        var element = document.getElementById(errorFieldID);
        element.className = 'infoText';
        element.innerHTML = message2;

    }
    currentFieldID = normalFieldID;
    return currentFieldID;
}

function DisplayInfoMessage(fieldID, messageID, message) {
    var element = document.getElementById(fieldID);
    element.className = '';

    var element = document.getElementById(messageID);
    element.className = 'infoText';
    element.innerHTML = message;
}

function DisplayErrorMessage(messageFieldID, message) {
    var element = document.getElementById(messageFieldID);
    element.className = 'errorText';
    element.innerHTML = message;
}
function DisplayInfoMessageOnRow(rowID, fieldID, messageID, message) {
    ////alert(rowID);
    var element = document.getElementById(rowID);
    element.className = 'tr';
    if (fieldID.length > 0) {
        var element = document.getElementById(fieldID);
        element.className = '';
    }
    var element = document.getElementById(messageID);
    element.className = 'infoText';
    element.innerHTML = message;
}

function ShowRowInError(rowID) {
    var element = document.getElementById(rowID);
    element.style.background = '#f9c2c2';
    element.style.border = '1px solid red';

}
function setFocus(fieldID) {
    element = document.getElementById(fieldID);
    element.focus();
}
function ShowRowValid(rowID) {
    var element = document.getElementById(rowID);
    element.style.background = '';
    element.style.border = '';
}
function DisplayErrorMessageOnRowNew(pageName, fieldID, rowID, messageFieldID, message) {
    var element = document.getElementById(rowID);
    element.className = 'error';

    var element = document.getElementById(messageFieldID);
    element.className = 'errorText';
    element.innerHTML = "::" + message;
}
function ShowValid(rowID) {
    var element = document.getElementById(rowID);
    element.style.background = '';
    element.style.border = '';
}