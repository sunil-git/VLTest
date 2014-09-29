<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BasketCount.ascx.cs" Inherits="Serenataflowers.Controls.BasketCount" %>
<asp:UpdatePanel ID="UpBasketCount" runat="server">
  <ContentTemplate>
  <asp:Label ID="basketCount" runat="server" Text=""></asp:Label>
 </ContentTemplate>
</asp:UpdatePanel>
