using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLib.Dto;
using Microsoft.AspNetCore.Mvc;
using SurveyCreatorService.Application;
using SurveyCreatorService.DomainLogic.SurveyAggregate;

namespace SurveyCreatorService.Controllers
{
    [Route("api/surveys")]
    [ApiController]
    public class SurveyCreatorController : ControllerBase
    {
        private readonly ISurveyService service;
        private readonly Random random;

        public SurveyCreatorController(ISurveyService service)
        {
            this.service = service;
            random = new Random();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<SurveyDto>> Get()
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
        private SurveyDto CreateDto(Survey newSurvey)
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
