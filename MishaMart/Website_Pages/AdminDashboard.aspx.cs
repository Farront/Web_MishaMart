using System;
using System.Data.SqlClient;
using System.Configuration;

namespace MishaMart.Website_Pages
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboardStats();
            }
        }

        private void LoadDashboardStats()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            decimal usdToVndRate = 26000m; // Approximate exchange rate (Update as needed)

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Total Registered Users
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users", conn))
                {
                    lblTotalUsers.Text = cmd.ExecuteScalar().ToString();
                }

                // Total Admins
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE RoleId = (SELECT RoleId FROM Roles WHERE RoleName = 'Admin')", conn))
                {
                    lblTotalAdmins.Text = cmd.ExecuteScalar().ToString();
                }

                // Total Products
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Products", conn))
                {
                    lblTotalProducts.Text = cmd.ExecuteScalar().ToString();
                }

                // Total Sales (Orders) this month
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT COUNT(*) FROM Orders
                    WHERE OrderDate >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)", conn))
                {
                    lblTotalSales.Text = cmd.ExecuteScalar().ToString();
                }


                // Active Users (Last 30 Days)
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE CreatedDate >= DATEADD(DAY, -30, GETDATE())", conn))
                {
                    lblActiveUsers.Text = cmd.ExecuteScalar().ToString();
                }

                // New Users (Last Month)
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE CreatedDate >= DATEADD(MONTH, -1, GETDATE())", conn))
                {
                    lblNewUsers.Text = cmd.ExecuteScalar().ToString();
                }

                // Best-Selling Products (Top 5)
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 5 P.Name, COUNT(O.ProductId) AS TotalSales FROM OrderDetails O JOIN Products P ON O.ProductId = P.ProductId GROUP BY P.Name ORDER BY TotalSales DESC", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    lblBestSellingProducts.Text = "";
                    while (reader.Read())
                    {
                        lblBestSellingProducts.Text += reader["Name"].ToString() + " (" + reader["TotalSales"].ToString() + " sales) <br />";
                    }
                    reader.Close();
                }

                // Current Month Revenue (Converted to VND)
                using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(TotalAmount), 0) FROM Orders WHERE OrderDate >= DATEADD(MONTH, 0, GETDATE())", conn))
                {
                    decimal currentRevenueUSD = Convert.ToDecimal(cmd.ExecuteScalar());
                    decimal currentRevenueVND = currentRevenueUSD * usdToVndRate;
                    lblCurrentMonthRevenue.Text = currentRevenueVND.ToString("N0") + " VND"; // Formatting for readability
                }
            }
        }
    }
}
