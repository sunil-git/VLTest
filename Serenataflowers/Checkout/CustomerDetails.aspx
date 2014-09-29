<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerDetails.aspx.cs" Inherits="Serenataflowers.Checkout.CustomerDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../Controls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../Controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Literal ID="ltTitle"  runat="server"></asp:Literal>
   <title id="Title1"  runat="server" visible="false"></title>
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <link type="text/css" rel="stylesheet" href="../Styles/m_style.css" />
    </head>
    <script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
     <style type="text/css">
        *html #dvPopup
        {
            top: expression(eval(document.documentElement.scrollTop)) !important;
        }
    </style>
    <script type="text/javascript" src="../Scripts/step2functions.js"></script>
    <script src="../Scripts/aesEncryption.js" type="text/javascript"></script>
    <script type="text/javascript" >

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
                        if (strType == "oId") {
                            //Replace Encrypted orderId in configXml File
                            var httpResponse = xmlhttp.responseText;
                            PageMethods.getEncryptedOrderId(httpResponse, encOrderIdSuccess);

                        }
                        //                        else if (strType == "orderxml") {
                        //                            //stroe the encryptedOrderxml in to Session
                        //                            var httpResponse = xmlhttp.responseText;
                        //                            PageMethods.getEncryptedOrder(httpResponse, encOrderXmlSuccess);

                        //                        }
                        else if (strType == "configxml") {
                            //stroe the encryptedConfigXml in to Session
                            var httpResponse = xmlhttp.responseText;
                            //PageMethods.getEncryptedConfig(httpResponse, encConfigXmlSuccess);


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
            //Check the configxml creation is happend or not.
            var boolConfigXml = document.getElementById('hdnBoolConfigXml').value;
            if (boolConfigXml == "false") {
                //Get the OrderID
                var orderId = document.getElementById('hdnOrderId').value;
                //send orderId for encryption
                sendXmlHttpRequest(orderId, "oId");
            }
            else {
                hideModal('modalPage');
            }
            //Get the orderxml
            //            var orderXml = document.getElementById('hdnOrderXmlFileName').value;
            //            //Send the orderxml to asp page for encryption
            //            sendXmlHttpRequest(orderXml, "orderxml");
        }
        function encOrderIdSuccess(configXmlFileName) {
            //Send the configxml to asp page for encryption
            //alert(configXmlFileName);
            //debugger;
            document.getElementById('hdnfilename').value = configXmlFileName;
            //sendXmlHttpRequest(configXmlFileName, "configxml");
            hideModal('modalPage');
        }
        //        function encOrderXmlSuccess(ResultString) {
        //            //alert(ResultString);
        //            //document.getElementById('order').value = ResultString;
        //        }
        function encConfigXmlSuccess(ResultString) {
            hideModal('modalPage');
            //alert(ResultString);
        }
    </script>
    <!--added for quantity verification start -->
    <script language="javascript" type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/modalpopup.js"></script>
      <script type="text/javascript">
          function chgQty(txtControlObj) {
              var qunatity = document.getElementById(txtControlObj.id).value;
              var priceId = txtControlObj.id.replace("txtQty", "lblPrice");
              var totalPriceId = txtControlObj.id.replace("txtQty", "lblTotal");
              var price = document.getElementById(priceId).innerHTML.replace("£", "");
              var totalPrice = formatCurrency(qunatity) * formatCurrency(price);
              document.getElementById(totalPriceId).innerHTML = "£" + formatCurrency(totalPrice);

          }
          function formatCurrency(num) {
              num = isNaN(num) || num === '' || num === null ? 0.00 : num;
              return parseFloat(num).toFixed(2);
          }
          function formatquntity(obj) {

              var p = obj;
              var pp = '';
              obj = obj.value;

              if (obj.length > 0) {
                  pp = '';
                  for (c = 0; c < obj.length; c++) {
                      if (isNaN(obj.charAt(c)) || obj.charAt(c) == ' ') {
                          //Assigned Default Value
                          pp = "1";
                          continue;
                      }
                      else {
                          pp = pp + obj.charAt(c);
                      }
                  }
                  p.value = pp;
              }
          }
    </script>
   
     <!--added for quantity verification end-->
<script language="javascript" type="text/javascript">
    function revealModal(divID) {
        document.getElementById(divID).style.display = "block";
    }

    function hideModal(divID) {
        document.getElementById(divID).style.display = "none";
    }
</script>
<body onload="encrypt();">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true"  runat="server">
    </asp:ScriptManager>
    <div id="dvPopup" style="display: none; width: auto; height: auto; border: 4px solid #000000;
        background-color: #FFFFFF;">
                           <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Repeater ID="rptOrders" runat="server" >
                            <HeaderTemplate>
                                <table id="tb" width="100%" class="m" cellspacing="1">
                                    <tr>
                                        <th class="top">
                                            Qty
                                        </th>
                                        <th class="top">
                                            Img
                                        </th>
                                        <th class="top">
                                            Product name
                                        </th>
                                        <th class="top">
                                            Price
                                        </th>
                                        <th class="top">
                                            Total
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtQty" Width="20px" Height="15px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>'
                                            AutoPostBack="true" onblur="chgQty(this)" OnTextChanged="txtQty_TextChanged" onkeyup="formatquntity(this)" MaxLength="3"></asp:TextBox>
                                        <asp:HiddenField ID="hdnRptProdId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' />
                                    </td>
                                    <td>
                                        <asp:Image  Width="30" ID="imgProduct" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "ImageSmallLow") %>'
                                            runat="server" />
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "ProductTitle") %>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price", "{0:£0.00}")%>'
                                            runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTotal" Text=' <%# DataBinder.Eval(Container.DataItem, "TotalPrice", "{0:£0.00}")%>'
                                            runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                          
                           
                            <tr>
								<td colspan="6">
									<span id="divwarn"  runat="server" style="color:red;width:265px;padding:2px;font-size:smaller; border:1px solid red;text-align:center;display:block;margin-top:3px;">You have multiple products in your basket. To rectify, simple change the quantity in the 'Qty' box above</span></td>
								</tr>
						
                                </Table>
                            </FooterTemplate>
                        </asp:Repeater>
                                            </ContentTemplate>
                </asp:UpdatePanel>
        
      
        <div style="text-align:center;padding:5px 0px 5px 0px;">
        
        <asp:ImageButton ID="BtnContinueToRecipient" runat="server" Text="Continue to recipient details" OnClick="continue_recipient_Click" ImageUrl="~/images/continuetorecipient.bmp"/>
    
            </div>
     
    </div>
     <div id="modalPage" class="ModalProgressContainer" style="height:5000px">
        <div class="ModalProgressContent">
        <img src="../images/ajax-loader.gif" alt="" />
        <div>Loading....</div>
        </div>
    </div>
    <div id="page_wrapper">
        <div id="content_normal">
            <!--Header DIV Start-->
            <div id="header">
                <uc1:Header ID="Header1" runat="server" EnableUrl="false" />
            </div>
            <div class="clearleft">
            </div>
            
            <!--Header DIV End-->
            <div class="divrow dottedline">
                <div class="recipentslbl100">
                    <h1 style="font-size: 18px;">
                         1. Please enter your billing details</h1>
                </div>
            </div>   
            <div class="divrow dottedline">
                <div class="recipentslbl100">              
                    <a href="m_summary.aspx" id="ancOrderId" runat="server">View order summary</a> <span id="spanOrderId" runat="server">
                    </span>
                </div>
            </div>         
            <div id="divFirstName" class="divrow dottedline">
                <div id="divFname">
                    <span class="redtxt">*</span> First name:</div>
                <div>
                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="100" onblur="validateField(this)"></asp:TextBox>
                    <span class="redtxt" style="display: none" id="spnFirstName">Enter first name</span>
                </div>
            </div>
            <!--First Name-->
            <div id="divLastName"  class="divrow dottedline">
                <div id="divlname"><span class="redtxt">*</span> Last name:</div>
                <div>
                    <asp:TextBox  MaxLength="100" ID="txtLastName" runat="server" onblur="validateField(this)"></asp:TextBox>
                    <span class="redtxt" style="display: none" id="spnLastName">Enter last name</span>
                </div>
            </div>
            <!--Last Name-->
            <div id="divEmail" class="divrow dottedline">
                <div id="divename">
                    <span class="redtxt">*</span> Email address:&nbsp;</div>
                <div>
                    <asp:TextBox  MaxLength="100" ID="txtEmail" runat="server" onblur="ValidateEmails()"></asp:TextBox>
                    <span class="redtxt" style="display: none" id="spnEmail">Enter valid email</span>
                </div>
            </div>
            <!--Email-->
             <div id="div4" class="dottedline">
                <div id="div5" style="padding-top:5px; padding-bottom:5px; width:100%">
                    <span class="redtxt">*</span> Full postcode:&nbsp;</div>
                <div style="width:100%; padding-bottom: 10px;">
                       <link rel="stylesheet" type="text/css" href="https://services.postcodeanywhere.co.uk/css/captureplus-1.34.min.css?key=ed89-rd15-zr57-bx68">
                        <script type="text/javascript" src="https://services.postcodeanywhere.co.uk/js/captureplus-1.34.min.js?key=ed89-rd15-zr57-bx68&amp;app=10891"></script>
                        <div id="ed89rd15zr57bx6810891">
                        </div>
                </div>
            </div>
            <!--PostCode-->
            <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="divrow dottedline" style="display:none">
                        <div id="divblanlk">
                            &nbsp;</div>
                        <div >
                            <asp:CheckBox ID="same_as_delivery" runat="server" Text="same as delivery address"
                                OnCheckedChanged="same_as_delivery_CheckedChanged" onclick="validateField(this)" AutoPostBack="true" /></div>
                    </div>
                    <div id="div1" runat="server">
                        <!--Check box for same as delivery address-->
                      <%--  <div class="divrow dottedline" id="DivFindpostcode" runat="server" style="height:65px;">
                            <div id="divfAdd">
                                <span class="redtxt">*</span> Full postcode:</div>
                            <div>
                                <asp:TextBox  MaxLength="100" ID="find_postcode" runat="server" Text="e.g. SE1 0HS" onclick="this.value='';"
                                    size="13" onkeyup="ValidtePostCodefields(this)" onblur="postcode_validateoOnBlur()"></asp:TextBox>
                               <span
                        id="spnTxtPostCode" class="infotext" style="display:inline; white-space:normal; font-size:11px;margin-left:4px;"></span> 
                                <br />
                                
                                    <asp:Button ID="find_residential" runat="server" 
                                        OnClick="find_residential_Click" CssClass="greenbtn" OnClientClick="javascript:return postcode_validate();" /></div>
                        </div>--%>
                        <!--Look up address by post code-->
                        <div class="dottedline" id="DivResidentialmanual" runat="server">
                         <%--   <div id="divblank1">
                                &nbsp;</div>--%>
                            <div style="height: 20px; padding-top: 10px; padding-bottom: 10px;">
                                <a id="residential_manual_link" href="#" onclick="javascript:return OnShowResidentialManualClick();">
                                    I don't know the full postcode / I don't live in the UK</a>
                            </div>
                        </div>
                        <!--Enter Address manually-->
                     <%--   <div class="divrow dottedline" id="DivBusinessmanual" runat="server">
                            <div id="divblank2">
                                &nbsp;</div>
                            <div>
                                <b>TIP:</b> For business or place of work?<br />
                                use our <a id="business_option" href="#" onclick="javascript:return OnShowBusinesslocatorClick();">
                                    Business Locator.</a></div>
                        </div>--%>
                        <!--Business locator-->
                    </div>
                    <div id="div2" runat="server" style="display: none">
                        <div class="divrow dottedline" id="DivFindBusiness" runat="server">
                            <div>
                                <span class="redtxt">*</span> Business/Organisation:&nbsp;</div>
                            <div>
                                <asp:TextBox  MaxLength="100" ID="find_business_name" onblur="ValidateBusinessFields(this)" Text="e.g. Serenata Flowers" runat="server" onclick="this.value='';"></asp:TextBox></div>
                        </div>
                        <!--Business/Organization-->
                        <div class="divrow dottedline" id="DivFindTown" runat="server">
                            <div id="divCityTown" class="recipentslbl100">
                                <div>
                                   <span class="redtxt">*</span> City or town:&nbsp;</div>
                                <div>
                                    <asp:TextBox MaxLength="100" ID="find_town" onblur="ValidateBusinessFields(this)" Text="e.g London" runat="server" onclick="this.value='';"></asp:TextBox>
                                </div>
                            </div>
                            <div></div>
                            <div>
                                <asp:Button ID="find_business" runat="server" OnClick="find_business_Click" CssClass="greenbtn"
                                    OnClientClick="javascript:return OnShowBusinessButtonClick();" /></div>
                        </div>
                        <!--Look up Business by post code-->
                        <div class="divrow dottedline" id="DivBusinessLink" runat="server">
                            <div>
                                &nbsp;</div>
                            <div>
                                <a id="business_manual_link" href="#" onclick="javascript:return OnShowEntermanuallyClick();">
                                    I prefer to enter the address manually</a>
                            </div>
                        </div>
                        <!--Prefer to Enter Address manually-->
                        <div class="divrow dottedline" id="DivResidential" runat="server">
                            <div>
                                &nbsp;</div>
                            <div>
                                Sending flower to a residential property? Use our <a id="residential_option" href="#"
                                    onclick="javascript:return OnShowResidentialPropertylocatorClick();">Residential
                                    Property Locator.</a>
                            </div>
                        </div>
                        <!--Residential Property Locator-->
                        <div class="dottedline divrow" id="DivErrorMsg" runat="server" style="display: none">
                            <div>
                            </div>
                            <div>
                                <span class="infotext" id="spnErrorMsg" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="dottedline divrow" id="DivErrorMsgFindPostcode" runat="server" style="display: none">
                        <div>
                        </div>
                        <div>
                            <span class="infotext" id="ErrorMsgFindPostcode" runat="server" />
                        </div>
                    </div>
                    <div class="divrow dottedline" id="DivAddressResult" runat="server" style="display: none">
                        <div id="SeAddr"> 
                            Select address:</div>
                        <div>
                        <%--onclick="postthis(this)" onchange="validateDdlValue(this);"--%>
                            <asp:DropDownList ID="ddlAddress"   runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlAddress_SelectedIndexChanged" Width="100%" />
                        </div>
                    </div>
                    <!--Address Result-->
                    <div class="dottedline divrow" id="DivAddressResultMsg" runat="server" style="display: none">
                        <div >
                        </div>
                        <div>
                            <span class="infotext" id="AddressResultMsg" runat="server" style="color:Orange"/>
                        </div>
                    </div>
                    <div id="div3" runat="server" style="display: none">
                        <div class="divrow dottedline" id="DivBusinessfield" runat="server">
                            <div>
                                Business/Organisation:&nbsp;</div>
                            <div>
                                <asp:TextBox  MaxLength="100" ID="organisation_field" runat="server"></asp:TextBox></div>
                        </div>
                        <!--Business/Organization-->
                        <div class="divrow dottedline" id="divAddressline1" runat="server">
                            <div>
                                <span class="redtxt">*</span> Address line 1: &nbsp;</div>
                            <div>
                                <asp:TextBox  MaxLength="100" ID="txtAddressline1" runat="server" onblur="validateField(this)"></asp:TextBox>
                                <span class="redtxt" style="display: none" id="spnAddressline1">Enter house number</span>
                            </div>
                            <!-- e.g. Flat 10 or Andover Terrace -->
                        </div>
                        <!-- Address Line 1 -->
                        <div class="divrow dottedline" id="DivAddressline2" runat="server">
                            <div>
                                Address line 2: &nbsp;</div>
                            <div>
                                <asp:TextBox  MaxLength="100" ID="street_field" runat="server"></asp:TextBox></div>
                            <!-- e.g. Dover Mansion -->
                        </div>
                        <!-- Address Line 2 -->
                        <div class="divrow dottedline" id="DivAddressline3" runat="server">
                            <div>
                                Address line 3: &nbsp;</div>
                            <div>
                                <asp:TextBox  MaxLength="100" ID="district_field" runat="server"></asp:TextBox></div>
                            <!-- e.g. Queens Club Gardens -->
                        </div>
                        <!-- Address Line 3 -->
                        <div class="divrow dottedline" id="divCitytown" runat="server">
                            <div>
                                <span class="redtxt">*</span> City or town:&nbsp;</div>
                            <div>
                                <asp:TextBox  MaxLength="100" ID="txtCitytown" runat="server" onblur="validateField(this)"></asp:TextBox>
                                <span class="redtxt" style="display: none" id="spnCitytown">Enter town/city name</span>
                                <!-- e.g. London -->
                            </div>
                        </div>
                        <!-- City or town -->
                        <div class="divrow dottedline" id="divPostcodefield" runat="server">
                            <div>
                                <span class="redtxt">*</span> Full postcode:&nbsp;</div>
                            <div>
                                <asp:TextBox  MaxLength="100" ID="txtPostcodefield" runat="server" onblur="validateField(this)" onkeyup="ValidtePostCodefields(this)"></asp:TextBox>
                                <span class="redtxt" style="display: none" id="spnPostcodefield">Enter postcode</span>
                                <!-- e.g. SE1 0HS -->
                            </div>
                        </div>
                        <!-- Full postcode -->
                        <div class="divrow dottedline" id="DivCountry" runat="server">
                            <div>
                                Country:&nbsp;</div>
                            <div>
                                <asp:DropDownList ID="invCountry" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <!-- Country -->
                        <div class="divrow dottedline" id="DivEmailnewsletter" runat="server">
                            <div>
                                Email newsletter:&nbsp;</div>
                            <div>
                                <asp:CheckBox ID="promo_email_field" runat="server" Checked="true" Style="float: left;" /></div>
                        </div>
                        <!-- Email newsletter -->
                        <div class="divrow dottedline" id="divContacttel" runat="server" visible="false">
                            <div>
                                <span class="redtxt">*</span> Contact tel:&nbsp;</div>
                            <div>
                                <asp:TextBox  MaxLength="100" ID="txtContacttel" runat="server" onblur="validateField(this)" onkeyup="Validetephone(this)"></asp:TextBox>
                                <span class="redtxt" style="display: none" id="spnContacttel">Enter phone number</span>
                            </div>
                        </div>
                        <!-- Contact Tel -->
                        <div class="divrow dottedline" id="DivUkmobile" runat="server">
                            <div>
                                UK Mobile number:&nbsp;</div>
                            <div>
                                <asp:TextBox  MaxLength="100" ID="mobile_field" runat="server"></asp:TextBox></div>
                        </div>
                        <!-- UK Mobile number -->
                        <div class="divrow dottedline" id="DivSMSNotification" runat="server">
                            <div>
                                SMS Notifications?:&nbsp;</div>
                            <div>
                                <asp:CheckBox ID="sms_field" runat="server" Style="float: left;" Text="for SMS dispatch &amp; delivery alerts only" /></div>
                        </div>
                        <!-- SMS Notifications -->
                        <div class="divrow dottedline" id="DivVoucher" runat="server">
                            <div>
                                Voucher code:&nbsp;</div>
                            <div>
                            <%--onkeyup="ValidtePostCodefields(this)"--%>
                                <asp:TextBox  MaxLength="100" ID="voucher_code" runat="server" ></asp:TextBox>
                                <span id="spnVoucher" runat="server" class="redtxt" style="float:left"></span>
                            </div>
                        </div>
                        <!-- Voucher code -->
                        <div class="divrow" id="DivContinue" runat="server">
                            <br />
                            <asp:ImageButton ID="btnContinue" runat="server" ImageUrl="https://checkout.serenataflowers.com/images/continue_recipient.gif"
                                Width="281" Height="60" OnClick="btnContinue_Click" OnClientClick="javascript:return ValidateAddressFields();" />
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="same_as_delivery" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="find_business" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlAddress" EventName="SelectedIndexChanged" />
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
            <!--Footer DIV Start-->
            <div id="Div1">
                <p class="lfloat fott">
                    <a class="btn" onclick="this.blur();" href="../default.aspx" runat="Server" id="ancStp"><span>« continue shopping</span>
                    </a>
                </p>
            </div>
            <div class="clearleft"></div>
            <br />
           
            <div id="FooterDiv">
                  <uc2:Footer ID="Footer1" runat="server" />
            </div>
            <!--Footer DIV End-->
        </div>
         <asp:HiddenField ID="hdnOrderId" runat="server" />
          <asp:HiddenField ID="hdnfilename" runat="server" />   
          <asp:HiddenField ID="hdnBoolConfigXml" runat="server" />

        <asp:HiddenField ID="hdnSelectedAddressValue" runat="server" />
        <asp:HiddenField ID="hdnGetPriceByOrderId" runat="server" />
        <asp:HiddenField ID="hdnEncryptedOrder" runat="server" />
        <asp:HiddenField ID="hdnEncryptedConfig" runat="server" />
         <asp:HiddenField ID="hdnEncryptionUrl" runat="server" />
         
        <asp:UpdatePanel ID="updOrder" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hdnOrderXmlFileName" runat="server" />                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
       <script type="text/javascript">
           function CapturePlusCallback(uid, response) {
               try {
                   // user has selected an address from Capture+   
                   document.getElementById('txtAddressline1').value = response[0].FormattedValue;
                   document.getElementById('txtCitytown').value = response[4].FormattedValue;
                   document.getElementById('txtPostcodefield').value = response[5].FormattedValue;
                   document.getElementById('div3').style.display = "block";
               } catch (e) {

               }
           }
     </script>
    </form>
    
    <%--https://transaction.serenata.co.uk/payment.asp  value='<%= Convert.ToString(Session["orderXml"]) %>' --%>
     <form id="frmOrder" action="https://transaction.serenata.co.uk/m_step3.asp" method="post">
        <input type="hidden" id="order" name="order"  />
        <input type="hidden" id="config" name="config"  />
        </form>
</body>
</html>
