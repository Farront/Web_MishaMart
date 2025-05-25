using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace MishaMart.Website_Pages
{
    public partial class UsersManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRoles();
                LoadUsers();
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
                    ddlNewRole.DataSource = reader;
                    ddlNewRole.DataTextField = "RoleName";
                    ddlNewRole.DataValueField = "RoleId";
                    ddlNewRole.DataBind();
                }
            }
        }

        private void LoadUsers()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT U.UserId, U.Username, U.Email, R.RoleName, U.CreatedDate FROM Users U JOIN Roles R ON U.RoleId = R.RoleId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    UsersGrid.DataSource = dt;
                    UsersGrid.DataBind();
                }
            }
        }

        protected void UsersGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string userId = e.CommandArgument.ToString();

            if (e.CommandName == "EditUser")
            {
                Response.Redirect($"EditUser.aspx?UserId={userId}");
            }
            else if (e.CommandName == "DeleteUser")
            {
                DeleteUser(userId);
            }
        }

        private void DeleteUser(string userId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM Users WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadUsers();
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            string hashedPassword = HashPassword(txtNewPassword.Text.Trim());

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Users (Username, Email, PasswordHash, RoleId, CreatedDate) VALUES (@Username, @Email, @PasswordHash, @RoleId, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", txtNewUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtNewEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    cmd.Parameters.AddWithValue("@RoleId", ddlNewRole.SelectedValue);

                    cmd.ExecuteNonQuery();
                }
            }

            LoadUsers();
            txtNewUsername.Text = "";
            txtNewEmail.Text = "";
            txtNewPassword.Text = "";
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