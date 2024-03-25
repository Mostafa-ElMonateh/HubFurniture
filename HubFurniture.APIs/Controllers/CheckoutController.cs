﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
namespace HubFurniture.APIs.Controllers
{
    [Route("[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IProductService _productService; //  an interface for product retrieval

        public CheckoutController(IConfiguration configuration, IProductService productService)
        {
            _configuration = configuration;
            _productService = productService;
        }

        [HttpPost("checkout-order")]
        public async Task<IActionResult> CheckoutOrder([FromBody] OrderDetails orderDetails)
        {
            if (orderDetails == null)
            {
                return BadRequest("Missing order details");
            }

            try
            {
                StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

                var products = await _productService.GetProductsAsync(); // GetProducts is asynchronous

                var lineItems = new List<SessionLineItemOptions>();
                foreach (var product in products)
                {
                    lineItems.Add(new SessionLineItemOptions
                    {
                        //Name = product.Name,
                        Quantity = product.Quantity,
                        //  Amount = (long)(product.Price * 100), // Convert to cents
                        //  Currency = "usd" // Replace with your currency
                    });
                }

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = lineItems,
                    Mode = "payment",
                    SuccessUrl = "",
                    CancelUrl = ""
                };

                var service = new SessionService();
                var session = await service.CreateAsync(options);

                return Ok(new { sessionId = session.Id });
            }
            catch (StripeException e)
            {
                return StatusCode(500, $"Error creating Stripe session: {e.Message}");
            }
        }
    }

    // Interface for retrieving products 
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }

    public class OrderDetails
    {
        [Required]  
        public string? Description { get; set; }

        [Required]  
        public decimal Amount { get; set; }
    }

    public class Product 
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

}