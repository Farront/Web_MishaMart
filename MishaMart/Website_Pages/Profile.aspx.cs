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
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProfile();
            }
        }

        private void LoadProfile()
        {
            // Fetch the currently logged-in user's username from the Session
            string username = Session["Username"] as string;

            if (string.IsNullOrEmpty(username))
            {
                // Redirect to login if no user is logged in
                Response.Redirect("~/Website_Pages/Login.aspx");
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Username, Email FROM Users WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtUsername.Text = reader["Username"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                    }
                }
            }
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Website_Pages/Login.aspx");
        }



        protected void UpdatePassword_Click(object sender, EventArgs e)
        {
            string username = Session["Username"] as string;
            string currentPassword = txtCurrentPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (newPassword != confirmPassword)
            {
                lblError.Text = "New Password and Confirm Password do not match.";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT PasswordHash FROM Users WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string storedPasswordHash = reader["PasswordHash"].ToString();

                        // Verify the current password
                        if (storedPasswordHash != currentPassword) // Replace with proper password hashing for production
                        {
                            lblError.Text = "Current Password is incorrect.";
                            return;
                        }
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string updateQuery = "UPDATE Users SET PasswordHash = @NewPassword WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword); // Replace with password hashing for production
                    cmd.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    lblMessage.Text = "Password updated successfully!";
                }
            }
        }
    }
}