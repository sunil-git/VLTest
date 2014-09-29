<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Panel.ascx.cs" Inherits="Serenataflowers.Controls.Panel" %>
<div data-role="panel" id="rightpanel" data-position="right" data-display="reveal" data-theme="b">
		
        <ul data-role="listview" data-theme="a" data-mini="true">
			<li id="liEditDelDate" runat="server"><a href="#DeliveryDate" data-rel="dialog" data-transition="pop">Edit delivery date</a></li>
			<li id="liEditBasket" runat="server"><a href="#EditBasket" data-rel="dialog"  data-transition="pop">Edit Basket</a></li>
			<li id="liVoucher" runat="server"><a href="#Voucher" data-rel="dialog" data-transition="pop">Use a voucher code</a></li>
			<li id="liContact" runat="server"><a href="#Contact" data-rel="dialog" data-transition="pop">Contact us</a></li>			
		</ul>
        
		<div style="clear:both;"></div><br />

        <a href="#demo-links" data-rel="close" data-role="button" data-theme="c" data-icon="delete" data-inline="true">Close panel</a>

	</div>