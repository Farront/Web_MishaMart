using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;
using System.Drawing;
using System.IO;

namespace MishaMart.Website_Pages
{
    public partial class ConfirmOrder : System.Web.UI.Page
    {
        private const decimal ExchangeRate = 26000; // Static exchange rate: 1 USD = 25,908 VND

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrderDetails();
            }
        }

        private void LoadOrderDetails()
        {
            string orderId = Request.QueryString["OrderId"];
            if (string.IsNullOrEmpty(orderId))
            {
                Response.Redirect("~/Website_Pages/Default.aspx");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string orderQuery = "SELECT O.OrderId, O.OrderDate, O.TotalAmount FROM Orders O WHERE O.OrderId = @OrderId";
                using (SqlCommand cmd = new SqlCommand(orderQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        lblOrderNumber.Text = reader["OrderId"].ToString();
                        lblOrderDate.Text = Convert.ToDateTime(reader["OrderDate"]).ToString("MMMM dd, yyyy");

                        decimal totalPriceUSD = Convert.ToDecimal(reader["TotalAmount"]);
                        decimal totalPriceVND = totalPriceUSD * ExchangeRate;
                        lblTotalAmountVND.Text = string.Format("{0:N0} ₫", totalPriceVND);
                    }
                    reader.Close();
                }

                string itemsQuery = @"SELECT P.Name AS ProductName, O.Price AS Price 
                                      FROM OrderDetails O 
                                      INNER JOIN Products P ON O.ProductId = P.ProductId 
                                      WHERE O.OrderId = @OrderId";

                using (SqlCommand cmd = new SqlCommand(itemsQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable itemsTable = new DataTable();
                    itemsTable.Load(reader);
                    OrderItemsRepeater.DataSource = itemsTable;
                    OrderItemsRepeater.DataBind();
                }

                GenerateQRCode(orderId);
            }
        }

        private void GenerateQRCode(string orderId)
        {
            string qrCodeData = $"https://yourpaymentgateway.com/pay?orderId={orderId}";

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeInfo = qrGenerator.CreateQrCode(qrCodeData, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeInfo);

                using (Bitmap qrCodeImageBitmap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        qrCodeImageBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        string base64Image = Convert.ToBase64String(ms.ToArray());
                        qrCodeImage.ImageUrl = "data:image/png;base64," + base64Image;
                    }
                }
            }
        }

        protected void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Website_Pages/ThankYou.aspx");
        }
    }
}
