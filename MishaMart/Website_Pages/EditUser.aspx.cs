using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MishaMart.Website_Pages
{
    public partial class EditUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRoles();
                LoadUserDetails();
            }
        }

        private void LoadRoles()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT RoleId, RoleName FROM Roles";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlRole.DataSource = reader;
                    ddlRole.DataTextField = "RoleName";
                    ddlRole.DataValueField = "RoleId";
                    ddlRole.DataBind();
                }
            }
        }

        private void LoadUserDetails()
        {
            string userId = Request.QueryString["UserId"];
            if (string.IsNullOrEmpty(userId))
            {
                Response.Redirect("~/UsersManager.aspx");
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Username, Email, RoleId FROM Users WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtUsername.Text = reader["Username"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        ddlRole.SelectedValue = reader["RoleId"].ToString();
                    }
                }
            }
        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            string userId = Request.QueryString["UserId"];
            if (string.IsNullOrEmpty(userId)) return;

            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "UPDATE Users SET Username = @Username, Email = @Email, RoleId = @RoleId WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@RoleId", ddlRole.SelectedValue);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    cmd.ExecuteNonQuery();
                }
            }

            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                UpdateUserPassword(userId, txtPassword.Text);
            }

            Response.Redirect("~/Website_Pages/UsersManager.aspx");
        }

        private void UpdateUserPassword(string userId, string newPassword)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            string hashedPassword = HashPassword(newPassword);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "UPDATE Users SET PasswordHash = @PasswordHash WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}