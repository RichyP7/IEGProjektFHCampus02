using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BlackFriday.Controllers
{
    [Produces("application/json")]
    [Route("api/PaymentMethods")]
    public class PaymentMethodsController : Controller
    {
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
        #region Data
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PaymentMethodsController> _logger;
        private static readonly string[] creditcardServiceBaseAddress = {"https://iegeasycreditcardservice20180922084919.azurewebsites.net/",
                                                                         "https://iegeasycreditcardservice20180922124832v2.azurewebsites.net/"};
        #endregion
        public PaymentMethodsController(ILogger<PaymentMethodsController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> acceptedPaymentMethods = null;//= new string[] { "Diners", "Master" };
            _logger.LogError("Accepted Paymentmethods");
            var client = _httpClientFactory.CreateClient("CreditCardService");

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(creditcardServiceBaseAddress[0]);
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



            HttpResponseMessage response = client.GetAsync("/api/AcceptedCreditCards").Result;
            if (response.IsSuccessStatusCode)
            {
                acceptedPaymentMethods = response.Content.ReadAsAsync<List<string>>().Result;
            }
          
            foreach (var item in acceptedPaymentMethods)
            {
                _logger.LogError("Paymentmethod {0}", new object[] { item });

            }
            return acceptedPaymentMethods;
        }
        public void DoSomething()
        {

        }
    }
}