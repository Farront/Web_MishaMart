<%@ Page Title="Sign Up" Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="MishaMart.Website_Pages.SignUp" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sign Up - MishaMart</title>

    <!-- Include Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="../Content/styles.css" />
</head>
<body class="login-page">
    <form id="formSignUp" runat="server">
        <div class="login-container d-flex align-items-center justify-content-center min-vh-100">
            <div class="card shadow-lg p-4" style="max-width: 400px; width: 100%; border-radius: 10px;">
                <h2 class="text-center fw-bold mb-4">Create Your Account</h2>
                <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3"></asp:Label>
                <div class="mb-3">
                    <label for="email" class="form-label">Email Address</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-lg rounded-pill" placeholder="Enter your email"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="username" class="form-label">Username</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control form-control-lg rounded-pill" placeholder="Enter your username"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="password" class="form-label">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control form-control-lg rounded-pill" placeholder="Create a password"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="confirmPassword" class="form-label">Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control form-control-lg rounded-pill" placeholder="Confirm your password"></asp:TextBox>
                </div>
                <div class="d-grid">
                    <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" CssClass="btn btn-primary btn-lg rounded-pill" OnClick="CreateAccount" />
                </div>
                <div class="text-center mt-3">
                    <a href="Login.aspx" class="text-muted small">Already have an account? Sign In</a>
                </div>
            </div>
        </div>
    </form>

    <!-- Include Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
