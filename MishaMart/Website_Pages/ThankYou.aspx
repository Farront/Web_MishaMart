<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThankYou.aspx.cs" Inherits="MishaMart.Website_Pages.ThankYou" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5 text-center">
        <h1 class="mb-4 text-success">Thank You for Your Purchase!</h1>
        <p>Your order has been successfully placed.</p>
        <p>Your Order Number: <strong><asp:Label ID="lblOrderNumber" runat="server" CssClass="text-primary"></asp:Label></strong></p>
        <p>We have sent a confirmation to your email address.</p>

        <div class="mt-4">
            <asp:Button ID="btnContinueShopping" runat="server" CssClass="btn btn-primary" Text="Continue Shopping" OnClick="btnContinueShopping_Click" />
            <asp:Button ID="btnViewMyOrder" runat="server" CssClass="btn btn-secondary" Text="View My Order" OnClick="btnViewMyOrder_Click" />
        </div>
    </div>
</asp:Content>
