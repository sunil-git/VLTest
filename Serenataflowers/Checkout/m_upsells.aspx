<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_upsells.aspx.cs" Inherits="Serenataflowers.Checkout.m_upsells" %>

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
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8" />  
 <meta name="viewport" content="width=device-width, initial-scale=1.0" />
      <asp:Literal ID="ltTitle"  runat="server"></asp:Literal>
     <title id="Title1"  runat="server" visible="false"></title>
  
     <link rel="stylesheet" href="https://code.jquery.com/mobile/1.3.1/jquery.mobile.structure-1.3.1.min.css" />
    <script src="https://code.jquery.com/jquery-1.10.0.min.js"></script> 


    <script src="../Scripts/CutOffTimer.js" type="text/javascript"></script>

<script>
$(document).bind("mobileinit", function(){
   $.mobile.ajaxEnabled =  false;
  $.mobile.loadingMessage =  false;
   $.mobile.pageLoadErrorMessage =  false;
 $.mobile.pushStateEnabled =  false;
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

          <style type="text/css" media="screen">
          textarea.ui-input-text {
            height: 40px!Important;
            transition: height 200ms linear 0s;
        }
	</style>
</head>
<script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
<body id="MasterBody" runat="server">
    <form id="Upsells" runat="server">
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

        function showEditBasket() {
            document.getElementById("RowWarnQty").style.display = "none";
            //document.getElementById('EditBasketheader').innerHTML = "Your Basket";
            $.mobile.changePage('#EditBasket', { transition: 'pop', role: 'dialog' });
        }

     </script>
    <div data-role="page" data-theme="a" id="upsells" data-title="Add Some Special Extras">
     <div data-role="header" data-theme="c" id="header">
		   <uc1:CheckoutHeader ID="Header" runat="server" EnableUrl="true" />
        <div class="header-btns">
	        <a href="#rightpanel" style="color:#fff;" class="menu-btn"></a>
            <a href="javascript:showEditBasket();" style="color:#fff;" data-rel="dialog" data-transition="pop" class="basket-btn" id="basketCount" runat="server">
            <uc5:BasketCount ID="DisplayBasketCount" runat="server" UpdateMode="Conditional" />
            </a>
	         
        </div>			
	</div>

    	<div data-role="content" data-theme="c">	 
		
		
		<div class="product-container">
			<h3 style="color:#8aa064;margin-top:-5px;">Add some special extras?</h3>
		<!-- vases /////////////// -->
			<div class="ui-bar ui-bar-a" id="divVases" runat="server" visible="false"><h3>Gorgeous Vases</h3></div>			
			
            <asp:Repeater ID="GorgeousVases" runat="server" OnItemDataBound="GorgeousVases_ItemDataBound" >
                <ItemTemplate>
                <div class="upselltable">
				    <a href="#zoom<%# DataBinder.Eval(Container.DataItem, "productid") %>" data-rel="dialog" data-transition="pop"><img class="upsellimage" src='<%# DataBinder.Eval(Container.DataItem, "img1_small_low")%>' border="0" alt="" width="75" height="88" /></a>
					    <span class="price"><%# DataBinder.Eval(Container.DataItem, "price", "{0:£0.00}")%></span>
					    <label style="width:35px;font-size:11px;margin-left:15px;"> <asp:CheckBox ID="Selectvase"  class="custom" data-theme="c" data-mini="true"  runat="server" />  &nbsp;</label>
                            <asp:HiddenField ID="hdnSpProdId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "productid") %>' />
                            <asp:HiddenField ID="hdnPrice" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "price") %>' />
			    </div>
                                       	
                </ItemTemplate>
            </asp:Repeater>
            
			<div style="clear:both;"></div><br />
		</div>	
		
		<div class="product-container">
		<!-- teddies //////////////// -->
			<div class="ui-bar ui-bar-a" id="divTeddy" runat="server" visible="false"><h3>Teddy Bears</h3></div>			
			   <asp:Repeater ID="TeddyBears" runat="server" OnItemDataBound="TeddyBears_ItemDataBound" >
                <ItemTemplate>
                <div class="upselltable">
				    <a href="#zoom<%# DataBinder.Eval(Container.DataItem, "productid") %>" data-rel="dialog" data-transition="pop"><img class="upsellimage" src='<%# DataBinder.Eval(Container.DataItem, "img1_small_low")%>' border="0" alt="" width="75" height="88" /></a>
					    <span class="price"><%# DataBinder.Eval(Container.DataItem, "price", "{0:£0.00}")%></span>
					    <label style="width:35px;font-size:11px;margin-left:15px;"> <asp:CheckBox ID="SelectTeddyBears"  class="custom" data-theme="c" data-mini="true"  runat="server" />  &nbsp;</label>
                            <asp:HiddenField ID="hdnTeddyBearProdId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "productid") %>' />
                            <asp:HiddenField ID="hdnTeddyBearPrice" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "price") %>' />
			    </div>
                                       	
                </ItemTemplate>
            </asp:Repeater>
            			
			<div style="clear:both;"></div><br />
		</div>	

		<div class="product-container">
		<!-- Chocolate, Birthday cake & Birthday Box //////////////// -->
			<div class="ui-bar ui-bar-a" id="DivChocolate" runat="server" visible="false"><h3>Chocolate, Cake & Gift Box</h3></div>			
			   <asp:Repeater ID="Chocolate" runat="server" OnItemDataBound="Chocolate_ItemDataBound" >
                <ItemTemplate>
                <div class="upselltable">
				    <a href="#zoom<%# DataBinder.Eval(Container.DataItem, "productid") %>" data-rel="dialog" data-transition="pop"><img class="upsellimage" src='<%# DataBinder.Eval(Container.DataItem, "img1_small_low")%>' border="0" alt="" width="75" height="88" /></a>
					    <span class="price"><%# DataBinder.Eval(Container.DataItem, "price", "{0:£0.00}")%></span>
					    <label style="width:35px;font-size:11px;margin-left:15px;"> <asp:CheckBox ID="SelectChocolate"  class="custom" data-theme="c" data-mini="true"  runat="server" />  &nbsp;</label>
                            <asp:HiddenField ID="hdnChocolateProdId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "productid") %>' />
                            <asp:HiddenField ID="hdnChocolatePrice" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "price") %>' />
			    </div>
                                       	
                </ItemTemplate>
            </asp:Repeater>
			<div style="clear:both;"></div><br />
		</div>	

			
		<div class="product-container">
		<!-- Balloons //////////////// -->
			<div class="ui-bar ui-bar-a" id="divBalloons" runat="server" visible="false"><h3>Balloons</h3></div>			
			
               <asp:Repeater ID="Balloons" runat="server" OnItemDataBound="Balloons_ItemDataBound" >
                <ItemTemplate>
                <div class="upselltable">
				    <a href="#zoom<%# DataBinder.Eval(Container.DataItem, "productid") %>" data-rel="dialog" data-transition="pop"><img class="upsellimage" src='<%# DataBinder.Eval(Container.DataItem, "img1_small_low")%>' border="0" alt="" width="75" height="88" /></a>
					    <span class="price"><%# DataBinder.Eval(Container.DataItem, "price", "{0:£0.00}")%></span>
					    <label style="width:35px;font-size:11px;margin-left:15px;"> <asp:CheckBox ID="SelectBalloons"  class="custom" data-theme="c" data-mini="true"  runat="server" />  &nbsp;</label>
                            <asp:HiddenField ID="hdnBalloonsProdId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "productid") %>' />
                            <asp:HiddenField ID="hdnBalloonsPrice" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "price") %>' />
			    </div>
                                       	
                </ItemTemplate>
            </asp:Repeater>
			<div style="clear:both;"></div><br />
		</div>	

        <div class="product-container">
		<!-- Wines //////////////// -->
			<div class="ui-bar ui-bar-a" id="divWines" runat="server" visible="false"><h3>Wines</h3></div>			
			
               <asp:Repeater ID="Wines" runat="server" OnItemDataBound="Wines_ItemDataBound" >
                <ItemTemplate>
                <div class="upselltable">
				    <a href="#zoom<%# DataBinder.Eval(Container.DataItem, "productid") %>" data-rel="dialog" data-transition="pop"><img class="upsellimage" src='<%# DataBinder.Eval(Container.DataItem, "img1_small_low")%>' border="0" alt="" width="75" height="88" /></a>
					    <span class="price"><%# DataBinder.Eval(Container.DataItem, "price", "{0:£0.00}")%></span>
					    <label style="width:35px;font-size:11px;margin-left:15px;"> <asp:CheckBox ID="SelectWines"  class="custom" data-theme="c" data-mini="true"  runat="server" />  &nbsp;</label>
                            <asp:HiddenField ID="hdnWinesProdId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "productid") %>' />
                            <asp:HiddenField ID="hdnWinesPrice" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "price") %>' />
			    </div>
                                       	
                </ItemTemplate>
            </asp:Repeater>
			<div style="clear:both;"></div><br />
		</div>	

		<div align="center" style="text-align:center;">
                <asp:Button ID="SaveAndCheckout" runat="server" Text="Save and Checkout"  OnClick="SaveAndCheckout_Click" />
        
		
		</div> 

		<br />

		<div align="center" style="text-align:center;">   
        <asp:Button ID="ContinueShopping" runat="server" Text="Continue shopping"  OnClick="ContinueShopping_Click" />      
			
		</div> 

		
		
	</div>

    <div data-role="footer" data-theme="c" style="padding:0 15px 0 15px;">
	   <uc2:CheckoutFooter ID="Footer" runat="server" />
	</div>  
     <uc3:Panel ID="Menu" runat="server" />
    </div>
<asp:Repeater ID="ZoomGorgeousVase" runat="server">
                <ItemTemplate>
                 <div data-role="page" id="zoom<%# DataBinder.Eval(Container.DataItem, "productid") %>" data-close-btn="right">

	<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:left;"><%# DataBinder.Eval(Container.DataItem, "producttitle") %></h3>
	</div>

	<div data-role="content" data-theme="a">	
		<div align="center">		
		<img src='<%# DataBinder.Eval(Container.DataItem, "img1_small_low")%>' border="0" alt="" />
		<p><%# DataBinder.Eval(Container.DataItem, "info2") %></p>
		</div>	
	</div>
	
	<div data-role="footer">		
	</div>
</div>     
                                       	
                </ItemTemplate>
            </asp:Repeater>
<asp:Repeater ID="ZoomTeddyBears" runat="server">
                <ItemTemplate>
                 <div data-role="page" id="zoom<%# DataBinder.Eval(Container.DataItem, "productid") %>" data-close-btn="right">

	<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:left;"><%# DataBinder.Eval(Container.DataItem, "producttitle") %></h3>
	</div>

	<div data-role="content" data-theme="a">	
		<div align="center">		
		<img src='<%# DataBinder.Eval(Container.DataItem, "img1_small_low")%>' border="0" alt="" />
		<p><%# DataBinder.Eval(Container.DataItem, "info2") %></p>
		</div>	
	</div>
	
	<div data-role="footer">		
	</div>
</div>     
                                       	
                </ItemTemplate>
            </asp:Repeater>
<asp:Repeater ID="ZoomChocolate" runat="server">
                <ItemTemplate>
                 <div data-role="page" id="zoom<%# DataBinder.Eval(Container.DataItem, "productid") %>" data-close-btn="right">

	<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:left;"><%# DataBinder.Eval(Container.DataItem, "producttitle") %></h3>
	</div>

	<div data-role="content" data-theme="a">	
		<div align="center">		
		<img src='<%# DataBinder.Eval(Container.DataItem, "img1_small_low")%>' border="0" alt="" />
		<p><%# DataBinder.Eval(Container.DataItem, "info2") %></p>
		</div>	
	</div>
	
	<div data-role="footer">		
	</div>
</div>     
                                       	
                </ItemTemplate>
            </asp:Repeater>
<asp:Repeater ID="ZoomBalloons" runat="server">
                <ItemTemplate>
                 <div data-role="page" id="zoom<%# DataBinder.Eval(Container.DataItem, "productid") %>" data-close-btn="right">

	<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:left;"><%# DataBinder.Eval(Container.DataItem, "producttitle") %></h3>
	</div>

	<div data-role="content" data-theme="a">	
		<div align="center">		
		<img src='<%# DataBinder.Eval(Container.DataItem, "img1_small_low")%>' border="0" alt="" />
		<p><%# DataBinder.Eval(Container.DataItem, "info2") %></p>
		</div>	
	</div>
	
	<div data-role="footer">		
	</div>
</div>     
                                       	
                </ItemTemplate>
            </asp:Repeater>
<asp:Repeater ID="ZoomWines" runat="server">
                <ItemTemplate>
                 <div data-role="page" id="zoom<%# DataBinder.Eval(Container.DataItem, "productid") %>" data-close-btn="right">

	<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:left;"><%# DataBinder.Eval(Container.DataItem, "producttitle") %></h3>
	</div>

	<div data-role="content" data-theme="a">	
		<div align="center">		
		<img src='<%# DataBinder.Eval(Container.DataItem, "img1_small_low")%>' border="0" alt="" />
		<p><%# DataBinder.Eval(Container.DataItem, "info2") %></p>
		</div>	
	</div>
	
	<div data-role="footer">		
	</div>
</div>     
                                       	
                </ItemTemplate>
            </asp:Repeater>
<!-- EditBasket Section Start-->
      <uc4:EditBasket ID="ModifyBasket" runat="server" UpdateMode="Conditional" OnButtonClick="Save_Click"  />     
<!-- EditAsket Section End-->
<!-- EditDelivery Section Start-->
       <uc6:EditDeliveryDate ID="ModifyDeliveryDate" runat="server" UpdateMode="Conditional" OnButtonClick="SaveDates_Click" />     
<!-- EditDelivery Section End-->
<!-- Voucher Section Start-->
       <uc7:Voucher ID="VoucherCode" runat="server" UpdateMode="Conditional" OnButtonClick="Voucher_Click" />     
<!-- Voucher Section End-->
<!-- Contact Us Section Start-->
 <uc8:ContactUs ID="UserContactUs" runat="server" />
  <uc9:Terms ID="Termsandcondition" runat="server" />
 <uc10:Privacy ID="Privacy" runat="server" />
 <!-- Contact Us Section End-->
<asp:Literal ID="ltrSpot" runat="server"></asp:Literal>
<asp:Button ID="btnHidden" runat="server" Visible="false" OnClick="btnHidden_Click" />

    </form>





</body>
</html>
