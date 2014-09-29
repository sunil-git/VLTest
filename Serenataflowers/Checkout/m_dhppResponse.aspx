<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_dhppResponse.aspx.cs" Inherits="Serenataflowers.Checkout.m_dhppResponse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />  
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title>Serenata Flowers - Payment</title>
  <meta charset="utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
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
<script src="../Scripts/commonfunctions.js"></script> 
</head>
<body onload="HideTC();">
    <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true"  runat="server">
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

          function displayError() {
            
              setDisplayState('ErrorChase', true);
            
          }
          function HideError() {

              setDisplayState('ErrorChase', false);


          }
          function HideTC() {
              window.parent.document.getElementById('divcheckbox').style.display = "none";
              
          }
     </script>
  

    <div data-role="content" data-theme="c" id="ErrorChase">	 
		
		<div style="clear:both;"></div>
        
       <h5 style="line-height:1.5em;color:#888;">There was an issue with the card details entered!. <br /><br/ >Please make sure you double check the details before submitting the payment.</h5>		
			
			<h5 style="line-height:1.5em;color:red;" id="ErrorChase" runat="server"></h5><br />	
					  
			


			<div align="center" style="text-align:center;">
        <asp:Button ID="btnBacktoPayment" runat="server"  data-theme="a" data-icon="arrow-l" data-iconpos="left" Text="Back to card details" OnClick="btnBacktoPayment_Click" />
				<%--<a  href="m_paymentdetails.aspx?s=<%=Request.QueryString["s"]%>" data-role="button" data-theme="a" data-icon="arrow-l" data-iconpos="left">Back to card details</a>				--%>
			</div> 

				
		
	</div>

    </form>
</body>
</html>
