<%@ Page Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="MishaMart.Website_Pages.EditProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Edit Product</h2>

        <asp:Panel ID="EditPanel" runat="server">
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
                <label>Upload New Description (.txt):</label>
                <asp:FileUpload ID="fileDescription" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label>Upload New Image:</label>
                <asp:FileUpload ID="fileImage" runat="server" CssClass="form-control" onchange="previewImage(this)" />
                <img id="imagePreview" src="#" alt="Current Image" class="mt-3" style="max-width: 300px; display: none;" />
            </div>

            <asp:Button ID="btnUpdateProduct" runat="server" Text="Update Product" CssClass="btn btn-primary mt-3" OnClick="btnUpdateProduct_Click" />
        </asp:Panel>
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
