using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using Polly.CircuitBreaker;
using Microsoft.Extensions.Configuration;

namespace BlackFriday.Controllers
{
    [Produces("application/json")]
    [Route("api/PaymentMethods")]
    public class PaymentMethodsController : Controller
    {
        #region Data
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PaymentMethodsController> _logger;
        private const string SUBURI = "api/AcceptedCreditCards";
        private const string HTTPCLIENTNAME = "DefaultConnection";
        private const string ALTERNATIVECLIENT = "AlternativeConnection";
        #endregion
        public PaymentMethodsController(ILogger<PaymentMethodsController> logger, IHttpClientFactory httpClientFactory,IConfiguration config)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IEnumerable<string>>  Get()
        {

            List<string> acceptedPaymentMethods = null;
            _logger.LogInformation("Accepted Paymentmethods");
            var client = _httpClientFactory.CreateClient(HTTPCLIENTNAME);
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.GetAsync(SUBURI);
            }
            catch (BrokenCircuitException ex)
            {
                response = await HandleBrokenCircuit( ex);
            }
            if (response.IsSuccessStatusCode)
            {
                acceptedPaymentMethods = response.Content.ReadAsAsync<List<string>>().Result;
                LogPaymentMethods(acceptedPaymentMethods);
            }
            return acceptedPaymentMethods;
        }
        private async Task<HttpResponseMessage> HandleBrokenCircuit(BrokenCircuitException ex)
        {
            _logger.LogError(" Backup Infrastructure Accepted Paymentmethods" + ex.Message);
            var client = _httpClientFactory.CreateClient(ALTERNATIVECLIENT);
            HttpResponseMessage response = await client.GetAsync( SUBURI);
            return response;
        }
        private void LogPaymentMethods(List<string> acceptedPaymentMethods)
        {
            foreach (var item in acceptedPaymentMethods)
            {
                _logger.LogError("Paymentmethod {0}", new object[] { item });
            }
        }
    }
}