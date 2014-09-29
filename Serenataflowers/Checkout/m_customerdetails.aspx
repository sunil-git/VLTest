<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_customerdetails.aspx.cs" Inherits="Serenataflowers.Checkout.m_customerdetails" EnableViewState="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Controls/CheckoutHeader.ascx" TagName="CheckoutHeader" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/CheckoutFooter.ascx" TagName="CheckoutFooter" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/Panel.ascx" TagName="Panel" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/EditBasket.ascx" TagName="EditBasket" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/BasketCount.ascx" TagName="BasketCount" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/EditDeliveryDate.ascx" TagName="EditDeliveryDate" TagPrefix="uc6" %>
<%@ Register Src="~/Controls/Voucher.ascx" TagName="Voucher1" TagPrefix="uc7" %>
<%@ Register Src="~/Controls/ContactUs.ascx" TagName="ContactUs" TagPrefix="uc8" %>
<%@ Register Src="~/Controls/m_Terms.ascx" TagName="Terms" TagPrefix="uc9" %>
<%@ Register Src="~/Controls/m_Privacy.ascx" TagName="Privacy" TagPrefix="uc10" %>
<%@ Register Src="~/Controls/PromptLogin.ascx" TagName="PromptLog" TagPrefix="uc11" %>
<%@ Register Src="~/Controls/PasswordReminder.ascx" TagName="ResetPassword" TagPrefix="uc11" %>
<%@ Register Src="~/Controls/ETEmailVerfiy.ascx" TagName="ETEmail" TagPrefix="ucET" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Serenata Flowers - Customer Details</title>
<meta charset="utf-8" />  

<meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no"  />

<link rel="stylesheet" href="https://code.jquery.com/mobile/1.3.1/jquery.mobile.structure-1.3.1.min.css" />
<script src="https://code.jquery.com/jquery-1.10.0.min.js"></script>

<script src="../Scripts/CutOffTimer.js" type="text/javascript"></script>

<script>
    $(document).bind("mobileinit", function () {
       $.mobile.ajaxEnabled = false;
       $.mobile.loadingMessage = false;
       $.mobile.pageLoadErrorMessage = false;
        $.mobile.pushStateEnabled = false;
    });

    function revealDeliveryCutoffMsg(txtMsg) {
        $.mobile.changePage('#DeliveryDate', { transition: 'pop', role: 'dialog' });
        //document.getElementById('ucDeliverControl_divCutOff').style.display = 'block';
        //document.getElementById('ucDeliverControl_hdnDeliveryCutOff').value = "true";
        //$('#edit-delivery').reveal();
        document.getElementById('ModifyDeliveryDate_spanCutOffMsg').innerHTML = txtMsg;
    }

 </script>
<script src="https://code.jquery.com/mobile/1.3.1/jquery.mobile-1.3.1.min.js"></script> 
<link rel="stylesheet" href="../stylesheets/serenata-flowers.min.css">
<link rel="stylesheet" href="../stylesheets/custom.css">
<script src="../Scripts/commonfunctions.js"></script> 
<script src="../Scripts/m_customerdetails.js"></script> 


    
</head>
<body id="MasterBody" runat="server">
    <form id="CustomerDetails" runat="server">
       <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true"  runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
      <script type="text/javascript">
          function bindEvents() {
              $(document).trigger('create');
             
          }

          bindEvents();

          //Re-bind for callbacks     
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              bindEvents();
          });
         
          function PromptLoginPopup() {
           
              $.mobile.changePage('#PromptLogin', { transition: 'pop', role: 'dialog' });


          }

          function EmailVerifyPopup(txtEmail) {

              document.getElementById('ValidateETEmail').value = txtEmail;

              $.mobile.changePage('#EmailVerify', { transition: 'pop', role: 'dialog' });


          }
          function SetCustFocus() {
              $("input[name=postal]").on({ touchstart: function (e) {
                  setTimeout(function () {
                      $("input[name=postal]").focus();
                  }, 300);
              }
          });
         
              $("#CustomerEmail").focus();
          }
          
//          $(document).ready(function () {

//              $("#CustomerEmail").focus();

//              loadCapturePlus();

//          });

//          function jScript() {


//              loadCapturePlus();

//          }


          function callservermethod() {
              try {
                  var name = $("#CustomerEmail").val();
                  PageMethods.GetCurrentDate(name, OnSuccess, OnFailure);
                  
                  return true;
              } catch (e) {
                  alert(e);
              }

          }
          function OnSuccess(result) {
              if (result) {
                  if (parseInt(result) > 0) {
                      PromptLoginPopup();
                  }
              }
          }
          function OnFailure(error) {
              alert(error);
          }
          //======================================================== FASTCLICK
          function FastButton(element, handler) {
              this.element = element;
              this.handler = handler;
              element.addEventListener('touchstart', this, false);
          };
          FastButton.prototype.handleEvent = function (event) {
              switch (event.type) {
                  case 'touchstart': this.onTouchStart(event); break;
                  case 'touchmove': this.onTouchMove(event); break;
                  case 'touchend': this.onClick(event); break;
                  case 'click': this.onClick(event); break;
              }
          };
          FastButton.prototype.onTouchStart = function (event) {

              event.stopPropagation();
              this.element.addEventListener('touchend', this, false);
              document.body.addEventListener('touchmove', this, false);
              this.startX = event.touches[0].clientX;
              this.startY = event.touches[0].clientY;
              isMoving = false;
          };
          FastButton.prototype.onTouchMove = function (event) {
              if (Math.abs(event.touches[0].clientX - this.startX) > 10 || Math.abs(event.touches[0].clientY - this.startY) > 10) {
                  this.reset();
              }
          };
          FastButton.prototype.onClick = function (event) {
              this.reset();
              this.handler(event);
              if (event.type == 'touchend') {
                  preventGhostClick(this.startX, this.startY);
              }
          };
          FastButton.prototype.reset = function () {
              this.element.removeEventListener('touchend', this, false);
              document.body.removeEventListener('touchmove', this, false);
          };
          function preventGhostClick(x, y) {
              coordinates.push(x, y);
              window.setTimeout(gpop, 2500);
          };
          function gpop() {
              coordinates.splice(0, 2);
          };
          function gonClick(event) {
              for (var i = 0; i < coordinates.length; i += 2) {
                  var x = coordinates[i];
                  var y = coordinates[i + 1];
                  if (Math.abs(event.clientX - x) < 25 && Math.abs(event.clientY - y) < 25) {
                      event.stopPropagation();
                      event.preventDefault();
                  }
              }
          };
          document.addEventListener('click', gonClick, true);
          var coordinates = [];
          function initFastButtons() {
              new FastButton(document.getElementById("fastclick"), goSomewhere);
              SetCustFocus();
          };
          function goSomewhere() {
              var theTarget = document.elementFromPoint(this.startX, this.startY);
              if (theTarget.nodeType == 3) theTarget = theTarget.parentNode;

              var theEvent = document.createEvent('MouseEvents');
              theEvent.initEvent('click', true, true);
              theTarget.dispatchEvent(theEvent);
          };
          //========================================================
          function showEditBasket() {
              document.getElementById("RowWarnQty").style.display = "none";
              //document.getElementById('EditBasketheader').innerHTML = "Your Basket";
              $.mobile.changePage('#EditBasket', { transition: 'pop', role: 'dialog' });
          }
     </script>
      <style type="text/css" media="screen">
          textarea.ui-input-text {
            height: 40px!Important;
            transition: height 200ms linear 0s;
        }
	</style>
    <div data-role="page" data-theme="a" id="customer1" data-title="Customer Details">

<!-- header -->
	
	<div data-role="header" data-theme="c" id="header">
		   <uc1:CheckoutHeader ID="CheckoutHeader1" runat="server" EnableUrl="true" />
       <div class="header-btns">
	        <a href="#rightpanel" style="color:#fff;" class="menu-btn"></a>
	        <a href="javascript:showEditBasket();" style="color:#fff;" data-rel="dialog" data-transition="pop" class="basket-btn" id="basketCount" runat="server">
             <uc5:BasketCount ID="DisplayBasketCount" runat="server" UpdateMode="Conditional" />
            </a>
        </div>
		<div style="clear:both;"></div>

			<fieldset class="ui-grid-b">
				<div class="ui-block-a step-active">
					<a href="" style="color:#fff;">Customer </a><span class="arrow-left"></span>
				</div>
				<div class="ui-block-b step-grey">
					<a href="" style="color:#fff;">Recipient</a><span class="arrow-left"></span>
				</div>
				<div class="ui-block-c step-grey">
					<a href="" style="color:#fff;">Payment</a><span class="arrow-left"></span>
				</div>	
			</fieldset>			
	</div>

<!-- content -->
	<div data-role="content" data-theme="c" >	
     
			  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
  <span id="fastclick">
  <div id="content1" runat="server">
		<div style="clear:both;"></div><br /><br />

		
			<div class="ui-bar ui-bar-a" style="margin:10px 0 10px 0;"><h3>Please enter your details</h3></div>	 
		


			<label for="email"><span style="color:red;">*</span> Email:</label>
          
            <textarea rows="1" ID="CustomerEmail" runat="server"  placeholder="mark@mail.com"  onblur="return VerifyETEmailAddress()"></textarea>
			
           <span style="display:none" class="errortext" id="ErrorCustomerEmail">Please enter a valid email address.</span>

			<label for="firstname"><span style="color:red;">*</span> First Name:</label>
			 <textarea rows="1" ID="CustomerFirstName" runat="server"  placeholder="e.g. Mark" onblur="ValidateFirstNameCust(true);"></textarea>
            <span style="display:none" class="errortext" id="ErrorCustomerFirstName">Please enter First name.</span>

			<label for="lastname">Last Name:</label>
			<textarea rows="1"  ID="CustomerLastName" runat="server"   placeholder="e.g. Smith" ></textarea>
			
            <div class="row" id="divPassword" runat="server" style="display:none">
 
			
			<label><span style="color:red;">*</span>Password:</label>			
            <textarea rows="1" ID="CustomerPassword" runat="server" MaxLength="20" ClientIDMode="Static"  onKeyUp="PasswordTyping(event)"  onblur="ValidatePassword(true);"></textarea>
            <textarea rows="1" cols="10" id="textareaCustomerPassword" ClientIDMode="Static"  runat="server" style="display:none"></textarea>
             <span style="display:none" class="errortext" id="ErrorCustomerPassword">Please enter  Password.</span>

			<label><span style="color:red;">*</span>Confirm Password:</label>
			 <textarea rows="1" ID="CustomerConfirmPassword" runat="server" MaxLength="20"  ClientIDMode="Static"  onKeyUp="ConfirmPasswordTyping(event)"  onblur="ValidateConfirmPassword(true);"></textarea>
             <textarea rows="1" cols="10" id="textareaCustomerConfirmPassword" ClientIDMode="Static"  runat="server" style="display:none"></textarea>
			<span style="display:none" class="errortext" id="ErrorPasswordNotMatched">The passwords you have entered doesnt match</span>	
             <span style="display:none" class="errortext" id="ErrorCustomerConfirmPassword">Please enter  Confirm Password.</span>					

		</div>	


			<br />
            <div class="margin:0 10px 0 10px;float:left;">
				<label for="addressfinder" style="color:#8aa064;font-weight:bold;">Address Finder:</label>
				<link rel="stylesheet" type="text/css" href="https://services.postcodeanywhere.co.uk/css/captureplus-1.34.min.css?key=ed89-rd15-zr57-bx68" />
       <script type="text/javascript" src="https://services.postcodeanywhere.co.uk/js/captureplus-1.34.min.js?key=ed89-rd15-zr57-bx68&amp;app=10891"></script>
					<div id="ed89rd15zr57bx6810891" onclick="javascript: setFocusonEmailFirstName();"></div>
			</div>	

			
				
			<br />
			<h5 style="text-align:center;margin:5px 0 5px 0;">OR</h5>

			
			<p style="text-align:center;"><a href="" onclick="javascript:clickEnterAddressManually();" >Enter address manually</a></p>		
			
			<br />
		
			<div align="center" style="text-align:center;">
				<%--<button onclick="location.href='index.asp'" type="submit" data-theme="a" data-icon="arrow-l" data-mini="true" data-iconpos="left">Back</button>--%>
                            <asp:Button ID="Button1" runat="server" Text="Back" data-theme="a" data-icon="arrow-l"
                                data-mini="true" data-iconpos="left" OnClick="Back_Click" />

			</div> 
			<div align="center" style="display:none">
		<asp:Button ID="CapturePCAData" runat="server" Text="Button" Style="display: none"  OnClick="CapturePCAData_Click" />
        </div> 
        </div>
  <div id="content2" style="display:none" runat="server"> 
  
		<div style="clear:both;"></div><br /><br />

		
			<div class="ui-bar ui-bar-a" style="margin:10px 0 10px 0;"><h3>Your details</h3></div>	
			
			<div class="box">
				<fieldset class="ui-grid-a">
						<div class="ui-block-a">
						<span class="infotext-dark">Your Name:</span>
						</div>
						<div class="ui-block-b">
						<span class="infotext" id="CustomerFullName" runat="server"></span>
						</div>	   
				</fieldset>
                <fieldset class="ui-grid-a">
						<div class="ui-block-a">
						<span class="infotext-dark">Your Email:</span>
						</div>
						<div class="ui-block-b">
						<span class="infotext" id="CustomerEmailAddress" runat="server"></span>
						</div>	   
				</fieldset>
                <fieldset class="ui-grid-a" id="fieldPCAPassword" style="display:none">
						<div class="ui-block-a">
						<span class="infotext-dark">Your Password:</span>
						</div>
						<div class="ui-block-b">
						<span class="infotext" id="CustomerPCAPassword" runat="server"></span>
						</div>	   
				</fieldset>

				<fieldset class="ui-grid-a">
						<div class="ui-block-a">
						<span class="infotext-dark">Your Address:</span>
						</div>
						<div class="ui-block-b">
						<span class="infotext" id="CustomerPCAAddress" runat="server"></span>
						</div>	   
				</fieldset>

				<fieldset class="ui-grid-a">
						<div class="ui-block-a">
						<span class="infotext-dark" >City:</span>
						</div>
						<div class="ui-block-b">
						<span class="infotext" id="CustomerPCACity" runat="server"></span>
						</div>	   
				</fieldset>

				<fieldset class="ui-grid-a">
						<div class="ui-block-a">
						<span class="infotext-dark">Postcode:</span>
						</div>
						<div class="ui-block-b">
						<span class="infotext" id="CustomerPCAPostCode" runat="server"></span>
						</div>	   
				</fieldset>

				<fieldset class="ui-grid-a">
						<div class="ui-block-a">
						<span class="infotext-dark" >Country:</span>
						</div>
						<div class="ui-block-b">
						<span class="infotext" id="CustomerPCACountry" runat="server"></span>
						</div>	   
				</fieldset>

				<div align="center" style="margin:5px 0 5px 0;">
					<a href="" data-role="button" data-theme="a" data-inline="true" data-mini="true" data-icon="edit" data-iconpos="left" onclick="clickPCAEdit();">Edit address</a>
				</div>

			</div>
			
			<br />
			<div style="width:100%;float:left;">
				<div style="width:35%;float:left;">
					<label for="countrycode"><span style="color:red;">*</span><span class="infotext-dark">Country code:</span></label>
                     <asp:TextBox ID="CustomerCountryCode"  ClientIDMode="Static"  runat="server" pattern="[0-9]*" placeholder="+44" onkeydown="return numbersOnly(event);" onpaste="return false;" onblur="ValidateMobCodeCust(true);"></asp:TextBox>
                  <span style="display:none" class="errortext" id="ErrorCustomerCountryCode">Please enter mobile code.</span>
				
				</div>
				<div style="width:60%;float:left;margin-left:10px;">
					<label for="countrycode"><span style="color:red;">*</span><span class="infotext-dark">Mobile Number:</span></label>
						<textarea rows="1" cols="1"  ID="CustomerMobileNumber" ClientIDMode="Static" runat="server" pattern="[0-9]*" placeholder="eg. 1234567" onkeydown="return numbersOnly(event);" onpaste="return false;" onblur="ValidateMobileNumberCust(true);"></textarea>
                    <span style="display:none" class="errortext" id="ErrorCustomerMobileNumber">Please enter mobile number.</span>
				</div>	
			</div>

			<label for="vouchercode">Voucher code:</label>
			<textarea rows="1" cols="1" ID="CustomerVoucherCode" runat="server" MaxLength="50" placeholder="e.g. o!PMrILaJfGq"
                    Style="margin-top: 5px" onblur="hideVoucherError();"></textarea>
                <span style="display: none" class="errortext" id="errorCustomerVoucherCode" runat="server"></span>

			<fieldset class="ui-grid-a">
					<div class="ui-block-a">
						<span class="infotext-dark" style="margin-top:10px;float:left;">Receive offers by email</span>
					</div>
					<div class="ui-block-b">
                    <asp:CheckBox ID="ReceiveOffersByEmail" ClientIDMode="Static" style="float:left;padding:7px;margin:10px 0 0 0;width:20px;" runat="server" Checked="true" /> 
						
					</div>	   
			</fieldset>

			<fieldset class="ui-grid-a">
					<div class="ui-block-a">
						<span class="infotext-dark" style="margin-top:10px;float:left;">Receive offers by SMS</span>
					</div>
					<div class="ui-block-b">
                     <asp:CheckBox ID="ReceiveOffersBySMS" style="float:left;padding:7px;margin:10px 0 0 0;width:20px;" ClientIDMode="Static" runat="server" Checked="true" />
						
					</div>	   
			</fieldset>
			<br />
		


			<div align="center" style="text-align:center;">
            <asp:Button ID="SaveRecipient" runat="server" Text="Continue to Recipient Details" OnClientClick="return ValidateSecondScreen();" OnClick="SaveRecipient_Click" />
				
			</div>
			
			
			<br />
		
			<div align="center" style="text-align:center;">
				 <asp:Button ID="ScreenBack" runat="server" Text="Back"  OnClick="Back_Click" />
			</div> 
			
   
        </div>
  <div id="content3" style="display:none" runat="server">

		<div style="clear:both;"></div><br /><br />

		
			<div class="ui-bar ui-bar-a" style="margin:10px 0 10px 0;"><h3>Enter your details</h3></div>	
            <div id="divManually" style="display:none">
			    <div class="box" >
				    <fieldset class="ui-grid-a">
						    <div class="ui-block-a">
						    <span class="infotext-dark">Customer Name:</span>
						    </div>
						    <div class="ui-block-b">
						    <span class="infotext" id="DisplayCustomerFullname" runat="server"></span>
						    </div>	   
				    </fieldset>
				    <fieldset class="ui-grid-a">
						    <div class="ui-block-a">
						    <span class="infotext-dark">Email:</span>
						    </div>
						    <div class="ui-block-b">
						    <span class="infotext" id="DisplayCustomerAddress" runat="server"></span>
						    </div>	   
				    </fieldset>
                    <fieldset class="ui-grid-a" id="ManualfieldCustomerPassword" style="display:none">
						    <div class="ui-block-a">
						    <span class="infotext-dark">Password:</span>
						    </div>
						    <div class="ui-block-b">
						    <span class="infotext" id="DisplayCustomerPassword"></span>
						    </div>	   
				    </fieldset>

				    <div align="center" style="margin:5px 0 5px 0;">
					    <a href="" data-role="button" data-theme="a" data-inline="true" data-mini="true" data-icon="edit" data-iconpos="left" onclick="clickManualEdit();">Edit</a>
				    </div>
                    <div  id="divManualEdit" style="display:none">
				        <label for=""><span style="color:red;">*</span>First name:</label>				       
                       <textarea rows="1" cols="1" ID="ManualEditFirstName" runat="server" ClientIDMode="Static" placeholder="e.g. Mary" onblur="ValidateManualEditFirstName(true);"></textarea>
                        <span style="display:none" class="errortext" id="ErrorManualEditFirstName">Please enter First name.</span>

				        <label for="">Last name:</label>
                       <textarea rows="1" cols="1" ID="ManualEditLastName" runat="server" ClientIDMode="Static" placeholder="e.g. Robinson" onblur=""></textarea>
				        

				        <label for=""><span style="color:red;">*</span>Email:</label>				       
				         <textarea rows="1" cols="1" ID="ManualEditEmail" runat="server" ClientIDMode="Static" placeholder="mark@mail.com"  onchange="return ValidateManualEditEmail();" onblur="return ValidateManualEditEmail();" ></textarea>
                        <span style="display:none" class="errortext" id="ErrorManualEditEmail">Please enter a valid email address.</span>

                        <div class="row" id="divManualCustomerPassword"  style="display:none">
 			
			            <label><span style="color:red;">*</span>Password:</label>			
                          <textarea rows="1" cols="1" ID="ManualEditCustomerPassword" runat="server" MaxLength="20" ClientIDMode="Static" TextMode="Password"  onblur="ValidateManualEditPassword(true);"></textarea>
                         <span style="display:none" class="errortext" id="errorManualEditCustomerPassword">Please enter  Password.</span>

			           				

		            </div>	

				        <div style="clear:both;"></div><br />
                        </div>
			        </div>

			    <br />
			
			    <label for="business">Business/Organisation:</label>
                 <textarea rows="1" cols="1"  ID="ManualEditOrganization" runat="server" ClientIDMode="Static" placeholder="e.g. Serenata Commerce Ltd"></textarea>

			    <label for="address1"><span style="color:red;">*</span> Address line 1:</label>
			   <textarea rows="1" cols="1"  ID="ManualEditCustomerHouseNumber" runat="server" ClientIDMode="Static" placeholder="e.g. Flat 3 or Huntingfield Road" onblur="ValidateManualEditCustomerHouseNumber(true);"></textarea>
			    <span style="display:none" class="errortext" id="ErrorManualEditCustomerHouseNumber">Please enter Address line 1.</span>


			    <label for="address2">Address line 2:</label>
			     <textarea rows="1" cols="1"  ID="ManualEditCustomerStreet" runat="server" ClientIDMode="Static" placeholder="e.g. Milton Mansion"></textarea>

			    <label for="address3">Address line 3:</label>
			    <textarea rows="1" cols="1"  ID="ManualEditCustomerDistrict" runat="server" ClientIDMode="Static" placeholder="e.g. Queens Club Gardens"></textarea>

			    <label for="city"><span style="color:red;">*</span> City or Town:</label>
			    <textarea rows="1" cols="1"  ID="ManualEditCustomerCity" runat="server" ClientIDMode="Static" placeholder="e.g. London" onblur="ValidateManualEditCustomerCity(true);"></textarea>
			    <span style="display:none" class="errortext" id="ErrorManualEditCustomerCity">Please enter City or town.</span>

			    <label for="postcode"><span style="color:red;">*</span> Postcode:</label>
			   <textarea rows="1" cols="1" ID="ManualEditCustomerPostCode" runat="server" ClientIDMode="Static" placeholder="e.g. SW15 5ET" onblur="ValidateManualEditCustomerPostCode(true);"></textarea>
                <span style="display:none" class="errortext" id="ErrorManualEditCustomerPostCode">Please enter Full Postcode.</span>

            </div>
            <div id="divPCAEdit" style="display:none" runat="server">
			    <div class="box" >
				  
                  
				        <label for=""><span style="color:red;">*</span>First name:</label>
				         <textarea rows="1" cols="1"  ID="PCAEditFirstName" runat="server" ClientIDMode="Static" placeholder="e.g. Mark" onblur="ValidatePCAEditFirstName(true);"></textarea>
                        <span style="display:none" class="errortext" id="ErrorPCAEditFirstName">Please enter First name.</span>

				        <label for="">Last name:</label>
                        <textarea rows="1" cols="1"  ID="PCAEditLastName" runat="server" ClientIDMode="Static" placeholder="e.g. Robinson"></textarea>
                        			       

				        <label for=""><span style="color:red;">*</span>Email:</label>
				       <textarea rows="1" cols="1"  ID="PCAEditEmail" runat="server" ClientIDMode="Static" placeholder="mark@mail.com"  onchange="return ValidatePCAEditEmail();" onblur="return ValidatePCAEditEmail();" ></textarea>
                        <span style="display:none" class="errortext" id="ErrorPCAEditEmail">Please enter a valid email address.</span>

                        <div class="row" id="divPCAEditPassword"  style="display:none">
 			
			            <label><span style="color:red;">*</span>Password:</label>			
                         <textarea rows="1" cols="1"  ID="PCAEditPassword" runat="server" MaxLength="20" ClientIDMode="Static" TextMode="Password"  onblur="ValidatePCAEditPassword(true);"></textarea>
                         <span style="display:none" class="errortext" id="ErrorPCAEditPassword">Please enter  Password.</span>

			           				

		                </div>

				        <label for="business">Business/Organisation:</label>
                         <textarea rows="1" cols="1"  ID="PCAEditOrganization" runat="server" ClientIDMode="Static" placeholder="e.g. Serenata Commerce Ltd"></textarea>


			            <label for="address1"><span style="color:red;">*</span> Address line 1:</label>
                        <textarea rows="1" cols="1"  ID="PCAEditCustomerHourseNumber" runat="server" ClientIDMode="Static" placeholder="e.g. Flat 3 or Huntingfield Road" onblur="ValidatePCAEditCustomerHouseNumber(true);"></textarea>
			            <span style="display:none" class="errortext" id="ErrorPCAEditCustomerHourseNumber">Please enter Address line 1.</span>

			            <label for="address2">Address line 2:</label>
                         <textarea rows="1" cols="1"  ID="PCAEditCustomerStreet" runat="server" ClientIDMode="Static" placeholder="e.g. Milton Mansion"></textarea>
			            

			            <label for="address3">Address line 3:</label>
                        <textarea rows="1" cols="1"  ID="PCAEditCustomerDistrict" runat="server" ClientIDMode="Static" placeholder="e.g. Queens Club Gardens"></textarea>
			            

			            <label for="city"><span style="color:red;">*</span> City or Town:</label>
                         <textarea rows="1" cols="1"  ID="PCAEditCustomerCity" runat="server" ClientIDMode="Static" placeholder="e.g. London" onblur="ValidatePCAEditCustomerCity(true);"></textarea>
			           <span style="display:none" class="errortext" id="ErrorPCAEditCustomerCity">Please enter City or town.</span>

			            <label for="postcode"><span style="color:red;">*</span> Postcode:</label>
                         <textarea rows="1" cols="1"  ID="PCAEditCustomerPostCode" runat="server" ClientIDMode="Static" placeholder="e.g. SW15 5ET" onblur="ValidatePCAEditCustomerPostCode(true);"></textarea>
                        <span style="display:none" class="errortext" id="ErrorPCAEditCustomerPostCode">Please enter Full Postcode.</span>
			            
				        <div style="clear:both;"></div><br />
                        </div>
			      

			    <br />
			
			    
            </div>
			<label for="country">Country:</label>
				<asp:DropDownList ID="CustomerCountry" ClientIDMode="Static" runat="server" style="padding:5px;" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="CustomerCountry_SelectedIndexChanged"></asp:DropDownList>

			<br />
			<div style="width:100%;float:left;">
				<div style="width:35%;float:left;">
					<label for="countrycode"><span style="color:red;">*</span><span class="infotext-dark">Country code:</span></label>
					 <asp:TextBox ID="CountryCode"  ClientIDMode="Static"  runat="server" placeholder="+44" onblur="ValidateCountryCode(true);" onkeydown="return numbersOnly(event);" onpaste="return false;"></asp:TextBox>
                  <span style="display:none" class="errortext" id="ErrorCountryCode">Please enter mobile code.</span>
				</div>
				<div style="width:60%;float:left;margin-left:10px;">
					<label for="mobilenumber" ><span style="color:red;">*</span><span class="infotext-dark">Mobile Number:</span></label>
				<textarea rows="1" cols="1"  ID="MobileNumber" ClientIDMode="Static" runat="server" placeholder="eg. 1234567" onkeydown="return numbersOnly(event);" onpaste="return false;" onblur="ValidateMobileNumber(true);"></textarea>
                    <span style="display:none" class="errortext" id="ErrorMobileNumber">Please enter mobile number.</span>
				</div>	
			</div>

			<div style="clear:both;"></div><br />

			<label for="vouchercode">Voucher code:</label>
			 <textarea rows="1" cols="1"  ID="DiscountVoucherCode" runat="server" MaxLength="50" placeholder="e.g. o!PMrILaJfGq"
                    Style="margin-top: 5px" onblur="hideDiscountVoucherError();"></textarea>
                <span style="display: none" class="errortext" id="ErrorVoucherCode" runat="server"></span>
			

			<fieldset class="ui-grid-a">
					<div class="ui-block-a">
						<span class="infotext-dark" style="margin-top:10px;float:left;">Receive offers by email</span>
					</div>
					<div class="ui-block-b">
						<asp:CheckBox ID="IsReceiveOffersByEmail" ClientIDMode="Static" style="float:left;padding:7px;margin:10px 0 0 0;width:20px;" runat="server" Checked="true" /> 
					</div>	   
			</fieldset>

			<fieldset class="ui-grid-a">
					<div class="ui-block-a">
						<span class="infotext-dark" style="margin-top:10px;float:left;">Receive offers by SMS</span>
					</div>
					<div class="ui-block-b">
						<asp:CheckBox ID="IsReceiveOffersBySMS" style="float:left;padding:7px;margin:10px 0 0 0;width:20px;" ClientIDMode="Static" runat="server" Checked="true" />
					</div>	   
			</fieldset>
			<br />

			<div style="clear:both;"></div><br />
		
			<div align="center" style="text-align:center;display:none" id="ManualSave">
				
                <asp:Button ID="ContinueRecipient" runat="server" Text="Continue to Recipient Details" OnClientClick="return ValidateThirdScreenManualSave();" OnClick="ContinueRecipient_Click" />
			</div>
			
            <div align="center" style="text-align:center;display:none" id="PCASave" runat="server">
				
                <asp:Button ID="SavePCADetails" runat="server" Text="Continue to Recipient Details" OnClientClick="return ValidateThirdScreenPCASave();" OnClick="SavePCADetails_Click" />
			</div>
			<br />
		
			<div align="center" style="text-align:center;">
				
                <asp:Button ID="Back" runat="server" Text="Back"  OnClick="Back_Click" />
			</div> 
			
			
		
      
  </div>
  </span>
     </ContentTemplate>
  <Triggers>
           
            <asp:AsyncPostBackTrigger ControlID="CapturePCAData" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="SaveRecipient" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ContinueRecipient" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="SavePCADetails" EventName="Click" />
              <asp:AsyncPostBackTrigger ControlID="CustomerCountry" EventName ="SelectedIndexChanged"/>
           </Triggers>
  </asp:UpdatePanel>
  		
	</div>

  
 
<!-- content -->
	<div data-role="footer" data-theme="c" style="padding:0 15px 0 15px;">
		 <uc2:CheckoutFooter ID="Footer" runat="server" />
	</div>  


	    <uc3:Panel ID="Menu" runat="server" />
   
</div>  
   <!-- EditBasket Section Start-->
      <uc4:EditBasket ID="ModifyBasket" runat="server" UpdateMode="Conditional" OnButtonClick="Save_Click"  />     
<!-- EditAsket Section End-->
<!-- EditDelivery Section Start-->
       <uc6:EditDeliveryDate ID="ModifyDeliveryDate" runat="server" UpdateMode="Conditional" OnButtonClick="SaveDates_Click" />     
<!-- EditDelivery Section End-->
<!-- Voucher Section Start-->
       <uc7:Voucher1 ID="VoucherCode" runat="server" UpdateMode="Conditional" OnButtonClick="Voucher_Click" />     
<!-- Voucher Section End-->
<!-- Contact Us Section Start-->
 <uc8:ContactUs ID="UserContactUs" runat="server" />
  <uc9:Terms ID="Termsandcondition" runat="server" />
 <uc10:Privacy ID="Privacy" runat="server" />
 <uc11:PromptLog id="PromptLogin_Id" runat="server" />
 <uc11:ResetPassword ID="SendPassword" runat="server" />
 <ucET:ETEmail id="idEtEmailVerify" runat="server" />
 <!-- Contact Us Section End-->
 <asp:HiddenField ID="hdnCustomerFirstname" runat="server" Value="" />
  <asp:HiddenField ID="hdnCustomerLastName" runat="server" Value="" />
 <asp:HiddenField ID="hdnPCAOrganisation" runat="server" Value="" />
 <asp:HiddenField ID="hdnPCAStreet" runat="server" Value="" />
 <asp:HiddenField ID="hdnPCAHouseNumber" runat="server" Value="" />
 <asp:HiddenField ID="hdnPCADistrict" runat="server" Value="" />
 <asp:HiddenField ID="hdnPCACity" runat="server" Value="" />
  <asp:HiddenField ID="hdnPCACountry" runat="server" Value="" />
    <asp:HiddenField ID="hdnDialingCode" runat="server" Value="" />
  <asp:HiddenField ID="hdnPCAPostCode" runat="server" Value="" />
   <asp:HiddenField ID="hdnPCAAddress" runat="server" Value="" />
    <asp:HiddenField ID="hdnEnterAddManually" runat="server" Value="" />
     <asp:HiddenField ID="hdnEnterManualyEdit" runat="server" Value="" />
     <asp:HiddenField ID="hdnPCAEdit" runat="server" Value="" />
    
    <script>


        function failedETEmail() {
            // alert('ss');
            ShowRowInError('CustomerEmail');
            setDisplayState('ErrorCustomerEmail', true);
            return false;
        }
        function PassedETEmail() {
            ShowValid('CustomerEmail');
            setDisplayState('ErrorCustomerEmail', false);
            return false;
        }




       






//        Sys.Application.add_load(jScript);
    
    </script>

    </form>
</body>
</html>
