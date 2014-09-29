

function GetXmlHttpObject(handler) {
    var isIE = (navigator.userAgent.indexOf('MSIE') >= 0) ? 1 : 0;
    var isIE5 = (navigator.appVersion.indexOf("MSIE 5.5") != -1) ? 1 : 0;
    var isOpera = ((navigator.userAgent.indexOf("Opera6") != -1) || (navigator.userAgent.indexOf("Opera/6") != -1)) ? 1 : 0;
    //netscape, safari, mozilla behave the same??? 
    var isNetscape = (navigator.userAgent.indexOf('Netscape') >= 0) ? 1 : 0;
    var localXmlHttp = null;

    // Depending on the browser, try to create the xmlHttp object 
    if (isIE) {
        //alert('isIE');
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
        //alert('isOpera');
        // Opera has some issues with xmlHttp object functionality 
        //alert('Opera detected. The page may not behave as expected.'); 
        //return; 
        return Sarissa.getXmlHttpRequest();
    }
    else {
        //alert('isSafari');
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

function createHtmlForm() {

}


