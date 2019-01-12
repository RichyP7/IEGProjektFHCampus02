using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLib.Discovery;
using CommonServiceLib.Dto;
using Microsoft.AspNetCore.Mvc;

namespace SurveyPublishService.Controllers
{
    

    [Route("api/surveys")]
    [ApiController]
    public class SurveyPublishController : ControllerBase
    {
        private readonly ApiClient apiClient = new ApiClient();

        [HttpGet]
        public ActionResult<SurveyAnalyzeDto> Get()
        {
            string surveyCreatorService = apiClient.GetLoadBalancedUrl("survey-creator-service");
            //TODO: get all from surveys from survey creator
            //TODO: send all surveys to survey analyzer
            //TODO: get response and return to client
            return new SurveyAnalyzeDto();
        }
    }
}
