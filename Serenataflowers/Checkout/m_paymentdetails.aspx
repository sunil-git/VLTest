<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_paymentdetails.aspx.cs" Inherits="Serenataflowers.Checkout.m_paymentdetails" %>
<%@ Register Src="~/Controls/CheckoutHeader.ascx" TagName="CheckoutHeader" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/CheckoutFooter.ascx" TagName="CheckoutFooter" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/EditDeliveryDate.ascx" TagName="EditDeliveryDate" TagPrefix="uc6" %>
<%@ Register Src="~/Controls/EditBasket.ascx" TagName="EditBasket" TagPrefix="uc4" %>

<%@ Register Src="~/Controls/m_Terms.ascx" TagName="Terms" TagPrefix="uc9" %>
<%@ Register Src="~/Controls/m_Privacy.ascx" TagName="Privacy" TagPrefix="uc10" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta charset="utf-8" />  
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title>Serenata Flowers - Paymentdetails</title>
  <meta charset="utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
<link rel="stylesheet" href="https://code.jquery.com/mobile/1.3.1/jquery.mobile.structure-1.3.1.min.css" />
<script src="https://code.jquery.com/jquery-1.10.0.min.js"></script>
<script>
    window.history.forward(1);
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
<script src="../Scripts/CutOffTimer.js" type="text/javascript"></script>
<script src="https://code.jquery.com/mobile/1.3.1/jquery.mobile-1.3.1.min.js"></script> 
<link rel="stylesheet" href="../stylesheets/serenata-flowers.min.css">
<link rel="stylesheet" href="../stylesheets/custom.css">
<script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
</head>
<body id="MasterBody" runat="server">
    <form id="paymentDetails" runat="server">
     <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
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
     </script>
     <script type="text/javascript">
         function hideshowPaymentDiv(objId) {
             var radiobutton = objId;
             if (radiobutton == 'radio-choice-0b') {
                 document.getElementById('divPaypal').style.display = 'block';
                 document.getElementById('divChase').style.display = 'none';
             }
             else {
                 document.getElementById('divChase').style.display = 'block';
                 document.getElementById('divPaypal').style.display = 'none';
             }
         }
         function HideError() {
             document.getElementById('divErrorDHPP').style.display = 'none';

         }
         function showError() {
             document.getElementById('divErrorDHPP').style.display = 'block';
         }
         function HideIsError() {
             document.getElementById('divPaymentExit').style.display = 'none';


         }
         function showIsError() {
             document.getElementById('divPaymentExit').style.display = 'block';

         }
         function SetTermState(objId) {

             if (document.getElementById(objId).checked) {

             }
             else {
                 document.getElementById('checkbox-Term').checked = true;
                 $.mobile.changePage('#paymentTerms', { transition: 'pop', role: 'dialog' });
             }

         }
         function Refresh() {
             var currentPageUrl = "";
             if (typeof this.href === "undefined") {
                 currentPageUrl = document.location.toString().toLowerCase();
             }
             else {
                 currentPageUrl = this.href.toString().toLowerCase();
             }
             return currentPageUrl;
         }
         function closeTerm() {
             $('.ui-dialog').dialog('close');

         }
</script>
       <style type="text/css" media="screen">
.fluidMedia {
    position: relative;
    padding-bottom: 56.25%; /* proportion value to aspect ratio 16:9 (9 / 16 = 0.5625 or 56.25%) */
    padding-top: 30px;
    height: 0;
    overflow: hidden;
}

.fluidMedia iframe {
    position: absolute;
    top: 0; 
    left: 0;
    width: 100%;
    height: 100%;
}
</style>
  <div data-role="page" data-theme="a" id="payment" data-title="Payment Details">
       <!-- /header //////////////////////////////////-->
	
	<div data-role="header" data-theme="c" id="header">
		  <uc1:CheckoutHeader ID="CheckoutHeader1" runat="server" EnableUrl="false" />
          <div class="header-btns">
	        
        </div>	
		<div style="clear:both;"></div>
			<fieldset class="ui-grid-b">
				<div class="ui-block-a step-active">
					<a id="ancArrowCust" runat="server" href="" style="color:#fff;">Customer </a><span class="arrow-left"></span>
				</div>
				<div class="ui-block-b step-active">
					<a id="ancArrowRec" runat="server" href="" style="color:#fff;">Recipient</a><span class="arrow-left"></span>
				</div>
				<div class="ui-block-c step-active">
					<a href="" style="color:#fff;cursor:default">Payment</a><span class="arrow-left"></span>
				</div>	
			</fieldset>	
	</div>
         
	<div data-role="content" data-theme="c">	 
		
		<div style="clear:both;"></div><br /><br />

		
			<div class="ui-bar ui-bar-a" style="margin:10px 0 10px 0;"><h3>Secure payment</h3> <img style="margin:-2px 0 2px 3px;" src="../images/secureicon.png" border="0" alt="" align="absmiddle" /></div>	

			<fieldset class="ui-grid-a">
				<div class="ui-block-a">
					<label for="radio-choice-0a">
	                	<input type="radio" name="radio-choice-0" id="radio-choice-0a" data-theme="a" data-mini="true" checked onclick="hideshowPaymentDiv(this.id)"/>Credit card
	                </label>
				</div>
				<div class="ui-block-b">
					<label for="radio-choice-0b">
	                	<input type="radio" name="radio-choice-0" id="radio-choice-0b" data-theme="a" data-mini="true" onclick="hideshowPaymentDiv(this.id)" />Paypal
	                </label>
				</div>	   
			</fieldset>
            <div style="float:left;display:none" id="divPaypal">
                <h1 style="font-size:16px;">Pay with PayPal</h1>
                 <a href="<%=PaypalURL%>">
				<img id="op_picture" src="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif"   border="0" alt="Pay with PayPal" />
				</a>
                 </div>
            <div  id="divChase">  
            <div style="float:none;display:none;" runat="server" id="divErrorDHPP">   
            <h5 style="line-height:1.5em;color:red;">Your session is expired.</h5>
					  
			<h5 style="line-height:1.5em;color:#888;">To try another card please <a onclick="location.href=''" href="">click here</a> or press the button below.</h5>


			<div align="center" style="text-align:center;">
				<a onclick="location.href=''" href="" data-role="button" data-theme="a" data-icon="arrow-l" data-iconpos="left">Try another card</a>				
			</div> 
             </div>   
             <div style="float:none;display:none;" runat="server" id="divPaymentExit">   
            <h5 style="line-height:1.5em;color:red;">Payment already recieved.</h5>
					  
			<h5 style="line-height:1.5em;color:#888;">We have already received payment for this order of £<span runat="server" id="isAmount"></span>.</h5>
            <div style="clear:both;"></div>							
						<br />

			
             </div>       
                       
		      <iframe frameborder="0" scrolling="no" width="100%" height="700" id="IframeDHHP"  src="<%=DoPayURL%>">
        
              </iframe>
              
           </div>
				<div id="divcheckbox" style="display:block">
		<label>
									<input type="checkbox" data-mini="true" onclick="SetTermState(this.id)" checked="checked" id="checkbox-Term" name="checkbox-Term"/>I accept terms and conditions
								</label>
</div>
								
	</div>
   
    <div data-role="footer" data-theme="c" style="padding:0 15px 0 15px;">
	  <uc2:CheckoutFooter ID="Footer" runat="server" />
	</div>
   
    </div>
 <uc9:Terms ID="Termsandcondition" runat="server" />
 <uc10:Privacy ID="Privacy" runat="server" />
       <uc6:EditDeliveryDate ID="ModifyDeliveryDate" runat="server" UpdateMode="Conditional" OnButtonClick="SaveDates_Click" />     
           <uc4:EditBasket ID="ModifyBasket" runat="server" UpdateMode="Conditional" OnButtonClick="Save_Click" />
 <!-- terms popup /////////////////// -->
<div data-role="page" id="paymentTerms" data-close-btn="right">

		<div data-role="header" data-theme="a" style="text-align:left;">				
		</div>

		<div data-role="content" data-theme="a">	
				<h1>Terms and conditions</h1>
				
				<p>By not agreeing to our Terms and conditions you can't proceed with payment and you will redirect your back to our home page.</p>

				<fieldset class="ui-grid-a">
					<div class="ui-block-a">
                    <a  href="" onclick="closeTerm();" data-role="button" data-theme="d">I Agree</a>				
						
					</div>
					<div class="ui-block-b">
                     <a  href="../Default.aspx" data-role="button" data-theme="d">I don't agree</a>				
						
					</div>	   
				</fieldset>
		</div>
		
	<div data-role="footer">		
	</div>
</div>

    </form>
</body>

</html>
