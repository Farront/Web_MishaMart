using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease.Activities;

namespace MishaMart.Website_Pages
{
	public partial class Product : System.Web.UI.Page
	{
        private const decimal ExchangeRate = 26000; // Static exchange rate: 1 USD = 25,908 VND
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string productId = Request.QueryString["ProductID"];
                if (!string.IsNullOrEmpty(productId))
                {
                    LoadProductDetails(productId);
                    LoadProductDescription(productId);
                }
            }
        }

        private void LoadProductDescription(string productId)
        {
            // Path to the description file
            string descriptionFilePath = Server.MapPath($"~/Content/ProductDescriptions/{productId}.txt");

            if (File.Exists(descriptionFilePath))
            {
                // Read the content from the file
                string descriptionContent = File.ReadAllText(descriptionFilePath);

                // Replace newlines with <br> for proper HTML rendering
                lblDetailedDescription.InnerHtml = descriptionContent.Replace(Environment.NewLine, "<br>");
            }
            else
            {
                lblDetailedDescription.InnerHtml = "Description not available for this product.";
            }
        }

        private void LoadProductDetails(string productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT P.Name, P.Description, P.Price, P.ImagePath, C.CategoryName 
                                 FROM Products P
                                 INNER JOIN Categories C ON P.CategoryId = C.CategoryId
                                 WHERE P.ProductId = @ProductId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblProductName.InnerText = reader["Name"].ToString();
                        lblShortDescription.InnerText = reader["Description"].ToString();
                        lblCategoryName.InnerText = reader["CategoryName"].ToString();
                        imgProduct.Src = reader["ImagePath"].ToString();
                        imgProduct.Alt = reader["Name"].ToString();

                        // Convert price from USD to VND and format it
                        decimal priceInUSD = Convert.ToDecimal(reader["Price"]);
                        decimal priceInVND = priceInUSD * ExchangeRate;
                        lblConvertedPrice.Text = string.Format("{0:N0} ₫", priceInVND);
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
            }

            LoadProductDescription(productId);
        }



        protected void BuyNow_Click(object sender, EventArgs e)
        {
            // Retrieve ProductID from the query string
            string productId = Request.QueryString["ProductID"];
            string username = Session["Username"]?.ToString();

            // Ensure the user is logged in
            if (string.IsNullOrEmpty(username))
            {
                // Redirect to Login page if not authenticated
                Response.Redirect("~/Website_Pages/Login.aspx");
                return;
            }

            if (!string.IsNullOrEmpty(productId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if the product is already in the cart
                    string checkQuery = "SELECT COUNT(*) FROM ShoppingCart WHERE Username = @Username AND ProductID = @ProductID";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", username);
                        checkCmd.Parameters.AddWithValue("@ProductID", productId);

                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            // Product is already in the cart
                            Response.Redirect("~/Website_Pages/Checkout.aspx"); // Redirect to the cart page
                            return;
                        }
                    }

                    // Add the product to the shopping cart
                    string insertQuery = "INSERT INTO ShoppingCart (Username, ProductID) VALUES (@Username, @ProductID)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@Username", username);
                        insertCmd.Parameters.AddWithValue("@ProductID", productId);
                        insertCmd.ExecuteNonQuery();
                    }

                    // Redirect to Cart or Checkout page
                    Response.Redirect("~/Website_Pages/Checkout.aspx"); // Adjust this to Checkout.aspx if needed
                }
            }
        }

        protected void btnReturnToDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }


    }
}