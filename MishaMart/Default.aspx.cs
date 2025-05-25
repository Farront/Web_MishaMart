using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace MishaMart
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }

        private void LoadProducts(string searchQuery = null)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //  JOIN with Categories table
                string query = @"
            SELECT 
                p.ProductId, 
                p.Name, 
                p.Description, 
                p.Price, 
                p.ImagePath 
            FROM 
                Products p
            LEFT JOIN 
                Categories c ON p.CategoryId = c.CategoryId";

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    query += " WHERE p.Name LIKE @Search OR p.Description LIKE @Search OR c.CategoryName LIKE @Search";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + searchQuery + "%");
                    }

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    if (dt.Rows.Count == 0)
                    {
                        ProductsRepeater.Visible = false;
                        PaginationLinks.Visible = false;
                        Response.Write("<p class='text-center text-muted'>No products found. Please try a different search term.</p>");
                    }
                    else
                    {
                        ProductsRepeater.Visible = true;
                        PaginationLinks.Visible = true;

                        // Using PagedDataSource for pagination
                        PagedDataSource pagedDataSource = new PagedDataSource
                        {
                            DataSource = dt.DefaultView,
                            AllowPaging = true,
                            PageSize = 15
                        };

                        int currentPage;
                        if (Request.QueryString["Page"] != null && int.TryParse(Request.QueryString["Page"], out currentPage))
                        {
                            pagedDataSource.CurrentPageIndex = currentPage;
                        }
                        else
                        {
                            pagedDataSource.CurrentPageIndex = 0;
                        }

                        ProductsRepeater.DataSource = pagedDataSource;
                        ProductsRepeater.DataBind();

                        GeneratePaginationLinks(pagedDataSource.PageCount, pagedDataSource.CurrentPageIndex);
                    }
                }
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();
            LoadProducts(searchQuery);
        }


        private void GeneratePaginationLinks(int totalPages, int currentPageIndex)
        {
            string paginationLinks = "<nav aria-label='Page navigation'><ul class='pagination'>";

            // Previous Button
            if (currentPageIndex > 0)
            {
                paginationLinks += $"<li class='page-item'><a class='page-link' href='?Page={currentPageIndex - 1}'>Previous</a></li>";
            }
            else
            {
                paginationLinks += "<li class='page-item disabled'><a class='page-link' href='#'>Previous</a></li>";
            }

            // Page Number Buttons
            for (int i = 0; i < totalPages; i++)
            {
                if (i == currentPageIndex)
                {
                    paginationLinks += $"<li class='page-item active'><a class='page-link' href='#'>{i + 1}</a></li>";
                }
                else
                {
                    paginationLinks += $"<li class='page-item'><a class='page-link' href='?Page={i}'>{i + 1}</a></li>";
                }
            }

            // Next Button
            if (currentPageIndex < totalPages - 1)
            {
                paginationLinks += $"<li class='page-item'><a class='page-link' href='?Page={currentPageIndex + 1}'>Next</a></li>";
            }
            else
            {
                paginationLinks += "<li class='page-item disabled'><a class='page-link' href='#'>Next</a></li>";
            }

            paginationLinks += "</ul></nav>";
            PaginationLinks.Text = paginationLinks;
        }

        protected void BuyNow_Command(object sender, CommandEventArgs e)
        {
            string productId = e.CommandArgument.ToString();

            // Redirect to the Product.aspx page with the ProductId in the query string
            Response.Redirect($"Website_Pages/Product.aspx?ProductID={productId}");
        }

    }
}
