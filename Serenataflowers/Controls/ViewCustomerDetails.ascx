<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCustomerDetails.ascx.cs"
    Inherits="Serenataflowers.Controls.ViewCustomerDetails" %>
<asp:UpdatePanel ID="CustInfoUpdatePanel" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="lblHiddenText" runat="server" Value="Customer Details:" />
        <asp:Label ID="lblName" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblOrganization" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblHouseNumber" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblStreet" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblDistrict" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblPostCode" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTown" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblCountry" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblMobile" runat="server" Visible="false"></asp:Label>
    </ContentTemplate>
</asp:UpdatePanel>
