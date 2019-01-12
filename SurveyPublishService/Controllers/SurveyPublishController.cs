using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CommonServiceLib.Discovery;
using CommonServiceLib.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace SurveyPublishService.Controllers
{
    

    [Route("api/surveys/publish")]
    [ApiController]
    public class SurveyPublishController : ControllerBase
    {
        private readonly ApiClient apiClient = new ApiClient();

        [HttpGet]
        public ActionResult<SurveyAnalyzeDto> Get()
        {
            List<SurveyDto> surveys = GetSurveys();
            return GetAnalyzedSurveys(surveys);
        }


        private List<SurveyDto> GetSurveys() {
            List<SurveyDto> surveys = null;
            HttpResponseMessage response = apiClient.client.GetAsync(apiClient.GetLoadBalancedUrl("survey-creator-service") + "api/surveys").Result;
            if (response.IsSuccessStatusCode)
            {
                surveys = response.Content.ReadAsAsync<List<SurveyDto>>().Result;
            }
            return surveys;
        }

        private SurveyAnalyzeDto GetAnalyzedSurveys(List<SurveyDto> surveys)
        {
            SurveyAnalyzeDto surveyAnalyze = null;
            HttpResponseMessage response = apiClient.client.PostAsync(apiClient.GetLoadBalancedUrl("survey-analyze-service") + "api/surveys/analyze", new StringContent(JArray.FromObject(surveys).ToString())).Result;
            if (response.IsSuccessStatusCode)
            {
                surveyAnalyze = response.Content.ReadAsAsync<SurveyAnalyzeDto>().Result;
            }
            return surveyAnalyze;
        }
    }
}
