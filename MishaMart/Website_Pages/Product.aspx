<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="MishaMart.Website_Pages.Product" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Additional product-specific styles */
        .product-container {
            border: 1px solid #ddd;
            padding: 20px;
            border-radius: 10px;
            background-color: #f9f9f9;
        }
        .long-description {
            margin-top: 20px;
            line-height: 1.6;
        }
    </style>

    <div class="container mt-5 product-container">
        <div class="row">
            <!-- Left Column: Product Image, Short Description, and Category -->
            <div class="col-md-6 d-flex flex-column align-items-center">
                <img id="imgProduct" runat="server" class="img-fluid rounded mx-auto d-block" alt="Product Image" />
                <p id="lblShortDescription" runat="server" class="text-justify mt-3"></p>
                <p class="text-muted"><strong>Category:</strong> <span id="lblCategoryName" runat="server"></span></p>
            </div>

            <!-- Right Column: Product Details and Long Description -->
            <div class="col-md-6">
                <h2 id="lblProductName" runat="server" class="mt-3"></h2>

                <!-- Price Conversion: USD to VND with thousands separator -->
                <p class="text-success"><strong>Price: <asp:Label ID="lblConvertedPrice" runat="server" /></strong></p>

                <div id="lblDetailedDescription" runat="server" class="long-description"></div>

                <!-- Buttons -->
                <div class="d-grid gap-2">
                    <asp:Button ID="btnBuyNow" runat="server" Text="Add to cart" CssClass="btn btn-success btn-lg" OnClick="BuyNow_Click" />
                    <asp:Button ID="btnReturnToDefault" runat="server" CssClass="btn btn-primary" Text="Return to Homepage" OnClick="btnReturnToDefault_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
