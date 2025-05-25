using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MishaMart.Website_Pages
{
    public partial class EditProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                LoadProductDetails();
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

        private void LoadProductDetails()
        {
            string productId = Request.QueryString["ProductId"];
            if (string.IsNullOrEmpty(productId))
            {
                Response.Redirect("~/ProdManager.aspx");
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Name, Price, CategoryId FROM Products WHERE ProductId = @ProductId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtProductName.Text = reader["Name"].ToString();
                        txtPrice.Text = reader["Price"].ToString();
                        ddlCategory.SelectedValue = reader["CategoryId"].ToString();
                    }
                }
            }
        }

        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            string productId = Request.QueryString["ProductId"];
            if (string.IsNullOrEmpty(productId)) return;

            // Validate price input
            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price))
            {
                Response.Write("<script>alert('Invalid price format. Please enter a valid number.');</script>");
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "UPDATE Products SET Name = @Name, Price = @Price, CategoryId = @CategoryId WHERE ProductId = @ProductId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", txtProductName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@CategoryId", ddlCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    cmd.ExecuteNonQuery();
                }
            }

            UploadFiles(productId);

            Response.Redirect("~/ProdManager.aspx");
        }

        private void UploadFiles(string productId)
        {
            string descriptionPath = Server.MapPath($"~/Content/ProductDescriptions/{productId}.txt");
            string imagePath = Server.MapPath($"~/Images/{productId}.jpg");

            try
            {
                // Replace Description File
                if (fileDescription.HasFile && fileDescription.PostedFile.ContentType == "text/plain")
                {
                    fileDescription.SaveAs(descriptionPath);
                }

                // Replace Image File
                if (fileImage.HasFile && fileImage.PostedFile.ContentType.StartsWith("image/"))
                {
                    fileImage.SaveAs(imagePath);
                }

                Response.Write("<script>alert('Product updated successfully!');</script>");
            }
            catch (Exception ex)
            {
                // Log error for debugging
                System.Diagnostics.Debug.WriteLine($"Error updating files: {ex.Message}");
                Response.Write("<script>alert('Error updating files. Please try again.');</script>");
            }
        }
    }
}