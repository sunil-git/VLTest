<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_recipientdetails.aspx.cs"
    Inherits="Serenataflowers.Checkout.m_recipientdetails" MaintainScrollPositionOnPostback="true" %>

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
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no" />
    <title>Serenata Flowers - Recipient Details</title>
    <link rel="stylesheet" href="https://code.jquery.com/mobile/1.3.1/jquery.mobile.structure-1.3.1.min.css" />
    <script src="https://code.jquery.com/jquery-1.10.0.min.js"></script>
    <script>
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
    <script src="../Scripts/commonfunctions.js"></script>
    <script src="../Scripts/m_recipientdetails.js"></script>
    <link rel="stylesheet" href="../stylesheets/custom.css">
    <script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
    <!-- OWL slider /////// -->
    <link rel="stylesheet" href="../stylesheets/owl.carousel.css">
    <link rel="stylesheet" href="../stylesheets/owl.theme.css">
    <script src="../javascripts/owl.carousel.js"></script>
</head>
<body id="MasterBody" runat="server">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">

        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            prm._scrollPosition = null;

            // xPos = $get('content2').scrollLeft;
            // yPos = $get('content2').scrollTop;
        }
        function EndRequestHandler(sender, args) {
            prm._scrollPosition = null;
            // $get('content2').scrollLeft = xPos;
            //$get('content2').scrollTop = yPos;
        }



        function bindEvents() {
            $(document).trigger('create');

        }

        bindEvents();


        //Re-bind for callbacks

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            bindEvents();


        });

        function ResetScrollPosition() {
            //setTimeout("window.scrollTo(0,800);", 0);
        }


        function showEditBasket() {
            document.getElementById("RowWarnQty").style.display = "none";
            //document.getElementById('EditBasketheader').innerHTML = "Your Basket";
            $.mobile.changePage('#EditBasket', { transition: 'pop', role: 'dialog' });
        }
        function CapturePlusLoaded(control) {
            control.selectCountry("GBR");
        }
        function countCharacters(myelement, maxLines) {
            var hidVal = document.getElementById('hdnMaxLength').value;
            var max_chars = parseInt(hidVal, 10);

            var counter = document.getElementById('mycounter');
            field = document.getElementById(myelement).value;
            field_length = field.length;

            if (field_length > max_chars) {
                // if too long...trim it!
                document.getElementById(myelement).value = field.substring(0, max_chars);
                counter.innerHTML = "You have reached the limit of " + max_chars + " characters for this field.";
            }
            else {
                // otherwise, update 'characters left' counter
                // Here we Calculate remaining characters  
                remaining_characters = max_chars - field_length;
                // Now Update the counter on the page 
                counter.innerHTML = remaining_characters + " characters left.";
            }

            var lines = field.replace(/\r/g, '').split('\n'), lines_removed;
            if (maxLines && lines.length > maxLines) {
                lines = lines.slice(0, maxLines);
                document.getElementById(myelement).value = lines.join('\n');
                counter.innerHTML = "You have reached the limit of " + maxLines + " lines for this field.";
            }

        }

    </script>
    <style type="text/css" media="screen">
        textarea.ui-input-text
        {
            height: 40px !important;
            transition: height 200ms linear 0s;
        }
        
        textarea.textMsg
        {
            height: 200px !important;
        }
    </style>
    <div data-role="page" data-theme="a" id="recipientdetails" data-title="Recipient Details ">
        <div data-role="header" data-theme="c" id="header">
            <uc1:CheckoutHeader ID="CheckoutHeader1" runat="server" EnableUrl="true" />
            <div class="header-btns">
                <a href="#rightpanel" style="color: #fff;" class="menu-btn"></a><a href="javascript:showEditBasket();"
                    style="color: #fff;" data-rel="dialog" data-transition="pop" class="basket-btn"
                    id="basketCount" runat="server">
                    <uc5:BasketCount ID="DisplayBasketCount" runat="server" UpdateMode="Conditional" />
                </a>
            </div>
            <div style="clear: both;">
            </div>
            <fieldset class="ui-grid-b">
                <div class="ui-block-a step-active">
                    <a id="ancArrowCust" runat="server" href="" style="color: #fff;">Customer </a><span
                        class="arrow-left"></span>
                </div>
                <div class="ui-block-b step-active">
                    <a href="" style="color: #fff; cursor: default">Recipient</a><span class="arrow-left"></span>
                </div>
                <div class="ui-block-c step-grey">
                    <a href="" style="color: #fff; cursor: default">Payment</a><span class="arrow-left"></span>
                </div>
            </fieldset>
        </div>
        <!-- /content /////////////////////////////////-->
        <div data-role="content" data-theme="c">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="content1" runat="server">
                        <div style="clear: both;">
                        </div>
                        <br />
                        <br />
                        <div id="divAddressBook" runat="server" style="display: none">
                            <div class="ui-bar ui-bar-a" style="margin: 10px 0 10px 0;">
                                <h3>
                                    Recipient details</h3>
                            </div>
                            <asp:DropDownList ID="drpAddressBook" runat="server" Style="padding: 5px;" AutoPostBack="true"
                                onchange="return Post2Screen();" OnSelectedIndexChanged="drpAddressBook_SelectedIndexChanged">
                            </asp:DropDownList>
                            <h3 style="text-align: center; margin: 10px 0 10px 0; color: #8aa064;">
                                OR</h3>
                        </div>
                        <div class="ui-bar ui-bar-a" style="margin: 10px 0 10px 0;">
                            <h3>
                                New Recipient</h3>
                        </div>
                        <label for="recipientname">
                            <span style="color: red;">*</span>Recipient Name:</label>
                        <textarea rows="1" cols="1" id="txtRecipientName" runat="server" clientidmode="Static"
                            placeholder="e.g. Mark" onblur="ValidateRecipientName();"></textarea>
                        <span style="display: none" class="errortext" id="ErrorRecipientName">Please enter recipient
                            name.</span>
                        <div class="ui-bar ui-bar-a" style="margin: 10px 0 10px 0;">
                            <h3>
                                Address</h3>
                        </div>
                        <label>
                            <asp:CheckBox ID="SameAsInvoiceAddress" name="checkbox-0 " data-iconpos="right" runat="server"
                                onclick="return SetSameAsCheckedState();" OnCheckedChanged="SameAsInvoiceAddress_CheckedChanged"
                                AutoPostBack="true" />Same as invoice address
                        </label>
                        <h3 style="text-align: center; margin: 10px 0 10px 0; color: #8aa064;">
                            OR</h3>
                        <div class="margin:0 10px 0 10px;float:left;">
                            <label for="addressfinder" style="color: #8aa064; font-weight: bold;">
                                Address Finder:</label>
                            <link rel="stylesheet" type="text/css" href="https://services.postcodeanywhere.co.uk/css/captureplus-1.34.min.css?key=ed89-rd15-zr57-bx68">
                            <script type="text/javascript" src="https://services.postcodeanywhere.co.uk/js/captureplus-1.34.min.js?key=ed89-rd15-zr57-bx68&amp;app=10891"></script>
                            <div id="ed89rd15zr57bx6810891" onclick="javascript: ValidateRecipientName();">
                            </div>
                        </div>
                        <h3 style="text-align: center; margin: 25px 0 10px 0; color: #8aa064;">
                            OR</h3>
                        <span id="sp"></span>
                        <p style="text-align: center;">
                            <a href="" onclick="javascript:recEnterAddressManually();">Enter address manually</a></p>
                        <br />
                        <div align="center" style="text-align: center;">
                            <asp:Button ID="Button1" runat="server" Text="Back" data-theme="a" data-icon="arrow-l"
                                data-mini="true" data-iconpos="left" OnClick="Back_Click" />
                        </div>
                        <br />
                        <div align="center" style="display: none">
                            <asp:Button ID="CapturePCAData" runat="server" Text="Button" Style="display: none"
                                OnClick="CapturePCAData_Click" />
                            <asp:Button ID="ManualAddress" runat="server" Text="Button" Style="display: none"
                                OnClick="ManualAddress_Click" />
                        </div>
                    </div>
                    <div style="display: none" id="content2" runat="server">
                        <div style="clear: both;">
                        </div>
                        <br />
                        <br />
                        <div class="ui-bar ui-bar-a" style="margin: 10px 0 10px 0;">
                            <h3>
                                Recipient details</h3>
                        </div>
                        <div class="box" id="divEditBox" runat="server">
                            <fieldset class="ui-grid-a">
                                <div class="ui-block-a">
                                    <span class="infotext-dark">Recipient Name:</span>
                                </div>
                                <div class="ui-block-b">
                                    <span class="infotext" id="CustomerFullName" runat="server"></span>
                                </div>
                            </fieldset>
                            <fieldset class="ui-grid-a">
                                <div class="ui-block-a">
                                    <span class="infotext-dark">Recipient Address:</span>
                                </div>
                                <div class="ui-block-b">
                                    <span class="infotext" id="CustomerPCAAddress" runat="server"></span><span id="spnHouseNumber"
                                        style="display: none" runat="server"></span><span id="spnOrganization" style="display: none"
                                            runat="server"></span><span id="spnStreet" style="display: none" runat="server">
                                    </span><span id="spnDistrict" style="display: none" runat="server"></span>
                                </div>
                            </fieldset>
                            <fieldset class="ui-grid-a">
                                <div class="ui-block-a">
                                    <span class="infotext-dark">City:</span>
                                </div>
                                <div class="ui-block-b">
                                    <span class="infotext" id="CustomerPCACity" runat="server"></span>
                                </div>
                            </fieldset>
                            <fieldset class="ui-grid-a">
                                <div class="ui-block-a">
                                    <span class="infotext-dark">Postcode:</span>
                                </div>
                                <div class="ui-block-b">
                                    <span class="infotext" id="CustomerPCAPostCode" runat="server"></span>
                                </div>
                            </fieldset>
                            <fieldset class="ui-grid-a">
                                <div class="ui-block-a">
                                    <span class="infotext-dark">Country:</span>
                                </div>
                                <div class="ui-block-b">
                                    <span class="infotext" id="CustomerPCACountry" runat="server"></span>
                                </div>
                            </fieldset>
                            <div align="center" style="margin: 5px 0 5px 0;">
                                <a href="" data-role="button" data-theme="a" data-inline="true" data-mini="true"
                                    data-icon="edit" data-iconpos="left" onclick="editAddress();">Edit address</a>
                            </div>
                        </div>
                        <br />
                        <div id="content3" runat="server" style="display: none">
                            <label for="recipientname">
                                <span style="color: red;">*</span>Recipient Name:</label>
                            <textarea rows="1" cols="1" id="RecipientName" runat="server" clientidmode="Static"
                                placeholder="e.g. Mark" onblur="ValidateRecipientName1();"></textarea>
                            <span style="display: none" class="errortext" id="ErrorRecipientName1">Please enter
                                recipient name.</span>
                            <label for="business">
                                Business/Organisation:</label>
                            <textarea rows="1" cols="1" id="recpientOrganization" runat="server" placeholder="e.g. Serenata Commerce Ltd"></textarea>
                            <label for="address1">
                                <span style="color: red;">*</span> Address line 1:</label>
                            <textarea rows="1" cols="1" id="recipientAddress1" runat="server" placeholder="e.g. Flat 3 or Huntingfield Road"
                                onchange="SetAddressVerifyVal('0');" onblur="return validateRecipientHouseNumber(true);"></textarea>
                            <span style="display: none" class="errortext" id="ErrorRecipientAddress1">Please enter
                                Address line 1.</span>
                            <label for="address2">
                                Address line 2:</label>
                            <textarea rows="1" cols="1" id="recipientAddress2" runat="server" placeholder="e.g. Milton Mansion"></textarea>
                            <label for="address3">
                                Address line 3:</label>
                            <textarea rows="1" cols="1" id="recipientAddress3" runat="server" placeholder="e.g. Queens Club Gardens"></textarea>
                            <label for="city">
                                <span style="color: red;">*</span> City or Town:</label>
                            <textarea rows="1" cols="1" id="recipientTown" runat="server" placeholder="e.g. London"
                                onchange="SetAddressVerifyVal('0');" onblur="return validateRecipienttown(true);"></textarea>
                            <span style="display: none" class="errortext" id="ErrorRecipientTown">Please enter City
                                or town.</span>
                            <label for="postcode">
                                <span style="color: red;">*</span> Postcode:</label>
                            <textarea rows="1" cols="1" id="recipientPostCode" runat="server" placeholder="e.g. SW15 5ET"
                                onchange="SetAddressVerifyVal('0');" onblur="return validateRecipientPostCode(true);"></textarea>
                            <span style="display: none" class="errortext" id="ErrorRecipientPostCode">Please enter
                                Full Postcode.</span>
                            <label for="country">
                                Country:</label>
                            <asp:DropDownList ID="RecipientCountry" runat="server" Style="padding: 5px;" Width="100%">
                            </asp:DropDownList>
                            <br />
                        </div>
                        <label for="deliveryinst">
                            Delivery instructions:</label>
                        <asp:DropDownList ID="drpDelIns" runat="server" onchange="javascript:DisplayHouseNo();">
                            <asp:ListItem Text="NO instructions needed" Value="NO instructions needed" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Leave on doorstep" Value="Leave on doorstep"></asp:ListItem>
                            <asp:ListItem Text="Leave in porch" Value="Leave in porch"></asp:ListItem>
                            <asp:ListItem Text="Leave with neighbour" Value="Leave with neighbour"></asp:ListItem>
                            <asp:ListItem Text="Leave at back of the property" Value="Leave at back of the property"></asp:ListItem>
                            <asp:ListItem Text="Leave behind shed" Value="Leave behind shed"></asp:ListItem>
                            <asp:ListItem Text="Leave in shed" Value="Leave in shed"></asp:ListItem>
                            <asp:ListItem Text="Please knock hard" Value="Please knock hard"></asp:ListItem>
                            <asp:ListItem Text="Please be patient" Value="Please be patient"></asp:ListItem>
                        </asp:DropDownList>
                        <div style="width: 100%; float: left;" id="divHouseNo" runat="server">
                            <div style="width: 35%; float: left;">
                                <label for="houseno">
                                    House No:</label>
                                <textarea rows="1" cols="1" id="txthouseNumber" runat="server" style="width: 70px;
                                    size: 3"></textarea>
                            </div>
                            <div style="width: 60%; float: left; margin: 33px 0 0 5px;">
                                <label>
                                    &nbsp;</label>
                                <span class="infotext">(if left with neighbour)</span>
                            </div>
                        </div>
                        <label for="occasion">
                            Occasion:</label>
                        <asp:DropDownList ID="Occasions" runat="server" AutoPostBack="True" onchange="return CheckCardSelectionChecked()"
                            OnSelectedIndexChanged="drpOccasions_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div style="clear: both;">
                        </div>
                        <br />
                        <div style="width: 100%; float: left;">
                            <div style="width: 35%; float: left;">
                                <span class="infotext-dark" style="float: left; margin-top: 12px;"></span><span style="float: left;">
                                    <label style="margin-top: 10px; float: left;" id="lblChooseCard" runat="server">
                                        Choose card:</label>
                            </div>
                            <div style="width: 60%; float: left; margin-left: 10px;" class="noMsgCheckBox">
                                <span class="infotext-dark" style="float: left; margin-top: 12px;">NO message:</span>
                                <span style="float: left;">
                                    <label style="width: 35px; font-size: 11px; margin-left: 15px; height: 35px;">
                                        <asp:CheckBox ID="ChooseACard" class="custom" data-theme="c" data-mini="true" runat="server"
                                            AutoPostBack="true" onclick="SetCheckedState(this.id)" OnCheckedChanged="chkCardSelection_CheckedChanged" />
                                        &nbsp;</label>
                                    <asp:HiddenField ID="hdnCardSelection" runat="server" />
                                </span>
                            </div>
                        </div>
                        <script>
                            $(document).ready(function () {

                                $("#upsells").owlCarousel({
                                    items: 10, //10 items above 1000px browser width
                                    itemsDesktop: [1000, 5], //5 items between 1000px and 901px
                                    itemsDesktopSmall: [900, 5], // betweem 900px and 601px
                                    itemsTablet: [600, 3], //3 items between 600 and 0;
                                    itemsMobile: false // itemsMobile disabled - inherit from itemsTablet option
                                });
                                loadCapturePlus();
                            });

                            function jScript() {
                                $("#upsells").owlCarousel({
                                    items: 10, //10 items above 1000px browser width
                                    itemsDesktop: [1000, 5], //5 items between 1000px and 901px
                                    itemsDesktopSmall: [900, 5], // betweem 900px and 601px
                                    itemsTablet: [600, 3], //3 items between 600 and 0;
                                    itemsMobile: false // itemsMobile disabled - inherit from itemsTablet option
                                });
                                loadCapturePlus();
                            }
			</script>
                        <div id="upsells" class="owl-carousel owl-theme" runat="server" data-iscroll>
                            <asp:HiddenField ID="hdnIsSelected" runat="server" Value="false" />
                            <asp:Repeater ID="rptCards" runat="server" OnItemCommand="rptCards_ItemCommand">
                                <ItemTemplate>
                                    <div class="item">
                                        <a href="#zoom<%# DataBinder.Eval(Container.DataItem, "productid") %>" data-rel="dialog" data-transition="pop">
                                            <img src="<%# Eval("Img1SmallHigh") %>" border="0" alt="" />
                                        </a>
                                        <br />
                                        <span class="upsell-input">
                                            <input name="messagecards" style="padding: 8px;" onclick="AddCardToBasket('<%# Eval("ProductID") +"','"+ Eval("Price") +"','"+ Eval("NoCard") %>')"
                                                type="radio" id='rdb_<%# Eval("ProductID") %>' <%# Eval("IsCheckedString") %> />
                                        </span><span class="upsell-price">
                                            <%# Convert.ToDecimal(Eval("Price")) > 0 ? string.Format("{0:£0.00}", Eval("Price")) : "Free"%>&nbsp;</span>
                                        <div style="clear: both;">
                                            &nbsp;</div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <script type="text/javascript" language="javascript">
                                Sys.Application.add_load(jScript);
                            </script>
                        </div>
                        <asp:LinkButton ID="lnkAddCardDummy" runat="server" OnClick="lnkAddCardDummy_Click"></asp:LinkButton>
                        <div style="clear: both;">
                        </div>
                        <br />
                        <div style="text-align: left;" runat="server" id="divMessage">
                            <label for="message">
                                Message:</label>
                            <textarea id="txtGiftMsg" runat="server" class="textMsg" onkeyup="javascript:countCharacters(this.id, 14)"></textarea>
                             <div id="divmaxlenmessage" class="infotext" style="width: 95%">
                                <span id="mycounter" runat="server"></span>
                            </div>
                            <input type="hidden" id="hdnMaxLength" runat="server" />
                        </div>
                        <div align="center" style="text-align: center;">
                            <asp:Button ID="SaveRecipient" runat="server" Text="Continue to Payment" data-theme="d"
                                data-icon="arrow-r" data-iconpos="right" OnClientClick="return continuePyament();"
                                OnClick="SaveRecipient_Click" />
                        </div>
                        <br />
                        <div align="center" style="text-align: center;">
                            <asp:Button ID="ScreenBack" runat="server" Text="Back" OnClick="Back_Click" />
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="CapturePCAData" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="drpAddressBook" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="SaveRecipient" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="SameAsInvoiceAddress" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="lnkAddCardDummy" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ChooseACard" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ManualAddress" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div data-role="footer" data-theme="c" style="padding: 0 15px 0 15px;">
            <uc2:CheckoutFooter ID="Footer" runat="server" />
        </div>
        <uc3:Panel ID="Menu" runat="server" />
    </div>
    <!-- Popup Section-->
    <asp:Repeater ID="ZoomGorgeousVase" runat="server">
        <ItemTemplate>
            <div data-role="page" id="zoom<%# DataBinder.Eval(Container.DataItem, "productid") %>"
                data-close-btn="right">
                <div data-role="header" data-theme="a" style="text-align: left;">
                    <h3 style="text-align: left;">
                        <%# DataBinder.Eval(Container.DataItem, "producttitle") %></h3>
                </div>
                <div data-role="content" data-theme="a">
                    <div align="center">
                        <img src='<%# DataBinder.Eval(Container.DataItem, "Img1SmallHigh")%>' border="0"
                            alt="" />
                        <p>
                            <%# DataBinder.Eval(Container.DataItem, "info2") %></p>
                    </div>
                </div>
                <div data-role="footer">
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <!-- Suggested Dates-->
    <div data-role="page" data-theme="a" id="SuggestedDate" data-title="Suggested delivery date">
        <div data-role="footer" data-theme="a" style="text-align: left;">
            <h3 style="text-align: left;">
                Suggested delivery date</h3>
        </div>
        <!-- /header -->
        <div data-role="content" data-theme="a">
            <small class="errortext" id="postcodeMessage" runat="server"></small>
            <asp:UpdatePanel ID="updSuggestetdate" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <label for="">
                        Suggested delivery date</label>
                    <asp:DropDownList ID="ddlSugestedDeliveryDate" runat="server" OnSelectedIndexChanged="ddlSugestedDeliveryDate_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <fieldset data-role="controlgroup" data-mini="true">
                        <legend>Delivery time:</legend>
                        <div style="margin-left: -40px">
                            <asp:RadioButtonList ID="rbtnLstsuggestedDeliveryOptions" runat="server" RepeatLayout="OrderedList"
                                RepeatColumns="1">
                            </asp:RadioButtonList>
                        </div>
                    </fieldset>
                    <div style="clear: both;">
                    </div>
                    <div align="center" style="margin: 5px 0 5px 0;">
                        <asp:Button ID="ChangeDate" runat="server" Text="CHANGE DATE" OnClientClick="Closesuggesteddelivery();"
                            data-theme="d" OnClick="ChangeDate_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="CancelOrder" runat="server" Text="CANCEL ORDER" OnClientClick="Closesuggesteddelivery();"
                            data-theme="d" OnClick="CancelOrder_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div data-role="footer" style="padding: 15px 0 15px 0; text-align: center;">
        </div>
    </div>
    <!-- Suggested Dates-->
    <!-- Post code message-->
    <div data-role="page" data-theme="a" id="postcode-message" data-title="Suggested delivery date">
        <div data-role="footer" data-theme="a" style="text-align: left;">
            <h3 style="text-align: center;">
                Cancel Order</h3>
        </div>
        <div data-role="content" data-theme="a">
            <asp:Label ID="lblpostcodeInfo" class="errortext" runat="server"></asp:Label>
            <br />
            <asp:Button ID="btnCancelOrder" runat="server" Text="CANCEL ORDER" OnClientClick="Closesuggesteddelivery();"
                data-theme="d" OnClick="CancelOrder_Click" />
        </div>
        <div data-role="footer" style="padding: 15px 0 15px 0; text-align: center;">
        </div>
    </div>
    <!-- Post code message-->
    <!-- Begin: Quantity Verification-->
    <div data-role="page" data-theme="a" id="quantity-verification" data-title="Quantity verification">
        <div data-role="header" data-theme="a" style="text-align: left;">
            <h3 style="text-align: left;">
                Quantity verification</h3>
        </div>
        <div data-role="content" data-theme="a">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div>
                        <p class="infotext">
                            <span style="color: Red">* </span>You have multiple products in your basket. To
                            rectify, simple change the quantity in the 'Qty' box above</p>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="SaveRecipient" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div data-role="footer" style="padding: 15px 0 15px 0; text-align: center;">
        </div>
    </div>
    <!-- End: Quantity Verification-->
    <!-- Suggested Address-->
    <div data-role="page" data-theme="a" id="suggest-address" data-title="Suggest Address">
        <div data-role="header" data-theme="a" style="text-align: left;">
            <h3 style="text-align: left;">
                Suggest Address</h3>
        </div>
        <div data-role="content" data-theme="a">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div>
                        <p class="infotext">
                            <span style="color: red;">* </span>We haven't been able to verify your entered address,
                            please select one from the list suggested, or continue to use the one entered.</p>
                    </div>
                    <asp:DropDownList ID="DrpSuggestedAddress" runat="server">
                    </asp:DropDownList>
                    <div style="text-align: center;">
                        <asp:Button ID="UseThisAddress" runat="server" data-theme="d" Text="USE THIS ADDRESS"
                            OnClick="UseThisAddress_Click" OnClientClick="return CheckListSelectedAdddress();" />
                        <asp:Button ID="UseTheAddressIEntered" runat="server" data-theme="d" Text="KEEP MY ADDRESS"
                            OnClick="UseTheAddressIEntered_Click" />
                    </div>
                    <div>
                        <p class="infotext">
                            <span style="color: Red">* </span>You hereby take full responsibility that the address
                            is correct. If our courier company can't find the address Serenata is not liable
                            for any compensation.</p>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="SaveRecipient" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div data-role="footer" style="padding: 15px 0 15px 0; text-align: center;">
        </div>
    </div>
    <div data-role="page" data-theme="a" id="suggest-notfound" data-title="Address not found">
        <div data-role="header" data-theme="a" style="text-align: left;">
            <h3 style="text-align: left;">
                Address not found</h3>
        </div>
        <div data-role="content" data-theme="a">
            <div id="divAddressFailure">
                <div>
                    <p class="infotext">
                        We haven't been able to verify your address and by clicking yes you hereby take
                        full responsibility that the address is correct. If our courier company can't find
                        the address Serenata is not liable for any compensation.</p>
                </div>
                <div style="text-align: center;">
                    <asp:Button ID="btnYes" runat="server" data-theme="d" Text="YES" OnClick="btnYes_Click" />
                    <asp:Button ID="btnNo" runat="server" data-theme="d" Text="NO" />
                </div>
            </div>
        </div>
        <div data-role="footer" style="padding: 15px 0 15px 0; text-align: center;">
        </div>
    </div>
    <!-- Suggested Address-->
    <uc4:EditBasket ID="ModifyBasket" runat="server" UpdateMode="Conditional" OnButtonClick="Save_Click" />
    <uc6:EditDeliveryDate ID="ModifyDeliveryDate" runat="server" UpdateMode="Conditional"
        OnButtonClick="SaveDates_Click" />
    <uc7:Voucher ID="VoucherCode" runat="server" UpdateMode="Conditional" OnButtonClick="Voucher_Click" />
    <uc8:ContactUs ID="UserContactUs" runat="server" />
    <uc9:Terms ID="Termsandcondition" runat="server" />
    <uc10:Privacy ID="Privacy" runat="server" />
    <!-- Popup Section end-->
    <asp:HiddenField ID="hdnAddressVerify" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCustomerFirstname" runat="server" Value="" />
    <asp:HiddenField ID="hdnCustomerLastName" runat="server" Value="" />
    <asp:HiddenField ID="hdnPCAOrganisation" runat="server" Value="" />
    <asp:HiddenField ID="hdnPCAStreet" runat="server" Value="" />
    <asp:HiddenField ID="hdnPCAHouseNumber" runat="server" Value="" />
    <asp:HiddenField ID="hdnPCADistrict" runat="server" Value="" />
    <asp:HiddenField ID="hdnPCACity" runat="server" Value="" />
    <asp:HiddenField ID="hdnPCACountry" runat="server" Value="" />
    <asp:HiddenField ID="hdnDialingCode" runat="server" Value="" />
    <asp:HiddenField ID="hdnPCAPostCode" runat="server" Value="" />
    <asp:HiddenField ID="hdnPCAAddress" runat="server" Value="" />
    <asp:HiddenField ID="hdnEnterAddManually" runat="server" Value="" />
    <asp:HiddenField ID="hdnEnterManualyEdit" runat="server" Value="" />
    <asp:HiddenField ID="hdnPCAEdit" runat="server" Value="" />
    <asp:HiddenField ID="hdnInternational" Value="no" runat="server" />
    <asp:HiddenField ID="hdnNextDelDate" runat="server" Value="0" />
    <asp:HiddenField ID="hdnHasApiAddress" runat="server" Value="0" />
    </form>
</body>
</html>
