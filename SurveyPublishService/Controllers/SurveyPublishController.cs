﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CommonServiceLib.Discovery;
using CommonServiceLib.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OpenTracing;

namespace SurveyPublishService.Controllers
{
    

    [Route("api/surveys/publish")]
    [ApiController]
    public class SurveyPublishController : ControllerBase
    {
        private readonly ITracer tracer;

        public SurveyPublishController(ITracer tracer)
        {
            this.tracer = tracer;
        }

        private readonly ApiClient apiClient = new ApiClient();

        [HttpGet]
        public ActionResult<SurveyAnalyzeDto> Get()
        {
            List<SurveyDto> surveys = GetSurveys();
            return GetAnalyzedSurveys(surveys);
        }


        private List<SurveyDto> GetSurveys() {
            using (IScope scope = tracer.BuildSpan("getting surveys").StartActive(finishSpanOnDispose: true))
            {
                List<SurveyDto> surveys = null;
                //HttpResponseMessage response = apiClient.client.GetAsync(apiClient.GetLoadBalancedUrl("survey-creator-service") + "api/surveys").Result;
                HttpResponseMessage response = apiClient.client.GetAsync("http://localhost:55463/api/surveys").Result;
                if (response.IsSuccessStatusCode)
                {
                    surveys = response.Content.ReadAsAsync<List<SurveyDto>>().Result;
                }
                return surveys;
            }
        }

        private SurveyAnalyzeDto GetAnalyzedSurveys(List<SurveyDto> surveys)
        {
            using (IScope scope = tracer.BuildSpan("getting surveys").StartActive(finishSpanOnDispose: true))
            {
                //HttpResponseMessage response = apiClient.client.PostAsJsonAsync(apiClient.GetLoadBalancedUrl("survey-analyze-service") + "api/surveys/analyze", surveys).Result;
                HttpResponseMessage response = apiClient.client.PostAsJsonAsync("http://localhost:55474/api/surveys/analyze", surveys).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<SurveyAnalyzeDto>().Result;
            }
        }
    }
}
