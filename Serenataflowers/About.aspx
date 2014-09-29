<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Serenataflowers.About" %>

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
    <form id="form1" runat="server">
    <div id="page_wrapper">
        <!--Header DIV Start-->
        <div id="header">
            <uc1:Header ID="Header1" runat="server" EnableUrl="true" />
        </div>
        <!--Header DIV End-->
        <!--CONTENET DIV Start-->
        <div id="content_normal">
            <h2>
                <b>Lucky 7</b><br />
                <span class="abtheading">week's worth of reasons why there's
                    no risk in choosing Serenata Flowers</span>
            </h2>
            <br />
            <h4 style="margin: 5px 0 5px 0;">
                <b>1. Much more than 15 minutes of fame</b></h4>
            <p style="margin: 5px 0 15px 0;">
                Our flowers are the equivalent of A-list celebrities: gorgeous, desirable and boasting
                a longer shelf life than their contemporaries. Sourced from the finest suppliers
                around the country, they're groomed with fastidious care, fashioned to perfection
                and elegantly transported to their destination in the shortest possible time. And
                Serenata Flowers have got staying power, too: the impact they create when they arrive
                means that you won't be quickly forgotten.</p>
            <h4 style="margin: 5px 0 5px 0;">
                <b>2. Freshness that puts daisies to shame</b></h4>
            <p style="margin: 5px 0 15px 0;">
                Instead of lounging about for days in the back of the floral-equivalent of a cross-channel
                tour bus that crawls to all the florist shops across the UK, Serenata's supermodels
                travel in style and in double-quick time. Air-con and foot spas (well, stem baths,
                to be truthful) are par for the course as our flowers are swanned direct from the
                Dutch auctions to our Heathrow Design Studio HQ.</p>
            <h4 style="margin: 5px 0 5px 0;">
                <b>3. More for love than for money (which means more for your money)</b></h4>
            <p style="margin: 5px 0 15px 0;">
                Relays schmelays, we say. When you order from Serenata Flowers, you order from a
                single, independent flower-loving company (the biggest one in the UK, in fact).
                We fight the good fight, and we do it alone. That means we're never passing on orders
                to other florists, taking a cut here, giving a cut there and appeasing any middlemen
                in between. For that reason, you only ever pay the true cost for a creative arrangement
                of exquisite blooms.
            </p>
            <h4 style="margin: 5px 0 5px 0;">
                <b>4. Not just perfect, but picture perfect</b></h4>
            <p style="margin: 5px 0 15px 0;">
                Every Serenata Flowers bouquet is lovingly crafted in-house by our team of inspired
                floral artists. It means we're able to monitor quality very closely and can promise
                that your purchase will never include a wilted blossom or tired stem. If we ever
                can't deliver exactly what you've ordered, we'll send you an email to let you know
                we'll be making a substitution. And you needn't fret about our florists' discretion...
                they would never dream of substituting anything of lesser value or of dodgy aesthetic
                appeal. Never.</p>
            <h4 style="margin: 5px 0 5px 0;">
                <b>5. Shiny, happy flower ambassadors</b></h4>
            <p style="margin: 5px 0 15px 0;">
                Our friendly, flower-loving customer service team like nothing more than shiny,
                happy customers. That's why they go out of their way to make ordering flowers as
                fun as receiving them. And we're happy to say it's not just us who think they're
                the bees' knees - they've got a shiny award from the DTI to prove it.
            </p>
            <h4 style="margin: 5px 0 5px 0;">
                <b>6. On time, every time</b></h4>
            <p style="margin: 5px 0 15px 0;">
                While there's a time and place for whimsy, we certainly don't think it's anywhere
                in between you placing an order on our website and your flower-loving friend being
                festooned with beautiful petals. That's why we only use the most reliable delivery
                service to chauffeur your flowers to their final destination. In short, your flowers
                arrive when you expect them, not when it suits someone else.
            </p>
            <h4 style="margin: 5px 0 5px 0;">
                <b>7. 100% satisfaction guarantee</b></h4>
            <p style="margin: 5px 0 15px 0;">
                Our auction house talent scouts are masters at spotting the hottest flowers on the
                floral catwalks. Every now and again, though, a charlatan gets through - a flower
                that looked far more than alright on the night, but who just can't go the distance.
                If any of these blooms end up in your bouquet, we promise to make up for our sins
                by offering you a <a href="#">full refund or replacement.</a>
            </p>
            <div class="clear">
            </div>
            <br />
        </div>
        <!--CONTENET DIV End-->
        <!--Footer DIV Start-->
        <div id="FooterDiv">
            <uc2:Footer ID="Footer1" runat="server" />
        </div>
        <!--Footer DIV End-->
    </div>
    </form>
</body>
</html>
