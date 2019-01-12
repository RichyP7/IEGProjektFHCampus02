using CommonServiceLib.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyCreatorService.DomainLogic.SurveyAggregate
{
    public class Question : Entity
    {
        public Question(string text, int id)
        {
            Text = text;
            this.Id = id;
        }

        public string Text { get; set; }
        public List<Answer> PossibleAnswers{get;set;}
    }
}
