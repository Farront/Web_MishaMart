using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace MishaMart.Website_Pages
{
    public partial class GetChartData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var data = new Dictionary<string, object>();

            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Sales Data for Chart
                var salesData = new Dictionary<string, List<object>> { { "labels", new List<object>() }, { "values", new List<object>() } };
                using (SqlCommand cmd = new SqlCommand("SELECT FORMAT(OrderDate, 'MMM') AS Month, SUM(TotalAmount) FROM Orders GROUP BY FORMAT(OrderDate, 'MMM') ORDER BY MIN(OrderDate)", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        salesData["labels"].Add(reader["Month"]);
                        salesData["values"].Add(reader["SUM(TotalAmount)"]);
                    }
                    reader.Close();
                }
                data["sales"] = salesData;

                // Top-Selling Products for Chart
                var productData = new Dictionary<string, List<object>> { { "labels", new List<object>() }, { "values", new List<object>() } };
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 5 P.Name, COUNT(O.ProductId) AS Sales FROM OrderDetails O JOIN Products P ON O.ProductId = P.ProductId GROUP BY P.Name ORDER BY Sales DESC", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        productData["labels"].Add(reader["Name"]);
                        productData["values"].Add(reader["Sales"]);
                    }
                    reader.Close();
                }
                data["products"] = productData;
            }

            // Convert to JSON
            Response.ContentType = "application/json";
            Response.Write(new JavaScriptSerializer().Serialize(data));
            Response.End();
        }
    }
}