<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MishaMart._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <!-- Header -->
        <header class="d-flex justify-content-between align-items-center py-3">
            <h1>MishaMart</h1>
        </header>

        <!-- Hero Section -->
        <div class="second-hero-section text-white p-5 rounded mb-4">
            <h2>Discover what the virtual world has to offer.</h2>
        </div>
        <div class="d-flex justify-content-center mb-4">
    <!-- Search Bar -->
        <div class="input-group" style="max-width: 2000px;">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search games..." />
            <button class="btn btn-primary" type="button" onclick="document.getElementById('<%= btnSearch.ClientID %>').click();">
                <i class="bi bi-search"></i> <!-- Bootstrap Search Icon -->
            </button>
            <asp:Button ID="btnSearch" runat="server" CssClass="d-none" OnClick="btnSearch_Click" />
        </div>
    </div>

 <!-- Product Grid -->
            <section class="row">
    <asp:Repeater ID="ProductsRepeater" runat="server">
        <ItemTemplate>
            <div class="col-md-4 mb-4">
                <div class="card h-100">
                    <!-- Make the image clickable -->
                    <a href='<%# ResolveUrl("~/Website_Pages/Product.aspx?ProductId=" + Eval("ProductId")) %>' style="text-decoration: none;">

                        <img src='<%# Eval("ImagePath") %>' class="card-img-top" alt='<%# Eval("Name") %>' style="object-fit: cover; height: 200px;">
                    </a>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<div class="card-body d-flex flex-column">
                        <!-- Make the name clickable -->
                        <a href='<%# ResolveUrl("~/Website_Pages/Product.aspx?ProductId=" + Eval("ProductId")) %>' style="text-decoration: none; color: inherit;">
                            <h5 class="card-title"><%# Eval("Name") %></h5>
                        </a>
                        <p class="card-text"><%# Eval("Description") %></p>
                        <p class="card-text"><strong>Price:</strong> <%# string.Format("{0:N0}", Convert.ToDecimal(Eval("Price")) * 26000) %> ₫</p>
                        <div class="mt-auto">
                            <div class="d-flex justify-content-center align-items-center" style="height: 100px;">
                                <!-- Buy Now Button -->
                                <asp:Button ID="btnBuyNow" runat="server"  CssClass="btn btn-success" CommandArgument='<%# Eval("ProductId") %>' OnCommand="BuyNow_Command" Text="Buy Now" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

                <!-- Pagination Controls -->
                <div class="pagination d-flex justify-content-center mt-4">
                    <asp:Literal ID="PaginationLinks" runat="server"></asp:Literal>
                </div>

</section>

    <!-- Bootstrap Script -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>

