<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactPage.aspx.cs" Inherits="Serenataflowers.ContactPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="Controls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="Controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <asp:Literal ID="ltTitle"  runat="server"></asp:Literal>
   <title id="Title1"  runat="server" visible="false"></title>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" name="viewport" />
    <link href="~/Styles/m_style.css" rel="stylesheet" type="text/css" />
 </head>
 <script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
    <script type="text/javascript">
        function DoPostBack() {
            __doPostBack('ancSend', '');
        } 
    </script>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="page_wrapper">
        <div id="content_normal">
            <!--Header DIV Start-->
            <div id="header">
                <uc1:Header ID="Header1" runat="server" EnableUrl="true" />
            </div>
            <!--Header DIV End-->
            <div id="EmailCenter">
            </div>
            <!--CONTENET DIV Start-->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="divReply" runat="server">
                        <div class="divrow dottedline">
                            <h4 style="font-size: 16px">
                                <asp:Label ID="lblName" runat="server" Width="200"></asp:Label></h4>
                            <span class="datetext" style="font-size: 10px">
                                <asp:Label ID="lblDateText" runat="server"></asp:Label></span>
                        </div>
                        <div class="divrow dottedline">
                            <div>
                                <p class="post">
                                    <asp:Label ID="lblLastMsg" runat="server" />
                                </p>
                            </div>
                        </div>
                        <div class="divrow dottedline">
                            <asp:TextBox ID="txtMessage" class="textarea" runat="server" Height="200px" TextMode="MultiLine" Text=""></asp:TextBox>
                        </div>
                        <div class="clearleft">
                        </div>
                        <br />
                        <div style="float: right">
                            <a href="Javascript:DoPostBack();" class="btnorange" id="ancSend"><span>Send Reply</span></a>
                        </div>
                    </div>

                    <div id="divResponse" runat="server" >
                            <div class="divrow" style="float:left;width:100%;">
                               <h4 style="font-size: 16px;color: orange;">
                                Thank You !</h4>
                                <br />
                                <p class="post">
                                    We have received your reply and will work hard to come back to you as soon as possible<br /> </br>
                                    Kind Regards, </br> </br>
                                    Serenata</br>
                                </p>
                                
                    </div>
                          </div>

                    <div class="divrow dottedline">
                     <br />
                        <h4 style="font-size: 16px">
                            History</h4>
                    </div>
                    <div class="clearleft"></div>
                    <div>
                        <br />
                        <asp:DataList ID="dlMsg" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                            RepeatColumns="1" OnItemDataBound="dlMsg_ItemDataBound">
                            <ItemTemplate>
                                <span class="datetext" style="font-size: 10px">
                                    <asp:Label ID="dateMsg" runat="server" Text=""></asp:Label></span>
                                <div class="divrow dottedline">
                                    <div>
                                        <p class="post">
                                            <asp:Label ID="lblMsg" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "encryptedMessage") %>'></asp:Label></p>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdnMessageId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IdMessage") %>' />
                                <asp:HiddenField ID="hdnFromName" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "MessageFromName") %>' />
                                <asp:HiddenField ID="hdnMsgDate" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "MessageDate") %>' />
                                <asp:HiddenField ID="hdnOrderId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "OrderId") %>' />
                            </ItemTemplate>
                        </asp:DataList>
                        <br />
                    </div>
                    <div class="clearleft">
                    </div>
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--CONTENET DIV End-->
            <!--Footer DIV Start-->
            <div id="FooterDiv">
                <uc2:Footer ID="Footer1" runat="server" />
            </div>
            <!--Footer DIV End-->
        </div>
    </div>
    </form>
</body>
</html>
