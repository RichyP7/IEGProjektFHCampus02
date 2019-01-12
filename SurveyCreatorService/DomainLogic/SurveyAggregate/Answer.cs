using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyCreatorService.DomainLogic.SurveyAggregate
{
    public class Answer
    {

        public bool IsRightAnswer { get;}
        public string Text { get; set; }
        public Answer(string text,bool isrightAnswer)
        {
            Text = text;
            this.IsRightAnswer = isrightAnswer;
        }
    }
}
