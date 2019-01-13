using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLib.Dto;
using Microsoft.AspNetCore.Mvc;

namespace SurveyAnalyzeService.Controllers
{
    [Route("api/surveys/analyze")]
    public class SurveyAnalyzeController : Controller
    {
        [HttpPost]
        public ActionResult<SurveyAnalyzeDto> AnalyzeSurveys([FromBody]List<SurveyDto> surveys)
        {
            return Ok(Analyze(surveys));
        }

        private SurveyAnalyzeDto Analyze(List<SurveyDto> surveys)
        {
            SurveyAnalyzeDto analyzeDto = new SurveyAnalyzeDto();
            int questions = 0;
            surveys.ForEach(it => questions += it.SurveyQuestions.Count);
            analyzeDto.OverallQuestionCount = questions;
            analyzeDto.SurveyCount = surveys.Count;
            return analyzeDto;
        }
    }
}