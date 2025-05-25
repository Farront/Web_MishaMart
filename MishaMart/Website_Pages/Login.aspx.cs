using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace MishaMart.Website_Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void AuthenticateUser(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text;

                string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = @"SELECT U.PasswordHash, U.Username, R.RoleName 
                                   FROM Users U
                                   INNER JOIN Roles R ON U.RoleId = R.RoleId
                                   WHERE U.Email = @Email";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string storedHash = reader["PasswordHash"].ToString();
                            string username = reader["Username"].ToString();
                            string roleName = reader["RoleName"].ToString(); // Get the role name

                            if (password == storedHash) // ⚠️ Use secure password hashing in production!
                            {
                                // Store user info in session
                                Session["Username"] = username;
                                Session["Role"] = roleName;

                                // Redirect based on role
                                if (roleName == "Admin")
                                {
                                    Response.Redirect("~/Website_Pages/AdminDashboard.aspx"); // Admin Panel
                                }
                                else
                                {
                                    Response.Redirect("~/Default.aspx"); // Regular user homepage
                                }
                            }
                            else
                            {
                                lblError.Text = "Invalid email or password.";
                            }
                        }
                        else
                        {
                            lblError.Text = "Invalid email or password.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occurred. Please try again.";
                Console.WriteLine(ex.Message);
            }
        }
    }
}
