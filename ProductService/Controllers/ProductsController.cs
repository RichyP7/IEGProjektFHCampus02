using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Products.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        [HttpGet]
        
        public IEnumerable<string> Get()
        {
            return new string[] { "Coca Cola", "Fanta", "Sprite", "Soda" };
        }
        
    }
}
