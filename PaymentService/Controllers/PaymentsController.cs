using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Data;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {

        private readonly PaymentStorage paymentStorage;

        public PaymentsController(PaymentStorage paymentStorage)
        {
            this.paymentStorage = paymentStorage;
        }

        // GET api/payments
        [HttpGet]
        public ActionResult<IEnumerable<Payment>> Get()
        {
            return paymentStorage.Payments;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<IEnumerable<Payment>> Post([FromBody] Payment payment)
        {
            paymentStorage.Payments.Add(payment);
            return paymentStorage.Payments;
        }
    }
}
