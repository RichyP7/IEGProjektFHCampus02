using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        // GET api/payments
        [HttpGet]
        public ActionResult<IEnumerable<Payment>> Get()
        {
            
            return new Payment[]
            {
                new Payment (){ Id = "paypal", Name = "Paypal", Description = "Paypal"},
                new Payment (){ Id = "sofort", Name = "Sofortüberweisung", Description = "Sofortüberweisung"}
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
