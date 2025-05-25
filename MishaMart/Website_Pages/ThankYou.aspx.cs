using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MishaMart.Website_Pages
{
	public partial class ThankYou : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrderDetails();
            }
        }

        private void LoadOrderDetails()
        {
            // Fetch the order ID from the query string
            string orderId = Request.QueryString["OrderId"];

            // Display the order ID on the page
            if (!string.IsNullOrEmpty(orderId))
            {
                lblOrderNumber.Text = orderId;
            }
            else
            {
                lblOrderNumber.Text = "Unknown"; // Fallback in case the OrderId is missing
            }
        }

        protected void btnContinueShopping_Click(object sender, EventArgs e)
        {
            // Redirect to Default.aspx
            Response.Redirect("~/Default.aspx");
        }

        protected void btnViewMyOrder_Click(object sender, EventArgs e)
        {
            // Redirect to Checkout.aspx with the order ID (if available)
            string orderId = lblOrderNumber.Text;
            if (!string.IsNullOrEmpty(orderId) && orderId != "Unknown")
            {
                Response.Redirect($"~/Checkout.aspx?OrderId={orderId}");
            }
            else
            {
                Response.Redirect("~/Checkout.aspx");
            }
        }
    }
}