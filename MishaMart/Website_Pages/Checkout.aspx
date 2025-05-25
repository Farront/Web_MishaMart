<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="MishaMart.Website_Pages.Checkout" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h1 class="mb-4">Checkout</h1>
        <link rel="stylesheet" type="text/css" href="Content/style.css" />

        <!-- Cart Summary -->
        <div class="mb-4">
            <h3>Order Summary</h3>
                <asp:Repeater ID="CartRepeater" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Game</th>
                                    <th>Price</th>
                                    <th></th> <!-- Remove the word "Remove" -->
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Name") %></td>
                            <td><%# string.Format("{0:N0} ₫", Convert.ToDecimal(Eval("Price")) * 26000) %></td>
                            <td class="text-center">
                                <!-- Keep the remove button with the X icon -->
                                <asp:LinkButton ID="btnRemove" runat="server" CommandArgument='<%# Eval("ProductID") %>' OnCommand="RemoveProduct_Command" CssClass="btn btn-danger btn-sm">
                                    <i class="bi bi-x"></i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                    <div class="text-end">
                        <strong>Total:</strong> <asp:Label ID="lblTotalPriceVND" runat="server" />
                    </div>
        </div>

        <div class="text-end">
            <asp:Button ID="btnConfirmOrder" runat="server" CssClass="btn btn-success me-2" Text="Confirm Order" OnClick="btnConfirmOrder_Click" />
            <asp:Button ID="btnReturnToDefault" runat="server" CssClass="transparent-button" Text="Continue Shopping" OnClick="btnReturnToDefault_Click" />
        </div>


    </div>
</asp:Content>
