<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="m_confirmation.aspx.cs"
    Inherits="Serenataflowers.Checkout.m_confirmation" %>

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
<%@ Register Src="~/Controls/ViewCustomerDetails.ascx" TagName="CustomerDetails"
    TagPrefix="ViewCustomer" %>
<%@ Register Src="~/Controls/ViewRecipientDetails.ascx" TagName="DeliveryDetails"
    TagPrefix="ViewDelivery" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Serenata Flowers - Confirmation</title>
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
    <script src="../Scripts/commonfunctions.js"></script>
    <script src="../Scripts/m_confirmation.js"></script>
    <link rel="stylesheet" href="../stylesheets/custom.css">
    <script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
    <!-- OWL slider /////// -->
    <link rel="stylesheet" href="../stylesheets/owl.carousel.css">
    <link rel="stylesheet" href="../stylesheets/owl.theme.css">
    <script src="../javascripts/owl.carousel.js"></script>

          <style type="text/css" media="screen">
          textarea.ui-input-text {
            height: 40px!Important;
            transition: height 200ms linear 0s;
        }
        
                   textarea.textMsg
        {
            height:200px !important;            
        }
	</style>

</head>
<body>
    <form id="form1" runat="server">
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
    <div data-role="page" data-theme="a" id="confirmation" data-title="Confirmation">
        <div data-role="header" data-theme="c" id="header">
            <uc1:CheckoutHeader ID="CheckoutHeader1" runat="server" EnableUrl="true" />
            <div class="header-btns" >
                <a href="#rightpanel" style="color: #fff;" class="menu-btn"></a><a href="#EditBasket"
                    style="color: #fff;display:none" data-rel="dialog" data-transition="pop" class="basket-btn"
                    id="basketCount" runat="server">
                    <uc5:BasketCount ID="DisplayBasketCount" runat="server" UpdateMode="Conditional" />
                </a>
            </div>
            <div style="clear: both;">
            </div>
        </div>
        <!-- /content /////////////////////////////////-->
        <div data-role="content" data-theme="c">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="content1" runat="server">
                        <div style="clear: both;">
                        </div>
                        <div class="ui-bar ui-bar-a" style="margin: 10px 0 10px 0;">
                            <h3>
                                Thank you for your order</h3>
                        </div>
                        <p>
                            Your order reference is <b style="color: #000;">
                                <asp:Label ID="lblOrderId" runat="server" /></b></p>
                        <p>
                            Please review your details below.</p>
                        <fieldset class="ui-grid-a">
                            <div class="ui-block-a">
                                <div class="box">
                                    <span class="infotext-dark"><b>Invoice address:</b><br />
                                        <ViewCustomer:CustomerDetails ID="ucCustomerDetails" runat="server" UpdateMode="Conditional" />
                                    </span>
                                    <div align="center" style="margin: 5px 0 5px 0;display:none">
                                        <a href="#" data-rel="dialog" data-transition="pop" data-role="button" data-theme="a"
                                            data-inline="true" data-mini="true" data-icon="edit" data-iconpos="left" onclick="CustomerEditAddress();">
                                            Edit</a>
                                    </div>
                                </div>
                            </div>
                            <div class="ui-block-b">
                                <div class="box" style="margin-left: 5px;">
                                    <span class="infotext-dark"><b>Recipient address:</b><br />
                                        <ViewDelivery:DeliveryDetails ID="ucDeliveryDetails" runat="server" UpdateMode="Conditional"
                                            ChildPageName="Confirmation" />
                                    </span>
                                    <div align="center" style="margin: 5px 0 5px 0;display:none">
                                        <a href="" data-rel="dialog" data-transition="pop" data-role="button" data-theme="a"
                                            data-inline="true" data-mini="true" data-icon="edit" data-iconpos="left" onclick="RecipientEditAddress();">
                                            Edit</a>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <br />
                    <!-- basket ///////////////////////// -->
                             <table>
                                <tbody>
                    <asp:Repeater ID="rptViewBasket" runat="server" OnItemDataBound="rptViewBasket_ItemDataBound">
                        <ItemTemplate>
                                        <tr>
                                        <td align="center" width="25%">
                                            <asp:Label ID="lblQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "quantity") %>'>
                                            </asp:Label>
                                        </td>
                                        <td align="center" width="30%">
                                            <img align="middle" style="border: 1px solid #ccc;" width="40" src="<%# DataBinder.Eval(Container.DataItem, "img1_small_low")%>"
                                                border="0" alt="" />
                                        </td>
                                        <td align="left" width="45%">
                                            <%# DataBinder.Eval(Container.DataItem, "producttitle") %>
                                        </td>
                                        <td align="center" width="10%">
                                            <asp:Label ID="lblPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "qtyPrice", "{0:£,0.00}") %>'>
                                            </asp:Label>
                                        </td>
                                    </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            
                        </FooterTemplate>
                    </asp:Repeater>
                    
                                
                                    <tr>
                                        <td align="center" width="25%">
                                            
                                        </td>
                                        <td align="center" width="30%">
                                            <img align="absmiddle" width="40" src="http://images.serenataflowers.com/delivery-icon.png"
                                                border="0" alt="" />
                                        </td>
                                        <td align="left" width="45%">
                                            <asp:Label ID="lblDeliveryType" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" width="10%">
                                            <asp:Label ID="lblDeliveryPrice" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trDiscount" runat="server">
                                        <td align="center" width="55%" colspan="2">
                                            <b>Discount:</b>
                                        </td>
                                        <td width="45%" >
                                            <asp:Label ID="hlinkVoucherTitle" runat="server"></asp:Label>
                                        </td>
                                        <td  align="center" width="10%">
                                            <b>
                                                <asp:Label ID="lblDiscountPrice" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                    
                                        <td align="center" colspan="2">
                                        </td>
                                        <td align="left" width="45%">
                                            <b>TOTAL :</b>
                                        </td>
                                        <td align="center" width="10%">
                                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                    <div align="center" style="margin: 5px 0 5px 0; display: none">
                        <a href="#EditBasket" id="basketCount1" data-rel="dialog" data-transition="pop" data-role="button"
                            data-theme="a" data-inline="true" data-mini="true" data-icon="edit" data-iconpos="left">
                            Edit Basket</a>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <!-- confirmation details ///////////////////////// -->
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td align="left" width="80%" style="vertical-align: top;">
                                    <b>Message:</b><br />
                                    <asp:Label ID="lblGiftMessage" runat="server" />
                                </td>
                                <td align="left" width="20%">
                                    <a href="#" data-rel="dialog" data-transition="pop" data-role="button" data-theme="a"
                                        data-inline="true" data-mini="true" data-icon="edit" data-iconpos="left" onclick="EditMessage();">
                                        Edit</a>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="80%" style="vertical-align: top;" colspan="2">
                                    <b>Delivery date:</b><br />
                                    <asp:Label ID="lblDeliveryDate" runat="server" />
                                </td>
                                <%--<td align="left" width="20%" style="display: none">
                                    <a href="#" data-rel="dialog" data-transition="pop" data-role="button" data-theme="a"
                                        data-inline="true" data-mini="true" data-icon="edit" data-iconpos="left">Edit</a>
                                </td>--%>
                            </tr>
                            <tr>
                                <td width="80%" align="left">
                                    <b>Delivery instructions:</b><br />
                                    <asp:Label ID="lblDeliveryInstruction" runat="server" />
                                </td>
                                <td align="left" width="20%">
                                    <a href="#" data-rel="dialog" data-transition="pop" data-role="button" data-theme="a"
                                        data-inline="true" data-mini="true" data-icon="edit" data-iconpos="left" onclick="EditDeliveryInstruction();">
                                        Edit</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <div align="center" style="margin: 5px 0 5px 0; display:none" >
                        <asp:Label ID="ChaseStatus" Font-Bold="true" ForeColor="Red" runat="server" /><br />
                        <asp:Button ID="Refund" runat="server" data-theme="d" Text="Refund" OnClick="Refund_Click"
                            /><br />
                        <asp:Button ID="Cancellation" runat="server" data-theme="d" Text="Cancellation" OnClick="Cancellation_Click"
                             />
                        <%-- <a href="" data-role="button" data-theme="a" data-inline="true" data-mini="true"
                            data-icon="edit" data-iconpos="left">Download invoice as PDF
                            <img src="https://www.serenataflowers.com/images/pdf-icon.png" align="absmiddle"
                                border="0" /></a>--%>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div data-role="footer" data-theme="c" style="padding: 0 15px 0 15px;">
            <uc2:CheckoutFooter ID="Footer" runat="server" />
        </div>
        <uc3:Panel ID="Menu" runat="server" />
    </div>
    <div data-role="page" data-theme="a" id="Recipient-address" data-title="Edit Your address">
        <div data-role="header" data-theme="a" style="text-align: left;">
            <h3 style="text-align: left;">
                Recipient address</h3>
        </div>
        <!-- /header -->
        <div data-role="content" data-theme="a">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <label>
                        <span style="color: red;">*</span>Recipient's name:</label>
                    <asp:TextBox ID="txtRecName" runat="server" placeholder="e.g. Mary Smith" onblur="ValidateRecName(true);"></asp:TextBox>
                    <span style="display: none" class="errortext" id="ErrortxtRecName">Please enter Recipient
                        name</span>
                    <label>
                        <span style="color: red;">*</span> Address line 1:</label>
                    <asp:TextBox ID="txtAddress1" runat="server" placeholder="e.g. 12 Barn Road" onblur="ValidateRecAddr1(true);"></asp:TextBox>
                    <span style="display: none" class="errortext" id="ErrortxtAddress1">Please enter Address
                        1</span>
                    <label>
                        Address line 2:</label>
                    <asp:TextBox ID="txtAddress2" runat="server" placeholder="e.g. Dover Mansion"></asp:TextBox>
                    <label>
                        Address line 3:</label>
                    <asp:TextBox ID="txtAddress3" runat="server" placeholder="e.g. Queens Club Gardens"></asp:TextBox>
                    <label>
                        <span style="color: red;">*</span> City or town:</label>
                    <asp:TextBox ID="txtCity" runat="server" placeholder="e.g. London" onblur="ValidateRecCity(true);"></asp:TextBox>
                    <span style="display: none" class="errortext" id="ErrortxtCity">Please enter City/Town</span>
                    <label>
                        <span style="color: red;">*</span> Postcode:</label>
                    <asp:TextBox ID="txtPostCode" runat="server" onblur="ValidateRecPostCode(true);"></asp:TextBox>
                    <span style="display: none" class="errortext" id="ErrortxtPostCode">Please enter PostCode</span>
                    <label>
                        Country:</label>
                    <asp:DropDownList ID="drpRecCountry" runat="server" Style="padding: 5px; margin: 0 0 12px;"
                        Width="100%">
                    </asp:DropDownList>
                    <div style="display: none" id="DivRecPhone">
                        <label>
                            Mobile number:</label>
                        <asp:TextBox ID="txtRecPhoneNumber" runat="server" placeholder="e.g. 07587679230"></asp:TextBox>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div align="center" style="margin: 5px 0 5px 0;">
                        <asp:LinkButton ID="lnkSaveRecManuallyAddress" runat="server" data-role="button"
                            data-theme="a" data-inline="true" data-mini="true" data-icon="edit" data-iconpos="left"
                            OnClientClick="javascript:return ValidateRecForm();" OnClick="lnkSaveRecManuallyAddress_Click">SAVE CHANGES</asp:LinkButton>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkSaveRecManuallyAddress" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <!-- /content -->
        <div data-role="footer" style="padding: 15px 0 15px 0; text-align: center;">
        </div>
        <!-- /footer -->
    </div>
    <div data-role="page" data-theme="a" id="customer-address" data-title="Edit Your address">
        <div data-role="header" data-theme="a" style="text-align: left;">
            <h3 style="text-align: left;">
                Your address</h3>
        </div>
        <!-- /header -->
        <div data-role="content" data-theme="a">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <label>
                        <span style="color: red;">*</span> Name:</label>
                    <asp:TextBox ID="txtCustName" runat="server" placeholder="e.g. Mary Smith" onblur="ValidateCustomerName(true);"></asp:TextBox>
                    <span style="display: none" class="errortext" id="ErrorCustomerName">Please enter Customer
                        name</span>
                    <label>
                        <span style="color: red;">*</span> Address line 1:</label>
                    <asp:TextBox ID="TxtCustAddr1" runat="server" placeholder="e.g. 12 Barn Road" onblur="ValidateCustomerAddr1(true);"></asp:TextBox>
                    <span style="display: none" class="errortext" id="ErrorCustAddr1">Please enter Address
                        1</span>
                    <label>
                        Address line 2:</label>
                    <asp:TextBox ID="TxtCustAddr2" runat="server" placeholder="e.g. Dover Mansion"></asp:TextBox>
                    <label>
                        Address line 3:</label>
                    <asp:TextBox ID="TxtCustAddr3" runat="server" placeholder="e.g. Queens Club Gardens"></asp:TextBox>
                    <label>
                        <span style="color: red;">*</span> City or town:</label>
                    <asp:TextBox ID="TxtCustTown" runat="server" placeholder="e.g. London" onblur="ValidateCustomerCity(true);"></asp:TextBox>
                    <span style="display: none" class="errortext" id="ErrorCustCity">Please enter City/Town</span>
                    <label>
                        <span style="color: red;">*</span> Postcode:</label>
                    <asp:TextBox ID="TxtCustPostCode" runat="server" onblur="ValidateCustomerPostCode(true);"></asp:TextBox>
                    <span style="display: none" class="errortext" id="ErrorPostCode">Please enter PostCode</span>
                    <label>
                        Country:</label>
                    <asp:DropDownList ID="ddlCustCountry" runat="server" Style="padding: 5px; margin: 0 0 12px;"
                        Width="100%">
                    </asp:DropDownList>
                    <label>
                        </span>Mobile number:</label>
                    <asp:TextBox ID="TxtCustMobile" runat="server" placeholder="e.g. 07587679230"></asp:TextBox>
                    <div style="clear: both;">
                    </div>
                    <div align="center" style="margin: 5px 0 5px 0;">
                        <asp:LinkButton ID="lnkSaveCustManuallyAddress" runat="server" data-role="button"
                            data-theme="a" data-inline="true" data-mini="true" data-icon="edit" data-iconpos="left"
                            OnClientClick="javascript:return ValidateCustForm();" OnClick="lnkSaveCustManuallyAddress_Click">SAVE CHANGES</asp:LinkButton>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkSaveCustManuallyAddress" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <!-- /content -->
        <div data-role="footer" style="padding: 15px 0 15px 0; text-align: center;">
        </div>
        <!-- /footer -->
    </div>
    <div data-role="page" data-theme="a" id="message" data-title="Edit message">
        <div data-role="header" data-theme="a" style="text-align: left;">
            <h3 style="text-align: left;">
                Edit message</h3>
        </div>
        <!-- /header -->
        <asp:UpdatePanel ID="UpdateMessage" runat="server">
            <ContentTemplate>
                <div data-role="content" data-theme="a">
                    <label for="">
                        Message:</label>
<%--                    <asp:TextBox ID="txtMessage" TextMode="MultiLine" Rows="5" runat="server" Style="height: 150px;"></asp:TextBox>--%>

                    <textarea id="txtMessage" class="textMsg" clientidmode="Static" runat="server"></textarea>

                    <div style="clear: both;">
                    </div>
                    <div align="center" style="margin: 5px 0 5px 0;">
                        <asp:LinkButton ID="lnkMessageBtn" runat="server" data-role="button" data-theme="a"
                            data-inline="true" data-mini="true" data-icon="edit" data-iconpos="left" OnClientClick=""
                            OnClick="lnkMessageBtn_Click">SAVE CHANGES</asp:LinkButton>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkMessageBtn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <!-- /content -->
        <div data-role="footer" style="padding: 15px 0 15px 0; text-align: center;">
        </div>
        <!-- /footer -->
    </div>
    <div data-role="page" data-theme="a" id="delivery-inst" data-title="Edit delivery instructions">
        <div data-role="header" data-theme="a" style="text-align: left;">
            <h3 style="text-align: left;">
                Edit delivery instructions</h3>
        </div>
        <!-- /header -->
        <asp:UpdatePanel ID="UpdateDeliveryInstruction" runat="server">
            <ContentTemplate>
                <div data-role="content" data-theme="a">
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
                        <asp:ListItem Text="Please be patient" Value="Please be patien"></asp:ListItem>
                    </asp:DropDownList>
                    <div style="width: 100%; float: left; display: none;" id="divHouseNo" runat="server">
                        <div style="width: 35%; float: left;">
                            <label for="houseno">
                                House No:</label>
                            <asp:TextBox ID="txthouseNumber" runat="server" Style="width: 70px; size: 3"></asp:TextBox>
                        </div>
                        <div style="width: 60%; float: left; margin: 33px 0 0 5px;">
                            <label>
                                &nbsp;</label>
                            <span class="infotext">(if left with neighbour)</span>
                        </div>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div align="center" style="margin: 5px 0 5px 0;">
                        <asp:LinkButton ID="lnkBtnDeliveryInst" runat="server" data-role="button" data-theme="a"
                            data-inline="true" data-mini="true" data-icon="edit" data-iconpos="left" OnClientClick=""
                            OnClick="lnkBtnDeliveryInst_Click">SAVE CHANGES</asp:LinkButton>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkBtnDeliveryInst" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <!-- /content -->
        <div data-role="footer" style="padding: 15px 0 15px 0; text-align: center;">
        </div>
        <!-- /footer -->
    </div>
    <!-- Suggested Address-->
    <div data-role="page" data-theme="a" id="suggest-address" data-title="Suggest Address">
        <div data-role="header" data-theme="a" style="text-align: left;">
            <h3 style="text-align: left;">
                Suggest Address</h3>
        </div>
        <div data-role="content" data-theme="a">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
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
                        <asp:Button ID="UseTheAddressIEntered" runat="server" data-theme="d" Text="USE THE ADDRESS I ENTERED"
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
                    <asp:AsyncPostBackTrigger ControlID="lnkSaveRecManuallyAddress" EventName="Click" />
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
    <!-- Post code message-->
    <div data-role="page" data-theme="a" id="postcode-message" data-title="Suggested delivery date">
        <div data-role="header" data-theme="a" style="text-align: left;">
            <h3 style="text-align: left;">
                Postcode message</h3>
        </div>
        <div data-role="content" data-theme="a">
            <asp:Label ID="lblpostcodeInfo" class="errortext" runat="server"></asp:Label>
        </div>
        <div data-role="footer" style="padding: 15px 0 15px 0; text-align: center;">
        </div>
    </div>
    <!-- Post code message-->
    <!-- Suggested Dates-->
    <div data-role="page" data-theme="a" id="SuggestedDate" data-title="Suggested delivery date">
        <div data-role="header" data-theme="a" style="text-align: left;">
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
                        <asp:Button ID="ChangeDate" runat="server" Text="CHANGE DATE" data-theme="d" OnClick="ChangeDate_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
    <!-- /page -->
    <!-- /page -->
    <!-- /page -->
    <!-- Popup Section-->
    <!-- Popup Section-->
    <uc4:EditBasket ID="ModifyBasket" runat="server" UpdateMode="Conditional" OnButtonClick="Save_Click" />
    <uc6:EditDeliveryDate ID="ModifyDeliveryDate" runat="server" UpdateMode="Conditional"
        OnButtonClick="SaveDates_Click" />
    <uc7:Voucher ID="VoucherCode" runat="server" UpdateMode="Conditional" OnButtonClick="Voucher_Click" />
    <uc8:ContactUs ID="UserContactUs" runat="server" />
    <uc9:Terms ID="Termsandcondition" runat="server" />
    <uc10:Privacy ID="Privacy" runat="server" />
    <asp:HiddenField ID="hdnAddressVerify" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCustomerFirstname" runat="server" Value="" />
    <asp:HiddenField ID="hdnCustomerLastName" runat="server" Value="" />
    <asp:HiddenField ID="hdnPCAOrganisation" runat="server" Value="" />
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
