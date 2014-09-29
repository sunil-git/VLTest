<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="Serenataflowers.ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Controls/CheckoutFooter.ascx" TagName="CheckoutFooter" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/m_Terms.ascx" TagName="Terms" TagPrefix="uc9" %>
<%@ Register Src="~/Controls/m_Privacy.ascx" TagName="Privacy" TagPrefix="uc10" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />  
 <meta name="viewport" content="width=device-width, initial-scale=1.0" />
 <title>Serenata Flowers - Sorry</title>
<link rel="stylesheet" href="http://code.jquery.com/mobile/1.3.1/jquery.mobile.structure-1.3.1.min.css" />
<script src="http://code.jquery.com/jquery-1.10.0.min.js"></script>
<script>
    $(document).bind("mobileinit", function () {
        $.mobile.ajaxEnabled = false;
        $.mobile.loadingMessage = false;
        $.mobile.pageLoadErrorMessage = false;
        $.mobile.pushStateEnabled = false;
    });
 </script>
<script src="http://code.jquery.com/mobile/1.3.1/jquery.mobile-1.3.1.min.js"></script> 
<link rel="stylesheet" href="stylesheets/serenata-flowers.min.css">	

<link rel="stylesheet" href="stylesheets/custom.css">

<script type="text/javascript" src="//nexus.ensighten.com/serenata/dev/Bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
       <div data-role="page" id="reset-password" data-title="Reset password">


	
	<div data-role="header" data-theme="c" id="header">
		<a href="../Default.aspx" data-role="none"><div id="logo-checkout-flowers"></div></a>		
	</div>


	<div data-role="content" data-theme="c">	 

		
			<h3 style="text-align:left;color:#8aa064;" runat="server" id="errMessage">Opps,Something has gone wrong</h3>		
			
            <span style="display:block" class="errortext" id="SmallResetNewPwd" runat="server"></span>

 <br />
			<asp:Button  ID="Revist" runat="server" data-theme="d" Text="Revist"  OnClick="Revist_Click" />
	
		
	</div>

	<div data-role="footer" data-theme="c" style="padding:0 15px 0 15px;">
	 <uc2:CheckoutFooter ID="Footer" runat="server" />
	</div>  
   
</div> 
 <uc9:Terms ID="Termsandcondition" runat="server" />
 <uc10:Privacy ID="Privacy" runat="server" />
    </form>
</body>
</html>
