using System;
using System.Configuration;
using System.Data.SqlClient;
using WebGrease.Activities;

namespace MishaMart.Website_Pages
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void CreateAccount(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();
                string confirmPassword = txtConfirmPassword.Text.Trim();

                if (password != confirmPassword)
                {
                    lblError.Text = "Passwords do not match.";
                    return;
                }

                // Insert into database
                string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = "INSERT INTO Users (Username, Email, Password) VALUES (@Username, @Email, @Password)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                Response.Redirect("Login.aspx"); // Redirect to Login page after successful registration
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occurred. Please try again.";
                Console.WriteLine(ex.Message); 
            }
        }


    }
}
