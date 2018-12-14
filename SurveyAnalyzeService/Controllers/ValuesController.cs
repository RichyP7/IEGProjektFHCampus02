using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLib.Discovery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SurveyAnalyzeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApiClient apiClient = new ApiClient(); 


        // GET api/values
        [HttpGet]
        public ActionResult<Dictionary<string, List<Uri>>> Get()
        {
            return apiClient.serverUrls;
           // return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{serviceId}")]
        public ActionResult<string> Get(string serviceId)
        {
            return apiClient.GetLoadBalancedUrl(serviceId);
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
