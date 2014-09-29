<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="amazonLocal5.aspx.cs" Inherits="Serenataflowers.amazonLocal5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="Controls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="Controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Literal ID="ltTitle" runat="server"></asp:Literal>
    <title id="hometitle" runat="server" visible="false"></title>
    <link href="Styles/m_style.css" rel="Stylesheet" type="text/css" />
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" name="viewport">
    <style type="text/css">
    .prodoverlay_wrapper em {
    color: #A4A4A4;
    display: block;
    font: italic 10px tahoma,verdana,sans-serif;
    height: 17px;
    padding: 2px 0 0;
    position: absolute;
    right: 4px;
    text-align: right;
    top: 130px;
    width: 150px;
}

    </style>
</head>
<script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
  
<body>
    <form id="form1" runat="server">
        <div id="page_wrapper">
            <!--CONTENET DIV Start-->
            <div id="content_normal">
                <!--Header DIV Start-->
                <div id="header">
                    <uc1:Header ID="Header1" runat="server" EnableUrl="true" />
                </div>
                <!--Header DIV End-->
                <!--Banner DIV Start-->
                <div class="banner">

                    <h1>
                        <asp:Label ID="lblContentTitle" runat="server" Text=""></asp:Label></h1>
                    <p>
                        <asp:Label ID="lblcontentDesc" runat="server" Text=""></asp:Label>
                        <span id="spntxt" runat="server" style="display: none"></span>
                    </p>
                    <a href="#" runat="server" id="ancBanner">
                        <img alt="" border="0" runat="server" id="banner" visible="false"></a>

                </div>
                <!--Banner DIV End-->
                <div>
                    <h2>Amazon Local Bouquets</h2>
                    <p>Please select your desired bouquet from one of these three beautiful gifts using your Amazon Local voucher.</p>
                    <div class="clear">
                    </div>
                </div>
                <!--Content Div-->
                <div id="DivPList" runat="server">
                    <%--Product 1--%>
                    <div runat="server" id="divImage1" class="prodimg">

                        <div class="prodoverlay_wrapper">
                            <a href='ViewProduct.aspx?ProdId=106078' id="ancOrdernow1" runat="server">
                                <img class="prodImage" src="http://productimages.serenataflowers.com/small_high_1_106078.jpg"
                                    alt="Ianthe" width="135" border="0"
                                    height="158">
                                <em>Next day<br />
                                    delivery</em>
                            </a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                        <div class="prodimgbottom">
                            <h5>
                                <a href='ViewProduct.aspx?ProdId=106078' id="ancname1" runat="server">Ianthe</a></h5>
                            <span id="price" runat="server">price:<b>£39.99</b></span>

                        </div>

                        <div class="prodbuttons probtnmar">
                            <div class="lfloat">
                                <a class="btnorange" title='Ianthe - Order' id="ancbtn1" runat="server"
                                    href='ViewProduct.aspx?ProdId=106078'><span>Order now »</span></a>

                            </div>
                        </div>
                    </div>
                    <%--Product 2--%>
                    <div runat="server" id="divImage2" class="prodimg">
                        <div class="prodoverlay_wrapper">
                            <a href='ViewProduct.aspx?ProdId=104475' id="ancOrdernow2" runat="server">
                                <img class="prodImage" src="http://productimages.serenataflowers.com/small_high_1_104475.jpg"
                                    alt="Team GB" width="135" border="0"
                                    height="158">
                                <em>Next day<br />
                                    delivery</em>
                            </a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                        <div class="prodimgbottom">
                            <h5>
                                <a href='ViewProduct.aspx?ProdId=104475' id="ancname2" runat="server">Team GB</a></h5>
                            <span id="Span1" runat="server">price:<b>£39.99</b></span>

                        </div>

                        <div class="prodbuttons probtnmar">
                            <div class="lfloat">
                                <a class="btnorange" title='Team GB - Order' id="ancbtn2" runat="server"
                                    href='ViewProduct.aspx?ProdId=104475'><span>Order now »</span></a>

                            </div>
                        </div>
                    </div>
                    <%--Product 3--%>

                    <div runat="server" id="divImage3" class="prodimg">
                        <div class="prodoverlay_wrapper">
                            <a href='ViewProduct.aspx?ProdId=105738' id="ancOrdernow3" runat="server">
                                <img class="prodImage" src="http://productimages.serenataflowers.com/small_high_1_105738.jpg"
                                    alt="Prince of Cambridge" width="135" border="0"
                                    height="158">
                                <em>Next day<br />
                                    delivery</em>
                            </a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                        <div class="prodimgbottom">
                            <h5>
                                <a href='ViewProduct.aspx?ProdId=105738' id="ancname3" runat="server">Prince of Cambridge</a></h5>
                            <span id="Span2" runat="server">price:<b>£39.99</b></span>

                        </div>

                        <div class="prodbuttons probtnmar">
                            <div class="lfloat">
                                <a class="btnorange" title='Prince of Cambridge - Order' id="ancbtn3" runat="server"
                                    href='ViewProduct.aspx?ProdId=105738'><span>Order now »</span></a>

                            </div>
                        </div>
                    </div>
                </div>

                <!--Content Div-->

                <div class="clear">
                </div>
                <div class="prodbuttons probtnmar" runat="server" id="dvcheckout" style="display: none">
                    <div class="lfloat">
                        <a class="btnorange" id="ancsavebtn" runat="server" href="#"><span>checkout »</span></a>
                    </div>
                </div>
                <div class="clear">
                </div>
                <!--Footer DIV Start-->
                <div id="FooterDiv">
                    <uc2:Footer ID="Footer1" runat="server" />
                </div>
                <!--Footer DIV End-->
            </div>
            <!--CONTENET DIV End-->
        </div>

    </form>


</body>
</html>
