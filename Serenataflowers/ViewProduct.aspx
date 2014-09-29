<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewProduct.aspx.cs" Inherits="Serenataflowers.ViewProduct" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="Controls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="Controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <asp:Literal ID="ltTitle"  runat="server"></asp:Literal>
   <title  runat="server" visible="false"></title>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" name="viewport">
    <link type="text/css" rel="stylesheet" href="styles/m_style.css" />
</head>
<script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
<body onload="getStorage();">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
       <asp:HiddenField ID="hdnbrowserID" runat="server" Value="" />
         <asp:HiddenField ID="hdndeviceID" runat="server" Value="" />
          <asp:HiddenField ID="hdnaltVisitorID" runat="server" Value="" />
   
      <script type="text/javascript">
          function getStorage() {
            
              if (window.sessionStorage) {

                  document.getElementById('hdnbrowserID').value = sessionStorage.getItem("browserID") //returns "Some Value"               
                              
                  document.getElementById('hdndeviceID').value = sessionStorage.getItem("deviceID") //returns "Some Value"
                  document.getElementById('hdnaltVisitorID').value = sessionStorage.getItem("altVisitorID") //returns "Some Value"
                  

              }
          }
</script>
    <div id="page_wrapper">
        <div id="content_normal">
            <!--Header DIV Start-->
            <div id="header">
                <uc1:Header ID="Header1" runat="server" EnableUrl="true" />
            </div>
            <!--Header DIV End-->
            <div class="clearleft">
            </div>
            <div id="mainimage" class="mainimgtop">
                <h2>
                    <%--Product Title with Old Price and Offer Price--%>
                    <asp:Label ID="lblProductTitle" runat="server" />
                    - <span class="oldprice" runat="server" id="spanOldPriceHeader">
                        <asp:Label ID="lblOldPrice" runat="server" />
                    </span>&nbsp;<span class="offerprice"><asp:Label ID="lblOfferPrice" runat="server" />
                    </span>
                </h2>
                 <%--Product Review details--%>
                 <div id="divOverlay" runat="server">           
                <span id="deliveryOverlay" Style="font: 11px tahoma,arial,verdana";>               
                 <asp:Image ID="imgStarRating" runat="server" BorderStyle="None" ImageAlign="AbsMiddle"/>
                <b><asp:Label ID="lblRating" runat="server" /></b>
                <asp:Label ID="lblof" runat="server" />
                <b><asp:Label ID="lblRatingCount" runat="server" /></b>
                <br>
                <a id="review"  href=""><asp:Label ID="lblReviewDesc" runat="server" /></a>
                </span>
                 </div>
                <div class="large_overlay">
                    <%--Large Image--%>
                    <asp:Image ID="imgLargeProduct" runat="server"  Width="300px" Height="350px" />
                </div>
            </div>
            <div class="step">
                <h3>
                    1. Please select a Product</h3>
                <asp:RadioButtonList CssClass="lfloat " ID="rbtnupgrade" runat="server"
                                RepeatColumns="1"> </asp:RadioButtonList>
                <div class="clearleft">
                </div>
                <br />
            </div>
            <div class="step">
                <h3>
                    2. Please select delivery date</h3><br />
                <asp:DropDownList ID="ddlDeliveryDates" runat="server" AutoPostBack="true" CssClass="lfloat"
                    OnSelectedIndexChanged="ddlDeliveryDates_SelectedIndexChanged"  />
                    <%--Style="font: 1em/1.4 courier new,Arial,Geneva,Helvetica,Sans-Serif;margin: 5px 0 0"--%>
                <div class="clearleft">
                </div>
                <br />
            </div>
            <div class="step">
                <h3>
                    3. Choose time</h3>
                <div id="delOptionSection">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:RadioButtonList CssClass="lfloat " ID="rbtnLstDeliveryOptions" runat="server"
                                RepeatColumns="1">
                            </asp:RadioButtonList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlDeliveryDates" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="clearleft">
                </div>
                <br />
            </div>
            <div class="step">
                <div class="clearleft">
                </div>
                <br />
                <div align="center">
                    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                            <asp:ImageButton runat="server" ID="imgBtnOrdernow" ImageUrl="http://images.serenataflowers.com/ordernownew08.gif" 
                                OnClick="imgBtnOrdernow_Click" CssClass="orderbtn" />
                     <%--   </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>              
                <div class="clearleft">
                </div>
                <br />
            </div>
             <div class="clearleft">
            </div>
              <div class="step" runat="server" id="dvcheckout" style="display:none" >
                       <div style="padding-left:38%">
                         <a class="btnorange"   id="ancsavebtn" runat="server" href="#"><span>checkout »</span></a>
                      </div>
                   </div>
            <div class="clearleft">
            </div>
            <br />
            <h2>
                <%--Product Title with Old Price and Offer Price--%>
                <asp:Label ID="lblProductTitleAtFooter" runat="server" />-
                <br />
                <span class="oldprice" runat="server" id="spanOldPriceFooter">
                    <asp:Label ID="lblOldPriceAtFooter" runat="server" /></span> <span class="offerprice">
                        <asp:Label ID="lblOfferPriceAtFooter" runat="server" CssClass="offerprice " /></span>
            </h2>
            <div class="clear">
            </div>
            <br />
            <strong>
                <asp:Label ID="lblBottomProductInfo" runat="server"></asp:Label>
            </strong>
            <br />
            <span>
                <asp:Label ID="lblProductDesciption" runat="server"></asp:Label>
            </span>
           <div class="clear">
            </div>
            <br />
            <!--Footer DIV Start-->
            <div id="FooterDiv">
                <uc2:Footer ID="Footer1" runat="server" />
            </div>
            <!--Footer DIV End-->
        </div>
           
        <asp:HiddenField ID="hdnOptionalPrice" runat="server" Value="0" />
        
        <!-- Added below Hidden field to store product price -->
        <asp:HiddenField ID="hdnProductPrice" runat="server" Value="0" />
      
    </div>
    </form>
</body>
</html>
