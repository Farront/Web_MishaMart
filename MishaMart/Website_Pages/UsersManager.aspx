<%@ Page Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UsersManager.aspx.cs" Inherits="MishaMart.Website_Pages.UsersManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Manage Users</h2>

        <div class="card mb-4">
            <div class="card-header bg-primary text-white">Add New User</div>
            <div class="card-body">
                <div class="form-group">
                    <label>Username:</label>
                    <asp:TextBox ID="txtNewUsername" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label>Email:</label>
                    <asp:TextBox ID="txtNewEmail" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label>Password:</label>
                    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" />
                </div>

                <div class="form-group">
                    <label>Role:</label>
                    <asp:DropDownList ID="ddlNewRole" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>

                <asp:Button ID="btnAddUser" runat="server" Text="Add User" CssClass="btn btn-success mt-3" OnClick="btnAddUser_Click" />
            </div>
        </div>

        <asp:GridView ID="UsersGrid" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" OnRowCommand="UsersGrid_RowCommand">
            <Columns>
                <asp:BoundField DataField="UserId" HeaderText="User ID" />
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="RoleName" HeaderText="Role" />
                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd}" />

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditUser" CommandArgument='<%# Eval("UserId") %>' CssClass="btn btn-warning btn-sm" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteUser" CommandArgument='<%# Eval("UserId") %>' CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Are you sure?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
