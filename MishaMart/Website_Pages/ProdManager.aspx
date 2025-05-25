<%@ Page Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProdManager.aspx.cs" Inherits="MishaMart.Website_Pages.ProdManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Product Management</h2>

        <div class="card">
            <div class="card-header bg-primary text-white">Add New Product</div>
            <div class="card-body">
                <div class="form-group">
                    <label>Product Name:</label>
                    <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label>Price (USD):</label>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label>Category:</label>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group">
                    <label>Upload Description (.txt):</label>
                    <asp:FileUpload ID="fileDescription" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label>Upload Image:</label>
                    <asp:FileUpload ID="fileImage" runat="server" CssClass="form-control" onchange="previewImage(this)" />
                    <img id="imagePreview" src="#" alt="Image Preview" class="mt-3" style="max-width: 300px; display: none;" />
                </div>

                <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" CssClass="btn btn-success mt-3" OnClick="btnAddProduct_Click" />
            </div>
        </div>

        <asp:GridView ID="ProductsGrid" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" OnRowCommand="ProductsGrid_RowCommand">
            <Columns>
                <asp:BoundField DataField="ProductId" HeaderText="Product ID" />
                <asp:BoundField DataField="Name" HeaderText="Product Name" />
                <asp:BoundField DataField="Price" HeaderText="Price (VND)" DataFormatString="{0:N0} ₫" HtmlEncode="False" />
                <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd}" />

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditProduct" CommandArgument='<%# Eval("ProductId") %>' CssClass="btn btn-warning btn-sm" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteProduct" CommandArgument='<%# Eval("ProductId") %>' CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Are you sure?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <script>
        function previewImage(input) {
            var file = input.files[0];
            var reader = new FileReader();

            reader.onload = function (e) {
                var preview = document.getElementById("imagePreview");
                preview.src = e.target.result;
                preview.style.display = "block";
            };

            if (file) {
                reader.readAsDataURL(file);
            }
        }
    </script>
</asp:Content>
