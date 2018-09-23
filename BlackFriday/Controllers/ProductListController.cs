using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlackFriday.Controllers
{
    [Route("api/[controller]")]
    public class ProductListController : Controller
    {
        private readonly ILogger<ProductListController> _logger;
        private static readonly string productServiceBaseAddress = "https://productservice20180922114817.azurewebsites.net/";

        public ProductListController(ILogger<ProductListController> logger)
        {
            _logger = logger;
        }
        // GET: http://iegblackfriday.azurewebsites.net/api/productlist
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> products = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(productServiceBaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(productServiceBaseAddress + "/api/products").Result;
            if (response.IsSuccessStatusCode)
            {
                products = response.Content.ReadAsAsync<List<string>>().Result;
            }
            foreach (var item in products)
            {
                _logger.LogError("Product {0}", new object[] { item });
            }
            return products;
        }
        
    }
}
