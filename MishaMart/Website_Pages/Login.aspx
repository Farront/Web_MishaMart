<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MishaMart.Website_Pages.Login" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login - MishaMart</title>

    <!-- Include Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Include Custom Styles -->
    <link rel="stylesheet" href="../Content/styles.css" />
</head>
<body class="login-page">
    <form id="formLogin" runat="server">
        <div class="login-container d-flex align-items-center justify-content-center min-vh-100">
            <div class="card shadow-lg p-4" style="max-width: 400px; width: 100%; border-radius: 10px;">
                <h2 class="text-center fw-bold mb-4">Sign in to Your Account</h2>
                <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
                <div class="mb-3">
                    <label for="email" class="form-label">Email Address</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-lg rounded-pill" placeholder="Enter your email"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="password" class="form-label">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control form-control-lg rounded-pill" placeholder="Enter your password"></asp:TextBox>
                </div>
                <div class="d-grid">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-lg rounded-pill" OnClick="AuthenticateUser" />
                </div>
                <div class="text-center mt-3">
                    <a href="#" class="text-muted small">Forgot your password?</a>
                </div>
            </div>
        </div>
    </form>

    <!-- Include Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>