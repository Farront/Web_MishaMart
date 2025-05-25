<%@ Page Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="MishaMart.Website_Pages.AdminDashboard" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="container mt-4">
        <h2>Admin Dashboard</h2>

        <div class="row">
            <div class="col-md-3">
                <div class="card text-white bg-primary mb-3 h-100">
                    <div class="card-header">Registered Users</div>
                    <div class="card-body">
                        <h4 class="card-title"><asp:Label ID="lblTotalUsers" runat="server" Text="Loading..." /></h4>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card text-white bg-primary mb-3 h-100">
                    <div class="card-header">Total Admins</div>
                    <div class="card-body">
                        <h4 class="card-title"><asp:Label ID="lblTotalAdmins" runat="server" Text="Loading..." /></h4>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card text-white bg-primary mb-3 h-100">
                    <div class="card-header">Total Products</div>
                    <div class="card-body">
                        <h4 class="card-title"><asp:Label ID="lblTotalProducts" runat="server" Text="Loading..." /></h4>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card text-white bg-primary mb-3 h-100">
                    <div class="card-header">This month Total Sales</div>
                    <div class="card-body">
                        <h4 class="card-title"><asp:Label ID="lblTotalSales" runat="server" Text="Loading..." /></h4>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-3">
                <div class="card text-white bg-primary mb-3 h-100">
                    <div class="card-header">Active Users (Last 30 Days)</div>
                    <div class="card-body">
                        <h4 class="card-title"><asp:Label ID="lblActiveUsers" runat="server" Text="Loading..." /></h4>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card text-white bg-primary mb-3 h-100">
                    <div class="card-header">New Users (Last Month)</div>
                    <div class="card-body">
                        <h4 class="card-title"><asp:Label ID="lblNewUsers" runat="server" Text="Loading..." /></h4>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card text-white bg-primary mb-3 h-100">
                    <div class="card-header">Best-Selling Products</div>
                    <div class="card-body">
                        <h5 class="card-title"><asp:Label ID="lblBestSellingProducts" runat="server" Text="Loading..." /></h5>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card text-white bg-primary mb-3 h-100">
                    <div class="card-header">Current Month Revenue (VND)</div>
                    <div class="card-body">
                        <h4 class="card-title"><asp:Label ID="lblCurrentMonthRevenue" runat="server" Text="Loading..." /></h4>
                    </div>
                </div>
            </div>
        </div>
    </div>



        <!-- Charts Section -->
        <div class="row mt-4">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">Monthly Sales Trend</div>
                    <div class="card-body">
                        <canvas id="salesChart"></canvas>
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">Top-Selling Products</div>
                    <div class="card-body">
                        <canvas id="productsChart"></canvas>
                    </div>
                </div>
            </div>
        </div>

    <!-- Chart.js Script -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        // Fetch data from the server via AJAX (to be populated dynamically)
        fetch('GetChartData.aspx')
            .then(response => response.json())
            .then(data => {
                // Sales Chart
                new Chart(document.getElementById('salesChart'), {
                    type: 'line',
                    data: {
                        labels: data.sales.labels,
                        datasets: [{
                            label: 'Revenue ($)',
                            data: data.sales.values,
                            backgroundColor: 'rgba(54, 162, 235, 0.5)',
                            borderColor: 'rgba(54, 162, 235, 1)',
                            borderWidth: 2
                        }]
                    }
                });

                // Products Chart
                new Chart(document.getElementById('productsChart'), {
                    type: 'bar',
                    data: {
                        labels: data.products.labels,
                        datasets: [{
                            label: 'Units Sold',
                            data: data.products.values,
                            backgroundColor: 'rgba(255, 99, 132, 0.5)',
                            borderColor: 'rgba(255, 99, 132, 1)',
                            borderWidth: 2
                        }]
                    }
                });
            });
    </script>
</asp:Content>

