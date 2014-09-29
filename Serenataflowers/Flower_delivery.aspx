<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Flower_delivery.aspx.cs" Inherits="Serenataflowers.Flower_delivery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 <%@ Register Src="Controls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
    <%@ Register Src="Controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <asp:Literal ID="ltTitle"  runat="server"></asp:Literal>
   <title id="Title1"  runat="server" visible="false"></title>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" name="viewport">
    <link href="~/Styles/m_style.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
<body>
    <form id="form2" runat="server">
    <div id="page_wrapper">
    <!--Header DIV Start-->
    
     <!--Header DIV End-->
     <!--CONTENET DIV Start-->
    <div id="content_normal">

    <div id="header">
        <uc1:Header ID="Header1" runat="server" EnableUrl="true"/>
    </div>
    <div id="Div1"> 

		<div class="clearleft"></div>
        <br>
		
			<h2>Delivery information</h2>
				<br>
				<table id="form1" class="delvinfotable" bgcolor="#cccccc" border="0" cellpadding="2" cellspacing="1">
					<tbody><tr>
						<td colspan="2" align="left"> <h4 style="margin:0;">UK Flower Delivery (courier)</h4></td>						
					</tr>
					<tr>
						<td>Standard delivery weekdays - FREE.</td>
						<td>Order by 8pm for next day delivery </td>
					</tr>
					<tr>
						<td>Saturday delivery: FREE</td>
						<td>Order by 8pm Friday for Saturday delivery</td>
					</tr>
					<tr>
						<td colspan="2" align="left"> <h4 style="margin:0;">Guaranteed time flower delivery (courier)</h4></td>
						
					</tr>
					<tr>
						<td>Before 1pm delivery (8am - 1pm)</td>
						<td>£4.99 surcharge </td>
					</tr>
					
					
					
					<tr>
						<td colspan="2" align="left"> <h4 style="margin:0;">Royal Mail delivered flowers (Flowers By Post)</h4></td>
						
					</tr>
					<tr>
						<td>Standard delivery weekdays  - FREE</td>
						<td>Order by 4pm for next day delivery (excluding Mondays) </td>
					</tr>
					<tr>
						<td>Saturday delivery - FREE</td>
						<td>Order by  Friday 4pm for Saturday delivery </td>
					</tr>
					<tr>
						<td>Recorded Signed For</td>
						<td>£2.99 surcharge (Tuesday - Saturday) </td>
					</tr>
					<tr>
						<td colspan="2" align="left"> <h4 style="margin:0;">Florist Delivered flowers (including same day)</h4></td>
						
					</tr>
					<tr>
						<td>Standard delivery weekdays</td>
						<td>£6.99 surcharge (Tuesday - Saturday) </td>
					</tr>
					<tr>
						<td>Sunday delivery (Valentines and Mothers Day only)</td>
						<td>£9.99 surcharge</td>
					</tr>															
				</tbody></table>

		<div class="clearleft"></div><br>

				<br><h4 style="margin:0;">Next-day courier delivery</h4>
					
				<ul class="list">
						<li>All next-day courier delivered flowers are dispatched from our
 central production facility. Other products are dispatched direct from 
suppliers.</li>
						<li>Deliveries are normally made between 8am and 8pm. However, 
during busy periods, these delivery times may differ slightly, and we 
reserve the right to deliver between 7am and 10pm.</li>
						<li>Deliveries are available from Monday to Saturday. We are currently unable to deliver orders on a Sunday or Bank Holidays.</li>
						<li>Our next-day courier deliveries are only available to UK 
mainland addresses. We are not able to deliver to Northern Ireland, 
Rebublic of Ireland, Scottish Highlands, Channel Islands and some rural 
areas. We are also unable to deliver to PO Boxes or BFPO addresses.</li>
						<li>Where possible, we will attempt to obtain a signature from the intended recipient of the flowers.</li>
						<li>If standard delivery is chosen, we cannot guarantee an exact delivery time</li>
						<li>If no-one is available to receive the delivery, our courier 
will normally follow any delivery instructions provided, leave the order
 securely or with a neighbour and leave a delivery card stating the 
item's location. In the event that they are unable to leave the order 
securely, the driver may return the item(s) to their local depot and 
leave a delivery card giving the recipient the option to call to 
reschedule delivery or collect the order from the courier's local 
delivery depot. If no action is taken, the courier will normally 
re-attempt delivery the following working day. Please note that only one
 re-delivery attempt will be made.</li>
						<li>Under normal circumstances, we receive dispatch and delivery 
notifications from our courier. We then use that information to keep you
 informed of the delivery progress of your order via email and/or SMS 
message. However, this service is not guaranteed. In addition, there may
 be some delay between the occurence of a delivery event and us 
receiving notification.</li>
						<li>In the event of a non-delivery, customers are normally entitled to a refund or redelivery under our 100% satisfaction guarantee.</li>
						<li>In the event of late delivery on the chosen delivery date, we 
will normally refund the delivery charge associated with your order</li>
						<li>For deliveries to large businesses, hospitals, universities 
etc, deliveries will normally be made to the reception or post-room. 
Delivery to the named recipient is not possible to these types of 
addresses in many cases. Please see below for more information.</li>
						<li>If the address provided is incorrect or incomplete or if the 
delivery is refused, we reserve the right to charge up to 100% of the 
original order value to re-deliver to the same or an alternate address.</li>
						<li>We are unable to call recipient prior to delivery.</li>
						<li>We do not guarantee to be able to follow delivery instructions.</li>						
					</ul>

					<br><h4 style="margin:0;">Postal/Budget range delivery</h4>
				<p>
					</p><ul class="list">
						<li>Although delivery is normally on the chosen delivery date, we 
cannot guarantee an exact delivery date for our budget range. Flowers 
are normally delivered within 1 - 2 days of your chosen delivery date. 
To guarantee delivery, we recommend that the delivery date selected is 
the day before the flowers are required. However, this may mean that the
 flowers arrive a day early. </li>
						<li>In the event of late delivery, we are unable to offer a refund or a resend.</li>
						<li>Deliveries are available from Tuesday to Saturday. We are 
currently unable to deliver flowers from our budget range on a Monday, 
Sunday or Bank Holidays.</li>
						<li>We do not recommend that our value range be chosen for deliveries to hospitals or funerals.</li>
						<li>For large businesses, hospitals, universities etc, flowers 
will normally be delivered to the reception or post-room. Delivery to 
the named recipient is not possible to these types of addresses in many 
cases.</li>
						<li>We are unable to provide delivery notifications for orders delivered by post.</li>
						<li>Delivery instructions cannot be followed.</li>
						<li>Timed deliveries are not available.</li>
						<li>In the event of a non-delivery or late delivery, the customer must claim directly with the Royal Mail.</li>
					</ul>
					<ul>
						<br><h4 style="margin:0;">First class:</h4>
						<ul class="list">
						<li>Orders sent via First Class cannot be tracked.</li> 
						</ul>
						<br><h4 style="margin:0;">Recorded Signed For:</h4>
						<ul class="list">
							<li>For 'recorded signed for' deliveries, a signature will be 
collected from the person that receives the delivery. This may not be 
the person named on the address label. </li>
							<li>It is possible to track the delivery via the Royal Mail's 'Track &amp; Trace' website.</li>
						</ul>
					</ul>
					<p></p>
					
					<br><h4 style="margin:0;">Same-Day Delivery</h4>
						<ul class="list">							
							<li>All same-day UK deliveries are passed to a relay network who then pass orders onto local florists.</li>
							<li>For some rural areas or areas where our florist network does 
not have a florist, we may not be able to fulfill the order. In this 
case, we will contact you offering a full refund or a next day delivery 
fulfilled from our central production faciliity.</li>
							<li>Deliveries are available from Monday to Saturday. Deliveries 
are normally made between 8am and 6pm by a florist local to the 
recipient address. However, during busy periods, these delivery times 
may differ slightly, and we reserve the right to deliver between 7am and
 8pm in exceptional circumstances. We are currently unable to deliver 
flowers on a Sunday or Bank Holidays.</li>
							<li>Our same-day deliveries are only available to UK mainland 
addresses. We are not able to deliver to Northern Ireland, Rebublic of 
Ireland, Scottish Highlands, Channel Islands and some rural areas. We 
are also unable to deliver to PO Boxes or BFPO addresses.</li>
							<li>For same-day flower deliveries, we cannot guarantee an exact delivery time</li>							
							<li>We are unable to provide delivery notifications for same-day orders.</li>						
							<li>If no-one is available to receive the flowers, the florist's 
driver will normally leave a delivery card giving the recipient the 
option to call to reschedule delivery.</li>
							<li>In the event of a non-delivery, customers are normally entitled to a refund or redelivery under our 100% satisfaction guarantee.</li>
							<li>In the event of late delivery on the chosen delivery date, we
 will normally refund the delivery charge associated with your order</li>
							<li>For deliveries to large businesses, hospitals, universities 
etc, although our florist's driver will try to deliver directly to the 
recipient, this is not guaranteed. In some cases, it may be necessary to
 leave flowers with the reception or post-room. It is then their 
responsibility to ensure that the flowers reach the recipient, Please 
see below for more information.</li>
							<li>If the recipient address provided is incorrect or incomplete 
or if the flowers are refused, we reserve the right to charge upto 100% 
of the original order value to re-deliver to the same or an alternate 
address.</li>
							<li>We do not guarantee to be able to follow delivery instructions.</li>
						</ul>
					
					<br><h4 style="margin:0;">International delivery</h4>
						<ul class="list">
							<li>All international  deliveries are passed to a relay network who then pass orders onto local florists.</li>
							<li>For some deliveries, it may be necessary for our courier to 
call the recipient to ensure that someone will be available to receive 
the flowers.</li>
							<li>Deliveries are available from Monday to Saturday. Deliveries 
are normally made between 8am and 6pm by a florist local to the 
recipient address. However, during peak periods, deliveries may be made 
until 8pm. We are currently unable to deliver flowers on a Sunday or 
Bank Holidays.</li>
							<li>For same-day flowers, deliveries can take place anytime between 8am and 7pm</li>
							<li>We are unable to provide delivery notifications for international orders.</li>						
							<li>If no-one is available to receive the flowers, the florist's 
driver will normally leave a delivery card giving the recipient the 
option to call to reschedule delivery.</li>
							<li>In the event of a non-delivery, customers are normally entitled to a refund or redelivery under our 100% satisfaction guarantee.</li>
							<li>In the event of late delivery on the chosen delivery date, we
 will normally refund the delivery charge associated with your order</li>
							<li>For deliveries to large businesses, hospitals, universities 
etc, although our florist's driver will try to deliver directly to the 
recipient, this is not guaranteed. In some cases, it may be necessary to
 leave flowers with the reception or post-room. It is then their 
responsibility to ensure that the flowers reach the recipient. Please 
see below for more information.</li>
							<li>If the recipient address provided is incorrect or incomplete 
or if the flowers are refused, we reserve the right to charge upto 100% 
of the original order value to re-deliver to the same or an alternate 
address.</li>
							<li>We do not guarantee to be able to follow delivery instructions.</li>
						</ul>

					<br><h4 style="margin:0;">Product-specific delivery information</h4>		
					<ul>
						<br><h4 style="margin:0;">Flowers</h4>
						<ul class="list">					
							<li>Our next-day flowers and flowers by post are delivered in a sturdy box and wrapped with cellophane for protection in transit. </li>
						</ul>					
					</ul>					
			  <ul>
						<br><h4 style="margin:0;">Hampers</h4>
				  <ul class="list">
					  <li>Hampers are normally dispatched to arrive on the selected delivery date or the day before.</li>
					  <li>Depending on the size of hamper, delivery will be either via Royal Mail or via courier. </li>
	              </ul>
					</ul>
					<a name="hospitals"></a><br><h4 style="margin:0;">Other important delivery information</h4>
					<ul class="list">
					<li>To enable us to contact you regarding a problem with your 
order, please make sure that you have provided a valid email address 
and/or contact telephone numbers.</li>
					<li>Please ensure that someone is available at the delivery address to receive the delivery.</li>
					<li>In the event of a delivery problem, if we are able to, we will try and contact the customer to try to resolve the issue.</li>
					<li>In rare circumstances, our drivers are unable to deliver an 
order. This may be due to problem with the delivery vehicle. We ensure 
that these occurrences are minimised, but in the event that we are 
unable to deliver the order owing to circumstances that are within our 
control, we will contact you immediately via email or phone to arrange a
 redelivery and refund the original delivery charge. We reserve the 
right to provide additional compensation in the form of a discount off a
 future purchase from www.SerenataFlowers.com. No other form of 
compensation is accepted.</li>
					<li>To ensure a successful delivery, please ensure that the 
delivery address is accurate and complete. If possible, please provide 
additional delivery instructions to help the delivery driver to find the
 recipient's address.</li>
					<li>We cannot be held responsible for failed deliveries that result
 from a problem with the address and/or postcode you provide at the time
 of order placement, if the delivery was refused by the recipient or the
 fact that the recipient no longer lives at the address provided.</li>
					<li>We are not responsible for any other costs incurred by the customer due to failed deliveries.</li>
					</ul>
					

					<br><h4 style="margin:0;">Deliveries to businesses, hospitals, universities</h4>
					<p>To ensure deliveries are successful, we recommend choosing a 
timed delivery option especially during peak periods (Christmas, 
Valentines Day and Mothers Day).</p>
					<p>In some circumstances, for example, when delivering to some 
business addresses, hospitals or universities, we may not be able to 
deliver the flowers directly to the intended recipient. Please note that
 these circumstances are outside our control. In this case, we will 
deliver to the reception area or post room and (where possible) obtain a
 signature from the receptionist (or equivalent). This signature will be
 our record of delivery.</p>
					<p>To ensure the highest likelihood of delivery success, please 
ensure that you provide as much information regarding the location of 
the recipient as possible within the delivery instructions, including 
department name, floor number, etc. </p>
					<br><h4>Hospital-specific delivery issues</h4>
					<p>Deliveries to hospitals are often problematic as patients move 
from ward to ward, or are discharged. In addition, many hospitals do not
 accept flower deliveries due to health &amp; safety restrictions.</p>
					<p>It is for this reason that we no longer recommend that flowers 
are delivered to a hospital. If an order is placed for delivery to a 
hospital and the delivery is not successful, we reserve the right to not
 refund or resend the order.</p>
					
					<br><h4>University-related delivery issues</h4>
					<p>Due to the size of university campuses, it can often be 
difficult to locate recipients at universities. We are therefore not 
able to deliver to recipients themselves and will always deliver to the 
university postroom or reception. Please ensure that you include enough 
information for the postroom/reception staff to be able to locate the 
intended recipient of the flowers. We are not liable for the 
non-delivery of flowers to recipients at universities as we have no 
control of the delivery once it reaches the postroom/reception. </p>
					<br><h4>Other business address delivery issues</h4>
					<p>If you wish to order flowers for delivery to a business address,
 we recommend that you use one of our timed delivery services (where 
available)to ensure delivery is made within office hours.</p>
					<p>In the event that our standard delivery option is chosen for a 
business address and a delivery between 8.00 a.m and 6.30 p.m. fails 
owing to the recipient not being available, or the business being 
closed, we will not accept any liability for the non-delivery and 
reserve the right to charge an additional production and delivery charge
 for any subsequent redelivery. If you decide to cancel the order 
instead of arranging a redelivery, we reserve the right to deduct a 
cancellation fee from the refund value.</p>
					
		
		<div class="clearleft"></div><br>
		
		


		

		
	</div> 

      <div id="FooterDiv">
    <uc2:Footer ID="Footer1" runat="server" />
    </div>
    </div>
     <!--CONTENET DIV End-->

   
    <!--Footer DIV End-->
    </div>
    </form>
</body>
</html>
