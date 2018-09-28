using BusinessLogic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Data
{
    public class PaymentStorage
    {

        List<Payment> payments;

        public PaymentStorage()
        {
            Payments = new List<Payment>()
            {
                new Payment() { Id = "paypal", Name = "Paypal", Description = "Paypal" },
                new Payment() { Id = "sofort", Name = "Sofortüberweisung", Description = "Sofortüberweisung" }
            };

        }

        public List<Payment> Payments { get => payments; set => payments = value; }
    }
}
