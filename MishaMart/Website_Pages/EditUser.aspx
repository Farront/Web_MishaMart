<%@ Page Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="MishaMart.Website_Pages.EditUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Edit User</h2>

        <asp:Panel ID="EditPanel" runat="server">
            <div class="form-group">
                <label>Username:</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label>Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label>Role:</label>
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label>New Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
            </div>

            <asp:Button ID="btnUpdateUser" runat="server" Text="Update User" CssClass="btn btn-primary mt-3" OnClick="btnUpdateUser_Click" />
        </asp:Panel>
    </div>
</asp:Content>

