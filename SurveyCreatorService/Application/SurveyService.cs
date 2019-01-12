using CommonServiceLib.Dto;
using SurveyCreatorService.DomainLogic.SurveyAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyCreatorService.Application
{
    public class SurveyService : ISurveyService
    {
        private readonly Random random;
        public SurveyService()
        {
            random = new Random();
        }

        public Survey CreateSurvey()
        {
            return CreateSurveyFromDto();
        }

        private Survey CreateSurveyFromDto()
        {
            Survey survey = new Survey(random.Next(50, 1000));
            List<Question> surveyQuestionsToAdd = new List<Question>();
            foreach (var randomQuestion in GetQuestions())
            {
                surveyQuestionsToAdd.Add(new Question(randomQuestion,random.Next(1000,5000)));
            }
            survey.Questions = surveyQuestionsToAdd;
            return survey;
        }

        private static string[] GetQuestions()
        {
            return new string[] { "test", "What does the fox say" };
        }
    }
}
