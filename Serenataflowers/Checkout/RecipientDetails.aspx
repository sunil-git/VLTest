<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecipientDetails.aspx.cs"
    Inherits="Serenataflowers.Checkout.RecipientDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../Controls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../Controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Literal ID="ltTitle" runat="server"></asp:Literal>
    <title id="Title1" runat="server" visible="false"></title>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" name="viewport" />
    <link type="text/css" rel="stylesheet" href="../styles/m_style.css" />
     <style type="text/css">
        *html #dvPopup
        {
            top: expression(eval(document.documentElement.scrollTop)) !important;
        }
    </style>
    </head>
    <script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
    <script type="text/javascript" src="../Scripts/step1functions.js"></script>
    <script src="../Scripts/aesEncryption.js" type="text/javascript"></script>
    <script type="text/javascript">
        //window.history.forward(1);

        function sendXmlHttpRequest(fNameOrId, strType) {
            var xmlhttp = GetXmlHttpObject();
            if (xmlhttp == null) { alert("Your browser does not support AJAX!"); return; }
            try {
                var url = document.getElementById('hdnEncryptionUrl').value;
                var params = "?fn=" + fNameOrId + "&t=" + strType;
                url = url + params;
                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        //debugger;
                        if (strType == "orderxml") {
                            //stroe the encryptedOrderxml in to Session
                            var httpResponse = xmlhttp.responseText;
                            document.getElementById('order').value = httpResponse;
                            formSubmit();
                        }
                        else if (strType == "configxml") {
                            //stroe the encryptedConfigXml in to Session
                            //debugger;  
                            var httpResponse = xmlhttp.responseText;
                            //PageMethods.getEncryptedConfig(httpResponse, encConfigXmlSuccess);
                            document.getElementById('config').value = httpResponse;

                        }
                    }
                }
                xmlhttp.open("GET", url, true);
                xmlhttp.send(null);
            }
            catch (err) {
                document.writeln("ERROR: " + err.description);

            }
        }

        function encrypt() {
            //debugger;
            //Get the orderxml file name
            var orderXml = document.getElementById('hdnOrderXmlFileName').value;
            //Send the orderxml to asp page for encryption
            sendXmlHttpRequest(orderXml, "orderxml");
        }
        function encOrderIdSuccess() {
            //Send the configxml to asp page for encryption
            //alert(configXmlFileName);
            //debugger;
            var configXmlFileName = document.getElementById('hdnconfilename').value;

            sendXmlHttpRequest(configXmlFileName, "configxml");

        }

        function formSubmit() {
            //debugger;
            document.forms["frmOrder"].submit();
        }
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>
    <script language="javascript" type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/modalpopup.js"></script>
    <script language="javascript" type="text/javascript">
        var rootUrl = '';
        // added for address verification
        function SetaddressVerifyVal(val) {
            document.getElementById("addressVerify").value = val;
        }

        function countCharacters(myelement) {

            var hidVal = document.getElementById('hdnMaxLength').value;
            var max_chars = parseInt(hidVal, 10);

            var counter = document.getElementById('mycounter');
            field = document.getElementById(myelement).value;
            field_length = field.length;

            if (field_length > max_chars) {
                // if too long...trim it!
                document.getElementById(myelement).value = field.substring(0, max_chars);
                counter.innerHTML = "You have reached the limit of " + max_chars + " characters for this field";
            }
            else {
                // otherwise, update 'characters left' counter
                // Here we Calculate remaining characters  
                remaining_characters = max_chars - field_length;
                // Now Update the counter on the page 
                counter.innerHTML = remaining_characters + " characters left.";
            }
        }

    </script>
     <script type="text/javascript">
         var IsContinuePaymentClick = false;
         function CapturePlusCallback(uid, response) {
             try {
                 CheckValidPostcode(response[5].FormattedValue);
                 // user has selected an address from Capture+   
                 document.getElementById('txtOrganisation_field').value = response[3].FormattedValue;
                 document.getElementById('txtAddressLine1').value = response[0].FormattedValue;
                 document.getElementById('txtStreet_field').value = response[1].FormattedValue;
                 document.getElementById('txtDistrict_field').value = response[2].FormattedValue;
                 document.getElementById('txtTownCity').value = response[4].FormattedValue;
                 document.getElementById('txtPostCode').value = response[5].FormattedValue;
                 document.getElementById('txtCountry_field').value = response[15].FormattedValue;
                 // document.getElementById('divAddressFields').style.display = "block";

                 //Uncheck the Same as Invoice checkbox when user select address from Address Finder Section
                 document.getElementById('same_as_delivery').checked = false;

             } catch (e) {

             }
         }

         function CheckValidPostcode(postCode) {
             try {
                 IsContinuePaymentClick = false;
                 if (postCode == "") {
                     revealModal('modalPage');
                     postCode = document.getElementById('txtPostCode').value;
                     IsContinuePaymentClick = true;
                 }
                 var ordID = document.getElementById('hdnDecrptedOrderId').value;
                 PageMethods.CheckValidPostcode(postCode, ordID, CheckValidPostcodeSucess);
                 return false;

             } catch (e) {

             }
         }

         function CheckValidPostcodeSucess(ResultString) {
             if (ResultString == true) {
                 document.getElementById('divPostCodeErrorMessage').style.display = "none";
                 DisplayAddressFields();
                 if (IsContinuePaymentClick) {
                     var result = ValidateAddressFields();
                     if (result) {
                         var button = document.getElementById('continue_bottom');
                         __doPostBack(button.name, "OnClick");

                     }
                     else {
                         hideModal('modalPage');
                     }
                 }
             }
             else {
                 hideModal('modalPage');
                 //alert('We are currently unable to deliver to this postcode on the selected delivery date. \nPlease accept our apologies for the inconvenience caused.');
                 document.getElementById('divPostCodeErrorMessage').style.display = "block";
                 document.getElementById('divPostCodeErrorMessage').scrollIntoView();
                 if (!IsContinuePaymentClick) {
                     if (document.getElementById('txtOrganisation_field').style.display == "none") {
                         HideAddressFields();
                     }
                     else {
                         clearControls();
                     }
                 }
                 else {
                     clearControls();
                 }

             }

             // alert(ResultString);
         }

         function clearControls() {
             document.getElementById('txtOrganisation_field').value = '';
             document.getElementById('txtAddressLine1').value = '';
             document.getElementById('txtStreet_field').value = '';
             document.getElementById('txtDistrict_field').value = '';
             document.getElementById('txtTownCity').value = '';
             document.getElementById('txtPostCode').value = '';
             document.getElementById('txtCountry_field').value = '';
         }

         function hidePostCodeErrorDiv() {
             if (document.getElementById('divPostCodeErrorMessage').style.display = "block")
                 document.getElementById('divPostCodeErrorMessage').style.display = "none";

             if (document.getElementById('divDelivery').style.display = "block")
                 document.getElementById('chkBxDelivery_instruction_check').setAttribute("checked", "checked");

             if (document.getElementById('divGiftMessage').style.display = "block")
                 document.getElementById('chkBxCard_message_check').setAttribute("checked", "checked");

         }
     </script>
     <script language="javascript" type="text/javascript">
         function revealModal(divID) {
             document.getElementById(divID).style.display = "block";
         }

         function hideModal(divID) {
             document.getElementById(divID).style.display = "none";
         }
</script>


<body onload="encOrderIdSuccess();">
    <form id="form1" runat="server">
       <div id="modalPage" class="ModalProgressContainer" style="height:5000px; display:none;">
        <div class="ModalProgressContent">
        <img src="../images/ajax-loader.gif" alt="" />
        <div>Loading....</div>
        </div>
    </div>
    <asp:HiddenField ID="addressVerify" runat="server" Value="0" />
    <input type="hidden" runat="server" id="hdnDecrptedOrderId" />
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="dvPopup" style="display: none; width: 280px; height: auto; border: 4px solid #000000;
                background-color: #FFFFFF;">
                <div id="divAddressSuccess">
                    <div style="text-align: right; position: relative;">
                        <strong><a href="#" style="text-decoration: none;font-size:smaller" onclick="useTheAddressIEntered('dvPopup'); hidePostCodeErrorDiv(); return false;">
                            close(x)</a></strong></div>
                    <div class="infotext" style="padding: 5px 5px 5px 5px; width: 270px; text-align: justify">
                        <span style="color: Red">* </span>We haven't been able to verify your entered address, please select one from the list suggested, or continue to use the one entered.</div>
                    <div style="padding: 5px 5px 0px 5px">
                        <asp:DropDownList runat="server" ID="lstSuggestedAdd" Width="270px"></asp:DropDownList>
                        
                    </div>
                    <div class="clearleft">
                    </div>
                    <div style="text-align: center; padding-top: 5px;">
                        <asp:ImageButton ID="UseThisAddress" runat="server" ImageUrl="~/images/usethisaddress.bmp"
                            OnClick="UseThisAddress_Click" OnClientClick="javascript:return useThisAddress('dvPopup');" />
                        <asp:ImageButton ID="UseTheAddressIEntered" runat="server" ImageUrl="~/images/usetheaddressIentered.bmp"
                            OnClick="UseTheAddressIEntered_Click" OnClientClick="javascript:return useTheAddressIEntered('dvPopup');" />
                         <%--<a style="margin-left:17px" class="btn" href="javascript:useThisAddress('dvPopup');"><span>Use this address</span></a>              --%>
                    </div>
                    <div class="infotext" style="padding: 5px 5px 5px 5px; width: 270px; text-align: justify">
                        <span style="color: Red">* </span>You hereby take full responsibility that the address
                        is correct. If our courier company can't find the address Serenata is not liable
                        for any compensation.</div>
                    <div class="clear">
                    </div>
                </div>
                <div id="divAddressFailure" style="display: none">
                    <div style="width: 270px; height: auto; padding: 5px 5px 5px 5px; color: Red; font-size: smaller;
                        text-align: justify">
                        We haven't been able to verify your address and by clicking ok you hereby take full
                        responsibility that the address is correct. If our courier company can't find the
                        address Serenata is not liable for any compensation.
                        <div class="spacer">
                        </div>
                    </div>
                    <div style="text-align: center;">
                        <asp:ImageButton ID="ImgBtnYes" runat="server" ImageUrl="~/images/yes.bmp" OnClick="ImgBtnYes_Click"
                            OnClientClick="javascript:return useTheAddressIEntered('dvPopup');" />
                        <asp:ImageButton ID="ImgBtnNo" runat="server" ImageUrl="~/images/noiwillcorrectmyaddress.bmp"
                            OnClientClick="javascript:return useTheAddressIEntered('dvPopup');" />
                        <%--           <a style="margin-left:17px" class="btn" href=""><span>Yes</span></a>
           <a style="margin-left:17px" class="btn" href=""><span>No, I will correct my address</span></a>--%>
                        <%--           <br /><br />--%>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="continue_bottom" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="page_wrapper">
        <div id="content_normal">
            <!--Header DIV Start-->
            <div id="header">
                <uc1:Header ID="Header1" runat="server" EnableUrl="false" />
            </div>
            <!--Header DIV End-->
            <div class="clearleft">
            </div>
            <asp:HiddenField ID="hdnSelectedAddressValue" runat="server" />
            <div class="divrow">
                <div class="recipentslbl100">
                    <h1 style="font-size: 18px;">
                        2. Please enter recipients details</h1>
                </div>
            </div>
            <div id="divFirstName" class="divrow dottedline" style="height: 60px;">
                        <div runat="server" id="recipname">
                            <span class="redtxt">*</span> Recipient's name:</div>
                        <div style="width: 55%;">
                            <asp:TextBox MaxLength="100" ID="txtFirstName" Text="" runat="server" Width="" onfocus="showMessage()"
                                onblur="validateField(this)" /><span id="spnFirstName" class="infotext" style="display: inline;
                                    white-space: normal; font-size: 11px; margin-left: 4px;"></span>
                        </div>
                    </div>
                     <div class="dottedline" id="divfAdd" runat="server">
                        <!--Check box for same as delivery address-->
                        <div style="padding-top:5px; padding-bottom:5px;"> <span class="redtxt">*</span> Full postcode:</div>
                        <div id="DivFindpostcode" runat="server" style="height: 45px;">
                            <link rel="stylesheet" type="text/css" href="https://services.postcodeanywhere.co.uk/css/captureplus-1.34.min.css?key=ed89-rd15-zr57-bx68">
                            <script type="text/javascript" src="https://services.postcodeanywhere.co.uk/js/captureplus-1.34.min.js?key=ed89-rd15-zr57-bx68&amp;app=10891"></script>
                            <div id="ed89rd15zr57bx6810891">
                            </div>
                        </div>
                    </div>
                      <div class="divrow dottedline" id="divPostCodeErrorMessage" style="display:none;">
                            <span id="spnMultiFP"  runat="server" style="color:red;padding:2px;font-size:smaller; border:1px solid red;text-align:center;display:block;margin-top:3px;">We are currently unable to deliver to this postcode on the selected delivery date. <br />Please accept our apologies for the inconvenience caused.</span>
                        <%-- <span class="redtxt" id="spanPostCodeErrorMessage" runat="server" style="color: Red">
                         
                         </span>--%>
                      </div>
                    <div class="divrow dottedline">
                        <div id="divsame_as_delivery" runat="server" style="width:100%; height:25px; padding-top:5px">
                            <asp:CheckBox ID="same_as_delivery" runat="server" Text="same as invoice address"
                                OnCheckedChanged="same_as_delivery_CheckedChanged" onclick="hidePostCodeErrorDiv();" AutoPostBack="true" /></div>
                    </div>
            <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    
                </ContentTemplate>
                <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="ddlAddressPostCodes" EventName="SelectedIndexChanged" /
                    <asp:AsyncPostBackTrigger ControlID="same_as_delivery" EventName="CheckedChanged" />
                </Triggers>
            </asp:UpdatePanel>--%>
            <asp:UpdatePanel ID="uPanelDivAddressFields" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hasApiAddress" runat="server" Value="0" />
                   
                    <%--<div id="divPostCodes" runat="server">
                        <div id="divRPostCode" class="divrow dottedline" style="height: 65px;">
                            <div id="divFPost" runat="server">
                                <span class="redtxt">*</span> Full postcode:
                            </div>
                            <div>
                                <asp:TextBox MaxLength="100" runat="server" ID="txtfind_postcode" Text="e.g. SE1 0HS"
                                    size="13" onclick="this.value='';" onblur="postcode_validateoOnBlur()" />
                                <span id="spnTxtPostCode" class="infotext" style="display: inline; white-space: normal;
                                    font-size: 11px; margin-left: 4px;"></span>
                                <br />
                                <asp:Button ID="imgBtnFind_residential" runat="server" Text="" OnClick="imgBtnFind_residential_Click"
                                    CssClass="greenbtn" OnClientClick="javascript:return postcode_validate();" />
                            </div>
                        </div>
                        <div class="divrow dottedline">
                            <div id="divpostdesc" runat="server">
                            </div>
                            <div>
                                OR, if you do not know the postcode,
                                <br />
                                <a href="#" onclick="javascript:return DisplayAddressFields();">Enter address Manually</a>
                                <br />
                                <b>Tip:</b> Sending flower to a Business or place of work? use our <a href="#" onclick="javascript:return DisplayBusinessLocator();">
                                    Business Locator.</a>
                            </div>
                        </div>
                    </div>
                    <div id="divBusinessLocator" style="display: none" runat="server">
                        <div id="divBOLocotor" class="divrow dottedline">
                            <div id="divBlocator" runat="server">
                                <span class="redtxt">*</span> Business/Organisation:
                            </div>
                            <div>
                                <asp:TextBox MaxLength="100" runat="server" ID="txtBusinessLocaterOrg" Text="e.g. Serenata Flowers"
                                    onclick="this.value='';" onblur="ValidateBusinessFields(this)" />
                            </div>
                        </div>
                        <div class="divrow dottedline">
                            <div id="divCityTown" class="recipentslbl100">
                                <div id="divctwn" runat="server">
                                    <span class="redtxt">*</span> City,town or postcode:
                                </div>
                                <div>
                                    <asp:TextBox MaxLength="100" runat="server" ID="txtBusinessLocaterPostCode" size="11"
                                        Text="e.g London" onclick="this.value='';" onblur="ValidateBusinessFields(this)"
                                        onkeyup="ValidtePostCodefields(this)" />
                                </div>
                            </div>
                            <div>
                            </div>
                            <div>
                                <asp:Button runat="server" ID="imgBtnBusinessLocFindPC" CssClass="greenbtn" Text=""
                                    OnClick="imgBtnFind_residential_Click" OnClientClick="javascript:return ValidateCityOrganisation();" />
                            </div>
                        </div>
                        <div class="divrow dottedline">
                            <div id="divbdesc" runat="server">
                                OR, if you do not know the postcode,
                                <br />
                                <a href="#" id="lnkBtnresidential_manual_link" onclick="javascript:return DisplayAddressFields();">
                                    Enter address Manually</a>
                            </div>
                            <div>
                                <b>Tip:</b> Sending flower to a home address? use our <a href="#" id="lnkBtnbusiness_locator_link"
                                    onclick="javascript:return HideAddressFields();">Residential Property Locator.</a>
                            </div>
                        </div>
                    </div>
                    <div class="divrow dottedline" id="divPostCode_Address" runat="server" style="display: none">
                        <div id="divSelectAddr" runat="server">
                            Select address:
                        </div>
                        <div>
                            <%--onclick="postthis(this)">
                            <asp:DropDownList ID="ddlAddressPostCodes" EnableViewState="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlAddressPostCodes_SelectedIndexChanged" runat="server"
                                Width="100%" />
                        </div>
                    </div>--%
                    <div class="divrow dottedline" id="divSelectAddressMsg" runat="server" style="display: none">
                        <div id="divPselct" runat="server">
                        </div>
                        <div>
                            <span class="infotext" id="AddressResultMsg" runat="server" style="color: Orange">Please
                                select an address from the list above</span>
                        </div>
                    </div>--%>
                    <div id="divAddressFields" style="display: none" runat="server">
                        <div class="divrow dottedline">
                            <div>
                                Business/Organisation:&nbsp;
                            </div>
                            <div>
                                <asp:TextBox MaxLength="100" ID="txtOrganisation_field" runat="server" />
                            </div>
                        </div>
                        <div id="divAddressLine1" class="divrow dottedline">
                            <div>
                                <span class="redtxt">*</span> Address line 1:&nbsp;
                            </div>
                            <div>
                                <asp:TextBox MaxLength="100" ID="txtAddressLine1" runat="server" onfocus="checkAbove(this.id)"
                                    onblur="validateField(this)" onchange="SetaddressVerifyVal('0');" />
                                <span class="redtxt" style="display: none">Enter address line1</span>
                            </div>
                        </div>
                        <div class="divrow dottedline">
                            <div>
                                Address line 2:&nbsp;
                            </div>
                            <div>
                                <asp:TextBox MaxLength="100" runat="server" ID="txtStreet_field" />
                            </div>
                        </div>
                        <div class="divrow dottedline">
                            <div>
                                Address line 3:&nbsp;
                            </div>
                            <div>
                                <asp:TextBox MaxLength="100" runat="server" ID="txtDistrict_field" />
                            </div>
                        </div>
                        <div id="divTownCity" class="divrow dottedline">
                            <div>
                                <span class="redtxt">*</span> City or town:&nbsp;
                            </div>
                            <div>
                                <asp:TextBox MaxLength="100" ID="txtTownCity" runat="server" onfocus="checkAbove(this.id)"
                                    onblur="validateField(this)" onchange="SetaddressVerifyVal('0');" />
                                <span class="redtxt" style="display: none" id="divCity">Enter city name</span>
                            </div>
                        </div>
                        <div class="divrow dottedline">
                            <div>
                                County:&nbsp;
                            </div>
                            <div>
                                <asp:TextBox MaxLength="100" ID="txtCounty_field" runat="server" />
                            </div>
                        </div>
                        <div id="divPostCode" class="divrow dottedline" style="height: 65px;">
                            <div>
                                <span class="redtxt">*</span> Postcode:&nbsp;
                            </div>
                            <div>
                                <asp:TextBox MaxLength="100" ID="txtPostCode" runat="server" onblur="validateField(this)"
                                    onkeyup="ValidtePostCodefields(this)" onchange="SetaddressVerifyVal('0');" />
                                <span class="redtxt" style="display: none; white-space: normal; font-size: 11px;
                                    margin-left: 4px;" id="divPostCode1">Please enter valid postcode</span>
                            </div>
                        </div>
                        <div class="divrow dottedline" id="divCountry_field">
                            <div>
                                <span class="redtxt">*</span> Country:&nbsp;
                            </div>
                            <div>
                                <asp:TextBox MaxLength="100" ID="txtCountry_field" runat="server" onblur="validateField(this)" />
                            </div>
                        </div>
                        <div class="divrow dottedline" id="divPrefer" runat="server">
                            Prefer to search for <a id="lnkBtnResidential_locator_link_from_manual" onclick="javascript:return HideAddressFields();"
                                href="#">residential property by postcode?</a>
                           <%-- <br />
                            Or use our <a id="" onclick="javascript:return DisplayBusinessLocator();" href="#">Business
                                locator by postcode.</a>--%>
                            <br />
                            <br />
                        </div>
                        <div class="divrow dottedline">
                            <div>
                                Recipient Phone number:&nbsp;
                            </div>
                            <div>
                                <asp:TextBox MaxLength="100" runat="server" ID="txtContact_field" />
                            </div>
                        </div>
                        <div class="divrow dottedline">
                            <div>
                                Delivery instructions:&nbsp;
                            </div>
                            <div>
                                <asp:CheckBox ID="chkBxDelivery_instruction_check" Checked="true" runat="server"
                                    onClick="javascript:return chkDelivaryChecked(this.checked);" />
                                <span class="infotext">include some delivery instructions</span>
                            </div>
                        </div>
                        <div class="divrow dottedline" id="divDelivery" style="display: block">
                            <div>
                              <%--   <span class="infotext">Please provide as much information as<br />
                                    possible to allow us to successfully<br />
                                    deliver your order. On time. First time.</span>--%>

                        <span class="infotext">Please select one of the options which 
                            the driver should follow 
                            if they are unable to deliver your order. 
                                    </span>
                            </div>
                            <div>
                                 <%--<textarea id="txtDelivery_instructions" rows="5" cols="20" runat="server" onblur="validateGiftMessage(this)"></textarea>--%>
                                
                                <asp:DropDownList ID="drpDelivery_Instructions" runat="server">
                                <asp:ListItem Text="Please select an option" Value="Please select an option" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Leave on doorstep" Value="Leave on doorstep"></asp:ListItem>
                                    <asp:ListItem Text="Leave in porch" Value="Leave in porch"></asp:ListItem>
                                    <asp:ListItem Text="Leave with neighbour" Value="Leave with neighbour"></asp:ListItem>
                                    <asp:ListItem Text="Leave at back of the property" Value="Leave at back of the property"></asp:ListItem>
                                    <asp:ListItem Text="Leave behind shed" Value="Leave behind shed"></asp:ListItem>
                                    <asp:ListItem Text="Leave in shed" Value="Leave in shed"></asp:ListItem>
                                    <asp:ListItem Text="Please knock hard" Value="Please knock hard"></asp:ListItem>
                                    <asp:ListItem Text="Please be patient" Value="Please be patien"></asp:ListItem>

                                </asp:DropDownList>
                                </div>
                        </div>
                        <div class="divrow dottedline">
                            <div>
                                Gift message:&nbsp;</div>
                            <div>
                                <asp:CheckBox ID="chkBxCard_message_check" Checked="true" runat="server" onClick="javascript:return chkGiftChecked(this.checked);" />
                                <span class="infotext">include gift message</span></div>
                        </div>
                        <div class="divrow dottedline" id="divGiftMessage" style="display: block">
                            <div>
                                <span class="infotext">Don't forget to include your name<br />
                                    (unless you fancy being all mysterious,<br />
                                    in which case we promise not to reveal<br />
                                    your true identity). </span>
                            </div>
                            <div>
                                <span id="giftmessageprompt">Please enter a gift message or check box above</span><br />
                                <textarea id="txtCard_message" name="card_message" rows="5" cols="20" runat="server" onkeyup="javascript:countCharacters(this.id)"
                                    onblur="validateGiftMessage(this)" ></textarea>
                                    <br />
                                    <div id="divmaxlenmessage" class="infotext" style="width:95%"><span id="mycounter" runat="server"></span></div>
                                    <input type="hidden" id="hdnMaxLength" runat="server" />
                            </div>
                        </div>
                        <div class="clearleft">
                        </div>
                        <br />
                        <div class="divrow">
                            <div>
                                <asp:ImageButton runat="server" ID="continue_bottom" alt="Continue to My details"
                                    ImageUrl="https://checkout.serenataflowers.com/images/continuepayment_08.gif"
                                    Width="281" Height="60" OnClick="continue_bottom_Click" OnClientClick="return CheckValidPostcode('');" />
                            </div>
                        </div>
                    </div>
                    <div class="dottedline divrow" id="divErrorMsg" runat="server" style="display: none">
                        <div>
                        </div>
                        <div>
                            <span id="spnErrorMsg" runat="server" class="infotext" />
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                   <%-- <asp:AsyncPostBackTrigger ControlID="ddlAddressPostCodes" EventName="SelectedIndexChanged" />--%>
                    <asp:AsyncPostBackTrigger ControlID="same_as_delivery" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="UseThisAddress" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ImgBtnYes" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="divrow">
                <div>
                    <span class="redtxt">*</span> <span style="font-size: 10px; color: #717171;">Required
                        field</span>
                </div>
            </div>
            <div class="clearleft">
            </div>
            <div id="Div1">
                <p class="lfloat fott">
                    <a class="btn" onclick="this.blur();" runat="Server" id="ancStp"><span>« Back</span>
                    </a>
                </p>
            </div>
            <div class="clearleft">
            </div>
            <br />
            <!--Footer DIV Start-->
            <div id="FooterDiv">
                <uc2:Footer ID="Footer1" runat="server" />
            </div>
            <!--Footer DIV End-->
        </div>
        <asp:HiddenField ID="hdnGetPriceByOrderId" runat="server" />
        <asp:HiddenField ID="hdnEncryptedOrder" runat="server" />
        <asp:HiddenField ID="hdnEncryptedConfig" runat="server" />
        <asp:HiddenField ID="hdnEncryptionUrl" runat="server" />
        <asp:HiddenField ID="hdnconfilename" runat="server" />
        <asp:UpdatePanel ID="updOrder" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hdnOrderXmlFileName" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    </form>
    <form id="frmOrder" action="http://94.199.191.253/payment.asp" method="post">
    <input type="hidden" id="order" name="order" />
    <input type="hidden" id="config" name="config" />
    </form>
</body>
</html>
