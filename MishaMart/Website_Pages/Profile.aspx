<%@ Page Title="Profile" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="MishaMart.Website_Pages.Profile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="profile-container">
        <div class="profile-header">
            <h2>Your Profile</h2>
            <p>Manage your details and preferences</p>
        </div>

        <!-- Profile Card -->
        <div class="profile-card">
            <h4>Account Details</h4>
            <div class="form-group">
                <label for="txtUsername">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Enabled="false" />
            </div>
            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false" />
            </div>
        </div>

        <!-- Update Password -->
        <div class="profile-card">
            <h4>Update Password</h4>
            <asp:Label ID="lblMessage" runat="server" CssClass="text-success" />
            <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
            <div class="form-group">
                <label for="txtCurrentPassword">Current Password</label>
                <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control" TextMode="Password" />
            </div>
            <div class="form-group">
                <label for="txtNewPassword">New Password</label>
                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" />
            </div>
            <div class="form-group">
                <label for="txtConfirmPassword">Confirm New Password</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" />
            </div>
            <asp:Button ID="btnUpdatePassword" runat="server" CssClass="btn btn-custom" Text="Update Password" OnClick="UpdatePassword_Click" />
        </div>

        <!-- Logout Button -->
        <div class="text-center">
            <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-danger" Text="Logout" OnClick="Logout_Click" />
        </div>
    </div>
</asp:Content>