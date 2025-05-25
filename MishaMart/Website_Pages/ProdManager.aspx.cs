using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MishaMart.Website_Pages
{
    public partial class ProdManager : System.Web.UI.Page
    {    
        private const decimal ExchangeRate = 26000m; // 1 USD = 26,000 VND
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
                LoadCategories();
            }
        }

        private void LoadCategories()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT CategoryId, CategoryName FROM Categories";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    ddlCategory.DataSource = cmd.ExecuteReader();
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataBind();
                }
            }
        }

        private void LoadProducts()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT P.ProductId, P.Name, P.Price, C.CategoryName, P.CreatedDate FROM Products P JOIN Categories C ON P.CategoryId = C.CategoryId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Convert USD price to VND in DataTable
                    foreach (DataRow row in dt.Rows)
                    {
                        decimal priceInUSD = Convert.ToDecimal(row["Price"]);
                        decimal priceInVND = priceInUSD * ExchangeRate;

                        row["Price"] = priceInVND;
                    }

                    // Format price when displaying
                    ProductsGrid.DataSource = dt;
                    ProductsGrid.DataBind();


                    ProductsGrid.DataSource = dt;
                    ProductsGrid.DataBind();
                }
            }
        }


        protected void ProductsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string productId = e.CommandArgument.ToString();

            if (e.CommandName == "EditProduct")
            {
                Response.Redirect($"EditProduct.aspx?ProductId={productId}");
            }
            else if (e.CommandName == "DeleteProduct")
            {
                DeleteProduct(productId);
            }
        }

        private void DeleteProduct(string productId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM Products WHERE ProductId = @ProductId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadProducts();
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text.Trim();
            decimal priceInUSD;
            string categoryId = ddlCategory.SelectedValue;

            if (string.IsNullOrEmpty(productName) || !decimal.TryParse(txtPrice.Text, out priceInUSD))
            {
                Response.Write("<script>alert('Please enter valid product details');</script>");
                return;
            }

            // Convert price to VND
            decimal priceInVND = priceInUSD * ExchangeRate;

            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            int newProductId;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Products (Name, Price, CategoryId, CreatedDate) OUTPUT INSERTED.ProductId VALUES (@Name, @Price, @CategoryId, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", productName);
                    cmd.Parameters.AddWithValue("@Price", priceInVND);  // Store converted price
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                    newProductId = (int)cmd.ExecuteScalar();
                }
            }

            UploadFiles(newProductId);
        }


        private void UploadFiles(int productId)
        {
            // Store files inside the web application folder
            string descriptionPath = Server.MapPath($"~/Content/ProductDescriptions/{productId}.txt");
            string imagePath = Server.MapPath($"~/Images/{productId}.jpg");

            try
            {
                // Save Description File
                if (fileDescription.HasFile && fileDescription.PostedFile.ContentType == "text/plain")
                {
                    fileDescription.SaveAs(descriptionPath);
                }

                // Save Image File inside ~/Images/
                if (fileImage.HasFile && fileImage.PostedFile.ContentType.StartsWith("image/"))
                {
                    fileImage.SaveAs(imagePath);
                }

                Response.Write("<script>alert('Product added successfully!');</script>");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error uploading files: {ex.Message}');</script>");
            }
        }
    }
}