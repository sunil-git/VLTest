/*
Cross-browser Modal Popup using Javascript (JQuery)
*/

//Modal popup background ID. 
//This value should be unique so that it does not conflict with other IDs in the page.
var _ModalPopupBackgroundID = 'backgroundPopup_XYZ_20110418_Custom';

function ShowModalPopup(modalPopupID) {

    //Setting modal popup window

    //Boolean to detect IE6.
    var isIE6 = (navigator.appVersion.toLowerCase().indexOf('msie 6') > 0);

    var popupID = "#" + modalPopupID;

    //Get popup window margin top and left
    var popupMarginTop = $(popupID).height() / 2;
    var popupMarginLeft = $(popupID).width() / 2;

    //Set popup window left and z-index
    //z-index of modal popup window should be higher than z-index of modal background
    $(popupID).css({
        'left': '50%',
        'z-index': 9999
    });

    //Special case for IE6 because it does not understand position: fixed
    if (isIE6) {
        $(popupID).css({
            'top': $(document).scrollTop(),
            'margin-top': $(window).height() / 2 - popupMarginTop,
            'margin-left': -popupMarginLeft,
            'display': 'block',
            'position': 'absolute'
        });
    }
    else {
        $(popupID).css({
            'top': '50%',
            'margin-top': -popupMarginTop,
            'margin-left': -popupMarginLeft,
            'display': 'block',
            'position': 'fixed'
        });
    }

    //Automatically adding modal background to the page.
    var backgroundSelector = $('<div id="' + _ModalPopupBackgroundID + '" ></div>');

    //Add modal background to the body of the page.
    backgroundSelector.appendTo('body');

    //Set CSS for modal background. Set z-index of background lower than popup window.
    backgroundSelector.css({
        'width': $(document).width(),
        'height': $(document).height(),
        'display': 'none',
        'background-color': '#555555',
        'position': 'absolute',
        'top': 0,
        'left': 0,
        'z-index': 9990
    });

    backgroundSelector.fadeTo('fast', 0.8);
}

function HideModalPopup(modalPopupID, navurl) {
    alert(navurl);
        //Hide modal popup window
        $("#" + modalPopupID).css('display', 'none');
        //Remove modal background from DOM.
        $("#" + _ModalPopupBackgroundID).remove();

        window.location = navurl + "&t=" + document.getElementById('hdnfilename').value;
        return true;

    }


    function useTheAddressIEntered(modalPopupID) {
        DisplayAddressFields();
        //Hide modal popup window
        $("#" + modalPopupID).css('display', 'none');
        //Remove modal background from DOM.
        $("#" + _ModalPopupBackgroundID).remove();
       
        
    }

    function useThisAddress(modalPopupID) {

        var element = document.getElementById('lstSuggestedAdd');
        var addressID = element.selectedIndex;
        if (addressID >= 1) {
          //  document.getElementById("addressValid").value = "1";
            //Hide modal popup window
            $("#" + modalPopupID).css('display', 'none');
            //Remove modal background from DOM.
            $("#" + _ModalPopupBackgroundID).remove();
            // alert("addressValid = " + document.getElementById("addressValid").value);
            return true;
        }
        else {
            alert('Please select an address from list.');
            return false;
        }

    }

    function popupSuggestedAddress()
    {
        var isAddressVerify = document.getElementById("addressVerify").value;
        var hasApiAdd = document.getElementById("hasApiAddress").value;
        if (isAddressVerify == "0") 
        {
            if (hasApiAdd == "1") 
            {
                document.getElementById('divAddressSuccess').style.display = 'block';
                document.getElementById('divAddressFailure').style.display = 'none';
            }
            else 
            {
                document.getElementById('divAddressSuccess').style.display = 'none';
                document.getElementById('divAddressFailure').style.display = 'block';
            }
          
            ShowModalPopup('dvPopup');
            return true;
        }
    }
   