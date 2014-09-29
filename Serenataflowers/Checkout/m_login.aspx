<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_login.aspx.cs" Inherits="Serenataflowers.Checkout.m_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Controls/CheckoutHeader.ascx" TagName="CheckoutHeader" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/CheckoutFooter.ascx" TagName="CheckoutFooter" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/Panel.ascx" TagName="Panel" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/EditBasket.ascx" TagName="EditBasket" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/BasketCount.ascx" TagName="BasketCount" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/EditDeliveryDate.ascx" TagName="EditDeliveryDate" TagPrefix="uc6" %>
<%@ Register Src="~/Controls/Voucher.ascx" TagName="Voucher" TagPrefix="uc7" %>
<%@ Register Src="~/Controls/ContactUs.ascx" TagName="ContactUs" TagPrefix="uc8" %>
<%@ Register Src="~/Controls/m_Terms.ascx" TagName="Terms" TagPrefix="uc9" %>
<%@ Register Src="~/Controls/m_Privacy.ascx" TagName="Privacy" TagPrefix="uc10" %>
<%@ Register Src="~/Controls/PasswordReminder.ascx" TagName="ResetPassword" TagPrefix="uc11" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />  
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title>Serenata Flowers - Login / Register</title>
<link rel="stylesheet" href="https://code.jquery.com/mobile/1.3.1/jquery.mobile.structure-1.3.1.min.css" />
<script src="https://code.jquery.com/jquery-1.10.0.min.js"></script>
<script>
    $(document).bind("mobileinit", function () {
        $.mobile.ajaxEnabled = false;
        $.mobile.loadingMessage = false;
        $.mobile.pageLoadErrorMessage = false;
        $.mobile.pushStateEnabled = false;
    });
 </script>
<script src="https://code.jquery.com/mobile/1.3.1/jquery.mobile-1.3.1.min.js"></script> 
<link rel="stylesheet" href="../stylesheets/serenata-flowers.min.css">
<link rel="stylesheet" href="../stylesheets/custom.css">


<style>
.progress-step {font-size:12px;background:#8aa064;padding:10px;color:#fff;text-shadow:1px 1px 0 #5e7040;}
.progress-step-disabled {font-size:12px;background:#f1f1f1;padding:10px;color:#ccc;text-shadow:1px 1px 0 #a9a8a8;}
.step-btn {width:100%;display:block;}







</style>

    <style type="text/css" media="screen">
        textarea.ui-input-text
        {
            height: 40px !important;
            transition: height 200ms linear 0s;
        }
        

    </style>

<script src="../Scripts/commonfunctions.js"></script> 
<script src="../Scripts/m_login.js"></script> 


<script type="text/javascript" src="//nexus.ensighten.com/serenata/dev/Bootstrap.js"></script>


</head>
<body onload="SocialLoad();">
    <form id="Login" runat="server">
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
          function clickCheckoutAsGuest() {
            
              __doPostBack("<%= this.ChechoutAsGuest.UniqueID %>", '');
          }
          function clickRegister() {

              __doPostBack("<%= this.Register.UniqueID %>", '');
          }
          function ExpnadExitCustomer() {
              $("#existCustomer").collapsible({ collapsed: false });
            
          }
          //"ui-collapsible-content ui-body-d" aria-hidden="false"
        </script>
               <script type="text/javascript">


                   //            function onLoad() {
                   //                // get user info
                   //                //gigya.socialize.getUserInfo({ callback: renderUI });

                   //                // register for connect status changes
                   //                gigya.socialize.addEventHandlers({ onLogin: renderUI, onLogout: renderUI });

                   //            }

                   function renderUI(res) {
                       if (res.user != null && res.user.isConnected) {
                       
                           document.getElementById('hdnSNEmail').value = res.user.email;
                           document.getElementById('hdnSNCountry').value = res.user.country;
                           document.getElementById('hdnSNTownCity').value = res.user.city;
                           document.getElementById('hdnSNName').value = res.user.firstName;
                           document.getElementById('hdnSNLastname').value = res.user.lastName;

                           document.getElementById('hdnSNPhone').value = res.user.phones;
                           document.getElementById('hdnSNAddressLine1').value = res.user.address;

                           document.getElementById('hdnSNloginProvider').value = res.user.loginProvider;
                           document.getElementById('hdnSNusername').value = res.user.username;
                           __doPostBack("<%= this.btnSocialLogin.UniqueID %>", '');


                       }
                   }


                   function showEditBasket() {
                       document.getElementById("RowWarnQty").style.display = "none";
                       //document.getElementById('EditBasketheader').innerHTML = "Your Basket";
                       $.mobile.changePage('#EditBasket', { transition: 'pop', role: 'dialog' });
                   }       
	    </script>
    <div data-role="page" id="login-checkout" data-title="Sign in or Register">

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
    <!-- header -->
<!-- content -->
	<div data-role="content" data-theme="c">	 
			  <asp:UpdatePanel ID="udpLogin" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
		<div style="clear:both;"></div><br /><br />
		
		<div data-role="collapsible"  data-inset="false" data-theme="b" data-content-theme="d" data-collapsed-icon="arrow-d" data-expanded-icon="arrow-u" data-iconpos="right">
			<h4>New Customer</h4>			

			<fieldset class="ui-grid-a">
					<div class="ui-block-a">               
                <button type="submit" data-theme="c" onclick="javascript:clickCheckoutAsGuest();">Checkout<br /> as guest</button></div>
					<div class="ui-block-b"><button type="submit" data-theme="d" onclick="javascript:clickRegister();">Register<br /> now</button></div>	   
			</fieldset>

			<h5 style="text-align:center;margin:5px 0 5px 0;">OR</h5>
			
			<div class="ui-bar ui-bar-a" style="text-align:center;"><h3>Sign in with Social account</h3></div>

			
<div style="clear:both;"></div><br />
			
<div align="center" style="text-align:center;">
				
<%--<a href="">
<img src="../images/fc-webicon-facebook.png" border="0" alt="login with facebook" /></a>
				<a href=""><img src="../images/fc-webicon-twitter.png" border="0" alt="login with Twitter" /></a>
				<a href=""><img src="../images/fc-webicon-googleplus.png" border="0" alt="login with Google plus" /></a>
				<a href=""><img src="../images/fc-webicon-linkedin.png" border="0" alt="login with LinkedIn" /></a>	--%>	
                <script type="text/javascript">
                    var login_params =
                    {                       
                     showTermsLink: 'false'
                    ,hideGigyaLink:true
                    , height: 92
                    , width: 280
                    , containerID: 'componentDiv'                    
                    , UIConfig: '<config><body><controls><snbuttons buttonsize="50" /></controls></body></config>'
                    , autoDetectUserProviders: ''
                    , facepilePosition: 'none'                   
                    }
                    </script>
                <div id="componentDiv" align="center" ></div>
            <script type="text/javascript">
                gigya.socialize.showLoginUI(login_params);
            </script>

				<div style="clear:both;"></div><br />
				<a href="#aboutSocial" data-rel="dialog" data-transition="pop">Tell me about social accounts</a>
			</div>

			

		</div>


		<div data-role="collapsible"  id="existCustomer" data-inset="false" data-theme="b" data-content-theme="d" data-collapsed-icon="arrow-d" data-expanded-icon="arrow-u" data-iconpos="right">
			<h4>Existing Customer</h4>
			
			<label for="password"><span style="color:red;">*</span>Email:</label>
                            <textarea rows="1" cols="1" id="LoginEmailAddress" runat="server" onblur="ValidateLoginEmail();" clientidmode="Static"
                                placeholder="Email address" ></textarea>



                            <span style="display:none" class="errortext" id="ErrorLoginEmailAddress">Please enter a valid email address.</span>



			<label for="password"><span style="color:red;">*</span>Password:</label>
            <asp:TextBox ID="LoginPassword" runat="server"  ClientIDMode="Static" onblur="ValidateloginPassword();" 
                            MaxLength="20" TextMode="Password"></asp:TextBox>
                            <span style="display:none" class="errortext" autocomplete="off" id="ErrorLoginPassword">Please enter Password.</span>
			                <span class="errortext" runat="server" id="ErrorSignIn" clientidmode="Static" style="display:none"></span>
			
			<fieldset class="ui-grid-a">
					<div class="ui-block-a"><asp:Button ID="SignIn" runat="server" Text="Sign in"  OnClientClick="javascript: return ValidateloginDetails();" OnClick="SignIn_Click" /></div>					
                    <div class="ui-block-b"><a href="#ResetPassword"  data-rel="dialog" data-transition="pop" style="float:right;margin-top:5px;">Forgotten password?</a></div>	   	   
			</fieldset>

			<h5 style="text-align:center;margin:5px 0 5px 0;">OR</h5>
			
			<div class="ui-bar ui-bar-a" style="text-align:center;"><h3>Sign in with Social account</h3></div>

			<div style="clear:both;"></div><br />
			<div align="center" style="text-align:center;">
					  <script type="text/javascript">
					      var login_params1 =
                    {
                        showTermsLink: 'false'
                    , hideGigyaLink: true
                    , height: 92
                    , width: 280
                    , containerID: 'componentDiv1'
                    , UIConfig: '<config><body><controls><snbuttons buttonsize="50" /></controls></body></config>'
                    , autoDetectUserProviders: ''
                    , facepilePosition: 'none'
                    }
                    </script>
                     <div id="componentDiv1" align="center" ></div>
                       <script type="text/javascript">
                           gigya.socialize.showLoginUI(login_params1);
            </script>	
				<div style="clear:both;"></div><br />
				<a href="#aboutSocial" data-rel="dialog" data-transition="pop">Tell me about social accounts</a>
			</div>

			

			
			

		</div>

 </ContentTemplate>
  <Triggers>
           <asp:AsyncPostBackTrigger ControlID="SignIn" EventName="Click" />
           </Triggers>
  </asp:UpdatePanel>
		
	</div>
    <!-- content -->
<!-- footer-->
	<div data-role="footer" data-theme="c" style="padding:0 15px 0 15px;">
	  <uc2:CheckoutFooter ID="Footer" runat="server" />
	</div>  

    <!-- footer-->
    <uc3:Panel ID="Menu" runat="server" />

</div>  

   <!-- Popup Section-->
<uc4:EditBasket ID="ModifyBasket" runat="server" UpdateMode="Conditional" OnButtonClick="Save_Click"  />
<uc6:EditDeliveryDate ID="ModifyDeliveryDate" runat="server" UpdateMode="Conditional" OnButtonClick="SaveDates_Click" />  
<uc7:Voucher ID="VoucherCode" runat="server" UpdateMode="Conditional" OnButtonClick="Voucher_Click" /> 
<uc8:ContactUs ID="UserContactUs" runat="server" />
<uc11:ResetPassword ID="SendPassword" runat="server" />
 <uc9:Terms ID="Termsandcondition" runat="server" />
 <uc10:Privacy ID="Privacy" runat="server" />

   <div data-role="page" data-theme="a" id="aboutSocial" data-title="Social Accounts">
<!-- About Social Accounts -->
	<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:left;">About Social Accounts</h3>
	</div>

		<div data-role="content" data-theme="a">	

							<p style="font-size:12px">By signing in with your social account you dont have to remember "one more password". As most of our customers are already signed in to Facebook or any other social media, they dont have to sign in to Serenata to get access to their address book or previous orders. </p>	
	
			
		</div><!-- /content -->
	
	<div data-role="footer" style="padding:15px 0 15px 0;text-align:center;">		
		
	</div>
</div> 
<!-- About Social Accounts -->  

   <!-- Popup Section-->

 <asp:Button ID="btnSocialLogin" runat="server" Text="Button" Visible="false"  OnClick="btnSocialLogin_Click"/>
  <asp:Button ID="ChechoutAsGuest" runat="server" Text="" Visible="false" OnClick="ChechoutAsGuest_Click" />
  <asp:Button ID="Register" runat="server" Text="" Visible="false" OnClick="Register_Click" />

  
    <asp:HiddenField ID="hdnSNAddressLine1" runat="server" Value="" />   
    <asp:HiddenField ID="hdnSNTownCity" runat="server" Value="" />   
    <asp:HiddenField ID="hdnSNCountry" runat="server" Value="" />
     <asp:HiddenField ID="hdnSNEmail" runat="server" Value="" />
     <asp:HiddenField ID="hdnSNName" runat="server" Value="" />
     <asp:HiddenField ID="hdnSNPhone" runat="server" Value="" />
     <asp:HiddenField ID="hdnSNloginProvider" runat="server" Value="" />
     <asp:HiddenField ID="hdnSNusername" runat="server" Value="" />
      <asp:HiddenField ID="hdnSNLastname" runat="server" Value="" />
    </form>
</body>
</html>
