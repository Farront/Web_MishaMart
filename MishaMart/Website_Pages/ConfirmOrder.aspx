<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmOrder.aspx.cs" Inherits="MishaMart.Website_Pages.ConfirmOrder" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h1 class="mb-4">Order Confirmation</h1>

        <!-- Order Details -->
        <div class="mb-4">
            <h3>Thank you for your purchase!</h3>
            <p>Your order number is: <strong><asp:Label ID="lblOrderNumber" runat="server"></asp:Label></strong></p>
            <p>Order Date: <strong><asp:Label ID="lblOrderDate" runat="server"></asp:Label></strong></p>
        </div>

        <!-- Purchased Items -->
        <div class="mb-4">
            <h3>Purchased Items</h3>
            <asp:Repeater ID="OrderItemsRepeater" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Product</th>
                                <th>Price</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("ProductName") %></td>
                        <td><%# string.Format("{0:N0} ₫", Convert.ToDecimal(Eval("Price")) * 26000) %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <!-- Total Amount -->
        <div class="text-end">
            <h4>Total: <asp:Label ID="lblTotalAmountVND" runat="server"></asp:Label></h4>
        </div>

        <!-- QR Code for Payment -->
        <div class="mb-4">
            <h3>Scan to Pay</h3>
            <asp:Image ID="qrCodeImage" runat="server" AlternateText="QR Code for Payment" />
        </div>

        <!-- Confirm Payment -->
        <div class="text-end">
            <asp:Button ID="btnConfirmPayment" runat="server" CssClass="btn btn-success" Text="Confirm Payment" OnClick="btnConfirmPayment_Click" />
        </div>
    </div>
</asp:Content>
