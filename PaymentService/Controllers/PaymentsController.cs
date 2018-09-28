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
        static List<Payment> payments;

        public PaymentsController()
        {
            payments = new List<Payment>()
            {
                new Payment() { Id = "paypal", Name = "Paypal", Description = "Paypal" },
                new Payment() { Id = "sofort", Name = "Sofortüberweisung", Description = "Sofortüberweisung" }
            };

        }

        public static List<Payment> Payments { get => payments; set => payments = value; }

        // GET api/payments
        [HttpGet]
        public ActionResult<IEnumerable<Payment>> Get()
        {
            return Payments;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<IEnumerable<Payment>> Post([FromBody] Payment payment)
        {
            Payments.Add(payment);
            return Payments;
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
