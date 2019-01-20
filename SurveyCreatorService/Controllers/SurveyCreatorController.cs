using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLib.Dto;
using Microsoft.AspNetCore.Mvc;
using SurveyCreatorService.Application;
using SurveyCreatorService.DomainLogic.SurveyAggregate;
using OpenTracing;

namespace SurveyCreatorService.Controllers
{
    [Route("api/surveys")]
    [ApiController]
    public class SurveyCreatorController : ControllerBase
    {
        private readonly ISurveyService service;
        private readonly Random random;
        private readonly ITracer tracer;

        public SurveyCreatorController(ISurveyService service, ITracer tracer)
        {
            this.service = service;
            random = new Random();
            this.tracer = tracer;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<SurveyDto>> Get()
        {
            using (IScope scope = tracer.BuildSpan("creating survey").StartActive(finishSpanOnDispose: true))
            {
                List<SurveyDto> survey = new List<SurveyDto>();
                for (int i = 0; i < random.Next(1, 10); i++)
                {

                    var newSurvey = service.CreateSurvey();
                    survey.Add(CreateDto(newSurvey));
                }
                if (!survey.Any())
                    return NoContent();
                return Ok(survey);
            }
        }
        private SurveyDto CreateDto(Survey newSurvey)
        {
            using (IScope scope = tracer.BuildSpan("add Questions").StartActive(finishSpanOnDispose: true))
            {
                return new SurveyDto()
                {
                    Id = newSurvey.Id,
                    SurveyQuestions = new List<QuestionDto>()
                {
                    new QuestionDto(2,"test")
                }
                };//ToDo Add questions
            }
        }
    }


}
