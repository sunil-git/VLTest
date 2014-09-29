<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewRecipientDetails.ascx.cs"
    Inherits="Serenataflowers.Controls.ViewRecipientDetails" %>
<asp:UpdatePanel ID="updDeliveryInfoPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Label ID="lblName" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblOrganization" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblHouseNumber" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblStreet" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblDistrict" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblPostCode" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTown" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblCountry" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblMobile" runat="server"  Visible="false"></asp:Label>
    </ContentTemplate>
</asp:UpdatePanel>
