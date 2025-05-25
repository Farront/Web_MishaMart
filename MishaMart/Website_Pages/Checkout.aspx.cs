using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MishaMart.Website_Pages
{
    public partial class Checkout : System.Web.UI.Page
    {
        private const decimal ExchangeRate = 26000; // Static exchange rate (1 USD = 25,908 VND)

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCart();
            }
        }

        private void LoadCart()
        {
            string username = Session["Username"]?.ToString();

            if (string.IsNullOrEmpty(username))
            {
                Response.Redirect("~/Website_Pages/Login.aspx");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT P.ProductID, P.Name, P.Price 
                                 FROM ShoppingCart C 
                                 INNER JOIN Products P ON C.ProductID = P.ProductId 
                                 WHERE C.Username = @Username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable cartTable = new DataTable();
                    cartTable.Load(reader);

                    CartRepeater.DataSource = cartTable;
                    CartRepeater.DataBind();

                    decimal totalPriceUSD = 0;
                    foreach (DataRow row in cartTable.Rows)
                    {
                        totalPriceUSD += Convert.ToDecimal(row["Price"]);
                    }

                    decimal totalPriceVND = totalPriceUSD * ExchangeRate;
                    lblTotalPriceVND.Text = string.Format("{0:N0} ₫", totalPriceVND);
                }
            }
        }

        protected void RemoveProduct_Command(object sender, CommandEventArgs e)
        {
            int productId = Convert.ToInt32(e.CommandArgument);
            string username = Session["Username"]?.ToString();

            if (string.IsNullOrEmpty(username))
            {
                Response.Redirect("~/Website_Pages/Login.aspx");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ShoppingCart WHERE Username = @Username AND ProductID = @ProductID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            LoadCart();
        }

        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            string username = Session["Username"]?.ToString();

            if (string.IsNullOrEmpty(username))
            {
                Response.Redirect("~/Website_Pages/Login.aspx");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            int orderId = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string insertOrderQuery = @"INSERT INTO Orders (UserId, TotalAmount, OrderDate) 
                                            OUTPUT INSERTED.OrderId 
                                            VALUES ((SELECT UserId FROM Users WHERE Username = @Username), @TotalAmount, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(insertOrderQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@TotalAmount", lblTotalPriceVND.Text);
                    orderId = (int)cmd.ExecuteScalar();
                }

                string insertOrderDetailsQuery = @"INSERT INTO OrderDetails (OrderId, ProductId, Price) 
                                                   SELECT @OrderId, C.ProductID, P.Price * @ExchangeRate 
                                                   FROM ShoppingCart C 
                                                   INNER JOIN Products P ON C.ProductID = P.ProductId 
                                                   WHERE C.Username = @Username;
                                                   DELETE FROM ShoppingCart WHERE Username = @Username";

                using (SqlCommand cmd = new SqlCommand(insertOrderDetailsQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@ExchangeRate", ExchangeRate);
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Redirect($"ConfirmOrder.aspx?OrderId={orderId}");
        }

        protected void btnReturnToDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}
