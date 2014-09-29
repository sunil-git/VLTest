<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_OrderTracking.aspx.cs" Inherits="Serenataflowers.m_OrderTracking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="Controls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="Controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Tracking</title>
     <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" name="viewport">
    <link type="text/css" rel="stylesheet" href="styles/m_style.css" />
</head>
<body>
    <form id="form1" runat="server">

    <div id="page_wrapper">  
	<div id="header">
                <uc1:Header ID="Header1" runat="server" EnableUrl="true" />
            </div>

	<div id="content_normal"> 
		
		<div style="clear:left;"></div>


			<h2 style="font-size:20px;">Track Your Order</h2>
			
			<p>If you need to know whether your gift has been delivered, or just want to find out the status of your recent order, then simply use our Order Tracking service below.</p>
            <br />
			<input type="text" id="txtOrder" runat="server" placeholder="Enter your order number. ie 214587542" style="width:90%;vertical-align:middle;padding:5px 10px 5px 10px;" />

                <br />  <br /> 
			<p style="margin-top:10px;">
		
                      <asp:LinkButton ID="TrackOrder" OnClick="TrackOrder_Click" CssClass="btnorange" runat="server"><span>Track now »</span></asp:LinkButton>
			</p>
           <br />
      <br />
        <br />
        
                   <div style="clear:left;" id="divresult" runat="server">
              	<h4 style="color:#4fb4e9;" id="OrderStatus" runat="server">You have an unfinished order</h4>
            </div>
                <br />
                 <br />
                  <br />
		<div style="clear:left;"></div>	
		<div id="FooterDiv">
                <uc2:Footer ID="Footer1" runat="server" />
            </div>
	</div>  
</div>  
      
 
    </form>
</body>
</html>
